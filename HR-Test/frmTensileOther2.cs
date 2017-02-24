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
    public partial class frmTensileOther2 : Form
    {
        //frmInput _frmi;
        Input.GBT228_2010Tensile _frmi;
        public frmTensileOther2( Input.GBT228_2010Tensile  frmi)
        {
            InitializeComponent();
            _frmi = frmi;
        }

        private void frmTensileOther2_Load(object sender, EventArgs e)
        {
            this._Lt.Text = _frmi._Lt.ToString();
            this._L01.Text = _frmi._L01.ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            if (!utils.IsNumeric(this._Lt.Text))
            {
                strErr += "'Lt'不为数字！\r\n\r\n";
            }
            if (!utils.IsNumeric(this._L01.Text))
            {
                strErr += "'L01'不为数字！\r\n\r\n";
            }
            

            if (!string.IsNullOrEmpty(strErr))
            { MessageBox.Show(strErr, "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); }
        }
    }
}
