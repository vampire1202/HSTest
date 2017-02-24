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
    public partial class ReSizePanel : UserControl
    {
        public ReSizePanel()
        {
            InitializeComponent();
        }
        bool allowSize = false;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBoxR_MouseDown(object sender, MouseEventArgs e)
        {
            allowSize = true;
        }

        private void pictureBoxR_MouseUp(object sender, MouseEventArgs e)
        {
            allowSize = false;
        }

        private void pictureBoxR_MouseMove(object sender, MouseEventArgs e)
        {
            if (allowSize)
            {
                this.Height = pictureBoxR.Top + e.Y;
                this.Width = pictureBoxR.Left + e.X;
            }
        }

        private void pictureBoxL_MouseDown(object sender, MouseEventArgs e)
        {
            allowSize = true;
        }

        private void pictureBoxL_MouseUp(object sender, MouseEventArgs e)
        {
            allowSize = false;
        }

        private void pictureBoxL_MouseMove(object sender, MouseEventArgs e)
        {
            if (allowSize)
            {
                this.Height = this.Height - e.Y;
                this.Width = this.Width - e.X;
                this.Left = this.Left + e.X;
                this.Top = this.Top + e.Y;
            }
        }

        private void ReSizePanel_MouseDown(object sender, MouseEventArgs e)
        {
            allowSize = true;
            this.Cursor = Cursors.Hand;
        }

        private void ReSizePanel_MouseUp(object sender, MouseEventArgs e)
        {
            allowSize = false;
            this.Cursor = Cursors.Arrow;
        }

        private void ReSizePanel_MouseMove(object sender, MouseEventArgs e)
        {
 
            if (allowSize)
            { 
                this.Top = this.Top;
                this.Left = this.Left;
            }
        }
    }
}
