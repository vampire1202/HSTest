using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace daReportDesigner
{
	/// <summary>
	/// Summary description for GridDialog.
	/// </summary>
	public class GridDialog : System.Windows.Forms.Form
	{
		
		private System.Windows.Forms.CheckBox showGridBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox gridSizeField;
		private System.Windows.Forms.Button cancelButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GridDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridDialog));
            this.showGridBox = new System.Windows.Forms.CheckBox();
            this.okButton = new System.Windows.Forms.Button();
            this.gridSizeField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // showGridBox
            // 
            this.showGridBox.Location = new System.Drawing.Point(26, 3);
            this.showGridBox.Name = "showGridBox";
            this.showGridBox.Size = new System.Drawing.Size(105, 25);
            this.showGridBox.TabIndex = 0;
            this.showGridBox.Text = "显示网格";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(178, 69);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(76, 25);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "确定";
            // 
            // gridSizeField
            // 
            this.gridSizeField.Location = new System.Drawing.Point(91, 34);
            this.gridSizeField.Name = "gridSizeField";
            this.gridSizeField.Size = new System.Drawing.Size(163, 21);
            this.gridSizeField.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "网格尺寸 : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(91, 69);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(77, 25);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "取消";
            // 
            // GridDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(269, 100);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridSizeField);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.showGridBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GridDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Grid options";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public bool ShowGrid
		{
			get { return showGridBox.Checked; }
			set { showGridBox.Checked = value;}
		}

		public int GridSize
		{
			get { return Convert.ToInt32(gridSizeField.Text); }
			set { gridSizeField.Text = value.ToString();}
		}
	}
}
