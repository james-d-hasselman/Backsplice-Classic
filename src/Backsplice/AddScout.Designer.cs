namespace Backsplice
{
    partial class AddScout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddScout));
            this.dgvPrograms = new System.Windows.Forms.DataGridView();
            this.clmProgram = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmClassSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblAddSeparator = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrograms)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPrograms
            // 
            this.dgvPrograms.AllowUserToAddRows = false;
            this.dgvPrograms.AllowUserToDeleteRows = false;
            this.dgvPrograms.AllowUserToResizeColumns = false;
            this.dgvPrograms.AllowUserToResizeRows = false;
            this.dgvPrograms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPrograms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvPrograms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrograms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmProgram,
            this.clmPeriod,
            this.clmClassSize});
            this.dgvPrograms.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvPrograms.Location = new System.Drawing.Point(12, 12);
            this.dgvPrograms.MultiSelect = false;
            this.dgvPrograms.Name = "dgvPrograms";
            this.dgvPrograms.ReadOnly = true;
            this.dgvPrograms.RowHeadersVisible = false;
            this.dgvPrograms.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvPrograms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPrograms.Size = new System.Drawing.Size(336, 237);
            this.dgvPrograms.TabIndex = 0;
            this.dgvPrograms.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPrograms_CellMouseDoubleClick);
            // 
            // clmProgram
            // 
            this.clmProgram.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.clmProgram.HeaderText = "Program";
            this.clmProgram.Name = "clmProgram";
            this.clmProgram.ReadOnly = true;
            this.clmProgram.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmProgram.Width = 71;
            // 
            // clmPeriod
            // 
            this.clmPeriod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.clmPeriod.HeaderText = "Period";
            this.clmPeriod.Name = "clmPeriod";
            this.clmPeriod.ReadOnly = true;
            this.clmPeriod.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmPeriod.Width = 62;
            // 
            // clmClassSize
            // 
            this.clmClassSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.clmClassSize.DefaultCellStyle = dataGridViewCellStyle1;
            this.clmClassSize.HeaderText = "Class Size";
            this.clmClassSize.Name = "clmClassSize";
            this.clmClassSize.ReadOnly = true;
            this.clmClassSize.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.clmClassSize.Width = 80;
            // 
            // lblAddSeparator
            // 
            this.lblAddSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAddSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAddSeparator.Location = new System.Drawing.Point(12, 252);
            this.lblAddSeparator.Name = "lblAddSeparator";
            this.lblAddSeparator.Size = new System.Drawing.Size(336, 2);
            this.lblAddSeparator.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(273, 257);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(192, 257);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // AddScout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 292);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblAddSeparator);
            this.Controls.Add(this.dgvPrograms);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(376, 330);
            this.Name = "AddScout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Scout";
            this.Load += new System.EventHandler(this.AddScout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrograms)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPrograms;
        private System.Windows.Forms.Label lblAddSeparator;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmProgram;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPeriod;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmClassSize;

    }
}