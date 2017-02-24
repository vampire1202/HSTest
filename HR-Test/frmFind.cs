using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using System.Xml.XPath;

namespace HR_Test
{
    public partial class frmFind : Form
    {
        private ZedGraph.GraphPane myPanel;
        //RollingPointPairList _RPPList0; 
        //存储采集数据
        private List<gdata>[] _List_Data;
        private List<gdata> _List_Data1;
        //曲线名数组
        private string[] strCurveName = new string[6];
        private string _selTestSampleGroup = string.Empty;
        //}
        string[] _Color_Array;
        //读取曲线的数据
        RollingPointPairList[] _RPPList_Read;
        RollingPointPairList _RPPList_ReadOne;
        //曲线路径
        private string _path = string.Empty;
        //选择试样数组
        private string[] _selTestSampleArray = null;
        //选择试样的曲线颜色数组
        private string[] _selColorArray = null;
        string _selColor = string.Empty;
        //曲线名数组  
        private int _sampleCount = 0;
        public Image img;
        ZedGraph.GraphPane showCurveResultPanel;

        public DataTable m_dt = new DataTable();
        public DataTable dtaver = new DataTable();
        public DataGridView dgvAvg = new DataGridView();
        private bool m_isSelSD = false;
        private bool m_isSelCV = false;
        private bool m_isSelMid = false;

        int _ShowX = 0;
        int _ShowY = 0;

        frmMain _fmMain;

        public frmFind(frmMain fmMain)
        {
            _fmMain = fmMain;
            InitializeComponent();
            showCurveResultPanel = this.zedGraphControl.GraphPane;
            zedGraphControl.Invalidated += new InvalidateEventHandler(zedGraphControl_Invalidated);
        }

