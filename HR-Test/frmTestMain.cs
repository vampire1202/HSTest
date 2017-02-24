using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using ZedGraph;

namespace HR_Test
{
    public partial class frmTestMain : Form
    {
        // Make up some data points based on the Sine function
        //private Timer timer = new Timer();
        //private MicroTimer mtimer = new MicroTimer();
        private Multimedia.Timer mtimer = new Multimedia.Timer(); 
       
        //开始时间
        private double tickStart = 0; 
        //显示曲线
        private RollingPointPairList _RPPList0;
        private RollingPointPairList _RPPList1;
        private RollingPointPairList _RPPList2;
        private RollingPointPairList _RPPList3;
        private RollingPointPairList _RPPList4;
        private RollingPointPairList _RPPList5;
        private GraphPane myPanel;

        //计算结果用到的 力-时间
        private RollingPointPairList _RPPF_T;
        //存储采集数据
        private List<gdata> _List_Data;
        private double _S0 = 0;
        //显示切换
        private bool _showYL = false;
        private bool _showYB = false;
        //曲线名数组
        private string[] strCurveName = new string[6];
        //暂停标识
        private bool cause = false;
        //选择的试样编号
        private string selTestSampleNo = string.Empty;

        private string _testSampleNo;
        public string TestSampleNo
        {
            get { return this._testSampleNo; }
            set { this._testSampleNo = value; }
        } 

        public frmTestMain()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            initChart(this.zedGraphControl);
            //ms间隔
            mtimer.Period = 50;
            mtimer.Mode = Multimedia.TimerMode.Periodic;
            mtimer.Tick += new EventHandler(mtimer_Tick);  

        }

