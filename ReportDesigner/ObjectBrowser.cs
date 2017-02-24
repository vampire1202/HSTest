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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using daReport;
using System.Xml;


namespace daReportDesigner
{
	public delegate void AfterCustomSelectHandler(TreeNode e,int action);

	/// <summary>
	/// Summary description for ObjectBrowser.
	/// </summary>
	public class ObjectBrowser : System.Windows.Forms.TreeView
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		TreeNode rootNode;
		TreeNode parametersNode;
		TreeNode contentNode;

		public TreeNode staticContentsNode;
		public TreeNode dynamicContentsNode;
		public event AfterCustomSelectHandler AfterCustomSelect;
		private ArrayList mNodesCollection = new ArrayList();
		
		public ObjectBrowser()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

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
			components = new System.ComponentModel.Container();
		}
		#endregion

		private void InitNodes()
		{
			this.Nodes.Clear();

			parametersNode = new TreeNode("属性列表"); 
			contentNode = new TreeNode("内容");
			staticContentsNode = new TreeNode("静态内容");
			dynamicContentsNode = new TreeNode("动态内容");
			contentNode.Nodes.Add(staticContentsNode);
			contentNode.Nodes.Add(dynamicContentsNode);
			contentNode.Expand();

			rootNode = new TreeNode("文档");
			
			rootNode.Nodes.Add(parametersNode);
			rootNode.Nodes.Add(contentNode);
			
			rootNode.Expand();
			this.Nodes.Add(rootNode);
			
		}

		public void SetData(ArrayList parameters, ICustomPaint[] staticElements, ICustomPaint[] dynamicElements)
		{
			InitNodes();
			for (int i=0;i<parameters.Count;i++)
			{
				parametersNode.Nodes.Add( new TreeNode(parameters[i].ToString(),2,2) );	
			}
			parametersNode.Expand();
			
			for (int i=0;i<staticElements.Length;i++)
			{
				if ( staticElements[i] is TextField)
				{
					TextField txtField = (TextField)staticElements[i];
					string theText = txtField.Text.Length>25 ? txtField.Text.Substring(0,25)+"..." : txtField.Text ;
                    string name = txtField.Name.ToString();
					staticContentsNode.Nodes.Add( new TreeNode( "Text field [" + name + "]",0,0) );	
				}
				else if ( staticElements[i] is daReport.PictureBox)
				{
					daReport.PictureBox picBox = (daReport.PictureBox)staticElements[i];
					string theText = picBox.ImageFile==null ? "none" : picBox.ImageFile; 
					staticContentsNode.Nodes.Add( new TreeNode("Picture [" + theText + "]",1,1) );	
				}
				else if ( staticElements[i] is daReport.ChartBox)
				{
					ChartBox chartBox = (ChartBox)staticElements[i];
					staticContentsNode.Nodes.Add( new TreeNode("Chart [" + chartBox.Name + "]",5,5) );	
				}
				else if ( staticElements[i] is daReport.StyledTable)
				{
					StyledTable styledTable = (StyledTable)staticElements[i];

					if (styledTable.DataSource != null)
						staticContentsNode.Nodes.Add( new TreeNode( "Styled table [" + styledTable.DataSource + "]",3,3) );
					else
						staticContentsNode.Nodes.Add( new TreeNode("Styled table",3,3) );
				}
			}
			staticContentsNode.Expand();

			for (int i=0;i<dynamicElements.Length;i++)
			{
				if ( dynamicElements[i] is TextField)
				{
					TextField txtField = (TextField)dynamicElements[i];
					string theText = txtField.Text.Length>25 ? txtField.Text.Substring(0,25)+"..." : txtField.Text ;
					dynamicContentsNode.Nodes.Add( new TreeNode("Text field [" + txtField.Name + "]",0,0) );	
				}
				else if ( dynamicElements[i] is daReport.PictureBox)
				{
					dynamicContentsNode.Nodes.Add( new TreeNode("Picture",1,1) );	
				}
				else if ( dynamicElements[i] is daReport.StyledTable)
				{
					StyledTable styledTable = (StyledTable)dynamicElements[i];

					if (styledTable.DataSource != null)
						dynamicContentsNode.Nodes.Add( new TreeNode("Styled table [" + styledTable.DataSource + "]",3,3) );
					else
						dynamicContentsNode.Nodes.Add( new TreeNode("Styled table",3,3) );
				}
			}
			dynamicContentsNode.Expand();
			
		}


		protected override void OnAfterSelect(TreeViewEventArgs e)
		{
			base.OnAfterSelect(e);

			removePaintFromNodes(rootNode);
			mNodesCollection.Clear();
			mNodesCollection.Add( e.Node );

			paintSelectedNodes();
			this.AfterCustomSelect(e.Node,0);
		}

		public bool IsStaticNode(TreeNode theNode)
		{
			if (theNode==null) return false;

			return theNode.Parent == staticContentsNode;
		}

		public bool IsDynamicNode(TreeNode theNode)
		{
			if (theNode==null) return false;

			return theNode.Parent == dynamicContentsNode;
		}

		public bool IsRootNode(TreeNode theNode)
		{
			if (theNode==null) return false;
			return theNode == this.Nodes[0];
		}

		public void SelectStaticNode(int t,int action)
		{
			if (action==0)
			{
				removePaintFromNodes(rootNode);
				mNodesCollection.Clear();
				mNodesCollection.Add(staticContentsNode.Nodes[t]);
			}
			else if (action==1)
			{
				mNodesCollection.Add(staticContentsNode.Nodes[t]);
			}
			else if (action==2)
			{
				removePaintFromNodes(staticContentsNode.Nodes[t]);
				mNodesCollection.Remove(staticContentsNode.Nodes[t]);
			}


			paintSelectedNodes();
			this.AfterCustomSelect(staticContentsNode.Nodes[t],action);
		}

		public void SelectDynamicNode(int t,int action)
		{
			if (action==0)
			{
				removePaintFromNodes(rootNode);
				mNodesCollection.Clear();
				mNodesCollection.Add(dynamicContentsNode.Nodes[t]);
			}
			else if (action==1)
			{
				mNodesCollection.Add(dynamicContentsNode.Nodes[t]);
			}
			else if (action==2)
			{
				removePaintFromNodes(staticContentsNode.Nodes[t]);
				mNodesCollection.Remove(dynamicContentsNode.Nodes[t]);
			}


			paintSelectedNodes();
			this.AfterCustomSelect(dynamicContentsNode.Nodes[t],action);
		}

		public void SetNodeText(ICustomPaint theObject,TreeNode theNode )
		{
			if ( theObject is TextField)
			{
				TextField txtField = (TextField)theObject;
				string theText = txtField.Text.Length>25 ? txtField.Text.Substring(0,25)+"..." : txtField.Text ;
                theNode.Name = txtField.Name;
                theNode.Text =  "Text field [" + theText + "]" ;	
			}
			else if ( theObject is daReport.PictureBox)
			{
				
				theNode.Text = "Picture" ;	
			}
			else if ( theObject is daReport.ChartBox)
			{
				ChartBox chartBox = (ChartBox)theObject;
				theNode.Text = "Chart [" + chartBox.Name + "]" ;	
			}
			else if ( theObject is daReport.StyledTable)
			{
				StyledTable styledTable = (StyledTable)theObject;

				if (styledTable.DataSource != null)
					theNode.Text = "Styled table [" + styledTable.DataSource + "]" ;
				else
					theNode.Text = "Styled table" ;
			}
			
		}

		public ArrayList SelectedNodes
		{           
			get
			{
				return mNodesCollection;
			}
			set
			{
                if (rootNode != null)
                {
                    removePaintFromNodes(rootNode);
                    mNodesCollection = value;
                    paintSelectedNodes();
                    this.AfterCustomSelect(rootNode, 0);
                }
			}  
		}


		protected void removePaintFromNodes(TreeNode CurrentNode)
		{
            try
            {
                if (CurrentNode.Nodes.Count != 0)
                {
                    foreach (TreeNode ChildNode in CurrentNode.Nodes)
                    {
                        removePaintFromNodes(ChildNode);
                    }
                }

                CurrentNode.BackColor = this.BackColor;
                CurrentNode.ForeColor = this.ForeColor;
            }
            catch { }
		}

		protected void paintSelectedNodes()
		{
            try
            {
                foreach (TreeNode n in mNodesCollection)
                {
                    n.BackColor = SystemColors.Highlight;
                    n.ForeColor = SystemColors.HighlightText;
                }
            }
            catch { }
		}
	}
}
