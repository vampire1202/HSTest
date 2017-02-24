namespace HR_Test
{
    partial class frmBendOther2
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
            this.b_Da = new System.Windows.Forms.TextBox();
            this.b_R = new System.Windows.Forms.TextBox();
            this.b_Ds = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.b_a = new System.Windows.Forms.TextBox();
            this.b_m = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.btnCancel = new Glass.GlassButton();
            this.btnOK = new Glass.GlassButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // b_Da
            // 
            this.b_Da.Font = new System.Drawing.Font("宋体", 11F);
            this.b_Da.Location = new System.Drawing.Point(190, 93);
            this.b_Da.Name = "b_Da";
            this.b_Da.Size = new System.Drawing.Size(80, 24);
            this.b_Da.TabIndex = 2;
            this.b_Da.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.b_Da.TextChanged += new System.EventHandler(this.b_Ds_TextChanged);
            // 
            // b_R
            // 
            this.b_R.Font = new System.Drawing.Font("宋体", 11F);
            this.b_R.Location = new System.Drawing.Point(390, 52);
            this.b_R.Name = "b_R";
            this.b_R.Size = new System.Drawing.Size(80, 24);
            this.b_R.TabIndex = 1;
            this.b_R.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.b_R.TextChanged += new System.EventHandler(this.b_Ds_TextChanged);
            // 
            // b_Ds
            // 
            this.b_Ds.Font = new System.Drawing.Font("宋体", 11F);
            this.b_Ds.Location = new System.Drawing.Point(190, 50);
            this.b_Ds.Name = "b_Ds";
            this.b_Ds.Size = new System.Drawing.Size(80, 24);
            this.b_Ds.TabIndex = 0;
            this.b_Ds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.b_Ds.TextChanged += new System.EventHandler(this.b_Ds_TextChanged);
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Font = new System.Drawing.Font("宋体", 11F);
            this.label74.Location = new System.Drawing.Point(271, 56);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(115, 15);
            this.label74.TabIndex = 128;
            this.label74.Text = "刀刃半径R(mm):";
            this.label74.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Font = new System.Drawing.Font("宋体", 11F);
            this.label83.Location = new System.Drawing.Point(34, 97);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(153, 15);
            this.label83.TabIndex = 125;
            this.label83.Text = "施力滚柱直径Da(mm):";
            this.label83.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Font = new System.Drawing.Font("宋体", 11F);
            this.label82.Location = new System.Drawing.Point(34, 54);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(153, 15);
            this.label82.TabIndex = 126;
            this.label82.Text = "支撑滚柱直径Ds(mm):";
            this.label82.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // b_a
            // 
            this.b_a.Font = new System.Drawing.Font("宋体", 11F);
            this.b_a.Location = new System.Drawing.Point(390, 93);
            this.b_a.Margin = new System.Windows.Forms.Padding(2);
            this.b_a.Name = "b_a";
            this.b_a.Size = new System.Drawing.Size(80, 24);
            this.b_a.TabIndex = 5;
            this.b_a.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.b_a.TextChanged += new System.EventHandler(this.b_Ds_TextChanged);
            // 
            // b_m
            // 
            this.b_m.Font = new System.Drawing.Font("宋体", 11F);
            this.b_m.Location = new System.Drawing.Point(190, 140);
            this.b_m.Margin = new System.Windows.Forms.Padding(2);
            this.b_m.Name = "b_m";
            this.b_m.Size = new System.Drawing.Size(80, 24);
            this.b_m.TabIndex = 6;
            this.b_m.TextChanged += new System.EventHandler(this.b_Ds_TextChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("宋体", 11F);
            this.label33.Location = new System.Drawing.Point(273, 97);
            this.label33.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(113, 15);
            this.label33.TabIndex = 135;
            this.label33.Text = "倒棱修正系数a:";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("宋体", 11F);
            this.label29.Location = new System.Drawing.Point(-1, 144);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(188, 15);
            this.label29.TabIndex = 133;
            this.label29.Text = "弯曲力挠度数据对的数目m:";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.GlowColor = System.Drawing.Color.DeepSkyBlue;
            this.btnCancel.Location = new System.Drawing.Point(378, 222);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.ShineColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Size = new System.Drawing.Size(95, 38);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.GlowColor = System.Drawing.Color.DeepSkyBlue;
            this.btnOK.Location = new System.Drawing.Point(277, 222);
            this.btnOK.Name = "btnOK";
            this.btnOK.ShineColor = System.Drawing.Color.AliceBlue;
            this.btnOK.Size = new System.Drawing.Size(95, 38);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(-1, 198);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(497, 12);
            this.label1.TabIndex = 138;
            this.label1.Text = "_________________________________________________________________________________" +
                "_";
            // 
            // frmBendOther2
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(494, 272);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.b_a);
            this.Controls.Add(this.b_m);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.b_Da);
            this.Controls.Add(this.b_R);
            this.Controls.Add(this.b_Ds);
            this.Controls.Add(this.label74);
            this.Controls.Add(this.label83);
            this.Controls.Add(this.label82);
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmBendOther2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "其他参数";
            this.Load += new System.EventHandler(this.frmBendOther2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox b_Da;
        private System.Windows.Forms.TextBox b_R;
        private System.Windows.Forms.TextBox b_Ds;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.TextBox b_a;
        private System.Windows.Forms.TextBox b_m;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label29;
        private Glass.GlassButton btnCancel;
        private Glass.GlassButton btnOK;
        private System.Windows.Forms.Label label1;
    }
}