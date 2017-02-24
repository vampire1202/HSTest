namespace HR_Test.UC
{
    partial class ReSizePanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReSizePanel));
            this.pictureBoxR = new System.Windows.Forms.PictureBox();
            this.pictureBoxL = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxL)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxR
            // 
            this.pictureBoxR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxR.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.pictureBoxR.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxR.Image")));
            this.pictureBoxR.Location = new System.Drawing.Point(132, 132);
            this.pictureBoxR.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxR.Name = "pictureBoxR";
            this.pictureBoxR.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxR.TabIndex = 0;
            this.pictureBoxR.TabStop = false;
            this.pictureBoxR.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxR_MouseMove);
            this.pictureBoxR.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxR_MouseDown);
            this.pictureBoxR.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxR_MouseUp);
            // 
            // pictureBoxL
            // 
            this.pictureBoxL.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.pictureBoxL.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxL.Image")));
            this.pictureBoxL.Location = new System.Drawing.Point(-1, -1);
            this.pictureBoxL.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxL.Name = "pictureBoxL";
            this.pictureBoxL.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxL.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxL.TabIndex = 0;
            this.pictureBoxL.TabStop = false;
            this.pictureBoxL.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxL_MouseMove);
            this.pictureBoxL.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxL_MouseDown);
            this.pictureBoxL.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxL_MouseUp);
            // 
            // ReSizePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pictureBoxR);
            this.Controls.Add(this.pictureBoxL);
            this.Name = "ReSizePanel";
            this.Size = new System.Drawing.Size(148, 148);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ReSizePanel_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ReSizePanel_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ReSizePanel_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxL)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxR;
        private System.Windows.Forms.PictureBox pictureBoxL;
    }
}
