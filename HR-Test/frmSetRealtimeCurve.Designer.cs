namespace HR_Test
{
    partial class frmSetRealtimeCurve
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tscbX1 = new System.Windows.Forms.ComboBox();
            this.tscbX2 = new System.Windows.Forms.ComboBox();
            this.tscbY2 = new System.Windows.Forms.ComboBox();
            this.tscbY1 = new System.Windows.Forms.ComboBox();
            this.btnExit = new Glass.GlassButton();
            this.tsBtnSave = new Glass.GlassButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = global::HR_Test.Properties.Resources.showxy1;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tscbX1);
            this.panel1.Controls.Add(this.tscbX2);
            this.panel1.Controls.Add(this.tscbY2);
            this.panel1.Controls.Add(this.tscbY1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(391, 235);
            this.panel1.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(187, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 14);
            this.label5.TabIndex = 0;
            this.label5.Text = "X1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 13F);
            this.label4.Location = new System.Drawing.Point(594, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "Y3";
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(187, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "X2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(26, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "Y2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(120, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Y1";
            // 
            // tscbX1
            // 
            this.tscbX1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbX1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tscbX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(67)))), ((int)(((byte)(108)))));
            this.tscbX1.FormattingEnabled = true;
            this.tscbX1.Items.AddRange(new object[] {
            "-空-",
            "时间",
            "位移",
            "应变",
            "应力",
            "变形"});
            this.tscbX1.Location = new System.Drawing.Point(214, 146);
            this.tscbX1.Name = "tscbX1";
            this.tscbX1.Size = new System.Drawing.Size(74, 22);
            this.tscbX1.TabIndex = 1;
            this.tscbX1.SelectedIndexChanged += new System.EventHandler(this.tscbX1_SelectedIndexChanged);
            // 
            // tscbX2
            // 
            this.tscbX2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbX2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tscbX2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(67)))), ((int)(((byte)(108)))));
            this.tscbX2.FormattingEnabled = true;
            this.tscbX2.Items.AddRange(new object[] {
            "-空-",
            "时间",
            "位移",
            "应变",
            "应力",
            "变形"});
            this.tscbX2.Location = new System.Drawing.Point(214, 190);
            this.tscbX2.Name = "tscbX2";
            this.tscbX2.Size = new System.Drawing.Size(74, 22);
            this.tscbX2.TabIndex = 1;
            this.tscbX2.SelectedIndexChanged += new System.EventHandler(this.tscbX2_SelectedIndexChanged);
            // 
            // tscbY2
            // 
            this.tscbY2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbY2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tscbY2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(67)))), ((int)(((byte)(108)))));
            this.tscbY2.FormattingEnabled = true;
            this.tscbY2.Items.AddRange(new object[] {
            "-空-",
            "负荷",
            "应力",
            "变形",
            "位移",
            "应变"});
            this.tscbY2.Location = new System.Drawing.Point(2, 83);
            this.tscbY2.Name = "tscbY2";
            this.tscbY2.Size = new System.Drawing.Size(74, 22);
            this.tscbY2.TabIndex = 1;
            this.tscbY2.SelectedIndexChanged += new System.EventHandler(this.tscbY2_SelectedIndexChanged);
            // 
            // tscbY1
            // 
            this.tscbY1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbY1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tscbY1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(67)))), ((int)(((byte)(108)))));
            this.tscbY1.FormattingEnabled = true;
            this.tscbY1.Items.AddRange(new object[] {
            "-空-",
            "负荷",
            "应力",
            "变形",
            "位移",
            "应变"});
            this.tscbY1.Location = new System.Drawing.Point(97, 83);
            this.tscbY1.Name = "tscbY1";
            this.tscbY1.Size = new System.Drawing.Size(74, 22);
            this.tscbY1.TabIndex = 1;
            this.tscbY1.SelectedIndexChanged += new System.EventHandler(this.tscbY1_SelectedIndexChanged);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnExit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.GlowColor = System.Drawing.Color.DeepSkyBlue;
            this.btnExit.Location = new System.Drawing.Point(227, 253);
            this.btnExit.Name = "btnExit";
            this.btnExit.ShineColor = System.Drawing.Color.AliceBlue;
            this.btnExit.Size = new System.Drawing.Size(74, 38);
            this.btnExit.TabIndex = 135;
            this.btnExit.Text = "取消";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tsBtnSave
            // 
            this.tsBtnSave.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tsBtnSave.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tsBtnSave.ForeColor = System.Drawing.Color.Black;
            this.tsBtnSave.GlowColor = System.Drawing.Color.DeepSkyBlue;
            this.tsBtnSave.Location = new System.Drawing.Point(125, 253);
            this.tsBtnSave.Name = "tsBtnSave";
            this.tsBtnSave.ShineColor = System.Drawing.Color.AliceBlue;
            this.tsBtnSave.Size = new System.Drawing.Size(74, 38);
            this.tsBtnSave.TabIndex = 134;
            this.tsBtnSave.Text = "确定";
            this.tsBtnSave.Click += new System.EventHandler(this.tsBtnSave_Click);
            // 
            // frmSetRealtimeCurve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(413, 301);
            this.ControlBox = false;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tsBtnSave);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSetRealtimeCurve";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "实时曲线显示设置";
            this.Load += new System.EventHandler(this.frmSetRealtimeCurve_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
          
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox tscbY1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox tscbX1;
        private System.Windows.Forms.ComboBox tscbX2;
        private System.Windows.Forms.ComboBox tscbY2;
        private Glass.GlassButton tsBtnSave;
        private Glass.GlassButton btnExit;
    }
}