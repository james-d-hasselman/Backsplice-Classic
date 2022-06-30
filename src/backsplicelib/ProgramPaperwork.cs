using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backsplice;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace backsplicelib
{
    public class ProgramPaperwork : Roster
    {
        // ProgramPaperwork constants
        private const int M_LAST_ROW = 30;
        private const int M_START_ROW = 12;
        private const double cm_dblSHEET_SIZE = 19.0;
        private const int M_NAME_COLUMN = 0;
        private const int M_TROOP_COLUMN = 1;

        private string m_strWeek = "";
        private string m_strPeriod = "";
        private string m_strFilePath;
        private int m_intSelectedSheet = 0;
        private bool m_blnDisposed = false;
        private bool m_blnIsEditable;

        /* OpenXML SDK 2.0 Code - Added 11/3/2010 */

        private SpreadsheetDocument m_ssdProgramPaperwork;
        private WorkbookPart m_wbpWorkbook;
        private WorksheetPart[] m_wspWorksheets;
        private SheetData m_sdSheetData;
        private SharedStringTable m_sstSharedStringTable;
        private List<Scout> m_scouts;

        public override void Open(string file)
        {
            /* OpenXML SDK Code - Added 11/3/2010 */

            // Save the file name and read-only status
            m_strFilePath = file;
            /* (The SpreadsheetDocument class of the OpenXML SDK 2.0 uses
             * an isEditable property to describe the read only status of the document
             * in order for a document to be read-only, isEditable must be set to false
             * and vice-versa
             */
            m_blnIsEditable = true;

            // Open the spreadsheet document
            m_ssdProgramPaperwork = SpreadsheetDocument.Open(m_strFilePath, m_blnIsEditable);
            // Get the workbook
            m_wbpWorkbook = m_ssdProgramPaperwork.WorkbookPart;
            // Get all of the worksheets
            m_wspWorksheets = m_wbpWorkbook.WorksheetParts.ToArray();
            // Get the sheet data from the first worksheet
            m_sdSheetData = m_wspWorksheets[0].Worksheet.Elements<SheetData>().First();
            // Get the shared string table
            m_sstSharedStringTable = m_wbpWorkbook.SharedStringTablePart.SharedStringTable;
            // Update the page headers
            UpdatePageHeader();

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

        /// <summary>
        /// Updates the right headers of all sheets
        /// </summary>
        private void UpdatePageHeader()
        {
            // Reset all of the right headers
            m_wspWorksheets = m_ssdProgramPaperwork.WorkbookPart.WorksheetParts.ToArray();
            for (int i = 0; i < m_wspWorksheets.Length; i++)
            {
                string header = m_wspWorksheets[i].Worksheet.Descendants<HeaderFooter>().First().OddHeader.Text;
                string[] split = new string[] { "&L", "&C", "&R" };
                string[] headerParts = header.Split(split, StringSplitOptions.RemoveEmptyEntries);
                string rightHeader = "&R" + headerParts[2];
                header = header.Replace(rightHeader, "&R&\"Georgia,Regular\"Page&U__" + (i + 1) + "__&Uof&U__" + m_wspWorksheets.Length + "__ &\"Arial,Regular\"    &U            ");
                m_wspWorksheets[i].Worksheet.Descendants<HeaderFooter>().First().OddHeader.Text = header;
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
                    if (m_blnIsEditable)
                    {
                        for (int i = 0; i < m_wspWorksheets.Count(); i++)
                        {
                            m_wspWorksheets[i].Worksheet.Save();
                        }
                    }
                    m_ssdProgramPaperwork.Dispose();
                }

                m_blnDisposed = true;
            }
        }

        public override void Save()
        {
            // calculate the number of sheets needed
            int neededSheets = (int)Math.Ceiling(m_scouts.Count / cm_dblSHEET_SIZE);
            // get the number of sheets in the workbook
            int currentSheets = m_wspWorksheets.Length;
            int difference = neededSheets - currentSheets;

            if (difference > 0)
            {
                // Add sheets
                for (int i = 0; i < difference; i++)
                {
                    this.AddSheet();
                }
            }
            else if (difference < 0)
            {
                for (int i = difference; i < 0; i++)
                {
                    this.DeleteSheet(m_wspWorksheets.Length - 1);
                }
            }

            int rowNumber = M_START_ROW;
            int currentSheet = 0;
            SheetData data = m_wspWorksheets[currentSheet].Worksheet.Elements<SheetData>().First();

            // for each scout
            foreach (Scout scout in m_scouts)
            {
                // if there are already 19 scouts in the current sheet
                if (rowNumber > M_LAST_ROW)
                {
                    // select the new sheet
                    currentSheet++;
                    data = m_wspWorksheets[currentSheet].Worksheet.Elements<SheetData>().First();

                    rowNumber = M_START_ROW;
                }

                // add the scout to the current sheet.
                Row row = (Row)data.ChildElements[rowNumber];
                Cell nameCell = (Cell)row.ChildElements[M_NAME_COLUMN];
                nameCell.DataType = CellValues.String;
                nameCell.CellValue = new CellValue(scout.GetName());

                Cell troopCell = (Cell)row.ChildElements[M_TROOP_COLUMN];
                troopCell.DataType = CellValues.String;
                troopCell.CellValue = new CellValue(scout.GetTroopString());
            }

            for (int i = 0; i < m_wspWorksheets.Count(); i++)
            {
                m_wspWorksheets[i].Worksheet.Save();
            }
        }

        /// <summary>
        /// Adds a new sheet to the paperwork
        /// </summary>
        private void AddSheet()
        {
            /* OpenXML SDK 2.0 Code - Added 11/5/2010 */

            WorkbookPart workbookPart = m_ssdProgramPaperwork.WorkbookPart;
            WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();

            // Create the new sheet
            WorksheetPart newWorksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            // Get the ID of the new worksheet part
            string strNewWorksheetPartID = workbookPart.GetIdOfPart(newWorksheetPart);

            // Get the last sheet in the workbook
            Sheet theSheet = workbookPart.Workbook.Descendants<Sheet>().Last();
            // Create the sheet for the new worksheet part using the information contained in the last sheet
            Sheet sheet = new Sheet();
            // Set the new sheet's ID to the ID of the new worsheet part
            sheet.Id = strNewWorksheetPartID;
            sheet.Name = "Sheet" + (theSheet.SheetId.Value + 1);
            sheet.SheetId = theSheet.SheetId.Value + 1;
            // Add the new sheet to the end of the workbook
            workbookPart.Workbook.Sheets.Append(sheet);

            // Write the template worksheet to the newly created worksheet
            OpenXmlReader reader = OpenXmlReader.Create(worksheetPart);
            OpenXmlWriter writer = OpenXmlWriter.Create(newWorksheetPart);

            while (reader.Read())
            {
                if (reader.ElementType == typeof(CellValue) ||
                    reader.ElementType == typeof(OddHeader) ||
                    reader.ElementType == typeof(OddFooter))
                {
                    writer.WriteElement(reader.LoadCurrentElement());
                }
                else if (reader.IsStartElement)
                {
                    writer.WriteStartElement(reader);
                }
                else if (reader.IsEndElement)
                {
                    writer.WriteEndElement();
                }
            }

            reader.Close();
            writer.Close();

            // Empty the new worksheet
            SheetData sdSheetData = newWorksheetPart.Worksheet.Elements<SheetData>().First();
            for (int i = M_START_ROW; i <= M_LAST_ROW; i++)
            {
                Row rRow = (Row)sdSheetData.ChildElements[i];

                Cell cName = (Cell)rRow.ChildElements[0];
                Cell cTroop = (Cell)rRow.ChildElements[1];

                cName.DataType = CellValues.String;
                cName.CellValue = new CellValue("");
                cTroop.DataType = CellValues.String;
                cTroop.CellValue = new CellValue("");
            }

            // Save the changes to the new worksheet
            newWorksheetPart.Worksheet.Save();

            // Update the headers
            UpdateHeaders();
        }

        /// <summary>
        /// Updates the headers of all sheets
        /// </summary>
        private void UpdateHeaders()
        {
            UpdatePageHeader();
            UpdateWeekHeader();
            UpdatePeriodHeader();
        }

        /// <summary>
        /// Deletes the currently selected sheet
        /// </summary>
        private void DeleteSheet(int sheet)
        {
            //Get the SheetToDelete from workbook.xml
            Sheet sTheSheet = m_wbpWorkbook.Workbook.Descendants<Sheet>().ToArray()[sheet];
            //Store the SheetID for the reference
            string strSheetId = sTheSheet.SheetId;

            // Remove the sheet reference from the workbook.
            WorksheetPart worksheetPart = (WorksheetPart)(m_wbpWorkbook.GetPartById(sTheSheet.Id));
            sTheSheet.Remove();
            // Delete the worksheet part.
            m_wbpWorkbook.DeletePart(worksheetPart);
            // Update the worksheet array
            m_wspWorksheets = m_wbpWorkbook.WorksheetParts.ToArray();

            // Update the page headers
            UpdatePageHeader();

            //SelectSheet(SelectedSheet - 1);
        }

        public override void AddScout(Scout scout)
        {
            this.m_scouts.Add(scout);
        }

        public override void RemoveScout(Scout scout)
        {
            this.m_scouts.Remove(scout);
        }

        public override void SetPeriod(string period)
        {
            // Prepare the period string
            if (period.StartsWith("period", true, null))
            {
                m_strPeriod = period.Remove(0, period.IndexOf(" "));
            }
            else
            {
                m_strPeriod = period;
            }

            //m_strPeriod = m_strPeriod.Insert(m_strPeriod.IndexOf("&"), "&");

            UpdatePeriodHeader();
        }

        /// <summary>
        /// Updates the period headers of all sheets
        /// </summary>
        private void UpdatePeriodHeader()
        {
            // Get all of the worksheets
            WorksheetPart[] wspWorksheetParts = m_ssdProgramPaperwork.WorkbookPart.WorksheetParts.ToArray();
            // Set the week for each worksheet
            for (int i = 0; i < wspWorksheetParts.Count(); i++)
            {
                string strHeader = wspWorksheetParts[i].Worksheet.Descendants<HeaderFooter>().First().OddHeader.Text;
                string[] strSplit = new string[] { "&L", "&C", "&R" };
                string[] strHeaderParts = strHeader.Split(strSplit, StringSplitOptions.RemoveEmptyEntries);
                string strLeftHeader = strHeaderParts[0];
                string strPeriodPart = strLeftHeader.Substring(strLeftHeader.IndexOf("Period"), strLeftHeader.Length - strLeftHeader.IndexOf("Period"));
                string strNewLeftHeader = strLeftHeader.Replace(strPeriodPart, "Period #&U_" + m_strPeriod + "_&U");
                strHeader = strHeader.Replace(strLeftHeader, strNewLeftHeader);
                wspWorksheetParts[i].Worksheet.Descendants<HeaderFooter>().First().OddHeader.Text = strHeader;
            }
        }

        public override void SetProgram(string program)
        {
            // Get all of the worksheets
            WorksheetPart[] wspWorksheetParts = m_ssdProgramPaperwork.WorkbookPart.WorksheetParts.ToArray();
            // Set the week for each worksheet
            for (int i = 0; i < wspWorksheetParts.Count(); i++)
            {
                string strHeader = wspWorksheetParts[i].Worksheet.Descendants<HeaderFooter>().First().OddHeader.Text;
                string[] strSplit = new string[] { "&L", "&C", "&R" };
                string[] strHeaderParts = strHeader.Split(strSplit, StringSplitOptions.RemoveEmptyEntries);
                string strLeftHeader = strHeaderParts[0];
                string strNewLeftHeader = program + " " + strLeftHeader;
                strHeader = strHeader.Replace(strLeftHeader, strNewLeftHeader);
                wspWorksheetParts[i].Worksheet.Descendants<HeaderFooter>().First().OddHeader.Text = strHeader;
            }
        }

        public override void SetWeek(string week)
        {
            m_strWeek = week;

            UpdateWeekHeader();
        }

        /// <summary>
        /// Updates the week headers of all sheets
        /// </summary>
        private void UpdateWeekHeader()
        {
            // Get all of the worksheets
            WorksheetPart[] wspWorksheetParts = m_ssdProgramPaperwork.WorkbookPart.WorksheetParts.ToArray();
            // Set the week for each worksheet
            for (int i = 0; i < wspWorksheetParts.Count(); i++)
            {
                string strHeader = wspWorksheetParts[i].Worksheet.Descendants<HeaderFooter>().First().OddHeader.Text;
                string[] strSplit = new string[] { "&L", "&C", "&R" };
                string[] strHeaderParts = strHeader.Split(strSplit, StringSplitOptions.RemoveEmptyEntries);
                string strLeftHeader = strHeaderParts[0];
                string strWeekPart = strLeftHeader.Substring(strLeftHeader.IndexOf("Week"), strLeftHeader.IndexOf("Period") - strLeftHeader.IndexOf("Week"));
                string strNewLeftHeader = strLeftHeader.Replace(strWeekPart, "Week  #&U_" + m_strWeek + "_&U    ");
                strHeader = strHeader.Replace(strLeftHeader, strNewLeftHeader);
                wspWorksheetParts[i].Worksheet.Descendants<HeaderFooter>().First().OddHeader.Text = strHeader;
            }
        }

        public override void Sort(IComparer<Scout> scoutComparer)
        {
            m_scouts.Sort(scoutComparer);
        }

        public override bool IsCompatible(string file)
        {
            SpreadsheetDocument ssdSpreadsheet;
            WorkbookPart wbpWorkbook;
            WorksheetPart wspWoksheet;
            HeaderFooter hfHeaderFooter;
            SharedStringTablePart sspSharedStrings;

            if (System.IO.File.Exists(file))
            {
                ssdSpreadsheet = SpreadsheetDocument.Open(file, false);
                wbpWorkbook = ssdSpreadsheet.WorkbookPart;
                wspWoksheet = wbpWorkbook.WorksheetParts.First();
                hfHeaderFooter = wspWoksheet.Worksheet.Elements<HeaderFooter>().First();
                sspSharedStrings = wbpWorkbook.SharedStringTablePart;

                // If the spreadsheet has a footer or a shared string table or more than one sheet
                if ((hfHeaderFooter.OddFooter != null || hfHeaderFooter.EvenFooter != null) || sspSharedStrings != null || wbpWorkbook.WorksheetParts.Count() > 1)
                {
                    ssdSpreadsheet.Dispose();
                    return true;
                }

                ssdSpreadsheet.Dispose();
            }

            return false;
        }
    }
}
