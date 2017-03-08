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
using ZedGraph;
using System.Threading;


namespace HR_Test
{
    public partial class frmReportEdit_T : Form
    {

        PropertyGrid propertyGrid1;
        FormPrinting.FormPrinting fp;
        //dragControl dc=new dragControl();

        PickBox _pb = new PickBox();
        object mCurrentSelectObject;
        private frmFind _fmFind;
        RollingPointPairList _RPPList0;
        int _ShowX;
        int _ShowY;
        //存储采集数据 
        private List<gdata> _List_Data1;
        //读取曲线的数据
        RollingPointPairList[] _RPPList_Read; 
        /// <summary>
        /// 选择的试样编号以供打印
        /// </summary>
        private string[] _selTestSampleArray = null;
        public string[] _SelTestSampleArray
        {
            get { return this._selTestSampleArray; }
            set { this._selTestSampleArray = value; }
        }
        private string[] _selColorArray = null;
        public string[] _SelColorArray
        {
            get { return this._selColorArray; }
            set { this._selColorArray = value; }
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

        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
        //public System.Windows.Forms.PictureBox pictureBox = new PictureBox();
        
        public System.Windows.Forms.DataGrid dgAver = new DataGrid();
        public System.Windows.Forms.DataGrid dg = new DataGrid();

        public frmReportEdit_T(frmFind fmFind)
        {
            InitializeComponent();
            //读取拉伸试验报告配置单 
            z = new ZedGraph.ZedGraphControl();
            _fmFind = fmFind;
            mCurrentSelectObject = this.panel2;            
            propertyGrid1 = new PropertyGrid();
            propertyGrid1.CommandsVisibleIfAvailable = true;
            propertyGrid1.TabIndex = 1;
            propertyGrid1.Text = "属性";
            propertyGrid1.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(propertyGrid1);
            getProperty();
        }

        void z_Invalidated(object sender, InvalidateEventArgs e)
        {
            if (z.GraphPane.XAxis != null)
            {
                Scale sScale = z.GraphPane.XAxis.Scale;
                switch (_ShowX)// -请选择-   时间,s  位移,μm  应变,μm
                {
                    case 1://时间
                        sScale.Mag = 0;
                        break;
                    case 2://位移 
                        if (sScale.Max > 1000)
                        {
                            sScale.Mag = 3;
                            sScale.Format = "0.00";
                        }
                        else
                        {
                            sScale.Mag = 0;
                            sScale.Format = "0.0";
                        }
                        break;
                    case 3:
                        if (sScale.Max > 1000)
                        {
                            sScale.Mag = 3;
                            sScale.Format = "0.00";
                        }
                        else
                        {
                            sScale.Mag = 0;
                            sScale.Format = "0.0";
                        }
                        break;
                }
                if (sScale.Max > 1000)
                {
                    if(((int)sScale.Max % 100) !=0)
                        sScale.Max = ((int)sScale.Max / 100) * 100+200;
                    else
                        sScale.Max = ((int)sScale.Max / 100) * 100;

                    sScale.Min = 0;
                }
                sScale.MajorStep = (sScale.Max - sScale.Min) / 5;
                sScale.MinorStep = sScale.MajorStep / 5;
            }

            if (z.GraphPane.YAxis != null)
            {
                Scale sScale = z.GraphPane.YAxis.Scale;
                switch (_ShowY)
                {
                    case 1:
                        if (sScale.Max > 1000)
                        {
                            sScale.Mag = 3;
                            sScale.Format = "0.00";
                        }
                        else
                        {
                            sScale.Mag = 0;
                            sScale.Format = "0.0";
                        }
                        break;
                    case 2:
                        //if (m_Stress > sScale.Max)
                        //{
                        //    sScale.Max = 2 * sScale.Max;
                        //}
                        break;
                    case 3:
                        //if (m_Elongate > sScale.Max)
                        //{
                        //    sScale.Max = 2 * sScale.Max;
                        if (sScale.Max > 1000)
                        {
                            sScale.Mag = 3;
                            sScale.Format = "0.00";
                        }
                        else
                        {
                            sScale.Mag = 0;
                            sScale.Format = "0.0";
                        }
                        //}
                        break;
                    case 4:
                        //if (m_Displacement > sScale.Max)
                        //{
                        //    sScale.Max = 2 * sScale.Max;
                        if (sScale.Max > 1000)
                        {
                            sScale.Mag = 3;
                            sScale.Format = "0.00";
                        }
                        else
                        {
                            sScale.Mag = 0;
                            sScale.Format = "0.0";
                        }
                        //}
                        break;
                }
                if (sScale.Max > 100)
                {
                    sScale.Max = ((int)sScale.Max / 10) * 10;
                    sScale.Min = 0;
                }
                sScale.MajorStep = (sScale.Max - sScale.Min) / 5;
                sScale.MinorStep = sScale.MajorStep / 5;
            } 
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

            zgControl.IsZoomOnMouseCenter = false;
            zgControl.IsEnableWheelZoom = false;

            _RPPList0 = new RollingPointPairList(100000);
            Legend l = zgControl.GraphPane.Legend;
            l.IsShowLegendSymbols = true;
            l.Gap = 0.5f;
            l.Position = LegendPos.Top;
            l.Border.IsVisible = false;
            l.FontSpec.Size = 14.0f;
            l.IsVisible = true;

            // Set the titles and axis labels
            GraphPane _ResultPanel = zgControl.GraphPane;
            _ResultPanel.Margin.All = 0;
            _ResultPanel.Title.Text = "";
            _ResultPanel.Title.IsVisible = false;
            _ResultPanel.IsFontsScaled = false;
            zgControl.IsZoomOnMouseCenter = false;
            zgControl.AutoValidate = AutoValidate.EnableAllowFocusChange;
            //XAxis
            //最后的显示值隐藏
            _ResultPanel.XAxis.Scale.FontSpec.Size = 14.0f;
            _ResultPanel.XAxis.Title.FontSpec.Size = 14.0f;
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
            _ResultPanel.XAxis.Scale.Max = 100;
            _ResultPanel.XAxis.Scale.MaxAuto = true;
            _ResultPanel.XAxis.MajorGrid.IsVisible = true;
            _ResultPanel.XAxis.MajorTic.IsOpposite = false;
            _ResultPanel.XAxis.MinorTic.IsOpposite = false;


            _ResultPanel.YAxis.Title.Text = "Y";
            _ResultPanel.YAxis.Scale.FontSpec.FontColor = Color.FromArgb(34, 67, 108);
            _ResultPanel.YAxis.Title.FontSpec.FontColor = Color.FromArgb(34, 67, 108);
            _ResultPanel.YAxis.Scale.FontSpec.Size = 14.0f;
            _ResultPanel.YAxis.Title.FontSpec.Size = 14.0f;
            _ResultPanel.YAxis.Scale.FontSpec.Family = "宋体";
            _ResultPanel.YAxis.Title.Gap = -0.5f;
            _ResultPanel.YAxis.Scale.Format = "0.0";
            _ResultPanel.YAxis.Scale.LabelGap = 0;
            // Align the Y2 axis labels so they are flush to the axis 
            _ResultPanel.YAxis.Scale.AlignH = AlignH.Center;
            _ResultPanel.YAxis.Scale.Min = 0;
            _ResultPanel.YAxis.Scale.Max = 100;
            _ResultPanel.YAxis.Scale.MaxAuto = true;
            _ResultPanel.YAxis.Scale.MinAuto = false;
            _ResultPanel.YAxis.MajorGrid.IsVisible = true;
            _ResultPanel.YAxis.MajorTic.IsOpposite = false;
            _ResultPanel.YAxis.MinorTic.IsOpposite = false;


            switch (_ShowY)
            {
                case 0:
                    _ResultPanel.YAxis.Title.Text = "Y1";
                    _ResultPanel.YAxis.Scale.LabelGap = 0;
                    _ResultPanel.YAxis.Scale.MaxAuto = true;
                    break;
                case 1:
                    _ResultPanel.YAxis.Title.Text = "负荷,N";
                    _ResultPanel.YAxis.Scale.LabelGap = 0;
                    _ResultPanel.YAxis.Scale.MaxAuto = true;
                    break;
                case 2:
                    _ResultPanel.YAxis.Title.Text = "应力,MPa";
                    _ResultPanel.YAxis.Scale.LabelGap = 0;
                    _ResultPanel.YAxis.Scale.MaxAuto = true;
                    break;
                case 3:
                    _ResultPanel.YAxis.Title.Text = "变形,μm";
                    _ResultPanel.YAxis.Scale.LabelGap = 0;
                    _ResultPanel.YAxis.Scale.MaxAuto = true;
                    break;
                case 4:
                    _ResultPanel.YAxis.Title.Text = "位移,μm";
                    _ResultPanel.YAxis.Scale.LabelGap = 0;
                    _ResultPanel.YAxis.Scale.MaxAuto = true;
                    break;
            }

            switch (_ShowX)
            {
                case 0:
                    _ResultPanel.XAxis.Title.Text = "X1";
                    _ResultPanel.XAxis.Scale.MaxAuto = true;
                    break;
                case 1:
                    _ResultPanel.XAxis.Title.Text = "时间,s";
                    _ResultPanel.XAxis.Scale.MaxAuto = true;
                    break;
                case 2:
                    _ResultPanel.XAxis.Title.Text = "位移,μm";
                    _ResultPanel.XAxis.Scale.LabelGap = 0;
                    _ResultPanel.XAxis.Scale.MaxAuto = true;
                    break;
                case 3:
                    _ResultPanel.XAxis.Title.Text = "应变,%";
                    _ResultPanel.XAxis.Scale.MaxAuto = true;
                    break;
                case 4:
                    _ResultPanel.XAxis.Title.Text = "变形,μm";
                    _ResultPanel.XAxis.Scale.MaxAuto = true;
                    break;
                default:
                    _ResultPanel.XAxis.Title.Text = "X1";
                    _ResultPanel.XAxis.Scale.MaxAuto = true;
                    break;
            }


            //_List_Data = new List<gdata>(100000);
            ////开始，增加的线是没有数据点的(也就是list为空)
            ////增加无数据的空线条，确定各线条显示的轴。
            //LineItem CurveList0 = myPanel.AddCurve("", _RPPList0, Color.Red, SymbolType.None);//Y1-X1   
            //CurveList0.Line.Width = 1;
            //CurveList0.YAxisIndex = 1;
            zgControl.AxisChange();
        }


        //初始化曲线控件上的曲线数量及名称
        private void InitCurveCount(ZedGraph.ZedGraphControl zgControl, string[] lineNameArray, string path, string[] colorArray)
        {
            if (lineNameArray != null)
            {
                _RPPList_Read = new RollingPointPairList[lineNameArray.Length]; 
                zgControl.GraphPane.CurveList.RemoveRange(0, zgControl.GraphPane.CurveList.Count);
                foreach (CurveItem ci in zgControl.GraphPane.CurveList)
                {
                    ci.Clear();
                }

                for (int i = 0; i < lineNameArray.Length; i++)
                {
                    LineItem CurveList = z.GraphPane.AddCurve(lineNameArray[i].ToString(), _RPPList_Read[i], Color.FromName(colorArray[i].ToString()), SymbolType.None);//Y1-X1 
                    //CurveList.Line.IsSmooth = true;
                   //CurveList.Line.SmoothTension = 1f;                    
                    CurveList.Line.Width = 0.1f; 
                    CurveList.Line.IsAntiAlias = true;
                    readCurveName(lineNameArray[i].ToString(), path, lineNameArray[i]);
                }
            }

            //MessageBox.Show(zgControl.GraphPane.CurveList.Count.ToString());
            //初始化曲线名称即 试样编号的名称 
            zgControl.RestoreScale(zgControl.GraphPane);
            //zgControl.Invalidate();
        }

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
                    if ((line = srLine.ReadLine()) != null)
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
                    showCurve(_List_Data1, this.z, curvename);
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

            int count = listGData.Count;

            int step = count / 3000;
            if (step == 0)
                step = 1;
            for (Int32 i = 0; i < listGData.Count-2; i+=step)
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
                switch (_ShowY)
                {
                    case 1:
                        switch (_ShowX)
                        {
                            case 1:
                                //strCurveName[0] = "力/时间";
                                LineItemListEdit_0.Add(time, F1value);
                                //_RPPList_Read[index].Add(time, F1value);
                                //_RPPList_ReadOne.Add(time, F1value);
                                break;
                            case 2:
                                //strCurveName[0] = "力/位移";
                                LineItemListEdit_0.Add(D1value, F1value);
                                //_RPPList_ReadOne.Add(D1value, F1value);
                                break;
                            case 3:
                                //strCurveName[0] = "力/应变";
                                LineItemListEdit_0.Add(YB1value, F1value);
                                //_RPPList_ReadOne.Add(YB1value, F1value);
                                break;
                            case 4:
                                LineItemListEdit_0.Add(BX1value, F1value);
                                //_RPPList_ReadOne.Add(BX1value, F1value);
                                break;
                            default:
                                //strCurveName[0] = "";                           
                                break;
                        }
                        break;
                    case 2:
                        switch (_ShowX)
                        {
                            case 1:
                                //strCurveName[0] = "应力/时间";
                                LineItemListEdit_0.Add(time, R1value);
                                //_RPPList_ReadOne.Add(time, R1value);
                                break;
                            case 2:
                                //strCurveName[0] = "应力/位移";
                                LineItemListEdit_0.Add(D1value, R1value);
                                //_RPPList_ReadOne.Add(D1value, R1value);
                                break;
                            case 3:
                                //strCurveName[0] = "应力/应变";
                                LineItemListEdit_0.Add(YB1value, R1value);
                                //_RPPList_ReadOne.Add(YB1value, R1value);
                                break;
                            case 4:
                                LineItemListEdit_0.Add(BX1value, R1value);
                                //_RPPList_ReadOne.Add(BX1value, R1value);
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        break;
                    case 3:
                        switch (_ShowX)
                        {
                            case 1:
                                //strCurveName[0] = "变形/时间";
                                LineItemListEdit_0.Add(time, BX1value);
                                //_RPPList_ReadOne.Add(time, BX1value);
                                break;
                            case 2:
                                //strCurveName[0] = "变形/位移";
                                LineItemListEdit_0.Add(D1value, BX1value);
                                //_RPPList_ReadOne.Add(D1value, BX1value);
                                break;
                            case 3:
                                //strCurveName[0] = "变形/应变";
                                LineItemListEdit_0.Add(YB1value, BX1value);
                                //_RPPList_ReadOne.Add(YB1value, BX1value);
                                break;
                            case 4:
                                LineItemListEdit_0.Add(BX1value, BX1value);
                                //_RPPList_ReadOne.Add(BX1value, BX1value);
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        break;
                    case 4:
                        switch (_ShowX)
                        {
                            case 1:
                                //strCurveName[0] = "位移/时间";
                                LineItemListEdit_0.Add(time, D1value);
                                //_RPPList_ReadOne.Add(time, D1value);
                                break;
                            case 2:
                                //strCurveName[0] = "位移/位移";
                                LineItemListEdit_0.Add(D1value, D1value);
                                //_RPPList_ReadOne.Add(D1value, D1value);
                                break;
                            case 3:
                                //strCurveName[0] = "位移/应变";
                                LineItemListEdit_0.Add(YB1value, D1value);
                                //_RPPList_ReadOne.Add(YB1value, D1value);
                                break;
                            case 4:
                                LineItemListEdit_0.Add(BX1value, D1value);
                                //_RPPList_ReadOne.Add(BX1value, D1value);
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


        private void AddGridStyle(DataGrid dg)
        {
            DataGridTableStyle myGridStyle = new DataGridTableStyle();
            myGridStyle.MappingName = "NamesTable"; 
            DataGridTextBoxColumn nameColumnStyle =
                new DataGridTextBoxColumn();
            nameColumnStyle.MappingName = "Name";
            nameColumnStyle.HeaderText = "Name"; 
            myGridStyle.GridColumnStyles.Add(nameColumnStyle); 
            dg.TableStyles.Add(myGridStyle);
        }

        private void ReadRPT_Config(Panel pal,string testType)
        {
            //读取位置 和 手动添加的控件
            XmlDocument xd = new XmlDocument();
            switch (testType)
            {
                case "GBT228-2010": xd.Load(AppDomain.CurrentDomain.BaseDirectory + "GBT228-2010.xml");
                    break;
                case "YBT5349-2006": xd.Load(AppDomain.CurrentDomain.BaseDirectory + "YBT5349-2006.xml");
                    break;
                case "GBT7314-2005": xd.Load(AppDomain.CurrentDomain.BaseDirectory + "GBT7314-2005.xml");
                    break;
                case "GBT28289-2012Shear": xd.Load(AppDomain.CurrentDomain.BaseDirectory + "GBT28289-2012Shear.xml");
                    break;
                case "GBT28289-2012Tensile": xd.Load(AppDomain.CurrentDomain.BaseDirectory + "GBT28289-2012Tensile.xml");
                    break;
                case "GBT28289-2012Twist": xd.Load(AppDomain.CurrentDomain.BaseDirectory + "GBT28289-2012Twist.xml");
                    break;
                case "GBT23615-2009TensileZong": xd.Load(AppDomain.CurrentDomain.BaseDirectory + "GBT23615-2009TensileZong.xml");
                    break; 
                case "GBT23615-2009TensileHeng": xd.Load(AppDomain.CurrentDomain.BaseDirectory + "GBT23615-2009TensileHeng.xml");
                    break;
                case "GBT3354-2014": xd.Load(AppDomain.CurrentDomain.BaseDirectory + "GBT3354-2014.xml");
                    break;
            }
        
            if (xd != null)
            {
                //读取
                XmlNodeList nodes = xd.SelectSingleNode("/Report").ChildNodes;
                foreach (XmlNode xn in nodes)
                {
                    switch(xn.Attributes["type"].Value)
                    {
                        case "DataGrid":
                            if (xn.Attributes["name"].Value == "dataGridAver")
                            {
                                //System.Windows.Forms.DataGridView dg = new DataGridView();
                                dgAver.Name = xn.Attributes["name"].Value;
                                dgAver.Width = int.Parse(xn.Attributes["width"].Value);
                                dgAver.Height = int.Parse(xn.Attributes["height"].Value);
                                dgAver.Left = int.Parse(xn.Attributes["left"].Value);
                                dgAver.Top = int.Parse(xn.Attributes["top"].Value);
                                dgAver.Font = new Font(xn.Attributes["fontname"].Value, float.Parse(xn.Attributes["fontsize"].Value));
                                dgAver.ReadOnly = true;
                                //dgAver.ColumnHeadersVisible = false;
                                dgAver.CaptionVisible = false;
                                dgAver.RowHeadersVisible = false;
                                pal.Controls.Add(dgAver);
                            }

                            if (xn.Attributes["name"].Value == "dataGrid")
                            {
                                //System.Windows.Forms.DataGridView dg = new DataGridView();
                                dg.Name = xn.Attributes["name"].Value;
                                dg.Width = int.Parse(xn.Attributes["width"].Value);
                                dg.Height = int.Parse(xn.Attributes["height"].Value);
                                dg.Left = int.Parse(xn.Attributes["left"].Value);
                                dg.Top = int.Parse(xn.Attributes["top"].Value);
                                dg.Font = new Font(xn.Attributes["fontname"].Value, float.Parse(xn.Attributes["fontsize"].Value));
                                //dg.ColumnHeadersVisible = false;
                                dg.RowHeadersVisible = false;
                                dg.CaptionVisible = false;
                                dg.ReadOnly = true;
                                
                                pal.Controls.Add(dg);
                            }

                            break;
                        case "Label":
                            System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
                            lbl.Name = xn.Attributes["name"].Value;
                            lbl.Width =int.Parse( xn.Attributes["width"].Value);
                            lbl.Height = int.Parse(xn.Attributes["height"].Value);
                            lbl.Left = int.Parse(xn.Attributes["left"].Value);
                            lbl.Top = int.Parse(xn.Attributes["top"].Value);
                            lbl.Text = xn.Attributes["text"].Value;
                            lbl.Font = new Font(xn.Attributes["fontname"].Value,float.Parse(xn.Attributes["fontsize"].Value));
                            lbl.AutoSize = true;
                            if (lbl.Name == "lblDate")
                                lbl.Text = DateTime.Now.ToLongDateString();
                           
                            if(lbl.Name.Contains("CanDel_"))
                                lbl.ContextMenuStrip = this.contextMenuStrip1;

                            pal.Controls.Add(lbl);
                            break;
                        //case "PictureBox":                            
                            //pictureBox.Name = xn.Attributes["name"].Value;
                            //pictureBox.Width = int.Parse(xn.Attributes["width"].Value);
                            //pictureBox.Height = int.Parse(xn.Attributes["height"].Value);
                            //pictureBox.Left = int.Parse(xn.Attributes["left"].Value);
                            //pictureBox.Top = int.Parse(xn.Attributes["top"].Value);
                            //pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                            //pictureBox.BorderStyle = BorderStyle.None; 

                            //if(pictureBox.Name.Contains("CanDel_"))
                            //{
                            //    System.Windows.Forms.PictureBox pb = new PictureBox();
                            //    pb.Name = xn.Attributes["name"].Value;
                            //    pb.Width = int.Parse(xn.Attributes["width"].Value);
                            //    pb.Height = int.Parse(xn.Attributes["height"].Value);
                            //    pb.Left = int.Parse(xn.Attributes["left"].Value);
                            //    pb.Top = int.Parse(xn.Attributes["top"].Value);
                            //    pb.SizeMode = PictureBoxSizeMode.Zoom;
                            //    pb.BorderStyle = BorderStyle.Fixed3D;
                            //    pb.ContextMenuStrip = this.contextMenuStrip1;  
                            //    pal.Controls.Add(pb);                                 
                            //}

                            //pal.Controls.Add(pictureBox); 
                            //break;

                        case "ZedGraphControl":                             
                            //z = _fmFind.zedGraphControl;                          
                            //if (pictureBox.Name.Contains("CanDel_"))
                            //{
                            //    System.Windows.Forms.PictureBox pb = new PictureBox();
                            //    pb.Name = xn.Attributes["name"].Value;
                            //    pb.Width = int.Parse(xn.Attributes["width"].Value);
                            //    pb.Height = int.Parse(xn.Attributes["height"].Value);
                            //    pb.Left = int.Parse(xn.Attributes["left"].Value);
                            //    pb.Top = int.Parse(xn.Attributes["top"].Value);
                            //    pb.SizeMode = PictureBoxSizeMode.Zoom;
                            //    pb.BorderStyle = BorderStyle.Fixed3D;
                            //    pb.ContextMenuStrip = this.contextMenuStrip1;
                            //    pal.Controls.Add(pb);
                            //}
                            pal.Controls.Add(z);  
                            z.Name = xn.Attributes["name"].Value;
                            z.Width = int.Parse(xn.Attributes["width"].Value);
                            z.Height = int.Parse(xn.Attributes["height"].Value);
                            z.Left = int.Parse(xn.Attributes["left"].Value);
                            z.Top = int.Parse(xn.Attributes["top"].Value);
                            z.RestoreScale(this.z.GraphPane);
                            z.Refresh();
                            break;

                        case "FlowLayoutPanel":
                            //flowLayoutPanel = new FlowLayoutPanel();
                            flowLayoutPanel.Name = xn.Attributes["name"].Value;
                            flowLayoutPanel.Width = int.Parse(xn.Attributes["width"].Value);
                            flowLayoutPanel.Height = int.Parse(xn.Attributes["height"].Value);
                            flowLayoutPanel.Left = int.Parse(xn.Attributes["left"].Value);
                            flowLayoutPanel.Top = int.Parse(xn.Attributes["top"].Value);
                            pal.Controls.Add(flowLayoutPanel);
                            break;
                        //case "Panel":
                        //    palpic.Name = xn.Attributes["name"].Value;
                        //    palpic.Width = int.Parse(xn.Attributes["width"].Value);
                        //    palpic.Height = int.Parse(xn.Attributes["height"].Value);
                        //    palpic.Left = int.Parse(xn.Attributes["left"].Value);
                        //    palpic.Top = int.Parse(xn.Attributes["top"].Value);
                        //    pal.Controls.Add(palpic);
                        //    break;
                    } 
                }
            }

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
                    collection.Add(new CustomProperty("文本对齐", "TextAlign", "内容", "文本对齐方式。", mCurrentSelectObject));
                    collection.Add(new CustomProperty("边框", "BorderStyle", "外观","边框。", mCurrentSelectObject)); 
                    collection.Add(new CustomProperty("背景色", "BackColor", "外观", "背景色。", mCurrentSelectObject));
                    collection.Add(new CustomProperty("字体颜色", "ForeColor", "外观", "前景色。", mCurrentSelectObject)); 
                    collection.Add(new CustomProperty("文本内容", "Text", "内容", "显示的文本内容。", mCurrentSelectObject, typeof(System.ComponentModel.Design.MultilineStringEditor)));
                    break;   
                case "PictureBox":
                    collection.Add(new CustomProperty("边框", "BorderStyle", "外观", "边框。", mCurrentSelectObject)); 

                    break;
                case "Panel":
                    collection.Add(new CustomProperty("边框", "BorderStyle", "外观", "边框。", mCurrentSelectObject));
                    collection.Add(new CustomProperty("背景色", "BackColor", "外观", "背景色。", mCurrentSelectObject));
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
            //读取报告控件信息
            ReadRPT_Config(this.panel2, this._TestType);
            dgAver.DataSource = _fmFind.dtaver;
            autoSizeDg(dgAver);
            dg.DataSource = _fmFind.m_dt; 
            //最后一列隐藏 
            autoSizeDg(dg);
            _ShowY =  int.Parse(RWconfig.GetAppSettings("ShowY"));
            _ShowX =  int.Parse(RWconfig.GetAppSettings("ShowX"));
            z.Invalidated += new InvalidateEventHandler(z_Invalidated);
            initResultCurve(this.z);  
            //this.tb_TestSampleTableAdapter.Fill(this._HR_TestDataDataSet.tb_TestSample);
            initProperty();           

            InitCurveCount(this.z, _selTestSampleArray, _testType, _selColorArray);

            fp = new FormPrinting.FormPrinting(this.panel2);
            fp.TopMargin = 0;
            fp.HAlignment = HorizontalAlignment.Center;
            fp.Orientation = FormPrinting.FormPrinting.OrientationENum.Portrait;
            this.panel2.Left = this.panel1.Width / 2 - this.panel2.Width / 2;
        }

        private void autoSizeDg(DataGrid _dbgrid)
        {
            DataTable _dt = (DataTable)_dbgrid.DataSource;
            DataGridTableStyle myGridTableStyle; 
            _dbgrid.TableStyles.Clear(); 
            if (_dbgrid.TableStyles.Count == 0)
            {
                myGridTableStyle = new DataGridTableStyle();
                myGridTableStyle.ColumnHeadersVisible = false;
                myGridTableStyle.RowHeadersVisible = false;
                myGridTableStyle.MappingName = _dt.TableName;
                //myGridTableStyle.RowHeadersVisible = false;
                for (int i = 0; i < _dt.Columns.Count; i++)
                {
                    DataGridTextBoxColumn dgtbox = new DataGridTextBoxColumn();
                    dgtbox.Alignment = HorizontalAlignment.Center;
                    dgtbox.ResetHeaderText();
                    dgtbox.HeaderText = _dt.Columns[i].ColumnName;                   
                    dgtbox.MappingName = _dt.Columns[i].ColumnName;
                    //dgtbox.Width = 100; 
                    myGridTableStyle.GridColumnStyles.Add(dgtbox);
                } 
                _dbgrid.TableStyles.Add(myGridTableStyle);
            }
            //autosize col width
            for (int i = 0; i < _dbgrid.TableStyles[0].GridColumnStyles.Count; i++)
            {
                AutoSizeCol(_dbgrid, i);
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
            //for (int i = 0; i < numRows; ++i)
            //{
            //    size = g.MeasureString(_dbgrid[i, col].ToString(), _dbgrid.Font);
            //    if (size.Width > width)
            //        width = size.Width;
            //}
            _dbgrid.TableStyles[0].GridColumnStyles[col].Width = (int)width + 5; 
            g.Dispose();            
        } 

        private void initProperty()
        {
            for (int i = 0; i < this.panel2.Controls.Count; i++)
            {
                this.panel2.Controls[i].MouseDown += new System.Windows.Forms.MouseEventHandler(MouseDown);
                //this.panel2.Controls[i].MouseLeave += new System.EventHandler(dc.MyMouseLeave);
                //this.panel2.Controls[i].MouseMove += new System.Windows.Forms.MouseEventHandler(dc.MyMouseMove);
            }
        } 

        private void MouseDown(object sender, MouseEventArgs e)
        {
            //dc.MyMouseDown(sender,e);      
            mCurrentSelectObject = sender;
            Control c = (Control)sender;
            if (_pb == null)
                _pb = new PickBox();
            _pb.WireControl(c);
            c.BringToFront();
            //c.BackColor = Color.SkyBlue;
            getProperty();
        }

        private void DelProperty()
        { 
            for (int i = 0; i < this.panel2.Controls.Count; i++)
            {
                this.panel2.Controls[i].MouseDown -= new System.Windows.Forms.MouseEventHandler(MouseDown);
                //this.panel2.Controls[i].MouseLeave -= new System.EventHandler(dc.MyMouseLeave);
                //this.panel2.Controls[i].MouseMove -= new System.Windows.Forms.MouseEventHandler(dc.MyMouseMove);
            }
        }
         

        public void tsbtnPrint_Click(object sender, EventArgs e)
        {   
            fp.PrintPreview = true; 
            fp.Print(); 
            this.Dispose();
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
            _fmFind.zedGraphControl.Dock = DockStyle.Fill;
            this.Dispose();
        }

        public void tsbtnPrinterSet_Click(object sender, EventArgs e)
        {             
            PrintDialog pd = new PrintDialog();
            pd.AllowSelection = true;
            pd.AllowPrintToFile = true;
            pd.AllowSomePages = true;           
            //pd.Document = fp.pd;
            pd.ShowDialog(this);
        }

        private void tsbtnAddText_Click(object sender, EventArgs e)
        {
           System.Windows.Forms. Label tb = new System.Windows.Forms.Label();
            tb.Text = "文本内容";
            tb.Name = "CanDel_" + this.panel2.Controls.Count;
            tb.ContextMenuStrip = this.contextMenuStrip1;
            tb.BorderStyle = BorderStyle.None;
            tb.Font = new Font("宋体", 12f);
            tb.BringToFront();
            tb.AutoSize = true;
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
            string xmlPath = string.Empty;
            switch (this._TestType)
            {

                case "GBT228-2010": xmlPath = AppDomain.CurrentDomain.BaseDirectory + "GBT228-2010.xml";
                    break;
                case "GBT7314-2005": xmlPath = AppDomain.CurrentDomain.BaseDirectory + "GBT7314-2005.xml";
                    break;
                case "YBT5349-2006": xmlPath = AppDomain.CurrentDomain.BaseDirectory + "YBT5349-2006.xml"; 
                    break;
                case "GBT28289-2012Shear": xmlPath = AppDomain.CurrentDomain.BaseDirectory + "GBT28289-2012Shear.xml";
                    break;
                case "GBT28289-2012Tensile": xmlPath = AppDomain.CurrentDomain.BaseDirectory + "GBT28289-2012Tensile.xml";
                    break;
                case "GBT28289-2012Twist": xmlPath = AppDomain.CurrentDomain.BaseDirectory + "GBT28289-2012Twist.xml";
                    break;
                case "GBT23615-2009TensileZong": xmlPath = AppDomain.CurrentDomain.BaseDirectory + "GBT23615-2009TensileZong.xml";
                    break;
                case "GBT23615-2009TensileHeng": xmlPath =AppDomain.CurrentDomain.BaseDirectory + "GBT23615-2009TensileHeng.xml";
                    break;
            }

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("   ");
            using (XmlWriter writer = XmlWriter.Create(xmlPath, settings))
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
                    
                    if (c.GetType().Name == "FlowLayoutPanel")
                    {
                        foreach (Control cc in c.Controls)
                        {
                            writer.WriteStartElement("control");
                            writer.WriteAttributeString("type", cc.GetType().Name);
                            writer.WriteAttributeString("name", cc.Name);
                            writer.WriteAttributeString("width", cc.Width.ToString());  
                            writer.WriteAttributeString("text", cc.Text);
                            writer.WriteAttributeString("fontname", cc.Font.Name.ToString());
                            writer.WriteAttributeString("fontsize", cc.Font.Size.ToString());
                            writer.WriteAttributeString("visible", cc.Visible.ToString());
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tsbtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void delControl_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            ToolStrip ts = tsmi.Owner;
            System.Windows.Forms.Label lbl = (System.Windows.Forms. Label)((ts as ContextMenuStrip).SourceControl);
            lbl.Dispose(); 
            this.panel3.Refresh();
        }
 

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Panel p = new Panel();
            p.Width = 700;
            p.Height = 300;
            p.BorderStyle = BorderStyle.None;
            p.BackColor = Color.Transparent;
            this.panel2.Controls.Add(p);
            DelProperty();
            initProperty();
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            Panel p = new Panel();
            p.Width = 700;
            p.Height = 300;
            p.BorderStyle = BorderStyle.None;
            p.BackColor = Color.Transparent;
            this.panel2.Controls.Add(p);
            DelProperty();
            initProperty();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //dgAver.DataSource = _fmFind.dtaver;

            DataTable tCust = new DataTable("Customers");  
            DataColumn cCustID = new DataColumn("CustID", typeof(int));
            DataColumn cCustName = new DataColumn("CustName");
            DataColumn cCurrent = new DataColumn("Current", typeof(bool));
            tCust.Columns.Add(cCustID);
            tCust.Columns.Add(cCustName);
            tCust.Columns.Add(cCurrent);
            dgAver.DataSource = tCust;
            dgAver.Refresh();
            dg.DataSource = _fmFind.m_dt; 
            dg.Refresh(); 
        }

        private void toolStripButton2_Click_2(object sender, EventArgs e)
        {
            PictureBox pb = new PictureBox();
            pb.Name = "CanDel_" + this.panel2.Controls.Count;
            pb.Width = 300;
            pb.Height = 300;
            pb.BorderStyle = BorderStyle.Fixed3D;
            pb.BackColor = Color.WhiteSmoke;
            this.panel2.Controls.Add(pb);
            DelProperty();
            initProperty();
        }

        private void toolStripButton2_Click_3(object sender, EventArgs e)
        {
            this.dg.TableStyles.Clear(); 
        }

        private void frmReportEdit_T_FormClosing(object sender, FormClosingEventArgs e)
        {
            _fmFind.zedGraphControl.Dock = DockStyle.Fill;
            this.Dispose();
        }

        private void frmReportEdit_T_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void panel2_MouseEnter(object sender, EventArgs e)
        {            
            z.BringToFront();
            z.Refresh();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            _pb.Remove();
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panel1.Focus();
        }

        public void tsbtnPrintP_Click(object sender, EventArgs e)
        {
            try
            {

                fp.PrintPreview = false;
                fp.Print();
                this.Dispose();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return;
            }
        }
      

 
    }
}
