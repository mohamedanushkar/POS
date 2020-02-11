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
    public partial class INvoice_Retail : Form
    {
        private int invNo;
        public INvoice_Retail(int InvNo)
        {
            InitializeComponent();
            invNo = InvNo;
        }

        private void INvoice_Retail_Load(object sender, EventArgs e)
        {
            string path = ConfigurationManager.AppSettings.Get("Path");
            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(@path + "Report_Invoice.rpt");
            cryRpt.SetParameterValue(@"ID", invNo);
            crystalReportViewer1.ReportSource = cryRpt;
            crystalReportViewer1.Refresh();
        }
    }
}
