using System;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Linq;

namespace Backsplice
{
    public partial class CreatePaperwork : Form
    {
        // CHOICES
        private const int cm_intDO_NOTHING = 1;  // If a template does not exist, do not create any paperwork for the course and move to the next one.
        private const int cm_intUSE_DEFAULT_TEMPLATE = 2;  // If a template does not exist, create paperwork using an empty paperwork template.
        private const int cm_intUSE_EXCEL_SPREADSHEET = 3;  // If a template does not exist, create a new excel document and write the course information to it.

        private int m_intNoTemplateAction = BackspliceMain.NoTemplate; // Tells the create paperwork method what action to take if a course does not have a template.  See the above section labeled CHOICES
        private bool m_blnSortScouts = BackspliceMain.SortScouts;
        private bool m_blnMakePaperwork = BackspliceMain.MakePaperwork;
        private string m_strWeek;

        // Delegates
        private delegate void SetProgressBarMaximumDelegate(int _intMaximum);
        private delegate void SetProgressBarValueDelegate(int _intValue);
        private delegate void ProgressBarStepDelegate();

        /// <summary>
        /// Default constructor
        /// </summary>
        public CreatePaperwork()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handler for the week combo box
        /// </summary>
        private void cboWeek_TextChanged(object sender, EventArgs e)
        {
            cboWeek.BackColor = SystemColors.Window;

            if (!cboWeek.Items.Contains(cboWeek.Text) && cboWeek.Text != "")
            {
                MessageBox.Show("Entry must be numeric and in the range 1-8.", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboWeek.BackColor = BackspliceMain.InvalidEntry;
                txtDoubleKnotRoster.Enabled = false;
                btnBrowse.Enabled = false;
                mnuBrowse.Enabled = false;
                btnCreate.Enabled = false;
                mnuCreatePaperwork.Enabled = false;
            }
            else if (cboWeek.Text == "")
            {
                cboWeek.BackColor = SystemColors.Window;
                txtDoubleKnotRoster.Enabled = false;
                btnBrowse.Enabled = false;
                mnuBrowse.Enabled = false;
                btnCreate.Enabled = false;
                mnuCreatePaperwork.Enabled = false;
            }
            else
            {
                cboWeek.BackColor = SystemColors.Window;
                txtDoubleKnotRoster.Enabled = true;
                btnBrowse.Enabled = true;
                mnuBrowse.Enabled = true;
                if (txtDoubleKnotRoster.Text != "")
                {
                    btnCreate.Enabled = true;
                    mnuCreatePaperwork.Enabled = true;
                }
                else
                {
                    btnCreate.Enabled = false;
                    mnuCreatePaperwork.Enabled = false;
                }
            }
        }
        
        /// <summary>
        /// Fills a default blank template for the current program
        /// </summary>
        /// <param name="_objDoubleKnot">DoubleKnot roster</param>
        /// <param name="_intRow">row in the DoubleKnot roster</param>
        /// <param name="_intNumberOfScouts">number of scouts in the program</param>
        /// <param name="_strTemplateFile">path to the default template file</param>
        /// <param name="_strProgram">name of the program</param>
        /// <param name="_strPeriod">period the program is taking place</param>
        private void fillDefaultTemplate(DoubleKnotRoster _objDoubleKnot, ScoutList _slScouts, string _strTemplateFile, string _strProgram, string _strPeriod)
        {
            // Fill the paperwork as usual
            fillRegularTemplate(_objDoubleKnot, _slScouts, _strTemplateFile, _strProgram, _strPeriod);

            // Set the program name
            string strPaperworkDirectory = BackspliceMain.SaveDirectory + @"\Week " + m_strWeek + @"\" + _strProgram;
            string strPaperworkFile = strPaperworkDirectory + @"\" + _strProgram.ToLower() + "_" + _strPeriod + ".xlsx";
            ProgramPaperwork paperwork = new ProgramPaperwork(strPaperworkFile, false);
            paperwork.SetProgramName(_strProgram);
            paperwork.Dispose();
        }

        /// <summary>
        /// Fills a regular excel spreadsheet for the current program
        /// </summary>
        /// <param name="_objDoubleKnot">DoubleKnot roster</param>
        /// <param name="_intRow">row in the DoubleKnot roster</param>
        /// <param name="_intNumberOfScouts">number of scouts in the program</param>
        /// <param name="_strProgram">name of the current program</param>
        /// <param name="_strPeriod">period the program is taking place</param>
        private void fillExcelSpreadsheet(DoubleKnotRoster _objDoubleKnot, ScoutList _slScouts, string _strProgram, string _strPeriod)
        {
            // Get the paperwork save directory
            string strPaperworkDirectory = BackspliceMain.SaveDirectory + @"\Week " + m_strWeek + @"\" + _strProgram;

            // If the path to the paperwork save directory does not exist, create it
            if (!System.IO.Directory.Exists(strPaperworkDirectory))
            {
                System.IO.Directory.CreateDirectory(strPaperworkDirectory);
            }

            // Get the file name for the paperwork
            string strPaperworkFile = strPaperworkDirectory + @"\" + _strProgram.ToLower() + "_" + _strPeriod + ".xlsx";

            // Create a new spreadsheet
            ExcelSpreadsheet objExcelSpreadsheet = new ExcelSpreadsheet(strPaperworkFile, false);
            objExcelSpreadsheet.SetPeriod(_strPeriod);
            objExcelSpreadsheet.SetProgram(_strProgram);

            // Fill the spreadsheet
            int intPaperworkSheetNumber = 0;
            System.Diagnostics.Debug.WriteLine(_strProgram + ": " + intPaperworkSheetNumber);
            int intScoutsAdded = 0;
            for (int i = 0; i < _slScouts.Count; i++)
            {
                Scout sScout = (Scout)_slScouts[i];
                objExcelSpreadsheet.SetName(ExcelSpreadsheet.StartRow + intScoutsAdded, sScout.GetName());
                objExcelSpreadsheet.SetTroop(ExcelSpreadsheet.StartRow + intScoutsAdded, sScout.GetTroopString());

                intScoutsAdded += 1;
                ProgressBarStep();
            }

            objExcelSpreadsheet.Dispose();
        }

        /// <summary>
        /// Fills a paperwork template for the current program
        /// </summary>
        /// <param name="_objDoubleKnot">a DoubleKnot roster</param>
        /// <param name="_intRow">a row in the double knot roster</param>
        /// <param name="_intNumberOfScouts">number of scouts in the program</param>
        /// <param name="_strTemplateFile">path to the program's template file</param>
        /// <param name="_strProgram">name of the program</param>
        /// <param name="_strPeriod">period the program is taking place</param>
        private void fillRegularTemplate(DoubleKnotRoster _objDoubleKnot, ScoutList _slScouts, string _strTemplateFile, string _strProgram, string _strPeriod)
        {
            // Get the paperwork save directory
            string strPaperworkDirectory = BackspliceMain.SaveDirectory + @"\Week " + m_strWeek + @"\" + _strProgram;

            // If the path to the paperwork save directory does not exist, create it
            if (!System.IO.Directory.Exists(strPaperworkDirectory))
            {
                System.IO.Directory.CreateDirectory(strPaperworkDirectory);
            }

            // Copy the template to the appropriate directory
            string strPaperworkFile = strPaperworkDirectory + @"\" + _strProgram.ToLower() + "_" + _strPeriod + ".xlsx";
            System.IO.File.Copy(_strTemplateFile, strPaperworkFile, true);

            // Prepare the template for processing
            ProgramPaperwork objPaperwork = new ProgramPaperwork(strPaperworkFile, false);
            objPaperwork.SetWeek(m_strWeek);
            objPaperwork.SetPeriod(_strPeriod);

            // Set the week and period header
            string[] strPeriodParts = _strPeriod.Split();
            int intPeriodNum;
            string strPeriod;
            bool blnNumeric = int.TryParse(strPeriodParts[strPeriodParts.Length - 1], out intPeriodNum);
            if (!blnNumeric)
            {
                strPeriod = _strPeriod;
            }
            else
            {
                strPeriod = strPeriodParts[strPeriodParts.Length - 1];
            }

            // Create the appropriate number of sheets
            double dblResult = _slScouts.Count / ProgramPaperwork.SheetSize;
            int intNumberOfSheets = (int)Math.Ceiling(dblResult) - 1;
            System.Diagnostics.Debug.WriteLine(_strProgram + " " + "Num Scouts: " + _slScouts.Count + " Result: " + dblResult + " Num Sheets " + intNumberOfSheets);

            objPaperwork.AddSheets(intNumberOfSheets);

            // Fill the paperwork
            int intPaperworkSheetNumber = 0;
            System.Diagnostics.Debug.WriteLine(_strProgram + ": " + intPaperworkSheetNumber);
            int intScoutsAdded = 0;
            for (int i = 0; i < _slScouts.Count; i++)
            {
                Scout sScout = (Scout)_slScouts[i];
                objPaperwork.SetName(ProgramPaperwork.StartRow + intScoutsAdded, sScout.GetName());
                objPaperwork.SetTroop(ProgramPaperwork.StartRow + intScoutsAdded, sScout.GetTroopString());

                intScoutsAdded += 1;
                // If the current sheet is full
                if (intScoutsAdded == 19 && intScoutsAdded < _slScouts.Count)
                {
                    // Switch to the next sheet
                    intPaperworkSheetNumber += 1;
                    objPaperwork.SelectSheet(intPaperworkSheetNumber);
                    intScoutsAdded = 0;
                }

                ProgressBarStep();
            }

            objPaperwork.Dispose();
        }

        /// <summary>
        /// Event handler for browse events
        /// </summary>
        private void OnBrowse(object sender, EventArgs e)
        {
            if (ofdDoubleKnot.ShowDialog() == DialogResult.OK)
            {
                txtDoubleKnotRoster.Text = ofdDoubleKnot.FileName;
            }
        }

        /// <summary>
        /// Closes the create paperwork window
        /// </summary>
        private void OnClose(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Creates paperwork for the specified week
        /// </summary>
        private void OnCreate(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(txtDoubleKnotRoster.Text))
            {
                // If the specified file does not exist
                MessageBox.Show("The specified file does not exist.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDoubleKnotRoster.BackColor = BackspliceMain.InvalidEntry;
                txtDoubleKnotRoster.Focus();
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                m_strWeek = cboWeek.Text;
                cboWeek.Enabled = false;
                txtDoubleKnotRoster.Enabled = false;
                btnBrowse.Enabled = false;
                btnCreate.Enabled = false;
                btnClose.Enabled = false;
                bgwCreate.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Event handler for the DoubleKnot roster text box
        /// </summary>
        private void txtDoubleKnotRoster_TextChanged(object sender, EventArgs e)
        {
            txtDoubleKnotRoster.BackColor = SystemColors.Window;

            if (txtDoubleKnotRoster.Text != "")
            {
                btnCreate.Enabled = true;
                mnuCreatePaperwork.Enabled = true;
            }
            else
            {
                btnCreate.Enabled = false;
                mnuCreatePaperwork.Enabled = false;
            }
        }

        private void bgwCreate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (!System.IO.File.Exists(txtDoubleKnotRoster.Text))
            {
                // If the specified file does not exist
                MessageBox.Show("The specified file does not exist.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDoubleKnotRoster.BackColor = BackspliceMain.InvalidEntry;
                txtDoubleKnotRoster.Focus();
            }
            else
            {
                // Get the week
                string strWeek = m_strWeek;
                // Copy the DoubleKnot roster to the paperwork directory for the week
                string strRosterSavePath = BackspliceMain.SaveDirectory;
                strRosterSavePath += @"\Week " + strWeek;
                System.Diagnostics.Debug.WriteLine(strRosterSavePath);

                // If the path to the save directory does not exist, create it
                if (!System.IO.Directory.Exists(strRosterSavePath))
                {
                    System.IO.Directory.CreateDirectory(strRosterSavePath);
                }

                // Get the file extension of the double knot roster (could be .xls or .xlsx)
                System.IO.FileInfo objRosterInfo = new System.IO.FileInfo(txtDoubleKnotRoster.Text);
                strRosterSavePath += @"\Week " + strWeek + objRosterInfo.Extension;
                // Copy the DoubleKnot roster to the current week's paperwork directory
                DoubleKnotRoster dkrRoster;
                
                System.IO.File.Copy(txtDoubleKnotRoster.Text, strRosterSavePath, true);

                // Sort the Doubleknot roster copy
                SortRoster(strRosterSavePath);

                // Open the DoubleKnot roster copy
                dkrRoster = new DoubleKnotRoster(strRosterSavePath, true);

                // Read all rows in the sheet
                int intRow = DoubleKnotRoster.StartRow;
                SetProgressBarMaximum(dkrRoster.Rows);
                SetProgressBarValue(intRow);

                while (intRow <= dkrRoster.Rows)
                {
                    // If the description does not contain Summer Camp Week
                    if (!dkrRoster.extractProgram(intRow).Contains("Summer Camp Week"))
                    {
                        // Get the program
                        string strProgram = dkrRoster.extractProgram(intRow);
                        // Get the period
                        string strPeriod = dkrRoster.extractPeriod(intRow);
                        // Count the number of scouts in the program
                        int intNumberOfScouts = 1; // REMOVE
                        ScoutList slScouts = new ScoutList();
                        slScouts.Add(new Scout(dkrRoster.extractName(intRow), dkrRoster.extractTroopNumber(intRow)));
                        if (intRow + 1 <= dkrRoster.Rows - 1)
                        {
                            intRow = intRow + 1;
                            string strTheProgram = dkrRoster.extractProgram(intRow);
                            string strThePeriod = dkrRoster.extractPeriod(intRow);

                            while (intRow <= dkrRoster.Rows && strTheProgram.Equals(strProgram) && strThePeriod.Equals(strPeriod))
                            {
                                intNumberOfScouts += 1; // REMOVE

                                slScouts.Add(new Scout(dkrRoster.extractName(intRow), dkrRoster.extractTroopNumber(intRow)));

                                intRow += 1;
                                if (intRow < dkrRoster.Rows)
                                {
                                    strTheProgram = dkrRoster.extractProgram(intRow);
                                    strThePeriod = dkrRoster.extractPeriod(intRow);
                                }
                            }
                        }

                        // Remove duplicates
                        int oldCount = slScouts.Count;
                        Scout[] scoutArray = new Scout[slScouts.Count];
                        for (int i = 0; i < scoutArray.Length; i++)
                        {
                            scoutArray[i] = (Scout)slScouts[i];
                        }

                        scoutArray = Enumerable.Distinct<Scout>(scoutArray).ToArray<Scout>();

                        slScouts = new ScoutList();
                        foreach (Scout scout in scoutArray)
                        {
                            slScouts.Add(scout);
                        }

                        int newCount = oldCount - slScouts.Count;
                        for (int i = 0; i < newCount; i++)
                        {
                            ProgressBarStep();
                        }

                        // If we are sorting the scouts
                        if (BackspliceMain.SortScouts)
                        {
                            slScouts.Sort();
                        }

                        // If we are making paperwork
                        if (BackspliceMain.MakePaperwork)
                        {
                            // If the program has a template
                            string strTemplateFile = BackspliceMain.TemplateDirectory + @"\" + strProgram + ".xlsx";
                            if (System.IO.File.Exists(strTemplateFile))
                            {
                                // Fill a paperwork template
                                this.fillRegularTemplate(dkrRoster, slScouts, strTemplateFile, strProgram, strPeriod);
                            }
                            else
                            {
                                // Take the appropriate no template action
                                switch (BackspliceMain.NoTemplate)
                                {
                                    case 1:  // Skip this course and move on
                                        for (int i = 0; i < intNumberOfScouts; i++)
                                        {
                                            ProgressBarStep();
                                        }
                                        break;
                                    case 2:  // Use the blank paperwork template
                                        strTemplateFile = BackspliceMain.TemplateDirectory + @"\Default Blank.xlsx";
                                        this.fillDefaultTemplate(dkrRoster, slScouts, strTemplateFile, strProgram, strPeriod);
                                        break;
                                    case 3:  // Use a new excel spreadsheet
                                        this.fillExcelSpreadsheet(dkrRoster, slScouts, strProgram, strPeriod);
                                        break;
                                }
                            }
                        }
                        else
                        {
                            // Fill an excel spreadsheet
                            this.fillExcelSpreadsheet(dkrRoster, slScouts, strProgram, strPeriod);
                        }
                    }
                    else
                    {
                        // Skip the row
                        intRow++;
                        ProgressBarStep();
                    }
                }

                dkrRoster.Dispose();
            }
        }

        private void SortRoster(string strRosterSavePath)
        {
            Excel.Application m_objExcel;
            Excel.Workbooks m_objWorkbooks;
            Excel.Workbook m_objPaperwork;

            m_objExcel = new Excel.Application();
            m_objExcel.DisplayAlerts = false;

            m_objWorkbooks = m_objExcel.Workbooks;
            m_objPaperwork = m_objWorkbooks.Open(strRosterSavePath, Missing.Value);


            Excel.Worksheet mainSheet = (Excel.Worksheet)m_objPaperwork.Sheets[1];
            int lastRow = 0;
            lastRow = mainSheet.UsedRange.Row + mainSheet.UsedRange.Rows.Count - 1;

            //Excel.Range range = mainSheet.Range[mainSheet.Cells[1, 2], mainSheet.UsedRange.Cells[mainSheet.UsedRange.Cells.Count, mainSheet.UsedRange.Rows.Count]];
            Excel.Range range = mainSheet.Range["A2", "F" + lastRow];
            range.Select();
            mainSheet.Sort.SortFields.Add(mainSheet.Range["D2", "D" + lastRow], Type.Missing, Excel.XlSortOrder.xlAscending);

            mainSheet.Sort.SetRange(range);
            mainSheet.Sort.Header = Excel.XlYesNoGuess.xlYes;
            mainSheet.Sort.MatchCase = false;
            mainSheet.Sort.Orientation = Excel.XlSortOrientation.xlSortColumns;
            mainSheet.Sort.SortMethod = Excel.XlSortMethod.xlPinYin;
            mainSheet.Sort.Apply();

            m_objPaperwork.Save();
            m_objPaperwork.Close(false, null, false);

            Marshal.ReleaseComObject(range);
            Marshal.ReleaseComObject(mainSheet);
            Marshal.ReleaseComObject(m_objPaperwork);
            Marshal.ReleaseComObject(m_objWorkbooks);


            m_objExcel.Quit();
            Marshal.ReleaseComObject(m_objExcel);
        }

        private void bgwCreate_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Cursor = Cursors.Default;
            cboWeek.Enabled = true;
            txtDoubleKnotRoster.Enabled = true;
            btnBrowse.Enabled = true;
            btnCreate.Enabled = true;
            btnClose.Enabled = true;
            MessageBox.Show("Back Splice has finished creating the paperwork for week " + cboWeek.Text + ".", "Creation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetProgressBarMaximum(int _intMaximum)
        {
            if (!InvokeRequired)
            {
                pgbProgress.Maximum = _intMaximum;
            }
            else
            {
                Invoke(new SetProgressBarMaximumDelegate(SetProgressBarMaximum), new object[] { _intMaximum });
            }
        }

        private void SetProgressBarValue(int _intValue)
        {
            if (!InvokeRequired)
            {
                pgbProgress.Value = _intValue;
            }
            else
            {
                Invoke(new SetProgressBarValueDelegate(SetProgressBarValue), new object[] { _intValue });
            }
        }

        private void ProgressBarStep()
        {
            if (!InvokeRequired)
            {
                pgbProgress.PerformStep();
            }
            else
            {
                Invoke(new ProgressBarStepDelegate(ProgressBarStep));
            }
        }

        private void bgwCreate_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
        
        }
    }
}
