namespace ADPOS
{
    partial class UC_Stock
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DataGrid_Stock_Details = new System.Windows.Forms.DataGridView();
            this.DataGrid_Manage_Stock = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.Btn_Search = new System.Windows.Forms.Button();
            this.txt_product_barcode = new System.Windows.Forms.TextBox();
            this.txt_Quantity = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_Supplier_Name = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid_Stock_Details)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid_Manage_Stock)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGrid_Stock_Details
            // 
            this.DataGrid_Stock_Details.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid_Stock_Details.Location = new System.Drawing.Point(3, 202);
            this.DataGrid_Stock_Details.Name = "DataGrid_Stock_Details";
            this.DataGrid_Stock_Details.Size = new System.Drawing.Size(468, 333);
            this.DataGrid_Stock_Details.TabIndex = 1;
            this.DataGrid_Stock_Details.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrid_Stock_Details_CellContentClick);
            // 
            // DataGrid_Manage_Stock
            // 
            this.DataGrid_Manage_Stock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid_Manage_Stock.Location = new System.Drawing.Point(477, 202);
            this.DataGrid_Manage_Stock.Name = "DataGrid_Manage_Stock";
            this.DataGrid_Manage_Stock.Size = new System.Drawing.Size(469, 333);
            this.DataGrid_Manage_Stock.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmb_Supplier_Name);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btn_Clear);
            this.groupBox1.Controls.Add(this.Btn_Search);
            this.groupBox1.Controls.Add(this.txt_product_barcode);
            this.groupBox1.Controls.Add(this.txt_Quantity);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(940, 193);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Update Stock";
            // 
            // btn_Clear
            // 
            this.btn_Clear.Location = new System.Drawing.Point(628, 102);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(75, 23);
            this.btn_Clear.TabIndex = 7;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.UseVisualStyleBackColor = true;
            // 
            // Btn_Search
            // 
            this.Btn_Search.Location = new System.Drawing.Point(526, 102);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new System.Drawing.Size(75, 23);
            this.Btn_Search.TabIndex = 6;
            this.Btn_Search.Text = "Search";
            this.Btn_Search.UseVisualStyleBackColor = true;
            // 
            // txt_product_barcode
            // 
            this.txt_product_barcode.Location = new System.Drawing.Point(121, 68);
            this.txt_product_barcode.Name = "txt_product_barcode";
            this.txt_product_barcode.Size = new System.Drawing.Size(215, 20);
            this.txt_product_barcode.TabIndex = 5;
            // 
            // txt_Quantity
            // 
            this.txt_Quantity.Location = new System.Drawing.Point(530, 68);
            this.txt_Quantity.Name = "txt_Quantity";
            this.txt_Quantity.Size = new System.Drawing.Size(215, 20);
            this.txt_Quantity.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Product BarCode";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(468, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quantity";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Supplier Name";
            // 
            // cmb_Supplier_Name
            // 
            this.cmb_Supplier_Name.FormattingEnabled = true;
            this.cmb_Supplier_Name.Location = new System.Drawing.Point(121, 99);
            this.cmb_Supplier_Name.Name = "cmb_Supplier_Name";
            this.cmb_Supplier_Name.Size = new System.Drawing.Size(215, 21);
            this.cmb_Supplier_Name.TabIndex = 9;
            // 
            // UC_Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DataGrid_Manage_Stock);
            this.Controls.Add(this.DataGrid_Stock_Details);
            this.Name = "UC_Stock";
            this.Size = new System.Drawing.Size(949, 538);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid_Stock_Details)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid_Manage_Stock)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGrid_Stock_Details;
        private System.Windows.Forms.DataGridView DataGrid_Manage_Stock;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmb_Supplier_Name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Button Btn_Search;
        private System.Windows.Forms.TextBox txt_product_barcode;
        private System.Windows.Forms.TextBox txt_Quantity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
