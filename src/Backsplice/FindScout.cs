using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Backsplice
{
    public partial class FindScout : Form
    {
        private List<CampProgram> m_objResults = new List<CampProgram>();

        public FindScout()
        {
            InitializeComponent();
        }

        private void OnFind()
        {
            lblScoutWeek.Text = "";
            lblScoutName.Text = "";
            lblScoutTroop.Text = "";
            btnDropAdd.Enabled = false;
            mnuDropAdd.Enabled = false;
            dgvMeritBadges.Rows.Clear();
            m_objResults.Clear();

            string strTroopNumber;
            string strWeek = cboWeek.Text;
            string strFirstName = txtFirstName.Text;
            string strLastName = txtLastName.Text;

            this.Cursor = Cursors.WaitCursor;
            this.m_objResults = BackspliceMain.FindScout(strWeek, strFirstName, strLastName, out strTroopNumber);

            if (m_objResults.Count > 0)
            {
                for (int i = 0; i < m_objResults.Count; i++)
                {
                    string[] strValues = new string[2];
                    strValues[0] = m_objResults[i].Name;
                    strValues[1] = m_objResults[i].Period;
                    dgvMeritBadges.Rows.Add(strValues);
                }

                lblScoutWeek.Text = strWeek;
                lblScoutName.Text = strFirstName + " " + strLastName;
                lblScoutTroop.Text = strTroopNumber;
                btnDropAdd.Enabled = true;
                mnuDropAdd.Enabled = true;

                this.Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show("Scout " + "\"" + strFirstName + " " + strLastName + "\"" + " was not found.  Did you select the correct week?", "Scout Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Cursor = Cursors.Default;
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            this.OnFind();
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
                    txtFirstName.Enabled = false;
                    txtLastName.Enabled = false;
                }
            }
            else if (cboWeek.Text == "")
            {
                cboWeek.BackColor = SystemColors.Window;
                txtFirstName.Enabled = false;
                txtLastName.Enabled = false;
                btnFind.Enabled = false;
                mnuFind.Enabled = false;
                btnDropAdd.Enabled = false;
                mnuDropAdd.Enabled = false;
            }
            else
            {
                cboWeek.BackColor = SystemColors.Window;
                txtFirstName.Enabled = true;
                txtLastName.Enabled = true;
                btnDropAdd.Enabled = false;
                mnuDropAdd.Enabled = false;
                if (txtFirstName.Text != "" && txtLastName.Text != "")
                {
                    btnFind.Enabled = true;
                    mnuFind.Enabled = true;
                }
                else
                {
                    btnFind.Enabled = false;
                    mnuFind.Enabled = false;
                }

                if (dgvMeritBadges.Rows.Count > 0)
                {
                    btnDropAdd.Enabled = true;
                    mnuDropAdd.Enabled = true;
                }
            }
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            this.OnNameEntry();
        }

        private void OnNameEntry()
        {
            if (txtFirstName.Text != "" && txtLastName.Text != "")
            {
                btnFind.Enabled = true;
                mnuFind.Enabled = true;
            }
            else
            {
                btnFind.Enabled = false;
                mnuFind.Enabled = false;
            }
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            this.OnNameEntry();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.OnClose();
        }

        private void mnuFind_Click(object sender, EventArgs e)
        {
            this.OnFind();
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            this.OnClose();
        }

        private void OnClose()
        {
            this.Close();
        }

        private void btnDropAdd_Click(object sender, EventArgs e)
        {
            this.OnDropAdd();
        }

        private void OnDropAdd()
        {
            DropAdd daDropAdd = new DropAdd(m_objResults, lblScoutWeek.Text, lblScoutName.Text, lblScoutTroop.Text);
            daDropAdd.ShowDialog();
            this.OnFind();
        }

        private void FindScout_Load(object sender, EventArgs e)
        {
            // Populate the week selection list with the available weeks
            string[] strWeeks = System.IO.Directory.GetDirectories(BackspliceMain.SaveDirectory);
            for (int i = 0; i < strWeeks.Length; i++)
            {
                string[] strWeekParts = strWeeks[i].Split(new char[] { '\\', ' ' });
                cboWeek.Items.Add(strWeekParts[strWeekParts.Length - 1]);
            }

            if (strWeeks.Length == 0)
            {
                MessageBox.Show("It appears that you have not created any paperwork yet. Please create paperwork before using Find Scout.", "Paperwork Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void mnuDropAdd_Click(object sender, EventArgs e)
        {
            this.OnDropAdd();
        }
    }
}
