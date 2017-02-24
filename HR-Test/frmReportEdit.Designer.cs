namespace HR_Test
{
    partial class frmReportEdit
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbtnExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsbtnMinimize = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnPrinterSet = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSave = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridViewAver = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.mainTitle = new System.Windows.Forms.Label();
            this.childTitle = new System.Windows.Forms.Label();
            this.lblLine2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblAssessor = new System.Windows.Forms.Label();
            this.lblTester = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.lblLine1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnPrint,
            this.tsbtnExit,
            this.toolStripButton1,
            this.tsbtnMinimize,
            this.toolStripLabel2,
            this.tsbtnPrinterSet,
            this.tsbtnSave});
            this.toolStrip1.Location = new System.Drawing.Point(5, 5);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1132, 75);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnPrint
            // 
            this.tsbtnPrint.Image = global::HR_Test.Properties.Resources.print;
            this.tsbtnPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrint.Name = "tsbtnPrint";
            this.tsbtnPrint.Size = new System.Drawing.Size(92, 72);
            this.tsbtnPrint.Text = "打印预览";
            this.tsbtnPrint.Click += new System.EventHandler(this.tsbtnPrint_Click);
            // 
            // tsbtnExit
            // 
            this.tsbtnExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnExit.Image = global::HR_Test.Properties.Resources.return1;
            this.tsbtnExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExit.Name = "tsbtnExit";
            this.tsbtnExit.Size = new System.Drawing.Size(68, 72);
            this.tsbtnExit.Text = "退出";
            this.tsbtnExit.Click += new System.EventHandler(this.tsbtnExit_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::HR_Test.Properties.Resources.printer;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(68, 72);
            this.toolStripButton1.Text = "打印";
            // 
            // tsbtnMinimize
            // 
            this.tsbtnMinimize.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnMinimize.Image = global::HR_Test.Properties.Resources.minimize;
            this.tsbtnMinimize.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnMinimize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnMinimize.Name = "tsbtnMinimize";
            this.tsbtnMinimize.Size = new System.Drawing.Size(80, 72);
            this.tsbtnMinimize.Text = "最小化";
            this.tsbtnMinimize.Click += new System.EventHandler(this.tsbtnMinimize_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel2.AutoSize = false;
            this.toolStripLabel2.Image = global::HR_Test.Properties.Resources.logo1;
            this.toolStripLabel2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(260, 47);
            // 
            // tsbtnPrinterSet
            // 
            this.tsbtnPrinterSet.Image = global::HR_Test.Properties.Resources.setprinter;
            this.tsbtnPrinterSet.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnPrinterSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrinterSet.Name = "tsbtnPrinterSet";
            this.tsbtnPrinterSet.Size = new System.Drawing.Size(104, 72);
            this.tsbtnPrinterSet.Text = "设置打印机";
            this.tsbtnPrinterSet.Click += new System.EventHandler(this.tsbtnPrinterSet_Click);
            // 
            // tsbtnSave
            // 
            this.tsbtnSave.Image = global::HR_Test.Properties.Resources.save;
            this.tsbtnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSave.Name = "tsbtnSave";
            this.tsbtnSave.Size = new System.Drawing.Size(92, 72);
            this.tsbtnSave.Text = "保存修改";
            this.tsbtnSave.Click += new System.EventHandler(this.tsbtnSave_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(231, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(906, 689);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dataGridViewAver);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.mainTitle);
            this.panel2.Controls.Add(this.childTitle);
            this.panel2.Controls.Add(this.lblLine2);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.lblDate);
            this.panel2.Controls.Add(this.lblAssessor);
            this.panel2.Controls.Add(this.lblTester);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.dataGridView);
            this.panel2.Controls.Add(this.flowLayoutPanel);
            this.panel2.Controls.Add(this.pictureBox);
            this.panel2.Controls.Add(this.lblLine1);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(50, 10);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(50);
            this.panel2.Size = new System.Drawing.Size(832, 1169);
            this.panel2.TabIndex = 3;
            this.panel2.Resize += new System.EventHandler(this.panel2_Resize);
            // 
            // dataGridViewAver
            // 
            this.dataGridViewAver.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewAver.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAver.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridViewAver.Location = new System.Drawing.Point(64, 555);
            this.dataGridViewAver.Name = "dataGridViewAver";
            this.dataGridViewAver.RowTemplate.Height = 23;
            this.dataGridViewAver.Size = new System.Drawing.Size(700, 48);
            this.dataGridViewAver.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(41, 625);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(764, 10);
            this.label7.TabIndex = 22;
            this.label7.Text = "—————————————————————————————————————————————————————————————————————";
            // 
            // mainTitle
            // 
            this.mainTitle.Font = new System.Drawing.Font("黑体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mainTitle.Location = new System.Drawing.Point(43, 50);
            this.mainTitle.Name = "mainTitle";
            this.mainTitle.Size = new System.Drawing.Size(762, 42);
            this.mainTitle.TabIndex = 19;
            this.mainTitle.Text = "设置主标题";
            this.mainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // childTitle
            // 
            this.childTitle.Font = new System.Drawing.Font("宋体", 12F);
            this.childTitle.Location = new System.Drawing.Point(43, 102);
            this.childTitle.Name = "childTitle";
            this.childTitle.Size = new System.Drawing.Size(762, 28);
            this.childTitle.TabIndex = 18;
            this.childTitle.Text = "这里设置副标题";
            this.childTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLine2
            // 
            this.lblLine2.AutoSize = true;
            this.lblLine2.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLine2.Location = new System.Drawing.Point(41, 1061);
            this.lblLine2.Name = "lblLine2";
            this.lblLine2.Size = new System.Drawing.Size(764, 10);
            this.lblLine2.TabIndex = 17;
            this.lblLine2.Text = "—————————————————————————————————————————————————————————————————————";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("隶书", 14F);
            this.label10.Location = new System.Drawing.Point(509, 1097);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 19);
            this.label10.TabIndex = 16;
            this.label10.Text = "报告日期:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("隶书", 14F);
            this.label9.Location = new System.Drawing.Point(293, 1097);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 19);
            this.label9.TabIndex = 15;
            this.label9.Text = "审核员:";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("隶书", 14F);
            this.lblDate.Location = new System.Drawing.Point(606, 1098);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(139, 19);
            this.lblDate.TabIndex = 14;
            this.lblDate.Text = "2011年12月1日";
            // 
            // lblAssessor
            // 
            this.lblAssessor.AutoSize = true;
            this.lblAssessor.Font = new System.Drawing.Font("隶书", 14F);
            this.lblAssessor.Location = new System.Drawing.Point(369, 1098);
            this.lblAssessor.Name = "lblAssessor";
            this.lblAssessor.Size = new System.Drawing.Size(49, 19);
            this.lblAssessor.TabIndex = 14;
            this.lblAssessor.Text = "李四";
            // 
            // lblTester
            // 
            this.lblTester.AutoSize = true;
            this.lblTester.Font = new System.Drawing.Font("隶书", 14F);
            this.lblTester.Location = new System.Drawing.Point(152, 1096);
            this.lblTester.Name = "lblTester";
            this.lblTester.Size = new System.Drawing.Size(49, 19);
            this.lblTester.TabIndex = 14;
            this.lblTester.Text = "张三";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("隶书", 14F);
            this.label8.Location = new System.Drawing.Point(72, 1095);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 19);
            this.label8.TabIndex = 14;
            this.label8.Text = "试验员:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("隶书", 14F);
            this.label6.Location = new System.Drawing.Point(53, 606);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 19);
            this.label6.TabIndex = 12;
            this.label6.Text = "试验曲线";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("隶书", 14F);
            this.label5.Location = new System.Drawing.Point(53, 283);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "试验结果";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("隶书", 14F);
            this.label3.Location = new System.Drawing.Point(53, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 19);
            this.label3.TabIndex = 9;
            this.label3.Text = "基本参数";
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView.Location = new System.Drawing.Point(64, 315);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(700, 237);
            this.dataGridView.TabIndex = 6;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel.Location = new System.Drawing.Point(64, 171);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(700, 109);
            this.flowLayoutPanel.TabIndex = 5;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(63, 638);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(701, 405);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // lblLine1
            // 
            this.lblLine1.AutoSize = true;
            this.lblLine1.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLine1.Location = new System.Drawing.Point(41, 158);
            this.lblLine1.Name = "lblLine1";
            this.lblLine1.Size = new System.Drawing.Size(764, 10);
            this.lblLine1.TabIndex = 10;
            this.lblLine1.Text = "—————————————————————————————————————————————————————————————————————";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(41, 302);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(764, 10);
            this.label4.TabIndex = 21;
            this.label4.Text = "—————————————————————————————————————————————————————————————————————";
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(5, 80);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(226, 689);
            this.panel3.TabIndex = 3;
            // 
            // frmReportEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 774);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmReportEdit";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ShowInTaskbar = false;
            this.Text = "frmReportEdit";
            this.Load += new System.EventHandler(this.frmReportEdit_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnPrint;
        private System.Windows.Forms.ToolStripButton tsbtnExit;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripButton tsbtnPrinterSet;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblLine1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblLine2;
        private System.Windows.Forms.Label childTitle;
        private System.Windows.Forms.Label mainTitle;
        public System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolStripButton tsbtnSave;
        private System.Windows.Forms.Label lblAssessor;
        private System.Windows.Forms.Label lblTester;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.DataGridView dataGridViewAver;
        public System.Windows.Forms.DataGridView dataGridView;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.ToolStripButton tsbtnMinimize;
    }
}