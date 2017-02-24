namespace HR_Test
{
    partial class frmReportEdit_T
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                _fmFind.zedGraphControl.Dock = System.Windows.Forms.DockStyle.Fill;
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportEdit_T));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbtnExit = new System.Windows.Forms.ToolStripButton();
            this.tsbtnPrintP = new System.Windows.Forms.ToolStripButton();
            this.tsbtnMinimize = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnPrinterSet = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.addText = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAddPic = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.delControl = new System.Windows.Forms.ToolStripMenuItem();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnPrint,
            this.tsbtnExit,
            this.tsbtnPrintP,
            this.tsbtnMinimize,
            this.toolStripLabel2,
            this.tsbtnPrinterSet,
            this.tsbtnSave,
            this.addText,
            this.tsbtnAddPic});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1159, 75);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnPrint
            // 
            this.tsbtnPrint.Image = global::HR_Test.Properties.Resources.print;
            this.tsbtnPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrint.Name = "tsbtnPrint";
            this.tsbtnPrint.Size = new System.Drawing.Size(67, 72);
            this.tsbtnPrint.Text = "打印预览";
            this.tsbtnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnPrint.Click += new System.EventHandler(this.tsbtnPrint_Click);
            // 
            // tsbtnExit
            // 
            this.tsbtnExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnExit.Image = global::HR_Test.Properties.Resources.return1;
            this.tsbtnExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExit.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tsbtnExit.Name = "tsbtnExit";
            this.tsbtnExit.Size = new System.Drawing.Size(52, 72);
            this.tsbtnExit.Text = "退出";
            this.tsbtnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnExit.Click += new System.EventHandler(this.tsbtnExit_Click);
            // 
            // tsbtnPrintP
            // 
            this.tsbtnPrintP.Image = global::HR_Test.Properties.Resources.printer;
            this.tsbtnPrintP.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnPrintP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrintP.Name = "tsbtnPrintP";
            this.tsbtnPrintP.Size = new System.Drawing.Size(52, 72);
            this.tsbtnPrintP.Text = "打印";
            this.tsbtnPrintP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnPrintP.Click += new System.EventHandler(this.tsbtnPrintP_Click);
            // 
            // tsbtnMinimize
            // 
            this.tsbtnMinimize.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnMinimize.Image = global::HR_Test.Properties.Resources.minimize_2;
            this.tsbtnMinimize.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnMinimize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnMinimize.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tsbtnMinimize.Name = "tsbtnMinimize";
            this.tsbtnMinimize.Size = new System.Drawing.Size(53, 72);
            this.tsbtnMinimize.Text = "最小化";
            this.tsbtnMinimize.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnMinimize.Click += new System.EventHandler(this.tsbtnMinimize_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel2.AutoSize = false;
            this.toolStripLabel2.Image = global::HR_Test.Properties.Resources.logo1;
            this.toolStripLabel2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(260, 66);
            // 
            // tsbtnPrinterSet
            // 
            this.tsbtnPrinterSet.Image = global::HR_Test.Properties.Resources.setprinter;
            this.tsbtnPrinterSet.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnPrinterSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrinterSet.Name = "tsbtnPrinterSet";
            this.tsbtnPrinterSet.Size = new System.Drawing.Size(81, 72);
            this.tsbtnPrinterSet.Text = "设置打印机";
            this.tsbtnPrinterSet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnPrinterSet.Click += new System.EventHandler(this.tsbtnPrinterSet_Click);
            // 
            // tsbtnSave
            // 
            this.tsbtnSave.Image = global::HR_Test.Properties.Resources.save;
            this.tsbtnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(67, 72);
            this.tsbtnSave.Text = "保存修改";
            this.tsbtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnSave.Click += new System.EventHandler(this.tsbtnSave_Click);
            // 
            // addText
            // 
            this.addText.Image = global::HR_Test.Properties.Resources.T48;
            this.addText.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.addText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addText.Name = "addText";
            this.addText.Size = new System.Drawing.Size(81, 72);
            this.addText.Text = "添加文本框";
            this.addText.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.addText.Click += new System.EventHandler(this.tsbtnAddText_Click);
            // 
            // tsbtnAddPic
            // 
            this.tsbtnAddPic.Image = global::HR_Test.Properties.Resources.pic48;
            this.tsbtnAddPic.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnAddPic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddPic.Name = "tsbtnAddPic";
            this.tsbtnAddPic.Size = new System.Drawing.Size(67, 72);
            this.tsbtnAddPic.Text = "添加图片";
            this.tsbtnAddPic.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnAddPic.Visible = false;
            this.tsbtnAddPic.Click += new System.EventHandler(this.toolStripButton2_Click_2);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(226, 75);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(933, 810);
            this.panel1.TabIndex = 2;
            this.panel1.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(50, 10);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(50);
            this.panel2.Size = new System.Drawing.Size(832, 1169);
            this.panel2.TabIndex = 3;
            this.panel2.Click += new System.EventHandler(this.panel2_Click);
            this.panel2.Resize += new System.EventHandler(this.panel2_Resize);
            this.panel2.MouseEnter += new System.EventHandler(this.panel2_MouseEnter);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 75);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(226, 810);
            this.panel3.TabIndex = 3;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delControl});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(99, 26);
            // 
            // delControl
            // 
            this.delControl.Name = "delControl";
            this.delControl.Size = new System.Drawing.Size(98, 22);
            this.delControl.Text = "删除";
            this.delControl.Click += new System.EventHandler(this.delControl_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.AllowSelection = true;
            this.printDialog1.AllowSomePages = true;
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // frmReportEdit_T
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1159, 885);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmReportEdit_T";
            this.ShowInTaskbar = false;
            this.Text = "frmReportEdit";
            this.Load += new System.EventHandler(this.frmReportEdit_Load);
            this.MouseEnter += new System.EventHandler(this.frmReportEdit_T_MouseEnter);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmReportEdit_T_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnExit;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripButton tsbtnPrinterSet;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStripButton tsbtnSave;
        private System.Windows.Forms.ToolStripButton tsbtnMinimize;
        private System.Windows.Forms.ToolStripButton addText;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem delControl;
        private System.Windows.Forms.ToolStripButton tsbtnAddPic;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.ToolStripButton tsbtnPrintP;
        private ZedGraph.ZedGraphControl z;
        public System.Windows.Forms.ToolStripButton tsbtnPrint;
    }
}