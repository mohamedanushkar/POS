using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using Microsoft.VisualBasic;

namespace ADPOS
{
    public partial class UC_Manage_Users : UserControl
    {

        public UC_Manage_Users()
        {
            InitializeComponent();
            DatabindtoGridView();
            DataBindToCombobox();
        }

        string ID;

        public class User
        {
        
            public int ID { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public int Phone { get; set; }
            public string Gender { get; set; }
            public string Mail { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public int User_Category_ID { get; set; }
            public void SaveUser(User usr)
            {

                using (MySqlConnection con = new MySqlConnection(LoginUser.cs))
                {
                    string sql = "INSERT INTO `tbl_user`(`User_ID`, `Name`, `Address`, `Phone`, `Gender`, `Mail`, `User_Name`, `Password`, `User_Category_ID`) VALUES ('"+ usr .ID+ "','"+ usr .Name+ "','"+ usr.Address+ "','"+ usr.Phone+ "','"+usr.Gender+"','"+usr.Mail+"','"+usr.UserName+"','"+usr.Password+"','"+usr.User_Category_ID+"');";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Added");

                }
            }
            public void DeleteUser(User usr)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this record ??", "Confirm Delete!!", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    using (MySqlConnection con = new MySqlConnection(LoginUser.cs))
                    {
                        string sql = "DELETE FROM `tbl_user` where User_ID=" + usr.ID;
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            public void UpdateUser(User usr)
            {
                using (MySqlConnection con = new MySqlConnection(LoginUser.cs))
                {
                    string sql = "UPDATE `tbl_user` SET `Name`='"+usr.Name+"',`Address`='"+usr.Address+"',`Phone`='"+usr.Phone+"',`Gender`='"+usr.Gender+"',`Mail`='"+usr.Mail+"',`User_Name`='"+usr.UserName+"',`Password`='"+usr.Password+"',`User_Category_ID`='"+usr.User_Category_ID+"' WHERE `User_ID`='"+usr.ID+"';";

                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully");

                }

            }
        }
        private void DatabindtoGridView()
        {
            using (MySqlConnection con = new MySqlConnection(LoginUser.cs))
            {
                string sql = "SELECT * FROM `tbl_user` INNER JOIN tbl_user_category ON tbl_user.User_Category_ID = tbl_user_category.User_Category_ID";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataGrid_user_list.DataSource = ds.Tables[0];
            }

            DataGrid_user_list.Columns[0].HeaderText = "User ID";
            DataGrid_user_list.Columns[1].HeaderText = "User Name";
            DataGrid_user_list.Columns[8].Visible = false;
            DataGrid_user_list.Columns[9].Visible = false;
        }

        public void newDetail()
        {
            txtName.Text = null;
            txtAddress.Text = null;
            txtPhone.Text = null;
            cmbGender.Enabled = true;
            txtMail.Text = null;
            txtUserName.Text = null;
            txtPassword.Text = null;
            txtConformPassword.Text = null;
            cmdUserCategory.Enabled = true;


        }

        private bool DataValid()
        {
            if (txtName.Text == "" )
            {
                MessageBox.Show("Enter Name");
                return false;
            }

            if (txtAddress.Text == "")
            {
                MessageBox.Show("Enter Address");
                return false;
            }

           
            if (txtPhone.Text == "" && !Information.IsNumeric(txtPhone.Text))
            {
                MessageBox.Show("Enter Unite Price");
                return false;
            }

            if (cmbGender.Text == "")
            {
                MessageBox.Show("Please Select The Gender");
                return false;
            }
            if (txtMail.Text == "")
            {
                MessageBox.Show("Enter Mail");
                return false;
            }
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Enter User Name");
                return false;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Enter Password");
                return false;
            }
            if (txtConformPassword.Text == "")
            {
                MessageBox.Show("Enter Confirm Password");
                return false;
            }
            if (txtPassword.Text != txtConformPassword.Text)
            {
                MessageBox.Show("Password Does not match");
                return false;
            }
            if (cmdUserCategory.Text == "")
            {
                MessageBox.Show("Slelct user Category");
                return false;
            }
            return true;
        }

        private void DataBindToCombobox()

        {

            using (MySqlConnection con = new MySqlConnection(LoginUser.cs))
            {

                string sql = "SELECT * FROM `tbl_user_category`";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                cmdUserCategory.DisplayMember = "User_Category";
                cmdUserCategory.ValueMember = "User_Category_ID";
                cmdUserCategory.DataSource = ds.Tables[0];

            }


        }

        private void DataGrid_user_list_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DataGrid_user_list.SelectedRows)
            {
                ID = row.Cells["User_ID"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value.ToString();
                cmbGender.Text = row.Cells["Gender"].Value.ToString();
                txtMail.Text = row.Cells["Mail"].Value.ToString();
                txtUserName.Text = row.Cells["User_Name"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();
                txtConformPassword.Text = row.Cells["Password"].Value.ToString();          
               
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DataValid())

            {
                User usr = new User();
                usr.ID = Convert.ToInt32(ID);
                usr.Name = txtName.Text;
                usr.Address = txtAddress.Text;
                usr.Phone = Convert.ToInt32(txtPhone.Text);
                usr.Gender = cmbGender.Text;
                usr.Mail = txtMail.Text;
                usr.UserName = txtUserName.Text;
                usr.Password = txtPassword.Text;
                usr.User_Category_ID = Convert.ToInt32(cmdUserCategory.SelectedValue.ToString());
                usr.SaveUser(usr);
                DatabindtoGridView();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            newDetail();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            User usr = new User();
            usr.ID = Convert.ToInt32(ID);
            usr.DeleteUser(usr);
            DatabindtoGridView();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (DataValid())

            {
                User usr = new User();
                usr.ID = Convert.ToInt32(ID);
                usr.Name = txtName.Text;
                usr.Address = txtAddress.Text;
                usr.Phone = Convert.ToInt32(txtPhone.Text);
                usr.Gender = cmbGender.Text;
                usr.Mail = txtMail.Text;
                usr.UserName = txtUserName.Text;
                usr.Password = txtPassword.Text;
                usr.User_Category_ID = Convert.ToInt32(cmdUserCategory.SelectedValue.ToString());
                usr.UpdateUser(usr);
                DatabindtoGridView();
            }
        }

      
    }
}
