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
    public partial class UC_Invoice : UserControl
    {
        public UC_Invoice()
        {
            InitializeComponent();

            DataBindToCombobox();
        }

        private void UC_Inventry_Load(object sender, EventArgs e)
        {

        }
        List<Inv_transactions> invlist = new List<Inv_transactions>();


        public class InvoiceAdd
        {
            public int Product_ID { get; set; }
            public int Quantity { get; set; }
            public int Price { get; set; }



            public void Update(InvoiceAdd prdt)
            {

                using (MySqlConnection con = new MySqlConnection(LoginUser.cs))
                {
                    string sql = "UPDATE `tbl_stock` SET `Quantity`= Quantity-'" + prdt.Quantity + "' WHERE `Product_ID`=" + prdt.Product_ID;
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.ExecuteNonQuery();

                }
            }

        }

        List<InvoiceAdd> Search = new List<InvoiceAdd>();

        public class inv_master_Retail
        {
            public DateTime InvDate { get; set; }

            public DateTime Time { get; set; }

            public int UserID { get; set; }
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
                string sql = "INSERT INTO `tbl_invoice_master_retail`( `Date`, `Time`, `User_ID`) VALUES ('" + inv.InvDate + "','" + inv.Time + "','" + inv.UserID + "')";
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
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                Inv_transactions invTr = new Inv_transactions();
                invTr.ProductID = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                invTr.Quantity = Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value);
                invTr.Total = Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value);
                invlist.Add(invTr);
            }

            int InvNo = SaveTransRec(invlist);
            return InvNo;
        }
        private int AutoIncreamentValue()
        {
            using (MySqlConnection con = new MySqlConnection(LoginUser.cs))
            {
                string sql = "SELECT MAX(Invoice_ID) as InvNo FROM `tbl_invoice_master_retail`";
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

                    String sql = "INSERT INTO `tbl_invoice_transactions_retail`(`Invoice_ID`, `Product_ID`, `Quantity`, `Total`) VALUES ('" + InvNo + "','" + inv.ProductID + "','" + inv.Quantity + "','" + inv.Total + "')";

                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    con.Open();
                    cmd.ExecuteNonQuery();


                }

            }
            return InvNo;

        }
       

       

      
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            LoginUser.IDGlobal = 2;

            inv_master_Retail invm = new inv_master_Retail();
            invm.InvDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            invm.Time = Convert.ToDateTime(DateTime.Now.ToShortTimeString());
            invm.UserID = LoginUser.IDGlobal;

            SaveMasterRec(invm);
            int InvNo = CreateInvTransList();

            MessageBox.Show("Invoice No:" + InvNo.ToString() + "Saved successfuly");
            invlist.Clear();
            dataGridView1.Rows.Clear();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (int.Parse(txtQuantity.Text) <= int.Parse(lblStockleft.Text))
            {
                InvoiceAdd inv = new InvoiceAdd();
                inv.Product_ID = Convert.ToInt32(cmbProductID.SelectedValue.ToString());
                inv.Quantity = Convert.ToInt32(txtQuantity.Text);
                inv.Update(inv);
                DataBindToCombobox();


                int row;
                dataGridView1.Rows.Add();
                row = dataGridView1.Rows.Count - 1;
                dataGridView1[0, row].Value = int.Parse(cmbProductID.SelectedValue.ToString());
                dataGridView1[1, row].Value = cmbProductID.Text;
                dataGridView1[2, row].Value = Convert.ToInt32(txtPrice.Text);
                dataGridView1[3, row].Value = Convert.ToDecimal(txtQuantity.Text);
                dataGridView1[4, row].Value = Convert.ToDecimal(txtTotal.Text);
            }
            else
            {
                MessageBox.Show("Stock Out Of Bounce");
            }
        }

        private void cmbProductID_SelectedIndexChanged(object sender, EventArgs e)
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

        private void txtQuantity_TextChanged_1(object sender, EventArgs e)
        {
            calculate();
        }
    }
}
