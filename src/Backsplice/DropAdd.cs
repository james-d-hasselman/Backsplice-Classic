using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Backsplice
{
    public partial class DropAdd : Form
    {
        private List<CampProgram> m_objResults = new List<CampProgram>();
        private string m_strFirstName;
        private string m_strWeek;
        private string m_strLastName;

        public DropAdd()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructs a DropAdd dialog that displays the given information about a scout.  This constructor is used when a
        /// user selects Drop/Add from the FindScout dialog.
        /// </summary>
        /// <param name="_objMeritBadges">A list of merit badges that the scout is enrolled in</param>
        /// <param name="_strWeek">The week that the scout came to camp</param>
        /// <param name="_strName">The full name of the scout</param>
        /// <param name="_strTroopNumber">The scout's troop</param>
        public DropAdd(List<CampProgram> _objMeritBadges, string _strWeek, string _strName, string _strTroopNumber)
        {
            InitializeComponent();

            m_objResults = new List<CampProgram>(_objMeritBadges);

            for (int i = 0; i < m_objResults.Count; i++)
            {
                string[] strValues = new string[2];
                strValues[0] = m_objResults[i].Name;
                strValues[1] = m_objResults[i].Period;
                dgvMeritBadges.Rows.Add(strValues);
            }

            lblScoutWeek.Text = _strWeek;
            lblScoutName.Text = _strName;
            lblScoutTroop.Text = _strTroopNumber;
        }

        /// <summary>
        /// Searches for a scout and displays the scout's name, troop, programs in which the scout is enrolled, and the week
        /// that the scout attended camp.  If the find operation is successful, the add operation is enabled.
        /// </summary>
        /// <param name="_strWeek">the week to search</param>
        /// <param name="_strFirstName">the first name of the scout to find</param>
        /// <param name="_strLastName">the last name of the scout to find</param>
        private void OnFind(string _strWeek, string _strFirstName, string _strLastName)
        {
            this.Cursor = Cursors.WaitCursor;
            lblScoutWeek.Text = "";
            lblScoutName.Text = "";
            lblScoutTroop.Text = "";
            btnDrop.Enabled = false;
            mnuDrop.Enabled = false;
            btnAdd.Enabled = false;
            mnuAdd.Enabled = false;
            dgvMeritBadges.Rows.Clear();
            m_objResults.Clear();

            string strTroopNumber;
            string strWeek = _strWeek;
            string strFirstName = _strFirstName;
            string strLastName = _strLastName;

            this.Cursor = Cursors.WaitCursor;
            this.m_objResults = BackspliceMain.FindScout(strWeek, strFirstName, strLastName, out strTroopNumber);

            if (m_objResults != null && m_objResults.Count > 0)
            {
                for (int i = 0; i < m_objResults.Count; i++)
                {
                    string[] strValues = new string[2];
                    strValues[0] = m_objResults[i].Name;
                    strValues[1] = m_objResults[i].Period;
                    dgvMeritBadges.Rows.Add(strValues);
                } 

                lblScoutWeek.Text = strWeek;
                lblScoutName.Text = strFirstName + " " + strLastName;
                lblScoutTroop.Text = strTroopNumber;
                btnAdd.Enabled = true;
                mnuAdd.Enabled = true;

                this.Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show("Scout " + "\"" + strFirstName + " " + strLastName + "\"" + " was not found.  Did you select the correct week?", "Scout Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Cursor = Cursors.Default;
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            m_strWeek = cboWeek.Text;
            m_strFirstName = txtFirstName.Text;
            m_strLastName = txtLastName.Text;
            this.OnFind(cboWeek.Text, txtFirstName.Text, txtLastName.Text);
        }

        private void cboWeek_TextChanged(object sender, EventArgs e)
        {
            cboWeek.BackColor = SystemColors.Window;

            if (!cboWeek.Items.Contains(cboWeek.Text) && cboWeek.Text != "")
            {
                int intWeek;
                int.TryParse(cboWeek.Text, out intWeek);
                // TODO hardcoded values for week number. 
                if (intWeek >= 1 && intWeek <= 8)
                {
                    MessageBox.Show("The paperwork for week " + intWeek + " has not been created yet.", "Paperwork Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Entry must be numeric and in the range 1-8.", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    cboWeek.BackColor = BackspliceMain.InvalidEntry;
                    txtFirstName.Enabled = false;
                    txtLastName.Enabled = false;
                    btnFind.Enabled = false;
                    mnuFind.Enabled = false;
                    btnDrop.Enabled = false;
                    mnuDrop.Enabled = false;
                    btnAdd.Enabled = false;
                    mnuAdd.Enabled = false;
                }
            }
            else if (cboWeek.Text == "")
            {
                txtFirstName.Enabled = false;
                txtLastName.Enabled = false;
                btnFind.Enabled = false;
                mnuFind.Enabled = false;
                btnDrop.Enabled = false;
                mnuDrop.Enabled = false;
                btnAdd.Enabled = false;
                mnuAdd.Enabled = false;
            }
            else
            {
                txtFirstName.Enabled = true;
                txtLastName.Enabled = true;
                btnDrop.Enabled = false;
                mnuDrop.Enabled = false;
                btnAdd.Enabled = false;
                mnuAdd.Enabled = false;
                if (txtFirstName.Text != "" && txtLastName.Text != "")
                {
                    btnFind.Enabled = true;
                    mnuFind.Enabled = true;
                }
                else
                {
                    btnFind.Enabled = false;
                    mnuFind.Enabled = false;
                }
            }
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            this.OnNameEntry();
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            this.OnNameEntry();
        }

        private void OnNameEntry()
        {
            if (txtFirstName.Text != "" && txtLastName.Text != "")
            {
                btnFind.Enabled = true;
                mnuFind.Enabled = true;
            }
            else
            {
                btnFind.Enabled = false;
                mnuFind.Enabled = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.OnClose();
        }

        private void OnClose()
        {
            this.Close();
        }

        private void mnuFind_Click(object sender, EventArgs e)
        {
            this.OnFind(cboWeek.Text, txtFirstName.Text, txtLastName.Text);
        }

        private void mnuDrop_Click(object sender, EventArgs e)
        {
            this.OnDrop();
        }

        // TODO much better, still worth another look.
        private void OnDrop()
        {
            this.Cursor = Cursors.WaitCursor;
             // Get the week
            string strWeek = lblScoutWeek.Text;
            char[] chrSeparator = new char[] {' '};
            string[] strNameParts = lblScoutName.Text.Split(chrSeparator);
            // Get the first name
            string strFirstName = strNameParts[0].Trim();
            // Get the last name
            string strLastName = strNameParts[1].Trim();
            // Get the troop number
            string strTroopNumber = lblScoutTroop.Text;
            // Get the program
            string strProgram = dgvMeritBadges.SelectedRows[0].Cells[0].Value.ToString();
            // Get the period
            string strPeriod = dgvMeritBadges.SelectedRows[0].Cells[1].Value.ToString();


            // Get the directory where the DoubleKnot roster is located
            string strRosterDirectoryPath = BackspliceMain.SaveDirectory + @"\Week " + strWeek;

            if (System.IO.Directory.Exists(strRosterDirectoryPath))
            {
                // Find the roster
                string[] strFiles = System.IO.Directory.GetFiles(strRosterDirectoryPath, "Week " + strWeek + "*");
                if (strFiles.Length > 0)
                {
                    // Remove the scout from the Doubleknot roster.
                    DeleteFromDoubleknotRoster(strFirstName, strLastName, strTroopNumber, strProgram, strPeriod, strFiles[0]);

                    if (System.IO.Directory.Exists(strRosterDirectoryPath + @"\" + strProgram))
                    {
                        // Find the roster
                        string[] strRosterFiles = System.IO.Directory.GetFiles(strRosterDirectoryPath + @"\" + strProgram, strProgram.ToLower() + "_" + strPeriod + "*");
                        if (strRosterFiles.Length > 0)
                        {
                            DeleteFromProgramPaperwork(strFirstName, strLastName, strTroopNumber, strRosterFiles[0]);

                            this.Cursor = Cursors.Default;
                            return;
                        }
                        else
                        {
                            // The user has not created paperwork for the selected week, or has deleted all or part of the paperwork
                            MessageBox.Show("The paperwork for " + strProgram + " does not exist", "Paperwork not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Cursor = Cursors.Default;
                            return;
                        }
                    }
                    else
                    {
                        // The user has not created paperwork for the selected week, or has deleted all or part of the paperwork
                        MessageBox.Show("The paperwork for " + strProgram + " does not exist", "Paperwork not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                else
                {
                    // The user has not created paperwork for the selected week, or has deleted all or part of the paperwork
                    MessageBox.Show("The paperwork for Week " + strWeek + " does not exist", "Paperwork not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                    return;
                }
            }
            else
            {
                // The user has not created paperwork for the selected week, or has deleted all or part of the paperwork
                MessageBox.Show("The paperwork for Week " + strWeek + " does not exist", "Paperwork not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
                return;
            }
        }

        private static void DeleteFromProgramPaperwork(string strFirstName, string strLastName, string strTroopNumber, string strProgramPaperworkPath)
        {
            System.Diagnostics.Debug.WriteLine(strProgramPaperworkPath);

            if (ProgramPaperwork.IsPaperworkSheet(strProgramPaperworkPath))
            {
                // Open the paperwork
                ProgramPaperwork ppProgram = new ProgramPaperwork(strProgramPaperworkPath, false);
                // Find the scout
                bool blnFound = false;
                for (int j = ProgramPaperwork.StartRow, sheet = 0, count = 0; !blnFound && j < ProgramPaperwork.LastRow && sheet < ppProgram.NumberOfSheets; j++, count++)
                {
                    // If we have reached the end of a sheet
                    if (count == ProgramPaperwork.SheetSize)
                    {
                        count = 0;
                        // Select the next sheet
                        ppProgram.SelectSheet(++count);
                    }

                    // If the scout has been found
                    if (ppProgram.GetLastName(j) == strLastName && ppProgram.GetFirstName(j) == strFirstName && ppProgram.GetTroop(j) == strTroopNumber)
                    {
                        blnFound = true;
                        // Remove the scout from the paperwork
                        ppProgram.RemoveRow(j);
                    }
                }

                ppProgram.Dispose();
            }
            else
            {
                // Open the excel spreadsheet
                ExcelSpreadsheet esProgram = new ExcelSpreadsheet(strProgramPaperworkPath, false);
                // Find the scout
                bool blnFound = false;
                for (int j = ExcelSpreadsheet.StartRow; !blnFound && j <= esProgram.LastRow; j++)
                {
                    // If the scout has been found
                    if (esProgram.GetName(j) == strLastName + ", " + strFirstName && esProgram.GetTroop(j) == strTroopNumber)
                    {
                        blnFound = true;
                        // Remove the scout from the paperwork
                        esProgram.RemoveRow(j);
                    }
                }

                esProgram.Dispose();
            }
        }

        private void DeleteFromDoubleknotRoster(string strFirstName, string strLastName, string strTroopNumber, string strProgram, string strPeriod, string strRosterPath)
        {
            // Open the DoubleKnot roster
            DoubleKnotRoster dkrRoster = new DoubleKnotRoster(strRosterPath, false);
            int i = DoubleKnotRoster.StartRow;
            while (i <= dkrRoster.Rows)
            {
                if (dkrRoster.extractLastName(i).Equals(strLastName) && dkrRoster.extractFirstName(i).Equals(strFirstName) && dkrRoster.extractTroopNumber(i).Equals(strTroopNumber) && dkrRoster.extractProgram(i).Equals(strProgram) && dkrRoster.extractPeriod(i).Equals(strPeriod))
                {
                    System.Diagnostics.Debug.WriteLine(dkrRoster.extractLastName(i) + " " + dkrRoster.extractProgram(i) + " " + dkrRoster.extractPeriod(i));
                    dkrRoster.RemoveRow(i);
                }
                i++;
            }

            // Clean up after ourself
            dkrRoster.Dispose();

            dgvMeritBadges.Rows.Remove(dgvMeritBadges.SelectedRows[0]);
        }

        private void mnuAdd_Click(object sender, EventArgs e)
        {
            this.OnAdd();
        }

        private void OnAdd()
        {
            string strWeek = lblScoutWeek.Text;
            string strName = lblScoutName.Text.Trim();
            string[] strScoutNameParts = strName.Split(new char[] {' '});
            string strFirstName = strScoutNameParts[0];
            string strLastName = strScoutNameParts[1];
            string strTroopNumber = lblScoutTroop.Text;
            this.Cursor = Cursors.WaitCursor;
            AddScout asAddScout = new AddScout(strWeek, strFirstName, strLastName, strTroopNumber);
            this.Cursor = Cursors.Default;
            asAddScout.ShowDialog();

            // Overload find to take a week as a parameter
            this.OnFind(strWeek, strFirstName, strLastName);
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            this.OnClose();
        }

        private void btnDrop_Click(object sender, EventArgs e)
        {
            this.OnDrop();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.OnAdd();
        }

        private void dgvMeritBadges_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection drcSelectedRows = dgvMeritBadges.SelectedRows;
            if (drcSelectedRows.Count > 0)
            {
                btnDrop.Enabled = true;
                mnuDrop.Enabled = true;
            }
            else
            {
                btnDrop.Enabled = false;
                mnuDrop.Enabled = false;
            }
        }

        private void dgvMeritBadges_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dgvMeritBadges.RowCount > 0)
            {
                btnAdd.Enabled = true;
                mnuAdd.Enabled = true;
            }
        }

        private void DropAdd_Load(object sender, EventArgs e)
        {
            // Populate the week selection list with the available weeks
            string[] strWeeks = System.IO.Directory.GetDirectories(BackspliceMain.SaveDirectory);
            for (int i = 0; i < strWeeks.Length; i++)
            {
                string[] strWeekParts = strWeeks[i].Split(new char[] { '\\', ' ' });
                cboWeek.Items.Add(strWeekParts[strWeekParts.Length - 1]);
            }
        }

        private void dgvMeritBadges_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.OnDrop();
        }
    }
}
