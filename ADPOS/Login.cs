using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace ADPOS
{
    public partial class Login : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
        public Login()
        {
            InitializeComponent();
        }

        private  int returnRows(string UserName, string Password)
        {
            using (MySqlConnection con = new MySqlConnection(cs))
            {
                string sql = "SELECT COUNT(*) AS cnt FROM tbl_user WHERE tbl_user.User_Name = '" + UserName + "' AND tbl_user.Password = '" + Password + "';";
                MySqlCommand cmd = new  MySqlCommand(sql, con);
                con.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int Count = Convert.ToInt32(dr["cnt"]);
                return Count;                
            }
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            var login = new LoginUser()
            {
                Uname = txtUserName.Text,
                Pswd=txtPassword.Text
            };
            

            int count = returnRows(login.Uname, login.Pswd);

            if (count == 1)
            {
                MessageBox.Show("Welcome "+login.Uname+"");
                frm_Admin myForm = new frm_Admin();
                this.Hide();
                myForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("user name or password incorrect");
            }
            
        }
           



       
    }
}
