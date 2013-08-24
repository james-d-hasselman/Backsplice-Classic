using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backsplice;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Windows.Forms;

namespace backsplicelib
{
    class Spreadsheet : Roster
    {
        // ConstantsC:\Users\James D. Hasselman\Documents\Visual Studio 2010\Projects\Backsplice\src\Backsplice\ExcelSpreadsheet.cs
        private const int M_NAME_COLUMN = 0;
        private const int M_TROOP_COLUMN = 1;
        private const int cm_intSTART_ROW = 2;
        private const string M_NAME_COLUMN_STRING = "A";
        private const string M_TROOP_COLUMN_STRING = "B";

        private string m_strFilePath;
        private bool m_blnIsEditable;
        private bool m_blnDisposed = false;
        private bool m_blnIsNew = false;
        private string m_strProgram = "";
        private string m_strPeriod = "";

        /* OpenXML SDK 2.0 Code - Added 11/9/2010 */
        private SpreadsheetDocument m_ssdSpreadsheet;
        private WorksheetPart m_wspWorksheet;
        private SheetData m_sdSheetData;
        private List<Scout> m_scouts;

        public override void Open(string file)
        {
            /* OpenXML SDK 2.0 Code - Added 11/2/2010 */

            // Save the filename and read-only status
            m_strFilePath = file;
            /* (The SpreadsheetDocument class of the OpenXML SDK 2.0 uses
             * an isEditable property to describe the read only status of the document
             * in order for a document to be read-only, isEditable must be set to false
             * and vice-versa
             */

            if (System.IO.File.Exists(m_strFilePath))
            {
                m_ssdSpreadsheet = SpreadsheetDocument.Open(m_strFilePath, true);
            }
            else
            {
                // The spreadsheet is new
                m_blnIsNew = true;

                // Create the file
                m_ssdSpreadsheet = SpreadsheetDocument.Create(m_strFilePath, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook);
                WorkbookPart workbookPart = m_ssdSpreadsheet.AddWorkbookPart();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                string relId = workbookPart.GetIdOfPart(worksheetPart);
                Workbook workbook = new Workbook();
                FileVersion fileVersion = new FileVersion { ApplicationName = "Microsoft Office Excel" };
                Worksheet worksheet = new Worksheet();
                SheetData sheetData = new SheetData();
                // Create the columns
                Columns colsColumns = new Columns();
                Column colNameColumn = new Column();
                colNameColumn.Min = 1;
                colNameColumn.Max = 1;
                colNameColumn.Width = 1;
                colNameColumn.CustomWidth = true;
                Column colTroopColumn = new Column();
                colTroopColumn.Min = 2;
                colTroopColumn.Max = 2;
                colTroopColumn.Width = 1;
                colTroopColumn.CustomWidth = true;
                colsColumns.Append(colNameColumn);
                colsColumns.Append(colTroopColumn);
                // Add the column headers
                Row rHeaderRow = new Row();
                Cell cNameHeader = new Cell();
                cNameHeader.CellReference = M_NAME_COLUMN_STRING + 1;
                cNameHeader.CellValue = new CellValue("Name");
                cNameHeader.DataType = CellValues.String;
                Cell cTroopHeader = new Cell();
                cTroopHeader.CellReference = M_TROOP_COLUMN_STRING + 1;
                cTroopHeader.CellValue = new CellValue("Troop");
                cTroopHeader.DataType = CellValues.String;
                rHeaderRow.AppendChild(cNameHeader);
                rHeaderRow.AppendChild(cTroopHeader);
                sheetData.AppendChild(rHeaderRow);

                worksheet.Append(colsColumns);
                worksheet.Append(sheetData);
                worksheetPart.Worksheet = worksheet;
                worksheetPart.Worksheet.Save();
                Sheets sheets = new Sheets();
                Sheet sheet = new Sheet { Name = "Sheet1", SheetId = 1, Id = relId };
                sheets.Append(sheet);
                workbook.Append(fileVersion);
                workbook.Append(sheets);
                m_ssdSpreadsheet.WorkbookPart.Workbook = workbook;
            }

            // Get the worksheet
            m_wspWorksheet = m_ssdSpreadsheet.WorkbookPart.WorksheetParts.First();
            // Get the sheet data
            m_sdSheetData = m_wspWorksheet.Worksheet.Elements<SheetData>().First();

            // Add all the scouts to the scout list.
            m_scouts = new List<Scout>();
            foreach (Row row in m_sdSheetData)
            {
                Cell nameCell = (Cell)row.ChildElements[M_NAME_COLUMN];
                Cell troopCell = (Cell)row.ChildElements[M_TROOP_COLUMN];

                if (nameCell.CellValue != null && troopCell.CellValue != null)
                {
                    Scout scout = new Scout(nameCell.CellValue.Text, troopCell.CellValue.Text);
                    m_scouts.Add(scout);
                }
            }
        }

        public override void Close()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Realeases all unmanaged resourses if they haven't been cleaned up already
        /// </summary>
        /// <param name="_blnDisposing">whether or not to preform clean up</param>
        protected virtual void Dispose(bool _blnDisposing)
        {
            if (!m_blnDisposed)
            {
                if (_blnDisposing)
                {
                    m_ssdSpreadsheet.Dispose();
                }

                m_blnDisposed = true;
            }
        }

