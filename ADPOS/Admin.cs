using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADPOS
{
    public partial class frm_Admin : Form
    {
        public frm_Admin()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void frm_Admin_Load(object sender, EventArgs e)
        {
            foreach (Control ctrl in panelLoad.Controls)
            {
                ctrl.Dispose();
            }
            panelLoad.Controls.Add(new UC_Admin());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in panelLoad.Controls)
            {
                ctrl.Dispose();
            }
            panelLoad.Controls.Add(new UC_Stock());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in panelLoad.Controls)
            {
                ctrl.Dispose();
            }
            panelLoad.Controls.Add(new UC_Supplier());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in panelLoad.Controls)
            {
                ctrl.Dispose();
            }
            panelLoad.Controls.Add(new UC_Manage_Users());
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in panelLoad.Controls)
            {
                ctrl.Dispose();
            }
            panelLoad.Controls.Add(new UC_Admin());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in panelLoad.Controls)
            {
                ctrl.Dispose();
            }
            panelLoad.Controls.Add(new UC_Inventory());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in panelLoad.Controls)
            {
                ctrl.Dispose();
            }
            panelLoad.Controls.Add(new UC_Stock());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in panelLoad.Controls)
            {
                ctrl.Dispose();
            }
            panelLoad.Controls.Add(new UC_Purchase_Order());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in panelLoad.Controls)
            {
                ctrl.Dispose();
            }
            panelLoad.Controls.Add(new UC_Search_Products());
        }
    }
}
