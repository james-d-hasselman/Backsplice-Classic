namespace Backsplice
{
    partial class PrintPaperwork
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintPaperwork));
            this.mbrPrintPaperwork = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sbrStatus = new System.Windows.Forms.StatusStrip();
            this.cboWeek = new System.Windows.Forms.ComboBox();
            this.lblWeek = new System.Windows.Forms.Label();
            this.lblMeritBadge = new System.Windows.Forms.Label();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.cboPeriod = new System.Windows.Forms.ComboBox();
            this.cboMeritBadge = new System.Windows.Forms.ComboBox();
            this.lblWeekSeparator = new System.Windows.Forms.Label();
            this.lblSelectionSeparator = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.pdPrintDialog = new System.Windows.Forms.PrintDialog();
            this.btnSendTo = new System.Windows.Forms.Button();
            this.sfdSendTo = new System.Windows.Forms.SaveFileDialog();
            this.fbdSendTo = new System.Windows.Forms.FolderBrowserDialog();
            this.mbrPrintPaperwork.SuspendLayout();
            this.SuspendLayout();
            // 
            // mbrPrintPaperwork
            // 
            this.mbrPrintPaperwork.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mbrPrintPaperwork.Location = new System.Drawing.Point(0, 0);
            this.mbrPrintPaperwork.Name = "mbrPrintPaperwork";
            this.mbrPrintPaperwork.Size = new System.Drawing.Size(310, 24);
            this.mbrPrintPaperwork.TabIndex = 0;
            this.mbrPrintPaperwork.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPrint,
            this.toolStripSeparator1,
            this.mnuClose});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mnuPrint
            // 
            this.mnuPrint.Enabled = false;
            this.mnuPrint.Name = "mnuPrint";
            this.mnuPrint.Size = new System.Drawing.Size(103, 22);
            this.mnuPrint.Text = "&Print";
            this.mnuPrint.Click += new System.EventHandler(this.mnuPrint_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(100, 6);
            // 
            // mnuClose
            // 
            this.mnuClose.Name = "mnuClose";
            this.mnuClose.Size = new System.Drawing.Size(103, 22);
            this.mnuClose.Text = "&Close";
            this.mnuClose.Click += new System.EventHandler(this.mnuClose_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(99, 22);
            this.helpToolStripMenuItem1.Text = "&Help";
            // 
            // sbrStatus
            // 
            this.sbrStatus.Location = new System.Drawing.Point(0, 138);
            this.sbrStatus.Name = "sbrStatus";
            this.sbrStatus.Size = new System.Drawing.Size(310, 22);
            this.sbrStatus.TabIndex = 1;
            this.sbrStatus.Text = "statusStrip1";
            // 
            // cboWeek
            // 
            this.cboWeek.FormattingEnabled = true;
            this.cboWeek.Location = new System.Drawing.Point(57, 27);
            this.cboWeek.MaxLength = 1;
            this.cboWeek.Name = "cboWeek";
            this.cboWeek.Size = new System.Drawing.Size(37, 21);
            this.cboWeek.Sorted = true;
            this.cboWeek.TabIndex = 5;
            this.cboWeek.TextChanged += new System.EventHandler(this.cboWeek_TextChanged);
            // 
            // lblWeek
            // 
            this.lblWeek.AutoSize = true;
            this.lblWeek.Location = new System.Drawing.Point(12, 30);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(39, 13);
            this.lblWeek.TabIndex = 4;
            this.lblWeek.Text = "Week:";
            // 
            // lblMeritBadge
            // 
            this.lblMeritBadge.AutoSize = true;
            this.lblMeritBadge.Location = new System.Drawing.Point(12, 59);
            this.lblMeritBadge.Name = "lblMeritBadge";
            this.lblMeritBadge.Size = new System.Drawing.Size(78, 13);
            this.lblMeritBadge.TabIndex = 7;
            this.lblMeritBadge.Text = "Merit Badge(s):";
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Location = new System.Drawing.Point(12, 86);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(51, 13);
            this.lblPeriod.TabIndex = 8;
            this.lblPeriod.Text = "Period(s):";
            // 
            // cboPeriod
            // 
            this.cboPeriod.Enabled = false;
            this.cboPeriod.FormattingEnabled = true;
            this.cboPeriod.Items.AddRange(new object[] {
            "All"});
            this.cboPeriod.Location = new System.Drawing.Point(96, 83);
            this.cboPeriod.Name = "cboPeriod";
            this.cboPeriod.Size = new System.Drawing.Size(121, 21);
            this.cboPeriod.Sorted = true;
            this.cboPeriod.TabIndex = 10;
            this.cboPeriod.Text = "All";
            // 
            // cboMeritBadge
            // 
            this.cboMeritBadge.Enabled = false;
            this.cboMeritBadge.FormattingEnabled = true;
            this.cboMeritBadge.Items.AddRange(new object[] {
            "All"});
            this.cboMeritBadge.Location = new System.Drawing.Point(96, 56);
            this.cboMeritBadge.Name = "cboMeritBadge";
            this.cboMeritBadge.Size = new System.Drawing.Size(121, 21);
            this.cboMeritBadge.Sorted = true;
            this.cboMeritBadge.TabIndex = 11;
            this.cboMeritBadge.Text = "All";
            this.cboMeritBadge.TextChanged += new System.EventHandler(this.cboMeritBadge_TextChanged);
            // 
            // lblWeekSeparator
            // 
            this.lblWeekSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWeekSeparator.Location = new System.Drawing.Point(12, 51);
            this.lblWeekSeparator.Name = "lblWeekSeparator";
            this.lblWeekSeparator.Size = new System.Drawing.Size(287, 2);
            this.lblWeekSeparator.TabIndex = 12;
            // 
            // lblSelectionSeparator
            // 
            this.lblSelectionSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSelectionSeparator.Location = new System.Drawing.Point(11, 107);
            this.lblSelectionSeparator.Name = "lblSelectionSeparator";
            this.lblSelectionSeparator.Size = new System.Drawing.Size(287, 2);
            this.lblSelectionSeparator.TabIndex = 13;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(223, 112);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Location = new System.Drawing.Point(61, 112);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 15;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // pdPrintDialog
            // 
            this.pdPrintDialog.UseEXDialog = true;
            // 
            // btnSendTo
            // 
            this.btnSendTo.Location = new System.Drawing.Point(142, 112);
            this.btnSendTo.Name = "btnSendTo";
            this.btnSendTo.Size = new System.Drawing.Size(75, 23);
            this.btnSendTo.TabIndex = 16;
            this.btnSendTo.Text = "Send To...";
            this.btnSendTo.UseVisualStyleBackColor = true;
            this.btnSendTo.Click += new System.EventHandler(this.btnSendTo_Click);
            // 
            // fbdSendTo
            // 
            this.fbdSendTo.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // PrintPaperwork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 160);
            this.Controls.Add(this.btnSendTo);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblSelectionSeparator);
            this.Controls.Add(this.lblWeekSeparator);
            this.Controls.Add(this.cboMeritBadge);
            this.Controls.Add(this.cboPeriod);
            this.Controls.Add(this.lblPeriod);
            this.Controls.Add(this.lblMeritBadge);
            this.Controls.Add(this.cboWeek);
            this.Controls.Add(this.lblWeek);
            this.Controls.Add(this.sbrStatus);
            this.Controls.Add(this.mbrPrintPaperwork);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mbrPrintPaperwork;
            this.Name = "PrintPaperwork";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Print Paperwork";
            this.Load += new System.EventHandler(this.PrintPaperwork_Load);
            this.mbrPrintPaperwork.ResumeLayout(false);
            this.mbrPrintPaperwork.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mbrPrintPaperwork;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuClose;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.StatusStrip sbrStatus;
        private System.Windows.Forms.ComboBox cboWeek;
        private System.Windows.Forms.Label lblWeek;
        private System.Windows.Forms.Label lblMeritBadge;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.ComboBox cboPeriod;
        private System.Windows.Forms.ComboBox cboMeritBadge;
        private System.Windows.Forms.Label lblWeekSeparator;
        private System.Windows.Forms.Label lblSelectionSeparator;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.PrintDialog pdPrintDialog;
        private System.Windows.Forms.Button btnSendTo;
        private System.Windows.Forms.SaveFileDialog sfdSendTo;
        private System.Windows.Forms.FolderBrowserDialog fbdSendTo;
    }
}