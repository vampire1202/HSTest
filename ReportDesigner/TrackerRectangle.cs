/*
This library is free software; you can redistribute it and/or modify it under the terms of the GNU Lesser General
Public License as published by the Free Software Foundation; either version 2.1 of the License, or (at your option)
any later version.

This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with this library; if not, write to
the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA 
*/

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using daReport;


namespace daReportDesigner
{
	
	public class TrackerRectangle
	{
		private Rectangle rc;
		private ICustomPaint.HorizontalAlignmentTypes pageAlignment;
		private ICustomPaint.VerticalAlignmentTypes pageVerticalAlignment;

		private Rectangle m_TopLeft;
		private Rectangle m_TopCenter;
		private Rectangle m_TopRight;
		private Rectangle m_CenterLeft;
		private Rectangle m_CenterRight;
		private Rectangle m_BottomLeft;
		private Rectangle m_BottomCenter;
		private Rectangle m_BottomRight;
		public static int m_nOffset = 4;

		public TrackerRectangle(Rectangle theRect,ICustomPaint.HorizontalAlignmentTypes HorizontalAlignment,ICustomPaint.VerticalAlignmentTypes VerticalAlignment)
		{
			rc = theRect;
			pageAlignment = HorizontalAlignment;
			pageVerticalAlignment = VerticalAlignment;
		}
	
		
		public void DrawSelectionGhostRect(Rectangle rc, Bitmap bm)
		{
			Graphics g = Graphics.FromImage(bm);
			ControlPaint.DrawFocusRectangle(g, rc, Color.Black, Color.Transparent);
			
			// Disposing resources
			g.Dispose();
		}

		public void InitTrackerRects()
		{
			Size sz = new Size(m_nOffset, m_nOffset);
			
			m_TopLeft = new Rectangle(new Point(rc.X - m_nOffset, rc.Y - m_nOffset), sz);

			if (pageVerticalAlignment != ICustomPaint.VerticalAlignmentTypes.Top)
				m_TopCenter = new Rectangle(new Point(rc.X + (rc.Width / 2) - (m_nOffset / 2), rc.Y - m_nOffset), sz);
			else
				m_TopRight = Rectangle.Empty;
			
			m_TopRight = new Rectangle(new Point(rc.X + rc.Width , rc.Y - m_nOffset), sz);

			if (pageAlignment != ICustomPaint.HorizontalAlignmentTypes.Left)
				m_CenterLeft = new Rectangle(new Point(rc.X - m_nOffset, rc.Y + (rc.Height / 2) - (m_nOffset / 2)), sz);
			else
				m_CenterLeft = Rectangle.Empty;
			
			if (pageAlignment != ICustomPaint.HorizontalAlignmentTypes.Right)
				m_CenterRight = new Rectangle(new Point(rc.X + rc.Width,  rc.Y + (rc.Height / 2) - (m_nOffset / 2)), sz);
			else
				m_CenterRight = Rectangle.Empty;

			
			
			m_BottomLeft = new Rectangle(new Point(rc.X - m_nOffset, rc.Y + rc.Height), sz);

			if (pageVerticalAlignment != ICustomPaint.VerticalAlignmentTypes.Bottom)
				m_BottomCenter = new Rectangle(new Point(rc.X + (rc.Width / 2) - (m_nOffset / 2), rc.Y + rc.Height), sz);
			else
				m_BottomCenter = Rectangle.Empty;
			
			m_BottomRight = new Rectangle(new Point(rc.X + rc.Width, rc.Y + rc.Height), sz);
		}

		public void DrawSelectionTrackers(Bitmap bm)
		{
			Brush b = new SolidBrush(Color.Black);
			InitTrackerRects();
			
			Graphics g = Graphics.FromImage(bm);
			System.Drawing.Drawing2D.HatchBrush aHatchBrush = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal, Color.Red, Color.White);
			
			g.DrawRectangle(new Pen(aHatchBrush,m_nOffset),rc.X-m_nOffset/2,rc.Y - m_nOffset/2,rc.Width+m_nOffset,rc.Height+m_nOffset);
			g.FillRectangle(b, m_TopLeft);
			g.FillRectangle(b, m_TopCenter);
			g.FillRectangle(b, m_TopRight);
			g.FillRectangle(b, m_CenterLeft);
			g.FillRectangle(b, m_CenterRight);
			g.FillRectangle(b, m_BottomLeft);
			g.FillRectangle(b, m_BottomCenter);
			g.FillRectangle(b, m_BottomRight);

			// Disposing resources
			b.Dispose();
			g.Dispose();
		}

		public Cursor CursorCheck(Point pt, ref int enResult)
		{
			if(rc.Contains(pt))
				return Cursors.SizeAll;

			InitTrackerRects();

			if(m_TopCenter.Contains(pt))
			{
				enResult = 1;
				return Cursors.SizeNS;
			}
			if(m_CenterRight.Contains(pt))
			{
				enResult = 2;
				return Cursors.SizeWE;
			}
			else if(m_BottomCenter.Contains(pt))
			{
				enResult = 3;
				return Cursors.SizeNS;
			}
			else if(m_CenterLeft.Contains(pt))
			{
				enResult = 4;
				return Cursors.SizeWE;
			}
			else
				return Cursors.Default;
		}
		
	}
}
