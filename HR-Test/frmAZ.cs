using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HR_Test
{
    public partial class frmAZ : Form
    {

        Process pcalc = null;
        public frmAZ()
        {
            InitializeComponent();          
        }

        /// <summary>
        /// 原始标距L0
        /// </summary>
        private double _l0;
        public double _L0
        {
            get { return _l0; }
            set { this._l0 = value; }
        }

        /// <summary>
        /// 原始横截面面积
        /// </summary>
        private double _s0;
        public double _S0
        {
            get { return _s0; }
            set { _s0 = value; }
        }

        /// <summary>
        /// 断后伸长率
        /// </summary>
        private double _a=0;
        public double _A
        {
            get { return _a; }
            set { this._a = value; }
        }

        /// <summary>
        /// 断后标距
        /// </summary>
        private double _lu = 0;
        public double _Lu
        {
            get { return _lu; }
            set { _lu = value; }
        }

        /// <summary>
        /// 断面收缩率
        /// </summary>
        private double _z=0;
        public double _Z
        {
            get { return _z; }
            set { this._z = value; }
        }

        private void frmAZ_Load(object sender, EventArgs e)
        {
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.radioButton1.Checked = true;
            this.radioButton2.Checked = false;
            this.radioButton3.Checked = false;
            this.textBox3.Enabled = false;
            this.btnCa.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //if (DialogResult.OK == MessageBox.Show("确认结果:A=" + this._a.ToString() + ",Z=" + this._z.ToString(),"提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Information))
            //{

            //}
            //else
            //{
            //    return;
            //}
      
        }

        private void txtL0_TextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(txtL0.Text))
                txtL0.Text = "";
            if (txtL0.Text.Length > 0 && !string.IsNullOrEmpty(this.txtL0.Text))
            {
                this._a = Math.Round(((Convert.ToDouble(this.txtL0.Text.Trim())-this._l0) / this._l0) * 100, 2);
                this.lblA.Text = this._a.ToString() + " %";
                this._lu = Convert.ToDouble(this.txtL0.Text.Trim());
            }
            else
            {
                this._a = 0;
                this.lblA.Text = this._a.ToString() + " %";
            }
        }

        private void txtS0_TextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(txtS0.Text))
                txtS0.Text = "";
            if (txtS0.Text.Length > 0 && !string.IsNullOrEmpty(txtS0.Text))
            {
               this._z =  Math.Round(((this._s0 - Convert.ToDouble(this.txtS0.Text.Trim())) / this._s0) * 100, 2);
               this.lblZ.Text = this._z.ToString() + " %";
            }
            else
            {
                this._z = 0;
                this.lblZ.Text = this._z.ToString() + " %";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this._a = 0;
            this._z = 0;
        }

        private void btnCa_Click(object sender, EventArgs e)
        {
            pcalc = Process.Start("calc.exe");
        }

        //获取文本框的结果         

        //[DllImport("user32.dll", EntryPoint = "FindWindow")]
        //public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        //[DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        //public static extern IntPtr FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpsz1, string lpsz2);
        //[DllImport("User32 ")]
        //public static extern bool SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);
        //public const int WM_GETTEXT = 0xD;
        //[DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        //public static extern int SetForegroundWindow(IntPtr hwnd);


        //private void btnGet_Click(object sender, EventArgs e)
        //{
        //    if (pcalc == null || pcalc.HasExited) return;
        //    string result = "";//最终获得计算器的结果         
        //    if (Environment.OSVersion.Version.Major >= 6)//win7 or vista            
        //    {                 //vista or win7发送ctrl+c获取返回值             
        //        SetForegroundWindow(pcalc.MainWindowHandle);
        //        SendKeys.SendWait("^(c)");
        //        result = Clipboard.GetText();
        //    }
        //    else
        //    {
        //        //xp或更早版本寻找对应控件获得返回值      
        //        IntPtr hEdit = FindWindowEx(pcalc.MainWindowHandle, IntPtr.Zero, "#32770", null);
        //        string w = " ";
        //        IntPtr ptr = Marshal.StringToHGlobalAnsi(w);
        //        if (SendMessage(hEdit, WM_GETTEXT, 100, ptr))
        //        {
        //            result = Marshal.PtrToStringAnsi(ptr);
        //        }
        //    }
        //    this.txtS0.Text = result;
        //    //MessageBox.Show(result);
        //}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (!utils.IsNumeric(tb.Text))
            {
                tb.Text = "";
                this.txtS0.Text = "";
            }

            if (radioButton1.Checked)
            {
                if (this.textBox1.Text.Length > 0 && !string.IsNullOrEmpty(textBox1.Text))
                {
                    if (this.textBox2.Text.Length > 0 && !string.IsNullOrEmpty(textBox2.Text))
                    {
                        double a = Convert.ToDouble(this.textBox1.Text.Trim());
                        double b = Convert.ToDouble(this.textBox2.Text.Trim());
                        this.txtS0.Text = Math.Round(a * b, 4).ToString();
                    }
                }
            }

            if (radioButton2.Checked)
            {
                if (this.textBox3.Text.Length > 0 && !string.IsNullOrEmpty(textBox3.Text))
                {
                    double d = Convert.ToDouble(this.textBox3.Text.Trim());
                    this.txtS0.Text = Math.Round(Math.PI * d * d / 4.0, 4).ToString();
                }
            }

            if (radioButton3.Checked)
            {
                this.btnCa.Enabled = true;
            }
            else
            {
                this.btnCa.Enabled = false;
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.txtS0.Text = "";
            if (radioButton1.Checked)
            {
                this.textBox1.Enabled = true;
                this.textBox2.Enabled = true;                
            }
            else
            {
                this.textBox1.Enabled = false;
                this.textBox1.Text = "";
                this.textBox2.Enabled = false;
                this.textBox2.Text = "";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.txtS0.Text = "";
            if (radioButton2.Checked)
            {
                this.textBox3.Enabled = true;
            }
            else
            {
                this.textBox3.Enabled = false;
                this.textBox3.Text = "";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.txtS0.Text = "";
            if (radioButton3.Checked)
            {
                this.btnCa.Enabled = true;
            }
            else
            {
                this.btnCa.Enabled = false;
                this.textBox3.Text = "";
            }
        }

        private void frmAZ_LocationChanged(object sender, EventArgs e)
        {

        }
    }
}
