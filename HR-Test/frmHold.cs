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
    public partial class frmHold : Form
    {
        //所有阀关闭
        public delegate void delbtnOK_Click(object sender, EventArgs e);
        public event delbtnOK_Click delbtnOKClick;
         
        public frmHold()
        {
            InitializeComponent();
        }

        private void frmHold_Load(object sender, EventArgs e)
        {
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (delbtnOKClick != null)
                delbtnOKClick(sender, e);
            //ftr.SendPauseTest();
            //ftr.m_holdContinue = true;
            //ftr.m_holdPause = false;
            this.Hide();
        }
     
    }
}
