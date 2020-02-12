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
    public partial class UC_Purchase_Order : UserControl
    {

        public UC_Purchase_Order()
        {
            InitializeComponent();
            DataBindToSupplier();
            DataBindToCombobox();
        }
        List<Inv_transactions> invlist = new List<Inv_transactions>();

        public class InvoiceAdd
        {
            public int Quantity { get; set; }
            public int Price { get; set; }          



        }

        List<InvoiceAdd> Search = new List<InvoiceAdd>();

        public class inv_master_Retail
        {
            public DateTime InvDate { get; set; }

            public DateTime Time { get; set; }

            public int UserID { get; set; }

            public int Supplier_ID { get; set; }
        }

        private void calculate()
        {
            if (!Information.IsNumeric(txtQuantity.Text))
            {
                if (txtQuantity.Text.Length >= 1)
                {

                    txtQuantity.Text = txtQuantity.Text.Remove(txtQuantity.Text.Length - 1);
                }
            }
            else
            {
                txtTotal.Text = Convert.ToString(int.Parse(txtPrice.Text) * int.Parse(txtQuantity.Text));
            }
        }
        private void DataBindToCombobox()

        {

            using (MySqlConnection con = new MySqlConnection(LoginUser.cs))
            {

                string sql = "SELECT * FROM `tbl_product`";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                cmbProductID.DisplayMember = "Name";
                cmbProductID.ValueMember = "Product_ID";
                cmbProductID.DataSource = ds.Tables[0];

            }


        }
        private void DataBindToSupplier()

        {

            using (MySqlConnection con = new MySqlConnection(LoginUser.cs))
            {

                string sql = "SELECT * FROM `tbl_Supplier`";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                cmb_Supplier.DisplayMember = "Supplier_Name";
                cmb_Supplier.ValueMember = "Supplier_ID";
                cmb_Supplier.DataSource = ds.Tables[0];

            }


        }

        public void geProductDetails(string inv)
        {
            InvoiceAdd invadd = new InvoiceAdd();
            using (MySqlConnection con = new MySqlConnection(LoginUser.cs))
            {
                string sql = "SELECT * FROM `tbl_product` INNER JOIN tbl_stock ON tbl_stock.Product_ID =tbl_product.Product_ID WHERE tbl_product.Product_ID='" + Convert.ToInt32(inv) + "';";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                con.Open();
                MySqlDataReader dr = cmd.ExecuteReader();


                if (dr.Read())
                {

                    txtPrice.Text = Convert.ToString(dr["Price"]);
                    lblStockleft.Text = Convert.ToString(dr["Quantity"]);

                }
                else
                {
                    MessageBox.Show("fail");
                }



            }


        }

        public void SaveMasterRec(inv_master_Retail inv)
        {
            using (MySqlConnection con = new MySqlConnection(LoginUser.cs))
            {
                string sql = "INSERT INTO `tbl_purchase_order`(`User_ID`, `Supplier_ID`, `Date`) VALUES ('"+inv.UserID+"','"+inv.Supplier_ID+"','"+inv.InvDate+"')";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public class Inv_transactions
        {
            public int invoiceID { get; set; }
            public int ProductID { get; set; }
            public Decimal Quantity { get; set; }
            public Decimal Total { get; set; }
        }

        public int CreateInvTransList()
        {
            for (int i = 0; i < DataGrid_temp_Purchase_order.Rows.Count; ++i)
            {
                Inv_transactions invTr = new Inv_transactions();
                invTr.ProductID = Convert.ToInt32(DataGrid_temp_Purchase_order.Rows[i].Cells[0].Value);
                invTr.Quantity = Convert.ToDecimal(DataGrid_temp_Purchase_order.Rows[i].Cells[3].Value);
                invTr.Total = Convert.ToDecimal(DataGrid_temp_Purchase_order.Rows[i].Cells[4].Value);
                invlist.Add(invTr);
            }

            int InvNo = SaveTransRec(invlist);
            return InvNo;
        }
        private int AutoIncreamentValue()
        {
            using (MySqlConnection con = new MySqlConnection(LoginUser.cs))
            {
                string sql = "SELECT MAX(Purchase_Order_ID) as InvNo FROM `tbl_purchase_order`";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                con.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int InvNo = Convert.ToInt32(dr["InvNo"]);

                return InvNo;
            }
        }

        public int SaveTransRec(List<Inv_transactions> invlist)
        {
            int InvNo = AutoIncreamentValue();

            foreach (Inv_transactions inv in invlist)
            {
                using (MySqlConnection con = new MySqlConnection(LoginUser.cs))
                {

                    String sql = "INSERT INTO  `tbl_purchase_order_trans`(`Purchase_Order_ID`, `Product_ID`, `Quantity`, `Total`)  VALUES ('" + InvNo + "','" + inv.ProductID + "','" + inv.Quantity + "','" + inv.Total + "');";

                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();


                }

            }
            return InvNo;

        }







        
       

     

        private void btnPrint_Click(object sender, EventArgs e)
        {
            INvoice_Retail frm = new INvoice_Retail(Convert.ToInt32(txtID.Text));
            frm.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cmbProductID_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            geProductDetails(cmbProductID.SelectedValue.ToString());

            foreach (var item in Search)
            {
                txtPrice.Text = Convert.ToString(item.Price);
                lblStockleft.Text = Convert.ToString(item.Quantity);
            }
            calculate();
            Search.Clear();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            
            calculate();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtQuantity.Text) <= int.Parse(lblStockleft.Text))
            {

                DataBindToCombobox();


                int row;
                DataGrid_temp_Purchase_order.Rows.Add();
                row = DataGrid_temp_Purchase_order.Rows.Count - 1;
                DataGrid_temp_Purchase_order[0, row].Value = int.Parse(cmbProductID.SelectedValue.ToString());
                DataGrid_temp_Purchase_order[1, row].Value = cmbProductID.Text;
                DataGrid_temp_Purchase_order[2, row].Value = Convert.ToInt32(txtPrice.Text);
                DataGrid_temp_Purchase_order[3, row].Value = Convert.ToDecimal(txtQuantity.Text);
                DataGrid_temp_Purchase_order[4, row].Value = Convert.ToDecimal(txtTotal.Text);
            }
            else
            {
                MessageBox.Show("Stock Out Of Bounce");
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            LoginUser.IDGlobal = 2;

            inv_master_Retail invm = new inv_master_Retail();
            invm.InvDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            invm.Time = Convert.ToDateTime(DateTime.Now.ToShortTimeString());
            invm.UserID = LoginUser.IDGlobal;
            invm.Supplier_ID = Convert.ToInt32(cmb_Supplier.SelectedValue.ToString());
            

            SaveMasterRec(invm);
            int InvNo = CreateInvTransList();

            MessageBox.Show("Invoice No:" + InvNo.ToString() + "Saved successfuly");
            invlist.Clear();
            DataGrid_temp_Purchase_order.Rows.Clear();

        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            Report_Purchaisee_Order frm = new Report_Purchaisee_Order(Convert.ToInt32(txtID.Text));
            frm.Show();
        }
    }
}
