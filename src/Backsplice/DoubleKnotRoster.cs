using System;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Backsplice
{
    class DoubleKnotRoster : IDisposable
    {
        // DoubleKnot roster constants
        public  const int m_intSTART_ROW = 2;
        private const int m_intLAST_NAME_COLUMN = 0;
        private const int m_intFIRST_NAME_COLUMN = 1;
        private const int m_intTROOP_COLUMN = 2;
        private const int m_intDESCRIPTION_COLUMN = 3;
        private const string m_strLAST_NAME_COLUMN = "A";
        private const string m_strFIRST_NAME_COLUMN = "B";
        private const string m_strTROOP_COLUMN = "C";
        private const string m_strDESCRIPTION_COLUMN = "D";
        private string m_strFile;
        private bool m_blnIsEditable;
        private bool m_blnDisposed;

        // OpenXML SDK 2.0 Code - Added 11/2/2010
        private SpreadsheetDocument m_ssdDoubleKnotRoster;
        private WorksheetPart m_wpWorksheet;
        private SheetData m_sdSheetData;
        private SharedStringTable m_sstSharedStringTable;

        /// <summary>
        /// Constructs a DoubleKnotRoster
        /// </summary>
        /// <exception cref="System.IO.FileNotFoundException">if the supplied file path does not exist</exception>
        /// <param name="_strFile">path to a DoubleKnot roster</param>
        /// <param name="_blnReadOnly">the read only status of the DoubleKnot roster</param>
        public DoubleKnotRoster(string _strFile, bool _blnReadOnly)
        {
            /* OpenXML SDK 2.0 Code - Added 11/2/2010 */

            // Save the filename and read-only status
            m_strFile = _strFile;
            /* (The SpreadsheetDocument class of the OpenXML SDK 2.0 uses
             * an isEditable property to describe the read only status of the document
             * in order for a document to be read-only, isEditable must be set to false
             * and vice-versa
             */
            m_blnIsEditable = !_blnReadOnly;

            // Open the file
            m_ssdDoubleKnotRoster = SpreadsheetDocument.Open(m_strFile, m_blnIsEditable);
            // Get the worksheet
            m_wpWorksheet = m_ssdDoubleKnotRoster.WorkbookPart.WorksheetParts.First();
            // Get the sheet data
            m_sdSheetData = m_wpWorksheet.Worksheet.Elements<SheetData>().First();
            // Get the shared string table
            m_sstSharedStringTable = m_ssdDoubleKnotRoster.WorkbookPart.SharedStringTablePart.SharedStringTable;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~DoubleKnotRoster()
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
        /// Gets the name from a row
        /// </summary>
        /// <param name="_intRow"></param>
        /// <returns></returns>
        public string extractName(int _intRow)
        {
            return extractLastName(_intRow) + ", " + extractFirstName(_intRow);
        }

        /// <summary>
        /// Gets the first name from a row
        /// </summary>
        /// <param name="_intRow">a row in the DoubleKnot roster</param>
        /// <returns>the first name</returns>
        public string extractFirstName(int _intRow)
        {
            /* OpenXML SDK 2.0 Code - Added 11/2/2010 */

            // Look for the first name in the shared string table
            string strFirstName = LookupSharedString(_intRow - 1, FirstNameColumn);

            return strFirstName;
        }

        /// <summary>
        /// Gets the last name from a row
        /// </summary>
        /// <param name="_intRow">a row in the DoubleKnot roster</param>
        /// <returns>the last name</returns>
        public string extractLastName(int _intRow)
        {
            /* OpenXML SDK 2.0 Code - Added 11/2/2010 */

            // Look for the last name in the shared string table
            string strLastName = LookupSharedString(_intRow - 1, LastNameColumn);

            return strLastName;
        }

        /// <summary>
        /// Gets the complete period string a row
        /// </summary>
        /// <param name="_intRow">a row in the DoubleKnot roster</param>
        /// <returns>the complete period string</returns>
        public string extractPeriod(int _intRow)
        {
            /* OpenXML SDK 2.0 Code - Added 11/2/2010 */

            // Look for the description in the shared string table
            // (the period is part of the description column of the DoubleKnot roster)
            string strDescription = LookupSharedString(_intRow - 1, DescriptionColumn);

            // extract the period from the string
            char[] chrSeparator = new char[] { '(', ')' };
            string[] strDescriptionParts = strDescription.Split(chrSeparator);
            string strPeriod = strDescriptionParts[1].Trim();

            return strPeriod;
        }

        /// <summary>
        /// Gets the period number from a row
        /// </summary>
        /// <param name="_intRow">a row in the DoubleKnot roster</param>
        /// <returns>the period number</returns>
        public int extractPeriodNumber(int _intRow)
        {
            string strPeriod = extractPeriod(_intRow);

            char[] chrSeparator = new char[] { ' ' };
            string[] strPeriodParts = strPeriod.Split(chrSeparator);

            for (int i = 0; i < strPeriodParts.Length; i++)
            {
                int intPeriodNumber;
                if (int.TryParse(strPeriodParts[i], out intPeriodNumber))
                {
                    return intPeriodNumber;
                }
            }

            return -1;
        }

        /// <summary>
        /// Gets the program name from a row
        /// </summary>
        /// <param name="_intRow">a row in the DoubleKnot roster</param>
        /// <returns>the program name</returns>
        public string extractProgram(int _intRow)
        {
            /* OpenXML SDK 2.0 Code - Added 11/2/2010 */

            // Look for the description in the shared string table
            // (The program name is part of the description column of the DoubleKnot roster)
            string strDescription = LookupSharedString(_intRow - 1, DescriptionColumn);

            // Extract the program name from the description
            char[] chrSeparator = new char[] { '(' };
            string[] strDescriptionParts = strDescription.Split(chrSeparator);
            string strProgram = strDescriptionParts[0].Trim();

            // Trim off the MB or MBs denoting Merit Badge(s)
            if (strProgram.EndsWith(" MB") || strProgram.EndsWith(" MBs"))
            {
                strProgram = strProgram.Remove(strProgram.Length - 3).Trim();
            }

            return strProgram;
        }

        /// <summary>
        /// Gets the troop number from a row
        /// </summary>
        /// <param name="_intRow">a row in the DoubleKnot roster</param>
        /// <returns>the troop number, "PROVO" if the scout is a provisional camper, or null if the troop number was not found</returns>
        public string extractTroopNumber(int _intRow)
        {
            /* OpenXML SDK 2.0 Code - Added 11/2/2010 */

            string strTroopString = LookupSharedString(_intRow - 1, TroopColumn);

            // If scout is a provisional camper
            if (strTroopString.ToUpper().Contains("PROVISIONAL") || strTroopString.ToUpper().Contains("PROVO"))
            {
                return "PROVO";
            }
            else // Look for a troop number
            {
                string strTroop = "";

                char[] chrTroopString = strTroopString.ToCharArray();
                bool blnFoundNumber = false;
                int i = 0;

                // Search the entire troop string until a number is found
                while(i < chrTroopString.Length && !blnFoundNumber)
                {
                    // Concatenate the first set of consecutive numbers onto the string
                    while (i < chrTroopString.Length && char.IsDigit(chrTroopString[i]))
                    {
                        strTroop += chrTroopString[i];
                        // A number has been found
                        blnFoundNumber = true;
                        // Look for more digits
                        i++;
                    }
                    i++;
                }

                // If a number has been found
                if (blnFoundNumber)
                {
                    return strTroop;
                }
            }

            // Troop number not found
            return null;
        }

        /// <summary>
        /// Inserts a scout into the DoubleKnot roster
        /// </summary>
        /// <param name="_intRow">a row in the DoubleKnot roster</param>
        /// <param name="_strLastName">a scout's last name</param>
        /// <param name="_strFirstName">a scout's first name</param>
        /// <param name="_strTroop">the scout's troop</param>
        /// <param name="_strMeritBadge">a camp program</param>
        /// <param name="_strPeriod">a program period</param>
        public void InsertRow(int _intRow, string _strLastName, string _strFirstName, string _strTroop, string _strMeritBadge, string _strPeriod)
        {
            /* OpenXML SDK 2.0 Code - Added 11/2/2010 */

            // Add each value to the shared string table
            string strLastNameIndex = AddSharedString(_strLastName);
            string strFirstNameIndex = AddSharedString(_strFirstName);
            string strTroopIndex = AddSharedString(_strTroop);
            string strDescriptionIndex = AddSharedString(_strMeritBadge + " MB " + "(" + _strPeriod + ")");

            // Create a cell value for each value added to the shared string table
            CellValue cvLastName = new CellValue(strLastNameIndex);
            CellValue cvFirstName = new CellValue(strFirstNameIndex);
            CellValue cvTroop = new CellValue(strTroopIndex);
            CellValue cvDescription = new CellValue(strDescriptionIndex);

            // Create a cell for each cell value
            Cell cLastName = new Cell();
            cLastName.CellValue = cvLastName;
            cLastName.DataType = CellValues.SharedString;
            cLastName.CellReference = LastNameColumnString + (_intRow);

            Cell cFirstName = new Cell();
            cFirstName.CellValue = cvFirstName;
            cFirstName.DataType = CellValues.SharedString;
            cFirstName.CellReference = FirstNameColumnString + (_intRow);

            Cell cTroop = new Cell();
            cTroop.CellValue = cvTroop;
            cTroop.DataType = CellValues.SharedString;
            cTroop.CellReference = TroopColumnString + (_intRow);

            Cell cDescription = new Cell();
            cDescription.CellValue = cvDescription;
            cDescription.DataType = CellValues.SharedString;
            cDescription.CellReference = DescriptionColumnString + (_intRow);

            // Create a row from the cells
            Row rRow = new Row();
            rRow.RowIndex = (DocumentFormat.OpenXml.UInt32Value)(uint)(_intRow);
            rRow.Append(cLastName);
            rRow.Append(cFirstName);
            rRow.Append(cTroop);
            rRow.Append(cDescription);

            // If the row is being inserted at the end of the sheet
            if (_intRow >= m_sdSheetData.ChildElements.Count())
            {
                // Append the row to the end of the worksheet
                m_sdSheetData.Append(rRow);
            }
            else
            {
                // Insert the row into the worksheet
                m_sdSheetData.InsertAt(rRow, _intRow - 1);

                // Update the index of all rows
                for (int i = _intRow; i < m_sdSheetData.Count(); i++)
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
            /* OpenXML SDK 2.0 Code - Added 11/3/2010 */

            // Get the row
            Row rRow = (Row)m_sdSheetData.ChildElements[_intRow - 1];
            
            // If the row is being removed from the end of the sheet
            if (_intRow >= m_sdSheetData.ChildElements.Count())
            {
                // Remove the row from the worksheet
                m_sdSheetData.RemoveChild(rRow);
            }
            else
            {
                // Remove the row from the worksheet
                m_sdSheetData.RemoveChild(rRow);

                // Update the index of all rows
                for (int i = _intRow - 1; i < m_sdSheetData.Count(); i++)
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
                        m_wpWorksheet.Worksheet.Save();
                    }
                    m_ssdDoubleKnotRoster.Dispose();
                }

                m_blnDisposed = true;
            }
        }

        /// <summary>
        /// Adds a string to the shared string table
        /// </summary>
        /// <param name="_strString">the string to add</param>
        /// <returns>the index of the new string in the shared string table</returns>
        private string AddSharedString(string _strString)
        {
            int index = 0;
            // See if the string already exists in the shared string table
            foreach (SharedStringItem ssiSharedString in m_sstSharedStringTable)
            {
                // If the string already exists in the shared string table
                if (ssiSharedString.InnerText.Equals(_strString))
                {
                    return index.ToString();
                }

                index++;
            }

            // The string does not exist in the shared string table so add it and return the index
            m_sstSharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(_strString)));
            m_sstSharedStringTable.Save();

            return index.ToString();
        }

        /// <summary>
        /// Looks up a cell value in a DoubleKnot roster's shared string table
        /// </summary>
        /// <param name="_intRow">a row in the DoubleKnotRoster</param>
        /// <param name="_intColumn">a column in the DoubleKnotRoster</param>
        /// <returns>the string found in the shared string table</returns>
        private string LookupSharedString(int _intRow, int _intColumn)
        {
            // Get the cell value
            Row rRow = (Row)m_sdSheetData.ChildElements[_intRow];
            Cell cCell;
            CellValue cvValue;

            if (_intColumn == DescriptionColumn)
            {
                cCell = rRow.Elements<Cell>().Where(c => string.Compare(c.CellReference.Value, DescriptionColumnString + (_intRow + 1), true) == 0).First();
                cvValue = cCell.CellValue;
            }
            else
            {
                cCell = (Cell)rRow.ChildElements[_intColumn];
                cvValue = cCell.CellValue;
            }

            // Look up the cell value in the shared string table
            string strSharedString = m_sstSharedStringTable.ElementAt(int.Parse(cvValue.Text)).InnerText.Trim();

            return strSharedString;
        }

        /// <summary>
        /// Gets the row where the DoubleKnot roster's data begins
        /// </summary>
        /// <value>row where the DoubleKnot roster's data begins</value>
        public static int StartRow
        {
            get
            {
                return m_intSTART_ROW;
            }
        }

        /// <summary>
        /// Gets the alphabetic string representing the description column
        /// </summary>
        /// <value>the alphabetic string representing the description column</value>
        public string DescriptionColumnString
        {
            get
            {
                return m_strDESCRIPTION_COLUMN;
            }
        }

        /// <summary>
        /// Gets the number of rows in the DoubleKnot roster
        /// </summary>
        /// <value>the number of rows in the DoubleKnot roster</value>
        public int Rows
        {
            get
            {
                /* OpenXML SDK 2.0 Code - Added 11/3/2010 */

                int intRows = m_sdSheetData.ChildElements.Count;

                return intRows;
            }
        }

        /// <summary>
        /// Gets the alphabetic string representing the troop column
        /// </summary>
        /// <value>the alphabetic string representing the troop column</value>
        public string TroopColumnString
        {
            get
            {
                return m_strTROOP_COLUMN;
            }
        }

        /// <summary>
        /// Gets the description column number
        /// </summary>
        /// <value>the description column number</value>
        private int DescriptionColumn
        {
            get
            {
                return m_intDESCRIPTION_COLUMN;
            }
        }

        /// <summary>
        /// Gets the first name column number
        /// </summary>
        /// <value>the first name column number</value>
        private int FirstNameColumn
        {
            get
            {
                return m_intFIRST_NAME_COLUMN;
            }
        }

        /// <summary>
        /// Gets the alphabetic string representing the first name column
        /// </summary>
        /// <value>the alphabetic string representing the first name column</value>
        private string FirstNameColumnString
        {
            get
            {
                return m_strFIRST_NAME_COLUMN;
            }
        }

        /// <summary>
        /// Gets the last name column number
        /// </summary>
        /// <value>the last name column number</value>
        private int LastNameColumn
        {
            get
            {
                return m_intLAST_NAME_COLUMN;
            }
        }

        /// <summary>
        /// Gets the alphabetic string representing the last name column
        /// </summary>
        /// <value>the alphabetic string representing the last name column</value>
        private string LastNameColumnString
        {
            get
            {
                return m_strLAST_NAME_COLUMN;
            }
        }

        /// <summary>
        /// Gets the troop column number
        /// </summary>
        /// <value>the troop column number</value>
        private int TroopColumn
        {
            get
            {
                return m_intTROOP_COLUMN;
            }
        }
    }
}
