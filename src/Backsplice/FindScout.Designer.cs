namespace Backsplice
{
    partial class FindScout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindScout));
            this.mbrFindScout = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFind = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDropAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.lblWeek = new System.Windows.Forms.Label();
            this.cboWeek = new System.Windows.Forms.ComboBox();
            this.lblWeekSeparator = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.lblFindSeparator = new System.Windows.Forms.Label();
            this.lblWeekResult = new System.Windows.Forms.Label();
            this.lblNameResult = new System.Windows.Forms.Label();
            this.lblTroopResult = new System.Windows.Forms.Label();
            this.lblMeritBadges = new System.Windows.Forms.Label();
            this.dgvMeritBadges = new System.Windows.Forms.DataGridView();
            this.clmMeritBadges = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sbrStatus = new System.Windows.Forms.StatusStrip();
            this.lblResultsSeparator = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDropAdd = new System.Windows.Forms.Button();
            this.lblScoutWeek = new System.Windows.Forms.Label();
            this.lblScoutName = new System.Windows.Forms.Label();
            this.lblScoutTroop = new System.Windows.Forms.Label();
            this.mbrFindScout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeritBadges)).BeginInit();
            this.SuspendLayout();
            // 
            // mbrFindScout
            // 
            this.mbrFindScout.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.helpToolStripMenuItem});
            this.mbrFindScout.Location = new System.Drawing.Point(0, 0);
            this.mbrFindScout.Name = "mbrFindScout";
            this.mbrFindScout.Size = new System.Drawing.Size(355, 24);
            this.mbrFindScout.TabIndex = 0;
            this.mbrFindScout.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClose});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mnuClose
            // 
            this.mnuClose.Name = "mnuClose";
            this.mnuClose.Size = new System.Drawing.Size(103, 22);
            this.mnuClose.Text = "&Close";
            this.mnuClose.Click += new System.EventHandler(this.mnuClose_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFind,
            this.toolStripSeparator2,
            this.mnuDropAdd});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(61, 20);
            this.toolStripMenuItem1.Text = "&Options";
            // 
            // mnuFind
            // 
            this.mnuFind.Enabled = false;
            this.mnuFind.Name = "mnuFind";
            this.mnuFind.Size = new System.Drawing.Size(127, 22);
            this.mnuFind.Text = "&Find";
            this.mnuFind.Click += new System.EventHandler(this.mnuFind_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(124, 6);
            // 
            // mnuDropAdd
            // 
            this.mnuDropAdd.Enabled = false;
            this.mnuDropAdd.Name = "mnuDropAdd";
            this.mnuDropAdd.Size = new System.Drawing.Size(127, 22);
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
            // lblWeek
            // 
            this.lblWeek.AutoSize = true;
            this.lblWeek.Location = new System.Drawing.Point(12, 30);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(39, 13);
            this.lblWeek.TabIndex = 1;
            this.lblWeek.Text = "Week:";
            // 
            // cboWeek
            // 
            this.cboWeek.FormattingEnabled = true;
            this.cboWeek.Location = new System.Drawing.Point(57, 27);
            this.cboWeek.MaxLength = 1;
            this.cboWeek.Name = "cboWeek";
            this.cboWeek.Size = new System.Drawing.Size(37, 21);
            this.cboWeek.TabIndex = 2;
            this.cboWeek.TextChanged += new System.EventHandler(this.cboWeek_TextChanged);
            // 
            // lblWeekSeparator
            // 
            this.lblWeekSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWeekSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblWeekSeparator.Location = new System.Drawing.Point(12, 51);
            this.lblWeekSeparator.Name = "lblWeekSeparator";
            this.lblWeekSeparator.Size = new System.Drawing.Size(331, 2);
            this.lblWeekSeparator.TabIndex = 3;
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(12, 59);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(60, 13);
            this.lblFirstName.TabIndex = 4;
            this.lblFirstName.Text = "First Name:";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFirstName.Enabled = false;
            this.txtFirstName.Location = new System.Drawing.Point(78, 56);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(184, 20);
            this.txtFirstName.TabIndex = 5;
            this.txtFirstName.TextChanged += new System.EventHandler(this.txtFirstName_TextChanged);
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(12, 85);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(61, 13);
            this.lblLastName.TabIndex = 6;
            this.lblLastName.Text = "Last Name:";
            // 
            // txtLastName
            // 
            this.txtLastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLastName.Enabled = false;
            this.txtLastName.Location = new System.Drawing.Point(78, 82);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(184, 20);
            this.txtLastName.TabIndex = 7;
            this.txtLastName.TextChanged += new System.EventHandler(this.txtLastName_TextChanged);
            // 
            // btnFind
            // 
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.Enabled = false;
            this.btnFind.Location = new System.Drawing.Point(268, 80);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 8;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // lblFindSeparator
            // 
            this.lblFindSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFindSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFindSeparator.Location = new System.Drawing.Point(12, 106);
            this.lblFindSeparator.Name = "lblFindSeparator";
            this.lblFindSeparator.Size = new System.Drawing.Size(331, 2);
            this.lblFindSeparator.TabIndex = 10;
            // 
            // lblWeekResult
            // 
            this.lblWeekResult.AutoSize = true;
            this.lblWeekResult.Location = new System.Drawing.Point(12, 108);
            this.lblWeekResult.Name = "lblWeekResult";
            this.lblWeekResult.Size = new System.Drawing.Size(39, 13);
            this.lblWeekResult.TabIndex = 9;
            this.lblWeekResult.Text = "Week:";
            // 
            // lblNameResult
            // 
            this.lblNameResult.AutoSize = true;
            this.lblNameResult.Location = new System.Drawing.Point(12, 121);
            this.lblNameResult.Name = "lblNameResult";
            this.lblNameResult.Size = new System.Drawing.Size(38, 13);
            this.lblNameResult.TabIndex = 12;
            this.lblNameResult.Text = "Name:";
            // 
            // lblTroopResult
            // 
            this.lblTroopResult.AutoSize = true;
            this.lblTroopResult.Location = new System.Drawing.Point(12, 134);
            this.lblTroopResult.Name = "lblTroopResult";
            this.lblTroopResult.Size = new System.Drawing.Size(38, 13);
            this.lblTroopResult.TabIndex = 14;
            this.lblTroopResult.Text = "Troop:";
            // 
            // lblMeritBadges
            // 
            this.lblMeritBadges.AutoSize = true;
            this.lblMeritBadges.Location = new System.Drawing.Point(12, 147);
            this.lblMeritBadges.Name = "lblMeritBadges";
            this.lblMeritBadges.Size = new System.Drawing.Size(78, 13);
            this.lblMeritBadges.TabIndex = 16;
            this.lblMeritBadges.Text = "Merit Badge(s):";
            // 
            // dgvMeritBadges
            // 
            this.dgvMeritBadges.AllowUserToAddRows = false;
            this.dgvMeritBadges.AllowUserToDeleteRows = false;
            this.dgvMeritBadges.AllowUserToResizeColumns = false;
            this.dgvMeritBadges.AllowUserToResizeRows = false;
            this.dgvMeritBadges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMeritBadges.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMeritBadges.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMeritBadges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMeritBadges.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmMeritBadges,
            this.clmPeriod});
            this.dgvMeritBadges.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMeritBadges.Location = new System.Drawing.Point(96, 149);
            this.dgvMeritBadges.Name = "dgvMeritBadges";
            this.dgvMeritBadges.RowHeadersVisible = false;
            this.dgvMeritBadges.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMeritBadges.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMeritBadges.Size = new System.Drawing.Size(247, 121);
            this.dgvMeritBadges.TabIndex = 17;
            // 
            // clmMeritBadges
            // 
            this.clmMeritBadges.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmMeritBadges.FillWeight = 102F;
            this.clmMeritBadges.HeaderText = "Merit Badge";
            this.clmMeritBadges.Name = "clmMeritBadges";
            // 
            // clmPeriod
            // 
            this.clmPeriod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmPeriod.FillWeight = 85F;
            this.clmPeriod.HeaderText = "Period";
            this.clmPeriod.Name = "clmPeriod";
            // 
            // sbrStatus
            // 
            this.sbrStatus.Location = new System.Drawing.Point(0, 304);
            this.sbrStatus.Name = "sbrStatus";
            this.sbrStatus.Size = new System.Drawing.Size(355, 22);
            this.sbrStatus.TabIndex = 21;
            this.sbrStatus.Text = "statusStrip1";
            // 
            // lblResultsSeparator
            // 
            this.lblResultsSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResultsSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblResultsSeparator.Location = new System.Drawing.Point(15, 273);
            this.lblResultsSeparator.Name = "lblResultsSeparator";
            this.lblResultsSeparator.Size = new System.Drawing.Size(331, 2);
            this.lblResultsSeparator.TabIndex = 18;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(268, 278);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDropAdd
            // 
            this.btnDropAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDropAdd.Enabled = false;
            this.btnDropAdd.Location = new System.Drawing.Point(187, 278);
            this.btnDropAdd.Name = "btnDropAdd";
            this.btnDropAdd.Size = new System.Drawing.Size(75, 23);
            this.btnDropAdd.TabIndex = 19;
            this.btnDropAdd.Text = "Drop/Add";
            this.btnDropAdd.UseVisualStyleBackColor = true;
            this.btnDropAdd.Click += new System.EventHandler(this.btnDropAdd_Click);
            // 
            // lblScoutWeek
            // 
            this.lblScoutWeek.AutoSize = true;
            this.lblScoutWeek.Location = new System.Drawing.Point(57, 108);
            this.lblScoutWeek.Name = "lblScoutWeek";
            this.lblScoutWeek.Size = new System.Drawing.Size(0, 13);
            this.lblScoutWeek.TabIndex = 11;
            // 
            // lblScoutName
            // 
            this.lblScoutName.AutoSize = true;
            this.lblScoutName.Location = new System.Drawing.Point(57, 121);
            this.lblScoutName.Name = "lblScoutName";
            this.lblScoutName.Size = new System.Drawing.Size(0, 13);
            this.lblScoutName.TabIndex = 13;
            // 
            // lblScoutTroop
            // 
            this.lblScoutTroop.AutoSize = true;
            this.lblScoutTroop.Location = new System.Drawing.Point(57, 134);
            this.lblScoutTroop.Name = "lblScoutTroop";
            this.lblScoutTroop.Size = new System.Drawing.Size(0, 13);
            this.lblScoutTroop.TabIndex = 15;
            // 
            // FindScout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 326);
            this.Controls.Add(this.lblScoutTroop);
            this.Controls.Add(this.lblScoutName);
            this.Controls.Add(this.lblScoutWeek);
            this.Controls.Add(this.btnDropAdd);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblResultsSeparator);
            this.Controls.Add(this.sbrStatus);
            this.Controls.Add(this.dgvMeritBadges);
            this.Controls.Add(this.lblMeritBadges);
            this.Controls.Add(this.lblTroopResult);
            this.Controls.Add(this.lblNameResult);
            this.Controls.Add(this.lblWeekResult);
            this.Controls.Add(this.lblFindSeparator);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.lblWeekSeparator);
            this.Controls.Add(this.cboWeek);
            this.Controls.Add(this.lblWeek);
            this.Controls.Add(this.mbrFindScout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(371, 364);
            this.Name = "FindScout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Find Scout";
            this.Load += new System.EventHandler(this.FindScout_Load);
            this.mbrFindScout.ResumeLayout(false);
            this.mbrFindScout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeritBadges)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mbrFindScout;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuClose;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuFind;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.Label lblWeek;
        private System.Windows.Forms.ComboBox cboWeek;
        private System.Windows.Forms.Label lblWeekSeparator;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label lblFindSeparator;
        private System.Windows.Forms.Label lblWeekResult;
        private System.Windows.Forms.Label lblNameResult;
        private System.Windows.Forms.Label lblTroopResult;
        private System.Windows.Forms.Label lblMeritBadges;
        private System.Windows.Forms.DataGridView dgvMeritBadges;
        private System.Windows.Forms.StatusStrip sbrStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuDropAdd;
        private System.Windows.Forms.Label lblResultsSeparator;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDropAdd;
        private System.Windows.Forms.Label lblScoutWeek;
        private System.Windows.Forms.Label lblScoutName;
        private System.Windows.Forms.Label lblScoutTroop;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmMeritBadges;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPeriod;
    }
}