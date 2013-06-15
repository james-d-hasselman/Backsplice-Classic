using System;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Backsplice
{
    class ProgramPaperwork : IDisposable
    {
        // ProgramPaperwork constants
        private const int cm_intLAST_ROW = 30;
        private const int cm_intSTART_ROW = 12;
        private const double cm_dblSHEET_SIZE = 19.0;
        private const int cm_intNAME_COLUMN = 0;
        private const int cm_intTROOP_COLUMN = 1;
        private const string cm_strNAME_COLUMN = "A";
        private const string cm_strTROOP_COLUMN = "B";

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

        public static bool IsPaperworkSheet(string _strFilePath)
        {
            SpreadsheetDocument ssdSpreadsheet;
            WorkbookPart wbpWorkbook;
            WorksheetPart wspWoksheet;
            HeaderFooter hfHeaderFooter;
            SharedStringTablePart sspSharedStrings;

            if (System.IO.File.Exists(_strFilePath))
            {
                ssdSpreadsheet = SpreadsheetDocument.Open(_strFilePath, false);
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

        /// <summary>
        /// Constructs a ProgramPaperwork
        /// </summary>
        /// <param name="_strFilePath">Path to a paperwork sheet</param>
        /// <param name="_blnReadOnly">The read-only status of the paperwork sheet</param>
        public ProgramPaperwork(string _strFilePath, bool _blnReadOnly)
        {
            /* OpenXML SDK Code - Added 11/3/2010 */

            // Save the file name and read-only status
            m_strFilePath = _strFilePath;
            /* (The SpreadsheetDocument class of the OpenXML SDK 2.0 uses
             * an isEditable property to describe the read only status of the document
             * in order for a document to be read-only, isEditable must be set to false
             * and vice-versa
             */
            m_blnIsEditable = !_blnReadOnly;

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
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~ProgramPaperwork()
        {
            Dispose(false);
        }

        /// <summary>
        /// Adds a new sheet to the paperwork
        /// </summary>
        public void AddSheet()
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
            for (int i = cm_intSTART_ROW; i <= cm_intLAST_ROW; i++)
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
        /// Adds sheets to the paperwork
        /// </summary>
        /// <param name="_intNumSheets">the number of sheets to add</param>
        public void AddSheets(int _intNumSheets)
        {
            for (int i = 0; i < _intNumSheets; i++)
            {
                this.AddSheet();
            }
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
        /// Gets a scout's first name
        /// </summary>
        /// <param name="_intRow">a row in the paperwork sheet</param>
        /// <returns></returns>
        public string GetFirstName(int _intRow)
        {
            string strName = this.GetName(_intRow);
            char[] chrSeparator = new char[] { ',' };
            string[] strNameParts = strName.Split(chrSeparator);

            return strNameParts[strNameParts.Length - 1].Trim();
        }

        /// <summary>
        /// Gets a scout's last name
        /// </summary>
        /// <param name="_intRow">a row in the paperwork sheet</param>
        /// <returns></returns>
        public string GetLastName(int _intRow)
        {
            string strName = this.GetName(_intRow);
            char[] chrSeparator = new char[] { ',' };
            string[] strNameParts = null;
            
            if(strName != null)
            {
                strNameParts = strName.Split(chrSeparator);
                return strNameParts[0].Trim();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a scout's name
        /// </summary>
        /// <param name="_intRow">row in the paperwork sheet</param>
        /// <returns>scout's name</returns>
        public string GetName(int _intRow)
        {
            /* OpenXML SDK 2.0 Code - Added 11/5/2010 */

            // Get the row
            Row rRow = (Row)m_sdSheetData.ChildElements[_intRow - 1];
            // Get the name cell
            Cell cName = (Cell)rRow.ChildElements[0];

            if (cName.CellValue.Text != null)
            {
                return cName.CellValue.Text.Trim();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a scout's troop
        /// </summary>
        /// <param name="_intRow">row in the paperwork sheet</param>
        /// <returns>scout's troop</returns>
        public string GetTroop(int _intRow)
        {
            /* OpenXML SDK 2.0 Code - Added 11/5/2010 */

            // Get the row
            Row rRow = (Row)m_sdSheetData.ChildElements[_intRow - 1];
            // Get the troop cell
            Cell cTroop = (Cell)rRow.ChildElements[1];

            return cTroop.CellValue.Text.Trim();
        }

        /// <summary>
        /// Inserts a row into the paperwork sheet
        /// </summary>
        /// <param name="_intRow">row in the paperwork sheet where the scout will be inserted</param>
        /// <param name="_strFirstName">scout's first name</param>
        /// <param name="_strLastName">scout's last name</param>
        /// <param name="_strTroopNumber">scout's troop number</param>
        public void InsertRow(int _intRow, string _strFirstName, string _strLastName, string _strTroopNumber)
        {
            /* OpenXML SDK 2.0 Code - Added 11/5/2010 */

            // Get the first row in the paperwork sheet
            Row rRow = (Row)m_sdSheetData.ChildElements[cm_intSTART_ROW];
            // Make a new row
            Row rNewRow = new Row();
            // Set the row index
            rNewRow.RowIndex = (uint)_intRow;
            // Copy the old row's height, custom height, and spans property
            rNewRow.CustomHeight = rRow.CustomHeight;
            rNewRow.Height = rRow.Height;
            rNewRow.Spans = rRow.Spans;

            // Copy all cells from the first row to the new row
            for (int i = 0; i < rRow.ChildElements.Count; i++)
            {
                Cell cCell = (Cell)rRow.ChildElements[i];
                Cell cACell = new Cell();

                // Get the column part of the cell reference
                string strColumn = "";
                foreach (char chrLetter in cCell.CellReference.ToString())
                {
                    if (char.IsLetter(chrLetter))
                    {
                        strColumn += chrLetter;
                    }
                }

                // Update the cell reference
                cACell.CellReference = strColumn + _intRow;
                // Copy the old cell's style index, 
                cACell.StyleIndex = cCell.StyleIndex;


                rNewRow.AppendChild(cACell);
            }

            // Set the name
            Cell cName = (Cell)rNewRow.ChildElements[NameColumn];
            cName.CellValue = new CellValue(_strLastName + ", " + _strFirstName);
            cName.DataType = CellValues.String;
            // Set the troop number
            Cell cTroop = (Cell)rNewRow.ChildElements[TroopColumn];
            cTroop.CellValue = new CellValue(_strTroopNumber);
            cTroop.DataType = CellValues.Number;

            // Insert the row
            m_sdSheetData.InsertAt(rNewRow, _intRow - 1);

            // Update the index of all rows
            for (int i = _intRow; i < m_sdSheetData.Count(); i++)
            {
                Row rARow = (Row)m_sdSheetData.ChildElements[i];
                rARow.RowIndex = rARow.RowIndex + 1;

                // Update the cell reference for all cells
                for (int j = 0; j < rARow.ChildElements.Count(); j++)
                {
                    Cell cACell = (Cell)rARow.ChildElements[j];
                    // Get the column part of the cell reference
                    string strColumn = "";
                    foreach (char chrLetter in cACell.CellReference.ToString())
                    {
                        if (char.IsLetter(chrLetter))
                        {
                            strColumn += chrLetter;
                        }
                    }

                    cACell.CellReference = strColumn + rARow.RowIndex.ToString();
                }
            }

            // If there is an extra row at the end of the sheet
            Row rExtraRow = (Row)m_sdSheetData.ChildElements[cm_intSTART_ROW + int.Parse(cm_dblSHEET_SIZE.ToString())];
            Cell cExtraName = (Cell)rExtraRow.ChildElements[NameColumn];
            Cell cExtraTroop = (Cell)rExtraRow.ChildElements[TroopColumn];
            //if (!cExtraName.CellValue.Text.Equals("") || !cExtraTroop.CellValue.Text.Equals(""))
            if((cExtraName.CellValue != null || cExtraTroop.CellValue != null) && (cExtraName.CellValue.Text != "" || cExtraTroop.CellValue.Text != ""))
            {
                // Save the name and troop
                string[] strNameParts = cExtraName.CellValue.Text.Split(new char[] { ',' });
                string strLastName = strNameParts[0].Trim();
                string strFirstName = strNameParts[1].Trim();
                string strTroop = cExtraTroop.CellValue.Text;

                // Remove the extra row
                RemoveRow(cm_intSTART_ROW + int.Parse(cm_dblSHEET_SIZE.ToString()) + 1);

                // If there is another paperwork sheet in the workbook
                if (SelectedSheet < NumberOfSheets - 1)
                {
                    // Select the next sheet
                    SelectSheet(SelectedSheet + 1);
                    // Insert the extra row at the top of the next sheet
                    InsertRow(StartRow, strFirstName, strLastName, strTroop);
                }
                else
                {
                    // Add a new sheet
                    AddSheet();
                    // Select the new sheet
                    SelectSheet(SelectedSheet + 1);
                    // Insert the extra row at the top of the next sheet
                    InsertRow(StartRow, strFirstName, strLastName, strTroop);
                }
            }
            else
            {
                // Remove the extra row
                RemoveRow(cm_intSTART_ROW + int.Parse(cm_dblSHEET_SIZE.ToString()) + 1);
            }
        }

        /// <summary>
        /// Removes a row from the paperwork
        /// </summary>
        /// <param name="_intRow">row to remove</param>
        public void RemoveRow(int _intRow)
        {
            // Adjust the index to account for the zero based sheet data
            RemoveARow(_intRow - 1);
        }

        /// <summary>
        /// Selects a sheet
        /// </summary>
        /// <param name="_intSheet">sheet to select</param>
        public void SelectSheet(int _intSheet)
        {
            /* OpenXML SDK 2.0 Code - Added 11/7/2010 */

            SelectedSheet = _intSheet;
            m_sdSheetData = m_wspWorksheets[SelectedSheet].Worksheet.Elements<SheetData>().First();
        }

        /// <summary>
        /// Sets a scout's name
        /// </summary>
        /// <param name="_intRow">row to set</param>
        /// <param name="_strName">scout's name</param>
        public void SetName(int _intRow, string _strName)
        {
            /* OpenXML SDK 2.0 Code - Added 11/7/2010 */
            
            int intRow = _intRow - 1;
            Row rRow = (Row)m_sdSheetData.ChildElements[intRow];
            Cell cCell = (Cell)rRow.ChildElements[NameColumn];
            cCell.DataType = CellValues.String;
            cCell.CellValue = new CellValue(_strName);
        }

        /// <summary>
        /// Sets the program period
        /// </summary>
        /// <param name="_strPeriod">period</param>
        public void SetPeriod(string _strPeriod)
        {
            // Prepare the period string
            if (_strPeriod.StartsWith("period", true, null))
            {
                m_strPeriod = _strPeriod.Remove(0, _strPeriod.IndexOf(" "));
            }
            else
            {
                m_strPeriod = _strPeriod;
            }

            //m_strPeriod = m_strPeriod.Insert(m_strPeriod.IndexOf("&"), "&");

            UpdatePeriodHeader();
        }

        /// <summary>
        /// Sets a scout's troop 
        /// </summary>
        /// <param name="_intRow">row in the paperwork sheet</param>
        /// <param name="_strTroop">scout's troop</param>
        public void SetTroop(int _intRow, string _strTroop)
        {
            /* OpenXML SDK 2.0 Code - Added 11/7/2010 */

            int intRow = _intRow - 1;
            Row rRow = (Row)m_sdSheetData.ChildElements[intRow];
            Cell cCell = (Cell)rRow.ChildElements[TroopColumn];
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
        /// Sets the week
        /// </summary>
        /// <param name="_strWeek">week</param>
        public void SetWeek(string _strWeek)
        {
            m_strWeek = _strWeek;

            UpdateWeekHeader();
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

        /// <summary>
        /// Removes a row from the paperwork sheet
        /// </summary>
        /// <param name="_intRow">row in the paperwork sheet</param>
        /// <returns>row that was removed</returns>
        private Row RemoveARow(int _intRow)
        {
            /* OpenXML SDK 2.0 Code - Added 11/5/2010 */
            
	        // Get the row
            Row rRow = (Row)m_sdSheetData.ChildElements[_intRow];
            if (_intRow <= cm_intLAST_ROW)
            {
                // Create a new last row
                Row rNewLastRow = new Row();
                // Set the new row's height
                rNewLastRow.CustomHeight = true;
                rNewLastRow.Height = rRow.Height;
                // Copy all cells from the row to the new last row
                for (int i = 0; i < rRow.ChildElements.Count; i++)
                {
                    Cell cCell = (Cell)rRow.ChildElements[i];
                    Cell cACell = new Cell();

                    if (cCell.CellValue != null)
                    {
                        cACell.CellValue = new CellValue(cCell.CellValue.Text);
                        cACell.DataType = cCell.DataType;
                    }

                    if (cCell.CellMetaIndex != null)
                    {
                        cACell.CellMetaIndex = (uint)cCell.CellMetaIndex;
                    }

                    if (cCell.CellFormula != null)
                    {
                        cACell.CellFormula = new CellFormula(cCell.CellFormula.ToString());
                    }

                    if (cCell.StyleIndex != null)
                    {
                        cACell.StyleIndex = cCell.StyleIndex;
                    }

                    // Get the column part of the cell reference
                    string strColumn = "";
                    foreach (char chrLetter in cCell.CellReference.ToString())
                    {
                        if (char.IsLetter(chrLetter))
                        {
                            strColumn += chrLetter;
                        }
                    }

                    // Adjust cell references to reference the last row
                    cACell.CellReference = strColumn + (ProgramPaperwork.LastRow);

                    rNewLastRow.AppendChild(cACell);
                }

                // Set the new last row's row index to the last row
                rNewLastRow.RowIndex = (uint)ProgramPaperwork.LastRow;
                // Empty the name and troop cells
                Cell cName = (Cell)rNewLastRow.ChildElements[NameColumn];
                cName.CellValue = new CellValue("");
                cName.DataType = CellValues.String;
                Cell cTroop = (Cell)rNewLastRow.ChildElements[TroopColumn];
                cTroop.CellValue = new CellValue("");
                cTroop.DataType = CellValues.Number;
                // Remove the last row
                m_sdSheetData.RemoveChild(rRow);
                // From the next row until the last row
                for (int i = _intRow; i < cm_intLAST_ROW; i++)
                {
                    // Decrement the row indicies and cell references
                    Row rARow = (Row)m_sdSheetData.ChildElements[i];
                    rARow.RowIndex = rARow.RowIndex - 1;
                    for (int j = 0; j < rARow.ChildElements.Count; j++)
                    {
                        Cell cACell = (Cell)rARow.ChildElements[j];

                        // Get the column part of the cell reference
                        string strColumn = "";
                        foreach (char chrLetter in cACell.CellReference.ToString())
                        {
                            if (char.IsLetter(chrLetter))
                            {
                                strColumn += chrLetter;
                            }
                        }

                        string strCellReference = cACell.CellReference.ToString();
                        string strRow = strCellReference.Remove(strCellReference.IndexOf(strColumn), strColumn.Length);
                        int intRow = int.Parse(strRow) - 1;

                        cACell.CellReference = strColumn + intRow;
                    }
                }

                // Append the new last row to the end of the sheet
                //m_sdSheetData.InsertAt(rNewLastRow, cm_intLAST_ROW + 1);
                m_sdSheetData.InsertAt(rNewLastRow, cm_intLAST_ROW);
            }
            else
            {
                m_sdSheetData.RemoveChild(rRow);

                // From the next row until the last row
                for (int i = _intRow; i < m_sdSheetData.ChildElements.Count; i++)
                {
                    // Decrement the row indicies and cell references
                    Row rARow = (Row)m_sdSheetData.ChildElements[i];
                    rARow.RowIndex = rARow.RowIndex - 1;
                    for (int j = 0; j < rARow.ChildElements.Count; j++)
                    {
                        Cell cACell = (Cell)rARow.ChildElements[j];

                        // Get the column part of the cell reference
                        string strColumn = "";
                        foreach (char chrLetter in cACell.CellReference.ToString())
                        {
                            if (char.IsLetter(chrLetter))
                            {
                                strColumn += chrLetter;
                            }
                        }

                        string strCellReference = cACell.CellReference.ToString();
                        string strRow = strCellReference.Remove(strCellReference.IndexOf(strColumn), strColumn.Length);
                        int intRow = int.Parse(strRow) - 1;

                        cACell.CellReference = strColumn + intRow;
                    }
                }
            }

            // If the sheet is empty
            Row rFirstRow = (Row)m_sdSheetData.ChildElements[cm_intSTART_ROW];
            Cell cFirstName = (Cell)rFirstRow.ChildElements[NameColumn];
            Cell cFirstTroop = (Cell)rFirstRow.ChildElements[TroopColumn];
            if (cFirstName.CellValue.Text == "" && cFirstTroop.CellValue.Text == "")
            {
                // Delete it
                DeleteSheet();
            }
            else
            {
                // If there is another sheet and the last row of the current sheet is empty
                Row rLastRow = (Row)m_sdSheetData.ChildElements[cm_intLAST_ROW];
                Cell cAName = (Cell)rLastRow.ChildElements[NameColumn];
                Cell cATroop = (Cell)rLastRow.ChildElements[TroopColumn];
                if (SelectedSheet < NumberOfSheets - 1 && (cAName.CellValue != null && cATroop.CellValue != null) && (cAName.CellValue.Text == "" && cATroop.CellValue.Text == ""))
                {
                    // Remove the first row from the next sheet
                    Row rRemovedRow = RemoveARow(cm_intSTART_ROW, SelectedSheet + 1);
                    // Copy the row into the last row of the current sheet
                    Cell cScoutName = (Cell)rRemovedRow.ChildElements[NameColumn];
                    Cell cScoutTroop = (Cell)rRemovedRow.ChildElements[TroopColumn];
                    //cAName.CellValue.Text = cScoutName.CellValue.Text;
                    //cATroop.CellValue.Text = cScoutTroop.CellValue.Text;

                    cAName.CellValue = new CellValue(cScoutName.CellValue.Text);
                    cATroop.CellValue = new CellValue(cScoutTroop.CellValue.Text);

                    // Nasty hack
                    for (int i = 0; i < m_wspWorksheets.Count(); i++)
                    {
                        m_wspWorksheets[i].Worksheet.Save();
                    }
                }
            }

            return rRow;
        }

        /// <summary>
        /// Removes a row from a paperwork sheet
        /// </summary>
        /// <param name="_intRow">row in the paperwork sheet</param>
        /// <param name="_intSheet">sheet to remove the row from</param>
        /// <returns>row that was removed</returns>
        private Row RemoveARow(int _intRow, int _intSheet)
        {
            int intSheet = m_intSelectedSheet;

            // Select the sheet
            SelectSheet(_intSheet);
            // Remove the row
            Row rRow = RemoveARow(_intRow);
            // Select the previous sheet
            SelectSheet(intSheet);

            return rRow;
        }

        /// <summary>
        /// Deletes the currently selected sheet
        /// </summary>
        private void DeleteSheet()
        {
            //Get the SheetToDelete from workbook.xml
            Sheet sTheSheet = m_wbpWorkbook.Workbook.Descendants<Sheet>().ToArray()[SelectedSheet];
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

            SelectSheet(SelectedSheet - 1);
        }

        /// <summary>
        /// Gets the selected sheet
        /// </summary>
        private int SelectedSheet
        {
            get
            {
                return m_intSelectedSheet;
            }

            set
            {
                m_intSelectedSheet = value;
            }
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

        /// <summary>
        /// Gets the last row of the paperwork sheet
        /// </summary>
        public static int LastRow
        {
            get
            {
                return cm_intLAST_ROW + 1;
            }
        }

        /// <summary>
        /// Gets the paperwork sheet size
        /// </summary>
        public static double SheetSize
        {
            get
            {
                return cm_dblSHEET_SIZE;
            }
        }

        /// <summary>
        /// Gets the starting row of the paperwork sheet
        /// </summary>
        public static int StartRow
        {
            get
            {
                return cm_intSTART_ROW + 1;
            }
        }

        /// <summary>
        /// Gets the number of sheets in the paperwork workbook
        /// </summary>
        public int NumberOfSheets
        {
            get
            {
                return m_wspWorksheets.Count();
            }
        }

        /// <summary>
        /// Gets the name column as an integer
        /// </summary>
        private int NameColumn
        {
            get
            {
                return cm_intNAME_COLUMN;
            }
        }

        /// <summary>
        /// Gets the name column as a string
        /// </summary>
        private static string NameColumnString
        {
            get
            {
                return cm_strNAME_COLUMN;
            }
        }

        /// <summary>
        /// Gets the troop column as an integer
        /// </summary>
        private int TroopColumn
        {
            get
            {
                return cm_intTROOP_COLUMN;
            }
        }

        /// <summary>
        /// Gets the string representing the troop column
        /// </summary>
        private static string TroopColumnString
        {
            get
            {
                return cm_strTROOP_COLUMN;
            }
        }

        /// <summary>
        /// Gets the paperwork sheet size as an integer
        /// </summary>
        private static int SheetSizeInteger
        {
            get
            {
                return int.Parse(cm_dblSHEET_SIZE.ToString());
            }
        }

        /// <summary>
        /// Sets the program name on all sheets.
        /// </summary>
        /// <param name="_strProgram">The program name.</param>
        public void SetProgramName(string _strProgram)
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
                string strNewLeftHeader = _strProgram + " " + strLeftHeader;
                strHeader = strHeader.Replace(strLeftHeader, strNewLeftHeader);
                wspWorksheetParts[i].Worksheet.Descendants<HeaderFooter>().First().OddHeader.Text = strHeader;
            }
        }
    }
}
 