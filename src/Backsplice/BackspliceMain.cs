using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Backsplice
{
    public partial class BackspliceMain : Form
    {
        public const string m_strErrorLogFileName = "error_log.txt";
        private static string m_strYear;

        public BackspliceMain()
        {
            InitializeComponent();
        }

        public static bool UseDefaultTemplateDirectory
        {
            get
            {
                return Properties.Settings.Default.UseDefaultTemplateDirectory;
            }
        }

        public static bool UseDefaultSaveDirectory
        {
            get
            {
                return Properties.Settings.Default.UseDefaultSaveDirectory;
            }
        }

        public static bool SortScouts
        {
            get
            {
                return Properties.Settings.Default.SortScouts;
            }
        }

        public static int NoTemplate
        {
            get
            {
                return Properties.Settings.Default.NoTemplate;
            }
        }

        public static bool MakePaperwork
        {
            get
            {
                return Properties.Settings.Default.MakePaperwork;
            }
        }

        private static string Year
        {
            get
            {
                return BackspliceMain.m_strYear;
            }
            set
            {
                BackspliceMain.m_strYear = value;
            }
        }

        public static string ErrorLog
        {
            get
            {

                return BackspliceMain.SaveDirectory + @"\" + BackspliceMain.m_strErrorLogFileName;
            }
        }

        public static Color InvalidEntry
        {
            get
            {
                return Color.LightCoral;
            }
        }

        public static string DefaultTemplateDirectory
        {
            get
            {
                // Get the application directory
                string strApplicationDirectory = BackspliceMain.BackspliceDiretory;
                string templateDirectory = strApplicationDirectory + @"\templates";

                return templateDirectory;
            }
        }

        public static string BackspliceDiretory
        {
            get
            {
                if (BackspliceMain.UseDefaultSaveDirectory)
                {
                    string strUserProfile = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Backsplice";
                    return strUserProfile;
                }
                else
                {
                    return Properties.Settings.Default.SaveDirectory + @"\Backsplice";
                }
            }
        }

        public static string SaveDirectory
        {
            get
            {
                if (BackspliceMain.UseDefaultSaveDirectory)
                {
                    string strUserProfile = BackspliceMain.BackspliceDiretory + @"\" + BackspliceMain.Year;
                    return strUserProfile;
                }
                else
                {
                    return BackspliceMain.BackspliceDiretory + @"\" + BackspliceMain.Year;
                }
            }
        }

        public static bool SortAlphabetically
        {
            get
            {
                return Properties.Settings.Default.SortAlphabetically;
            }
        }

        public static bool SortByTroop
        {
            get
            {
                return Properties.Settings.Default.SortByTroop;
            }
        }

        public static string TemplateDirectory
        {
            get
            {
                if (BackspliceMain.UseDefaultTemplateDirectory)
                {
                    return BackspliceMain.DefaultTemplateDirectory;
                }
                else
                {
                    return Properties.Settings.Default.TemplateDirectory;
                }
            }
        }

        public static List<CampProgram> FindScout(string _strWeek, string _strFirstName, string _strLastName, out string _strTroopNumber)
        {
            List<CampProgram> objResults = new List<CampProgram>();

            // Get the week
            string strWeek = _strWeek;
            // Get the first name
            string strFirstName = _strFirstName;
            // Get the last name
            string strLastName = _strLastName;
            // Get the troop number
            _strTroopNumber = null;

            // Get the directory where the DoubleKnot roster is located
            string strRosterDirectoryPath = BackspliceMain.SaveDirectory + @"\Week " + strWeek;
            string strRosterPath = "";

            if (System.IO.Directory.Exists(strRosterDirectoryPath))
            {
                // Find the roster
                string[] strFiles = System.IO.Directory.GetFiles(strRosterDirectoryPath, "Week " + strWeek + "*");
                if (strFiles.Length > 0)
                {
                    strRosterPath = strFiles[0];

                    // Open the DoubleKnot roster
                    DoubleKnotRoster dkrRoster = new DoubleKnotRoster(strRosterPath, true);
                    // Search for the scout
                    for (int i = DoubleKnotRoster.StartRow; i <= dkrRoster.Rows; i++)
                    {
                        if (!dkrRoster.extractProgram(i).Contains("Summer Camp Week"))
                        {
                            string strRosterFirstName = dkrRoster.extractFirstName(i);
                            string strRosterLastName = dkrRoster.extractLastName(i);

                            if (strFirstName.Equals(strRosterFirstName) && strLastName.Equals(strRosterLastName))
                            {
                                int intInsertIndex = 0;
                                CampProgram cpNewProgram = new CampProgram(dkrRoster.extractProgram(i), dkrRoster.extractPeriod(i), dkrRoster.extractPeriodNumber(i));

                                if (objResults.Count == 0)
                                {
                                    //System.Diagnostics.Debug.WriteLine("EMPTY");
                                    objResults.Add(cpNewProgram);
                                    _strTroopNumber = dkrRoster.extractTroopNumber(i);
                                }
                                else
                                {
                                    // Get the period
                                    string strPeriod = cpNewProgram.Period;
                                    // If the string starts with period
                                    if (strPeriod.StartsWith("period", true, null))
                                    {
                                        // Compare the period number to all others in the list
                                        int j = 0;
                                        bool blnStop = false;
                                        while (j < objResults.Count && !blnStop)
                                        {
                                            CampProgram cpOldProgram = objResults[j];
                                            // Get the old value's period string
                                            string strOldPeriod = cpOldProgram.Period;
                                            // If the old period string starts with period
                                            if (strOldPeriod.StartsWith("period", true, null))
                                            {
                                                // If the old value's period number is less than the new values period number
                                                if (cpOldProgram.PeriodNumber < cpNewProgram.PeriodNumber)
                                                {
                                                    // Increment the insertIndex
                                                    intInsertIndex++;
                                                }
                                                else
                                                {
                                                    // Stop the search
                                                    blnStop = true;
                                                }

                                                j++;
                                            }
                                            else if (strOldPeriod.StartsWith("all", true, null))
                                            {
                                                // Increment the insert index
                                                intInsertIndex++;
                                                j++;
                                            }
                                            else if (strOldPeriod.StartsWith("late", true, null))
                                            {
                                                // Stop the search
                                                blnStop = true;
                                                i++;
                                            }
                                            else
                                            {
                                                // insert the program at the current spot.
                                                blnStop = true;
                                            }
                                        }

                                        objResults.Insert(intInsertIndex, cpNewProgram);
                                    }
                                    else if (strPeriod.StartsWith("all", true, null))
                                    {
                                        // Add the merit badge to the beginning of the list
                                        objResults.Insert(0, cpNewProgram);
                                    }
                                    //else if (strPeriod.StartsWith("late", true, null))
                                    else
                                    {
                                        // Add the merit badge to the end of the list
                                        objResults.Insert(objResults.Count, cpNewProgram);
                                    }

                                    System.Diagnostics.Debug.WriteLine(dkrRoster.extractProgram(i));
                                }
                            }
                        }
                    }

                    // Clean up after our self
                    dkrRoster.Dispose();

                    objResults = new List<CampProgram>(Enumerable.Distinct<CampProgram>(objResults));

                    return objResults;
                }
            }

            // The user has not created paperwork for the selected week, or has deleted all or part of the paperwork
            MessageBox.Show("The paperwork for Week " + strWeek + " does not exist", "Paperwork not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }

        private void OnFindScout()
        {
            FindScout frmFindScout = new FindScout();
            frmFindScout.ShowDialog();
        }

        private void OnDropAdd()
        {
            DropAdd frmDropAdd = new DropAdd();
            frmDropAdd.ShowDialog();
        }

        private void OnCreatePaperwork()
        {
            CreatePaperwork frmCreatePaperwork = new CreatePaperwork();
            frmCreatePaperwork.ShowDialog();
            this.ValidatePaperworkDirectories();
        }

        private void OnPrintPaperwork()
        {
            PrintPaperwork frmPrintPaperwork = new PrintPaperwork();
            frmPrintPaperwork.ShowDialog();
        }

        private void btnCreatePaperwork_Click(object sender, EventArgs e)
        {
            this.OnCreatePaperwork();
        }

        private void btnFindScout_Click(object sender, EventArgs e)
        {
            this.OnFindScout();
        }

        private void btnDropAdd_Click(object sender, EventArgs e)
        {
            this.OnDropAdd();
        }

        private void btnPrintPaperwork_Click(object sender, EventArgs e)
        {
            this.OnPrintPaperwork();
        }

        private void Backsplice_Load(object sender, EventArgs e)
        {
            BackspliceMain.Year = DateTime.Now.Year.ToString();
            this.ValidatePaperworkDirectories();
        }

        private void ValidatePaperworkDirectories()
        {
            if (System.IO.Directory.Exists(BackspliceMain.SaveDirectory))
            {
                string[] strWeeks = System.IO.Directory.GetDirectories(BackspliceMain.SaveDirectory);
                if (strWeeks.Length > 0)
                {
                    btnPrintPaperwork.Enabled = true;
                    btnFindScout.Enabled = true;
                    btnDropAdd.Enabled = true;
                }
            }
        }

        private void mnuCreatePaperwork_Click(object sender, EventArgs e)
        {
            this.OnCreatePaperwork();
        }

        private void mnuPrintPaperwork_Click(object sender, EventArgs e)
        {
            this.OnPrintPaperwork();
        }

        private void mnuFindScout_Click(object sender, EventArgs e)
        {
            this.OnFindScout();
        }

        private void mnuDropAdd_Click(object sender, EventArgs e)
        {
            this.OnDropAdd();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuPreferences_Click(object sender, EventArgs e)
        {
            Preferences frmPreferences = new Preferences();
            frmPreferences.ShowDialog();
            this.ValidatePaperworkDirectories();
        }
    }
}