        private void frmTestMain_Load(object sender, EventArgs e)
        {
            readMethod(this.tvTestSample);
            readFinishSample(this.dataGridView);
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Curve"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Curve");
            }
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Curve\\Extend"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Curve\\Extend");
            }
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Curve\\Compress"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Curve\\Compress");
            }
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Curve\\Bend"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Curve\\Bend");
            }
            this.tscbX1.SelectedIndex = int.Parse(RWconfig.GetAppSettings("X1"));
            this.tscbX2.SelectedIndex = int.Parse(RWconfig.GetAppSettings("X2"));
            this.tscbY1.SelectedIndex = int.Parse(RWconfig.GetAppSettings("Y1"));
            this.tscbY2.SelectedIndex = int.Parse(RWconfig.GetAppSettings("Y2"));
            this.tscbY3.SelectedIndex = int.Parse(RWconfig.GetAppSettings("Y3"));
        }

        private void readMethod(TreeView tv)
        {
            tv.Nodes.Clear();
            tv.Nodes.Add("拉伸试验");
            BLL.TestSample bllTs = new HR_Test.BLL.TestSample();
            DataSet ds = bllTs.GetList(" isFinish=false ");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TreeNode tn = new TreeNode();
                tn.Text = ds.Tables[0].Rows[i]["testSampleNo"].ToString();
                tn.Name = "extend" + ds.Tables[0].Rows[i]["ID"].ToString();
                tv.Nodes[0].Nodes.Add(tn);
            }
            ds.Dispose();

            tv.Nodes.Add("压缩试验");
            BLL.Compress bllC = new HR_Test.BLL.Compress();
            DataSet dsc = bllC.GetList(" isFinish=false ");
            for (int j = 0; j < dsc.Tables[0].Rows.Count; j++)
            {
                TreeNode tn = new TreeNode();
                tn.Text = dsc.Tables[0].Rows[j]["testSampleNo"].ToString();
                tn.Name = "compress" + dsc.Tables[0].Rows[j]["ID"].ToString();
                tv.Nodes[1].Nodes.Add(tn);
            }
            dsc.Dispose();

            tv.Nodes.Add("弯曲试验");
            BLL.Bend bllb = new HR_Test.BLL.Bend();
            DataSet dsb = bllb.GetList(" isFinish=false ");
            for (int k = 0; k < dsb.Tables[0].Rows.Count; k++)
            {
                TreeNode tn = new TreeNode();
                tn.Text = dsb.Tables[0].Rows[k]["testSampleNo"].ToString();
                tn.Name = "bend" + dsb.Tables[0].Rows[k]["ID"].ToString();
                tv.Nodes[2].Nodes.Add(tn);
            }
            dsb.Dispose();
            // tv.ExpandAll();
        }

        private void readFinishSample(DataGridView dg)
        {
            BLL.TestSample bllTs = new HR_Test.BLL.TestSample();
            DataSet ds = bllTs.GetFinishList("isFinish=true");
            DataTable dt = ds.Tables[0];
            dg.DataSource = dt;
            dg.Refresh();
            ds.Dispose();
            dg.Name = "extend";
            dg.Refresh();
        }

        private void readFinishSample_C(DataGridView dg)
        {
            BLL.Compress bllTs = new HR_Test.BLL.Compress();
            DataSet ds = bllTs.GetFinishList("isFinish=true");
            DataTable dt = ds.Tables[0];
            dg.DataSource = dt;
            dg.Refresh();
            ds.Dispose();
            dg.Name = "compress";
            dg.Refresh();
        }

        private void readFinishSample_B(DataGridView dg)
        {
            BLL.Bend bllTs = new HR_Test.BLL.Bend();
            DataSet ds = bllTs.GetFinishList("isFinish=true");
            DataTable dt = ds.Tables[0];
            dg.DataSource = dt;
            dg.Refresh();
            ds.Dispose();
            dg.Name = "bend";
            dg.Refresh();
        }

        private void initChart(ZedGraph.ZedGraphControl zgControl)
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

            _RPPList0 = new RollingPointPairList(100000);
            _RPPList1 = new RollingPointPairList(100000);
            _RPPList2 = new RollingPointPairList(100000);
            _RPPList3 = new RollingPointPairList(100000);
            _RPPList4 = new RollingPointPairList(100000);
            _RPPList5 = new RollingPointPairList(100000);
            _RPPF_T = new RollingPointPairList(100000);
            //ZedGraph  
            //zedGraphControl1.IsAntiAlias = true; 
            Legend l = zgControl.GraphPane.Legend;
            l.IsShowLegendSymbols = true;
            l.Gap = 0;
            l.Position = LegendPos.InsideTopLeft;
            l.FontSpec.Size = 8;

            // Set the titles and axis labels
            myPanel = zgControl.GraphPane;
            myPanel.Margin.All = 3;
            myPanel.Title.Text = "";
            myPanel.Title.IsVisible = false;
            zgControl.IsZoomOnMouseCenter = false;
            myPanel.Fill = new Fill(Color.WhiteSmoke, Color.Lavender, 0F);


            //XAxis
            //最后的显示值隐藏
            //myPanel.XAxis.Scale.IsSkipLastLabel = true;
            //myPanel.XAxis.Scale.IsLabelsInside = true; 
            myPanel.XAxis.Scale.FontSpec.Size = 8;
            myPanel.XAxis.Title.FontSpec.Size = 8;
            myPanel.XAxis.Title.FontSpec.FontColor = Color.Navy;
            myPanel.XAxis.Scale.FontSpec.FontColor = Color.Navy;
            myPanel.XAxis.MajorTic.IsOpposite = false;
            myPanel.XAxis.MinorTic.IsOpposite = false;
            myPanel.XAxis.Title.Text = "X1";
            myPanel.XAxis.Scale.AlignH = AlignH.Center;
            myPanel.XAxis.Scale.LabelGap = 0;
            myPanel.XAxis.Scale.Format = "0.0";
            myPanel.XAxis.Scale.Min = 0;
            myPanel.XAxis.Scale.MaxAuto = true;
            myPanel.XAxis.Scale.MajorStepAuto = true;
            myPanel.XAxis.Scale.MinorStepAuto = true;
            myPanel.XAxis.MajorGrid.IsVisible = true;

            myPanel.X2Axis.Title.Text = "X2";
            myPanel.X2Axis.Title.Gap = 0;
            myPanel.X2Axis.IsVisible = true;
            myPanel.X2Axis.Scale.FontSpec.FontColor = Color.Navy;
            myPanel.X2Axis.Title.FontSpec.FontColor = Color.Navy;
            myPanel.X2Axis.Scale.FontSpec.Size = 8;
            myPanel.X2Axis.Title.FontSpec.Size = 8;
            myPanel.X2Axis.MajorTic.IsOpposite = false;
            myPanel.X2Axis.MinorTic.IsOpposite = false;
            myPanel.X2Axis.Scale.AlignH = AlignH.Center;
            myPanel.X2Axis.Scale.Format = "0.0";
            myPanel.X2Axis.Scale.Min = 0;
            myPanel.X2Axis.Scale.LabelGap = 0;
            myPanel.X2Axis.Scale.MaxAuto = true;
            myPanel.X2Axis.Scale.MajorStepAuto = true;
            myPanel.X2Axis.Scale.MinorStepAuto = true;

            myPanel.AddYAxis("应力,MPa");
            myPanel.YAxisList[1].Title.Text = "Y1";
            myPanel.YAxisList[1].IsVisible = true;
            myPanel.YAxisList[1].Scale.FontSpec.FontColor = Color.Navy;
            myPanel.YAxisList[1].Title.FontSpec.FontColor = Color.Navy;
            myPanel.YAxisList[1].Scale.FontSpec.Size = 8;
            myPanel.YAxisList[1].Title.FontSpec.Size = 8;
            myPanel.YAxisList[1].MajorTic.IsOpposite = false;
            myPanel.YAxisList[1].MinorTic.IsOpposite = false;
            myPanel.YAxisList[1].MajorGrid.IsVisible = true;
            myPanel.YAxisList[1].Scale.Format = "0.0";
            myPanel.YAxisList[1].Scale.LabelGap = 0;
            myPanel.YAxisList[1].Scale.Align = AlignP.Inside;
            myPanel.YAxisList[1].Scale.Min = 0;
            myPanel.YAxisList[1].Scale.MaxAuto = true;
            myPanel.YAxisList[1].Scale.MajorStepAuto = true;
            myPanel.YAxisList[1].Scale.MinorStepAuto = true;

            //YAxis
            myPanel.YAxis.Scale.FontSpec.Size = 8;
            myPanel.YAxis.Title.FontSpec.Size = 8;
            myPanel.YAxis.Scale.FontSpec.FontColor = Color.Navy;
            myPanel.YAxis.Title.FontSpec.FontColor = Color.Navy;
            myPanel.YAxis.MajorTic.IsOpposite = false;
            myPanel.YAxis.MinorTic.IsOpposite = false;
            myPanel.YAxis.Title.Text = "Y2";
            myPanel.YAxis.Scale.Format = "0.0";
            myPanel.YAxis.Scale.Min = 0;
            myPanel.YAxis.Scale.LabelGap = 0;
            myPanel.YAxis.Scale.MaxAuto = true;
            myPanel.YAxis.Scale.MajorStepAuto = true;
            myPanel.YAxis.Scale.MinorStepAuto = true;

            myPanel.Y2Axis.Title.Text = "Y3";
            myPanel.Y2Axis.IsVisible = true;
            myPanel.Y2Axis.Scale.FontSpec.FontColor = Color.Navy;
            myPanel.Y2Axis.Title.FontSpec.FontColor = Color.Navy;
            myPanel.Y2Axis.Scale.FontSpec.Size = 8;
            myPanel.Y2Axis.Title.FontSpec.Size =8;
            myPanel.Y2Axis.Scale.Format = "0.0";
            myPanel.Y2Axis.MajorTic.IsOpposite = false;
            myPanel.Y2Axis.MinorTic.IsOpposite = false;
            myPanel.Y2Axis.Scale.LabelGap = 0;
            // Align the Y2 axis labels so they are flush to the axis 
            myPanel.Y2Axis.Scale.AlignH = AlignH.Center;
            myPanel.Y2Axis.Scale.Min = 0;
            myPanel.Y2Axis.Scale.MaxAuto = true;
            myPanel.Y2Axis.Scale.MajorStepAuto = true;
            myPanel.Y2Axis.Scale.MinorStepAuto = true;

            _List_Data = new List<gdata>(100000);
            //开始，增加的线是没有数据点的(也就是list为空)
            //增加无数据的空线条，确定各线条显示的轴。
            LineItem CurveList0 = myPanel.AddCurve("", _RPPList0, Color.Red, SymbolType.None);//Y1-X1   
            CurveList0.Line.Width = 2;
            CurveList0.YAxisIndex = 1;

            LineItem CurveList1 = myPanel.AddCurve("", _RPPList1, Color.Green, SymbolType.None);//Y1-X2
            CurveList1.Line.Width = 2;
            CurveList1.YAxisIndex = 1;
            CurveList1.IsX2Axis = true;

            LineItem CurveList2 = myPanel.AddCurve("", _RPPList2, Color.Blue, SymbolType.None);//Y2-X1 
            CurveList2.Line.Width = 2;

            LineItem CurveList3 = myPanel.AddCurve("", _RPPList3, Color.Purple, SymbolType.None);//Y2-X2 
            CurveList3.Line.Width = 2;
            CurveList3.IsX2Axis = true;

            LineItem CurveList4 = myPanel.AddCurve("", _RPPList4, Color.Goldenrod, SymbolType.None);//Y3-X1
            CurveList4.Line.Width = 2;
            CurveList4.IsY2Axis = true;

            LineItem CurveList5 = myPanel.AddCurve("", _RPPList5, Color.Lime, SymbolType.None); //Y3-X2
            CurveList5.Line.Width = 2;
            CurveList5.IsX2Axis = true;
            CurveList5.IsY2Axis = true;
            zgControl.AxisChange();
        }

        void mtimer_Tick(object sender, EventArgs e)
        {
            //确保CurveList不为空
            if (zedGraphControl.GraphPane.CurveList.Count <= 0)
            {
                MessageBox.Show("无显示曲线!");
                mtimer.Stop();
                return;
            }

            //取Graph曲线，也就是
            //第一步:在GraphPane.CurveList集合中查找CurveItem
            LineItem LineItem0 = zedGraphControl.GraphPane.CurveList[0] as LineItem;
            LineItem LineItem1 = zedGraphControl.GraphPane.CurveList[1] as LineItem;
            LineItem LineItem2 = zedGraphControl.GraphPane.CurveList[2] as LineItem;
            LineItem LineItem3 = zedGraphControl.GraphPane.CurveList[3] as LineItem;
            LineItem LineItem4 = zedGraphControl.GraphPane.CurveList[4] as LineItem;
            LineItem LineItem5 = zedGraphControl.GraphPane.CurveList[5] as LineItem;

            if (LineItem0 == null)
                return;
            if (LineItem1 == null)
                return;
            if (LineItem2 == null)
                return;
            if (LineItem3 == null)
                return;
            if (LineItem4 == null)
                return;
            if (LineItem5 == null)
                return;

            //第二步:在CurveItem中访问PointPairList(或者其它的IPointList)，根据自己的需要增加新数据或修改已存在的数据
            IPointListEdit LineItemListEdit_0 = LineItem0.Points as IPointListEdit;
            if (LineItemListEdit_0 == null)
                return;
            IPointListEdit LineItemListEdit_1 = LineItem1.Points as IPointListEdit;
            if (LineItemListEdit_1 == null)
                return;
            IPointListEdit LineItemListEdit_2 = LineItem2.Points as IPointListEdit;
            if (LineItemListEdit_2 == null)
                return;
            IPointListEdit LineItemListEdit_3 = LineItem3.Points as IPointListEdit;
            if (LineItemListEdit_3 == null)
                return;
            IPointListEdit LineItemListEdit_4 = LineItem4.Points as IPointListEdit;
            if (LineItemListEdit_4 == null)
                return;
            IPointListEdit LineItemListEdit_5 = LineItem5.Points as IPointListEdit;
            if (LineItemListEdit_5 == null)
                return;

            //采集数据
            //时间
            double time = 1;
            //力
            double F1value = (Math.Sqrt(time * 6));
            //应力
            double R1value = F1value * F1value * F1value / 3;
            //位移
            double D1value = Math.Sqrt(time);
            //变形
            double BX1value = Math.Sqrt(time / 100);
            //应变
            double YB1value = D1value * 1.6;

            //显示数据：力值 时间 位移 应力 变形 应变
            this.lblFShow.Text = F1value.ToString("f3");
            this.lblTimeShow.Text = time.ToString("f2");
            this.lblDShow.Text = D1value.ToString("f3");
            this.lblYLShow.Text = R1value.ToString("f3");
            this.lblBXShow.Text = BX1value.ToString("f4");
            this.lblYBShow.Text = YB1value.ToString("f4");
            _RPPF_T.Add(time, F1value);

            //显示曲线数据
            #region Y1-X1 / Y1-X2 第一、二条曲线
            switch (this.tscbY1.SelectedIndex)
            {
                case 1:
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "力/时间";
                            LineItemListEdit_0.Add(time, F1value);
                            _RPPList0.Add(time, F1value);
                            break;
                        case 2:
                            //strCurveName[0] = "力/位移";
                            LineItemListEdit_0.Add(D1value, F1value);
                            _RPPList0.Add(D1value, F1value);
                            break;
                        case 3:
                            //strCurveName[0] = "力/应变";
                            LineItemListEdit_0.Add(YB1value, F1value);
                            _RPPList0.Add(YB1value, F1value);
                            break;
                        default:
                            //strCurveName[0] = "";                           
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "力/时间";
                            LineItemListEdit_1.Add(time, F1value);
                            _RPPList1.Add(time, F1value);
                            break;
                        case 2:
                            //strCurveName[0] = "力/位移";
                            LineItemListEdit_1.Add(D1value, F1value);
                            _RPPList1.Add(D1value, F1value);
                            break;
                        case 3:
                            //strCurveName[0] = "力/应变";
                            LineItemListEdit_1.Add(YB1value, F1value);
                            _RPPList1.Add(YB1value, F1value);
                            break;
                        default:
                            //strCurveName[0] = "";                           
                            break;
                    }

                    break;
                case 2:
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "应力/时间";
                            LineItemListEdit_0.Add(time, R1value);
                            _RPPList0.Add(time, R1value);
                            break;
                        case 2:
                            //strCurveName[0] = "应力/位移";
                            LineItemListEdit_0.Add(D1value, R1value);
                            _RPPList0.Add(D1value, R1value);
                            break;
                        case 3:
                            //strCurveName[0] = "应力/应变";
                            LineItemListEdit_0.Add(YB1value, R1value);
                            _RPPList0.Add(YB1value, R1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "应力/时间";
                            LineItemListEdit_1.Add(time, R1value);
                            _RPPList1.Add(time, R1value);
                            break;
                        case 2:
                            //strCurveName[0] = "应力/位移";
                            LineItemListEdit_1.Add(D1value, R1value);
                            _RPPList1.Add(D1value, R1value);
                            break;
                        case 3:
                            //strCurveName[0] = "应力/应变";
                            LineItemListEdit_1.Add(YB1value, R1value);
                            _RPPList1.Add(YB1value, R1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }

                    break;
                case 3:
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "变形/时间";
                            LineItemListEdit_0.Add(time, BX1value);
                            _RPPList0.Add(time, BX1value);
                            break;
                        case 2:
                            //strCurveName[0] = "变形/位移";
                            LineItemListEdit_0.Add(D1value, BX1value);
                            _RPPList0.Add(D1value, BX1value);
                            break;
                        case 3:
                            //strCurveName[0] = "变形/应变";
                            LineItemListEdit_0.Add(YB1value, BX1value);
                            _RPPList0.Add(YB1value, BX1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "变形/时间";
                            LineItemListEdit_1.Add(time, BX1value);
                            _RPPList1.Add(time, BX1value);
                            break;
                        case 2:
                            //strCurveName[0] = "变形/位移";
                            LineItemListEdit_1.Add(D1value, BX1value);
                            _RPPList1.Add(D1value, BX1value);
                            break;
                        case 3:
                            //strCurveName[0] = "变形/应变";
                            LineItemListEdit_1.Add(YB1value, BX1value);
                            _RPPList1.Add(YB1value, BX1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }
                    break;
                case 4:
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "位移/时间";
                            LineItemListEdit_0.Add(time, D1value);
                            _RPPList0.Add(time, D1value);
                            break;
                        case 2:
                            //strCurveName[0] = "位移/位移";
                            LineItemListEdit_0.Add(D1value, D1value);
                            _RPPList0.Add(D1value, D1value);
                            break;
                        case 3:
                            //strCurveName[0] = "位移/应变";
                            LineItemListEdit_0.Add(YB1value, D1value);
                            _RPPList0.Add(YB1value, D1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "位移/时间";
                            LineItemListEdit_1.Add(time, D1value);
                            _RPPList1.Add(time, D1value);
                            break;
                        case 2:
                            //strCurveName[0] = "位移/位移";
                            LineItemListEdit_1.Add(D1value, D1value);
                            _RPPList1.Add(D1value, D1value);
                            break;
                        case 3:
                            //strCurveName[0] = "位移/应变";
                            LineItemListEdit_1.Add(YB1value, D1value);
                            _RPPList1.Add(YB1value, D1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }
                    break;
                default:
                    strCurveName[0] = "";
                    strCurveName[1] = "";
                    break;
            }
            #endregion

            #region Y2-X1 / Y2-X2 第三、四条曲线
            switch (this.tscbY2.SelectedIndex)
            {
                case 1:
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "力/时间";
                            LineItemListEdit_2.Add(time, F1value);
                            _RPPList2.Add(time, F1value);
                            break;
                        case 2:
                            //strCurveName[0] = "力/位移";
                            LineItemListEdit_2.Add(D1value, F1value);
                            _RPPList2.Add(D1value, F1value);
                            break;
                        case 3:
                            //strCurveName[0] = "力/应变";
                            LineItemListEdit_2.Add(YB1value, F1value);
                            _RPPList2.Add(YB1value, F1value);
                            break;
                        default:
                            //strCurveName[0] = "";                           
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "力/时间";
                            LineItemListEdit_3.Add(time, F1value);
                            _RPPList3.Add(time, F1value);
                            break;
                        case 2:
                            //strCurveName[0] = "力/位移";
                            LineItemListEdit_3.Add(D1value, F1value);
                            _RPPList3.Add(D1value, F1value);
                            break;
                        case 3:
                            //strCurveName[0] = "力/应变";
                            LineItemListEdit_3.Add(YB1value, F1value);
                            _RPPList3.Add(YB1value, F1value);
                            break;
                        default:
                            //strCurveName[0] = "";                           
                            break;
                    }

                    break;
                case 2:
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "应力/时间";
                            LineItemListEdit_2.Add(time, R1value);
                            _RPPList2.Add(time, R1value);
                            break;
                        case 2:
                            //strCurveName[0] = "应力/位移";
                            LineItemListEdit_2.Add(D1value, R1value);
                            _RPPList2.Add(D1value, R1value);
                            break;
                        case 3:
                            //strCurveName[0] = "应力/应变";
                            LineItemListEdit_2.Add(YB1value, R1value);
                            _RPPList2.Add(YB1value, R1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "应力/时间";
                            LineItemListEdit_3.Add(time, R1value);
                            _RPPList3.Add(time, R1value);
                            break;
                        case 2:
                            //strCurveName[0] = "应力/位移";
                            LineItemListEdit_3.Add(D1value, R1value);
                            _RPPList3.Add(D1value, R1value);
                            break;
                        case 3:
                            //strCurveName[0] = "应力/应变";
                            LineItemListEdit_3.Add(YB1value, R1value);
                            _RPPList3.Add(YB1value, R1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }

                    break;
                case 3:
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "变形/时间";
                            LineItemListEdit_2.Add(time, BX1value);
                            _RPPList2.Add(time, BX1value);
                            break;
                        case 2:
                            //strCurveName[0] = "变形/位移";
                            LineItemListEdit_2.Add(D1value, BX1value);
                            _RPPList2.Add(D1value, BX1value);
                            break;
                        case 3:
                            //strCurveName[0] = "变形/应变";
                            LineItemListEdit_2.Add(YB1value, BX1value);
                            _RPPList2.Add(YB1value, BX1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "变形/时间";
                            LineItemListEdit_3.Add(time, BX1value);
                            _RPPList3.Add(time, BX1value);
                            break;
                        case 2:
                            //strCurveName[0] = "变形/位移";
                            LineItemListEdit_3.Add(D1value, BX1value);
                            _RPPList3.Add(D1value, BX1value);
                            break;
                        case 3:
                            //strCurveName[0] = "变形/应变";
                            LineItemListEdit_3.Add(YB1value, BX1value);
                            _RPPList3.Add(YB1value, BX1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }
                    break;
                case 4:
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "位移/时间";
                            LineItemListEdit_2.Add(time, D1value);
                            _RPPList2.Add(time, D1value);
                            break;
                        case 2:
                            //strCurveName[0] = "位移/位移";
                            LineItemListEdit_2.Add(D1value, D1value);
                            _RPPList2.Add(D1value, D1value);
                            break;
                        case 3:
                            //strCurveName[0] = "位移/应变";
                            LineItemListEdit_2.Add(YB1value, D1value);
                            _RPPList2.Add(YB1value, D1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "位移/时间";
                            LineItemListEdit_3.Add(time, D1value);
                            _RPPList3.Add(time, D1value);
                            break;
                        case 2:
                            //strCurveName[0] = "位移/位移";
                            LineItemListEdit_3.Add(D1value, D1value);
                            _RPPList3.Add(D1value, D1value);
                            break;
                        case 3:
                            //strCurveName[0] = "位移/应变";
                            LineItemListEdit_3.Add(YB1value, D1value);
                            _RPPList3.Add(YB1value, D1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }
                    break;
                default:
                    strCurveName[2] = "";
                    strCurveName[3] = "";
                    break;
            }
            #endregion

            #region Y3-X1 / Y3-X2 第五、六条曲线
            switch (this.tscbY3.SelectedIndex)
            {
                case 1:
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "力/时间";
                            LineItemListEdit_4.Add(time, F1value);
                            _RPPList4.Add(time, F1value);
                            break;
                        case 2:
                            //strCurveName[0] = "力/位移";
                            LineItemListEdit_4.Add(D1value, F1value);
                            _RPPList4.Add(D1value, F1value);
                            break;
                        case 3:
                            //strCurveName[0] = "力/应变";
                            LineItemListEdit_4.Add(YB1value, F1value);
                            _RPPList4.Add(YB1value, F1value);
                            break;
                        default:
                            //strCurveName[0] = "";                           
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "力/时间";
                            LineItemListEdit_5.Add(time, F1value);
                            _RPPList5.Add(time, F1value);
                            break;
                        case 2:
                            //strCurveName[0] = "力/位移";
                            LineItemListEdit_5.Add(D1value, F1value);
                            _RPPList5.Add(D1value, F1value);
                            break;
                        case 3:
                            //strCurveName[0] = "力/应变";
                            LineItemListEdit_5.Add(YB1value, F1value);
                            _RPPList5.Add(YB1value, F1value);
                            break;
                        default:
                            //strCurveName[0] = "";                           
                            break;
                    }

                    break;
                case 2:
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "应力/时间";
                            LineItemListEdit_4.Add(time, R1value);
                            _RPPList4.Add(time, R1value);
                            break;
                        case 2:
                            //strCurveName[0] = "应力/位移";
                            LineItemListEdit_4.Add(D1value, R1value);
                            _RPPList4.Add(D1value, R1value);
                            break;
                        case 3:
                            //strCurveName[0] = "应力/应变";
                            LineItemListEdit_4.Add(YB1value, R1value);
                            _RPPList4.Add(YB1value, R1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "应力/时间";
                            LineItemListEdit_5.Add(time, R1value);
                            _RPPList5.Add(time, R1value);
                            break;
                        case 2:
                            //strCurveName[0] = "应力/位移";
                            LineItemListEdit_5.Add(D1value, R1value);
                            _RPPList5.Add(D1value, R1value);
                            break;
                        case 3:
                            //strCurveName[0] = "应力/应变";
                            LineItemListEdit_5.Add(YB1value, R1value);
                            _RPPList5.Add(YB1value, R1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }

                    break;
                case 3:
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "变形/时间";
                            LineItemListEdit_4.Add(time, BX1value);
                            _RPPList4.Add(time, BX1value);
                            break;
                        case 2:
                            //strCurveName[0] = "变形/位移";
                            LineItemListEdit_4.Add(D1value, BX1value);
                            _RPPList4.Add(D1value, BX1value);
                            break;
                        case 3:
                            //strCurveName[0] = "变形/应变";
                            LineItemListEdit_4.Add(YB1value, BX1value);
                            _RPPList4.Add(YB1value, BX1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "变形/时间";
                            LineItemListEdit_5.Add(time, BX1value);
                            _RPPList5.Add(time, BX1value);
                            break;
                        case 2:
                            //strCurveName[0] = "变形/位移";
                            LineItemListEdit_5.Add(D1value, BX1value);
                            _RPPList5.Add(D1value, BX1value);
                            break;
                        case 3:
                            //strCurveName[0] = "变形/应变";
                            LineItemListEdit_5.Add(YB1value, BX1value);
                            _RPPList5.Add(YB1value, BX1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }
                    break;
                case 4:
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "位移/时间";
                            LineItemListEdit_4.Add(time, D1value);
                            _RPPList4.Add(time, D1value);
                            break;
                        case 2:
                            //strCurveName[0] = "位移/位移";
                            LineItemListEdit_4.Add(D1value, D1value);
                            _RPPList4.Add(D1value, D1value);
                            break;
                        case 3:
                            //strCurveName[0] = "位移/应变";
                            LineItemListEdit_4.Add(YB1value, D1value);
                            _RPPList4.Add(YB1value, D1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1:
                            //strCurveName[0] = "位移/时间";
                            LineItemListEdit_5.Add(time, D1value);
                            _RPPList5.Add(time, D1value);
                            break;
                        case 2:
                            //strCurveName[0] = "位移/位移";
                            LineItemListEdit_5.Add(D1value, D1value);
                            _RPPList5.Add(D1value, D1value);
                            break;
                        case 3:
                            //strCurveName[0] = "位移/应变";
                            LineItemListEdit_5.Add(YB1value, D1value);
                            _RPPList5.Add(YB1value, D1value);
                            break;
                        default:
                            //strCurveName[0] = "";
                            break;
                    }
                    break;
                default:
                    strCurveName[4] = "";
                    strCurveName[5] = "";
                    break;
            }
            #endregion

            //存储采集的数据
            gdata gd = new gdata();
            gd.F1 = Math.Round(F1value, 4); ;
            gd.F2 = 0;
            gd.F3 = 0;
            gd.D1 = Math.Round(D1value, 4);
            gd.D2 = 0;
            gd.D3 = 0;
            gd.BX1 = Math.Round(BX1value, 4);
            gd.BX2 = 0;
            gd.BX3 = 0;
            gd.YL1 = Math.Round(R1value, 4);
            gd.YL2 = 0;
            gd.YL3 = 0;
            gd.YB1 = Math.Round(YB1value, 4);
            gd.YB2 = 0;
            gd.YB3 = 0;
            gd.Ts = Math.Round(time, 2);
            _List_Data.Add(gd);

            //坐标轴自动增加
            //Scale x2Scale = zedGraphControl.GraphPane.X2Axis.Scale;
            //x2Scale.MajorStep = 10;
            //x2Scale.MinAuto = true;
            //if (time > x2Scale.Max)
            //{
            //    x2Scale.Max = time + 5;// xScale.MajorStep;
            //    x2Scale.Min = 0;// xScale.Max - 30.0;
            //}

            //第三步:调用AxisChange()方法更新X和Y轴的范围
            zedGraphControl.AxisChange();
            //第四步:调用Invalidate()方法更新图表
            zedGraphControl.Invalidate();
        }

 

        #region
        //void mtimer_MicroTimerElapsed(object sender, MicroTimerEventArgs timerEventArgs)
        //{ 
        //  //确保CurveList不为空
        //  if (zedGraphControl1.GraphPane.CurveList.Count <= 0)
        //    return;       
        //  //取Graph第一个曲线，也就是第一步:在GraphPane.CurveList集合中查找CurveItem
        //  LineItem curve = zedGraphControl1.GraphPane.CurveList[0] as LineItem;
        //  if (curve == null)
        //    return; 
        //  //第二步:在CurveItem中访问PointPairList(或者其它的IPointList)，根据自己的需要增加新数据或修改已存在的数据
        //  IPointListEdit list = curve.Points as IPointListEdit; 
        //  if (list == null)
        //    return;

        //  double time = (Environment.TickCount - tickStart) / 1000.0; 
        //  this.lblTime.Text = time.ToString("0.000");
        //  list.Add(time,Math.Sqrt(time)); 

        //  Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
        //  //xScale.MajorStep = 20;
        //  if (time > xScale.Max)
        //  {
        //    xScale.Max = time + 5;// xScale.MajorStep;
        //    xScale.Min = 0;// xScale.Max - 30.0;
        //  }
        //  Scale yScale = zedGraphControl1.GraphPane.YAxis.Scale;
        //  if (Math.Sqrt(time) > yScale.Max)
        //  {
        //    yScale.Max = Math.Sqrt(time) + 5;
        //    yScale.Min = 0;
        //  }

        //  //第三步:调用ZedGraphControl.AxisChange()方法更新X和Y轴的范围
        //  zedGraphControl1.AxisChange(); 
        //  //第四步:调用Form.Invalidate()方法更新图表
        //  zedGraphControl1.Invalidate(); 
        //}

        //void timer_Tick(object sender, EventArgs e)
        //{ 
        //  // Make sure that the curvelist has at least one curve
        //  //确保CurveList不为空
        //  if (zedGraphControl1.GraphPane.CurveList.Count <= 0)
        //    return;

        //  // Get the first CurveItem in the graph
        //  //取Graph第一个曲线，也就是第一步:在GraphPane.CurveList集合中查找CurveItem
        //  LineItem curve = zedGraphControl1.GraphPane.CurveList[0] as LineItem;
        //  if (curve == null)
        //    return;
        //  // Get the PointPairList
        //  //第二步:在CurveItem中访问PointPairList(或者其它的IPointList)，根据自己的需要增加新数据或修改已存在的数据
        //  IPointListEdit list = curve.Points as IPointListEdit;
        //  // If this is null, it means the reference at curve.Points does not
        //  // support IPointListEdit, so we won't be able to modify it
        //  if (list == null)
        //    return;
        //  // Time is measured in seconds
        //  double time = (Environment.TickCount - tickStart) / 1000.0;


        //  this.lblTime.Text = time.ToString("0.000");

        //  // 3 seconds per cycle
        //  list.Add(time, Math.Sin(2.0 * Math.PI * time / 3.0));
        //  // Keep the X scale at a rolling 30 second interval, with one
        //  // major step between the max X value and the end of the axis
        //  Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
        //  //xScale.MajorStep = 20;
        //  if (time > xScale.Max)
        //  {
        //    xScale.Max = time + 5;// xScale.MajorStep;
        //    xScale.Min = 0;// xScale.Max - 30.0;
        //  }
        //  // Make sure the Y axis is rescaled to accommodate actual data
        //  //第三步:调用ZedGraphControl.AxisChange()方法更新X和Y轴的范围
        //  zedGraphControl1.AxisChange();
        //  // Force a redraw
        //  //第四步:调用Form.Invalidate()方法更新图表
        //  zedGraphControl1.Invalidate();
        //}



        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //  // Make sure that the curvelist has at least one curve
        //  //确保CurveList不为空
        //  if (zedGraphControl1.GraphPane.CurveList.Count <= 0)
        //    return;

        //  // Get the first CurveItem in the graph
        //  //取Graph第一个曲线，也就是第一步:在GraphPane.CurveList集合中查找CurveItem
        //  LineItem curve = zedGraphControl1.GraphPane.CurveList[0] as LineItem;
        //  if (curve == null)
        //    return;
        //  // Get the PointPairList
        //  //第二步:在CurveItem中访问PointPairList(或者其它的IPointList)，根据自己的需要增加新数据或修改已存在的数据
        //  IPointListEdit list = curve.Points as IPointListEdit;
        //  // If this is null, it means the reference at curve.Points does not
        //  // support IPointListEdit, so we won't be able to modify it
        //  if (list == null)
        //    return;
        //  // Time is measured in seconds
        //  double time = (Environment.TickCount - tickStart) / 1000.0;
        //  this.lblTime.Text = time.ToString("0.000");

        //  // 3 seconds per cycle
        //  list.Add(time, Math.Sin(2.0 * Math.PI * time / 3.0));
        //  // Keep the X scale at a rolling 30 second interval, with one
        //  // major step between the max X value and the end of the axis
        //  Scale xScale = zedGraphControl1.GraphPane.XAxis.Scale;
        //  //xScale.MajorStep = 20;
        //  if (time > xScale.Max)
        //  {
        //    xScale.Max = time + 5;// xScale.MajorStep;
        //    xScale.Min = 0;// xScale.Max - 30.0;
        //  }
        //  // Make sure the Y axis is rescaled to accommodate actual data
        //  //第三步:调用ZedGraphControl.AxisChange()方法更新X和Y轴的范围
        //  zedGraphControl1.AxisChange();
        //  // Force a redraw
        //  //第四步:调用Form.Invalidate()方法更新图表
        //  zedGraphControl1.Invalidate();
        //}
        #endregion

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmTestMain_SizeChanged(object sender, EventArgs e)
        {
            this.customPanel1.Width = this.customPanel2.Width = this.customPanel3.Width = this.customPanel4.Width = this.panel2.Width / 4 - 6;
            this.customPanel1.Left = 4;
            this.customPanel2.Left = this.panel2.Width / 4 + 2;
            this.customPanel3.Left = 2 * this.panel2.Width / 4 + 2;
            this.customPanel4.Left = 3 * this.panel2.Width / 4 + 2;
            this.customPanel1.Top = this.customPanel2.Top = this.customPanel3.Top = this.customPanel4.Top = 3;
        }

        private void tsbtn_Start_Click(object sender, EventArgs e)
        {
            if (this.lblTestSampleNo.Text.Trim().Length > 0)
            {
                //this.panel1.Visible = false;
                _S0 = double.Parse(this.lblS0.Text);
                if (_S0 > 0)
                {
                    this.dataGridView.Visible = false;
                   
                    this.tsbtn_Start.Enabled = false;
                    this.tsbtn_Stop.Enabled = true;
                    this.tsbtn_Pause.Enabled = true;
                    tickStart = Environment.TickCount;
                    this.btnZeroD.Enabled = this.btnZeroF.Enabled = this.btnZeroS.Enabled  = false;
                    this.tscbX1.Enabled = this.tscbX2.Enabled = this.tscbY1.Enabled = this.tscbY2.Enabled = this.tscbY3.Enabled = false;
                    initShowCurve();
                    foreach (LineItem li in this.zedGraphControl.GraphPane.CurveList)
                    {
                        li.Clear();
                    } 
                    mtimer.Start();
                }
                else
                {
                    MessageBox.Show("S0不能为0");
                    return;
                }
            }
            else
            {
                MessageBox.Show("请选择试样");
                return;
            }
        }

        private void tsbtn_Pause_Click(object sender, EventArgs e)
        {
            
        }

        //停止试验
        private void tsbtn_Stop_Click(object sender, EventArgs e)
        {
            mtimer.Stop();
            this.tsbtn_Start.Enabled = true;
            this.tsbtn_Stop.Enabled = false;
            this.tsbtn_Pause.Enabled = false;

            //计算最大值             
            if (this._RPPList0 != null && this._RPPList0.Count > 0)
            {
                this.zedGraphControl.GraphPane.CurveList[0].Label.Text += " Max:(" + getFm(_RPPList0).Y + "/" + getFm(_RPPList0).X + ")";
            }
            if (this._RPPList1 != null && this._RPPList1.Count > 0)
            {
                this.zedGraphControl.GraphPane.CurveList[1].Label.Text += " Max:(" + getFm(_RPPList1).Y + "/" + getFm(_RPPList1).X + ")";
            }
            if (this._RPPList2 != null && this._RPPList2.Count > 0)
            {
                this.zedGraphControl.GraphPane.CurveList[2].Label.Text += " Max:(" + getFm(_RPPList2).Y + "/" + getFm(_RPPList2).X + ")";
            }
            if (this._RPPList3 != null && this._RPPList3.Count > 0)
            {
                this.zedGraphControl.GraphPane.CurveList[3].Label.Text += " Max:(" + getFm(_RPPList3).Y + "/" + getFm(_RPPList3).X + ")";
            }
            if (this._RPPList4 != null && this._RPPList4.Count > 0)
            {
                this.zedGraphControl.GraphPane.CurveList[4].Label.Text += " Max:(" + getFm(_RPPList4).Y + "/" + getFm(_RPPList4).X + ")";
            }
            if (this._RPPList5 != null && this._RPPList5.Count > 0)
            {
                this.zedGraphControl.GraphPane.CurveList[5].Label.Text += " Max:(" + getFm(_RPPList5).Y + "/" + getFm(_RPPList5).X + ")";
            }

            //确认试验类型 ,写入试验结果
            string strContain = this.tvTestSample.SelectedNode.Name;
            int intContain = 0;
            if (strContain.Contains("extend"))
                intContain = 1;
            if (strContain.Contains("compress"))
                intContain = 2;
            if (strContain.Contains("bend"))
                intContain = 3;

            switch (intContain)
            {
                case 1:
                    if (this.lblTestSampleID.Text.Trim().Length > 0)
                    {
                        BLL.TestSample bllts = new HR_Test.BLL.TestSample();
                        Model.TestSample mts = bllts.GetModel(long.Parse(this.lblTestSampleID.Text));
                        //给曲线添加文本框
                        //TextObj text = new TextObj(this.zedGraphControl.GraphPane.CurveList[0].Label.Text + " Max:(" + getFm(_RPPList0).Y + "/" + getFm(_RPPList0).X + ")", 0, 0, CoordType.PaneFraction);
                        //text.Location.AlignH = AlignH.Left;
                        //text.Location.AlignV = AlignV.Top;
                        //text.FontSpec.Size = 5;
                        //text.FontSpec.FontColor = this.zedGraphControl.GraphPane.CurveList[0].Color;
                        //text.FontSpec.StringAlignment = StringAlignment.Far;
                        //this.zedGraphControl.GraphPane.GraphObjList.Add(text); 
                        //写入最大值 Fm Rm A Z
                        mts.Fm = getFm(this._RPPF_T).Y;
                        mts.Rm = Math.Round((double)(mts.Fm / mts.S0),4);
                        mts.testCondition = this.lblTestMethod.Text;
                        mts.isFinish = true;
                        bllts.Update(mts);
                        readMethod(this.tvTestSample);
                        readFinishSample(this.dataGridView);
                        MessageBox.Show(_List_Data.Count.ToString());
                        //MessageBox.Show(_RPPList1.Count.ToString()); 
                        this.btnZeroD.Enabled = this.btnZeroF.Enabled = this.btnZeroS.Enabled = true;
                        this.tscbX1.Enabled = this.tscbX2.Enabled = this.tscbY1.Enabled = this.tscbY2.Enabled = this.tscbY3.Enabled = true;
                        // File.Create(AppDomain.CurrentDomain.BaseDirectory + "data.txt");
                        //保存曲线
                        string curveName = AppDomain.CurrentDomain.BaseDirectory + "Curve\\Extend\\" + this.lblTestSampleNo.Text + ".txt";
                        StreamWriter sw = new StreamWriter(curveName);
                        sw.WriteLine("testSampleNo,S0,L0");
                        sw.WriteLine(this.lblTestSampleNo.Text + "," + this._S0.ToString("0.000") + "," + this.lblL0.Text);
                        foreach (gdata gd in _List_Data)
                        {
                            sw.WriteLine(gd.F1 + "," + gd.F2 + "," + gd.F3 + "," + gd.D1 + "," + gd.D2 + "," + gd.D3 + "," + gd.BX1 + "," + gd.BX2 + "," + gd.BX3 + "," + gd.YL1 + "," + gd.YL2 + "," + gd.YL3 + "," + gd.YB1 + "," + gd.YB2 + "," + gd.YB3 + "," + gd.Ts);
                            sw.Flush();
                        }
                        sw.Close();
                        sw.Dispose();
                        safeCurveFile(curveName, curveName.Replace(".txt", ".lin"));
                    }
                    break;
                case 2:
                    if (this.lblTestSampleID.Text.Trim().Length > 0)
                    {
                        BLL.Compress bllts = new HR_Test.BLL.Compress();
                        Model.Compress mts = bllts.GetModel(long.Parse(this.lblTestSampleID.Text));

                        //写入最大值 Fm Rm A Z
                        mts.Fmc = getFm(this._RPPF_T).Y;
                        mts.Rmc = mts.Fmc / double.Parse(mts.S0.ToString());
                        mts.isFinish = true;
                        mts.testCondition = this.lblTestMethod.Text;
                        mts.assessor = "";
                        mts.testSampleNo = this.lblTestSampleNo.Text;
                        bllts.Update(mts);
                        readMethod(this.tvTestSample);
                        readFinishSample_C(this.dataGridView);
                        MessageBox.Show(_List_Data.Count.ToString());
                        this.btnZeroD.Enabled = this.btnZeroF.Enabled = this.btnZeroS.Enabled= true;
                        this.tscbX1.Enabled = this.tscbX2.Enabled = this.tscbY1.Enabled = this.tscbY2.Enabled = this.tscbY3.Enabled = true;
                        // File.Create(AppDomain.CurrentDomain.BaseDirectory + "data.txt");
                        //保存曲线
                        string curveName = AppDomain.CurrentDomain.BaseDirectory + "Curve\\Compress\\" + this.lblTestSampleNo.Text + ".txt";
                        StreamWriter sw = new StreamWriter(curveName);
                        sw.WriteLine("testSampleNo,S0,L0");
                        sw.WriteLine(this.lblTestSampleNo.Text + "," + this._S0.ToString("0.000") + "," + this.lblL0.Text);
                        foreach (gdata gd in _List_Data)
                        {
                            sw.WriteLine(gd.F1 + "," + gd.F2 + "," + gd.F3 + "," + gd.D1 + "," + gd.D2 + "," + gd.D3 + "," + gd.BX1 + "," + gd.BX2 + "," + gd.BX3 + "," + gd.YL1 + "," + gd.YL2 + "," + gd.YL3 + "," + gd.YB1 + "," + gd.YB2 + "," + gd.YB3 + "," + gd.Ts);
                            sw.Flush();
                        }
                        sw.Close();
                        sw.Dispose();
                        safeCurveFile(curveName, curveName.Replace(".txt", ".lin"));
                    }
                    break;
                case 3:
                    if (this.lblTestSampleID.Text.Trim().Length > 0)
                    {
                        BLL.Bend bllts = new HR_Test.BLL.Bend();
                        Model.Bend mts = bllts.GetModel(long.Parse(this.lblTestSampleID.Text));
                        //写入试验结果 
                        mts.isFinish = true;
                        mts.testCondition = this.lblTestMethod.Text;
                        mts.assessor = "";
                        mts.testSampleNo = this.lblTestSampleNo.Text;
                        bllts.Update(mts);
                        readMethod(this.tvTestSample);
                        readFinishSample_B(this.dataGridView);
                        MessageBox.Show(_List_Data.Count.ToString());
                        this.btnZeroD.Enabled = this.btnZeroF.Enabled = this.btnZeroS.Enabled  = true;
                        this.tscbX1.Enabled = this.tscbX2.Enabled = this.tscbY1.Enabled = this.tscbY2.Enabled = this.tscbY3.Enabled = true;
                        // File.Create(AppDomain.CurrentDomain.BaseDirectory + "data.txt");
                        //保存曲线
                        string curveName = AppDomain.CurrentDomain.BaseDirectory + "Curve\\Bend\\" + this.lblTestSampleNo.Text + ".txt";
                        StreamWriter sw = new StreamWriter(curveName);
                        sw.WriteLine("testSampleNo,l,D");
                        sw.WriteLine(this.lblTestSampleNo.Text + "," + this._S0.ToString("0.000") + "," + this.lblL0.Text);
                        foreach (gdata gd in _List_Data)
                        {
                            sw.WriteLine(gd.F1 + "," + gd.F2 + "," + gd.F3 + "," + gd.D1 + "," + gd.D2 + "," + gd.D3 + "," + gd.BX1 + "," + gd.BX2 + "," + gd.BX3 + "," + gd.YL1 + "," + gd.YL2 + "," + gd.YL3 + "," + gd.YB1 + "," + gd.YB2 + "," + gd.YB3 + "," + gd.Ts);
                            sw.Flush();
                        }
                        sw.Close();
                        sw.Dispose();
                        safeCurveFile(curveName, curveName.Replace(".txt", ".lin"));
                    }
                    break;
                default:
                    break;
            }

            //清空曲线数组
            _RPPF_T = null;
            _RPPList0 = null;
            _RPPList1 = null;
            _RPPList2 = null;
            _RPPList3 = null;
            _RPPList4 = null;
            _RPPList5 = null;

            _RPPList0 = new RollingPointPairList(100000);
            _RPPList1 = new RollingPointPairList(100000);
            _RPPList2 = new RollingPointPairList(100000);
            _RPPList3 = new RollingPointPairList(100000);
            _RPPList4 = new RollingPointPairList(100000);
            _RPPList5 = new RollingPointPairList(100000);
            _RPPF_T = new RollingPointPairList(100000);

            //删除源文件
            //if (File.Exists(curveName))
            //    File.Delete(curveName);
            this.lblTestSampleID.Text = this.lblTestSampleNo.Text = this.lblL0.Text = this.lblS0.Text = "";
            this.dataGridView.Visible = true;
            this.panel1.Visible = true;
        }

        //取最大值的点
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

        //取最大值的序号
        private Int32 getYMaxIndex(RollingPointPairList rppl)
        {
            PointD fm = new PointD();
            Int32 i = 0;
            Int32 j = 0;
            while (i < rppl.Count)
            {
                if (fm.Y < rppl[i].Y)
                {
                    fm.Y = rppl[i].Y;
                    fm.X = rppl[i].X;
                    j = i;
                }
                i++;
            }
            return j;
        }

        private void safeCurveFile(string inFilePath, string outFilePath)
        {
            // Distribute this key to the user who will decrypt this file.
            string sSecretKey;
            // Get the Key for the file to Encrypt.            
            string[] key = RWconfig.GetAppSettings("code").ToString().Split('-');
            byte[] keyee = new byte[8];
            //转换为 key byte数组
            for (int j = 0; j < key.Length; j++)
            {
                keyee[j] = Byte.Parse(key[j], System.Globalization.NumberStyles.HexNumber);
            }
            sSecretKey = ASCIIEncoding.ASCII.GetString(keyee);
            // For additional security Pin the key.
            GCHandle gch = GCHandle.Alloc(sSecretKey, GCHandleType.Pinned);
            // Encrypt the file.    
            Safe.EncryptFile(inFilePath, outFilePath, sSecretKey);
            // Decrypt the file.
            // Remove the Key from memory. 
            Safe.ZeroMemory(gch.AddrOfPinnedObject(), sSecretKey.Length * 2);
            gch.Free();
        }

        private void tsbtnCurve_Click(object sender, EventArgs e)
        {
            frmAnalysiseCurve fac = new frmAnalysiseCurve();
            fac.TopLevel = false;
            fac.Name = "c_AnalysiseCurve";
            this.Parent.Controls.Add(fac);
            fac.BringToFront();
            fac.WindowState = FormWindowState.Maximized;
            fac.Show();
        }

        private void tsbtn_Exit_Click(object sender, EventArgs e)
        {
            //this.mtimer.Stop();
            this.Dispose();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //safeCurveFile(AppDomain.CurrentDomain.BaseDirectory + "data1.lin", AppDomain.CurrentDomain.BaseDirectory + "data2.txt");
        }

        private void tsbtn_Curve_Click(object sender, EventArgs e)
        {
            //safeCurveFile(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +"\\HRData.txt", AppDomain.CurrentDomain.BaseDirectory + "Curve\\test-1.lin");
            switch (dataGridView.Name)
            {
                case "extend":
                    if (!string.IsNullOrEmpty(selTestSampleNo))
                    {
                        frmAnalysiseCurve fac = new frmAnalysiseCurve();
                        fac.WindowState = FormWindowState.Maximized;
                        fac._CurveName = AppDomain.CurrentDomain.BaseDirectory + "Curve\\Extend\\" + selTestSampleNo + ".lin";
                        fac.ShowDialog();
                    }
                    break;
                case "compress":
                    if (!string.IsNullOrEmpty(selTestSampleNo))
                    {
                        frmAnalysiseCurve fac = new frmAnalysiseCurve();
                        fac.WindowState = FormWindowState.Maximized;
                        fac._CurveName = AppDomain.CurrentDomain.BaseDirectory + "Curve\\Compress\\" + selTestSampleNo + ".lin";
                        fac.ShowDialog();
                    }
                    break;
                case "bend":
                    if (!string.IsNullOrEmpty(selTestSampleNo))
                    {
                        frmAnalysiseCurve fac = new frmAnalysiseCurve();
                        fac.WindowState = FormWindowState.Maximized;
                        fac._CurveName = AppDomain.CurrentDomain.BaseDirectory + "Curve\\Bend\\" + selTestSampleNo + ".lin";
                        fac.ShowDialog();
                    }
                    break;
            }
        }

        private void tvTestSample_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string method = "";
            switch (e.Node.Text)
            {
                case "拉伸试验":
                    this.dataGridView.Name = "extend";
                    readFinishSample(this.dataGridView);
                    break;
                case "压缩试验":
                    this.dataGridView.Name = "compress";
                    readFinishSample_C(this.dataGridView);
                    break;
                case "弯曲试验":
                    this.dataGridView.Name = "bend";
                    readFinishSample_B(this.dataGridView);
                    break;
                default:
                    break;
            }

            if (e.Node.Name != null && e.Node.Name.Trim().Length > 0)
            {
                tvTestSample.SelectedNode = e.Node; 
                if (e.Node.Name.Contains("extend"))
                {
                    BLL.TestSample bllts = new HR_Test.BLL.TestSample();
                    Model.TestSample mts = bllts.GetModel(int.Parse(e.Node.Name.Replace("extend", "")));
                    this.lblL0.Text = mts.L0.ToString();
                    this.lblTestSampleNo.Text = mts.testSampleNo;
                    this.lblS0.Text = mts.S0.ToString();
                    this.lblTestSampleID.Text = mts.ID.ToString();
                    BLL.ControlMethod bllcm = new HR_Test.BLL.ControlMethod();
                    Model.ControlMethod mcm = bllcm.GetModel(mts.testMethodName);
                    this.label32.Text = "S0(mm²):";
                    this.label31.Text = "L0(mm):";
                    readFinishSample(this.dataGridView);
                }

                if (e.Node.Name.Contains("compress"))
                {
                    BLL.Compress bllts = new HR_Test.BLL.Compress();
                    Model.Compress mts = bllts.GetModel(int.Parse(e.Node.Name.Replace("compress", "")));
                    this.lblL0.Text = mts.L0.ToString();
                    this.lblTestSampleNo.Text = mts.testSampleNo;
                    this.lblS0.Text = mts.S0.ToString();
                    this.lblTestSampleID.Text = mts.ID.ToString(); 
                    BLL.ControlMethod_C bllcm = new HR_Test.BLL.ControlMethod_C();
                    Model.ControlMethod_C mcm = bllcm.GetModel(mts.testMethodName);
                    this.label32.Text = "S0(mm²):";
                    this.label31.Text = "L0(mm):";
                    readFinishSample_C(this.dataGridView);
                }

                if (e.Node.Name.Contains("bend"))
                {
                    BLL.Bend bllts = new HR_Test.BLL.Bend();
                    Model.Bend mts = bllts.GetModel(int.Parse(e.Node.Name.Replace("bend", "")));
                    this.label32.Text = "l(mm):";
                    this.label31.Text = "D(mm):";

                    this.lblL0.Text = mts.L.ToString();
                    this.lblTestSampleNo.Text = mts.testSampleNo;
                    this.lblS0.Text = mts.D.ToString();

                    this.lblTestSampleID.Text = mts.ID.ToString();
                    BLL.ControlMethod_B bllcm = new HR_Test.BLL.ControlMethod_B();
                    Model.ControlMethod_B mcm = bllcm.GetModel(mts.testMethodName); 
                    readFinishSample_B(this.dataGridView);
                }

                //if (mcm.isProLoad)
                //{
                //  method += "预载 : 是;\r\n";
                //  switch (mcm.proLoadType)
                //  {
                //    case 0:
                //      method += "预载值:负荷 " + mcm.proLoadValue + "kN\r\n";
                //      break;
                //    case 1:
                //      method += "预载值:变形 " + mcm.proLoadValue + "mm\r\n";
                //      break;
                //  }

                //  switch (mcm.proLoadControlType)
                //  {
                //    case 0:
                //      method += "控制方式:负荷 " + mcm.proLoadSpeed + "kN/s\r\n";
                //      break;
                //    case 1:
                //      method += "控制方式:位移 " + mcm.proLoadSpeed + "mm/min\r\n";
                //      break;
                //  }
                //}
                //else
                //{
                //  method += "预载 : 否;";
                //}

                //method += "---------------\r\n";
                //if (mcm != null)
                //{
                //    if (true)
                //    {
                //        method += "不连续屈服\r\n";
                //        method += "---------------\r\n";
                //        method += "弹性段 - \r\n";
                //        string[] str1 = mcm.controlType1.Split(',');
                //        switch (str1[0])
                //        {
                //            case "0":
                //                method += "位移控制:" + str1[1] + "mm/min\r\n";
                //                break;
                //            case "1":
                //                method += "eLe控制:" + str1[1] + "mm/min\r\n";
                //                break;
                //            case "2":
                //                method += "eLc控制:" + str1[1] + "mm/min\r\n";
                //                break;
                //            case "3":
                //                method += "应力控制:" + str1[1] + "MPa/s\r\n";
                //                break;
                //        }
                //        switch (str1[2])
                //        {
                //            case "0":
                //                method += "转换点 负荷:" + str1[3] + "kN\r\n\r\n";
                //                break;
                //            case "1":
                //                method += "转换点 应力:" + str1[3] + "MPa\r\n\r\n";
                //                break;
                //            case "2":
                //                method += "转换点 变形:" + str1[3] + "mm\r\n\r\n";
                //                break;
                //            case "3":
                //                method += "转换点 应变:" + str1[3] + "mm \r\n\r\n";
                //                break;
                //        }

                //        method += "屈服段 - \r\n";
                //        string[] str2 = mcm.controlType2.Split(',');
                //        switch (str2[0])
                //        {
                //            case "0":
                //                method += "位移控制 :" + str1[1] + "mm/min\r\n";
                //                break;
                //            case "1":
                //                method += "eLe控制 :" + str1[1] + "mm/min\r\n ";
                //                break;
                //            case "2":
                //                method += "eLc控制 :" + str1[1] + "mm/min\r\n";
                //                break;
                //            case "3":
                //                method += "应力控制 :" + str1[1] + "MPa/s\r\n ";
                //                break;
                //        }
                //        switch (str2[2])
                //        {
                //            case "0":
                //                method += "转换点 负荷:" + str1[3] + "kN\r\n\r\n";
                //                break;
                //            case "1":
                //                method += "转换点 应力:" + str1[3] + "MPa\r\n\r\n";
                //                break;
                //            case "2":
                //                method += "转换点 变形:" + str1[3] + "mm\r\n\r\n";
                //                break;
                //            case "3":
                //                method += "转换点 应变:" + str1[3] + "mm\r\n\r\n";
                //                break;
                //        }

                //        method += "加工硬化段 - \r\n";
                //        string[] str3 = mcm.controlType3.Split(',');
                //        switch (str3[0])
                //        {
                //            case "0":
                //                method += "位移控制 :" + str1[1] + "mm/min\r\n";
                //                break;
                //            case "1":
                //                method += "eLe控制 :" + str1[1] + "mm/min\r\n";
                //                break;
                //            case "2":
                //                method += "eLc控制 :" + str1[1] + "mm/min\r\n";
                //                break;
                //            case "3":
                //                method += "应力控制 :" + str1[1] + "MPa/s\r\n";
                //                break;
                //        }
                //        method += "停止测试:" + mcm.stopValue + " %\r\n";
                //    }
                //    else
                //    {
                //        method += "连续屈服 \r\n";
                //        method += "---------------\r\n";
                //        method += "弹性段 - \r\n";
                //        string[] str1 = mcm.controlType1.Split(',');
                //        switch (str1[0])
                //        {
                //            case "0":
                //                method += "位移控制:" + str1[1] + "mm/min\r\n";
                //                break;
                //            case "1":
                //                method += "eLe控制:" + str1[1] + "mm/min\r\n ";
                //                break;
                //            case "2":
                //                method += "eLc控制:" + str1[1] + "mm/min\r\n ";
                //                break;
                //            case "3":
                //                method += "应力控制:" + str1[1] + "MPa/s\r\n";
                //                break;
                //        }
                //        switch (str1[2])
                //        {
                //            case "0":
                //                method += "转换点负荷:" + str1[3] + "kN\r\n\r\n";
                //                break;
                //            case "1":
                //                method += "转换点应力:" + str1[3] + "MPa\r\n\r\n";
                //                break;
                //            case "2":
                //                method += "转换点变形:" + str1[3] + "mm\r\n\r\n";
                //                break;
                //            case "3":
                //                method += "转换点应变:" + str1[3] + "mm\r\n\r\n";
                //                break;
                //        }
                //        method += "试验段 -\r\n ";
                //        string[] str2 = mcm.controlType2.Split(',');
                //        switch (str2[0])
                //        {
                //            case "0":
                //                method += "位移控制:" + str1[1] + "mm/min\r\n";
                //                break;
                //            case "1":
                //                method += "eLe控制:" + str1[1] + "mm/min\r\n";
                //                break;
                //            case "2":
                //                method += "eLc控制:" + str1[1] + "mm/min\r\n";
                //                break;
                //            case "3":
                //                method += "应力控制:" + str1[1] + "MPa/s\r\n";
                //                break;
                //        }
                //        method += "停止测试:" + mcm.stopValue + " %\r\n";
                //    }
                //    method += "---------------\r\n";
                //    if (mcm.isTakeDownExten)
                //    {
                //        method += "是否取引伸计: 是\r\n";
                //        method += "取引伸计通道:" + mcm.extenChannel + "\r\n";
                //        switch (int.Parse(mcm.sign))
                //        {
                //            case 0:
                //                method += "取引伸计值: " + mcm.extenValue + " %";
                //                break;
                //            case 1:
                //                method += "取引伸计值: " + mcm.extenValue + " mm";
                //                break;
                //        }


                //    }
                //    else
                //    {
                //        method += "是否取引伸计: 否";
                //    }
                //}
                this.lblTestMethod.Text = method;
            }
        }

        private void btnChangeYL_Click(object sender, EventArgs e)
        {
            _showYL = !_showYL;
            if (_showYL)
            {
                this.lblYLShow.Visible = true;
                this.lblFShow.Visible = false;
                this.lblFuhe.Text = "应力";
                this.btnChangeYL.Text = "负荷";
                this.lblkN.Text = "[MPa]";
            }
            else
            {
                this.lblYLShow.Visible = false;
                this.lblFShow.Visible = true;
                this.lblFuhe.Text = "负荷";
                this.btnChangeYL.Text = "应力";
                this.lblkN.Text = "[kN]";
            }
        }

        private void btnYb_Click(object sender, EventArgs e)
        {
            _showYB = !_showYB;
            if (_showYB)
            {
                this.lblYBShow.Visible = true;
                this.lblBXShow.Visible = false;
                this.lblBx.Text = "应变";
                this.btnYb.Text = "变形";
            }
            else
            {
                this.lblYBShow.Visible = false;
                this.lblBXShow.Visible = true;
                this.lblBx.Text = "变形";
                this.btnYb.Text = "应变";
            }
        }

        private void tscbX1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscbX1.SelectedIndex == tscbX2.SelectedIndex && tscbX1.SelectedIndex != 0)
            {
                MessageBox.Show("X1,X2两轴不能选择同样的数据，请重新选择。");
                tscbX1.SelectedIndex = 0;
                initShowCurve(); RWconfig.SetAppSettings("X1", this.tscbX1.SelectedIndex.ToString());
                return;
            }
            switch (tscbX1.SelectedIndex)
            {
                case 0:
                    myPanel.XAxis.Title.Text = "X1";
                    myPanel.XAxis.Scale.MaxAuto = true;
                    break;
                case 1:
                    myPanel.XAxis.Title.Text = "时间,s";
                    myPanel.XAxis.Scale.MaxAuto = true;
                    break;
                case 2:
                    myPanel.XAxis.Title.Text = "位移,mm";
                    myPanel.XAxis.Scale.LabelGap = 0;
                    myPanel.XAxis.Scale.MaxAuto = true;
                    break;
                case 3:
                    myPanel.XAxis.Title.Text = "应变,mm";
                    myPanel.XAxis.Scale.MaxAuto = true;
                    break;
                default:
                    myPanel.XAxis.Title.Text = "X1";
                    myPanel.XAxis.Scale.MaxAuto = true;
                    break;
            }
            initShowCurve();
            this.zedGraphControl.Refresh();
            RWconfig.SetAppSettings("X1", this.tscbX1.SelectedIndex.ToString());
        }

        private void tscbX2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscbX1.SelectedIndex == tscbX2.SelectedIndex && tscbX2.SelectedIndex != 0)
            {
                MessageBox.Show("x1,x2两轴不能选择同样的数据，请重新选择。");
                tscbX2.SelectedIndex = 0; initShowCurve();
                RWconfig.SetAppSettings("X2", this.tscbX2.SelectedIndex.ToString());
                return;
            }
            switch (tscbX2.SelectedIndex)
            {
                case 0:
                    myPanel.X2Axis.Title.Text = "X2";
                    myPanel.X2Axis.Scale.MaxAuto = true;
                    break;
                case 1:
                    myPanel.X2Axis.Title.Text = "时间,s";
                    myPanel.X2Axis.Scale.MaxAuto = true;
                    break;
                case 2:
                    myPanel.X2Axis.Title.Text = "位移,mm";
                    myPanel.X2Axis.Scale.LabelGap = 0;
                    myPanel.X2Axis.Scale.MaxAuto = true;
                    break;

                case 3:
                    myPanel.X2Axis.Title.Text = "应变,mm";
                    myPanel.X2Axis.IsVisible = true;
                    myPanel.X2Axis.MajorGrid.IsVisible = true;
                    myPanel.X2Axis.Scale.LabelGap = 0;
                    myPanel.X2Axis.Scale.MaxAuto = true;
                    break;
                default:
                    myPanel.X2Axis.Title.Text = "X2";
                    myPanel.X2Axis.Scale.MaxAuto = true;
                    break;
            }
            initShowCurve();
            this.zedGraphControl.Refresh();
            RWconfig.SetAppSettings("X2", this.tscbX2.SelectedIndex.ToString());
        }

        private void tscbY1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //0-请选择-   1力,kN  2应力,MPa 3变形,mm 4应变,mm 5位移,mm
            if ((tscbY1.SelectedIndex == tscbY2.SelectedIndex || tscbY1.SelectedIndex == tscbY3.SelectedIndex) && tscbY1.SelectedIndex != 0)
            {
                tscbY1.SelectedIndex = 0;
                initShowCurve(); RWconfig.SetAppSettings("Y1", this.tscbY1.SelectedIndex.ToString());
                MessageBox.Show("Y1,Y2,Y3 不能选择同样的数据，请重新选择。");
                return;
            }
            switch (tscbY1.SelectedIndex)
            {
                case 0:
                    myPanel.YAxisList[1].Title.Text = "Y1";
                    myPanel.YAxisList[1].Scale.LabelGap = 0;
                    myPanel.YAxisList[1].Scale.MaxAuto = true;
                    break;
                case 1:
                    myPanel.YAxisList[1].Title.Text = "力,kN";
                    myPanel.YAxisList[1].Scale.LabelGap = 0;
                    myPanel.YAxisList[1].Scale.MaxAuto = true;
                    break;
                case 2:
                    myPanel.YAxisList[1].Title.Text = "应力,MPa";
                    myPanel.YAxisList[1].Scale.LabelGap = 0;
                    myPanel.YAxisList[1].Scale.MaxAuto = true;
                    break;
                case 3:
                    myPanel.YAxisList[1].Title.Text = "变形,mm";
                    myPanel.YAxisList[1].Scale.LabelGap = 0;
                    myPanel.YAxisList[1].Scale.MaxAuto = true;
                    break;
                case 4:
                    myPanel.YAxisList[1].Title.Text = "位移,mm";
                    myPanel.YAxisList[1].Scale.LabelGap = 0;
                    myPanel.YAxisList[1].Scale.MaxAuto = true;
                    break;
            }
            initShowCurve();
            this.zedGraphControl.Refresh(); RWconfig.SetAppSettings("Y1", this.tscbY1.SelectedIndex.ToString());

        }

        private void tscbY2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //0-请选择-   1力,kN  2应力,MPa 3变形,mm 4应变,mm 5位移,mm
            if ((tscbY2.SelectedIndex == tscbY1.SelectedIndex || tscbY2.SelectedIndex == tscbY3.SelectedIndex) && tscbY2.SelectedIndex != 0)
            {
                MessageBox.Show("Y1,Y2,Y3 不能选择同样的数据，请重新选择。");
                tscbY2.SelectedIndex = 0; initShowCurve(); RWconfig.SetAppSettings("Y2", this.tscbY2.SelectedIndex.ToString());
                return;
            }
            switch (tscbY2.SelectedIndex)
            {
                case 0:
                    myPanel.YAxis.Title.Text = "Y2";
                    myPanel.YAxis.Scale.MaxAuto = true;
                    break;
                case 1:
                    myPanel.YAxis.Title.Text = "力,kN";
                    myPanel.YAxis.Scale.MaxAuto = true;
                    break;
                case 2:
                    myPanel.YAxis.Title.Text = "应力,MPa";
                    myPanel.YAxis.Scale.MaxAuto = true;
                    break;
                case 3:
                    myPanel.YAxis.Title.Text = "变形,mm";
                    myPanel.YAxis.Scale.MaxAuto = true;
                    break;
                case 4:
                    myPanel.YAxis.Title.Text = "位移,mm";
                    myPanel.YAxis.Scale.MaxAuto = true;
                    break;
            } initShowCurve(); this.zedGraphControl.Refresh(); RWconfig.SetAppSettings("Y2", this.tscbY2.SelectedIndex.ToString());

        }

        private void tscbY3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //0-请选择-   1力,kN  2应力,MPa 3变形,mm 4应变,mm 5位移,mm
            if ((tscbY3.SelectedIndex == tscbY1.SelectedIndex || tscbY3.SelectedIndex == tscbY2.SelectedIndex) && tscbY3.SelectedIndex != 0)
            {
                MessageBox.Show("Y1,Y2,Y3 不能选择同样的数据，请重新选择。");
                tscbY3.SelectedIndex = 0; initShowCurve(); RWconfig.SetAppSettings("Y3", this.tscbY3.SelectedIndex.ToString());
                return;
            }
            switch (tscbY3.SelectedIndex)
            {
                case 0:
                    myPanel.Y2Axis.Title.Text = "Y3";
                    myPanel.Y2Axis.Scale.MaxAuto = true;
                    break;
                case 1:
                    myPanel.Y2Axis.Title.Text = "力,kN";
                    myPanel.Y2Axis.Scale.MaxAuto = true;
                    break;
                case 2:
                    myPanel.Y2Axis.Title.Text = "应力,MPa";
                    myPanel.Y2Axis.Scale.MaxAuto = true;
                    break;
                case 3:
                    myPanel.Y2Axis.Title.Text = "变形,mm";
                    myPanel.Y2Axis.Scale.MaxAuto = true;
                    break;
                case 4:
                    myPanel.Y2Axis.Title.Text = "位移,mm";
                    myPanel.Y2Axis.Scale.MaxAuto = true;
                    break;
            } initShowCurve(); this.zedGraphControl.Refresh(); RWconfig.SetAppSettings("Y3", this.tscbY3.SelectedIndex.ToString());
        }

        private void initShowCurve()
        {
            foreach (LineItem li in zedGraphControl.GraphPane.CurveList)
            {
                li.Label.Text = "";
            }

            //string strSelIndexX = this.tscbX1.SelectedIndex.ToString() + this.tscbX2.SelectedIndex.ToString();
            //string strSelIndexY = this.tscbY1.SelectedIndex.ToString() + this.tscbY2.SelectedIndex.ToString() +this.tscbY3.SelectedIndex.ToString(); 

            //          //Y1    0-请选择-    1力,kN       2应力,MPa      3变形,mm    4位移,mm
            //          //Y2    0-请选择-    1力,kN       2应力,MPa      3变形,mm    4位移,mm
            //          //Y3    0-请选择-    1力,kN       2应力,MPa      3变形,mm    4位移,mm

            //          //X1    0-请选择-    1时间,s      2位移,mm       3应变,mm
            //          //X2    0-请选择-    1时间,s      2位移,mm       3应变,mm

            //总共 6 条曲线
            //Y1-X1 Y1-X2 Y2-X1 Y2-X2 Y3-X1 Y3-X2;

            #region Y1-X1 / Y1-X2 第一、二条曲线
            switch (this.tscbY1.SelectedIndex)
            {
                case 1: this.zedGraphControl.GraphPane.YAxisList[1].IsVisible = true;
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1:
                            strCurveName[0] = "力/时间";
                            this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            break;
                        case 2:
                            strCurveName[0] = "力/位移";
                            this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            break;
                        case 3:
                            strCurveName[0] = "力/应变";
                            this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            break;
                        default:
                            strCurveName[0] = "";
                            this.zedGraphControl.GraphPane.XAxis.IsVisible = false;
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1:
                            strCurveName[1] = "力/时间";
                            this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            break;
                        case 2:
                            strCurveName[1] = "力/位移";
                            this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            break;
                        case 3:
                            strCurveName[1] = "力/应变";
                            this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            break;
                        default:
                            strCurveName[1] = "";
                            this.zedGraphControl.GraphPane.X2Axis.IsVisible = false;
                            break;
                    }

                    break;
                case 2: this.zedGraphControl.GraphPane.YAxisList[1].IsVisible = true;
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1:
                            strCurveName[0] = "应力/时间"; this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            break;
                        case 2:
                            strCurveName[0] = "应力/位移"; this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            break;
                        case 3:
                            strCurveName[0] = "应力/应变"; this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            break;
                        default:
                            strCurveName[0] = ""; this.zedGraphControl.GraphPane.XAxis.IsVisible = false;
                            break;
                    } switch (this.tscbX2.SelectedIndex)
                    {
                        case 1:
                            strCurveName[1] = "应力/时间"; this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            break;
                        case 2:
                            strCurveName[1] = "应力/位移"; this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            break;
                        case 3:
                            strCurveName[1] = "应力/应变"; this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            break;
                        default:
                            strCurveName[1] = ""; this.zedGraphControl.GraphPane.X2Axis.IsVisible = false;
                            break;
                    }

                    break;
                case 3: this.zedGraphControl.GraphPane.YAxisList[1].IsVisible = true;
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1:
                            strCurveName[0] = "变形/时间"; this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            break;
                        case 2:
                            strCurveName[0] = "变形/位移"; this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            break;
                        case 3:
                            strCurveName[0] = "变形/应变"; this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            break;
                        default:
                            strCurveName[0] = ""; this.zedGraphControl.GraphPane.XAxis.IsVisible = false;
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1:
                            strCurveName[1] = "变形/时间"; this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            break;
                        case 2:
                            strCurveName[1] = "变形/位移"; this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            break;
                        case 3:
                            strCurveName[1] = "变形/应变"; this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            break;
                        default:
                            strCurveName[1] = ""; this.zedGraphControl.GraphPane.X2Axis.IsVisible = false;
                            break;
                    }
                    break;
                case 4: this.zedGraphControl.GraphPane.YAxisList[1].IsVisible = true;
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1:
                            strCurveName[0] = "位移/时间"; this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            break;
                        case 2:
                            strCurveName[0] = "位移/位移"; this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            break;
                        case 3:
                            strCurveName[0] = "位移/应变"; this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            break;
                        default:
                            strCurveName[0] = ""; this.zedGraphControl.GraphPane.XAxis.IsVisible = false;
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1:
                            strCurveName[1] = "位移/时间"; this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            break;
                        case 2:
                            strCurveName[1] = "位移/位移"; this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            break;
                        case 3:
                            strCurveName[1] = "位移/应变"; this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            break;
                        default:
                            strCurveName[1] = ""; this.zedGraphControl.GraphPane.X2Axis.IsVisible = false;
                            break;
                    }
                    break;
                default: this.zedGraphControl.GraphPane.YAxisList[1].IsVisible = false;
                    strCurveName[0] = "";
                    strCurveName[1] = "";

                    break;
            }
            #endregion

            #region Y2-X1 / Y2-X2 第三、四条曲线
            switch (this.tscbY2.SelectedIndex)
            {
                case 1: this.zedGraphControl.GraphPane.YAxis.IsVisible = true;
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "力/时间";
                            break;
                        case 2: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "力/位移";
                            break;
                        case 3: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "力/应变";
                            break;
                        default: this.zedGraphControl.GraphPane.XAxis.IsVisible = false;
                            strCurveName[2] = "";
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "力/时间";
                            break;
                        case 2: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "力/位移";
                            break;
                        case 3: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "力/应变";
                            break;
                        default: this.zedGraphControl.GraphPane.X2Axis.IsVisible = false;
                            strCurveName[3] = "";
                            break;
                    }

                    break;
                case 2: this.zedGraphControl.GraphPane.YAxis.IsVisible = true;
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "应力/时间";
                            break;
                        case 2: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "应力/位移";
                            break;
                        case 3: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "应力/应变";
                            break;
                        default: this.zedGraphControl.GraphPane.XAxis.IsVisible = false;
                            strCurveName[2] = "";
                            break;
                    } switch (this.tscbX2.SelectedIndex)
                    {
                        case 1: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "应力/时间";
                            break;
                        case 2: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "应力/位移";
                            break;
                        case 3: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "应力/应变";
                            break;
                        default: this.zedGraphControl.GraphPane.X2Axis.IsVisible = false;
                            strCurveName[3] = "";
                            break;
                    }

                    break;
                case 3: this.zedGraphControl.GraphPane.YAxis.IsVisible = true;
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "变形/时间";
                            break;
                        case 2: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "变形/位移";
                            break;
                        case 3: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "变形/应变";
                            break;
                        default: this.zedGraphControl.GraphPane.XAxis.IsVisible = false;
                            strCurveName[2] = "";
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "变形/时间";
                            break;
                        case 2: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "变形/位移";
                            break;
                        case 3: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "变形/应变";
                            break;
                        default: this.zedGraphControl.GraphPane.X2Axis.IsVisible = false;
                            strCurveName[3] = "";
                            break;
                    }
                    break;
                case 4: this.zedGraphControl.GraphPane.YAxis.IsVisible = true;
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "位移/时间";
                            break;
                        case 2: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "位移/位移";
                            break;
                        case 3: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "位移/应变";
                            break;
                        default: this.zedGraphControl.GraphPane.XAxis.IsVisible = false;
                            strCurveName[2] = "";
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "位移/时间";
                            break;
                        case 2: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "位移/位移";
                            break;
                        case 3: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "位移/应变";
                            break;
                        default: this.zedGraphControl.GraphPane.X2Axis.IsVisible = false;
                            strCurveName[3] = "";
                            break;
                    }
                    break;
                default: this.zedGraphControl.GraphPane.YAxis.IsVisible = false;
                    strCurveName[2] = "";
                    strCurveName[3] = "";
                    break;
            }
            #endregion

            #region Y3-X1 / Y3-X2 第五、六条曲线
            switch (this.tscbY3.SelectedIndex)
            {
                case 1: this.zedGraphControl.GraphPane.Y2Axis.IsVisible = true;
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[4] = "力/时间";
                            break;
                        case 2: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[4] = "力/位移";
                            break;
                        case 3: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[4] = "力/应变";
                            break;
                        default: this.zedGraphControl.GraphPane.XAxis.IsVisible = false;
                            strCurveName[4] = "";
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[5] = "力/时间";
                            break;
                        case 2: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[5] = "力/位移";
                            break;
                        case 3: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[5] = "力/应变";
                            break;
                        default: this.zedGraphControl.GraphPane.X2Axis.IsVisible = false;
                            strCurveName[5] = "";
                            break;
                    }

                    break;
                case 2: this.zedGraphControl.GraphPane.Y2Axis.IsVisible = true;
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[4] = "应力/时间";
                            break;
                        case 2: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[4] = "应力/位移";
                            break;
                        case 3: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[4] = "应力/应变";
                            break;
                        default: this.zedGraphControl.GraphPane.XAxis.IsVisible = false;
                            strCurveName[4] = "";
                            break;
                    } switch (this.tscbX2.SelectedIndex)
                    {
                        case 1: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[5] = "应力/时间";
                            break;
                        case 2: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[5] = "应力/位移";
                            break;
                        case 3: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[5] = "应力/应变";
                            break;
                        default: this.zedGraphControl.GraphPane.X2Axis.IsVisible = false;
                            strCurveName[5] = "";
                            break;
                    }

                    break;
                case 3: this.zedGraphControl.GraphPane.Y2Axis.IsVisible = true;
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[4] = "变形/时间";
                            break;
                        case 2: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[4] = "变形/位移";
                            break;
                        case 3: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[4] = "变形/应变";
                            break;
                        default: this.zedGraphControl.GraphPane.XAxis.IsVisible = false;
                            strCurveName[4] = "";
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[5] = "变形/时间";
                            break;
                        case 2: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[5] = "变形/位移";
                            break;
                        case 3: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[5] = "变形/应变";
                            break;
                        default: this.zedGraphControl.GraphPane.X2Axis.IsVisible = false;
                            strCurveName[5] = "";
                            break;
                    }
                    break;
                case 4: this.zedGraphControl.GraphPane.Y2Axis.IsVisible = true;
                    switch (this.tscbX1.SelectedIndex)
                    {
                        case 1: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[4] = "位移/时间";
                            break;
                        case 2: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[4] = "位移/位移";
                            break;
                        case 3: this.zedGraphControl.GraphPane.XAxis.IsVisible = true;
                            strCurveName[4] = "位移/应变";
                            break;
                        default: this.zedGraphControl.GraphPane.XAxis.IsVisible = false;
                            strCurveName[4] = "";
                            break;
                    }
                    switch (this.tscbX2.SelectedIndex)
                    {
                        case 1: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[5] = "位移/时间";
                            break;
                        case 2: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[5] = "位移/位移";
                            break;
                        case 3: this.zedGraphControl.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[5] = "位移/应变";
                            break;
                        default: this.zedGraphControl.GraphPane.X2Axis.IsVisible = false;
                            strCurveName[5] = "";
                            break;
                    }
                    break;
                default: this.zedGraphControl.GraphPane.Y2Axis.IsVisible = false;
                    strCurveName[4] = "";
                    strCurveName[5] = "";
                    break;
            }
            #endregion

            for (int i = 0; i < 6; i++)
            {
                zedGraphControl.GraphPane.CurveList[i].Label.Text = strCurveName[i].ToString();
            }
            zedGraphControl.Refresh();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.dataGridView.Rows[e.RowIndex].Selected = true;
                selTestSampleNo = this.dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void tscbY1_Click(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)//判断是否窗体最小化
            {
                this.WindowState = FormWindowState.Normal;
            }

            this.Activate();//激活窗体
            this.notifyIcon1.Visible = false;//在任务栏区域中不显示图标
            this.ShowInTaskbar = true;　//窗体在任务栏区域中可见  

        }

        private void tsbtnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;//最小化时隐藏窗体
            //this.Visible = false; //是否显示该控件
            this.notifyIcon1.Visible = true;     //图标在任务栏区域中可见 
            this.notifyIcon1.ShowBalloonTip(300, "提示", "HS-Test", ToolTipIcon.Info); //设置气球状工具提示显示的时间为10秒
            this.ShowInTaskbar = true;//windows任务栏中不显示窗体  
        }
    }
}
