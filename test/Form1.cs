using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Math.Pow(10.0, 3).ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HR_Test.Struc.ctrlcommand[] al= new HR_Test.Struc.ctrlcommand[12];
            al[9].m_CtrlSpeed = 200;


            List<HR_Test.Struc.ctrlcommand> lst = new List<HR_Test.Struc.ctrlcommand>();
            HR_Test.Struc.ctrlcommand c = new HR_Test.Struc.ctrlcommand();
            c.m_CtrlSpeed = 1;
            lst.Add(c);
            HR_Test.Struc.ctrlcommand d = new HR_Test.Struc.ctrlcommand();
            d.m_CtrlSpeed = 2;
            lst.Add(d);

            for (int i = 0; i < lst.Count; i++)
            {
                al[i] = lst[i];
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int comcount = 5;
            byte[] bufcommand = new byte[5];
            bufcommand[0] = Convert.ToByte(0x10 + comcount);

        }

        private void toolStripButton1_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void toolStripButton1_Paint(object sender, PaintEventArgs e)
        { 
        }

        private void toolStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            MessageBox.Show("move");
        }

        private void toolStrip1_Validated(object sender, EventArgs e)
        {
            MessageBox.Show("validate");
        }

        private void toolStripButton1_RightToLeftChanged(object sender, EventArgs e)
        {

        }

        private void toolStrip1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void userControl11_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
                    }
    }
}
