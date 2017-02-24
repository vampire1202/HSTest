using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace test
{
    public partial class UserControl1 : ToolStrip
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        protected override void OnMouseMove(MouseEventArgs mea)
        {
            base.OnMouseMove(mea);
        }
    }
}
