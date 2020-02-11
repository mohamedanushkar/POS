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
using CrystalDecisions.CrystalReports.Engine;

namespace ADPOS
{
    public partial class UC_Report_Invoice : UserControl
    {
        private int invNo;
        public UC_Report_Invoice(int InvNo)
        {
            invNo = InvNo;
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void UC_Report_Invoice_Load(object sender, EventArgs e)
        {
            string path = ConfigurationManager.AppSettings.Get("Path");
            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(@path + "CrystalReport1.rpt");
            cryRpt.SetParameterValue(@"InvNo", invNo);
            crystalReportViewer1.ReportSource = cryRpt;
            crystalReportViewer1.Refresh();
        }
    }
}
