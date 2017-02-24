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
    public partial class frmBendOther2 : Form
    {
        Input.YBT5349_2006Bend _fi;
        public frmBendOther2(Input.YBT5349_2006Bend fi)
        {
            InitializeComponent();
            _fi = fi;
        }
         
        public double _B_Ds
        {
            get { return Convert.ToDouble(this.b_Ds.Text.Trim()); }
            set { this.b_Ds.Text = value.ToString(); }
        }

        public double _B_Da
        {
            get { return Convert.ToDouble(this.b_Da.Text.Trim()); }
            set { this.b_Da.Text = value.ToString(); }
        }

        public double _B_R
        {
            get { return Convert.ToDouble(this.b_R.Text.Trim()); }
            set { this.b_R.Text = value.ToString(); }
        } 
        public int _B_m
        {
            get { return Convert.ToInt32(this.b_m.Text.Trim()); }
            set { this.b_m.Text = value.ToString(); }
        }
         
        public double _B_a
        {
            get { return Convert.ToDouble(this.b_a.Text.Trim()); }
            set { this.b_a.Text = value.ToString(); }
        } 

        private void frmBendOther2_Load(object sender, EventArgs e)
        {
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.b_Da.Text = _fi._b_Da.ToString();
            this.b_Ds.Text = _fi._b_Ds.ToString();
            this.b_m.Text = _fi._b_m.ToString(); 
            this.b_R.Text = _fi._b_R.ToString();
            this.b_a.Text = _fi._b_a.ToString();
        }

        private void b_Ds_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (string.IsNullOrEmpty(t.Text) || !utils.IsNumeric(t.Text))
            {
                t.Text = "0";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _fi._b_Da= Convert.ToDouble( this.b_Da.Text);
            _fi._b_Ds = Convert.ToDouble(this.b_Ds.Text);
            _fi._b_m = Convert.ToInt32(this.b_m.Text); 
            _fi._b_R = Convert.ToDouble(this.b_R.Text);
            _fi._b_a = Convert.ToDouble(this.b_a.Text); 
        }
    }
}
