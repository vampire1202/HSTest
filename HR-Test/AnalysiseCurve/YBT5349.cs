using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace HR_Test.AnalysiseCurve
{
    public partial class YBT5349 : Form
    {

        ZedGraph.GraphPane _ResultPanel;

        //读取曲线的数据
        RollingPointPairList _RPPList_Read;
        //RollingPointPairList _RPPList0;

        BLL.Bend bllBend;
        Model.Bend modelBend;

        //存储采集数据
        private List<gdata> _List_Data;
        //曲线名数组
        private string[] strCurveName = new string[6];
        //指定放大区域的Panel
        PictureBox _palZoom;
        PickBox pb;
        RectTracker rect;
        public YBT5349()
        {
            InitializeComponent();
            // initChart(this.zedGraphControl);  
        }

        private double m_SR;
        public double M_SR
        {
            get { return m_SR; }
            set { m_SR = value; }
        }

        //分辨率常量
        private const uint m_Resolution = 120000;

        private string _testType = string.Empty;
        public string _TestType
        {
            get { return this._testType; }
            set { this._testType = value; }
        }

        private string _lineColor = string.Empty;
        public string _LineColor
        {
            get { return this._lineColor; }
            set { this._lineColor = value; }
        }


        private string _testSampleNo = string.Empty;
        public string _TestSampleNo
        {
            get { return this._testSampleNo; }
            set { this._testSampleNo = value; }
        }

        //参与计算的变量
        double m_Fm = 0;//最大力值        
        int m_FmIndex;//最大值的索引值
        double m_Fn;//实时力值 
        double m_F = 0;//实时采集力值 

        int m_FrIndex = 0;//Fp02索引值
        double m_Lm;//最大值时的延伸
     
        double m_FR05;
        double m_FR01;
        double m_LR05;
        double m_LR01;
        double m_Ls;
        double m_l;
        double m_Le;
        double m_n;
        double m_S0;
        double m_Eb;
        double m_Fpb;
        double m_σpb;
        double m_Epb; 
        double m_Y;
        
        //手动求Fp02的变量
        int m_firstPIndex;
        int m_secondPIndex;
        int m_midPIndex;

        //手动求上下屈服标志
        bool m_FlagHandFRL;
        bool m_FlagHandFRH;
        double m_HandFeL;
        double m_HandFeH;
        double m_HandReL;
        double m_HandReH;

        //力-位移 求FP02标志
        bool m_FlagFp02L;
        //力-变形求Fp02标志
        bool m_FlagFp02E;

        Symbol m_zedGraphSyb;

        /// <summary>
        /// 曲线名称
        /// </summary>
        private string _curveName;
        public string _CurveName
        {
            get { return this._curveName; }
            set { this._curveName = value; }
        }

        /// <summary>
        /// 是否选择了计算上或下屈服
        /// </summary>
        private bool _isSelReH;
        public bool _IsSelReH
        {
            get { return this._isSelReH; }
            set { this._isSelReH = value; }
        }

        /// <summary>
        /// 是否选择了计算上或下屈服
        /// </summary>
        private bool _isSelReL;
        public bool _IsSelReL
        {
            get { return this._isSelReL; }
            set { this._isSelReL = value; }
        }


        private double GetSR(ushort SensorScale)
        {
            uint m_ScaleValue = GetScale(SensorScale);
            //负荷控制速度 
            double _SR = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
            return _SR;
        }


        public uint GetScale(UInt16 Scale)
        {
            uint m_Scale = 0;
            //量程指数
            UInt16 m_E = (UInt16)(Scale & 0x000f);

            //量程基数 
            UInt16 SigValue = (UInt16)(Scale >> 8);

            m_Scale = (uint)SigValue * (uint)Math.Pow(10.0, m_E);

            return m_Scale;
        }

        private void readCurveName()
        {
            //若曲线存在
            if (File.Exists(_curveName))
            {
                //先解密文件
                string outputFile = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HRData.txt";
                string sSecretKey;

                string[] key = RWconfig.GetAppSettings("code").ToString().Split('-');
                byte[] keyee = new byte[8];
                //转换为 key byte数组
                for (int j = 0; j < key.Length; j++)
                {
                    keyee[j] = Byte.Parse(key[j], System.Globalization.NumberStyles.HexNumber);
                }
                sSecretKey = ASCIIEncoding.ASCII.GetString(keyee);
                GCHandle gch = GCHandle.Alloc(sSecretKey, GCHandleType.Pinned);
                Safe.DecryptFile(_curveName, outputFile, sSecretKey);
                Safe.ZeroMemory(gch.AddrOfPinnedObject(), sSecretKey.Length * 2);
                gch.Free();

                //读取曲线
                using (StreamReader srLine = new StreamReader(outputFile))
                {
                    string[] testSampleInfo1 = srLine.ReadLine().Split(',');
                    string[] testSampleInfo2 = srLine.ReadLine().Split(',');
                    string[] testSampleInfo3 = srLine.ReadLine().Split(',');
                    this.zedGraphControl.PrintDocument.DocumentName = testSampleInfo2[0].ToString() + " 试验曲线";
                    //this.zedGraphControl.GraphPane.Title.IsVisible = true;
                    String line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
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
                        _List_Data.Add(_gdata);
                    }
                    srLine.Close();
                    srLine.Dispose();
                    //showCurve(_List_Data);
                    InitCurve(this.zedGraphControl, this.tslblSampleNo.Text, this._testType, this._lineColor);
                }
            }
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

        private void tsbtnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void gbtnExit_Click(object sender, EventArgs e)
        {

        }


        private void frmAnalysiseCurve_Load(object sender, EventArgs e)
        {
            //m_SR = GetSR((ushort)_fmMain.m_SensorArray[_fmMain.m_LSensorArray[0].SensorIndex].scale);

            _ResultPanel = this.zedGraphControl.GraphPane;
            initResultCurve(this.zedGraphControl);
            this.zedGraphControl.Invalidated += new InvalidateEventHandler(zedGraphControl_Invalidated);
            this.cmbYr.SelectedIndex = int.Parse(RWconfig.GetAppSettings("ShowY"));
            this.cmbXr.SelectedIndex = int.Parse(RWconfig.GetAppSettings("ShowX"));
            this.zedGraphControl.RestoreScale(this.zedGraphControl.GraphPane);

            m_zedGraphSyb = new Symbol();
            m_zedGraphSyb.IsAntiAlias = true;
            m_zedGraphSyb.Type = SymbolType.Circle;
            m_zedGraphSyb.Size = 4;
            m_zedGraphSyb.Border.Width = 2;
            m_zedGraphSyb.Fill.Color = Color.DarkRed;
            m_zedGraphSyb.IsVisible = true;

            if (this._List_Data != null)
            {
                CalcData(this._List_Data, _isSelReH, _isSelReL);
            }
            else
            {
                MessageBox.Show(this, "曲线数据不存在!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                this.Dispose();
            }

            //将计算后的值呈现在Label上
            bllBend = new HR_Test.BLL.Bend();
            modelBend = bllBend.GetModel(_TestSampleNo);
            m_Ls=(double)modelBend.Ls;
            m_l=(double)modelBend.l_l;
            m_Le = (double)modelBend.Le;
            m_n = (double)modelBend.n ;
            switch (modelBend.testType)
            {
                case "三点弯曲":
                    m_S0 = (float)(4 * modelBend.W / modelBend.Ls);
                    break;
                case "四点弯曲":
                    m_S0 = (float)(2 * modelBend.W / modelBend.l_l);
                    break;
            } 
            m_Eb = (double)modelBend.Eb;
            m_Epb = (double)modelBend.εpb;
            //Y的值
            switch (modelBend.sampleType)
            {
                case "圆柱形":
                    m_Y = (float)modelBend.d / 2;
                    break;
                case "矩形":
                    m_Y = (float)modelBend.h / 2;
                    break;
            }

            if (modelBend != null)
            {
                if (this.flowLayoutPanel1.Controls.Find("Fbb(kN)", false).Length > 0)
                {
                    UC.Result lblFm = (UC.Result)this.flowLayoutPanel1.Controls.Find("Fbb(kN)", false)[0];
                    lblFm.Text = (modelBend.Fbb / 1000.0).ToString();
                }
                if (this.flowLayoutPanel1.Controls.Find("σbb(MPa)", false).Length > 0)
                {
                    UC.Result lblRm = (UC.Result)this.flowLayoutPanel1.Controls.Find("σbb(MPa)", false)[0];
                    lblRm.Text = modelBend.σbb.ToString();// (m_Fm / m_S0).ToString("G5");
                }
                if (this.flowLayoutPanel1.Controls.Find("Fpb(kN)", false).Length > 0)
                {
                    UC.Result lblFeL = (UC.Result)this.flowLayoutPanel1.Controls.Find("Fpb(kN)", false)[0];
                    lblFeL.Text = (modelBend.Fpb / 1000.0).ToString();// (_List_Data[m_FRLIndex].F1 / 1000.0).ToString("G5");
                }

                if (this.flowLayoutPanel1.Controls.Find("σpb(MPa)", false).Length > 0)
                {
                    UC.Result lblFeH = (UC.Result)this.flowLayoutPanel1.Controls.Find("σpb(MPa)", false)[0];
                    lblFeH.Text = (modelBend.σpb).ToString(); //(_List_Data[m_FRHIndex].F1 / 1000.0).ToString("G5");
                }
                if (this.flowLayoutPanel1.Controls.Find("Eb(MPa)", false).Length > 0)
                {
                    UC.Result lblReL = (UC.Result)this.flowLayoutPanel1.Controls.Find("Eb(MPa)", false)[0];
                    lblReL.Text = modelBend.Eb.ToString();// (_List_Data[m_FRLIndex].F1 / m_S0).ToString("G5");
                }                
            }

        }

        void zedGraphControl_Invalidated(object sender, InvalidateEventArgs e)
        {
            if (this.zedGraphControl.GraphPane.XAxis != null)
            {
                Scale sScale = _ResultPanel.XAxis.Scale;

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

                _ResultPanel.XAxis.Scale.MajorStep = (_ResultPanel.XAxis.Scale.Max - _ResultPanel.XAxis.Scale.Min) / 5;
                _ResultPanel.XAxis.Scale.MinorStep = _ResultPanel.XAxis.Scale.MajorStep / 5;
            }

            if (this.zedGraphControl.GraphPane.YAxis != null)
            {
                Scale sScale = _ResultPanel.YAxis.Scale;
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

                _ResultPanel.YAxis.Scale.MajorStep = (_ResultPanel.YAxis.Scale.Max - _ResultPanel.YAxis.Scale.Min) / 5;
                _ResultPanel.YAxis.Scale.MinorStep = _ResultPanel.YAxis.Scale.MajorStep / 5;
            }
        } 
   
        //保存修改结果
        private void gBtnSaveResult_Click(object sender, EventArgs e)
        {
            //{ "-", "-", "fbb", "fn", "fn-1","frb","y","Fo","Fpb","Frb","Fbb","Fn","Fn-1","Z","S","W","I","Eb","σpb","σrb","σbb","U" };
            BLL.Bend bllBs = new HR_Test.BLL.Bend();
            Model.Bend modelBs = bllBs.GetModel(this.tslblSampleNo.Text);
            foreach (Control c in this.flowLayoutPanel1.Controls)
            {
                UC.Result ucR = (UC.Result)c;
                switch (ucR.Name)
                {
                    case "fbb(mm)":                               
                        modelBs.f_bb = double.Parse((ucR.txtFiledContent).Text);
                        break;
                    case "fn(mm)":
                        modelBs.f_n = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "fn-1(mm)":
                        modelBs.f_n1 = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "frb(mm)":
                        modelBs.f_rb = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "y(mm)":
                        modelBs.y = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "Fo(N)":
                        modelBs.Fo = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "Fpb(kN)":
                        modelBs.Fpb = double.Parse(ucR.txtFiledContent.Text) /1000.0;
                        break;
                    case "Frb(kN)":
                        modelBs.Frb = double.Parse(ucR.txtFiledContent.Text) /1000.0;
                        break;
                    case "Fbb(kN)":
                        modelBs.Fbb = double.Parse(ucR.txtFiledContent.Text) / 1000.0;
                        break;
                    case "Fn(kN)":
                        modelBs.Fn = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "Fn-1(kN)":
                        modelBs.Fn1 = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "Z":
                        modelBs.Z = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "S":
                        modelBs.S = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "W":
                        modelBs.W = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "I":
                        modelBs.I = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "Eb(MPa)":
                        modelBs.Eb = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "σpb(MPa)":
                        modelBs.σpb = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "σrb(MPa)":
                        modelBs.σrb = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "σbb(MPa)":
                        modelBs.σbb = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "U(J)":
                        modelBs.U = double.Parse(ucR.txtFiledContent.Text);
                        break;
                }
            }

            if (bllBs.Update(modelBs))
            {
                MessageBox.Show(this, "更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
        }

        private void cmbYr_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.zedGraphControl.GraphPane.GraphObjList.Clear();
            initResultCurve(this.zedGraphControl);

            InitCurve(this.zedGraphControl, this.tslblSampleNo.Text, "YBT5349-2006", _lineColor);
            
            switch (cmbYr.SelectedIndex)
            {
                case 0:
                    this._ResultPanel.YAxis.Title.Text = "Y1";
                    _ResultPanel.YAxis.Scale.LabelGap = 0;
                    break;
                case 1:
                    _ResultPanel.YAxis.Title.Text = "负荷,N";
                    _ResultPanel.YAxis.Scale.LabelGap = 0;
                    break;
                case 2:
                    _ResultPanel.YAxis.Title.Text = "应力,MPa";
                    _ResultPanel.YAxis.Scale.LabelGap = 0;
                    break;
                case 3:
                    _ResultPanel.YAxis.Title.Text = "变形,mm";
                    _ResultPanel.YAxis.Scale.LabelGap = 0;
                    break;
                case 4:
                    _ResultPanel.YAxis.Title.Text = "位移,mm";
                    _ResultPanel.YAxis.Scale.LabelGap = 0;
                    break;
            }
            switch (cmbXr.SelectedIndex)
            {
                case 0:
                    _ResultPanel.XAxis.Title.Text = "X1";
                    break;
                case 1:
                    _ResultPanel.XAxis.Title.Text = "时间,s";
                    //_ResultPanel.XAxis.Scale.MaxAuto = false;
                    //_ResultPanel.XAxis.Scale.Max = (int)(_List_Data[_List_Data.Count - 1].Ts / 10) * 10 + 10;
                    break;
                case 2:
                    _ResultPanel.XAxis.Title.Text = "位移,mm";
                    _ResultPanel.XAxis.Scale.LabelGap = 0;
                    break;
                case 3:
                    _ResultPanel.XAxis.Title.Text = "应变,%";
                    //_ResultPanel.XAxis.Scale.MaxAuto = false;
                    //_ResultPanel.XAxis.Scale.Max = (int)(_List_Data[_List_Data.Count - 1].YB1) +1;
                    break;
                case 4:
                    _ResultPanel.XAxis.Title.Text = "变形,mm";
                    break;
                case 5:
                    _ResultPanel.XAxis.Title.Text = "应力,MPa";

                    break;
                default:
                    _ResultPanel.XAxis.Title.Text = "X1";
                    break;
            }

            RWconfig.SetAppSettings("ShowY", this.cmbYr.SelectedIndex.ToString());
            RestoreZScale();
            //RestoreZScale();
        }

        private void cmbXr_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.zedGraphControl.GraphPane.GraphObjList.Clear();
            initResultCurve(this.zedGraphControl);
            InitCurve(this.zedGraphControl, this.tslblSampleNo.Text, "YBT5349-2006", _lineColor);
            switch (cmbXr.SelectedIndex)
            {
                case 0:
                    _ResultPanel.XAxis.Title.Text = "X1";
                    break;
                case 1:
                    _ResultPanel.XAxis.Title.Text = "时间,s";
                    //_ResultPanel.XAxis.Scale.MaxAuto = false;
                    //_ResultPanel.XAxis.Scale.Max = (int)(_List_Data[_List_Data.Count - 1].Ts / 10) * 10 + 10;
                    break;
                case 2:
                    _ResultPanel.XAxis.Title.Text = "位移,mm";
                    _ResultPanel.XAxis.Scale.LabelGap = 0;
                    break;
                case 3:
                    _ResultPanel.XAxis.Title.Text = "应变,%";
                    //_ResultPanel.XAxis.Scale.MaxAuto = false;
                    //_ResultPanel.XAxis.Scale.Max = (int)(_List_Data[_List_Data.Count - 1].YB1) +1;
                    break;
                case 4:
                    _ResultPanel.XAxis.Title.Text = "变形,mm";
                    break;
                case 5:
                    _ResultPanel.XAxis.Title.Text = "应力,MPa";

                    break;
                default:
                    _ResultPanel.XAxis.Title.Text = "X1";
                    break;
            }
            switch (cmbYr.SelectedIndex)
            {
                case 0:
                    this._ResultPanel.YAxis.Title.Text = "Y1";
                    _ResultPanel.YAxis.Scale.LabelGap = 0;
                    break;
                case 1:
                    _ResultPanel.YAxis.Title.Text = "负荷,N";
                    _ResultPanel.YAxis.Scale.LabelGap = 0;
                    break;
                case 2:
                    _ResultPanel.YAxis.Title.Text = "应力,MPa";
                    _ResultPanel.YAxis.Scale.LabelGap = 0;
                    break;
                case 3:
                    _ResultPanel.YAxis.Title.Text = "变形,mm";
                    _ResultPanel.YAxis.Scale.LabelGap = 0;
                    break;
                case 4:
                    _ResultPanel.YAxis.Title.Text = "位移,mm";
                    _ResultPanel.YAxis.Scale.LabelGap = 0;
                    break;
            }
            RWconfig.SetAppSettings("ShowX", this.cmbXr.SelectedIndex.ToString());

            RestoreZScale();
            //RestoreZScale();
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

            //_RPPList0 = new RollingPointPairList(50000);

            //ZedGraph  
            //zedGraphControl1.IsAntiAlias = true; 
            Legend l = zgControl.GraphPane.Legend;
            l.IsShowLegendSymbols = true;
            l.Gap = 0;
            l.Position = LegendPos.InsideTopLeft;
            l.FontSpec.Size = 16.0f;
            l.IsVisible = false;

            // Set the titles and axis labels
            _ResultPanel = zgControl.GraphPane;
            _ResultPanel.Margin.All = 8;
            _ResultPanel.Margin.Top = 15;
            _ResultPanel.Title.Text = "";
            _ResultPanel.Title.IsVisible = false;
            _ResultPanel.IsFontsScaled = false;

            zgControl.IsZoomOnMouseCenter = false;
            zgControl.IsShowContextMenu = false;
            zgControl.IsShowPointValues = true;
            zgControl.MouseClick += new MouseEventHandler(zgControl_MouseClick);

            //XAxis
            //最后的显示值隐藏
            _ResultPanel.XAxis.Scale.FontSpec.Size = 16.0f;
            _ResultPanel.XAxis.Title.FontSpec.Size = 16.0f;
            _ResultPanel.XAxis.Scale.FontSpec.Family = "宋体";
            _ResultPanel.XAxis.Scale.FontSpec.IsBold = true;
            _ResultPanel.XAxis.Title.FontSpec.FontColor = Color.FromArgb(34, 67, 108);
            _ResultPanel.XAxis.Scale.FontSpec.FontColor = Color.FromArgb(34, 67, 108);
            _ResultPanel.XAxis.Title.Text = "X";
            _ResultPanel.XAxis.Title.Gap = -0.5f;
            _ResultPanel.XAxis.Scale.AlignH = AlignH.Center;
            _ResultPanel.XAxis.Scale.LabelGap = 0;
            _ResultPanel.XAxis.Scale.Format = "0.0";
            _ResultPanel.XAxis.Scale.MinGrace = 0.0;
            _ResultPanel.XAxis.Scale.MaxGrace = 0.05;
            _ResultPanel.XAxis.Scale.Min = 0;
            _ResultPanel.XAxis.Scale.MinAuto = false;
            _ResultPanel.XAxis.Scale.Max = 1;
            _ResultPanel.XAxis.Scale.MaxAuto = false;
            _ResultPanel.XAxis.MajorGrid.IsVisible = true;
            _ResultPanel.XAxis.MinorGrid.IsVisible = false;
            _ResultPanel.XAxis.MinorGrid.Color = Color.Silver;
            _ResultPanel.XAxis.MinorGrid.DashOff = 1;
            _ResultPanel.XAxis.MinorGrid.DashOn = 1;


            _ResultPanel.YAxis.Title.Text = "Y";
            _ResultPanel.YAxis.Title.Gap = -0.5f;
            _ResultPanel.YAxis.Scale.FontSpec.FontColor = Color.FromArgb(34, 67, 108);
            _ResultPanel.YAxis.Title.FontSpec.FontColor = Color.FromArgb(34, 67, 108);
            _ResultPanel.YAxis.Scale.FontSpec.Size = 16.0f;
            _ResultPanel.YAxis.Title.FontSpec.Size = 16.0f;
            _ResultPanel.YAxis.Scale.FontSpec.Family = "宋体";
            _ResultPanel.YAxis.Scale.FontSpec.IsBold = true;
            _ResultPanel.YAxis.Scale.Format = "0.0";
            _ResultPanel.YAxis.Scale.MinGrace = 0.0;
            _ResultPanel.YAxis.Scale.MaxGrace = 0.05;
            _ResultPanel.YAxis.Scale.LabelGap = 0;
            // Align the Y2 axis labels so they are flush to the axis 
            _ResultPanel.YAxis.Scale.AlignH = AlignH.Center;
            _ResultPanel.YAxis.Scale.Min = 0;
            _ResultPanel.YAxis.Scale.MinAuto = false;
            _ResultPanel.YAxis.Scale.Max = 1;
            _ResultPanel.YAxis.Scale.MaxAuto = false;
            _ResultPanel.YAxis.MajorGrid.IsVisible = true;
            _ResultPanel.YAxis.MinorGrid.IsVisible = false;
            _ResultPanel.YAxis.MinorGrid.Color = Color.Silver;
            _ResultPanel.YAxis.MinorGrid.DashOff = 1;
            _ResultPanel.YAxis.MinorGrid.DashOn = 1;

            zgControl.AxisChange();
            zgControl.Invalidate();
        }

        void zgControl_MouseClick(object sender, MouseEventArgs e)
        {
            _palZoom_MouseClick(sender, e);
        }

        //初始化曲线控件上的曲线数量及名称
        private void InitCurve(ZedGraph.ZedGraphControl zgControl, string curveName, string path, string lineColor)
        {
            if (curveName != null)
            {
                //_RPPList_Read = new RollingPointPairList(100000);

                _ResultPanel = zgControl.GraphPane;
                zgControl.GraphPane.CurveList.RemoveRange(0, zgControl.GraphPane.CurveList.Count);

                if (_List_Data != null)
                    _List_Data = null;

                //foreach (CurveItem ci in zgControl.GraphPane.CurveList)
                //{
                //    ci.Clear();
                //}

                LineItem CurveList = _ResultPanel.AddCurve(curveName, _RPPList_Read, Color.FromName(lineColor), SymbolType.None);//Y1-X1 
                CurveList.Line.IsAntiAlias = true;
                readCurveName(curveName, path);
            }

            //MessageBox.Show(zgControl.GraphPane.CurveList.Count.ToString());
            //初始化曲线名称即 试样编号的名称 
            zgControl.AxisChange();
            zgControl.RestoreScale(this._ResultPanel);
        }

        //读取曲线文件
        private void readCurveName(string curveName, string path)
        {
            //若曲线存在
            string curvePath = @"E:\衡新试验数据\" + "Curve\\" + path + "\\" + curveName + ".txt";
            if (File.Exists(curvePath))
            {
                //读取曲线 
                _List_Data = new List<gdata>();
                //建立曲线点 
                //_RPPList_Read = new RollingPointPairList(100000);
                using (StreamReader srLine = new StreamReader(curvePath))
                {
                    string[] testSampleInfo1 = srLine.ReadLine().Split(',');
                    string[] testSampleInfo2 = srLine.ReadLine().Split(',');
                    //添加试验标志
                    //AddText(fs, "testType,testSampleNo,S0,Ls,l,Le,Ep,Er,n");
                    //if (testSampleInfo2[0] == "bend")
                    //{
                    //    m_S0 = double.Parse(testSampleInfo2[2]);
                    //    m_Ls = double.Parse(testSampleInfo2[3]);
                    //    m_l = double.Parse(testSampleInfo2[4]);
                    //    m_Le = double.Parse(testSampleInfo2[5]);
                    //    m_Ep = double.Parse(testSampleInfo2[6]);
                    //    m_n = double.Parse(testSampleInfo2[8]);
                    //}
                    if (srLine.ReadLine() != null)
                    { string[] testSampleInfo3 = srLine.ReadLine().Split(','); }

                    //this.zedGraphControl.PrintDocument.DocumentName = testSampleInfo2[0].ToString() + " 试验曲线";
                    //this.zedGraphControl.GraphPane.Title.IsVisible = true;
                    String line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
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
                        _List_Data.Add(_gdata);
                    }
                    srLine.Close();
                    srLine.Dispose();
                    //显示曲线
                    showCurve(_List_Data, this.zedGraphControl);
                }
            }
        }

        private void CalcData(List<gdata> listGData, bool isSelReH, bool isSelReL)
        {
            m_Fn = float.MinValue;
            m_F = float.MinValue;
            m_Fm = float.MinValue;
            for (Int32 i = 1; i < listGData.Count; i++)
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
                //存储前一点的值
                m_Fn = m_F;
                //实时得值
                m_F = F1value;
                //存储最大值            
                if (m_F > m_Fm)
                {
                    m_Fm = m_F;
                    m_FmIndex = i;
                    //最大值的延伸，此处用位移表示
                    m_Lm = D1value;
                }
            }
        }

        //显示一条曲线
        private void showCurve(List<gdata> listGData, ZedGraph.ZedGraphControl zgControl)
        {
            LineItem LineItem0 = zgControl.GraphPane.CurveList[0] as LineItem;
            LineItem0.Line.IsAntiAlias = true;
            if (LineItem0 == null)
                return;

            //第二步:在CurveItem中访问PointPairList(或者其它的IPointList)，根据自己的需要增加新数据或修改已存在的数据
            IPointListEdit LineItemListEdit_0 = LineItem0.Points as IPointListEdit;
            if (LineItemListEdit_0 == null)
                return;

            for (Int32 i = 1; i < listGData.Count-2; i++)
            {
                //采集数据
                //时间
                double time = listGData[i].Ts;
                //力
                double F1value = listGData[i].F1;
                //应力
                double YL1Value = listGData[i].YL1;
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
                                //_RPPList_Read.Add(time, F1value);
                                break;
                            case 2:
                                //strCurveName[0] = "力/位移";
                                LineItemListEdit_0.Add(D1value, F1value);
                                //_RPPList_Read.Add(D1value, F1value);
                                break;
                            case 3:
                                //strCurveName[0] = "力/应变";
                                LineItemListEdit_0.Add(YB1value, F1value);
                                //_RPPList_Read.Add(YB1value, F1value);
                                break;
                            case 4:
                                LineItemListEdit_0.Add(BX1value, F1value);
                                //_RPPList_Read.Add(BX1value, F1value);
                                break;
                            case 5:
                                LineItemListEdit_0.Add(YL1Value, F1value);
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
                                //strCurveName[0] = "应力/时间";
                                LineItemListEdit_0.Add(time, YL1Value);
                                //_RPPList_Read.Add(time, R1value);
                                break;
                            case 2:
                                //strCurveName[0] = "应力/位移";
                                LineItemListEdit_0.Add(D1value, YL1Value);
                                //_RPPList_Read.Add(D1value, R1value);
                                break;
                            case 3:
                                //strCurveName[0] = "应力/应变";
                                LineItemListEdit_0.Add(YB1value, YL1Value);
                                //_RPPList_Read.Add(YB1value, R1value);
                                break;
                            case 4:
                                LineItemListEdit_0.Add(BX1value, YL1Value);
                                //_RPPList_Read.Add(BX1value, R1value);
                                break;
                            case 5:
                                LineItemListEdit_0.Add(YL1Value, YL1Value);
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
                                //_RPPList_Read.Add(time, BX1value);
                                break;
                            case 2:
                                //strCurveName[0] = "变形/位移";
                                LineItemListEdit_0.Add(D1value, BX1value);
                                //_RPPList_Read.Add(D1value, BX1value);
                                break;
                            case 3:
                                //strCurveName[0] = "变形/应变";
                                LineItemListEdit_0.Add(YB1value, BX1value);
                                //_RPPList_Read.Add(YB1value, BX1value);
                                break;
                            case 4:
                                LineItemListEdit_0.Add(BX1value, BX1value);
                                //_RPPList_Read.Add(BX1value, BX1value);
                                break;
                            case 5:
                                LineItemListEdit_0.Add(YL1Value, BX1value);
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
                                //_RPPList_Read.Add(time, D1value);
                                break;
                            case 2:
                                //strCurveName[0] = "位移/位移";
                                LineItemListEdit_0.Add(D1value, D1value);
                                //_RPPList_Read.Add(D1value, D1value);
                                break;
                            case 3:
                                //strCurveName[0] = "位移/应变";
                                LineItemListEdit_0.Add(YB1value, D1value);
                                //_RPPList_Read.Add(YB1value, D1value);
                                break;
                            case 4:
                                LineItemListEdit_0.Add(BX1value, D1value);
                                //_RPPList_Read.Add(BX1value, D1value);
                                break;
                            case 5:
                                LineItemListEdit_0.Add(YL1Value, D1value);
                                //_RPPList_Read.Add(BX1value, D1value);
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

        private void tsbtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void tsbtnSelRect_Click(object sender, EventArgs e)
        {
            //this.zedGraphControl.Invalidated -= new InvalidateEventHandler(zedGraphControl_Invalidated);
            m_firstPIndex = m_secondPIndex = 0;
            //this.cmbYr.SelectedIndex = 1;
            //this.cmbXr.SelectedIndex = 2;  

            //if (!this.zedGraphControl.Controls.Contains(_palZoom))
            //{

            //    _palZoom = new  PictureBox();                
            //    _palZoom.Name = "pzoom";
            //    _palZoom.Tag = "pzoom"; 
            //    _palZoom.BackColor = Color.Transparent;
            //    _palZoom.BorderStyle = BorderStyle.FixedSingle;
            //    _palZoom.Width = this.zedGraphControl.Width / 5;
            //    _palZoom.Height = this.zedGraphControl.Height * 2 / 5;
            //    _palZoom.Left = this.zedGraphControl.Width / 5;
            //    _palZoom.Top = this.zedGraphControl.Height / 5;              
            //    this.zedGraphControl.Controls.Add(_palZoom);                 
            //    pb = new PickBox();
            //    pb.WireControl(_palZoom);
            //} 
            //移除已经求出的划线
            /* List<CurveItem> results = this.zedGraphControl.GraphPane.CurveList.FindAll(FindAllCurve);
             if (results.Count != 0)
             {
                 foreach (CurveItem ci in results)
                 {
                     this.zedGraphControl.GraphPane.CurveList.Remove(ci);
                 }
             }
             */
            //this.zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllCurveFp02);
            //this.zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFp02);

            RestoreZScale();
            //RestoreZScale();
        }

        private void RestoreZScale()
        {
            this.zedGraphControl.RestoreScale(this.zedGraphControl.GraphPane);
            m_FlagFp02L = false;
            m_FlagFp02E = false;
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

        // Explicit predicate delegate. 
        private static bool FindAllCurveFp02(CurveItem ci)
        {
            if (ci.Tag == null) return false;
            if (ci.Tag.ToString() == ("Fp02"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Explicit predicate delegate. 
        private static bool FindAllFp02(ZedGraph.GraphObj lo)
        {
            if (lo.Tag.ToString() == ("Fp02"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void AutoCalcTest()
        {
            int tempIndex = 0;
            //FrIndex初始值
            m_FrIndex = m_FmIndex;
            int count = 0;
            double a = 0;
            double k = 0;
            int fr05index = 0;
            int fr01index = 0;
            double ep02L0 = 0;

            //逐次逼近法 求取Fp02

            do
            {
                tempIndex = m_FrIndex;
                if (GetFp02IndexTest(_List_Data, tempIndex, out m_FrIndex, out a, out k, out fr05index, out fr01index, out ep02L0))
                {
                    count++;
                    //MessageBox.Show(count.ToString() + ":" + m_FrIndex.ToString() + "," + tempIndex.ToString());
                }

                if (count > 500)
                {
                    MessageBox.Show(this, "计算失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            while (m_FrIndex > tempIndex + 2 || m_FrIndex < tempIndex - 2);
        }

        private bool GetFp02IndexTest(List<gdata> List_Data, int _FRInIndex, out int _FROutIndex, out double _a, out double _k, out int Fr05Index, out int Fr01Index, out double ep02L0)
        {
            double Fr = List_Data[_FRInIndex].F1;
            int lCount = List_Data.Count;
            //查找Fr 0.5的点
            //求出Fr05和 Fr01点
            _FROutIndex = 0;
            _a = 0;
            _k = 0;
            Fr05Index = 0;
            Fr01Index = 0;
            ep02L0 = m_Ls * 10 * m_Epb;
            for (int m = 0; m < _FRInIndex; m++)
            {
                if (List_Data[m].F1 >= Fr * 0.6)
                {
                    m_FR05 = List_Data[m].F1;
                    m_LR05 = List_Data[m].D1;
                    Fr05Index = m;
                    break;
                }
            }

            for (int n = 0; n < _FRInIndex; n++)
            {
                if (_List_Data[n].F1 >= Fr * 0.3)
                {
                    m_FR01 = List_Data[n].F1;
                    m_LR01 = List_Data[n].D1;
                    Fr01Index = n;
                    break;
                }
            }

            //计算斜率,在 0.5 和 0.1之间取10点

            int[] kdot = Get0501k(Fr01Index, Fr05Index);
            double sumk = 0;

            for (int i = 0; i < kdot.Length - 1; i++)
            {
                double kone = (List_Data[kdot[i + 1]].F1 - List_Data[kdot[i]].F1) / (List_Data[kdot[i + 1]].D1 - List_Data[kdot[i]].D1);
                sumk += kone;
            }
            _k = sumk / (kdot.Length - 1);

            //计算偏移量
            _a = m_LR05 - (m_FR05 / _k);

            //计算出的Li值，注：100为 L0的 0。2%,此处假设为 50mm * 1000 * 0.2%
            //double Li = a + Fr / k + 100;

            for (int i = 0; i < lCount; i++)
            {
                double Lii = _a + ep02L0 + List_Data[i].F1 / _k;
                if (Lii <= List_Data[i].D1)
                {
                    _FROutIndex = i;
                    break;
                }
            }

            if (_FRInIndex != 0 && _a != 0 && _k != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //力-位移 曲线求Fp0.2
        /// <summary>
        /// 自动求 Fp02 已经注释
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        void _palZoom_MouseClick(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            //主要取得对应两点的 力值 index;
            //放大两点 

            if (e.Button == MouseButtons.Right)
            {
                if (!this.zedGraphControl.Controls.Contains(_palZoom))
                {
                    return;
                }
                double x1; double x2;
                double y1; double y2;
                double x3; double x4;
                double y3; double y4;

                PointF pLeftTop = new PointF();
                pLeftTop.X = this._palZoom.Left;
                pLeftTop.Y = this._palZoom.Top;

                PointF pLeftBottom = new PointF();
                pLeftBottom.X = this._palZoom.Left;
                pLeftBottom.Y = this._palZoom.Top + this._palZoom.Height;

                PointF pRightTop = new PointF();
                pRightTop.X = this._palZoom.Left + this._palZoom.Width;
                pRightTop.Y = this._palZoom.Top;

                PointF pRightBottom = new PointF();
                pRightBottom.X = this._palZoom.Left + this._palZoom.Width;
                pRightBottom.Y = this._palZoom.Top + this._palZoom.Height;

                this.zedGraphControl.GraphPane.ReverseTransform(pLeftTop, out x1, out y1);
                this.zedGraphControl.GraphPane.ReverseTransform(pRightTop, out x2, out y2);
                this.zedGraphControl.GraphPane.ReverseTransform(pLeftBottom, out x3, out y3);
                this.zedGraphControl.GraphPane.ReverseTransform(pRightBottom, out x4, out y4);

                //第一点index
                m_firstPIndex = GetIndex(_List_Data, y3);
                //第二点index
                m_secondPIndex = GetIndex(_List_Data, y1);

                //如果选择框的点不符合标准
                if (m_firstPIndex == 0 || m_secondPIndex == 0)
                {
                    MessageBox.Show(this, "请重新选择!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //MessageBox.Show(m_firstPIndex + "," + m_secondPIndex);

                this._palZoom.Dispose();

                //确定选框的坐标 
                pb.Remove();

                #region 力-位移求Fpb02
                
                if (m_FlagFp02L)
                {
                    //获取计算斜率点
                    int[] kindex = Get0501(m_firstPIndex, m_secondPIndex);

                    //计算斜率
                    double sumk = 0;
                    double k = 0;
                    for (int i = 0; i < kindex.Length - 1; i++)
                    {
                        double kone = (_List_Data[kindex[i + 1]].F1 - _List_Data[kindex[i]].F1) / (_List_Data[kindex[i + 1]].D1 - _List_Data[kindex[i]].D1);
                        sumk += kone;
                    }

                    k = sumk / (kindex.Length - 1);

                    //取中间点index
                    m_midPIndex = m_firstPIndex + (m_secondPIndex - m_firstPIndex) / 2;
                    //以中间点为准，画一条斜率k的直线，相交于 坐标轴x ，与 圆点的距离 就是偏离值 a;
                    //力 - 位移 曲线 点斜式方程： y=k(x-x1)+y1 
                    //this.zedGraphControl.RestoreScale(this.zedGraphControl.GraphPane);
                    //偏离值
                    double a = _List_Data[m_midPIndex].D1 - (_List_Data[m_midPIndex].F1 / k);
                    //x = (zedmax - y1 + kx1) / k; 
                    double[] _line_x = { _List_Data[m_midPIndex].D1 - (_List_Data[m_midPIndex].F1 / k), _List_Data[m_midPIndex].D1, ((_List_Data[m_FmIndex].F1 + 200) - _List_Data[m_midPIndex].F1 + k * _List_Data[m_midPIndex].D1) / k };
                    double[] _line_y = { 0, _List_Data[m_midPIndex].F1, (_List_Data[m_FmIndex].F1 + 200) };
                    LineItem li = this.zedGraphControl.GraphPane.AddCurve("cline_0501_1", _line_x, _line_y, Color.DarkGreen, SymbolType.None);
                    //li.Symbol = m_zedGraphSyb;
                    li.Line.IsAntiAlias = true;
                    li.Line.Width = 1f;
                    li.Tag = "Fp02";

                    //读取数据库的 Ep0.2
                    double ep02L0 = 0;
                    switch (modelBend.testType)
                    {
                        case "三点弯曲":
                            ep02L0 = m_n * m_Ls * m_Ls * m_Epb / (12 * m_Y);
                            break;
                        case "四点弯曲":
                            ep02L0 = m_n * (3 * m_Ls * m_Ls - 4 * m_l * m_l) / (24 * m_Y);
                            break;
                    }
                    if (ep02L0 == 0)
                        return;

                    //L = a + ep02L0 + _List_Data[i].F1 / k;
                    for (int i = m_firstPIndex; i < _List_Data.Count; i++)
                    {
                        double Lii = a + ep02L0 + _List_Data[i].F1 / k;
                        if (Lii <= _List_Data[i].D1)
                        {
                            m_FrIndex = i;
                            break;
                        }
                    }
                    //fp02的线
                    double[] _lineFp02_x = { a + ep02L0, _List_Data[m_FrIndex].D1, ((_List_Data[m_FmIndex].F1 + 200) - _List_Data[m_midPIndex].F1 + k * _List_Data[m_midPIndex].D1) / k + ep02L0 };
                    double[] _lineFp02_y = { 0, _List_Data[m_FrIndex].F1, (_List_Data[m_FmIndex].F1 + 200) };
                    LineItem liFp02 = this.zedGraphControl.GraphPane.AddCurve("cline_0501_2", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.None);
                    //liFp02.Symbol = m_zedGraphSyb;
                    liFp02.Line.IsAntiAlias = true;
                    liFp02.Line.Width = 1f;
                    liFp02.Tag = "Fp02";
                    //求弹性模量Eb
                    //计算弹性模量
                    float deltaLoad = _List_Data[m_secondPIndex].F1 - _List_Data[m_firstPIndex].F1;
                    float deltaD = _List_Data[m_secondPIndex].D1 - _List_Data[m_firstPIndex].D1;
                    float ls3 = (float)(modelBend.Ls * modelBend.Ls * modelBend.Ls);
                    float ls2 = (float)(modelBend.Ls * modelBend.Ls);
                    float l2 = (float)(modelBend.l_l * modelBend.l_l);
                    switch (modelBend.testType)
                    {
                        case "三点弯曲":
                            float eb1 = (float)(ls3 / (48.0f * modelBend.I)) * (deltaLoad / deltaD);
                            //---------------------------------------                                    
                            m_Eb = utils.GetRoundEbOfBend(eb1);
                            modelBend.Eb = m_Eb;
                            break;
                        case "四点弯曲":
                            float eb2 = (float)((modelBend.l_l * (3.0f * ls2 - 4 * l2)) * (deltaLoad / deltaD) / (48.0f * modelBend.I));
                            m_Eb = utils.GetRoundEbOfBend(eb2);
                            modelBend.Eb = m_Eb;
                            //结果修约
                            break;
                    }
                    //添加 值 的标注
                    m_Fpb = Math.Round(_List_Data[m_FrIndex].F1 / 1000, 3);
                    m_σpb = utils.GetRoundStressOfBend(_List_Data[m_FrIndex].YL1);
                    ZedGraph.TextObj t = new TextObj("Fpb = " + m_Fpb.ToString() + " kN\r\n"
                        + "σpb = " + m_σpb + " MPa\r\nEb = " + m_Eb + " Mpa"
                        , _List_Data[m_FrIndex].D1, _List_Data[m_FrIndex].F1);
                     
                    t.FontSpec.FontColor = Color.Navy;
                    t.Location.AlignH = AlignH.Left;
                    t.Location.AlignV = AlignV.Top;
                    t.FontSpec.IsBold = true;
                    t.FontSpec.Border.IsVisible = false;
                    
                    //t.ZOrder = ZOrder.E_BehindCurves;

                    t.Tag = "Fp02";
                    this.zedGraphControl.GraphPane.GraphObjList.Add(t);

                    //标注Fp02的点
                    double[] _Fp02x = { _List_Data[m_FrIndex].D1 };
                    double[] _Fp02y = { _List_Data[m_FrIndex].F1 };
                    LineItem pointFp02 = this.zedGraphControl.GraphPane.AddCurve("cline_fp02", _Fp02x, _Fp02y, Color.Blue, SymbolType.UserDefined);
                    pointFp02.Symbol = m_zedGraphSyb;
                    pointFp02.Line.IsAntiAlias = true;
                    pointFp02.Line.Width = 2f;
                    pointFp02.Tag = "Fp02";

                    ////标注第一个点 
                    //double[] _line_first_x = { _List_Data[m_firstPIndex].D1 };
                    //double[] _line_first_y = { _List_Data[m_firstPIndex].F1 };
                    //LineItem li_first = this.zedGraphControl.GraphPane.AddCurve("cline_first", _line_first_x, _line_first_y, Color.Navy, SymbolType.UserDefined);
                    //li_first.Symbol = m_zedGraphSyb;
                    //li_first.Line.IsAntiAlias = true;
                    //li_first.Line.Width = 2f;
                    //li_first.Tag = "Fp02";

                    ////标注第二个点
                    //double[] _line_second_x = { _List_Data[m_secondPIndex].D1 };
                    //double[] _line_second_y = { _List_Data[m_secondPIndex].F1 };
                    //LineItem li_second = this.zedGraphControl.GraphPane.AddCurve("cline_second", _line_second_x, _line_second_y, Color.Navy, SymbolType.UserDefined);
                    //li_second.Symbol = m_zedGraphSyb;
                    //li_second.Line.IsAntiAlias = true;
                    //li_second.Line.Width = 2f;
                    //li_second.Tag = "Fp02";

                    //标注中间的点
                    //double[] _line_mid_x = { _List_Data[m_midPIndex].D1 };
                    //double[] _line_mid_y = { _List_Data[m_midPIndex].F1 };
                    //LineItem line_mid = this.zedGraphControl.GraphPane.AddCurve("cline_mid", _line_mid_x, _line_mid_y, Color.Navy, SymbolType.UserDefined);
                    //line_mid.Symbol = m_zedGraphSyb;
                    //line_mid.Line.IsAntiAlias = true;
                    //line_mid.Line.Width = 2f;
                    //line_mid.Tag = "Fp02";

                    //将新值从Label上表现出来
                    //显示Fp02 控件名称 4
                    //UC.Result lblFp02 = (UC.Result)this.flowLayoutPanel1.Controls.Find("Rp", false)[0];
                    //lblFp02.Text = (_List_Data[m_FrIndex].F1 / m_S0).ToString("G5"); 
                }
#endregion

                #region 力 - 变形 求Fpb02
                
                if (m_FlagFp02E)
                {
                    //获取计算斜率点
                    int[] kindex = Get0501(m_firstPIndex, m_secondPIndex);

                    //计算斜率
                    double sumk = 0;
                    double k = 0;
                    for (int i = 0; i < kindex.Length - 1; i++)
                    {
                        double kone = (_List_Data[kindex[i + 1]].F1 - _List_Data[kindex[i]].F1) / (_List_Data[kindex[i + 1]].BX1 - _List_Data[kindex[i]].BX1);
                        sumk += kone;
                    }

                    k = sumk / (kindex.Length - 1);

                    //取中间点index
                    m_midPIndex = m_firstPIndex + (m_secondPIndex - m_firstPIndex) / 2;

                    //以中间点为准，画一条斜率k的直线，相交于 坐标轴x ，与 圆点的距离 就是偏离值 a;
                    //力 - 位移 曲线 点斜式方程： y=k(x-x1)+y1 

                    //this.zedGraphControl.RestoreScale(this.zedGraphControl.GraphPane);

                    //偏离值
                    double a = _List_Data[m_midPIndex].BX1 - (_List_Data[m_midPIndex].F1 / k);
                    //x = (zedmax - y1 + kx1) / k; 


                    //根据chart上curvelist count判断是否添加曲线
                    double[] _line_x = { _List_Data[m_midPIndex].BX1 - (_List_Data[m_midPIndex].F1 / k), _List_Data[m_midPIndex].BX1, ((_List_Data[m_FmIndex].F1 + 200) - _List_Data[m_midPIndex].F1 + k * _List_Data[m_midPIndex].BX1) / k };
                    double[] _line_y = { 0, _List_Data[m_midPIndex].F1, (_List_Data[m_FmIndex].F1 + 200) };
                    LineItem li = this.zedGraphControl.GraphPane.AddCurve("cline_0501_1", _line_x, _line_y, Color.DarkGreen, SymbolType.None);
                    //li.Symbol = m_zedGraphSyb;
                    li.Line.IsAntiAlias = true;
                    li.Line.Width = 1f;
                    li.Tag = "Fp02";


                    //读取数据库的 Ep0.2
                    //读取数据库的 Ep0.2
                    double ep02L0 = 0;
                    switch (modelBend.testType)
                    {
                        case "三点弯曲":
                            ep02L0 = m_n * m_Ls * m_Ls * m_Epb / (12 * m_Y);
                            break;
                        case "四点弯曲":
                            ep02L0 = m_n * (3 * m_Ls * m_Ls - 4 * m_l * m_l) / (24 * m_Y);
                            break;
                    }
                    if (ep02L0 == 0)
                        return;


                    //L = a + ep02L0 + _List_Data[i].F1 / k;
                    for (int i = m_firstPIndex; i < _List_Data.Count; i++)
                    {
                        double Lii = a + ep02L0 + _List_Data[i].F1 / k;
                        if (Lii <= _List_Data[i].BX1)
                        {
                            m_FrIndex = i;
                            break;
                        }
                    }

                    //求弹性模量Eb
                    //计算弹性模量
                    float deltaLoad = _List_Data[m_secondPIndex].F1 - _List_Data[m_firstPIndex].F1;
                    float deltaD = _List_Data[m_secondPIndex].BX1 - _List_Data[m_firstPIndex].BX1;
                    float ls3 = (float)(modelBend.Ls * modelBend.Ls * modelBend.Ls);
                    float ls2 = (float)(modelBend.Ls * modelBend.Ls);
                    float l2 = (float)(modelBend.l_l * modelBend.l_l);
                    switch (modelBend.testType)
                    {
                        case "三点弯曲":
                            float eb1 = (float)(ls3 / (48.0f * modelBend.I)) * (deltaLoad / deltaD);
                            //---------------------------------------                                    
                            m_Eb = utils.GetRoundEbOfBend(eb1);
                            modelBend.Eb = m_Eb;
                            break;
                        case "四点弯曲":
                            float eb2 = (float)((modelBend.l_l * (3.0f * ls2 - 4 * l2)) * (deltaLoad / deltaD) / (48.0f * modelBend.I));
                            m_Eb = utils.GetRoundEbOfBend(eb2);
                            modelBend.Eb = m_Eb;
                            //结果修约
                            break;
                    }

                    //fp02的线
                    double[] _lineFp02_x = { a + ep02L0, _List_Data[m_FrIndex].BX1, ((_List_Data[m_FmIndex].F1 + 200) - _List_Data[m_midPIndex].F1 + k * _List_Data[m_midPIndex].BX1) / k + ep02L0 };
                    double[] _lineFp02_y = { 0, _List_Data[m_FrIndex].F1, (_List_Data[m_FmIndex].F1 + 200) };
                    LineItem liFp02 = this.zedGraphControl.GraphPane.AddCurve("cline_0501_2", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.None);
                    //liFp02.Symbol = m_zedGraphSyb;
                    liFp02.Line.IsAntiAlias = true;
                    liFp02.Line.Width = 1f;
                    liFp02.Tag = "Fp02";

                    //标注Fp02的点
                    double[] _Fp02x = { _List_Data[m_FrIndex].BX1 };
                    double[] _Fp02y = { _List_Data[m_FrIndex].F1 };
                    LineItem pointFp02 = this.zedGraphControl.GraphPane.AddCurve("cline_fp02", _Fp02x, _Fp02y, Color.Blue, SymbolType.UserDefined);
                    pointFp02.Symbol = m_zedGraphSyb;
                    pointFp02.Line.IsAntiAlias = true;
                    pointFp02.Line.Width = 2f;
                    pointFp02.Tag = "Fp02";

                    //添加 值 的标注
                    m_Fpb = Math.Round(_List_Data[m_FrIndex].F1 / 1000, 3);
                    m_σpb = utils.GetRoundStressOfBend(_List_Data[m_FrIndex].YL1);
                    ZedGraph.TextObj t = new TextObj("Fpb = " + m_Fpb.ToString() + " kN\r\n"
                        + "σpb = " + m_σpb + " MPa\r\nEb = " + m_Eb + " Mpa"
                        , _List_Data[m_FrIndex].D1, _List_Data[m_FrIndex].F1);

                    t.FontSpec.FontColor = Color.Navy;
                    t.Location.AlignH = AlignH.Left;
                    t.Location.AlignV = AlignV.Top;
                    t.FontSpec.Border.IsVisible = false;
                    t.ZOrder = ZOrder.E_BehindCurves;
                    t.FontSpec.IsBold = true;
                    t.Tag = "Fp02";
                    this.zedGraphControl.GraphPane.GraphObjList.Add(t);

                    ////标注第一个点 
                    //double[] _line_first_x = { _List_Data[m_firstPIndex].BX1 };
                    //double[] _line_first_y = { _List_Data[m_firstPIndex].F1 };
                    //LineItem li_first = this.zedGraphControl.GraphPane.AddCurve("cline_first", _line_first_x, _line_first_y, Color.Navy, SymbolType.UserDefined);
                    //li_first.Symbol = m_zedGraphSyb;
                    //li_first.Line.IsAntiAlias = true;
                    //li_first.Line.Width = 2f;
                    //li_first.Tag = "Fp02";

                    ////标注第二个点
                    //double[] _line_second_x = { _List_Data[m_secondPIndex].BX1 };
                    //double[] _line_second_y = { _List_Data[m_secondPIndex].F1 };
                    //LineItem li_second = this.zedGraphControl.GraphPane.AddCurve("cline_second", _line_second_x, _line_second_y, Color.Navy, SymbolType.UserDefined);
                    //li_second.Symbol = m_zedGraphSyb;
                    //li_second.Line.IsAntiAlias = true;
                    //li_second.Line.Width = 2f;
                    //li_second.Tag = "Fp02";

                    //标注中间的点
                    double[] _line_mid_x = { _List_Data[m_midPIndex].BX1 };
                    double[] _line_mid_y = { _List_Data[m_midPIndex].F1 };
                    LineItem line_mid = this.zedGraphControl.GraphPane.AddCurve("cline_mid", _line_mid_x, _line_mid_y, Color.Navy, SymbolType.UserDefined);
                    line_mid.Symbol = m_zedGraphSyb;
                    line_mid.Line.IsAntiAlias = true;
                    line_mid.Line.Width = 2f;
                    line_mid.Tag = "Fp02";                 

                }
                #endregion 
                this.zedGraphControl.Invalidate();
                this.zedGraphControl.Refresh();
            }
        }

        private void tsBtnFp02_Click(object sender, EventArgs e)
        {

            if (this.cmbYr.SelectedIndex != 1 | this.cmbXr.SelectedIndex != 2)
            {
                MessageBox.Show(this, "请选择 负荷 - 位移 曲线分析!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            m_FlagFp02L = true;
            m_FlagFp02E = false;

            if (!this.zedGraphControl.Controls.Contains(_palZoom))
            {
                _palZoom = new PictureBox();
                _palZoom.Name = "pzoom";
                _palZoom.Tag = "pzoom";
                _palZoom.BackColor = Color.Transparent;
                _palZoom.BorderStyle = BorderStyle.FixedSingle;
                _palZoom.Width = this.zedGraphControl.Width / 5;
                _palZoom.Height = this.zedGraphControl.Height * 2 / 5;
                _palZoom.Left = this.zedGraphControl.Width / 5;
                _palZoom.Top = this.zedGraphControl.Height / 5;
                _palZoom.Capture = false;
                this.zedGraphControl.Controls.Add(_palZoom);

                pb = new PickBox();
                pb.WireControl(_palZoom);
                pb.Focus(_palZoom, e);
                _palZoom.MouseClick += new MouseEventHandler(_palZoom_MouseClick);
            }

            this.zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllCurveFp02);
            this.zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFp02);
            this.zedGraphControl.Refresh();

        }



        private bool GetFp02Index(List<gdata> List_Data, int _FRInIndex, out int _FROutIndex, out double _a, out double _k, out int Fr05Index, out int Fr01Index, out double ep02L0)
        {
            double Fr = List_Data[_FRInIndex].F1;
            //查找Fr 0.5的点
            //求出Fr05和 Fr01点
            _FROutIndex = 0;
            _a = 0;
            _k = 0;
            Fr05Index = 0;
            Fr01Index = 0;
            ep02L0 = m_Ls * m_Epb * 10;
            for (int m = 0; m < _FRInIndex; m++)
            {
                if (List_Data[m].F1 >= Fr * 0.6)
                {
                    m_FR05 = List_Data[m].F1;
                    m_LR05 = List_Data[m].D1;
                    Fr05Index = m;
                    break;
                }
            }

            for (int n = 0; n < _FRInIndex; n++)
            {
                if (_List_Data[n].F1 >= Fr * 0.3)
                {
                    m_FR01 = List_Data[n].F1;
                    m_LR01 = List_Data[n].D1;
                    Fr01Index = n;
                    break;
                }
            }

            //计算斜率,在 0.5 和 0.1之间取10点

            int[] kdot = Get0501k(Fr01Index, Fr05Index);
            double sumk = 0;

            for (int i = 0; i < kdot.Length - 1; i++)
            {
                double kone = (List_Data[kdot[i + 1]].F1 - List_Data[kdot[i]].F1) / (List_Data[kdot[i + 1]].D1 - List_Data[kdot[i]].D1);
                sumk += kone;
            }
            _k = sumk / (kdot.Length - 1);

            //计算偏移量
            _a = m_LR05 - (m_FR05 / _k);

            //计算出的Li值，注：100为 L0的 0。2%,此处假设为 50mm * 1000 * 0.2%
            //double Li = a + Fr / k + 100; 
            for (int i = 0; i < List_Data.Count; i++)
            {
                double Lii = _a + ep02L0 + List_Data[i].F1 / _k;
                if (Lii <= List_Data[i].D1)
                {
                    _FROutIndex = i;
                    break;
                }
            }

            if (_FRInIndex != 0 && _a != 0 && _k != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool GetFp02IndexOnE(List<gdata> List_Data, int _FRInIndex, out int _FROutIndex, out double _a, out double _k, out int Fr05Index, out int Fr01Index, out double ep02L0)
        {
            double Fr = List_Data[_FRInIndex].F1;
            //查找Fr 0.5的点
            //求出Fr05和 Fr01点
            _FROutIndex = 0;
            _a = 0;
            _k = 0;
            Fr05Index = 0;
            Fr01Index = 0;
            ep02L0 = m_Ls * 10 * m_Epb;
            for (int m = 0; m < _FRInIndex; m++)
            {
                if (List_Data[m].F1 >= Fr * 0.6)
                {
                    m_FR05 = List_Data[m].F1;
                    m_LR05 = List_Data[m].BX1;
                    Fr05Index = m;
                    break;
                }
            }

            for (int n = 0; n < _FRInIndex; n++)
            {
                if (_List_Data[n].F1 >= Fr * 0.3)
                {
                    m_FR01 = List_Data[n].F1;
                    m_LR01 = List_Data[n].BX1;
                    Fr01Index = n;
                    break;
                }
            }

            //计算斜率,在 0.5 和 0.1之间取10点

            int[] kdot = Get0501k(Fr01Index, Fr05Index);
            double sumk = 0;

            for (int i = 0; i < kdot.Length - 1; i++)
            {
                double kone = (List_Data[kdot[i + 1]].F1 - List_Data[kdot[i]].F1) / (List_Data[kdot[i + 1]].BX1 - List_Data[kdot[i]].BX1);
                sumk += kone;
            }
            _k = sumk / (kdot.Length - 1);

            //计算偏移量
            _a = m_LR05 - (m_FR05 / _k);

            //计算出的Li值，注：100为 L0的 0。2%,此处假设为 50mm * 1000 * 0.2%
            //double Li = a + Fr / k + 100; 
            for (int i = 0; i < List_Data.Count; i++)
            {
                double Lii = _a + ep02L0 + List_Data[i].F1 / _k;
                if (Lii <= List_Data[i].BX1)
                {
                    _FROutIndex = i;
                    break;
                }
            }

            if (_FRInIndex != 0 && _a != 0 && _k != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //计算斜率的点
        private int[] Get0501k(int minIndex, int maxIndex)
        {
            int[] _tenValue = new int[10];
            int i = 0;
            if (maxIndex > minIndex + 10)
            {
                for (int j = 0; j < 10; j++)
                {
                    _tenValue[j] = minIndex + ((maxIndex - minIndex) / 10) * i;
                    i++;
                }
            }
            return _tenValue;
        }


        //计算斜率的点
        private int[] Get0501(int minIndex, int maxIndex)
        {
            int[] _tenValue = new int[5];
            int i = 0;
            if (maxIndex > minIndex + 5)
            {
                for (int j = 0; j < 5; j++)
                {
                    _tenValue[j] = minIndex + ((maxIndex - minIndex) / 5) * i;
                    i++;
                }
            }
            return _tenValue;
        }



        private void tsbtnEFp02_Click(object sender, EventArgs e)
        {
            if (this.cmbYr.SelectedIndex != 1 || this.cmbXr.SelectedIndex != 4)
            {
                MessageBox.Show(this, "请选择 负荷 - 变形 曲线分析!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            m_FlagFp02L = false;
            m_FlagFp02E = true;

            if (!this.zedGraphControl.Controls.Contains(_palZoom))
            {

                _palZoom = new PictureBox();
                _palZoom.Name = "pzoom";
                _palZoom.Tag = "pzoom";
                _palZoom.BackColor = Color.Transparent;
                _palZoom.BorderStyle = BorderStyle.FixedSingle;
                _palZoom.Width = this.zedGraphControl.Width / 5;
                _palZoom.Height = this.zedGraphControl.Height * 2 / 5;
                _palZoom.Left = this.zedGraphControl.Width / 5;
                _palZoom.Top = this.zedGraphControl.Height / 5;
                this.zedGraphControl.Controls.Add(_palZoom);
                pb = new PickBox();
                pb.WireControl(_palZoom);
                pb.Focus(_palZoom, e);
                _palZoom.MouseClick += new MouseEventHandler(_palZoom_MouseClick);
            }

            this.zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllCurveFp02);
            this.zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFp02);
            this.zedGraphControl.Refresh();
        }

        private static bool FindAllFeH(ZedGraph.GraphObj gObj)
        {
            if (gObj.Tag.ToString().Contains("FeH"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool FindAllFeH(ZedGraph.CurveItem ci)
        {
            if (ci.Tag == null) return false;
            if (ci.Tag.ToString().Contains("FeH"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool FindAllFm(ZedGraph.GraphObj gObj)
        {
            if (gObj.Tag.ToString().Contains("Fm"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool FindAllFm(ZedGraph.CurveItem ci)
        {
            if (ci.Tag == null) return false;
            if (ci.Tag.ToString().Contains("Fm"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void tsbtnReH_Click(object sender, EventArgs e)
        {
            m_FlagHandFRH = true;
            m_FlagHandFRL = false;
            //if (this.cmbYr.SelectedIndex == 1 & this.cmbXr.SelectedIndex == 2)//负荷-位移
            //{
            //    if (m_FRHIndex != 0)
            //    {
            //        this.zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFeH);
            //        this.zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllFeH);
            //        //画点标注
            //        double[] _lineFp02_x = { _List_Data[m_FRHIndex].D1 };
            //        double[] _lineFp02_y = { _List_Data[m_FRHIndex].F1 };

            //        LineItem liFrH = this.zedGraphControl.GraphPane.AddCurve("cline_0501_3", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.UserDefined);
            //        liFrH.Symbol = m_zedGraphSyb;
            //        liFrH.Tag = "FeH";

            //确认点的位置


            /*
            ZedGraph.EllipseObj eo = new EllipseObj(_List_Data[m_FRHIndex].D1, _List_Data[m_FRHIndex].F1, 1d, 1d, Color.DarkRed, Color.Blue);
            eo.Tag = "FeH";
            eo.ZOrder = ZOrder.E_BehindCurves;
            this.zedGraphControl.GraphPane.GraphObjList.Add(eo);
             * */

            /*
            ZedGraph.LineObj l = new LineObj(_List_Data[m_FRHIndex].D1, _List_Data[m_FRHIndex].F1, _List_Data[m_FRHIndex].D1, _List_Data[m_FRHIndex].F1-1); 
            l.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            l.Line.Width = 4;
            l.Line.Color = Color.Navy;
            l.Tag = "FeH";
            this.zedGraphControl.GraphPane.GraphObjList.Add(l);
            */

            //添加 值 的标注
            //ZedGraph.TextObj t = new TextObj("FeH = " + (_List_Data[m_FRHIndex].F1 / 1000.0).ToString("G5") + " kN \r\nReH=" + (_List_Data[m_FRHIndex].F1 / m_S0).ToString("G5") + " MPa", _List_Data[m_FRHIndex].D1, _List_Data[m_FRHIndex].F1);
            //t.FontSpec.FontColor = Color.Navy;
            //t.Location.AlignH = AlignH.Right;
            //t.Location.AlignV = AlignV.Bottom;
            //t.FontSpec.IsBold = true;
            //t.FontSpec.StringAlignment = StringAlignment.Near;
            //t.FontSpec.Border.IsVisible = false;
            //t.ZOrder = ZOrder.E_BehindCurves;
            //t.Tag = "FeH";
            //this.zedGraphControl.GraphPane.GraphObjList.Add(t);
            //this.zedGraphControl.Refresh();
            //    }
            //}

            //if (this.cmbYr.SelectedIndex == 1 & this.cmbXr.SelectedIndex == 4)//负荷-变形
            //{
            //    if (m_FRHIndex != 0)
            //    {
            //        this.zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFeH);
            //        this.zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllFeH);
            //        //画点标注
            //        double[] _lineFp02_x = { _List_Data[m_FRHIndex].BX1 };
            //        double[] _lineFp02_y = { _List_Data[m_FRHIndex].F1 };
            //        LineItem liFrH = this.zedGraphControl.GraphPane.AddCurve("cline_0501_3", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.UserDefined);
            //        liFrH.Tag = "FeH";
            //        liFrH.Symbol = m_zedGraphSyb;

            //        //添加 值 的标注
            //        ZedGraph.TextObj t = new TextObj("FeH = " + (_List_Data[m_FRHIndex].F1 / 1000).ToString("G5") + " kN \r\nReH=" + (_List_Data[m_FRHIndex].F1 / 1000 * m_S0).ToString("G5") + " MPa", _List_Data[m_FRHIndex].BX1, _List_Data[m_FRHIndex].F1);
            //        t.FontSpec.FontColor = Color.Navy;
            //        t.Location.AlignH = AlignH.Left;
            //        t.Location.AlignV = AlignV.Top;
            //        t.FontSpec.IsBold = true;
            //        t.FontSpec.StringAlignment = StringAlignment.Near;
            //        t.FontSpec.Border.IsVisible = false;
            //        t.ZOrder = ZOrder.E_BehindCurves;
            //        t.Tag = "FeH";
            //        this.zedGraphControl.GraphPane.GraphObjList.Add(t);
            //        this.zedGraphControl.Refresh();
            //    }
            //}

        }


        private static bool FindAllFeL(ZedGraph.GraphObj gObj)
        {
            if (gObj.Tag.ToString().Contains("FeL"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool FindAllFeL(ZedGraph.CurveItem gObj)
        {
            if (gObj.Tag == null) return false;
            if (gObj.Tag.ToString().Contains("FeL"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        private void tsbtnReL_Click(object sender, EventArgs e)
        {
            m_FlagHandFRH = false;
            m_FlagHandFRL = true;           
        }

        private void tsbtnZoom_Click(object sender, EventArgs e)
        {
            //放大框选部分,若无框则不动作,
            if (!this.zedGraphControl.Controls.Contains(_palZoom))
            {

                _palZoom = new PictureBox();
                _palZoom.Name = "pzoom_user";
                _palZoom.Tag = "pzoom_user";
                _palZoom.BackColor = Color.Transparent;
                _palZoom.BorderStyle = BorderStyle.FixedSingle;
                _palZoom.Width = this.zedGraphControl.Width / 5;
                _palZoom.Height = this.zedGraphControl.Height * 2 / 5;
                _palZoom.Left = this.zedGraphControl.Width / 5;
                _palZoom.Top = this.zedGraphControl.Height / 5;
                this.zedGraphControl.Controls.Add(_palZoom);
                _palZoom.Focus();
                pb = new PickBox();
                pb.WireControl(_palZoom);
                pb.Focus(_palZoom, e);
                return;
            }

            //放大框选部分后 选择曲线的起始点 清零,有框则清除
            double x1; double x2;
            double y1; double y2;
            double x3; double x4;
            double y3; double y4;

            PointF pLeftTop = new PointF();
            pLeftTop.X = this._palZoom.Left;
            pLeftTop.Y = this._palZoom.Top;

            PointF pLeftBottom = new PointF();
            pLeftBottom.X = this._palZoom.Left;
            pLeftBottom.Y = this._palZoom.Top + this._palZoom.Height;

            PointF pRightTop = new PointF();
            pRightTop.X = this._palZoom.Left + this._palZoom.Width;
            pRightTop.Y = this._palZoom.Top;

            PointF pRightBottom = new PointF();
            pRightBottom.X = this._palZoom.Left + this._palZoom.Width;
            pRightBottom.Y = this._palZoom.Top + this._palZoom.Height;

            this.zedGraphControl.GraphPane.ReverseTransform(pLeftTop, out x1, out y1);
            this.zedGraphControl.GraphPane.ReverseTransform(pRightTop, out x2, out y2);
            this.zedGraphControl.GraphPane.ReverseTransform(pLeftBottom, out x3, out y3);
            this.zedGraphControl.GraphPane.ReverseTransform(pRightBottom, out x4, out y4);

            if (x1 < 0) x1 = x3 = 0;
            if (x2 < 0) x2 = x4 = 0;
            if (y1 < 0) y1 = y2 = 0;
            if (y3 < 0) y3 = y4 = 0;

            this.zedGraphControl.GraphPane.XAxis.Scale.MinAuto = false;
            this.zedGraphControl.GraphPane.XAxis.Scale.MaxAuto = false;
            this.zedGraphControl.GraphPane.YAxis.Scale.MinAuto = false;
            this.zedGraphControl.GraphPane.YAxis.Scale.MaxAuto = false;

            this.zedGraphControl.GraphPane.XAxis.Scale.Min = x1;
            this.zedGraphControl.GraphPane.XAxis.Scale.Max = x2;

            this.zedGraphControl.GraphPane.YAxis.Scale.Min = y3;
            this.zedGraphControl.GraphPane.YAxis.Scale.Max = y1;

            //this.zedGraphControl.GraphPane.XAxis.Scale.Format = "f1";
            //this.zedGraphControl.GraphPane.YAxis.Scale.Format = "f1";

            this.zedGraphControl.GraphPane.XAxis.Scale.BaseTic = this.zedGraphControl.GraphPane.XAxis.Scale.Min;
            this.zedGraphControl.GraphPane.YAxis.Scale.BaseTic = this.zedGraphControl.GraphPane.YAxis.Scale.Min;

            //this.zedGraphControl.GraphPane.XAxis.Scale.Mag = 0;
            //this.zedGraphControl.GraphPane.YAxis.Scale.Mag = 0;
            this.zedGraphControl.GraphPane.XAxis.Scale.MajorStep = (int)(this.zedGraphControl.GraphPane.XAxis.Scale.Max - this.zedGraphControl.GraphPane.XAxis.Scale.Min) / 5;
            this.zedGraphControl.GraphPane.YAxis.Scale.MajorStep = (int)(this.zedGraphControl.GraphPane.YAxis.Scale.Max - this.zedGraphControl.GraphPane.YAxis.Scale.Min) / 5;

            pb.Remove();
            this._palZoom.Dispose();
            this.zedGraphControl.Refresh();
            ////第一点index
            //m_firstPIndex = GetIndex(_List_Data, y3);
            ////第二点index
            //m_secondPIndex = GetIndex(_List_Data, y1);
            ////MessageBox.Show(m_firstPIndex + "," + m_secondPIndex);
            //确定选框的坐标            
        }

        private int GetIndex(List<gdata> ldata, double yValue)
        {
            int index = 0;
            for (int i = 0; i < ldata.Count; i++)
            {
                if (ldata[i].F1 >= yValue)
                {
                    index = i;
                    break;
                }
                else
                {
                    index = 0;
                }
            }
            return index;
        }

        private void gBtnSave_Click(object sender, EventArgs e)
        {
            //显示Fp02 控件名称 4

            if (m_Fpb > 0 && this.flowLayoutPanel1.Controls.Find("Fpb(kN)", false).Length > 0)
            {
                UC.Result lblFpb = (UC.Result)this.flowLayoutPanel1.Controls.Find("Fpb(kN)", false)[0];
                lblFpb.Text = m_Fpb.ToString();
            }

            if (m_σpb > 0 && this.flowLayoutPanel1.Controls.Find("σpb(MPa)", false).Length > 0)
            {
                UC.Result lblσpb = (UC.Result)this.flowLayoutPanel1.Controls.Find("σpb(MPa)", false)[0];
                lblσpb.Text = m_σpb.ToString();
            }

            if (m_Eb > 0 && this.flowLayoutPanel1.Controls.Find("Eb(MPa)", false).Length > 0)
            {
                UC.Result lblEb = (UC.Result)this.flowLayoutPanel1.Controls.Find("Eb(MPa)", false)[0];
                lblEb.Text = m_Eb.ToString();
            }

           if (m_FmIndex > 0 && this.flowLayoutPanel1.Controls.Find("Fbb(kN)", false).Length > 0)
            {
                UC.Result lblFbb = (UC.Result)this.flowLayoutPanel1.Controls.Find("Fbb(kN)", false)[0];
                lblFbb.Text = (_List_Data[m_FmIndex].F1 / 1000.0).ToString("f4");
            } 
        }

        private void zedGraphControl_MouseClick(object sender, MouseEventArgs e)
        {
            //double x1, y1;
            //this.zedGraphControl.GraphPane.ReverseTransform(e.Location, out x1, out y1);
            //MessageBox.Show(x1.ToString() + "," + y1.ToString());
            //PointF p = new PointF((float)x1,(float) y1);
            int nearP = 0;
            ZedGraph.CurveItem ci = null;
            PointF p = (PointF)e.Location;

            if (this.zedGraphControl.GraphPane.FindNearestPoint(p, this.zedGraphControl.GraphPane.CurveList, out ci, out nearP) && m_FlagHandFRH)
            {
                this.zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFeH);
                this.zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllFeH);
                //画点标注
                double[] _lineFp02_x = { this.zedGraphControl.GraphPane.CurveList[0].Points[nearP].X };
                double[] _lineFp02_y = { this.zedGraphControl.GraphPane.CurveList[0].Points[nearP].Y };

                LineItem liFrH = this.zedGraphControl.GraphPane.AddCurve("cline_0501_3", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.UserDefined);
                liFrH.Symbol = m_zedGraphSyb;
                liFrH.Tag = "FeH";

                m_HandFeH = Convert.ToDouble((_List_Data[nearP].F1 / 1000.0).ToString("G5"));
                m_HandReH = Convert.ToDouble((m_HandFeH * 1000 / m_S0).ToString("G5"));

                ZedGraph.TextObj t = new TextObj("FeH = " + m_HandFeH + " kN \r\nReH=" + m_HandReH + " MPa", this.zedGraphControl.GraphPane.CurveList[0].Points[nearP].X, this.zedGraphControl.GraphPane.CurveList[0].Points[nearP].Y);

                t.FontSpec.FontColor = Color.Navy;
                t.Location.AlignH = AlignH.Right;
                t.Location.AlignV = AlignV.Bottom;
                t.FontSpec.IsBold = true;
                t.FontSpec.StringAlignment = StringAlignment.Near;
                t.FontSpec.Border.IsVisible = false;
                t.ZOrder = ZOrder.E_BehindCurves;
                t.Tag = "FeH";
                this.zedGraphControl.GraphPane.GraphObjList.Add(t);
                this.zedGraphControl.Refresh();
                m_FlagHandFRH = false;
                m_FlagHandFRL = false;
            }

            if (this.zedGraphControl.GraphPane.FindNearestPoint(p, this.zedGraphControl.GraphPane.CurveList, out ci, out nearP) && m_FlagHandFRL)
            {
                this.zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFeL);
                this.zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllFeL);
                //画点标注
                double[] _lineFp02_x = { this.zedGraphControl.GraphPane.CurveList[0].Points[nearP].X };
                double[] _lineFp02_y = { this.zedGraphControl.GraphPane.CurveList[0].Points[nearP].Y };
                LineItem liFrH = this.zedGraphControl.GraphPane.AddCurve("cline_0501_4", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.UserDefined);
                liFrH.Symbol = m_zedGraphSyb;
                liFrH.Tag = "FeL";

                m_HandFeL = Convert.ToDouble((_List_Data[nearP].F1 / 1000.0).ToString("G5"));
                m_HandReL = Convert.ToDouble((m_HandFeL * 1000 / m_S0).ToString("G5"));

                ZedGraph.TextObj t = new TextObj("FeL = " + m_HandFeL + " kN \r\nReL=" + m_HandReL + " MPa", this.zedGraphControl.GraphPane.CurveList[0].Points[nearP].X, this.zedGraphControl.GraphPane.CurveList[0].Points[nearP].Y);
                t.FontSpec.FontColor = Color.Navy;
                t.Location.AlignH = AlignH.Left;
                t.Location.AlignV = AlignV.Bottom;
                t.FontSpec.IsBold = true;
                t.FontSpec.StringAlignment = StringAlignment.Far;
                t.FontSpec.Border.IsVisible = false;
                t.ZOrder = ZOrder.E_BehindCurves;
                t.Tag = "FeL";
                this.zedGraphControl.GraphPane.GraphObjList.Add(t);
                this.zedGraphControl.Refresh();
                m_FlagHandFRH = false;
                m_FlagHandFRL = false;
            }

        }

        private void cmbXr_Click(object sender, EventArgs e)
        {

        }

        private void tsbtnAutoFeH_Click(object sender, EventArgs e)
        {
            //if (this.cmbYr.SelectedIndex == 1 & this.cmbXr.SelectedIndex == 2)//负荷-位移
            //{
            //    if (m_FRHIndex != 0)
            //    {
            //        this.zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFeH);
            //        this.zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllFeH);
            //        //画点标注
            //        double[] _lineFp02_x = { _List_Data[m_FRHIndex].D1 };
            //        double[] _lineFp02_y = { _List_Data[m_FRHIndex].F1 };

            //        LineItem liFrH = this.zedGraphControl.GraphPane.AddCurve("cline_0501_3", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.UserDefined);
            //        liFrH.Symbol = m_zedGraphSyb;
            //        liFrH.Tag = "FeH";

            //        //确认点的位置


            //        ZedGraph.EllipseObj eo = new EllipseObj(_List_Data[m_FRHIndex].D1, _List_Data[m_FRHIndex].F1, 1d, 1d, Color.DarkRed, Color.Blue);
            //        eo.Tag = "FeH";
            //        eo.ZOrder = ZOrder.E_BehindCurves;
            //        this.zedGraphControl.GraphPane.GraphObjList.Add(eo);


            //        ZedGraph.LineObj l = new LineObj(_List_Data[m_FRHIndex].D1, _List_Data[m_FRHIndex].F1, _List_Data[m_FRHIndex].D1, _List_Data[m_FRHIndex].F1 - 1);
            //        l.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid;
            //        l.Line.Width = 4;
            //        l.Line.Color = Color.Navy;
            //        l.Tag = "FeH";
            //        this.zedGraphControl.GraphPane.GraphObjList.Add(l);

            //        //添加 值 的标注
            //        ZedGraph.TextObj t = new TextObj("FeH = " + (_List_Data[m_FRHIndex].F1 / 1000.0).ToString("G5") + " kN \r\nReH=" + (_List_Data[m_FRHIndex].F1 / m_S0).ToString("G5") + " MPa", _List_Data[m_FRHIndex].D1, _List_Data[m_FRHIndex].F1);
            //        t.FontSpec.FontColor = Color.Navy;
            //        t.Location.AlignH = AlignH.Right;
            //        t.Location.AlignV = AlignV.Bottom;
            //        t.FontSpec.IsBold = true;
            //        t.FontSpec.StringAlignment = StringAlignment.Near;
            //        t.FontSpec.Border.IsVisible = false;
            //        t.ZOrder = ZOrder.E_BehindCurves;
            //        t.Tag = "FeH";
            //        this.zedGraphControl.GraphPane.GraphObjList.Add(t);
            //        this.zedGraphControl.Refresh();
            //    }
            //}

            //if (this.cmbYr.SelectedIndex == 1 & this.cmbXr.SelectedIndex == 4)//负荷-变形
            //{
            //    if (m_FRHIndex != 0)
            //    {
            //        this.zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFeH);
            //        this.zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllFeH);
            //        //画点标注
            //        double[] _lineFp02_x = { _List_Data[m_FRHIndex].BX1 };
            //        double[] _lineFp02_y = { _List_Data[m_FRHIndex].F1 };
            //        LineItem liFrH = this.zedGraphControl.GraphPane.AddCurve("cline_0501_3", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.UserDefined);
            //        liFrH.Tag = "FeH";
            //        liFrH.Symbol = m_zedGraphSyb;

            //        //添加 值 的标注
            //        ZedGraph.TextObj t = new TextObj("FeH = " + (_List_Data[m_FRHIndex].F1 / 1000).ToString("G5") + " kN \r\nReH=" + (_List_Data[m_FRHIndex].F1 / 1000 * m_S0).ToString("G5") + " MPa", _List_Data[m_FRHIndex].BX1, _List_Data[m_FRHIndex].F1);
            //        t.FontSpec.FontColor = Color.Navy;
            //        t.Location.AlignH = AlignH.Left;
            //        t.Location.AlignV = AlignV.Top;
            //        t.FontSpec.IsBold = true;
            //        t.FontSpec.StringAlignment = StringAlignment.Near;
            //        t.FontSpec.Border.IsVisible = false;
            //        t.ZOrder = ZOrder.E_BehindCurves;
            //        t.Tag = "FeH";
            //        this.zedGraphControl.GraphPane.GraphObjList.Add(t);
            //        this.zedGraphControl.Refresh();
            //    }
            //}
        }

        private void tsbtnAutoFeL_Click(object sender, EventArgs e)
        {
            //if (this.cmbYr.SelectedIndex == 1 & this.cmbXr.SelectedIndex == 2)//负荷-位移
            //{
            //    if (m_FRLIndex != 0)
            //    {
            //        zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFeL);
            //        zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllFeL);
            //        //画点标注
            //        double[] _lineFp02_x = { _List_Data[m_FRLIndex].D1 };
            //        double[] _lineFp02_y = { _List_Data[m_FRLIndex].F1 };
            //        LineItem liFrH = this.zedGraphControl.GraphPane.AddCurve("cline_0501_4", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.UserDefined);
            //        liFrH.Symbol = m_zedGraphSyb;
            //        liFrH.Tag = "FeL";

            //        //添加 值 的标注
            //        ZedGraph.TextObj t = new TextObj("FeL = " + (_List_Data[m_FRLIndex].F1 / 1000.0).ToString("G5") + " kN \r\nReL=" + (_List_Data[m_FRLIndex].F1 / m_S0).ToString("G5") + " MPa", _List_Data[m_FRLIndex].D1, _List_Data[m_FRLIndex].F1);

            //        t.FontSpec.FontColor = Color.Navy;
            //        t.Location.AlignH = AlignH.Left;
            //        t.Location.AlignV = AlignV.Top;
            //        t.FontSpec.IsBold = true;
            //        t.FontSpec.StringAlignment = StringAlignment.Near;
            //        t.FontSpec.Border.IsVisible = false;
            //        t.FontSpec.IsBold = true;
            //        //t.ZOrder = ZOrder.E_BehindCurves;
            //        t.Tag = "FeL";
            //        this.zedGraphControl.GraphPane.GraphObjList.Add(t);
            //        this.zedGraphControl.Refresh();
            //    }
            //}

            //if (this.cmbYr.SelectedIndex == 1 & this.cmbXr.SelectedIndex == 4)//负荷-变形
            //{
            //    if (m_FRLIndex != 0)
            //    {
            //        zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFeL);
            //        zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllFeL);
            //        //画点标注
            //        double[] _lineFp02_x = { _List_Data[m_FRLIndex].BX1 };
            //        double[] _lineFp02_y = { _List_Data[m_FRLIndex].F1 };
            //        LineItem liFrH = this.zedGraphControl.GraphPane.AddCurve("cline_0501_3", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.UserDefined);
            //        liFrH.Symbol = m_zedGraphSyb;
            //        liFrH.Tag = "FeL";

            //        //添加 值 的标注
            //        ZedGraph.TextObj t = new TextObj("FeL = " + (_List_Data[m_FRLIndex].F1 / 1000).ToString("G5") + " kN \r\nReL=" + (_List_Data[m_FRLIndex].F1 / 1000 * m_S0).ToString("G5") + " MPa", _List_Data[m_FRLIndex].BX1, _List_Data[m_FRLIndex].F1);

            //        t.FontSpec.FontColor = Color.Navy;
            //        t.Location.AlignH = AlignH.Left;
            //        t.Location.AlignV = AlignV.Top;
            //        t.FontSpec.IsBold = true;
            //        t.FontSpec.StringAlignment = StringAlignment.Near;
            //        t.FontSpec.Border.IsVisible = false;
            //        t.ZOrder = ZOrder.E_BehindCurves;
            //        t.Tag = "FeL";
            //        t.FontSpec.Size = 16f;
            //        this.zedGraphControl.GraphPane.GraphObjList.Add(t);
            //        this.zedGraphControl.Refresh();
            //    }
            //}
        }

        private void tsbtnMax_Click(object sender, EventArgs e)
        {
            if (this.cmbYr.SelectedIndex == 1 & this.cmbXr.SelectedIndex == 2)//负荷-位移
            {
                if (m_FmIndex != 0)
                {
                    this.zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFm);
                    this.zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllFm);
                    //画点标注
                    double[] _lineFm_x = { _List_Data[m_FmIndex].D1 };
                    double[] _lineFm_y = { _List_Data[m_FmIndex].F1 };

                    LineItem liFrH = this.zedGraphControl.GraphPane.AddCurve("cline_fm", _lineFm_x, _lineFm_y, Color.Blue, SymbolType.UserDefined);
                    liFrH.Symbol = m_zedGraphSyb;
                    liFrH.Tag = "Fm";

                    //确认点的位置
                    //ZedGraph.EllipseObj eo = new EllipseObj(_List_Data[m_FmIndex].D1, _List_Data[m_FmIndex].F1, 1d, 1d, Color.DarkRed, Color.Blue);
                    //eo.Tag = "Fm";
                    //eo.ZOrder = ZOrder.E_BehindCurves;
                    //this.zedGraphControl.GraphPane.GraphObjList.Add(eo);


                    ZedGraph.LineObj l = new LineObj(_List_Data[m_FmIndex].D1, _List_Data[m_FmIndex].F1, _List_Data[m_FmIndex].D1, _List_Data[m_FmIndex].F1 - 1);
                    l.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid;
                    l.Line.Width = 4;
                    l.Line.Color = Color.Navy;
                    l.Tag = "Fm";
                    this.zedGraphControl.GraphPane.GraphObjList.Add(l);

                    //添加 值 的标注
                    ZedGraph.TextObj t = new TextObj("Fbb = " + (_List_Data[m_FmIndex].F1 / 1000.0).ToString("f4") + " kN \r\n σbb=" + utils.GetRoundStressOfBend(_List_Data[m_FmIndex].YL1) + " MPa", _List_Data[m_FmIndex].D1, _List_Data[m_FmIndex].F1);
                    t.FontSpec.FontColor = Color.Navy;
                    t.Location.AlignH = AlignH.Right;
                    t.Location.AlignV = AlignV.Bottom;
                    t.FontSpec.IsBold = true;
                    t.FontSpec.StringAlignment = StringAlignment.Near;
                    t.FontSpec.Border.IsVisible = false;
                    t.ZOrder = ZOrder.E_BehindCurves;
                    t.Tag = "Fm";
                    this.zedGraphControl.GraphPane.GraphObjList.Add(t);
                    this.zedGraphControl.Refresh();
                }
            }


                if (this.cmbYr.SelectedIndex == 1 & this.cmbXr.SelectedIndex == 1)//负荷-时间
                {
                    if (m_FmIndex != 0)
                    {
                        this.zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFm);
                        this.zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllFm);
                        //画点标注
                        double[] _lineFm_x = { _List_Data[m_FmIndex].Ts };
                        double[] _lineFm_y = { _List_Data[m_FmIndex].F1 };

                        LineItem liFrH = this.zedGraphControl.GraphPane.AddCurve("cline_fm", _lineFm_x, _lineFm_y, Color.Blue, SymbolType.UserDefined);
                        liFrH.Symbol = m_zedGraphSyb;
                        liFrH.Tag = "Fm";

                        //确认点的位置
                        //ZedGraph.EllipseObj eo = new EllipseObj(_List_Data[m_FmIndex].Ts, _List_Data[m_FmIndex].F1, 1d, 1d, Color.Transparent, Color.Blue);
                        //eo.Tag = "Fm";
                        ////eo.ZOrder = ZOrder.E_BehindCurves;
                        //this.zedGraphControl.GraphPane.GraphObjList.Add(eo);


                        ZedGraph.LineObj l = new LineObj(_List_Data[m_FmIndex].Ts, _List_Data[m_FmIndex].F1, _List_Data[m_FmIndex].Ts, _List_Data[m_FmIndex].F1 - 1);
                        l.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid;
                        l.Line.Width = 1;
                        l.Line.Color = Color.Navy;
                        l.Tag = "Fm";
                        this.zedGraphControl.GraphPane.GraphObjList.Add(l);

                        //添加 值 的标注
                        ZedGraph.TextObj t = new TextObj("Fbb = " + (_List_Data[m_FmIndex].F1 / 1000.0).ToString("f4") + " kN \r\n σbb=" + utils.GetRoundStressOfBend(_List_Data[m_FmIndex].YL1) + " MPa", _List_Data[m_FmIndex].Ts, _List_Data[m_FmIndex].F1);
                        t.FontSpec.FontColor = Color.Navy;
                        t.Location.AlignH = AlignH.Right;
                        t.Location.AlignV = AlignV.Bottom;
                        t.FontSpec.IsBold = true;
                        t.FontSpec.StringAlignment = StringAlignment.Near;
                        t.FontSpec.Border.IsVisible = false;
                        t.ZOrder = ZOrder.E_BehindCurves;
                        t.Tag = "Fm";
                        this.zedGraphControl.GraphPane.GraphObjList.Add(t);
                        this.zedGraphControl.Refresh();
                    }
                } 
        }

        private void cmbYr_Click(object sender, EventArgs e)
        {

        }
    }
}
