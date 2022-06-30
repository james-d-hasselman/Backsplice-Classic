using System;
using System.Windows.Forms;

namespace Backsplice
{
    public partial class AddScout : Form
    {
        private string m_strWeek;
        private string m_strFirstName;
        private string m_strLastName;
        private string m_strTroopNumber;
        private string m_strRosterPath;
        private string m_strRosterDirectoryPath;
        private bool m_blnSortScouts = BackspliceMain.SortScouts;

        public AddScout(string _strWeek, string _strFirstName, string _strLastName, string _strTroopNumber)
        {
            InitializeComponent();

            dgvPrograms.Columns[dgvPrograms.Columns.Count - 1].ValueType = typeof(int);

            m_strWeek = _strWeek;
            m_strFirstName = _strFirstName;
            m_strLastName = _strLastName;
            m_strTroopNumber = _strTroopNumber;
            
            // Get the directory where the DoubleKnot roster is located
            m_strRosterDirectoryPath = BackspliceMain.SaveDirectory + @"\Week " + _strWeek;
            m_strRosterPath = "";

            System.Diagnostics.Debug.WriteLine(m_strRosterDirectoryPath);
        }

        private void ReadCourses()
        {
            if (System.IO.Directory.Exists(m_strRosterDirectoryPath))
            {
                // Find the roster
                string[] strFiles = System.IO.Directory.GetFiles(m_strRosterDirectoryPath, "Week " + m_strWeek + "*");
                if (strFiles.Length > 0)
                {
                    m_strRosterPath = strFiles[0];

                    // Open the DoubleKnot roster
                    DoubleKnotRoster dkrRoster = new DoubleKnotRoster(m_strRosterPath, true);
                    // Start at the beginning of the roster
                    int i = DoubleKnotRoster.StartRow;
                    // Read the first course name
                    string strCourseName = dkrRoster.extractProgram(i);
                    // Read the first period
                    string strPeriod = dkrRoster.extractPeriod(i);
                    // Increment the count of registered scouts
                    i++;
                    int intRegisteredScouts = 1;
                    // While not at the end of the roster
                    while (i < dkrRoster.Rows)
                    {
                        // Read a course name
                        string strNextCourse = dkrRoster.extractProgram(i);
                        // Read a period
                        string strNextPeriod = dkrRoster.extractPeriod(i);
                        // If this new course is the same as the original
                        if (strNextCourse.Equals(strCourseName) && strNextPeriod.Equals(strPeriod))
                        {
                            // Increment the count of registered scouts
                            intRegisteredScouts++;
                        }
                        else
                        {
                            if (!strCourseName.Contains("Summer Camp Week"))
                            {
                                System.Diagnostics.Debug.WriteLine(strCourseName + " " + strPeriod + " " + intRegisteredScouts.ToString());
                                // Add the course and the count to the data grid
                                object[] objValues = new object[3];

                                objValues[0] = strCourseName;
                                objValues[1] = strPeriod;
                                objValues[2] = intRegisteredScouts;
                                dgvPrograms.Rows.Add(objValues);
                            }
                            // Make this new course the original
                            strCourseName = strNextCourse;
                            strPeriod = strNextPeriod;
                            // Set the course count to 1
                            intRegisteredScouts = 1;
                        }

                        i++;
                    }

                    dkrRoster.Dispose();
                    return;
                }
            }

            MessageBox.Show("The paperwork for Week " + m_strWeek + " does not exist", "Paperwork not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        private void OnClose()
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.OnClose();
        }

        private void OnAdd()
        {
            this.Cursor = Cursors.WaitCursor;
            string strFirstName = m_strFirstName;
            string strLastName = m_strLastName;
            string strTroopNumber = m_strTroopNumber;
            string strName = strLastName + ", " + strFirstName;
            // Get the program
            string strProgram = dgvPrograms.SelectedRows[0].Cells[0].Value.ToString();
            // Get the period
            string strPeriod = dgvPrograms.SelectedRows[0].Cells[1].Value.ToString();
            // Open the double knot roster
            DoubleKnotRoster dkrRoster = new DoubleKnotRoster(m_strRosterPath, false);
            int i = DoubleKnotRoster.StartRow;
            int index = 0;
            bool blnInserted = false;
            while (i <= dkrRoster.Rows && !blnInserted)
            {
                if (dkrRoster.extractProgram(i).Equals(strProgram) && dkrRoster.extractPeriod(i).Equals(strPeriod))
                {
                    index = i;

                    if (strLastName.CompareTo(dkrRoster.extractLastName(i)) <= 0)
                    {
                        // Search for the proper place to insert the name
                        /*int j = i;
                        while (j < dkrRoster.Rows && !blnInserted)
                        {
                            if (strLastName.CompareTo(dkrRoster.extractLastName(j)) <= 0)
                            {*/
                        // Insert the name
                        dkrRoster.InsertRow(i, strLastName, strFirstName, m_strTroopNumber, strProgram, strPeriod);
                        blnInserted = true;
                        /*}

                        j++;
                    }*/
                    }
                }

                i++;
            }

            dkrRoster.Dispose();

            string strProgramPaperworkPath;


            if (System.IO.Directory.Exists(m_strRosterDirectoryPath))
            {
                // Find the roster
                string[] strFiles = System.IO.Directory.GetFiles(m_strRosterDirectoryPath + @"\" + strProgram, strProgram.ToLower() + "_" + strPeriod + "*");
                if (strFiles.Length > 0)
                {
                    strProgramPaperworkPath = strFiles[0];
                    System.Diagnostics.Debug.WriteLine(strProgramPaperworkPath);

                    if (ProgramPaperwork.IsPaperworkSheet(strProgramPaperworkPath))
                    {
                        if (this.m_blnSortScouts)
                        {
                            // Open the program paperwork
                            ProgramPaperwork ppProgram = new ProgramPaperwork(strProgramPaperworkPath, false);
                            // Search for the proper place to insert the name
                            int k = 0;
                            bool blnNameInserted = false;

                            if (Backsplice.Properties.Settings.Default.SortAlphabetically && Backsplice.Properties.Settings.Default.SortByTroop)
                            {
                                while (k < ppProgram.NumberOfSheets && !blnNameInserted)
                                {
                                    ppProgram.SelectSheet(k);
                                    int l = ProgramPaperwork.StartRow;
                                    // Search each sheet
                                    while (l < (ProgramPaperwork.StartRow + ProgramPaperwork.SheetSize) && !blnNameInserted)
                                    {
                                        if (ppProgram.GetName(l) == null)
                                        {
                                            // Add the name to the sheet
                                            ppProgram.SetName(l, strLastName + ", " + strFirstName);
                                            ppProgram.SetTroop(l, m_strTroopNumber);
                                            blnNameInserted = true;
                                        }
                                        else if ((strName.CompareTo(ppProgram.GetName(l)) <= 0 && int.Parse(strTroopNumber) <= int.Parse(ppProgram.GetTroop(l))))
                                        {
                                            // Insert the name
                                            ppProgram.InsertRow(l, strFirstName, strLastName, m_strTroopNumber);
                                            blnNameInserted = true;
                                        }

                                        l++;
                                    }

                                    k++;
                                }
                            }
                            else if (Backsplice.Properties.Settings.Default.SortAlphabetically)
                            {
                                while (k < ppProgram.NumberOfSheets && !blnNameInserted)
                                {
                                    ppProgram.SelectSheet(k);
                                    int l = ProgramPaperwork.StartRow;
                                    // Search each sheet
                                    while (l < (ProgramPaperwork.StartRow + ProgramPaperwork.SheetSize) && !blnNameInserted)
                                    {
                                        if (ppProgram.GetName(l) == null)
                                        {
                                            // Add the name to the sheet
                                            ppProgram.SetName(l, strLastName + ", " + strFirstName);
                                            ppProgram.SetTroop(l, m_strTroopNumber);
                                            blnNameInserted = true;
                                        }
                                        else if (strName.CompareTo(ppProgram.GetName(l)) <= 0)
                                        {
                                            // Insert the name
                                            ppProgram.InsertRow(l, strFirstName, strLastName, m_strTroopNumber);
                                            blnNameInserted = true;
                                        }

                                        l++;
                                    }

                                    k++;
                                }
                            }
                            else
                            {
                                while (k < ppProgram.NumberOfSheets && !blnNameInserted)
                                {
                                    ppProgram.SelectSheet(k);
                                    int l = ProgramPaperwork.StartRow;
                                    // Search each sheet
                                    while (l < (ProgramPaperwork.StartRow + ProgramPaperwork.SheetSize) && !blnNameInserted)
                                    {
                                        if (ppProgram.GetName(l) == null)
                                        {
                                            // Add the name to the sheet
                                            ppProgram.SetName(l, strLastName + ", " + strFirstName);
                                            ppProgram.SetTroop(l, m_strTroopNumber);
                                            blnNameInserted = true;
                                        }
                                        else if (int.Parse(strTroopNumber) <= int.Parse(ppProgram.GetTroop(l)))
                                        {
                                            // Insert the name
                                            ppProgram.InsertRow(l, strFirstName, strLastName, m_strTroopNumber);
                                            blnNameInserted = true;
                                        }

                                        l++;
                                    }

                                    k++;
                                }
                            }

                            ppProgram.Dispose();
                        }
                        else
                        {
                            // Open the program paperwork
                            ProgramPaperwork ppProgram = new ProgramPaperwork(strProgramPaperworkPath, false);
                            // Search for the proper place to insert the name
                            int k = 0;
                            bool blnNameInserted = false;
                            while (k < ppProgram.NumberOfSheets && !blnNameInserted)
                            {
                                ppProgram.SelectSheet(k);
                                int l = ProgramPaperwork.StartRow;
                                // Search each sheet
                                while (l < (ProgramPaperwork.StartRow + ProgramPaperwork.SheetSize) && !blnNameInserted)
                                {
                                    if (ppProgram.GetName(l) == null)
                                    {
                                        // Add the name to the sheet
                                        ppProgram.SetName(l, strLastName + ", " + strFirstName);
                                        ppProgram.SetTroop(l, m_strTroopNumber);
                                        blnNameInserted = true;
                                    }
                                    else if ((strName.CompareTo(ppProgram.GetName(l)) <= 0))
                                    {
                                        // Insert the name
                                        ppProgram.InsertRow(l, strFirstName, strLastName, m_strTroopNumber);
                                        blnNameInserted = true;
                                    }

                                    l++;
                                }

                                k++;
                            }

                            ppProgram.Dispose();
                        }
                    }
                    else
                    {
                        if (this.m_blnSortScouts)
                        {
                            // Open the Excel Spreadsheet
                            ExcelSpreadsheet objSpreadsheet = new ExcelSpreadsheet(strProgramPaperworkPath, false);
                            // Search for the proper place to insert the name
                            bool blnNameInserted = false;
                            int l = ExcelSpreadsheet.StartRow;
                            // Search each sheet
                            if (Backsplice.Properties.Settings.Default.SortAlphabetically && Backsplice.Properties.Settings.Default.SortByTroop)
                            {
                                while (l <= objSpreadsheet.LastRow && !blnNameInserted)
                                {
                                    if ((strName.CompareTo(objSpreadsheet.GetName(l)) <= 0 && int.Parse(strTroopNumber) <= int.Parse(objSpreadsheet.GetTroop(l))))
                                    {
                                        // Insert the name
                                        objSpreadsheet.InsertRow(l, strFirstName, strLastName, m_strTroopNumber);
                                        blnNameInserted = true;
                                    }

                                    l++;
                                }
                            }
                            else if (Backsplice.Properties.Settings.Default.SortAlphabetically)
                            {
                                while (l <= objSpreadsheet.LastRow && !blnNameInserted)
                                {
                                    if (strName.CompareTo(objSpreadsheet.GetName(l)) <= 0)
                                    {
                                        // Insert the name
                                        objSpreadsheet.InsertRow(l, strFirstName, strLastName, m_strTroopNumber);
                                        blnNameInserted = true;
                                    }

                                    l++;
                                }
                            }
                            else
                            {
                                while (l <= objSpreadsheet.LastRow && !blnNameInserted)
                                {
                                    if (int.Parse(strTroopNumber) <= int.Parse(objSpreadsheet.GetTroop(l)))
                                    {
                                        // Insert the name
                                        objSpreadsheet.InsertRow(l, strFirstName, strLastName, m_strTroopNumber);
                                        blnNameInserted = true;
                                    }

                                    l++;
                                }
                            }

                            if (!blnNameInserted)
                            {
                                objSpreadsheet.InsertRow(l, strFirstName, strLastName, m_strTroopNumber);
                            }

                            objSpreadsheet.Dispose();
                        }
                        else
                        {
                            // Open the Excel Spreadsheet
                            ExcelSpreadsheet objSpreadsheet = new ExcelSpreadsheet(strProgramPaperworkPath, false);
                            // Search for the proper place to insert the name
                            bool blnNameInserted = false;
                            int l = ExcelSpreadsheet.StartRow;
                            // Search each sheet
                            while (l <= objSpreadsheet.LastRow && !blnNameInserted)
                            {
                                if ((strName.CompareTo(objSpreadsheet.GetName(l)) <= 0))
                                {
                                    // Insert the name
                                    objSpreadsheet.InsertRow(l, strFirstName, strLastName, m_strTroopNumber);
                                    blnNameInserted = true;
                                }

                                l++;
                            }

                            if (!blnNameInserted)
                            {
                                objSpreadsheet.InsertRow(l + 1, strFirstName, strLastName, m_strTroopNumber);
                            }

                            objSpreadsheet.Dispose();
                        }
                    }

                    this.Cursor = Cursors.Default;
                    this.OnClose();

                    return;
                }
            }

            // The user has not created paperwork for the selected week, or has deleted all or part of the paperwork
            MessageBox.Show("The paperwork for " + strProgram + " does not exist", "Paperwork not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Cursor = Cursors.Default;
            return;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.OnAdd();
        }

        private void dgvPrograms_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.OnAdd();
        }

        private void AddScout_Load(object sender, EventArgs e)
        {
            ReadCourses();
        }
    }
}
