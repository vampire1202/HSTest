namespace HR_Test.UC
{
    partial class Result
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
            this.lblFiledName = new System.Windows.Forms.Label();
            this.txtFiledContent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblFiledName
            // 
            this.lblFiledName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFiledName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiledName.ForeColor = System.Drawing.Color.Black;
            this.lblFiledName.Location = new System.Drawing.Point(0, 3);
            this.lblFiledName.Margin = new System.Windows.Forms.Padding(0);
            this.lblFiledName.Name = "lblFiledName";
            this.lblFiledName.Size = new System.Drawing.Size(47, 25);
            this.lblFiledName.TabIndex = 1;
            this.lblFiledName.Text = "σpb:";
            this.lblFiledName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFiledContent
            // 
            this.txtFiledContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFiledContent.Location = new System.Drawing.Point(47, 3);
            this.txtFiledContent.Name = "txtFiledContent";
            this.txtFiledContent.Size = new System.Drawing.Size(135, 25);
            this.txtFiledContent.TabIndex = 2;
            this.txtFiledContent.Text = "label1";
            this.txtFiledContent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Result
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.txtFiledContent);
            this.Controls.Add(this.lblFiledName);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Result";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.Size = new System.Drawing.Size(182, 28);
            this.Load += new System.EventHandler(this.UserField_Load);
            this.SizeChanged += new System.EventHandler(this.Result_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFiledName;
        public System.Windows.Forms.Label txtFiledContent;

    }
}
