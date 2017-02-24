using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HR_Test.UC
{
    public partial class MethodItem : UserControl
    {
        public MethodItem()
        {
            InitializeComponent();
        }

        private void _cmbControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //0-位移控制
            //1-负荷控制
            //2-应力控制
            //3-ēLc控制
            //4-ēLe控制
            switch (_cmbControlType.SelectedIndex)
            {
                case 0: lblDw.Text = "mm/min";break;
                case 1: lblDw.Text = "kN/s"; break;
                case 2: lblDw.Text = "MPa/s"; break;
                case 3:case 4: lblDw.Text = "/s"; break;
            }
        }

        private void _cmbChangeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //位移
            //负荷
            //变形
            //应力
            //应变
            switch (_cmbChangeType.SelectedIndex)
            {
                case 0: lblDw2.Text = "mm"; break;
                case 1: lblDw2.Text = "kN"; break;
                case 2: lblDw2.Text = "mm"; break;
                case 3: lblDw2.Text = "MPa"; break;
                case 4: lblDw2.Text = "mm"; break;
            }
        }

        private void _txtControlSpeed_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (!utils.IsNumeric(tb.Text))
                tb.Text = "";
        }

        private void btnAddMthodItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
