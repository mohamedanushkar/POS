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
    public partial class UC_Inventory : UserControl
    {
        public static string cs = ConfigurationManager.ConnectionStrings["dbcon"].ToString(); public UC_Inventory()
        {
            InitializeComponent();
            DataBindToCombobox();
            DatabindtoGridView();
        }


        public class Product
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public decimal Unit_Price { get; set; }
            public int Prodict_Category { get; set; }
            public void SaveEmp(Product emp)
            {

                using (MySqlConnection con = new MySqlConnection(cs))
                {
                    string sql = "INSERT INTO `tbl_product`(`Product_ID`, `Name`, `Description`, `Price`, `Unit_Price`, `Product_Category_ID`)  values ('" + emp.ID + "','" + emp.Name + "','" + emp.Description + "','" + emp.Price + "','" + emp.Unit_Price + "','" + emp.Prodict_Category + "');";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Added");

                }
            }
            public void DeleteProduct(Product prdt)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this record ??","Confirm Delete!!",MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    using (MySqlConnection con = new MySqlConnection(cs))
                    {
                        string sql = "delete from tbl_product where Product_ID=" + prdt.ID;
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            public void UpdateProduct(Product prdt)
            {
                using (MySqlConnection con = new MySqlConnection(cs))
                {
                    string sql = "UPDATE `tbl_product` SET `Name`='"+prdt.Name+"',`Description`='"+prdt.Description+"',`Price`='"+prdt.Price+"',`Unit_Price`='"+prdt.Unit_Price+"' WHERE  `Product_ID`='"+prdt.ID+"'";

                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully");

                }

            }
        }
        private void DatabindtoGridView()
        {
            using (MySqlConnection con = new MySqlConnection(cs))
            {
                string sql = "SELECT * FROM tbl_product AS a INNER JOIN tbl_product_category AS b ON a.Product_Category_ID = b.Product_Category_ID";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataGrid_Inventory_view.DataSource = ds.Tables[0];
            }

            DataGrid_Inventory_view.Columns[0].HeaderText = "Product ID";
            DataGrid_Inventory_view.Columns[1].HeaderText = "Product Name";
            DataGrid_Inventory_view.Columns[4].HeaderText = "Unit Price";
            DataGrid_Inventory_view.Columns[5].Visible = false;
            DataGrid_Inventory_view.Columns[6].Visible = false;
            DataGrid_Inventory_view.Columns[7].Visible = false;
            DataGrid_Inventory_view.Columns[8].HeaderText = "Category Name";
        }

        public void newDetail()
        {
            txtDescription.Text = null;
            txtPrice.Text = null;
            txtProductID.Text = null;
            txtProductName.Text = null;
            txtUnitPrice.Text = null;
            cmdProductCategory.Enabled = true;
            txtProductID.Enabled = true;


        }

        private bool DataValid()
        {
            if (txtProductID.Text == "" && !Information.IsNumeric(txtProductID.Text))
            {
                MessageBox.Show("Enter Product ID in Numeric Value");
                return false;
            }

            if (txtProductName.Text == "")
            {
                MessageBox.Show("Enter Product Name");
                return false;
            }

            if (txtDescription.Text == "")
            {
                MessageBox.Show("Enter Product Description");
                return false;
            }
            if (txtUnitPrice.Text == "" && !Information.IsNumeric(txtUnitPrice.Text))
            {
                MessageBox.Show("Enter Unite Price");
                return false;
            }

            if (txtPrice.Text == "" && !Information.IsNumeric(txtPrice.Text))
            {
                MessageBox.Show("Enter Price");
                return false;
            }
            if (cmdProductCategory.Text == "")
            {
                MessageBox.Show("Enter Product Category");
                return false;
            }
            return true;
        }

        private void DataBindToCombobox()

        {

            using (MySqlConnection con = new MySqlConnection(cs))
            {

                string sql = "select * from tbl_product_category";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                cmdProductCategory.DisplayMember = "Category_Name";
                cmdProductCategory.ValueMember = "Product_Category_ID";
                cmdProductCategory.DataSource = ds.Tables[0];

            }


        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DataValid())
            {
                Product emp = new Product();
                emp.ID = Convert.ToInt32(txtProductID.Text);
                emp.Name = txtProductName.Text;
                emp.Description = txtDescription.Text;
                emp.Price = Convert.ToInt32(txtPrice.Text);
                emp.Unit_Price = Convert.ToInt32(txtUnitPrice.Text);
                emp.Prodict_Category = Convert.ToInt32(cmdProductCategory.SelectedValue.ToString());
                emp.SaveEmp(emp);
                DatabindtoGridView();
            }
        }

        private void DataGrid_Inventory_view_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DataGrid_Inventory_view.SelectedRows)
            {
                txtProductID.Text = row.Cells["Product_ID"].Value.ToString();
                txtProductName.Text = row.Cells["Name"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();
                txtPrice.Text = row.Cells["Price"].Value.ToString();
                txtUnitPrice.Text = row.Cells["Unit_Price"].Value.ToString();
                cmdProductCategory.Text = row.Cells["Category_Name"].Value.ToString();
                cmdProductCategory.Enabled = false;
                txtProductID.Enabled = false;
                    }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            newDetail();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Product emp = new Product();
            emp.ID = Convert.ToInt32(txtProductID.Text);
            emp.DeleteProduct(emp);
            DatabindtoGridView();

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (DataValid())

            {
                Product emp = new Product();
                emp.ID = Convert.ToInt32(txtProductID.Text);
                emp.Name = txtProductName.Text;
                emp.Description = txtDescription.Text;
                emp.Price = Convert.ToInt32(txtPrice.Text);
                emp.Unit_Price = Convert.ToInt32(txtUnitPrice.Text);
                emp.UpdateProduct(emp);
                DatabindtoGridView();


            }
        }
    }
}
