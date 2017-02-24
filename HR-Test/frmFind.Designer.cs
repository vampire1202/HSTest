namespace HR_Test
{
    partial class frmFind
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnFind = new System.Windows.Forms.ToolStripButton();
            this.tsbtnEditReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.tsbtnMinimize = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnPrintPreview = new System.Windows.Forms.ToolStripButton();
            this.tsbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFind = new Glass.GlassButton();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsbtnRescale = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtTestSampleNo = new System.Windows.Forms.TextBox();
            this.txtTestNo = new System.Windows.Forms.TextBox();
            this.cmbTestStandard = new System.Windows.Forms.ComboBox();
            this.cmbTestType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStandardTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.zedGraphControl = new ZedGraph.ZedGraphControl();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cmbYr = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.cmbXr = new System.Windows.Forms.ToolStripComboBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnFind,
            this.tsbtnEditReport,
            this.toolStripButton3,
            this.tsbtnMinimize,
            this.toolStripLabel2,
            this.tsbtnPrintPreview,
            this.tsbtnPrint});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1129, 75);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnFind
            // 
            this.tsbtnFind.Image = global::HR_Test.Properties.Resources.find;
            this.tsbtnFind.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnFind.Margin = new System.Windows.Forms.Padding(0, 1, 8, 2);
            this.tsbtnFind.Name = "tsbtnFind";
            this.tsbtnFind.Size = new System.Drawing.Size(52, 72);
            this.tsbtnFind.Text = "查询";
            this.tsbtnFind.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // tsbtnEditReport
            // 
            this.tsbtnEditReport.Image = global::HR_Test.Properties.Resources.edit;
            this.tsbtnEditReport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnEditReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnEditReport.Margin = new System.Windows.Forms.Padding(0, 1, 8, 2);
            this.tsbtnEditReport.Name = "tsbtnEditReport";
            this.tsbtnEditReport.Size = new System.Drawing.Size(67, 72);
            this.tsbtnEditReport.Text = "编辑报告";
            this.tsbtnEditReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnEditReport.Click += new System.EventHandler(this.tsbtnEditReport_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripButton3.Image = global::HR_Test.Properties.Resources.return1;
            this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(52, 72);
            this.toolStripButton3.Text = "退出";
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // tsbtnMinimize
            // 
            this.tsbtnMinimize.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnMinimize.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsbtnMinimize.Image = global::HR_Test.Properties.Resources.minimize;
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
            // tsbtnPrintPreview
            // 
            this.tsbtnPrintPreview.Image = global::HR_Test.Properties.Resources.print;
            this.tsbtnPrintPreview.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrintPreview.Margin = new System.Windows.Forms.Padding(0, 1, 8, 2);
            this.tsbtnPrintPreview.Name = "tsbtnPrintPreview";
            this.tsbtnPrintPreview.Size = new System.Drawing.Size(67, 72);
            this.tsbtnPrintPreview.Text = "打印预览";
            this.tsbtnPrintPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnPrintPreview.Click += new System.EventHandler(this.tsbtnPrintPreview_Click);
            // 
            // tsbtnPrint
            // 
            this.tsbtnPrint.Image = global::HR_Test.Properties.Resources.printer;
            this.tsbtnPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrint.Margin = new System.Windows.Forms.Padding(0, 1, 8, 2);
            this.tsbtnPrint.Name = "tsbtnPrint";
            this.tsbtnPrint.Size = new System.Drawing.Size(52, 72);
            this.tsbtnPrint.Text = "打印";
            this.tsbtnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnPrint.Click += new System.EventHandler(this.tsbtnPrint_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.toolStrip3);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.txtTestSampleNo);
            this.groupBox1.Controls.Add(this.txtTestNo);
            this.groupBox1.Controls.Add(this.cmbTestStandard);
            this.groupBox1.Controls.Add(this.cmbTestType);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblStandardTitle);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(0, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(245, 561);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // btnFind
            // 
            this.btnFind.BackColor = System.Drawing.Color.Snow;
            this.btnFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnFind.Font = new System.Drawing.Font("宋体", 10.5F);
            this.btnFind.ForeColor = System.Drawing.Color.Black;
            this.btnFind.GlowColor = System.Drawing.Color.DeepSkyBlue;
            this.btnFind.Image = global::HR_Test.Properties.Resources.find;
            this.btnFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFind.Location = new System.Drawing.Point(134, 351);
            this.btnFind.Name = "btnFind";
            this.btnFind.ShineColor = System.Drawing.Color.WhiteSmoke;
            this.btnFind.Size = new System.Drawing.Size(95, 61);
            this.btnFind.TabIndex = 137;
            this.btnFind.Text = "查询";
            this.btnFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.toolStrip3.AutoSize = false;
            this.toolStrip3.BackColor = System.Drawing.SystemColors.Info;
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.tsbtnRescale,
            this.toolStripButton4,
            this.toolStripButton5});
            this.toolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip3.Location = new System.Drawing.Point(67, 415);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(108, 146);
            this.toolStrip3.TabIndex = 5;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::HR_Test.Properties.Resources.zoomin;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(52, 52);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsbtnRescale
            // 
            this.tsbtnRescale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRescale.Image = global::HR_Test.Properties.Resources.zoomone;
            this.tsbtnRescale.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnRescale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRescale.Name = "tsbtnRescale";
            this.tsbtnRescale.Size = new System.Drawing.Size(52, 52);
            this.tsbtnRescale.Text = "toolStripButton2";
            this.tsbtnRescale.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::HR_Test.Properties.Resources.T48;
            this.toolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(52, 52);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::HR_Test.Properties.Resources.shi;
            this.toolStripButton5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(52, 52);
            this.toolStripButton5.Text = "toolStripButton5";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(12, 311);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(217, 23);
            this.dateTimePicker2.TabIndex = 3;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 264);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(217, 23);
            this.dateTimePicker1.TabIndex = 3;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // txtTestSampleNo
            // 
            this.txtTestSampleNo.Location = new System.Drawing.Point(12, 217);
            this.txtTestSampleNo.Name = "txtTestSampleNo";
            this.txtTestSampleNo.Size = new System.Drawing.Size(217, 23);
            this.txtTestSampleNo.TabIndex = 2;
            this.txtTestSampleNo.TextChanged += new System.EventHandler(this.txtTestSampleNo_TextChanged);
            // 
            // txtTestNo
            // 
            this.txtTestNo.Location = new System.Drawing.Point(12, 170);
            this.txtTestNo.Name = "txtTestNo";
            this.txtTestNo.Size = new System.Drawing.Size(217, 23);
            this.txtTestNo.TabIndex = 2;
            this.txtTestNo.TextChanged += new System.EventHandler(this.txtTestNo_TextChanged);
            // 
            // cmbTestStandard
            // 
            this.cmbTestStandard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTestStandard.FormattingEnabled = true;
            this.cmbTestStandard.Location = new System.Drawing.Point(12, 47);
            this.cmbTestStandard.Name = "cmbTestStandard";
            this.cmbTestStandard.Size = new System.Drawing.Size(217, 22);
            this.cmbTestStandard.TabIndex = 1;
            this.cmbTestStandard.SelectedIndexChanged += new System.EventHandler(this.cmbTestStandard_SelectedIndexChanged);
            // 
            // cmbTestType
            // 
            this.cmbTestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTestType.FormattingEnabled = true;
            this.cmbTestType.Location = new System.Drawing.Point(12, 119);
            this.cmbTestType.Name = "cmbTestType";
            this.cmbTestType.Size = new System.Drawing.Size(217, 22);
            this.cmbTestType.TabIndex = 1;
            this.cmbTestType.SelectedIndexChanged += new System.EventHandler(this.cmbTestType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 292);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "至";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 245);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "试验日期:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "试样编号:";
            // 
            // lblStandardTitle
            // 
            this.lblStandardTitle.AutoSize = true;
            this.lblStandardTitle.ForeColor = System.Drawing.Color.Navy;
            this.lblStandardTitle.Location = new System.Drawing.Point(12, 72);
            this.lblStandardTitle.Name = "lblStandardTitle";
            this.lblStandardTitle.Size = new System.Drawing.Size(0, 14);
            this.lblStandardTitle.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "试验编号:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 0;
            this.label6.Text = "试验标准:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "试验类型:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.zedGraphControl);
            this.groupBox2.Controls.Add(this.toolStrip2);
            this.groupBox2.Controls.Add(this.dataGridView);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(245, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(884, 561);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "试验数据";
            // 
            // zedGraphControl
            // 
            this.zedGraphControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl.Location = new System.Drawing.Point(3, 221);
            this.zedGraphControl.Margin = new System.Windows.Forms.Padding(0);
            this.zedGraphControl.Name = "zedGraphControl";
            this.zedGraphControl.ScrollGrace = 0;
            this.zedGraphControl.ScrollMaxX = 0;
            this.zedGraphControl.ScrollMaxY = 0;
            this.zedGraphControl.ScrollMaxY2 = 0;
            this.zedGraphControl.ScrollMinX = 0;
            this.zedGraphControl.ScrollMinY = 0;
            this.zedGraphControl.ScrollMinY2 = 0;
            this.zedGraphControl.Size = new System.Drawing.Size(878, 337);
            this.zedGraphControl.TabIndex = 1;
            this.zedGraphControl.MouseEnter += new System.EventHandler(this.zedGraphControl_MouseEnter);
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cmbYr,
            this.toolStripLabel3,
            this.cmbXr});
            this.toolStrip2.Location = new System.Drawing.Point(3, 165);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(878, 56);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "Y:";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(119, 53);
            this.toolStripLabel1.Text = "坐标轴数据    Y:";
            // 
            // cmbYr
            // 
            this.cmbYr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYr.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbYr.Items.AddRange(new object[] {
            "-空-",
            "负荷",
            "应力",
            "变形",
            "位移"});
            this.cmbYr.Name = "cmbYr";
            this.cmbYr.Size = new System.Drawing.Size(100, 56);
            this.cmbYr.SelectedIndexChanged += new System.EventHandler(this.cmbYr_SelectedIndexChanged);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(49, 53);
            this.toolStripLabel3.Text = "    X:";
            // 
            // cmbXr
            // 
            this.cmbXr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbXr.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbXr.Items.AddRange(new object[] {
            "-空-",
            "时间",
            "位移",
            "应变",
            "变形",
            "应力"});
            this.cmbXr.Name = "cmbXr";
            this.cmbXr.Size = new System.Drawing.Size(100, 56);
            this.cmbXr.SelectedIndexChanged += new System.EventHandler(this.cmbXr_SelectedIndexChanged);
            this.cmbXr.Click += new System.EventHandler(this.cmbXr_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.dataGridView.Location = new System.Drawing.Point(3, 19);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.ShowEditingIcon = false;
            this.dataGridView.Size = new System.Drawing.Size(878, 146);
            this.dataGridView.TabIndex = 3;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView.MouseEnter += new System.EventHandler(this.dataGridView_MouseEnter);
            // 
            // frmFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 636);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmFind";
            this.ShowInTaskbar = false;
            this.Text = "查询与打印";
            this.Load += new System.EventHandler(this.frmFind_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnFind;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox txtTestSampleNo;
        private System.Windows.Forms.TextBox txtTestNo;
        private System.Windows.Forms.ComboBox cmbTestType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton tsbtnPrint;
        private System.Windows.Forms.ToolStripButton tsbtnEditReport;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton tsbtnRescale;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cmbYr;
        private System.Windows.Forms.ToolStripComboBox cmbXr;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton tsbtnMinimize;
        private Glass.GlassButton btnFind;
        private System.Windows.Forms.ToolStripButton tsbtnPrintPreview;
        public ZedGraph.ZedGraphControl zedGraphControl;
        private System.Windows.Forms.ComboBox cmbTestStandard;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblStandardTitle;
    }
}