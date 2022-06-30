using System;
using System.Drawing;
using System.Windows.Forms;

namespace Backsplice
{
    public partial class Preferences : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Preferences()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPreferences_Load(object sender, EventArgs e)
        {
            // Display the current save directory preference
            bool blnUseDefaultSaveDirectory = BackspliceMain.UseDefaultSaveDirectory;
            if (blnUseDefaultSaveDirectory)
            {
                rbnDefaultPaperwork.Select();
                txtPaperwork.Enabled = false;
                btnBrowsePaperwork.Enabled = false;
                txtPaperwork.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            else
            {
                rbnOtherPaperwork.Select();
                txtPaperwork.Text = BackspliceMain.SaveDirectory;
                txtPaperwork.ForeColor = Color.Black;
            }

            // Display the current template directory preference
            bool blnUseDefaultTemplateDirectory = BackspliceMain.UseDefaultTemplateDirectory;
            if (blnUseDefaultTemplateDirectory)
            {
                System.Diagnostics.Debug.WriteLine(BackspliceMain.DefaultTemplateDirectory);
                rbnDefaultTemplates.Select();
                txtTemplates.Enabled = false;
                btnBrowseTemplates.Enabled = false;
                txtTemplates.Text = BackspliceMain.DefaultTemplateDirectory;
            }
            else
            {
                rbnOtherTemplates.Select();
                txtTemplates.Text = BackspliceMain.TemplateDirectory;
                txtTemplates.ForeColor = Color.Black;
            }

            // Display the current create paperwork preference
            bool blnCreatePaperwork = BackspliceMain.MakePaperwork;
            if (blnCreatePaperwork)
            {
                rbnCreatePaperwork.Select();
            }
            else
            {
                rbnCreateExcelSpreadsheets.Select();
                // We will not be using templates so we do not need to specify a no template action
                gbxNoTemplate.Enabled = false;
                // Let the user know that excel spreadsheets will be created regardless
                rbnNewSpreadsheet.Select();
            }

            // Display the current sorting preferences
            cbxAlphabetically.Checked = BackspliceMain.SortAlphabetically;
            cbxByTroop.Checked = BackspliceMain.SortByTroop;

            // Display the current no template action
            int intNoTemplateAction = BackspliceMain.NoTemplate;
            switch (intNoTemplateAction)
            {
                case 1:
                    rbnSkipProgram.Select();
                    break;
                case 2:
                    rbnUseDefaultTemplate.Select();
                    break;
                case 3:
                    rbnNewSpreadsheet.Select();
                    break;
            }

            btnApply.Enabled = false;
        }

        /// <summary>
        /// Handles the click event for the paperwork directory browse button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowsePaperwork_Click(object sender, EventArgs e)
        {
            fbdPreferences.ShowDialog();
            txtPaperwork.ForeColor = Color.Black;
            txtPaperwork.Text = fbdPreferences.SelectedPath;
        }

        /// <summary>
        /// Handles the click event for the template directory browse button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowseTemplates_Click(object sender, EventArgs e)
        {
            fbdPreferences.ShowDialog();
            txtTemplates.ForeColor = Color.Black;
            // If the user has chosen a path
            if (!fbdPreferences.SelectedPath.Equals(""))
            {
                // Display the path
                txtTemplates.Text = fbdPreferences.SelectedPath;
                btnApply.Focus();
            }
            else
            {
                // Tell the user to specify a path
                txtTemplates.ForeColor = SystemColors.GrayText;
                txtTemplates.Text = "Please Specify...";
                btnBrowseTemplates.Focus();
            }
        }

        /// <summary>
        /// Discards all changes and closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Displays help for the preferences window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHelp_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Applies all changes and closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            OnApply(sender, e);

