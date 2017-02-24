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
    public partial class frmTestMethod : Form
    {
  
        public frmTestMethod()
        {
            InitializeComponent();

            this.chkLbResult.Items.AddRange(new object[] {
            "             Fm               kN                 最大力",
            "             Rm               MPa                抗拉强度",
            "             ReH              MPa                上屈服强度",
            "             ReL              MPa                下屈服强度",
            "             Rp               MPa                规定塑性延伸强度",
            "             Rt               MPa                规定总延伸强度",
            "             Rr               MPa                规定残余延伸强度",
            "             E                GPa                弹性模量",
            "             m                MPa                应力-延伸率曲线在给定试验时刻的斜率",
            "             mΕ              MPa                应力-延伸率曲线弹性部分的斜率",
            "             A                %                  断后伸长率",
            "             At               %                  断裂总延伸率",
            "             Ae               %                  屈服点延伸率",
            "             Ag               %                  最大力Fm塑性延伸率",
            "             Agt              %                  最大力Fm总延伸率",
            "             Avm(Awn)         %                  无缩颈塑性伸长率",
            "             Z                %                  断面收缩率",
            "             S                %                  标准偏差",
            "             平均值           MPa                剔除最大值最小值后的平均值",                
            });
        }

        private void glassButton9_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected override void WndProc(ref   System.Windows.Forms.Message m)
        {
            base.WndProc(ref   m);//基类执行 
            if (m.Msg == 132)//鼠标的移动消息（包括非窗口的移动） 
            {
                //基类执行后m有了返回值,鼠标在窗口的每个地方的返回值都不同 
                if ((IntPtr)2 == m.Result)//如果返回值是2，则说明鼠标是在标题拦 
                {
                    //将返回值改为1(窗口的客户区)，这样系统就以为是 
                    //在客户区拖动的鼠标，窗体就不会移动 
                    m.Result = (IntPtr)1;
                }
            }
        } 


        //private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    Brush _textBrush;
        //    Brush _bgcolor;

        //    // Get the item from the collection.
        //    TabPage _tabPage = tabControl1.TabPages[e.Index];

        //    // Get the real bounds for the tab rectangle.
        //    Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

        //    if (e.State == DrawItemState.Selected)
        //    {
        //        // Draw a different background color, and don't paint a focus rectangle.
        //        _textBrush = new SolidBrush(Color.FromKnownColor(KnownColor.Black));
        //        _bgcolor = new SolidBrush(Color.FromKnownColor(KnownColor.WhiteSmoke));
        //        g.FillRectangle(_bgcolor, e.Bounds); 
        //    }
        //    else
        //    {
        //        _textBrush = new SolidBrush(Color.FromKnownColor(KnownColor.Black));
        //        _bgcolor = new SolidBrush(Color.FromKnownColor(KnownColor.MenuBar));
        //        g.FillRectangle(_bgcolor, e.Bounds); 
        //    }
        //    // Use our own font.
        //    Font _tabFont = new Font("宋体", (float)12.0, FontStyle.Bold, GraphicsUnit.Point);
        //    // Draw string. Center the text.
        //    StringFormat _stringFlags = new StringFormat();
        //    _stringFlags.Alignment = StringAlignment.Center;
        //    _stringFlags.LineAlignment = StringAlignment.Center;
        //    g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        //}

        //private void frmParameter_Load(object sender, EventArgs e)
        //{
        //    chkAllow_CheckedChanged(sender, e);
        //}

        private void chkAllow_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllow.Checked == true)
            {
                this.gbAllow.Enabled = true;
            }
            else
            {
                this.gbAllow.Enabled = false;
            }
        }

        private void cmbYZValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbYZValue.SelectedIndex)
            {
                case 0:
                    this.lblkN.Text = "kN";
                    this.lblkNs.Text = "kN/s";
                    this.txtYZValue.Enabled = true;
                    this.txtYZSpeed.Enabled = true;
                    break;
                case 1:
                    this.lblkN.Text = "mm";
                    this.lblkNs.Text = "mm/min";
                    this.txtYZValue.Enabled = true;
                    this.txtYZSpeed.Enabled = true;
                    break;
                default:
                    this.txtYZValue.Enabled = false;
                    this.txtYZSpeed.Enabled = false;
                    break;
            }
        }

        private void glassButton1_Click(object sender, EventArgs e)
        {
            gbtnTestControl1.BackColor = Color.Orange;
            gbtnTestControl2.BackColor = Color.Silver;
            this.pictureBox.Image = global::HR_Test.Properties.Resources.t1;
        }

        private void glassButton2_Click(object sender, EventArgs e)
        {
            gbtnTestControl1.BackColor = Color.Silver;
            gbtnTestControl2.BackColor = Color.Orange;
            this.pictureBox.Image = global::HR_Test.Properties.Resources.t2;
        }

        private void chkYs_CheckedChanged(object sender, EventArgs e)
        {
            if (chkYs.Checked == true)
            {
                this.gbYs.Enabled = true;
            }
            else
            {
                this.gbYs.Enabled = false;
            }
        }

        private void frmTestMethod_SizeChanged(object sender, EventArgs e)
        {
            this.pbTestTitle.Left = this.Width / 2 - this.pbTestTitle.Width / 2;
        }

        //private void gbAllow_Enter(object sender, EventArgs e)
        //{

        //}
    }
}
