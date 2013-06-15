namespace Mockups
{
    partial class TemplateMapping1
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
            this.doubleknotRosterLabel = new System.Windows.Forms.Label();
            this.templateDirectoryLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.Program = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Template = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // doubleknotRosterLabel
            // 
            this.doubleknotRosterLabel.AutoSize = true;
            this.doubleknotRosterLabel.Location = new System.Drawing.Point(12, 15);
            this.doubleknotRosterLabel.Name = "doubleknotRosterLabel";
            this.doubleknotRosterLabel.Size = new System.Drawing.Size(99, 13);
            this.doubleknotRosterLabel.TabIndex = 0;
            this.doubleknotRosterLabel.Text = "Doubleknot Roster:";
            // 
            // templateDirectoryLabel
            // 
            this.templateDirectoryLabel.AutoSize = true;
            this.templateDirectoryLabel.Location = new System.Drawing.Point(12, 41);
            this.templateDirectoryLabel.Name = "templateDirectoryLabel";
            this.templateDirectoryLabel.Size = new System.Drawing.Size(99, 13);
            this.templateDirectoryLabel.TabIndex = 1;
            this.templateDirectoryLabel.Text = "Template Directory:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(117, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(301, 20);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(117, 38);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(301, 20);
            this.textBox2.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(424, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Browse...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(424, 36);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Browse...";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Program,
            this.Template});
            this.dataGridView1.Location = new System.Drawing.Point(12, 95);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(487, 271);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 372);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(114, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Choose Template...";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // Program
            // 
            this.Program.HeaderText = "Program";
            this.Program.Name = "Program";
            // 
            // Template
            // 
            this.Template.HeaderText = "Template";
            this.Template.Name = "Template";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 66);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(114, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "Map Templates";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // TemplateMapping1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 423);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.templateDirectoryLabel);
            this.Controls.Add(this.doubleknotRosterLabel);
            this.Name = "TemplateMapping1";
            this.Text = "TemplateMapping1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label doubleknotRosterLabel;
        private System.Windows.Forms.Label templateDirectoryLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Program;
        private System.Windows.Forms.DataGridViewTextBoxColumn Template;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

