namespace HR_Test
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panelContainer = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbtnExit = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAccess = new System.Windows.Forms.ToolStripButton();
            this.palMain = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnMinimize = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gbtnParameter = new Glass.GlassButton();
            this.gbtnHelp = new Glass.GlassButton();
            this.gbtnTest = new Glass.GlassButton();
            this.gbtnTestMethod = new Glass.GlassButton();
            this.gbtnReport = new Glass.GlassButton();
            this.glassButton4 = new Glass.GlassButton();
            this.glassButton8 = new Glass.GlassButton();
            this.gbtnSet = new Glass.GlassButton();
            this.panelContainer.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.palMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.White;
            this.panelContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContainer.Controls.Add(this.listView1);
            this.panelContainer.Controls.Add(this.toolStrip2);
            this.panelContainer.Controls.Add(this.palMain);
            this.panelContainer.Controls.Add(this.panel1);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 10);
            this.panelContainer.Margin = new System.Windows.Forms.Padding(20, 18, 20, 18);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(998, 683);
            this.panelContainer.TabIndex = 2;
            this.panelContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContainer_Paint);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(38, 513);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(396, 75);
            this.listView1.TabIndex = 15;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.Visible = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.BackColor = System.Drawing.Color.LightGray;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnExit,
            this.tsbtnAccess});
            this.toolStrip2.Location = new System.Drawing.Point(0, 592);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(996, 89);
            this.toolStrip2.TabIndex = 18;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbtnExit
            // 
            this.tsbtnExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnExit.AutoSize = false;
            this.tsbtnExit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tsbtnExit.Font = new System.Drawing.Font("宋体", 10.5F);
            this.tsbtnExit.ForeColor = System.Drawing.Color.Crimson;
            this.tsbtnExit.Image = global::HR_Test.Properties.Resources.application_exit;
            this.tsbtnExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExit.Margin = new System.Windows.Forms.Padding(0, 1, 5, 5);
            this.tsbtnExit.Name = "tsbtnExit";
            this.tsbtnExit.Size = new System.Drawing.Size(118, 85);
            this.tsbtnExit.Text = "退出";
            this.tsbtnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbtnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnExit.Click += new System.EventHandler(this.tsbtnExit_Click);
            // 
            // tsbtnAccess
            // 
            this.tsbtnAccess.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnAccess.AutoSize = false;
            this.tsbtnAccess.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tsbtnAccess.Font = new System.Drawing.Font("宋体", 10.5F);
            this.tsbtnAccess.ForeColor = System.Drawing.Color.Blue;
            this.tsbtnAccess.Image = global::HR_Test.Properties.Resources.usb;
            this.tsbtnAccess.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnAccess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAccess.Margin = new System.Windows.Forms.Padding(0, 1, 5, 5);
            this.tsbtnAccess.Name = "tsbtnAccess";
            this.tsbtnAccess.Size = new System.Drawing.Size(118, 85);
            this.tsbtnAccess.Text = "连接";
            this.tsbtnAccess.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbtnAccess.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbtnAccess.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnAccess.Click += new System.EventHandler(this.btnAccess_Click);
            // 
            // palMain
            // 
            this.palMain.BackColor = System.Drawing.Color.Transparent;
            this.palMain.Controls.Add(this.gbtnParameter);
            this.palMain.Controls.Add(this.gbtnHelp);
            this.palMain.Controls.Add(this.gbtnTest);
            this.palMain.Controls.Add(this.gbtnTestMethod);
            this.palMain.Controls.Add(this.gbtnReport);
            this.palMain.Controls.Add(this.glassButton4);
            this.palMain.Controls.Add(this.glassButton8);
            this.palMain.Controls.Add(this.gbtnSet);
            this.palMain.Location = new System.Drawing.Point(20, 143);
            this.palMain.Name = "palMain";
            this.palMain.Size = new System.Drawing.Size(857, 364);
            this.palMain.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(996, 120);
            this.panel1.TabIndex = 16;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnMinimize,
            this.toolStripLabel1,
            this.toolStripLabel2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(996, 89);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnMinimize
            // 
            this.tsbtnMinimize.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnMinimize.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbtnMinimize.ForeColor = System.Drawing.Color.Black;
            this.tsbtnMinimize.Image = global::HR_Test.Properties.Resources.minimize_2;
            this.tsbtnMinimize.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnMinimize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnMinimize.Name = "tsbtnMinimize";
            this.tsbtnMinimize.Size = new System.Drawing.Size(53, 86);
            this.tsbtnMinimize.Text = "最小化";
            this.tsbtnMinimize.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnMinimize.Click += new System.EventHandler(this.tsbtnMinimize_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.BackgroundImage = global::HR_Test.Properties.Resources.hstest;
            this.toolStripLabel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.toolStripLabel1.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(158, 86);
            this.toolStripLabel1.Text = "              ";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel2.AutoSize = false;
            this.toolStripLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripLabel2.Image = global::HR_Test.Properties.Resources.logo;
            this.toolStripLabel2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(388, 86);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::HR_Test.Properties.Resources.bk2;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(998, 10);
            this.panel2.TabIndex = 19;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::HR_Test.Properties.Resources.bk2;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 693);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(998, 8);
            this.panel3.TabIndex = 21;
            // 
            // gbtnParameter
            // 
            this.gbtnParameter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(78)))), ((int)(((byte)(133)))));
            this.gbtnParameter.Font = new System.Drawing.Font("迷你简隶书", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbtnParameter.GlowColor = System.Drawing.Color.White;
            this.gbtnParameter.Image = global::HR_Test.Properties.Resources._20110704100834860_easyicon_cn_128;
            this.gbtnParameter.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.gbtnParameter.InnerBorderColor = System.Drawing.Color.LightSkyBlue;
            this.gbtnParameter.Location = new System.Drawing.Point(250, 14);
            this.gbtnParameter.Name = "gbtnParameter";
            this.gbtnParameter.OuterBorderColor = System.Drawing.Color.SteelBlue;
            this.gbtnParameter.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.gbtnParameter.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(78)))), ((int)(((byte)(133)))));
            this.gbtnParameter.Size = new System.Drawing.Size(123, 134);
            this.gbtnParameter.TabIndex = 10;
            this.gbtnParameter.Text = "参数输入";
            this.gbtnParameter.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.gbtnParameter.Click += new System.EventHandler(this.gbtnParameter_Click);
            this.gbtnParameter.MouseEnter += new System.EventHandler(this.gbtnTestMethod_MouseEnter);
            this.gbtnParameter.MouseLeave += new System.EventHandler(this.gbtnTestMethod_MouseLeave);
            // 
            // gbtnHelp
            // 
            this.gbtnHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(78)))), ((int)(((byte)(133)))));
            this.gbtnHelp.Font = new System.Drawing.Font("迷你简隶书", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbtnHelp.GlowColor = System.Drawing.Color.White;
            this.gbtnHelp.Image = global::HR_Test.Properties.Resources.help2;
            this.gbtnHelp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.gbtnHelp.InnerBorderColor = System.Drawing.Color.LightSkyBlue;
            this.gbtnHelp.Location = new System.Drawing.Point(482, 216);
            this.gbtnHelp.Name = "gbtnHelp";
            this.gbtnHelp.OuterBorderColor = System.Drawing.Color.SteelBlue;
            this.gbtnHelp.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.gbtnHelp.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(78)))), ((int)(((byte)(133)))));
            this.gbtnHelp.Size = new System.Drawing.Size(123, 134);
            this.gbtnHelp.TabIndex = 10;
            this.gbtnHelp.Text = "帮助";
            this.gbtnHelp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.gbtnHelp.MouseEnter += new System.EventHandler(this.gbtnTestMethod_MouseEnter);
            this.gbtnHelp.MouseLeave += new System.EventHandler(this.gbtnTestMethod_MouseLeave);
            // 
            // gbtnTest
            // 
            this.gbtnTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(78)))), ((int)(((byte)(133)))));
            this.gbtnTest.Font = new System.Drawing.Font("迷你简隶书", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbtnTest.GlowColor = System.Drawing.Color.White;
            this.gbtnTest.Image = global::HR_Test.Properties.Resources.testt;
            this.gbtnTest.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.gbtnTest.InnerBorderColor = System.Drawing.Color.LightSkyBlue;
            this.gbtnTest.Location = new System.Drawing.Point(18, 216);
            this.gbtnTest.Name = "gbtnTest";
            this.gbtnTest.OuterBorderColor = System.Drawing.Color.SteelBlue;
            this.gbtnTest.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.gbtnTest.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(78)))), ((int)(((byte)(133)))));
            this.gbtnTest.Size = new System.Drawing.Size(123, 134);
            this.gbtnTest.TabIndex = 10;
            this.gbtnTest.Text = "测试";
            this.gbtnTest.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.gbtnTest.Click += new System.EventHandler(this.gbtnTest_Click);
            this.gbtnTest.MouseEnter += new System.EventHandler(this.gbtnTestMethod_MouseEnter);
            this.gbtnTest.MouseLeave += new System.EventHandler(this.gbtnTestMethod_MouseLeave);
            // 
            // gbtnTestMethod
            // 
            this.gbtnTestMethod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(78)))), ((int)(((byte)(133)))));
            this.gbtnTestMethod.Font = new System.Drawing.Font("迷你简隶书", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbtnTestMethod.GlowColor = System.Drawing.Color.White;
            this.gbtnTestMethod.Image = global::HR_Test.Properties.Resources.testmethod;
            this.gbtnTestMethod.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.gbtnTestMethod.InnerBorderColor = System.Drawing.Color.LightSkyBlue;
            this.gbtnTestMethod.Location = new System.Drawing.Point(18, 14);
            this.gbtnTestMethod.Name = "gbtnTestMethod";
            this.gbtnTestMethod.OuterBorderColor = System.Drawing.Color.SteelBlue;
            this.gbtnTestMethod.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.gbtnTestMethod.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(78)))), ((int)(((byte)(133)))));
            this.gbtnTestMethod.Size = new System.Drawing.Size(123, 134);
            this.gbtnTestMethod.TabIndex = 10;
            this.gbtnTestMethod.Text = "试验方法";
            this.gbtnTestMethod.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.gbtnTestMethod.Click += new System.EventHandler(this.gbtnTestMethod_Click);
            this.gbtnTestMethod.MouseEnter += new System.EventHandler(this.gbtnTestMethod_MouseLeave);
            this.gbtnTestMethod.MouseLeave += new System.EventHandler(this.gbtnTestMethod_MouseLeave);
            // 
            // gbtnReport
            // 
            this.gbtnReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(78)))), ((int)(((byte)(133)))));
            this.gbtnReport.Font = new System.Drawing.Font("迷你简隶书", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbtnReport.GlowColor = System.Drawing.Color.White;
            this.gbtnReport.Image = global::HR_Test.Properties.Resources.report;
            this.gbtnReport.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.gbtnReport.InnerBorderColor = System.Drawing.Color.LightSkyBlue;
            this.gbtnReport.Location = new System.Drawing.Point(250, 216);
            this.gbtnReport.Name = "gbtnReport";
            this.gbtnReport.OuterBorderColor = System.Drawing.Color.SteelBlue;
            this.gbtnReport.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.gbtnReport.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(78)))), ((int)(((byte)(133)))));
            this.gbtnReport.Size = new System.Drawing.Size(123, 134);
            this.gbtnReport.TabIndex = 10;
            this.gbtnReport.Text = "试验报告";
            this.gbtnReport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.gbtnReport.Click += new System.EventHandler(this.gbtnReport_Click);
            this.gbtnReport.MouseEnter += new System.EventHandler(this.gbtnTestMethod_MouseEnter);
            this.gbtnReport.MouseLeave += new System.EventHandler(this.gbtnTestMethod_MouseLeave);
            // 
            // glassButton4
            // 
            this.glassButton4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(78)))), ((int)(((byte)(133)))));
            this.glassButton4.Font = new System.Drawing.Font("迷你简隶书", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glassButton4.GlowColor = System.Drawing.Color.White;
            this.glassButton4.Image = global::HR_Test.Properties.Resources.set1;
            this.glassButton4.InnerBorderColor = System.Drawing.Color.LightSkyBlue;
            this.glassButton4.Location = new System.Drawing.Point(714, 14);
            this.glassButton4.Name = "glassButton4";
            this.glassButton4.OuterBorderColor = System.Drawing.Color.SteelBlue;
            this.glassButton4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.glassButton4.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(78)))), ((int)(((byte)(133)))));
            this.glassButton4.Size = new System.Drawing.Size(123, 134);
            this.glassButton4.TabIndex = 10;
            this.glassButton4.Text = "导入方法";
            this.glassButton4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.glassButton4.MouseEnter += new System.EventHandler(this.gbtnTestMethod_MouseEnter);
            this.glassButton4.MouseLeave += new System.EventHandler(this.gbtnTestMethod_MouseLeave);
            // 
            // glassButton8
            // 
            this.glassButton8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(78)))), ((int)(((byte)(133)))));
            this.glassButton8.Font = new System.Drawing.Font("迷你简隶书", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.glassButton8.GlowColor = System.Drawing.Color.White;
            this.glassButton8.Image = global::HR_Test.Properties.Resources.about1;
            this.glassButton8.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.glassButton8.InnerBorderColor = System.Drawing.Color.LightSkyBlue;
            this.glassButton8.Location = new System.Drawing.Point(714, 216);
            this.glassButton8.Name = "glassButton8";
            this.glassButton8.OuterBorderColor = System.Drawing.Color.SteelBlue;
            this.glassButton8.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.glassButton8.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(78)))), ((int)(((byte)(133)))));
            this.glassButton8.Size = new System.Drawing.Size(123, 134);
            this.glassButton8.TabIndex = 10;
            this.glassButton8.Text = "关于";
            this.glassButton8.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.glassButton8.MouseEnter += new System.EventHandler(this.gbtnTestMethod_MouseEnter);
            this.glassButton8.MouseLeave += new System.EventHandler(this.gbtnTestMethod_MouseLeave);
            // 
            // gbtnSet
            // 
            this.gbtnSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(78)))), ((int)(((byte)(133)))));
            this.gbtnSet.Font = new System.Drawing.Font("迷你简隶书", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbtnSet.GlowColor = System.Drawing.Color.White;
            this.gbtnSet.Image = global::HR_Test.Properties.Resources.sets;
            this.gbtnSet.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.gbtnSet.InnerBorderColor = System.Drawing.Color.LightSkyBlue;
            this.gbtnSet.Location = new System.Drawing.Point(482, 14);
            this.gbtnSet.Name = "gbtnSet";
            this.gbtnSet.OuterBorderColor = System.Drawing.Color.SteelBlue;
            this.gbtnSet.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.gbtnSet.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(78)))), ((int)(((byte)(133)))));
            this.gbtnSet.Size = new System.Drawing.Size(123, 134);
            this.gbtnSet.TabIndex = 10;
            this.gbtnSet.Text = "仪器设置";
            this.gbtnSet.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.gbtnSet.Click += new System.EventHandler(this.gbtnSet_Click);
            this.gbtnSet.MouseEnter += new System.EventHandler(this.gbtnTestMethod_MouseEnter);
            this.gbtnSet.MouseLeave += new System.EventHandler(this.gbtnTestMethod_MouseLeave);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(998, 701);
            this.ControlBox = false;
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HR-Test";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.panelContainer.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.palMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private Glass.GlassButton glassButton8;
        private Glass.GlassButton glassButton4;
        private Glass.GlassButton gbtnHelp;
        private Glass.GlassButton gbtnSet;
        private Glass.GlassButton gbtnReport;
        private Glass.GlassButton gbtnTest;
        private System.Windows.Forms.Panel palMain;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnMinimize;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbtnExit;
        private System.Windows.Forms.ToolStripButton tsbtnAccess;
        private Glass.GlassButton gbtnParameter;
        private Glass.GlassButton gbtnTestMethod;

    }
}