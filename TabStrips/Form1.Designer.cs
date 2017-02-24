using System.Windows.Forms;
using Messir.Windows.Forms;
namespace TabStrips
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSelected = new System.Windows.Forms.Label();
            this.tabStrip3 = new Messir.Windows.Forms.TabStrip();
            this.tabNetwork = new Messir.Windows.Forms.TabStripButton();
            this.tabColors = new Messir.Windows.Forms.TabStripButton();
            this.tabSearch = new Messir.Windows.Forms.TabStripButton();
            this.tabStrip2 = new Messir.Windows.Forms.TabStrip();
            this.tabSounds = new Messir.Windows.Forms.TabStripButton();
            this.tabDatabases = new Messir.Windows.Forms.TabStripButton();
            this.tabNotes = new Messir.Windows.Forms.TabStripButton();
            this.tabStrip1 = new Messir.Windows.Forms.TabStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tabPage1 = new Messir.Windows.Forms.TabStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tabPage2 = new Messir.Windows.Forms.TabStripButton();
            this.tabPage3 = new Messir.Windows.Forms.TabStripButton();
            this.tabPage4 = new Messir.Windows.Forms.TabStripButton();
            this.panel1.SuspendLayout();
            this.tabStrip3.SuspendLayout();
            this.tabStrip2.SuspendLayout();
            this.tabStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.lblSelected);
            this.panel1.Controls.Add(this.tabStrip3);
            this.panel1.Controls.Add(this.tabStrip2);
            this.panel1.Controls.Add(this.tabStrip1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(526, 295);
            this.panel1.TabIndex = 2;
            // 
            // lblSelected
            // 
            this.lblSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelected.Location = new System.Drawing.Point(123, 0);
            this.lblSelected.Margin = new System.Windows.Forms.Padding(0);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(377, 270);
            this.lblSelected.TabIndex = 4;
            this.lblSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabStrip3
            // 
            this.tabStrip3.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabStrip3.FlipButtons = true;
            this.tabStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tabStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabNetwork,
            this.tabColors,
            this.tabSearch});
            this.tabStrip3.Location = new System.Drawing.Point(500, 0);
            this.tabStrip3.Name = "tabStrip3";
            this.tabStrip3.RenderStyle = System.Windows.Forms.ToolStripRenderMode.System;
            this.tabStrip3.SelectedTab = this.tabNetwork;
            this.tabStrip3.Size = new System.Drawing.Size(26, 270);
            this.tabStrip3.TabIndex = 3;
            this.tabStrip3.Text = "tabStrip3";
            this.tabStrip3.UseVisualStyles = true;
            this.tabStrip3.SelectedTabChanged += new System.EventHandler<Messir.Windows.Forms.SelectedTabChangedEventArgs>(this.tabStrips_SelectedTabChanged);
            // 
            // tabNetwork
            // 
            this.tabNetwork.Checked = true;
            this.tabNetwork.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tabNetwork.Image = ((System.Drawing.Image)(resources.GetObject("tabNetwork.Image")));
            this.tabNetwork.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tabNetwork.IsSelected = true;
            this.tabNetwork.Margin = new System.Windows.Forms.Padding(0);
            this.tabNetwork.Name = "tabNetwork";
            this.tabNetwork.Padding = new System.Windows.Forms.Padding(0);
            this.tabNetwork.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabNetwork.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.tabNetwork.Size = new System.Drawing.Size(25, 77);
            this.tabNetwork.Text = "Network";
            this.tabNetwork.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            this.tabNetwork.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tabColors
            // 
            this.tabColors.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tabColors.Image = ((System.Drawing.Image)(resources.GetObject("tabColors.Image")));
            this.tabColors.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tabColors.IsSelected = false;
            this.tabColors.Margin = new System.Windows.Forms.Padding(0);
            this.tabColors.Name = "tabColors";
            this.tabColors.Padding = new System.Windows.Forms.Padding(0);
            this.tabColors.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabColors.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.tabColors.Size = new System.Drawing.Size(25, 67);
            this.tabColors.Text = "Colors";
            this.tabColors.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            this.tabColors.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tabSearch
            // 
            this.tabSearch.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tabSearch.Image = ((System.Drawing.Image)(resources.GetObject("tabSearch.Image")));
            this.tabSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tabSearch.IsSelected = false;
            this.tabSearch.Margin = new System.Windows.Forms.Padding(0);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Padding = new System.Windows.Forms.Padding(0);
            this.tabSearch.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabSearch.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.tabSearch.Size = new System.Drawing.Size(25, 70);
            this.tabSearch.Text = "Search";
            this.tabSearch.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            this.tabSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tabStrip2
            // 
            this.tabStrip2.AutoSize = false;
            this.tabStrip2.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabStrip2.FlipButtons = false;
            this.tabStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabSounds,
            this.tabDatabases,
            this.tabNotes});
            this.tabStrip2.Location = new System.Drawing.Point(0, 0);
            this.tabStrip2.Name = "tabStrip2";
            this.tabStrip2.RenderStyle = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.tabStrip2.SelectedTab = this.tabDatabases;
            this.tabStrip2.Size = new System.Drawing.Size(123, 270);
            this.tabStrip2.TabIndex = 2;
            this.tabStrip2.Text = "tabStrip2";
            this.tabStrip2.UseVisualStyles = true;
            this.tabStrip2.SelectedTabChanged += new System.EventHandler<Messir.Windows.Forms.SelectedTabChangedEventArgs>(this.tabStrips_SelectedTabChanged);
            // 
            // tabSounds
            // 
            this.tabSounds.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tabSounds.Image = ((System.Drawing.Image)(resources.GetObject("tabSounds.Image")));
            this.tabSounds.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tabSounds.IsSelected = false;
            this.tabSounds.Margin = new System.Windows.Forms.Padding(0);
            this.tabSounds.Name = "tabSounds";
            this.tabSounds.Padding = new System.Windows.Forms.Padding(0);
            this.tabSounds.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabSounds.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.tabSounds.Size = new System.Drawing.Size(122, 43);
            this.tabSounds.Text = "Sounds";
            this.tabSounds.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tabDatabases
            // 
            this.tabDatabases.Checked = true;
            this.tabDatabases.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tabDatabases.Image = ((System.Drawing.Image)(resources.GetObject("tabDatabases.Image")));
            this.tabDatabases.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tabDatabases.IsSelected = true;
            this.tabDatabases.Margin = new System.Windows.Forms.Padding(0);
            this.tabDatabases.Name = "tabDatabases";
            this.tabDatabases.Padding = new System.Windows.Forms.Padding(0);
            this.tabDatabases.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabDatabases.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.tabDatabases.Size = new System.Drawing.Size(122, 43);
            this.tabDatabases.Text = "Databases";
            this.tabDatabases.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tabNotes
            // 
            this.tabNotes.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tabNotes.Image = ((System.Drawing.Image)(resources.GetObject("tabNotes.Image")));
            this.tabNotes.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tabNotes.IsSelected = false;
            this.tabNotes.Margin = new System.Windows.Forms.Padding(0);
            this.tabNotes.Name = "tabNotes";
            this.tabNotes.Padding = new System.Windows.Forms.Padding(0);
            this.tabNotes.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabNotes.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.tabNotes.Size = new System.Drawing.Size(122, 43);
            this.tabNotes.Text = "Notes";
            this.tabNotes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tabStrip1
            // 
            this.tabStrip1.AllowItemReorder = true;
            this.tabStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabStrip1.FlipButtons = true;
            this.tabStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.tabPage1,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tabPage2,
            this.tabPage3,
            this.tabPage4});
            this.tabStrip1.Location = new System.Drawing.Point(0, 270);
            this.tabStrip1.Name = "tabStrip1";
            this.tabStrip1.RenderStyle = System.Windows.Forms.ToolStripRenderMode.System;
            this.tabStrip1.SelectedTab = this.tabPage1;
            this.tabStrip1.Size = new System.Drawing.Size(526, 25);
            this.tabStrip1.TabIndex = 1;
            this.tabStrip1.Text = "tabStrip1";
            this.tabStrip1.UseVisualStyles = false;
            this.tabStrip1.SelectedTabChanged += new System.EventHandler<Messir.Windows.Forms.SelectedTabChangedEventArgs>(this.tabStrips_SelectedTabChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(48, 22);
            this.toolStripLabel2.Text = "General:";
            // 
            // tabPage1
            // 
            this.tabPage1.Checked = true;
            this.tabPage1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tabPage1.DoubleClickEnabled = true;
            this.tabPage1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabPage1.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Image = ((System.Drawing.Image)(resources.GetObject("tabPage1.Image")));
            this.tabPage1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tabPage1.IsSelected = true;
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(0);
            this.tabPage1.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPage1.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Size = new System.Drawing.Size(81, 25);
            this.tabPage1.Text = "Tab Page 1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(44, 22);
            this.toolStripLabel1.Text = "Special:";
            // 
            // tabPage2
            // 
            this.tabPage2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabPage2.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tabPage2.Image = ((System.Drawing.Image)(resources.GetObject("tabPage2.Image")));
            this.tabPage2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tabPage2.IsSelected = false;
            this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(0);
            this.tabPage2.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabPage2.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.tabPage2.Size = new System.Drawing.Size(81, 25);
            this.tabPage2.Text = "Tab Page 2";
            // 
            // tabPage3
            // 
            this.tabPage3.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabPage3.HotTextColor = System.Drawing.SystemColors.ControlText;
            this.tabPage3.Image = ((System.Drawing.Image)(resources.GetObject("tabPage3.Image")));
            this.tabPage3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tabPage3.IsSelected = false;
            this.tabPage3.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(0);
            this.tabPage3.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabPage3.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.tabPage3.Size = new System.Drawing.Size(81, 25);
            this.tabPage3.Text = "Tab Page 3";
            // 
            // tabPage4
            // 
            this.tabPage4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage4.HotTextColor = System.Drawing.Color.Red;
            this.tabPage4.IsSelected = false;
            this.tabPage4.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(0);
            this.tabPage4.SelectedFont = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabPage4.SelectedTextColor = System.Drawing.Color.Blue;
            this.tabPage4.Size = new System.Drawing.Size(65, 25);
            this.tabPage4.Text = "Tab Page 4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 319);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "TabStrip Demo";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabStrip3.ResumeLayout(false);
            this.tabStrip3.PerformLayout();
            this.tabStrip2.ResumeLayout(false);
            this.tabStrip2.PerformLayout();
            this.tabStrip1.ResumeLayout(false);
            this.tabStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TabStrip tabStrip1;
        private ToolStripLabel toolStripLabel2;
        private TabStripButton tabPage1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel toolStripLabel1;
        private TabStripButton tabPage2;
        private TabStripButton tabPage3;
        private TabStripButton tabPage4;
        private Panel panel1;
        private TabStrip tabStrip2;
        private TabStripButton tabSounds;
        private TabStripButton tabDatabases;
        private TabStripButton tabNotes;
        private TabStrip tabStrip3;
        private TabStripButton tabNetwork;
        private TabStripButton tabColors;
        private TabStripButton tabSearch;
        private Label lblSelected;
    }
}

