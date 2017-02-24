using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TabStrips
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tabStrips_SelectedTabChanged(null, new Messir.Windows.Forms.SelectedTabChangedEventArgs(null));
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            
        }

        private void tabStrips_SelectedTabChanged(object sender, Messir.Windows.Forms.SelectedTabChangedEventArgs e)
        {
            lblSelected.Text = string.Format("First TabStrip: {0}\nSecond TabStrip: {1}\nThird TabStrip: {2}",
                (tabStrip1.SelectedTab == null) ? "(none)" : tabStrip1.SelectedTab.Text,
                (tabStrip2.SelectedTab == null) ? "(none)" : tabStrip2.SelectedTab.Text,
                (tabStrip3.SelectedTab == null) ? "(none)" : tabStrip3.SelectedTab.Text);


        }

    }
}