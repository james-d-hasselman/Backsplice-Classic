namespace Backsplice
{
    partial class BackspliceMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackspliceMain));
            this.mbrBackspliceMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPreferences = new System.Windows.Forms.ToolStripMenuItem();
            this.paperworkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCreatePaperwork = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrintPaperwork = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFindScout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDropAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.lblCreatePaperwork = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreatePaperwork = new System.Windows.Forms.Button();
            this.btnPrintPaperwork = new System.Windows.Forms.Button();
            this.btnFindScout = new System.Windows.Forms.Button();
            this.lblFindScout = new System.Windows.Forms.Label();
            this.btnDropAdd = new System.Windows.Forms.Button();
            this.lblDropAdd = new System.Windows.Forms.Label();
            this.sbrFindScout = new System.Windows.Forms.StatusStrip();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.mbrBackspliceMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mbrBackspliceMenu
            // 
            this.mbrBackspliceMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.paperworkToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mbrBackspliceMenu.Location = new System.Drawing.Point(0, 0);
            this.mbrBackspliceMenu.Name = "mbrBackspliceMenu";
            this.mbrBackspliceMenu.Size = new System.Drawing.Size(332, 24);
            this.mbrBackspliceMenu.TabIndex = 0;
            this.mbrBackspliceMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUpdate,
            this.toolStripSeparator1,
            this.mnuExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mnuUpdate
            // 
            this.mnuUpdate.Name = "mnuUpdate";
            this.mnuUpdate.Size = new System.Drawing.Size(112, 22);
            this.mnuUpdate.Text = "&Update";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(109, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(112, 22);
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPreferences});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(39, 20);
            this.toolStripMenuItem1.Text = "&Edit";
            // 
            // mnuPreferences
            // 
            this.mnuPreferences.Name = "mnuPreferences";
            this.mnuPreferences.Size = new System.Drawing.Size(144, 22);
            this.mnuPreferences.Text = "&Preferences...";
            this.mnuPreferences.Click += new System.EventHandler(this.mnuPreferences_Click);
            // 
            // paperworkToolStripMenuItem
            // 
            this.paperworkToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCreatePaperwork,
            this.mnuPrintPaperwork,
            this.toolStripSeparator2,
            this.mnuFindScout,
            this.mnuDropAdd});
            this.paperworkToolStripMenuItem.Name = "paperworkToolStripMenuItem";
            this.paperworkToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.paperworkToolStripMenuItem.Text = "&Paperwork";
            // 
            // mnuCreatePaperwork
            // 
            this.mnuCreatePaperwork.Name = "mnuCreatePaperwork";
            this.mnuCreatePaperwork.Size = new System.Drawing.Size(167, 22);
            this.mnuCreatePaperwork.Text = "&Create Paperwork";
            this.mnuCreatePaperwork.Click += new System.EventHandler(this.mnuCreatePaperwork_Click);
            // 
            // mnuPrintPaperwork
            // 
            this.mnuPrintPaperwork.Name = "mnuPrintPaperwork";
            this.mnuPrintPaperwork.Size = new System.Drawing.Size(167, 22);
            this.mnuPrintPaperwork.Text = "&Print Paperwork";
            this.mnuPrintPaperwork.Click += new System.EventHandler(this.mnuPrintPaperwork_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(164, 6);
            // 
            // mnuFindScout
            // 
            this.mnuFindScout.Name = "mnuFindScout";
            this.mnuFindScout.Size = new System.Drawing.Size(167, 22);
            this.mnuFindScout.Text = "&Find Scout";
            this.mnuFindScout.Click += new System.EventHandler(this.mnuFindScout_Click);
            // 
            // mnuDropAdd
            // 
            this.mnuDropAdd.Name = "mnuDropAdd";
            this.mnuDropAdd.Size = new System.Drawing.Size(167, 22);
            this.mnuDropAdd.Text = "&Drop/Add";
            this.mnuDropAdd.Click += new System.EventHandler(this.mnuDropAdd_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelp});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // mnuHelp
            // 
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(99, 22);
            this.mnuHelp.Text = "&Help";
            // 
            // lblCreatePaperwork
            // 
            this.lblCreatePaperwork.AutoSize = true;
            this.lblCreatePaperwork.Location = new System.Drawing.Point(118, 32);
            this.lblCreatePaperwork.Name = "lblCreatePaperwork";
            this.lblCreatePaperwork.Size = new System.Drawing.Size(205, 13);
            this.lblCreatePaperwork.TabIndex = 1;
            this.lblCreatePaperwork.Text = "Create paperwork for this week\'s program.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Print previously created paperwork.";
            // 
            // btnCreatePaperwork
            // 
            this.btnCreatePaperwork.Location = new System.Drawing.Point(12, 27);
            this.btnCreatePaperwork.Name = "btnCreatePaperwork";
            this.btnCreatePaperwork.Size = new System.Drawing.Size(100, 23);
            this.btnCreatePaperwork.TabIndex = 3;
            this.btnCreatePaperwork.Text = "Create Paperwork";
            this.btnCreatePaperwork.UseVisualStyleBackColor = true;
            this.btnCreatePaperwork.Click += new System.EventHandler(this.btnCreatePaperwork_Click);
            // 
            // btnPrintPaperwork
            // 
            this.btnPrintPaperwork.Enabled = false;
            this.btnPrintPaperwork.Location = new System.Drawing.Point(12, 56);
            this.btnPrintPaperwork.Name = "btnPrintPaperwork";
            this.btnPrintPaperwork.Size = new System.Drawing.Size(100, 23);
            this.btnPrintPaperwork.TabIndex = 4;
            this.btnPrintPaperwork.Text = "Print Paperwork";
            this.btnPrintPaperwork.UseVisualStyleBackColor = true;
            this.btnPrintPaperwork.Click += new System.EventHandler(this.btnPrintPaperwork_Click);
            // 
            // btnFindScout
            // 
            this.btnFindScout.Enabled = false;
            this.btnFindScout.Location = new System.Drawing.Point(12, 85);
            this.btnFindScout.Name = "btnFindScout";
            this.btnFindScout.Size = new System.Drawing.Size(100, 23);
            this.btnFindScout.TabIndex = 5;
            this.btnFindScout.Text = "Find Scout";
            this.btnFindScout.UseVisualStyleBackColor = true;
            this.btnFindScout.Click += new System.EventHandler(this.btnFindScout_Click);
            // 
            // lblFindScout
            // 
            this.lblFindScout.AutoSize = true;
            this.lblFindScout.Location = new System.Drawing.Point(118, 90);
            this.lblFindScout.Name = "lblFindScout";
            this.lblFindScout.Size = new System.Drawing.Size(181, 13);
            this.lblFindScout.TabIndex = 6;
            this.lblFindScout.Text = "Search for information about a scout.";
            // 
            // btnDropAdd
            // 
            this.btnDropAdd.Enabled = false;
            this.btnDropAdd.Location = new System.Drawing.Point(12, 114);
            this.btnDropAdd.Name = "btnDropAdd";
            this.btnDropAdd.Size = new System.Drawing.Size(100, 23);
            this.btnDropAdd.TabIndex = 7;
            this.btnDropAdd.Text = "Drop/Add";
            this.btnDropAdd.UseVisualStyleBackColor = true;
            this.btnDropAdd.Click += new System.EventHandler(this.btnDropAdd_Click);
            // 
            // lblDropAdd
            // 
            this.lblDropAdd.AutoSize = true;
            this.lblDropAdd.Location = new System.Drawing.Point(118, 119);
            this.lblDropAdd.Name = "lblDropAdd";
            this.lblDropAdd.Size = new System.Drawing.Size(179, 13);
            this.lblDropAdd.TabIndex = 8;
            this.lblDropAdd.Text = "Change a scout\'s program schedule.";
            // 
            // sbrFindScout
            // 
            this.sbrFindScout.Location = new System.Drawing.Point(0, 140);
            this.sbrFindScout.Name = "sbrFindScout";
            this.sbrFindScout.Size = new System.Drawing.Size(332, 22);
            this.sbrFindScout.TabIndex = 9;
            this.sbrFindScout.Text = "statusStrip1";
            // 
            // BackspliceMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 162);
            this.Controls.Add(this.sbrFindScout);
            this.Controls.Add(this.lblDropAdd);
            this.Controls.Add(this.btnDropAdd);
            this.Controls.Add(this.lblFindScout);
            this.Controls.Add(this.btnFindScout);
            this.Controls.Add(this.btnPrintPaperwork);
            this.Controls.Add(this.btnCreatePaperwork);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCreatePaperwork);
            this.Controls.Add(this.mbrBackspliceMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mbrBackspliceMenu;
            this.MinimumSize = new System.Drawing.Size(348, 200);
            this.Name = "BackspliceMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backsplice";
            this.Load += new System.EventHandler(this.Backsplice_Load);
            this.mbrBackspliceMenu.ResumeLayout(false);
            this.mbrBackspliceMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mbrBackspliceMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem paperworkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuCreatePaperwork;
        private System.Windows.Forms.ToolStripMenuItem mnuPrintPaperwork;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuFindScout;
        private System.Windows.Forms.ToolStripMenuItem mnuDropAdd;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.Label lblCreatePaperwork;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreatePaperwork;
        private System.Windows.Forms.Button btnPrintPaperwork;
        private System.Windows.Forms.Button btnFindScout;
        private System.Windows.Forms.Label lblFindScout;
        private System.Windows.Forms.Button btnDropAdd;
        private System.Windows.Forms.Label lblDropAdd;
        private System.Windows.Forms.StatusStrip sbrFindScout;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuPreferences;
        private System.Windows.Forms.HelpProvider helpProvider1;
    }
}

