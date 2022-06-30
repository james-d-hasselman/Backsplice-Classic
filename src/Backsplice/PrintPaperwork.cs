using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Backsplice
{
    public partial class PrintPaperwork : Form
    {
        private Excel.Application m_objExcel;
        private Excel.Workbooks m_objWorkbooks;
        private Excel.Workbook m_objPaperwork;
        private string strPrinter;
        private string m_strPaperworkPath = BackspliceMain.SaveDirectory;
        private string m_strProgramPath;

        public PrintPaperwork()
        {
            InitializeComponent();
        }

        private void OnClose()
        {
            this.Close();
        }

        private void OnSendTo()
        {
            DialogResult drResult = fbdSendTo.ShowDialog();
            
            if (drResult == DialogResult.OK)
            {
                // Get the week
                string strWeek = cboWeek.Text;
                // Get the progams
                string strPrograms = cboMeritBadge.Text;
                // Get the periods
                string strPeriods = cboPeriod.Text;
                // Get the year
                string strYear = DateTime.Now.Year.ToString();
                // Get the destination
                string strDestination = fbdSendTo.SelectedPath;
                // If the user wants to all periods of all programs in the given program area or areas
                if (strPrograms == "All" && strPeriods == "All")
                {
                    DirectoryCopy(BackspliceMain.SaveDirectory + @"\Week " + strWeek, strDestination + @"\Week " + strWeek + " - " + strYear, true);
                }
                else if(strPrograms != "All" && strPeriods == "All")
                {
                    DirectoryCopy(BackspliceMain.SaveDirectory + @"\Week " + strWeek + @"\" + strPrograms, strDestination + @"\" + strPrograms + " - Week " + strWeek + " " + strYear, true);
                }
                else if (strPrograms != "All" && strPeriods != "All")
                {
                    System.Diagnostics.Debug.WriteLine("HERE FIRST");
                    string strDirectoryPath = BackspliceMain.SaveDirectory + @"\Week " + strWeek + @"\" + strPrograms;

                    string[] strProgramPeriods = System.IO.Directory.GetFiles(strDirectoryPath, "*" + strPeriods + "*");
                    for (int i = 0; i < strProgramPeriods.Length; i++)
                    {
                        System.Diagnostics.Debug.WriteLine("HERE NOW");
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(strProgramPeriods[i]);
                        if ((fileInfo.Extension.Equals(".xls") || fileInfo.Extension.Equals(".xlsx")) && strProgramPeriods[i].Contains("_"))
                        {
                            System.IO.Directory.CreateDirectory(strDestination + @"\" + strPrograms + " - " + strYear);
                            System.IO.FileInfo objFile = new System.IO.FileInfo(strProgramPeriods[i]);
                            System.IO.File.Copy(strProgramPeriods[i], strDestination + @"\" + strPrograms + " - " + strYear + @"\" + objFile.Name);
                        }
                    }
                }
                else if (strPrograms == "All" && strPeriods != "All")
                {
                    string[] strProgramAreaDirectories = System.IO.Directory.GetDirectories(BackspliceMain.SaveDirectory + @"\Week " + strWeek);
                    for (int i = 0; i < strProgramAreaDirectories.Length; i++)
                    {
                        System.IO.DirectoryInfo objProgramAreaDirectoryInfo = new System.IO.DirectoryInfo(strProgramAreaDirectories[i]);
                        string strProgramArea = objProgramAreaDirectoryInfo.Name;
                        string[] strProgramDirectories = System.IO.Directory.GetDirectories(strProgramAreaDirectories[i]);
                        
                        for (int j = 0; j < strProgramDirectories.Length; j++)
                        {
                            System.IO.DirectoryInfo objDirectoryInfo = new System.IO.DirectoryInfo(strProgramDirectories[j]);
                            string strProgram = objDirectoryInfo.Name;

                            string[] strProgramPeriods = System.IO.Directory.GetFiles(strProgramDirectories[j], "*" + strPeriods + "*");
                            for (int k = 0; k < strProgramPeriods.Length; k++)
                            {
                                System.IO.FileInfo fileInfo = new System.IO.FileInfo(strProgramPeriods[k]);
                                if ((fileInfo.Extension.Equals(".xls") || fileInfo.Extension.Equals(".xlsx")) && strProgramPeriods[k].Contains("_"))
                                {
                                    System.IO.Directory.CreateDirectory(strDestination + @"\Week " + strWeek + " - " + strYear + @"\" + strProgramArea + @"\" + strProgram);
                                    System.IO.FileInfo objFile = new System.IO.FileInfo(strProgramPeriods[k]);
                                    System.IO.File.Copy(strProgramPeriods[k], strDestination + @"\Week " + strWeek + " - " + strYear + @"\" + strProgramArea + @"\" + strProgram + @"\" + objFile.Name + objFile.Extension);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void OnPrint()
        { 
            DialogResult result = pdPrintDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                m_objExcel = new Excel.Application();
                strPrinter = pdPrintDialog.PrinterSettings.PrinterName;
                m_objExcel.DisplayAlerts = false;

                // Get the week
                string strWeek = cboWeek.Text;
                // Get the progams
                string strPrograms = cboMeritBadge.Text;
                // Get the periods
                string strPeriods = cboPeriod.Text;
                // Get the year
                string strYear = DateTime.Now.Year.ToString();
                // If the user wants to all periods of all programs in the given program area or areas
                if (strPrograms == "All" && strPeriods == "All")
                {
                    string[] strProgramList = new string[cboMeritBadge.Items.Count];
                    cboMeritBadge.Items.CopyTo(strProgramList, 0);
                    string strProgramPaperworkPath = BackspliceMain.SaveDirectory + @"\Week " + strWeek + @"\";
                    for (int i = 0; i < strProgramList.Length; i++)
                    {
                        if (strProgramList[i] != "All")
                        {
                            string[] strProgramPeriods = System.IO.Directory.GetFiles(strProgramPaperworkPath + @"\" + strProgramList[i]);
                            for (int j = 0; j < strProgramPeriods.Length; j++)
                            {
                                System.IO.FileInfo fileInfo = new System.IO.FileInfo(strProgramPeriods[j]);
                                if ((fileInfo.Extension.Equals(".xls") || fileInfo.Extension.Equals(".xlsx")) && strProgramPeriods[j].Contains("_"))
                                {
                                    this.PrintExcelFile(strProgramPeriods[j]);
                                } 
                            }
                        }
                    }
                }
                else if (strPrograms != "All" && strPeriods == "All")
                {
                    // The user wants to print all the paperwork for a single program
                    string strProgramPaperworkPath = BackspliceMain.SaveDirectory + @"\Week " + strWeek + @"\" + strPrograms;
                    string[] strProgramPeriods = System.IO.Directory.GetFiles(strProgramPaperworkPath);
                    for (int i = 0; i < strProgramPeriods.Length; i++)
                    {
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(strProgramPeriods[i]);
                        if ((fileInfo.Extension.Equals(".xls") || fileInfo.Extension.Equals(".xlsx")) && strProgramPeriods[i].Contains("_"))
                        {
                            this.PrintExcelFile(strProgramPeriods[i]);
                        } 
                    }
                }
                else if (strPrograms == "All" && strPeriods != "All")
                {
                    // The user wants to print all programs occurring during a given period
                    string[] strProgramList = new string[cboMeritBadge.Items.Count];
                    cboMeritBadge.Items.CopyTo(strProgramList, 0);
                    string strProgramPaperworkPath = BackspliceMain.SaveDirectory + @"\Week " + strWeek + @"\";
                    for (int i = 0; i < strProgramList.Length; i++)
                    {
                        if (strProgramList[i] != "All")
                        {
                            string[] strProgramPeriods = System.IO.Directory.GetFiles(strProgramPaperworkPath + strProgramList[i], strPeriods + "*");
                            for (int j = 0; j < strProgramPeriods.Length; j++)
                            {
                                System.IO.FileInfo fileInfo = new System.IO.FileInfo(strProgramPeriods[i]);
                                if ((fileInfo.Extension.Equals(".xls") || fileInfo.Extension.Equals(".xlsx")) && strProgramPeriods[i].Contains("_"))
                                {
                                    this.PrintExcelFile(strProgramPeriods[i]);
                                } 
                            }
                        }
                    }
                }
                else if (strPrograms != "All" && strPeriods != "All")
                {
                    // the user wants to print a specific period of a specific program
                    string strProgramPaperworkPath = BackspliceMain.SaveDirectory + @"\Week " + strWeek  + @"\" + strPrograms;
                    string[] strProgramPeriods = System.IO.Directory.GetFiles(strProgramPaperworkPath, strPrograms.ToLower() + "_" + strPeriods + "*");
                    for (int i = 0; i < strProgramPeriods.Length; i++)
                    {
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(strProgramPeriods[i]);
                        if ((fileInfo.Extension.Equals(".xls") || fileInfo.Extension.Equals(".xlsx")) && strProgramPeriods[i].Contains("_"))
                        {
                            this.PrintExcelFile(strProgramPeriods[i]);
                        } 
                    }
                }
                else
                {
                    MessageBox.Show("Unexpected condition!", "Uh-Oh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Cursor = Cursors.Default;
                }

                m_objExcel.Quit();
                Marshal.ReleaseComObject(m_objExcel);
                MessageBox.Show("The print operation is complete.", "Printing Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                
            }

            btnClose.Focus();
        }

        private void PrintExcelFile(string _strFile)
        {
            // Open the Paperwork
            //Excel.Workbooks objWorkbooks = m_objExcel.Workbooks;
            m_objWorkbooks = m_objExcel.Workbooks;
            m_objPaperwork = m_objWorkbooks.Open(_strFile, Missing.Value, true);
            m_objPaperwork.PrintOutEx(Missing.Value, Missing.Value, Missing.Value, false, strPrinter,  false, false, Missing.Value, true);
            m_objPaperwork.Close(false, null, false);

            Marshal.ReleaseComObject(m_objPaperwork);
            Marshal.ReleaseComObject(m_objWorkbooks);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.OnPrint();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.OnClose();
        }

        private void mnuPrint_Click(object sender, EventArgs e)
        {
            this.OnPrint();
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            this.OnClose();
        }

        private void cboWeek_TextChanged(object sender, EventArgs e)
        {
            cboWeek.BackColor = SystemColors.Window;

            if (!cboWeek.Items.Contains(cboWeek.Text) && cboWeek.Text != "")
            {
                int intWeek;
                int.TryParse(cboWeek.Text, out intWeek);
                if (intWeek >= 1 && intWeek <= 8)
                {
                    MessageBox.Show("The paperwork for week " + intWeek + " has not been created yet.", "Paperwork Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Entry must be numeric and in the range 1-8.", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                    cboWeek.BackColor = BackspliceMain.InvalidEntry;
                    cboMeritBadge.Enabled = false;
                    cboPeriod.Enabled = false;
                    btnPrint.Enabled = false;
                    mnuPrint.Enabled = false;
                    this.m_strProgramPath = "";
                    cboMeritBadge.Items.Clear();
                    cboMeritBadge.Items.Add("All");
                    cboMeritBadge.Text = "All";
                    cboPeriod.Items.Clear();
                    cboPeriod.Items.Add("All");
                    cboPeriod.Text = "All";
                }
            }
            else if (cboWeek.Text == "")
            {
                cboWeek.BackColor = SystemColors.Window;
                cboMeritBadge.Enabled = false;
                cboPeriod.Enabled = false;
                btnPrint.Enabled = false;
                mnuPrint.Enabled = false;
                this.m_strProgramPath = "";
                cboMeritBadge.Items.Clear();
                cboMeritBadge.Items.Add("All");
                cboPeriod.Items.Clear();
                cboPeriod.Items.Add("All");
            }
            else
            {
                cboWeek.BackColor = SystemColors.Window;
                cboMeritBadge.Enabled = true;
                cboPeriod.Enabled = true;
                btnPrint.Enabled = true;
                mnuPrint.Enabled = true;
                this.m_strProgramPath = this.m_strPaperworkPath + @"\Week " + cboWeek.Text;
                string[] strProgramAreas = System.IO.Directory.GetDirectories(m_strProgramPath);
                string[] strDirectoryParts;
                for (int i = 0; i < strProgramAreas.Length; i++)
                {
                    strDirectoryParts = strProgramAreas[i].Split(new char[] { '\\' });
                    string strMeritBadgesPath = this.m_strProgramPath + @"\" + strDirectoryParts[strDirectoryParts.Length - 1];
                }
                // Get all the merit badges
                this.RefreshMeritBadges();
                btnPrint.Focus();
            }
        }

        private void PrintPaperwork_Load(object sender, EventArgs e)
        {
            // Populate the week selection list with the available weeks
            string[] strWeeks = System.IO.Directory.GetDirectories(m_strPaperworkPath);
            for (int i = 0; i < strWeeks.Length; i++)
            {
                string[] strWeekParts = strWeeks[i].Split(new char[] {'\\', ' '});
                cboWeek.Items.Add(strWeekParts[strWeekParts.Length - 1]);
            }
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(sourceDirName);
            System.IO.DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new System.IO.DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!System.IO.Directory.Exists(destDirName))
            {
                System.IO.Directory.CreateDirectory(destDirName);
            }


            // Get the file contents of the directory to copy.
            System.IO.FileInfo[] files = dir.GetFiles();

            foreach (System.IO.FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string temppath = System.IO.Path.Combine(destDirName, file.Name);

                // Copy the file.
                file.CopyTo(temppath, false);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {
                foreach (System.IO.DirectoryInfo subdir in dirs)
                {
                    // Create the subdirectory.
                    string temppath = System.IO.Path.Combine(destDirName, subdir.Name);

                    // Copy the subdirectories.
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private void RefreshMeritBadges()
        {
            cboMeritBadge.Items.Clear();
            cboMeritBadge.Items.Add("All");
            cboMeritBadge.Text = "All";

            string strMeritBadgesPath = this.m_strProgramPath;
            string[] strMeritBadges = System.IO.Directory.GetDirectories(strMeritBadgesPath);
            for (int j = 0; j < strMeritBadges.Length; j++)
            {
                string[] strMeritBadgeParts = strMeritBadges[j].Split(new char[] { '\\' });
                cboMeritBadge.Items.Add(strMeritBadgeParts[strMeritBadgeParts.Length - 1]);
            }

            this.RefreshPeriods();
        }

        private void RefreshPeriods()
        {
            cboPeriod.Items.Clear();
            cboPeriod.Items.Add("All");
            cboPeriod.Text = "All";

            if (cboMeritBadge.Text.Equals("All"))
            {
                List<string> lstPeriods = new List<string>();
                for (int i = 0; i < cboMeritBadge.Items.Count; i++)
                {
                    if (!cboMeritBadge.Items[i].Equals("All"))
                    {
                        string strPeriodsPath = this.m_strProgramPath + @"\" + cboMeritBadge.Items[i];
                        string[] strPeriods = System.IO.Directory.GetFiles(strPeriodsPath);
                        //System.Diagnostics.Debug.WriteLine(strPeriodsPath);
                        for (int j = 0; j < strPeriods.Length; j++)
                        {
                            System.IO.FileInfo fileInfo = new System.IO.FileInfo(strPeriods[j]);
                            if ((fileInfo.Extension.Equals(".xls") || fileInfo.Extension.Equals(".xlsx")) && strPeriods[j].Contains("_"))
                            {
                                string[] strPeriodParts = strPeriods[j].Split(new char[] { '_' });
                                //string[] strPeriodParts = strPeriods[j].Split(new char[] { '\\', '.' });
                                string[] strParts = strPeriodParts[1].Split(new char[] { '.' });
                                lstPeriods.Add(strParts[0]);
                                //cboPeriod.Items.Add(strPeriodParts[strPeriodParts.Length - 1]);
                            }  
                        }
                    }
                }

                lstPeriods = new List<string>(lstPeriods.Distinct<string>());
                for (int i = 0; i < lstPeriods.Count; i++)
                {
                    cboPeriod.Items.Add(lstPeriods[i]);
                }
            }
            else
            {
                List<string> lstPeriods = new List<string>();
                string strPeriodsPath = this.m_strProgramPath + @"\" + cboMeritBadge.Text;
                string[] strPeriods = System.IO.Directory.GetFiles(strPeriodsPath);
                if (strPeriods.Length > 1)
                {
                    for (int j = 0; j < strPeriods.Length; j++)
                    {
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(strPeriods[j]);
                        if ((fileInfo.Extension.Equals(".xls") || fileInfo.Extension.Equals(".xlsx")) && strPeriods[j].Contains("_"))
                        {
                            string[] strPeriodParts = strPeriods[j].Split(new char[] { '_' });
                            //string[] strPeriodParts = strPeriods[j].Split(new char[] { '\\', '.' });
                            string[] strParts = strPeriodParts[1].Split(new char[] { '.' });
                            lstPeriods.Add(strParts[0]);
                            //cboPeriod.Items.Add(strPeriodParts[strPeriodParts.Length - 1]);
                        }  
                    }
                }

                lstPeriods = new List<string>(lstPeriods.Distinct<string>());
                for (int i = 0; i < lstPeriods.Count; i++)
                {
                    cboPeriod.Items.Add(lstPeriods[i]);
                }
            }
        }

        private void cboMeritBadge_TextChanged(object sender, EventArgs e)
        {
            this.RefreshPeriods();
        }

        private void btnSendTo_Click(object sender, EventArgs e)
        {
            this.OnSendTo();
        }
    }
}