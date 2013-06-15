namespace Backsplice
{
    partial class CreatePaperwork
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreatePaperwork));
            this.mbrCreatePaperwork = new System.Windows.Forms.MenuStrip();
            this.mnuFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBrowse = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCreatePaperwork = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.cboWeek = new System.Windows.Forms.ComboBox();
            this.lblWeek = new System.Windows.Forms.Label();
            this.lblWeekSeparator = new System.Windows.Forms.Label();
            this.lblDoubleKnotRoster = new System.Windows.Forms.Label();
            this.txtDoubleKnotRoster = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblBrowseSeparator = new System.Windows.Forms.Label();
            this.sbrStatus = new System.Windows.Forms.StatusStrip();
            this.pgbProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.ofdDoubleKnot = new System.Windows.Forms.OpenFileDialog();
            this.bgwCreate = new System.ComponentModel.BackgroundWorker();
            this.mbrCreatePaperwork.SuspendLayout();
            this.sbrStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // mbrCreatePaperwork
            // 
            this.mbrCreatePaperwork.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileMenu,
            this.mnuOptionsMenu,
            this.mnuHelpMenu});
            this.mbrCreatePaperwork.Location = new System.Drawing.Point(0, 0);
            this.mbrCreatePaperwork.Name = "mbrCreatePaperwork";
            this.mbrCreatePaperwork.Size = new System.Drawing.Size(461, 24);
            this.mbrCreatePaperwork.TabIndex = 0;
            this.mbrCreatePaperwork.Text = "menuStrip1";
            // 
            // mnuFileMenu
            // 
            this.mnuFileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBrowse,
            this.toolStripSeparator2,
            this.mnuClose});
            this.mnuFileMenu.Name = "mnuFileMenu";
            this.mnuFileMenu.Size = new System.Drawing.Size(37, 20);
            this.mnuFileMenu.Text = "&File";
            // 
            // mnuBrowse
            // 
            this.mnuBrowse.Enabled = false;
            this.mnuBrowse.Name = "mnuBrowse";
            this.mnuBrowse.Size = new System.Drawing.Size(121, 22);
            this.mnuBrowse.Text = "&Browse...";
            this.mnuBrowse.Click += new System.EventHandler(this.OnBrowse);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(118, 6);
            // 
            // mnuClose
            // 
            this.mnuClose.Name = "mnuClose";
            this.mnuClose.Size = new System.Drawing.Size(121, 22);
            this.mnuClose.Text = "&Close";
            this.mnuClose.Click += new System.EventHandler(this.OnClose);
            // 
            // mnuOptionsMenu
            // 
            this.mnuOptionsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCreatePaperwork});
            this.mnuOptionsMenu.Name = "mnuOptionsMenu";
            this.mnuOptionsMenu.Size = new System.Drawing.Size(61, 20);
            this.mnuOptionsMenu.Text = "&Options";
            // 
            // mnuCreatePaperwork
            // 
            this.mnuCreatePaperwork.Enabled = false;
            this.mnuCreatePaperwork.Name = "mnuCreatePaperwork";
            this.mnuCreatePaperwork.Size = new System.Drawing.Size(108, 22);
            this.mnuCreatePaperwork.Text = "&Create";
            this.mnuCreatePaperwork.Click += new System.EventHandler(this.OnCreate);
            // 
            // mnuHelpMenu
            // 
            this.mnuHelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelp});
            this.mnuHelpMenu.Name = "mnuHelpMenu";
            this.mnuHelpMenu.Size = new System.Drawing.Size(44, 20);
            this.mnuHelpMenu.Text = "&Help";
            // 
            // mnuHelp
            // 
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(99, 22);
            this.mnuHelp.Text = "&Help";
            // 
            // cboWeek
            // 
            this.cboWeek.FormatString = "N0";
            this.cboWeek.FormattingEnabled = true;
            this.cboWeek.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cboWeek.Location = new System.Drawing.Point(57, 27);
            this.cboWeek.MaxLength = 1;
            this.cboWeek.Name = "cboWeek";
            this.cboWeek.Size = new System.Drawing.Size(37, 21);
            this.cboWeek.TabIndex = 2;
            this.cboWeek.TextChanged += new System.EventHandler(this.cboWeek_TextChanged);
            // 
            // lblWeek
            // 
            this.lblWeek.AutoSize = true;
            this.lblWeek.Location = new System.Drawing.Point(12, 30);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(39, 13);
            this.lblWeek.TabIndex = 1;
            this.lblWeek.Text = "Week:";
            // 
            // lblWeekSeparator
            // 
            this.lblWeekSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWeekSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWeekSeparator.Location = new System.Drawing.Point(12, 51);
            this.lblWeekSeparator.Name = "lblWeekSeparator";
            this.lblWeekSeparator.Size = new System.Drawing.Size(437, 2);
            this.lblWeekSeparator.TabIndex = 3;
            // 
            // lblDoubleKnotRoster
            // 
            this.lblDoubleKnotRoster.AutoSize = true;
            this.lblDoubleKnotRoster.Location = new System.Drawing.Point(12, 59);
            this.lblDoubleKnotRoster.Name = "lblDoubleKnotRoster";
            this.lblDoubleKnotRoster.Size = new System.Drawing.Size(99, 13);
            this.lblDoubleKnotRoster.TabIndex = 4;
            this.lblDoubleKnotRoster.Text = "Doubleknot Roster:";
            // 
            // txtDoubleKnotRoster
            // 
            this.txtDoubleKnotRoster.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDoubleKnotRoster.Enabled = false;
            this.txtDoubleKnotRoster.Location = new System.Drawing.Point(118, 56);
            this.txtDoubleKnotRoster.Name = "txtDoubleKnotRoster";
            this.txtDoubleKnotRoster.Size = new System.Drawing.Size(250, 20);
            this.txtDoubleKnotRoster.TabIndex = 5;
            this.txtDoubleKnotRoster.TextChanged += new System.EventHandler(this.txtDoubleKnotRoster_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Enabled = false;
            this.btnBrowse.Location = new System.Drawing.Point(374, 54);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.OnBrowse);
            // 
            // lblBrowseSeparator
            // 
            this.lblBrowseSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBrowseSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBrowseSeparator.Location = new System.Drawing.Point(12, 79);
            this.lblBrowseSeparator.Name = "lblBrowseSeparator";
            this.lblBrowseSeparator.Size = new System.Drawing.Size(437, 2);
            this.lblBrowseSeparator.TabIndex = 7;
            // 
            // sbrStatus
            // 
            this.sbrStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pgbProgress});
            this.sbrStatus.Location = new System.Drawing.Point(0, 110);
            this.sbrStatus.Name = "sbrStatus";
            this.sbrStatus.Size = new System.Drawing.Size(461, 22);
            this.sbrStatus.TabIndex = 10;
            this.sbrStatus.Text = "statusStrip1";
            // 
            // pgbProgress
            // 
            this.pgbProgress.Name = "pgbProgress";
            this.pgbProgress.Size = new System.Drawing.Size(100, 16);
            this.pgbProgress.Step = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(374, 84);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.OnClose);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Enabled = false;
            this.btnCreate.Location = new System.Drawing.Point(293, 84);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 8;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.OnCreate);
            // 
            // ofdDoubleKnot
            // 
            this.ofdDoubleKnot.Filter = "Excel 2007 Spreadsheet (.xlsx)|*.xlsx|Excel 97 - 2003 Spreadsheets (.xls)|*.xls";
            this.ofdDoubleKnot.Title = "Select a DoubleKnot roster";
            // 
            // bgwCreate
            // 
            this.bgwCreate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCreate_DoWork);
            this.bgwCreate.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwCreate_ProgressChanged);
            this.bgwCreate.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwCreate_RunWorkerCompleted);
            // 
            // CreatePaperwork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 132);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.sbrStatus);
            this.Controls.Add(this.lblBrowseSeparator);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtDoubleKnotRoster);
            this.Controls.Add(this.lblDoubleKnotRoster);
            this.Controls.Add(this.lblWeekSeparator);
            this.Controls.Add(this.mbrCreatePaperwork);
            this.Controls.Add(this.cboWeek);
            this.Controls.Add(this.lblWeek);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(477, 170);
            this.Name = "CreatePaperwork";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Paperwork";
            this.mbrCreatePaperwork.ResumeLayout(false);
            this.mbrCreatePaperwork.PerformLayout();
            this.sbrStatus.ResumeLayout(false);
            this.sbrStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mbrCreatePaperwork;
        private System.Windows.Forms.ToolStripMenuItem mnuFileMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuClose;
        private System.Windows.Forms.ComboBox cboWeek;
        private System.Windows.Forms.Label lblWeek;
        private System.Windows.Forms.Label lblWeekSeparator;
        private System.Windows.Forms.Label lblDoubleKnotRoster;
        private System.Windows.Forms.TextBox txtDoubleKnotRoster;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.ToolStripMenuItem mnuBrowse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuCreatePaperwork;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.Label lblBrowseSeparator;
        private System.Windows.Forms.StatusStrip sbrStatus;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.OpenFileDialog ofdDoubleKnot;
        private System.Windows.Forms.ToolStripProgressBar pgbProgress;
        private System.ComponentModel.BackgroundWorker bgwCreate;
    }
}