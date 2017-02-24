namespace HR_Test
{
    partial class frmTensileOther2
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
            this.btnCancel = new Glass.GlassButton();
            this.btnOk = new Glass.GlassButton();
            this.label1 = new System.Windows.Forms.Label();
            this._Lt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._L01 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.GlowColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(227, 130);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.ShineColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Size = new System.Drawing.Size(95, 38);
            this.btnCancel.TabIndex = 136;
            this.btnCancel.Text = "取消";
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.GlowColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(123, 130);
            this.btnOk.Name = "btnOk";
            this.btnOk.ShineColor = System.Drawing.Color.AliceBlue;
            this.btnOk.Size = new System.Drawing.Size(95, 38);
            this.btnOk.TabIndex = 135;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F);
            this.label1.Location = new System.Drawing.Point(46, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 15);
            this.label1.TabIndex = 137;
            this.label1.Text = "试样总长度 Lt(mm):";
            // 
            // _Lt
            // 
            this._Lt.Font = new System.Drawing.Font("宋体", 12F);
            this._Lt.Location = new System.Drawing.Point(198, 31);
            this._Lt.Name = "_Lt";
            this._Lt.Size = new System.Drawing.Size(183, 26);
            this._Lt.TabIndex = 138;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(22, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 15);
            this.label2.TabIndex = 137;
            this.label2.Text = "Awn原始标距 L′o(mm):";
            // 
            // _L01
            // 
            this._L01.Font = new System.Drawing.Font("宋体", 12F);
            this._L01.Location = new System.Drawing.Point(198, 70);
            this._L01.Name = "_L01";
            this._L01.Size = new System.Drawing.Size(183, 26);
            this._L01.TabIndex = 138;
            // 
            // frmTensileOther2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(435, 180);
            this.Controls.Add(this._L01);
            this.Controls.Add(this._Lt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmTensileOther2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmTensileOther2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Glass.GlassButton btnCancel;
        private Glass.GlassButton btnOk;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox _Lt;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox _L01;
    }
}