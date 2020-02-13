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
using Microsoft.VisualBasic;

namespace ADPOS
{
    public partial class UC_Stock : UserControl
    {

        public UC_Stock()
        {
            InitializeComponent();
            DatabindtoProductGridView();
        }


        private void DataGrid_Stock_Details_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        public class Stock
        {
            public int productID { get; set; }
            public Decimal quantity { get; set; }

            public void SaveProduct(Stock stk)
            {

                using (MySqlConnection con = new MySqlConnection(LoginUser.cs))
                {
                    string sql = "INSERT INTO `tbl_stock`( `Product_ID`, `Quantity`) VALUES ('" + stk.productID + "','" + stk.quantity + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Added");

                }

            }
            public void UpdateProduct(Stock stk)
            {
                using (MySqlConnection con = new MySqlConnection(LoginUser.cs))
                {
                    string sql = "UPDATE `tbl_stock` SET `Quantity`='" + stk.quantity + "'WHERE `Product_ID`='" + stk.productID + "'";

                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully");

                }

            }





        }
        private void DatabindtoProductGridView()
        {
            using (MySqlConnection con = new MySqlConnection(LoginUser.cs))
            {
                string sql = "SELECT a.Product_ID,a.Name, b.Category_Name, c.Quantity FROM tbl_product AS a INNER JOIN tbl_product_category AS b ON a.Product_Category_ID = b.Product_Category_ID INNER JOIN tbl_stock as c ON a.Product_ID = c.Product_ID";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataGrid_Product_Details.DataSource = ds.Tables[0];
            }

            DataGrid_Product_Details.Columns[0].HeaderText = "Product ID";

        }
        private bool DataValid()
        {
            if (txt_product_ID.Text == "" && !Information.IsNumeric(txt_product_ID.Text))
            {
                MessageBox.Show("Enter Product ID in Numeric Value");
                return false;
            }

            if (txt_Quantity.Text == "" && !Information.IsNumeric(txt_Quantity.Text))
            {
                MessageBox.Show("Enter Product Name");
                return false;
            }

            return true;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (DataValid())
            {
                Stock stk = new Stock();
                stk.productID = Convert.ToInt32(txt_product_ID.Text);
                stk.quantity = Convert.ToDecimal(txt_Quantity.Text);




                int count = returnRows(stk.productID);
                if (count >= 1)
                {
                    if (DataValid())
                    {
                        stk.UpdateProduct(stk);
                        DatabindtoProductGridView();
                    }
                }
                else
                {
                    stk.SaveProduct(stk);
                    DatabindtoProductGridView();
                }


            }
        }
        private int returnRows(int PID)
        {
            using (MySqlConnection con = new MySqlConnection(LoginUser.cs))
            {
                string sql = "SELECT COUNT(*) as cnt FROM `tbl_stock` WHERE tbl_stock.Product_ID = '" + PID + "';";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                con.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int Count = Convert.ToInt32(dr["cnt"]);
                return Count;
            }
        }
        private void Btn_Update_Click(object sender, EventArgs e)
        {
            if (DataValid())
            {
                Stock stk = new Stock();
                stk.productID = Convert.ToInt32(txt_product_ID.Text);
                stk.quantity = Convert.ToDecimal(txt_Quantity.Text);
                stk.UpdateProduct(stk);
                DatabindtoProductGridView();
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_product_ID.Text = "";
            txt_Quantity.Text = "";
            txt_product_ID.Focus();
        }
    }
}
