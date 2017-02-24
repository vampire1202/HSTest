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
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using daReport;


namespace daReportDesigner
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainFrame : System.Windows.Forms.Form
	{
		#region Declarations
		DaPrintDocument theDocument;
		ICustomPaint[] staticObjects;
		ICustomPaint[] dynamicObjects;
		ArrayList parameters;
		AboutFrame aboutFrame;

		private string theFilenameToSave;

		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem fileMenuItem;
		private System.Windows.Forms.MenuItem exitMenuItem;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBarPanel messagePanel;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.Panel rightPanel;
		private System.Windows.Forms.Panel propertyPanel;
		private System.Windows.Forms.Label propertyLabel;
        private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.Panel browserPanel;
		private System.Windows.Forms.Label browserLabel;
		private System.Windows.Forms.MenuItem openMenuItem;
		private System.Windows.Forms.MenuItem menuItem2;
		private daReportDesigner.ObjectBrowser objectBrowser;
		private System.Windows.Forms.ImageList browserImageList;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.ContextMenu browserContextMenuStatic;
		private System.Windows.Forms.ContextMenu browserContextMenuGeneral;
		private System.Windows.Forms.MenuItem addParameterGeneral;
		private System.Windows.Forms.MenuItem saveAsMenuItem;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label propertyObject;
		private System.Windows.Forms.MenuItem saveMenuItem;
		private System.Windows.Forms.StatusBarPanel selectionPanel;
		private System.Windows.Forms.MenuItem addStaticTextFieldMenuItem;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem deleteStaticObjectMenuItem;
		private System.Windows.Forms.MenuItem newMenuItem;
		private System.Windows.Forms.MenuItem addStaticPictureBoxMenuItem;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem backOneMenuItem;
		private System.Windows.Forms.MenuItem sendToBackStaticMenuItem;
		private System.Windows.Forms.MenuItem forwardOneStaticMenuItem;
		private System.Windows.Forms.MenuItem bringForwardStaticMenuItem;
		private System.Windows.Forms.MenuItem addTableStaticMenuItem;
		private System.Windows.Forms.ContextMenu browserStaticRootContextMenu;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.ContextMenu browserDynamicRootContextMenu;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem addTableDynamicMenuItem;
		private System.Windows.Forms.MenuItem closeDocumentMenuItem;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem aboutMenuItem;
		private System.Windows.Forms.MenuItem addPageNumberMenuItem;
		private System.Windows.Forms.ContextMenu browserContextMenuDynamic;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.MenuItem deleteDynamicMenuItem;
		private System.Windows.Forms.MenuItem backOneDynamicMenuItem;
		private System.Windows.Forms.MenuItem forwardOneDynamicMenuItem;
		private System.Windows.Forms.MenuItem sendToBackDynamicMenuItem;
		private System.Windows.Forms.MenuItem bringToFrontDynamicMenuItem;
		private System.Windows.Forms.MenuItem addStaticChartMenuItem;
		private System.Windows.Forms.MenuItem addStaticChartRootMenuItem;
		private System.Windows.Forms.MenuItem duplicateStaticMenuItem;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
		private System.Windows.Forms.MenuItem printPreviewMenuItem;
		private System.Windows.Forms.MenuItem menuItem19;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem duplicateDynamicMenuItem;
		private System.Windows.Forms.TabControl tabPanel;
		private System.Windows.Forms.Panel scrollPanel;
		private daReportDesigner.DesignPane designPane;
		private System.Windows.Forms.TabPage editPage;
		private System.Windows.Forms.MenuItem optionsMenuItem;
		private System.Windows.Forms.MenuItem gridMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton 新建NToolStripButton;
        private ToolStripButton 打开OToolStripButton;
        private ToolStripButton 保存SToolStripButton;
        private ToolStripButton 打印PToolStripButton;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripButton 帮助LToolStripButton;
        private TextBox textBox1;
        private SplitContainer splitContainer1;
		private System.ComponentModel.IContainer components;
		#endregion

		#region Creator

		public MainFrame()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			Arrange();
		}

		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrame));
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.fileMenuItem = new System.Windows.Forms.MenuItem();
            this.newMenuItem = new System.Windows.Forms.MenuItem();
            this.openMenuItem = new System.Windows.Forms.MenuItem();
            this.saveMenuItem = new System.Windows.Forms.MenuItem();
            this.saveAsMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.printPreviewMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.closeDocumentMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.optionsMenuItem = new System.Windows.Forms.MenuItem();
            this.gridMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.messagePanel = new System.Windows.Forms.StatusBarPanel();
            this.selectionPanel = new System.Windows.Forms.StatusBarPanel();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.browserPanel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.browserImageList = new System.Windows.Forms.ImageList(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.browserLabel = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.propertyPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.propertyObject = new System.Windows.Forms.Label();
            this.propertyLabel = new System.Windows.Forms.Label();
            this.browserContextMenuStatic = new System.Windows.Forms.ContextMenu();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.addStaticTextFieldMenuItem = new System.Windows.Forms.MenuItem();
            this.addStaticPictureBoxMenuItem = new System.Windows.Forms.MenuItem();
            this.addTableStaticMenuItem = new System.Windows.Forms.MenuItem();
            this.addStaticChartMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.deleteStaticObjectMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.backOneMenuItem = new System.Windows.Forms.MenuItem();
            this.sendToBackStaticMenuItem = new System.Windows.Forms.MenuItem();
            this.forwardOneStaticMenuItem = new System.Windows.Forms.MenuItem();
            this.bringForwardStaticMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.duplicateStaticMenuItem = new System.Windows.Forms.MenuItem();
            this.browserContextMenuGeneral = new System.Windows.Forms.ContextMenu();
            this.addParameterGeneral = new System.Windows.Forms.MenuItem();
            this.browserStaticRootContextMenu = new System.Windows.Forms.ContextMenu();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.addStaticChartRootMenuItem = new System.Windows.Forms.MenuItem();
            this.browserDynamicRootContextMenu = new System.Windows.Forms.ContextMenu();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.addPageNumberMenuItem = new System.Windows.Forms.MenuItem();
            this.addTableDynamicMenuItem = new System.Windows.Forms.MenuItem();
            this.browserContextMenuDynamic = new System.Windows.Forms.ContextMenu();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.deleteDynamicMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.backOneDynamicMenuItem = new System.Windows.Forms.MenuItem();
            this.sendToBackDynamicMenuItem = new System.Windows.Forms.MenuItem();
            this.forwardOneDynamicMenuItem = new System.Windows.Forms.MenuItem();
            this.bringToFrontDynamicMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.duplicateDynamicMenuItem = new System.Windows.Forms.MenuItem();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.tabPanel = new System.Windows.Forms.TabControl();
            this.editPage = new System.Windows.Forms.TabPage();
            this.scrollPanel = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.新建NToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.打开OToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.保存SToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.打印PToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.帮助LToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.designPane = new daReportDesigner.DesignPane();
            this.objectBrowser = new daReportDesigner.ObjectBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.messagePanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectionPanel)).BeginInit();
            this.rightPanel.SuspendLayout();
            this.browserPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.propertyPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPanel.SuspendLayout();
            this.editPage.SuspendLayout();
            this.scrollPanel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenuItem,
            this.optionsMenuItem,
            this.menuItem11});
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.Index = 0;
            this.fileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.newMenuItem,
            this.openMenuItem,
            this.saveMenuItem,
            this.saveAsMenuItem,
            this.menuItem19,
            this.printPreviewMenuItem,
            this.menuItem12,
            this.closeDocumentMenuItem,
            this.menuItem2,
            this.exitMenuItem});
            this.fileMenuItem.Text = "&文件(F)";
            // 
            // newMenuItem
            // 
            this.newMenuItem.Index = 0;
            this.newMenuItem.Text = "新建(&N";
            this.newMenuItem.Click += new System.EventHandler(this.newMenuItem_Click);
            // 
            // openMenuItem
            // 
            this.openMenuItem.Index = 1;
            this.openMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.openMenuItem.Text = "打开(&O ...)";
            this.openMenuItem.Click += new System.EventHandler(this.openMenuItem_Click);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Index = 2;
            this.saveMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.saveMenuItem.Text = "保存(&S)";
            this.saveMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
            // 
            // saveAsMenuItem
            // 
            this.saveAsMenuItem.Index = 3;
            this.saveAsMenuItem.Text = "另存为(&Save as ...)";
            this.saveAsMenuItem.Click += new System.EventHandler(this.saveAsMenuItem_Click);
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 4;
            this.menuItem19.Text = "-";
            // 
            // printPreviewMenuItem
            // 
            this.printPreviewMenuItem.Index = 5;
            this.printPreviewMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftP;
            this.printPreviewMenuItem.Text = "打印预览(&P";
            this.printPreviewMenuItem.Click += new System.EventHandler(this.printPreviewMenuItem_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 6;
            this.menuItem12.Text = "-";
            // 
            // closeDocumentMenuItem
            // 
            this.closeDocumentMenuItem.Index = 7;
            this.closeDocumentMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlW;
            this.closeDocumentMenuItem.Text = "关闭(&C";
            this.closeDocumentMenuItem.Click += new System.EventHandler(this.closeDocumentMenuItem_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 8;
            this.menuItem2.Text = "-";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 9;
            this.exitMenuItem.Text = "退出(&Exit)";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // optionsMenuItem
            // 
            this.optionsMenuItem.Index = 1;
            this.optionsMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.gridMenuItem});
            this.optionsMenuItem.Text = "选项(&Options)";
            // 
            // gridMenuItem
            // 
            this.gridMenuItem.Index = 0;
            this.gridMenuItem.Text = "网格(&Grid ...)";
            this.gridMenuItem.Click += new System.EventHandler(this.gridMenuItem_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 2;
            this.menuItem11.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.aboutMenuItem});
            this.menuItem11.Text = "帮助(&H)";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Index = 0;
            this.aboutMenuItem.Text = "关于(&About ...)";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, -24);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.messagePanel,
            this.selectionPanel});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(968, 24);
            this.statusBar1.SizingGrip = false;
            this.statusBar1.TabIndex = 3;
            this.statusBar1.Text = "statusBar1";
            // 
            // messagePanel
            // 
            this.messagePanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.messagePanel.Name = "messagePanel";
            this.messagePanel.Width = 748;
            // 
            // selectionPanel
            // 
            this.selectionPanel.Name = "selectionPanel";
            this.selectionPanel.Width = 220;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propertyGrid.Location = new System.Drawing.Point(0, 30);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(260, 253);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.browserPanel);
            this.rightPanel.Controls.Add(this.splitter2);
            this.rightPanel.Controls.Add(this.propertyPanel);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(0, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(264, 0);
            this.rightPanel.TabIndex = 5;
            // 
            // browserPanel
            // 
            this.browserPanel.Controls.Add(this.panel3);
            this.browserPanel.Controls.Add(this.browserLabel);
            this.browserPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserPanel.Location = new System.Drawing.Point(0, 0);
            this.browserPanel.Name = "browserPanel";
            this.browserPanel.Padding = new System.Windows.Forms.Padding(1, 0, 2, 0);
            this.browserPanel.Size = new System.Drawing.Size(264, 0);
            this.browserPanel.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.objectBrowser);
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1, 17);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.panel3.Size = new System.Drawing.Size(261, 0);
            this.panel3.TabIndex = 3;
            // 
            // browserImageList
            // 
            this.browserImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("browserImageList.ImageStream")));
            this.browserImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.browserImageList.Images.SetKeyName(0, "");
            this.browserImageList.Images.SetKeyName(1, "");
            this.browserImageList.Images.SetKeyName(2, "");
            this.browserImageList.Images.SetKeyName(3, "");
            this.browserImageList.Images.SetKeyName(4, "");
            this.browserImageList.Images.SetKeyName(5, "");
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.ForeColor = System.Drawing.Color.Red;
            this.textBox1.Location = new System.Drawing.Point(0, -93);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(261, 93);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "模板修改说明：\r\n1、添加文本框字段时，name属性设置为顺序数值,比如：1,2,3...。\r\n2、添加图片框时，name设置为顺序数值比如：1,2,3...。\r" +
                "\n3、title,content,date三个文本框分别表示\"报告标题\",\"诊断结果\",\"报告日期\",建议不修改。";
            // 
            // browserLabel
            // 
            this.browserLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.browserLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.browserLabel.Location = new System.Drawing.Point(1, 0);
            this.browserLabel.Name = "browserLabel";
            this.browserLabel.Size = new System.Drawing.Size(261, 17);
            this.browserLabel.TabIndex = 0;
            this.browserLabel.Text = "对象列表";
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, -305);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(264, 3);
            this.splitter2.TabIndex = 1;
            this.splitter2.TabStop = false;
            // 
            // propertyPanel
            // 
            this.propertyPanel.Controls.Add(this.panel1);
            this.propertyPanel.Controls.Add(this.propertyLabel);
            this.propertyPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.propertyPanel.Location = new System.Drawing.Point(0, -302);
            this.propertyPanel.Name = "propertyPanel";
            this.propertyPanel.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.propertyPanel.Size = new System.Drawing.Size(264, 302);
            this.propertyPanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.propertyGrid);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 17);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panel1.Size = new System.Drawing.Size(260, 285);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.propertyObject);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 2);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panel2.Size = new System.Drawing.Size(260, 28);
            this.panel2.TabIndex = 0;
            // 
            // propertyObject
            // 
            this.propertyObject.BackColor = System.Drawing.Color.White;
            this.propertyObject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.propertyObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyObject.Location = new System.Drawing.Point(0, 2);
            this.propertyObject.Name = "propertyObject";
            this.propertyObject.Size = new System.Drawing.Size(260, 24);
            this.propertyObject.TabIndex = 0;
            this.propertyObject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // propertyLabel
            // 
            this.propertyLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.propertyLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.propertyLabel.Location = new System.Drawing.Point(2, 0);
            this.propertyLabel.Name = "propertyLabel";
            this.propertyLabel.Size = new System.Drawing.Size(260, 17);
            this.propertyLabel.TabIndex = 0;
            this.propertyLabel.Text = "属性";
            // 
            // browserContextMenuStatic
            // 
            this.browserContextMenuStatic.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem3,
            this.addStaticTextFieldMenuItem,
            this.addStaticPictureBoxMenuItem,
            this.addTableStaticMenuItem,
            this.addStaticChartMenuItem,
            this.menuItem4,
            this.deleteStaticObjectMenuItem,
            this.menuItem1,
            this.backOneMenuItem,
            this.sendToBackStaticMenuItem,
            this.forwardOneStaticMenuItem,
            this.bringForwardStaticMenuItem,
            this.menuItem17,
            this.duplicateStaticMenuItem});
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 0;
            this.menuItem3.Text = "添加属性";
            // 
            // addStaticTextFieldMenuItem
            // 
            this.addStaticTextFieldMenuItem.Index = 1;
            this.addStaticTextFieldMenuItem.Text = "添加文本框";
            this.addStaticTextFieldMenuItem.Click += new System.EventHandler(this.addStaticTextFieldMenuItem_Click);
            // 
            // addStaticPictureBoxMenuItem
            // 
            this.addStaticPictureBoxMenuItem.Index = 2;
            this.addStaticPictureBoxMenuItem.Text = "添加图片";
            this.addStaticPictureBoxMenuItem.Click += new System.EventHandler(this.addStaticPictureBoxMenuItem_Click);
            // 
            // addTableStaticMenuItem
            // 
            this.addTableStaticMenuItem.Index = 3;
            this.addTableStaticMenuItem.Text = "添加表格";
            this.addTableStaticMenuItem.Click += new System.EventHandler(this.addTableStaticMenuItem_Click);
            // 
            // addStaticChartMenuItem
            // 
            this.addStaticChartMenuItem.Index = 4;
            this.addStaticChartMenuItem.Text = "添加图表";
            this.addStaticChartMenuItem.Click += new System.EventHandler(this.addStaticChartMenuItem_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 5;
            this.menuItem4.Text = "-";
            // 
            // deleteStaticObjectMenuItem
            // 
            this.deleteStaticObjectMenuItem.Index = 6;
            this.deleteStaticObjectMenuItem.Text = "删除";
            this.deleteStaticObjectMenuItem.Click += new System.EventHandler(this.deleteStaticObjectMenuItem_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 7;
            this.menuItem1.Text = "-";
            // 
            // backOneMenuItem
            // 
            this.backOneMenuItem.Index = 8;
            this.backOneMenuItem.Text = "放置在后一层";
            this.backOneMenuItem.Click += new System.EventHandler(this.backOneStaticMenuItem_Click);
            // 
            // sendToBackStaticMenuItem
            // 
            this.sendToBackStaticMenuItem.Index = 9;
            this.sendToBackStaticMenuItem.Text = "放置在最后";
            this.sendToBackStaticMenuItem.Click += new System.EventHandler(this.sendToBackStaticMenuItem_Click);
            // 
            // forwardOneStaticMenuItem
            // 
            this.forwardOneStaticMenuItem.Index = 10;
            this.forwardOneStaticMenuItem.Text = "放置在前一层";
            this.forwardOneStaticMenuItem.Click += new System.EventHandler(this.forwardOneStaticMenuItem_Click);
            // 
            // bringForwardStaticMenuItem
            // 
            this.bringForwardStaticMenuItem.Index = 11;
            this.bringForwardStaticMenuItem.Text = "放置在最前";
            this.bringForwardStaticMenuItem.Click += new System.EventHandler(this.bringForwardStaticMenuItem_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 12;
            this.menuItem17.Text = "-";
            // 
            // duplicateStaticMenuItem
            // 
            this.duplicateStaticMenuItem.Index = 13;
            this.duplicateStaticMenuItem.Text = "复制";
            this.duplicateStaticMenuItem.Click += new System.EventHandler(this.duplicateStaticMenuItem_Click);
            // 
            // browserContextMenuGeneral
            // 
            this.browserContextMenuGeneral.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.addParameterGeneral});
            // 
            // addParameterGeneral
            // 
            this.addParameterGeneral.Index = 0;
            this.addParameterGeneral.Text = "添加属性";
            this.addParameterGeneral.Click += new System.EventHandler(this.addParameterGeneral_Click);
            // 
            // browserStaticRootContextMenu
            // 
            this.browserStaticRootContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem5,
            this.menuItem6,
            this.menuItem7,
            this.menuItem8,
            this.addStaticChartRootMenuItem});
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 0;
            this.menuItem5.Text = "添加属性值";
            this.menuItem5.Click += new System.EventHandler(this.addParameterGeneral_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 1;
            this.menuItem6.Text = "添加文本框";
            this.menuItem6.Click += new System.EventHandler(this.addStaticTextFieldMenuItem_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 2;
            this.menuItem7.Text = "添加图片框";
            this.menuItem7.Click += new System.EventHandler(this.addStaticPictureBoxMenuItem_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 3;
            this.menuItem8.Text = "添加表格";
            this.menuItem8.Click += new System.EventHandler(this.addTableStaticMenuItem_Click);
            // 
            // addStaticChartRootMenuItem
            // 
            this.addStaticChartRootMenuItem.Index = 4;
            this.addStaticChartRootMenuItem.Text = "添加图表";
            this.addStaticChartRootMenuItem.Click += new System.EventHandler(this.addStaticChartMenuItem_Click);
            // 
            // browserDynamicRootContextMenu
            // 
            this.browserDynamicRootContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem9,
            this.addPageNumberMenuItem,
            this.addTableDynamicMenuItem});
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 0;
            this.menuItem9.Text = "添加属性值";
            this.menuItem9.Click += new System.EventHandler(this.addParameterGeneral_Click);
            // 
            // addPageNumberMenuItem
            // 
            this.addPageNumberMenuItem.Index = 1;
            this.addPageNumberMenuItem.Text = "添加页码";
            this.addPageNumberMenuItem.Click += new System.EventHandler(this.addPageNumberMenuItem_Click);
            // 
            // addTableDynamicMenuItem
            // 
            this.addTableDynamicMenuItem.Index = 2;
            this.addTableDynamicMenuItem.Text = "添加表格";
            this.addTableDynamicMenuItem.Click += new System.EventHandler(this.addTableDynamicMenuItem_Click);
            // 
            // browserContextMenuDynamic
            // 
            this.browserContextMenuDynamic.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem10,
            this.menuItem13,
            this.menuItem15,
            this.menuItem16,
            this.deleteDynamicMenuItem,
            this.menuItem18,
            this.backOneDynamicMenuItem,
            this.sendToBackDynamicMenuItem,
            this.forwardOneDynamicMenuItem,
            this.bringToFrontDynamicMenuItem,
            this.menuItem14,
            this.duplicateDynamicMenuItem});
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 0;
            this.menuItem10.Text = "添加属性值";
            this.menuItem10.Click += new System.EventHandler(this.addParameterGeneral_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 1;
            this.menuItem13.Text = "添加页码";
            this.menuItem13.Click += new System.EventHandler(this.addPageNumberMenuItem_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 2;
            this.menuItem15.Text = "添加表格";
            this.menuItem15.Click += new System.EventHandler(this.addTableDynamicMenuItem_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 3;
            this.menuItem16.Text = "-";
            // 
            // deleteDynamicMenuItem
            // 
            this.deleteDynamicMenuItem.Index = 4;
            this.deleteDynamicMenuItem.Text = "删除";
            this.deleteDynamicMenuItem.Click += new System.EventHandler(this.deleteDynamicMenuItem_Click);
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 5;
            this.menuItem18.Text = "-";
            // 
            // backOneDynamicMenuItem
            // 
            this.backOneDynamicMenuItem.Index = 6;
            this.backOneDynamicMenuItem.Text = "置后一层";
            this.backOneDynamicMenuItem.Click += new System.EventHandler(this.backOneDynamicMenuItem_Click);
            // 
            // sendToBackDynamicMenuItem
            // 
            this.sendToBackDynamicMenuItem.Index = 7;
            this.sendToBackDynamicMenuItem.Text = "置于最后";
            this.sendToBackDynamicMenuItem.Click += new System.EventHandler(this.sendToBackDynamicMenuItem_Click);
            // 
            // forwardOneDynamicMenuItem
            // 
            this.forwardOneDynamicMenuItem.Index = 8;
            this.forwardOneDynamicMenuItem.Text = "置前一层";
            this.forwardOneDynamicMenuItem.Click += new System.EventHandler(this.forwardOneDynamicMenuItem_Click);
            // 
            // bringToFrontDynamicMenuItem
            // 
            this.bringToFrontDynamicMenuItem.Index = 9;
            this.bringToFrontDynamicMenuItem.Text = "置于最前";
            this.bringToFrontDynamicMenuItem.Click += new System.EventHandler(this.bringForwardDynamicMenuItem_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 10;
            this.menuItem14.Text = "-";
            // 
            // duplicateDynamicMenuItem
            // 
            this.duplicateDynamicMenuItem.Index = 11;
            this.duplicateDynamicMenuItem.Text = "复制";
            this.duplicateDynamicMenuItem.Click += new System.EventHandler(this.duplicateDynamicMenuItem_Click);
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.Visible = false;
            // 
            // tabPanel
            // 
            this.tabPanel.Controls.Add(this.editPage);
            this.tabPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPanel.Location = new System.Drawing.Point(0, 0);
            this.tabPanel.Name = "tabPanel";
            this.tabPanel.SelectedIndex = 0;
            this.tabPanel.Size = new System.Drawing.Size(893, 0);
            this.tabPanel.TabIndex = 7;
            // 
            // editPage
            // 
            this.editPage.Controls.Add(this.scrollPanel);
            this.editPage.Location = new System.Drawing.Point(4, 22);
            this.editPage.Name = "editPage";
            this.editPage.Size = new System.Drawing.Size(885, 0);
            this.editPage.TabIndex = 0;
            this.editPage.Text = "页面";
            this.editPage.UseVisualStyleBackColor = true;
            // 
            // scrollPanel
            // 
            this.scrollPanel.AutoScroll = true;
            this.scrollPanel.Controls.Add(this.designPane);
            this.scrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollPanel.Location = new System.Drawing.Point(0, 0);
            this.scrollPanel.Name = "scrollPanel";
            this.scrollPanel.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.scrollPanel.Size = new System.Drawing.Size(885, 0);
            this.scrollPanel.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建NToolStripButton,
            this.打开OToolStripButton,
            this.保存SToolStripButton,
            this.打印PToolStripButton,
            this.toolStripSeparator,
            this.帮助LToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(968, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // 新建NToolStripButton
            // 
            this.新建NToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.新建NToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("新建NToolStripButton.Image")));
            this.新建NToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.新建NToolStripButton.Name = "新建NToolStripButton";
            this.新建NToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.新建NToolStripButton.Text = "新建(&N)";
            this.新建NToolStripButton.Click += new System.EventHandler(this.newMenuItem_Click);
            // 
            // 打开OToolStripButton
            // 
            this.打开OToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.打开OToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("打开OToolStripButton.Image")));
            this.打开OToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.打开OToolStripButton.Name = "打开OToolStripButton";
            this.打开OToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.打开OToolStripButton.Text = "打开(&O)";
            this.打开OToolStripButton.Click += new System.EventHandler(this.openMenuItem_Click);
            // 
            // 保存SToolStripButton
            // 
            this.保存SToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.保存SToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("保存SToolStripButton.Image")));
            this.保存SToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.保存SToolStripButton.Name = "保存SToolStripButton";
            this.保存SToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.保存SToolStripButton.Text = "保存(&S)";
            this.保存SToolStripButton.Click += new System.EventHandler(this.saveMenuItem_Click);
            // 
            // 打印PToolStripButton
            // 
            this.打印PToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.打印PToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("打印PToolStripButton.Image")));
            this.打印PToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.打印PToolStripButton.Name = "打印PToolStripButton";
            this.打印PToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.打印PToolStripButton.Text = "打印(&P)";
            this.打印PToolStripButton.Click += new System.EventHandler(this.printPreviewMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // 帮助LToolStripButton
            // 
            this.帮助LToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.帮助LToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("帮助LToolStripButton.Image")));
            this.帮助LToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.帮助LToolStripButton.Name = "帮助LToolStripButton";
            this.帮助LToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.帮助LToolStripButton.Text = "帮助(&L)";
            this.帮助LToolStripButton.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rightPanel);
            this.splitContainer1.Size = new System.Drawing.Size(968, 0);
            this.splitContainer1.SplitterDistance = 744;
            this.splitContainer1.TabIndex = 9;
            // 
            // designPane
            // 
            this.designPane.BackColor = System.Drawing.Color.White;
            this.designPane.GridSize = 8;
            this.designPane.Location = new System.Drawing.Point(0, 0);
            this.designPane.Name = "designPane";
            this.designPane.ShowGrid = false;
            this.designPane.Size = new System.Drawing.Size(368, 379);
            this.designPane.TabIndex = 0;
            this.designPane.OnMoveFinished += new daReportDesigner.PaintObjectHandler(this.designPane_OnMoveFinished);
            this.designPane.OnSelectionChanged += new daReportDesigner.SelectionChangedHandler(this.designPane_OnSelectionChanged);
            this.designPane.OnMoving += new daReportDesigner.PaintObjectHandler(this.designPane_OnMoving);
            // 
            // objectBrowser
            // 
            this.objectBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectBrowser.HideSelection = false;
            this.objectBrowser.ImageIndex = 4;
            this.objectBrowser.ImageList = this.browserImageList;
            this.objectBrowser.Location = new System.Drawing.Point(0, 2);
            this.objectBrowser.Name = "objectBrowser";
            this.objectBrowser.SelectedImageIndex = 4;
            this.objectBrowser.SelectedNodes = ((System.Collections.ArrayList)(resources.GetObject("objectBrowser.SelectedNodes")));
            this.objectBrowser.Size = new System.Drawing.Size(261, 0);
            this.objectBrowser.TabIndex = 0;
            this.objectBrowser.AfterCustomSelect += new daReportDesigner.AfterCustomSelectHandler(this.objectBrowser_AfterCustomSelect);
            // 
            // MainFrame
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(968, 0);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Menu = this.mainMenu;
            this.Name = "MainFrame";
            this.Text = "报表设计器";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainFrame_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainFrame_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainFrame_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.messagePanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectionPanel)).EndInit();
            this.rightPanel.ResumeLayout(false);
            this.browserPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.propertyPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabPanel.ResumeLayout(false);
            this.editPage.ResumeLayout(false);
            this.scrollPanel.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainFrame());
		}


		#endregion

		#region Public Methods

		public void NewDocument()
		{
			theDocument = new DaPrintDocument(true);

			parameters = new ArrayList();
			staticObjects = new ICustomPaint[0];
			dynamicObjects = new ICustomPaint[0];
			theDocument.OnMarginsChanged += new MarginsChangedHandler(RepeatAlignments);
			theDocument.OnPageSizeChanged += new PageSizeChangedHandler(ResizeDesignPane);
			theDocument.OnPageLayoutChanged += new PageLayoutChangedHandler(RepeatAlignments);
			objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
	
			designPane.SetSize(theDocument.DefaultPageSettings);
			designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);

			editPage.Text = "[新报告文档]";
			if (!tabPanel.TabPages.Contains(editPage))
				tabPanel.TabPages.Add(this.editPage);

			objectBrowser.Focus();

		}


		public void OpenDocument(string filename)
		{
			theDocument = new DaPrintDocument(true);
			theDocument.setXML(filename);
			
			parameters = new ArrayList(theDocument.Parameters);
			staticObjects = theDocument.StaticObjects;
			dynamicObjects = theDocument.DynamicObjects;

			theDocument.OnPageSizeChanged += new PageSizeChangedHandler(ResizeDesignPane);
						
			objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
	
			designPane.SetSize(theDocument.DefaultPageSettings);
			designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);
			
			objectBrowser.SelectedNode = objectBrowser.Nodes[0];
			
			if (!tabPanel.TabPages.Contains(editPage))
				tabPanel.TabPages.Add(this.editPage);
			editPage.Text = "[" + filename + "]";
			theFilenameToSave = filename;
			
			WriteMessage("Document opened. Select Static or Dynamic contents node in Object Browser and then right-click to add new element.");			
			objectBrowser.Focus();

		}

		
		public void RepeatAlignments(object sender)
		{
			for (int i=0;i<this.staticObjects.Length;i++)
			{
				staticObjects[i].HorizontalAlignment = staticObjects[i].HorizontalAlignment;
				staticObjects[i].VerticalAlignment = staticObjects[i].VerticalAlignment;
			}

			for (int i=0;i<this.dynamicObjects.Length;i++)
			{
				dynamicObjects[i].HorizontalAlignment = dynamicObjects[i].HorizontalAlignment;
				dynamicObjects[i].VerticalAlignment = dynamicObjects[i].VerticalAlignment;
			}
		}


		public void WriteMessage(string theMessage)
		{
			messagePanel.Text = theMessage;
		}

		
		#endregion

		#region Object and Event Handlers

		private void designPane_OnMoveFinished(daReport.ICustomPaint obj)
		{
			propertyGrid.Refresh();
			selectionPanel.Text = "X: "+obj.X.ToString() + "  Y: " + obj.Y.ToString() + "  Width: " + obj.Width.ToString() + "  Height: " + obj.Height.ToString();
		}


		private void designPane_OnMoving(daReport.ICustomPaint obj)
		{
			selectionPanel.Text = "X: "+obj.X.ToString() + "  Y: " + obj.Y.ToString() + "  Width: " + obj.Width.ToString() + "  Height: " + obj.Height.ToString();
		}
		

		private void designPane_OnSelectionChanged(daReport.ICustomPaint obj,int action)
		{
			objectBrowser.SelectedNode = null;

			if (obj==null)
			{
				objectBrowser.SelectedNode = objectBrowser.Nodes[0];
				objectBrowser.SelectedNodes = new ArrayList();
				WriteMessage("");
				return;
			}
			

			int objNum = FindStaticObject(obj);
			if (objNum>=0) 
			{
				objectBrowser.SelectStaticNode(objNum,action);
				WriteMessage("Selected objects : " + objectBrowser.SelectedNodes.Count + "   :::   Ctrl + Left Click to perform multiple selection   :::   Ctrl + Arrows for moving selection");
				return;
			}

			objNum = FindDynamicObject(obj);
			if (objNum>=0) 
			{
				objectBrowser.SelectDynamicNode(objNum,action);
				WriteMessage("Selected objects : " + objectBrowser.SelectedNodes.Count + "   :::   Ctrl + Left Click to perform multiple selection   :::   Ctrl + Arrows for moving selection");
				return;
			}

		}
		
		private void objectBrowser_AfterCustomSelect(System.Windows.Forms.TreeNode e, int action)
		{

			if ( e == objectBrowser.Nodes[0])
			{
				propertyGrid.SelectedObject = theDocument;
				propertyObject.Text = "Print document";
				designPane.UpdateSelection(null,0);
				selectionPanel.Text = "";
			}
			else if ( !objectBrowser.IsStaticNode(e) && !objectBrowser.IsDynamicNode(e) )
			{
				if ( e == objectBrowser.staticContentsNode )
					objectBrowser.ContextMenu = this.browserStaticRootContextMenu;
				else if ( e == objectBrowser.dynamicContentsNode )
					objectBrowser.ContextMenu = this.browserDynamicRootContextMenu;
				else
					objectBrowser.ContextMenu = browserContextMenuGeneral;

				propertyGrid.SelectedObject = null;
				propertyObject.Text = "";				
				designPane.UpdateSelection(null,0);
				selectionPanel.Text = "";
			}
			else
			{
				ArrayList SelectedObjects = new ArrayList();

				foreach (TreeNode CurrentNode in objectBrowser.SelectedNodes)
				{
					if ( objectBrowser.IsStaticNode(CurrentNode))
					{
						SelectedObjects.Add(staticObjects[CurrentNode.Index]);

						if ( CurrentNode == objectBrowser.SelectedNodes[objectBrowser.SelectedNodes.Count-1] )
						{
							propertyObject.Text = CurrentNode.Text;
							objectBrowser.ContextMenu = browserContextMenuStatic;
							selectionPanel.Text = "X: "+staticObjects[e.Index].X.ToString() + "  Y: " + staticObjects[e.Index].Y.ToString() + "  Width: " + staticObjects[e.Index].Width.ToString() + "  Height: " + staticObjects[e.Index].Height.ToString();
						}																						  
					}
					else if ( objectBrowser.IsDynamicNode(CurrentNode) )
					{
						SelectedObjects.Add(dynamicObjects[CurrentNode.Index]);
						
						if ( CurrentNode == objectBrowser.SelectedNodes[objectBrowser.SelectedNodes.Count-1] )
						{
							propertyObject.Text = CurrentNode.Text;
							objectBrowser.ContextMenu = browserContextMenuDynamic;
							selectionPanel.Text = "X: "+dynamicObjects[e.Index].X.ToString() + "  Y: " + dynamicObjects[e.Index].Y.ToString() + "  Width: " + dynamicObjects[e.Index].Width.ToString() + "  Height: " + dynamicObjects[e.Index].Height.ToString();
						}
					}
				}

				propertyGrid.SelectedObjects = SelectedObjects.ToArray();
				if (objectBrowser.SelectedNodes.Count > 1)
					propertyObject.Text = "Common Properties";

				if ( objectBrowser.IsStaticNode(e) )
				{
					designPane.UpdateSelection(staticObjects[e.Index],action);
				}
				else if ( objectBrowser.IsDynamicNode(e) )
				{
					designPane.UpdateSelection(dynamicObjects[e.Index],action);
				}

			}

		}


		private void propertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			if ( objectBrowser.SelectedNode != null )
			{
				TreeNode selectedNode = objectBrowser.SelectedNode;
				if ( objectBrowser.IsRootNode(selectedNode) )
				{
					if (e.ChangedItem.Label == "Layout")
					{
						designPane.SetSize(theDocument.DefaultPageSettings);
					}
				
					parameters = new ArrayList(theDocument.Parameters);
					objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
					objectBrowser.SelectedNode = objectBrowser.Nodes[0];
							
				}
			}
			else
			{
				if ( objectBrowser.SelectedNodes.Count == 1 )
				{
					TreeNode selectedNode = objectBrowser.SelectedNodes[0] as TreeNode;

					if ( objectBrowser.IsStaticNode(selectedNode) )
						objectBrowser.SetNodeText(staticObjects[selectedNode.Index],selectedNode);
					else if ( objectBrowser.IsDynamicNode(selectedNode) )
						objectBrowser.SetNodeText(dynamicObjects[selectedNode.Index],selectedNode);
				
			
					//propertyObject.Text = objectBrowser.SelectedNode.Text;
				}
			}

			designPane.Refresh();
		}
		

		private void MainFrame_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.Modifiers == Keys.Control && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Right || e.KeyCode == Keys.Left) )
			{
				designPane.DesignPane_KeyDown(this,e);
				e.Handled = true;
			}
			else if (e.Modifiers == Keys.Control)
			{
				designPane.DesignPane_KeyDown(this,e);
				e.Handled = true;
			}
			else 
				e.Handled = false;

		}


		private void MainFrame_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			designPane.DesignPane_KeyUp(this,e);
		}

		#endregion

		#region Menu Event Handlers

		#region File Menu

		private void closeDocumentMenuItem_Click(object sender, System.EventArgs e)
		{
			CloseDocument();
		}

		
		private void exitMenuItem_Click(object sender, System.EventArgs e)
		{
			DoClose();
		}
		
		
		private void newMenuItem_Click(object sender, System.EventArgs e)
		{
			NewDocument();
			this.saveAsMenuItem.Enabled = true;
			this.saveMenuItem.Enabled = true;
			this.closeDocumentMenuItem.Enabled = true;
		}

		
		private void openMenuItem_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog fod = new OpenFileDialog();
			if (DialogResult.OK == fod.ShowDialog(this))
			{
				OpenDocument(fod.FileName);

				this.saveAsMenuItem.Enabled = true;
				this.saveMenuItem.Enabled = true;
				this.closeDocumentMenuItem.Enabled = true;
				this.printPreviewMenuItem.Enabled = true;
			}
		}

		
		public void printPreviewMenuItem_Click(object sender, System.EventArgs e)
		{
			
			string tmpFile = createTempFileName();
			ReportWriter.WriteReport(tmpFile,parameters,staticObjects,dynamicObjects,theDocument);
			DaPrintDocument tmpDocument = new DaPrintDocument(true);
			tmpDocument.setXML(tmpFile);
			tmpDocument.DocRoot = theDocument.DocRoot;
			printPreviewDialog = new PrintPreviewDialog();
			printPreviewDialog.Document = tmpDocument;
			printPreviewDialog.WindowState = FormWindowState.Maximized;
			printPreviewDialog.ShowDialog(this);
			System.IO.File.Delete(tmpFile);
			
		}


		private string createTempFileName()
		{
			string tmpDir = Environment.GetEnvironmentVariable("tmp");
			if (tmpDir == null || tmpDir.Equals(""))
				tmpDir = Application.StartupPath;
			DateTime date = DateTime.Now;
			string dateString = String.Format("{0}{1}{2}_{3}{4}{5}",date.Day,date.Month,date.Year,date.Hour,date.Minute,date.Second);
			return tmpDir + "\\daReport_temp_" + dateString + ".xml";
		}
		
		
		private void saveAsMenuItem_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog fod = new SaveFileDialog();
			fod.Filter = "Xml file|*.xml";
			if (DialogResult.OK == fod.ShowDialog(this))
			{
				ReportWriter.WriteReport(fod.FileName,parameters,staticObjects,dynamicObjects,theDocument);				
				OpenDocument(fod.FileName);
				WriteMessage("Document saved.");				
			}
		}

		
		private void saveMenuItem_Click(object sender, System.EventArgs e)
		{
			if (theFilenameToSave == null)
			{
				saveAsMenuItem_Click(sender,e);
			}
			else
			{
				ReportWriter.WriteReport(theFilenameToSave,parameters,staticObjects,dynamicObjects,theDocument);	
				WriteMessage("Document saved.");
			}
		}

		
		#endregion

		#region About Menu

		private void aboutMenuItem_Click(object sender, System.EventArgs e)
		{
			
			if (aboutFrame==null || aboutFrame.IsDisposed)
				aboutFrame = new AboutFrame();

			aboutFrame.ShowDialog(this);
		}


		#endregion

		#region Options Menu

		private void gridMenuItem_Click(object sender, System.EventArgs e)
		{
			GridDialog gridDialog = new GridDialog();
			gridDialog.ShowGrid = designPane.ShowGrid;
			gridDialog.GridSize = designPane.GridSize;

			if (DialogResult.OK == gridDialog.ShowDialog(this))
			{
				designPane.ShowGrid = gridDialog.ShowGrid;
				designPane.GridSize = gridDialog.GridSize;
			}
		}

		#endregion

		#endregion

		#region Context Menu Event handlers

		#region browserContextMenuGeneral

		private void addParameterGeneral_Click(object sender, System.EventArgs e)
		{
			AddParameter();
		}


		#endregion

		#region browserContextMenuDynamic

		private void addPageNumberMenuItem_Click(object sender, System.EventArgs e)
		{
			int currentNumberOfDynamicObjects = this.dynamicObjects.Length;

			ICustomPaint[] tmp = new ICustomPaint[currentNumberOfDynamicObjects+1];

			Array.Copy(dynamicObjects,0,tmp,0,currentNumberOfDynamicObjects);
			
			TextField txtField = new TextField("name",10,10,200,25,theDocument);
			txtField.Text = "Page : $P{pageNumber}";
			txtField.BorderWidth = 1;
			tmp[currentNumberOfDynamicObjects] = txtField;

			dynamicObjects = tmp;

			objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
			designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);

			objectBrowser.SelectDynamicNode(currentNumberOfDynamicObjects,0);
		}


		private void addTableDynamicMenuItem_Click(object sender, System.EventArgs e)
		{
			int currentNumberOfDynamicObjects = this.dynamicObjects.Length;

			ICustomPaint[] tmp = new ICustomPaint[currentNumberOfDynamicObjects+1];

			Array.Copy(dynamicObjects,0,tmp,0,currentNumberOfDynamicObjects);
			
			StyledTable styledTable = new StyledTable(20,20,200,100,theDocument);
			StyledTableColumn[] kolone = new StyledTableColumn[1];
			kolone[0] = new StyledTableColumn();
			kolone[0].Label = "Dynamic table";
			styledTable.Columns = kolone;
			styledTable.DataSource = "printTable";
			
			tmp[currentNumberOfDynamicObjects] = styledTable;

			dynamicObjects = tmp;

			objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
			designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);

			objectBrowser.SelectDynamicNode(currentNumberOfDynamicObjects,0);
		}
		
		
		private void backOneDynamicMenuItem_Click(object sender, System.EventArgs e)
		{
			int numOfDynamicNodesSelected = 0;
			for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
			{
				TreeNode tmp = (TreeNode) objectBrowser.SelectedNodes[i];
				if (objectBrowser.IsDynamicNode(tmp))
					numOfDynamicNodesSelected++;
			}


			if (numOfDynamicNodesSelected>0)
			{
				int[] nodeIndexes = new int[numOfDynamicNodesSelected];
				
				for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
				{
					TreeNode tmpNode = (TreeNode) objectBrowser.SelectedNodes[i];
					int theIndex = objectBrowser.dynamicContentsNode.Nodes.IndexOf(tmpNode);
										
					nodeIndexes[i] = theIndex<=0 ? theIndex : theIndex-1 ;

					if (theIndex>0)
						SwitchObjects(dynamicObjects,theIndex,theIndex-1);

				} 

				objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
				designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);
				
				for (int i=0;i<numOfDynamicNodesSelected;i++)
				{
					objectBrowser.SelectDynamicNode(nodeIndexes[i],i==0?0:1);
				}
			}						
		}

		
		private void bringForwardDynamicMenuItem_Click(object sender, System.EventArgs e)
		{
			int currentNumberOfDynamicObjects = this.dynamicObjects.Length;

			int numOfDynamicNodesSelected = 0;
			for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
			{
				TreeNode tmp = (TreeNode) objectBrowser.SelectedNodes[i];
				if (objectBrowser.IsDynamicNode(tmp))
					numOfDynamicNodesSelected++;
			}

			if (numOfDynamicNodesSelected>0)
			{
				for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
				{
					TreeNode tmpNode = (TreeNode) objectBrowser.SelectedNodes[i];

					int theIndex = objectBrowser.dynamicContentsNode.Nodes.IndexOf(tmpNode);

					if (theIndex>=objectBrowser.dynamicContentsNode.Nodes.Count-1) return;
			
					ICustomPaint[] tmp = new ICustomPaint[currentNumberOfDynamicObjects];
					tmp[currentNumberOfDynamicObjects-1] = dynamicObjects[theIndex];

					Array.Copy(dynamicObjects,0,tmp,0,theIndex);
					Array.Copy(dynamicObjects,theIndex+1,tmp,theIndex,currentNumberOfDynamicObjects-theIndex-1);
				
					dynamicObjects = tmp;
				}

				objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
				designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);
				
				for (int i=numOfDynamicNodesSelected;i>0;i--)
				{
					objectBrowser.SelectDynamicNode(currentNumberOfDynamicObjects-i,i==numOfDynamicNodesSelected?0:1);
				}
			}
			
		}


		private void deleteDynamicMenuItem_Click(object sender, System.EventArgs e)
		{
			DeleteNodes();
		}
		
		
		private void duplicateDynamicMenuItem_Click(object sender, System.EventArgs e)
		{
			duplicateObjects();
		}
		
		
		private void forwardOneDynamicMenuItem_Click(object sender, System.EventArgs e)
		{
			int numOfDynamicNodesSelected = 0;
			for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
			{
				TreeNode tmp = (TreeNode) objectBrowser.SelectedNodes[i];
				if (objectBrowser.IsDynamicNode(tmp))
					numOfDynamicNodesSelected++;
			}
			
			if (numOfDynamicNodesSelected>0)
			{
				int[] nodeIndexes = new int[numOfDynamicNodesSelected];
				
				for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
				{
					TreeNode tmpNode = (TreeNode) objectBrowser.SelectedNodes[i];
					int theIndex = objectBrowser.dynamicContentsNode.Nodes.IndexOf(tmpNode);
										
					nodeIndexes[i] = theIndex >= objectBrowser.dynamicContentsNode.Nodes.Count-1 ? theIndex : theIndex+1 ;

					if (theIndex<objectBrowser.dynamicContentsNode.Nodes.Count-1)
						SwitchObjects(dynamicObjects,theIndex,theIndex+1);

				} 

				objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
				designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);
				
				for (int i=0;i<numOfDynamicNodesSelected;i++)
				{
					objectBrowser.SelectDynamicNode(nodeIndexes[i],i==0?0:1);
				}
			}	
			
		}


		private void sendToBackDynamicMenuItem_Click(object sender, System.EventArgs e)
		{
			int currentNumberOfDynamicObjects = this.dynamicObjects.Length;

			int numOfDynamicNodesSelected = 0;
			for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
			{
				TreeNode tmp = (TreeNode) objectBrowser.SelectedNodes[i];
				if (objectBrowser.IsDynamicNode(tmp))
					numOfDynamicNodesSelected++;
			}
			
			if (numOfDynamicNodesSelected>0)
			{
				for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
				{
					TreeNode tmpNode = (TreeNode) objectBrowser.SelectedNodes[i];


					int theIndex = objectBrowser.dynamicContentsNode.Nodes.IndexOf(tmpNode);

					if (theIndex<=0) return;
			
					ICustomPaint[] tmp = new ICustomPaint[currentNumberOfDynamicObjects];

					tmp[0] = dynamicObjects[theIndex];

					Array.Copy(dynamicObjects,0,tmp,1,theIndex);
					Array.Copy(dynamicObjects,theIndex+1,tmp,theIndex+1,currentNumberOfDynamicObjects-theIndex-1);

					dynamicObjects = tmp;
				}

				objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
				designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);

				for (int i=numOfDynamicNodesSelected;i>0;i--)				
				{
					objectBrowser.SelectDynamicNode(i-1,i==numOfDynamicNodesSelected?0:1);
				}
			}
		}


		#endregion

		#region browserContextMenuStatic
		
		private void addStaticChartMenuItem_Click(object sender, System.EventArgs e)
		{
			int currentNumberOfStaticObjects = this.staticObjects.Length;

			ICustomPaint[] tmp = new ICustomPaint[currentNumberOfStaticObjects+1];

			Array.Copy(staticObjects,0,tmp,0,currentNumberOfStaticObjects);

			ChartBox chartBox = new ChartBox(20,20,300,200,theDocument);
			chartBox.Title = "Chart Title";
			chartBox.Name = "chart"+currentNumberOfStaticObjects.ToString();

			tmp[currentNumberOfStaticObjects] = chartBox;

			staticObjects = tmp;

			objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
			designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);

			objectBrowser.SelectStaticNode(currentNumberOfStaticObjects,0);
		}
		

		private void addStaticPictureBoxMenuItem_Click(object sender, System.EventArgs e)
		{
			int currentNumberOfStaticObjects = this.staticObjects.Length;

			ICustomPaint[] tmp = new ICustomPaint[currentNumberOfStaticObjects+1];

			Array.Copy(staticObjects,0,tmp,0,currentNumberOfStaticObjects);
			
			daReport.PictureBox pictureBox = new daReport.PictureBox(20,20,125,125,theDocument);
			pictureBox.BorderWidth = 1;
			if ( theDocument.DocRoot != "")
				pictureBox.SetDocumentRoot(theDocument.DocRoot);
			else
				pictureBox.SetDocumentRoot(Application.StartupPath);
			pictureBox.ImageFile = null;
			
			tmp[currentNumberOfStaticObjects] = pictureBox;

			staticObjects = tmp;

			objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
			designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);

			objectBrowser.SelectStaticNode(currentNumberOfStaticObjects,0);

		}


		private void addStaticTextFieldMenuItem_Click(object sender, System.EventArgs e)
		{
			int currentNumberOfStaticObjects = this.staticObjects.Length;

			ICustomPaint[] tmp = new ICustomPaint[currentNumberOfStaticObjects+1];

			Array.Copy(staticObjects,0,tmp,0,currentNumberOfStaticObjects);
			
			TextField txtField = new TextField("name",10,10,100,100,theDocument);
			txtField.Text = "Sample text";
            txtField.Name = "name";
			txtField.BorderWidth = 1;
			tmp[currentNumberOfStaticObjects] = txtField;

			staticObjects = tmp;

			objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
			designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);

			objectBrowser.SelectStaticNode(currentNumberOfStaticObjects,0);

		}


		private void addTableStaticMenuItem_Click(object sender, System.EventArgs e)
		{
			int currentNumberOfStaticObjects = this.staticObjects.Length;

			ICustomPaint[] tmp = new ICustomPaint[currentNumberOfStaticObjects+1];

			Array.Copy(staticObjects,0,tmp,0,currentNumberOfStaticObjects);
			
			StyledTable styledTable = new StyledTable(20,20,200,100,theDocument);
			StyledTableColumn[] kolone = new StyledTableColumn[1];
			kolone[0] = new StyledTableColumn();
			kolone[0].Label = "Static table";
			styledTable.Columns = kolone;
			
			tmp[currentNumberOfStaticObjects] = styledTable;

			staticObjects = tmp;

			objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
			designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);

			objectBrowser.SelectStaticNode(currentNumberOfStaticObjects,0);
		}
		
		
		private void backOneStaticMenuItem_Click(object sender, System.EventArgs e)
		{
			
			int numOfStaticNodesSelected = 0;
			for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
			{
				TreeNode tmp = (TreeNode) objectBrowser.SelectedNodes[i];
				if (objectBrowser.IsStaticNode(tmp))
					numOfStaticNodesSelected++;
			}


			if (numOfStaticNodesSelected>0)
			{
				int[] nodeIndexes = new int[numOfStaticNodesSelected];
				
				for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
				{
					TreeNode tmpNode = (TreeNode) objectBrowser.SelectedNodes[i];
					int theIndex = objectBrowser.staticContentsNode.Nodes.IndexOf(tmpNode);
										
					nodeIndexes[i] = theIndex<=0 ? theIndex : theIndex-1 ;

					if (theIndex>0)
						SwitchObjects(staticObjects,theIndex,theIndex-1);

				} 

				objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
				designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);
				
				for (int i=0;i<numOfStaticNodesSelected;i++)
				{
					objectBrowser.SelectStaticNode(nodeIndexes[i],i==0?0:1);
				}
			}			
		}
		
		
		private void bringForwardStaticMenuItem_Click(object sender, System.EventArgs e)
		{
			int currentNumberOfStaticObjects = this.staticObjects.Length;
			
			int numOfStaticNodesSelected = 0;
			for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
			{
				TreeNode tmp = (TreeNode) objectBrowser.SelectedNodes[i];
				if (objectBrowser.IsStaticNode(tmp))
					numOfStaticNodesSelected++;
			}

			if (numOfStaticNodesSelected>0)
			{
				for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
				{
					TreeNode tmpNode = (TreeNode) objectBrowser.SelectedNodes[i];

					int theIndex = objectBrowser.staticContentsNode.Nodes.IndexOf(tmpNode);

					if (theIndex>=objectBrowser.staticContentsNode.Nodes.Count-1) return;
			
					ICustomPaint[] tmp = new ICustomPaint[currentNumberOfStaticObjects];
					tmp[currentNumberOfStaticObjects-1] = staticObjects[theIndex];

					Array.Copy(staticObjects,0,tmp,0,theIndex);
					Array.Copy(staticObjects,theIndex+1,tmp,theIndex,currentNumberOfStaticObjects-theIndex-1);
				
					staticObjects = tmp;
				}

				objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
				designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);
				
				for (int i=numOfStaticNodesSelected;i>0;i--)
				{
					objectBrowser.SelectStaticNode(currentNumberOfStaticObjects-i,i==numOfStaticNodesSelected?0:1);
				}
			}

		}
		
		
		private void deleteStaticObjectMenuItem_Click(object sender, System.EventArgs e)
		{
			DeleteNodes();
		}
		
		
		private void duplicateStaticMenuItem_Click(object sender, System.EventArgs e)
		{
			duplicateObjects();
		}
		
		
		private void forwardOneStaticMenuItem_Click(object sender, System.EventArgs e)
		{
			int numOfStaticNodesSelected = 0;
			for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
			{
				TreeNode tmp = (TreeNode) objectBrowser.SelectedNodes[i];
				if (objectBrowser.IsStaticNode(tmp))
					numOfStaticNodesSelected++;
			}
			
			if (numOfStaticNodesSelected>0)
			{
				int[] nodeIndexes = new int[numOfStaticNodesSelected];
				
				for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
				{
					TreeNode tmpNode = (TreeNode) objectBrowser.SelectedNodes[i];
					int theIndex = objectBrowser.staticContentsNode.Nodes.IndexOf(tmpNode);
										
					nodeIndexes[i] = theIndex >= objectBrowser.staticContentsNode.Nodes.Count-1 ? theIndex : theIndex+1 ;

					if (theIndex<objectBrowser.staticContentsNode.Nodes.Count-1)
						SwitchObjects(staticObjects,theIndex,theIndex+1);

				} 

				objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
				designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);
				
				for (int i=0;i<numOfStaticNodesSelected;i++)
				{
					objectBrowser.SelectStaticNode(nodeIndexes[i],i==0?0:1);
				}
			}	
			
		}
		
		
		private void sendToBackStaticMenuItem_Click(object sender, System.EventArgs e)
		{
			int currentNumberOfStaticObjects = this.staticObjects.Length;

			int numOfStaticNodesSelected = 0;
			for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
			{
				TreeNode tmp = (TreeNode) objectBrowser.SelectedNodes[i];
				if (objectBrowser.IsStaticNode(tmp))
					numOfStaticNodesSelected++;
			}
			
			if (numOfStaticNodesSelected>0)
			{
				for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
				{
					TreeNode tmpNode = (TreeNode) objectBrowser.SelectedNodes[i];


					int theIndex = objectBrowser.staticContentsNode.Nodes.IndexOf(tmpNode);

					if (theIndex<=0) return;
			
					ICustomPaint[] tmp = new ICustomPaint[currentNumberOfStaticObjects];

					tmp[0] = staticObjects[theIndex];

					Array.Copy(staticObjects,0,tmp,1,theIndex);
					Array.Copy(staticObjects,theIndex+1,tmp,theIndex+1,currentNumberOfStaticObjects-theIndex-1);

					staticObjects = tmp;
				}

				objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
				designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);

				for (int i=numOfStaticNodesSelected;i>0;i--)				
				{
					objectBrowser.SelectStaticNode(i-1,i==numOfStaticNodesSelected?0:1);
				}
			}
	
		}

		
		#endregion

		#endregion

		#region Private Functions

		private void Arrange()
		{
			tabPanel.TabPages.Clear();
		}

		
		private void AddParameter()
		{
			int parameterNumber = parameters.Count;
			parameters.Add( "parameter"+parameterNumber.ToString() );
			
			string[] tmp = new string[parameters.Count];
			for (int i=0;i<parameters.Count;i++)
			{
				tmp[i] = parameters[i].ToString();
			}

			theDocument.Parameters = tmp;
			objectBrowser.SetData (parameters,staticObjects,dynamicObjects);
			propertyGrid.Refresh();
		}

		
		private void CloseDocument()
		{
			theDocument = null;
			dynamicObjects = null;
			staticObjects = null;
			parameters = null;
			propertyGrid.SelectedObject = null;
			objectBrowser.Nodes.Clear();
			tabPanel.TabPages.Clear();

			this.saveAsMenuItem.Enabled = false;
			this.saveMenuItem.Enabled = false;
			this.closeDocumentMenuItem.Enabled = false;
			this.printPreviewMenuItem.Enabled = false;
			
			this.propertyObject.Text = "";
			WriteMessage("Document closed.");



		}
		

		private void DeleteNodes()
		{
			int currentNumberOfStaticObjects = this.staticObjects.Length;
			
			int numOfStaticNodesSelected = 0;
			for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
			{
				TreeNode tmp = (TreeNode) objectBrowser.SelectedNodes[i];
				if (objectBrowser.IsStaticNode(tmp))
					numOfStaticNodesSelected++;
			}

			int currentNumberOfDynamicObjects = this.dynamicObjects.Length;

			int numOfDynamicNodesSelected = 0;
			for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
			{
				TreeNode tmp = (TreeNode) objectBrowser.SelectedNodes[i];
				if (objectBrowser.IsDynamicNode(tmp))
					numOfDynamicNodesSelected++;
			}

			if (numOfStaticNodesSelected>0)
			{
				ICustomPaint[] tmp = new ICustomPaint[currentNumberOfStaticObjects-numOfStaticNodesSelected];
				
				int j=0;
				for (int i=0;i<staticObjects.Length;i++)
				{
					if ( !objectBrowser.SelectedNodes.Contains(objectBrowser.staticContentsNode.Nodes[i]) )
					{
						tmp[j++] = staticObjects[i];
					}
				}

				staticObjects = tmp;				
			}

			if (numOfDynamicNodesSelected>0)
			{
				ICustomPaint[] tmp = new ICustomPaint[currentNumberOfDynamicObjects-numOfDynamicNodesSelected];
				
				int j=0;
				for (int i=0;i<dynamicObjects.Length;i++)
				{
					if ( !objectBrowser.SelectedNodes.Contains(objectBrowser.dynamicContentsNode.Nodes[i]) )
					{
						tmp[j++] = dynamicObjects[i];
					}
				}

				dynamicObjects = tmp;				
			}

			objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
			designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);
			designPane.SelectObject(null);

		}
		
		
		private void DoClose()
		{
			this.Close();
			Application.Exit();
		}
		
		private void duplicateObjects()
		{
			int currentNumberOfStaticObjects = this.staticObjects.Length;
			int currentNumberOfDynamicObjects = this.dynamicObjects.Length;

			int numOfStaticNodesSelected = 0;
			for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
			{
				TreeNode tmp = (TreeNode) objectBrowser.SelectedNodes[i];
				if (objectBrowser.IsStaticNode(tmp))
					numOfStaticNodesSelected++;
			}

			int numOfDynamicNodesSelected = 0;
			for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
			{
				TreeNode tmp = (TreeNode) objectBrowser.SelectedNodes[i];
				if (objectBrowser.IsDynamicNode(tmp))
					numOfDynamicNodesSelected++;
			}

			
			if (numOfStaticNodesSelected>0)
			{
				ICustomPaint[] tmp = new ICustomPaint[currentNumberOfStaticObjects+numOfStaticNodesSelected];

				int j = 0;
				for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
				{
					TreeNode tmpNode = (TreeNode) objectBrowser.SelectedNodes[i];
					
					if (objectBrowser.IsStaticNode(tmpNode))
					{
						ICustomPaint duplicate = null;
						int theIndex = objectBrowser.staticContentsNode.Nodes.IndexOf(tmpNode);
						if (theIndex>=0) 
						{
							duplicate = staticObjects[theIndex].Clone() as ICustomPaint;
							duplicate.X += 10;
							duplicate.Y -= 10;
							tmp[currentNumberOfStaticObjects+j] = duplicate;
							j++;
						}						
					}			
				}

				Array.Copy(staticObjects,0,tmp,0,currentNumberOfStaticObjects);			
				staticObjects = tmp;
			}

			if (numOfDynamicNodesSelected>0)
			{
				ICustomPaint[] tmp = new ICustomPaint[currentNumberOfDynamicObjects+numOfDynamicNodesSelected];

				int j = 0;
				for (int i=0;i<objectBrowser.SelectedNodes.Count;i++)
				{
					TreeNode tmpNode = (TreeNode) objectBrowser.SelectedNodes[i];
					
					if (objectBrowser.IsDynamicNode(tmpNode))
					{
						ICustomPaint duplicate = null;
						int theIndex = objectBrowser.dynamicContentsNode.Nodes.IndexOf(tmpNode);
						if (theIndex>=0) 
						{
							duplicate = dynamicObjects[theIndex].Clone() as ICustomPaint;
							duplicate.X += 10;
							duplicate.Y -= 10;
							tmp[currentNumberOfDynamicObjects+j] = duplicate;
							j++;
						}						
					}			
				}

				Array.Copy(dynamicObjects,0,tmp,0,currentNumberOfDynamicObjects);			
				dynamicObjects = tmp;
			}

			objectBrowser.SetData(parameters,staticObjects,dynamicObjects);
			designPane.SetObjects(staticObjects,dynamicObjects,theDocument.DefaultPageSettings);

			for (int i=numOfStaticNodesSelected;i>0;i--)
			{
				objectBrowser.SelectStaticNode(currentNumberOfStaticObjects + numOfStaticNodesSelected - i, i==numOfStaticNodesSelected?0:1);
			}

			for (int i=numOfDynamicNodesSelected;i>0;i--)
			{
				objectBrowser.SelectDynamicNode(currentNumberOfDynamicObjects + numOfDynamicNodesSelected - i, (i==numOfDynamicNodesSelected && numOfStaticNodesSelected==0)?0:1);
			}
		}

				
		private void SwitchObjects(ICustomPaint[] theObjects,int firstIndex, int secondIndex)
		{
			
			ICustomPaint tmp = theObjects[secondIndex];
			theObjects[secondIndex] = theObjects[firstIndex];
			theObjects[firstIndex] = tmp;
	
		}

		
		private void ResizeDesignPane(object sender)
		{
			designPane.SetSize(theDocument.DefaultPageSettings);
			RepeatAlignments(theDocument);
		}
		
		
		#endregion
		
		#region Private Properties

		private int FindDynamicObject(ICustomPaint t)
		{
			for (int i=0;i<dynamicObjects.Length;i++)
			{
				if ( dynamicObjects[i] == t )
					return i;
			}

			return -1;
		}
		

		private int FindStaticObject(ICustomPaint t)
		{
			for (int i=0;i<staticObjects.Length;i++)
			{
				if ( staticObjects[i] == t )
					return i;
			}

			return -1;
		}


		#endregion

        private void MainFrame_Load(object sender, EventArgs e)
        {

        }

		
	
	}
}
