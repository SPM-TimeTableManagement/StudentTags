using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentTagsSprint1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            customizeDesign();
        }

        private void customizeDesign()
        {
            panelSubStuMenu.Visible = false;
        }

        private void hideSubMenu()
        {
            if (panelSubStuMenu.Visible == true)
            {
                panelSubStuMenu.Visible = false;
            }

        }

        private void showSubMenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSubMenu();
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }
        }

        private void btnStu_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubStuMenu);
        }

        private void btnProg_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new Programme());
            
        }

        private void btnStuManage_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new EditStBatch());
        }

        private void btnStuBatch_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new StudentList());
        }

        private void btnSubGroup_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new EditStSubBatch());
        }

        private void btnTag_Click(object sender, EventArgs e)
        {
            openChildForm(new Tags());
        }

        private void buttonNotAvailable_Click(object sender, EventArgs e)
        {
            openChildForm(new NotAvailable());
        }

        private Form activeForm = null;

        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelForm1.Controls.Add(childForm);
            panelForm1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void buttonConsecutive_Click(object sender, EventArgs e)
        {
            openChildForm(new ConsecutiveSessions());
        }

        private void buttonOverlap_Click(object sender, EventArgs e)
        {
            openChildForm(new NotOverlapping());
        }

        private void buttonParallel_Click(object sender, EventArgs e)
        {
            openChildForm(new ParallelSession());

        }
    }
}
