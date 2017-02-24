using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HR_Test
{
    public partial class frmSetResultCurve : Form
    {
        public frmSetResultCurve()
        {
            InitializeComponent();
            this.btnSave.DialogResult = DialogResult.OK;
            this.btnExit.DialogResult = DialogResult.Cancel;
        }

        private void frmSetResultCurve_Load(object sender, EventArgs e)
        {
            //this.tscbX1.SelectedIndex = int.Parse(RWconfig.GetAppSettings("X1"));
            //this.tscbX2.SelectedIndex = int.Parse(RWconfig.GetAppSettings("X2"));
            //this.tscbY1.SelectedIndex = int.Parse(RWconfig.GetAppSettings("Y1"));
            //this.tscbY2.SelectedIndex = int.Parse(RWconfig.GetAppSettings("Y2"));
            //this.tscbY3.SelectedIndex = int.Parse(RWconfig.GetAppSettings("Y3"));
            this.cmbYr.SelectedIndex = int.Parse(RWconfig.GetAppSettings("ShowY"));
            this.cmbXr.SelectedIndex = int.Parse(RWconfig.GetAppSettings("ShowX"));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            RWconfig.SetAppSettings("ShowY", this.cmbYr.SelectedIndex.ToString());
            RWconfig.SetAppSettings("ShowX", this.cmbXr.SelectedIndex.ToString());
        }
    }
}
