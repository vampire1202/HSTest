namespace HR_Test.UC
{
    partial class UserField
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
            this.txtFiledContent = new System.Windows.Forms.TextBox();
            this.lblFiledName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFiledContent
            // 
            this.txtFiledContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFiledContent.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFiledContent.Location = new System.Drawing.Point(70, 3);
            this.txtFiledContent.Margin = new System.Windows.Forms.Padding(0);
            this.txtFiledContent.Name = "txtFiledContent";
            this.txtFiledContent.Size = new System.Drawing.Size(217, 23);
            this.txtFiledContent.TabIndex = 2;
            this.txtFiledContent.TextChanged += new System.EventHandler(this.txtFiledContent_TextChanged);
            // 
            // lblFiledName
            // 
            this.lblFiledName.AutoSize = true;
            this.lblFiledName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFiledName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiledName.ForeColor = System.Drawing.Color.Black;
            this.lblFiledName.Location = new System.Drawing.Point(0, 3);
            this.lblFiledName.Margin = new System.Windows.Forms.Padding(0);
            this.lblFiledName.Name = "lblFiledName";
            this.lblFiledName.Size = new System.Drawing.Size(70, 14);
            this.lblFiledName.TabIndex = 1;
            this.lblFiledName.Text = "这是测试:";
            this.lblFiledName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtFiledContent);
            this.panel1.Controls.Add(this.lblFiledName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel1.Size = new System.Drawing.Size(287, 29);
            this.panel1.TabIndex = 3;
            // 
            // UserField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UserField";
            this.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.Size = new System.Drawing.Size(287, 31);
            this.Load += new System.EventHandler(this.UserField_Load);
            this.SizeChanged += new System.EventHandler(this.UserField_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFiledName;
        public System.Windows.Forms.TextBox txtFiledContent;
        private System.Windows.Forms.Panel panel1;

    }
}