        /// <summary>
        /// Gets the witdth of the text
        /// </summary>
        /// <param name="font">text font</param>
        /// <param name="fontSize">text size</param>
        /// <param name="text">the text</param>
        /// <returns></returns>
        private static double GetWidth(string font, int fontSize, string text)
        {
            System.Drawing.Font stringFont = new System.Drawing.Font(font, fontSize);
            return GetWidth(stringFont, text);
        }

        /// <summary>
        /// Gets the width of the text
        /// </summary>
        /// <param name="stringFont">text font</param>
        /// <param name="text">the text</param>
        /// <returns></returns>
        private static double GetWidth(System.Drawing.Font stringFont, string text)
        {
            // This formula is based on this article plus a nudge ( + 0.2M )
            // http://msdn.microsoft.com/en-us/library/documentformat.openxml.spreadsheet.column.width.aspx
            // Truncate(((256 * Solve_For_This + Truncate(128 / 7)) / 256) * 7) = DeterminePixelsOfString

            Size textSize = TextRenderer.MeasureText(text, stringFont);
            double width = (double)(((textSize.Width / (double)7) * 256) - (128 / 7)) / 256;
            width = (double)decimal.Round((decimal)width + 0.2M, 2);

            return width;
        }

        /// <summary>
        /// Calculates the best width for a column
        /// </summary>
        /// <param name="_intColumn">a column in the spreadsheet</param>
        /// <returns></returns>
        private double CalculateBestWidth(int _intColumn)
        {
            double dblWidth = 0;

            // Go through each row
            for (int i = 0; i < m_sdSheetData.Count(); i++)
            {
                Row rRow = (Row)m_sdSheetData.ChildElements[i];
                if (_intColumn < rRow.ChildElements.Count)
                {
                    Cell cCell = (Cell)rRow.ChildElements[_intColumn];

                    // Find out which cell contains the longest text
                    double dblCellWidth = GetWidth("Arial", 12, cCell.CellValue.Text);
                    if (dblCellWidth > dblWidth)
                    {
                        dblWidth = dblCellWidth;
                    }
                }
            }

            return dblWidth;
        }

        /// <summary>
        /// Set the spreadsheet header
        /// </summary>
        private void SetHeader()
        {
            HeaderFooter hfHeaderFooter = m_wspWorksheet.Worksheet.Descendants<HeaderFooter>().FirstOrDefault();

            if (hfHeaderFooter == null)
            {
                hfHeaderFooter = new HeaderFooter();
                hfHeaderFooter.AppendChild<OddHeader>(new OddHeader());
                hfHeaderFooter.AppendChild<EvenHeader>(new EvenHeader());
                m_wspWorksheet.Worksheet.AppendChild<HeaderFooter>(hfHeaderFooter);
            }

            string strLeftHeader = "&L" + m_strProgram + " " + m_strPeriod;
            string strRightHeader = "&RPage &P of &N";

            hfHeaderFooter.EvenHeader.Text = strLeftHeader + strRightHeader;
            hfHeaderFooter.OddHeader.Text = strLeftHeader + strRightHeader;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Spreadsheet()
        {
            Dispose(false);
        }

        public override void Save()
        {
            if (m_blnIsNew)
            {
                SetHeader();
            }

            Columns colsColumns;

            // TODO verify that this is necessary. Requires more research.
            try
            {
                colsColumns = m_wspWorksheet.Worksheet.Elements<Columns>().First();
            }
            catch (InvalidOperationException)
            {
                colsColumns = new Columns();
                colsColumns.AppendChild(new Column());
                colsColumns.AppendChild(new Column());
                m_wspWorksheet.Worksheet.AppendChild(colsColumns);
            }

            for (int i = 0; i < colsColumns.Count(); i++)
            {
                Column colColumn = (Column)colsColumns.ChildElements[i];
                //colColumn.CustomWidth = true;
                colColumn.Width = CalculateBestWidth(i);
                //colColumn.Min = (uint)(i + 1);
                //colColumn.Max = (uint)(i + 1);
            }

            m_wspWorksheet.Worksheet.Save();
        }

        public override void AddScout(Scout scout)
        {
            m_scouts.Add(scout);
        }

        public override void RemoveScout(Scout scout)
        {
            m_scouts.Remove(scout);
        }

        /// <summary>
        /// Set the period
        /// </summary>
        /// <param name="_strPeriod">period</param>
        public override void SetPeriod(string period)
        {
            m_strPeriod = period;
            SetHeader();
        }

        /// <summary>
        /// Set the program
        /// </summary>
        /// <param name="_strProgram">program</param>
        public override void SetProgram(string program)
        {
            m_strProgram = program;
            SetHeader();
        }

        public override void SetWeek(string week)
        {
            throw new NotImplementedException();
        }

        public override void Sort(IComparer<Scout> scoutComparer)
        {
            m_scouts.Sort(scoutComparer);
        }

        public override bool IsCompatible(string file)
        {
            throw new NotImplementedException();
        }
    }
}
