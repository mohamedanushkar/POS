using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace ADPOS
{
    public partial class UC_Inventory : UserControl
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
        public UC_Inventory()
        {
            InitializeComponent();
            LoadProductCategoryId();
        }
        //Load PRODUCT CATEGORY to a combobox
        private void LoadProductCategoryId()
        {
            using (MySqlConnection con = new MySqlConnection(cs))
            {
                try {
                    con.Open(); // open connection
                    var query = "SELECT Category_Name FROM tbl_Product_Category"; // combo box query
                    using (var command = new MySqlCommand(query, con))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            //Iterate through the rows and add it to the combobox's items
                            while (reader.Read())
                            {
                                cmbProductCategory.Items.Add(reader.GetString("Category_Name"));
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
                
            }
        }
        private int AutoIncrementValue() // auto increment function
        {
            using (MySqlConnection con = new MySqlConnection(cs))
            {
                string sql = "SELECT `inv_no` FROM `inv_master` ORDER BY inv_no DESC LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                con.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int Invno = Convert.ToInt32(dr["inv_no"]);
                return Invno;

            }
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection())
            try
            {
                con.Open();
                //This is my update query in which i am taking input from the user through windows forms and update the record.  
                string Query = "UPDATE tbl_product SET Product_ID='"++"',Product_Barcode='"+txtProductBarcode.Text+"',Name='"+txtProductName.Text+"',Description='"+txtDescription.Text+"',Price='"+txtPrice.Text+"',Unit_Price='"+txtUnitPrice.Text+"',Product_Category_ID='"++"';" ;
                MySqlCommand cmd = new MySqlCommand(Query, con);
                MySqlDataReader dr = cmd.ExecuteReader();
                MessageBox.Show("Data Updated");
                con.Close();//Connection closed here  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
