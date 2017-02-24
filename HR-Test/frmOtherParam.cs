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
    public partial class frmOtherParam : Form
    {
        public string _SendCompany
        {
            get { return this._sendCompany.Text; }
            set { this._sendCompany.Text = value; }
        }

        public string _Temperature
        {
            get { return this._temperature.Text; }
            set { this._temperature.Text = value; }
        }

        public string _SampleCharacter
        {
            get { return this._sampleCharacter.Text; }
            set { this._sampleCharacter.Text = value; }
        }

        public string _HotStatus
        {
            get { return this._hotStatus.Text; }
            set { this._hotStatus.Text = value; }
        }

        public string _Humidity
        {
            get { return this._humidity.Text; }
            set { this._humidity.Text = value; }
        }

        public string _Condition
        {
            get { return this._condition.Text; }
            set { this._condition.Text = value; }
        }

        public string _Controlmode
        {
            get { return this._controlmode.Text; }
            set { this._controlmode.Text = value; }
        } 

        public frmOtherParam()
        {
            InitializeComponent();
        }

      
        private void frmTensileOther1_Load(object sender, EventArgs e)
        {
           
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string strErr=string.Empty;
            if (this._sendCompany.Text.Trim().Length == 0)
            {
                strErr += "'送检单位'不能为空！\r\n\r\n";
            }
            if (this._temperature.Text.Trim().Length == 0)
            {
                strErr += "'试验温度'不能为空！\r\n\r\n";
            }
            if (this._sampleCharacter.Text.Trim().Length == 0)
            {
                strErr += "'试样标识'不能为空！\r\n\r\n";
            }
            if (this._condition.Text.Trim().Length == 0)
            {
                strErr += "'试验条件'不能为空！\r\n\r\n";
            }
            if (this._controlmode.Text.Trim().Length == 0)
            {
                strErr += "'控制方式'不能为空！\r\n\r\n";
            }

            if (!string.IsNullOrEmpty(strErr))
            { MessageBox.Show(strErr, "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning); }
        }
    }
}
