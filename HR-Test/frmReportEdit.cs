using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PropertyGridEx;
using System.Xml;
using System.Xml.XPath;


namespace HR_Test
{
    public partial class frmReportEdit : Form
    {

        PropertyGrid propertyGrid1;
        dragControl dc=new dragControl();  
        object mCurrentSelectObject;
        private frmMain _fmMain;

        /// <summary>
        /// 选择的试样编号以供打印
        /// </summary>
        private string[] _selTestSampleArray = null;
        public string[] _SelTestSampleArray
        {
            get { return this._selTestSampleArray; }
            set { this._selTestSampleArray = value; }
        }

        /// <summary>
        /// 试验类型
        /// </summary>
        private string _testType = string.Empty;
        public string _TestType
        {
            get { return this._testType; }
            set { this._testType = value; }
        }

        public frmReportEdit(frmMain fmMain)
        {
            InitializeComponent();
            _fmMain = fmMain;
            mCurrentSelectObject = this.panel2; 
            propertyGrid1 = new PropertyGrid();
            propertyGrid1.CommandsVisibleIfAvailable = true; 
            propertyGrid1.TabIndex = 1;
            propertyGrid1.Text = "属性";
            propertyGrid1.Dock = DockStyle.Fill;  
            this.panel3.Controls.Add(propertyGrid1);
            //this.pictureBox.Size = this.zedGraphControl.Size;
            //this.pictureBox.Location = this.zedGraphControl.Location;
        }

        //自定义属性
        private void getProperty()
        {
            CustomPropertyCollection collection = new CustomPropertyCollection();  
            Type type = mCurrentSelectObject.GetType();
            
            collection.Add(new CustomProperty("字体", "Font", "外观", "字体。", mCurrentSelectObject)); 

            switch (type.Name)
            {
                case "Label":
                case "TextBox":
                case "Panel":
                    collection.Add(new CustomProperty("文本对齐", "TextAlign", "内容", "文本对齐方式。", mCurrentSelectObject));
                    collection.Add(new CustomProperty("边框", "BorderStyle", "外观","边框。", mCurrentSelectObject)); 
                    collection.Add(new CustomProperty("背景色", "BackColor", "外观", "背景色。", mCurrentSelectObject));
                    collection.Add(new CustomProperty("字体颜色", "ForeColor", "外观", "前景色。", mCurrentSelectObject)); 
                    collection.Add(new CustomProperty("文本内容", "Text", "内容", "显示的文本内容。", mCurrentSelectObject, typeof(System.ComponentModel.Design.MultilineStringEditor)));
                    break;   
            } 

            collection.Add(new CustomProperty("宽度", "Width", "大小", "大小。", mCurrentSelectObject));
            collection.Add(new CustomProperty("高度", "Height", "大小", "大小。", mCurrentSelectObject));

            collection.Add(new CustomProperty("Left", "Left", "位置", "左。", mCurrentSelectObject));
            collection.Add(new CustomProperty("Top", "Top", "位置", "右。", mCurrentSelectObject)); 

            propertyGrid1.SelectedObject = collection;
        }


        private void frmReportEdit_Load(object sender, EventArgs e)
        {
            initProperty(); 

        } 

        private void initProperty()
        {
            //for (int i = 0; i < this.panel1.Controls.Count; i++)
            //{ 
            //    this.panel1.Controls[i].MouseDown += new System.Windows.Forms.MouseEventHandler(MouseDown);
            //    this.panel1.Controls[i].MouseLeave += new System.EventHandler(dc.MyMouseLeave);
            //    this.panel1.Controls[i].MouseMove += new System.Windows.Forms.MouseEventHandler(dc.MyMouseMove);
            //}

            for (int i = 0; i < this.panel2.Controls.Count; i++)
            {
                this.panel2.Controls[i].MouseDown += new System.Windows.Forms.MouseEventHandler(MouseDown);
                this.panel2.Controls[i].MouseLeave += new System.EventHandler(dc.MyMouseLeave);
                this.panel2.Controls[i].MouseMove += new System.Windows.Forms.MouseEventHandler(dc.MyMouseMove);
            }
        } 

        private void MouseDown(object sender, MouseEventArgs e)
        {
            dc.MyMouseDown(sender,e);
            mCurrentSelectObject = sender;
            getProperty();
        }

        private void DelProperty()
        {
            //for (int i = 0; i < this.panel1.Controls.Count; i++)
            //{
            //    this.panel1.Controls[i].MouseDown -= new System.Windows.Forms.MouseEventHandler(MouseDown);
            //    this.panel1.Controls[i].MouseLeave -= new System.EventHandler(dc.MyMouseLeave);
            //    this.panel1.Controls[i].MouseMove -= new System.Windows.Forms.MouseEventHandler(dc.MyMouseMove);
            //}

            for (int i = 0; i < this.panel2.Controls.Count; i++)
            {
                this.panel2.Controls[i].MouseDown -= new System.Windows.Forms.MouseEventHandler(MouseDown);
                this.panel2.Controls[i].MouseLeave -= new System.EventHandler(dc.MyMouseLeave);
                this.panel2.Controls[i].MouseMove -= new System.Windows.Forms.MouseEventHandler(dc.MyMouseMove);
            }
        }
         

