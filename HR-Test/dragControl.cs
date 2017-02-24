using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace HR_Test
{
    public class dragControl
    {
        const int Band = 5;
        const int MinWidth = 10;
        const int MinHeight = 10;
        private EnumMousePointPosition m_MousePointPosition;
        private Point p, p1;

        public enum EnumMousePointPosition
        {
            MouseSizeNone = 0, //'无
            MouseSizeRight = 1, //'拉伸右边框
            MouseSizeLeft = 2, //'拉伸左边框
            MouseSizeBottom = 3, //'拉伸下边框
            MouseSizeTop = 4, //'拉伸上边框
            MouseSizeTopLeft = 5, //'拉伸左上角
            MouseSizeTopRight = 6, //'拉伸右上角
            MouseSizeBottomLeft = 7, //'拉伸左下角
            MouseSizeBottomRight = 8, //'拉伸右下角
            MouseDrag = 9 // '鼠标拖动
        }

        //定义自己的MyMouseDown事件
        public void MyMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            p.X = e.X;
            p.Y = e.Y;
            p1.X = e.X;
            p1.Y = e.Y;  
           
        }

        //定义自己的MyMouseLeave事件
        public void MyMouseLeave(object sender, System.EventArgs e)
        {
            m_MousePointPosition = EnumMousePointPosition.MouseSizeNone;

            ((Control)sender).Cursor = Cursors.Arrow;
            ((Control)sender).BackColor = Color.White;
        }

        //设计一个函数，确定光标在控件不同位置的样式
        public EnumMousePointPosition MousePointPosition(Size size, System.Windows.Forms.MouseEventArgs e)
        {
            if ((e.X >= -1 * Band) | (e.X <= size.Width) | (e.Y >= -1 * Band) | (e.Y <= size.Height))
            {
                if (e.X < Band)
                {
                    if (e.Y < Band)
                    {
                        return EnumMousePointPosition.MouseSizeTopLeft;
                    }
                    else
                    {
                        if (e.Y > -1 * Band + size.Height)
                        {
                            return EnumMousePointPosition.MouseSizeBottomLeft;
                        }
                        else
                        {
                            return EnumMousePointPosition.MouseSizeLeft;
                        }
                    }
                }
                else
                {
                    if (e.X > -1 * Band + size.Width)
                    {
                        if (e.Y < Band)
                        {
                            return EnumMousePointPosition.MouseSizeTopRight;
                        }
                        else
                        {
                            if (e.Y > -1 * Band + size.Height)
                            {
                                return EnumMousePointPosition.MouseSizeBottomRight;
                            }
                            else
                            {
                                return EnumMousePointPosition.MouseSizeRight;
                            }
                        }
                    }
                    else
                    {
                        if (e.Y < Band)
                        {
                            return EnumMousePointPosition.MouseSizeTop;
                        }
                        else
                        {
                            if (e.Y > -1 * Band + size.Height)
                            {
                                return EnumMousePointPosition.MouseSizeBottom;
                            }
                            else
                            {
                                return EnumMousePointPosition.MouseDrag;
                            }
                        }
                    }
                }
            }
            else
            {
                return EnumMousePointPosition.MouseSizeNone;
            }
        }

        //定义自己的MyMouseMove事件，在这个事件里，会使用上面设计的函数
        public void MyMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Control lCtrl = (sender as Control);
            if (e.Button == MouseButtons.Left)
            {
                switch (m_MousePointPosition)
                {
                    case EnumMousePointPosition.MouseDrag:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Top = lCtrl.Top + e.Y - p.Y;
                        break;
                    case EnumMousePointPosition.MouseSizeBottom:
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点
                        break;
                    case EnumMousePointPosition.MouseSizeBottomRight:
                        lCtrl.Width = lCtrl.Width + e.X - p1.X;
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点
                        break;
                    case EnumMousePointPosition.MouseSizeRight:
                        lCtrl.Width = lCtrl.Width + e.X - p1.X;
                        // lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点
                        break;
                    case EnumMousePointPosition.MouseSizeTop:
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        break;
                    case EnumMousePointPosition.MouseSizeLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        break;
                    case EnumMousePointPosition.MouseSizeBottomLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        lCtrl.Height = lCtrl.Height + e.Y - p1.Y;
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点
                        break;
                    case EnumMousePointPosition.MouseSizeTopRight:
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Width = lCtrl.Width + (e.X - p1.X);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        p1.X = e.X;
                        p1.Y = e.Y; //'记录光标拖动的当前点
                        break;
                    case EnumMousePointPosition.MouseSizeTopLeft:
                        lCtrl.Left = lCtrl.Left + e.X - p.X;
                        lCtrl.Top = lCtrl.Top + (e.Y - p.Y);
                        lCtrl.Width = lCtrl.Width - (e.X - p.X);
                        lCtrl.Height = lCtrl.Height - (e.Y - p.Y);
                        break;
                    default:
                        break;
                }
                if (lCtrl.Width < MinWidth) lCtrl.Width = MinWidth;
                if (lCtrl.Height < MinHeight) lCtrl.Height = MinHeight;
            }
            else
            {
                m_MousePointPosition = MousePointPosition(lCtrl.Size, e); //'判断光标的位置状态
                switch (m_MousePointPosition) //'改变光标
                {
                    case EnumMousePointPosition.MouseSizeNone:
                        ((Control)sender).Cursor = Cursors.Arrow; //'箭头
                        break;
                    case EnumMousePointPosition.MouseDrag:
                        ((Control)sender).Cursor = Cursors.SizeAll; //'四方向
                        break;
                    case EnumMousePointPosition.MouseSizeBottom:
                        ((Control)sender).Cursor = Cursors.SizeNS; //'南北
                        break;
                    case EnumMousePointPosition.MouseSizeTop:
                        ((Control)sender).Cursor = Cursors.SizeNS; //'南北
                        break;
                    case EnumMousePointPosition.MouseSizeLeft:
                        ((Control)sender).Cursor = Cursors.SizeWE; //'东西
                        break;
                    case EnumMousePointPosition.MouseSizeRight:
                        ((Control)sender).Cursor = Cursors.SizeWE; //'东西
                        break;
                    case EnumMousePointPosition.MouseSizeBottomLeft:
                        ((Control)sender).Cursor = Cursors.SizeNESW; //'东北到南西
                        break;
                    case EnumMousePointPosition.MouseSizeBottomRight:
                        ((Control)sender).Cursor = Cursors.SizeNWSE; //'东南到西北
                        break;
                    case EnumMousePointPosition.MouseSizeTopLeft:
                        ((Control)sender).Cursor = Cursors.SizeNWSE; //'东南到西北
                        break;
                    case EnumMousePointPosition.MouseSizeTopRight:
                        ((Control)sender).Cursor = Cursors.SizeNESW; //'东北到南西
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
