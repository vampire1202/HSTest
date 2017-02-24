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
    public partial class GBT7314 : Form
    {

        ZedGraph.GraphPane _ResultPanel;

        //读取曲线的数据
        RollingPointPairList _RPPList_Read;
        //RollingPointPairList _RPPList0;


        //存储采集数据
        private List<gdata> _List_Data;
        //曲线名数组
        private string[] strCurveName = new string[6];
        //指定放大区域的Panel
        PictureBox _palZoom;
        PickBox pb;
        RectTracker rect;
        public GBT7314()
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
        double m_FRH = 0;//上屈服力值
        int m_FRHIndex;//上屈服力值索引值
        double m_FRL = 0;//下屈服力值
        int m_FRLIndex;//下屈服力值索引值
        double m_FRLFirst = 0;//初始效应值
        double m_Fn;//实时力值 
        double m_F = 0;//实时采集力值 

        int m_FrIndex = 0;//Fp02索引值
        int m_FtIndex = 0; 
        double m_Lm;//最大值时的延伸
        bool m_FlagStage1Start;//阶段1启动标志
        bool m_FlagStage1Stop;//阶段1停止标志
        bool m_FlagStage2Start;//阶段2启动标志
        bool m_FlagStage2Stop;//阶段2结束标注
        bool m_FlagStage3Start;
        bool m_FlagStage3Stop;

        double m_FR05;
        double m_FR01;
        double m_LR05;
        double m_LR01;

        //试验结果
        double m_L0;
        double m_S0;
        double m_Epc;
        double m_Etc;
        double m_nc;
        double m_Ec;

        bool m_FlagFRH;//上屈服点已求出标志
        bool m_FlagFRL;//下屈服点已求出标志 

        int m_RLCounter;//下屈服计数器

        //手动求Fp02的变量
        int m_firstPIndex;
        int m_secondPIndex;
        int m_midPIndex;

        //手动求上下屈服标志
        bool m_FlagHandFRLc;
        bool m_FlagHandFRHc;
        bool m_FlagHandFmc;
        double m_HandFeLc;
        double m_HandFeHc;
        double m_HandReLc;
        double m_HandReHc;
        double m_HandFmc;
        double m_HandRmc;

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

        BLL.Compress m_t;
        Model.Compress m_m;
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
            m_t = new HR_Test.BLL.Compress();
            m_m = m_t.GetModel(_TestSampleNo);
            if (m_m != null)
            {
                if (this.flowLayoutPanel1.Controls.Find("Fmc", false).Length > 0)
                {
                    UC.Result lblFm = (UC.Result)this.flowLayoutPanel1.Controls.Find("Fmc", false)[0];
                    lblFm.Text = m_m.Fmc.ToString();
                }
                if (this.flowLayoutPanel1.Controls.Find("FeLc", false).Length > 0)
                {
                    UC.Result lblFeL = (UC.Result)this.flowLayoutPanel1.Controls.Find("FeLc", false)[0];
                    lblFeL.Text = (m_m.FeLc).ToString();
                }

                if (this.flowLayoutPanel1.Controls.Find("FeHc", false).Length > 0)
                {
                    UC.Result lblFeH = (UC.Result)this.flowLayoutPanel1.Controls.Find("FeHc", false)[0];
                    lblFeH.Text = (m_m.FeHc).ToString();
                }
                if (this.flowLayoutPanel1.Controls.Find("Fpc", false).Length > 0)
                {
                    UC.Result lblFpc = (UC.Result)this.flowLayoutPanel1.Controls.Find("Fpc", false)[0];
                    lblFpc.Text = m_m.Fpc.ToString();
                }

                if (this.flowLayoutPanel1.Controls.Find("Ftc", false).Length > 0)
                {
                    UC.Result lblFtc = (UC.Result)this.flowLayoutPanel1.Controls.Find("Ftc", false)[0];
                    lblFtc.Text = m_m.Ftc.ToString();
                }

                if (this.flowLayoutPanel1.Controls.Find("Rmc", false).Length > 0)
                {
                    UC.Result lblRm = (UC.Result)this.flowLayoutPanel1.Controls.Find("Rmc", false)[0];
                    lblRm.Text = m_m.Rmc.ToString();
                }
                
                if (this.flowLayoutPanel1.Controls.Find("ReLc", false).Length > 0)
                {
                    UC.Result lblReL = (UC.Result)this.flowLayoutPanel1.Controls.Find("ReLc", false)[0];
                    lblReL.Text = m_m.ReLc.ToString();
                }
                if (this.flowLayoutPanel1.Controls.Find("ReHc", false).Length > 0)
                {
                    UC.Result lblReH = (UC.Result)this.flowLayoutPanel1.Controls.Find("ReHc", false)[0];
                    lblReH.Text = m_m.ReHc.ToString();
                }
                if (this.flowLayoutPanel1.Controls.Find("Rtc", false).Length > 0)
                {
                    UC.Result lblRtc = (UC.Result)this.flowLayoutPanel1.Controls.Find("Rtc", false)[0];
                    lblRtc.Text = m_m.Rtc.ToString();
                }

                if (this.flowLayoutPanel1.Controls.Find("Rpc", false).Length > 0)
                {
                    UC.Result lblRpc = (UC.Result)this.flowLayoutPanel1.Controls.Find("Rpc", false)[0];
                    lblRpc.Text = m_m.Rpc.ToString();
                }

                if (this.flowLayoutPanel1.Controls.Find("Ec", false).Length > 0)
                {
                    UC.Result lblEc = (UC.Result)this.flowLayoutPanel1.Controls.Find("Ec", false)[0];
                    lblEc.Text = m_m.Ec.ToString();
                }
                m_L0 =(double)m_m.L0;
                m_Epc =(double)m_m.εpc;
                m_Etc = (double)m_m.εtc;
                m_nc = (double)m_m.n;
                m_S0 = (double)m_m.S0;
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
                    //case 3://应变                       
                    //        sScale.Mag = 0;
                    //        sScale.Format = "0.000";
                    //    break;
                    //case 4://变形
                    //    if (sScale.Max > 1000)
                    //    {
                    //        sScale.Mag = 3;
                    //        sScale.Format = "0.000";
                    //    }
                    //    else
                    //    {
                    //        sScale.Mag = 0;
                    //        sScale.Format = "0.0";
                    //    }
                    //    break;
                    //case 5://应力
                    //    sScale.Mag = 0;
                    //    sScale.Format = "0.0";
                    //    break;
                }

                //if (_ResultPanel.XAxis.Scale.Max > 100)
                //{
                //    _ResultPanel.XAxis.Scale.Max = ((int)_ResultPanel.XAxis.Scale.Max / 100) * 100 +100;
                //    _ResultPanel.XAxis.Scale.Min = 0;
                //}

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

                //if (_ResultPanel.YAxis.Scale.Max > 100)
                //{
                //    _ResultPanel.YAxis.Scale.Max = ((int)_ResultPanel.YAxis.Scale.Max / 100) * 100+100;
                //    _ResultPanel.YAxis.Scale.Min = 0;
                //}

                _ResultPanel.YAxis.Scale.MajorStep = (_ResultPanel.YAxis.Scale.Max - _ResultPanel.YAxis.Scale.Min) / 5;
                _ResultPanel.YAxis.Scale.MinorStep = _ResultPanel.YAxis.Scale.MajorStep / 5;
            }
        } 

        // string[] _lblTensile_Result = { "-", "-", "Fm", "Rm", "ReH", "ReL", "Rp", "Rt", "Rr", "εp", "εt", "εr", "E", "m", "mE", "A", "Ae", "Ag", "At", "Agt", "Awn", "Lm", "Lf", "Z", "X", "S", "X￣" };

        // string[] _lblCompress_Result = { "-", "-", "△L", "εpc", "εtc", "n", "F0", "Ff", "Fpc", "Ftc", "FeHc", "FeLc", "Fmc", "Rpc", "Rtc", "ReHc", "ReLc", "Rmc", "Ec", "X", "X￣" };

        //  string[] _lblBend_Result = { "-", "-", "α", "r", "f" };


        //保存修改结果
        private void gBtnSaveResult_Click(object sender, EventArgs e)
        {                    
            BLL.Compress bllCs = new HR_Test.BLL.Compress();
            Model.Compress modelCs = bllCs.GetModel(this.tslblSampleNo.Text);
            foreach (UC.Result ucR in this.flowLayoutPanel1.Controls)
            {
                switch (ucR.Name)
                {
                    //case "0":
                    //    modelCs.deltaL = double.Parse(ucR.txtFiledContent.Text);
                    //    break;
                    //case "1":
                    //    modelCs.εpc = double.Parse(ucR.txtFiledContent.Text);
                    //    break;
                    //case "2":
                    //    modelCs.εtc = double.Parse(ucR.txtFiledContent.Text);
                    //    break;
                    //case "3":
                    //    modelCs.n = double.Parse(ucR.txtFiledContent.Text);
                    //    break;
                    //case "4":
                    //    modelCs.F0 = double.Parse(ucR.txtFiledContent.Text);
                    //    break;
                    //case "5":
                    //    modelCs.Ff = double.Parse(ucR.txtFiledContent.Text);
                    //    break;
                    case "Fpc(N)":
                        modelCs.Fpc = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "Ftc(N)":
                        modelCs.Ftc = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "FeHc(N)":
                        modelCs.FeHc = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "FeLc(N)":
                        modelCs.FeLc = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "Fmc(N)":
                        modelCs.Fmc = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "Rpc(MPa)":
                        modelCs.Rpc = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "Rtc(MPa)":
                        modelCs.Rtc = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "ReHc(MPa)":
                        modelCs.ReHc = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "ReLc(MPa)":
                        modelCs.ReLc = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "Rmc(MPa)":
                        modelCs.Rmc = double.Parse(ucR.txtFiledContent.Text);
                        break;
                    case "Ec(MPa)":
                        modelCs.Ec = double.Parse(ucR.txtFiledContent.Text);
                        break;                    
                }
            }
            if (bllCs.Update(modelCs))
            {
                MessageBox.Show(this, "更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                    
              
        }

        private void cmbYr_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.zedGraphControl.GraphPane.GraphObjList.Clear();
            initResultCurve(this.zedGraphControl);
            InitCurve(this.zedGraphControl, this.tslblSampleNo.Text, this._TestType, _lineColor);
                  
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
            InitCurve(this.zedGraphControl, this.tslblSampleNo.Text,this._testType, _lineColor);                 
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
                    // ("testType,testSampleNo,S0,L0,Le,Lc,Ep,Et,Er");
                    //if (testSampleInfo2[0] == "tensile")
                    //{
                    //    m_S0 = double.Parse(testSampleInfo2[2]);
                    //    m_L0 = double.Parse(testSampleInfo2[3]);
                    //    m_Ep = double.Parse(testSampleInfo2[6]);
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
                //记录前第五点的值
                //if (i > 6)
                //    m_pre5dotvalue = listGData[i - 5].F1;

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
                if (!isSelReH && !isSelReL)
                {//如果实时力值大于前一点力值
                    if (m_F > m_Fm)
                    {
                        m_Fm = m_F;
                        m_FmIndex = i - 1;
                        //最大值的延伸，此处用位移表示
                        m_Lm = D1value;
                    }
                }


                //-----------------上升阶段------------------
                if (isSelReH || isSelReL)
                {
                    #region 计算上下屈服
                    if (m_F > m_Fn + 3 * m_SR)//m_Fn
                    {
                        m_RLCounter = 0;
                        //如果阶段1已经发生
                        if (m_FlagStage1Start == true)
                        {
                            //如果阶段1还没结束
                            if (m_FlagStage1Stop == false)
                            {
                                //上升时立马给阶段1停止标志
                                m_FlagStage1Stop = true;
                                //表示求出下屈服
                                m_FlagFRL = true;
                                //开始存储最大值
                                m_Fm = m_F;
                                //下屈服的值为刚好上升的前一点值 ，此处貌似为初始效应值
                                m_FRLFirst = m_Fn;
                                m_FRL = m_Fn;
                                m_FRLIndex = i - 1;
                            }
                            //如果阶段1已经结束
                            else
                            {
                                //如果第二阶段已经开始
                                if (m_FlagStage2Start == true)
                                {
                                    //如果阶段2已经开始还没结束就是第二次下降的最低值
                                    if (m_FlagStage2Stop == false)
                                    {
                                        m_FlagStage2Stop = true;
                                        //存储第二次下降最低的值，此值是去掉初始效应的第二次下降最低值
                                        m_FRL = m_Fn;
                                        m_FlagFRL = true;
                                        m_FRLIndex = i - 1;
                                    }
                                    else
                                    {
                                        //如果第三阶段已经开始
                                        if (m_FlagStage3Start == true)
                                        {
                                            if (m_FlagStage3Stop == false)
                                            {
                                                //第三阶段结束标志
                                                m_FlagStage3Stop = true;
                                                m_FlagStage3Start = false;
                                                if (m_Fn < m_FRL)
                                                {
                                                    m_FRL = m_Fn;
                                                    m_FRLIndex = i - 1;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else if (m_F == m_Fn)//平移阶段
                    {
                        //如果上下屈服都未求出
                        if (m_FlagFRH == false && m_FlagFRL == false)
                        {
                            m_RLCounter++;
                            //如果值保持相等大于3个点
                            if (m_RLCounter > 3)
                            {
                                //表示上下屈服已求出
                                m_FlagFRH = true;
                                m_FlagFRL = true;
                                m_FRL = m_F;
                                m_FRLIndex = i - 1;
                                m_FRH = 0;
                                m_FRHIndex = 0;
                            }
                        }
                    }
                    else if (m_F < m_Fn - 3 * m_SR) //m_pre5dotvaluem_Fn下降阶段
                    {
                        m_RLCounter = 0;
                        //如果阶段1还未发生,首次下降,
                        if (m_FlagStage1Start == false)
                        {
                            //存储上屈服
                            m_FRH = m_Fn;
                            //置求出上屈服标志为1
                            m_FlagFRH = true;
                            //阶段1开始标志,开始进入下降阶段
                            m_FlagStage1Start = true;
                            m_FRHIndex = i - 1;
                        }
                        else//如果阶段1已经发生
                        {
                            //第二次下降
                            if (m_FlagStage2Start == false)
                            {
                                //追踪下屈服
                                m_FRLFirst = m_F;
                                if(m_FlagStage1Stop)
                                    m_FlagStage2Start = true;
                            }
                            //如果阶段1已经结束
                            else
                            {
                                //第三次下降,以后就循环第三次的标志直到试验结束
                                if (m_FlagStage3Start == false)
                                {
                                    m_FlagStage3Start = true;
                                    m_FlagStage3Stop = false;
                                }
                            }

                            //阶段1以后的初始下降点就是最大值的判定
                            if (m_Fn > m_Fm)
                            {
                                m_Fm = m_Fn;
                                m_FmIndex = i - 1;
                                m_Lm = D1value;
                            }
                        }
                    }
                    #endregion
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
            ep02L0 = m_L0 * 10 * m_Epc * m_nc;
            for (int m = 0; m < _FRInIndex; m++)
            {
                if (List_Data[m].F1 >= Fr * 0.5)
                {
                    m_FR05 = List_Data[m].F1;
                    m_LR05 = List_Data[m].D1;
                    Fr05Index = m;
                    break;
                }
            }

            for (int n = 0; n < _FRInIndex; n++)
            {
                if (_List_Data[n].F1 >= Fr * 0.1)
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

                //放大框选部分
                //this.zedGraphControl.GraphPane.XAxis.Scale.Min = x1;
                //this.zedGraphControl.GraphPane.XAxis.Scale.Max = x2;
                //this.zedGraphControl.GraphPane.YAxis.Scale.Min = y3;
                //this.zedGraphControl.GraphPane.YAxis.Scale.Max = y1;
                //this.zedGraphControl.GraphPane.XAxis.Scale.Mag = 0;
                //this.zedGraphControl.GraphPane.YAxis.Scale.Mag = 0;
                //this.zedGraphControl.GraphPane.XAxis.Scale.MajorStep = (x2 - x1) / 5;
                //this.zedGraphControl.GraphPane.YAxis.Scale.MajorStep = (y1 - y3) / 5;
                //this.zedGraphControl.Refresh();

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

                //求弹性模量Ec
                m_Ec = Math.Round(((_List_Data[m_secondPIndex].F1 - _List_Data[m_firstPIndex].F1) * m_L0) / ((_List_Data[m_secondPIndex].D1 - _List_Data[m_firstPIndex].D1)*m_S0),2);
                this._palZoom.Dispose();

                //确定选框的坐标 
                pb.Remove();


                if (m_FlagFp02L)
                {
                #region 力-位移 求Fpc 和 Rpc
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

                    #region 求取Fpc 和 Rpc 并划线

                    double ep02L0 = m_L0 * m_Epc * 10 * m_nc ;
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

                    //添加 值 的标注
                    ZedGraph.TextObj t = new TextObj("Fpc = " + (_List_Data[m_FrIndex].F1 / 1000.0).ToString("G5") + " kN\r\n" + "Rpc =" + (_List_Data[m_FrIndex].YL1).ToString("G5") + " MPa", _List_Data[m_FrIndex].D1, _List_Data[m_FrIndex].F1);
                    t.FontSpec.FontColor = Color.Navy;
                    t.Location.AlignH = AlignH.Left;
                    t.Location.AlignV = AlignV.Top;
                    t.FontSpec.IsBold = true;
                    t.FontSpec.Border.IsVisible = false;
                    //t.ZOrder = ZOrder.E_BehindCurves;
                    t.Tag = "Fp02";
                    this.zedGraphControl.GraphPane.GraphObjList.Add(t);

                    #endregion

                    #region 求取Ftc 和 Rtc 并划线
                    double et02L0 = m_L0 * m_Etc * 10 * m_nc;
                    if (et02L0 == 0)
                        return;

                    //L = a + ep02L0 + _List_Data[i].F1 / k;
                    for (int i = m_firstPIndex; i < _List_Data.Count; i++)
                    {
                        double Lii = a + et02L0 + _List_Data[i].F1 / k;
                        if (Lii <= _List_Data[i].D1)
                        {
                            m_FtIndex = i;
                            break;
                        }
                    }
                    //ft02的线
                    double[] _lineFt02_x = { a + et02L0, _List_Data[m_FtIndex].D1, ((_List_Data[m_FmIndex].F1 + 200) - _List_Data[m_midPIndex].F1 + k * _List_Data[m_midPIndex].D1) / k + et02L0 };
                    double[] _lineFt02_y = { 0, _List_Data[m_FtIndex].F1, (_List_Data[m_FmIndex].F1 + 200) };
                    LineItem liFt02 = this.zedGraphControl.GraphPane.AddCurve("cline_0501_3", _lineFt02_x, _lineFt02_y, Color.Blue, SymbolType.None);
                    //liFp02.Symbol = m_zedGraphSyb;
                    liFt02.Line.IsAntiAlias = true;
                    liFt02.Line.Width = 1f;
                    liFt02.Tag = "Fp02";

                    //添加 值 的标注
                    ZedGraph.TextObj tc = new TextObj("Ftc = " + (_List_Data[m_FtIndex].F1 / 1000.0).ToString("G5") + " kN\r\n" + "Rtc =" + (_List_Data[m_FtIndex].YL1).ToString("G5") + " MPa", _List_Data[m_FtIndex].D1, _List_Data[m_FtIndex].F1);
                    tc.FontSpec.FontColor = Color.Navy;
                    tc.Location.AlignH = AlignH.Left;
                    tc.Location.AlignV = AlignV.Top;
                    tc.FontSpec.IsBold = true;
                    tc.FontSpec.Border.IsVisible = false;
                    //t.ZOrder = ZOrder.E_BehindCurves;
                    tc.Tag = "Fp02";
                    this.zedGraphControl.GraphPane.GraphObjList.Add(tc);
                    #endregion 

                    //划线Fp
                    double[] _Fp02x = { _List_Data[m_FrIndex].D1 };
                    double[] _Fp02y = { _List_Data[m_FrIndex].F1 };
                    LineItem pointFp02 = this.zedGraphControl.GraphPane.AddCurve("cline_fp02", _Fp02x, _Fp02y, Color.Blue, SymbolType.UserDefined);
                    pointFp02.Symbol = m_zedGraphSyb;
                    pointFp02.Line.IsAntiAlias = true;
                    pointFp02.Line.Width = 2f;
                    pointFp02.Tag = "Fp02";

                    //划线Ft
                    double[] _Ft02x = { _List_Data[m_FtIndex].D1 };
                    double[] _Ft02y = { _List_Data[m_FtIndex].F1 };
                    LineItem pointFt02 = this.zedGraphControl.GraphPane.AddCurve("cline_ft02", _Ft02x, _Ft02y, Color.Blue, SymbolType.UserDefined);
                    pointFp02.Symbol = m_zedGraphSyb;
                    pointFp02.Line.IsAntiAlias = true;
                    pointFp02.Line.Width = 2f;
                    pointFp02.Tag = "Fp02";

                    //标注中间的点
                    double[] _line_mid_x = { _List_Data[m_midPIndex].D1 };
                    double[] _line_mid_y = { _List_Data[m_midPIndex].F1 };
                    LineItem line_mid = this.zedGraphControl.GraphPane.AddCurve("cline_mid", _line_mid_x, _line_mid_y, Color.Navy, SymbolType.UserDefined);
                    line_mid.Symbol = m_zedGraphSyb;
                    line_mid.Line.IsAntiAlias = true;
                    line_mid.Line.Width = 2f;
                    line_mid.Tag = "Fp02";

                    //将新值从Label上表现出来
                    //显示Fp02 控件名称 4
                    //UC.Result lblFp02 = (UC.Result)this.flowLayoutPanel1.Controls.Find("Rp", false)[0];
                    //lblFp02.Text = (_List_Data[m_FrIndex].F1 / m_S0).ToString("G5"); 
                }

#endregion

                #region 力-变形 求Fpc 和 Ftc
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
                    //偏离值
                    double a = _List_Data[m_midPIndex].BX1 - (_List_Data[m_midPIndex].F1 / k);

                    //根据chart上curvelist count判断是否添加曲线
                    double[] _line_x = { _List_Data[m_midPIndex].BX1 - (_List_Data[m_midPIndex].F1 / k), _List_Data[m_midPIndex].BX1, ((_List_Data[m_FmIndex].F1 + 200) - _List_Data[m_midPIndex].F1 + k * _List_Data[m_midPIndex].BX1) / k };
                    double[] _line_y = { 0, _List_Data[m_midPIndex].F1, (_List_Data[m_FmIndex].F1 + 200) };
                    LineItem li = this.zedGraphControl.GraphPane.AddCurve("cline_0501_1", _line_x, _line_y, Color.DarkGreen, SymbolType.None);
                    //li.Symbol = m_zedGraphSyb;
                    li.Line.IsAntiAlias = true;
                    li.Line.Width = 1f;
                    li.Tag = "Fp02";


                    //读取数据库的 Ep0.2
                    double ep02L0 = m_L0 * m_Epc * 10 * m_nc;
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
                    ZedGraph.TextObj t = new TextObj("Fpc = " + (_List_Data[m_FrIndex].F1 / 1000).ToString("G5") + " kN\r\n" + "Rpc =" + (_List_Data[m_FrIndex].YL1).ToString("G5") + " MPa", _List_Data[m_FrIndex].BX1, _List_Data[m_FrIndex].F1);
                    t.FontSpec.FontColor = Color.Navy;
                    t.Location.AlignH = AlignH.Left;
                    t.Location.AlignV = AlignV.Top;
                    t.FontSpec.Border.IsVisible = false;
                    t.ZOrder = ZOrder.E_BehindCurves;
                    t.FontSpec.IsBold = true;
                    t.Tag = "Fp02";

                    //读取数据库的 Ep0.2
                    double et02L0 = m_L0 * m_Etc * 10 * m_nc;
                    if (et02L0 == 0)
                        return;
                    //L = a + ep02L0 + _List_Data[i].F1 / k;
                    for (int i = m_firstPIndex; i < _List_Data.Count; i++)
                    {
                        double Lii = a + et02L0 + _List_Data[i].F1 / k;
                        if (Lii <= _List_Data[i].BX1)
                        {
                            m_FrIndex = i;
                            break;
                        }
                    }
                    //fp02的线
                    double[] _lineFt02_x = { a + et02L0, _List_Data[m_FtIndex].BX1, ((_List_Data[m_FmIndex].F1 + 200) - _List_Data[m_midPIndex].F1 + k * _List_Data[m_midPIndex].BX1) / k + et02L0 };
                    double[] _lineFt02_y = { 0, _List_Data[m_FrIndex].F1, (_List_Data[m_FmIndex].F1 + 200) };
                    LineItem liFt02 = this.zedGraphControl.GraphPane.AddCurve("cline_0501_2", _lineFt02_x, _lineFt02_y, Color.Blue, SymbolType.None);
                    //liFp02.Symbol = m_zedGraphSyb;
                    liFt02.Line.IsAntiAlias = true;
                    liFt02.Line.Width = 1f;
                    liFt02.Tag = "Fp02";

                    //标注Fp02的点
                    double[] _Ft02x = { _List_Data[m_FtIndex].BX1 };
                    double[] _Ft02y = { _List_Data[m_FtIndex].F1 };
                    LineItem pointFt02 = this.zedGraphControl.GraphPane.AddCurve("cline_fp02", _Ft02x, _Ft02y, Color.Blue, SymbolType.UserDefined);
                    pointFt02.Symbol = m_zedGraphSyb;
                    pointFt02.Line.IsAntiAlias = true;
                    pointFt02.Line.Width = 2f;
                    pointFt02.Tag = "Fp02";

                    //添加 值 的标注
                    ZedGraph.TextObj tc = new TextObj("Ftc = " + (_List_Data[m_FtIndex].F1 / 1000).ToString("G5") + " kN\r\n" + "Rtc =" + (_List_Data[m_FtIndex].YL1).ToString("G5") + " MPa", _List_Data[m_FtIndex].BX1, _List_Data[m_FtIndex].F1);
                    tc.FontSpec.FontColor = Color.Navy;
                    tc.Location.AlignH = AlignH.Left;
                    tc.Location.AlignV = AlignV.Top;
                    tc.FontSpec.Border.IsVisible = false;
                    tc.ZOrder = ZOrder.E_BehindCurves;
                    tc.FontSpec.IsBold = true;
                    tc.Tag = "Fp02";
                    this.zedGraphControl.GraphPane.GraphObjList.Add(tc);

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
            ep02L0 = m_L0 * m_Epc * 10 *m_nc;
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
            ep02L0 = m_L0 * 10 * m_Epc *m_nc;
            for (int m = 0; m < _FRInIndex; m++)
            {
                if (List_Data[m].F1 >= Fr * 0.5)
                {
                    m_FR05 = List_Data[m].F1;
                    m_LR05 = List_Data[m].BX1;
                    Fr05Index = m;
                    break;
                }
            }

            for (int n = 0; n < _FRInIndex; n++)
            {
                if (_List_Data[n].F1 >= Fr * 0.1)
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

            //if (m_firstPIndex == 0 || m_secondPIndex == 0)
            //{
            //    MessageBox.Show("请用方框选择曲线区域");
            //    return;
            //}



            ////////获取计算斜率点
            //////int[] kindex = Get0501(m_firstPIndex, m_secondPIndex);

            ////////计算斜率
            //////double sumk = 0;
            //////double k = 0;
            //////for (int i = 0; i < kindex.Length - 1; i++)
            //////{
            //////    double kone = (_List_Data[kindex[i + 1]].F1 - _List_Data[kindex[i]].F1) / (_List_Data[kindex[i + 1]].BX1 - _List_Data[kindex[i]].BX1);
            //////    sumk += kone;
            //////}

            //////k = sumk / (kindex.Length - 1);

            ////////取中间点index
            //////int midIndex = m_firstPIndex + (m_secondPIndex - m_firstPIndex) / 2;

            ////////以中间点为准，画一条斜率k的直线，相交于 坐标轴x ，与 圆点的距离 就是偏离值 a;
            ////////力 - 位移 曲线 点斜式方程： y=k(x-x1)+y1 

            //////this.zedGraphControl.RestoreScale(this.zedGraphControl.GraphPane);

            ////////根据chart上curvelist count判断是否添加曲线
            ////////偏离值
            //////double a = _List_Data[midIndex].BX1 - (_List_Data[midIndex].F1 / k);
            ////////x = (zedmax - y1 + kx1) / k; 
            //////double[] _line_x = { _List_Data[midIndex].BX1 - (_List_Data[midIndex].F1 / k), _List_Data[midIndex].BX1, (this.zedGraphControl.GraphPane.YAxis.Scale.Max - _List_Data[midIndex].F1 + k * _List_Data[midIndex].BX1) / k };
            //////double[] _line_y = { 0, _List_Data[midIndex].F1, this.zedGraphControl.GraphPane.YAxis.Scale.Max };
            //////LineItem li = this.zedGraphControl.GraphPane.AddCurve("cline_0501_1", _line_x, _line_y, Color.DarkGreen, SymbolType.UserDefined);
            //////li.Symbol = m_zedGraphSyb;
            //////li.Line.IsAntiAlias = true;
            //////li.Line.Width = 1f;
            //////li.Tag = "Fp02";
            ////////读取数据库的 Ep0.2
            //////double ep02L0 = m_L0 * m_Ep * 10;
            ////////L = a + ep02L0 + _List_Data[i].F1 / k;
            //////for (int i = m_firstPIndex; i < _List_Data.Count; i++)
            //////{
            //////    double Lii = a + ep02L0 + _List_Data[i].F1 / k;
            //////    if (Lii <= _List_Data[i].BX1)
            //////    {
            //////        m_FrIndex = i;
            //////        break;
            //////    }
            //////}
            ////////fp02的线
            //////double[] _lineFp02_x = { a + ep02L0, _List_Data[m_FrIndex].BX1, (this.zedGraphControl.GraphPane.YAxis.Scale.Max - _List_Data[midIndex].F1 + k * _List_Data[midIndex].BX1) / k + ep02L0 };
            //////double[] _lineFp02_y = { 0, _List_Data[m_FrIndex].F1, this.zedGraphControl.GraphPane.YAxis.Scale.Max };
            //////LineItem liFp02 = this.zedGraphControl.GraphPane.AddCurve("cline_0501_2", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.UserDefined);
            //////liFp02.Symbol = m_zedGraphSyb;
            //////liFp02.Line.IsAntiAlias = true;
            //////liFp02.Line.Width = 1f;
            //////liFp02.Tag = "Fp02";

            ////////添加 值 的标注
            //////ZedGraph.TextObj t = new TextObj("Fp02 = " + (_List_Data[m_FrIndex].F1 / 1000).ToString("G5") + " kN", _List_Data[m_FrIndex].BX1, _List_Data[m_FrIndex].F1);
            //////t.FontSpec.FontColor = Color.Navy;
            //////t.Location.AlignH = AlignH.Left;
            //////t.Location.AlignV = AlignV.Top;
            //////t.FontSpec.Border.IsVisible = false;
            //////t.ZOrder = ZOrder.E_BehindCurves;
            //////t.FontSpec.IsBold = true;
            //////t.Tag = "Fp02";
            //////this.zedGraphControl.GraphPane.GraphObjList.Add(t);
            //////this.zedGraphControl.Refresh();  

            /*
            this.cmbYr.SelectedIndex = 1;
            this.cmbXr.SelectedIndex = 4;
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
                if (GetFp02IndexOnE(_List_Data, tempIndex, out m_FrIndex, out a, out k, out fr05index, out fr01index, out ep02L0))
                {
                    count++;
                }

                if (count > 500)
                {
                    MessageBox.Show("计算失败!");
                    return;
                }
            }
            while (m_FrIndex > tempIndex + 2 || m_FrIndex < tempIndex - 2);

            //在曲线上划线 斜率为k 偏离圆心为 a 的直线 y=k(a-x) x=a+y/k 
            //求出的 0.1 0.5 连线 
            Symbol syb = new Symbol();
            syb.IsAntiAlias = true;
            syb.Type = SymbolType.Circle;
            syb.Size = 3;
            syb.Fill.Color = Color.Navy;
            syb.IsVisible = true;

            if (this.zedGraphControl.GraphPane.CurveList["0501"] == null)
            {
                double[] line1x = { a, _List_Data[fr01index].D1, a + (_List_Data[fr05index].F1 / k) };
                double[] line1y = { 0, _List_Data[fr01index].F1, _List_Data[fr05index].F1 };
                LineItem li = this.zedGraphControl.GraphPane.AddCurve("0501", line1x, line1y, Color.DarkGreen, SymbolType.UserDefined);
                li.Symbol = syb;
                li.Line.IsAntiAlias = true;
                li.Line.Width = 1.5f;
            }

            //在曲线上划线 斜率为k 偏离圆心为 a + ep02L0 的直线 y=k(a+ep02L0-x) x=a+ep02L0+y/k  
            if (this.zedGraphControl.GraphPane.CurveList["Fp02"] == null)
            {
                double[] line2x = { a + ep02L0, _List_Data[m_FrIndex].D1 };//a + ep02L0 + (_List_Data[m_FrIndex].F1 / k)
                double[] line2y = { 0, _List_Data[m_FrIndex].F1 };
                //Fp02 连线
                LineItem lifp02 = this.zedGraphControl.GraphPane.AddCurve("Fp02", line2x, line2y, Color.DarkBlue, SymbolType.UserDefined);
                lifp02.Symbol = syb;
                lifp02.Line.IsAntiAlias = true;
                lifp02.Line.Width = 1.5f;
            }
            this.zedGraphControl.Refresh();

            //显示Fp02 控件名称 4
            UC.Result lblFp02 = (UC.Result)this.flowLayoutPanel1.Controls.Find("4", false)[0];
            lblFp02.Text = (_List_Data[m_FrIndex].F1 / m_S0).ToString("G5");
             * 
             * */
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
            m_FlagHandFRHc = true;
            m_FlagHandFRLc = false;
            m_FlagHandFmc = false;
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
            m_FlagHandFRHc = false;
            m_FlagHandFRLc = true; 
            m_FlagHandFmc = false;
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

            if (m_HandFeHc > 0 && this.flowLayoutPanel1.Controls.Find("FeHc(N)", false).Length > 0)
            {
                UC.Result lblFeHc = (UC.Result)this.flowLayoutPanel1.Controls.Find("FeHc(N)", false)[0];
                lblFeHc.Text = m_HandFeHc.ToString("G6");
            }

            if (m_HandReHc > 0 && this.flowLayoutPanel1.Controls.Find("ReHc(MPa)", false).Length > 0)
            {
                UC.Result lblReHc = (UC.Result)this.flowLayoutPanel1.Controls.Find("ReHc(MPa)", false)[0];
                lblReHc.Text = RoundR(m_HandReHc).ToString();
            }

            if (m_HandFeLc > 0 && this.flowLayoutPanel1.Controls.Find("FeLc(N)", false).Length > 0)
            {
                UC.Result lblFeLc = (UC.Result)this.flowLayoutPanel1.Controls.Find("FeLc(N)", false)[0];
                lblFeLc.Text = m_HandFeLc.ToString("G6");
            }

            if (m_HandReLc > 0 && this.flowLayoutPanel1.Controls.Find("ReLc(MPa)", false).Length > 0)
            {
                UC.Result lblReLc = (UC.Result)this.flowLayoutPanel1.Controls.Find("ReLc(MPa)", false)[0];
                lblReLc.Text = RoundR(m_HandReLc).ToString();
            }

            if (m_HandRmc > 0 && this.flowLayoutPanel1.Controls.Find("Rmc(MPa)", false).Length > 0)
            {
                UC.Result lblRmc = (UC.Result)this.flowLayoutPanel1.Controls.Find("Rmc(MPa)", false)[0];
                lblRmc.Text = RoundR(m_HandRmc).ToString();
            }

            if (m_HandFmc > 0 && this.flowLayoutPanel1.Controls.Find("Fmc(N)", false).Length > 0)
            {
                UC.Result lblFmc = (UC.Result)this.flowLayoutPanel1.Controls.Find("Fmc(N)", false)[0];
                lblFmc.Text = m_HandFmc.ToString("G6");
            }

            if (m_FrIndex > 0 && this.flowLayoutPanel1.Controls.Find("Fpc(N)", false).Length > 0)
            {
                UC.Result lblFpc = (UC.Result)this.flowLayoutPanel1.Controls.Find("Fpc(N)", false)[0];
                lblFpc.Text = (_List_Data[m_FrIndex].F1).ToString("G6");
            }

            if (m_FrIndex > 0 && this.flowLayoutPanel1.Controls.Find("Rpc(MPa)", false).Length > 0)
            {
                UC.Result lblRpc = (UC.Result)this.flowLayoutPanel1.Controls.Find("Rpc(MPa)", false)[0];
                lblRpc.Text = RoundR(_List_Data[m_FrIndex].YL1).ToString();
            }

            if (m_Ec > 0 && this.flowLayoutPanel1.Controls.Find("Ec(MPa)", false).Length > 0)
            { 
                UC.Result lblEc = (UC.Result)this.flowLayoutPanel1.Controls.Find("Ec(MPa)", false)[0];
                lblEc.Text = RoundR(m_Ec).ToString(); 
            }

            if (m_FtIndex > 0 && this.flowLayoutPanel1.Controls.Find("Ftc(N)", false).Length > 0)
            {
                UC.Result lblFpc = (UC.Result)this.flowLayoutPanel1.Controls.Find("Ftc(N)", false)[0];
                lblFpc.Text = (_List_Data[m_FtIndex].F1).ToString("G6");
            }

            if (m_FtIndex > 0 && this.flowLayoutPanel1.Controls.Find("Rtc(MPa)", false).Length > 0)
            {
                UC.Result lblRtc = (UC.Result)this.flowLayoutPanel1.Controls.Find("Rtc(MPa)", false)[0];
                lblRtc.Text = RoundR(_List_Data[m_FtIndex].YL1).ToString();
            }

        }

        private int RoundR(double r)
        {
            int re = 0;
            if (r <= 200.0)
                re = (int)r;
            if (r > 200 && r < 1000)
                re = ((int)r / 5) * 5;
            if (r > 1000)
                re = ((int)r / 10) * 10;
            return re;
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

            if (this.zedGraphControl.GraphPane.FindNearestPoint(p, this.zedGraphControl.GraphPane.CurveList, out ci, out nearP) && m_FlagHandFRHc)
            {
                this.zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFeH);
                this.zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllFeH);
                //画点标注
                double[] _lineFp02_x = {ci.Points[nearP+1].X };
                double[] _lineFp02_y = { ci.Points[nearP+1].Y };

                LineItem liFrH = this.zedGraphControl.GraphPane.AddCurve("cline_0501_3", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.UserDefined);
                liFrH.Symbol = m_zedGraphSyb;
                liFrH.Tag = "FeH";

                m_HandFeHc = Convert.ToDouble((_List_Data[nearP+1].F1).ToString("G6"));
                m_HandReHc = Convert.ToDouble((m_HandFeHc / m_S0).ToString("G6"));

                ZedGraph.TextObj t = new TextObj("FeHc = " + m_HandFeHc + " N \r\nReHc=" + m_HandReHc + " MPa", ci.Points[nearP+1].X, ci.Points[nearP+1].Y);

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
                m_FlagHandFRHc = false;
                m_FlagHandFRLc = false;
                m_FlagHandFmc = false;
            }

            if (this.zedGraphControl.GraphPane.FindNearestPoint(p, this.zedGraphControl.GraphPane.CurveList, out ci, out nearP) && m_FlagHandFRLc)
            {
                this.zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFeL);
                this.zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllFeL);
                //画点标注
                double[] _lineFp02_x = { ci.Points[nearP+1].X };
                double[] _lineFp02_y = { ci.Points[nearP+1].Y };
                LineItem liFrH = this.zedGraphControl.GraphPane.AddCurve("cline_0501_4", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.UserDefined);
                liFrH.Symbol = m_zedGraphSyb;
                liFrH.Tag = "FeL";

                m_HandFeLc = Convert.ToDouble((_List_Data[nearP+1].F1).ToString("G6"));
                m_HandReLc = Convert.ToDouble((m_HandFeLc / m_S0).ToString("G6"));

                ZedGraph.TextObj t = new TextObj("FeLc = " + m_HandFeLc + " N \r\nReLc=" + m_HandReLc + " MPa", ci.Points[nearP+1].X,ci.Points[nearP+1].Y);
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
                m_FlagHandFRHc = false;
                m_FlagHandFRLc = false;
                m_FlagHandFmc = false;
            }


            if (this.zedGraphControl.GraphPane.FindNearestPoint(p, this.zedGraphControl.GraphPane.CurveList, out ci, out nearP) && m_FlagHandFmc)
            {
                this.zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFm);
                this.zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllFm);
                //画点标注
                double[] _lineFp02_x = { ci.Points[nearP+1].X };
                double[] _lineFp02_y = { ci.Points[nearP+1].Y };
                LineItem liFrH = this.zedGraphControl.GraphPane.AddCurve("cline_0501_5", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.UserDefined);
                liFrH.Symbol = m_zedGraphSyb;
                liFrH.Tag = "Fm";

                m_HandFmc = Convert.ToDouble((_List_Data[nearP+1].F1).ToString("G6"));
                m_HandRmc = Convert.ToDouble((m_HandFmc / m_S0).ToString("G6"));

                ZedGraph.TextObj t = new TextObj("Fmc = " + m_HandFmc + " N \r\nRmc=" + m_HandRmc + " MPa", ci.Points[nearP+1].X, ci.Points[nearP+1].Y);
                t.FontSpec.FontColor = Color.Navy;
                t.Location.AlignH = AlignH.Left;
                t.Location.AlignV = AlignV.Bottom;
                t.FontSpec.IsBold = true;
                t.FontSpec.StringAlignment = StringAlignment.Far;
                t.FontSpec.Border.IsVisible = false;
                t.ZOrder = ZOrder.E_BehindCurves;
                t.Tag = "Fm";
                this.zedGraphControl.GraphPane.GraphObjList.Add(t);
                this.zedGraphControl.Refresh();
                m_FlagHandFRHc = false;
                m_FlagHandFRLc = false;
                m_FlagHandFmc = false;
            }

        }

        private void tsbtnAZ_Click(object sender, EventArgs e)
        {
            m_FlagHandFRHc = false;
            m_FlagHandFRLc = false;
            m_FlagHandFmc = true;
            //frmAZ az = new frmAZ();
            //az._L0 = (double)m_m.L0;
            //az._S0 = (double)m_m.S0;
            //if (DialogResult.OK == az.ShowDialog())
            //{
            //    m_m.Lu = az._Lu;
            //    if (this.flowLayoutPanel1.Controls.Find("A", false).Length > 0)
            //    {
            //        UC.Result lblA = (UC.Result)this.flowLayoutPanel1.Controls.Find("A", false)[0];
            //        lblA.Text = az._A.ToString();
            //    }
            //    if (this.flowLayoutPanel1.Controls.Find("Z", false).Length > 0)
            //    {
            //        UC.Result lblZ = (UC.Result)this.flowLayoutPanel1.Controls.Find("Z", false)[0];
            //        lblZ.Text = az._Z.ToString();
            //    }
            //    m_t.Update(m_m);
            //}

        }

        private void cmbXr_Click(object sender, EventArgs e)
        {

        }

        private void tsbtnAutoFeH_Click(object sender, EventArgs e)
        {
            if (this.cmbYr.SelectedIndex == 1 & this.cmbXr.SelectedIndex == 2)//负荷-位移
            {
                if (m_FRHIndex != 0)
                {
                    this.zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFeH);
                    this.zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllFeH);
                    //画点标注
                    double[] _lineFp02_x = { _List_Data[m_FRHIndex].D1 };
                    double[] _lineFp02_y = { _List_Data[m_FRHIndex].F1 };

                    LineItem liFrH = this.zedGraphControl.GraphPane.AddCurve("cline_0501_3", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.UserDefined);
                    liFrH.Symbol = m_zedGraphSyb;
                    liFrH.Tag = "FeH";

                    //确认点的位置


                    ZedGraph.EllipseObj eo = new EllipseObj(_List_Data[m_FRHIndex].D1, _List_Data[m_FRHIndex].F1, 1d, 1d, Color.DarkRed, Color.Blue);
                    eo.Tag = "FeH";
                    eo.ZOrder = ZOrder.E_BehindCurves;
                    this.zedGraphControl.GraphPane.GraphObjList.Add(eo);


                    ZedGraph.LineObj l = new LineObj(_List_Data[m_FRHIndex].D1, _List_Data[m_FRHIndex].F1, _List_Data[m_FRHIndex].D1, _List_Data[m_FRHIndex].F1 - 1);
                    l.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid;
                    l.Line.Width = 4;
                    l.Line.Color = Color.Navy;
                    l.Tag = "FeH";
                    this.zedGraphControl.GraphPane.GraphObjList.Add(l);

                    //添加 值 的标注
                    ZedGraph.TextObj t = new TextObj("FeHc = " + (_List_Data[m_FRHIndex].F1 / 1000.0).ToString("G5") + " kN \r\nReHc=" + (_List_Data[m_FRHIndex].F1 / m_S0).ToString("G5") + " MPa", _List_Data[m_FRHIndex].D1, _List_Data[m_FRHIndex].F1);
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
                }
            }

            if (this.cmbYr.SelectedIndex == 1 & this.cmbXr.SelectedIndex == 4)//负荷-变形
            {
                if (m_FRHIndex != 0)
                {
                    this.zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFeH);
                    this.zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllFeH);
                    //画点标注
                    double[] _lineFp02_x = { _List_Data[m_FRHIndex].BX1 };
                    double[] _lineFp02_y = { _List_Data[m_FRHIndex].F1 };
                    LineItem liFrH = this.zedGraphControl.GraphPane.AddCurve("cline_0501_3", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.UserDefined);
                    liFrH.Tag = "FeH";
                    liFrH.Symbol = m_zedGraphSyb;

                    //添加 值 的标注
                    ZedGraph.TextObj t = new TextObj("FeHc = " + (_List_Data[m_FRHIndex].F1 / 1000).ToString("G5") + " kN \r\nReHc=" + (_List_Data[m_FRHIndex].F1 / 1000 * m_S0).ToString("G5") + " MPa", _List_Data[m_FRHIndex].BX1, _List_Data[m_FRHIndex].F1);
                    t.FontSpec.FontColor = Color.Navy;
                    t.Location.AlignH = AlignH.Left;
                    t.Location.AlignV = AlignV.Top;
                    t.FontSpec.IsBold = true;
                    t.FontSpec.StringAlignment = StringAlignment.Near;
                    t.FontSpec.Border.IsVisible = false;
                    t.ZOrder = ZOrder.E_BehindCurves;
                    t.Tag = "FeH";
                    this.zedGraphControl.GraphPane.GraphObjList.Add(t);
                    this.zedGraphControl.Refresh();
                }
            }
        }

        private void tsbtnAutoFeL_Click(object sender, EventArgs e)
        {
            if (this.cmbYr.SelectedIndex == 1 & this.cmbXr.SelectedIndex == 2)//负荷-位移
            {
                if (m_FRLIndex != 0)
                {
                    zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFeL);
                    zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllFeL);
                    //画点标注
                    double[] _lineFp02_x = { _List_Data[m_FRLIndex].D1 };
                    double[] _lineFp02_y = { _List_Data[m_FRLIndex].F1 };
                    LineItem liFrH = this.zedGraphControl.GraphPane.AddCurve("cline_0501_4", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.UserDefined);
                    liFrH.Symbol = m_zedGraphSyb;
                    liFrH.Tag = "FeL";

                    //添加 值 的标注
                    ZedGraph.TextObj t = new TextObj("FeLc = " + (_List_Data[m_FRLIndex].F1 / 1000.0).ToString("G5") + " kN \r\nReLc=" + (_List_Data[m_FRLIndex].F1 / m_S0).ToString("G5") + " MPa", _List_Data[m_FRLIndex].D1, _List_Data[m_FRLIndex].F1);

                    t.FontSpec.FontColor = Color.Navy;
                    t.Location.AlignH = AlignH.Left;
                    t.Location.AlignV = AlignV.Top;
                    t.FontSpec.IsBold = true;
                    t.FontSpec.StringAlignment = StringAlignment.Near;
                    t.FontSpec.Border.IsVisible = false;
                    t.FontSpec.IsBold = true;
                    //t.ZOrder = ZOrder.E_BehindCurves;
                    t.Tag = "FeL";
                    this.zedGraphControl.GraphPane.GraphObjList.Add(t);
                    this.zedGraphControl.Refresh();
                }
            }

            if (this.cmbYr.SelectedIndex == 1 & this.cmbXr.SelectedIndex == 4)//负荷-变形
            {
                if (m_FRLIndex != 0)
                {
                    zedGraphControl.GraphPane.GraphObjList.RemoveAll(FindAllFeL);
                    zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllFeL);
                    //画点标注
                    double[] _lineFp02_x = { _List_Data[m_FRLIndex].BX1 };
                    double[] _lineFp02_y = { _List_Data[m_FRLIndex].F1 };
                    LineItem liFrH = this.zedGraphControl.GraphPane.AddCurve("cline_0501_3", _lineFp02_x, _lineFp02_y, Color.Blue, SymbolType.UserDefined);
                    liFrH.Symbol = m_zedGraphSyb;
                    liFrH.Tag = "FeL";

                    //添加 值 的标注
                    ZedGraph.TextObj t = new TextObj("FeL = " + (_List_Data[m_FRLIndex].F1 / 1000).ToString("G5") + " kN \r\nReL=" + (_List_Data[m_FRLIndex].F1 / 1000 * m_S0).ToString("G5") + " MPa", _List_Data[m_FRLIndex].BX1, _List_Data[m_FRLIndex].F1);

                    t.FontSpec.FontColor = Color.Navy;
                    t.Location.AlignH = AlignH.Left;
                    t.Location.AlignV = AlignV.Top;
                    t.FontSpec.IsBold = true;
                    t.FontSpec.StringAlignment = StringAlignment.Near;
                    t.FontSpec.Border.IsVisible = false;
                    t.ZOrder = ZOrder.E_BehindCurves;
                    t.Tag = "FeL";
                    t.FontSpec.Size = 16f;
                    this.zedGraphControl.GraphPane.GraphObjList.Add(t);
                    this.zedGraphControl.Refresh();
                }
            }
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
                    ZedGraph.EllipseObj eo = new EllipseObj(_List_Data[m_FmIndex].D1, _List_Data[m_FmIndex].F1, 1d, 1d, Color.DarkRed, Color.Blue);
                    eo.Tag = "Fm";
                    eo.ZOrder = ZOrder.E_BehindCurves;
                    this.zedGraphControl.GraphPane.GraphObjList.Add(eo);


                    ZedGraph.LineObj l = new LineObj(_List_Data[m_FmIndex].D1, _List_Data[m_FmIndex].F1, _List_Data[m_FmIndex].D1, _List_Data[m_FmIndex].F1 - 1);
                    l.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid;
                    l.Line.Width = 4;
                    l.Line.Color = Color.Navy;
                    l.Tag = "Fm";
                    this.zedGraphControl.GraphPane.GraphObjList.Add(l);

                    //添加 值 的标注
                    ZedGraph.TextObj t = new TextObj("Fmc = " + (_List_Data[m_FmIndex].F1 / 1000.0).ToString("G5") + " kN \r\nRmc=" + (_List_Data[m_FmIndex].F1 / m_S0).ToString("G5") + " MPa", _List_Data[m_FmIndex].D1, _List_Data[m_FmIndex].F1);
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

        private void tsbtnEc_Click(object sender, EventArgs e)
        {

        }      

    }
}
