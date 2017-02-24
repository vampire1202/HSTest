using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace HR_Test
{
    class toolStripButton : ToolStripButton
    {
        private PictureBox _pb;
        public PictureBox _Pb
        {
            get { return _pb; }
            set { _pb = value; }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e); 
            _pb.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e); 
            _pb.Invalidate();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e); 
        }
      
    }
}
