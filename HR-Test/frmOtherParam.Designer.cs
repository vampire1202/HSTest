namespace HR_Test
{
    partial class frmOtherParam
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
            this.label2 = new System.Windows.Forms.Label();
            this._sampleCharacter = new System.Windows.Forms.TextBox();
            this.lbltem = new System.Windows.Forms.Label();
            this._temperature = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._humidity = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this._sendCompany = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this._hotStatus = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this._controlmode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this._condition = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.GlowColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(371, 222);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.ShineColor = System.Drawing.Color.AliceBlue;
            this.btnCancel.Size = new System.Drawing.Size(95, 38);
            this.btnCancel.TabIndex = 134;
            this.btnCancel.Text = "取消";
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.ForeColor = System.Drawing.Color.Black;
            this.btnOk.GlowColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(260, 222);
            this.btnOk.Name = "btnOk";
            this.btnOk.ShineColor = System.Drawing.Color.AliceBlue;
            this.btnOk.Size = new System.Drawing.Size(95, 38);
            this.btnOk.TabIndex = 133;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(32, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 135;
            this.label2.Text = "试样标识:";
            // 
            // _sampleCharacter
            // 
            this._sampleCharacter.Font = new System.Drawing.Font("宋体", 11F);
            this._sampleCharacter.Location = new System.Drawing.Point(113, 38);
            this._sampleCharacter.Name = "_sampleCharacter";
            this._sampleCharacter.Size = new System.Drawing.Size(128, 24);
            this._sampleCharacter.TabIndex = 136;
            // 
            // lbltem
            // 
            this.lbltem.AutoSize = true;
            this.lbltem.Font = new System.Drawing.Font("宋体", 11F);
            this.lbltem.Location = new System.Drawing.Point(1, 173);
            this.lbltem.Name = "lbltem";
            this.lbltem.Size = new System.Drawing.Size(106, 15);
            this.lbltem.TabIndex = 135;
            this.lbltem.Text = "试验温度(℃):";
            // 
            // _temperature
            // 
            this._temperature.Font = new System.Drawing.Font("宋体", 11F);
            this._temperature.Location = new System.Drawing.Point(113, 170);
            this._temperature.Name = "_temperature";
            this._temperature.Size = new System.Drawing.Size(128, 24);
            this._temperature.TabIndex = 136;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 11F);
            this.label4.Location = new System.Drawing.Point(8, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 15);
            this.label4.TabIndex = 135;
            this.label4.Text = "试验湿度(%):";
            // 
            // _humidity
            // 
            this._humidity.Font = new System.Drawing.Font("宋体", 11F);
            this._humidity.Location = new System.Drawing.Point(113, 126);
            this._humidity.Name = "_humidity";
            this._humidity.Size = new System.Drawing.Size(128, 24);
            this._humidity.TabIndex = 136;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11F);
            this.label5.Location = new System.Drawing.Point(257, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 15);
            this.label5.TabIndex = 135;
            this.label5.Text = "送检单位:";
            // 
            // _sendCompany
            // 
            this._sendCompany.Font = new System.Drawing.Font("宋体", 11F);
            this._sendCompany.Location = new System.Drawing.Point(338, 81);
            this._sendCompany.Name = "_sendCompany";
            this._sendCompany.Size = new System.Drawing.Size(128, 24);
            this._sendCompany.TabIndex = 136;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 11F);
            this.label6.Location = new System.Drawing.Point(17, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 15);
            this.label6.TabIndex = 135;
            this.label6.Text = "热处理状态:";
            // 
            // _hotStatus
            // 
            this._hotStatus.Font = new System.Drawing.Font("宋体", 11F);
            this._hotStatus.Location = new System.Drawing.Point(113, 82);
            this._hotStatus.Name = "_hotStatus";
            this._hotStatus.Size = new System.Drawing.Size(128, 24);
            this._hotStatus.TabIndex = 136;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 11F);
            this.label7.Location = new System.Drawing.Point(257, 129);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 15);
            this.label7.TabIndex = 135;
            this.label7.Text = "控制方式:";
            // 
            // _controlmode
            // 
            this._controlmode.Font = new System.Drawing.Font("宋体", 11F);
            this._controlmode.Location = new System.Drawing.Point(338, 125);
            this._controlmode.Name = "_controlmode";
            this._controlmode.Size = new System.Drawing.Size(128, 24);
            this._controlmode.TabIndex = 136;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 11F);
            this.label8.Location = new System.Drawing.Point(257, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 15);
            this.label8.TabIndex = 135;
            this.label8.Text = "试验条件:";
            // 
            // _condition
            // 
            this._condition.Font = new System.Drawing.Font("宋体", 11F);
            this._condition.Location = new System.Drawing.Point(338, 37);
            this._condition.Name = "_condition";
            this._condition.Size = new System.Drawing.Size(128, 24);
            this._condition.TabIndex = 136;
            // 
            // frmOtherParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(494, 272);
            this.Controls.Add(this.lbltem);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._sampleCharacter);
            this.Controls.Add(this._humidity);
            this.Controls.Add(this._temperature);
            this.Controls.Add(this._hotStatus);
            this.Controls.Add(this._controlmode);
            this.Controls.Add(this._condition);
            this.Controls.Add(this._sendCompany);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmOtherParam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "其他试验参数";
            this.Load += new System.EventHandler(this.frmTensileOther1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Glass.GlassButton btnCancel;
        private Glass.GlassButton btnOk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox _sampleCharacter;
        private System.Windows.Forms.TextBox _humidity;
        private System.Windows.Forms.TextBox _sendCompany;
        private System.Windows.Forms.TextBox _hotStatus;
        private System.Windows.Forms.TextBox _controlmode;
        private System.Windows.Forms.TextBox _condition;
        public System.Windows.Forms.Label lbltem;
        public System.Windows.Forms.TextBox _temperature;
    }
}