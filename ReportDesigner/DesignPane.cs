


using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using daReport;


namespace daReportDesigner
{
	/// <summary>
	/// Summary description for DesignPane.
	/// 
	/// </summary>
	/// 

	public delegate void SelectionChangedHandler(ICustomPaint obj,int action);
	public delegate void PaintObjectHandler(ICustomPaint obj);
	public class DesignPane : System.Windows.Forms.UserControl
	{
		
		private System.ComponentModel.Container components = null;
		
		ICustomPaint[] staticObjects;
		ICustomPaint[] dynamicObjects;
		PageSettings pageSettings;

		public event SelectionChangedHandler OnSelectionChanged;
		public event PaintObjectHandler OnMoving;
		public event PaintObjectHandler OnMoveFinished;
		private bool shouldRepaint = false;
		private bool mouseDown = false;	
		private bool mControlPressed = false;
		public enum RESIZE_BORDER { NONE = 0, TOP, RIGHT, BOTTOM, LEFT };
		private RESIZE_BORDER mBorder = 0;
		public enum MOUSE_MODE { NONE = 0, SELECTED };
		private MOUSE_MODE mMouseMode = 0;

		Image drawingImage;
		Image trackerImage;
		
		// obo je novo 22.07.2005
		ICustomPaint selectedObject;
		ArrayList selectedObjects = new ArrayList();
		ICustomPaint moveReferenceObject;

		TrackerRectangle selectionRectangle;
		private Point ptStart = new Point(0,0);
		private Point initialPosition = new Point(0,0);

		private Brush brushBg;
		private bool showGrid = false;
		private int gridSize = 8;
		private SolidBrush gridBrush = new SolidBrush(Color.Gainsboro);
		private Bitmap backImage;


		public DesignPane()
		{
			

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			drawingImage = new Bitmap(this.ClientRectangle.Width,this.ClientRectangle.Height);
			CreateBackImage();
			trackerImage = (Bitmap)drawingImage.Clone();
		}



		public void SetSize(PageSettings settings)
		{
			this.Size = new System.Drawing.Size(settings.Bounds.Width, settings.Bounds.Height);
			drawingImage = new Bitmap(this.ClientRectangle.Width,this.ClientRectangle.Height);
			trackerImage = (Bitmap)drawingImage.Clone();
		}


