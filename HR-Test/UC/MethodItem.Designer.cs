namespace HR_Test.UC
{
    partial class MethodItem
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDw = new System.Windows.Forms.Label();
            this.lblDw2 = new System.Windows.Forms.Label();
            this.btnAddMthodItem = new Glass.GlassButton();
            this._txtChangeValue = new System.Windows.Forms.TextBox();
            this._cmbChangeType = new System.Windows.Forms.ComboBox();
            this._txtControlSpeed = new System.Windows.Forms.TextBox();
            this._cmbControlType = new System.Windows.Forms.ComboBox();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.BackColor = System.Drawing.Color.Transparent;
            this.groupBox.Controls.Add(this.label13);
            this.groupBox.Controls.Add(this.label8);
            this.groupBox.Controls.Add(this.lblDw);
            this.groupBox.Controls.Add(this.lblDw2);
            this.groupBox.Controls.Add(this._txtControlSpeed);
            this.groupBox.Controls.Add(this._cmbControlType);
            this.groupBox.Controls.Add(this._txtChangeValue);
            this.groupBox.Controls.Add(this._cmbChangeType);
            this.groupBox.Location = new System.Drawing.Point(5, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(547, 45);
            this.groupBox.TabIndex = 23;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "1";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(8, 18);
            this.label13.Name = "label13";
            this.label13.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label13.Size = new System.Drawing.Size(70, 14);
            this.label13.TabIndex = 14;
            this.label13.Text = "控制方式:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(287, 19);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(84, 14);
            this.label8.TabIndex = 15;
            this.label8.Text = "控制转换点:";
            // 
            // lblDw
            // 
            this.lblDw.AutoSize = true;
            this.lblDw.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDw.Location = new System.Drawing.Point(232, 19);
            this.lblDw.Name = "lblDw";
            this.lblDw.Size = new System.Drawing.Size(49, 14);
            this.lblDw.TabIndex = 20;
            this.lblDw.Text = "mm/min";
            // 
            // lblDw2
            // 
            this.lblDw2.AutoSize = true;
            this.lblDw2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDw2.Location = new System.Drawing.Point(523, 20);
            this.lblDw2.Name = "lblDw2";
            this.lblDw2.Size = new System.Drawing.Size(21, 14);
            this.lblDw2.TabIndex = 21;
            this.lblDw2.Text = "kN";
            // 
            // btnAddMthodItem
            // 
            this.btnAddMthodItem.BackColor = System.Drawing.Color.Orange;
            this.btnAddMthodItem.ForeColor = System.Drawing.Color.Black;
            this.btnAddMthodItem.GlowColor = System.Drawing.Color.DarkRed;
            this.btnAddMthodItem.Location = new System.Drawing.Point(555, 13);
            this.btnAddMthodItem.Name = "btnAddMthodItem";
            this.btnAddMthodItem.Size = new System.Drawing.Size(56, 26);
            this.btnAddMthodItem.TabIndex = 24;
            this.btnAddMthodItem.Text = "删除";
            this.btnAddMthodItem.Click += new System.EventHandler(this.btnAddMthodItem_Click);
            // 
            // _txtChangeValue
            // 
            this._txtChangeValue.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._txtChangeValue.Location = new System.Drawing.Point(465, 15);
            this._txtChangeValue.Name = "_txtChangeValue";
            this._txtChangeValue.Size = new System.Drawing.Size(54, 23);
            this._txtChangeValue.TabIndex = 19;
            this._txtChangeValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._txtChangeValue.TextChanged += new System.EventHandler(this._txtControlSpeed_TextChanged);
            // 
            // _cmbChangeType
            // 
            this._cmbChangeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbChangeType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._cmbChangeType.FormattingEnabled = true;
            this._cmbChangeType.Items.AddRange(new object[] {
            "位移",
            "负荷",
            "变形",
            "应力",
            "应变"});
            this._cmbChangeType.Location = new System.Drawing.Point(371, 14);
            this._cmbChangeType.Name = "_cmbChangeType";
            this._cmbChangeType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmbChangeType.Size = new System.Drawing.Size(90, 24);
            this._cmbChangeType.TabIndex = 16;
            this._cmbChangeType.SelectedIndexChanged += new System.EventHandler(this._cmbChangeType_SelectedIndexChanged);
            // 
            // _txtControlSpeed
            // 
            this._txtControlSpeed.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._txtControlSpeed.Location = new System.Drawing.Point(173, 15);
            this._txtControlSpeed.Name = "_txtControlSpeed";
            this._txtControlSpeed.Size = new System.Drawing.Size(54, 23);
            this._txtControlSpeed.TabIndex = 18;
            this._txtControlSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._txtControlSpeed.TextChanged += new System.EventHandler(this._txtControlSpeed_TextChanged);
            // 
            // _cmbControlType
            // 
            this._cmbControlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbControlType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._cmbControlType.FormattingEnabled = true;
            this._cmbControlType.Items.AddRange(new object[] {
            "位移控制",
            "负荷控制",
            "应力控制",
            "ēLc控制",
            "ēLe控制"});
            this._cmbControlType.Location = new System.Drawing.Point(80, 14);
            this._cmbControlType.Name = "_cmbControlType";
            this._cmbControlType.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmbControlType.Size = new System.Drawing.Size(90, 24);
            this._cmbControlType.TabIndex = 17;
            this._cmbControlType.SelectedIndexChanged += new System.EventHandler(this._cmbControlType_SelectedIndexChanged);
            // 
            // MethodItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddMthodItem);
            this.Controls.Add(this.groupBox);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "MethodItem";
            this.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.Size = new System.Drawing.Size(617, 50);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblDw;
        private System.Windows.Forms.Label lblDw2;
        public System.Windows.Forms.GroupBox groupBox;
        private Glass.GlassButton btnAddMthodItem;
        public System.Windows.Forms.TextBox _txtControlSpeed;
        public System.Windows.Forms.ComboBox _cmbControlType;
        public System.Windows.Forms.TextBox _txtChangeValue;
        public System.Windows.Forms.ComboBox _cmbChangeType;
    }
}
