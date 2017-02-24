namespace HR_Test
{
    partial class frmSetResultCurve
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
            this.tslblYr = new System.Windows.Forms.ToolStripLabel();
            this.cmbYr = new System.Windows.Forms.ToolStripComboBox();
            this.tslblXr = new System.Windows.Forms.ToolStripLabel();
            this.cmbXr = new System.Windows.Forms.ToolStripComboBox();
            this.btnSave = new Glass.GlassButton();
            this.btnExit = new Glass.GlassButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblYr,
            this.cmbYr,
            this.tslblXr,
            this.cmbXr});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(245, 84);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tslblYr
            // 
            this.tslblYr.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tslblYr.Name = "tslblYr";
            this.tslblYr.Size = new System.Drawing.Size(21, 81);
            this.tslblYr.Text = "Y:";
            // 
            // cmbYr
            // 
            this.cmbYr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYr.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbYr.Items.AddRange(new object[] {
            "-请选择-",
            "负荷",
            "应力",
            "变形",
            "位移"});
            this.cmbYr.Name = "cmbYr";
            this.cmbYr.Size = new System.Drawing.Size(87, 84);
            // 
            // tslblXr
            // 
            this.tslblXr.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tslblXr.Name = "tslblXr";
            this.tslblXr.Size = new System.Drawing.Size(21, 81);
            this.tslblXr.Text = "X:";
            // 
            // cmbXr
            // 
            this.cmbXr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbXr.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbXr.Items.AddRange(new object[] {
            "-请选择-",
            "时间",
            "位移",
            "应变",
            "变形",
            "应力"});
            this.cmbXr.Name = "cmbXr";
            this.cmbXr.Size = new System.Drawing.Size(87, 84);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSave.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.GlowColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSave.Location = new System.Drawing.Point(45, 101);
            this.btnSave.Name = "btnSave";
            this.btnSave.ShineColor = System.Drawing.Color.AliceBlue;
            this.btnSave.Size = new System.Drawing.Size(74, 38);
            this.btnSave.TabIndex = 130;
            this.btnSave.Text = "确定";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnExit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.GlowColor = System.Drawing.Color.DeepSkyBlue;
            this.btnExit.Location = new System.Drawing.Point(125, 101);
            this.btnExit.Name = "btnExit";
            this.btnExit.ShineColor = System.Drawing.Color.AliceBlue;
            this.btnExit.Size = new System.Drawing.Size(74, 38);
            this.btnExit.TabIndex = 131;
            this.btnExit.Text = "取消";
            // 
            // frmSetResultCurve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 151);
            this.ControlBox = false;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSetResultCurve";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "结果曲线显示设置";
            this.Load += new System.EventHandler(this.frmSetResultCurve_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel tslblYr;
        private System.Windows.Forms.ToolStripComboBox cmbYr;
        private System.Windows.Forms.ToolStripLabel tslblXr;
        private System.Windows.Forms.ToolStripComboBox cmbXr;
        private Glass.GlassButton btnSave;
        private Glass.GlassButton btnExit;
    }
}