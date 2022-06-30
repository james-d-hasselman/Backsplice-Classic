namespace Backsplice
{
    partial class Preferences
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Preferences));
            this.gbxPaperworkDirectory = new System.Windows.Forms.GroupBox();
            this.btnBrowsePaperwork = new System.Windows.Forms.Button();
            this.txtPaperwork = new System.Windows.Forms.TextBox();
            this.rbnOtherPaperwork = new System.Windows.Forms.RadioButton();
            this.rbnDefaultPaperwork = new System.Windows.Forms.RadioButton();
            this.gbxTemplatesDirectory = new System.Windows.Forms.GroupBox();
            this.rbnDefaultTemplates = new System.Windows.Forms.RadioButton();
            this.btnBrowseTemplates = new System.Windows.Forms.Button();
            this.txtTemplates = new System.Windows.Forms.TextBox();
            this.rbnOtherTemplates = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.fbdPreferences = new System.Windows.Forms.FolderBrowserDialog();
            this.btnOK = new System.Windows.Forms.Button();
            this.hlpPreferences = new System.Windows.Forms.HelpProvider();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.tbpCreatePaperwork = new System.Windows.Forms.TabPage();
            this.gbxSortScouts = new System.Windows.Forms.GroupBox();
            this.cbxByTroop = new System.Windows.Forms.CheckBox();
            this.cbxAlphabetically = new System.Windows.Forms.CheckBox();
            this.gbxNoTemplate = new System.Windows.Forms.GroupBox();
            this.rbnNewSpreadsheet = new System.Windows.Forms.RadioButton();
            this.rbnUseDefaultTemplate = new System.Windows.Forms.RadioButton();
            this.rbnSkipProgram = new System.Windows.Forms.RadioButton();
            this.gbxCreate = new System.Windows.Forms.GroupBox();
            this.rbnCreateExcelSpreadsheets = new System.Windows.Forms.RadioButton();
            this.rbnCreatePaperwork = new System.Windows.Forms.RadioButton();
            this.gbxPaperworkDirectory.SuspendLayout();
            this.gbxTemplatesDirectory.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            this.tbpCreatePaperwork.SuspendLayout();
            this.gbxSortScouts.SuspendLayout();
            this.gbxNoTemplate.SuspendLayout();
            this.gbxCreate.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxPaperworkDirectory
            // 
            this.gbxPaperworkDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxPaperworkDirectory.Controls.Add(this.btnBrowsePaperwork);
            this.gbxPaperworkDirectory.Controls.Add(this.txtPaperwork);
            this.gbxPaperworkDirectory.Controls.Add(this.rbnOtherPaperwork);
            this.gbxPaperworkDirectory.Controls.Add(this.rbnDefaultPaperwork);
            this.hlpPreferences.SetHelpKeyword(this.gbxPaperworkDirectory, "PaperworkSaveDirectory.html");
            this.hlpPreferences.SetHelpNavigator(this.gbxPaperworkDirectory, System.Windows.Forms.HelpNavigator.Topic);
            this.gbxPaperworkDirectory.Location = new System.Drawing.Point(6, 6);
            this.gbxPaperworkDirectory.Name = "gbxPaperworkDirectory";
            this.hlpPreferences.SetShowHelp(this.gbxPaperworkDirectory, true);
            this.gbxPaperworkDirectory.Size = new System.Drawing.Size(411, 68);
            this.gbxPaperworkDirectory.TabIndex = 0;
            this.gbxPaperworkDirectory.TabStop = false;
            this.gbxPaperworkDirectory.Text = "Paperwork Save Directory";
            // 
            // btnBrowsePaperwork
            // 
            this.btnBrowsePaperwork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.hlpPreferences.SetHelpKeyword(this.btnBrowsePaperwork, "BrowseSaveDir.html");
            this.hlpPreferences.SetHelpNavigator(this.btnBrowsePaperwork, System.Windows.Forms.HelpNavigator.Topic);
            this.btnBrowsePaperwork.Location = new System.Drawing.Point(330, 39);
            this.btnBrowsePaperwork.Name = "btnBrowsePaperwork";
            this.hlpPreferences.SetShowHelp(this.btnBrowsePaperwork, true);
            this.btnBrowsePaperwork.Size = new System.Drawing.Size(75, 23);
            this.btnBrowsePaperwork.TabIndex = 3;
            this.btnBrowsePaperwork.Text = "Browse...";
            this.btnBrowsePaperwork.UseVisualStyleBackColor = true;
            this.btnBrowsePaperwork.Click += new System.EventHandler(this.btnBrowsePaperwork_Click);
            // 
            // txtPaperwork
            // 
            this.txtPaperwork.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPaperwork.Location = new System.Drawing.Point(63, 41);
            this.txtPaperwork.Name = "txtPaperwork";
            this.txtPaperwork.Size = new System.Drawing.Size(261, 20);
            this.txtPaperwork.TabIndex = 2;
            this.txtPaperwork.TextChanged += new System.EventHandler(this.txtPaperwork_TextChanged);
            this.txtPaperwork.Enter += new System.EventHandler(this.txtPaperwork_Enter);
            // 
            // rbnOtherPaperwork
            // 
            this.rbnOtherPaperwork.AutoSize = true;
            this.hlpPreferences.SetHelpKeyword(this.rbnOtherPaperwork, "OtherSaveDir.html");
            this.hlpPreferences.SetHelpNavigator(this.rbnOtherPaperwork, System.Windows.Forms.HelpNavigator.Topic);
            this.rbnOtherPaperwork.Location = new System.Drawing.Point(6, 42);
            this.rbnOtherPaperwork.Name = "rbnOtherPaperwork";
            this.hlpPreferences.SetShowHelp(this.rbnOtherPaperwork, true);
            this.rbnOtherPaperwork.Size = new System.Drawing.Size(51, 17);
            this.rbnOtherPaperwork.TabIndex = 1;
            this.rbnOtherPaperwork.Text = "Other";
            this.rbnOtherPaperwork.UseVisualStyleBackColor = true;
            this.rbnOtherPaperwork.CheckedChanged += new System.EventHandler(this.rbnOtherPaperwork_CheckedChanged);
            // 
            // rbnDefaultPaperwork
            // 
            this.rbnDefaultPaperwork.AutoSize = true;
            this.hlpPreferences.SetHelpKeyword(this.rbnDefaultPaperwork, "DefaultSaveDir.html");
            this.hlpPreferences.SetHelpNavigator(this.rbnDefaultPaperwork, System.Windows.Forms.HelpNavigator.Topic);
            this.rbnDefaultPaperwork.Location = new System.Drawing.Point(6, 19);
            this.rbnDefaultPaperwork.Name = "rbnDefaultPaperwork";
            this.hlpPreferences.SetShowHelp(this.rbnDefaultPaperwork, true);
            this.rbnDefaultPaperwork.Size = new System.Drawing.Size(59, 17);
            this.rbnDefaultPaperwork.TabIndex = 0;
            this.rbnDefaultPaperwork.Text = "Default";
            this.rbnDefaultPaperwork.UseVisualStyleBackColor = true;
            this.rbnDefaultPaperwork.CheckedChanged += new System.EventHandler(this.rbnDefaultPaperwork_CheckedChanged);
            // 
            // gbxTemplatesDirectory
            // 
            this.gbxTemplatesDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxTemplatesDirectory.Controls.Add(this.rbnDefaultTemplates);
            this.gbxTemplatesDirectory.Controls.Add(this.btnBrowseTemplates);
            this.gbxTemplatesDirectory.Controls.Add(this.txtTemplates);
            this.gbxTemplatesDirectory.Controls.Add(this.rbnOtherTemplates);
            this.hlpPreferences.SetHelpKeyword(this.gbxTemplatesDirectory, "TemplatesDirectory.html");
            this.hlpPreferences.SetHelpNavigator(this.gbxTemplatesDirectory, System.Windows.Forms.HelpNavigator.Topic);
            this.gbxTemplatesDirectory.Location = new System.Drawing.Point(6, 80);
            this.gbxTemplatesDirectory.Name = "gbxTemplatesDirectory";
            this.hlpPreferences.SetShowHelp(this.gbxTemplatesDirectory, true);
            this.gbxTemplatesDirectory.Size = new System.Drawing.Size(411, 68);
            this.gbxTemplatesDirectory.TabIndex = 1;
            this.gbxTemplatesDirectory.TabStop = false;
            this.gbxTemplatesDirectory.Text = "Template Directory";
            // 
            // rbnDefaultTemplates
            // 
            this.rbnDefaultTemplates.AutoSize = true;
            this.rbnDefaultTemplates.Location = new System.Drawing.Point(6, 18);
            this.rbnDefaultTemplates.Name = "rbnDefaultTemplates";
            this.rbnDefaultTemplates.Size = new System.Drawing.Size(59, 17);
            this.rbnDefaultTemplates.TabIndex = 4;
            this.rbnDefaultTemplates.TabStop = true;
            this.rbnDefaultTemplates.Text = "Default";
            this.rbnDefaultTemplates.UseVisualStyleBackColor = true;
            this.rbnDefaultTemplates.CheckedChanged += new System.EventHandler(this.rbnDefaultTemplates_CheckedChanged);
            // 
            // btnBrowseTemplates
            // 
            this.btnBrowseTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.hlpPreferences.SetHelpKeyword(this.btnBrowseTemplates, "BrowseTemplateDir.html");
            this.hlpPreferences.SetHelpNavigator(this.btnBrowseTemplates, System.Windows.Forms.HelpNavigator.Topic);
            this.btnBrowseTemplates.Location = new System.Drawing.Point(330, 39);
            this.btnBrowseTemplates.Name = "btnBrowseTemplates";
            this.hlpPreferences.SetShowHelp(this.btnBrowseTemplates, true);
            this.btnBrowseTemplates.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseTemplates.TabIndex = 3;
            this.btnBrowseTemplates.Text = "Browse...";
            this.btnBrowseTemplates.UseVisualStyleBackColor = true;
            this.btnBrowseTemplates.Click += new System.EventHandler(this.btnBrowseTemplates_Click);
            // 
            // txtTemplates
            // 
            this.txtTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTemplates.Location = new System.Drawing.Point(63, 41);
            this.txtTemplates.Name = "txtTemplates";
            this.txtTemplates.Size = new System.Drawing.Size(261, 20);
            this.txtTemplates.TabIndex = 2;
            this.txtTemplates.TextChanged += new System.EventHandler(this.txtTemplates_TextChanged);
            this.txtTemplates.Enter += new System.EventHandler(this.txtTemplates_Enter);
            // 
            // rbnOtherTemplates
            // 
            this.rbnOtherTemplates.AutoSize = true;
            this.hlpPreferences.SetHelpKeyword(this.rbnOtherTemplates, "OtherTemplateDir.html");
            this.hlpPreferences.SetHelpNavigator(this.rbnOtherTemplates, System.Windows.Forms.HelpNavigator.Topic);
            this.rbnOtherTemplates.Location = new System.Drawing.Point(6, 42);
            this.rbnOtherTemplates.Name = "rbnOtherTemplates";
            this.hlpPreferences.SetShowHelp(this.rbnOtherTemplates, true);
            this.rbnOtherTemplates.Size = new System.Drawing.Size(51, 17);
            this.rbnOtherTemplates.TabIndex = 1;
            this.rbnOtherTemplates.TabStop = true;
            this.rbnOtherTemplates.Text = "Other";
            this.rbnOtherTemplates.UseVisualStyleBackColor = true;
            this.rbnOtherTemplates.CheckedChanged += new System.EventHandler(this.rbnOtherTemplates_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.hlpPreferences.SetHelpKeyword(this.btnCancel, "CancelPreferences.html");
            this.hlpPreferences.SetHelpNavigator(this.btnCancel, System.Windows.Forms.HelpNavigator.Topic);
            this.btnCancel.Location = new System.Drawing.Point(287, 293);
            this.btnCancel.Name = "btnCancel";
            this.hlpPreferences.SetShowHelp(this.btnCancel, true);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Enabled = false;
            this.hlpPreferences.SetHelpKeyword(this.btnApply, "ApplyPreferences.html");
            this.hlpPreferences.SetHelpNavigator(this.btnApply, System.Windows.Forms.HelpNavigator.Topic);
            this.btnApply.Location = new System.Drawing.Point(368, 293);
            this.btnApply.Name = "btnApply";
            this.hlpPreferences.SetShowHelp(this.btnApply, true);
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.OnApply);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHelp.Location = new System.Drawing.Point(12, 293);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 5;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // fbdPreferences
            // 
            this.fbdPreferences.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.hlpPreferences.SetHelpKeyword(this.btnOK, "OKPreferences.html");
            this.hlpPreferences.SetHelpNavigator(this.btnOK, System.Windows.Forms.HelpNavigator.Topic);
            this.btnOK.Location = new System.Drawing.Point(206, 293);
            this.btnOK.Name = "btnOK";
            this.hlpPreferences.SetShowHelp(this.btnOK, true);
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // hlpPreferences
            // 
            this.hlpPreferences.HelpNamespace = "D:\\Users\\James D. Hasselman\\Documents\\BackSplice Help\\BackSplice Help.chm";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tbpGeneral);
            this.tabControl1.Controls.Add(this.tbpCreatePaperwork);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(431, 275);
            this.tabControl1.TabIndex = 6;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.gbxPaperworkDirectory);
            this.tbpGeneral.Controls.Add(this.gbxTemplatesDirectory);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeneral.Size = new System.Drawing.Size(423, 249);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // tbpCreatePaperwork
            // 
            this.tbpCreatePaperwork.Controls.Add(this.gbxSortScouts);
            this.tbpCreatePaperwork.Controls.Add(this.gbxNoTemplate);
            this.tbpCreatePaperwork.Controls.Add(this.gbxCreate);
            this.tbpCreatePaperwork.Location = new System.Drawing.Point(4, 22);
            this.tbpCreatePaperwork.Name = "tbpCreatePaperwork";
            this.tbpCreatePaperwork.Padding = new System.Windows.Forms.Padding(3);
            this.tbpCreatePaperwork.Size = new System.Drawing.Size(423, 249);
            this.tbpCreatePaperwork.TabIndex = 1;
            this.tbpCreatePaperwork.Text = "Create Paperwork";
            this.tbpCreatePaperwork.UseVisualStyleBackColor = true;
            // 
            // gbxSortScouts
            // 
            this.gbxSortScouts.Controls.Add(this.cbxByTroop);
            this.gbxSortScouts.Controls.Add(this.cbxAlphabetically);
            this.gbxSortScouts.Location = new System.Drawing.Point(210, 6);
            this.gbxSortScouts.Name = "gbxSortScouts";
            this.gbxSortScouts.Size = new System.Drawing.Size(207, 65);
            this.gbxSortScouts.TabIndex = 1;
            this.gbxSortScouts.TabStop = false;
            this.gbxSortScouts.Text = "Sort Scouts";
            // 
            // cbxByTroop
            // 
            this.cbxByTroop.AutoSize = true;
            this.cbxByTroop.Location = new System.Drawing.Point(6, 42);
            this.cbxByTroop.Name = "cbxByTroop";
            this.cbxByTroop.Size = new System.Drawing.Size(69, 17);
            this.cbxByTroop.TabIndex = 5;
            this.cbxByTroop.Text = "By Troop";
            this.cbxByTroop.UseVisualStyleBackColor = true;
            this.cbxByTroop.CheckedChanged += new System.EventHandler(this.cbxByTroop_CheckedChanged);
            // 
            // cbxAlphabetically
            // 
            this.cbxAlphabetically.AutoSize = true;
            this.cbxAlphabetically.Location = new System.Drawing.Point(6, 19);
            this.cbxAlphabetically.Name = "cbxAlphabetically";
            this.cbxAlphabetically.Size = new System.Drawing.Size(91, 17);
            this.cbxAlphabetically.TabIndex = 4;
            this.cbxAlphabetically.Text = "Alphabetically";
            this.cbxAlphabetically.UseVisualStyleBackColor = true;
            this.cbxAlphabetically.CheckedChanged += new System.EventHandler(this.cbxAlphabetically_CheckedChanged);
            // 
            // gbxNoTemplate
            // 
            this.gbxNoTemplate.Controls.Add(this.rbnNewSpreadsheet);
            this.gbxNoTemplate.Controls.Add(this.rbnUseDefaultTemplate);
            this.gbxNoTemplate.Controls.Add(this.rbnSkipProgram);
            this.gbxNoTemplate.Location = new System.Drawing.Point(6, 77);
            this.gbxNoTemplate.Name = "gbxNoTemplate";
            this.gbxNoTemplate.Size = new System.Drawing.Size(411, 100);
            this.gbxNoTemplate.TabIndex = 1;
            this.gbxNoTemplate.TabStop = false;
            this.gbxNoTemplate.Text = "No Template Found";
            // 
            // rbnNewSpreadsheet
            // 
            this.rbnNewSpreadsheet.AutoSize = true;
            this.rbnNewSpreadsheet.Location = new System.Drawing.Point(3, 66);
            this.rbnNewSpreadsheet.Name = "rbnNewSpreadsheet";
            this.rbnNewSpreadsheet.Size = new System.Drawing.Size(148, 17);
            this.rbnNewSpreadsheet.TabIndex = 2;
            this.rbnNewSpreadsheet.TabStop = true;
            this.rbnNewSpreadsheet.Text = "Create Excel Spreadsheet";
            this.rbnNewSpreadsheet.UseVisualStyleBackColor = true;
            this.rbnNewSpreadsheet.CheckedChanged += new System.EventHandler(this.rbnNewSpreadsheet_CheckedChanged);
            // 
            // rbnUseDefaultTemplate
            // 
            this.rbnUseDefaultTemplate.AutoSize = true;
            this.rbnUseDefaultTemplate.Location = new System.Drawing.Point(3, 43);
            this.rbnUseDefaultTemplate.Name = "rbnUseDefaultTemplate";
            this.rbnUseDefaultTemplate.Size = new System.Drawing.Size(121, 17);
            this.rbnUseDefaultTemplate.TabIndex = 1;
            this.rbnUseDefaultTemplate.TabStop = true;
            this.rbnUseDefaultTemplate.Text = "Use Blank Template";
            this.rbnUseDefaultTemplate.UseVisualStyleBackColor = true;
            this.rbnUseDefaultTemplate.CheckedChanged += new System.EventHandler(this.rbnUseDefaultTemplate_CheckedChanged);
            // 
            // rbnSkipProgram
            // 
            this.rbnSkipProgram.AutoSize = true;
            this.rbnSkipProgram.Location = new System.Drawing.Point(3, 20);
            this.rbnSkipProgram.Name = "rbnSkipProgram";
            this.rbnSkipProgram.Size = new System.Drawing.Size(88, 17);
            this.rbnSkipProgram.TabIndex = 0;
            this.rbnSkipProgram.TabStop = true;
            this.rbnSkipProgram.Text = "Skip Program";
            this.rbnSkipProgram.UseVisualStyleBackColor = true;
            this.rbnSkipProgram.CheckedChanged += new System.EventHandler(this.rbnSkipProgram_CheckedChanged);
            // 
            // gbxCreate
            // 
            this.gbxCreate.Controls.Add(this.rbnCreateExcelSpreadsheets);
            this.gbxCreate.Controls.Add(this.rbnCreatePaperwork);
            this.gbxCreate.Location = new System.Drawing.Point(6, 6);
            this.gbxCreate.Name = "gbxCreate";
            this.gbxCreate.Size = new System.Drawing.Size(198, 65);
            this.gbxCreate.TabIndex = 0;
            this.gbxCreate.TabStop = false;
            this.gbxCreate.Text = "Create";
            // 
            // rbnCreateExcelSpreadsheets
            // 
            this.rbnCreateExcelSpreadsheets.AutoSize = true;
            this.rbnCreateExcelSpreadsheets.Location = new System.Drawing.Point(6, 42);
            this.rbnCreateExcelSpreadsheets.Name = "rbnCreateExcelSpreadsheets";
            this.rbnCreateExcelSpreadsheets.Size = new System.Drawing.Size(119, 17);
            this.rbnCreateExcelSpreadsheets.TabIndex = 1;
            this.rbnCreateExcelSpreadsheets.TabStop = true;
            this.rbnCreateExcelSpreadsheets.Text = "Excel Spreadsheets";
            this.rbnCreateExcelSpreadsheets.UseVisualStyleBackColor = true;
            this.rbnCreateExcelSpreadsheets.CheckedChanged += new System.EventHandler(this.rbnCreateExcelSpreadsheets_CheckedChanged);
            // 
            // rbnCreatePaperwork
            // 
            this.rbnCreatePaperwork.AutoSize = true;
            this.rbnCreatePaperwork.Location = new System.Drawing.Point(6, 19);
            this.rbnCreatePaperwork.Name = "rbnCreatePaperwork";
            this.rbnCreatePaperwork.Size = new System.Drawing.Size(76, 17);
            this.rbnCreatePaperwork.TabIndex = 0;
            this.rbnCreatePaperwork.TabStop = true;
            this.rbnCreatePaperwork.Text = "Paperwork";
            this.rbnCreatePaperwork.UseVisualStyleBackColor = true;
            this.rbnCreatePaperwork.CheckedChanged += new System.EventHandler(this.rbnCreatePaperwork_CheckedChanged);
            // 
            // Preferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 328);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.hlpPreferences.SetHelpKeyword(this, "Preferences.html");
            this.hlpPreferences.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(471, 366);
            this.Name = "Preferences";
            this.hlpPreferences.SetShowHelp(this, true);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.frmPreferences_Load);
            this.gbxPaperworkDirectory.ResumeLayout(false);
            this.gbxPaperworkDirectory.PerformLayout();
            this.gbxTemplatesDirectory.ResumeLayout(false);
            this.gbxTemplatesDirectory.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.tbpCreatePaperwork.ResumeLayout(false);
            this.gbxSortScouts.ResumeLayout(false);
            this.gbxSortScouts.PerformLayout();
            this.gbxNoTemplate.ResumeLayout(false);
            this.gbxNoTemplate.PerformLayout();
            this.gbxCreate.ResumeLayout(false);
            this.gbxCreate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxPaperworkDirectory;
        private System.Windows.Forms.TextBox txtPaperwork;
        private System.Windows.Forms.RadioButton rbnOtherPaperwork;
        private System.Windows.Forms.RadioButton rbnDefaultPaperwork;
        private System.Windows.Forms.Button btnBrowsePaperwork;
        private System.Windows.Forms.GroupBox gbxTemplatesDirectory;
        private System.Windows.Forms.Button btnBrowseTemplates;
        private System.Windows.Forms.TextBox txtTemplates;
        private System.Windows.Forms.RadioButton rbnOtherTemplates;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.FolderBrowserDialog fbdPreferences;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.HelpProvider hlpPreferences;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpGeneral;
        private System.Windows.Forms.TabPage tbpCreatePaperwork;
        private System.Windows.Forms.GroupBox gbxCreate;
        private System.Windows.Forms.GroupBox gbxSortScouts;
        private System.Windows.Forms.GroupBox gbxNoTemplate;
        private System.Windows.Forms.RadioButton rbnCreateExcelSpreadsheets;
        private System.Windows.Forms.RadioButton rbnCreatePaperwork;
        private System.Windows.Forms.RadioButton rbnNewSpreadsheet;
        private System.Windows.Forms.RadioButton rbnUseDefaultTemplate;
        private System.Windows.Forms.RadioButton rbnSkipProgram;
        private System.Windows.Forms.RadioButton rbnDefaultTemplates;
        private System.Windows.Forms.CheckBox cbxByTroop;
        private System.Windows.Forms.CheckBox cbxAlphabetically;
    }
}