        public void tsbtnPrint_Click(object sender, EventArgs e)
        {
            //string picPath = AppDomain.CurrentDomain.BaseDirectory + "temp.bmp";
            //Bitmap bm = new Bitmap(this.zedGraphControl.Width, this.zedGraphControl.Height);
            //this.zedGraphControl.DrawToBitmap(bm, this.zedGraphControl.ClientRectangle); 
            //MemoryStream ms = new MemoryStream();
            //bm.Save(ms,System.Drawing.Imaging.ImageFormat.Bmp);
            //this.pictureBox.Size = bm.Size;
            //this.pictureBox.Image = Image.FromStream(ms);
            //bm.Dispose();
            //ms.Dispose(); 

            FormPrinting.FormPrinting fp = new FormPrinting.FormPrinting(this.panel2);  
            fp.TopMargin = 0; 

            fp.HAlignment = HorizontalAlignment.Center;
            fp.Orientation = FormPrinting.FormPrinting.OrientationENum.Portrait;

            fp.PrintPreview = true;
            fp.TopMargin = 0;
            fp.TrimBlankLines("TestForm");
            fp.TextBoxBoxed = true;
            fp.Print();
            
        }

        //[System.Runtime.InteropServices.DllImport("gdi32.dll")]
        //public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        //private Bitmap memoryImage;
        //private void CaptureScreen()
        //{
        //    Graphics mygraphics = this.panel2.CreateGraphics();
        //    Size s = this.panel2.Size;
        //    memoryImage = new Bitmap(s.Width, s.Height, mygraphics);
        //    Graphics memoryGraphics = Graphics.FromImage(memoryImage);
        //    IntPtr dc1 = mygraphics.GetHdc();
        //    IntPtr dc2 = memoryGraphics.GetHdc();
        //    BitBlt(dc2, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height, dc1, 0, 0, 13369376);
        //    mygraphics.ReleaseHdc(dc1);
        //    memoryGraphics.ReleaseHdc(dc2);
        //}
        //private void printDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //    e.Graphics.DrawImage(memoryImage, 0, 0);
        //}
        //private void printButton_Click(System.Object sender, System.EventArgs e)
        //{
        //    Bitmap bm = new Bitmap(this.zedGraphControl1.Width, this.zedGraphControl1.Height);
        //    this.zedGraphControl1.DrawToBitmap(bm, this.zedGraphControl1.ClientRectangle);
        //    MemoryStream ms = new MemoryStream();
        //    bm.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
        //    this.pictureBox1.Image = null;
        //    this.pictureBox1.Size = bm.Size;
        //    this.pictureBox1.Image = Image.FromStream(ms);
        //    bm.Dispose();
        //    ms.Dispose(); 

        //    CaptureScreen(); 
        //    printDocument1.Print();
        //}


        private void tsbtnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void zedGraphControl1_SizeChanged(object sender, EventArgs e)
        {
            //this.pictureBox.Size = this.zedGraphControl.Size;
        }

        private void tsbtnPrinterSet_Click(object sender, EventArgs e)
        {
            
        }

        private void tsbtnAddText_Click(object sender, EventArgs e)
        {
            Label tb = new Label();
            tb.Text = "label";
            tb.BorderStyle = BorderStyle.None;
            this.panel2.Controls.Add(tb);
            DelProperty();
            initProperty();
        }

        private void tsbtnAddPanel_Click(object sender, EventArgs e)
        {
            Panel p = new Panel();
            p.Width = 500;
            p.Height = 300;
            p.BorderStyle = BorderStyle.None;
            p.BackColor = Color.Transparent;
            this.panel2.Controls.Add(p);
            DelProperty();
            initProperty();
        }

        private void btnSetTitle_Click(object sender, EventArgs e)
        {
            //RWconfig.SetAppSettings("reportTitle", this.txtReportTitle.Text.Trim());
            //RWconfig.SetAppSettings("reportTitleFontSize", this.cmbFontSize.Text.Trim());
            //Font f = new Font("宋体", 8 * float.Parse(this.cmbFontSize.Text), FontStyle.Bold);
            //this.lblLabelSize.Font =f;
            //MessageBox.Show("设置成功");
        }

        private void txtReportTitle_TextChanged(object sender, EventArgs e)
        {
            //this.lblLabelSize.Text = this.txtReportTitle.Text.Trim();
        }

        private void panel2_Resize(object sender, EventArgs e)
        {
            //label4.Text = "width=" + panel2.Width + ",height=" + panel2.Height;
        }

        private void zedGraphControl1_LocationChanged(object sender, EventArgs e)
        {
            //this.pictureBox.Location = this.zedGraphControl.Location;
        }

        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            //string strControl=string.Empty;
            //strControl = "<Report>";
            //foreach (Control c in this.panel2.Controls)
            //{
            //    strControl += "<control type=\"";
            //    strControl += c.GetType().Name+ "\" name=\"" + c.Name +" width=\"" + c.Width + "\" height=\""+c.Height  +"\" left=\""+c.Left.ToString() +"\" top=\"" +c.Top.ToString()+"\" text=\"" + c.Text+"\"" + " fontname=\"" + c.Font.Name+ "\" fontsize=\""+c.Font.Size +"\" >";
            //    strControl += "</control>";
            //}
            //strControl += "</Report>";

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("   ");
            using (XmlWriter writer = XmlWriter.Create(AppDomain.CurrentDomain.BaseDirectory+"reportTensile.xml", settings))
            {
                // Write XML data. 
                writer.WriteStartElement("Report"); 
                foreach (Control c in this.panel2.Controls)
                {
                    writer.WriteStartElement("control");
                    writer.WriteAttributeString("type",c.GetType().Name);
                    writer.WriteAttributeString("name", c.Name);
                    writer.WriteAttributeString("width",c.Width.ToString());
                    writer.WriteAttributeString("height",c.Height.ToString());
                    writer.WriteAttributeString("left", c.Left.ToString());
                    writer.WriteAttributeString("top", c.Top.ToString());
                    writer.WriteAttributeString("text", c.Text);
                    writer.WriteAttributeString("fontname", c.Font.Name.ToString());
                    writer.WriteAttributeString("fontsize", c.Font.Size.ToString()); 
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tsbtnMinimize_Click(object sender, EventArgs e)
        {

        }
    }
}
