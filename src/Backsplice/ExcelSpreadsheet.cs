using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Backsplice
{
    /// <summary>
    /// Excel Spreadsheet
    /// </summary>
    class ExcelSpreadsheet : IDisposable
    {
        // ConstantsC:\Users\James D. Hasselman\Documents\Visual Studio 2010\Projects\Backsplice\src\Backsplice\ExcelSpreadsheet.cs
        private const int cm_intNAME_COLUMN = 0;
        private const int cm_intTROOP_COLUMN = 1;
        private const int cm_intSTART_ROW = 2;
        private const string cm_strNAME_COLUMN = "A";
        private const string cm_strTROOP_COLUMN = "B";
        
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

        /// <summary>
        /// The first row in the spreadsheet
        /// </summary>
        public static int StartRow
        {
            get
            {
                return cm_intSTART_ROW;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_strFilePath">path to an excel spreadsheet</param>
        /// <param name="_blnReadOnly">read only status</param>
        public ExcelSpreadsheet(string _strFilePath, bool _blnReadOnly)
        {
            /* OpenXML SDK 2.0 Code - Added 11/2/2010 */

            // Save the filename and read-only status
            m_strFilePath = _strFilePath;
            /* (The SpreadsheetDocument class of the OpenXML SDK 2.0 uses
             * an isEditable property to describe the read only status of the document
             * in order for a document to be read-only, isEditable must be set to false
             * and vice-versa
             */
            m_blnIsEditable = !_blnReadOnly;

            if (System.IO.File.Exists(m_strFilePath))
            {
                m_ssdSpreadsheet = SpreadsheetDocument.Open(m_strFilePath, m_blnIsEditable);
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
                cNameHeader.CellReference = NameColumnString + 1;
                cNameHeader.CellValue = new CellValue("Name");
                cNameHeader.DataType = CellValues.String;
                Cell cTroopHeader = new Cell();
                cTroopHeader.CellReference = TroopColumnString + 1;
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
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~ExcelSpreadsheet()
        {
            Dispose(false);
        }

        /// <summary>
        /// Saves the content, closes the document, and releases all unmanaged resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Sets the value of the name column
        /// </summary>
        /// <param name="_intRow">row in the spreadsheet</param>
        /// <param name="_strName">name</param>
        public void SetName(int _intRow, string _strName)
        {
            /* OpenXML SDK 2.0 Code - Added 11/9/2010 */
            
            int intRow = _intRow - 1;

            Cell cCell = GetCell(intRow, NameColumn, NameColumnString);

            // Set the cell value
            cCell.CellValue = new CellValue(_strName);
        }

        /// <summary>
        /// Takes a row number, column number, and column string as input. If the
        /// the row and column already exist it returns the existing cell. If not,
        /// it adds a new row, a new cell, and returns the new cell.
        /// </summary>
        /// <param name="intRow">The row to retrieve</param>
        /// <param name="column">The column to retreive</param>
        /// <param name="columnString">The string name of the column (such as 'A')</param>
        /// <returns></returns>
        private Cell GetCell(int intRow, int column, string columnString)
        {
            // Get or create the row
            Row rRow;

            if (intRow >= 0 && intRow < m_sdSheetData.ChildElements.Count)
            {
                rRow = (Row)m_sdSheetData.ChildElements[intRow];
            }
            else
            {
                rRow = new Row();
                rRow.RowIndex = (uint)intRow + 1;
                m_sdSheetData.AppendChild(rRow);
            }

            // Get or create the cell
            Cell cCell;

            if (column < rRow.ChildElements.Count)
            {
                cCell = (Cell)rRow.ChildElements[column];
            }
            else
            {
                cCell = new Cell();
                // Set the cell reference
                cCell.CellReference = columnString + (intRow + 1);
                // Set the cell type
                cCell.DataType = CellValues.String;
                rRow.AppendChild(cCell);
            }

            return cCell;
        }

        /// <summary>
        /// Sets the value of the troop column
        /// </summary>
        /// <param name="_intRow">row in the spreadsheet</param>
        /// <param name="_strTroop">troop</param>
        public void SetTroop(int _intRow, string _strTroop)
        {
            /* OpenXML SDK 2.0 Code - Added 11/9/2010 */

            int intRow = _intRow - 1;

            Cell cCell = GetCell(intRow, TroopColumn, TroopColumnString);

            // Set the cell value
            int intResult;
            if (int.TryParse(_strTroop, out intResult))
            {
                cCell.DataType = CellValues.Number;
            }
            else
            {
                cCell.DataType = CellValues.String;
            }

            cCell.CellValue = new CellValue(_strTroop);
        }

        /// <summary>
        /// Gets the value of the name column
        /// </summary>
        /// <param name="_intRow">row in the spreadsheet</param>
        /// <returns></returns>
        public string GetName(int _intRow)
        {
            /* OpenXML SDK 2.0 Code - Added 11/9/2010 */

            int intRow = _intRow - 1;

            // Get the row
            Row rRow = (Row)m_sdSheetData.ChildElements[intRow];
            // Get the cell
            Cell cCell = (Cell)rRow.ChildElements[NameColumn];

            return cCell.CellValue.Text;
        }

        /// <summary>
        /// Gets the value of the troop column
        /// </summary>
        /// <param name="_intRow">row in the spreadsheet</param>
        /// <returns></returns>
        public string GetTroop(int _intRow)
        {
            /* OpenXML SDK 2.0 Code - Added 11/9/2010 */

            int intRow = _intRow - 1;

            // Get the row
            Row rRow = (Row)m_sdSheetData.ChildElements[intRow];
            // Get the cell
            Cell cCell = (Cell)rRow.ChildElements[TroopColumn];

            return cCell.CellValue.Text;
        }

        /// <summary>
        /// Inserts a scout into the spreadsheet
        /// </summary>
        /// <param name="_intRow">a row in the spreadsheet</param>
        /// <param name="_strFirstName">a scout's first name</param>
        /// <param name="_strLastName">a scout's last name</param>
        /// <param name="_strTroop">the scout's troop</param>
        /// <param name="_strMeritBadge">a camp program</param>
        /// <param name="_strPeriod">a program period</param>
        public void InsertRow(int _intRow, string _strFirstName, string _strLastName, string _strTroop)
        {
            /* OpenXML SDK 2.0 Code - Added 11/10/2010 */

            int intRow = _intRow - 1;

            // Create a cell for the name and troop
            Cell cName = new Cell();
            cName.CellValue = new CellValue(_strLastName + ", " + _strFirstName);
            cName.DataType = CellValues.String;
            cName.CellReference = NameColumnString + (intRow + 1);

            Cell cTroop = new Cell();
            cTroop.CellValue = new CellValue(_strTroop);
            cTroop.DataType = CellValues.Number;
            cTroop.CellReference = TroopColumnString + (intRow + 1);

            // Create a row from the cells
            Row rRow = new Row();
            rRow.RowIndex = (uint)intRow + 1;
            rRow.Append(cName);
            rRow.Append(cTroop);

            // If the row is being inserted at the end of the sheet
            if (intRow >= m_sdSheetData.ChildElements.Count())
            {
                // Append the row to the end of the worksheet
                m_sdSheetData.Append(rRow);
            }
            else
            {
                // Insert the row into the worksheet
                m_sdSheetData.InsertAt(rRow, intRow);

                // Update the index of all rows
                for (int i = intRow + 1; i < m_sdSheetData.Count(); i++)
                {
                    Row rARow = (Row)m_sdSheetData.ChildElements[i];
                    rARow.RowIndex = rARow.RowIndex + 1;

                    // Update the cell reference for all cells
                    for (int j = 0; j < rARow.ChildElements.Count(); j++)
                    {
                        Cell cACell = (Cell)rARow.ChildElements[j];
                        char chrColumn = cACell.CellReference.ToString()[0];
                        cACell.CellReference = chrColumn + rARow.RowIndex.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Removes an existing row from the DoubleKnot roster
        /// </summary>
        /// <param name="_intRow">a row in the DoubleKnot roster</param>
        public void RemoveRow(int _intRow)
        {
            /* OpenXML SDK 2.0 Code - Added 11/10/2010 */

            int intRow = _intRow - 1;

            // Get the row
            Row rRow = (Row)m_sdSheetData.ChildElements[intRow];

            // If the row is being removed from the end of the sheet
            if (intRow >= m_sdSheetData.ChildElements.Count())
            {
                // Remove the row from the worksheet
                m_sdSheetData.RemoveChild(rRow);
            }
            else
            {
                // Remove the row from the worksheet
                m_sdSheetData.RemoveChild(rRow);

                // Update the index of all rows
                for (int i = intRow; i < m_sdSheetData.Count(); i++)
                {
                    Row rARow = (Row)m_sdSheetData.ChildElements[i];
                    rARow.RowIndex = rARow.RowIndex - 1;

                    // Update the cell reference for all cells
                    for (int j = 0; j < rARow.ChildElements.Count(); j++)
                    {
                        Cell cACell = (Cell)rARow.ChildElements[j];
                        char chrColumn = cACell.CellReference.ToString()[0];
                        cACell.CellReference = chrColumn + rARow.RowIndex.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Set the period
        /// </summary>
        /// <param name="_strPeriod">period</param>
        public void SetPeriod(string _strPeriod)
        {
            m_strPeriod = _strPeriod;
            
            //m_strPeriod = m_strPeriod.Insert(m_strPeriod.IndexOf("&"), "&");

            SetHeader();
        }

        /// <summary>
        /// Set the program
        /// </summary>
        /// <param name="_strProgram">program</param>
        public void SetProgram(string _strProgram)
        {
            m_strProgram = _strProgram;
            SetHeader();
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
                    if (m_blnIsEditable)
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
            for(int i = 0; i < m_sdSheetData.Count(); i++)
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

        public int LastRow
        {
            get
            {
                return m_sdSheetData.ChildElements.Count;
            }
        }

        /// <summary>
        /// Gets the name column index
        /// </summary>
        private static int NameColumn
        {
            get
            {
                return cm_intNAME_COLUMN;
            }
        }

        /// <summary>
        /// Gets the string representing the name column
        /// </summary>
        private string NameColumnString
        {
            get { return cm_strNAME_COLUMN; }
        } 

        /// <summary>
        /// Gets the troop column index
        /// </summary>
        private static int TroopColumn
        {
            get
            {
                return cm_intTROOP_COLUMN;
            }
        }

        /// <summary>
        /// Gets the string representing the name column
        /// </summary>
        private string TroopColumnString
        {
            get { return cm_strTROOP_COLUMN; }
        } 
    }
}