        void zedGraphControl_Invalidated(object sender, InvalidateEventArgs e)
        {
            if (this.zedGraphControl.GraphPane.XAxis != null)
            {
                Scale sScale = this.zedGraphControl.GraphPane.XAxis.Scale;

                switch (this.cmbXr.SelectedIndex)// -请选择-   时间,s  位移,μm  应变,μm
                {
                    case 1://时间
                    case 3:
                    case 5:
                        sScale.Mag = 0;
                        sScale.Format = "0.0";
                        break;
                    case 2://位移 
                    case 4:
                        if (sScale.Max > 1000)
                        {
                            sScale.Mag = 3;
                            sScale.Format = "0.000";
                        }
                        else
                        {
                            sScale.Mag = 0;
                            sScale.Format = "0.0";
                        }
                        break;
                }
                this.zedGraphControl.GraphPane.XAxis.Scale.MajorStep = (this.zedGraphControl.GraphPane.XAxis.Scale.Max - this.zedGraphControl.GraphPane.XAxis.Scale.Min) / 5;
                this.zedGraphControl.GraphPane.XAxis.Scale.MinorStep = this.zedGraphControl.GraphPane.XAxis.Scale.MajorStep / 5;
            }

            if (this.zedGraphControl.GraphPane.YAxis != null)
            {
                Scale sScale = this.zedGraphControl.GraphPane.YAxis.Scale;
                switch (this.cmbYr.SelectedIndex)
                {
                    case 1://负荷
                        if (sScale.Max > 1000)
                        {
                            sScale.Mag = 3;
                            sScale.Format = "0.000";
                        }
                        else
                        {
                            sScale.Mag = 0;
                            sScale.Format = "0.0";
                        }
                        break;
                    case 2://应力
                        //if (m_Stress > sScale.Max)
                        //{
                        //    sScale.Max = 2 * sScale.Max;
                        //}
                        sScale.Mag = 0;
                        sScale.Format = "0.0";
                        break;
                    case 3://变形
                        //if (m_Elongate > sScale.Max)
                        //{
                        //    sScale.Max = 2 * sScale.Max;
                        if (sScale.Max > 1000)
                        {
                            sScale.Mag = 3;
                            sScale.Format = "0.000";
                        }
                        else
                        {
                            sScale.Mag = 0;
                            sScale.Format = "0.0";
                        }
                        //}
                        break;
                    case 4://位移
                        //if (m_Displacement > sScale.Max)
                        //{
                        //    sScale.Max = 2 * sScale.Max;
                        if (sScale.Max > 1000)
                        {
                            sScale.Mag = 3;
                            sScale.Format = "0.000";
                        }
                        else
                        {
                            sScale.Mag = 0;
                            sScale.Format = "0.0";
                        }
                        //}
                        break;
                }

                //if (_ResultPanel.YAxis.Scale.Max > 100)
                //{
                //    _ResultPanel.YAxis.Scale.Max = ((int)_ResultPanel.YAxis.Scale.Max / 10) * 10;
                //    _ResultPanel.YAxis.Scale.Min = 0;
                //}

                this.zedGraphControl.GraphPane.YAxis.Scale.MajorStep = (this.zedGraphControl.GraphPane.YAxis.Scale.Max - this.zedGraphControl.GraphPane.YAxis.Scale.Min) / 5;
                this.zedGraphControl.GraphPane.YAxis.Scale.MinorStep = this.zedGraphControl.GraphPane.YAxis.Scale.MajorStep / 5;
            }
        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void tsbtnPrint_Click(object sender, EventArgs e)
        {
            if (_selTestSampleArray != null)
            {
                frmReportEdit_T fre = new frmReportEdit_T(this);
                switch (dataGridView.Tag.ToString())
                {
                    case "GBT23615-2009TensileZong":
                        proPrint23615TensileZong(fre);
                        break;
                    case "GBT23615-2009TensileHeng":
                        proPrint23615TensileHeng(fre);
                        break;
                    case "tb_TestSample":
                        proPrintT(fre);
                        break;
                    case "tb_Compress":
                        proPrintC(fre);
                        break;
                    case "tb_Bend":
                        proPrintB(fre);
                        break;
                    case "GBT28289-2012Tensile":
                        proPrint28289Tensile(fre);
                        break;
                    case "GBT28289-2012Twist":
                        proPrint28289Twist(fre);
                        break;
                    case "GBT28289-2012Shear":
                        proPrint28289Shear(fre);
                        break;
                }
                
                fre.WindowState = FormWindowState.Minimized;
                
                fre.Show();
                
                fre.tsbtnPrintP_Click(sender, e);
                fre.Dispose();
            }
        }

        private void frmFind_Load(object sender, EventArgs e)
        {
            _Color_Array = new string[] { "Red", "Green", "Blue", "Teal", "DarkOrange", "Chocolate", "BlueViolet", "Indigo", "Magenta", "LightCoral", "LawnGreen", "Aqua", "DarkViolet", "DeepPink", "DeepSkyblue", "HotPink", "SpringGreen", "GreenYellow", "Peru", "Black" };
            initResultCurve(this.zedGraphControl);
            _ShowY = this.cmbYr.SelectedIndex = int.Parse(RWconfig.GetAppSettings("ShowY"));
            _ShowX = this.cmbXr.SelectedIndex = int.Parse(RWconfig.GetAppSettings("ShowX"));
            this.dataGridView.Tag = "tb_TestSample"; 
            myPanel = this.zedGraphControl.GraphPane;
            TestStandard.SampleControl.ReadTestStandard(this.cmbTestStandard);
            //btnFind_Click(this.btnFind, e);
            //readFinishSample(this.dataGridView);
        }

        //初始化试验结果曲线
        private void initResultCurve(ZedGraph.ZedGraphControl zgControl)
        {
            #region
            //Random random = new Random();
            //for (int pointIndex = 0; pointIndex < 50; pointIndex++)
            //{
            //  chart.Series[0].Points.AddY(random.Next(32, 95));
            //}
            //// Set series chart type
            //chart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            //// Set point labels
            //chart.Series[0].IsValueShownAsLabel = false;
            //chart.Series[0].IsVisibleInLegend = false;
            //// Enable X axis margin
            //chart.ChartAreas["Default"].AxisX.IsMarginVisible = false;
            //chart.Titles[0].Text = "力-位移";      
            //// Show as 3D
            //chart.ChartAreas["Default"].Area3DStyle.Enable3D = false; 
            #endregion

            //_RPPList0 = new RollingPointPairList(100000);
            Legend l = zgControl.GraphPane.Legend;
            l.IsShowLegendSymbols = true;
            l.Gap = 0.5f;
            l.Position = LegendPos.Top;
            l.Border.IsVisible = false;
            l.FontSpec.Size = 16.0f;
            l.IsVisible = true;

            // Set the titles and axis labels
            GraphPane _ResultPanel = zgControl.GraphPane;
            _ResultPanel.Margin.All = 0;
            _ResultPanel.Title.Text = "";
            _ResultPanel.Title.IsVisible = false;
            _ResultPanel.IsFontsScaled = false;
            zgControl.IsZoomOnMouseCenter = false;

            //XAxis
            //最后的显示值隐藏
            _ResultPanel.XAxis.Scale.FontSpec.Size = 16.0f;
            _ResultPanel.XAxis.Title.FontSpec.Size = 16.0f;
            _ResultPanel.XAxis.Scale.FontSpec.Family = "宋体";
            _ResultPanel.XAxis.Title.FontSpec.FontColor = Color.FromArgb(34, 67, 108);
            _ResultPanel.XAxis.Scale.FontSpec.FontColor = Color.FromArgb(34, 67, 108);
            _ResultPanel.XAxis.Title.Text = "X";
            _ResultPanel.XAxis.Title.Gap = -0.5f;
            _ResultPanel.XAxis.Scale.AlignH = AlignH.Center;
            _ResultPanel.XAxis.Scale.IsReverse = false;
            _ResultPanel.XAxis.Scale.LabelGap = 0;
            _ResultPanel.XAxis.Scale.Format = "0.0";
            _ResultPanel.XAxis.Scale.MinGrace = 0.0;
            _ResultPanel.XAxis.Scale.MaxGrace = 0.0;
            _ResultPanel.XAxis.Scale.Min = 0;
            _ResultPanel.XAxis.Scale.MinAuto = false;
            _ResultPanel.XAxis.Scale.Max = 1;
            _ResultPanel.XAxis.Scale.MaxAuto = false;
            _ResultPanel.XAxis.MajorGrid.IsVisible = true;
            _ResultPanel.XAxis.MajorTic.IsOpposite = false;
            _ResultPanel.XAxis.MinorTic.IsOpposite = false;


            _ResultPanel.YAxis.Title.Text = "Y";
            _ResultPanel.YAxis.Scale.FontSpec.FontColor = Color.FromArgb(34, 67, 108);
            _ResultPanel.YAxis.Title.FontSpec.FontColor = Color.FromArgb(34, 67, 108);
            _ResultPanel.YAxis.Scale.FontSpec.Size = 16.0f;
            _ResultPanel.YAxis.Title.FontSpec.Size = 16.0f;
            _ResultPanel.YAxis.Scale.FontSpec.Family = "宋体";
            _ResultPanel.YAxis.Title.Gap = -0.5f;
            _ResultPanel.YAxis.Scale.Format = "0.0";
            _ResultPanel.YAxis.Scale.LabelGap = 0;
            // Align the Y2 axis labels so they are flush to the axis 
            _ResultPanel.YAxis.Scale.AlignH = AlignH.Center;
            _ResultPanel.YAxis.Scale.Min = 0;
            _ResultPanel.YAxis.Scale.Max = 1;
            _ResultPanel.YAxis.Scale.MaxAuto = false;
            _ResultPanel.YAxis.Scale.MinAuto = false;
            _ResultPanel.YAxis.MajorGrid.IsVisible = true;
            _ResultPanel.YAxis.MajorTic.IsOpposite = false;
            _ResultPanel.YAxis.MinorTic.IsOpposite = false;

            //_List_Data = new List<gdata>(100000);
            ////开始，增加的线是没有数据点的(也就是list为空)
            ////增加无数据的空线条，确定各线条显示的轴。
            //LineItem CurveList0 = myPanel.AddCurve("", _RPPList0, Color.Red, SymbolType.None);//Y1-X1   
            //CurveList0.Line.Width = 1;
            //CurveList0.YAxisIndex = 1;
            zgControl.AxisChange();
        }

        private void RestoreZScale()
        {
            this.zedGraphControl.RestoreScale(this.zedGraphControl.GraphPane);
            int xmax = (int)this.zedGraphControl.GraphPane.XAxis.Scale.Max;
            int cxmax = (xmax / 5) * 5;
            if (xmax > cxmax)
            {
                this.zedGraphControl.GraphPane.XAxis.Scale.Max = cxmax + 5;
            }
            else
            {
                this.zedGraphControl.GraphPane.XAxis.Scale.Max = cxmax;
            }
            this.zedGraphControl.GraphPane.XAxis.Scale.Min = 0;

            this.zedGraphControl.GraphPane.XAxis.Scale.MajorStep = this.zedGraphControl.GraphPane.XAxis.Scale.Max / 5;
            this.zedGraphControl.AxisChange();
            this.zedGraphControl.Invalidate();

            int ymax = (int)this.zedGraphControl.GraphPane.YAxis.Scale.Max;
            int cymax = (ymax / 5) * 5;
            if (ymax > cymax)
            {
                this.zedGraphControl.GraphPane.YAxis.Scale.Max = cymax + 5;
            }
            else
            {
                this.zedGraphControl.GraphPane.YAxis.Scale.Max = cymax;
            }
            this.zedGraphControl.GraphPane.YAxis.Scale.Min = 0;
            this.zedGraphControl.GraphPane.YAxis.Scale.MajorStep = this.zedGraphControl.GraphPane.YAxis.Scale.Max / 5;
            this.zedGraphControl.AxisChange();
            this.zedGraphControl.Invalidate();

            this.zedGraphControl.GraphPane.XAxis.Scale.BaseTic = 0;
            this.zedGraphControl.GraphPane.YAxis.Scale.BaseTic = 0;
            this.zedGraphControl.Refresh();
        } 

        private void ReadGBT7314(DataGridView dg)
        {
            dg.MultiSelect = true;
            dg.DataSource = null;
            dg.Columns.Clear();
            dg.Name = "compress";
            dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            BLL.Compress bllTs = new HR_Test.BLL.Compress();
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(this.txtTestNo.Text.Trim()))
            {
                strWhere += " and testNo='" + this.txtTestNo.Text.Trim() + "'";
            }

            if (!string.IsNullOrEmpty(this.txtTestSampleNo.Text.Trim()))
            {
                strWhere += " and testSampleNO='" + this.txtTestSampleNo.Text.Trim() + "'";
            }

            double maxValue = 0;
            DataSet dsmax = bllTs.GetMaxFmc(" isFinish=true  and testDate>=#" + this.dateTimePicker1.Value.Date + "# and testDate<=#" + this.dateTimePicker2.Value.Date + "#" + strWhere);
            if (dsmax != null)
            {
                if (dsmax.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["Fmc"].ToString()))
                        maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["Fmc"].ToString());
                }
            }

            DataSet ds = bllTs.GetFinishList(" isFinish=true  and testDate>=#" + this.dateTimePicker1.Value.Date + "# and testDate<=#" + this.dateTimePicker2.Value.Date + "#" + strWhere,maxValue);
            DataTable dt = ds.Tables[0];
            dg.DataSource = dt;
            dg.Refresh();
            ds.Dispose();

            DataGridViewCheckBoxColumn chkcol = new DataGridViewCheckBoxColumn();
            chkcol.Name = "选择";
            chkcol.MinimumWidth = 50;

            DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
            c.Name = "曲线";
            dg.Columns.Insert(0, chkcol);
            dg.Columns.Insert(1, c);

            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (i > 19)
                {
                    dg.Rows[i].Cells[1].Style.BackColor = Color.FromName(_Color_Array[i % 20]);
                }
                else
                {
                    dg.Rows[i].Cells[1].Style.BackColor = Color.FromName(_Color_Array[i]);
                }
            }

            dg.Columns[1].Frozen = true;
            dg.Columns[2].Frozen = true;

            if (dg.ColumnCount > 0)
            {
                foreach (DataGridViewColumn dgvc in dg.Columns)
                {
                    dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            dg.Refresh();



            foreach (CurveItem ci in this.zedGraphControl.GraphPane.CurveList)
            {
                ci.Clear();
                ci.Label.Text = "";
            }

            this.zedGraphControl.AxisChange();
            this.zedGraphControl.Refresh();

        }

        private void ReadYBT5349(DataGridView dg)
        {
            //dg.MultiSelect = true;
            //dg.DataSource = null;
            dg.Columns.Clear();
            dg.Name = "bend";
            //dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            BLL.Bend bllBend = new HR_Test.BLL.Bend();
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(this.txtTestNo.Text.Trim()))
            {
                strWhere += " and testNo like '%" + this.txtTestNo.Text.Trim() + "%'";
            }

            if (!string.IsNullOrEmpty(this.txtTestSampleNo.Text.Trim()))
            {
                strWhere += " and testSampleNO like '%" + this.txtTestSampleNo.Text.Trim() + "%'";
            }
            double maxValue = 0;
            DataSet dtmax = bllBend.GetFbbMax("isFinish=true and isEffective=false and testDate>=#" + this.dateTimePicker1.Value.Date + "# and testDate<=#" + this.dateTimePicker2.Value.Date + "#" + strWhere);
            if (dtmax != null)
            {
                if (!string.IsNullOrEmpty(dtmax.Tables[0].Rows[0]["Fbb"].ToString()))
                {
                    maxValue = Convert.ToDouble(dtmax.Tables[0].Rows[0]["Fbb"].ToString());
                }
            }
            DataSet ds = bllBend.GetFinishListDefault("isFinish=true and isEffective=false and testDate>=#" + this.dateTimePicker1.Value.Date + "# and testDate<=#" + this.dateTimePicker2.Value.Date + "#" + strWhere, maxValue);
            DataTable dt = ds.Tables[0];
            dg.DataSource = dt;

            DataGridViewCheckBoxColumn chkcol = new DataGridViewCheckBoxColumn();
            //DataGridViewDisableCheckBoxColumn chkcol = new DataGridViewDisableCheckBoxColumn();            
            chkcol.Name = "选择";

            DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
            c.Visible = true;
            c.Name = "  ";
            dg.Columns.Insert(0, c);
            dg.Columns.Insert(0, chkcol);

            dg.Columns[1].Frozen = true;
            dg.Columns[2].Frozen = true;

            //dg.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);  
            dg.Columns[0].Width = 50;
            dg.Columns[1].Width = 50;
            dg.Columns[2].Width = 100;
            if (dg.ColumnCount > 0)
            {
                foreach (DataGridViewColumn dgvc in dg.Columns)
                {
                    dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvc.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            dg.Refresh();

            for (int i = 0; i < dg.Rows.Count; i++)
            {
                if (i > 19)
                {
                    dg.Rows[i].Cells[1].Style.BackColor = Color.FromName(_Color_Array[i % 20]);
                    dg.Rows[i].Cells[1].Style.ForeColor = Color.FromName(_Color_Array[i % 20]);
                    dg.Rows[i].Cells[1].Style.SelectionForeColor = Color.FromName(_Color_Array[i % 20]);
                    dg.Rows[i].Cells[1].Style.SelectionBackColor = Color.FromName(_Color_Array[i % 20]);
                    dg.Rows[i].Cells[1].Value = _Color_Array[i % 20].ToString();
                }
                else
                {
                    dg.Rows[i].Cells[1].Style.BackColor = Color.FromName(_Color_Array[i]);
                    dg.Rows[i].Cells[1].Style.ForeColor = Color.FromName(_Color_Array[i]);
                    dg.Rows[i].Cells[1].Style.SelectionForeColor = Color.FromName(_Color_Array[i]);
                    dg.Rows[i].Cells[1].Style.SelectionBackColor = Color.FromName(_Color_Array[i]);
                    dg.Rows[i].Cells[1].Value = _Color_Array[i].ToString();
                }
            }

            foreach (CurveItem ci in this.zedGraphControl.GraphPane.CurveList)
            {
                ci.Clear();
                ci.Label.Text = "";
            }

            this.zedGraphControl.AxisChange();
            this.zedGraphControl.Refresh();
            ds.Dispose();
        }

        private PointD getFm(RollingPointPairList rppl)
        {
            PointD fm = new PointD();
            Int32 i = 0;
            while (i < rppl.Count)
            {
                if (fm.Y < rppl[i].Y)
                {
                    fm.Y = rppl[i].Y;
                    fm.X = rppl[i].X;
                }
                i++;
            }
            return fm;
        }

        private void autoSizeDg(DataGrid _dg)
        {
            DataTable _dt = (DataTable)_dg.DataSource;
            DataGridTableStyle myGridTableStyle;
            if (_dg.TableStyles.Count == 0)
            {
                myGridTableStyle = new DataGridTableStyle();
                myGridTableStyle.ColumnHeadersVisible = false;
                for (int i = 0; i < _dt.Columns.Count; i++)
                {
                    DataGridTextBoxColumn dgtbox = new DataGridTextBoxColumn();
                    dgtbox.Alignment = HorizontalAlignment.Left;
                    dgtbox.HeaderText = _dt.Columns[i].ColumnName;
                    dgtbox.MappingName = _dt.Columns[i].ColumnName;
                    //dgtbox.Width = 70;
                    myGridTableStyle.GridColumnStyles.Add(dgtbox);
                }
                _dg.TableStyles.Add(myGridTableStyle);
            }
            //autosize col width
            for (int i = 0; i < _dg.TableStyles[0].GridColumnStyles.Count; i++)
            {
                AutoSizeCol(_dg, i);
            }
        }

        /// autosize the first column
        /// </summary>
        /// <param name="col">column number</param>
        private void AutoSizeCol(DataGrid _dbgrid, int col)
        {
            float width = 0;
            int numRows = ((DataTable)_dbgrid.DataSource).Rows.Count;
            Graphics g = Graphics.FromHwnd(_dbgrid.Handle);
            StringFormat sf = new StringFormat(StringFormat.GenericDefault);
            SizeF size;
            SizeF headerSize;
            headerSize = g.MeasureString(_dbgrid.TableStyles[0].GridColumnStyles[col].HeaderText.ToString(), _dbgrid.Font);
            for (int i = 0; i < numRows; ++i)
            {
                string s = _dbgrid[i, col].ToString();
                size = g.MeasureString(s, _dbgrid.Font);

                if (size.Width > width)
                    width = size.Width;
                if (headerSize.Width > size.Width)
                    width = headerSize.Width;
            }

            _dbgrid.TableStyles[0].GridColumnStyles[col].Width = (int)width + 5;
            g.Dispose();
        }

        private void tsbtnEditReport_Click(object sender, EventArgs e)
        {               
            if (_selTestSampleArray != null)
            {
                frmReportEdit_T fre = new frmReportEdit_T(this);
                switch (dataGridView.Tag.ToString())
                {
                    case "GBT23615-2009TensileZong":
                        proPrint23615TensileZong(fre);
                        break; 
                    case "GBT23615-2009TensileHeng":
                        proPrint23615TensileHeng(fre);
                        break;
                    case "tb_TestSample":
                        proPrintT(fre);
                        break;
                    case "tb_Compress":
                        proPrintC(fre);
                        break;
                    case "tb_Bend":
                        proPrintB(fre);
                    break;
                    case "GBT28289-2012Tensile":
                        proPrint28289Tensile(fre);
                    break;
                    case "GBT28289-2012Twist":
                        proPrint28289Twist(fre);
                    break;
                    case "GBT28289-2012Shear":  
                        proPrint28289Shear(fre);
                    break;
                }
               
                fre.WindowState = FormWindowState.Maximized;                
                fre.Show();
            }
        }

        private void proPrint23615TensileHeng(frmReportEdit_T fre)
        {
            fre.FormBorderStyle = FormBorderStyle.None;
            fre.TopLevel = false;
            fre.Parent = this;
            fre.BringToFront();
            fre.Size = this.Size;
            fre._TestType = "GBT23615-2009TensileHeng";
            //曲线图
            this.zedGraphControl.Dock = DockStyle.None;
            //读取各种报告配置文件
            XmlDocument xd = new XmlDocument();
            int picWidth = 700;
            int picHeight = 430;
            xd.Load(AppDomain.CurrentDomain.BaseDirectory + "GBT23615-2009TensileHeng.xml");
            if (xd != null)
            {
                //读取
                XmlNode node = xd.SelectSingleNode("/Report/ZedGraphControl");
                if (node != null)
                {
                    picWidth = int.Parse(node.Attributes["width"].Value);
                    picHeight = int.Parse(node.Attributes["height"].Value);
                }
            }

            //以控件方式打印

            fre._SelTestSampleArray = this._selTestSampleArray;
            fre._SelColorArray = this._selColorArray;
            int ab = 0;
            //基本参数,读取试验方法参数 
            if (_selTestSampleArray != null)
            //查找第一个选择的试样用的何种试验方法
            {
                string testSampleNo = _selTestSampleArray[0];
                BLL.GBT236152009_TensileHeng bllZong = new HR_Test.BLL.GBT236152009_TensileHeng();
                Model.GBT236152009_TensileHeng modZong = bllZong.GetModel(testSampleNo);

                //读取试验方法
                string testMethod = modZong.testMethod;
                //从试验方法库中查找该方法的基本试验参数
                BLL.GBT236152009_Method tsMethod = new HR_Test.BLL.GBT236152009_Method();
                Model.GBT236152009_Method modMethod = tsMethod.GetModel(testMethod);

                if (modMethod == null)
                {
                    MessageBox.Show(this, "试验方法 '" + testMethod + "' 不存在,请检查!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //读取试验方法的选择结果项
                BLL.GBT236152009_SelHeng bllSel = new HR_Test.BLL.GBT236152009_SelHeng();
                Model.GBT236152009_SelHeng mSel = bllSel.GetModel(testMethod);

                #region 读取试验方法的基本参数

                System.Windows.Forms.Label lbl1 = new System.Windows.Forms.Label();
                lbl1.Width = 175;
                lbl1.Name = "lbl1";
                lbl1.Text = "送检单位:" + modMethod.sendCompany;
                lbl1.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl1);

                System.Windows.Forms.Label lbl2 = new System.Windows.Forms.Label();
                lbl2.Width = 175;
                lbl2.Name = "lbl2";
                lbl2.Text = "材料规格:" + modMethod.stuffSpec;
                lbl2.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl2);

                System.Windows.Forms.Label lbl3 = new System.Windows.Forms.Label();
                lbl3.Width = 175;
                lbl3.Name = "lbl3";
                lbl3.Text = "材料牌号:" + modMethod.stuffCardNo;
                lbl3.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl3);

                System.Windows.Forms.Label lbl4 = new System.Windows.Forms.Label();
                lbl4.Width = 175;
                lbl4.Name = "lbl4";
                lbl4.Text = "材料类型:" + modMethod.stuffType;
                lbl4.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl4);

                System.Windows.Forms.Label lbl5 = new System.Windows.Forms.Label();
                lbl5.Width = 175;
                lbl5.Name = "lbl5";
                lbl5.Text = "试样标识:" + modMethod.sampleCharacter;
                lbl5.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl5);

                System.Windows.Forms.Label lbl6 = new System.Windows.Forms.Label();
                lbl6.Width = 175;
                lbl6.Name = "lbl6";
                if (modMethod.temperature != 0)
                    lbl6.Text = "试验温度:" + modMethod.temperature + "℃";
                else
                    lbl6.Text = "试验温度:" + "-";
                lbl6.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl6);

                System.Windows.Forms.Label lbl7 = new System.Windows.Forms.Label();
                lbl7.Width = 175;
                lbl7.Name = "lbl7";
                lbl7.Text = "试验湿度:" + modMethod.humidity + "%";
                lbl7.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl7);

                System.Windows.Forms.Label lbl8 = new System.Windows.Forms.Label();
                lbl8.Width = 175;
                lbl8.Name = "lbl8";
                lbl8.Text = "热处理状态:" + modMethod.hotStatus;
                lbl8.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl8);

                System.Windows.Forms.Label lbl9 = new System.Windows.Forms.Label();
                lbl9.Width = 175;
                lbl9.Name = "lbl9";
                lbl9.Text = "试样取样:" + modMethod.getSample;
                lbl9.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl9);

                //试验条件
                System.Windows.Forms.Label lbl15 = new System.Windows.Forms.Label();
                lbl15.Width = 175;
                lbl15.Name = "lbl15";
                lbl15.Text = "试验条件:" + modMethod.condition;
                lbl15.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl15);

                //控制方式
                System.Windows.Forms.Label lbl16 = new System.Windows.Forms.Label();
                lbl16.Width = 175;
                lbl16.Name = "lbl16";
                lbl16.Text = "控制方式:" + modMethod.controlmode;
                lbl16.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl16);

                //试验设备
                System.Windows.Forms.Label lbl17 = new System.Windows.Forms.Label();
                lbl17.Width = 175;
                lbl17.Name = "lbl17";
                lbl17.Text = "试验设备:" + modMethod.mathineType;
                lbl17.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl17);

                //拉伸方式
                System.Windows.Forms.Label lbl19 = new System.Windows.Forms.Label();
                lbl19.Width = 175;
                lbl19.Name = "lbl19";
                lbl19.Text = "拉伸方式:" + modMethod.xmlPath;
                lbl19.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl19);

                //试验标准
                System.Windows.Forms.Label lbl18 = new System.Windows.Forms.Label();
                lbl18.Width = 220;
                lbl18.Name = "lbl18";
                lbl18.Text = "试验标准:" + modMethod.testStandard;
                lbl18.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl18);

                fre.flowLayoutPanel.Refresh();
                #endregion

                //所选择的试验结果列
                // 0 选择结果项 1 计算结果项平均值
                //StringBuilder[] strSel = new StringBuilder[2];

                //StringBuilder strSelcol = strSel[0];
                //StringBuilder strSelcolAver = strSel[1];

                #region 读取选择的试样的试验结果
                string strSqlSample = string.Empty;
                foreach (string str in _selTestSampleArray)
                {
                    strSqlSample += str + "','";
                }
                strSqlSample = strSqlSample.Remove(strSqlSample.Length - 2, 2);
                string testNo = modZong.testNo;

                //TestStandard.GBT28289_2012.Shear.readFinishSample(this.dataGridView, this.dgvAvg, testNo, dateTimePicker1, dateTimePicker2, this.zedGraphControl);
                //生成试验结果列表
                m_dt = TestStandard.GBT23615_2009.TensileHeng.CreateFinishViewReport(testMethod, " testSampleNo in ('" + strSqlSample + ")");
                //生成试验结果平均值表
                TestStandard.GBT23615_2009.TensileHeng.CreateAverageViewReport(testMethod, " testSampleNo in ('" + strSqlSample + ")", this.dgvAvg);
                dtaver = (DataTable)this.dgvAvg.DataSource;
                #endregion
            }
        }

        private void proPrint23615TensileZong(frmReportEdit_T fre)
        {
            fre.FormBorderStyle = FormBorderStyle.None;
            fre.TopLevel = false;
            fre.Parent = this;
            fre.BringToFront();
            fre.Size = this.Size;
            fre._TestType = "GBT23615-2009TensileZong";
            //曲线图
            this.zedGraphControl.Dock = DockStyle.None;
            //读取各种报告配置文件
            XmlDocument xd = new XmlDocument();
            int picWidth = 700;
            int picHeight = 430;
            xd.Load(AppDomain.CurrentDomain.BaseDirectory + "GBT23615-2009TensileZong.xml");
            if (xd != null)
            {
                //读取
                XmlNode node = xd.SelectSingleNode("/Report/ZedGraphControl");
                if (node != null)
                {
                    picWidth = int.Parse(node.Attributes["width"].Value);
                    picHeight = int.Parse(node.Attributes["height"].Value);
                }
            }

            //以控件方式打印

            fre._SelTestSampleArray = this._selTestSampleArray;
            fre._SelColorArray = this._selColorArray;
            int ab = 0;
            //基本参数,读取试验方法参数 
            if (_selTestSampleArray != null)
            //查找第一个选择的试样用的何种试验方法
            {
                string testSampleNo = _selTestSampleArray[0];
                BLL.GBT236152009_TensileZong bllZong = new HR_Test.BLL.GBT236152009_TensileZong();
                Model.GBT236152009_TensileZong modZong = bllZong.GetModel(testSampleNo);

                //读取试验方法
                string testMethod = modZong.testMethod;
                //从试验方法库中查找该方法的基本试验参数
                BLL.GBT236152009_Method tsMethod = new HR_Test.BLL.GBT236152009_Method();
                Model.GBT236152009_Method modMethod = tsMethod.GetModel(testMethod);

                if (modMethod == null)
                {
                    MessageBox.Show(this, "试验方法 '" + testMethod + "' 不存在,请检查!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //读取试验方法的选择结果项
                BLL.GBT236152009_SelZong bllSel = new HR_Test.BLL.GBT236152009_SelZong();
                Model.GBT236152009_SelZong mSel = bllSel.GetModel(testMethod);

                #region 读取试验方法的基本参数

                System.Windows.Forms.Label lbl1 = new System.Windows.Forms.Label();
                lbl1.Width = 175;
                lbl1.Name = "lbl1";
                lbl1.Text = "送检单位:" + modMethod.sendCompany;
                lbl1.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl1);

                System.Windows.Forms.Label lbl2 = new System.Windows.Forms.Label();
                lbl2.Width = 175;
                lbl2.Name = "lbl2";
                lbl2.Text = "材料规格:" + modMethod.stuffSpec;
                lbl2.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl2);

                System.Windows.Forms.Label lbl3 = new System.Windows.Forms.Label();
                lbl3.Width = 175;
                lbl3.Name = "lbl3";
                lbl3.Text = "材料牌号:" + modMethod.stuffCardNo;
                lbl3.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl3);

                System.Windows.Forms.Label lbl4 = new System.Windows.Forms.Label();
                lbl4.Width = 175;
                lbl4.Name = "lbl4";
                lbl4.Text = "材料类型:" + modMethod.stuffType;
                lbl4.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl4);

                System.Windows.Forms.Label lbl5 = new System.Windows.Forms.Label();
                lbl5.Width = 175;
                lbl5.Name = "lbl5";
                lbl5.Text = "试样标识:" + modMethod.sampleCharacter;
                lbl5.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl5);

                System.Windows.Forms.Label lbl6 = new System.Windows.Forms.Label();
                lbl6.Width = 175;
                lbl6.Name = "lbl6";
                if (modMethod.temperature != 0)
                    lbl6.Text = "试验温度:" + modMethod.temperature + "℃";
                else
                    lbl6.Text = "试验温度:" + "-";
                lbl6.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl6);

                System.Windows.Forms.Label lbl7 = new System.Windows.Forms.Label();
                lbl7.Width = 175;
                lbl7.Name = "lbl7";
                lbl7.Text = "试验湿度:" + modMethod.humidity + "%";
                lbl7.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl7);

                System.Windows.Forms.Label lbl8 = new System.Windows.Forms.Label();
                lbl8.Width = 175;
                lbl8.Name = "lbl8";
                lbl8.Text = "热处理状态:" + modMethod.hotStatus;
                lbl8.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl8);

                System.Windows.Forms.Label lbl9 = new System.Windows.Forms.Label();
                lbl9.Width = 175;
                lbl9.Name = "lbl9";
                lbl9.Text = "试样取样:" + modMethod.getSample;
                lbl9.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl9);

                //试验条件
                System.Windows.Forms.Label lbl15 = new System.Windows.Forms.Label();
                lbl15.Width = 175;
                lbl15.Name = "lbl15";
                lbl15.Text = "试验条件:" + modMethod.condition;
                lbl15.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl15);

                //控制方式
                System.Windows.Forms.Label lbl16 = new System.Windows.Forms.Label();
                lbl16.Width = 175;
                lbl16.Name = "lbl16";
                lbl16.Text = "控制方式:" + modMethod.controlmode;
                lbl16.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl16);

                //试验设备
                System.Windows.Forms.Label lbl17 = new System.Windows.Forms.Label();
                lbl17.Width = 175;
                lbl17.Name = "lbl17";
                lbl17.Text = "试验设备:" + modMethod.mathineType;
                lbl17.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl17);

                //拉伸方式
                System.Windows.Forms.Label lbl19 = new System.Windows.Forms.Label();
                lbl19.Width = 175;
                lbl19.Name = "lbl19";
                lbl19.Text = "拉伸方式:" + modMethod.xmlPath;
                lbl19.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl19);

                //试验标准
                System.Windows.Forms.Label lbl18 = new System.Windows.Forms.Label();
                lbl18.Width = 220;
                lbl18.Name = "lbl18";
                lbl18.Text = "试验标准:" + modMethod.testStandard;
                lbl18.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl18);

                fre.flowLayoutPanel.Refresh();
                #endregion

                //所选择的试验结果列
                // 0 选择结果项 1 计算结果项平均值
                StringBuilder[] strSel = new StringBuilder[2];

                StringBuilder strSelcol = strSel[0];
                StringBuilder strSelcolAver = strSel[1];

                #region 读取选择的试样的试验结果
                string strSqlSample = string.Empty;
                foreach (string str in _selTestSampleArray)
                {
                    strSqlSample += str + "','";
                }
                strSqlSample = strSqlSample.Remove(strSqlSample.Length - 2, 2);
                string testNo = modZong.testNo;

                //TestStandard.GBT28289_2012.Shear.readFinishSample(this.dataGridView, this.dgvAvg, testNo, dateTimePicker1, dateTimePicker2, this.zedGraphControl);
                //生成试验结果列表
                m_dt = TestStandard.GBT23615_2009.TensileZong.CreateFinishViewReport(testMethod, " testSampleNo in ('" + strSqlSample + ")");
                //生成试验结果平均值表
                TestStandard.GBT23615_2009.TensileZong.CreateAverageViewReport(testMethod, " testSampleNo in ('" + strSqlSample + ")", this.dgvAvg);
                dtaver = (DataTable)this.dgvAvg.DataSource;
                #endregion
            }
        }

        private void proPrint28289Twist(frmReportEdit_T fre)
        {
            fre.FormBorderStyle = FormBorderStyle.None;
            fre.TopLevel = false;
            fre.Parent = this;
            fre.BringToFront();
            fre.Size = this.Size;
            fre._TestType = "GBT28289-2012Twist";
            //曲线图
            this.zedGraphControl.Dock = DockStyle.None;
            //读取各种报告配置文件
            XmlDocument xd = new XmlDocument();
            int picWidth = 700;
            int picHeight = 430;
            xd.Load(AppDomain.CurrentDomain.BaseDirectory + "GBT28289-2012Tensile.xml");
            if (xd != null)
            {
                //读取
                XmlNode node = xd.SelectSingleNode("/Report/ZedGraphControl");
                if (node != null)
                {
                    picWidth = int.Parse(node.Attributes["width"].Value);
                    picHeight = int.Parse(node.Attributes["height"].Value);
                }
            }

            //以控件方式打印

            fre._SelTestSampleArray = this._selTestSampleArray;
            fre._SelColorArray = this._selColorArray;
            int ab = 0;
            //基本参数,读取试验方法参数 
            if (_selTestSampleArray != null)
            //查找第一个选择的试样用的何种试验方法
            {
                string testSampleNo = _selTestSampleArray[0];
                BLL.GBT282892012_Twist bllShear = new HR_Test.BLL.GBT282892012_Twist();
                Model.GBT282892012_Twist modShear = bllShear.GetModel(testSampleNo);

                //读取试验方法
                string testMethod = modShear.testMethod;
                //从试验方法库中查找该方法的基本试验参数
                BLL.GBT282892012_Method tsMethod = new HR_Test.BLL.GBT282892012_Method();
                Model.GBT282892012_Method modMethod = tsMethod.GetModel(testMethod);

                if (modMethod == null)
                {
                    MessageBox.Show(this, "试验方法 '" + testMethod + "' 不存在,请检查!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //读取试验方法的选择结果项
                BLL.GBT282892012_TwistSel bllSel = new HR_Test.BLL.GBT282892012_TwistSel();
                Model.GBT282892012_TwistSel mSel = bllSel.GetModel(testMethod);

                #region 读取试验方法的基本参数

                System.Windows.Forms.Label lbl1 = new System.Windows.Forms.Label();
                lbl1.Width = 175;
                lbl1.Name = "lbl1";
                lbl1.Text = "送检单位:" + modMethod.sendCompany;
                lbl1.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl1);

                System.Windows.Forms.Label lbl2 = new System.Windows.Forms.Label();
                lbl2.Width = 175;
                lbl2.Name = "lbl2";
                lbl2.Text = "材料规格:" + modMethod.stuffSpec;
                lbl2.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl2);

                System.Windows.Forms.Label lbl3 = new System.Windows.Forms.Label();
                lbl3.Width = 175;
                lbl3.Name = "lbl3";
                lbl3.Text = "材料牌号:" + modMethod.stuffCardNo;
                lbl3.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl3);

                System.Windows.Forms.Label lbl4 = new System.Windows.Forms.Label();
                lbl4.Width = 175;
                lbl4.Name = "lbl4";
                lbl4.Text = "材料类型:" + modMethod.stuffType;
                lbl4.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl4);

                System.Windows.Forms.Label lbl5 = new System.Windows.Forms.Label();
                lbl5.Width = 175;
                lbl5.Name = "lbl5";
                lbl5.Text = "试样标识:" + modMethod.sampleCharacter;
                lbl5.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl5);

                System.Windows.Forms.Label lbl6 = new System.Windows.Forms.Label();
                lbl6.Width = 175;
                lbl6.Name = "lbl6";
                if (modMethod.temperature != 0)
                    lbl6.Text = "试验温度:" + modMethod.temperature + "℃";
                else
                    lbl6.Text = "试验温度:" + "-";
                lbl6.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl6);

                System.Windows.Forms.Label lbl7 = new System.Windows.Forms.Label();
                lbl7.Width = 175;
                lbl7.Name = "lbl7";
                lbl7.Text = "试验湿度:" + modMethod.humidity + "%";
                lbl7.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl7);

                System.Windows.Forms.Label lbl8 = new System.Windows.Forms.Label();
                lbl8.Width = 175;
                lbl8.Name = "lbl8";
                lbl8.Text = "热处理状态:" + modMethod.hotStatus;
                lbl8.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl8);

                System.Windows.Forms.Label lbl9 = new System.Windows.Forms.Label();
                lbl9.Width = 175;
                lbl9.Name = "lbl9";
                lbl9.Text = "试样取样:" + modMethod.getSample;
                lbl9.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl9);

                //试验条件
                System.Windows.Forms.Label lbl15 = new System.Windows.Forms.Label();
                lbl15.Width = 175;
                lbl15.Name = "lbl15";
                lbl15.Text = "试验条件:" + modMethod.condition;
                lbl15.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl15);

                //控制方式
                System.Windows.Forms.Label lbl16 = new System.Windows.Forms.Label();
                lbl16.Width = 175;
                lbl16.Name = "lbl16";
                lbl16.Text = "控制方式:" + modMethod.controlmode;
                lbl16.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl16);

                //试验设备
                System.Windows.Forms.Label lbl17 = new System.Windows.Forms.Label();
                lbl17.Width = 175;
                lbl17.Name = "lbl17";
                lbl17.Text = "试验设备:" + modMethod.mathineType;
                lbl17.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl17);

                //试验标准
                System.Windows.Forms.Label lbl18 = new System.Windows.Forms.Label();
                lbl18.Width = 175;
                lbl18.Name = "lbl18";
                lbl18.Text = "试验标准:" + modMethod.testStandard;
                lbl18.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl18);

                fre.flowLayoutPanel.Refresh();
                #endregion

                //所选择的试验结果列
                // 0 选择结果项 1 计算结果项平均值
                //StringBuilder[] strSel = new StringBuilder[2];

                //StringBuilder strSelcol = strSel[0];
                //StringBuilder strSelcolAver = strSel[1];

                #region 读取选择的试样的试验结果
                string strSqlSample = string.Empty;
                foreach (string str in _selTestSampleArray)
                {
                    strSqlSample += str + "','";
                }
                strSqlSample = strSqlSample.Remove(strSqlSample.Length - 2, 2);
                string testNo = modShear.testNo;

                //TestStandard.GBT28289_2012.Shear.readFinishSample(this.dataGridView, this.dgvAvg, testNo, dateTimePicker1, dateTimePicker2, this.zedGraphControl);
                //生成试验结果列表
                m_dt = TestStandard.GBT28289_2012.Twist.CreateFinishViewReport(testMethod, " testSampleNo in ('" + strSqlSample + ")");
                //生成试验结果平均值表
                TestStandard.GBT28289_2012.Twist.CreateAverageViewReport(testMethod, " testSampleNo in ('" + strSqlSample + ")", this.dgvAvg);
                dtaver = (DataTable)this.dgvAvg.DataSource;
                #endregion
            }
        }

        

        private void proPrint28289Tensile(frmReportEdit_T fre)
        {
            fre.FormBorderStyle = FormBorderStyle.None;
            fre.TopLevel = false;
            fre.Parent = this;
            fre.BringToFront();
            fre.Size = this.Size;
            fre._TestType = "GBT28289-2012Tensile";
            //曲线图
            this.zedGraphControl.Dock = DockStyle.None;
            //读取各种报告配置文件
            XmlDocument xd = new XmlDocument();
            int picWidth = 700;
            int picHeight = 430;
            xd.Load(AppDomain.CurrentDomain.BaseDirectory + "GBT28289-2012Tensile.xml");
            if (xd != null)
            {
                //读取
                XmlNode node = xd.SelectSingleNode("/Report/ZedGraphControl");
                if (node != null)
                {
                    picWidth = int.Parse(node.Attributes["width"].Value);
                    picHeight = int.Parse(node.Attributes["height"].Value);
                }
            }

            //以控件方式打印

            fre._SelTestSampleArray = this._selTestSampleArray;
            fre._SelColorArray = this._selColorArray;
            int ab = 0;
            //基本参数,读取试验方法参数 
            if (_selTestSampleArray != null)
            //查找第一个选择的试样用的何种试验方法
            {
                string testSampleNo = _selTestSampleArray[0];
                BLL.GBT282892012_Tensile bllShear = new HR_Test.BLL.GBT282892012_Tensile();
                Model.GBT282892012_Tensile modShear = bllShear.GetModel(testSampleNo);

                //读取试验方法
                string testMethod = modShear.testMethod;
                //从试验方法库中查找该方法的基本试验参数
                BLL.GBT282892012_Method tsMethod = new HR_Test.BLL.GBT282892012_Method();
                Model.GBT282892012_Method modMethod = tsMethod.GetModel(testMethod);

                if (modMethod == null)
                {
                    MessageBox.Show(this, "试验方法 '" + testMethod + "' 不存在,请检查!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //读取试验方法的选择结果项
                BLL.GBT282892012_TensileSel bllSel = new HR_Test.BLL.GBT282892012_TensileSel();
                Model.GBT282892012_TensileSel mSel = bllSel.GetModel(testMethod);

                #region 读取试验方法的基本参数

                System.Windows.Forms.Label lbl1 = new System.Windows.Forms.Label();
                lbl1.Width = 175;
                lbl1.Name = "lbl1";
                lbl1.Text = "送检单位:" + modMethod.sendCompany;
                lbl1.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl1);

                System.Windows.Forms.Label lbl2 = new System.Windows.Forms.Label();
                lbl2.Width = 175;
                lbl2.Name = "lbl2";
                lbl2.Text = "材料规格:" + modMethod.stuffSpec;
                lbl2.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl2);

                System.Windows.Forms.Label lbl3 = new System.Windows.Forms.Label();
                lbl3.Width = 175;
                lbl3.Name = "lbl3";
                lbl3.Text = "材料牌号:" + modMethod.stuffCardNo;
                lbl3.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl3);

                System.Windows.Forms.Label lbl4 = new System.Windows.Forms.Label();
                lbl4.Width = 175;
                lbl4.Name = "lbl4";
                lbl4.Text = "材料类型:" + modMethod.stuffType;
                lbl4.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl4);

                System.Windows.Forms.Label lbl5 = new System.Windows.Forms.Label();
                lbl5.Width = 175;
                lbl5.Name = "lbl5";
                lbl5.Text = "试样标识:" + modMethod.sampleCharacter;
                lbl5.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl5);

                System.Windows.Forms.Label lbl6 = new System.Windows.Forms.Label();
                lbl6.Width = 175;
                lbl6.Name = "lbl6";
                if (modMethod.temperature != 0)
                    lbl6.Text = "试验温度:" + modMethod.temperature + "℃";
                else
                    lbl6.Text = "试验温度:" + "-";
                lbl6.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl6);

                System.Windows.Forms.Label lbl7 = new System.Windows.Forms.Label();
                lbl7.Width = 175;
                lbl7.Name = "lbl7";
                lbl7.Text = "试验湿度:" + modMethod.humidity + "%";
                lbl7.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl7);

                System.Windows.Forms.Label lbl8 = new System.Windows.Forms.Label();
                lbl8.Width = 175;
                lbl8.Name = "lbl8";
                lbl8.Text = "热处理状态:" + modMethod.hotStatus;
                lbl8.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl8);

                System.Windows.Forms.Label lbl9 = new System.Windows.Forms.Label();
                lbl9.Width = 175;
                lbl9.Name = "lbl9";
                lbl9.Text = "试样取样:" + modMethod.getSample;
                lbl9.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl9);

                //试验条件
                System.Windows.Forms.Label lbl15 = new System.Windows.Forms.Label();
                lbl15.Width = 175;
                lbl15.Name = "lbl15";
                lbl15.Text = "试验条件:" + modMethod.condition;
                lbl15.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl15);

                //控制方式
                System.Windows.Forms.Label lbl16 = new System.Windows.Forms.Label();
                lbl16.Width = 175;
                lbl16.Name = "lbl16";
                lbl16.Text = "控制方式:" + modMethod.controlmode;
                lbl16.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl16);

                //试验设备
                System.Windows.Forms.Label lbl17 = new System.Windows.Forms.Label();
                lbl17.Width = 175;
                lbl17.Name = "lbl17";
                lbl17.Text = "试验设备:" + modMethod.mathineType;
                lbl17.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl17);

                //试验标准
                System.Windows.Forms.Label lbl18 = new System.Windows.Forms.Label();
                lbl18.Width = 175;
                lbl18.Name = "lbl18";
                lbl18.Text = "试验标准:" + modMethod.testStandard;
                lbl18.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl18);

                fre.flowLayoutPanel.Refresh();
                #endregion

                //所选择的试验结果列
                // 0 选择结果项 1 计算结果项平均值
                //StringBuilder[] strSel = new StringBuilder[2];

                //StringBuilder strSelcol = strSel[0];
                //StringBuilder strSelcolAver = strSel[1];

                #region 读取选择的试样的试验结果
                string strSqlSample = string.Empty;
                foreach (string str in _selTestSampleArray)
                {
                    strSqlSample += str + "','";
                }
                strSqlSample = strSqlSample.Remove(strSqlSample.Length - 2, 2);
                string testNo = modShear.testNo;

                //TestStandard.GBT28289_2012.Shear.readFinishSample(this.dataGridView, this.dgvAvg, testNo, dateTimePicker1, dateTimePicker2, this.zedGraphControl);
                //生成试验结果列表
                m_dt = TestStandard.GBT28289_2012.Tensile.CreateFinishViewReport(testMethod," testSampleNo in ('" + strSqlSample + ")" );
                //生成试验结果平均值表
                TestStandard.GBT28289_2012.Tensile.CreateAverageViewReport(testMethod, " testSampleNo in ('" + strSqlSample + ")", this.dgvAvg);
                dtaver = (DataTable)this.dgvAvg.DataSource;
                #endregion
            }
        }

        private void proPrint28289Shear(frmReportEdit_T fre)
        {
            fre.FormBorderStyle = FormBorderStyle.None;
            fre.TopLevel = false;
            fre.Parent = this;
            fre.BringToFront();
            fre.Size = this.Size;
            fre._TestType = "GBT28289-2012Shear";
            //曲线图
            this.zedGraphControl.Dock = DockStyle.None;
            //读取各种报告配置文件
            XmlDocument xd = new XmlDocument();
            int picWidth = 700;
            int picHeight = 430;
            xd.Load(AppDomain.CurrentDomain.BaseDirectory + "GBT28289-2012Shear.xml");
            if (xd != null)
            {
                //读取
                XmlNode node = xd.SelectSingleNode("/Report/ZedGraphControl");
                if (node != null)
                {
                    picWidth = int.Parse(node.Attributes["width"].Value);
                    picHeight = int.Parse(node.Attributes["height"].Value);
                }
            }

            //以控件方式打印

            fre._SelTestSampleArray = this._selTestSampleArray;
            fre._SelColorArray = this._selColorArray;
            int ab = 0;
            //基本参数,读取试验方法参数 
            if (_selTestSampleArray != null)
            //查找第一个选择的试样用的何种试验方法
            {
                string testSampleNo = _selTestSampleArray[0];
                BLL.GBT282892012_Shear bllShear = new HR_Test.BLL.GBT282892012_Shear();
                Model.GBT282892012_Shear modShear = bllShear.GetModel(testSampleNo);

                //读取试验方法
                string testMethod = modShear.testMethod;
                //从试验方法库中查找该方法的基本试验参数
                BLL.GBT282892012_Method tsMethod = new HR_Test.BLL.GBT282892012_Method();
                Model.GBT282892012_Method modMethod = tsMethod.GetModel(testMethod);

                if (modMethod == null)
                {
                    MessageBox.Show(this, "试验方法 '" + testMethod + "' 不存在,请检查!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //读取试验方法的选择结果项
                BLL.GBT282892012_ShearSel bllSel = new HR_Test.BLL.GBT282892012_ShearSel();
                Model.GBT282892012_ShearSel mSel = bllSel.GetModel(testMethod);

                #region 读取试验方法的基本参数
               
                System.Windows.Forms.Label lbl1 = new System.Windows.Forms.Label();
                lbl1.Width = 175;
                lbl1.Name = "lbl1";
                lbl1.Text = "送检单位:" + modMethod.sendCompany;
                lbl1.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl1);
               
                System.Windows.Forms.Label lbl2 = new System.Windows.Forms.Label();
                lbl2.Width = 175;
                lbl2.Name = "lbl2";
                lbl2.Text = "材料规格:" + modMethod.stuffSpec;
                lbl2.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl2);
              
                System.Windows.Forms.Label lbl3 = new System.Windows.Forms.Label();
                lbl3.Width = 175;
                lbl3.Name = "lbl3";
                lbl3.Text = "材料牌号:" + modMethod.stuffCardNo;
                lbl3.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl3);
               
                System.Windows.Forms.Label lbl4 = new System.Windows.Forms.Label();
                lbl4.Width = 175;
                lbl4.Name = "lbl4";
                lbl4.Text = "材料类型:" + modMethod.stuffType;
                lbl4.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl4);
              
                System.Windows.Forms.Label lbl5 = new System.Windows.Forms.Label();
                lbl5.Width = 175;
                lbl5.Name = "lbl5";
                lbl5.Text = "试样标识:" + modMethod.sampleCharacter;
                lbl5.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl5);
               
                System.Windows.Forms.Label lbl6 = new System.Windows.Forms.Label();
                lbl6.Width = 175;
                lbl6.Name = "lbl6";
                if (modMethod.temperature != 0)
                    lbl6.Text = "试验温度:" + modMethod.temperature + "℃";
                else
                    lbl6.Text = "试验温度:" + "-";
                lbl6.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl6);
               
                System.Windows.Forms.Label lbl7 = new System.Windows.Forms.Label();
                lbl7.Width = 175;
                lbl7.Name = "lbl7";
                lbl7.Text = "试验湿度:" + modMethod.humidity + "%";
                lbl7.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl7);
              
                System.Windows.Forms.Label lbl8 = new System.Windows.Forms.Label();
                lbl8.Width = 175;
                lbl8.Name = "lbl8";
                lbl8.Text = "热处理状态:" + modMethod.hotStatus;
                lbl8.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl8);
               
                System.Windows.Forms.Label lbl9 = new System.Windows.Forms.Label();
                lbl9.Width = 175;
                lbl9.Name = "lbl9";
                lbl9.Text = "试样取样:" + modMethod.getSample;
                lbl9.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl9);
                
                ////原始标距
                //System.Windows.Forms.Label lbl10 = new System.Windows.Forms.Label();
                //lbl10.Width = 175;
                //lbl10.Name = "lbl10";
                //lbl10.Text = "跨距:" + modMethod.Ls + " mm";
                //lbl10.Font = new Font("宋体", 10);
                //fre.flowLayoutPanel.Controls.Add(lbl10);

                ////平行长度
                //System.Windows.Forms.Label lbl11 = new System.Windows.Forms.Label();
                //lbl11.Width = 175;
                //lbl11.Name = "lbl11";
                //lbl11.Text = "挠度计跨距:" + modMethod.Le + " mm";
                //lbl11.Font = new Font("宋体", 10);
                //fre.flowLayoutPanel.Controls.Add(lbl11);

                //System.Windows.Forms.Label lbl12 = new System.Windows.Forms.Label();
                //lbl12.Width = 175;
                //lbl12.Name = "lbl12";
                //lbl12.Text = "挠度放大倍数:" + modMethod.n;
                //lbl12.Font = new Font("宋体", 10);
                //fre.flowLayoutPanel.Controls.Add(lbl12);

                //System.Windows.Forms.Label lbl13 = new System.Windows.Forms.Label();
                //lbl13.Width = 175;
                //lbl13.Name = "lbl13";
                //if (modMethod.l_l != 0)
                //    lbl13.Text = "力臂:" + modMethod.l_l + " mm";
                //else
                //    lbl13.Text = "力臂:-";
                //lbl13.Font = new Font("宋体", 10);
                //fre.flowLayoutPanel.Controls.Add(lbl13);

                //System.Windows.Forms.Label lbl14 = new System.Windows.Forms.Label();
                //lbl14.Width = 175;
                //lbl14.Name = "lbl14";
                //lbl14.Text = "弯曲类型:" + modMethod.testType;
                //lbl14.Font = new Font("宋体", 10);
                //fre.flowLayoutPanel.Controls.Add(lbl14);

                //试验条件
                System.Windows.Forms.Label lbl15 = new System.Windows.Forms.Label();
                lbl15.Width = 175;
                lbl15.Name = "lbl15";
                lbl15.Text = "试验条件:" + modMethod.condition;
                lbl15.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl15);

                //控制方式
                System.Windows.Forms.Label lbl16 = new System.Windows.Forms.Label();
                lbl16.Width = 175;
                lbl16.Name = "lbl16";
                lbl16.Text = "控制方式:" + modMethod.controlmode;
                lbl16.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl16);

                //试验设备
                System.Windows.Forms.Label lbl17 = new System.Windows.Forms.Label();
                lbl17.Width = 175;
                lbl17.Name = "lbl17";
                lbl17.Text = "试验设备:" + modMethod.mathineType;
                lbl17.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl17);

                //试验标准
                System.Windows.Forms.Label lbl18 = new System.Windows.Forms.Label();
                lbl18.Width = 175;
                lbl18.Name = "lbl18";
                lbl18.Text = "试验标准:" + modMethod.testStandard;
                lbl18.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl18);

                fre.flowLayoutPanel.Refresh();
                #endregion

                //所选择的试验结果列
                // 0 选择结果项 1 计算结果项平均值
                //StringBuilder[] strSel = new StringBuilder[2]; 

                //StringBuilder strSelcol = strSel[0];
                //StringBuilder strSelcolAver = strSel[1];

                #region 读取选择的试样的试验结果
                string strSqlSample = string.Empty;
                string strSqlSampleAvg = string.Empty;
                foreach (string str in _selTestSampleArray)
                {
                    strSqlSample += str + "','";
                    strSqlSampleAvg += str + ",";
                }
                strSqlSample = strSqlSample.Remove(strSqlSample.Length - 2, 2);
                strSqlSampleAvg = strSqlSampleAvg.Remove(strSqlSampleAvg.Length - 1, 1); 

                string testNo = modShear.testNo;
                string strTest = testNo.Substring(0, testNo.Length - 1);
                string strL = strTest + "L";
                string strH = strTest + "H";
                string strR = strTest + "R";
                string testNoArr = "('" + strL + "','" + strR + "','" + strH + "')";

                //TestStandard.GBT28289_2012.Shear.readFinishSample(this.dataGridView, this.dgvAvg, testNo, dateTimePicker1, dateTimePicker2, this.zedGraphControl);
                //生成试验结果列表
                m_dt = TestStandard.GBT28289_2012.Shear.CreateFinishViewReport(strSqlSample, this.dateTimePicker1, this.dateTimePicker2);
                //生成试验结果平均值表
                TestStandard.GBT28289_2012.Shear.CreateAverageViewReport(testMethod, strSqlSampleAvg, this.dgvAvg);
                dtaver = (DataTable)this.dgvAvg.DataSource;
                #endregion
            }
        }

        private void proPrintT(frmReportEdit_T fre)
        {
            fre.FormBorderStyle = FormBorderStyle.None;
            fre.TopLevel = false;
            fre.Parent = this;
            fre.BringToFront();
            fre.Size = this.Size;
            fre._TestType = "GBT228-2010";
            //曲线图
            this.zedGraphControl.Dock = DockStyle.None;

            //读取各种报告配置文件
            XmlDocument xd = new XmlDocument();
            int picWidth = 700;
            int picHeight = 430;
            xd.Load(AppDomain.CurrentDomain.BaseDirectory + "GBT228-2010.xml");
            if (xd != null)
            {
                //读取
                XmlNode node = xd.SelectSingleNode("/Report/ZedGraphControl");
                if (node != null)
                {
                    picWidth = int.Parse(node.Attributes["width"].Value);
                    picHeight = int.Parse(node.Attributes["height"].Value);
                }
            }
            /*
            //以图片形式打印
            //this.zedGraphControl.Width = picWidth;
            //this.zedGraphControl.Height = picHeight;
            //this.zedGraphControl.GraphPane.XAxis.Scale.FontSpec.Size = 14.0f;
            //this.zedGraphControl.GraphPane.XAxis.Title.FontSpec.Size = 14.0f;
            //this.zedGraphControl.GraphPane.YAxis.Scale.FontSpec.Size = 14.0f;
            //this.zedGraphControl.GraphPane.YAxis.Title.FontSpec.Size = 14.0f;
            //this.zedGraphControl.GraphPane.Legend.FontSpec.Size = 14.0f;

            //img = this.zedGraphControl.GetImage();
            //Image newImg;
            ////缩放图片生成新图
            ////新建一个bmp图片
            //System.Drawing.Image newImage = new System.Drawing.Bitmap((int)fre.pictureBox.Width, (int)fre.pictureBox.Height);
            ////新建一个画板
            //System.Drawing.Graphics newG = System.Drawing.Graphics.FromImage(newImage);
            ////设置质量
            //newG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //newG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            ////置背景色
            //newG.Clear(Color.White);
            ////画图
            //newG.DrawImage(img, new System.Drawing.Rectangle(0, 0, newImage.Width, newImage.Height), new System.Drawing.Rectangle(0, 0, fre.pictureBox.Width, fre.pictureBox.Height), System.Drawing.GraphicsUnit.Pixel);
            //fre.pictureBox.Image = newImage;
            //this.zedGraphControl.GraphPane.XAxis.Scale.FontSpec.Size = 16.0f;
            //this.zedGraphControl.GraphPane.XAxis.Title.FontSpec.Size = 16.0f;
            //this.zedGraphControl.GraphPane.YAxis.Scale.FontSpec.Size = 16.0f;
            //this.zedGraphControl.GraphPane.YAxis.Title.FontSpec.Size = 16.0f;
            //this.zedGraphControl.GraphPane.Legend.FontSpec.Size = 16.0f;
            //this.zedGraphControl.Dock = DockStyle.Fill;
             */

            //以控件方式打印

            fre._SelTestSampleArray = this._selTestSampleArray;
            fre._SelColorArray = this._selColorArray;
            //fre._TestType = dataGridView.Tag.ToString();
            int ab = 0;
            //基本参数,读取试验方法参数 
            if (_selTestSampleArray != null)
            //查找第一个选择的试样用的何种试验方法
            {
                string testSampleNo = _selTestSampleArray[0];
                BLL.TestSample bllTs = new HR_Test.BLL.TestSample();
                Model.TestSample modTs = bllTs.GetModel(testSampleNo);
                //查找第一根式样是否是矩形
                if (modTs.b0 != 0)
                    ab = 1;
                if (modTs.d0 != 0)
                    ab = 2;
                if (modTs.Do != 0)
                    ab = 3;

                //读取试验方法
                string testMethod = modTs.testMethodName;
                //从试验方法库中查找该方法的基本试验参数
                BLL.ControlMethod tsMethod = new HR_Test.BLL.ControlMethod();
                Model.ControlMethod modMethod = tsMethod.GetModel(testMethod);

                if (modMethod == null)
                {
                    MessageBox.Show(this, "试验方法 '" + testMethod + "' 不存在,请检查!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //读取试验方法的选择结果项
                BLL.SelTestResult bllSel = new HR_Test.BLL.SelTestResult();
                Model.SelTestResult mSel = bllSel.GetModel(testMethod);

                #region 读取试验方法的基本参数
                //报告界面中加入基本参数
                //label width 175*4 = 700;
                //送检单位
                //if (!string.IsNullOrEmpty(modMethod.sendCompany) && modMethod.sendCompany != "-")
                //{
                System.Windows.Forms.Label lbl1 = new System.Windows.Forms.Label();
                lbl1.Width = 175;
                lbl1.Name = "lbl1";
                lbl1.Text = "送检单位:" + modMethod.sendCompany;
                lbl1.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl1);
                //}

                //材料规格
                //if (!string.IsNullOrEmpty(modMethod.stuffSpec) && modMethod.stuffSpec != "-")
                //{
                System.Windows.Forms.Label lbl2 = new System.Windows.Forms.Label();
                lbl2.Width = 175;
                lbl2.Name = "lbl2";
                lbl2.Text = "材料规格:" + modMethod.stuffSpec;
                lbl2.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl2);
                //}

                //材料牌号
                //if (!string.IsNullOrEmpty(modMethod.stuffCardNo) && modMethod.stuffCardNo != "-")
                //{
                System.Windows.Forms.Label lbl3 = new System.Windows.Forms.Label();
                lbl3.Width = 175;
                lbl3.Name = "lbl3";
                lbl3.Text = "材料牌号:" + modMethod.stuffCardNo;
                lbl3.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl3);
                //}
                //材料类型
                //if (!string.IsNullOrEmpty(modMethod.stuffType) && modMethod.stuffType != "-")
                //{
                System.Windows.Forms.Label lbl4 = new System.Windows.Forms.Label();
                lbl4.Width = 175;
                lbl4.Name = "lbl4";
                lbl4.Text = "材料类型:" + modMethod.stuffType;
                lbl4.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl4);
                //}
                //试样标识
                //if (!string.IsNullOrEmpty(modMethod.sampleCharacter) && modMethod.sampleCharacter != "-")
                //{
                System.Windows.Forms.Label lbl5 = new System.Windows.Forms.Label();
                lbl5.Width = 175;
                lbl5.Name = "lbl5";
                lbl5.Text = "试样标识:" + modMethod.sampleCharacter;
                lbl5.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl5);
                //}

                //试验温度
                //if (modMethod.temperature != 0)
                //{
                System.Windows.Forms.Label lbl6 = new System.Windows.Forms.Label();
                lbl6.Width = 175;
                lbl6.Name = "lbl6";
                if (modMethod.temperature != 0)
                    lbl6.Text = "试验温度:" + modMethod.temperature + "℃";
                else
                    lbl6.Text = "试验温度:" + "-";
                lbl6.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl6);
                //}

                //试验湿度
                //if (modMethod.humidity != 0)
                //{
                System.Windows.Forms.Label lbl7 = new System.Windows.Forms.Label();
                lbl7.Width = 175;
                lbl7.Name = "lbl7";
                lbl7.Text = "试验湿度:" + modMethod.humidity + "%";
                lbl7.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl7);
                //}

                //热处理状态
                //if (!string.IsNullOrEmpty(modMethod.hotStatus) && modMethod.hotStatus != "-")
                //{
                System.Windows.Forms.Label lbl8 = new System.Windows.Forms.Label();
                lbl8.Width = 175;
                lbl8.Name = "lbl8";
                lbl8.Text = "热处理状态:" + modMethod.hotStatus;
                lbl8.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl8);
                //}

                //试样取样
                //if (!string.IsNullOrEmpty(modMethod.getSample) && modMethod.getSample != "-")
                //{
                System.Windows.Forms.Label lbl9 = new System.Windows.Forms.Label();
                lbl9.Width = 175;
                lbl9.Name = "lbl9";
                lbl9.Text = "试样取样:" + modMethod.getSample;
                lbl9.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl9);
                //}

                //原始标距
                System.Windows.Forms.Label lbl10 = new System.Windows.Forms.Label();
                lbl10.Width = 175;
                lbl10.Name = "lbl10";
                lbl10.Text = "原始标距:" + modMethod.L0 + " mm";
                lbl10.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl10);

                //平行长度
                System.Windows.Forms.Label lbl11 = new System.Windows.Forms.Label();
                lbl11.Width = 175;
                lbl11.Name = "lbl11";
                lbl11.Text = "平行长度:" + modMethod.Lc + " mm";
                lbl11.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl11);

                //试样总长
                //if (modMethod.Lt > 0)
                //{
                System.Windows.Forms.Label lbl12 = new System.Windows.Forms.Label();
                lbl12.Width = 175;
                lbl12.Name = "lbl12";
                if (modMethod.Lt != 0)
                    lbl12.Text = "试样总长:" + modMethod.Lt + " mm";
                else
                    lbl12.Text = "试样总长:-";
                lbl12.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl12);
                //}

                //引伸计标距
                //if (modMethod.Le > 0)
                //{
                System.Windows.Forms.Label lbl13 = new System.Windows.Forms.Label();
                lbl13.Width = 175;
                lbl13.Name = "lbl13";
                if (modMethod.Le != 0)
                    lbl13.Text = "引伸计标距:" + modMethod.Le + " mm";
                else
                    lbl13.Text = "引伸计标距:-";

                lbl13.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl13);
                //}

                //Awn原始标距
                //if (modMethod.L01 > 0)
                //{
                System.Windows.Forms.Label lbl14 = new System.Windows.Forms.Label();
                lbl14.Width = 175;
                lbl14.Name = "lbl14";
                if (modMethod.L01 != 0)
                    lbl14.Text = "Awn原始标距:" + modMethod.L01 + " mm";
                else
                    lbl14.Text = "Awn原始标距:-";
                lbl14.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl14);
                //}

                //试验条件
                //if (!string.IsNullOrEmpty(modMethod.condition) && modMethod.condition != "-")
                //{
                System.Windows.Forms.Label lbl15 = new System.Windows.Forms.Label();
                lbl15.Width = 175;
                lbl15.Name = "lbl15";
                lbl15.Text = "试验条件:" + modMethod.condition;
                lbl15.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl15);
                //}
                //控制方式
                //if (!string.IsNullOrEmpty(modMethod.controlmode) && modMethod.controlmode != "-")
                //{
                System.Windows.Forms.Label lbl16 = new System.Windows.Forms.Label();
                lbl16.Width = 175;
                lbl16.Name = "lbl16";
                lbl16.Text = "控制方式:" + modMethod.controlmode;
                lbl16.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl16);
                //}
                //试验设备
                //if (!string.IsNullOrEmpty(modMethod.mathineType) && modMethod.mathineType != "-")
                //{
                System.Windows.Forms.Label lbl17 = new System.Windows.Forms.Label();
                lbl17.Width = 175;
                lbl17.Name = "lbl17";
                lbl17.Text = "试验设备:" + modMethod.mathineType;
                lbl17.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl17);
                //}
                //试验设备
                //if (!string.IsNullOrEmpty(modMethod.testStandard) && modMethod.testStandard != "-")
                //{
                System.Windows.Forms.Label lbl18 = new System.Windows.Forms.Label();
                lbl18.Width = 175;
                lbl18.Name = "lbl18";
                lbl18.Text = "试验标准:" + modMethod.testStandard;
                lbl18.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl18);
                //}

                fre.flowLayoutPanel.Refresh();
                #endregion

                //所选择的试验结果列
                StringBuilder strSelcol = new StringBuilder();

                //平均值
                StringBuilder strSelcolAver = new StringBuilder();
                string strSqlSample = string.Empty;
                foreach (string str in _selTestSampleArray)
                {
                    strSqlSample += str + "','";
                }
                strSqlSample = strSqlSample.Remove(strSqlSample.Length - 2, 2);

                BLL.TestSample bllts = new HR_Test.BLL.TestSample();
                DataSet dsmax = bllts.GetMaxFm(" testSampleNo in ('" + strSqlSample + ") and isFinish=true ");
                double maxValue = 0;
                if (dsmax != null)
                {
                    maxValue = Convert.ToDouble( dsmax.Tables[0].Rows[0]["Fm"].ToString());
                }

                int dotvalue = utils.Dotvalue(maxValue); 
                if (maxValue < 1000.0)
                {
                    switch (dotvalue)
                    {
                        case 2:
                            strSelcol.Append(" Format([Fm],'0.00') as [Fm(N)],");
                            strSelcolAver.Append(" Format(Avg([Fm]),'0.00') as [Fm(N)],");
                            break;
                        case 3:
                            strSelcol.Append(" Format([Fm],'0.000') as [Fm(N)],");
                            strSelcolAver.Append(" Format(Avg([Fm]),'0.000') as [Fm(N)],");
                            break;
                        case 4:
                            strSelcol.Append(" Format([Fm],'0.0000') as [Fm(N)],");
                            strSelcolAver.Append(" Format(Avg([Fm]),'0.0000') as [Fm(N)],");
                            break;
                        case 5:
                            strSelcol.Append(" Format([Fm],'0.00000') as [Fm(N)],");
                            strSelcolAver.Append(" Format(Avg([Fm]),'0.00000') as [Fm(N)],");
                            break;
                        default:
                            strSelcol.Append(" Format([Fm],'0.00') as [Fm(N)],");
                            strSelcolAver.Append(" Format(Avg([Fm]),'0.00') as [Fm(N)],");
                            break;
                    }
                  
                }
                if (maxValue >= 1000.0)
                {
                    switch (dotvalue)
                    {
                        case 2:
                            strSelcol.Append(" Format([Fm]/1000.0,'0.00') as [Fm(kN)],");
                            strSelcolAver.Append(" Format(Avg([Fm])/1000.0,'0.00') as [Fm(kN)],");
                            break;
                        case 3:
                            strSelcol.Append(" Format([Fm]/1000.0,'0.000') as [Fm(kN)],");
                            strSelcolAver.Append(" Format(Avg([Fm])/1000.0,'0.000') as [Fm(kN)],");
                            break;
                        case 4:
                            strSelcol.Append(" Format([Fm]/1000.0,'0.0000') as [Fm(kN)],");
                            strSelcolAver.Append(" Format(Avg([Fm])/1000.0,'0.0000') as [Fm(kN)],");
                            break;
                        case 5:
                            strSelcol.Append(" Format([Fm]/1000.0,'0.00000') as [Fm(kN)],");
                            strSelcolAver.Append(" Format(Avg([Fm])/1000.0,'0.00000') as [Fm(kN)],");
                            break;
                        default:
                            strSelcol.Append(" Format([Fm]/1000.0,'0.00') as [Fm(kN)],");
                            strSelcolAver.Append(" Format(Avg([Fm])/1000.0,'0.00') as [Fm(kN)],");
                            break;
                    }
                } 
                ////生成"平均值"的 查询语句
                //if (maxValue < 10000.0)
                //{
                //    strSelcol.Append(" FORMAT([Fm],'0.00') as [Fm(N)],");
                //    strSelcolAver.Append(" FORMAT(Avg([Fm]),'0.00') as [Fm(N)],");
                //}
                //if (maxValue > 10000.0 && maxValue < 100000.0)
                //{
                //    strSelcol.Append(" FORMAT([Fm]/1000.0,'0.0000') as [Fm(kN)],");
                //    strSelcolAver.Append(" FORMAT(Avg([Fm]/1000.0),'0.0000') as [Fm(kN)],");
                //}
                //if (maxValue > 100000.0)
                //{
                //    strSelcol.Append(" FORMAT([Fm]/1000.0,'0.000') as [Fm(kN)],");
                //    strSelcolAver.Append(" FORMAT(Avg([Fm]/1000.0),'0.000') as [Fm(kN)],");
                //}
                strSelcol.Append(" round([Rm],2) as [Rm(MPa)],");
                strSelcolAver.Append(" round(Avg([Rm]),3) as [Rm(MPa)],");

                if (mSel != null)
                {
                    if (mSel.ReH == true)
                    {
                        strSelcol.Append(" [ReH] as [ReH(MPa)],"); strSelcolAver.Append(" round(Avg([ReH]),2) as [ReH(MPa)],"); //strSelcolSD.Append("0.001,");strSelcolSD.Append(" round((SUM([ReH])-MAX([ReH])-MIN([ReH]))/(COUNT(*)-2),2) as [ReH],");
                    }

                    if (mSel.ReL == true) { strSelcol.Append(" [ReL] as [ReL(MPa)],"); strSelcolAver.Append(" round(Avg([ReL]),2) as [ReL(MPa)],"); }//strSelcolSD.Append("0.001,");strSelcolSD.Append(" round((SUM([ReL])-MAX([ReL])-MIN([ReL]))/(COUNT(*)-2),2) as [ReL],");
                    if (mSel.Rp == true) { strSelcol.Append(" [Rp] as [Rp(MPa)],"); strSelcolAver.Append(" round(Avg([Rp]),2) as [Rp(MPa)],"); }// strSelcolSD.Append("0.001,");strSelcolSD.Append(" round((SUM([Rp])-MAX([Rp])-MIN([Rp]))/(COUNT(*)-2),2) as [Rp],"); }
                    if (mSel.Rt == true) { strSelcol.Append(" [Rt] as [Rt(MPa)],"); strSelcolAver.Append(" round(Avg([Rt]),2) as [Rt(MPa)],"); }// strSelcolSD.Append("0.001,");strSelcolSD.Append(" round((SUM([Rt])-MAX([Rt])-MIN([Rt]))/(COUNT(*)-2),2) as [Rt],"); }
                    if (mSel.Rr == true) { strSelcol.Append(" [Rr] as [Rr(MPa)],"); strSelcolAver.Append(" round(Avg([Rr]),2) as [Rr(MPa)],"); }//strSelcolSD.Append("0.001,"); strSelcolSD.Append(" round((SUM([Rr])-MAX([Rr])-MIN([Rr]))/(COUNT(*)-2),2) as [Rr],"); }
                    if (mSel.E == true) { strSelcol.Append(" [E] as [E],"); strSelcolAver.Append(" round(Avg([E]),2) as [E],"); }//strSelcolSD.Append("0.001,"); strSelcolSD.Append(" round((SUM([E])-MAX([E])-MIN([E]))/(COUNT(*)-2),2) as [E],"); }
                    if (mSel.mE == true) { strSelcol.Append(" [mE] as [mE],"); strSelcolAver.Append(" round(Avg([mE]),2) as [mE],"); }//strSelcolSD.Append("0.001,");strSelcolSD.Append(" round((SUM([mE])-MAX([mE])-MIN([mE]))/(COUNT(*)-2),2) as [mE],"); }
                    if (mSel.A == true) { strSelcol.Append(" [A] as [A(%)],"); strSelcolAver.Append(" round(Avg([A]),2) as [A(%)],"); }//strSelcolSD.Append("0.001,"); strSelcolSD.Append(" round((SUM([A])-MAX([A])-MIN([A]))/(COUNT(*)-2),2) as [A],"); }
                    if (mSel.Aee == true) { strSelcol.Append(" [Aee] as [Ae],"); strSelcolAver.Append(" round(Avg([Aee]),2) as [Ae],"); }// strSelcolSD.Append("0.001,"); strSelcolSD.Append(" round((SUM([Aee])-MAX([Aee])-MIN([Aee]))/(COUNT(*)-2),2) as [Ae],"); }
                    if (mSel.Agg == true) { strSelcol.Append(" [Agg] as [Ag],"); strSelcolAver.Append(" round(Avg([Agg]),2) as [Ag],"); }//strSelcolSD.Append("0.001,");  strSelcolSD.Append(" round((SUM([Agg])-MAX([Agg])-MIN([Agg]))/(COUNT(*)-2),2) as [Ag],"); }
                    if (mSel.Att == true) { strSelcol.Append(" [Att] as [At],"); strSelcolAver.Append(" round(Avg([Att]),2) as [At],"); }//  strSelcolSD.Append("0.001,"); strSelcolSD.Append(" round((SUM([Att])-MAX([Att])-MIN([Att]))/(COUNT(*)-2),2) as [At],"); }
                    if (mSel.Aggtt == true) { strSelcol.Append(" [Aggtt] as [Agt],"); strSelcolAver.Append(" round(Avg([Aggtt]),2) as [Agt],"); }// strSelcolSD.Append("0.001,");strSelcolSD.Append(" round((SUM([Aggtt])-MAX([Aggtt])-MIN([Aggtt]))/(COUNT(*)-2),2) as [Agt],"); }
                    if (mSel.Awnwn == true) { strSelcol.Append(" [Awnwn] as [Awn],"); strSelcolAver.Append(" round(Avg([Awnwn]),2) as [Awn],"); }// strSelcolSD.Append("0.001,"); strSelcolSD.Append(" round((SUM([Awnwn])-MAX([Awnwn])-MIN([Awnwn]))/(COUNT(*)-2),2) as [Awn],"); }
                    if (mSel.Lm == true) { strSelcol.Append(" [Lm] as [Lm(mm)],"); strSelcolAver.Append(" round(Avg([Lm]),2) as [Lm(mm)],"); }//strSelcolSD.Append("0.001,"); strSelcolSD.Append(" round((SUM([Lm])-MAX([Lm])-MIN([Lm]))/(COUNT(*)-2),2) as [△Lm],"); }
                    if (mSel.deltaLm == true) { strSelcol.Append(" [deltaLm] as [△Lm(mm)],"); strSelcolAver.Append(" round(Avg([deltaLm]),2) as [△Lm(mm)],"); }//strSelcolSD.Append("0.001,"); strSelcolSD.Append(" round((SUM([Lm])-MAX([Lm])-MIN([Lm]))/(COUNT(*)-2),2) as [△Lm],"); }
                    if (mSel.Lf == true) { strSelcol.Append(" [Lf] as [△Lf],"); strSelcolAver.Append(" round(Avg([Lf]),2) as [△Lf],"); }// strSelcolSD.Append("0.001,"); strSelcolSD.Append(" round((SUM([Lf])-MAX([Lf])-MIN([Lf]))/(COUNT(*)-2),2) as [△Lf],"); }
                    if (mSel.Z == true) { strSelcol.Append(" [Z] as [Z(%)],"); strSelcolAver.Append(" round(Avg([Z]),2) as [Z(%)],"); }// strSelcolSD.Append("0.001,"); strSelcolSD.Append(" round((SUM([Z])-MAX([Z])-MIN([Z]))/(COUNT(*)-2),2) as [Z],"); }
                    //if (mSel.SS == true) { strSelcol.Append(" [SS] as [S],"); strSelcolAver.Append(" round(Avg([SS]),2) as [S],"); strSelcolSD.Append("0.005,"); }//strSelcolSD.Append(" round((SUM([SS])-MAX([SS])-MIN([SS]))/(COUNT(*)-2),2) as [S],"); }

                    m_isSelSD = mSel.SS;
                    m_isSelMid = mSel.Avera1;
                    m_isSelCV = mSel.CV;

                }
                if (strSelcolAver != null)
                    strSelcolAver = strSelcolAver.Remove(strSelcolAver.Length - 1, 1);

                #region 读取选择的试样的试验结果
            
                //生成试验结果列表
                m_dt = TestStandard.GBT228_2010.CreateViewReport(strSelcol.ToString(), strSqlSample, ab); 

                //生成试验结果平均值表
                dtaver = TestStandard.GBT228_2010.CreateAverageViewReport(strSelcolAver, strSelcol, strSqlSample, ab);
                #endregion
            }
        }

        private void proPrintB(frmReportEdit_T fre)
        {
            fre.FormBorderStyle = FormBorderStyle.None;
            fre.TopLevel = false;
            fre.Parent = this;
            fre.BringToFront();
            fre.Size = this.Size;
            fre._TestType = "YBT5349-2006";
            //曲线图
            this.zedGraphControl.Dock = DockStyle.None;
            //读取各种报告配置文件
            XmlDocument xd = new XmlDocument();
            int picWidth = 700;
            int picHeight = 430;
            xd.Load(AppDomain.CurrentDomain.BaseDirectory + "YBT5349-2006.xml");
            if (xd != null)
            {
                //读取
                XmlNode node = xd.SelectSingleNode("/Report/ZedGraphControl");
                if (node != null)
                {
                    picWidth = int.Parse(node.Attributes["width"].Value);
                    picHeight = int.Parse(node.Attributes["height"].Value);
                }
            }

            //以控件方式打印

            fre._SelTestSampleArray = this._selTestSampleArray;
            fre._SelColorArray = this._selColorArray;
            int ab = 0;
            //基本参数,读取试验方法参数 
            if (_selTestSampleArray != null)
            //查找第一个选择的试样用的何种试验方法
            {
                string testSampleNo = _selTestSampleArray[0];
                BLL.Bend bllBend = new HR_Test.BLL.Bend();
                Model.Bend modBend = bllBend.GetModel(testSampleNo);
            
                //读取试验方法
                string testMethod = modBend.testMethod;
                //从试验方法库中查找该方法的基本试验参数
                BLL.ControlMethod_B tsMethod = new HR_Test.BLL.ControlMethod_B();
                Model.ControlMethod_B modMethod = tsMethod.GetModel(testMethod);

                if (modMethod == null)
                {
                    MessageBox.Show(this, "试验方法 '" + testMethod + "' 不存在,请检查!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //读取试验方法的选择结果项
                BLL.SelTestResult_B bllSel = new HR_Test.BLL.SelTestResult_B();
                Model.SelTestResult_B mSel = bllSel.GetModel(testMethod); 

                #region 读取试验方法的基本参数
                //报告界面中加入基本参数
                //label width 175*4 = 700;
                //送检单位
                //if (!string.IsNullOrEmpty(modMethod.sendCompany) && modMethod.sendCompany != "-")
                //{
                System.Windows.Forms.Label lbl1 = new System.Windows.Forms.Label();
                lbl1.Width = 175;
                lbl1.Name = "lbl1";
                lbl1.Text = "送检单位:" + modMethod.sendCompany;
                lbl1.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl1);
                //}

                //材料规格
                //if (!string.IsNullOrEmpty(modMethod.stuffSpec) && modMethod.stuffSpec != "-")
                //{
                System.Windows.Forms.Label lbl2 = new System.Windows.Forms.Label();
                lbl2.Width = 175;
                lbl2.Name = "lbl2";
                lbl2.Text = "材料规格:" + modMethod.stuffSpec;
                lbl2.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl2);
                //}

                //材料牌号
                //if (!string.IsNullOrEmpty(modMethod.stuffCardNo) && modMethod.stuffCardNo != "-")
                //{
                System.Windows.Forms.Label lbl3 = new System.Windows.Forms.Label();
                lbl3.Width = 175;
                lbl3.Name = "lbl3";
                lbl3.Text = "材料牌号:" + modMethod.stuffCardNo;
                lbl3.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl3);
                //}
                //材料类型
                //if (!string.IsNullOrEmpty(modMethod.stuffType) && modMethod.stuffType != "-")
                //{
                System.Windows.Forms.Label lbl4 = new System.Windows.Forms.Label();
                lbl4.Width = 175;
                lbl4.Name = "lbl4";
                lbl4.Text = "材料类型:" + modMethod.stuffType;
                lbl4.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl4);
                //}
                //试样标识
                //if (!string.IsNullOrEmpty(modMethod.sampleCharacter) && modMethod.sampleCharacter != "-")
                //{
                System.Windows.Forms.Label lbl5 = new System.Windows.Forms.Label();
                lbl5.Width = 175;
                lbl5.Name = "lbl5";
                lbl5.Text = "试样标识:" + modMethod.sampleCharacter;
                lbl5.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl5);
                //}

                //试验温度
                //if (modMethod.temperature != 0)
                //{
                System.Windows.Forms.Label lbl6 = new System.Windows.Forms.Label();
                lbl6.Width = 175;
                lbl6.Name = "lbl6";
                if (modMethod.temperature != 0)
                    lbl6.Text = "试验温度:" + modMethod.temperature + "℃";
                else
                    lbl6.Text = "试验温度:" + "-";
                lbl6.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl6);
                //}

                //试验湿度
                //if (modMethod.humidity != 0)
                //{
                System.Windows.Forms.Label lbl7 = new System.Windows.Forms.Label();
                lbl7.Width = 175;
                lbl7.Name = "lbl7";
                lbl7.Text = "试验湿度:" + modMethod.humidity + "%";
                lbl7.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl7);
                //}

                //热处理状态
                //if (!string.IsNullOrEmpty(modMethod.hotStatus) && modMethod.hotStatus != "-")
                //{
                System.Windows.Forms.Label lbl8 = new System.Windows.Forms.Label();
                lbl8.Width = 175;
                lbl8.Name = "lbl8";
                lbl8.Text = "热处理状态:" + modMethod.hotStatus;
                lbl8.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl8);
                //}

                //试样取样
                //if (!string.IsNullOrEmpty(modMethod.getSample) && modMethod.getSample != "-")
                //{
                System.Windows.Forms.Label lbl9 = new System.Windows.Forms.Label();
                lbl9.Width = 175;
                lbl9.Name = "lbl9";
                lbl9.Text = "试样取样:" + modMethod.getSample;
                lbl9.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl9);
                //}

                //原始标距
                System.Windows.Forms.Label lbl10 = new System.Windows.Forms.Label();
                lbl10.Width = 175;
                lbl10.Name = "lbl10";
                lbl10.Text = "跨距:" + modMethod.Ls + " mm";
                lbl10.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl10);

                //平行长度
                System.Windows.Forms.Label lbl11 = new System.Windows.Forms.Label();
                lbl11.Width = 175;
                lbl11.Name = "lbl11";
                lbl11.Text = "挠度计跨距:" + modMethod.Le + " mm";
                lbl11.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl11);

                System.Windows.Forms.Label lbl12 = new System.Windows.Forms.Label();
                lbl12.Width = 175;
                lbl12.Name = "lbl12";
                lbl12.Text = "挠度放大倍数:" + modMethod.n;
                lbl12.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl12);

                System.Windows.Forms.Label lbl13 = new System.Windows.Forms.Label();
                lbl13.Width = 175;
                lbl13.Name = "lbl13";
                if (modMethod.l_l != 0)
                    lbl13.Text = "力臂:" + modMethod.l_l + " mm";
                else
                    lbl13.Text = "力臂:-";
                lbl13.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl13);

                System.Windows.Forms.Label lbl14 = new System.Windows.Forms.Label();
                lbl14.Width = 175;
                lbl14.Name = "lbl14";               
                lbl14.Text = "弯曲类型:" + modMethod.testType;               
                lbl14.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl14);

                //试验条件
                System.Windows.Forms.Label lbl15 = new System.Windows.Forms.Label();
                lbl15.Width = 175;
                lbl15.Name = "lbl15";
                lbl15.Text = "试验条件:" + modMethod.condition;
                lbl15.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl15);

                //控制方式
                System.Windows.Forms.Label lbl16 = new System.Windows.Forms.Label();
                lbl16.Width = 175;
                lbl16.Name = "lbl16";
                lbl16.Text = "控制方式:" + modMethod.controlmode;
                lbl16.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl16);

                //试验设备
                System.Windows.Forms.Label lbl17 = new System.Windows.Forms.Label();
                lbl17.Width = 175;
                lbl17.Name = "lbl17";
                lbl17.Text = "试验设备:" + modMethod.mathineType;
                lbl17.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl17);

                //试验标准
                System.Windows.Forms.Label lbl18 = new System.Windows.Forms.Label();
                lbl18.Width = 175;
                lbl18.Name = "lbl18";
                lbl18.Text = "试验标准:" + modMethod.testStandard;
                lbl18.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl18);

                fre.flowLayoutPanel.Refresh();
                #endregion

              
                //生成"平均值"的 查询语句
                m_isSelSD = mSel.SD;
                m_isSelMid = mSel.Mid;
                m_isSelCV = mSel.CV;

                //if (strSel[0] != null)
                //    strSel[0] = strSel[0].Remove(strSel[0].Length - 1, 1);

                #region 读取选择的试样的试验结果
                string strSqlSample = string.Empty;
                foreach (string str in _selTestSampleArray)
                {
                    strSqlSample += str + "','";
                }
                strSqlSample = strSqlSample.Remove(strSqlSample.Length - 2, 2);

                DataSet dsmax = bllBend.GetFbbMax(" testSampleNo in ('" + strSqlSample + ") and isFinish=true ");
                 double maxvalue =0;
                if(!string.IsNullOrEmpty( dsmax.Tables[0].Rows[0]["Fbb"].ToString()))
                    maxvalue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["Fbb"].ToString());
                  //所选择的试验结果列
                // 0 选择结果项 1 计算结果项平均值

                StringBuilder[] strSel = new StringBuilder[2];
                strSel = TestStandard.YBT5349_2006.strSql_B(mSel.methodName,maxvalue);
                StringBuilder strSelcol = strSel[0];
                StringBuilder strSelcolAver = strSel[1];

                //生成试验结果列表
                m_dt = TestStandard.YBT5349_2006.CreateViewReport(strSelcol.ToString(), strSqlSample);
                //生成试验结果平均值表
                dtaver =  TestStandard.YBT5349_2006.CreateAverageViewReport(strSelcol,strSelcolAver, strSqlSample);
                #endregion
            }
        }      

        private void proPrintC(frmReportEdit_T fre)
        {
            fre.FormBorderStyle = FormBorderStyle.None;
            fre.TopLevel = false;
            fre.Parent = this;
            fre.BringToFront();
            fre._TestType = "GBT7314-2005";
            fre.Size = this.Size;
            //曲线图
            this.zedGraphControl.Dock = DockStyle.None;

            //读取各种报告配置文件
            XmlDocument xd = new XmlDocument();
            int picWidth = 700;
            int picHeight = 430;
            xd.Load(AppDomain.CurrentDomain.BaseDirectory + "GBT7314-2005.xml");
            if (xd != null)
            {
                //读取
                XmlNode node = xd.SelectSingleNode("/Report/ZedGraphControl");
                if (node != null)
                {
                    picWidth = int.Parse(node.Attributes["width"].Value);
                    picHeight = int.Parse(node.Attributes["height"].Value);
                }
            }
            /*
            //以图片形式打印
            //this.zedGraphControl.Width = picWidth;
            //this.zedGraphControl.Height = picHeight;
            //this.zedGraphControl.GraphPane.XAxis.Scale.FontSpec.Size = 14.0f;
            //this.zedGraphControl.GraphPane.XAxis.Title.FontSpec.Size = 14.0f;
            //this.zedGraphControl.GraphPane.YAxis.Scale.FontSpec.Size = 14.0f;
            //this.zedGraphControl.GraphPane.YAxis.Title.FontSpec.Size = 14.0f;
            //this.zedGraphControl.GraphPane.Legend.FontSpec.Size = 14.0f;

            //img = this.zedGraphControl.GetImage();
            //Image newImg;
            ////缩放图片生成新图
            ////新建一个bmp图片
            //System.Drawing.Image newImage = new System.Drawing.Bitmap((int)fre.pictureBox.Width, (int)fre.pictureBox.Height);
            ////新建一个画板
            //System.Drawing.Graphics newG = System.Drawing.Graphics.FromImage(newImage);
            ////设置质量
            //newG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //newG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            ////置背景色
            //newG.Clear(Color.White);
            ////画图
            //newG.DrawImage(img, new System.Drawing.Rectangle(0, 0, newImage.Width, newImage.Height), new System.Drawing.Rectangle(0, 0, fre.pictureBox.Width, fre.pictureBox.Height), System.Drawing.GraphicsUnit.Pixel);
            //fre.pictureBox.Image = newImage;
            //this.zedGraphControl.GraphPane.XAxis.Scale.FontSpec.Size = 16.0f;
            //this.zedGraphControl.GraphPane.XAxis.Title.FontSpec.Size = 16.0f;
            //this.zedGraphControl.GraphPane.YAxis.Scale.FontSpec.Size = 16.0f;
            //this.zedGraphControl.GraphPane.YAxis.Title.FontSpec.Size = 16.0f;
            //this.zedGraphControl.GraphPane.Legend.FontSpec.Size = 16.0f;
            //this.zedGraphControl.Dock = DockStyle.Fill;
             */

            //以控件方式打印

            fre._SelTestSampleArray = this._selTestSampleArray;
            fre._SelColorArray = this._selColorArray;
            int ab = 0;
            //基本参数,读取试验方法参数 
            if (_selTestSampleArray != null)
            //查找第一个选择的试样用的何种试验方法
            {
                string testSampleNo = _selTestSampleArray[0];
                BLL.Compress bllTs = new HR_Test.BLL.Compress();
                Model.Compress modTs = bllTs.GetModel(testSampleNo);
                //查找第一根式样是否是矩形
                if (modTs.a != 0)
                    ab = 1;
                if (modTs.d != 0)
                    ab = 2; 

                //读取试验方法
                string testMethod = modTs.testMethodName;
                //从试验方法库中查找该方法的基本试验参数
                BLL.ControlMethod_C tsMethod = new HR_Test.BLL.ControlMethod_C();
                Model.ControlMethod_C modMethod = tsMethod.GetModel(testMethod);

                if (modMethod == null)
                {
                    MessageBox.Show(this, "试验方法 '" + testMethod + "' 不存在,请检查!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //读取试验方法的选择结果项
                BLL.SelTestResult_C bllSel = new HR_Test.BLL.SelTestResult_C();
                Model.SelTestResult_C mSel = bllSel.GetModel(testMethod);

                #region 读取试验方法的基本参数
                //报告界面中加入基本参数
                //label width 175*4 = 700;
                //送检单位
                //if (!string.IsNullOrEmpty(modMethod.sendCompany) && modMethod.sendCompany != "-")
                //{
                System.Windows.Forms.Label lbl1 = new System.Windows.Forms.Label();
                lbl1.Width = 175;
                lbl1.Name = "lbl1";
                lbl1.Text = "送检单位:" + modMethod.sendCompany;
                lbl1.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl1);
                //}

                //材料规格
                //if (!string.IsNullOrEmpty(modMethod.stuffSpec) && modMethod.stuffSpec != "-")
                //{
                System.Windows.Forms.Label lbl2 = new System.Windows.Forms.Label();
                lbl2.Width = 175;
                lbl2.Name = "lbl2";
                lbl2.Text = "材料规格:" + modMethod.stuffSpec;
                lbl2.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl2);
                //}

                //材料牌号
                //if (!string.IsNullOrEmpty(modMethod.stuffCardNo) && modMethod.stuffCardNo != "-")
                //{
                System.Windows.Forms.Label lbl3 = new System.Windows.Forms.Label();
                lbl3.Width = 175;
                lbl3.Name = "lbl3";
                lbl3.Text = "材料牌号:" + modMethod.stuffCardNo;
                lbl3.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl3);
                //}
                //材料类型
                //if (!string.IsNullOrEmpty(modMethod.stuffType) && modMethod.stuffType != "-")
                //{
                System.Windows.Forms.Label lbl4 = new System.Windows.Forms.Label();
                lbl4.Width = 175;
                lbl4.Name = "lbl4";
                lbl4.Text = "材料类型:" + modMethod.stuffType;
                lbl4.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl4);
                //}
                //试样标识
                //if (!string.IsNullOrEmpty(modMethod.sampleCharacter) && modMethod.sampleCharacter != "-")
                //{
                System.Windows.Forms.Label lbl5 = new System.Windows.Forms.Label();
                lbl5.Width = 175;
                lbl5.Name = "lbl5";
                lbl5.Text = "试样标识:" + modMethod.sampleCharacter;
                lbl5.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl5);
                //}

                //试验温度
                //if (modMethod.temperature != 0)
                //{
                System.Windows.Forms.Label lbl6 = new System.Windows.Forms.Label();
                lbl6.Width = 175;
                lbl6.Name = "lbl6";
                if (modMethod.temperature != 0)
                    lbl6.Text = "试验温度:" + modMethod.temperature + "℃";
                else
                    lbl6.Text = "试验温度:" + "-";
                lbl6.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl6);
                //}

                //试验湿度
                //if (modMethod.humidity != 0)
                //{
                System.Windows.Forms.Label lbl7 = new System.Windows.Forms.Label();
                lbl7.Width = 175;
                lbl7.Name = "lbl7";
                lbl7.Text = "试验湿度:" + modMethod.humidity + "%";
                lbl7.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl7);
                //}

                //热处理状态
                //if (!string.IsNullOrEmpty(modMethod.hotStatus) && modMethod.hotStatus != "-")
                //{
                System.Windows.Forms.Label lbl8 = new System.Windows.Forms.Label();
                lbl8.Width = 175;
                lbl8.Name = "lbl8";
                lbl8.Text = "热处理状态:" + modMethod.hotStatus;
                lbl8.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl8);
                //}

                //试样取样
                //if (!string.IsNullOrEmpty(modMethod.getSample) && modMethod.getSample != "-")
                //{
                System.Windows.Forms.Label lbl9 = new System.Windows.Forms.Label();
                lbl9.Width = 175;
                lbl9.Name = "lbl9";
                lbl9.Text = "试样取样:" + modMethod.getSample;
                lbl9.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl9);
                //}

                //原始标距
                System.Windows.Forms.Label lbl10 = new System.Windows.Forms.Label();
                lbl10.Width = 175;
                lbl10.Name = "lbl10";
                lbl10.Text = "原始标距:" + modMethod.L0 + " mm";
                lbl10.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl10);

                //平行长度
                System.Windows.Forms.Label lbl11 = new System.Windows.Forms.Label();
                lbl11.Width = 175;
                lbl11.Name = "lbl11";
                lbl11.Text = "试样长度:" + modMethod.L + " mm";
                lbl11.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl11); 

                ////引伸计标距
                ////if (modMethod.Le > 0)
                ////{
                //System.Windows.Forms.Label lbl13 = new System.Windows.Forms.Label();
                //lbl13.Width = 175;
                //lbl13.Name = "lbl13";
                //if (modMethod. != 0)
                //    lbl13.Text = "引伸计标距:" + modMethod.Le + " mm";
                //else
                //    lbl13.Text = "引伸计标距:-";

                //lbl13.Font = new Font("宋体", 10);
                //fre.flowLayoutPanel.Controls.Add(lbl13);
                //}

                ////Awn原始标距
                ////if (modMethod.L01 > 0)
                ////{
                //System.Windows.Forms.Label lbl14 = new System.Windows.Forms.Label();
                //lbl14.Width = 175;
                //lbl14.Name = "lbl14";
                //if (modMethod.L01 != 0)
                //    lbl14.Text = "Awn原始标距:" + modMethod.L01 + " mm";
                //else
                //    lbl14.Text = "Awn原始标距:-";
                //lbl14.Font = new Font("宋体", 10);
                //fre.flowLayoutPanel.Controls.Add(lbl14);
                //}

                //试验条件
                //if (!string.IsNullOrEmpty(modMethod.condition) && modMethod.condition != "-")
                //{
                System.Windows.Forms.Label lbl15 = new System.Windows.Forms.Label();
                lbl15.Width = 175;
                lbl15.Name = "lbl15";
                lbl15.Text = "试验条件:" + modMethod.condition;
                lbl15.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl15);
                //}
                //控制方式
                //if (!string.IsNullOrEmpty(modMethod.controlmode) && modMethod.controlmode != "-")
                //{
                System.Windows.Forms.Label lbl16 = new System.Windows.Forms.Label();
                lbl16.Width = 175;
                lbl16.Name = "lbl16";
                lbl16.Text = "控制方式:" + modMethod.controlmode;
                lbl16.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl16);
                //}
                //试验设备
                //if (!string.IsNullOrEmpty(modMethod.mathineType) && modMethod.mathineType != "-")
                //{
                System.Windows.Forms.Label lbl17 = new System.Windows.Forms.Label();
                lbl17.Width = 175;
                lbl17.Name = "lbl17";
                lbl17.Text = "试验设备:" + modMethod.mathineType;
                lbl17.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl17);
                //}
                //试验设备
                //if (!string.IsNullOrEmpty(modMethod.testStandard) && modMethod.testStandard != "-")
                //{
                System.Windows.Forms.Label lbl18 = new System.Windows.Forms.Label();
                lbl18.Width = 175;
                lbl18.Name = "lbl18";
                lbl18.Text = "试验标准:" + modMethod.testStandard;
                lbl18.Font = new Font("宋体", 10);
                fre.flowLayoutPanel.Controls.Add(lbl18);
                //}

                fre.flowLayoutPanel.Refresh();
                #endregion

                //所选择的试验结果列
                StringBuilder strSelcol = new StringBuilder();
                //平均值
                StringBuilder strSelcolAver = new StringBuilder();


                string strSqlSample = string.Empty;
                foreach (string str in _selTestSampleArray)
                {
                    strSqlSample += str + "','";
                }
                strSqlSample = strSqlSample.Remove(strSqlSample.Length - 2, 2);

                BLL.Compress bllts = new HR_Test.BLL.Compress();
                DataSet dsMax = bllts.GetMaxFmc(" testSampleNo in ('" + strSqlSample + ") and isFinish=true ");
                double maxValue = 0;
                if (dsMax != null)
                {
                    maxValue = Convert.ToDouble(dsMax.Tables[0].Rows[0]["Fmc"].ToString());
                }

                StringBuilder[] strSql = TestStandard.GBT7314_2005.strSql_T(modMethod.methodName,maxValue);
                strSelcol = strSql[0];
                strSelcolAver = strSql[1];
                m_isSelSD = mSel.SD;
                m_isSelMid = mSel.Mid;
                m_isSelCV = mSel.CV; 

                #region 读取选择的试样的试验结果             
               
                //生成试验结果列表
                m_dt = TestStandard.GBT7314_2005.CreateViewReport(strSelcol.ToString(), strSqlSample, ab);
                //生成试验结果平均值表
                dtaver = TestStandard.GBT7314_2005.CreateAverageViewReport(strSelcol,strSelcolAver , strSqlSample, ab);
                #endregion
            }
        }


      

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.dataGridView.Rows[e.RowIndex].Cells[0].Value = !Convert.ToBoolean(this.dataGridView.Rows[e.RowIndex].Cells[0].Value);
                this.dataGridView.Rows[e.RowIndex].Selected = Convert.ToBoolean(this.dataGridView.Rows[e.RowIndex].Cells[0].Value);

                string selTestSampleNo = string.Empty;
                string selColor = string.Empty;
                _selTestSampleArray = null;

                int selCount = 0;
                selCount = SelCount();

                for (int i = 0; i < this.dataGridView.Rows.Count; i++)
                {
                    //this.dataGridView.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    if (Convert.ToBoolean(this.dataGridView.Rows[i].Cells[0].Value))
                    {
                        selTestSampleNo += this.dataGridView.Rows[i].Cells[2].Value.ToString() + ",";
                        this.dataGridView.Rows[i].Cells[0].Style.BackColor = Color.DeepSkyBlue;
                        if (this.dataGridView.Rows[i].Cells[1].Value == null)
                        {
                            MessageBox.Show("请重新查询!");
                            return;
                        }
                        selColor += this.dataGridView.Rows[i].Cells[1].Value.ToString() + ",";
                    }
                    else
                    {
                        this.dataGridView.Rows[i].Cells[0].Style.BackColor = Color.White;
                    }
                }
 
                switch (dataGridView.Tag.ToString())
                {
                    case "GBT23615-2009TensileZong":
                        _path = "GBT23615-2009TensileZong";
                        break;
                    case "GBT23615-2009TensileHeng":
                        _path = "GBT23615-2009TensileHeng";
                        break;
                    case "tb_TestSample":
                        _path = "GBT228-2010";
                        break;
                    case "tb_Compress":
                        _path = "GBT7314-2005";
                        break;
                    case "tb_Bend":
                        _path = "YBT5349-2006";
                        break;
                    case "GBT28289-2012Tensile":
                        _path = "GBT28289-2012Tensile";
                        break;
                    case "GBT28289-2012Shear":
                        _path = "GBT28289-2012Shear";
                        break;
                    case "GBT28289-2012Twist":
                        _path = "GBT28289-2012Twist";
                        break;
                    default:
                        _path = "";
                        break;
                }

                if (!string.IsNullOrEmpty(_path))
                {
                    if (!string.IsNullOrEmpty(selTestSampleNo))
                    {
                        selTestSampleNo = selTestSampleNo.Remove(selTestSampleNo.LastIndexOf(','));
                        //曲线数组名
                        _selTestSampleArray = selTestSampleNo.Split(',');
                        _selColorArray = selColor.Split(',');
                    }

                    //若选择了该行，则添加一条曲线Scale
                    if (this.dataGridView.Rows[e.RowIndex].Selected)
                    {
                        string colorc = this.dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                        string sampleNo = this.dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                        _RPPList_ReadOne = new RollingPointPairList(100000);
                        LineItem li = myPanel.AddCurve(sampleNo, _RPPList_ReadOne, Color.FromName(colorc), SymbolType.None);//Y1-X1 
                        li.Line.IsAntiAlias = true;
                        readCurveName(this.dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(), _path, sampleNo);
                    }
                    else
                    {
                        CurveItem ci = this.zedGraphControl.GraphPane.CurveList[this.dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString()];
                        this.zedGraphControl.GraphPane.CurveList.Remove(ci);
                    } 
                    this.zedGraphControl.RestoreScale(this.zedGraphControl.GraphPane);
                }

            }
        }

        private int SelCount()
        {
            int selCount = 0;
            for (int i = 0; i < this.dataGridView.Rows.Count; i++)
            {
                //this.dataGridView.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (Convert.ToBoolean(this.dataGridView.Rows[i].Cells[0].Value) == true)
                {
                    //selTestSampleNo += this.dataGridView.Rows[i].Cells[2].Value.ToString() + ",";
                    //colorName += this.dataGridView.Rows[i].Cells[1].Style.BackColor.Name + ",";
                    this.dataGridView.Rows[i].Cells[0].Style.BackColor = Color.Black;
                    selCount++;
                }
                else
                {
                    this.dataGridView.Rows[i].Cells[0].Style.BackColor = Color.White;
                }
            }
            return selCount;
        }

        //初始化曲线控件上的曲线数量及名称
        private void InitCurveCount(ZedGraph.ZedGraphControl zgControl, string[] lineNameArray, string path, string[] colorArray)
        {
            //for (int j = 0; j < zgControl.GraphPane.CurveList.Count; j++)
            //{
            //    zgControl.GraphPane.CurveList.RemoveAt(j);
            //}
            if (lineNameArray != null)
            {
                _RPPList_Read = new RollingPointPairList[lineNameArray.Length];

                myPanel = zgControl.GraphPane;
                zgControl.GraphPane.CurveList.RemoveRange(0, zgControl.GraphPane.CurveList.Count);

                if (_List_Data != null)
                    _List_Data = null;

                foreach (CurveItem ci in zgControl.GraphPane.CurveList)
                {
                    ci.Clear();
                }

                for (int i = 0; i < lineNameArray.Length; i++)
                {
                    LineItem CurveList = myPanel.AddCurve(lineNameArray[i].ToString(), _RPPList_Read[i], Color.FromName(colorArray[i].ToString()), SymbolType.None);//Y1-X1 
                    readCurveName(lineNameArray[i].ToString(), path, lineNameArray[i]);
                }
            }

            //MessageBox.Show(zgControl.GraphPane.CurveList.Count.ToString());
            //初始化曲线名称即 试样编号的名称 
            zgControl.RestoreScale(zgControl.GraphPane);
            //zgControl.Invalidate();
        }

        //读取曲线文件
        private void readCurveName(string curveName, string path, string curvename)
        {
            //若曲线存在
            //string curvePath = @"E:\衡新试验数据\" + "Curve\\" + path + "\\" + curveName + ".lin";
            string curvePath = @"E:\衡新试验数据\" + "Curve\\" + path + "\\" + curveName + ".txt";
            if (File.Exists(curvePath))
            {
                string outputFile = curvePath;
                //读取曲线 
                _List_Data1 = new List<gdata>();
                //建立曲线点 
                //_RPPList_Read[index] = new RollingPointPairList(100000); 

                using (StreamReader srLine = new StreamReader(outputFile))
                {
                    string[] testSampleInfo1 = srLine.ReadLine().Split(',');
                    string[] testSampleInfo2 = srLine.ReadLine().Split(',');
                    String line;
                    if (srLine.ReadLine() != null)
                    { string[] testSampleInfo3 = srLine.ReadLine().Split(','); }
                    while ((line = srLine.ReadLine()) != null)
                    {
                        string[] gdataArray = line.Split(',');
                        gdata _gdata = new gdata();
                        _gdata.F1 = float.Parse(gdataArray[0]);
                        _gdata.F2 = float.Parse(gdataArray[1]);
                        _gdata.F3 = float.Parse(gdataArray[2]);
                        _gdata.D1 = float.Parse(gdataArray[3]);
                        _gdata.D2 = float.Parse(gdataArray[4]);
                        _gdata.D3 = float.Parse(gdataArray[5]);
                        _gdata.BX1 = float.Parse(gdataArray[6]);
                        _gdata.BX2 = float.Parse(gdataArray[7]);
                        _gdata.BX3 = float.Parse(gdataArray[8]);
                        _gdata.YL1 = float.Parse(gdataArray[9]);
                        _gdata.YL2 = float.Parse(gdataArray[10]);
                        _gdata.YL3 = float.Parse(gdataArray[11]);
                        _gdata.YB1 = float.Parse(gdataArray[12]);
                        _gdata.YB2 = float.Parse(gdataArray[13]);
                        _gdata.YB3 = float.Parse(gdataArray[14]);
                        _gdata.Ts = float.Parse(gdataArray[15]);
                        _List_Data1.Add(_gdata);
                    }
                    srLine.Close();
                    srLine.Dispose();
                    //显示曲线
                    showCurve(_List_Data1, this.zedGraphControl, curvename);
                }
            }
        }

        //显示一条曲线
        private void showCurve(List<gdata> listGData, ZedGraph.ZedGraphControl zgControl, string curvename)
        {
            LineItem LineItem0 = zgControl.GraphPane.CurveList[curvename] as LineItem;
            if (LineItem0 == null)
                return;

            //第二步:在CurveItem中访问PointPairList(或者其它的IPointList)，根据自己的需要增加新数据或修改已存在的数据
            IPointListEdit LineItemListEdit_0 = LineItem0.Points as IPointListEdit;
            if (LineItemListEdit_0 == null)
                return;

            int step = listGData.Count / 3000;
            if (step == 0) step = 1;

            for (Int32 i = 0; i < listGData.Count-2; i += step)
            {
                //采集数据
                //时间
                double time = listGData[i].Ts;
                //力
                double F1value = listGData[i].F1;
                //应力
                double R1value = listGData[i].YL1;
                //位移
                double D1value = listGData[i].D1;
                //变形
                double BX1value = listGData[i].BX1;
                //应变
                double YB1value = listGData[i].YB1;

                //显示曲线数据
                #region  cmbYr,cmbXr 轴
                switch (this.cmbYr.SelectedIndex)
                {
                    case 1:
                        switch (this.cmbXr.SelectedIndex)
                        {
                            case 1:
                                //strCurveName[0] = "力/时间";
                                LineItemListEdit_0.Add(time, F1value);
                                break;
                            case 2:
                                //strCurveName[0] = "力/位移";
                                LineItemListEdit_0.Add(D1value, F1value);
                                break;
                            case 3:
                                //strCurveName[0] = "力/应变";
                                LineItemListEdit_0.Add(YB1value, F1value);
                                break;
                            case 4:
                                LineItemListEdit_0.Add(BX1value, F1value);
                                break;
                            case 5:
                                LineItemListEdit_0.Add(R1value, F1value);
                                break;
                            default:
                                //strCurveName[0] = "";                           
                                break;
                        }
                        break;
                    case 2:
                        switch (this.cmbXr.SelectedIndex)
                        {
                            case 1:
                                LineItemListEdit_0.Add(time, R1value);
                                break;
                            case 2:
                                LineItemListEdit_0.Add(D1value, R1value);
                                break;
                            case 3:
                                //strCurveName[0] = "应力/应变";
                                LineItemListEdit_0.Add(YB1value, R1value);
                                break;
                            case 4:
                                LineItemListEdit_0.Add(BX1value, R1value);
                                break;
                            case 5:
                                //LineItemListEdit_0.Add(R1value, F1value);
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        break;
                    case 3:
                        switch (this.cmbXr.SelectedIndex)
                        {
                            case 1:
                                //strCurveName[0] = "变形/时间";
                                LineItemListEdit_0.Add(time, BX1value);
                                break;
                            case 2:
                                //strCurveName[0] = "变形/位移";
                                LineItemListEdit_0.Add(D1value, BX1value);
                                break;
                            case 3:
                                //strCurveName[0] = "变形/应变";
                                LineItemListEdit_0.Add(YB1value, BX1value);
                                break;
                            case 4:
                                LineItemListEdit_0.Add(BX1value, BX1value);
                                break;
                            case 5:
                                LineItemListEdit_0.Add(R1value, BX1value);
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        break;
                    case 4:
                        switch (this.cmbXr.SelectedIndex)
                        {
                            case 1:
                                //strCurveName[0] = "位移/时间";
                                LineItemListEdit_0.Add(time, D1value);
                                break;
                            case 2:
                                //strCurveName[0] = "位移/位移";
                                LineItemListEdit_0.Add(D1value, D1value);
                                break;
                            case 3:
                                //strCurveName[0] = "位移/应变";
                                LineItemListEdit_0.Add(YB1value, D1value);
                                break;
                            case 4:
                                LineItemListEdit_0.Add(BX1value, D1value);
                                break;
                            case 5:
                                LineItemListEdit_0.Add(R1value, D1value);
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        break;
                    default:
                        //strCurveName[0] = "";
                        //strCurveName[1] = "";
                        break;
                }
                #endregion

            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            readFinishSample(this.dataGridView, this.dataGridView.Tag.ToString());
        }    

        private void cmbTestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbTestType.SelectedItem!=null)
            {
                DataRowView dr = (DataRowView)cmbTestType.SelectedItem;
                string testResultTable = dr["resultTableName"].ToString();
                switch (testResultTable)
                {
                    case "tb_GBT236152009_TensileZong":
                        this.dataGridView.Tag = "GBT23615-2009TensileZong";
                        break;
                    case "tb_GBT236152009_TensileHeng":
                        this.dataGridView.Tag = "GBT23615-2009TensileHeng";
                        break;
                    case "tb_GBT282892012_Tensile": 
                        this.dataGridView.Tag = "GBT28289-2012Tensile";
                        break;
                    case "tb_GBT282892012_Shear":  
                        this.dataGridView.Tag = "GBT28289-2012Shear";
                        break;
                    case "tb_GBT282892012_Twist":
                        this.dataGridView.Tag = "GBT28289-2012Twist";
                        break;
                    case "tb_TestSample":
                        this.dataGridView.Tag = "tb_TestSample";
                        break;
                    case "tb_Compress": 
                        this.dataGridView.Tag = "tb_Compress";
                        break;
                    case "tb_Bend":
                        this.dataGridView.Tag = "tb_Bend";
                        break;
                }

                readFinishSample(this.dataGridView, this.dataGridView.Tag.ToString());
            }

            //btnFind_Click(this.btnFind, e);
        }

        private void readFinishSample(DataGridView dg, string gridviewTag)
        {
            dg.Columns.Clear();
            _selTestSampleArray = null;
            //dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            switch (gridviewTag)
            {
                case "GBT23615-2009TensileHeng":
                    TestStandard.GBT23615_2009.TensileHeng.readFinishSampleReport(this.dataGridView, this.txtTestNo.Text, this.txtTestSampleNo.Text, this.dateTimePicker1, this.dateTimePicker2, this.zedGraphControl);
                    break;
                case "GBT23615-2009TensileZong":
                    TestStandard.GBT23615_2009.TensileZong.readFinishSampleReport(this.dataGridView, this.txtTestNo.Text, this.txtTestSampleNo.Text, this.dateTimePicker1, this.dateTimePicker2, this.zedGraphControl);
                    break;
                case "tb_TestSample":
                    TestStandard.GBT228_2010.ReadGBT228(this.dataGridView,this.txtTestNo.Text,this.txtTestSampleNo.Text,dateTimePicker1,dateTimePicker2,zedGraphControl);
                    break;
                case "tb_Bend":
                    ReadYBT5349(this.dataGridView);
                    break;
                case "tb_Compress":
                    TestStandard.GBT7314_2005.ReadGBT7314(this.dataGridView, this.txtTestNo.Text, this.txtTestSampleNo.Text, dateTimePicker1, dateTimePicker2, zedGraphControl);
                    break;
                case "GBT28289-2012Tensile":
                    TestStandard.GBT28289_2012.Tensile.readFinishSampleReport(this.dataGridView, this.txtTestNo.Text, this.txtTestSampleNo.Text, this.dateTimePicker1,this.dateTimePicker2, this.zedGraphControl);
                    break;
                case "GBT28289-2012Shear":
                    TestStandard.GBT28289_2012.Shear.readFinishSampleReport(this.dataGridView, this.txtTestNo.Text,this.txtTestSampleNo.Text, this.dateTimePicker1, this.dateTimePicker2, this.zedGraphControl);
                    break;
                case "GBT28289-2012Twist":
                    TestStandard.GBT28289_2012.Twist.readFinishSampleReport(this.dataGridView, this.txtTestNo.Text, this.txtTestSampleNo.Text, this.dateTimePicker1, this.dateTimePicker2, this.zedGraphControl);
                    break;
            } 
        } 

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.zedGraphControl.RestoreScale(this.zedGraphControl.GraphPane);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.zedGraphControl.ZoomOutAll(this.zedGraphControl.GraphPane);
        }

        private void cmbYr_SelectedIndexChanged(object sender, EventArgs e)
        {
            //initResultCurve(this.zedGraphControl);
            if (_selColorArray != null)
            {
                switch (dataGridView.Tag.ToString())
                {
                    case "tb_TestSample":
                        InitCurveCount(this.zedGraphControl, _selTestSampleArray, "GBT228-2010", _selColorArray);
                        break;
                    case "tb_Compress":
                        InitCurveCount(this.zedGraphControl, _selTestSampleArray, "GBT7314-2005", _selColorArray);
                        break;
                    case "tb_Bend":
                        InitCurveCount(this.zedGraphControl, _selTestSampleArray, "YBT5349-2006", _selColorArray);
                        break;
                    case "GBT28289-2012Tensile":
                        InitCurveCount(this.zedGraphControl, _selTestSampleArray, "GBT28289-2012Tensile", _selColorArray);
                        break;
                    case "GBT28289-2012Twist":
                        InitCurveCount(this.zedGraphControl, _selTestSampleArray, "GBT28289-2012Twist", _selColorArray);
                        break;
                    case "GBT28289-2012Shear":
                        InitCurveCount(this.zedGraphControl, _selTestSampleArray, "GBT28289-2012Shear", _selColorArray);
                        break;
                    case "GBT23615-2009TensileHeng":
                        InitCurveCount(this.zedGraphControl, _selTestSampleArray, "GBT23615-2009TensileHeng", _selColorArray); break;
                    case "GBT23615-2009TensileZong":
                        InitCurveCount(this.zedGraphControl, _selTestSampleArray, "GBT23615-2009TensileZong", _selColorArray); break;
                }
            }
            switch (cmbXr.SelectedIndex)
            {
                case 0:
                    showCurveResultPanel.XAxis.Title.Text = "X1";
                    break;
                case 1:
                    showCurveResultPanel.XAxis.Title.Text = "时间,s";
                    break;
                case 2:
                    showCurveResultPanel.XAxis.Title.Text = "位移,μm";
                    break;
                case 3:
                    showCurveResultPanel.XAxis.Title.Text = "应变,%";
                    break;
                case 4:
                    showCurveResultPanel.XAxis.Title.Text = "变形,μm";
                    break;
                case 5:
                    showCurveResultPanel.XAxis.Title.Text = "应力,MPa";
                    break;
                default:
                    showCurveResultPanel.XAxis.Title.Text = "X1";
                    break;
            }
            switch (cmbYr.SelectedIndex)
            {
                case 0:
                    showCurveResultPanel.YAxis.Title.Text = "Y1";
                    break;
                case 1:
                    showCurveResultPanel.YAxis.Title.Text = "负荷,N";
                    break;
                case 2:
                    showCurveResultPanel.YAxis.Title.Text = "应力,MPa";
                    break;
                case 3:
                    showCurveResultPanel.YAxis.Title.Text = "变形,μm";
                    break;
                case 4:
                    showCurveResultPanel.YAxis.Title.Text = "位移,μm";
                    break;
            }
            this.zedGraphControl.GraphPane.XAxis.Scale.Max = 1;
            this.zedGraphControl.GraphPane.YAxis.Scale.Max = 1;
            this.zedGraphControl.AxisChange();
            this.zedGraphControl.Refresh();
            RWconfig.SetAppSettings("ShowY", this.cmbYr.SelectedIndex.ToString());
            RestoreZScale();
        }

        private void cmbXr_SelectedIndexChanged(object sender, EventArgs e)
        {
            //initResultCurve(this.zedGraphControl);
            if (_selColorArray != null)
            {
                switch (dataGridView.Tag.ToString())
                {
                    case "tb_TestSample":
                        InitCurveCount(this.zedGraphControl, _selTestSampleArray, "GBT228-2010", _selColorArray);
                        break;
                    case "tb_Compress":
                        InitCurveCount(this.zedGraphControl, _selTestSampleArray, "GBT7314-2005", _selColorArray);
                        break;
                    case "tb_Bend":
                        InitCurveCount(this.zedGraphControl, _selTestSampleArray, "YBT5349-2006", _selColorArray);
                        break;
                    case "GBT28289-2012Tensile":
                        InitCurveCount(this.zedGraphControl, _selTestSampleArray, "GBT28289-2012Tensile", _selColorArray); 
                        break;
                    case "GBT28289-2012Twist":
                        InitCurveCount(this.zedGraphControl, _selTestSampleArray, "GBT28289-2012Twist", _selColorArray);
                        break;
                    case "GBT28289-2012Shear":
                        InitCurveCount(this.zedGraphControl, _selTestSampleArray, "GBT28289-2012Shear", _selColorArray);
                        break;
                    case "GBT23615-2009TensileHeng":
                        InitCurveCount(this.zedGraphControl, _selTestSampleArray, "GBT23615-2009TensileHeng", _selColorArray); break;
                    case "GBT23615-2009TensileZong":
                        InitCurveCount(this.zedGraphControl, _selTestSampleArray, "GBT23615-2009TensileZong", _selColorArray); break;

                }
            }

            switch (cmbXr.SelectedIndex)
            {
                case 0:
                    showCurveResultPanel.XAxis.Title.Text = "X1";
                    break;
                case 1:
                    showCurveResultPanel.XAxis.Title.Text = "时间,s";
                    break;
                case 2:
                    showCurveResultPanel.XAxis.Title.Text = "位移,μm";
                    break;
                case 3:
                    showCurveResultPanel.XAxis.Title.Text = "应变,%";

                    break;
                case 4:
                    showCurveResultPanel.XAxis.Title.Text = "变形,μm";
                    break;
                case 5:
                    showCurveResultPanel.XAxis.Title.Text = "应力,MPa";
                    break;
                default:
                    showCurveResultPanel.XAxis.Title.Text = "X1";
                    break;
            }
            switch (cmbYr.SelectedIndex)
            {
                case 0:
                    showCurveResultPanel.YAxis.Title.Text = "Y1";
                    break;
                case 1:
                    showCurveResultPanel.YAxis.Title.Text = "负荷,N";
                    break;
                case 2:
                    showCurveResultPanel.YAxis.Title.Text = "应力,MPa";
                    break;
                case 3:
                    showCurveResultPanel.YAxis.Title.Text = "变形,μm";
                    break;
                case 4:
                    showCurveResultPanel.YAxis.Title.Text = "位移,μm";
                    break;
            }

            this.zedGraphControl.GraphPane.XAxis.Scale.Max = 1;
            this.zedGraphControl.GraphPane.YAxis.Scale.Max = 1;
            this.zedGraphControl.AxisChange();
            this.zedGraphControl.Refresh();
            RestoreZScale();
            RWconfig.SetAppSettings("ShowX", this.cmbXr.SelectedIndex.ToString());
        }

        private void tsbtnMinimize_Click(object sender, EventArgs e)
        {
            _fmMain.WindowState = FormWindowState.Minimized;
        }

         private void cmbXr_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_MouseEnter(object sender, EventArgs e)
        {
            dataGridView.Focus();
        }

        private void zedGraphControl_MouseEnter(object sender, EventArgs e)
        {
            zedGraphControl.Focus();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(this.btnFind, e);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            btnFind_Click(this.btnFind, e);
        }

        private void txtTestNo_TextChanged(object sender, EventArgs e)
        {
            //btnFind_Click(sender, e);
        }

        private void txtTestSampleNo_TextChanged(object sender, EventArgs e)
        {
            //btnFind_Click(sender, e);
        }

        private void cmbTestStandard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTestStandard.SelectedItem != null)
            {
                DataRowView dv = (DataRowView)this.cmbTestStandard.SelectedItem;
                BLL.Standard blls = new HR_Test.BLL.Standard();
                DataSet ds = blls.GetList(" standardNo='" + dv["standardNo"].ToString() + "'");
                this.cmbTestType.DataSource = ds.Tables[0];
                this.cmbTestType.DisplayMember = "testType";
                this.cmbTestType.ValueMember = "ID";
                DataRowView drv = (DataRowView)this.cmbTestType.SelectedItem;
                this.lblStandardTitle.Text = drv["standardName"].ToString();
            }
        }

        private void tsbtnPrintPreview_Click(object sender, EventArgs e)
        {
            if (_selTestSampleArray != null)
            {
                frmReportEdit_T fre = new frmReportEdit_T(this);
                switch (dataGridView.Tag.ToString())
                {
                    case "tb_TestSample":
                        proPrintT(fre);
                        break;
                    case "tb_Compress":
                        proPrintC(fre);
                        break;
                    case "tb_Bend":
                        proPrintB(fre);
                        break;
                    case "GBT28289-2012Tensile":
                        proPrint28289Tensile(fre);
                        break;
                    case "GBT28289-2012Twist":
                        proPrint28289Twist(fre);
                        break;
                    case "GBT28289-2012Shear":
                        proPrint28289Shear(fre);
                        break;
                    case "GBT23615-2009TensileHeng":
                        proPrint23615TensileHeng(fre); break;
                    case "GBT23615-2009TensileZong":
                        proPrint23615TensileZong(fre); break;
                }
                fre.WindowState = FormWindowState.Minimized;               
                fre.Show();
                fre.tsbtnPrint_Click(sender, e);
                fre.Dispose();
            }          
        }
    }
}