            // If the changes were applied succesfully
            if (!btnApply.Enabled)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Handles the checked changed event for the alphabetical sort check box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxAlphabetically_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        /// <summary>
        /// Handles the checked changed event for the by troop sort check box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxByTroop_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        /// <summary>
        /// Handles the click event for the apply button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnApply(object sender, EventArgs e)
        {
            //TODO this function is too long, make a generic one to handle both preferences. Whew, this one is a toughie.

            // Set the save directory preference
            if (rbnDefaultPaperwork.Checked == true)
            {
                Properties.Settings.Default.UseDefaultSaveDirectory = true;
            }
            else
            {
                string[] strLogicalDrives = System.IO.Directory.GetLogicalDrives();
                bool blnValidPath = false;

                // Check that the user has selected a valid drive
                for (int i = 0; i < strLogicalDrives.Length; i++)
                {
                    if (txtPaperwork.Text.StartsWith(strLogicalDrives[i]))
                    {
                        blnValidPath = true;
                    }
                }

                if (!blnValidPath)
                {
                    string strDrives = "";
                    for (int i = 0; i < strLogicalDrives.Length; i++)
                    {
                        strDrives += strLogicalDrives[i] + ", ";
                    }

                    MessageBox.Show("You must specify a valid drive (" + strDrives.Substring(0, strDrives.Length - 2) + ")", "Invalid Save Directory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPaperwork.BackColor = BackspliceMain.InvalidEntry;
                    txtPaperwork.Focus();
                    return;
                }
                else
                {
                    bool blnBollux = true;
                    // Attempt to create the path
                    
                    // This exception handling strategy is acceptable here.
                    try
                    {
                        System.IO.Directory.CreateDirectory(txtPaperwork.Text);
                        blnBollux = false;
                    }
                    catch (ArgumentNullException ex)
                    {
                        MessageBox.Show(ex.Message, "Folder Name Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message, "Invalid Path Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (System.IO.PathTooLongException ex)
                    {
                        MessageBox.Show(ex.Message, "Path Too Long", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (System.Security.SecurityException ex)
                    {
                        MessageBox.Show(ex.Message, "Security Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        MessageBox.Show(ex.Message, "Insufficient Access Privileges", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (System.IO.IOException ex)
                    {
                        MessageBox.Show(ex.Message, "Folder is Read-Only", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (NotSupportedException ex)
                    {
                        MessageBox.Show(ex.Message, "Invalid Path Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    // If the path could not be created
                    if (blnBollux)
                    {
                        // Let the user know they have entered an invalid paperwork save path
                        txtPaperwork.BackColor = BackspliceMain.InvalidEntry;
                        txtPaperwork.Focus();
                    }
                    else
                    {
                        // Otherwise, set the paperwork save directory path
                        Properties.Settings.Default.UseDefaultSaveDirectory = false;
                        string saveDirectory = txtPaperwork.Text;
                        saveDirectory = saveDirectory.TrimEnd(new char[] { '\\' });
                        Properties.Settings.Default.SaveDirectory = saveDirectory;
                    }
                }
            }

            // Set the template directory preference
            if (rbnDefaultTemplates.Checked == true)
            {
                Properties.Settings.Default.UseDefaultTemplateDirectory = true;
            }
            else
            {
                string[] strLogicalDrives = System.IO.Directory.GetLogicalDrives();
                bool blnValidPath = false;

                // Check that the user has selected a valid drive
                for (int i = 0; i < strLogicalDrives.Length; i++)
                {
                    if (txtTemplates.Text.StartsWith(strLogicalDrives[i]))
                    {
                        blnValidPath = true;
                    }
                }

                if (!blnValidPath)
                {
                    string strDrives = "";
                    for (int i = 0; i < strLogicalDrives.Length; i++)
                    {
                        strDrives += strLogicalDrives[i] + ", ";
                    }

                    MessageBox.Show("You must specify a valid drive (" + strDrives.Substring(0, strDrives.Length - 2) + ")", "Invalid Template Directory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTemplates.BackColor = BackspliceMain.InvalidEntry;
                    txtTemplates.Focus();
                    return;
                }
                else
                {
                    bool blnBollux = true;
                    // Attempt to create the path
                    
                    // This strategy is acceptable here.
                    try
                    {
                        System.IO.Directory.CreateDirectory(txtTemplates.Text);
                        blnBollux = false;
                    }
                    catch (ArgumentNullException ex)
                    {
                        MessageBox.Show(ex.Message, "Folder Name Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message, "Invalid Path Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (System.IO.PathTooLongException ex)
                    {
                        MessageBox.Show(ex.Message, "Path Too Long", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (System.Security.SecurityException ex)
                    {
                        MessageBox.Show(ex.Message, "Security Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        MessageBox.Show(ex.Message, "Insufficient Access Privileges", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (System.IO.IOException ex)
                    {
                        MessageBox.Show(ex.Message, "Folder is Read-Only", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (NotSupportedException ex)
                    {
                        MessageBox.Show(ex.Message, "Invalid Path Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    // If the path could not be created
                    if (blnBollux)
                    {
                        // Let the user know they have entered an invalid paperwork save path
                        txtTemplates.BackColor = BackspliceMain.InvalidEntry;
                        txtTemplates.Focus();
                    }
                    else
                    {
                        // Otherwise, set the paperwork save directory path
                        Properties.Settings.Default.UseDefaultTemplateDirectory = false;
                        string templateDirectory = txtTemplates.Text;
                        templateDirectory = templateDirectory.TrimEnd(new char[] { '\\' });
                        Properties.Settings.Default.TemplateDirectory = templateDirectory;
                    }    
                }
            }

            // Set the create preference
            if (rbnCreatePaperwork.Checked)
            {
                Properties.Settings.Default.MakePaperwork = true;
            }
            else
            {
                Properties.Settings.Default.MakePaperwork = false;
            }

            // Set the sorting preference
            if (cbxAlphabetically.Checked || cbxByTroop.Checked)
            {
                Properties.Settings.Default.SortScouts = true;
            }
            else
            {
                Properties.Settings.Default.SortScouts = false;
            }

            Properties.Settings.Default.SortAlphabetically = cbxAlphabetically.Checked;
            Properties.Settings.Default.SortByTroop = cbxByTroop.Checked;

            // Set the no template preference
            if (rbnSkipProgram.Checked)
            {
                Properties.Settings.Default.NoTemplate = 1;
            }
            else if (rbnUseDefaultTemplate.Checked)
            {
                Properties.Settings.Default.NoTemplate = 2;
            }
            else
            {
                Properties.Settings.Default.NoTemplate = 3;
            }

            Properties.Settings.Default.Save();
            btnApply.Enabled = false;
        }

        /// <summary>
        /// Handles the checked changed event for the create excel spreadsheets radio button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbnCreateExcelSpreadsheets_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;

            if (rbnCreateExcelSpreadsheets.Checked)
            {
                // We will not be using templates so we do not need to specify a no template action
                gbxNoTemplate.Enabled = false;
                // Let the user know that excel spreadsheets will be created regardless
                rbnNewSpreadsheet.Select();
            }
        }

        /// <summary>
        /// Handles the checked changed event for the create paperwork radio button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbnCreatePaperwork_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;

            if (rbnCreatePaperwork.Checked)
            {
                // We will be working with templates so we need to specify a no template action
                gbxNoTemplate.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the checked changed event for the default paperwork radio button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbnDefaultPaperwork_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnDefaultPaperwork.Checked)
            {
                txtPaperwork.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                txtPaperwork.Enabled = false;
                btnBrowsePaperwork.Enabled = false;
                btnApply.Enabled = true;
                btnApply.Focus();
            }
        }

        /// <summary>
        /// Handles the checked changed event for the default templates radio button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbnDefaultTemplates_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnDefaultTemplates.Checked)
            {
                txtTemplates.Text = BackspliceMain.DefaultTemplateDirectory;
                txtTemplates.Enabled = false;
                btnBrowseTemplates.Enabled = false;
                btnApply.Enabled = true;
                btnApply.Focus();
            }
        }

        /// <summary>
        /// Handles the checked changed event for the new spreadsheet radio button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbnNewSpreadsheet_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        /// <summary>
        /// Handles the checked changed event for the other paperwork radio button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbnOtherPaperwork_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnOtherPaperwork.Checked)
            {
                txtPaperwork.ForeColor = SystemColors.GrayText;
                txtPaperwork.Text = "Please Specify...";
                txtPaperwork.Enabled = true;
                btnBrowsePaperwork.Enabled = true;
                btnApply.Enabled = true;
                btnBrowsePaperwork.Focus();
            }
        }

        /// <summary>
        /// Handles the checked changed event for the other templates radio button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbnOtherTemplates_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnOtherTemplates.Checked)
            {
                txtTemplates.ForeColor = SystemColors.GrayText;
                txtTemplates.Text = "Please Specify...";
                txtTemplates.Enabled = true;
                btnBrowseTemplates.Enabled = true;
                btnApply.Enabled = true;
                btnBrowseTemplates.Focus();
            }
        }

        /// <summary>
        /// Handles the checked changed event for the skip program radio button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbnSkipProgram_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        /// <summary>
        /// Handles the checked changed event for the use default template radio button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbnUseDefaultTemplate_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        /// <summary>
        /// Handles the enter event for the paperwork text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPaperwork_Enter(object sender, EventArgs e)
        {
            if (txtPaperwork.ForeColor == SystemColors.GrayText)
            {
                txtPaperwork.Text = "";
                txtPaperwork.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Handles the text changed event for the paperwork text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPaperwork_TextChanged(object sender, EventArgs e)
        {
            if (txtPaperwork.ForeColor != SystemColors.GrayText)
            {
                btnApply.Enabled = true;
            }

            if (txtPaperwork.BackColor == BackspliceMain.InvalidEntry)
            {
                txtPaperwork.BackColor = SystemColors.Window;
            }
        }

        /// <summary>
        /// Handles the enter event for the templates text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTemplates_Enter(object sender, EventArgs e)
        {
            if (txtTemplates.ForeColor == SystemColors.GrayText)
            {
                txtTemplates.Text = "";
                txtTemplates.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Handles the text changed event for the templates text box 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTemplates_TextChanged(object sender, EventArgs e)
        {
            if (txtTemplates.ForeColor != SystemColors.GrayText)
            {
                btnApply.Enabled = true;
            }

            if (txtTemplates.BackColor == BackspliceMain.InvalidEntry)
            {
                txtTemplates.BackColor = SystemColors.Window;
            }
        }
    }
}
