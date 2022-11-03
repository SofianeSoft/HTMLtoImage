using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTMLtoImage
{
    public partial class Form2 : Form
    {
        public String base64String { get; set; }
        public Form2()
        {
            InitializeComponent();
            ///this.reportViewer1.LocalReport.ReportPath = @"c:\users\pc\source\repos\htmltoimage\HTMLtoImage\Report1.rdlc";
        }
        public void showredlc()
        {
            this.ShowDialog();
       
            
        }
    
        private void Form2_Load(object sender, EventArgs e)
        {
            Microsoft.Reporting.WinForms.ReportParameter rpIMG1 = new Microsoft.Reporting.WinForms.ReportParameter("img", base64String);
            reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter[] { rpIMG1 });
            reportViewer1.RefreshReport();
            // this.reportViewer2.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
