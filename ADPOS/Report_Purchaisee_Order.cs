using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;

namespace ADPOS
{
    public partial class Report_Purchaisee_Order : Form
    {
        private int invNo;
        public Report_Purchaisee_Order(int InvNo)
        {
            InitializeComponent(); 
            invNo = InvNo;
        }

        private void Report_Purchaisee_Order_Load(object sender, EventArgs e)
        {
            string path = ConfigurationManager.AppSettings.Get("Path");
            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(@path + "Report_Purchaise_Order.rpt");
            cryRpt.SetParameterValue(@"ID", invNo);
            crystalReportViewer1.ReportSource = cryRpt;
            crystalReportViewer1.Refresh();
        }
    }
}