		public void SetObjects(ICustomPaint[] staticElements,ICustomPaint[] dynamicElements,PageSettings thePageSettings)
		{
			staticObjects = staticElements;
			dynamicObjects = dynamicElements;

			pageSettings = thePageSettings;
			
			this.Invalidate();
			
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// DesignPane
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Name = "DesignPane";
			this.Size = new System.Drawing.Size(428, 436);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DesignPane_MouseUp);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.DesignPane_Paint);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DesignPane_KeyUp);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DesignPane_KeyDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DesignPane_MouseMove);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DesignPane_MouseDown);

		}
		#endregion

		private void DoPaint()
		{
			drawingImage.Dispose();
			drawingImage = new Bitmap(this.ClientRectangle.Width,this.ClientRectangle.Height);

			Graphics g = Graphics.FromImage(drawingImage);
			
			if (showGrid && gridSize!=0)
			{
				brushBg = new TextureBrush(backImage,WrapMode.Tile);
				g.FillRectangle(brushBg, this.ClientRectangle);
			}
			else
			{
				brushBg = new SolidBrush(this.BackColor);
				g.FillRectangle(brushBg, this.ClientRectangle);
			}
			
			PaintMargins(g);

			PaintStaticObjects(g);
			PaintDynamicObjects(g);

			brushBg.Dispose();
			g.Dispose();

			PaintImage(drawingImage);
			PaintSelection(g);
		}


		private void CreateBackImage()
		{
			backImage = new Bitmap(gridSize,gridSize);
			Graphics g = Graphics.FromImage(backImage);
			g.FillRectangle(new SolidBrush(this.BackColor),0,0,gridSize,gridSize);
			g.FillEllipse(gridBrush,0,0,2,2);
			g.FillEllipse(gridBrush,0,gridSize,2,2);
			g.FillEllipse(gridBrush,gridSize,0,2,2);
			g.FillEllipse(gridBrush,gridSize,gridSize,2,2);
		}
		

		private void PaintMargins(Graphics g)
		{
			if (pageSettings != null)
			{
				Rectangle marginRectangle = new Rectangle(pageSettings.Margins.Left,pageSettings.Margins.Top,this.ClientRectangle.Width-pageSettings.Margins.Right-pageSettings.Margins.Left,this.ClientRectangle.Height-pageSettings.Margins.Bottom-pageSettings.Margins.Top);

				g.DrawRectangle(new Pen(Color.LightGray,1),marginRectangle);
				g.DrawString("margins (not printable)",new Font("Tahoma",8),new SolidBrush(Color.Silver),marginRectangle.X+1,marginRectangle.Y);
			}
		}

		private void PaintStaticObjects(Graphics g)
		{
			if (staticObjects == null) return;

			for (int i=0;i<staticObjects.Length;i++)
			{
				staticObjects[i].Paint(g);
			}
		}

		private void PaintDynamicObjects(Graphics g)
		{
			if (dynamicObjects == null) return;

			for (int i=0;i<dynamicObjects.Length;i++)
			{
				dynamicObjects[i].Paint(g);
			}
		}

		private void PaintSelection(Graphics g)
		{
			if (selectionRectangle == null) return;
						
			trackerImage.Dispose();
			trackerImage = new Bitmap(this.ClientRectangle.Width,this.ClientRectangle.Height);

			foreach (ICustomPaint CurrentObject in this.selectedObjects)
			{
				ICustomPaint obj = (ICustomPaint)CurrentObject;
				selectionRectangle = new TrackerRectangle(obj.GetRegion(),obj.HorizontalAlignment,obj.VerticalAlignment);
				selectionRectangle.DrawSelectionTrackers((Bitmap)trackerImage);
			}
			
			PaintImage(trackerImage);
		}

		private void PaintImage(Image i)
		{	
			Graphics g = this.CreateGraphics();
			g.DrawImage(i, this.ClientRectangle);
			g.Dispose();
		}

		private void DesignPane_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			DoPaint();
		}

		public void SelectObject(ICustomPaint theSelection)
		{
			if (theSelection != null)
			{
				selectedObject = theSelection;
				selectionRectangle = new TrackerRectangle(theSelection.GetRegion(),theSelection.HorizontalAlignment, theSelection.VerticalAlignment);

				trackerImage = (Bitmap)drawingImage.Clone();
				selectionRectangle.DrawSelectionTrackers((Bitmap)trackerImage);
				PaintImage(trackerImage);
			}
			else
			{
				selectedObject = null;
				selectionRectangle = null;
				PaintImage(drawingImage);
			}
			
		}


		public void UpdateSelection(ICustomPaint theSelection,int action)
		{
			if (theSelection != null)
			{
				if (action==0)
				{
					selectionRectangle = new TrackerRectangle(theSelection.GetRegion(),theSelection.HorizontalAlignment, theSelection.VerticalAlignment);

					trackerImage = (Bitmap)drawingImage.Clone();
					selectionRectangle.DrawSelectionTrackers((Bitmap)trackerImage);
					PaintImage(trackerImage);
					selectedObjects.Clear();
					selectedObjects.Add(theSelection);

				}
				else if (action==1)
				{
					selectionRectangle = new TrackerRectangle(theSelection.GetRegion(),theSelection.HorizontalAlignment, theSelection.VerticalAlignment);

					selectionRectangle.DrawSelectionTrackers((Bitmap)trackerImage);
					PaintImage(trackerImage);
					selectedObjects.Add(theSelection);
				}
				else if (action==2)
				{
					selectedObjects.Remove(theSelection);
					DoPaint();
				}
			}
			else
			{
				selectedObjects.Clear();
				selectionRectangle = null;
				trackerImage = (Bitmap)drawingImage.Clone();
				PaintImage(drawingImage);
			}
		}

		private ICustomPaint FindObjectAt(int x,int y)
		{

			foreach (ICustomPaint CurrentObject in this.selectedObjects)
			{
				if (CurrentObject.GetRegion().Contains(x,y))
					return CurrentObject;
			}

			for (int i=staticObjects.Length;i>0;i--)
			{
				if ( staticObjects[i-1].GetRegion().Contains(x,y) && staticObjects[i-1].Selectable)
					return staticObjects[i-1];
			}

			for (int i=dynamicObjects.Length;i>0;i--)
			{
				if ( dynamicObjects[i-1].GetRegion().Contains(x,y) && dynamicObjects[i-1].Selectable)
					return dynamicObjects[i-1];
			}
	
			return null;
		}



		private void DesignPane_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{			
			mouseDown = true;

			if ( this.Cursor == Cursors.SizeNS || this.Cursor == Cursors.SizeWE )
			{
				mMouseMode = MOUSE_MODE.SELECTED;
				return;
			}

			ptStart = new Point(e.X, e.Y);
			initialPosition = new Point(e.X, e.Y);

			if ( this.Cursor == Cursors.SizeAll )
			{
				mMouseMode = MOUSE_MODE.SELECTED;
			}

		}

		private void DesignPane_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			mouseDown = false;
			mMouseMode = MOUSE_MODE.NONE;

			if ( e.X==initialPosition.X && e.Y==initialPosition.Y )
			{
				object t = FindObjectAt(initialPosition.X,initialPosition.Y);
			

				if (t!=null)
				{
					if (this.mControlPressed)
					{
						if (this.selectedObjects.Contains(t))
						{
							if(OnSelectionChanged!=null)
								OnSelectionChanged((ICustomPaint)t,2);
						}
						else
						{
							if(OnSelectionChanged!=null)
								OnSelectionChanged((ICustomPaint)t,1);
						}
					}
					else
					{
						if(OnSelectionChanged!=null)
							OnSelectionChanged((ICustomPaint)t,0);
					}
				}
				else
				{
					if(OnSelectionChanged!=null)
						OnSelectionChanged(null,0);
				}
			}

			if(shouldRepaint)
			{
				shouldRepaint = false;
				DoPaint();

				if(OnMoveFinished!=null && selectedObjects.Count>0)
				{
					OnMoveFinished((ICustomPaint)selectedObjects[selectedObjects.Count-1]);
				}
			}
		}

		public void DesignPane_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			
			if (mouseDown)
			{
				if (mMouseMode == MOUSE_MODE.SELECTED)
				{
					if (mBorder != RESIZE_BORDER.NONE)
					{
						
						int xOffset = 0;
						int yOffset = 0;
						CalculateStretchOffsets(e.X,e.Y,ref xOffset,ref yOffset,mBorder);

						trackerImage.Dispose();
						trackerImage = (Bitmap)drawingImage.Clone();

						for (int i=0;i<selectedObjects.Count;i++)
						{
							ICustomPaint theObject = (ICustomPaint)selectedObjects[i];

							switch(mBorder)
							{
								case RESIZE_BORDER.TOP:
									if (theObject.Height - yOffset >=0)
									{
										if (theObject.VerticalAlignment == ICustomPaint.VerticalAlignmentTypes.None)
										{
											theObject.Height -= yOffset;
											theObject.Y += yOffset;
										}
										else if (theObject.VerticalAlignment == ICustomPaint.VerticalAlignmentTypes.Bottom || selectedObject.VerticalAlignment == ICustomPaint.VerticalAlignmentTypes.Middle)
										{
											theObject.Y += yOffset;
										}
									}
									break;
								case RESIZE_BORDER.RIGHT:
									if (theObject.Width + xOffset >= 0)
										theObject.Width += xOffset;
									break;
								case RESIZE_BORDER.BOTTOM:
									if (theObject.Height + yOffset >=0)
										theObject.Height += yOffset;
									break;
								case RESIZE_BORDER.LEFT:
									if (theObject.Width - xOffset >= 0)
									{
										if (theObject.HorizontalAlignment == ICustomPaint.HorizontalAlignmentTypes.None)
										{
											theObject.Width -= xOffset;
											theObject.X += xOffset; 
										}
										else if (theObject.HorizontalAlignment == ICustomPaint.HorizontalAlignmentTypes.Right || theObject.HorizontalAlignment == ICustomPaint.HorizontalAlignmentTypes.Center)
										{
											theObject.X += xOffset; 
										}
									}
									break;
							}

							TrackerRectangle selRectangle = new TrackerRectangle(theObject.GetRegion(),theObject.HorizontalAlignment,theObject.VerticalAlignment);							
							selRectangle.DrawSelectionTrackers((Bitmap)trackerImage);
						}

						
						PaintImage(trackerImage);							
						shouldRepaint = true;
					}
					else
					{
						if ( Math.Abs(e.X-ptStart.X)>0 || Math.Abs(e.Y-ptStart.Y)>0)
						{														
							trackerImage.Dispose();
							trackerImage = (Bitmap)drawingImage.Clone();							
							
							for (int i=0;i<selectedObjects.Count;i++)
							{
								ICustomPaint theObject = (ICustomPaint)selectedObjects[i];
								MoveSelection(theObject,theObject.X+e.X-ptStart.X,theObject.Y+e.Y-ptStart.Y);
							}

							PaintImage(trackerImage);
							ptStart = new Point(e.X, e.Y);
							shouldRepaint = true;
						}												
					}

					if(OnMoving!=null && selectedObjects.Count>0)
					{
						OnMoving((ICustomPaint)selectedObjects[selectedObjects.Count-1]);
					}

				}
			}
			else
			{
				
				if (selectedObjects.Count>0)
				{
					for (int i=0;i<selectedObjects.Count;i++)
					{
						ICustomPaint theObject = (ICustomPaint)selectedObjects[i];
						Rectangle theArea = theObject.GetRegion();
						theArea.Offset(-TrackerRectangle.m_nOffset,-TrackerRectangle.m_nOffset);
						theArea.Inflate(2*TrackerRectangle.m_nOffset,2*TrackerRectangle.m_nOffset);

						if ( theArea.Contains(e.X,e.Y) )
						{							
							TrackerRectangle tmp = new TrackerRectangle(theObject.GetRegion(),theObject.HorizontalAlignment, theObject.VerticalAlignment);
							int nResult = 0;
							this.Cursor = tmp.CursorCheck(new Point(e.X, e.Y), ref nResult);
							this.mBorder = (RESIZE_BORDER)nResult;
							moveReferenceObject = (ICustomPaint)selectedObjects[i];
							return;
						}
					}
				}
				this.Cursor = Cursors.Default;			
			}
		}


		private void CalculateStretchOffsets(int mouseX,int mouseY,ref int xOffset,ref int yOffset,RESIZE_BORDER border)
		{
			
			ICustomPaint theObject = moveReferenceObject;
			switch(border)
			{
				case RESIZE_BORDER.TOP:
					yOffset = mouseY - theObject.Y;
					xOffset = 0;
					break;

				case RESIZE_BORDER.BOTTOM:
					yOffset = mouseY - (theObject.Y+theObject.Height);
					xOffset = 0;
					break;

				case RESIZE_BORDER.LEFT:
					yOffset = 0;
					xOffset = mouseX - theObject.X;
					break;
						
				case RESIZE_BORDER.RIGHT:
					yOffset = 0;
					xOffset = mouseX - (theObject.X+theObject.Width);
					break;
						
				default:
					yOffset = 0;
					xOffset = 0;
					break;
			}
		}

		public void DesignPane_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			
			if (e.Control)
				this.mControlPressed = true;

			if (selectedObjects.Count>0)
			{
				
				for (int i=0;i<selectedObjects.Count;i++)				
				{
					ICustomPaint theObject = (ICustomPaint)selectedObjects[i];

					if (e.KeyCode == Keys.Down && e.Modifiers == Keys.Control)
					{
						trackerImage.Dispose();
						trackerImage = (Bitmap)drawingImage.Clone();
						MoveSelection(theObject,theObject.X,theObject.Y+1);
						PaintImage(trackerImage);
						shouldRepaint = true;
					}
					else if (e.KeyCode == Keys.Up && e.Modifiers == Keys.Control)
					{
						trackerImage.Dispose();
						trackerImage = (Bitmap)drawingImage.Clone();
						MoveSelection(theObject,theObject.X,theObject.Y-1);
						PaintImage(trackerImage);
						shouldRepaint = true;
					}						
					else if (e.KeyCode == Keys.Left && e.Modifiers == Keys.Control)
					{
						trackerImage.Dispose();
						trackerImage = (Bitmap)drawingImage.Clone();
						MoveSelection(theObject,theObject.X-1,theObject.Y);
						PaintImage(trackerImage);
						shouldRepaint = true;
					}						
					else if (e.KeyCode == Keys.Right && e.Modifiers == Keys.Control)
					{
						trackerImage.Dispose();
						trackerImage = (Bitmap)drawingImage.Clone();
						MoveSelection(theObject,theObject.X+1,theObject.Y);
						PaintImage(trackerImage);
						shouldRepaint = true;
					}						
					else
						return;
			
				}
				
				if(OnMoving!=null)
				{
					OnMoving((ICustomPaint)selectedObjects[selectedObjects.Count-1]);
				}
				
			}
		}

		public void DesignPane_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.mControlPressed = false;

			if (selectedObjects.Count>0)
			{
				if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
				{
					if(shouldRepaint)
					{
						shouldRepaint = false;
						DoPaint();

						if(OnMoveFinished!=null)
							OnMoveFinished((ICustomPaint)selectedObjects[selectedObjects.Count-1]);
					}
				}
			}
		}

		
		protected override void OnMouseDown(MouseEventArgs e)
		{		
			DesignPane_MouseDown(this,e);				
		}
		
		private void MoveSelection(ICustomPaint theElement,int xOrigin,int yOrigin)
		{
			if (theElement.HorizontalAlignment == ICustomPaint.HorizontalAlignmentTypes.None)
				theElement.X = xOrigin;

			if (theElement.VerticalAlignment == ICustomPaint.VerticalAlignmentTypes.None)
				theElement.Y = yOrigin;

			selectionRectangle = new TrackerRectangle(theElement.GetRegion(),theElement.HorizontalAlignment,theElement.VerticalAlignment);							
			selectionRectangle.DrawSelectionTrackers((Bitmap)trackerImage);
		}


		public bool ShowGrid
		{
			get {return showGrid;}
			set 
			{
				showGrid = value;
				CreateBackImage();
				DoPaint();
			}
		}

		public int GridSize
		{
			get {return gridSize;}
			set 
			{
				gridSize = value;
				CreateBackImage();
				DoPaint();
			}
		}

		




		



		

		






		
		
	}
}
