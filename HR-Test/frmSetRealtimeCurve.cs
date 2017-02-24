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
    public partial class frmSetRealtimeCurve : Form
    {
        public frmSetRealtimeCurve()
        {
            InitializeComponent();
            this.tsBtnSave.DialogResult = DialogResult.OK;
            this.btnExit.DialogResult = DialogResult.Cancel;
        }

        private void tsBtnSave_Click(object sender, EventArgs e)
        {
            RWconfig.SetAppSettings("Y1", this.tscbY1.SelectedIndex.ToString());
            RWconfig.SetAppSettings("Y2", this.tscbY2.SelectedIndex.ToString()); 
            RWconfig.SetAppSettings("X1", this.tscbX1.SelectedIndex.ToString());
            RWconfig.SetAppSettings("X2", this.tscbX2.SelectedIndex.ToString());
        }

        private void tscbY1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //0-空-   1负荷,kN  2应力,MPa 3变形,mm 4应变,mm 5位移,mm
            if ((tscbY1.SelectedIndex == tscbY2.SelectedIndex ) && tscbY1.SelectedIndex != 0)
            {
                tscbY1.SelectedIndex = 0;
                //RWconfig.SetAppSettings("Y1", this.tscbY1.SelectedIndex.ToString());
                MessageBox.Show(this.Parent,"Y1,Y2 不能选择同样的数据，请重新选择。","警告",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            if (tscbY1.SelectedIndex == 0)
            {
                tscbY2.Enabled = false;
                tscbY2.SelectedIndex = 0;
            }
            else
            {
                tscbY2.Enabled = true;
            }
        }

        private void tscbY2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //0-请选择-   1力,kN  2应力,MPa 3变形,mm 4应变,mm 5位移,mm
            if ((tscbY2.SelectedIndex == tscbY1.SelectedIndex ) && tscbY2.SelectedIndex != 0)
            {
                MessageBox.Show(this.Parent, "Y1,Y2 不能选择同样的数据，请重新选择。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tscbY2.SelectedIndex = 0; 
                //RWconfig.SetAppSettings("Y2", this.tscbY2.SelectedIndex.ToString());
                return;
            }
        } 

        private void tscbX1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscbX1.SelectedIndex == tscbX2.SelectedIndex && tscbX1.SelectedIndex != 0)
            {
                MessageBox.Show(this.Parent, "X1,X2 不能选择同样的数据，请重新选择。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tscbX1.SelectedIndex = 0;
                //RWconfig.SetAppSettings("X1", this.tscbX1.SelectedIndex.ToString());
                return;
            }

            if (tscbX1.SelectedIndex == 0)
            {
                tscbX2.Enabled = false;
                tscbX2.SelectedIndex = 0;
            }
            else
            {
                tscbX2.Enabled = true;
            }
        }

        private void tscbX2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscbX1.SelectedIndex == tscbX2.SelectedIndex && tscbX2.SelectedIndex != 0)
            {
                MessageBox.Show(this.Parent, "X1,X2 不能选择同样的数据，请重新选择。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tscbX2.SelectedIndex = 0; 
                //RWconfig.SetAppSettings("X2", this.tscbX2.SelectedIndex.ToString());
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmSetRealtimeCurve_Load(object sender, EventArgs e)
        {
            this.tscbX1.SelectedIndex = int.Parse(RWconfig.GetAppSettings("X1"));
            this.tscbX2.SelectedIndex = int.Parse(RWconfig.GetAppSettings("X2"));
            this.tscbY1.SelectedIndex = int.Parse(RWconfig.GetAppSettings("Y1"));
            this.tscbY2.SelectedIndex = int.Parse(RWconfig.GetAppSettings("Y2")); 
            //this.cmbYr.SelectedIndex = int.Parse(RWconfig.GetAppSettings("ShowY"));
            //this.cmbXr.SelectedIndex = int.Parse(RWconfig.GetAppSettings("ShowX"));
        } 
    }
}
