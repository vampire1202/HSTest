﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HR_Test
{
    public partial class frmFailureMode : Form
    {
        public frmFailureMode()
        {
            InitializeComponent();
        }

        string _mode;
        public string _Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.cmbA.SelectedIndex = 3;
            this.cmbB.SelectedIndex = 0;
            this.cmbC.SelectedIndex = 1;
        }

        private void frmFailureMode_Load(object sender, EventArgs e)
        {
            CreateList();
        }
        List<failureMode> lstcharA;
        List<failureMode> lstcharB;
        List<failureMode> lstcharC;
        private void CreateList()
        {
            //失效形式
            lstcharA = new List<failureMode>();          
            lstcharA.Add(new failureMode("A","角铺层破坏"));
            lstcharA.Add(new failureMode("D", "边缘分层"));
            lstcharA.Add(new failureMode("G", "夹持破坏或加强片脱落"));
            lstcharA.Add(new failureMode("L", "横向"));
            lstcharA.Add(new failureMode("M(xyz)", "多模式"));
            lstcharA.Add(new failureMode("S", "纵向劈裂"));
            lstcharA.Add(new failureMode("X", "散丝"));
            lstcharA.Add(new failureMode("O", "其他")); 
            cmbA.Items.Clear();
            foreach(var item in lstcharA)
            {
                cmbA.Items.Add(item.Char + " - " + item.Info);
            }
           
            //失效区域
            lstcharB = new List<failureMode>();
            lstcharB.Add(new failureMode("I", "夹持/加强片内部"));
            lstcharB.Add(new failureMode("A", "夹持根部或加强片根部"));
            lstcharB.Add(new failureMode("W", "距离夹持/加强片小于1倍的宽度"));
            lstcharB.Add(new failureMode("G", "工作段"));
            lstcharB.Add(new failureMode("M", "多处"));
            this.cmbB.Items.Clear(); 
            foreach (var item in lstcharB)
            {
                cmbB.Items.Add(item.Char + " - " + item.Info);
            }

            //失效部位
            lstcharC = new List<failureMode>();
            lstcharC.Add(new failureMode("B", "上部"));
            lstcharC.Add(new failureMode("T", "下部"));
            lstcharC.Add(new failureMode("L", "左部"));
            lstcharC.Add(new failureMode("R", "右部"));
            lstcharC.Add(new failureMode("M", "中间"));
            this.cmbC.Items.Clear();
            foreach (var item in lstcharC)
            {
                cmbC.Items.Add(item.Char + " - " + item.Info);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.cmbA.SelectedIndex = 2;
            this.cmbB.SelectedIndex = 1;
            this.cmbC.SelectedIndex = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.cmbA.SelectedIndex = 3;
            this.cmbB.SelectedIndex = 1;
            this.cmbC.SelectedIndex = 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.cmbA.SelectedIndex = 1;
            this.cmbB.SelectedIndex = 3;
            this.cmbC.SelectedIndex = 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.cmbA.SelectedIndex = 3;
            this.cmbB.SelectedIndex = 3;
            this.cmbC.SelectedIndex = 4;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.cmbA.SelectedIndex = 5;
            this.cmbB.SelectedIndex = 3;
            this.cmbC.SelectedIndex = 4;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.cmbA.SelectedIndex = 0;
            this.cmbB.SelectedIndex = 3;
            this.cmbC.SelectedIndex = 4;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.cmbA.SelectedIndex = 6;
            this.cmbB.SelectedIndex = 3;
            this.cmbC.SelectedIndex = 4;
        }

        bool canClose = false;

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.cmbA.SelectedIndex == -1 || this.cmbB.SelectedIndex == -1 || this.cmbC.SelectedIndex == -1)
                canClose = false;
            else
            {
                canClose = true;
                _mode = this.lstcharA[this.cmbA.SelectedIndex].Char + this.lstcharB[this.cmbB.SelectedIndex].Char + this.lstcharC[this.cmbC.SelectedIndex].Char;
            }
        }

        private void frmFailureMode_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (canClose)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            canClose = true;
        }
    }

    class failureMode
    {
        string _char;
        public string Char
        {
            get { return _char; }
            set { _char = value; }
        }
        string _info;
        public string Info
        {
            get { return _info; }
            set { _info = value; }
        }
        public failureMode(string a,string b)
        {
             _char  = a   ;
             _info  = b  ;
        }
    }
}
