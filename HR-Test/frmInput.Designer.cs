namespace HR_Test
{
    partial class frmInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInput));
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbtnExit = new System.Windows.Forms.ToolStripButton();
            this.tsbtnMinimize = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tvTestMethod = new System.Windows.Forms.TreeView();
            this.palInput = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1276, 75);
            this.panel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.toolStripLabel1,
            this.tsbtnExit,
            this.tsbtnMinimize,
            this.toolStripLabel2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1276, 75);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Image = global::HR_Test.Properties.Resources._20110704100834860_easyicon_cn_128;
            this.toolStripLabel3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(96, 72);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel1.Image")));
            this.toolStripLabel1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(117, 72);
            this.toolStripLabel1.ToolTipText = "输入试验参数";
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
            // tsbtnMinimize
            // 
            this.tsbtnMinimize.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnMinimize.Image = global::HR_Test.Properties.Resources.minimize;
            this.tsbtnMinimize.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnMinimize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnMinimize.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.tsbtnMinimize.Name = "tsbtnMinimize";
            this.tsbtnMinimize.Size = new System.Drawing.Size(56, 72);
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
            this.toolStripLabel2.Size = new System.Drawing.Size(280, 66);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tvTestMethod);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 75);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(179, 802);
            this.panel2.TabIndex = 2;
            // 
            // tvTestMethod
            // 
            this.tvTestMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTestMethod.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvTestMethod.Location = new System.Drawing.Point(0, 0);
            this.tvTestMethod.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tvTestMethod.Name = "tvTestMethod";
            this.tvTestMethod.Size = new System.Drawing.Size(179, 802);
            this.tvTestMethod.TabIndex = 0;
            this.tvTestMethod.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvTestMethod_NodeMouseClick);
            // 
            // palInput
            // 
            this.palInput.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.palInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palInput.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.palInput.ForeColor = System.Drawing.Color.Black;
            this.palInput.Location = new System.Drawing.Point(179, 75);
            this.palInput.Name = "palInput";
            this.palInput.Size = new System.Drawing.Size(1097, 802);
            this.palInput.TabIndex = 7;
            // 
            // frmInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1276, 877);
            this.Controls.Add(this.palInput);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmInput";
            this.ShowInTaskbar = false;
            this.Text = "参数输入";
            this.Load += new System.EventHandler(this.frmInput_Load);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton tsbtnExit;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton tsbtnMinimize;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.Panel palInput;
        public System.Windows.Forms.TreeView tvTestMethod;
    }
}