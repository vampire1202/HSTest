using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.IO;
using ZedGraph;
using System.Threading;
using System.Threading.Tasks;

namespace HR_Test
{

    public partial class frmTestResult : Form
    {
        /*  20150818 新增 自动求取断后伸长率
         *  1、条件：a.设置 使用变形引伸计 b.设置求取断后伸长率
         *  2、步骤：a.以逐次逼近法测定曲线弹性段框图为基础，求出按负荷变形方式下的偏移量a和曲线的斜率，如果试验没有要求
         *          求Rp,可以在试验完成的曲线上不描绘弹性逼近线，Rp结果不存于数据库，曲线界面不出现Rp的结果。
         *          b.按公式  k=F结束点力点/L,输入参数 k 和 F，求出 L，
         *          c.总变形量以公式 ΔLag = ΔLf（断裂总延伸) - a; 
         *          d.求出A = ΔLag / Le
         *  修改项:
         *  a.在主界面 - 仪器设置 - 主机类型 中图标有错误
         *  单向上箭头 此图标改为双向。被误用为单向空间应改正：
         *  双箭头为单向空间        单箭头为 双向空间
         *  原图尺寸大小调小
         *  b.试验机最低测量范围选择 
         *      0.5% - 2%
         *  c.关于ΔLm求出，如果使用引申计标志，ΔLm为最大力下的变形值，如果没使用引申计,ΔLm为最大力下的位移值。
         *  20170302增加GBT3354-2014标准
         */

        //分辨率常量
        private const uint m_Resolution = 120000;
        /// <summary>
        /// 传感器存在标志
        /// </summary>
        private byte m_SensorArrayFlag;
        public byte M_SensorArrayFlag
        {
            get { return m_SensorArrayFlag; }
            set { this.m_SensorArrayFlag = value; }
        }

        /// <summary>
        /// 传感器数量
        /// </summary>
        private int m_SensorCount = 0;
        public int M_SensorCount
        {
            get { return m_SensorCount; }
            set { m_SensorCount = value; }
        }

        /// <summary>
        /// 位移传感器数量
        /// </summary>
        private int m_DisplacementSensorCount = 0;
        public int M_DisplacementSensorCount
        {
            get { return m_DisplacementSensorCount; }
            set { m_DisplacementSensorCount = value; }
        }


        /// <summary>
        /// 负荷传感器数量
        /// </summary>
        private int m_LoadSensorCount = 0;
        public int M_LoadSensorCount
        {
            get { return m_LoadSensorCount; }
            set { m_LoadSensorCount = value; }
        }

        /// <summary>
        /// 变形传感器数量 
        /// </summary>  
        private int m_ElongateSensorCount = 0;
        public int M_ElongateSensorCount
        {
            get { return m_ElongateSensorCount; }
            set { m_ElongateSensorCount = value; }
        }

        /// <summary>
        /// 位移传感器数组
        /// </summary>
        private Struc.SensorArray[] m_DSensorArray = null;
        public Struc.SensorArray[] M_DSensorArray
        {
            get { return m_DSensorArray; }
            set { m_DSensorArray = value; }
        }

        /// <summary>
        /// 力传感器数组
        /// </summary>
        private Struc.SensorArray[] m_LSensorArray = null;
        public Struc.SensorArray[] M_LSensorArray
        {
            get { return m_LSensorArray; }
            set { m_LSensorArray = value; }
        }

        /// <summary>
        /// 变形传感器数组
        /// </summary>
        private Struc.SensorArray[] m_ESensorArray = null;
        public Struc.SensorArray[] M_ESensorArray
        {
            get { return m_ESensorArray; }
            set { m_ESensorArray = value; }
        }

        /// <summary>
        /// 传感器数组
        /// </summary>
        private Struc.sensor[] m_SensorArray;
        public Struc.sensor[] M_SensorArray
        {
            get { return m_SensorArray; }
            set { m_SensorArray = value; }
        }

        //取引伸计对话框即保持对话框
        frmHold m_fh;
        //控制命令
        private Struc.ctrlcommand[] m_CtrlCommandArray = new Struc.ctrlcommand[12];
        private List<Struc.ctrlcommand> m_CtrlCommandList = new List<Struc.ctrlcommand>();

        //判断转换点的相关参数
        private int m_pcount = 0;
        private bool m_isaddcount = false;
        private int loopCount = 0;
        private float m_ppreload;
        private float m_ppredis;

        private string m_methodContent = string.Empty;
        bool m_isProLoad = false;
        public bool _showThreadFlag;

        //是否使用引伸计数据库标记
        private bool _useExten = false;
        private bool _useExten2 = false;
        private bool m_useExten = false;
        private bool m_useExten2 = false;
        private double m_e1 = 0;
        private double m_e2 = 0;
        private float m_extenValue;
        private int m_extenType = 2;

        //拉伸试验力学性能参数选择
        //自动求取Rp02参数
        float m_a = 0;
        float m_k = 0;
        int m_fr05index = 0;
        int m_fr01index = 0;
        float m_ep02L0 = 0;
        bool m_isSelRp = false;
        bool m_isSelReL = false;
        bool m_isSelReH = false;
        bool m_isSelFm = false;
        bool m_isSelRtc = false;
        bool m_isSelRm = false;
        bool m_isSelLm = false;
        bool m_isSelA = false;
        bool m_isSelZ = false;
        bool m_isSelE = false;
        bool m_isSelDeltaLm = false;
        double m_startLm, m_stopLm;
        bool m_isHandaz = false;
        bool m_isSavecurve = false;
        bool m_calSuccess = false;

        //弯曲试验力学性能参数选择
        //弹性模量
        bool m_isSelEb = false;
        float m_Eb;
        //规定非比例弯曲力
        bool m_isSelFpb = false;
        float m_Fpb = 0;
        //规定残余弯曲力
        bool m_isSelFrb = false;
        float m_Frb = 0;
        //最大弯曲力
        bool m_isSelFbb = false;
        float m_Fbb;
        //最大弯曲应力
        bool m_isSelσbb = false;
        float m_σbb;


        //当力值超过量程的1/200,启动判断停止的标志
        private float m_CheckStopValue = 0f;
        private bool m_CheckStop = true;
        //是否启动判断
        private bool m_Check = false;

        //暂停标志
        bool m_pause = false;
        //取引伸计的暂停标志
        public bool m_holdPause = false;
        //取引伸计后的继续标志
        public bool m_holdContinue = false;
        //实时显示曲线线程
        Thread _threadShowCurve;
        //读取结果数据线程
        Thread _threadReadCurve;
        //当存储数据超过1k条，开启存储数据线程，而后清空采集的数据
        Thread _threadSaveData;
        //lock标志
        private static object m_state = new object();

        public string m_machineType = string.Empty;
        float m_minLoad = 0.1f;

        //public ManualResetEvent manualReset;

        private static Mutex m_mutex = new Mutex();

        //显示值
        float m_Displacement = 0f;      //位移
        float m_Elongate = 0f;          //变形
        float m_Elongate1 = 0f;          //变形1
        float m_Load = 0f;              //力
        float m_Time = 0f;              //时间
        float m_YingLi = 0f;            //应力
        float m_YingBian = 0f;            //应变 

        string m_TestSampleNo;
        string m_TestNo;
        float m_S0;                     //面积

        float m_L0;                     //GBT228 L0原始标距,GBT28289 L试样长度,YBT5349 跨距
        float m_Lc;                     //Lc
        float m_Le;                     //Le
        float m_Ep;
        float m_Et;
        float m_Er;
        float m_Ll;
        float m_Lt;

        float m_n;                      //挠度计放大倍数
        float m_Y;                      //弯曲计算参数

        float m_FR05;
        float m_FR01;
        float m_LR05;
        float m_LR01;
        int m_FmIndex;                  //最大值的索引值 
        int m_FrIndex;

        //参与计算的变量
        float m_Fm = 0;                 //最大力值

        float m_Fmax = 0;               //GBT28289 最大力值 
        float m_Rp = 0;                 //GBT28289 单位长度上承受最大力值
        float m_Rtc = 0;

        int m_FmaxIndex;
        float m_FRH = 0;                //上屈服力值
        float m_FRL = 0;                //下屈服力值

        float m_FRLFirst = 0;           //初始效应值
        float m_Fn;                     //实时力值 
        float m_F = 0;                  //实时采集力值

        bool m_FlagStage1Start;         //阶段1启动标志
        bool m_FlagStage1Stop;          //阶段1停止标志
        bool m_FlagStage2Start;         //阶段2启动标志
        bool m_FlagStage2Stop;          //阶段2结束标注
        bool m_FlagStage3Start;
        bool m_FlagStage3Stop;
        double m_StopValue;

        bool m_FlagFRH;                 //上屈服点已求出标志
        bool m_FlagFRL;                 //下屈服点已求出标志
        //int m_StartCounter;           //判断是否要进行功能的计数器
        int m_RLCounter;                //下屈服计数器

        private bool m_isSelSD = false;
        private bool m_isSelCV = false;
        private bool m_isSelMid = false;

        //负荷分辨率
        private double m_LoadResolutionValue = 0;
        //变形分辨率
        private double m_ElongateResolutionValue = 0;

        byte[] buf;
        byte[] bufCommand;

        private volatile bool isTest = false;

        string _testpath = string.Empty;
        private static string[] _Color_Array = { "Crimson", "Green", "Blue", "Teal", "DarkOrange", "Chocolate", "BlueViolet", "Indigo", "Magenta", "LightCoral", "LawnGreen", "Aqua", "DarkViolet", "DeepPink", "DeepSkyblue", "HotPink", "SpringGreen", "GreenYellow", "Peru", "Black" };
        private static string[] _lblTensile_Result = { "-", "-", "Fm", "Rm", "ReH", "ReL", "Rp", "Rt", "Rr", "E", "m", "mE", "A", "Ae", "Ag", "At", "Agt", "Awn", "Lm", "ΔLm", "Lf", "Z", "X", "S", "X￣" };
        private static string[] _lblGBT7314_Result = { "-", "-", "Fmc(N)", "Fpc(N)", "Ftc(N)", "FeHc(N)", "FeLc(N)", "Rmc(MPa)", "Rpc(MPa)", "Rtc(MPa)", "ReHc(MPa)", "ReLc(MPa)", "Ec(MPa)" };
        private static string[] _lblBend_Result = { "-", "-", "fbb(mm)", "fn(mm)", "fn-1(mm)", "frb(mm)", "y(mm)", "Fo(N)", "Fpb(kN)", "Frb(kN)", "Fbb(kN)", "Fn(kN)", "Fn-1(kN)", "Z", "S", "W", "I", "Eb(MPa)", "σpb(MPa)", "σrb(MPa)", "σbb(MPa)", "U(J)" };

        private ArrayList _List_Testing_Data = new ArrayList();
        private gdata m_hold_data = new gdata();
        private List<gdata> m_l = new List<gdata>();

        //显示切换
        private bool _showYL = false;
        private bool _showYB = false;

        //曲线名数组
        private string[] strCurveName = new string[4];
        ZedGraph.GraphPane _ResultPanel;

        //开始时间
        //private double tickStart = 0;

        //读取曲线的数据
        IPointList _RPPList_ReadOne;

        //曲线路径
        string m_path = string.Empty;

        //试验类型
        string m_testType = string.Empty;

        //选择试样数组
        List<SelTestSample> _selTestSampleArray = new List<SelTestSample>();

        //选择试样的曲线颜色数组
        string[] _selColorArray = null;
        string _selColor = string.Empty;

        //曲线名数组 
        string _selTestSampleGroup = string.Empty;

        ////////////////////////////批量试验相关//////////////////////
        //试样列表树点击事件
        private TreeNodeMouseClickEventArgs e_Node = null;
        //未做完的试样组List
        List<string> m_lstNoTestSamples = new List<string>();
        //是否返回
        //bool m_IsReturn = true;
        /////////////////////////////////////////////////////////////


        //显示曲线设置参数
        int _X1 = 0;
        int _X2 = 0;
        int _Y1 = 0;
        int _Y2 = 0;
        int _ShowX = 0;
        int _ShowY = 0;

        #region 实时曲线相关变量
        /// <summary>
        /// x轴的边距
        /// </summary>
        private const int c_kXAxisIndent = 58;
        /// <summary>
        /// y轴的边距
        /// </summary>
        private const int c_kYAxisIndent = 50;
        /// <summary>
        /// x轴的边距
        /// </summary>
        private const int c_kXAxisoffset = 36;
        /// <summary>
        /// y轴的边距
        /// </summary>
        private const int c_kYAxisoffset = 36;

        //轴的最大最小值
        private float c_x1maximum = 1;
        private float c_x1minimum = 0;
        private float c_y1maximum = 1;
        private float c_y1minimum = 0;

        private float c_x2maximum = 1;
        private float c_x2minimum = 0;
        private float c_y2maximum = 1;
        private float c_y2minimum = 0;

        private bool c_isshowx1;
        private bool c_isshowx2;
        private bool c_isshowy1;
        private bool c_isshowy2;

        private Pen c_axisPenDot = new Pen(Color.DarkGray, 1);
        private Pen c_axisPenSolid = new Pen(Color.Black, 1);

        /// <summary>
        /// 坐标轴分段数
        /// </summary>
        private float _ticksPerAxis = 5.0f;

        /// <summary>
        /// x.y坐标轴标题
        /// </summary>
        private string m_LabelX1 = string.Empty;
        private string m_LabelY1 = string.Empty;
        private string m_LabelX2 = string.Empty;
        private string m_LabelY2 = string.Empty;

        /// <summary>
        /// 数据轴的值超过1000是否千分数显示
        /// </summary>
        private bool isShow103X1 = false;
        private bool isShow103Y1 = false;
        /// <summary>
        /// 数据轴的值超过1000是否千分数显示
        /// </summary>
        private bool isShow103X2 = false;
        private bool isShow103Y2 = false;

        private const int c_kedu = 6;

        private bool isShowResult = false;

        Pen y1Pen = new Pen(Brushes.Purple);
        Pen y2Pen = new Pen(Brushes.DarkGreen);
        Pen x1Pen = new Pen(Brushes.Blue);
        Pen x2Pen = new Pen(Brushes.Crimson);

        Pen py1_x1 = new Pen(Brushes.Purple);
        Pen py1_x2 = new Pen(Brushes.Crimson);
        Pen py2_x1 = new Pen(Brushes.Blue);
        Pen py2_x2 = new Pen(Brushes.DarkGreen);

        private Font GraphFont = new Font("Arial", 12, FontStyle.Bold);
        List<PointF> ly1_x1 = new List<PointF>();
        List<PointF> ly1_x2 = new List<PointF>();
        List<PointF> ly2_x1 = new List<PointF>();
        List<PointF> ly2_x2 = new List<PointF>();

        /// <summary>
        /// chart Legend title
        /// </summary>
        string y1_x1 = string.Empty;
        string y1_x2 = string.Empty;
        string y2_x1 = string.Empty;
        string y2_x2 = string.Empty;

        /// <summary>
        /// x轴数量
        /// </summary>
        private const int c_xAxisCount = 2;
        /// <summary>
        /// y轴数量
        /// </summary>
        private const int c_yAxisCount = 2;

        #endregion

        /// <summary>
        /// 传感器的分辨率
        /// </summary>
        /// <param name="SensorScale"></param>
        /// <returns></returns>
        private double GetSensorSR(ushort SensorScale)
        {
            int m_ScaleValue = GetScale(SensorScale);
            double m_SR = (m_ScaleValue * 1.0f) / (m_Resolution * 1.0f);
            return m_SR;
        }

        //获取传感器量程
        public int GetScale(int Scale)
        {
            int m_Scale = 0;
            //量程指数
            int m_E = (Scale & 0x000f);
            //量程基数
            int SigValue = (Scale >> 8);
            m_Scale = (int)(SigValue * Math.Pow(10.0, m_E));
            return m_Scale;
        }

        frmMain _fmMain;
        public frmTestResult(frmMain fmain)
        {
            InitializeComponent();
            _fmMain = fmain;
            Task t = tskReadSample();
        }

        void zedGraphControl_Invalidated(object sender, InvalidateEventArgs e)
        {
            if (this.zedGraphControl.GraphPane.XAxis != null)
            {
                Scale sScale = _ResultPanel.XAxis.Scale;

                switch (_ShowX)// -请选择-   时间,s  位移,μm  应变,μm
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

                if (_ResultPanel.XAxis.Scale.Max > 100)
                {
                    _ResultPanel.XAxis.Scale.Max = ((int)_ResultPanel.XAxis.Scale.Max / 10) * 10 + 10;
                    _ResultPanel.XAxis.Scale.Min = 0;
                }
                _ResultPanel.XAxis.Scale.MajorStep = (_ResultPanel.XAxis.Scale.Max - _ResultPanel.XAxis.Scale.Min) / 5;
                _ResultPanel.XAxis.Scale.MinorStep = _ResultPanel.XAxis.Scale.MajorStep / 5;
            }

            if (this.zedGraphControl.GraphPane.YAxis != null)
            {
                Scale sScale = _ResultPanel.YAxis.Scale;
                switch (_ShowY)
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
                        //if (m_YingLi > sScale.Max)
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

                if (_ResultPanel.YAxis.Scale.Max > 100)
                {
                    _ResultPanel.YAxis.Scale.Max = ((int)_ResultPanel.YAxis.Scale.Max / 10) * 10 + 10;
                    _ResultPanel.YAxis.Scale.Min = 0;
                }

                _ResultPanel.YAxis.Scale.MajorStep = (_ResultPanel.YAxis.Scale.Max - _ResultPanel.YAxis.Scale.Min) / 5;
                _ResultPanel.YAxis.Scale.MinorStep = _ResultPanel.YAxis.Scale.MajorStep / 5;
            }
        }

        //格式化显示值
        public string FloatDisplay(Int32 Number, UInt16 Scale, UInt32 Resolution, byte FlagLFE)
        {
            string strDisp;
            double floatnumber;
            UInt32 _scale = 1;
            UInt16 _e = 0;
            UInt16 Resolution_E = 0;
            int DotValue = 0;
            int SigValue = 0;

            //量程指数
            _e = (UInt16)(Scale & 0x000f);
            //量程基数 
            SigValue = (UInt16)(Scale >> 8);
            _scale = (uint)SigValue * (uint)Math.Pow(10.0, _e);

            //分辨率
            //m_Resolution = Resolution;

            //采集的代码
            floatnumber = Number;

            //0x01位移 0x02负荷 0x03变形 0x04大变形
            //如果是负荷 或 变形 ，需要进行纲量变换
            if ((FlagLFE == 0x02) || (FlagLFE == 0x03))
            {
                floatnumber = floatnumber * _scale / Resolution;
            }

            //分辨率的指数
            Resolution_E = (UInt16)(Resolution.ToString().Length - 1);

            //如果是负荷和位移,小数点位置算法
            if ((FlagLFE == 0x02) || (FlagLFE == 0x03))
            {
                //0x01位移 0x02负荷 0x03变形 0x04大变形

                DotValue = Resolution_E - _e;//分辨率指数 减去 量程指数
                //如果 量程首位小于分辨率 / 10的分辨率指数次方
                if (SigValue < (Resolution / Math.Pow(10.0, Resolution_E)))
                {
                    if (DotValue >= 0)
                    {
                        DotValue = DotValue + 1;
                    }
                }
            }
            else
            {
                DotValue = 0;
            }

            //如果采集代码的绝对值>=1000 
            if (Math.Abs(floatnumber) >= 1000)
            {
                //采集代码除以1000
                floatnumber = (double)(floatnumber / 1000.0);
                //小数点位数加3
                DotValue = DotValue + 3;
            }

            if (DotValue < 0)
            {
                DotValue = 0;
            }

            string strFormat = "{0,6:f" + DotValue.ToString() + "}";

            strDisp = string.Format(strFormat, floatnumber);
            if (strDisp.Length > 6)
                strDisp = strDisp.Substring(0, 6);

            if (strDisp.Contains("."))
            {
                if (strDisp.IndexOf(".") == 5)
                    strDisp = strDisp.Substring(0, 5);
            }
            return strDisp;
        }


        async Task TskSendOrder()
        {
            //Test
            dt = DateTime.Now;
            while (_showThreadFlag)
            {
                await Task.Delay(40);
                int ret;
                int offset = 0;
                int len = 2;
                int m_lvalue = 0;
                int m_dvalue = 0;
                int m_evalue = 0;
                int m_tvalue = 0;
                int m_valueS = 0;
                int m_index = 0;
                if ((m_SensorArrayFlag == 1) && (m_SensorCount != 0))
                {
                    buf[0] = 0x03;									                //命令字节
                    buf[1] = Convert.ToByte(offset / 256);							//偏移量
                    buf[2] = Convert.ToByte(offset % 256);
                    buf[3] = Convert.ToByte(len / 256);								//每次读的长度
                    buf[4] = Convert.ToByte(len % 256);

                    ret = RwUsb.WriteData1582(1, buf, 5, 1000);				        //发送读命令

                    len = (m_SensorCount + 2) * 4;

                    ret = RwUsb.ReadData1582(4, buf, len, 1000);				    //读数据

                    //0x01位移 0x02负荷 0x03变形 0x04大变形

                    /*---------------------m_Load display----------------*/

                    if (m_LoadSensorCount != 0)
                    {
                        m_index = m_LSensorArray[0].SensorIndex * 4 + 3;
                        m_lvalue = buf[m_index];

                        m_lvalue = m_lvalue << 8;
                        m_index = m_LSensorArray[0].SensorIndex * 4 + 2;
                        m_lvalue |= buf[m_index];

                        m_lvalue = m_lvalue << 8;
                        m_index = m_LSensorArray[0].SensorIndex * 4 + 1;
                        m_lvalue |= buf[m_index];

                        m_lvalue = m_lvalue << 8;
                        m_index = m_LSensorArray[0].SensorIndex * 4 + 0;
                        m_lvalue |= buf[m_index];
                        //如果是单空间和试验为压缩或弯曲试验 GBT228-2010
                        if (m_machineType == "0" && (m_testType == "compress" || m_testType == "bend" || m_testType == "shear" || m_testType == "twist"))
                            m_lvalue = -m_lvalue;
                        m_ppreload = m_Load;
                        m_Load = (float)(m_lvalue * m_LoadResolutionValue);
                        if (m_CtrlCommandArray[0].m_StopPointType == 0x81)//如果I一阶段为负荷停止点
                        {
                            //转换点时记录数据
                            if (m_Load >= m_CtrlCommandArray[0].m_StopPoint * m_LoadResolutionValue && m_pcount == 0)
                                m_isaddcount = true;
                            if (m_isaddcount)
                            {
                                if (m_CtrlCommandArray[0].m_CtrlSpeed > 0 && m_CtrlCommandArray[1].m_CtrlSpeed > 0)
                                {
                                    m_pcount++;
                                    if (m_pcount > 10)
                                        m_isaddcount = false;
                                    if (m_Load < m_ppreload)
                                        m_Load = m_ppreload;
                                }
                            }
                        }
                        this.BeginInvoke(new Action(() =>
                            {
                                this.lblFShow.Text = FloatDisplay(m_lvalue, (ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale, m_Resolution, 0x02);
                                this.lblFShow.Refresh();
                            }));

                    }

                    /*---------------------m_Displacement display----------------*/

                    if (m_DisplacementSensorCount != 0)
                    {
                        m_index = m_DSensorArray[0].SensorIndex * 4 + 3;
                        m_dvalue = buf[m_index];

                        m_dvalue = m_dvalue << 8;
                        m_index = m_DSensorArray[0].SensorIndex * 4 + 2;
                        m_dvalue |= buf[m_index];

                        m_dvalue = m_dvalue << 8;
                        m_index = m_DSensorArray[0].SensorIndex * 4 + 1;
                        m_dvalue |= buf[m_index];

                        m_dvalue = m_dvalue << 8;
                        m_index = m_DSensorArray[0].SensorIndex * 4 + 0;
                        m_dvalue |= buf[m_index];

                        if (m_machineType == "0" && (m_testType == "compress" || m_testType == "bend" || m_testType == "shear" || m_testType == "twist"))
                            m_dvalue = -m_dvalue;
                        //记录前一点位移
                        m_ppredis = m_Displacement;
                        //位移
                        m_Displacement = (float)(m_dvalue);
                        if (m_CtrlCommandArray[0].m_StopPointType == 0x80)//如果I一阶段为位移停止点
                        {
                            //转换点时记录数据
                            if (m_Displacement >= m_CtrlCommandArray[0].m_StopPoint && m_pcount == 0)
                                m_isaddcount = true;
                            if (m_isaddcount)
                            {
                                if (m_CtrlCommandArray[0].m_CtrlSpeed > 0 & m_CtrlCommandArray[1].m_CtrlSpeed > 0)
                                {
                                    m_pcount++;
                                    if (m_pcount > 10)
                                        m_isaddcount = false;
                                    //if (m_Displacement < m_ppredis)
                                    //    m_Displacement = m_ppredis;
                                    if (m_Load < m_ppreload)
                                        m_Load = m_ppreload;
                                }
                            }
                        }
                        this.BeginInvoke(new Action(() =>
                            {
                                this.lblDShow.Text = FloatDisplay(m_dvalue, (ushort)m_SensorArray[m_DSensorArray[0].SensorIndex].scale, m_Resolution, 0x01);
                                this.lblDShow.Refresh();
                            }));
                    }

                    /*---------------------elongate display----------------*/

                    if (m_ElongateSensorCount != 0)
                    {
                        m_index = m_ESensorArray[0].SensorIndex * 4 + 3;
                        m_evalue = buf[m_index];

                        m_evalue = m_evalue << 8;
                        m_index = m_ESensorArray[0].SensorIndex * 4 + 2;
                        m_evalue |= buf[m_index];

                        m_evalue = m_evalue << 8;
                        m_index = m_ESensorArray[0].SensorIndex * 4 + 1;
                        m_evalue |= buf[m_index];

                        m_evalue = m_evalue << 8;
                        m_index = m_ESensorArray[0].SensorIndex * 4 + 0;
                        m_evalue |= buf[m_index];

                        if (m_machineType == "0" && (m_testType == "compress" || m_testType == "bend" || m_testType == "shear" || m_testType == "twist"))
                            m_evalue = -m_evalue;

                        //变形
                        m_Elongate = (float)(m_evalue * m_ElongateResolutionValue);

                        if (m_ESensorArray.Length>1)
                        {
                            //变形2
                            m_index = m_ESensorArray[1].SensorIndex * 4 + 3;
                            m_evalue = buf[m_index];

                            m_evalue = m_evalue << 8;
                            m_index = m_ESensorArray[1].SensorIndex * 4 + 2;
                            m_evalue |= buf[m_index];

                            m_evalue = m_evalue << 8;
                            m_index = m_ESensorArray[1].SensorIndex * 4 + 1;
                            m_evalue |= buf[m_index];

                            m_evalue = m_evalue << 8;
                            m_index = m_ESensorArray[1].SensorIndex * 4 + 0;
                            m_evalue |= buf[m_index];

                            if (m_machineType == "0" && (m_testType == "compress" || m_testType == "bend" || m_testType == "shear" || m_testType == "twist"))
                                m_evalue = -m_evalue;
                            //变形
                            m_Elongate1 = (float)(m_evalue * m_ElongateResolutionValue);
                        }
                      

                        if (!isTest)
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                this.lblBXShow.Text = FloatDisplay(m_evalue, (ushort)m_SensorArray[m_ESensorArray[0].SensorIndex].scale, m_Resolution, 0x03);
                                this.lblBXShow.Invalidate();
                                this.lblBXShow2.Text = FloatDisplay(m_evalue, (ushort)m_SensorArray[m_ESensorArray[1].SensorIndex].scale, m_Resolution, 0x03);
                                this.lblBXShow2.Invalidate();
                            }));
                        }
                    }

                    /*-------------------time display-----------------------*/
                    m_valueS = buf[m_SensorCount * 4 + 2];     //+2
                    m_valueS = m_valueS << 4;
                    m_tvalue = buf[m_SensorCount * 4 + 1] & 0x0f0;//+1
                    m_tvalue = m_tvalue >> 4;
                    m_valueS |= (m_tvalue & 0x0f);
                    m_tvalue = buf[m_SensorCount * 4 + 1] & 0x0f;  //+1
                    m_tvalue = m_tvalue << 8;
                    m_tvalue |= buf[m_SensorCount * 4 + 0];      //+0
                    m_Time = (float)((m_valueS * 1000.0f + m_tvalue) / 1000.0f);
                }

                //Test  
                m_Load += 0.3f;
                m_Time = (float)(DateTime.Now - dt).TotalSeconds;
                m_Displacement += 0.128f;
                m_Elongate = 2 * m_Load;
                m_YingBian = (float)(m_Elongate / m_Lt) * 100;
                m_YingLi = (float)Math.Log(m_Load);

                if (isTest)
                {
                    gdata gd = new gdata();

                    #region 如果使用引申计
                    if (_useExten)
                    {
                        switch (m_extenType)
                        {
                            case 0://% 应变
                                if (m_YingBian >= m_extenValue)
                                {
                                    //取引伸计暂停时的值
                                    SendPauseTest();
                                    _useExten = false;
                                    m_holdPause = true;
                                    m_holdContinue = false;
                                    this.BeginInvoke(new Action(() =>
                            {
                                btnZeroS.Enabled = false;
                                //弹出取引伸计框
                                m_fh.Show();
                                m_fh.TopMost = true;
                            }));
                                }
                                break;
                            case 1://mm 变形
                                if (m_Elongate >= m_extenValue)
                                {
                                    SendPauseTest();
                                    _useExten = false;
                                    m_holdPause = true;
                                    m_holdContinue = false;

                                    this.BeginInvoke(new Action(() =>
                            {
                                btnZeroS.Enabled = false;
                                m_fh.Show();
                                m_fh.TopMost = true;
                            }));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    #endregion

                    #region 当取引伸计 暂停时
                    if (m_pause && m_holdPause)
                    {
                        if (m_Load < m_hold_data.F1)
                            m_Load = (float)m_hold_data.F1;
                        m_Elongate = (float)m_hold_data.BX1;
                    }
                    #endregion

                    #region 当取引申计后 , 保持
                    if (m_holdContinue)
                    {
                        if (m_Load <= m_hold_data.F1)
                            m_Load = (float)m_hold_data.F1;
                        if (m_Load > m_hold_data.F1)
                            m_hold_data.F1 = 0;
                        m_Elongate = m_Displacement - (float)m_hold_data.D1 + (float)m_hold_data.BX1;
                    }
                    #endregion


                    if (m_ElongateResolutionValue != 0)
                    {
                        this.BeginInvoke(new Action(() =>
                            {
                                this.lblBXShow.Text = FloatDisplay((int)(m_Elongate / m_ElongateResolutionValue), (ushort)m_SensorArray[m_ESensorArray[0].SensorIndex].scale, m_Resolution, 0x03);
                                this.lblBXShow.Refresh();
                            }));
                    }

                    gd.F1 = (float)Math.Round(m_Load, 3);
                    gd.F2 = 0;
                    gd.F3 = 0;
                    gd.D1 = (float)Math.Round(m_Displacement, 3);
                    gd.D2 = 0;
                    gd.D3 = 0;
                    gd.BX1 = (float)Math.Round(m_Elongate, 3);
                    gd.BX2 = 0;
                    gd.BX3 = 0;
                    gd.YL1 = (float)Math.Round(m_YingLi, 3);
                    gd.YL2 = 0;
                    gd.YL3 = 0;
                    gd.YB1 = (float)Math.Round(m_YingBian, 6);
                    gd.YB2 = 0;
                    gd.YB3 = 0;
                    gd.Ts = (float)Math.Round(m_Time, 3);

                    //_List_Data.Add(gd);
                    _List_Testing_Data.Add(gd);

                    //自动存储线程                                    
                    if (_List_Testing_Data.Count >= 500)
                    {
                        gdata[] temp = new gdata[500];
                        _List_Testing_Data.CopyTo(temp);
                        _List_Testing_Data.RemoveRange(0, 500);
                        if (_threadSaveData == null)
                        {
                            _threadSaveData = new Thread(new ParameterizedThreadStart(SaveCurveData));
                            _threadSaveData.IsBackground = true;
                            _threadSaveData.Start(temp);
                            _threadSaveData.Join();
                            _threadSaveData = null;
                        }
                    }

                    //存储前一点的值
                    m_Fn = m_F;
                    //实时得值
                    m_F = m_Load;

                    //当力超过传感器量程的1/200,启动判断
                    if (m_CheckStop)
                    {
                        if (m_F > m_CheckStopValue)
                        {
                            m_Check = true;
                            m_CheckStop = false;
                        }
                    }

                    //自动停止
                    if (m_F < Math.Abs(m_Fn) * (m_StopValue / 100.0) && m_Check)
                    {
                        isTest = false;
                        SendStopTest();
                        this.BeginInvoke(new Action(() =>
                            {
                                tsbtn_Stop_Click(tsbtn_Stop, new EventArgs());
                            }));
                    }

                    //获取自定义试验的最后一条命令
                    //如果是位移控制的停止点
                    //Test屏蔽
                    //if (m_CtrlCommandList[m_CtrlCommandList.Count - 1].m_StopPointType == 0x80)
                    //{
                    //    if (m_Displacement >= m_CtrlCommandList[m_CtrlCommandList.Count - 1].m_StopPoint)
                    //    {
                    //        isTest = false;
                    //        tsbtn_Stop_Click(tsbtn_Stop, new EventArgs());
                    //    }
                    //}
                    ////如果是负荷停止点
                    //if (m_CtrlCommandList[m_CtrlCommandList.Count - 1].m_StopPointType == 0x81)
                    //{
                    //    if (m_Load >= m_CtrlCommandList[m_CtrlCommandList.Count - 1].m_StopPoint)
                    //    {
                    //        isTest = false;
                    //        tsbtn_Stop_Click(tsbtn_Stop, new EventArgs());
                    //    }
                    //}

                    //应力
                    if (m_S0 != 0)
                        m_YingLi = m_Load / m_S0;
                    else
                        m_YingLi = 0;

                    //应变 百分比
                    if (m_Le != 0)
                        m_YingBian = m_Elongate / (m_Le * 10.0f);
                    else
                        m_YingBian = 0;
                }

                this.BeginInvoke(
                    new Action(() =>
                                {
                                    if (Math.Abs(m_Load) >= 1000)
                                    {
                                        lblkN.Text = "kN";
                                        lblkN.Refresh();
                                    }
                                    else
                                    {
                                        lblkN.Text = "N";
                                        lblkN.Refresh();
                                    }

                                    //位移
                                    if (Math.Abs(m_Displacement) >= 1000.0)
                                    {
                                        lblmm1.Text = "mm";
                                        lblmm1.Refresh();
                                    }
                                    else
                                    {
                                        lblmm1.Text = "μm";
                                        lblmm1.Refresh();
                                    }

                                    //变形
                                    if (Math.Abs(m_Elongate) > 1000 && lblBXShow.Visible == true)
                                    {
                                        lblmm2.Text = "mm";
                                        lblmm2.Refresh();
                                    }
                                    else
                                    {
                                        lblmm2.Text = "μm";
                                        lblmm2.Refresh();
                                    }

                                    //应变
                                    lblYBShow.Text = m_YingBian.ToString("f4");
                                    lblYBShow.Refresh();

                                    //应力
                                    lblYLShow.Text = m_YingLi.ToString("f2");
                                    lblYLShow.Refresh();

                                    //时间
                                    lblTimeShow.Text = m_Time.ToString("f2");
                                    lblTimeShow.Refresh();
                                }));
            }
        }

        public void ThreadSendOrder()
        {
            Task t = TskSendOrder();
            //new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;
            //    Thread.CurrentThread.Priority = ThreadPriority.Normal;
            //    while (_showThreadFlag)
            //    {
            //        //采集频率 40ms
            //        Thread.Sleep(40);
            //        BeginInvoke(new Action(() =>
            //        {
            //            lock (m_state)
            //            {
            //                int ret;
            //                int offset = 0;
            //                int len = 2;
            //                int m_lvalue = 0;
            //                int m_dvalue = 0;
            //                int m_evalue = 0;
            //                int m_tvalue = 0;
            //                int m_valueS = 0;
            //                int m_index = 0;
            //                if ((m_SensorArrayFlag == 1) && (m_SensorCount != 0))
            //                {
            //                    buf[0] = 0x03;									                //命令字节
            //                    buf[1] = Convert.ToByte(offset / 256);							//偏移量
            //                    buf[2] = Convert.ToByte(offset % 256);
            //                    buf[3] = Convert.ToByte(len / 256);								//每次读的长度
            //                    buf[4] = Convert.ToByte(len % 256);

            //                    ret = RwUsb.WriteData1582(1, buf, 5, 1000);				        //发送读命令

            //                    len = (m_SensorCount + 2) * 4;

            //                    ret = RwUsb.ReadData1582(4, buf, len, 1000);				    //读数据

            //                    //0x01位移 0x02负荷 0x03变形 0x04大变形

            //                    /*---------------------m_Load display----------------*/

            //                    if (m_LoadSensorCount != 0)
            //                    {
            //                        m_index = m_LSensorArray[0].SensorIndex * 4 + 3;
            //                        m_lvalue = buf[m_index];

            //                        m_lvalue = m_lvalue << 8;
            //                        m_index = m_LSensorArray[0].SensorIndex * 4 + 2;
            //                        m_lvalue |= buf[m_index];

            //                        m_lvalue = m_lvalue << 8;
            //                        m_index = m_LSensorArray[0].SensorIndex * 4 + 1;
            //                        m_lvalue |= buf[m_index];

            //                        m_lvalue = m_lvalue << 8;
            //                        m_index = m_LSensorArray[0].SensorIndex * 4 + 0;
            //                        m_lvalue |= buf[m_index];
            //                        //如果是单空间和试验为压缩或弯曲试验 GBT228-2010
            //                        if (m_machineType == "0" && (m_testType == "compress" || m_testType == "bend" || m_testType == "shear" || m_testType == "twist"))
            //                            m_lvalue = -m_lvalue;
            //                        m_ppreload = m_Load;
            //                        m_Load = (double)(m_lvalue * m_LoadResolutionValue);
            //                        if (m_CtrlCommandArray[0].m_StopPointType == 0x81)//如果I一阶段为负荷停止点
            //                        {
            //                            //转换点时记录数据
            //                            if (m_Load >= m_CtrlCommandArray[0].m_StopPoint * m_LoadResolutionValue && m_pcount == 0)
            //                                m_isaddcount = true;
            //                            if (m_isaddcount)
            //                            {
            //                                if (m_CtrlCommandArray[0].m_CtrlSpeed > 0 && m_CtrlCommandArray[1].m_CtrlSpeed > 0)
            //                                {
            //                                    m_pcount++;
            //                                    if (m_pcount > 10)
            //                                        m_isaddcount = false;
            //                                    if (m_Load < m_ppreload)
            //                                        m_Load = m_ppreload;
            //                                }
            //                            }
            //                        }

            //                        this.lblFShow.Text = FloatDisplay(m_lvalue, (ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale, m_Resolution, 0x02);
            //                        this.lblFShow.Refresh();

            //                    }

            //                    /*---------------------m_Displacement display----------------*/

            //                    if (m_DisplacementSensorCount != 0)
            //                    {
            //                        m_index = m_DSensorArray[0].SensorIndex * 4 + 3;
            //                        m_dvalue = buf[m_index];

            //                        m_dvalue = m_dvalue << 8;
            //                        m_index = m_DSensorArray[0].SensorIndex * 4 + 2;
            //                        m_dvalue |= buf[m_index];

            //                        m_dvalue = m_dvalue << 8;
            //                        m_index = m_DSensorArray[0].SensorIndex * 4 + 1;
            //                        m_dvalue |= buf[m_index];

            //                        m_dvalue = m_dvalue << 8;
            //                        m_index = m_DSensorArray[0].SensorIndex * 4 + 0;
            //                        m_dvalue |= buf[m_index];

            //                        if (m_machineType == "0" && (m_testType == "compress" || m_testType == "bend" || m_testType == "shear" || m_testType == "twist"))
            //                            m_dvalue = -m_dvalue;
            //                        //记录前一点位移
            //                        m_ppredis = m_Displacement;
            //                        //位移
            //                        m_Displacement = (double)(m_dvalue);
            //                        if (m_CtrlCommandArray[0].m_StopPointType == 0x80)//如果I一阶段为位移停止点
            //                        {
            //                            //转换点时记录数据
            //                            if (m_Displacement >= m_CtrlCommandArray[0].m_StopPoint && m_pcount == 0)
            //                                m_isaddcount = true;
            //                            if (m_isaddcount)
            //                            {
            //                                if (m_CtrlCommandArray[0].m_CtrlSpeed > 0 & m_CtrlCommandArray[1].m_CtrlSpeed > 0)
            //                                {
            //                                    m_pcount++;
            //                                    if (m_pcount > 10)
            //                                        m_isaddcount = false;
            //                                    //if (m_Displacement < m_ppredis)
            //                                    //    m_Displacement = m_ppredis;
            //                                    if (m_Load < m_ppreload)
            //                                        m_Load = m_ppreload;
            //                                }
            //                            }
            //                        }
            //                        this.lblDShow.Text = FloatDisplay(m_dvalue, (ushort)m_SensorArray[m_DSensorArray[0].SensorIndex].scale, m_Resolution, 0x01);
            //                        this.lblDShow.Refresh();
            //                    }

            //                    /*---------------------elongate display----------------*/

            //                    if (m_ElongateSensorCount != 0)
            //                    {
            //                        m_index = m_ESensorArray[0].SensorIndex * 4 + 3;
            //                        m_evalue = buf[m_index];

            //                        m_evalue = m_evalue << 8;
            //                        m_index = m_ESensorArray[0].SensorIndex * 4 + 2;
            //                        m_evalue |= buf[m_index];

            //                        m_evalue = m_evalue << 8;
            //                        m_index = m_ESensorArray[0].SensorIndex * 4 + 1;
            //                        m_evalue |= buf[m_index];

            //                        m_evalue = m_evalue << 8;
            //                        m_index = m_ESensorArray[0].SensorIndex * 4 + 0;
            //                        m_evalue |= buf[m_index];

            //                        if (m_machineType == "0" && (m_testType == "compress" || m_testType == "bend" || m_testType == "shear" || m_testType == "twist"))
            //                            m_evalue = -m_evalue;

            //                        //变形
            //                        m_Elongate = (double)(m_evalue * m_ElongateResolutionValue);

            //                        if (!isTest)
            //                        {
            //                            this.lblBXShow.Text = FloatDisplay(m_evalue, (ushort)m_SensorArray[m_ESensorArray[0].SensorIndex].scale, m_Resolution, 0x03);
            //                            this.lblBXShow.Refresh();
            //                        }
            //                    }

            //                    /*-------------------time display-----------------------*/
            //                    m_valueS = buf[m_SensorCount * 4 + 2];     //+2
            //                    m_valueS = m_valueS << 4;
            //                    m_tvalue = buf[m_SensorCount * 4 + 1] & 0x0f0;//+1
            //                    m_tvalue = m_tvalue >> 4;
            //                    m_valueS |= (m_tvalue & 0x0f);
            //                    m_tvalue = buf[m_SensorCount * 4 + 1] & 0x0f;  //+1
            //                    m_tvalue = m_tvalue << 8;
            //                    m_tvalue |= buf[m_SensorCount * 4 + 0];      //+0
            //                    m_Time = (double)((m_valueS * 1000.0f + m_tvalue) / 1000.0f);
            //                }

            //                //Test  
            //                m_Load += 0.3f;
            //                m_Time = (double)(DateTime.Now - dt).TotalSeconds;
            //                m_Displacement += 0.128f;
            //                m_Elongate = 2 * m_Load;
            //                m_YingBian = (double)(m_Elongate / m_Lt) * 100;
            //                m_YingLi = (double)Math.Log(m_Load);

            //                if (isTest)
            //                {
            //                    gdata gd = new gdata();

            //                    #region 如果使用引申计
            //                    if (_useExten)
            //                    {
            //                        switch (m_extenType)
            //                        {
            //                            case 0://% 应变
            //                                if (m_YingBian >= m_extenValue)
            //                                {
            //                                    //取引伸计暂停时的值
            //                                    SendPauseTest();
            //                                    //m_hold_data.BX1 = m_Elongate;
            //                                    //m_hold_data.D1 = m_Displacement;
            //                                    //m_hold_data.F1 = m_Load;
            //                                    _useExten = false;
            //                                    m_holdPause = true;
            //                                    m_holdContinue = false;
            //                                    btnZeroS.Enabled = false;
            //                                    //弹出取引伸计框
            //                                    //Thread.Sleep(50);
            //                                    m_fh.Show();
            //                                    m_fh.TopMost = true;
            //                                }
            //                                break;
            //                            case 1://mm 变形
            //                                if (m_Elongate >= m_extenValue)
            //                                {
            //                                    SendPauseTest();
            //                                    //取引伸计暂停时的值
            //                                    //m_hold_data.BX1 = m_Elongate;
            //                                    //m_hold_data.D1 = m_Displacement;
            //                                    //m_hold_data.F1 = m_Load;
            //                                    _useExten = false;
            //                                    m_holdPause = true;
            //                                    m_holdContinue = false;
            //                                    btnZeroS.Enabled = false;
            //                                    //弹出取引伸计框
            //                                    //Thread.Sleep(50);
            //                                    m_fh.Show();
            //                                    m_fh.TopMost = true;
            //                                }
            //                                break;
            //                            default:
            //                                break;
            //                        }
            //                    }
            //                    #endregion

            //                    #region 当取引伸计 暂停时
            //                    if (m_pause && m_holdPause)
            //                    {
            //                        if (m_Load < m_hold_data.F1)
            //                            m_Load = (double)m_hold_data.F1;
            //                        m_Elongate = (double)m_hold_data.BX1;
            //                    }
            //                    #endregion

            //                    #region 当取引申计后 , 保持
            //                    if (m_holdContinue)
            //                    {
            //                        if (m_Load <= m_hold_data.F1)
            //                            m_Load = (double)m_hold_data.F1;
            //                        if (m_Load > m_hold_data.F1)
            //                            m_hold_data.F1 = 0;
            //                        m_Elongate = m_Displacement - (double)m_hold_data.D1 + (double)m_hold_data.BX1;
            //                    }
            //                    #endregion


            //                    if (m_ElongateResolutionValue != 0)
            //                    {
            //                        this.lblBXShow.Text = FloatDisplay((int)(m_Elongate / m_ElongateResolutionValue), (ushort)m_SensorArray[m_ESensorArray[0].SensorIndex].scale, m_Resolution, 0x03);
            //                        this.lblBXShow.Refresh();
            //                    }

            //                    gd.F1 = (double)Math.Round(m_Load, 3);
            //                    gd.F2 = 0;
            //                    gd.F3 = 0;
            //                    gd.D1 = (double)Math.Round(m_Displacement, 3);
            //                    gd.D2 = 0;
            //                    gd.D3 = 0;
            //                    gd.BX1 = (double)Math.Round(m_Elongate, 3);
            //                    gd.BX2 = 0;
            //                    gd.BX3 = 0;
            //                    gd.YL1 = (double)Math.Round(m_YingLi, 3);
            //                    gd.YL2 = 0;
            //                    gd.YL3 = 0;
            //                    gd.YB1 = (double)Math.Round(m_YingBian, 6);
            //                    gd.YB2 = 0;
            //                    gd.YB3 = 0;
            //                    gd.Ts = (double)Math.Round(m_Time, 3);

            //                    //_List_Data.Add(gd);
            //                    _List_Testing_Data.Add(gd);

            //                    //自动存储线程                                    
            //                    if (_List_Testing_Data.Count >= 500)
            //                    {
            //                        gdata[] temp = new gdata[500];
            //                        _List_Testing_Data.CopyTo(temp);
            //                        _List_Testing_Data.RemoveRange(0, 500);
            //                        if (_threadSaveData == null)
            //                        {
            //                            _threadSaveData = new Thread(new ParameterizedThreadStart(SaveCurveData));
            //                            _threadSaveData.IsBackground = true;
            //                            _threadSaveData.Start(temp);
            //                            //while (_threadSaveData.IsAlive) ;
            //                            //Thread.Sleep(20);
            //                            _threadSaveData.Join();
            //                            _threadSaveData = null;
            //                        }
            //                    }

            //                    //存储前一点的值
            //                    m_Fn = m_F;
            //                    //实时得值
            //                    m_F = m_Load;

            //                    //当力超过传感器量程的1/200,启动判断
            //                    if (m_CheckStop)
            //                    {
            //                        if (m_F > m_CheckStopValue)
            //                        {
            //                            m_Check = true;
            //                            m_CheckStop = false;
            //                        }
            //                    }

            //                    //自动停止
            //                    if (m_F < Math.Abs(m_Fn) * (m_StopValue / 100.0) && m_Check)
            //                    {
            //                        isTest = false;
            //                        SendStopTest();
            //                        tsbtn_Stop_Click(tsbtn_Stop, new EventArgs());
            //                    }

            //                    //获取自定义试验的最后一条命令
            //                    //如果是位移控制的停止点
            //                    //Test屏蔽
            //                    //if (m_CtrlCommandList[m_CtrlCommandList.Count - 1].m_StopPointType == 0x80)
            //                    //{
            //                    //    if (m_Displacement >= m_CtrlCommandList[m_CtrlCommandList.Count - 1].m_StopPoint)
            //                    //    {
            //                    //        isTest = false;
            //                    //        tsbtn_Stop_Click(tsbtn_Stop, new EventArgs());
            //                    //    }
            //                    //}
            //                    ////如果是负荷停止点
            //                    //if (m_CtrlCommandList[m_CtrlCommandList.Count - 1].m_StopPointType == 0x81)
            //                    //{
            //                    //    if (m_Load >= m_CtrlCommandList[m_CtrlCommandList.Count - 1].m_StopPoint)
            //                    //    {
            //                    //        isTest = false;
            //                    //        tsbtn_Stop_Click(tsbtn_Stop, new EventArgs());
            //                    //    }
            //                    //}

            //                    //应力
            //                    if (m_S0 != 0)
            //                        m_YingLi = m_Load / m_S0;
            //                    else
            //                        m_YingLi = 0;

            //                    //应变 百分比
            //                    if (m_Le != 0)
            //                        m_YingBian = m_Elongate / (m_Le * 10.0f);
            //                    else
            //                        m_YingBian = 0;
            //                }
            //            }

            //            if (Math.Abs(m_Load) >= 1000)
            //            {
            //                lblkN.Text = "kN";
            //                lblkN.Refresh();
            //            }
            //            else
            //            {
            //                lblkN.Text = "N";
            //                lblkN.Refresh();
            //            }

            //            //位移
            //            if (Math.Abs(m_Displacement) >= 1000.0)
            //            {
            //                lblmm1.Text = "mm";
            //                lblmm1.Refresh();
            //            }
            //            else
            //            {
            //                lblmm1.Text = "μm";
            //                lblmm1.Refresh();
            //            }

            //            //变形
            //            if (Math.Abs(m_Elongate) > 1000 && lblBXShow.Visible == true)
            //            {
            //                lblmm2.Text = "mm";
            //                lblmm2.Refresh();
            //            }
            //            else
            //            {
            //                lblmm2.Text = "μm";
            //                lblmm2.Refresh();
            //            }

            //            //应变
            //            lblYBShow.Text = m_YingBian.ToString("f4");
            //            lblYBShow.Refresh();

            //            //应力
            //            lblYLShow.Text = m_YingLi.ToString("f2");
            //            lblYLShow.Refresh();

            //            //时间
            //            lblTimeShow.Text = m_Time.ToString("f2");
            //            lblTimeShow.Refresh();

            //        }));
            //    }
            //}).Start();
        }

        private void ReadCurveSet()
        {
            _X1 = int.Parse(RWconfig.GetAppSettings("X1"));
            _X2 = int.Parse(RWconfig.GetAppSettings("X2"));
            _Y1 = int.Parse(RWconfig.GetAppSettings("Y1"));
            _Y2 = int.Parse(RWconfig.GetAppSettings("Y2"));
            _ShowY = int.Parse(RWconfig.GetAppSettings("ShowY"));
            _ShowX = int.Parse(RWconfig.GetAppSettings("ShowX"));
        }

        private void CreateFolder()
        {
            if (!Directory.Exists(@"E:\衡新试验数据\Curve"))
            {
                Directory.CreateDirectory(@"E:\衡新试验数据\Curve");
            }
        }

        public void GetTestValue()
        {
            if (m_SensorArray != null && m_LSensorArray != null && m_ESensorArray != null && m_SensorCount > 0)
            {
                m_LoadResolutionValue = GetSensorSR((ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale);// 3 * ((ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale) / m_Resolution;
                m_ElongateResolutionValue = GetSensorSR((ushort)m_SensorArray[m_ESensorArray[0].SensorIndex].scale);
                //Test
                //MessageBox.Show("m_ElongateResolutionValue:"+m_ElongateResolutionValue.ToString()); 
                m_minLoad = Convert.ToSingle(RWconfig.GetAppSettings("minLoad"));
                m_CheckStopValue = GetScale((ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale) * m_minLoad / 100.0f;
                m_machineType = RWconfig.GetAppSettings("machineType");
            }
        }

        private void frmTestResult_Load(object sender, EventArgs e)
        {
            //标志力值 分辨率的值
            GetTestValue();
            CreateAverageView_Default(this.dataGridViewSum, "-");
            buf = new byte[203];
            this.zedGraphControl.Invalidated += new InvalidateEventHandler(zedGraphControl_Invalidated);
            this.pbChart.Invalidated += new InvalidateEventHandler(pbChart_Invalidated);
            initResultCurve(this.zedGraphControl);
            ShowResultPanel();
            ShowResultCurve();
            TestStandard.GBT228_2010.readFinishSample(this.dataGridView, this.dataGridViewSum, "", this.dateTimePicker, this.zedGraphControl);
            CreateFolder();
            m_fh = new frmHold();
            m_fh.delbtnOKClick += new frmHold.delbtnOK_Click(m_fh_delbtnOKClick);
            //manualReset = new ManualResetEvent(false);
            while (!this.IsHandleCreated)
            {
                ;
            }
        }

        void m_fh_delbtnOKClick(object sender, EventArgs e)
        {
            this.SendPauseTest();
            this.m_holdContinue = true;
            this.m_holdPause = false;
            this.tsbtnYSJ.Enabled = false;
        }

        //显示实时曲线界面
        private void ShowCurvePanel()
        {
            this.tsbtn_Exit.Visible = false;
            this.tsbtnReturn.Visible = true;
            this.toolStrip1.Refresh();
            this.pbChart.Visible = true;
            this.pbChart.Parent = this.panel3;
            this.pbChart.Dock = DockStyle.Fill;
            this.tsbtnSetRealtimeCurve.Visible = true;
            this.splitContainer1.Visible = false;
            this.tsbtnShowResultCurve.Visible = false;
        }

        //显示试验结果界面
        private void ShowResultPanel()
        {
            this.tsbtn_Exit.Visible = true;
            this.tsbtnReturn.Visible = false;
            this.toolStrip1.Refresh();
            this.pbChart.Visible = false;
            this.splitContainer1.Visible = true;
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Parent = this.panel3;
            this.tsbtnSetRealtimeCurve.Visible = false;
            this.tsbtnShowResultCurve.Visible = true;
            this.Refresh();
        }

        private void tsbtn_Exit_Click(object sender, EventArgs e)
        {
            _showThreadFlag = false;
            Thread.Sleep(100);
            this.Hide();
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

            ZedGraph.Legend l = zgControl.GraphPane.Legend;
            l.IsShowLegendSymbols = true;
            l.Gap = 0.5f;
            l.Border = new Border(Color.White, 0f);
            l.Position = LegendPos.TopFlushLeft;
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
            zgControl.ZoomButtons = MouseButtons.None;
            zgControl.ZoomButtons2 = MouseButtons.None;

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
            _ResultPanel.XAxis.Scale.MaxGrace = 0.0;
            _ResultPanel.XAxis.Scale.Min = 0;
            _ResultPanel.XAxis.Scale.MinAuto = false;
            _ResultPanel.XAxis.Scale.Max = 1;
            _ResultPanel.XAxis.Scale.MaxAuto = true;
            _ResultPanel.XAxis.MajorGrid.IsVisible = true;

            _ResultPanel.YAxis.Title.Text = "Y";
            _ResultPanel.YAxis.Title.Gap = -0.5f;
            _ResultPanel.YAxis.Scale.FontSpec.FontColor = Color.FromArgb(34, 67, 108);
            _ResultPanel.YAxis.Title.FontSpec.FontColor = Color.FromArgb(34, 67, 108);
            _ResultPanel.YAxis.Scale.FontSpec.Size = 16.0f;
            _ResultPanel.YAxis.Title.FontSpec.Size = 16.0f;
            _ResultPanel.YAxis.Scale.FontSpec.Family = "宋体";
            _ResultPanel.YAxis.Scale.FontSpec.IsBold = true;
            _ResultPanel.YAxis.Scale.Format = "0.0";
            _ResultPanel.YAxis.Scale.LabelGap = 0;
            // Align the Y2 axis labels so they are flush to the axis 
            _ResultPanel.YAxis.Scale.AlignH = AlignH.Center;
            _ResultPanel.YAxis.Scale.Min = 0;
            _ResultPanel.YAxis.Scale.MinAuto = false;
            _ResultPanel.YAxis.Scale.Max = 1;
            _ResultPanel.YAxis.Scale.MaxAuto = true;
            _ResultPanel.YAxis.MajorGrid.IsVisible = true;

            zgControl.AxisChange();
        }

        ////读取指定日期的所有试验
        //public void ReadSample(TreeView tv)
        //{
        //    tv.Nodes.Clear();

        //    #region 拉伸试验
        //    BLL.TestSample bllTs = new HR_Test.BLL.TestSample();
        //    //查询不重复项
        //    DataSet ds = bllTs.GetNotOverlapList1(" testDate = #" + this.dateTimePicker.Value.Date + "#");
        //    int rCount = ds.Tables[0].Rows.Count;
        //    for (int i = 0; i < rCount; i++)
        //    {
        //        TreeNode tn = new TreeNode();
        //        tn.Text = ds.Tables[0].Rows[i]["testNo"].ToString();
        //        DataSet _ds = bllTs.GetList(" testNo='" + ds.Tables[0].Rows[i]["testNo"].ToString() + "' and testMethodName='" + ds.Tables[0].Rows[i]["testMethodName"].ToString() + "' and testDate=#" + this.dateTimePicker.Value.Date + "#");
        //        tn.Name = "tensile";
        //        tn.ImageIndex = 0;
        //        tv.Nodes.Add(tn);
        //        foreach (DataRow dr in _ds.Tables[0].Rows)
        //        {
        //            if (Convert.ToBoolean(dr["isFinish"].ToString()) == true)
        //            {
        //                //左侧node完成试验的图标
        //                TreeNode ftn = new TreeNode();
        //                ftn.ImageIndex = 1;
        //                ftn.Text = dr["testSampleNo"].ToString();
        //                ftn.Name = "tensile_c";
        //                tv.Nodes[i].Nodes.Add(ftn);
        //            }
        //            else
        //            {
        //                //左侧node未完成试验的图标
        //                //左侧node完成试验的图标
        //                TreeNode ftn = new TreeNode();
        //                ftn.ImageIndex = 2;
        //                ftn.Text = dr["testSampleNo"].ToString();
        //                ftn.Name = "tensile_c";
        //                tv.Nodes[i].Nodes.Add(ftn);
        //            }
        //        }
        //        _ds.Dispose();
        //    }
        //    ds.Dispose();

        //    #endregion

        //    #region 压缩试验
        //    BLL.Compress bllC = new HR_Test.BLL.Compress();
        //    DataSet dsc = bllC.GetNotOverlapList(" testDate = #" + this.dateTimePicker.Value.Date + "#");
        //    int cCount2 = dsc.Tables[0].Rows.Count;
        //    for (int j = 0; j < cCount2; j++)
        //    {
        //        TreeNode tn = new TreeNode();
        //        tn.Text = dsc.Tables[0].Rows[j]["testNo"].ToString();
        //        DataSet _dsc = bllC.GetList(" testNo='" + dsc.Tables[0].Rows[j]["testNo"].ToString() + "' and testDate=#" + this.dateTimePicker.Value.Date + "#");
        //        tn.Name = "compress";
        //        tn.ImageIndex = 0;
        //        tv.Nodes.Add(tn);
        //        foreach (DataRow dr in _dsc.Tables[0].Rows)
        //        {
        //            if (Convert.ToBoolean(dr["isFinish"].ToString()) == true)
        //            {
        //                //左侧node完成试验的图标
        //                TreeNode ftn = new TreeNode();
        //                ftn.ImageIndex = 1;
        //                ftn.Text = dr["testSampleNo"].ToString();
        //                ftn.Name = "compress_c";
        //                tv.Nodes[tv.Nodes.Count - 1].Nodes.Add(ftn);
        //            }
        //            else
        //            {
        //                //左侧node未完成试验的图标 
        //                TreeNode ftn = new TreeNode();
        //                ftn.ImageIndex = 2;
        //                ftn.Text = dr["testSampleNo"].ToString();
        //                ftn.Name = "compress_c";
        //                tv.Nodes[tv.Nodes.Count - 1].Nodes.Add(ftn);
        //            }
        //        }
        //        _dsc.Dispose();
        //    }
        //    dsc.Dispose();
        //    #endregion

        //    #region 弯曲试验
        //    BLL.Bend bllb = new HR_Test.BLL.Bend();
        //    DataSet dsb = bllb.GetNotOverlapList("testDate=#" + this.dateTimePicker.Value.Date + "#");
        //    int rCount3 = dsb.Tables[0].Rows.Count;
        //    for (int j = 0; j < rCount3; j++)
        //    {
        //        TreeNode tn = new TreeNode();
        //        tn.Text = dsb.Tables[0].Rows[j]["testNo"].ToString();
        //        DataSet _dsb = bllb.GetList(" testNo='" + dsb.Tables[0].Rows[j]["testNo"].ToString() + "' and testDate=#" + this.dateTimePicker.Value.Date + "#");
        //        tn.Name = "bend";
        //        tn.ImageIndex = 0;
        //        tv.Nodes.Add(tn);
        //        foreach (DataRow dr in _dsb.Tables[0].Rows)
        //        {
        //            if (Convert.ToBoolean(dr["isFinish"].ToString()) == true)
        //            {
        //                //左侧node完成试验的图标
        //                TreeNode ftn = new TreeNode();
        //                ftn.ImageIndex = 1;
        //                ftn.Text = dr["testSampleNo"].ToString();
        //                ftn.Name = "bend_c";
        //                tv.Nodes[tv.Nodes.Count - 1].Nodes.Add(ftn);
        //            }
        //            else
        //            {
        //                //左侧node未完成试验的图标
        //                //左侧node完成试验的图标
        //                TreeNode ftn = new TreeNode();
        //                ftn.ImageIndex = 2;
        //                ftn.Text = dr["testSampleNo"].ToString();
        //                ftn.Name = "bend_c";
        //                tv.Nodes[tv.Nodes.Count - 1].Nodes.Add(ftn);
        //            }
        //        }
        //        _dsb.Dispose();
        //    }
        //    dsb.Dispose();
        //    #endregion

        //    if (tv.Nodes.Count == 0)
        //        tv.Nodes.Add("无");
        //    //tv.ExpandAll();
        //}

        #region 重写DataGridViewCheckBox列 类
        public class DataGridViewDisableCheckBoxCell : DataGridViewCheckBoxCell
        {
            public bool Enabled { get; set; }

            // Override the Clone method so that the Enabled property is copied.
            public override object Clone()
            {
                DataGridViewDisableCheckBoxCell cell = (DataGridViewDisableCheckBoxCell)base.Clone();
                cell.Enabled = this.Enabled;
                return cell;
            }

            // By default, enable the CheckBox cell.
            public DataGridViewDisableCheckBoxCell()
            {
                this.Enabled = true;
            }

            // Three state checkbox column cell
            protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
                DataGridViewElementStates elementState, object value, object formattedValue, string errorText,
                DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
            {
                // The checkBox cell is disabled, so paint the border, background, and disabled checkBox for the cell.
                if (!this.Enabled)
                {
                    // Draw the cell background, if specified.
                    if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
                    {
                        SolidBrush cellBackground = new SolidBrush(cellStyle.BackColor);
                        graphics.FillRectangle(cellBackground, cellBounds);
                        cellBackground.Dispose();
                    }

                    // Draw the cell borders, if specified.
                    if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
                    {
                        PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
                    }

                    // Calculate the area in which to draw the checkBox.
                    CheckBoxState state = CheckBoxState.MixedDisabled;
                    Size size = CheckBoxRenderer.GetGlyphSize(graphics, state);
                    Point center = new Point(cellBounds.X, cellBounds.Y);
                    center.X += (cellBounds.Width - size.Width) / 2;
                    center.Y += (cellBounds.Height - size.Height) / 2;

                    // Draw the disabled checkBox.
                    CheckBoxRenderer.DrawCheckBox(graphics, center, state);
                }
                else
                {
                    // The checkBox cell is enabled, so let the base class, handle the painting.
                    base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
                }
            }
        }

        public class DataGridViewDisableCheckBoxColumn : DataGridViewCheckBoxColumn
        {
            public DataGridViewDisableCheckBoxColumn()
            {
                this.CellTemplate = new DataGridViewDisableCheckBoxCell();
            }
        }
        #endregion

        //-------------------------------

        //获取 拉伸试验 结果的 DataSource 

        //-------------------------------


        //求取平均值 X~,S,V
        private void CreateAverageView_Default(DataGridView dv, string testNo)
        {
            dv.Columns.Clear();

            BLL.TestSample bllts = new HR_Test.BLL.TestSample();
            //求平均
            DataSet dst = bllts.GetFinishSumList(" testNo='" + testNo + "'and isFinish=true ");

            //获取试样数量
            DataSet ds_counter = bllts.GetFinishList(" testNo='" + testNo + "' and isFinish=true ", 0);

            //添加新列
            DataColumn dc = new DataColumn();
            dc = dst.Tables[0].Columns.Add("数量(" + ds_counter.Tables[0].Rows.Count + ")", System.Type.GetType("System.String"));

            //设置为第0列
            dst.Tables[0].Columns["数量(" + ds_counter.Tables[0].Rows.Count + ")"].SetOrdinal(0);
            dst.Tables[0].Rows.Clear();
            dst.Tables[0].Rows.Add(new object[] { "MEAN:" });
            dst.Tables[0].Rows.Add(new object[] { "S.D.:" });
            dst.Tables[0].Rows.Add(new object[] { "C.V.:" });
            dst.Tables[0].Rows.Add(new object[] { "Mid.:" });
            dv.DataSource = dst.Tables[0];
            dv.Columns[0].Frozen = true;
            dv.ReadOnly = true;
            dv.Refresh();
            ds_counter.Dispose();
            dst.Dispose();
        }

        //获取 压缩试验 结果的 DataSource
        private void readFinishSample_C(DataGridView dg, string testNo)
        {
            //dg.MultiSelect = true;  
            dg.DataSource = null;
            dg.Columns.Clear();
            dg.RowHeadersVisible = false;
            BLL.Compress bllTs = new HR_Test.BLL.Compress();
            if (string.IsNullOrEmpty(testNo))
            {
                DataSet ds = bllTs.GetFinishList(" isFinish=true and testDate =#" + this.dateTimePicker.Value.Date + "#", 0);
                DataTable dt = ds.Tables[0];
                dg.DataSource = dt;
                ds.Dispose();
            }
            else
            {
                DataSet ds = bllTs.GetFinishList(" isFinish=true and testNo='" + testNo + "' and testDate=#" + this.dateTimePicker.Value.Date + "#", 0);
                DataTable dt = ds.Tables[0];
                dg.DataSource = dt;
                ds.Dispose();
            }
            DataGridViewCheckBoxColumn chkcol = new DataGridViewCheckBoxColumn();
            chkcol.Name = "选择";
            chkcol.MinimumWidth = 50;
            chkcol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
            c.Name = "    ";
            dg.Columns.Insert(0, chkcol);
            dg.Columns.Insert(1, c);
            int rCount = this.dataGridView.Rows.Count;
            for (int i = 0; i < rCount; i++)
            {
                if (i > 19)
                {
                    this.dataGridView.Rows[i].Cells[1].Style.BackColor = Color.FromName(_Color_Array[i % 20]);
                }
                else
                {
                    this.dataGridView.Rows[i].Cells[1].Style.BackColor = Color.FromName(_Color_Array[i]);
                }
            }

            foreach (DataGridViewColumn dgvc in dg.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dg.Columns[1].Frozen = true;
            dg.Columns[2].Frozen = true;
            dg.Refresh();
        }



        //底部数码显示位置及宽度
        private void frmTestResult_SizeChanged(object sender, EventArgs e)
        {
            this.customPanel1.Width = this.customPanel2.Width = this.customPanel3.Width = this.customPanel4.Width = (this.panel2.Width / 4 - 8);
            this.customPanel1.Left = 6;
            this.customPanel2.Left = this.panel2.Width / 4 + 5;
            this.customPanel3.Left = 2 * this.panel2.Width / 4 + 3;
            this.customPanel4.Left = 3 * this.panel2.Width / 4 + 2;
            this.customPanel1.Top = this.customPanel2.Top = this.customPanel3.Top = this.customPanel4.Top = 3;
            this.Refresh();
            this.Update();
        }

        //点击左侧树形列表，读取完成的试验
        private void tvTestSample_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.X > 20)
            {
                m_calSuccess = false;
                m_TestSampleNo = string.Empty;
                m_TestNo = string.Empty;
                m_pcount = 0;
                m_ppredis = 0;
                m_ppreload = 0;
                m_isaddcount = false;
                tvTestSample.SelectedNode = e.Node;
                _selTestSampleArray.Clear();
                m_isProLoad = false;
                this.txtMethod.Text = "";
                DataRow drv;
                if (m_CtrlCommandList.Count > 0) { m_CtrlCommandList.Clear(); }
                if (e.Node.Tag != null)
                {
                    drv = (DataRow)e.Node.Tag;
                    m_TestSampleNo = drv["testSampleNo"].ToString();
                    m_TestNo = drv["testNo"].ToString();
                }
                else
                {
                    drv = null;
                    m_TestSampleNo = string.Empty;
                    m_TestNo = e.Node.Text;
                }

                switch (e.Node.Name)
                {
                    #region GBT23615-2009TensileZong
                    case "GBT23615-2009TensileZong":
                        m_path = "GBT23615-2009TensileZong";
                        m_testType = "tensile";
                        isShowResult = false;
                        this.dataGridView.Tag = "GBT23615-2009TensileZong";
                        m_calSuccess = false;
                        m_lstNoTestSamples.Clear();
                        if (e.Node.ImageIndex == 0)//表示已完成的试验
                        {
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.GBT23615_2009.TensileZong.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            foreach (DataGridViewRow drow in this.dataGridView.Rows)
                            {
                                drow.Cells[0].Value = false;
                                drow.Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                            }
                        }
                        break;
                    case "GBT23615-2009TensileZong_c":
                        m_path = "GBT23615-2009TensileZong";
                        m_testType = "tensile";
                        this.dataGridView.Tag = "GBT23615-2009TensileZong";
                        BLL.GBT236152009_TensileZong bll23615z = new HR_Test.BLL.GBT236152009_TensileZong();
                        Model.GBT236152009_TensileZong model23615z = bll23615z.GetModel(m_TestSampleNo);

                        m_S0 = (float)model23615z.S0.Value;
                        m_L0 = (float)model23615z.L0.Value;

                        m_useExten = model23615z.isUseExtensometer;
                        if (e.Node.ImageIndex == 0)//表示已完成的试验组
                        {
                            m_calSuccess = false;
                            isShowResult = false;
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.GBT23615_2009.TensileZong.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            int dgvRowsCount = this.dataGridView.Rows.Count;
                            for (int i = 0; i < dgvRowsCount; i++)
                            {
                                this.dataGridView.Rows[i].Cells[0].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                            }
                        }
                        else if (e.Node.ImageIndex == 2)//未完成的试验
                        {
                            this.dataGridView.UseWaitCursor = true;
                            m_calSuccess = false;
                            isShowResult = false;
                            _useExten = false;
                            m_useExten = false;
                            m_holdPause = false;
                            m_holdContinue = false;
                            btnZeroS.Enabled = true;
                            ZeroAllValue();
                            ShowCurvePanel();
                            ChangeRealTimeXYChart();
                            _testpath = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + m_TestSampleNo + ".txt";
                            if (!Directory.Exists("E:\\衡新试验数据\\Curve\\" + m_path))
                            {
                                Directory.CreateDirectory("E:\\衡新试验数据\\Curve\\" + m_path);
                            }
                            TestStandard.GBT23615_2009.TensileZong.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);

                            //读取试验方法生成控制命令数组 
                            if (!string.IsNullOrEmpty(m_TestSampleNo))
                            {
                                //写入试样信息
                                try
                                {
                                    TestStandard.GBT23615_2009.TensileZong.CreateCurveFile(_testpath, model23615z);
                                    Thread.Sleep(5);
                                    if (m_SensorCount > 0)
                                    {
                                        if (ReadMethod("GBT23615-2009", model23615z.testMethodName, out m_CtrlCommandArray, out loopCount, out m_methodContent, out m_isProLoad))
                                        {
                                            //写入命令数组 
                                            if (loopCount == 0) loopCount = 1;
                                            //读取第一段和第二段试验控制

                                            if (WriteCommand(m_CtrlCommandArray, 12, loopCount, m_isProLoad, "tensile", RWconfig.GetAppSettings("machineType")))
                                            {
                                                tsbtn_Start.Enabled = true;
                                            }
                                            else
                                            {
                                                tsbtn_Start.Enabled = false;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show(this, "该试样所指定的试验方法不存在,请重新选择!", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                            tsbtn_Start.Enabled = false;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, "请连接设备!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        tsbtn_Start.Enabled = false;
                                    }
                                }
                                catch (Exception ee)
                                {
                                    MessageBox.Show(ee.ToString());
                                    return;
                                }
                                finally
                                {
                                    //this.dataGridView.UseWaitCursor = false; 
                                    //tsbtn_Start.Enabled = true;
                                }
                            }
                        }
                        else if (e.Node.ImageIndex == 1)
                        {
                            //选择一根已完成的试验试样编号
                            tsbtn_Start.Enabled = false;
                            TestStandard.GBT23615_2009.TensileZong.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            ShowResultPanel();
                            int dgvRows = this.dataGridView.Rows.Count;
                            this.zedGraphControl.GraphPane.CurveList.Clear();
                            for (int i = 0; i < dgvRows; i++)
                            {
                                this.dataGridView.Rows[i].Cells[0].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                if (this.dataGridView.Rows[i].Cells[3].Value.ToString() == m_TestSampleNo)
                                {
                                    dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                                }
                            }
                        }
                        break;
                    #endregion

                    #region GBT23615-2009TensileHeng
                    case "GBT23615-2009TensileHeng":
                        m_path = "GBT23615-2009TensileHeng";
                        m_testType = "tensile";
                        isShowResult = false;
                        this.dataGridView.Tag = "GBT23615-2009TensileHeng";
                        m_calSuccess = false;
                        m_lstNoTestSamples.Clear();
                        if (e.Node.ImageIndex == 0)//表示已完成的试验
                        {
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.GBT23615_2009.TensileHeng.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            foreach (DataGridViewRow drow in this.dataGridView.Rows)
                            {
                                drow.Cells[0].Value = false;
                                drow.Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                            }
                        }
                        break;
                    case "GBT23615-2009TensileHeng_c":
                        m_path = "GBT23615-2009TensileHeng";
                        m_testType = "tensile";
                        this.dataGridView.Tag = "GBT23615-2009TensileHeng";
                        BLL.GBT236152009_TensileHeng bll23615h = new HR_Test.BLL.GBT236152009_TensileHeng();
                        Model.GBT236152009_TensileHeng model23615h = bll23615h.GetModel(m_TestSampleNo);

                        m_S0 = (float)model23615h.S0;
                        //m_L0 = (double)model23615h.L;
                        //m_t = (double)model23615h.t;

                        m_useExten = model23615h.isUseExtensometer;
                        if (e.Node.ImageIndex == 0)//表示已完成的试验组
                        {
                            m_calSuccess = false;
                            isShowResult = false;
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.GBT23615_2009.TensileHeng.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            int dgvRowsCount = this.dataGridView.Rows.Count;
                            for (int i = 0; i < dgvRowsCount; i++)
                            {
                                this.dataGridView.Rows[i].Cells[0].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                            }
                        }
                        else if (e.Node.ImageIndex == 2)//未完成的试验
                        {
                            this.dataGridView.UseWaitCursor = true;
                            m_calSuccess = false;
                            isShowResult = false;
                            _useExten = false;
                            m_useExten = false;
                            m_holdPause = false;
                            m_holdContinue = false;
                            btnZeroS.Enabled = true;
                            ZeroAllValue();
                            ShowCurvePanel();
                            ChangeRealTimeXYChart();
                            _testpath = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + m_TestSampleNo + ".txt";
                            if (!Directory.Exists("E:\\衡新试验数据\\Curve\\" + m_path))
                            {
                                Directory.CreateDirectory("E:\\衡新试验数据\\Curve\\" + m_path);
                            }

                            TestStandard.GBT23615_2009.TensileHeng.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);

                            //读取试验方法生成控制命令数组 
                            if (!string.IsNullOrEmpty(m_TestSampleNo))
                            {
                                //写入试样信息
                                try
                                {
                                    TestStandard.GBT23615_2009.TensileHeng.CreateCurveFile(_testpath, model23615h);
                                    Thread.Sleep(5);
                                    if (m_SensorCount > 0)
                                    {
                                        if (ReadMethod("GBT23615-2009", model23615h.testMethodName, out m_CtrlCommandArray, out loopCount, out m_methodContent, out m_isProLoad))
                                        {
                                            //写入命令数组 
                                            if (loopCount == 0) loopCount = 1;
                                            if (WriteCommand(m_CtrlCommandArray, 12, loopCount, m_isProLoad, "tensile", RWconfig.GetAppSettings("machineType")))
                                            {
                                                tsbtn_Start.Enabled = true;
                                            }
                                            else
                                            {
                                                tsbtn_Start.Enabled = false;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show(this, "该试样所指定的试验方法不存在,请重新选择!", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                            tsbtn_Start.Enabled = false;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, "请连接设备!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        tsbtn_Start.Enabled = false;
                                    }
                                }
                                catch (Exception ee)
                                {
                                    MessageBox.Show(ee.ToString());
                                    return;
                                }
                                finally
                                {
                                    this.dataGridView.UseWaitCursor = false;
                                    //tsbtn_Start.Enabled = true;
                                }
                            }
                        }
                        else if (e.Node.ImageIndex == 1)
                        {
                            tsbtn_Start.Enabled = false;
                            //选择一根已完成的试验试样编号
                            TestStandard.GBT23615_2009.TensileHeng.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            ShowResultPanel();
                            int dgvRows = this.dataGridView.Rows.Count;
                            this.zedGraphControl.GraphPane.CurveList.Clear();
                            for (int i = 0; i < dgvRows; i++)
                            {
                                this.dataGridView.Rows[i].Cells[0].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                if (this.dataGridView.Rows[i].Cells[3].Value.ToString() == m_TestSampleNo)
                                {
                                    dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                                }
                            }
                        }
                        break;
                    #endregion

                    #region GBT228-2010

                    case "GBT228-2010":
                        m_path = "GBT228-2010";
                        m_testType = "GBT228-2010";
                        isShowResult = false;
                        this.dataGridView.Tag = "GBT228-2010";
                        m_calSuccess = false;
                        m_lstNoTestSamples.Clear();
                        if (e.Node.ImageIndex == 0)//表示已完成的试验
                        {
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.GBT228_2010.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            //m_lstNoTestSamples.Clear();
                            //this.lblInfo.Text = "当前试样:无\r\n下一个试样:无"; 
                            foreach (DataGridViewRow drow in this.dataGridView.Rows)
                            {
                                drow.Cells[0].Value = false;
                                drow.Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                            }
                        }
                        break;

                    case "GBT228-2010_c":
                        m_path = "GBT228-2010";
                        m_testType = "tensile";
                        this.dataGridView.Tag = "GBT228-2010";
                        BLL.TestSample bllTensile = new HR_Test.BLL.TestSample();
                        Model.TestSample modelTensile = bllTensile.GetModel(m_TestSampleNo);
                        m_S0 = (float)modelTensile.S0;
                        m_L0 = (float)modelTensile.L0;
                        m_Lc = (float)modelTensile.Lc;
                        m_Le = (float)modelTensile.Le;
                        m_Ep = (float)modelTensile.εp;
                        m_Et = (float)modelTensile.εt;
                        m_Er = (float)modelTensile.εr;
                        m_useExten = modelTensile.isUseExtensometer;
                        if (e.Node.ImageIndex == 0)//表示已完成的试验组
                        {
                            m_calSuccess = false;
                            isShowResult = false;
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.GBT228_2010.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            int dgvRowsCount = this.dataGridView.Rows.Count;
                            for (int i = 0; i < dgvRowsCount; i++)
                            {
                                this.dataGridView.Rows[i].Cells[1].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                            }
                        }
                        else if (e.Node.ImageIndex == 2)//未完成的试验
                        {
                            this.dataGridView.UseWaitCursor = true;
                            m_calSuccess = false;
                            isShowResult = false;
                            _useExten = false;
                            m_useExten = false;
                            m_holdPause = false;
                            m_holdContinue = false;
                            btnZeroS.Enabled = true;
                            ZeroAllValue();
                            ShowCurvePanel();
                            ChangeRealTimeXYChart();
                            _testpath = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + m_TestSampleNo + ".txt";
                            if (!Directory.Exists("E:\\衡新试验数据\\Curve\\" + m_path))
                            {
                                Directory.CreateDirectory("E:\\衡新试验数据\\Curve\\" + m_path);
                            }
                            TestStandard.GBT228_2010.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);

                            //BLL.TestSample bllts = new HR_Test.BLL.TestSample();
                            //DataSet ds = bllts.GetList(" isFinish =false and testNo='" + testNo + "' ");

                            //生成未做试验的试样编号组
                            //m_lstNoTestSamples.Clear();

                            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            //{
                            //    m_lstNoTestSamples.Add(ds.Tables[0].Rows[i]["testSampleNo"].ToString());
                            //}

                            //if (m_lstNoTestSamples.Count > 1 && m_TestSampleNo == m_lstNoTestSamples[0].ToString())
                            //{
                            //    this.lblInfo.Text = "当前试样:" + m_TestSampleNo + "\r\n下一个试样:" + m_lstNoTestSamples[1].ToString();
                            //}
                            //else if (m_lstNoTestSamples.Count > 1 && m_TestSampleNo != m_lstNoTestSamples[0].ToString())
                            //{
                            //    this.lblInfo.Text = "当前试样:" + m_TestSampleNo + "\r\n下一个试样:" + m_lstNoTestSamples[0].ToString();
                            //}
                            //else if (m_lstNoTestSamples.Count == 1)
                            //{
                            //    this.lblInfo.Text = "当前试样:" + m_TestSampleNo + "\r\n下一个试样:无";
                            //}
                            //else
                            //{
                            //    this.lblInfo.Text = "当前试样:无\r\n下一个试样:无";
                            //}

                            //读取试验方法生成控制命令数组 
                            if (!string.IsNullOrEmpty(m_TestSampleNo))
                            {
                                //写入试样信息
                                try
                                {
                                    TestStandard.GBT228_2010.CreateCurveFile(_testpath, modelTensile);
                                    Thread.Sleep(5);
                                    if (m_SensorCount > 0)
                                    {
                                        if (ReadMethod("GBT228-2010", modelTensile.testMethodName, out m_CtrlCommandArray, out loopCount, out m_methodContent, out m_isProLoad))
                                        {
                                            //写入命令数组 
                                            if (loopCount == 0) loopCount = 1;
                                            if (WriteCommand(m_CtrlCommandArray, 12, loopCount, m_isProLoad, "tensile", RWconfig.GetAppSettings("machineType")))
                                            {
                                                tsbtn_Start.Enabled = true;
                                            }
                                            else
                                            {
                                                tsbtn_Start.Enabled = false;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show(this, "该试样所指定的试验方法不存在,请重新选择!", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                            tsbtn_Start.Enabled = false;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, "请连接设备!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        tsbtn_Start.Enabled = false;
                                    }
                                }
                                catch (Exception ee)
                                {
                                    MessageBox.Show(ee.ToString());
                                    return;
                                }
                                finally
                                {
                                    this.dataGridView.UseWaitCursor = false;
                                    //Test
                                    //tsbtn_Start.Enabled = true;
                                }
                            }
                        }
                        else if (e.Node.ImageIndex == 1)
                        {
                            //选择一根已完成的试验试样编号
                            //m_lstNoTestSamples.Clear();
                            //this.lblInfo.Text = "当前试样:无\r\n下一个试样:无"; 
                            //m_TestSampleNo = e.Node.Text;
                            tsbtn_Start.Enabled = false;
                            TestStandard.GBT228_2010.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            ShowResultPanel();
                            int dgvRows = this.dataGridView.Rows.Count;
                            this.zedGraphControl.GraphPane.CurveList.Clear();
                            for (int i = 0; i < dgvRows; i++)
                            {
                                this.dataGridView.Rows[i].Cells[0].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                if (this.dataGridView.Rows[i].Cells[3].Value.ToString() == m_TestSampleNo)
                                {
                                    dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                                    /*ChangeRealTimeXYChart();
                                     //读取结果数据
                                     //ReadOneListData(m_path, m_TestSampleNo);
                                     //计算最大值index
                               
                                    m_l = ReadOneListData(m_path, m_TestSampleNo);
                                    if (m_l != null)
                                     {
                                         CalcData(m_l,m_isSelReH,m_isSelReL);
                                         //画曲线
                                         ReadResultData(m_l);
                                         //ReadResultCurve(this.pbChart, m_l);
                                         //标记结果
                                         //test
                                         //if (m_l.Count > 50)
                                         //    AutoDrawResult(m_l);                                        
                                         isShowResult = false;
                                     }
                                     else
                                     {
                                         MessageBox.Show(this, "曲线数据不存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                         return;
                                     }
                                      * */
                                }
                            }
                        }
                        break;
                    #endregion

                    #region GBT3354-2014
                    case "GBT3354-2014":
                        m_path = "GBT3354-2014";
                        m_testType = "GBT3354-2014";
                        isShowResult = false;
                        this.dataGridView.Tag = "GBT3354-2014";
                        m_calSuccess = false;
                        m_lstNoTestSamples.Clear();
                        if (e.Node.ImageIndex == 0)//表示已完成的试验
                        {
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.GBT3354_2014.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            //m_lstNoTestSamples.Clear();
                            //this.lblInfo.Text = "当前试样:无\r\n下一个试样:无"; 
                            foreach (DataGridViewRow drow in this.dataGridView.Rows)
                            {
                                drow.Cells[0].Value = false;
                                drow.Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                            }
                        }
                        break;

                    case "GBT3354-2014_c":
                        m_path = "GBT3354-2014";
                        m_testType = "tensile";
                        this.dataGridView.Tag = "GBT3354-2014";
                        BLL.GBT3354_Samples bll3354 = new HR_Test.BLL.GBT3354_Samples();
                        Model.GBT3354_Samples model3354 = bll3354.GetModel(m_TestSampleNo);
                        if (model3354 == null)
                            return;
                        //读取参数
                        m_S0 = (float)model3354.S0.Value;
                        m_Ll = (float)model3354.lL.Value;
                        m_Lt = (float)model3354.lT.Value;
                        m_e1 = model3354.εz1.Value;
                        m_e2 = model3354.εz2.Value;
                        m_useExten = model3354.isUseExtensometer1;
                        m_useExten2 = model3354.isUseExtensometer2;

                        if (e.Node.ImageIndex == 0)//表示已完成的试验组
                        {
                            m_calSuccess = false;
                            isShowResult = false;
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.GBT3354_2014.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            int dgvRowsCount = this.dataGridView.Rows.Count;
                            for (int i = 0; i < dgvRowsCount; i++)
                            {
                                this.dataGridView.Rows[i].Cells[1].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                            }
                        }
                        else if (e.Node.ImageIndex == 2)//未完成的试验
                        {
                            this.dataGridView.UseWaitCursor = true;
                            m_calSuccess = false;
                            isShowResult = false;
                            m_useExten = false;
                            m_useExten2 = false;
                            _useExten = false;
                            m_holdPause = false;
                            m_holdContinue = false;
                            btnZeroS.Enabled = true;
                            ZeroAllValue();
                            ShowCurvePanel();
                            ChangeRealTimeXYChart();
                            _testpath = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + m_TestSampleNo + ".txt";
                            if (!Directory.Exists("E:\\衡新试验数据\\Curve\\" + m_path))
                            {
                                Directory.CreateDirectory("E:\\衡新试验数据\\Curve\\" + m_path);
                            }
                            TestStandard.GBT3354_2014.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);


                            //读取试验方法生成控制命令数组 
                            if (!string.IsNullOrEmpty(m_TestSampleNo))
                            {
                                //写入试样信息
                                try
                                {
                                    TestStandard.GBT3354_2014.CreateCurveFile(_testpath, model3354);
                                    //Test
                                    //if (m_SensorCount > 0)
                                    //{
                                    if (ReadMethod("GBT3354-2014", model3354.testMethodName, out m_CtrlCommandArray, out loopCount, out m_methodContent, out m_isProLoad))
                                    {
                                        //写入命令数组 
                                        if (loopCount == 0) loopCount = 1;
                                        if (WriteCommand(m_CtrlCommandArray, 12, loopCount, m_isProLoad, "tensile", RWconfig.GetAppSettings("machineType")))
                                        {
                                            tsbtn_Start.Enabled = true;
                                        }
                                        else
                                        {
                                            tsbtn_Start.Enabled = false;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, "该试样所指定的试验方法不存在,请重新选择!", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                        tsbtn_Start.Enabled = false;
                                        return;
                                    }
                                    //}
                                    //else
                                    //{
                                    //    MessageBox.Show(this, "请连接设备!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    //    tsbtn_Start.Enabled = false;
                                    //}
                                }
                                catch (Exception ee)
                                {
                                    MessageBox.Show(ee.ToString());
                                    return;
                                }
                                finally
                                {
                                    this.dataGridView.UseWaitCursor = false;
                                    //Test
                                    tsbtn_Start.Enabled = true;
                                }
                            }
                        }
                        else if (e.Node.ImageIndex == 1)
                        {
                            //选择一根已完成的试验试样编号                          
                            tsbtn_Start.Enabled = false;
                            TestStandard.GBT3354_2014.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            ShowResultPanel();
                            int dgvRows = this.dataGridView.Rows.Count;
                            this.zedGraphControl.GraphPane.CurveList.Clear();
                            for (int i = 0; i < dgvRows; i++)
                            {
                                this.dataGridView.Rows[i].Cells[0].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                if (this.dataGridView.Rows[i].Cells[3].Value.ToString() == m_TestSampleNo)
                                {
                                    dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                                }
                            }
                        }
                        break;
                    #endregion

                    #region GBT7314-2005
                    case "GBT7314-2005":
                        m_path = "GBT7314-2005";
                        m_testType = "compress";
                        isShowResult = false;
                        this.dataGridView.Tag = "GBT7314-2005";
                        m_calSuccess = false;
                        m_lstNoTestSamples.Clear();
                        if (e.Node.ImageIndex == 0)//表示已完成的试验
                        {
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.GBT7314_2005.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            //m_lstNoTestSamples.Clear();
                            //this.lblInfo.Text = "当前试样:无\r\n下一个试样:无"; 
                            foreach (DataGridViewRow drow in this.dataGridView.Rows)
                            {
                                drow.Cells[0].Value = false;
                                drow.Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                            }
                        }
                        break;
                    case "GBT7314-2005_c":
                        m_path = "GBT7314-2005";
                        m_testType = "compress";
                        this.dataGridView.Tag = "GBT7314-2005";
                        BLL.Compress bllCompress = new HR_Test.BLL.Compress();
                        Model.Compress modelCompress = bllCompress.GetModel(m_TestSampleNo);
                        m_S0 = (float)modelCompress.S0;
                        m_L0 = (float)modelCompress.L0;
                        m_Lc = (float)modelCompress.L;
                        m_n = (float)modelCompress.n;
                        m_Ep = (float)modelCompress.εpc;
                        m_Et = (float)modelCompress.εtc;
                        m_useExten = modelCompress.isUseExtensometer;
                        if (e.Node.ImageIndex == 0)//表示已完成的试验组
                        {
                            m_calSuccess = false;
                            isShowResult = false;
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.GBT7314_2005.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            int dgvRowsCount = this.dataGridView.Rows.Count;
                            for (int i = 0; i < dgvRowsCount; i++)
                            {
                                this.dataGridView.Rows[i].Cells[0].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                            }
                        }
                        else if (e.Node.ImageIndex == 2)//未完成的试验
                        {
                            this.dataGridView.UseWaitCursor = true;
                            m_calSuccess = false;
                            isShowResult = false;
                            _useExten = false;
                            m_useExten = false;
                            m_holdPause = false;
                            m_holdContinue = false;
                            btnZeroS.Enabled = true;
                            ZeroAllValue();
                            ShowCurvePanel();
                            ChangeRealTimeXYChart();
                            _testpath = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + m_TestSampleNo + ".txt";
                            if (!Directory.Exists("E:\\衡新试验数据\\Curve\\" + m_path))
                            {
                                Directory.CreateDirectory("E:\\衡新试验数据\\Curve\\" + m_path);
                            }
                            TestStandard.GBT7314_2005.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);

                            //BLL.TestSample bllts = new HR_Test.BLL.TestSample();
                            //DataSet ds = bllts.GetList(" isFinish =false and testNo='" + testNo + "' ");

                            //生成未做试验的试样编号组
                            //m_lstNoTestSamples.Clear();

                            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            //{
                            //    m_lstNoTestSamples.Add(ds.Tables[0].Rows[i]["testSampleNo"].ToString());
                            //}

                            //if (m_lstNoTestSamples.Count > 1 && m_TestSampleNo == m_lstNoTestSamples[0].ToString())
                            //{
                            //    this.lblInfo.Text = "当前试样:" + m_TestSampleNo + "\r\n下一个试样:" + m_lstNoTestSamples[1].ToString();
                            //}
                            //else if (m_lstNoTestSamples.Count > 1 && m_TestSampleNo != m_lstNoTestSamples[0].ToString())
                            //{
                            //    this.lblInfo.Text = "当前试样:" + m_TestSampleNo + "\r\n下一个试样:" + m_lstNoTestSamples[0].ToString();
                            //}
                            //else if (m_lstNoTestSamples.Count == 1)
                            //{
                            //    this.lblInfo.Text = "当前试样:" + m_TestSampleNo + "\r\n下一个试样:无";
                            //}
                            //else
                            //{
                            //    this.lblInfo.Text = "当前试样:无\r\n下一个试样:无";
                            //}

                            //读取试验方法生成控制命令数组 
                            if (!string.IsNullOrEmpty(m_TestSampleNo))
                            {
                                //写入试样信息
                                try
                                {
                                    TestStandard.GBT7314_2005.CreateCurveFile(_testpath, modelCompress);
                                    Thread.Sleep(5);
                                    if (m_SensorCount > 0)
                                    {
                                        if (ReadMethod("GBT7314-2005", modelCompress.testMethodName, out m_CtrlCommandArray, out loopCount, out m_methodContent, out m_isProLoad))
                                        {
                                            //MessageBox.Show("true");
                                            //写入命令数组 
                                            if (loopCount == 0) loopCount = 1;
                                            if (WriteCommand(m_CtrlCommandArray, 12, loopCount, m_isProLoad, "compress", RWconfig.GetAppSettings("machineType")))
                                            {
                                                tsbtn_Start.Enabled = true;
                                            }
                                            else
                                            {
                                                tsbtn_Start.Enabled = false;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show(this, "该试样所指定的试验方法不存在,请重新选择!", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                            tsbtn_Start.Enabled = false;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, "请连接设备!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        tsbtn_Start.Enabled = false;
                                    }
                                }
                                catch (Exception ee)
                                {
                                    MessageBox.Show(ee.ToString());
                                    return;
                                }
                                finally
                                {
                                    this.dataGridView.UseWaitCursor = false;
                                    //tsbtn_Start.Enabled = true;
                                }
                            }
                        }
                        else if (e.Node.ImageIndex == 1)
                        {
                            //选择一根已完成的试验试样编号
                            //m_lstNoTestSamples.Clear();
                            //this.lblInfo.Text = "当前试样:无\r\n下一个试样:无"; 
                            //m_TestSampleNo = e.Node.Text;
                            tsbtn_Start.Enabled = false;
                            TestStandard.GBT7314_2005.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            ShowResultPanel();
                            int dgvRows = this.dataGridView.Rows.Count;
                            this.zedGraphControl.GraphPane.CurveList.Clear();
                            for (int i = 0; i < dgvRows; i++)
                            {
                                this.dataGridView.Rows[i].Cells[0].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                if (this.dataGridView.Rows[i].Cells[3].Value.ToString() == m_TestSampleNo)
                                {
                                    dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                                    /*ChangeRealTimeXYChart();
                                     //读取结果数据
                                     //ReadOneListData(m_path, m_TestSampleNo);
                                     //计算最大值index
                               
                                    m_l = ReadOneListData(m_path, m_TestSampleNo);
                                    if (m_l != null)
                                     {
                                         CalcData(m_l,m_isSelReH,m_isSelReL);
                                         //画曲线
                                         ReadResultData(m_l);
                                         //ReadResultCurve(this.pbChart, m_l);
                                         //标记结果
                                         //test
                                         //if (m_l.Count > 50)
                                         //    AutoDrawResult(m_l);                                        
                                         isShowResult = false;
                                     }
                                     else
                                     {
                                         MessageBox.Show(this, "曲线数据不存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                         return;
                                     }
                                      * */
                                }
                            }
                        }
                        break;
                    #endregion

                    #region YBT5349-2006
                    case "YBT5349-2006":
                        m_path = "YBT5349-2006";
                        m_testType = "bend";
                        isShowResult = false;
                        this.dataGridView.Tag = "YBT5349-2006";
                        m_calSuccess = false;
                        m_lstNoTestSamples.Clear();
                        if (e.Node.ImageIndex == 0)//表示已完成的试验
                        {
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.YBT5349_2006.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            //m_lstNoTestSamples.Clear();
                            //this.lblInfo.Text = "当前试样:无\r\n下一个试样:无"; 
                            foreach (DataGridViewRow drow in this.dataGridView.Rows)
                            {
                                drow.Cells[0].Value = false;
                                drow.Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                            }
                        }
                        break;

                    case "YBT5349-2006_c":
                        m_path = "YBT5349-2006";
                        m_testType = "bend";
                        this.dataGridView.Tag = "YBT5349-2006";
                        BLL.Bend bllBend = new HR_Test.BLL.Bend();
                        Model.Bend modelBend = bllBend.GetModel(m_TestSampleNo);
                        //抗弯强度 三点弯曲： σrb = Frb*Ls/4W 即 =Frb/(4W/Ls);  四点弯曲：σrb = Frb*l/2W 即 =Frb/(2W/l)
                        switch (modelBend.testType)
                        {
                            case "三点弯曲":
                                m_S0 = (float)(4 * modelBend.W / modelBend.Ls);
                                break;
                            case "四点弯曲":
                                m_S0 = (float)(2 * modelBend.W / modelBend.l_l);
                                break;
                        }

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

                        //跨距
                        m_L0 = (float)modelBend.Ls;
                        //力臂
                        m_Lc = (float)modelBend.l_l;
                        //挠度计跨距
                        m_Le = (float)modelBend.Le;
                        //规定非比例弯曲应变
                        m_Ep = (float)modelBend.εpb;
                        //规定残余弯曲应变
                        m_Er = (float)modelBend.εrb;
                        //挠度计放大倍数
                        m_n = (float)modelBend.n;

                        m_useExten = modelBend.isUseExtensometer;

                        if (e.Node.ImageIndex == 0)//表示已完成的试验组
                        {
                            m_calSuccess = false;
                            isShowResult = false;
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.YBT5349_2006.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            int dgvRowsCount = this.dataGridView.Rows.Count;
                            for (int i = 0; i < dgvRowsCount; i++)
                            {
                                this.dataGridView.Rows[i].Cells[0].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                            }
                        }
                        else if (e.Node.ImageIndex == 2)//未完成的试验
                        {
                            this.dataGridView.UseWaitCursor = true;
                            m_calSuccess = false;
                            isShowResult = false;
                            _useExten = false;
                            m_useExten = false;
                            m_holdPause = false;
                            m_holdContinue = false;
                            btnZeroS.Enabled = true;
                            ZeroAllValue();
                            ShowCurvePanel();
                            ChangeRealTimeXYChart();

                            _testpath = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + m_TestSampleNo + ".txt";
                            if (!Directory.Exists("E:\\衡新试验数据\\Curve\\" + m_path))
                            {
                                Directory.CreateDirectory("E:\\衡新试验数据\\Curve\\" + m_path);
                            }

                            TestStandard.YBT5349_2006.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);


                            //读取试验方法生成控制命令数组 
                            if (!string.IsNullOrEmpty(m_TestSampleNo))
                            {
                                //写入试样信息
                                try
                                {
                                    FileStream fs = new FileStream(_testpath, FileMode.Create, FileAccess.ReadWrite);
                                    utils.AddText(fs, "testType,testSampleNo,S0,Ls,l,Le,Ep,Er,n");
                                    utils.AddText(fs, "\r\n");
                                    utils.AddText(fs, "bend," + modelBend.testSampleNo + "," + m_S0 + "," + m_L0 + "," + m_Lc + "," + m_Le + "," + m_Ep + "," + m_Er + "," + m_n);
                                    utils.AddText(fs, "\r\n");
                                    fs.Flush();
                                    fs.Close();
                                    fs.Dispose();
                                    Thread.Sleep(5);
                                    if (m_SensorCount > 0)
                                    {
                                        if (ReadMethod("YBT5349-2006", modelBend.testMethod, out m_CtrlCommandArray, out loopCount, out m_methodContent, out m_isProLoad))
                                        {
                                            //MessageBox.Show("true");
                                            //写入命令数组 
                                            if (loopCount == 0) loopCount = 1;
                                            if (WriteCommand(m_CtrlCommandArray, 12, loopCount, m_isProLoad, "bend", RWconfig.GetAppSettings("machineType")))
                                            {
                                                tsbtn_Start.Enabled = true;
                                            }
                                            else
                                            {
                                                tsbtn_Start.Enabled = false;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show(this, "该试样所指定的试验方法不存在,请重新选择!", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                            tsbtn_Start.Enabled = false;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, "请连接设备!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        tsbtn_Start.Enabled = false;
                                    }
                                }
                                catch (Exception ee)
                                {
                                    MessageBox.Show(ee.ToString());
                                    return;
                                }
                                finally
                                {
                                    this.dataGridView.UseWaitCursor = false;
                                    //tsbtn_Start.Enabled = true;
                                }
                            }
                        }
                        else if (e.Node.ImageIndex == 1)
                        {
                            //选择一根已完成的试验试样编号
                            //m_lstNoTestSamples.Clear();
                            //this.lblInfo.Text = "当前试样:无\r\n下一个试样:无";
                            //m_TestSampleNo = e.Node.Text;
                            tsbtn_Start.Enabled = false;
                            TestStandard.YBT5349_2006.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            ShowResultPanel();
                            int dgvRows = this.dataGridView.Rows.Count;
                            this.zedGraphControl.GraphPane.CurveList.Clear();
                            for (int i = 0; i < dgvRows; i++)
                            {
                                this.dataGridView.Rows[i].Cells[0].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                if (this.dataGridView.Rows[i].Cells[3].Value.ToString() == m_TestSampleNo)
                                {
                                    dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                                    /*ChangeRealTimeXYChart();
                                     //读取结果数据
                                     //ReadOneListData(m_path, m_TestSampleNo);
                                     //计算最大值index
                               
                                    m_l = ReadOneListData(m_path, m_TestSampleNo);
                                    if (m_l != null)
                                     {
                                         CalcData(m_l,m_isSelReH,m_isSelReL);
                                         //画曲线
                                         ReadResultData(m_l);
                                         //ReadResultCurve(this.pbChart, m_l);
                                         //标记结果
                                         //test
                                         //if (m_l.Count > 50)
                                         //    AutoDrawResult(m_l);                                        
                                         isShowResult = false;
                                     }
                                     else
                                     {
                                         MessageBox.Show(this, "曲线数据不存在!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                         return;
                                     }
                                      * */
                                }
                            }
                        }

                        break;
                    #endregion

                    #region GBT28289-2012Tensile
                    case "GBT28289-2012Tensile":
                        m_path = "GBT28289-2012Tensile";
                        m_testType = "tensile";
                        isShowResult = false;
                        this.dataGridView.Tag = "GBT28289-2012Tensile";
                        m_calSuccess = false;
                        m_lstNoTestSamples.Clear();
                        if (e.Node.ImageIndex == 0)//表示已完成的试验
                        {
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.GBT28289_2012.Tensile.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            foreach (DataGridViewRow drow in this.dataGridView.Rows)
                            {
                                drow.Cells[0].Value = false;
                                drow.Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                            }
                        }
                        break;
                    case "GBT28289-2012Tensile_c":
                        m_path = "GBT28289-2012Tensile";
                        m_testType = "tensile";
                        this.dataGridView.Tag = "GBT28289-2012Tensile";
                        BLL.GBT282892012_Tensile bll28289t = new HR_Test.BLL.GBT282892012_Tensile();
                        Model.GBT282892012_Tensile model28289t = bll28289t.GetModel(m_TestSampleNo);

                        m_S0 = (float)model28289t.S0;
                        m_L0 = (float)model28289t.L;

                        m_useExten = model28289t.isUseExtensometer;
                        if (e.Node.ImageIndex == 0)//表示已完成的试验组
                        {
                            m_calSuccess = false;
                            isShowResult = false;
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.GBT28289_2012.Tensile.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            int dgvRowsCount = this.dataGridView.Rows.Count;
                            for (int i = 0; i < dgvRowsCount; i++)
                            {
                                this.dataGridView.Rows[i].Cells[0].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                            }
                        }
                        else if (e.Node.ImageIndex == 2)//未完成的试验
                        {
                            this.dataGridView.UseWaitCursor = true;
                            m_calSuccess = false;
                            isShowResult = false;
                            _useExten = false;
                            m_useExten = false;
                            m_holdPause = false;
                            m_holdContinue = false;
                            btnZeroS.Enabled = true;
                            ZeroAllValue();
                            ShowCurvePanel();
                            ChangeRealTimeXYChart();
                            _testpath = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + m_TestSampleNo + ".txt";
                            if (!Directory.Exists("E:\\衡新试验数据\\Curve\\" + m_path))
                            {
                                Directory.CreateDirectory("E:\\衡新试验数据\\Curve\\" + m_path);
                            }
                            TestStandard.GBT28289_2012.Tensile.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);

                            //读取试验方法生成控制命令数组 
                            if (!string.IsNullOrEmpty(m_TestSampleNo))
                            {
                                //写入试样信息
                                try
                                {
                                    TestStandard.GBT28289_2012.Tensile.CreateCurveFile(_testpath, model28289t);
                                    Thread.Sleep(5);
                                    if (m_SensorCount > 0)
                                    {
                                        if (ReadMethod("GBT28289-2012", model28289t.testMethodName, out m_CtrlCommandArray, out loopCount, out m_methodContent, out m_isProLoad))
                                        {
                                            //写入命令数组 
                                            if (loopCount == 0) loopCount = 1;
                                            if (WriteCommand(m_CtrlCommandArray, 12, loopCount, m_isProLoad, "tensile", RWconfig.GetAppSettings("machineType")))
                                            {
                                                tsbtn_Start.Enabled = true;
                                            }
                                            else
                                            {
                                                tsbtn_Start.Enabled = false;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show(this, "该试样所指定的试验方法不存在,请重新选择!", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                            tsbtn_Start.Enabled = false;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, "请连接设备!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        tsbtn_Start.Enabled = false;
                                    }
                                }
                                catch (Exception ee)
                                {
                                    MessageBox.Show(ee.ToString());
                                    return;
                                }
                                finally
                                {
                                    this.dataGridView.UseWaitCursor = false;
                                    //tsbtn_Start.Enabled = true;
                                }
                            }
                        }
                        else if (e.Node.ImageIndex == 1)
                        {
                            //选择一根已完成的试验试样编号
                            tsbtn_Start.Enabled = false;
                            TestStandard.GBT28289_2012.Tensile.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            ShowResultPanel();
                            int dgvRows = this.dataGridView.Rows.Count;
                            this.zedGraphControl.GraphPane.CurveList.Clear();
                            for (int i = 0; i < dgvRows; i++)
                            {
                                this.dataGridView.Rows[i].Cells[0].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                if (this.dataGridView.Rows[i].Cells[3].Value.ToString() == m_TestSampleNo)
                                {
                                    dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                                }
                            }
                        }
                        break;
                    #endregion

                    #region GBT28289-2012Shear
                    case "GBT28289-2012Shear":
                        m_path = "GBT28289-2012Shear";
                        m_testType = "shear";
                        isShowResult = false;
                        this.dataGridView.Tag = "GBT28289-2012Shear";
                        m_calSuccess = false;
                        m_lstNoTestSamples.Clear();
                        if (e.Node.ImageIndex == 0)//表示已完成的试验
                        {
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.GBT28289_2012.Shear.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            foreach (DataGridViewRow drow in this.dataGridView.Rows)
                            {
                                drow.Cells[0].Value = false;
                                drow.Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                            }
                        }
                        break;
                    case "GBT28289-2012Shear_c":
                        m_path = "GBT28289-2012Shear";
                        m_testType = "shear";
                        this.dataGridView.Tag = "GBT28289-2012Shear";
                        BLL.GBT282892012_Shear bll28289s = new HR_Test.BLL.GBT282892012_Shear();
                        Model.GBT282892012_Shear model28289s = bll28289s.GetModel(m_TestSampleNo);

                        m_S0 = (float)model28289s.S0;
                        m_L0 = (float)model28289s.L;

                        m_useExten = model28289s.isUseExtensometer;
                        if (e.Node.ImageIndex == 0)//表示已完成的试验组
                        {
                            m_calSuccess = false;
                            isShowResult = false;
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.GBT28289_2012.Shear.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            int dgvRowsCount = this.dataGridView.Rows.Count;
                            for (int i = 0; i < dgvRowsCount; i++)
                            {
                                this.dataGridView.Rows[i].Cells[0].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                            }
                        }
                        else if (e.Node.ImageIndex == 2)//未完成的试验
                        {
                            this.dataGridView.UseWaitCursor = true;
                            m_calSuccess = false;
                            isShowResult = false;
                            _useExten = false;
                            m_useExten = false;
                            m_holdPause = false;
                            m_holdContinue = false;
                            btnZeroS.Enabled = true;
                            ZeroAllValue();
                            ShowCurvePanel();
                            ChangeRealTimeXYChart();
                            _testpath = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + m_TestSampleNo + ".txt";
                            if (!Directory.Exists("E:\\衡新试验数据\\Curve\\" + m_path))
                            {
                                Directory.CreateDirectory("E:\\衡新试验数据\\Curve\\" + m_path);
                            }
                            TestStandard.GBT28289_2012.Shear.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);

                            //读取试验方法生成控制命令数组 
                            if (!string.IsNullOrEmpty(m_TestSampleNo))
                            {
                                //写入试样信息
                                try
                                {
                                    TestStandard.GBT28289_2012.Shear.CreateCurveFile(_testpath, model28289s);
                                    Thread.Sleep(5);
                                    if (m_SensorCount > 0)
                                    {
                                        if (ReadMethod("GBT28289-2012", model28289s.testMethodName, out m_CtrlCommandArray, out loopCount, out m_methodContent, out m_isProLoad))
                                        {
                                            //写入命令数组 
                                            if (loopCount == 0) loopCount = 1;
                                            if (WriteCommand(m_CtrlCommandArray, 12, loopCount, m_isProLoad, "shear", RWconfig.GetAppSettings("machineType")))
                                            {
                                                tsbtn_Start.Enabled = true;
                                            }
                                            else
                                            {
                                                tsbtn_Start.Enabled = false;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show(this, "该试样所指定的试验方法不存在,请重新选择!", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                            tsbtn_Start.Enabled = false;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, "请连接设备!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        tsbtn_Start.Enabled = false;
                                    }
                                }
                                catch (Exception ee)
                                {
                                    MessageBox.Show(ee.ToString());
                                    return;
                                }
                                finally
                                {
                                    this.dataGridView.UseWaitCursor = false;
                                    //tsbtn_Start.Enabled = true;
                                }
                            }
                        }
                        else if (e.Node.ImageIndex == 1)
                        {
                            //选择一根已完成的试验试样编号
                            tsbtn_Start.Enabled = false;
                            TestStandard.GBT28289_2012.Shear.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            ShowResultPanel();
                            int dgvRows = this.dataGridView.Rows.Count;
                            this.zedGraphControl.GraphPane.CurveList.Clear();
                            for (int i = 0; i < dgvRows; i++)
                            {
                                this.dataGridView.Rows[i].Cells[0].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                if (this.dataGridView.Rows[i].Cells[3].Value.ToString() == m_TestSampleNo)
                                {
                                    dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                                }
                            }
                        }
                        break;
                    #endregion

                    #region GBT28289-2012Twist
                    case "GBT28289-2012Twist":
                        m_path = "GBT28289-2012Twist";
                        m_testType = "twist";
                        isShowResult = false;
                        this.dataGridView.Tag = "GBT28289-2012Twist";
                        m_calSuccess = false;
                        m_lstNoTestSamples.Clear();
                        if (e.Node.ImageIndex == 0)//表示已完成的试验
                        {
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.GBT28289_2012.Twist.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            foreach (DataGridViewRow drow in this.dataGridView.Rows)
                            {
                                drow.Cells[0].Value = false;
                                drow.Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                            }
                        }
                        break;
                    case "GBT28289-2012Twist_c":
                        m_path = "GBT28289-2012Twist";
                        m_testType = "twist";
                        this.dataGridView.Tag = "GBT28289-2012Twist";
                        BLL.GBT282892012_Twist bll28289tw = new HR_Test.BLL.GBT282892012_Twist();
                        Model.GBT282892012_Twist model28289tw = bll28289tw.GetModel(m_TestSampleNo);

                        m_S0 = (float)model28289tw.S0;
                        m_L0 = (float)model28289tw.L;
                        m_useExten = model28289tw.isUseExtensometer;
                        if (e.Node.ImageIndex == 0)//表示已完成的试验组
                        {
                            m_calSuccess = false;
                            isShowResult = false;
                            ShowResultPanel();
                            tsbtn_Start.Enabled = false;
                            //读取试验结果 
                            TestStandard.GBT28289_2012.Twist.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            int dgvRowsCount = this.dataGridView.Rows.Count;
                            for (int i = 0; i < dgvRowsCount; i++)
                            {
                                this.dataGridView.Rows[i].Cells[0].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                            }
                        }
                        else if (e.Node.ImageIndex == 2)//未完成的试验
                        {
                            this.dataGridView.UseWaitCursor = true;
                            m_calSuccess = false;
                            isShowResult = false;
                            _useExten = false;
                            m_useExten = false;
                            m_holdPause = false;
                            m_holdContinue = false;
                            btnZeroS.Enabled = true;
                            ZeroAllValue();
                            ShowCurvePanel();
                            ChangeRealTimeXYChart();
                            _testpath = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + m_TestSampleNo + ".txt";
                            if (!Directory.Exists("E:\\衡新试验数据\\Curve\\" + m_path))
                            {
                                Directory.CreateDirectory("E:\\衡新试验数据\\Curve\\" + m_path);
                            }
                            TestStandard.GBT28289_2012.Twist.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);

                            //读取试验方法生成控制命令数组 
                            if (!string.IsNullOrEmpty(m_TestSampleNo))
                            {
                                //写入试样信息
                                try
                                {
                                    TestStandard.GBT28289_2012.Twist.CreateCurveFile(_testpath, model28289tw);
                                    Thread.Sleep(5);
                                    if (m_SensorCount > 0)
                                    {
                                        if (ReadMethod("GBT28289-2012", model28289tw.testMethodName, out m_CtrlCommandArray, out loopCount, out m_methodContent, out m_isProLoad))
                                        {
                                            //写入命令数组 
                                            if (loopCount == 0) loopCount = 1;
                                            if (WriteCommand(m_CtrlCommandArray, 12, loopCount, m_isProLoad, "twist", RWconfig.GetAppSettings("machineType")))
                                            {
                                                tsbtn_Start.Enabled = true;
                                            }
                                            else
                                            {
                                                tsbtn_Start.Enabled = false;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show(this, "该试样所指定的试验方法不存在,请重新选择!", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                            tsbtn_Start.Enabled = false;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, "请连接设备!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        tsbtn_Start.Enabled = false;
                                    }
                                }
                                catch (Exception ee)
                                {
                                    MessageBox.Show(ee.ToString());
                                    return;
                                }
                                finally
                                {
                                    this.dataGridView.UseWaitCursor = false;
                                    //tsbtn_Start.Enabled = true;
                                }
                            }
                        }
                        else if (e.Node.ImageIndex == 1)
                        {
                            //选择一根已完成的试验试样编号
                            tsbtn_Start.Enabled = false;
                            TestStandard.GBT28289_2012.Twist.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            ShowResultPanel();
                            int dgvRows = this.dataGridView.Rows.Count;
                            this.zedGraphControl.GraphPane.CurveList.Clear();
                            for (int i = 0; i < dgvRows; i++)
                            {
                                this.dataGridView.Rows[i].Cells[0].Value = false;
                                this.dataGridView.Rows[i].Selected = false;
                                if (this.dataGridView.Rows[i].Cells[3].Value.ToString() == m_TestSampleNo)
                                {
                                    dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, this.dataGridView.Rows[i].Index));
                                }
                            }
                        }
                        break;
                    #endregion

                    default:
                        break;
                }
            }
        }

        private List<gdata> ReadOneListData(string _path, string _curveName)
        {
            //读取曲线         
            List<gdata> lg = new List<gdata>();
            string curvePath = "E:\\衡新试验数据\\Curve\\" + _path + "\\" + _curveName + ".txt";
            if (File.Exists(curvePath))
            {
                using (StreamReader srLine = new StreamReader(curvePath))
                {
                    if (srLine.ReadLine() != null)
                    { string[] testSampleInfo1 = srLine.ReadLine().Split(','); }
                    if (srLine.ReadLine() != null)
                    { string[] testSampleInfo2 = srLine.ReadLine().Split(','); }
                    if (srLine.ReadLine() != null)
                    { string[] testSampleInfo3 = srLine.ReadLine().Split(','); }
                    String line;
                    // Read and display lines from the file until the end of
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
                        lg.Add(_gdata);
                    }
                    srLine.Close();
                    srLine.Dispose();
                    return lg;
                }
            }
            else
            {
                return null;
            }
        }

        //写入试验命令
        private bool WriteControlCommand(Struc.ctrlcommand[] comArry, int commandCount, int loopCount, string testtype, string machineType)
        {
            /*--------------------------------------------------------------*/
            if (m_SensorCount == 0)
            {
                MessageBox.Show(this, "未连接设备", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {

                buf[0] = Convert.ToByte(commandCount);
                buf[1] = Convert.ToByte(loopCount);
                Int32 m_Value1 = 0;
                Int32 m_Value2 = 0;
                int comArryLenth = comArry.Length;
                for (int i = 0; i < comArryLenth; i++)
                {
                    /*--------------------------------------------*/
                    buf[i * 12 + 2] = m_CtrlCommandArray[i].m_CtrlType;
                    buf[i * 12 + 3] = m_CtrlCommandArray[i].m_CtrlChannel;
                    switch (testtype)
                    {
                        case "bend":
                        case "compress":
                        case "shear":
                        case "twist":
                            if (machineType == "0")
                            {
                                m_Value1 = (0 - comArry[i].m_CtrlSpeed);
                                m_Value2 = (0 - comArry[i].m_StopPoint);
                            }
                            else
                            {
                                m_Value1 = comArry[i].m_CtrlSpeed;
                                m_Value2 = comArry[i].m_StopPoint;
                            }
                            break;
                        case "tensile":
                            m_Value1 = comArry[i].m_CtrlSpeed;
                            m_Value2 = comArry[i].m_StopPoint;
                            break;
                    }
                    //m_Value1 = m_CtrlCommandArray[i].m_CtrlSpeed;
                    buf[i * 12 + 4] = (byte)m_Value1;
                    m_Value1 = m_Value1 >> 8;
                    buf[i * 12 + 5] = (byte)m_Value1;
                    m_Value1 = m_Value1 >> 8;
                    buf[i * 12 + 6] = (byte)m_Value1;
                    m_Value1 = m_Value1 >> 8;
                    buf[i * 12 + 7] = (byte)m_Value1;

                    buf[i * 12 + 8] = m_CtrlCommandArray[i].m_StopPointType;
                    buf[i * 12 + 9] = m_CtrlCommandArray[i].m_StopPointChannel;

                    //m_Value2 = m_CtrlCommandArray[i].m_StopPoint;
                    buf[i * 12 + 10] = (byte)m_Value2;
                    m_Value2 = m_Value2 >> 8;
                    buf[i * 12 + 11] = (byte)m_Value2;
                    m_Value2 = m_Value2 >> 8;
                    buf[i * 12 + 12] = (byte)m_Value2;
                    m_Value2 = m_Value2 >> 8;
                    buf[i * 12 + 13] = (byte)m_Value2;

                    /*---------------------------------------------*/
                }

                int len = 122;
                Thread.Sleep(50);
                int ret = RwUsb.WriteData1582(3, buf, len, 3000);			//写数据   
                Thread.Sleep(50);
                if (ret != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 写入命令
        /// </summary>
        /// <param name="comArry">命令数组</param>
        /// <param name="commandCount">命令计数器</param>
        /// <param name="loopCount">循环次数</param>
        /// <param name="preTest">是否预载</param>
        /// <param name="machineType">试验机类型</param>
        /// <returns></returns>
        private bool WriteCommand(Struc.ctrlcommand[] comArry, int commandCount, int loopCount, bool isPreLoad, string testType, string machineType)
        {
            /* 1、由原来的发送长度122改成147，命令数组增加两个元素共 12位，
             * 2、[10]为预测试命令 
             * a.预载值为停止点(停止通道由kN或mm来选择);
             * b.控制模式为控制方式
             * c.预载速度为控制速度
             * 3、[11]为快速返回速度，速度值为返回速度，其他不关心
             * 4、在压缩和弯曲试验中，如果主机为单空间型，控制速度应取反，也就是输入速度为正，发送的控制速度为负，预测试控制速度和快速返回速度除外；
             * 停止点的值，在主机为单空间型时，也应该取反；
             * 5、命令循环数由原来的单字节改为双字节，低字节为数据的低8位，高字节为高8位；
             * 6、因为命令循环数增加了1个字节，因此后续的控制命令在命令数组的位置索引都应该增加1，如控制类型有原来的[i*12+2]变为现在的[i*12+3]
             * 7、命令数组由于增加了两个元素，因此命令数组的索引由原来的10增加12个。
             * 8、命令计数器，原来只传递命令计数器,修改为低四位传递命令数,高四位为预测试是否使能
             * --------------------------------------------------------------*/
            bufCommand = new byte[147];
            if (m_SensorCount == 0)
            {
                MessageBox.Show(this, "未连接设备!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                // * 8、命令计数器，原来只传递命令计数器,修改为低四位传递命令数,高四位为预测试是否使能
                if (isPreLoad)
                    bufCommand[0] = 0x1C;
                else
                    bufCommand[0] = 0x0C;

                //5、命令循环数由原来的单字节改为双字节,先发低后发高
                //buf[1] = Convert.ToByte(loopCount);
                bufCommand[1] = Convert.ToByte(loopCount & 0xFF);//低八位
                bufCommand[2] = Convert.ToByte((loopCount >> 8) & 0xFF);//高八位

                //int value2temp=0;
                int comArryLenth = comArry.Length;
                for (int i = 0; i < comArryLenth; i++)
                {
                    /*--------------------------------------------*/
                    //6、增加索引
                    Int32 m_Value1 = 0;
                    Int32 m_Value2 = 0;
                    bufCommand[i * 12 + 3] = comArry[i].m_CtrlType;
                    bufCommand[i * 12 + 4] = comArry[i].m_CtrlChannel;
                    switch (testType)
                    {
                        case "bend":
                        case "compress":
                        case "shear":
                        case "twist":
                            if (machineType == "0")//单空间型
                            {
                                m_Value2 = (0 - comArry[i].m_StopPoint);
                                if (i != 10 && i != 11)
                                    m_Value1 = (0 - comArry[i].m_CtrlSpeed);
                                else
                                    m_Value1 = comArry[i].m_CtrlSpeed;
                            }
                            else
                            {
                                m_Value1 = comArry[i].m_CtrlSpeed;
                                m_Value2 = comArry[i].m_StopPoint;
                            }

                            break;
                        case "tensile":
                            m_Value1 = comArry[i].m_CtrlSpeed;
                            m_Value2 = comArry[i].m_StopPoint;
                            break;
                        default:
                            m_Value1 = comArry[i].m_CtrlSpeed;
                            m_Value2 = comArry[i].m_StopPoint;
                            break;
                    }

                    bufCommand[i * 12 + 5] = (byte)(m_Value1 & 0xFF);
                    m_Value1 = (m_Value1 >> 8);
                    bufCommand[i * 12 + 6] = (byte)(m_Value1 & 0xFF);
                    m_Value1 = (m_Value1 >> 8);
                    bufCommand[i * 12 + 7] = (byte)(m_Value1 & 0xFF);
                    m_Value1 = (m_Value1 >> 8);
                    bufCommand[i * 12 + 8] = (byte)(m_Value1 & 0xFF);

                    bufCommand[i * 12 + 9] = comArry[i].m_StopPointType;
                    bufCommand[i * 12 + 10] = comArry[i].m_StopPointChannel;

                    //m_Value = m_CtrlCommandArray[i].m_StopPoint;

                    bufCommand[i * 12 + 11] = (byte)(m_Value2 & 0xFF);
                    m_Value2 = (m_Value2 >> 8);
                    bufCommand[i * 12 + 12] = (byte)(m_Value2 & 0xFF);
                    m_Value2 = (m_Value2 >> 8);
                    bufCommand[i * 12 + 13] = (byte)(m_Value2 & 0xFF);
                    m_Value2 = (m_Value2 >> 8);
                    bufCommand[i * 12 + 14] = (byte)(m_Value2 & 0xFF);
                    /*---------------------------------------------*/

                }

                int len = 147;//1、原来的长度为122
                //Test
                //StringBuilder sb = new StringBuilder();
                //sb.Append("写入控制器命令为:\r\n");
                //foreach (byte b in bufCommand)
                //{
                //    sb.Append("0x" + b.ToString("X2") + " ");
                //}
                //sb.Append("\r\n写入控制器预载命令为\r\n: ");
                //for (int i = 123; i <= 134; i++)
                //{
                //    sb.Append("0x" + bufCommand[i].ToString("X2") + " ");
                //}

                //MessageBox.Show(sb.ToString());

                Thread.Sleep(30);
                int ret = RwUsb.WriteData1582(3, bufCommand, len, 3000);			//写数据  
                Thread.Sleep(30);
                if (ret != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        //读取试验方法，生成 m_CtrlCommandArray 控制命令数组
        private bool ReadMethod(string testType, string testMethodName, out Struc.ctrlcommand[] commandArray, out int loopCount, out string methodContent, out bool isProLoad)
        {
            int m_ScaleValue = 0;
            //新命令 12 段
            commandArray = new Struc.ctrlcommand[12];
            loopCount = 0;
            methodContent = string.Empty;
            isProLoad = false;
            switch (testType)
            {
                case "GBT228-2010":
                    BLL.ControlMethod bllControlMethod = new HR_Test.BLL.ControlMethod();
                    Model.ControlMethod modelControlMethod = bllControlMethod.GetModel(testMethodName);
                    if (modelControlMethod != null)
                    {
                        TestMethodDis(this.txtMethod, "试验方法:" + testMethodName + "\r\n", Color.Black);
                        TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                        #region ///////////////////////是 否 预 载?///////////////////////////////
                        isProLoad = modelControlMethod.isProLoad;
                        if (modelControlMethod.isProLoad)
                        {
                            Struc.ctrlcommand m_Command = new Struc.ctrlcommand();
                            TestMethodDis(this.txtMethod, "是否预载:是;\r\n", Color.Black);
                            //控制预载 变形或负荷 
                            switch (modelControlMethod.proLoadControlType)
                            {
                                case 0://位移
                                    //泛型命令组
                                    m_Command.m_CtrlType = 0x80;
                                    m_Command.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                    m_Command.m_CtrlSpeed = (int)(modelControlMethod.proLoadSpeed * 1000);//位移控制速度
                                    TestMethodDis(this.txtMethod, "预载控制:位移; 速度:" + modelControlMethod.proLoadSpeed.ToString() + " mm/min\r\n", Color.Crimson);
                                    break;
                                case 1://负荷
                                    //泛型命令组
                                    m_Command.m_CtrlType = 0x81;
                                    m_Command.m_CtrlChannel = m_LSensorArray[0].SensorIndex;
                                    double m_SR = GetSensorSR((ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale);
                                    m_Command.m_CtrlSpeed = (int)((modelControlMethod.proLoadSpeed * 1000.0 / m_SR) + 0.2d);
                                    TestMethodDis(this.txtMethod, "预载控制:负荷; 速度:" + modelControlMethod.proLoadSpeed.ToString() + " kN/s\r\n", Color.Crimson);
                                    break;
                            }

                            //预载值停止点类型 变形或负荷
                            switch (modelControlMethod.proLoadType)
                            {
                                case 0://变形 
                                    m_Command.m_StopPointType = 0x82;
                                    m_Command.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                    double m_SR = GetSensorSR((ushort)m_SensorArray[m_ESensorArray[0].SensorIndex].scale);
                                    m_Command.m_StopPoint = (int)((modelControlMethod.proLoadValue * 1000.0 / m_SR) + 0.2d);
                                    methodContent += "预载停止点:变形; 值:" + modelControlMethod.proLoadValue + " mm\r\n";
                                    TestMethodDis(this.txtMethod, "预载停止点:变形; 值:" + modelControlMethod.proLoadValue.ToString() + " mm\r\n", Color.Crimson);
                                    break;
                                case 1: //负荷 
                                    //停止的值， 负荷
                                    m_Command.m_StopPointType = 0x81;
                                    m_Command.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                    double m_SR1 = GetSensorSR((ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale);
                                    m_Command.m_StopPoint = (int)((modelControlMethod.proLoadValue * 1000.0 / m_SR1) + 0.2d);
                                    //MessageBox.Show("负荷传感器序号:"+ m_LSensorArray[0].SensorIndex.ToString()+"负荷分辨率:" + m_SR1.ToString() + "负荷停止点:" + m_Command.m_StopPoint.ToString());
                                    TestMethodDis(this.txtMethod, "预载停止点:负荷; 值:" + modelControlMethod.proLoadValue.ToString() + " kN\r\n", Color.Crimson);
                                    break;
                            }
                            commandArray[10] = m_Command;
                        }
                        #endregion

                        #region///////////////////////不连续屈服 和 连续屈服//////////////


                        if (modelControlMethod.isLxqf == 1 || modelControlMethod.isLxqf == 2)
                        {
                            #region//////////////////弹性阶段//////////////////
                            //TestMethodDis(this.txtMethod, "试验类型:不连续屈服\r\n--------\r\n", Color.Black);
                            string[] controlType1 = modelControlMethod.controlType1.Split(',');

                            if (controlType1.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(controlType1, "弹性"));
                            }
                            #endregion
                        }

                        if (modelControlMethod.isLxqf == 1)
                        {
                            #region//////////////////屈服阶段//////////////////
                            //methodContent += "--------\r\n";
                            TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                            Struc.ctrlcommand m_Command2 = new Struc.ctrlcommand();
                            string[] controlType2 = modelControlMethod.controlType2.Split(',');

                            if (controlType2.Length == 4)
                            {

                                #region 屈服阶段控制
                                //控制方式：位移,eLc
                                switch (controlType2[0])
                                {// 0,5.3,1,20.68
                                    case "0"://位移控制
                                        m_Command2.m_CtrlType = 0x80;
                                        m_Command2.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command2.m_CtrlSpeed = (int)(double.Parse(controlType2[1]) * 1000.0);//位移控制速度

                                        TestMethodDis(this.txtMethod, "屈服阶段控制:位移\r\n", Color.Blue);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType2[1].ToString() + " mm/min \r\n", Color.Blue);

                                        break;
                                    case "1"://eLc控制   Lc*eLc=位移速度
                                        m_Command2.m_CtrlType = 0x80;//0,5.3,1,20.68
                                        m_Command2.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        //转换成位移控制速度
                                        m_Command2.m_CtrlSpeed = (int)(double.Parse(controlType2[1]) * m_Lc * 1000.0);

                                        TestMethodDis(this.txtMethod, "屈服阶段控制:eLc\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType2[1].ToString() + " /s\r\n", Color.Crimson);

                                        break;
                                }
                                #endregion

                                #region 屈服阶段停止转换点

                                //停止转换点：位移 0x80,负荷 0x81,变形 0x82, 应力,应变,
                                switch (controlType2[2])
                                {
                                    case "0":
                                        //停止的类型 位移
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x80;
                                        m_Command2.m_StopPointChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command2.m_StopPoint = (int)(double.Parse(controlType2[3]) * 1000.0);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:位移\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3].ToString() + " mm\r\n", Color.Crimson);
                                        break;
                                    case "1":
                                        //停止的类型 负荷
                                        m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x81;
                                        m_Command2.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        double m_SR = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 / m_SR) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:负荷\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3].ToString() + " kN\r\n", Color.Crimson);
                                        break;
                                    case "2":
                                        //停止的类型 变形
                                        m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));

                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x82;
                                        m_Command2.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                        double m_SR1 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 / m_SR1) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:变形\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3].ToString() + " mm\r\n", Color.Crimson);

                                        break;
                                    case "3":
                                        //停止类型 应力 转换为负荷 为 应力*面积 = 负荷；1Mp * 1mm^2 = 1N                                   
                                        m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x81;
                                        m_Command2.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        double m_SR2 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * m_S0 * 1000.0 / m_SR2) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:应力\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3].ToString() + " MPa\r\n", Color.Crimson);

                                        break;
                                    case "4":
                                        //停止类型 （应变 = 变形/标距） 转换成 （变形 = 应变 * 引伸计标距Le）

                                        m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));

                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x82;
                                        m_Command2.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                        double m_SR3 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 * m_Le / m_SR3) + 0.2d);

                                        TestMethodDis(this.txtMethod, "屈服阶段结束:应变\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3].ToString() + " %\r\n", Color.Crimson);

                                        break;
                                }

                                #endregion

                                m_CtrlCommandList.Add(m_Command2);
                            }


                            #endregion
                        }

                        if (modelControlMethod.isLxqf == 1 || modelControlMethod.isLxqf == 2)
                        {
                            #region//////////////////加工硬化阶段//////////////////


                            TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                            Struc.ctrlcommand m_Command3 = new Struc.ctrlcommand();
                            string[] controlType3 = null;
                            if (modelControlMethod.isLxqf == 1)
                                controlType3 = modelControlMethod.controlType3.Split(',');
                            if (modelControlMethod.isLxqf == 2)
                                controlType3 = modelControlMethod.controlType2.Split(',');

                            if (controlType3.Length == 2)
                            {
                                #region 加工硬化阶段控制
                                //控制方式：位移
                                switch (controlType3[0])
                                {// 0,5.3,1,20.68
                                    case "0":
                                        m_Command3.m_CtrlType = 0x80;
                                        m_Command3.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command3.m_CtrlSpeed = (int)(double.Parse(controlType3[1]) * 1000.0);//位移控制速度

                                        m_ScaleValue = GetScale(m_SensorArray[m_LSensorArray[0].SensorIndex].scale);
                                        m_Command3.m_StopPointType = 0x81;
                                        m_Command3.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        m_Command3.m_StopPoint = m_ScaleValue;//加工硬化阶段停止点为负荷量程

                                        TestMethodDis(this.txtMethod, "加工硬化阶段控制:位移\r\n", Color.Purple);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType3[1].ToString() + " mm/min\r\n", Color.Purple);

                                        break;
                                    case "1"://eLc控制 eLc * Lc =位移速度
                                        m_Command3.m_CtrlType = 0x80;//0,5.3,1,20.68
                                        m_Command3.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        //转换成位移控制速度
                                        m_Command3.m_CtrlSpeed = (int)(double.Parse(controlType3[1]) * m_Lc * 1000.0);

                                        ////停止的类型 位移
                                        //m_Command3.m_StopPointType = 0x80;
                                        //m_Command3.m_StopPointChannel = m_DSensorArray[0].SensorIndex;

                                        //m_ScaleValue = m_SensorArray[m_DSensorArray[0].SensorIndex].scale;
                                        //m_ScaleValue = m_ScaleValue >> 8;
                                        //m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_DSensorArray[0].SensorIndex].scale & 0x000f));

                                        //m_Command3.m_StopPoint = (int)(m_ScaleValue);//加工硬化阶段停止点为量程

                                        //停止的类型 负荷
                                        m_ScaleValue = GetScale(m_SensorArray[m_LSensorArray[0].SensorIndex].scale);
                                        m_Command3.m_StopPointType = 0x81;
                                        m_Command3.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        m_Command3.m_StopPoint = m_ScaleValue;//加工硬化阶段停止点为负荷量程 

                                        TestMethodDis(this.txtMethod, "加工硬化阶段控制:eLc\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType3[1].ToString() + " /s\r\n", Color.Crimson);

                                        break;
                                }

                                #endregion

                                m_CtrlCommandList.Add(m_Command3);
                            }

                            #endregion
                        }


                        if (modelControlMethod.isTakeDownExten)
                        {
                            TestMethodDis(this.txtMethod, "试验过程取引伸计:是\r\n", Color.Green);
                            switch (modelControlMethod.selResultID)
                            {
                                case 0://%
                                    m_extenValue = (float)modelControlMethod.extenValue;
                                    m_extenType = 0;
                                    TestMethodDis(this.txtMethod, "取引伸计值:" + modelControlMethod.extenValue.ToString() + "%\r\n", Color.Green);
                                    TestMethodDis(this.txtMethod, "取引伸计通道:" + modelControlMethod.extenChannel.ToString() + "\r\n", Color.Green);
                                    break;
                                case 1://mm
                                    m_extenValue = (float)modelControlMethod.extenValue * 1000;
                                    m_extenType = 1;
                                    TestMethodDis(this.txtMethod, "取引伸计值:" + modelControlMethod.extenValue.ToString() + "mm\r\n", Color.Green);
                                    TestMethodDis(this.txtMethod, "取引伸计通道:" + modelControlMethod.extenChannel.ToString() + "\r\n", Color.Green);
                                    break;
                                default:
                                    m_extenType = 2;
                                    break;
                            }
                        }
                        else
                        {
                            TestMethodDis(this.txtMethod, "试验过程未使用引伸计\r\n", Color.Green);
                        }

                        #endregion

                        #region 自定义试验
                        if (modelControlMethod.isLxqf == 3)
                        {
                            TestMethodDis(this.txtMethod, "试验类型:自定义试验\r\n--------\r\n", Color.Black);

                            string[] customControlType1 = modelControlMethod.controlType1.Split(',');
                            if (customControlType1.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType1, "1"));
                            }

                            string[] customControlType2 = modelControlMethod.controlType2.Split(',');
                            if (customControlType2.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType2, "2"));
                            }

                            string[] customControlType3 = modelControlMethod.controlType3.Split(',');
                            if (customControlType2.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType3, "3"));
                            }

                            string[] customControlType4 = modelControlMethod.controlType4.Split(',');
                            if (customControlType4.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType4, "4"));
                            }

                            string[] customControlType5 = modelControlMethod.controlType5.Split(',');
                            if (customControlType5.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType5, "5"));
                            }

                            string[] customControlType6 = modelControlMethod.controlType6.Split(',');
                            if (customControlType6.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType6, "6"));
                            }

                            string[] customControlType7 = modelControlMethod.controlType7.Split(',');
                            if (customControlType7.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType7, "7"));
                            }

                            string[] customControlType8 = modelControlMethod.controlType8.Split(',');
                            if (customControlType8.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType8, "8"));
                            }

                            string[] customControlType9 = modelControlMethod.controlType9.Split(',');
                            if (customControlType9.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType9, "9"));
                            }

                            string[] customControlType10 = modelControlMethod.controlType10.Split(',');
                            if (customControlType10.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType10, "10"));
                            }

                            //string[] customControlType11 = modelControlMethod.controlType11.Split(',');
                            //if (customControlType11.Length == 4)
                            //{
                            //    m_CtrlCommandList.Add(GetCtrlCommand(customControlType11, "11"));
                            //}

                            //string[] customControlType12 = modelControlMethod.controlType12.Split(',');
                            //if (customControlType12.Length == 4)
                            //{
                            //    m_CtrlCommandList.Add(GetCtrlCommand(customControlType12, "12"));
                            //}
                        }
                        #endregion

                        //commandArray = m_CtrlCommandList.ToArray();
                        for (int i = 0; i < m_CtrlCommandList.Count; i++)
                        {
                            commandArray[i] = m_CtrlCommandList[i];
                        }

                        //最后一项是返回速度
                        int returnspeed = 200000;

                        if (!string.IsNullOrEmpty(modelControlMethod.controlType12))
                            returnspeed = int.Parse(modelControlMethod.controlType12.ToString()) * 1000;
                        Struc.ctrlcommand speedorder = new Struc.ctrlcommand();
                        speedorder.m_CtrlSpeed = returnspeed;
                        commandArray[11] = speedorder;

                        m_StopValue = (double)modelControlMethod.stopValue;
                        if (m_StopValue == 0)
                            m_StopValue = 80;
                        loopCount = (int)modelControlMethod.circleNum;

                        m_methodContent = methodContent;
                    }
                    break;
                case "GBT3354-2014":
                    BLL.GBT3354_Method bll3354Method = new HR_Test.BLL.GBT3354_Method();
                    Model.GBT3354_Method model3354Method = bll3354Method.GetModel(testMethodName);
                    if (model3354Method != null)
                    {
                        TestMethodDis(this.txtMethod, "试验方法:" + testMethodName + "\r\n", Color.Black);
                        TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                        #region ///////////////////////是 否 预 载?///////////////////////////////
                        isProLoad = model3354Method.isProLoad;
                        if (model3354Method.isProLoad)
                        {
                            Struc.ctrlcommand m_Command = new Struc.ctrlcommand();
                            TestMethodDis(this.txtMethod, "是否预载:是;\r\n", Color.Black);
                            //控制预载 变形或负荷 
                            switch (model3354Method.proLoadControlType)
                            {
                                case 0://位移
                                    //泛型命令组
                                    m_Command.m_CtrlType = 0x80;
                                    m_Command.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                    m_Command.m_CtrlSpeed = (int)(model3354Method.proLoadSpeed * 1000);//位移控制速度
                                    TestMethodDis(this.txtMethod, "预载控制:位移; 速度:" + model3354Method.proLoadSpeed.ToString() + " mm/min\r\n", Color.Crimson);
                                    break;
                                case 1://负荷
                                    //泛型命令组
                                    m_Command.m_CtrlType = 0x81;
                                    m_Command.m_CtrlChannel = m_LSensorArray[0].SensorIndex;
                                    double m_SR = GetSensorSR((ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale);
                                    m_Command.m_CtrlSpeed = (int)((model3354Method.proLoadSpeed * 1000.0 / m_SR) + 0.2d);
                                    TestMethodDis(this.txtMethod, "预载控制:负荷; 速度:" + model3354Method.proLoadSpeed.ToString() + " kN/s\r\n", Color.Crimson);
                                    break;
                            }

                            //预载值停止点类型 变形或负荷
                            switch (model3354Method.proLoadType)
                            {
                                case 0://变形 
                                    m_Command.m_StopPointType = 0x82;
                                    m_Command.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                    double m_SR = GetSensorSR((ushort)m_SensorArray[m_ESensorArray[0].SensorIndex].scale);
                                    m_Command.m_StopPoint = (int)((model3354Method.proLoadValue * 1000.0 / m_SR) + 0.2d);
                                    methodContent += "预载停止点:变形; 值:" + model3354Method.proLoadValue + " mm\r\n";
                                    TestMethodDis(this.txtMethod, "预载停止点:变形; 值:" + model3354Method.proLoadValue.ToString() + " mm\r\n", Color.Crimson);
                                    break;
                                case 1: //负荷 
                                    //停止的值， 负荷
                                    m_Command.m_StopPointType = 0x81;
                                    m_Command.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                    double m_SR1 = GetSensorSR((ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale);
                                    m_Command.m_StopPoint = (int)((model3354Method.proLoadValue * 1000.0 / m_SR1) + 0.2d);
                                    //MessageBox.Show("负荷传感器序号:"+ m_LSensorArray[0].SensorIndex.ToString()+"负荷分辨率:" + m_SR1.ToString() + "负荷停止点:" + m_Command.m_StopPoint.ToString());
                                    TestMethodDis(this.txtMethod, "预载停止点:负荷; 值:" + model3354Method.proLoadValue.ToString() + " kN\r\n", Color.Crimson);
                                    break;
                            }
                            commandArray[10] = m_Command;
                        }
                        #endregion

                        #region///////////////////////不连续屈服 和 连续屈服//////////////


                        if (model3354Method.isLxqf == 1 || model3354Method.isLxqf == 2)
                        {
                            #region//////////////////弹性阶段//////////////////
                            //TestMethodDis(this.txtMethod, "试验类型:不连续屈服\r\n--------\r\n", Color.Black);
                            string[] controlType1 = model3354Method.controlType1.Split(',');

                            if (controlType1.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(controlType1, "弹性"));
                            }
                            #endregion
                        }

                        if (model3354Method.isLxqf == 1)
                        {
                            #region//////////////////屈服阶段//////////////////
                            //methodContent += "--------\r\n";
                            TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                            Struc.ctrlcommand m_Command2 = new Struc.ctrlcommand();
                            string[] controlType2 = model3354Method.controlType2.Split(',');

                            if (controlType2.Length == 4)
                            {

                                #region 屈服阶段控制
                                //控制方式：位移,eLc
                                switch (controlType2[0])
                                {// 0,5.3,1,20.68
                                    case "0"://位移控制
                                        m_Command2.m_CtrlType = 0x80;
                                        m_Command2.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command2.m_CtrlSpeed = (int)(double.Parse(controlType2[1]) * 1000.0);//位移控制速度

                                        TestMethodDis(this.txtMethod, "屈服阶段控制:位移\r\n", Color.Blue);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType2[1].ToString() + " mm/min \r\n", Color.Blue);

                                        break;
                                    case "1"://eLc控制   Lc*eLc=位移速度
                                        m_Command2.m_CtrlType = 0x80;//0,5.3,1,20.68
                                        m_Command2.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        //转换成位移控制速度
                                        m_Command2.m_CtrlSpeed = (int)(double.Parse(controlType2[1]) * m_Lc * 1000.0);

                                        TestMethodDis(this.txtMethod, "屈服阶段控制:eLc\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType2[1].ToString() + " /s\r\n", Color.Crimson);

                                        break;
                                }
                                #endregion

                                #region 屈服阶段停止转换点

                                //停止转换点：位移 0x80,负荷 0x81,变形 0x82, 应力,应变,
                                switch (controlType2[2])
                                {
                                    case "0":
                                        //停止的类型 位移
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x80;
                                        m_Command2.m_StopPointChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command2.m_StopPoint = (int)(double.Parse(controlType2[3]) * 1000.0);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:位移\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3].ToString() + " mm\r\n", Color.Crimson);
                                        break;
                                    case "1":
                                        //停止的类型 负荷
                                        m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x81;
                                        m_Command2.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        double m_SR = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 / m_SR) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:负荷\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3].ToString() + " kN\r\n", Color.Crimson);
                                        break;
                                    case "2":
                                        //停止的类型 变形
                                        m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));

                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x82;
                                        m_Command2.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                        double m_SR1 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 / m_SR1) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:变形\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3].ToString() + " mm\r\n", Color.Crimson);

                                        break;
                                    case "3":
                                        //停止类型 应力 转换为负荷 为 应力*面积 = 负荷；1Mp * 1mm^2 = 1N                                   
                                        m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x81;
                                        m_Command2.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        double m_SR2 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * m_S0 * 1000.0 / m_SR2) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:应力\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3].ToString() + " MPa\r\n", Color.Crimson);

                                        break;
                                    case "4":
                                        //停止类型 （应变 = 变形/标距） 转换成 （变形 = 应变 * 引伸计标距Le）

                                        m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));

                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x82;
                                        m_Command2.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                        double m_SR3 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 * m_Le / m_SR3) + 0.2d);

                                        TestMethodDis(this.txtMethod, "屈服阶段结束:应变\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3].ToString() + " %\r\n", Color.Crimson);

                                        break;
                                }

                                #endregion

                                m_CtrlCommandList.Add(m_Command2);
                            }


                            #endregion
                        }

                        if (model3354Method.isLxqf == 1 || model3354Method.isLxqf == 2)
                        {
                            #region//////////////////加工硬化阶段//////////////////


                            TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                            Struc.ctrlcommand m_Command3 = new Struc.ctrlcommand();
                            string[] controlType3 = null;
                            if (model3354Method.isLxqf == 1)
                                controlType3 = model3354Method.controlType3.Split(',');
                            if (model3354Method.isLxqf == 2)
                                controlType3 = model3354Method.controlType2.Split(',');

                            if (controlType3.Length == 2)
                            {
                                #region 加工硬化阶段控制
                                //控制方式：位移
                                switch (controlType3[0])
                                {// 0,5.3,1,20.68
                                    case "0":
                                        m_Command3.m_CtrlType = 0x80;
                                        m_Command3.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command3.m_CtrlSpeed = (int)(double.Parse(controlType3[1]) * 1000.0);//位移控制速度

                                        m_ScaleValue = GetScale(m_SensorArray[m_LSensorArray[0].SensorIndex].scale);
                                        m_Command3.m_StopPointType = 0x81;
                                        m_Command3.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        m_Command3.m_StopPoint = m_ScaleValue;//加工硬化阶段停止点为负荷量程

                                        TestMethodDis(this.txtMethod, "加工硬化阶段控制:位移\r\n", Color.Purple);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType3[1].ToString() + " mm/min\r\n", Color.Purple);

                                        break;
                                    case "1"://eLc控制 eLc * Lc =位移速度
                                        m_Command3.m_CtrlType = 0x80;//0,5.3,1,20.68
                                        m_Command3.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        //转换成位移控制速度
                                        m_Command3.m_CtrlSpeed = (int)(double.Parse(controlType3[1]) * m_Lc * 1000.0);

                                        ////停止的类型 位移
                                        //m_Command3.m_StopPointType = 0x80;
                                        //m_Command3.m_StopPointChannel = m_DSensorArray[0].SensorIndex;

                                        //m_ScaleValue = m_SensorArray[m_DSensorArray[0].SensorIndex].scale;
                                        //m_ScaleValue = m_ScaleValue >> 8;
                                        //m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_DSensorArray[0].SensorIndex].scale & 0x000f));

                                        //m_Command3.m_StopPoint = (int)(m_ScaleValue);//加工硬化阶段停止点为量程

                                        //停止的类型 负荷
                                        m_ScaleValue = GetScale(m_SensorArray[m_LSensorArray[0].SensorIndex].scale);
                                        m_Command3.m_StopPointType = 0x81;
                                        m_Command3.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        m_Command3.m_StopPoint = m_ScaleValue;//加工硬化阶段停止点为负荷量程 

                                        TestMethodDis(this.txtMethod, "加工硬化阶段控制:eLc\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType3[1].ToString() + " /s\r\n", Color.Crimson);

                                        break;
                                }

                                #endregion

                                m_CtrlCommandList.Add(m_Command3);
                            }

                            #endregion
                        }


                        if (model3354Method.isTakeDownExten)
                        {
                            TestMethodDis(this.txtMethod, "试验过程取引伸计:是\r\n", Color.Green);
                            switch (model3354Method.selResultID)
                            {
                                case 0://%
                                    m_extenValue = (float)model3354Method.extenValue;
                                    m_extenType = 0;
                                    TestMethodDis(this.txtMethod, "取引伸计值:" + model3354Method.extenValue.ToString() + "%\r\n", Color.Green);
                                    TestMethodDis(this.txtMethod, "取引伸计通道:" + model3354Method.extenChannel.ToString() + "\r\n", Color.Green);
                                    break;
                                case 1://mm
                                    m_extenValue = (float)model3354Method.extenValue * 1000;
                                    m_extenType = 1;
                                    TestMethodDis(this.txtMethod, "取引伸计值:" + model3354Method.extenValue.ToString() + "mm\r\n", Color.Green);
                                    TestMethodDis(this.txtMethod, "取引伸计通道:" + model3354Method.extenChannel.ToString() + "\r\n", Color.Green);
                                    break;
                                default:
                                    m_extenType = 2;
                                    break;
                            }
                        }
                        else
                        {
                            TestMethodDis(this.txtMethod, "试验过程未使用引伸计\r\n", Color.Green);
                        }

                        #endregion

                        #region 自定义试验
                        if (model3354Method.isLxqf == 3)
                        {
                            TestMethodDis(this.txtMethod, "试验类型:自定义试验\r\n--------\r\n", Color.Black);

                            string[] customControlType1 = model3354Method.controlType1.Split(',');
                            if (customControlType1.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType1, "1"));
                            }

                            string[] customControlType2 = model3354Method.controlType2.Split(',');
                            if (customControlType2.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType2, "2"));
                            }

                            string[] customControlType3 = model3354Method.controlType3.Split(',');
                            if (customControlType2.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType3, "3"));
                            }

                            string[] customControlType4 = model3354Method.controlType4.Split(',');
                            if (customControlType4.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType4, "4"));
                            }

                            string[] customControlType5 = model3354Method.controlType5.Split(',');
                            if (customControlType5.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType5, "5"));
                            }

                            string[] customControlType6 = model3354Method.controlType6.Split(',');
                            if (customControlType6.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType6, "6"));
                            }

                            string[] customControlType7 = model3354Method.controlType7.Split(',');
                            if (customControlType7.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType7, "7"));
                            }

                            string[] customControlType8 = model3354Method.controlType8.Split(',');
                            if (customControlType8.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType8, "8"));
                            }

                            string[] customControlType9 = model3354Method.controlType9.Split(',');
                            if (customControlType9.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType9, "9"));
                            }

                            string[] customControlType10 = model3354Method.controlType10.Split(',');
                            if (customControlType10.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType10, "10"));
                            }

                            //string[] customControlType11 = modelControlMethod.controlType11.Split(',');
                            //if (customControlType11.Length == 4)
                            //{
                            //    m_CtrlCommandList.Add(GetCtrlCommand(customControlType11, "11"));
                            //}

                            //string[] customControlType12 = modelControlMethod.controlType12.Split(',');
                            //if (customControlType12.Length == 4)
                            //{
                            //    m_CtrlCommandList.Add(GetCtrlCommand(customControlType12, "12"));
                            //}
                        }
                        #endregion

                        //commandArray = m_CtrlCommandList.ToArray();
                        for (int i = 0; i < m_CtrlCommandList.Count; i++)
                        {
                            commandArray[i] = m_CtrlCommandList[i];
                        }

                        //最后一项是返回速度
                        int returnspeed = 200000;

                        if (!string.IsNullOrEmpty(model3354Method.controlType12))
                            returnspeed = int.Parse(model3354Method.controlType12.ToString()) * 1000;
                        Struc.ctrlcommand speedorder = new Struc.ctrlcommand();
                        speedorder.m_CtrlSpeed = returnspeed;
                        commandArray[11] = speedorder;

                        m_StopValue = (double)model3354Method.stopValue;
                        if (m_StopValue == 0)
                            m_StopValue = 80;
                        loopCount = (int)model3354Method.circleNum;

                        m_methodContent = methodContent;
                    }
                    break;
                case "GBT7314-2005":
                    BLL.ControlMethod_C bllControlMethod_C = new HR_Test.BLL.ControlMethod_C();
                    Model.ControlMethod_C modelControlMethod_C = bllControlMethod_C.GetModel(testMethodName);
                    if (modelControlMethod_C != null)
                    {
                        TestMethodDis(this.txtMethod, "试验方法:" + testMethodName + "\r\n", Color.Black);
                        TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                        #region ///////////////////////是 否 预 载?///////////////////////////////
                        isProLoad = modelControlMethod_C.isProLoad;
                        if (modelControlMethod_C.isProLoad)
                        {
                            Struc.ctrlcommand m_Command = new Struc.ctrlcommand();
                            //methodContent += "是否预载:是;\r\n";
                            TestMethodDis(this.txtMethod, "是否预载:是;\r\n", Color.Black);

                            //控制预载 变形或负荷 
                            switch (modelControlMethod_C.proLoadControlType)
                            {
                                case 0://位移
                                    //泛型命令组
                                    m_Command.m_CtrlType = 0x80;
                                    m_Command.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                    m_Command.m_CtrlSpeed = (int)(modelControlMethod_C.proLoadSpeed * 1000);//位移控制速度
                                    TestMethodDis(this.txtMethod, "预载控制:位移; 速度:" + modelControlMethod_C.proLoadSpeed.ToString() + " mm/min\r\n", Color.Crimson);
                                    break;
                                case 1://负荷
                                    //泛型命令组
                                    m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                    m_ScaleValue = m_ScaleValue >> 8;
                                    m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                                    //m_ScaleValue = (int)GetScale((ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale);
                                    m_Command.m_CtrlType = 0x81;
                                    m_Command.m_CtrlChannel = m_LSensorArray[0].SensorIndex;
                                    double m_SR = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                    m_Command.m_CtrlSpeed = (int)((modelControlMethod_C.proLoadSpeed * 1000.0 / m_SR) + 0.2d);
                                    TestMethodDis(this.txtMethod, "预载控制:负荷; 速度:" + modelControlMethod_C.proLoadSpeed.ToString() + " kN/s\r\n", Color.Crimson);
                                    break;
                            }

                            //预载值停止点类型 变形或负荷
                            switch (modelControlMethod_C.proLoadType)
                            {
                                case 0://变形 

                                    //停止的值， 变形
                                    m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                    m_ScaleValue = m_ScaleValue >> 8;
                                    m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));
                                    //泛型命令组
                                    m_Command.m_StopPointType = 0x82;
                                    m_Command.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                    double m_SR = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                    m_Command.m_StopPoint = (int)((modelControlMethod_C.proLoadValue * 1000.0 / m_SR) + 0.2d);
                                    //methodContent += "预载停止点:变形; 值:" + modelControlMethod_C.proLoadValue + " mm\r\n";
                                    TestMethodDis(this.txtMethod, "预载停止点:变形; 值:" + modelControlMethod_C.proLoadValue.ToString() + " mm\r\n", Color.Crimson);
                                    break;
                                case 1: //负荷 
                                    //停止的值， 负荷
                                    m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                    m_ScaleValue = m_ScaleValue >> 8;
                                    m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));

                                    //泛型命令组
                                    m_Command.m_StopPointType = 0x81;
                                    m_Command.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                    double m_SR1 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                    m_Command.m_StopPoint = (int)((modelControlMethod_C.proLoadValue * 1000.0 / m_SR1) + 0.2d);

                                    //methodContent += "预载停止点:负荷; 值:" + modelControlMethod_C.proLoadValue + " mm\r\n";
                                    TestMethodDis(this.txtMethod, "预载停止点:负荷; 值:" + modelControlMethod_C.proLoadValue + " kN\r\n", Color.Crimson);

                                    break;
                            }
                            //m_CtrlCommandList.Add(m_Command);
                            commandArray[10] = m_Command;
                        }
                        #endregion

                        #region///////////////////////不连续屈服 和 连续屈服//////////////


                        if (modelControlMethod_C.isLxqf == 1 || modelControlMethod_C.isLxqf == 2)
                        {
                            #region//////////////////弹性阶段//////////////////

                            //TestMethodDis(this.txtMethod, "试验类型:不连续屈服\r\n--------\r\n", Color.Black);

                            string[] controlType1 = modelControlMethod_C.controlType1.Split(',');

                            if (controlType1.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(controlType1, "弹性"));
                            }
                            #endregion
                        }

                        if (modelControlMethod_C.isLxqf == 1)
                        {
                            #region//////////////////屈服阶段//////////////////
                            //methodContent += "--------\r\n";
                            TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                            Struc.ctrlcommand m_Command2 = new Struc.ctrlcommand();
                            string[] controlType2 = modelControlMethod_C.controlType2.Split(',');

                            if (controlType2.Length == 4)
                            {

                                #region 屈服阶段控制
                                //控制方式：位移,eLc
                                switch (controlType2[0])
                                {// 0,5.3,1,20.68
                                    case "0"://位移控制
                                        m_Command2.m_CtrlType = 0x80;
                                        m_Command2.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command2.m_CtrlSpeed = (int)(double.Parse(controlType2[1]) * 1000.0);//位移控制速度

                                        TestMethodDis(this.txtMethod, "屈服阶段控制:位移\r\n", Color.Blue);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType2[1].ToString() + " mm/min \r\n", Color.Blue);

                                        break;
                                    case "1"://eLc控制   Lc*eLc=位移速度
                                        m_Command2.m_CtrlType = 0x80;//0,5.3,1,20.68
                                        m_Command2.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        //转换成位移控制速度
                                        m_Command2.m_CtrlSpeed = (int)(double.Parse(controlType2[1]) * m_Lc * 1000.0);

                                        TestMethodDis(this.txtMethod, "屈服阶段控制:eLc\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType2[1].ToString() + " /s\r\n", Color.Crimson);

                                        break;
                                }
                                #endregion

                                #region 屈服阶段停止转换点

                                //停止转换点：位移 0x80,负荷 0x81,变形 0x82, 应力,应变,
                                switch (controlType2[2])
                                {
                                    case "0":
                                        //停止的类型 位移
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x80;
                                        m_Command2.m_StopPointChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command2.m_StopPoint = (int)(double.Parse(controlType2[3]) * 1000.0);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:位移\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " mm\r\n", Color.Crimson);
                                        break;
                                    case "1":
                                        //停止的类型 负荷
                                        m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x81;
                                        m_Command2.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        double m_SR = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 / m_SR) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:负荷\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " kN\r\n", Color.Crimson);
                                        break;
                                    case "2":
                                        //停止的类型 变形
                                        m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));

                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x82;
                                        m_Command2.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                        double m_SR1 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 / m_SR1) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:变形\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " mm\r\n", Color.Crimson);

                                        break;
                                    case "3":
                                        //停止类型 应力 转换为负荷 为 应力*面积 = 负荷；1Mp * 1mm^2 = 1N                                   
                                        m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x81;
                                        m_Command2.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        double m_SR2 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * m_S0 * 1000.0 / m_SR2) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:应力\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " MPa\r\n", Color.Crimson);

                                        break;
                                    case "4":
                                        //停止类型 （应变 = 变形/标距） 转换成 （变形 = 应变 * 引伸计标距Le）

                                        m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));

                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x82;
                                        m_Command2.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                        double m_SR3 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 * m_Le / m_SR3) + 0.2d);

                                        TestMethodDis(this.txtMethod, "屈服阶段结束:应变\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " %\r\n", Color.Crimson);

                                        break;
                                }

                                #endregion

                                m_CtrlCommandList.Add(m_Command2);
                            }


                            #endregion
                        }

                        if (modelControlMethod_C.isLxqf == 1 || modelControlMethod_C.isLxqf == 2)
                        {
                            #region//////////////////加工硬化阶段//////////////////


                            TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                            Struc.ctrlcommand m_Command3 = new Struc.ctrlcommand();
                            string[] controlType3 = modelControlMethod_C.controlType3.Split(',');

                            if (controlType3.Length == 2)
                            {
                                #region 加工硬化阶段控制
                                //控制方式：位移,eLc
                                switch (controlType3[0])
                                {// 0,5.3,1,20.68
                                    case "0":
                                        m_Command3.m_CtrlType = 0x80;
                                        m_Command3.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command3.m_CtrlSpeed = (int)(double.Parse(controlType3[1]) * 1000.0);//位移控制速度

                                        m_Command3.m_StopPointType = 0x80;
                                        m_Command3.m_StopPointChannel = m_DSensorArray[0].SensorIndex;

                                        m_ScaleValue = m_SensorArray[m_DSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_DSensorArray[0].SensorIndex].scale & 0x000f));

                                        m_Command3.m_StopPoint = (int)(m_ScaleValue);//加工硬化阶段停止点为量程

                                        //methodContent += "加工硬化阶段控制:位移\r\n";
                                        //methodContent += "速度:" + double.Parse(controlType3[1]) + " mm/min\r\n";
                                        TestMethodDis(this.txtMethod, "加工硬化阶段控制:位移\r\n", Color.Purple);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType3[1].ToString() + " mm/min\r\n", Color.Purple);

                                        break;
                                    case "1"://eLc控制 eLc * Lc =位移速度
                                        m_Command3.m_CtrlType = 0x80;//0,5.3,1,20.68
                                        m_Command3.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        //转换成位移控制速度
                                        m_Command3.m_CtrlSpeed = (int)(double.Parse(controlType3[1]) * m_Lc * 1000.0);

                                        //停止的类型 位移
                                        m_Command3.m_StopPointType = 0x80;
                                        m_Command3.m_StopPointChannel = m_DSensorArray[0].SensorIndex;

                                        m_ScaleValue = m_SensorArray[m_DSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_DSensorArray[0].SensorIndex].scale & 0x000f));

                                        m_Command3.m_StopPoint = (int)(m_ScaleValue);//加工硬化阶段停止点为量程

                                        TestMethodDis(this.txtMethod, "加工硬化阶段控制:eLc\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType3[1].ToString() + " /s\r\n", Color.Crimson);

                                        break;
                                }

                                #endregion

                                m_CtrlCommandList.Add(m_Command3);
                            }

                            #endregion
                        }

                        #endregion

                        #region 自定义试验
                        if (modelControlMethod_C.isLxqf == 3)
                        {
                            TestMethodDis(this.txtMethod, "试验类型:自定义试验\r\n--------\r\n", Color.Black);

                            string[] customControlType1 = modelControlMethod_C.controlType1.Split(',');
                            if (customControlType1.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType1, "1"));
                            }

                            string[] customControlType2 = modelControlMethod_C.controlType2.Split(',');
                            if (customControlType2.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType2, "2"));
                            }

                            string[] customControlType3 = modelControlMethod_C.controlType3.Split(',');
                            if (customControlType2.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType3, "3"));
                            }

                            string[] customControlType4 = modelControlMethod_C.controlType4.Split(',');
                            if (customControlType4.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType4, "4"));
                            }

                            string[] customControlType5 = modelControlMethod_C.controlType5.Split(',');
                            if (customControlType5.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType5, "5"));
                            }

                            string[] customControlType6 = modelControlMethod_C.controlType6.Split(',');
                            if (customControlType6.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType6, "6"));
                            }

                            string[] customControlType7 = modelControlMethod_C.controlType7.Split(',');
                            if (customControlType7.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType7, "7"));
                            }

                            string[] customControlType8 = modelControlMethod_C.controlType8.Split(',');
                            if (customControlType8.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType8, "8"));
                            }

                            string[] customControlType9 = modelControlMethod_C.controlType9.Split(',');
                            if (customControlType9.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType9, "9"));
                            }

                            string[] customControlType10 = modelControlMethod_C.controlType10.Split(',');
                            if (customControlType10.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType10, "10"));
                            }

                            string[] customControlType11 = modelControlMethod_C.controlType11.Split(',');
                            if (customControlType11.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType11, "11"));
                            }

                            string[] customControlType12 = modelControlMethod_C.controlType12.Split(',');
                            if (customControlType12.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType12, "12"));
                            }
                        }
                        #endregion

                        //commandArray = m_CtrlCommandList.ToArray();
                        for (int i = 0; i < m_CtrlCommandList.Count; i++)
                        {
                            commandArray[i] = m_CtrlCommandList[i];
                        }
                        //最后一项是返回速度
                        int returnspeed = 200000;
                        if (!string.IsNullOrEmpty(modelControlMethod_C.controlType12))
                            returnspeed = int.Parse(modelControlMethod_C.controlType12.ToString()) * 1000;
                        Struc.ctrlcommand speedorder = new Struc.ctrlcommand();
                        speedorder.m_CtrlSpeed = returnspeed;
                        commandArray[11] = speedorder;

                        m_StopValue = (double)modelControlMethod_C.stopValue;
                        if (m_StopValue == 0)
                            m_StopValue = 80;
                        loopCount = (int)modelControlMethod_C.circleNum;

                        //m_methodContent = methodContent;


                    }
                    break;
                case "YBT5349-2006":
                    BLL.ControlMethod_B bllControlMethod_B = new HR_Test.BLL.ControlMethod_B();
                    Model.ControlMethod_B modelControlMethod_B = bllControlMethod_B.GetModel(testMethodName);
                    if (modelControlMethod_B != null)
                    {
                        TestMethodDis(this.txtMethod, "试验方法:" + testMethodName + "\r\n", Color.Black);
                        TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                        #region ///////////////////////是 否 预 载?///////////////////////////////
                        isProLoad = modelControlMethod_B.isProLoad;
                        if (modelControlMethod_B.isProLoad)
                        {
                            Struc.ctrlcommand m_Command = new Struc.ctrlcommand();
                            //methodContent += "是否预载:是;\r\n";
                            TestMethodDis(this.txtMethod, "是否预载:是;\r\n", Color.Black);

                            //控制预载 变形或负荷 
                            switch (modelControlMethod_B.proLoadControlType)
                            {
                                case 0://位移
                                    //泛型命令组
                                    m_Command.m_CtrlType = 0x80;
                                    m_Command.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                    m_Command.m_CtrlSpeed = (int)(modelControlMethod_B.proLoadSpeed * 1000);//位移控制速度
                                    TestMethodDis(this.txtMethod, "预载控制:位移; 速度:" + modelControlMethod_B.proLoadSpeed.ToString() + " mm/min\r\n", Color.Crimson);
                                    break;
                                case 1://负荷
                                    //泛型命令组
                                    m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                    m_ScaleValue = m_ScaleValue >> 8;
                                    m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                                    //m_ScaleValue = (int)GetScale((ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale);
                                    m_Command.m_CtrlType = 0x81;
                                    m_Command.m_CtrlChannel = m_LSensorArray[0].SensorIndex;
                                    double m_SR = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                    m_Command.m_CtrlSpeed = (int)((modelControlMethod_B.proLoadSpeed * 1000.0 / m_SR) + 0.2d);
                                    TestMethodDis(this.txtMethod, "预载控制:负荷; 速度:" + modelControlMethod_B.proLoadSpeed.ToString() + " kN/s\r\n", Color.Crimson);
                                    break;
                            }

                            //预载值停止点类型 变形或负荷
                            switch (modelControlMethod_B.proLoadType)
                            {
                                case 0://变形 

                                    //停止的值， 变形
                                    m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                    m_ScaleValue = m_ScaleValue >> 8;
                                    m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));
                                    //泛型命令组
                                    m_Command.m_StopPointType = 0x82;
                                    m_Command.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                    double m_SR = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                    m_Command.m_StopPoint = (int)((modelControlMethod_B.proLoadValue * 1000.0 / m_SR) + 0.2d);
                                    //methodContent += "预载停止点:变形; 值:" + modelControlMethod_B.proLoadValue + " mm\r\n";
                                    TestMethodDis(this.txtMethod, "预载停止点:变形; 值:" + modelControlMethod_B.proLoadValue.ToString() + " mm\r\n", Color.Crimson);
                                    break;
                                case 1: //负荷 
                                    //停止的值， 负荷
                                    m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                    m_ScaleValue = m_ScaleValue >> 8;
                                    m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));

                                    //泛型命令组
                                    m_Command.m_StopPointType = 0x81;
                                    m_Command.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                    double m_SR1 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                    m_Command.m_StopPoint = (int)((modelControlMethod_B.proLoadValue * 1000.0 / m_SR1) + 0.2d);

                                    //methodContent += "预载停止点:负荷; 值:" + modelControlMethod_B.proLoadValue + " mm\r\n";
                                    TestMethodDis(this.txtMethod, "预载停止点:负荷; 值:" + modelControlMethod_B.proLoadValue.ToString() + " mm\r\n", Color.Crimson);

                                    break;
                            }
                            //m_CtrlCommandList.Add(m_Command);
                            commandArray[10] = m_Command;
                        }
                        #endregion

                        #region///////////////////////不连续屈服 和 连续屈服//////////////


                        if (modelControlMethod_B.isLxqf == 1 || modelControlMethod_B.isLxqf == 2)
                        {
                            #region//////////////////弹性阶段//////////////////

                            //TestMethodDis(this.txtMethod, "试验类型:不连续屈服\r\n--------\r\n", Color.Black);

                            string[] controlType1 = modelControlMethod_B.controlType1.Split(',');

                            if (controlType1.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(controlType1, "弹性"));
                            }
                            #endregion
                        }

                        if (modelControlMethod_B.isLxqf == 1)
                        {
                            #region//////////////////屈服阶段//////////////////
                            //methodContent += "--------\r\n";
                            TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                            Struc.ctrlcommand m_Command2 = new Struc.ctrlcommand();
                            string[] controlType2 = modelControlMethod_B.controlType2.Split(',');

                            if (controlType2.Length == 4)
                            {

                                #region 屈服阶段控制
                                //控制方式：位移,eLc
                                switch (controlType2[0])
                                {// 0,5.3,1,20.68
                                    case "0"://位移控制
                                        m_Command2.m_CtrlType = 0x80;
                                        m_Command2.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command2.m_CtrlSpeed = (int)(double.Parse(controlType2[1]) * 1000.0);//位移控制速度

                                        TestMethodDis(this.txtMethod, "屈服阶段控制:位移\r\n", Color.Blue);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType2[1].ToString() + " mm/min \r\n", Color.Blue);

                                        break;
                                    case "1"://eLc控制   Lc*eLc=位移速度
                                        m_Command2.m_CtrlType = 0x80;//0,5.3,1,20.68
                                        m_Command2.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        //转换成位移控制速度
                                        m_Command2.m_CtrlSpeed = (int)(double.Parse(controlType2[1]) * m_Lc * 1000.0);

                                        TestMethodDis(this.txtMethod, "屈服阶段控制:eLc\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType2[1].ToString() + " /s\r\n", Color.Crimson);

                                        break;
                                }
                                #endregion

                                #region 屈服阶段停止转换点

                                //停止转换点：位移 0x80,负荷 0x81,变形 0x82, 应力,应变,
                                switch (controlType2[2])
                                {
                                    case "0":
                                        //停止的类型 位移
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x80;
                                        m_Command2.m_StopPointChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command2.m_StopPoint = (int)(double.Parse(controlType2[3]) * 1000.0);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:位移\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " mm\r\n", Color.Crimson);
                                        break;
                                    case "1":
                                        //停止的类型 负荷
                                        m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x81;
                                        m_Command2.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        double m_SR = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 / m_SR) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:负荷\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " kN\r\n", Color.Crimson);
                                        break;
                                    case "2":
                                        //停止的类型 变形
                                        m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));

                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x82;
                                        m_Command2.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                        double m_SR1 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 / m_SR1) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:变形\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " mm\r\n", Color.Crimson);

                                        break;
                                    case "3":
                                        //停止类型 应力 转换为负荷 为 应力*面积 = 负荷；1Mp * 1mm^2 = 1N                                   
                                        m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x81;
                                        m_Command2.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        double m_SR2 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * m_S0 * 1000.0 / m_SR2) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:应力\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " MPa\r\n", Color.Crimson);

                                        break;
                                    case "4":
                                        //停止类型 （应变 = 变形/标距） 转换成 （变形 = 应变 * 引伸计标距Le）

                                        m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));

                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x82;
                                        m_Command2.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                        double m_SR3 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 * m_Le / m_SR3) + 0.2d);

                                        TestMethodDis(this.txtMethod, "屈服阶段结束:应变\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " %\r\n", Color.Crimson);

                                        break;
                                }

                                #endregion

                                m_CtrlCommandList.Add(m_Command2);
                            }


                            #endregion
                        }

                        if (modelControlMethod_B.isLxqf == 1 || modelControlMethod_B.isLxqf == 2)
                        {
                            #region//////////////////加工硬化阶段//////////////////


                            TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                            Struc.ctrlcommand m_Command3 = new Struc.ctrlcommand();
                            string[] controlType3 = modelControlMethod_B.controlType3.Split(',');

                            if (controlType3.Length == 2)
                            {
                                #region 加工硬化阶段控制
                                //控制方式：位移,eLc
                                switch (controlType3[0])
                                {// 0,5.3,1,20.68
                                    case "0":
                                        m_Command3.m_CtrlType = 0x80;
                                        m_Command3.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command3.m_CtrlSpeed = (int)(double.Parse(controlType3[1]) * 1000.0);//位移控制速度

                                        m_Command3.m_StopPointType = 0x80;
                                        m_Command3.m_StopPointChannel = m_DSensorArray[0].SensorIndex;

                                        m_ScaleValue = m_SensorArray[m_DSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_DSensorArray[0].SensorIndex].scale & 0x000f));

                                        m_Command3.m_StopPoint = (int)(m_ScaleValue);//加工硬化阶段停止点为量程

                                        //methodContent += "加工硬化阶段控制:位移\r\n";
                                        //methodContent += "速度:" + double.Parse(controlType3[1]) + " mm/min\r\n";
                                        TestMethodDis(this.txtMethod, "加工硬化阶段控制:位移\r\n", Color.Purple);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType3[1].ToString() + " mm/min\r\n", Color.Purple);

                                        break;
                                    case "1"://eLc控制 eLc * Lc =位移速度
                                        m_Command3.m_CtrlType = 0x80;//0,5.3,1,20.68
                                        m_Command3.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        //转换成位移控制速度
                                        m_Command3.m_CtrlSpeed = (int)(double.Parse(controlType3[1]) * m_Lc * 1000.0);

                                        //停止的类型 位移
                                        m_Command3.m_StopPointType = 0x80;
                                        m_Command3.m_StopPointChannel = m_DSensorArray[0].SensorIndex;

                                        m_ScaleValue = m_SensorArray[m_DSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_DSensorArray[0].SensorIndex].scale & 0x000f));

                                        m_Command3.m_StopPoint = (int)(m_ScaleValue);//加工硬化阶段停止点为量程

                                        TestMethodDis(this.txtMethod, "加工硬化阶段控制:eLc\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType3[1].ToString() + " /s\r\n", Color.Crimson);

                                        break;
                                }

                                #endregion

                                m_CtrlCommandList.Add(m_Command3);
                            }

                            #endregion
                        }

                        #endregion

                        #region 自定义试验
                        if (modelControlMethod_B.isLxqf == 3)
                        {
                            TestMethodDis(this.txtMethod, "试验类型:自定义试验\r\n--------\r\n", Color.Black);

                            string[] customControlType1 = modelControlMethod_B.controlType1.Split(',');
                            if (customControlType1.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType1, "1"));
                            }

                            string[] customControlType2 = modelControlMethod_B.controlType2.Split(',');
                            if (customControlType2.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType2, "2"));
                            }

                            string[] customControlType3 = modelControlMethod_B.controlType3.Split(',');
                            if (customControlType2.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType3, "3"));
                            }

                            string[] customControlType4 = modelControlMethod_B.controlType4.Split(',');
                            if (customControlType4.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType4, "4"));
                            }

                            string[] customControlType5 = modelControlMethod_B.controlType5.Split(',');
                            if (customControlType5.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType5, "5"));
                            }

                            string[] customControlType6 = modelControlMethod_B.controlType6.Split(',');
                            if (customControlType6.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType6, "6"));
                            }

                            string[] customControlType7 = modelControlMethod_B.controlType7.Split(',');
                            if (customControlType7.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType7, "7"));
                            }

                            string[] customControlType8 = modelControlMethod_B.controlType8.Split(',');
                            if (customControlType8.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType8, "8"));
                            }

                            string[] customControlType9 = modelControlMethod_B.controlType9.Split(',');
                            if (customControlType9.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType9, "9"));
                            }

                            string[] customControlType10 = modelControlMethod_B.controlType10.Split(',');
                            if (customControlType10.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType10, "10"));
                            }

                            string[] customControlType11 = modelControlMethod_B.controlType11.Split(',');
                            if (customControlType11.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType11, "11"));
                            }

                            string[] customControlType12 = modelControlMethod_B.controlType12.Split(',');
                            if (customControlType12.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType12, "12"));
                            }
                        }
                        #endregion

                        //commandArray = m_CtrlCommandList.ToArray();
                        for (int i = 0; i < m_CtrlCommandList.Count; i++)
                        {
                            commandArray[i] = m_CtrlCommandList[i];
                        }
                        //最后一项是返回速度
                        int returnspeed = 200000;
                        if (!string.IsNullOrEmpty(modelControlMethod_B.controlType12))
                            returnspeed = int.Parse(modelControlMethod_B.controlType12.ToString()) * 1000;
                        Struc.ctrlcommand speedorder = new Struc.ctrlcommand();
                        speedorder.m_CtrlSpeed = returnspeed;
                        commandArray[11] = speedorder;
                        //m_CtrlCommandArray.CopyTo(commandArray, 0);
                        m_StopValue = (double)modelControlMethod_B.stopValue;
                        if (m_StopValue == 0)
                            m_StopValue = 80;
                        loopCount = (int)modelControlMethod_B.circleNum;

                        m_methodContent = methodContent;
                    }
                    break;

                case "GBT28289-2012":
                    BLL.GBT282892012_Method bGBT28289Method = new HR_Test.BLL.GBT282892012_Method();
                    Model.GBT282892012_Method mGBT28289Method = bGBT28289Method.GetModel(testMethodName);
                    if (mGBT28289Method != null)
                    {
                        TestMethodDis(this.txtMethod, "试验方法:" + testMethodName + "\r\n", Color.Black);
                        TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                        #region ///////////////////////是 否 预 载?///////////////////////////////
                        isProLoad = mGBT28289Method.isProLoad;
                        if (mGBT28289Method.isProLoad)
                        {
                            Struc.ctrlcommand m_Command = new Struc.ctrlcommand();
                            //methodContent += "是否预载:是;\r\n";
                            TestMethodDis(this.txtMethod, "是否预载:是;\r\n", Color.Black);

                            //控制预载 变形或负荷 
                            switch (mGBT28289Method.proLoadControlType)
                            {
                                case 0://位移
                                    //泛型命令组
                                    m_Command.m_CtrlType = 0x80;
                                    m_Command.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                    m_Command.m_CtrlSpeed = (int)(mGBT28289Method.proLoadSpeed * 1000);//位移控制速度
                                    TestMethodDis(this.txtMethod, "预载控制:位移; 速度:" + mGBT28289Method.proLoadSpeed.ToString() + " mm/min\r\n", Color.Crimson);
                                    break;
                                case 1://负荷
                                    //泛型命令组
                                    m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                    m_ScaleValue = m_ScaleValue >> 8;
                                    m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                                    //m_ScaleValue = (int)GetScale((ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale);
                                    m_Command.m_CtrlType = 0x81;
                                    m_Command.m_CtrlChannel = m_LSensorArray[0].SensorIndex;
                                    double m_SR = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                    m_Command.m_CtrlSpeed = (int)((mGBT28289Method.proLoadSpeed * 1000.0 / m_SR) + 0.2d);
                                    TestMethodDis(this.txtMethod, "预载控制:负荷; 速度:" + mGBT28289Method.proLoadSpeed.ToString() + " kN/s\r\n", Color.Crimson);
                                    break;
                            }

                            //预载值停止点类型 变形或负荷
                            switch (mGBT28289Method.proLoadType)
                            {
                                case 0://变形 

                                    //停止的值， 变形
                                    m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                    m_ScaleValue = m_ScaleValue >> 8;
                                    m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));
                                    //泛型命令组
                                    m_Command.m_StopPointType = 0x82;
                                    m_Command.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                    double m_SR = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                    m_Command.m_StopPoint = (int)((mGBT28289Method.proLoadValue * 1000.0 / m_SR) + 0.2d);
                                    //methodContent += "预载停止点:变形; 值:" + modelControlMethod_C.proLoadValue + " mm\r\n";
                                    TestMethodDis(this.txtMethod, "预载停止点:变形; 值:" + mGBT28289Method.proLoadValue.ToString() + " mm\r\n", Color.Crimson);
                                    break;
                                case 1: //负荷 
                                    //停止的值， 负荷
                                    m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                    m_ScaleValue = m_ScaleValue >> 8;
                                    m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));

                                    //泛型命令组
                                    m_Command.m_StopPointType = 0x81;
                                    m_Command.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                    double m_SR1 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                    m_Command.m_StopPoint = (int)((mGBT28289Method.proLoadValue * 1000.0 / m_SR1) + 0.2d);

                                    //methodContent += "预载停止点:负荷; 值:" + modelControlMethod_C.proLoadValue + " mm\r\n";
                                    TestMethodDis(this.txtMethod, "预载停止点:负荷; 值:" + mGBT28289Method.proLoadValue + " kN\r\n", Color.Crimson);

                                    break;
                            }
                            //m_CtrlCommandList.Add(m_Command);
                            commandArray[10] = m_Command;
                        }
                        #endregion

                        #region///////////////////////不连续屈服 和 连续屈服//////////////


                        if (mGBT28289Method.isLxqf == 1 || mGBT28289Method.isLxqf == 2)
                        {
                            #region//////////////////弹性阶段//////////////////

                            //TestMethodDis(this.txtMethod, "试验类型:不连续屈服\r\n--------\r\n", Color.Black);

                            string[] controlType1 = mGBT28289Method.controlType1.Split(',');

                            if (controlType1.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(controlType1, "弹性"));
                            }
                            #endregion
                        }

                        if (mGBT28289Method.isLxqf == 1)
                        {
                            #region//////////////////屈服阶段//////////////////
                            //methodContent += "--------\r\n";
                            TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                            Struc.ctrlcommand m_Command2 = new Struc.ctrlcommand();
                            string[] controlType2 = mGBT28289Method.controlType2.Split(',');

                            if (controlType2.Length == 4)
                            {

                                #region 屈服阶段控制
                                //控制方式：位移,eLc
                                switch (controlType2[0])
                                {// 0,5.3,1,20.68
                                    case "0"://位移控制
                                        m_Command2.m_CtrlType = 0x80;
                                        m_Command2.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command2.m_CtrlSpeed = (int)(double.Parse(controlType2[1]) * 1000.0);//位移控制速度

                                        TestMethodDis(this.txtMethod, "屈服阶段控制:位移\r\n", Color.Blue);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType2[1].ToString() + " mm/min \r\n", Color.Blue);

                                        break;
                                    case "1"://eLc控制   Lc*eLc=位移速度
                                        m_Command2.m_CtrlType = 0x80;//0,5.3,1,20.68
                                        m_Command2.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        //转换成位移控制速度
                                        m_Command2.m_CtrlSpeed = (int)(double.Parse(controlType2[1]) * m_Lc * 1000.0);

                                        TestMethodDis(this.txtMethod, "屈服阶段控制:eLc\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType2[1].ToString() + " /s\r\n", Color.Crimson);

                                        break;
                                }
                                #endregion

                                #region 屈服阶段停止转换点

                                //停止转换点：位移 0x80,负荷 0x81,变形 0x82, 应力,应变,
                                switch (controlType2[2])
                                {
                                    case "0":
                                        //停止的类型 位移
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x80;
                                        m_Command2.m_StopPointChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command2.m_StopPoint = (int)(double.Parse(controlType2[3]) * 1000.0);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:位移\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " mm\r\n", Color.Crimson);
                                        break;
                                    case "1":
                                        //停止的类型 负荷
                                        m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x81;
                                        m_Command2.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        double m_SR = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 / m_SR) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:负荷\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " kN\r\n", Color.Crimson);
                                        break;
                                    case "2":
                                        //停止的类型 变形
                                        m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));

                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x82;
                                        m_Command2.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                        double m_SR1 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 / m_SR1) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:变形\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " mm\r\n", Color.Crimson);

                                        break;
                                    case "3":
                                        //停止类型 应力 转换为负荷 为 应力*面积 = 负荷；1Mp * 1mm^2 = 1N                                   
                                        m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x81;
                                        m_Command2.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        double m_SR2 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * m_S0 * 1000.0 / m_SR2) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:应力\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " MPa\r\n", Color.Crimson);

                                        break;
                                    case "4":
                                        //停止类型 （应变 = 变形/标距） 转换成 （变形 = 应变 * 引伸计标距Le）

                                        m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));

                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x82;
                                        m_Command2.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                        double m_SR3 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 * m_Le / m_SR3) + 0.2d);

                                        TestMethodDis(this.txtMethod, "屈服阶段结束:应变\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " %\r\n", Color.Crimson);

                                        break;
                                }

                                #endregion

                                m_CtrlCommandList.Add(m_Command2);
                            }


                            #endregion
                        }

                        if (mGBT28289Method.isLxqf == 1 || mGBT28289Method.isLxqf == 2)
                        {
                            #region//////////////////加工硬化阶段//////////////////


                            TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                            Struc.ctrlcommand m_Command3 = new Struc.ctrlcommand();
                            string[] controlType3 = mGBT28289Method.controlType3.Split(',');

                            if (controlType3.Length == 2)
                            {
                                #region 加工硬化阶段控制
                                //控制方式：位移,eLc
                                switch (controlType3[0])
                                {// 0,5.3,1,20.68
                                    case "0":
                                        m_Command3.m_CtrlType = 0x80;
                                        m_Command3.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command3.m_CtrlSpeed = (int)(double.Parse(controlType3[1]) * 1000.0);//位移控制速度

                                        m_Command3.m_StopPointType = 0x80;
                                        m_Command3.m_StopPointChannel = m_DSensorArray[0].SensorIndex;

                                        m_ScaleValue = m_SensorArray[m_DSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_DSensorArray[0].SensorIndex].scale & 0x000f));

                                        m_Command3.m_StopPoint = (int)(m_ScaleValue);//加工硬化阶段停止点为量程

                                        //methodContent += "加工硬化阶段控制:位移\r\n";
                                        //methodContent += "速度:" + double.Parse(controlType3[1]) + " mm/min\r\n";
                                        TestMethodDis(this.txtMethod, "加工硬化阶段控制:位移\r\n", Color.Purple);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType3[1].ToString() + " mm/min\r\n", Color.Purple);

                                        break;
                                    case "1"://eLc控制 eLc * Lc =位移速度
                                        m_Command3.m_CtrlType = 0x80;//0,5.3,1,20.68
                                        m_Command3.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        //转换成位移控制速度
                                        m_Command3.m_CtrlSpeed = (int)(double.Parse(controlType3[1]) * m_Lc * 1000.0);

                                        //停止的类型 位移
                                        m_Command3.m_StopPointType = 0x80;
                                        m_Command3.m_StopPointChannel = m_DSensorArray[0].SensorIndex;

                                        m_ScaleValue = m_SensorArray[m_DSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_DSensorArray[0].SensorIndex].scale & 0x000f));

                                        m_Command3.m_StopPoint = (int)(m_ScaleValue);//加工硬化阶段停止点为量程

                                        TestMethodDis(this.txtMethod, "加工硬化阶段控制:eLc\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType3[1].ToString() + " /s\r\n", Color.Crimson);

                                        break;
                                }

                                #endregion

                                m_CtrlCommandList.Add(m_Command3);
                            }

                            #endregion
                        }

                        #endregion

                        #region 自定义试验
                        if (mGBT28289Method.isLxqf == 3)
                        {
                            TestMethodDis(this.txtMethod, "试验类型:自定义试验\r\n--------\r\n", Color.Black);

                            string[] customControlType1 = mGBT28289Method.controlType1.Split(',');
                            if (customControlType1.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType1, "1"));
                            }

                            string[] customControlType2 = mGBT28289Method.controlType2.Split(',');
                            if (customControlType2.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType2, "2"));
                            }

                            string[] customControlType3 = mGBT28289Method.controlType3.Split(',');
                            if (customControlType2.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType3, "3"));
                            }

                            string[] customControlType4 = mGBT28289Method.controlType4.Split(',');
                            if (customControlType4.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType4, "4"));
                            }

                            string[] customControlType5 = mGBT28289Method.controlType5.Split(',');
                            if (customControlType5.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType5, "5"));
                            }

                            string[] customControlType6 = mGBT28289Method.controlType6.Split(',');
                            if (customControlType6.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType6, "6"));
                            }

                            string[] customControlType7 = mGBT28289Method.controlType7.Split(',');
                            if (customControlType7.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType7, "7"));
                            }

                            string[] customControlType8 = mGBT28289Method.controlType8.Split(',');
                            if (customControlType8.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType8, "8"));
                            }

                            string[] customControlType9 = mGBT28289Method.controlType9.Split(',');
                            if (customControlType9.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType9, "9"));
                            }

                            string[] customControlType10 = mGBT28289Method.controlType10.Split(',');
                            if (customControlType10.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType10, "10"));
                            }

                            string[] customControlType11 = mGBT28289Method.controlType11.Split(',');
                            if (customControlType11.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType11, "11"));
                            }

                            string[] customControlType12 = mGBT28289Method.controlType12.Split(',');
                            if (customControlType12.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType12, "12"));
                            }
                        }
                        #endregion

                        //commandArray = m_CtrlCommandList.ToArray(); 
                        for (int i = 0; i < m_CtrlCommandList.Count; i++)
                        {
                            commandArray[i] = m_CtrlCommandList[i];
                        }
                        //最后一项是返回速度
                        int returnspeed = 200000;
                        if (!string.IsNullOrEmpty(mGBT28289Method.controlType12))
                            returnspeed = int.Parse(mGBT28289Method.controlType12.ToString()) * 1000;
                        Struc.ctrlcommand speedorder = new Struc.ctrlcommand();
                        speedorder.m_CtrlSpeed = returnspeed;
                        commandArray[11] = speedorder;

                        m_StopValue = (double)mGBT28289Method.stopValue;
                        if (m_StopValue == 0)
                            m_StopValue = 80;
                        loopCount = (int)mGBT28289Method.circleNum;

                        m_methodContent = methodContent;
                    }
                    break;

                case "GBT23615-2009":
                    BLL.GBT236152009_Method bGBT23615Method = new HR_Test.BLL.GBT236152009_Method();
                    Model.GBT236152009_Method mGBT23615Method = bGBT23615Method.GetModel(testMethodName);
                    if (mGBT23615Method != null)
                    {
                        TestMethodDis(this.txtMethod, "试验方法:" + testMethodName + "\r\n", Color.Black);
                        TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                        #region ///////////////////////是 否 预 载?///////////////////////////////
                        isProLoad = mGBT23615Method.isProLoad;
                        if (mGBT23615Method.isProLoad)
                        {
                            Struc.ctrlcommand m_Command = new Struc.ctrlcommand();
                            //methodContent += "是否预载:是;\r\n";
                            TestMethodDis(this.txtMethod, "是否预载:是;\r\n", Color.Black);

                            //控制预载 变形或负荷 
                            switch (mGBT23615Method.proLoadControlType)
                            {
                                case 0://位移
                                    //泛型命令组
                                    m_Command.m_CtrlType = 0x80;
                                    m_Command.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                    m_Command.m_CtrlSpeed = (int)(mGBT23615Method.proLoadSpeed * 1000);//位移控制速度
                                    TestMethodDis(this.txtMethod, "预载控制:位移; 速度:" + mGBT23615Method.proLoadSpeed.ToString() + " mm/min\r\n", Color.Crimson);
                                    break;
                                case 1://负荷
                                    //泛型命令组
                                    m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                    m_ScaleValue = m_ScaleValue >> 8;
                                    m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                                    //m_ScaleValue = (int)GetScale((ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale);
                                    m_Command.m_CtrlType = 0x81;
                                    m_Command.m_CtrlChannel = m_LSensorArray[0].SensorIndex;
                                    double m_SR = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                    m_Command.m_CtrlSpeed = (int)((mGBT23615Method.proLoadSpeed * 1000.0 / m_SR) + 0.2d);
                                    TestMethodDis(this.txtMethod, "预载控制:负荷; 速度:" + mGBT23615Method.proLoadSpeed.ToString() + " kN/s\r\n", Color.Crimson);
                                    break;
                            }

                            //预载值停止点类型 变形或负荷
                            switch (mGBT23615Method.proLoadType)
                            {
                                case 0://变形 

                                    //停止的值， 变形
                                    m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                    m_ScaleValue = m_ScaleValue >> 8;
                                    m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));
                                    //泛型命令组
                                    m_Command.m_StopPointType = 0x82;
                                    m_Command.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                    double m_SR = m_ScaleValue / m_Resolution;
                                    m_Command.m_StopPoint = (int)((mGBT23615Method.proLoadValue * 1000.0 / m_SR) + 0.2d);
                                    //methodContent += "预载停止点:变形; 值:" + modelControlMethod_C.proLoadValue + " mm\r\n";
                                    TestMethodDis(this.txtMethod, "预载停止点:变形; 值:" + mGBT23615Method.proLoadValue.ToString() + " mm\r\n", Color.Crimson);
                                    break;
                                case 1: //负荷 
                                    //停止的值， 负荷
                                    m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                    m_ScaleValue = m_ScaleValue >> 8;
                                    m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));

                                    //泛型命令组
                                    m_Command.m_StopPointType = 0x81;
                                    m_Command.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                    double m_SR1 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                    m_Command.m_StopPoint = (int)((mGBT23615Method.proLoadValue * 1000.0 / m_SR1) + 0.2d);

                                    //methodContent += "预载停止点:负荷; 值:" + modelControlMethod_C.proLoadValue + " mm\r\n";
                                    TestMethodDis(this.txtMethod, "预载停止点:负荷; 值:" + mGBT23615Method.proLoadValue + " kN\r\n", Color.Crimson);

                                    break;
                            }
                            //m_CtrlCommandList.Add(m_Command);
                            commandArray[10] = m_Command;
                        }
                        #endregion

                        #region///////////////////////不连续屈服 和 连续屈服//////////////


                        if (mGBT23615Method.isLxqf == 1 || mGBT23615Method.isLxqf == 2)
                        {
                            #region//////////////////弹性阶段//////////////////

                            //TestMethodDis(this.txtMethod, "试验类型:不连续屈服\r\n--------\r\n", Color.Black);

                            string[] controlType1 = mGBT23615Method.controlType1.Split(',');

                            if (controlType1.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(controlType1, "弹性"));
                            }
                            #endregion
                        }

                        if (mGBT23615Method.isLxqf == 1)
                        {
                            #region//////////////////屈服阶段//////////////////
                            //methodContent += "--------\r\n";
                            TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                            Struc.ctrlcommand m_Command2 = new Struc.ctrlcommand();
                            string[] controlType2 = mGBT23615Method.controlType2.Split(',');

                            if (controlType2.Length == 4)
                            {

                                #region 屈服阶段控制
                                //控制方式：位移,eLc
                                switch (controlType2[0])
                                {// 0,5.3,1,20.68
                                    case "0"://位移控制
                                        m_Command2.m_CtrlType = 0x80;
                                        m_Command2.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command2.m_CtrlSpeed = (int)(double.Parse(controlType2[1]) * 1000.0);//位移控制速度

                                        TestMethodDis(this.txtMethod, "屈服阶段控制:位移\r\n", Color.Blue);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType2[1].ToString() + " mm/min \r\n", Color.Blue);

                                        break;
                                    case "1"://eLc控制   Lc*eLc=位移速度
                                        m_Command2.m_CtrlType = 0x80;//0,5.3,1,20.68
                                        m_Command2.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        //转换成位移控制速度
                                        m_Command2.m_CtrlSpeed = (int)(double.Parse(controlType2[1]) * m_Lc * 1000.0);

                                        TestMethodDis(this.txtMethod, "屈服阶段控制:eLc\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType2[1].ToString() + " /s\r\n", Color.Crimson);

                                        break;
                                }
                                #endregion

                                #region 屈服阶段停止转换点

                                //停止转换点：位移 0x80,负荷 0x81,变形 0x82, 应力,应变,
                                switch (controlType2[2])
                                {
                                    case "0":
                                        //停止的类型 位移
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x80;
                                        m_Command2.m_StopPointChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command2.m_StopPoint = (int)(double.Parse(controlType2[3]) * 1000.0);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:位移\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " mm\r\n", Color.Crimson);
                                        break;
                                    case "1":
                                        //停止的类型 负荷
                                        m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x81;
                                        m_Command2.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        double m_SR = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 / m_SR) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:负荷\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " kN\r\n", Color.Crimson);
                                        break;
                                    case "2":
                                        //停止的类型 变形
                                        m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));

                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x82;
                                        m_Command2.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                        double m_SR1 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 / m_SR1) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:变形\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " mm\r\n", Color.Crimson);

                                        break;
                                    case "3":
                                        //停止类型 应力 转换为负荷 为 应力*面积 = 负荷；1Mp * 1mm^2 = 1N                                   
                                        m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x81;
                                        m_Command2.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        double m_SR2 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * m_S0 * 1000.0 / m_SR2) + 0.2d);
                                        TestMethodDis(this.txtMethod, "屈服阶段结束:应力\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " MPa\r\n", Color.Crimson);

                                        break;
                                    case "4":
                                        //停止类型 （应变 = 变形/标距） 转换成 （变形 = 应变 * 引伸计标距Le）

                                        m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));

                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x82;
                                        m_Command2.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                        double m_SR3 = (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                                        m_Command2.m_StopPoint = (int)((double.Parse(controlType2[3]) * 1000.0 * m_Le / m_SR3) + 0.2d);

                                        TestMethodDis(this.txtMethod, "屈服阶段结束:应变\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + controlType2[3] + " %\r\n", Color.Crimson);

                                        break;
                                }

                                #endregion

                                m_CtrlCommandList.Add(m_Command2);
                            }


                            #endregion
                        }

                        if (mGBT23615Method.isLxqf == 1 || mGBT23615Method.isLxqf == 2)
                        {
                            #region//////////////////加工硬化阶段//////////////////


                            TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                            Struc.ctrlcommand m_Command3 = new Struc.ctrlcommand();
                            string[] controlType3 = mGBT23615Method.controlType3.Split(',');

                            if (controlType3.Length == 2)
                            {
                                #region 加工硬化阶段控制
                                //控制方式：位移,eLc
                                switch (controlType3[0])
                                {// 0,5.3,1,20.68
                                    case "0":
                                        m_Command3.m_CtrlType = 0x80;
                                        m_Command3.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command3.m_CtrlSpeed = (int)(double.Parse(controlType3[1]) * 1000.0);//位移控制速度

                                        m_Command3.m_StopPointType = 0x80;
                                        m_Command3.m_StopPointChannel = m_DSensorArray[0].SensorIndex;

                                        m_ScaleValue = m_SensorArray[m_DSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_DSensorArray[0].SensorIndex].scale & 0x000f));

                                        m_Command3.m_StopPoint = (int)(m_ScaleValue);//加工硬化阶段停止点为量程

                                        //methodContent += "加工硬化阶段控制:位移\r\n";
                                        //methodContent += "速度:" + double.Parse(controlType3[1]) + " mm/min\r\n";
                                        TestMethodDis(this.txtMethod, "加工硬化阶段控制:位移\r\n", Color.Purple);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType3[1].ToString() + " mm/min\r\n", Color.Purple);

                                        break;
                                    case "1"://eLc控制 eLc * Lc =位移速度
                                        m_Command3.m_CtrlType = 0x80;//0,5.3,1,20.68
                                        m_Command3.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        //转换成位移控制速度
                                        m_Command3.m_CtrlSpeed = (int)(double.Parse(controlType3[1]) * m_Lc * 1000.0);

                                        //停止的类型 位移
                                        m_Command3.m_StopPointType = 0x80;
                                        m_Command3.m_StopPointChannel = m_DSensorArray[0].SensorIndex;

                                        m_ScaleValue = m_SensorArray[m_DSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_DSensorArray[0].SensorIndex].scale & 0x000f));

                                        m_Command3.m_StopPoint = (int)(m_ScaleValue);//加工硬化阶段停止点为量程

                                        TestMethodDis(this.txtMethod, "加工硬化阶段控制:eLc\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "速度:" + controlType3[1].ToString() + " /s\r\n", Color.Crimson);

                                        break;
                                }

                                #endregion

                                m_CtrlCommandList.Add(m_Command3);
                            }

                            #endregion
                        }

                        #endregion

                        #region 自定义试验
                        if (mGBT23615Method.isLxqf == 3)
                        {
                            TestMethodDis(this.txtMethod, "试验类型:自定义试验\r\n--------\r\n", Color.Black);

                            string[] customControlType1 = mGBT23615Method.controlType1.Split(',');
                            if (customControlType1.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType1, "1"));
                            }

                            string[] customControlType2 = mGBT23615Method.controlType2.Split(',');
                            if (customControlType2.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType2, "2"));
                            }

                            string[] customControlType3 = mGBT23615Method.controlType3.Split(',');
                            if (customControlType2.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType3, "3"));
                            }

                            string[] customControlType4 = mGBT23615Method.controlType4.Split(',');
                            if (customControlType4.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType4, "4"));
                            }

                            string[] customControlType5 = mGBT23615Method.controlType5.Split(',');
                            if (customControlType5.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType5, "5"));
                            }

                            string[] customControlType6 = mGBT23615Method.controlType6.Split(',');
                            if (customControlType6.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType6, "6"));
                            }

                            string[] customControlType7 = mGBT23615Method.controlType7.Split(',');
                            if (customControlType7.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType7, "7"));
                            }

                            string[] customControlType8 = mGBT23615Method.controlType8.Split(',');
                            if (customControlType8.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType8, "8"));
                            }

                            string[] customControlType9 = mGBT23615Method.controlType9.Split(',');
                            if (customControlType9.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType9, "9"));
                            }

                            string[] customControlType10 = mGBT23615Method.controlType10.Split(',');
                            if (customControlType10.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType10, "10"));
                            }

                            string[] customControlType11 = mGBT23615Method.controlType11.Split(',');
                            if (customControlType11.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType11, "11"));
                            }

                            string[] customControlType12 = mGBT23615Method.controlType12.Split(',');
                            if (customControlType12.Length == 4)
                            {
                                m_CtrlCommandList.Add(GetCtrlCommand(customControlType12, "12"));
                            }
                        }
                        #endregion

                        //commandArray = m_CtrlCommandList.ToArray(); 
                        for (int i = 0; i < m_CtrlCommandList.Count; i++)
                        {
                            commandArray[i] = m_CtrlCommandList[i];
                        }
                        //最后一项是返回速度
                        int returnspeed = 200000;
                        if (!string.IsNullOrEmpty(mGBT23615Method.controlType12))
                            returnspeed = int.Parse(mGBT23615Method.controlType12.ToString()) * 1000;
                        Struc.ctrlcommand speedorder = new Struc.ctrlcommand();
                        speedorder.m_CtrlSpeed = returnspeed;
                        commandArray[11] = speedorder;

                        m_StopValue = (double)mGBT23615Method.stopValue;
                        if (m_StopValue == 0)
                            m_StopValue = 80;
                        loopCount = (int)mGBT23615Method.circleNum;
                        m_methodContent = methodContent;
                    }
                    break;
            }

            TestMethodDis(this.txtMethod, "循环次数:" + loopCount.ToString() + "\r\n", Color.Black);
            TestMethodDis(this.txtMethod, "停止试验:" + m_StopValue.ToString() + "%\r\n", Color.Black);

            if (commandArray != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Struc.ctrlcommand GetCtrlCommand(string[] customControlType, string strIndex)
        {
            //int m_ScaleValue = 0;
            Struc.ctrlcommand m_CustomCommand = new Struc.ctrlcommand();
            if (customControlType.Length == 4)
            {
                #region  阶段控制
                //-------控制方式------
                //位移控制
                //负荷控制
                //应力控制
                //ēLc控制
                //ēLe控制
                //变形控制
                switch (customControlType[0])
                {
                    case "0"://位移控制
                        m_CustomCommand.m_CtrlType = 0x80;//0,5.3,1,20.68
                        m_CustomCommand.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                        //位移控制速度
                        m_CustomCommand.m_CtrlSpeed = (int)(double.Parse(customControlType[1]) * 1000.0);
                        TestMethodDis(this.txtMethod, strIndex + "阶段控制:位移\r\n", Color.Crimson);
                        TestMethodDis(this.txtMethod, "速度:" + customControlType[1].ToString() + " mm/min\r\n", Color.Crimson);
                        break;
                    case "1"://负荷控制
                        //m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                        //m_ScaleValue = m_ScaleValue >> 8;
                        //m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));                         
                        //泛型命令组
                        m_CustomCommand.m_CtrlType = 0x81;
                        m_CustomCommand.m_CtrlChannel = m_LSensorArray[0].SensorIndex;
                        //负荷控制速度 
                        double m_SR = GetSensorSR((ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale);
                        m_CustomCommand.m_CtrlSpeed = (int)((double.Parse(customControlType[1]) * 1000.0 / m_SR) + 0.2d);
                        TestMethodDis(this.txtMethod, strIndex + "阶段控制:负荷\r\n", Color.Crimson);
                        TestMethodDis(this.txtMethod, "速度:" + customControlType[1].ToString() + " kN/s\r\n", Color.Crimson);
                        break;
                    case "2"://应力控制 负荷/面积 = 应力； 转换为负荷控制为 应力*面积 = 负荷；1Mp * 1mm^2 = 1N
                        //m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                        //m_ScaleValue = m_ScaleValue >> 8;
                        //m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                        //泛型命令组
                        m_CustomCommand.m_CtrlType = 0x81;
                        m_CustomCommand.m_CtrlChannel = m_LSensorArray[0].SensorIndex;
                        //负荷控制速度                        
                        double m_SR1 = GetSensorSR((ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale); //(m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                        m_CustomCommand.m_CtrlSpeed = (int)((double.Parse(customControlType[1]) * m_S0 / m_SR1) + 0.2d);
                        TestMethodDis(this.txtMethod, strIndex + "阶段控制:应力\r\n", Color.Crimson);
                        TestMethodDis(this.txtMethod, "速度:" + customControlType[1].ToString() + " MPa/s\r\n", Color.Crimson);
                        break;

                    case "3"://eLc控制  eLc * Lc = 位移控制速度
                        m_CustomCommand.m_CtrlType = 0x80;//0,5.3,1,20.68
                        m_CustomCommand.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                        //转换成位移控制速度
                        m_CustomCommand.m_CtrlSpeed = (int)(double.Parse(customControlType[1]) * m_Lc * 1000.0 + 0.2d);
                        TestMethodDis(this.txtMethod, strIndex + "阶段控制:eLc\r\n", Color.Crimson);
                        TestMethodDis(this.txtMethod, "速度:" + customControlType[1].ToString() + " /s\r\n", Color.Crimson);
                        break;
                    case "4"://eLe控制 eLe * Le = 变形控制速度
                        //泛型命令组
                        m_CustomCommand.m_CtrlType = 0x82;
                        m_CustomCommand.m_CtrlChannel = m_ESensorArray[0].SensorIndex;
                        //------转换为变形控制速度
                        double m_SR3 = GetSensorSR((ushort)m_SensorArray[m_ESensorArray[0].SensorIndex].scale); //(m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                        m_CustomCommand.m_CtrlSpeed = (int)((double.Parse(customControlType[1]) * m_Le / m_SR3) + 0.2d);//(int)( double.Parse(customControlType[1]) / m_SR3); //
                        TestMethodDis(this.txtMethod, strIndex + "阶段控制:eLe\r\n", Color.Crimson);
                        TestMethodDis(this.txtMethod, "速度:" + customControlType[1].ToString() + " /s\r\n", Color.Crimson);
                        break;
                    case "5"://变形控制
                        m_CustomCommand.m_CtrlType = 0x82;
                        m_CustomCommand.m_CtrlChannel = m_ESensorArray[0].SensorIndex;
                        //------变形控制速度
                        double m_SR4 = GetSensorSR((ushort)m_SensorArray[m_ESensorArray[0].SensorIndex].scale);//分辨率
                        m_CustomCommand.m_CtrlSpeed = (int)((double.Parse(customControlType[1]) * 1000.0 / m_SR4) + 0.2d);//(int)( double.Parse(customControlType[1]) / m_SR3); //
                        TestMethodDis(this.txtMethod, strIndex + "阶段控制:变形\r\n", Color.Crimson);
                        TestMethodDis(this.txtMethod, "速度:" + customControlType[1].ToString() + " mm/s\r\n", Color.Crimson);
                        break;
                }

                #endregion

                #region 阶段停止转换点
                //停止转换点：位移 0x80,负荷 0x81,变形 0x82, 应力,应变,
                switch (customControlType[2])
                {
                    case "0":
                        //停止的类型 位移
                        //泛型命令组
                        m_CustomCommand.m_StopPointType = 0x80;
                        m_CustomCommand.m_StopPointChannel = m_DSensorArray[0].SensorIndex;
                        m_CustomCommand.m_StopPoint = (int)(double.Parse(customControlType[3]) * 1000.0);
                        TestMethodDis(this.txtMethod, strIndex + "阶段结束:位移\r\n", Color.Crimson);
                        TestMethodDis(this.txtMethod, "结束值:" + customControlType[3].ToString() + " mm\r\n", Color.Crimson);
                        break;
                    case "1":
                        //停止的类型 负荷
                        //m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                        //m_ScaleValue = m_ScaleValue >> 8;
                        //m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                        //泛型命令组
                        m_CustomCommand.m_StopPointType = 0x81;
                        m_CustomCommand.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                        double m_SR = GetSensorSR((ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale);//(m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                        m_CustomCommand.m_StopPoint = (int)((double.Parse(customControlType[3]) * 1000.0 / m_SR) + 0.2d);
                        TestMethodDis(this.txtMethod, strIndex + "阶段结束:负荷\r\n", Color.Crimson);
                        TestMethodDis(this.txtMethod, "结束值:" + customControlType[3].ToString() + " kN\r\n", Color.Crimson);
                        break;
                    case "2":
                        //停止的类型 变形
                        //m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                        //m_ScaleValue = m_ScaleValue >> 8;
                        //m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));

                        //泛型命令组
                        m_CustomCommand.m_StopPointType = 0x82;
                        m_CustomCommand.m_StopPointChannel = m_ESensorArray[0].SensorIndex;

                        double m_SR1 = GetSensorSR((ushort)m_SensorArray[m_ESensorArray[0].SensorIndex].scale); //(m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                        m_CustomCommand.m_StopPoint = (int)((double.Parse(customControlType[3]) * 1000.0 / m_SR1) + 0.2d);

                        TestMethodDis(this.txtMethod, strIndex + "阶段结束:变形\r\n", Color.Crimson);
                        TestMethodDis(this.txtMethod, "结束值:" + customControlType[3].ToString() + " mm\r\n", Color.Crimson);

                        break;
                    case "3":
                        //停止类型 应力 转换为负荷 为 应力*面积 = 负荷；1Mp * 1mm^2 = 1N                                   
                        //m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                        //m_ScaleValue = m_ScaleValue >> 8;
                        //m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));
                        //泛型命令组
                        m_CustomCommand.m_StopPointType = 0x81;
                        m_CustomCommand.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                        double m_SR2 = GetSensorSR((ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale);// (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                        m_CustomCommand.m_StopPoint = (int)((double.Parse(customControlType[3]) * m_S0 * 1000.0 / m_SR2) + 0.2d);
                        TestMethodDis(this.txtMethod, strIndex + "阶段结束:应力\r\n", Color.Crimson);
                        TestMethodDis(this.txtMethod, "结束值:" + customControlType[3].ToString() + " MPa\r\n", Color.Crimson);

                        break;
                    case "4":
                        //停止类型 应变 = 变形/标距 转换成变形 = 应变 * 引伸计标距Le 
                        //m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                        //m_ScaleValue = m_ScaleValue >> 8;
                        //m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));

                        //泛型命令组
                        m_CustomCommand.m_StopPointType = 0x82;
                        m_CustomCommand.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                        double m_SR3 = GetSensorSR((ushort)m_SensorArray[m_ESensorArray[0].SensorIndex].scale);// (m_ScaleValue * 1.0d) / (m_Resolution * 1.0d);
                        m_CustomCommand.m_StopPoint = (int)((double.Parse(customControlType[3]) * 1000.0 * m_Le / m_SR3) + 0.2d);

                        TestMethodDis(this.txtMethod, strIndex + "阶段结束:应变\r\n", Color.Crimson);
                        TestMethodDis(this.txtMethod, "结束值:" + customControlType[3].ToString() + " %\r\n", Color.Crimson);

                        break;
                }
                #endregion

                TestMethodDis(this.txtMethod, "--------\r\n", Color.Crimson);
            }
            return m_CustomCommand;
        }

        #region
        /// <summary> 
        /// 不同颜色显示试验方法阶段的详细描述
        /// </summary> 
        /// <param name="text"></param> 
        public void TestMethodDis(RichTextBox rtbox, string text, Color c)
        {
            rtbox.SelectionColor = c;
            rtbox.AppendText(text);
            rtbox.SelectionColor = Color.Black;

        }
        #endregion

        //简易日期查询
        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            Task t = tskReadSample();
        }

        async Task tskReadSample()
        {
            this.tvTestSample.Nodes.Clear();
            var t = Task<List<TreeNode>>.Run(() =>
             {
                 return TestStandard.SampleControl.ReadSample(this.dateTimePicker.Value.Date);
             });
            await t;
            //this.BeginInvoke(new Action(() =>
            //{ 
                if (t.Result != null)
                { 
                    List<TreeNode> ltn = (List<TreeNode>)t.Result;
                    foreach (TreeNode tn in ltn)
                    {
                        this.tvTestSample.Nodes.Add(tn);
                    }
                }
                else
                    this.tvTestSample.Nodes.Add("无");
                this.tvTestSample.ExpandAll(); 
            //}));
        }

        delegate void delInitCurveCount(ZedGraph.ZedGraphControl zedGraph, string[] selarry, string sampleMode, string[] colorarray);

        //读取曲线文件
        private void readCurveName(ZedGraph.ZedGraphControl zedGraph, string curveName, string path)
        {
            //若曲线存在
            string curvePath = @"E:\衡新试验数据\" + "Curve\\" + path + "\\" + curveName + ".txt";
            if (File.Exists(curvePath))
            {
                //读取曲线 
                List<gdata> _List_DataReadOne = new List<gdata>();
                using (StreamReader srLine = new StreamReader(curvePath))
                {
                    string[] testSampleInfo1 = srLine.ReadLine().Split(',');
                    string[] testSampleInfo2 = srLine.ReadLine().Split(',');
                    if (srLine.ReadLine() != null)
                    { string[] testSampleInfo3 = srLine.ReadLine().Split(','); }
                    String line;
                    // Read and display lines from the file until the end of
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
                        _List_DataReadOne.Add(_gdata);
                    }
                    srLine.Close();
                    srLine.Dispose();
                }

                //显示曲线
                LineItem LineItem0 = zedGraph.GraphPane.CurveList[curveName] as LineItem;
                if (LineItem0 == null)
                    return;

                //第二步:在CurveItem中访问PointPairList(或者其它的IPointList)，根据自己的需要增加新数据或修改已存在的数据
                IPointListEdit LineItemListEdit_0 = LineItem0.Points as IPointListEdit;
                if (LineItemListEdit_0 == null)
                    return;
                int lCount = _List_DataReadOne.Count - 2;
                int step = lCount / 2000;
                if (step == 0)
                    step = 1;

                #region  20130428 添加lock锁
                //lock (m_state)
                //{

                for (int i = 0; i < lCount; i += step)
                {
                    //采集数据
                    //时间
                    float time = _List_DataReadOne[i].Ts;
                    //力
                    float F1value = _List_DataReadOne[i].F1;
                    //应力
                    float R1value = _List_DataReadOne[i].YL1;
                    //位移
                    float D1value = _List_DataReadOne[i].D1;
                    //变形
                    float BX1value = _List_DataReadOne[i].BX1;
                    //应变
                    float YB1value = _List_DataReadOne[i].YB1;

                    //显示曲线数据
                    #region  cmbYr,cmbXr 轴
                    switch (this._ShowY)
                    {
                        case 1:
                            switch (this._ShowX)
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
                            switch (this._ShowX)
                            {
                                case 1:
                                    //strCurveName[0] = "应力/时间";
                                    LineItemListEdit_0.Add(time, R1value);
                                    break;
                                case 2:
                                    //strCurveName[0] = "应力/位移";
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
                                    LineItemListEdit_0.Add(R1value, R1value);
                                    break;
                                default:
                                    //strCurveName[0] = "";
                                    break;
                            }
                            break;
                        case 3:
                            switch (this._ShowX)
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
                            switch (this._ShowX)
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
                            break;
                    }
                    #endregion
                }
                //}
                #endregion

                _List_DataReadOne.Clear();
                zedGraph.Refresh();
                zedGraph.RestoreScale(zedGraph.GraphPane);
            }
        }

        //private void ReadEnd(IAsyncResult ar)
        //{
        //    m_fs.EndRead(ar);
        //    m_fs.Close();
        //}

        //显示一条曲线
        private void showCurve(List<gdata> listGData, ZedGraph.ZedGraphControl zgControl, string curveName)
        {
            LineItem LineItem0 = zgControl.GraphPane.CurveList[curveName] as LineItem;
            if (LineItem0 == null)
                return;

            //第二步:在CurveItem中访问PointPairList(或者其它的IPointList)，根据自己的需要增加新数据或修改已存在的数据
            IPointListEdit LineItemListEdit_0 = LineItem0.Points as IPointListEdit;
            if (LineItemListEdit_0 == null)
                return;
            int lCount = listGData.Count;
            for (Int32 i = 0; i < lCount; i++)
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
                switch (this._ShowY)
                {
                    case 1:
                        switch (this._ShowX)
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
                        switch (this._ShowX)
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
                        switch (this._ShowX)
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
                        switch (this._ShowX)
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


            zgControl.Refresh();
            zgControl.RestoreScale(zgControl.GraphPane);

            //if (this._RPPList0 != null && this._RPPList0[sampleIndex].Count > 0)
            //{
            //    zgControl.GraphPane.CurveList[0 + 6 * sampleIndex].Label.Text += " Max:(" + getFm(_RPPList0[sampleIndex]).Y + "/" + getFm(_RPPList0[sampleIndex]).X + ")";
            //}
        }

        private void ZeroAllValue()
        {
            //m_Displacement_Test = 0f;      //位移
            //m_Elongate_Test = 0f;          //变形
            //m_Load_Test = 0f;              //力
            //m_Time_Test = 0f;              //时间
            //m_Stress_Test = 0f;            //应力
            //m_Strain_Test = 0f;            //应变  
            m_useExten = false;
            m_useExten2 = false;
            m_Fm = 0;//最大力值
            m_Fmax = 0;
            m_FRH = 0;//上屈服力值
            m_FRL = 0;//下屈服力值
            m_Fn = 0;//实时力值 
            m_FlagStage1Start = false;//阶段1启动标志
            m_FlagStage1Stop = false;//阶段1停止标志
            m_FlagStage2Start = false;//阶段2启动标志
            m_FlagStage2Stop = false;
            m_FlagStage3Start = false;
            m_FlagStage3Stop = false;
            m_F = 0;//实时采集力值
            m_FlagFRH = false;//上屈服点已求出标志
            m_FlagFRL = false;//下屈服点已求出标志
            //m_StartCounter = 0;//判断是否要进行功能的计数器
            m_CheckStop = true;
            m_Check = false;
            m_RLCounter = 0;//平台下屈服计数器
            m_a = 0;
            m_k = 0;
            m_fr05index = 0;
            m_fr01index = 0;
            m_ep02L0 = 0;
            //_List_Data.Clear();
            _List_Testing_Data.Clear();
        }

        DateTime dt;
        private void tsbtn_Start_Click(object sender, EventArgs e)
        {
            m_hold_data.D1 = 0;
            m_hold_data.F1 = 0;
            m_hold_data.BX1 = 0;
            //Test
            dt = DateTime.Now;
            //if (m_LoadSensorCount == 0)
            //{
            //    MessageBox.Show(this, "未连接设备!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}           
            ShowCurvePanel();
            if (string.IsNullOrEmpty(m_TestSampleNo))
            {
                MessageBox.Show(this, "请选择试样编号", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //所有参与计算的数据清0
            ZeroAllValue();
            //实例化缓存采集数据的泛型变量
            //_List_Data = new List<gdata>();
            _List_Testing_Data = new ArrayList();
            //各控件 Enable设置
            this.tsbtn_Start.Enabled = false;
            this.tsbtnReturn.Enabled = false;
            this.tsbtn_Stop.Enabled = true;
            this.tsbtn_Pause.Enabled = true;
            this.tvTestSample.Enabled = false;
            this.tsbtn_Curve.Enabled = false;
            this.tsbtn_Exit.Enabled = false;
            this.tsbtn_Return.Enabled = false;
            this.tsbtn_Zero.Enabled = false;
            this.btnZeroD.Enabled = this.btnZeroF.Enabled = this.btnZeroS.Enabled = false;
            this.dateTimePicker.Enabled = false;
            //m_IsReturn = false;
            //发送开始命令
            SendStartTest();
            m_startLm = m_Displacement;
            //进行试验标志
            isTest = true;
            //添加实时显示曲线数据线程
            if (_threadShowCurve == null)
            {
                _threadShowCurve = new Thread(new ThreadStart(AddChartDataLoop));
                _threadShowCurve.IsBackground = true;
                _threadShowCurve.Start();
                //_threadShowCurve.Join();
            }
        }

        private void SendStartTest()
        {
            lock (m_state)
            {
                Thread.Sleep(30);
                byte[] buf = new byte[5];
                int ret;
                buf[0] = 0x04;									//命令字节
                buf[1] = 0xf1;							        //试验启动命令
                buf[2] = 0x00;
                buf[3] = 0x00;
                buf[4] = 0x00;
                ret = RwUsb.WriteData1582(1, buf, 5, 1000);				//发送写命令
                Thread.Sleep(30);
            }
        }

        /// <summary>
        /// 计算试验结果
        /// </summary>
        /// <param name="lGdata"></param>
        private void CalcData(List<gdata> lGdata, bool isSelReH, bool isSelReL)
        {
            try
            {
                if (lGdata != null)
                {
                    int lCount = lGdata.Count;
                    if (lCount < 1) return;
                    m_Fn = 0;// float.MinValue;
                    m_F = 0;// float.MinValue;
                    m_Fm = 0;// float.MinValue;
                    m_Fmax = 0;// float.MinValue;
                    //int start = 0;
                    //if (lCount > 50)
                    //    start = 50;
                    for (int i = 1; i < lCount; i++)
                    {
                        float load = (float)lGdata[i].F1;
                        if (load >= m_CheckStopValue * 2)
                        {
                            //求取最大值的点 
                            //存储前一点的值
                            m_Fn = m_F;
                            //实时得值
                            m_F = load;

                            if (m_F > m_Fmax)
                            {
                                m_Fmax = m_F;
                                m_FmaxIndex = i;
                            }

                            //存储最大值(如果没有选择计算上下屈服,则直接存储最大值)
                            //if (!isSelReL && !isSelReH)
                            //{
                            //如果实时力值大于前一点力值
                            if (m_F > m_Fm)
                            {
                                m_Fm = m_F;
                                m_FmIndex = i;
                            }
                            //}

                            if (isSelReH || isSelReL)
                            {
                                #region 计算上下屈服
                                //-----------------上升阶段------------------
                                if (m_F > m_Fn + 10 * m_LoadResolutionValue) //Convert.ToDouble(m_Pre5DotValue[4].ToString())
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
                                                                m_FRL = m_Fn;
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
                                            m_FRH = 0;
                                        }
                                    }
                                }
                                else if (m_F < m_Fn - 10 * m_LoadResolutionValue)// Convert.ToDouble(m_Pre5DotValue[4].ToString())下降阶段
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
                                    }
                                    else//如果阶段1已经发生
                                    {
                                        //第二次下降
                                        if (m_FlagStage2Start == false)
                                        {
                                            //追踪下屈服
                                            m_FRLFirst = m_F;
                                            if (m_FlagStage1Stop)
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
                                            m_FmIndex = i;
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 计算试验结果Fbb FbbIndex
        /// </summary>
        /// <param name="lGdata"></param>
        private void CalcData_B(List<gdata> lGdata)
        {
            if (lGdata != null)
            {
                //计算最大值 Fbb
                int lCount = lGdata.Count;
                m_Fn = float.MinValue;
                m_F = float.MinValue;
                m_Fmax = float.MinValue;
                for (int i = 1; i < lCount; i++)
                {
                    float load = (float)lGdata[i].F1;
                    m_Fn = m_F;
                    m_F = load;
                    if (m_F > m_Fmax)
                    {
                        m_Fmax = m_F;
                        m_FmaxIndex = i;
                    }
                }
            }
        }


        private void tsbtn_Stop_Click(object sender, EventArgs e)
        {
            SendStopTest();
            //停止试验 
            isTest = false;
            _threadShowCurve = null;
            m_stopLm = m_Displacement;
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.BringToFront();
            //ZeroAllValue();
            //各控件 Enable设置
            this.tsbtn_Start.Enabled = false;
            this.tsbtnReturn.Enabled = true;
            this.tsbtn_Pause.Enabled = false;
            this.tsbtn_Stop.Enabled = false;
            this.tvTestSample.Enabled = true;
            this.tsbtn_Curve.Enabled = true;
            this.tsbtn_Exit.Enabled = true;
            this.tsbtn_Return.Enabled = true;
            this.tsbtn_Zero.Enabled = true;
            this.btnZeroD.Enabled = this.btnZeroF.Enabled = this.btnZeroS.Enabled = true;
            this.dateTimePicker.Enabled = true;
            //m_IsReturn = false;
            this.lblUseExten.Text = "停用引伸计";
            this.lblUseExten.ForeColor = Color.DarkOrange;
            this.lblUseExten.Refresh();

            //确认试验类型 ,写入试验结果
            string strContain = this.tvTestSample.SelectedNode.Name;
            switch (strContain)
            {
                #region GBT23615-2009TensileZong
                case "GBT23615-2009TensileZong_c":
                    if (!string.IsNullOrEmpty(m_TestSampleNo))
                    {
                        BLL.GBT236152009_TensileZong bllt = new HR_Test.BLL.GBT236152009_TensileZong();
                        Model.GBT236152009_TensileZong mt = bllt.GetModel(m_TestSampleNo);
                        int index = this.tvTestSample.SelectedNode.Parent.Index;
                        //保存曲线
                        string curveName = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + mt.testSampleNo + ".txt";
                        if (File.Exists(curveName))
                        {
                            FileStream fs = new FileStream(curveName, FileMode.Append, FileAccess.Write);
                            foreach (gdata gd in _List_Testing_Data)//_List_Array_Data
                            {
                                utils.AddText(fs, gd.F1 + "," + gd.F2 + "," + gd.F3 + "," + gd.D1 + "," + gd.D2 + "," + gd.D3 + "," + gd.BX1 + "," + gd.BX2 + "," + gd.BX3 + "," + gd.YL1 + "," + gd.YL2 + "," + gd.YL3 + "," + gd.YB1 + "," + gd.YB2 + "," + gd.YB3 + "," + gd.Ts);
                                utils.AddText(fs, "\r\n");
                            }
                            fs.Flush();
                            fs.Close();
                            fs.Dispose();
                        }
                        Thread.Sleep(20);
                        //读取试验方法中是否选取了保存曲线
                        BLL.GBT236152009_SelZong bllts = new HR_Test.BLL.GBT236152009_SelZong();
                        Model.GBT236152009_SelZong mts = bllts.GetModel(mt.testMethod);
                        if (mts != null)
                        {
                            m_isSavecurve = mts.isSaveCurve;
                            m_isHandaz = mts.Z;
                        }

                        //计算数据 Fm ReL ReH
                        m_l = ReadOneListData(m_path, m_TestSampleNo);
                        if (m_l != null)
                        {
                            double Fmax = 0;
                            int FmaxIndex = 0;
                            TestStandard.GBT23615_2009.CalcFmax(m_l, out Fmax, out FmaxIndex);
                            m_FmaxIndex = FmaxIndex;
                            m_Fmax = (float)Math.Round(Fmax, 2);
                            mt.Fmax = m_Fmax;
                            m_Rp = (float)Math.Round(Fmax / (double)mt.S0, 2);
                            mt.T2 = m_Rp;
                        }

                        mt.testCondition = "-";// this.lblTestMethod.Text; 
                        mt.isFinish = true;

                        frmAZ az = new frmAZ();
                        az.LocationChanged += new EventHandler(az_LocationChanged);
                        az.txtS0.Enabled = false;
                        az.gbS0.Enabled = false;
                        az._L0 = (double)mt.L0;
                        Thread.Sleep(50);
                        //是否手动求取AZ
                        if (m_isHandaz)
                        {
                            if (DialogResult.OK == az.ShowDialog(this))
                            {
                                mt.Z = az._A;
                            }
                            else
                            {
                                mt.Z = 0;
                            }
                        }
                        else
                        {
                            mt.Z = 0;
                        }

                        if (bllt.Update(mt))
                        {
                            isShowResult = true;
                            //刷新试验结果列表
                            TestStandard.GBT23615_2009.TensileZong.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            //刷新试样列表
                            //TestStandard.SampleControl.ReadSample(this.tvTestSample, this.dateTimePicker);
                            this.tvTestSample.SelectedNode.ImageIndex = 1;
                            this.tvTestSample.Update();
                            this.tvTestSample.Refresh();
                            ShowResultCurve();

                            //如果不保存曲线则删除该曲线数据
                            if (!m_isSavecurve)
                                File.Delete(curveName);

                            //默认选择全部完成的试样
                            this.zedGraphControl.GraphPane.CurveList.Clear();
                            foreach (DataGridViewRow drow in this.dataGridView.Rows)
                            {
                                drow.Cells[0].Value = false;
                                drow.Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                            }
                            if (index >= 0)
                                this.tvTestSample.Nodes[index].ExpandAll();
                        }
                    }
                    if (m_l != null)
                    {
                        ChangeRealTimeXYChart();
                        ReadResultData(m_l);
                    }
                    break;
                #endregion

                #region GBT23615-2009TensileHeng
                case "GBT23615-2009TensileHeng_c":
                    if (!string.IsNullOrEmpty(m_TestSampleNo))
                    {
                        BLL.GBT236152009_TensileHeng bllt = new HR_Test.BLL.GBT236152009_TensileHeng();
                        Model.GBT236152009_TensileHeng mt = bllt.GetModel(m_TestSampleNo);
                        int index = this.tvTestSample.SelectedNode.Parent.Index;
                        //保存曲线
                        string curveName = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + mt.testSampleNo + ".txt";
                        if (File.Exists(curveName))
                        {
                            FileStream fs = new FileStream(curveName, FileMode.Append, FileAccess.Write);
                            foreach (gdata gd in _List_Testing_Data)//_List_Array_Data
                            {
                                utils.AddText(fs, gd.F1 + "," + gd.F2 + "," + gd.F3 + "," + gd.D1 + "," + gd.D2 + "," + gd.D3 + "," + gd.BX1 + "," + gd.BX2 + "," + gd.BX3 + "," + gd.YL1 + "," + gd.YL2 + "," + gd.YL3 + "," + gd.YB1 + "," + gd.YB2 + "," + gd.YB3 + "," + gd.Ts);
                                utils.AddText(fs, "\r\n");
                            }
                            fs.Flush();
                            fs.Close();
                            fs.Dispose();
                        }
                        Thread.Sleep(20);
                        //读取试验方法中是否选取了保存曲线
                        BLL.GBT236152009_SelHeng bllts = new HR_Test.BLL.GBT236152009_SelHeng();
                        Model.GBT236152009_SelHeng mts = bllts.GetModel(mt.testMethod);
                        if (mts != null)
                        {
                            m_isSavecurve = mts.isSaveCurve;
                        }

                        //计算数据 Fm ReL ReH
                        m_l = ReadOneListData(m_path, m_TestSampleNo);
                        if (m_l != null)
                        {
                            double Fmax = 0;
                            int FmaxIndex = 0;
                            TestStandard.GBT23615_2009.CalcFmax(m_l, out Fmax, out FmaxIndex);
                            m_FmaxIndex = FmaxIndex;
                            m_Fmax = (float)Math.Round(Fmax, 2);
                            mt.Fmax = m_Fmax;
                            m_Rp = (float)Math.Round(Fmax / (double)mt.S0, 2);
                            mt.T1 = m_Rp;
                        }

                        mt.testCondition = "-";// this.lblTestMethod.Text; 
                        mt.isFinish = true;

                        if (bllt.Update(mt))
                        {
                            isShowResult = true;
                            //刷新试验结果列表
                            TestStandard.GBT23615_2009.TensileHeng.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            //刷新试样列表
                            //TestStandard.SampleControl.ReadSample(this.tvTestSample, this.dateTimePicker);
                            this.tvTestSample.SelectedNode.ImageIndex = 1;
                            this.tvTestSample.Update();
                            this.tvTestSample.Refresh();
                            ShowResultCurve();

                            //如果不保存曲线则删除该曲线数据
                            if (!m_isSavecurve)
                                File.Delete(curveName);

                            //默认选择全部完成的试样
                            this.zedGraphControl.GraphPane.CurveList.Clear();
                            foreach (DataGridViewRow drow in this.dataGridView.Rows)
                            {
                                drow.Cells[0].Value = false;
                                drow.Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                            }
                            if (index >= 0)
                                this.tvTestSample.Nodes[index].ExpandAll();
                        }
                    }
                    if (m_l != null)
                    {
                        ChangeRealTimeXYChart();
                        ReadResultData(m_l);
                    }
                    break;
                #endregion

                #region GBT228-2010
                case "GBT228-2010_c":
                    try
                    {
                        if (!string.IsNullOrEmpty(m_TestSampleNo))
                        {
                            BLL.TestSample bllts = new HR_Test.BLL.TestSample();
                            Model.TestSample mts = bllts.GetModel(m_TestSampleNo);

                            int index = this.tvTestSample.SelectedNode.Parent.Index;

                            //保存曲线
                            string curveName = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + mts.testSampleNo + ".txt";
                            if (File.Exists(curveName))
                            {
                                FileStream fs = new FileStream(curveName, FileMode.Append, FileAccess.Write);
                                foreach (gdata gd in _List_Testing_Data)//_List_Array_Data
                                {
                                    utils.AddText(fs, gd.F1 + "," + gd.F2 + "," + gd.F3 + "," + gd.D1 + "," + gd.D2 + "," + gd.D3 + "," + gd.BX1 + "," + gd.BX2 + "," + gd.BX3 + "," + gd.YL1 + "," + gd.YL2 + "," + gd.YL3 + "," + gd.YB1 + "," + gd.YB2 + "," + gd.YB3 + "," + gd.Ts);
                                    utils.AddText(fs, "\r\n");
                                }
                                fs.Flush();
                                fs.Close();
                                fs.Dispose();
                            }

                            //读取试验方法中是否选取了Rp
                            BLL.SelTestResult bst = new HR_Test.BLL.SelTestResult();
                            Model.SelTestResult mst = bst.GetModel(mts.testMethodName);

                            if (mst != null)
                            {
                                m_isSelRp = mst.Rp;
                                m_isSelReH = mst.ReH;
                                m_isSelReL = mst.ReL;
                                m_isSelFm = mst.Fm;
                                m_isSelRm = mst.Rm;
                                m_isHandaz = mst.Handaz;
                                m_isSavecurve = mst.Savecurve;
                                m_isSelLm = mst.Lm;
                                m_isSelDeltaLm = mst.deltaLm;
                                m_isSelA = mst.A;
                                m_isSelZ = mst.Z;
                                m_isSelE = mst.E;
                            }

                            //计算数据 Fm ReL ReH
                            m_l = ReadOneListData(m_path, m_TestSampleNo);
                            if (m_l != null)
                            {
                                CalcData(m_l, m_isSelReL, m_isSelReH);
                            }

                            //写入最大值 Fm Rm A Z
                            //MessageBox.Show("m_FRH=" + m_FRH.ToString() + ",m_Fm=" + m_Fm.ToString() + ",m_FRLFirst=" + m_FRLFirst.ToString() + ",m_FRL=" + m_FRL.ToString());
                            if (m_l.Count > m_FmIndex)
                            {
                                mts.Fm = Convert.ToDouble((m_l[m_FmIndex].F1).ToString("f2"));
                                mts.Rm = Convert.ToDouble(m_l[m_FmIndex].YL1.ToString("f2"));
                            }

                            //上屈服强度(力首次下降前对应的应力)
                            if (m_isSelReH)
                                mts.ReH = Convert.ToDouble((m_FRH / m_S0).ToString("f2"));
                            else
                                mts.ReH = 0;
                            //下屈服强度(不计初始瞬时效应时屈服阶段中的最小力对应的应力) 
                            if (m_isSelReL)
                                mts.ReL = Convert.ToDouble((m_FRL / m_S0).ToString("f2"));
                            else
                                mts.ReL = 0;

                            if (m_isSelLm)
                                mts.Lm = Math.Round((m_stopLm - m_startLm) / 1000.0, 2);


                            //如果使用引伸计，用力-变形曲线求Fp02,并且试验结果选取了Rp,并且试验过程中使用引伸计
                            //m_extenType 读取数据库确认参数值, m_isSelRp 确认是否试验结果选择了Rp,_useExten 试验界面是否使用引申计
                            if (m_extenType != 2 && m_useExten) // m_isSelRp && 
                            {
                                mts.isUseExtensometer = true;
                                m_FrIndex = m_FmIndex;
                                int tempIndex = 0;
                                int count = 0;

                                if (m_isSelDeltaLm)
                                    mts.deltaLm = Math.Round(m_l[m_FmIndex].BX1 / 1000.0, 2);

                                //断裂总延伸
                                if (m_l.Count > 2)
                                {
                                    mts.Lf = Math.Round(m_l[m_l.Count - 2].BX1 / 1000.0, 2);
                                }

                                //逐次逼近法 求取Fp02 ,只要使用了引申计，都会执行此函数
                                do
                                {
                                    tempIndex = m_FrIndex;
                                    if (GetFp02IndexOnE(m_l, tempIndex, out m_FrIndex, out m_a, out m_k, out m_fr05index, out m_fr01index, out m_ep02L0))
                                    {
                                        count++;
                                        //MessageBox.Show(count.ToString() + ":" + m_FrIndex.ToString() + "," + tempIndex.ToString());  

                                        //自动求取A

                                        /*  1、条件：   a.设置 使用变形引伸计
                                         *              b.设置求取断后伸长率
                                         *  2、步骤：   a.以逐次逼近法测定曲线弹性段框图为基础，求出按负荷变形方式下的偏移量a和曲线的斜率，如果试验没有要求
                                         *                求Rp,可以在试验完成的曲线上不描绘弹性逼近线，Rp结果不存于数据库，曲线界面不出现Rp的结果。
                                         *              b.按公式  k=F结束点力点/L,输入参数 k 和 F，求出 L，
                                         *              c.总变形量以公式 ΔLag = ΔLf（断裂总延伸) - L-a;  
                                         *              d.求出A = ΔLag / Le(引申计标距）
                                        */
                                        if (m_isSelA)
                                        {
                                            double L = m_l[m_l.Count - 2].F1 / (m_k * 1000.0);
                                            double deltaLag = Math.Round((double)mts.Lf - L - m_a, 2);
                                            mts.A = Math.Round(deltaLag * 100.0 / m_Le, 2);
                                        }

                                        if (m_isSelE)
                                        {
                                            //计算弹性模量,取 05的点的 应力/应变   GPa
                                            double yl = (m_l[m_fr05index].YL1 - m_l[m_fr01index].YL1);
                                            double yb = (m_l[m_fr05index].YB1 - m_l[m_fr01index].YB1) / 100.0;
                                            if (yb > 0)
                                                mts.E = Math.Round(yl / (1000.0 * yb), 2);
                                            else
                                                mts.E = 0;
                                            m_Eb = (float)mts.E.Value;
                                        }

                                        if (count > 500)
                                        {
                                            break;
                                        }
                                    }
                                }
                                while (m_FrIndex > tempIndex + 3 || m_FrIndex < tempIndex - 3);

                                if (count > 500)
                                {
                                    //MessageBox.Show(this, "自动计算失败!", "提示(F-E)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    //m_calSuccess = false;
                                    //mts.Rp = 0;
                                    //m_Rp = 0;                                   
                                    m_FrIndex = m_FmIndex;
                                    tempIndex = m_FrIndex;
                                    GetFp02IndexOnE(m_l, tempIndex, out m_FrIndex, out m_a, out m_k, out m_fr05index, out m_fr01index, out m_ep02L0);
                                    mts.Rp = Math.Round(m_l[m_FrIndex].YL1, 2);
                                    m_Rp = (float)Math.Round(m_l[m_FrIndex].YL1, 2);
                                    m_calSuccess = true;
                                    if (m_isSelE)
                                    {
                                        //计算弹性模量,取 05的点的 应力/应变
                                        double yl = (m_l[m_fr05index].YL1 - m_l[m_fr01index].YL1);
                                        double yb = (m_l[m_fr05index].YB1 - m_l[m_fr01index].YB1) / 100.0;
                                        if (yb > 0)
                                            mts.E = Math.Round(yl / (1000.0 * yb), 2);
                                        else
                                            mts.E = 0;
                                        m_Eb = (float)mts.E.Value;
                                    }
                                }
                                else
                                {
                                    mts.Rp = Math.Round(m_l[m_FrIndex].YL1, 2);
                                    m_Rp = (float)Math.Round(m_l[m_FrIndex].YL1, 2);
                                    m_calSuccess = true;
                                    if (m_isSelE)
                                    {
                                        //计算弹性模量,取 05的点的 应力/应变
                                        double yl = (m_l[m_fr05index].YL1 - m_l[m_fr01index].YL1);
                                        double yb = (m_l[m_fr05index].YB1 - m_l[m_fr01index].YB1) / 100.0;
                                        if (yb > 0)
                                            mts.E = Math.Round(yl / (1000.0 * yb), 2);
                                        else
                                            mts.E = 0;
                                        m_Eb = (float)mts.E.Value;
                                    }
                                }
                                //应力/应变 弹性模量E N/m²
                                //if(m_isSelE)
                                //    mts.E = 

                                //重新读取_List_Data显示结果曲线
                                //if (mts.Rp > 0)
                                //{
                                //标注曲线20130515取消自动求取的Rp划线,只在试验停止后显示
                                //Graphics g = this.pbChart.CreateGraphics();
                                //DrawResult(g,m_l, m_k, m_a, m_fr05index, m_fr01index);
                                //}

                            }
                            else if (m_isSelRp & !m_useExten) //如果没用引伸计，用力-位移曲线求Fp02
                            {
                                mts.isUseExtensometer = false;
                                int tempIndex = 0;
                                //FrIndex初始值
                                m_FrIndex = m_FmIndex;
                                int count = 0;

                                if (m_isSelDeltaLm)
                                    mts.deltaLm = Math.Round(m_l[m_FmIndex].D1 / 1000.0, 2);

                                //逐次逼近法 求取Fp02
                                do
                                {
                                    tempIndex = m_FrIndex;
                                    if (GetFp02Index(m_l, tempIndex, out m_FrIndex, out m_a, out m_k, out m_fr05index, out m_fr01index, out m_ep02L0))
                                    {
                                        count++;
                                        if (count > 500)
                                            break;
                                    }
                                }
                                while (m_FrIndex > tempIndex + 3 || m_FrIndex < tempIndex - 3);

                                //MessageBox.Show(count.ToString() + ":" + m_FrIndex.ToString() + "," + tempIndex.ToString());
                                if (count > 500)
                                {
                                    //MessageBox.Show(this, "自动求取计算失败!", "提示(F-D)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    //mts.Rp = 0;
                                    //m_Rp = 0;
                                    //mts.isUseExtensometer = false;
                                    //m_calSuccess = false;
                                    m_FrIndex = m_FmIndex;
                                    tempIndex = m_FrIndex;
                                    GetFp02Index(m_l, tempIndex, out m_FrIndex, out m_a, out m_k, out m_fr05index, out m_fr01index, out m_ep02L0);
                                    mts.Rp = Math.Round(m_l[m_FrIndex].YL1, 2);
                                    m_Rp = (float)Math.Round(m_l[m_FrIndex].YL1, 2);
                                    if (m_isSelE)
                                    {
                                        //计算弹性模量,取 05的点的 应力/应变
                                        double yl = (m_l[m_fr05index].YL1 - m_l[m_fr01index].YL1);
                                        double yb = ((m_l[m_fr05index].D1 - m_l[m_fr01index].D1) / (m_L0 * 1000.0));
                                        if (yb > 0)
                                            mts.E = Math.Round(yl / (1000.0 * yb), 2);
                                        else
                                            mts.E = 0;
                                        m_Eb = (float)mts.E.Value;
                                    }
                                    m_calSuccess = true;
                                }
                                else
                                {
                                    mts.Rp = Math.Round(m_l[m_FrIndex].YL1, 2);
                                    m_Rp = (float)Math.Round(m_l[m_FrIndex].YL1, 2);
                                    if (m_isSelE)
                                    {
                                        //计算弹性模量,取 05的点的 应力/应变
                                        double yl = (m_l[m_fr05index].YL1 - m_l[m_fr01index].YL1);
                                        double yb = ((m_l[m_fr05index].D1 - m_l[m_fr01index].D1) / (m_L0 * 1000.0));
                                        if (yb > 0)
                                            mts.E = Math.Round(yl / (1000.0 * yb), 2);
                                        else
                                            mts.E = 0;
                                        m_Eb = (float)mts.E.Value;
                                    }
                                    m_calSuccess = true;
                                }

                                //if (mts.Rp > 0)
                                //{
                                //只在试验停止后显示划线和标注点
                                //Graphics g = this.pbChart.CreateGraphics();
                                //DrawResult(g,m_l, m_k, m_a, m_fr05index, m_fr01index);
                                //}
                            }

                            mts.testCondition = "-";// this.lblTestMethod.Text; 
                            //Test
                            mts.isFinish = true;

                            frmAZ az = new frmAZ();
                            az.LocationChanged += new EventHandler(az_LocationChanged);
                            az._L0 = (double)mts.L0;
                            az._S0 = (double)mts.S0;
                            az._A = (double)mts.A;
                            az.txtL0.Text = ((1 + mts.A / 100.0) * mts.Le).ToString();
                            Thread.Sleep(50);

                            //是否手动求取AZ
                            if (m_isHandaz)
                            {
                                if (DialogResult.OK == az.ShowDialog(this))
                                {
                                    mts.A = az._A;
                                    mts.Z = az._Z;
                                    mts.Lu = az._Lu;
                                }
                                else
                                {
                                    mts.A = 0;
                                    mts.Z = 0;
                                    mts.Lu = 0;
                                }
                            }
                            else
                            {
                                mts.A = 0;
                                mts.Z = 0;
                                mts.Lu = 0;
                            }

                            if (bllts.Update(mts))
                            {
                                isShowResult = true;
                                TestStandard.GBT228_2010.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                                //无刷新左侧树形节点，直接修改imageindex;
                                //TestStandard.SampleControl.ReadSample(this.tvTestSample, this.dateTimePicker);
                                this.tvTestSample.SelectedNode.ImageIndex = 1;
                                this.tvTestSample.Update();
                                this.tvTestSample.Refresh();
                                ShowResultCurve();

                                //如果不保存曲线则删除该曲线数据
                                if (!m_isSavecurve)
                                    File.Delete(curveName);

                                //默认选择全部完成的试样
                                this.zedGraphControl.GraphPane.CurveList.Clear();
                                foreach (DataGridViewRow drow in this.dataGridView.Rows)
                                {
                                    drow.Cells[0].Value = false;
                                    drow.Selected = false;
                                    dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                                }
                                if (index >= 0)
                                    this.tvTestSample.Nodes[index].ExpandAll();
                            }
                        }

                        if (m_l != null)
                        {
                            ChangeRealTimeXYChart();
                            //ReadResultCurve(this.pbChart, m_l);
                            ReadResultData(m_l);
                        }
                    }
                    catch (Exception ee)
                    {
                        //MessageBox.Show(ee.ToString()); 
                        return;
                    }

                    //查找该试验组未做完试验的试样编号
                    //查找第一个未做完的试样编号
                    //if (m_lstNoTestSamples.Count > 0)
                    //{
                    //    m_lstNoTestSamples.Remove(m_TestSampleNo);
                    //    if (m_lstNoTestSamples.Count == 0)
                    //    {
                    //        this.e_Node = null;
                    //        MessageBox.Show("已完成该组试验!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        return;
                    //    }

                    //    TreeNode tn = new TreeNode(m_lstNoTestSamples[0]);
                    //    tn.Text = m_lstNoTestSamples[0];
                    //    tn.Name = "tensile_c";
                    //    tn.ImageIndex = 2;
                    //    tn.SelectedImageIndex = 3;
                    //    e_Node = new TreeNodeMouseClickEventArgs(tn, MouseButtons.Left, 1, 25, 2);
                    //    //查找当前试样的编号以便选中
                    //    TreeNode tf = null;
                    //    foreach (TreeNode t in this.tvTestSample.Nodes)
                    //    {
                    //        tf = FindNodeByValue(t, m_lstNoTestSamples[0]);
                    //        if (tf != null)
                    //            break;
                    //    }

                    //    if (tf != null)
                    //    {
                    //        tvTestSample.SelectedNode = tf;
                    //        if (tf.Parent != null)
                    //            tf.Parent.Expand();
                    //    }
                    //    //如果选择了批量试验
                    //    if (chkTestAll.Checked)
                    //    {
                    //        tvTestSample_NodeMouseClick(this.tvTestSample, e_Node);
                    //    }
                    //}          

                    break;
                #endregion

                #region GBT7314-2005
                case "GBT7314-2005_c":
                    try
                    {
                        if (!string.IsNullOrEmpty(m_TestSampleNo))
                        {
                            BLL.Compress bllts = new HR_Test.BLL.Compress();
                            Model.Compress mts = bllts.GetModel(m_TestSampleNo);

                            int index = this.tvTestSample.SelectedNode.Parent.Index;

                            //保存曲线
                            string curveName = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + mts.testSampleNo + ".txt";
                            if (File.Exists(curveName))
                            {
                                FileStream fs = new FileStream(curveName, FileMode.Append, FileAccess.Write);
                                foreach (gdata gd in _List_Testing_Data)//_List_Array_Data
                                {
                                    utils.AddText(fs, gd.F1 + "," + gd.F2 + "," + gd.F3 + "," + gd.D1 + "," + gd.D2 + "," + gd.D3 + "," + gd.BX1 + "," + gd.BX2 + "," + gd.BX3 + "," + gd.YL1 + "," + gd.YL2 + "," + gd.YL3 + "," + gd.YB1 + "," + gd.YB2 + "," + gd.YB3 + "," + gd.Ts);
                                    utils.AddText(fs, "\r\n");
                                }
                                fs.Flush();
                                fs.Close();
                                fs.Dispose();
                            }
                            Thread.Sleep(20);

                            //读取试验方法中是否选取了Rp
                            BLL.SelTestResult_C bst = new HR_Test.BLL.SelTestResult_C();
                            Model.SelTestResult_C mst = bst.GetModel(mts.testMethodName);

                            if (mst != null)
                            {
                                m_isSelRp = mst.Rpc; //规定非比例压缩强度
                                m_isSelRtc = mst.Rtc;//规定总压缩强度
                                m_isSelReH = mst.ReHc;
                                m_isSelReL = mst.ReLc;
                                m_isSelFm = mst.Fmc;
                                m_isSelRm = mst.Rmc;
                                m_isSelEb = mst.Ec;
                                m_isSavecurve = mst.saveCurve;

                            }
                            //计算数据 Fm ReL ReH
                            m_l.Clear();
                            m_l = ReadOneListData(m_path, m_TestSampleNo);
                            if (m_l != null)
                            {
                                CalcData(m_l, m_isSelReL, m_isSelReH);
                            }

                            //写入最大值 Fm Rm A Z
                            //MessageBox.Show("m_FRH=" + m_FRH.ToString() + ",m_Fm=" + m_Fm.ToString() + ",m_FRLFirst=" + m_FRLFirst.ToString() + ",m_FRL=" + m_FRL.ToString());
                            //保存结果时判断是否超过1000N
                            if (m_l.Count > m_FmaxIndex)
                            {
                                mts.Fmc = Convert.ToDouble((m_l[m_FmaxIndex].F1).ToString("f2"));
                                mts.Rmc = Convert.ToDouble(m_l[m_FmaxIndex].YL1.ToString("G5"));
                            }

                            //上屈服强度(力首次下降前对应的应力)
                            if (m_isSelReH)
                                mts.ReHc = Convert.ToDouble((m_FRH / m_S0).ToString("G5"));
                            else
                                mts.ReHc = 0;
                            //下屈服强度(不计初始瞬时效应时屈服阶段中的最小力对应的应力) 
                            if (m_isSelReL)
                                mts.ReLc = Convert.ToDouble((m_FRL / m_S0).ToString("G5"));
                            else
                                mts.ReLc = 0;

                            //求取Fpc Rpc
                            //如果使用引伸计，用力-变形曲线求Fp02,并且试验结果选取了Rp,并且试验过程中使用引伸计
                            //m_extenType 读取数据库确认参数值, m_isSelRp 确认是否试验结果选择了Rp,_useExten 试验界面是否使用引申计
                            if (m_extenType != 2 && m_isSelRp && m_useExten)
                            {
                                mts.isUseExtensometer = true;
                                m_FrIndex = m_FmIndex;
                                int tempIndex = 0;
                                int count = 0;
                                //逐次逼近法 求取Fp02
                                do
                                {
                                    tempIndex = m_FrIndex;

                                    if (GetFp02IndexOnE_7314(m_l, tempIndex, m_Ep, out m_FrIndex, out m_a, out m_k, out m_fr05index, out m_fr01index, out m_ep02L0))
                                    {
                                        count++;
                                        //MessageBox.Show(count.ToString() + ":" + m_FrIndex.ToString() + "," + tempIndex.ToString());                                    
                                        mts.Rpc = double.Parse((m_l[m_FrIndex].YL1).ToString("G5"));
                                        mts.Fpc = double.Parse((m_l[m_FrIndex].F1).ToString("f2"));
                                        m_Rp = float.Parse((m_l[m_FrIndex].YL1).ToString("G5"));
                                        m_calSuccess = true;
                                        if (count > 500)
                                        {
                                            MessageBox.Show(this, "计算Rpc失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            m_calSuccess = false;
                                            mts.Rpc = 0;
                                            mts.Fpc = 0;
                                            m_Rp = 0;
                                            mts.isUseExtensometer = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        m_calSuccess = false;
                                        mts.Rpc = 0;
                                        mts.Fpc = 0;
                                        m_Rp = 0f;
                                        mts.isUseExtensometer = true;
                                        MessageBox.Show(this, "计算Rpc失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                while (m_FrIndex > tempIndex + 2 || m_FrIndex < tempIndex - 2);

                                //重新读取_List_Data显示结果曲线
                                //if (mts.Rp > 0)
                                //{
                                //标注曲线20130515取消自动求取的Rp划线,只在试验停止后显示
                                //Graphics g = this.pbChart.CreateGraphics();
                                //DrawResult(g,m_l, m_k, m_a, m_fr05index, m_fr01index);
                                //}

                            }
                            else if (m_isSelRp) //如果没用引伸计，用力-位移曲线求Fp02
                            {
                                mts.isUseExtensometer = false;
                                int tempIndex = 0;
                                //FrIndex初始值
                                m_FrIndex = m_FmIndex;
                                int count = 0;
                                //逐次逼近法 求取Fp02
                                do
                                {
                                    tempIndex = m_FrIndex;
                                    if (GetFp02Index_7314(m_l, tempIndex, m_Ep, out m_FrIndex, out m_a, out m_k, out m_fr05index, out m_fr01index, out m_ep02L0))
                                    {
                                        count++;
                                        mts.Rpc = double.Parse((m_l[m_FrIndex].YL1).ToString("G5"));
                                        mts.Fpc = double.Parse((m_l[m_FrIndex].F1).ToString("f2"));
                                        m_Rp = float.Parse((m_l[m_FrIndex].YL1).ToString("G5"));
                                        m_calSuccess = true;
                                        //MessageBox.Show(count.ToString() + ":" + m_FrIndex.ToString() + "," + tempIndex.ToString());
                                        if (count > 500)
                                        {
                                            MessageBox.Show(this, "计算失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            mts.Rpc = 0;
                                            mts.Fpc = 0;
                                            mts.isUseExtensometer = false;
                                            m_calSuccess = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, "计算失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        mts.Rpc = 0;
                                        mts.Fpc = 0;
                                        mts.isUseExtensometer = false;
                                        m_calSuccess = false;
                                    }
                                }
                                while (m_FrIndex > tempIndex + 2 || m_FrIndex < tempIndex - 2);
                            }

                            //求取Rtc
                            //如果使用引伸计，用力-变形曲线求Fp02,并且试验结果选取了Rp,并且试验过程中使用引伸计
                            //m_extenType 读取数据库确认参数值, m_isSelRp 确认是否试验结果选择了Rp,_useExten 试验界面是否使用引申计
                            if (m_extenType != 2 && m_isSelRtc && m_useExten)
                            {
                                mts.isUseExtensometer = true;
                                m_FrIndex = m_FmIndex;
                                int tempIndex = 0;
                                int count = 0;
                                //逐次逼近法 求取Fp02
                                do
                                {
                                    tempIndex = m_FrIndex;

                                    if (GetFp02IndexOnE_7314(m_l, tempIndex, m_Et, out m_FrIndex, out m_a, out m_k, out m_fr05index, out m_fr01index, out m_ep02L0))
                                    {
                                        count++;
                                        //MessageBox.Show(count.ToString() + ":" + m_FrIndex.ToString() + "," + tempIndex.ToString());                                    
                                        mts.Rtc = double.Parse((m_l[m_FrIndex].YL1).ToString("G5"));
                                        mts.Ftc = double.Parse((m_l[m_FrIndex].F1).ToString("f2"));
                                        m_Rtc = float.Parse((m_l[m_FrIndex].YL1).ToString("G5"));
                                        m_calSuccess = true;
                                        if (count > 500)
                                        {
                                            MessageBox.Show(this, "计算Rtc失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            m_calSuccess = false;
                                            mts.Rtc = 0;
                                            mts.Ftc = 0;
                                            m_Rtc = 0;
                                            mts.isUseExtensometer = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        m_calSuccess = false;
                                        mts.Rtc = 0;
                                        mts.Ftc = 0;
                                        m_Rtc = 0f;
                                        mts.isUseExtensometer = true;
                                        MessageBox.Show(this, "计算Rtc失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                while (m_FrIndex > tempIndex + 2 || m_FrIndex < tempIndex - 2);

                                //重新读取_List_Data显示结果曲线
                                //if (mts.Rp > 0)
                                //{
                                //标注曲线20130515取消自动求取的Rp划线,只在试验停止后显示
                                //Graphics g = this.pbChart.CreateGraphics();
                                //DrawResult(g,m_l, m_k, m_a, m_fr05index, m_fr01index);
                                //}

                            }
                            else if (m_isSelRtc) //如果没用引伸计，用力-位移曲线求Fp02
                            {
                                mts.isUseExtensometer = false;
                                int tempIndex = 0;
                                //FrIndex初始值
                                m_FrIndex = m_FmIndex;
                                int count = 0;
                                //逐次逼近法 求取Fp02
                                do
                                {
                                    tempIndex = m_FrIndex;
                                    if (GetFp02Index_7314(m_l, tempIndex, m_Et, out m_FrIndex, out m_a, out m_k, out m_fr05index, out m_fr01index, out m_ep02L0))
                                    {
                                        count++;
                                        mts.Rtc = double.Parse((m_l[m_FrIndex].YL1).ToString("G5"));
                                        mts.Ftc = double.Parse((m_l[m_FrIndex].F1).ToString("f2"));
                                        m_Rtc = float.Parse((m_l[m_FrIndex].YL1).ToString("G5"));
                                        m_calSuccess = true;
                                        //MessageBox.Show(count.ToString() + ":" + m_FrIndex.ToString() + "," + tempIndex.ToString());
                                        if (count > 500)
                                        {
                                            MessageBox.Show(this, "计算失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            mts.Rtc = 0;
                                            mts.Ftc = 0;
                                            m_Rtc = 0;
                                            mts.isUseExtensometer = false;
                                            m_calSuccess = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, "计算失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        mts.Rtc = 0;
                                        mts.Ftc = 0;
                                        m_Rtc = 0;
                                        mts.isUseExtensometer = false;
                                        m_calSuccess = false;
                                    }
                                }
                                while (m_FrIndex > tempIndex + 2 || m_FrIndex < tempIndex - 2);
                            }


                            mts.testCondition = "-";// this.lblTestMethod.Text; 
                            mts.isFinish = true;
                            if (bllts.Update(mts))
                            {
                                isShowResult = true;
                                TestStandard.GBT7314_2005.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                                //TestStandard.SampleControl.ReadSample(this.tvTestSample, this.dateTimePicker);
                                this.tvTestSample.SelectedNode.ImageIndex = 1;
                                this.tvTestSample.Update();
                                this.tvTestSample.Refresh();
                                ShowResultCurve();

                                //如果不保存曲线则删除该曲线数据
                                if (!m_isSavecurve)
                                    File.Delete(curveName);

                                //默认选择全部完成的试样
                                this.zedGraphControl.GraphPane.CurveList.Clear();
                                foreach (DataGridViewRow drow in this.dataGridView.Rows)
                                {
                                    drow.Cells[0].Value = false;
                                    drow.Selected = false;
                                    dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                                }
                                if (index >= 0)
                                    this.tvTestSample.Nodes[index].ExpandAll();


                                if (m_l != null)
                                {
                                    ChangeRealTimeXYChart();
                                    //ReadResultCurve(this.pbChart, m_l);
                                    ReadResultData(m_l);
                                }
                            }
                        }
                    }
                    catch { }

                    break;
                #endregion

                #region YBT5349-2006
                case "YBT5349-2006_c":
                    if (this.tvTestSample.SelectedNode.Text.Length > 0)
                    {
                        BLL.Bend bllBend = new HR_Test.BLL.Bend();
                        Model.Bend mBend = bllBend.GetModel(this.tvTestSample.SelectedNode.Text);

                        int index = this.tvTestSample.SelectedNode.Parent.Index;

                        //保存曲线
                        string curveName = @"E:\衡新试验数据\" + "Curve\\" + m_path + "\\" + mBend.testSampleNo + ".txt";
                        if (File.Exists(curveName))
                        {
                            FileStream fs = new FileStream(curveName, FileMode.Append, FileAccess.Write);
                            foreach (gdata gd in _List_Testing_Data)//_List_Array_Data
                            {
                                utils.AddText(fs, gd.F1 + "," + gd.F2 + "," + gd.F3 + "," + gd.D1 + "," + gd.D2 + "," + gd.D3 + "," + gd.BX1 + "," + gd.BX2 + "," + gd.BX3 + "," + gd.YL1 + "," + gd.YL2 + "," + gd.YL3 + "," + gd.YB1 + "," + gd.YB2 + "," + gd.YB3 + "," + gd.Ts);
                                utils.AddText(fs, "\r\n");
                            }
                            fs.Flush();
                            fs.Close();
                            fs.Dispose();
                        }
                        Thread.Sleep(20);

                        //读取试验方法中是否选取了Rp
                        BLL.SelTestResult_B bst = new HR_Test.BLL.SelTestResult_B();
                        Model.SelTestResult_B mst = bst.GetModel(mBend.testMethod);

                        if (mst != null)
                        {
                            //弯曲试验力学性能参数选择
                            //弹性模量
                            m_isSelEb = mst.Eb;
                            //规定非比例弯曲力
                            if (mst.Fpb || mst.σpb)
                                m_isSelFpb = mst.Fpb;
                            //规定残余弯曲力
                            if (mst.Frb || mst.σrb || mst.f_rb)
                                m_isSelFrb = mst.Frb;
                            //最大弯曲力
                            m_isSelFbb = mst.Fbb;
                            //最大弯曲应力
                            m_isSelσbb = mst.σbb;

                            m_isHandaz = false;

                            m_isSavecurve = mst.saveCurve;
                        }

                        //计算数据 Fm ReL ReH
                        m_l = ReadOneListData(m_path, m_TestSampleNo);
                        if (m_l != null)
                        {
                            //计算最大力值
                            CalcData_B(m_l);
                        }
                        m_Fbb = (float)Math.Round((m_l[m_FmaxIndex].F1), 2);
                        mBend.Fbb = m_Fbb;
                        m_σbb = (float)Math.Round(m_l[m_FmaxIndex].YL1, 2);
                        mBend.σbb = m_σbb;

                        //计算弹性模量
                        //如果使用引伸计，用力-挠度 曲线求Fp02,并且试验结果选取了 Rp,并且试验过程中使用引伸计
                        //m_extenType 读取数据库确认参数值, m_isSelRp 确认是否试验结果选择了Rp,_useExten 试验界面是否使用引申计
                        if (m_extenType != 2 && m_useExten)
                        {
                            mBend.isUseExtensometer = true;
                            m_FrIndex = m_FmaxIndex;
                            int tempIndex = 0;
                            int count = 0;

                            //逐次逼近法 求取Fp02
                            do
                            {
                                tempIndex = m_FrIndex;

                                if (GetFp02IndexOnE_B(m_l, tempIndex, mBend.testType, out m_FrIndex, out m_a, out m_k, out m_fr05index, out m_fr01index, out m_ep02L0))
                                {
                                    count++;
                                    //MessageBox.Show(count.ToString() + ":" + m_FrIndex.ToString() + "," + tempIndex.ToString());                                    
                                    double pb = (m_l[m_FrIndex].YL1);
                                    mBend.σpb = utils.GetRoundStressOfBend(pb);
                                    m_Rp = float.Parse((m_l[m_FrIndex].YL1).ToString());
                                    m_calSuccess = true;
                                    if (count > 500)
                                    {
                                        MessageBox.Show(this, "计算失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        m_calSuccess = false;
                                        mBend.σpb = 0;
                                        m_Rp = 0;
                                        mBend.isUseExtensometer = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    m_calSuccess = false;
                                    mBend.σpb = 0;
                                    m_Rp = 0f;
                                    mBend.isUseExtensometer = true;
                                    MessageBox.Show(this, "计算失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            while (m_FrIndex > tempIndex + 2 || m_FrIndex < tempIndex - 2);


                            //计算弹性模量
                            float deltaLoad = m_l[m_fr05index].F1 - m_l[m_fr01index].F1;
                            float deltaBX = m_l[m_fr05index].BX1 - m_l[m_fr01index].BX1;
                            float ls3 = (float)(mBend.Ls * mBend.Ls * mBend.Ls);
                            float ls2 = (float)(mBend.Ls * mBend.Ls);
                            float l2 = (float)(mBend.l_l * mBend.l_l);

                            switch (mBend.testType)
                            {
                                case "三点弯曲":
                                    float eb1 = (float)(ls3 / (48.0f * mBend.I)) * (deltaLoad / deltaBX);
                                    m_Eb = utils.GetRoundEbOfBend(eb1);
                                    mBend.Eb = m_Eb;
                                    break;
                                case "四点弯曲":
                                    float eb2 = (float)((mBend.l_l * (3.0f * ls2 - 4 * l2)) * (deltaLoad / deltaBX) / (48.0f * mBend.I));
                                    m_Eb = utils.GetRoundEbOfBend(eb2);
                                    mBend.Eb = m_Eb;
                                    //结果修约
                                    break;
                            }

                            //重新读取_List_Data显示结果曲线
                            //if (mts.Rp > 0)
                            //{
                            //标注曲线20130515取消自动求取的Rp划线,只在试验停止后显示
                            //Graphics g = this.pbChart.CreateGraphics();
                            //DrawResult(g,m_l, m_k, m_a, m_fr05index, m_fr01index);
                            //} 

                        }
                        else if (!m_useExten) //如果没用引伸计，用力-位移曲线求Fp02
                        {
                            mBend.isUseExtensometer = false;
                            int tempIndex = 0;
                            //FrIndex初始值
                            m_FrIndex = m_FmaxIndex;
                            int count = 0;
                            //逐次逼近法 求取Fpb
                            do
                            {
                                tempIndex = m_FrIndex;
                                if (GetFp02Index_B(m_l, tempIndex, mBend.testType, out m_FrIndex, out m_a, out m_k, out m_fr05index, out m_fr01index, out m_ep02L0))
                                {
                                    count++;
                                    double pb = m_l[m_FrIndex].YL1;
                                    mBend.σpb = utils.GetRoundStressOfBend(pb);
                                    m_Rp = float.Parse(mBend.σpb.ToString());
                                    m_calSuccess = true;
                                    //MessageBox.Show(count.ToString() + ":" + m_FrIndex.ToString() + "," + tempIndex.ToString());
                                    if (count > 500)
                                    {
                                        MessageBox.Show(this, "计算失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        mBend.σpb = 0;
                                        mBend.isUseExtensometer = false;
                                        m_calSuccess = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(this, "计算失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    mBend.σpb = 0;
                                    mBend.isUseExtensometer = false;
                                    m_calSuccess = false;
                                }
                            }
                            while (m_FrIndex > tempIndex + 2 || m_FrIndex < tempIndex - 2);

                            //计算弹性模量
                            float deltaLoad = m_l[m_fr05index].F1 - m_l[m_fr01index].F1;
                            float deltaBX = m_l[m_fr05index].BX1 - m_l[m_fr01index].BX1;
                            float ls3 = (float)(mBend.Ls * mBend.Ls * mBend.Ls);
                            float ls2 = (float)(mBend.Ls * mBend.Ls);
                            float l2 = (float)(mBend.l_l * mBend.l_l);
                            switch (mBend.testType)
                            {
                                case "三点弯曲":
                                    float eb1 = (float)(ls3 / (48.0f * mBend.I)) * (deltaLoad / deltaBX);
                                    //---------------------------------------                                    
                                    m_Eb = utils.GetRoundEbOfBend(eb1);
                                    mBend.Eb = m_Eb;
                                    break;
                                case "四点弯曲":
                                    float eb2 = (float)((mBend.l_l * (3.0f * ls2 - 4 * l2)) * (deltaLoad / deltaBX) / (48.0f * mBend.I));
                                    m_Eb = utils.GetRoundEbOfBend(eb2);
                                    mBend.Eb = m_Eb;
                                    //结果修约
                                    break;
                            }
                            //if (mts.Rp > 0)
                            //{
                            //只在试验停止后显示划线和标注点
                            //Graphics g = this.pbChart.CreateGraphics();
                            //DrawResult(g,m_l, m_k, m_a, m_fr05index, m_fr01index);
                            //}
                        }

                        mBend.testCondition = "-";// this.lblTestMethod.Text; 
                        mBend.isFinish = true;

                        if (bllBend.Update(mBend))
                        {
                            isShowResult = true;
                            TestStandard.YBT5349_2006.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            //TestStandard.SampleControl.ReadSample(this.tvTestSample, this.dateTimePicker);
                            this.tvTestSample.SelectedNode.ImageIndex = 1;
                            this.tvTestSample.Update();
                            this.tvTestSample.Refresh();
                            ShowResultCurve();

                            //如果不保存曲线则删除该曲线数据
                            if (!m_isSavecurve)
                                File.Delete(curveName);

                            //默认选择全部完成的试样
                            this.zedGraphControl.GraphPane.CurveList.Clear();
                            foreach (DataGridViewRow drow in this.dataGridView.Rows)
                            {
                                drow.Cells[0].Value = false;
                                drow.Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                            }
                            if (index >= 0)
                                this.tvTestSample.Nodes[index].ExpandAll();
                        }
                    }

                    //重新读取曲线
                    if (m_l != null)
                    {
                        ChangeRealTimeXYChart();
                        ReadResultData(m_l);
                    }

                    break;
                #endregion

                #region GBT28289-2012Tensile
                case "GBT28289-2012Tensile_c":
                    if (!string.IsNullOrEmpty(m_TestSampleNo))
                    {
                        BLL.GBT282892012_Tensile bllt = new HR_Test.BLL.GBT282892012_Tensile();
                        Model.GBT282892012_Tensile mt = bllt.GetModel(m_TestSampleNo);
                        int index = this.tvTestSample.SelectedNode.Parent.Index;
                        //保存曲线
                        string curveName = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + mt.testSampleNo + ".txt";
                        if (File.Exists(curveName))
                        {
                            FileStream fs = new FileStream(curveName, FileMode.Append, FileAccess.Write);
                            foreach (gdata gd in _List_Testing_Data)//_List_Array_Data
                            {
                                utils.AddText(fs, gd.F1 + "," + gd.F2 + "," + gd.F3 + "," + gd.D1 + "," + gd.D2 + "," + gd.D3 + "," + gd.BX1 + "," + gd.BX2 + "," + gd.BX3 + "," + gd.YL1 + "," + gd.YL2 + "," + gd.YL3 + "," + gd.YB1 + "," + gd.YB2 + "," + gd.YB3 + "," + gd.Ts);
                                utils.AddText(fs, "\r\n");
                            }
                            fs.Flush();
                            fs.Close();
                            fs.Dispose();
                        }
                        Thread.Sleep(20);
                        //读取试验方法中是否选取了保存曲线
                        BLL.GBT282892012_TensileSel bllts = new HR_Test.BLL.GBT282892012_TensileSel();
                        Model.GBT282892012_TensileSel mts = bllts.GetModel(mt.testMethod);
                        if (mts != null)
                        {
                            m_isSavecurve = mts.isSaveCurve;
                        }

                        //计算数据 Fm ReL ReH
                        m_l = ReadOneListData(m_path, m_TestSampleNo);
                        if (m_l != null)
                        {
                            double FQmax = 0;
                            int FQmaxIndex = 0;
                            TestStandard.GBT28289_2012.CalcFmax(m_l, out FQmax, out FQmaxIndex);
                            m_FmaxIndex = FQmaxIndex;
                            m_Fmax = (float)Math.Round(FQmax, 2);
                            mt.FQmax = m_Fmax;
                            m_Rp = (float)Math.Round(FQmax / (double)mt.L, 2);
                            mt.Q = m_Rp;
                        }

                        mt.testCondition = "-";// this.lblTestMethod.Text; 
                        mt.isFinish = true;

                        if (bllt.Update(mt))
                        {
                            isShowResult = true;
                            //刷新试验结果列表
                            TestStandard.GBT28289_2012.Tensile.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            //刷新试样列表
                            //TestStandard.SampleControl.ReadSample(this.tvTestSample, this.dateTimePicker);
                            this.tvTestSample.SelectedNode.ImageIndex = 1;
                            this.tvTestSample.Update();
                            this.tvTestSample.Refresh();
                            ShowResultCurve();

                            //如果不保存曲线则删除该曲线数据
                            if (!m_isSavecurve)
                                File.Delete(curveName);

                            //默认选择全部完成的试样
                            this.zedGraphControl.GraphPane.CurveList.Clear();
                            foreach (DataGridViewRow drow in this.dataGridView.Rows)
                            {
                                drow.Cells[0].Value = false;
                                drow.Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                            }
                            if (index >= 0)
                                this.tvTestSample.Nodes[index].ExpandAll();
                        }
                    }
                    if (m_l != null)
                    {
                        ChangeRealTimeXYChart();
                        ReadResultData(m_l);
                    }
                    break;
                #endregion

                #region GBT28289-2012Twist
                case "GBT28289-2012Twist_c":
                    if (!string.IsNullOrEmpty(m_TestSampleNo))
                    {
                        BLL.GBT282892012_Twist blltw = new HR_Test.BLL.GBT282892012_Twist();
                        Model.GBT282892012_Twist mtw = blltw.GetModel(m_TestSampleNo);

                        int index = this.tvTestSample.SelectedNode.Parent.Index;

                        //保存曲线
                        string curveName = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + mtw.testSampleNo + ".txt";
                        if (File.Exists(curveName))
                        {
                            FileStream fs = new FileStream(curveName, FileMode.Append, FileAccess.Write);
                            foreach (gdata gd in _List_Testing_Data)//_List_Array_Data
                            {
                                utils.AddText(fs, gd.F1 + "," + gd.F2 + "," + gd.F3 + "," + gd.D1 + "," + gd.D2 + "," + gd.D3 + "," + gd.BX1 + "," + gd.BX2 + "," + gd.BX3 + "," + gd.YL1 + "," + gd.YL2 + "," + gd.YL3 + "," + gd.YB1 + "," + gd.YB2 + "," + gd.YB3 + "," + gd.Ts);
                                utils.AddText(fs, "\r\n");
                            }
                            fs.Flush();
                            fs.Close();
                            fs.Dispose();
                        }
                        Thread.Sleep(20);
                        //读取试验方法中是否选取了保存曲线
                        BLL.GBT282892012_TwistSel blltws = new HR_Test.BLL.GBT282892012_TwistSel();
                        Model.GBT282892012_TwistSel mtws = blltws.GetModel(mtw.testMethod);
                        if (mtws != null)
                        {
                            m_isSavecurve = mtws.saveCurve;
                        }

                        //计算数据 Fm ReL ReH
                        m_l = ReadOneListData(m_path, m_TestSampleNo);
                        if (m_l != null)
                        {
                            double FMmax = 0;
                            int FMmaxIndex = 0;
                            TestStandard.GBT28289_2012.CalcFmax(m_l, out FMmax, out FMmaxIndex);
                            m_FmaxIndex = FMmaxIndex;
                            m_Fmax = (float)Math.Round(FMmax, 2);
                            mtw.FMmax = m_Fmax;
                            m_Rp = (float)Math.Round((FMmax / 1000.0) * (double)mtw.L0, 2);
                            mtw.M = m_Rp;
                        }

                        mtw.testCondition = "-";// this.lblTestMethod.Text; 
                        mtw.isFinish = true;

                        if (blltw.Update(mtw))
                        {
                            isShowResult = true;
                            //刷新试验结果列表
                            TestStandard.GBT28289_2012.Twist.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            //刷新试样列表
                            //TestStandard.SampleControl.ReadSample(this.tvTestSample, this.dateTimePicker);
                            this.tvTestSample.SelectedNode.ImageIndex = 1;
                            this.tvTestSample.Update();
                            this.tvTestSample.Refresh();
                            ShowResultCurve();

                            //如果不保存曲线则删除该曲线数据
                            if (!m_isSavecurve)
                                File.Delete(curveName);

                            //默认选择全部完成的试样
                            this.zedGraphControl.GraphPane.CurveList.Clear();
                            foreach (DataGridViewRow drow in this.dataGridView.Rows)
                            {
                                drow.Cells[0].Value = false;
                                drow.Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                            }
                            if (index >= 0)
                                this.tvTestSample.Nodes[index].ExpandAll();
                        }
                    }
                    if (m_l != null)
                    {
                        ChangeRealTimeXYChart();
                        ReadResultData(m_l);
                    }
                    break;
                #endregion

                #region GBT28289-2012Shear
                case "GBT28289-2012Shear_c":
                    if (!string.IsNullOrEmpty(m_TestSampleNo))
                    {
                        BLL.GBT282892012_Shear blls = new HR_Test.BLL.GBT282892012_Shear();
                        Model.GBT282892012_Shear ms = blls.GetModel(m_TestSampleNo);

                        int index = this.tvTestSample.SelectedNode.Parent.Index;

                        //保存曲线
                        string curveName = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + ms.testSampleNo + ".txt";
                        if (File.Exists(curveName))
                        {
                            FileStream fs = new FileStream(curveName, FileMode.Append, FileAccess.Write);
                            foreach (gdata gd in _List_Testing_Data)//_List_Array_Data
                            {
                                utils.AddText(fs, gd.F1 + "," + gd.F2 + "," + gd.F3 + "," + gd.D1 + "," + gd.D2 + "," + gd.D3 + "," + gd.BX1 + "," + gd.BX2 + "," + gd.BX3 + "," + gd.YL1 + "," + gd.YL2 + "," + gd.YL3 + "," + gd.YB1 + "," + gd.YB2 + "," + gd.YB3 + "," + gd.Ts);
                                utils.AddText(fs, "\r\n");
                            }
                            fs.Flush();
                            fs.Close();
                            fs.Dispose();
                        }
                        Thread.Sleep(20);
                        //读取试验方法中是否选取了保存曲线
                        BLL.GBT282892012_ShearSel bllss = new HR_Test.BLL.GBT282892012_ShearSel();
                        Model.GBT282892012_ShearSel mss = bllss.GetModel(ms.testMethod);
                        if (mss != null)
                        {
                            m_isSavecurve = mss.saveCurve;
                        }
                        //计算数据 Fm ReL ReH
                        m_l = ReadOneListData(m_path, m_TestSampleNo);
                        if (m_l != null)
                        {
                            double FTmax = 0;
                            int FTmaxIndex = 0;
                            TestStandard.GBT28289_2012.CalcFmax(m_l, out FTmax, out FTmaxIndex);
                            m_FmaxIndex = FTmaxIndex;

                            //ReadResultCurve(this.pbChart, m_l);
                            //ReadResultData(m_l);
                            m_Fmax = (float)Math.Round(FTmax, 2);
                            ms.FTmax = m_Fmax;
                            m_Rp = (float)Math.Round(FTmax / (double)ms.L, 2);
                            ms.T = m_Rp;
                        }

                        ms.testCondition = "-";// this.lblTestMethod.Text; 
                        ms.isFinish = true;

                        if (blls.Update(ms))
                        {
                            isShowResult = true;
                            //刷新试验结果列表
                            TestStandard.GBT28289_2012.Shear.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            //刷新试样列表
                            //TestStandard.SampleControl.ReadSample(this.tvTestSample, this.dateTimePicker);
                            this.tvTestSample.SelectedNode.ImageIndex = 1;
                            this.tvTestSample.Update();
                            this.tvTestSample.Refresh();
                            ShowResultCurve();
                            //如果不保存曲线则删除该曲线数据
                            if (!m_isSavecurve)
                                File.Delete(curveName);

                            //默认选择全部完成的试样
                            this.zedGraphControl.GraphPane.CurveList.Clear();
                            foreach (DataGridViewRow drow in this.dataGridView.Rows)
                            {
                                drow.Cells[0].Value = false;
                                drow.Selected = false;
                                dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                            }
                            if (index >= 0)
                                this.tvTestSample.Nodes[index].ExpandAll();
                        }
                    }
                    if (m_l != null)
                    {
                        //重新读取原始数据划线
                        ChangeRealTimeXYChart();
                        ReadResultData(m_l);
                    }
                    break;
                #endregion

                #region GBT3354-2014
                case "GBT3354-2014_c":
                    try
                    {
                        if (!string.IsNullOrEmpty(m_TestSampleNo))
                        {
                            BLL.GBT3354_Samples bll3354 = new HR_Test.BLL.GBT3354_Samples();
                            Model.GBT3354_Samples m3354 = bll3354.GetModel(m_TestSampleNo);

                            int index = this.tvTestSample.SelectedNode.Parent.Index;

                            //保存曲线
                            string curveName = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + m3354.testSampleNo + ".txt";
                            if (File.Exists(curveName))
                            {
                                FileStream fs = new FileStream(curveName, FileMode.Append, FileAccess.Write);
                                foreach (gdata gd in _List_Testing_Data)//_List_Array_Data
                                {
                                    utils.AddText(fs, gd.F1 + "," + gd.F2 + "," + gd.F3 + "," + gd.D1 + "," + gd.D2 + "," + gd.D3 + "," + gd.BX1 + "," + gd.BX2 + "," + gd.BX3 + "," + gd.YL1 + "," + gd.YL2 + "," + gd.YL3 + "," + gd.YB1 + "," + gd.YB2 + "," + gd.YB3 + "," + gd.Ts);
                                    utils.AddText(fs, "\r\n");
                                }
                                fs.Flush();
                                fs.Close();
                                fs.Dispose();
                            }

                            //读取试验方法中是否选取了Rp
                            BLL.GBT3354_Sel b3354sel = new HR_Test.BLL.GBT3354_Sel();
                            Model.GBT3354_Sel m3354sel = b3354sel.GetModel(m3354.testMethodName);

                            if (m3354sel == null)
                                return;

                            //计算数据 Fm ReL ReH
                            m_l = ReadOneListData(m_path, m_TestSampleNo);

                            //写入最大值 Fm Rm A Z
                            //MessageBox.Show("m_FRH=" + m_FRH.ToString() + ",m_Fm=" + m_Fm.ToString() + ",m_FRLFirst=" + m_FRLFirst.ToString() + ",m_FRL=" + m_FRL.ToString());
                            if (m_l.Count > m_FmIndex)
                            {
                                m3354.Pmax = Convert.ToDouble((m_l[m_l.Count - 2].F1).ToString("f2"));
                                m3354.σt = Convert.ToDouble(m_l[m_l.Count - 2].YL1.ToString("f2"));
                            }
                            if (m3354sel.ε1t)
                                m3354.ε1t = Convert.ToDouble(m_l[m_l.Count - 2].YB1.ToString("f2"));


                            //如果使用引伸计，用力-变形曲线求Fp02,并且试验结果选取了Rp,并且试验过程中使用引伸计
                            //m_extenType 读取数据库确认参数值, m_isSelRp 确认是否试验结果选择了Rp,_useExten 试验界面是否使用引申计


                            m3354.testCondition = "-";// this.lblTestMethod.Text; 
                            //Test
                            m3354.isFinish = true;

                            if (bll3354.Update(m3354))
                            {
                                isShowResult = true;
                                TestStandard.GBT3354_2014.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                                //无刷新左侧树形节点，直接修改imageindex;
                                //TestStandard.SampleControl.ReadSample(this.tvTestSample, this.dateTimePicker);
                                this.tvTestSample.SelectedNode.ImageIndex = 1;
                                this.tvTestSample.Update();
                                this.tvTestSample.Refresh();
                                ShowResultCurve();

                                //如果不保存曲线则删除该曲线数据
                                if (!m3354sel.saveCurve)
                                    File.Delete(curveName);

                                //默认选择全部完成的试样
                                this.zedGraphControl.GraphPane.CurveList.Clear();
                                foreach (DataGridViewRow drow in this.dataGridView.Rows)
                                {
                                    drow.Cells[0].Value = false;
                                    drow.Selected = false;
                                    dataGridView_CellClick(this.dataGridView, new DataGridViewCellEventArgs(0, drow.Index));
                                }
                                if (index >= 0)
                                    this.tvTestSample.Nodes[index].ExpandAll();
                            }
                        }

                        if (m_l != null)
                        {
                            ChangeRealTimeXYChart();
                            //ReadResultCurve(this.pbChart, m_l);
                            ReadResultData(m_l);
                        }
                    }
                    catch (Exception ee)
                    {
                        //MessageBox.Show(ee.ToString()); 
                        return;
                    }

                    //查找该试验组未做完试验的试样编号
                    //查找第一个未做完的试样编号
                    //if (m_lstNoTestSamples.Count > 0)
                    //{
                    //    m_lstNoTestSamples.Remove(m_TestSampleNo);
                    //    if (m_lstNoTestSamples.Count == 0)
                    //    {
                    //        this.e_Node = null;
                    //        MessageBox.Show("已完成该组试验!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        return;
                    //    }

                    //    TreeNode tn = new TreeNode(m_lstNoTestSamples[0]);
                    //    tn.Text = m_lstNoTestSamples[0];
                    //    tn.Name = "tensile_c";
                    //    tn.ImageIndex = 2;
                    //    tn.SelectedImageIndex = 3;
                    //    e_Node = new TreeNodeMouseClickEventArgs(tn, MouseButtons.Left, 1, 25, 2);
                    //    //查找当前试样的编号以便选中
                    //    TreeNode tf = null;
                    //    foreach (TreeNode t in this.tvTestSample.Nodes)
                    //    {
                    //        tf = FindNodeByValue(t, m_lstNoTestSamples[0]);
                    //        if (tf != null)
                    //            break;
                    //    }

                    //    if (tf != null)
                    //    {
                    //        tvTestSample.SelectedNode = tf;
                    //        if (tf.Parent != null)
                    //            tf.Parent.Expand();
                    //    }
                    //    //如果选择了批量试验
                    //    if (chkTestAll.Checked)
                    //    {
                    //        tvTestSample_NodeMouseClick(this.tvTestSample, e_Node);
                    //    }
                    //}          

                    break;
                #endregion

            }
        }

        void az_LocationChanged(object sender, EventArgs e)
        {
            this.pbChart.Invalidate();
        }


        //标注曲线
        private void DrawResult(Graphics g, List<gdata> _l, float k, float a, int index05, int index01)
        {
            Pen rpen = new Pen(Brushes.Navy);
            //在结果曲线上标注
            switch (_Y1)
            {
                case 1:
                    switch (_X1)
                    {
                        case 2://负荷-位移
                            if (m_extenType == 2 || !m_isSelRp || !m_useExten)
                            {
                                //Fp02直线
                                float xd_1 = _l[m_FrIndex].D1 - _l[m_FrIndex].F1 / k;
                                //this.chart1.Series["Rp02"].Points.AddXY(_l[m_FrIndex].D1, _l[m_FrIndex].F1);
                                float xd_2 = _l[m_FrIndex].D1 + (_l[m_FmIndex].F1 + 100 - _l[m_FrIndex].F1) / k;
                                float yd_2 = _l[m_FmIndex].F1 + 100;
                                g.DrawLine(rpen, new Point(c_TranslateX1(xd_1), c_TranslateY1(0)), new Point(c_TranslateX1(xd_2), c_TranslateY1(yd_2)));
                                //0501直线
                                float xd1_0501 = a;
                                float yd1_0501 = 0;
                                float yd2_0501 = _l[m_FmIndex].F1 + 100;
                                float xd2_0501 = (yd2_0501 / k) + a;
                                g.DrawLine(rpen, new Point(c_TranslateX1(xd1_0501), c_TranslateY1(yd1_0501)), new Point(c_TranslateX1(xd2_0501), c_TranslateY1(yd2_0501)));

                                //最大值的点
                                Point p_fm = new Point(c_TranslateX1(_l[m_FmIndex].D1), c_TranslateY1(_l[m_FmIndex].F1));
                                //添加提示框
                                string s1 = "Fm:" + (_l[m_FmIndex].F1 / 1000.0).ToString("f2") + " kN\r\nRm:" + _l[m_FmIndex].YL1.ToString("f2") + " MPa";
                                g.DrawString(s1, GraphFont, Brushes.Navy, p_fm.X, p_fm.Y - 2 * GraphFont.Height);
                                g.DrawEllipse(rpen, p_fm.X - 4, p_fm.Y - 4, 8, 8);
                                g.FillEllipse(Brushes.Navy, p_fm.X - 4, p_fm.Y - 4, 8, 8);

                                //Fp02的点
                                Point p_fp02 = new Point(c_TranslateX1(_l[m_FrIndex].D1), c_TranslateY1(_l[m_FrIndex].F1));
                                //添加提示框
                                string s2 = "Fp:" + (_l[m_FrIndex].F1 / 1000.0).ToString("f2") + " kN\r\nRp:" + _l[m_FrIndex].YL1.ToString("f2") + " MPa";
                                g.DrawString(s2, GraphFont, Brushes.Navy, p_fp02);
                                g.DrawEllipse(rpen, p_fp02.X - 4, p_fp02.Y - 4, 8, 8);
                                g.FillEllipse(Brushes.Navy, p_fp02.X - 4, p_fp02.Y - 4, 8, 8);
                            }
                            break;
                        case 5://Y1-X1 负荷-变形
                            //Fp02直线 取消变形求Rp划线
                            if (m_useExten)
                            {
                                float x2_1 = _l[m_FrIndex].BX1 - _l[m_FrIndex].F1 / k;
                                float x2_2 = _l[m_FrIndex].BX1 + (_l[m_FmIndex].F1 + 100 - _l[m_FrIndex].F1) / k;
                                float y2_2 = _l[m_FmIndex].F1 + 100;
                                //this.chart1.Series["Rp02"].Points.AddXY(x2_2, y2_2);
                                Point p_bx1 = new Point(c_TranslateX1(x2_1), c_TranslateY1(0));
                                Point p_bx2 = new Point(c_TranslateX1(x2_2), c_TranslateY1(y2_2));
                                g.DrawLine(rpen, p_bx1, p_bx2);

                                //0501直线
                                float x3_0501 = a;
                                float y3_0501 = 0;
                                float y4_0501 = _l[m_FmIndex].F1 + 100;
                                float x4_0501 = (y4_0501 / k) + a;
                                g.DrawLine(rpen, new Point(c_TranslateX1(x3_0501), c_TranslateY1(y3_0501)), new Point(c_TranslateX1(x4_0501), c_TranslateY1(y4_0501)));

                                //最大值的点
                                Point p_fm = new Point(c_TranslateX1(_l[m_FmIndex].BX1), c_TranslateY1(_l[m_FmIndex].F1));

                                //添加提示框
                                string s1 = "Fm:" + (_l[m_FmIndex].F1 / 1000.0).ToString("f2") + " kN\r\nRm:" + _l[m_FmIndex].YL1.ToString("f2") + " MPa";
                                //this.a_fm.Visible = true;
                                g.DrawString(s1, GraphFont, Brushes.Navy, p_fm.X, p_fm.Y - 2 * GraphFont.Height);
                                g.DrawEllipse(rpen, p_fm.X - 4, p_fm.Y - 4, 8, 8);
                                g.FillEllipse(Brushes.Navy, p_fm.X - 4, p_fm.Y - 4, 8, 8);

                                //Fp02的点                              
                                //添加提示框
                                Point p_fp02 = new Point(c_TranslateX1(_l[m_FrIndex].BX1), c_TranslateY1(_l[m_FrIndex].F1));
                                string s2 = "Fp:" + (_l[m_FrIndex].F1 / 1000.0).ToString("f2") + " kN\r\nRp:" + _l[m_FrIndex].YL1.ToString("f2") + " MPa";
                                g.DrawString(s2, GraphFont, Brushes.Navy, p_fp02);
                                g.DrawEllipse(rpen, p_fp02.X - 4, p_fp02.Y - 4, 8, 8);
                                g.FillEllipse(Brushes.Navy, p_fp02.X - 4, p_fp02.Y - 4, 8, 8);
                            }
                            break;
                    }
                    break;
            }
        }


        //逐步逼近法求Rp02
        private bool GetFp02Index(List<gdata> List_Data, int _FRInIndex, out int _FROutIndex, out float _a, out float _k, out int Fr05Index, out int Fr01Index, out float ep02L0)
        {
            if (List_Data.Count == 0)
            {
                _FROutIndex = 0;
                _a = 0;
                _k = 0;
                Fr05Index = 0;
                Fr01Index = 0;
                ep02L0 = 0;
                return false;
            }
            float Fr = List_Data[_FRInIndex].F1;
            int lCount = List_Data.Count;
            //查找Fr 0.5的点
            //求出Fr05和 Fr01点
            _FROutIndex = 0;
            _a = 0;
            _k = 0;
            Fr05Index = 0;
            Fr01Index = 0;
            ep02L0 = m_L0 * 10 * m_Ep;
            if (ep02L0 == 0)
                ep02L0 = m_L0 * 2f;

            //for (int m = 0; m < _FRInIndex; m++)
            //{
            //    if (List_Data[m].F1 >= Fr * 0.6)
            //    {
            //        m_FR05 = List_Data[m].F1;
            //        m_LR05 = List_Data[m].D1;
            //        Fr05Index = m;
            //        break;
            //    }
            //}
            Fr05Index = List_Data.IndexOf(List_Data.Find(p => p.F1 >= Fr * 0.6));
            m_FR05 = List_Data[Fr05Index].F1;
            m_LR05 = List_Data[Fr05Index].D1;
            //for (int n = 0; n < _FRInIndex; n++)
            //{
            //    if (List_Data[n].F1 >= Fr * 0.32)
            //    {
            //        m_FR01 = List_Data[n].F1;
            //        m_LR01 = List_Data[n].D1;
            //        Fr01Index = n;
            //        break;
            //    }
            //}
            Fr01Index = List_Data.IndexOf(List_Data.Find(p => p.F1 >= Fr * 0.32));
            m_FR01 = List_Data[Fr01Index].F1;
            m_LR01 = List_Data[Fr01Index].D1;
            //计算斜率,在 0.5 和 0.1之间取10点

            //int[] kdot = Get0501k(Fr01Index, Fr05Index);
            //double sumk = 0;
            //for (int i = 0; i < kdot.Length - 1; i++)
            //{
            //    double kone = (List_Data[kdot[i + 1]].F1 - List_Data[kdot[i]].F1) / (List_Data[kdot[i + 1]].D1 - List_Data[kdot[i]].D1);
            //    sumk += kone;
            //}
            //_k = sumk / (kdot.Length - 1);

            _k = (List_Data[Fr05Index].F1 - List_Data[Fr01Index].F1) / (List_Data[Fr05Index].D1 - List_Data[Fr01Index].D1);

            //计算偏移量
            _a = m_LR05 - (m_FR05 / _k);

            //计算出的Li值，注：100为 L0的 0。2%,此处假设为 50mm * 1000 * 0.2%
            //double Li = a + Fr / k + 100;
            //for (int i = 0; i < lCount; i++)
            //{
            //    double Lii = _a + ep02L0 + List_Data[i].F1 / _k;
            //    if (Lii <= List_Data[i].D1)
            //    {
            //        _FROutIndex = i;
            //        break;
            //    }
            //}
            double a = _a;
            double ep02 = ep02L0;
            double k = _k;

            _FROutIndex = List_Data.IndexOf(List_Data.Find(p => p.D1 >= a + ep02 + p.F1 / k));

            if (_FRInIndex != 0 && _a != 0 && _k != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool GetFp02Index_7314(List<gdata> List_Data, int _FRInIndex, float _EpcorEtc, out int _FROutIndex, out float _a, out float _k, out int Fr05Index, out int Fr01Index, out float ep02L0)
        {
            if (List_Data.Count == 0)
            {
                _FROutIndex = 0;
                _a = 0;
                _k = 0;
                Fr05Index = 0;
                Fr01Index = 0;
                ep02L0 = 0;
                return false;
            }

            float Fr = List_Data[_FRInIndex].F1;
            int lCount = List_Data.Count;
            //查找Fr 0.5的点
            //求出Fr05和 Fr01点
            _FROutIndex = 0;
            _a = 0;
            _k = 0;
            Fr05Index = 0;
            Fr01Index = 0;
            ep02L0 = m_L0 * 10 * _EpcorEtc;
            if (ep02L0 == 0)
                return false;
            for (int m = 0; m < _FRInIndex; m++)
            {
                if (List_Data[m].F1 >= Fr * 0.75)
                {
                    m_FR05 = List_Data[m].F1;
                    m_LR05 = List_Data[m].D1;
                    Fr05Index = m;
                    break;
                }
            }

            for (int n = 0; n < _FRInIndex; n++)
            {
                if (List_Data[n].F1 >= Fr * 0.45)
                {
                    m_FR01 = List_Data[n].F1;
                    m_LR01 = List_Data[n].D1;
                    Fr01Index = n;
                    break;
                }
            }

            //计算斜率,在 0.5 和 0.1之间取10点

            //int[] kdot = Get0501k(Fr01Index, Fr05Index);
            //double sumk = 0;
            //for (int i = 0; i < kdot.Length - 1; i++)
            //{
            //    double kone = (List_Data[kdot[i + 1]].F1 - List_Data[kdot[i]].F1) / (List_Data[kdot[i + 1]].D1 - List_Data[kdot[i]].D1);
            //    sumk += kone;
            //}
            //_k = sumk / (kdot.Length - 1);

            _k = (List_Data[Fr05Index].F1 - List_Data[Fr01Index].F1) / (List_Data[Fr05Index].D1 - List_Data[Fr01Index].D1);

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="List_Data"></param>
        /// <param name="_FRInIndex"></param>
        /// <param name="_FROutIndex"></param>
        /// <param name="_a"></param>
        /// <param name="_k"></param>
        /// <param name="Fr05Index"></param>
        /// <param name="Fr01Index"></param>
        /// <param name="ep02L0"></param>
        /// <returns></returns>
        private bool GetFp02Index_B(List<gdata> List_Data, int _FRInIndex, string _testType, out int _FROutIndex, out float _a, out float _k, out int Fr05Index, out int Fr01Index, out float ep02L0)
        {
            if (List_Data.Count == 0)
            {
                _FROutIndex = 0;
                _a = 0;
                _k = 0;
                Fr05Index = 0;
                Fr01Index = 0;
                ep02L0 = 0;
                return false;
            }

            float Fr = List_Data[_FRInIndex].F1;
            int lCount = List_Data.Count;
            //查找Fr 0.5的点
            //求出Fr05和 Fr01点
            _FROutIndex = 0;
            _a = 0;
            _k = 0;
            Fr05Index = 0;
            Fr01Index = 0;
            ep02L0 = 0;
            switch (_testType)
            {
                case "三点弯曲":
                    ep02L0 = m_n * m_L0 * m_L0 * m_Ep / (12 * m_Y);
                    break;
                case "四点弯曲":
                    ep02L0 = m_n * (3 * m_L0 * m_L0 - 4 * m_Lc * m_Lc) / (24 * m_Y);
                    break;
            }

            if (ep02L0 == 0)
                return false;

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
                if (List_Data[n].F1 >= Fr * 0.1)
                {
                    m_FR01 = List_Data[n].F1;
                    m_LR01 = List_Data[n].D1;
                    Fr01Index = n;
                    break;
                }
            }

            //计算斜率,在 0.5 和 0.1之间取10点

            //int[] kdot = Get0501k(Fr01Index, Fr05Index);
            //double sumk = 0;
            //for (int i = 0; i < kdot.Length - 1; i++)
            //{
            //    double kone = (List_Data[kdot[i + 1]].F1 - List_Data[kdot[i]].F1) / (List_Data[kdot[i + 1]].D1 - List_Data[kdot[i]].D1);
            //    sumk += kone;
            //}
            //_k = sumk / (kdot.Length - 1);

            _k = (List_Data[Fr05Index].F1 - List_Data[Fr01Index].F1) / (List_Data[Fr05Index].D1 - List_Data[Fr01Index].D1);

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


        //查找弯曲试验 弹性段的两个点 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="List_Data">曲线数据</param>
        /// <param name="_FRInIndex">输入最大值的序号</param>
        /// <param name="_testType">试验类型:三点弯曲 四点弯曲</param>
        /// <param name="_sampleType">试样类型:圆柱形 矩形</param>
        /// <param name="_FROutIndex">输出Epb的序号</param>
        /// <param name="_a">偏移量</param>
        /// <param name="_k">斜率</param>
        /// <param name="Fr05Index">弹性段两个点</param>
        /// <param name="Fr01Index">弹性段两个点</param>
        /// <param name="ep02L0"></param>
        /// <returns></returns>
        private bool GetFp02IndexOnE_B(List<gdata> List_Data, int _FRInIndex, string _testType, out int _FROutIndex, out float _a, out float _k, out int Fr05Index, out int Fr01Index, out float ep02L0)
        {
            if (List_Data.Count == 0)
            {
                _FROutIndex = 0;
                _a = 0;
                _k = 0;
                Fr05Index = 0;
                Fr01Index = 0;
                ep02L0 = 0;
                return false;
            }
            float Fr = List_Data[_FRInIndex].F1;
            //查找Fr 0.5的点
            //求出Fr05和 Fr01点
            _FROutIndex = 0;
            _a = 0;
            _k = 0;
            Fr05Index = 0;
            Fr01Index = 0;
            //计算OC长度
            //公式 
            //3点弯曲 n*Ls*Ls*m_Ep /(12*m_Y)
            ep02L0 = 0;

            switch (_testType)
            {
                case "三点弯曲":
                    ep02L0 = m_n * m_L0 * m_L0 * m_Ep / (12 * m_Y);
                    break;
                case "四点弯曲":
                    ep02L0 = m_n * (3 * m_L0 * m_L0 - 4 * m_Lc * m_Lc) / (24 * m_Y);
                    break;
            }

            if (ep02L0 == 0)
                return false;

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
                if (List_Data[n].F1 >= Fr * 0.1)
                {
                    m_FR01 = List_Data[n].F1;
                    m_LR01 = List_Data[n].BX1;
                    Fr01Index = n;
                    break;
                }
            }

            //计算斜率,在 0.5 和 0.1之间取10点
            //int[] kdot = Get0501k(Fr01Index, Fr05Index);
            //double sumk = 0;
            //for (int i = 0; i < kdot.Length - 1; i++)
            //{
            //    double kone = (List_Data[kdot[i + 1]].F1 - List_Data[kdot[i]].F1) / (List_Data[kdot[i + 1]].BX1 - List_Data[kdot[i]].BX1);
            //    sumk += kone;
            //}
            //_k = sumk / (kdot.Length - 1);
            _k = (List_Data[Fr05Index].F1 - List_Data[Fr01Index].F1) / (List_Data[Fr05Index].BX1 - List_Data[Fr01Index].BX1);
            //计算偏移量
            _a = m_LR05 - (m_FR05 / _k);
            //计算出的Li值，注：100为 L0的 0。2%,此处假设为 50mm * 1000 * 0.2%
            //double Li = a + Fr / k + 100; 
            int lCount = List_Data.Count;
            for (int i = 0; i < lCount; i++)
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

        private bool GetFp02IndexOnE(List<gdata> List_Data, int _FRInIndex, out int _FROutIndex, out float _a, out float _k, out int Fr05Index, out int Fr01Index, out float ep02L0)
        {
            if (List_Data.Count == 0)
            {
                _FROutIndex = 0;
                _a = 0;
                _k = 0;
                Fr05Index = 0;
                Fr01Index = 0;
                ep02L0 = 0;
                return false;
            }
            float Fr = List_Data[_FRInIndex].F1;
            //查找Fr 0.5的点
            //求出Fr05和 Fr01点
            _FROutIndex = 0;
            _a = 0;
            _k = 0;
            Fr05Index = 0;
            Fr01Index = 0;
            ep02L0 = m_L0 * 10 * m_Ep;
            if (ep02L0 == 0)
                ep02L0 = m_L0 * 10f * 0.2f;
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
                if (List_Data[n].F1 >= Fr * 0.32)
                {
                    m_FR01 = List_Data[n].F1;
                    m_LR01 = List_Data[n].BX1;
                    Fr01Index = n;
                    break;
                }
            }

            //计算斜率,在 0.5 和 0.1之间取10点
            //int[] kdot = Get0501k(Fr01Index, Fr05Index);
            //double sumk = 0;
            //for (int i = 0; i < kdot.Length - 1; i++)
            //{
            //    double kone = (List_Data[kdot[i + 1]].F1 - List_Data[kdot[i]].F1) / (List_Data[kdot[i + 1]].BX1 - List_Data[kdot[i]].BX1);
            //    sumk += kone;
            //}
            //_k = sumk / (kdot.Length - 1);
            _k = (List_Data[Fr05Index].F1 - List_Data[Fr01Index].F1) / (List_Data[Fr05Index].BX1 - List_Data[Fr01Index].BX1);
            //计算偏移量
            _a = m_LR05 - (m_FR05 / _k);
            //double Li = a + Fr / k + 100; 
            int lCount = List_Data.Count;
            for (int i = 0; i < lCount; i++)
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

        private bool GetFp02IndexOnE_7314(List<gdata> List_Data, int _FRInIndex, float _EpcOrEtc, out int _FROutIndex, out float _a, out float _k, out int Fr05Index, out int Fr01Index, out float ep02L0)
        {
            if (List_Data.Count == 0)
            {
                _FROutIndex = 0;
                _a = 0;
                _k = 0;
                Fr05Index = 0;
                Fr01Index = 0;
                ep02L0 = 0;
                return false;
            }

            float Fr = List_Data[_FRInIndex].F1;
            //查找Fr 0.5的点
            //求出Fr05和 Fr01点
            _FROutIndex = 0;
            _a = 0;
            _k = 0;
            Fr05Index = 0;
            Fr01Index = 0;
            ep02L0 = m_L0 * 10 * _EpcOrEtc * m_n;
            if (ep02L0 == 0)
                return false;
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
                if (List_Data[n].F1 >= Fr * 0.3)
                {
                    m_FR01 = List_Data[n].F1;
                    m_LR01 = List_Data[n].BX1;
                    Fr01Index = n;
                    break;
                }
            }

            //计算斜率,在 0.5 和 0.1之间取10点
            //int[] kdot = Get0501k(Fr01Index, Fr05Index);
            //double sumk = 0;
            //for (int i = 0; i < kdot.Length - 1; i++)
            //{
            //    double kone = (List_Data[kdot[i + 1]].F1 - List_Data[kdot[i]].F1) / (List_Data[kdot[i + 1]].BX1 - List_Data[kdot[i]].BX1);
            //    sumk += kone;
            //}
            //_k = sumk / (kdot.Length - 1);
            _k = (List_Data[Fr05Index].F1 - List_Data[Fr01Index].F1) / (List_Data[Fr05Index].BX1 - List_Data[Fr01Index].BX1);
            //计算偏移量
            _a = m_LR05 - (m_FR05 / _k);
            //double Li = a + Fr / k + 100; 
            int lCount = List_Data.Count;
            for (int i = 0; i < lCount; i++)
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

        private void SendStopTest()
        {
            lock (m_state)
            {
                Thread.Sleep(30);
                byte[] buf = new byte[5];
                int ret;
                buf[0] = 0x04;									//命令字节
                buf[1] = 0xf2;									//试验停止命令
                buf[2] = 0;
                buf[3] = 0;
                buf[4] = 0;
                ret = RwUsb.WriteData1582(1, buf, 5, 1000);				//发送写命令
                Thread.Sleep(30);
            }
        }

        public void tsbtn_Pause_Click(object sender, EventArgs e)
        {
            SendPauseTest();
        }

        public void SendPauseTest()
        {
            m_pause = !m_pause;
            lock (m_state)
            {
                Thread.Sleep(10);
                byte[] buf = new byte[5];
                int ret;
                buf[0] = 0x04;									//命令字节
                buf[1] = 0xf3;							        //试验保持命令
                buf[2] = 0;
                buf[3] = 0;
                buf[4] = 0;
                m_hold_data.BX1 = m_Elongate;
                m_hold_data.D1 = m_Displacement;
                m_hold_data.F1 = m_Load;
                ret = RwUsb.WriteData1582(1, buf, 5, 1000);	    //发送写命令
                Thread.Sleep(10);
            }
        }

        private void btnChangeYL_Click_1(object sender, EventArgs e)
        {
            _showYL = !_showYL;
            if (_showYL)
            {
                this.lblYLShow.Visible = true;
                this.lblFShow.Visible = false;
                this.lblFuhe.Text = "应力";
                this.btnChangeYL.Text = "负荷";
                this.lblkN.Visible = false;
                this.lblkN.Refresh();
                this.lblMPa.Visible = true;
                this.lblMPa.BringToFront();
                this.lblMPa.Refresh();
                this.lblFuhe.Refresh();
                this.btnChangeYL.Refresh();
            }
            else
            {
                this.lblYLShow.Visible = false;
                this.lblFShow.Visible = true;
                this.lblFuhe.Text = "负荷";
                this.btnChangeYL.Text = "应力";
                this.lblkN.Visible = true;
                this.lblMPa.Visible = false;
                this.lblkN.BringToFront();
                this.lblkN.Refresh();
                this.lblFuhe.Refresh();
                this.btnChangeYL.Refresh();
            }
        }

        private void btnYb_Click(object sender, EventArgs e)
        {
            _showYB = !_showYB;
            if (_showYB)
            {
                this.lblYBShow.Visible = true;
                lblmm3.Visible = true;
                this.lblBXShow.Visible = false;
                lblmm2.Visible = false;
                this.lblBx.Text = "应变";
                this.btnYb.Text = "变形";
                this.lblBx.Refresh();
                this.btnYb.Refresh();
                this.lblYBShow.Refresh();
            }
            else
            {
                this.lblYBShow.Visible = false;
                lblmm3.Visible = false;
                this.lblBXShow.Visible = true;
                lblmm2.Visible = true;
                this.lblBx.Text = "变形";
                this.btnYb.Text = "应变";
                this.lblBXShow.Refresh();
                this.lblBx.Refresh();
                this.btnYb.Refresh();
            }
        }

        //改变显示结果曲线的数据轴
        private void ShowResultCurve()
        {
            ReadCurveSet();
            _ResultPanel.XAxis.Scale.Max = 1;
            _ResultPanel.YAxis.Scale.Max = 1;
            switch (_ShowY)
            {
                case 0:
                    _ResultPanel.YAxis.Title.Text = "Y1";
                    break;
                case 1:
                    _ResultPanel.YAxis.Title.Text = "负荷,N";
                    break;
                case 2:
                    _ResultPanel.YAxis.Title.Text = "应力,MPa";
                    break;
                case 3:
                    _ResultPanel.YAxis.Title.Text = "变形,μm";
                    break;
                case 4:
                    _ResultPanel.YAxis.Title.Text = "位移,μm";
                    break;
            }

            switch (_ShowX)
            {
                case 0:
                    _ResultPanel.XAxis.Title.Text = "X1";
                    break;
                case 1:
                    _ResultPanel.XAxis.Title.Text = "时间,s";

                    break;
                case 2:
                    _ResultPanel.XAxis.Title.Text = "位移,μm";
                    break;
                case 3:
                    _ResultPanel.XAxis.Title.Text = "应变,%";
                    break;
                case 4:
                    _ResultPanel.XAxis.Title.Text = "变形,μm";
                    break;
                case 5:
                    _ResultPanel.XAxis.Title.Text = "应力,MPa";
                    break;
                default:
                    _ResultPanel.XAxis.Title.Text = "X1";
                    break;
            }
            if (dataGridView.Tag == null)
                return;
            // RWconfig.SetAppSettings("ShowX", this.cmbXr.SelectedIndex.ToString());
            switch (dataGridView.Tag.ToString())
            {
                case "GBT228-2010":
                    this.m_path = "GBT228-2010";
                    break;
                case "GBT7314-2005":

                    this.m_path = "GBT7314-2005";
                    break;
                case "YBT5349-2006":

                    this.m_path = "YBT5349-2006";
                    break;
                case "GBT28289-2012Tensile":
                    this.m_path = "GBT28289-2012Tensile";
                    break;
                case "GBT28289-2012Shear":
                    this.m_path = "GBT28289-2012Shear";
                    break;
                case "GBT28289-2012Twist":
                    this.m_path = "GBT28289-2012Twist";
                    break;
            }

            //先清空曲线
            this._ResultPanel.CurveList.Clear();
            //MessageBox.Show(this._ResultPanel.CurveList.Count.ToString());
            //调用
            int rCount = dataGridView.Rows.Count;
            for (int i = 0; i < rCount; i++)
            {
                if (dataGridView.Rows[i].Cells[0].Value != null)
                {
                    if (Convert.ToBoolean(dataGridView.Rows[i].Cells[0].Value.ToString()) == true)
                    {
                        //_RPPList_ReadOne.Clear(); 
                        LineItem li = _ResultPanel.AddCurve(dataGridView.Rows[i].Cells[3].Value.ToString(), _RPPList_ReadOne, Color.FromName(dataGridView.Rows[i].Cells[1].Value.ToString()), SymbolType.None);//Y1-X1 
                        li.Line.IsAntiAlias = true;
                        this.m_SelectTestSampleNo = dataGridView.Rows[i].Cells[3].Value.ToString();
                        if (_threadReadCurve == null)
                        {
                            _threadReadCurve = new Thread(new ThreadStart(ReadCurveData));
                            _threadReadCurve.IsBackground = true;
                            _threadReadCurve.Start();
                            _threadReadCurve = null;
                        }
                        //_RPPList_ReadOne.Clear();
                        //readCurveName(this.zedGraphControl,dataGridView.Rows[i].Cells[2].Value.ToString(), "Tensile");
                    }
                }
            }
        }

        private List<SelTestSample> GetSelSample()
        {
            List<SelTestSample> lstSel = new List<SelTestSample>();
            string selTestNo = string.Empty;
            int rCount = this.dataGridView.Rows.Count;
            for (int i = 0; i < rCount; i++)
            {
                if (Convert.ToBoolean(this.dataGridView.Rows[i].Cells[0].Value) == true)
                {
                    SelTestSample sl = new SelTestSample();
                    sl._SelTestSample = this.dataGridView.Rows[i].Cells[3].Value.ToString();
                    sl._RowIndex = this.dataGridView.Rows[i].Index;
                    lstSel.Add(sl);
                }
            }
            return lstSel;
        }

        private UC.Result CreateResultCtrl(string fieldName, string title, string value, object tag)
        {
            UC.Result ucResult = new HR_Test.UC.Result();
            ucResult._FieldName = title + ":";
            ucResult.Name = fieldName;
            ucResult.txtFiledContent.Text = value;
            ucResult.Tag = tag;
            return ucResult;
        }


        private void tsbtn_Curve_Click(object sender, EventArgs e)
        {
            _selTestSampleArray = GetSelSample();
            if (_selTestSampleArray.Count > 0)
            {
                switch (dataGridView.Tag.ToString())
                {
                    case "GBT23615-2009TensileZong":
                        if (_selTestSampleArray[_selTestSampleArray.Count - 1] != null)
                        {
                            AnalysiseCurve.GBT23615TensileZong fac = new HR_Test.AnalysiseCurve.GBT23615TensileZong();
                            fac.M_SR = this.m_LoadResolutionValue;
                            fac.WindowState = FormWindowState.Maximized;
                            fac.tslblSampleNo.Text = _selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample;
                            fac._TestSampleNo = _selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample;
                            fac._CurveName = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + _selTestSampleArray[_selTestSampleArray.Count - 1] + ".txt";
                            fac._TestType = "GBT23615-2009TensileZong";
                            fac._LineColor = "Brown";// this.dataGridView.Rows[dataGridView.SelectedRows.Count
                            //读取试样结果  
                            BLL.GBT236152009_TensileZong bllts = new HR_Test.BLL.GBT236152009_TensileZong();
                            Model.GBT236152009_TensileZong modelTs = bllts.GetModel(_selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample);
                            DataSet dsTestSample = bllts.GetResult(" testSampleNo='" + _selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample + "'");
                            //读取选择试验结果表
                            BLL.GBT236152009_SelZong bllSz = new HR_Test.BLL.GBT236152009_SelZong();
                            DataSet dsSt = bllSz.GetList(" methodName='" + modelTs.testMethodName + "'");

                            if (dsSt != null)
                            {
                                if (dsSt.Tables[0].Rows.Count > 0)
                                {
                                    //是否选择 
                                    if (Convert.ToBoolean(dsSt.Tables[0].Rows[0]["Fmax"].ToString()) == true)
                                    {
                                        UC.Result ucResult = new HR_Test.UC.Result();
                                        ucResult._FieldName = "Fmax(kN):";
                                        ucResult.Name = "Fmax";
                                        ucResult.txtFiledContent.Text = dsTestSample.Tables[0].Rows[0]["Fmax(kN)"].ToString();
                                        fac.flowLayoutPanel1.Controls.Add(ucResult);
                                    }
                                    if (Convert.ToBoolean(dsSt.Tables[0].Rows[0]["T2"].ToString()) == true)
                                    {
                                        UC.Result ucResult = new HR_Test.UC.Result();
                                        ucResult._FieldName = "T2(MPa):";
                                        ucResult.Name = "T2";
                                        ucResult.txtFiledContent.Text = dsTestSample.Tables[0].Rows[0]["T2(MPa)"].ToString();
                                        fac.flowLayoutPanel1.Controls.Add(ucResult);
                                    }
                                    if (Convert.ToBoolean(dsSt.Tables[0].Rows[0]["E"].ToString()) == true)
                                    {
                                        UC.Result ucResult = new HR_Test.UC.Result();
                                        ucResult._FieldName = "E(MPa):";
                                        ucResult.Name = "E";
                                        ucResult.txtFiledContent.Text = dsTestSample.Tables[0].Rows[0]["E(MPa)"].ToString();
                                        fac.flowLayoutPanel1.Controls.Add(ucResult);
                                    }
                                    if (Convert.ToBoolean(dsSt.Tables[0].Rows[0]["Z"].ToString()) == true)
                                    {
                                        UC.Result ucResult = new HR_Test.UC.Result();
                                        ucResult._FieldName = "A(%):";
                                        ucResult.Name = "A";
                                        ucResult.txtFiledContent.Text = dsTestSample.Tables[0].Rows[0]["A(%)"].ToString();
                                        fac.flowLayoutPanel1.Controls.Add(ucResult);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(this, "该试样的试验方法已不存在!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    dsSt.Dispose();
                                    dsTestSample.Dispose();
                                    return;
                                }
                            }
                            dsSt.Dispose();
                            dsTestSample.Dispose();
                            fac.ShowDialog();
                            TestStandard.GBT23615_2009.TensileZong.readFinishSample(dataGridView, dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            dataGridView_CellClick(sender, new DataGridViewCellEventArgs(0, _selTestSampleArray[_selTestSampleArray.Count - 1]._RowIndex));
                        }
                        break;

                    case "GBT228-2010":
                        if (_selTestSampleArray[_selTestSampleArray.Count - 1] != null)
                        {
                            AnalysiseCurve.GBT228 fac = new HR_Test.AnalysiseCurve.GBT228();
                            fac.M_SR = this.m_LoadResolutionValue;
                            fac.m_checkstopvalue = m_CheckStopValue;
                            fac.WindowState = FormWindowState.Maximized;
                            fac.tslblSampleNo.Text = _selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample;
                            fac._TestSampleNo = _selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample;
                            fac._CurveName = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + _selTestSampleArray[_selTestSampleArray.Count - 1] + ".txt";
                            fac._TestType = "GBT228-2010";
                            fac._LineColor = "Brown";// this.dataGridView.Rows[dataGridView.SelectedRows.Count
                            //读取试样结果  
                            BLL.TestSample bllts = new HR_Test.BLL.TestSample();
                            Model.TestSample modelTs = bllts.GetModel(_selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample);
                            //DataSet dsTestSample = bllts.GetList(" testSampleNo='" + _selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample + "'");

                            //读取选择试验结果表
                            BLL.SelTestResult bllSt = new HR_Test.BLL.SelTestResult();
                            //DataSet dsSt = bllSt.GetList(" methodName='" + modelTs.testMethodName + "'");
                            Model.SelTestResult mSelT = bllSt.GetModel(modelTs.testMethodName);

                            if (mSelT != null)
                            {
                                //是否选择上或下屈服
                                //"-", "-", "Fm", "Rm", "ReH", "ReL", "Rp", "Rt", "Rr", "E", "m", "mE", "A", "Ae", "Ag", "At", "Agt", "Awn","Lm", "ΔLm", "Lf", "Z", "X", "S", "X￣"
                                if (mSelT.ReH) fac._IsSelReH = true;
                                if (mSelT.ReL) fac._IsSelReL = true;
                                if (mSelT.Fm)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("Fm", "Fm", (modelTs.Fm.Value / 1000.0).ToString("f2") + " kN", (modelTs.Fm.Value / 1000.0).ToString("f2")));
                                if (mSelT.Rm)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("Rm", "Rm", modelTs.Rm.Value.ToString("f2") + " MPa", modelTs.Rm.Value.ToString("f2")));
                                if (mSelT.ReH)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("ReH", "ReH", modelTs.ReH.Value.ToString("f2") + " MPa", modelTs.ReH.Value.ToString("f2")));
                                if (mSelT.ReL)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("ReL", "ReL", modelTs.ReL.Value.ToString("f2") + " MPa", modelTs.ReL.Value.ToString("f2")));
                                if (mSelT.Rp)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("Rp", "Rp", modelTs.Rp.Value.ToString("f2") + " MPa", modelTs.Rp.Value.ToString("f2")));
                                if (mSelT.Rr)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("Rr", "Rr", modelTs.Rr.Value.ToString("f2") + " MPa", modelTs.Rr.Value.ToString("f2")));
                                if (mSelT.Rt)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("Rt", "Rt", modelTs.Rt.Value.ToString("f2") + " MPa", modelTs.Rt.Value.ToString("f2")));
                                if (mSelT.Lm)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("Lm", "Lm", modelTs.Lm.Value.ToString("f2") + " mm", modelTs.Lm.Value.ToString("f2") + " mm"));
                                if (mSelT.deltaLm)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("ΔLm", "ΔLm", modelTs.deltaLm.Value.ToString("f2") + " mm", modelTs.deltaLm.Value.ToString("f2")));
                                if (mSelT.Lf)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("Lf", "Lf", modelTs.Lf.Value.ToString("f2") + " mm", modelTs.Lf.Value.ToString("f2")));


                                if (mSelT.A)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("A", "A", modelTs.A.Value.ToString("f2") + " %", modelTs.A.Value.ToString("f2")));
                                if (mSelT.Aee)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("Ae", "Ae", modelTs.Aee.Value.ToString("f2") + " %", modelTs.Aee.Value.ToString("f2")));
                                if (mSelT.Agg)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("Ag", "Ag", modelTs.Agg.Value.ToString("f2") + " %", modelTs.Agg.Value.ToString("f2")));
                                if (mSelT.Aggtt)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("Agt", "Agt", modelTs.Aggtt.Value.ToString("f2") + " %", modelTs.Aggtt.Value.ToString("f2")));
                                if (mSelT.Att)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("At", "At", modelTs.Att.Value.ToString("f2") + " %", modelTs.Att.Value.ToString("f2")));
                                if (mSelT.Awnwn)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("Awn", "Awn", modelTs.Awnwn.Value.ToString("f2"), modelTs.Awnwn.Value.ToString("f2")));
                                if (mSelT.m)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("m", "m", modelTs.m.Value.ToString("f2"), modelTs.m.Value.ToString("f2")));

                                if (mSelT.E)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("E", "E", modelTs.E.Value.ToString("f2") + " GPa", modelTs.E.Value.ToString("f2")));
                                if (mSelT.mE)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("mE", "mE", modelTs.mE.Value.ToString("f2"), modelTs.mE.Value.ToString("f2")));
                                if (mSelT.Z)
                                    fac.flowLayoutPanel1.Controls.Add(CreateResultCtrl("Z", "Z", modelTs.Z.Value.ToString("f2") + " %", modelTs.Z.Value.ToString("f2")));

                            }
                            else
                            {
                                MessageBox.Show(this, "该试样的试验方法已不存在!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                return;
                            }

                            fac.ShowDialog();
                            TestStandard.GBT228_2010.readFinishSample(dataGridView, dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            dataGridView_CellClick(sender, new DataGridViewCellEventArgs(0, _selTestSampleArray[_selTestSampleArray.Count - 1]._RowIndex));
                        }
                        break;
                    case "GBT7314-2005":
                        if (_selTestSampleArray[_selTestSampleArray.Count - 1] != null)
                        {
                            AnalysiseCurve.GBT7314 fac = new HR_Test.AnalysiseCurve.GBT7314();
                            fac.M_SR = this.m_LoadResolutionValue;
                            fac.WindowState = FormWindowState.Maximized;
                            fac.tslblSampleNo.Text = _selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample;
                            fac._TestSampleNo = _selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample;
                            fac._CurveName = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + _selTestSampleArray[_selTestSampleArray.Count - 1] + ".txt";
                            fac._TestType = "GBT7314-2005";
                            fac._LineColor = "Brown";// this.dataGridView.Rows[dataGridView.SelectedRows.Count
                            //读取试样结果  
                            BLL.Compress bllts = new HR_Test.BLL.Compress();
                            Model.Compress modelTs = bllts.GetModel(_selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample);
                            DataSet dsTestSample = bllts.GetResultRow(" testSampleNo='" + _selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample + "'");
                            //读取选择试验结果表
                            BLL.SelTestResult_C bllSt = new HR_Test.BLL.SelTestResult_C();
                            DataSet dsSt = bllSt.GetList(" methodName='" + modelTs.testMethodName + "'");

                            if (dsSt != null)
                            {
                                if (dsSt.Tables[0].Rows.Count > 0)
                                {
                                    //是否选择上或下屈服
                                    if (Convert.ToBoolean(dsSt.Tables[0].Rows[0]["ReHc"].ToString()) == true) fac._IsSelReH = true;
                                    if (Convert.ToBoolean(dsSt.Tables[0].Rows[0]["ReLc"].ToString()) == true) fac._IsSelReL = true;

                                    int cCount = dsSt.Tables[0].Columns.Count;
                                    for (int i = 2; i < cCount - 5; i++)
                                    {

                                        if (Convert.ToBoolean(dsSt.Tables[0].Rows[0][i].ToString()) == true)
                                        {
                                            UC.Result ucResult = new HR_Test.UC.Result();
                                            ucResult._FieldName = _lblGBT7314_Result[i] + ":";
                                            ucResult.Name = _lblGBT7314_Result[i].ToString();
                                            ucResult.txtFiledContent.Text = dsTestSample.Tables[0].Rows[0][i - 1].ToString();
                                            fac.flowLayoutPanel1.Controls.Add(ucResult);
                                        }
                                        else
                                        {
                                            UC.Result ucResult = new HR_Test.UC.Result();
                                            ucResult._FieldName = _lblGBT7314_Result[i] + ":";
                                            ucResult.txtFiledContent.Enabled = false;
                                            ucResult.Visible = false;
                                            ucResult.Name = (i - 2).ToString();
                                            ucResult.txtFiledContent.Text = dsTestSample.Tables[0].Rows[0][i - 1].ToString();
                                            fac.flowLayoutPanel1.Controls.Add(ucResult);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(this, "该试样的试验方法已不存在!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    dsSt.Dispose();
                                    dsTestSample.Dispose();
                                    return;
                                }
                            }
                            dsSt.Dispose();
                            dsTestSample.Dispose();
                            fac.ShowDialog();
                            TestStandard.GBT7314_2005.readFinishSample(dataGridView, dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            dataGridView_CellClick(sender, new DataGridViewCellEventArgs(0, _selTestSampleArray[_selTestSampleArray.Count - 1]._RowIndex));
                        }
                        break;
                    case "YBT5349-2006":
                        if (_selTestSampleArray[_selTestSampleArray.Count - 1] != null)
                        {
                            AnalysiseCurve.YBT5349 fac = new HR_Test.AnalysiseCurve.YBT5349();
                            fac.M_SR = this.m_LoadResolutionValue;
                            fac.WindowState = FormWindowState.Maximized;
                            fac.tslblSampleNo.Text = _selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample;
                            fac._TestSampleNo = _selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample;
                            fac._CurveName = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + _selTestSampleArray[_selTestSampleArray.Count - 1] + ".txt";
                            fac._TestType = "YBT5349-2006";
                            fac._LineColor = "Brown";// this.dataGridView.Rows[dataGridView.SelectedRows.Count
                            //读取试样结果  
                            BLL.Bend bllBend = new HR_Test.BLL.Bend();
                            Model.Bend modelBend = bllBend.GetModel(_selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample);

                            DataSet dsTestSample = bllBend.GetList(" testSampleNo='" + _selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample + "'");


                            //读取选择试验结果表
                            BLL.SelTestResult_B bllSt = new HR_Test.BLL.SelTestResult_B();
                            DataSet dsSt = bllSt.GetList(" methodName='" + modelBend.testMethod + "'");

                            if (dsSt != null)
                            {
                                if (dsSt.Tables[0].Rows.Count > 0)
                                {
                                    //是否选择上或下屈服
                                    //if (Convert.ToBoolean(dsSt.Tables[0].Rows[0]["ReH"].ToString()) == true) fac._IsSelReH = true;
                                    //if (Convert.ToBoolean(dsSt.Tables[0].Rows[0]["ReL"].ToString()) == true) fac._IsSelReL = true;

                                    int cCount = dsSt.Tables[0].Columns.Count;
                                    for (int i = 2; i < cCount - 5; i++)
                                    {

                                        if (Convert.ToBoolean(dsSt.Tables[0].Rows[0][i].ToString()) == true)
                                        {
                                            UC.Result ucResult = new HR_Test.UC.Result();
                                            ucResult._FieldName = _lblBend_Result[i] + ":";
                                            ucResult.Name = _lblBend_Result[i].ToString();
                                            ucResult.txtFiledContent.Text = dsTestSample.Tables[0].Rows[0][i + 35].ToString();
                                            fac.flowLayoutPanel1.Controls.Add(ucResult);
                                        }
                                        else
                                        {
                                            UC.Result ucResult = new HR_Test.UC.Result();
                                            ucResult._FieldName = _lblBend_Result[i] + ":";
                                            ucResult.txtFiledContent.Enabled = false;
                                            ucResult.Visible = false;
                                            ucResult.Name = _lblBend_Result[i].ToString();
                                            ucResult.txtFiledContent.Text = dsTestSample.Tables[0].Rows[0][i + 35].ToString();
                                            fac.flowLayoutPanel1.Controls.Add(ucResult);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(this, "该试样的试验方法已不存在!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    dsSt.Dispose();
                                    dsTestSample.Dispose();
                                    return;
                                }
                            }
                            dsSt.Dispose();
                            dsTestSample.Dispose();
                            fac.ShowDialog();
                            TestStandard.YBT5349_2006.readFinishSample(this.dataGridView, this.dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            dataGridView_CellClick(sender, new DataGridViewCellEventArgs(0, _selTestSampleArray[_selTestSampleArray.Count - 1]._RowIndex));
                        }
                        break;
                    case "GBT28289-2012Shear":
                        if (_selTestSampleArray[_selTestSampleArray.Count - 1] != null)
                        {
                            AnalysiseCurve.GBT28289Shear fac = new HR_Test.AnalysiseCurve.GBT28289Shear();
                            fac.M_SR = this.m_LoadResolutionValue;
                            fac.WindowState = FormWindowState.Maximized;
                            fac.tslblSampleNo.Text = _selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample;
                            fac._TestSampleNo = _selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample;
                            fac._CurveName = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + _selTestSampleArray[_selTestSampleArray.Count - 1] + ".txt";
                            fac._TestType = "GBT28289-2012Shear";
                            fac._LineColor = "Brown";// this.dataGridView.Rows[dataGridView.SelectedRows.Count
                            //读取试样结果  
                            BLL.GBT282892012_Shear bllts = new HR_Test.BLL.GBT282892012_Shear();
                            Model.GBT282892012_Shear modelTs = bllts.GetModel(_selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample);
                            DataSet dsTestSample = bllts.GetResult(" testSampleNo='" + _selTestSampleArray[_selTestSampleArray.Count - 1]._SelTestSample + "'");
                            //读取选择试验结果表
                            BLL.GBT282892012_ShearSel bllSt = new HR_Test.BLL.GBT282892012_ShearSel();
                            DataSet dsSt = bllSt.GetList(" methodName='" + modelTs.testMethodName + "'");

                            if (dsSt != null)
                            {
                                if (dsSt.Tables[0].Rows.Count > 0)
                                {
                                    //是否选择上或下屈服
                                    if (Convert.ToBoolean(dsSt.Tables[0].Rows[0]["FTmax"].ToString()) == true)
                                    {
                                        UC.Result ucResult = new HR_Test.UC.Result();
                                        ucResult._FieldName = "FTmax(kN):";
                                        ucResult.Name = "FTmax";
                                        ucResult.txtFiledContent.Text = dsTestSample.Tables[0].Rows[0]["FTmax(kN)"].ToString();
                                        fac.flowLayoutPanel1.Controls.Add(ucResult);
                                    }
                                    if (Convert.ToBoolean(dsSt.Tables[0].Rows[0]["T"].ToString()) == true)
                                    {
                                        UC.Result ucResult = new HR_Test.UC.Result();
                                        ucResult._FieldName = "T(N/mm):";
                                        ucResult.Name = "T";
                                        ucResult.txtFiledContent.Text = dsTestSample.Tables[0].Rows[0]["T(N/mm)"].ToString();
                                        fac.flowLayoutPanel1.Controls.Add(ucResult);
                                    }
                                    if (Convert.ToBoolean(dsSt.Tables[0].Rows[0]["C1"].ToString()) == true)
                                    {
                                        UC.Result ucResult = new HR_Test.UC.Result();
                                        ucResult._FieldName = "C1(N/mm²):";
                                        ucResult.Name = "C1";
                                        ucResult.txtFiledContent.Text = dsTestSample.Tables[0].Rows[0]["C1(N/mm²)"].ToString();
                                        fac.flowLayoutPanel1.Controls.Add(ucResult);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(this, "该试样的试验方法已不存在!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    dsSt.Dispose();
                                    dsTestSample.Dispose();
                                    return;
                                }
                            }
                            dsSt.Dispose();
                            dsTestSample.Dispose();
                            fac.ShowDialog();
                            TestStandard.GBT28289_2012.Shear.readFinishSample(dataGridView, dataGridViewSum, m_TestNo, this.dateTimePicker, this.zedGraphControl);
                            dataGridView_CellClick(sender, new DataGridViewCellEventArgs(0, _selTestSampleArray[_selTestSampleArray.Count - 1]._RowIndex));
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show(this, "请选择曲线!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnZeroF_Click(object sender, EventArgs e)
        {
            if (m_LoadSensorCount == 0)
            {
                MessageBox.Show(this, "未连接设备", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lock (m_state)
            {
                Thread.Sleep(30);
                byte[] buf = new byte[5];
                int ret;

                buf[0] = 0x04;									//命令字节
                buf[1] = 0xde;									//清零命令
                buf[2] = m_LSensorArray[0].SensorIndex;			//因为是简单测试程序，所以只取了零号索引。
                buf[3] = 0;
                buf[4] = 0;

                ret = RwUsb.WriteData1582(1, buf, 5, 1000);		//发送写命令
                Thread.Sleep(30);
            }
        }

        private void btnZeroS_Click(object sender, EventArgs e)
        {
            if (m_ElongateSensorCount == 0)
            {
                MessageBox.Show(this, "未连接设备!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // TODO: 在此添加控件通知处理程序代码
            lock (m_state)
            {
                Thread.Sleep(30);
                byte[] buf = new byte[5];
                int ret;
                buf[0] = 0x04;									//命令字节
                buf[1] = 0xde;									//清零命令
                buf[2] = m_ESensorArray[0].SensorIndex;			//因为是简单测试程序，所以只取了零号索引。
                buf[3] = 0;
                buf[4] = 0;
                ret = RwUsb.WriteData1582(1, buf, 5, 1000);				//发送写命令
                Thread.Sleep(30);
            }
            //试验过程中使用引伸计
            _useExten = true;
            //画曲线标志
            m_useExten = true;
            if (_useExten)
            {
                this.lblUseExten.Text = "使用引伸计";
                this.lblUseExten.ForeColor = Color.Green;
                this.tsbtnYSJ.Enabled = true;
                this.lblUseExten.Refresh();
            }
        }

        private void tsbtnMinimize_Click_1(object sender, EventArgs e)
        {
            _fmMain.WindowState = FormWindowState.Minimized;
            //this.WindowState = FormWindowState.Minimized;
        }

        private void btnZeroD_Click(object sender, EventArgs e)
        {
            if (m_DisplacementSensorCount == 0)
            {
                MessageBox.Show(this, "未连接设备", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lock (m_state)
            {
                Thread.Sleep(30);
                byte[] buf = new byte[5];
                int ret;

                buf[0] = 0x04;									//命令字节
                buf[1] = 0xde;									//清零命令
                buf[2] = m_DSensorArray[0].SensorIndex;			//因为是简单测试程序，所以只取了零号索引。
                buf[3] = 0;
                buf[4] = 0;

                ret = RwUsb.WriteData1582(1, buf, 5, 1000);				//发送写命令
                Thread.Sleep(30);
            }
        }

        private void tsbtn_Return_Click(object sender, EventArgs e)
        {
            //TEST
            if (m_LoadSensorCount == 0)
            {
                MessageBox.Show(this, "未连接设备", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lock (m_state)
            {
                Thread.Sleep(50);
                // TODO: 在此添加控件通知处理程序代码
                byte[] buf = new byte[5];// char buf[5];
                int ret;
                buf[0] = 0x04;									//命令字节
                buf[1] = 0xf6;									//清零命令
                buf[2] = 0;										//
                buf[3] = 0;
                buf[4] = 0;
                ret = RwUsb.WriteData1582(1, buf, 5, 1000);				//发送写命令
                Thread.Sleep(50);
                //m_IsReturn = true;
            }
        }

        private void tsbtnSetRealtimeCurve_Click(object sender, EventArgs e)
        {
            frmSetRealtimeCurve fs = new frmSetRealtimeCurve();
            if (DialogResult.OK == fs.ShowDialog())
            {
                ChangeRealTimeXYChart();
                Thread.Sleep(5);
                if (isTest)
                {
                    ReadTestingData();
                }
                //  list_data.count >1,read _list_data 
                if (!isTest)
                {
                    //this.calloutAnnotation.Visible = false;
                    //this.a_fm.Visible = false;
                    //this.a_rp02.Visible = false;
                    ChangeRealTimeXYChart();
                    //读取结果数据到 _List_Data
                    List<gdata> _l = ReadOneListData(m_path, this.m_TestSampleNo);
                    if (_l != null)
                    {
                        if (_l.Count > 0)
                        {
                            //计算
                            CalcData(_l, m_isSelReH, m_isSelReL);
                            //ReadResultCurve(this.pbChart, _l);
                            ReadResultData(_l);
                            //自动求取
                            //AutoDrawResult(_l);
                            isShowResult = false;
                        }
                    }
                }
            }
        }

        private void AutoDrawResult(List<gdata> _l)
        {
            m_FrIndex = m_FmIndex;
            int tempIndex = 0;
            int count = 0;
            if (_X1 == 5 && _Y1 == 1 && m_useExten)
            {
                do
                {
                    tempIndex = m_FrIndex;
                    if (GetFp02IndexOnE(_l, tempIndex, out m_FrIndex, out m_a, out m_k, out m_fr05index, out m_fr01index, out m_ep02L0))
                    {
                        count++;
                        //MessageBox.Show(count.ToString() + ":" + m_FrIndex.ToString() + "," + tempIndex.ToString());                                    
                        m_Rp = float.Parse((_l[m_FrIndex].YL1).ToString("G5"));
                        m_calSuccess = true;
                        if (count > 500)
                        {
                            m_Rp = 0;
                            m_calSuccess = false;
                            MessageBox.Show(this, "计算失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    }
                    else
                    {
                        m_calSuccess = false;
                        m_Rp = 0;
                        MessageBox.Show(this, "计算失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                while (m_FrIndex > tempIndex + 2 || m_FrIndex < tempIndex - 2);

                //重新读取_List_Data显示结果曲线
                //if (Rp > 0)
                //{
                //判断曲线数据 以及 标注 20130515取消Rp自动划线
                //DrawResult(g,_l, m_k, m_a, m_fr05index, m_fr01index);
                //}

            }

            if (_X1 == 2 && _Y1 == 1 && !m_useExten)
            {
                do
                {
                    tempIndex = m_FrIndex;
                    if (GetFp02Index(_l, tempIndex, out m_FrIndex, out m_a, out m_k, out m_fr05index, out m_fr01index, out m_ep02L0))
                    {
                        count++;
                        m_Rp = float.Parse((_l[m_FrIndex].YL1).ToString("G5"));
                        m_calSuccess = true;
                        //MessageBox.Show(count.ToString() + ":" + m_FrIndex.ToString() + "," + tempIndex.ToString());
                        if (count > 500)
                        {
                            m_Rp = 0;
                            m_calSuccess = false;
                            MessageBox.Show(this, "计算失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    }
                    else
                    {
                        m_calSuccess = false;
                        m_Rp = 0;
                        MessageBox.Show(this, "计算失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                while (m_FrIndex > tempIndex + 2 || m_FrIndex < tempIndex - 2);

                //if (Rp > 0)
                //{
                //在试验结果曲线上标注Rp02的斜线和点
                //Graphics g = this.pbChart.CreateGraphics();
                //DrawResult(g,_l, m_k, m_a, m_fr05index, m_fr01index);
                //}
            }
            pbChart.Invalidate();
        }

        delegate void delReadResultCurve(PictureBox _chart, List<gdata> _l);
        private void ReadResultData(List<gdata> _l)
        {
            delReadResultCurve rtc = new delReadResultCurve(ReadResultCurve);
            if (this.pbChart.InvokeRequired)
            {
                this.pbChart.BeginInvoke(rtc, new object[] { this.pbChart, _l });
            }
            else
            {
                ReadResultCurve(this.pbChart, _l);
            }
        }
        private void ReadResultCurve(PictureBox _chart, List<gdata> _l)
        {
            try
            {
                if (_l != null)
                {
                    int listDataCount = _l.Count;
                    if (listDataCount > 0)
                    {
                        gdata[] gdata = new gdata[listDataCount];
                        _l.CopyTo(gdata);
                        //ly1_x1.RemoveRange(0, ly1_x1.Count);
                        //ly1_x2.RemoveRange(0, ly1_x2.Count);
                        //ly2_x1.RemoveRange(0, ly2_x1.Count);
                        //ly2_x2.RemoveRange(0, ly2_x2.Count);
                        ly1_x1.Clear();
                        ly1_x2.Clear();
                        ly2_x1.Clear();
                        ly2_x2.Clear();
                        //_chart.Refresh();
                        //_chart.Invalidate();
                        //Thread.Sleep(20);
                        //int step = listDataCount / 800;
                        //if (step == 0)
                        //    step = 1;
                        for (int i = 1; i < listDataCount - 2; i++)
                        {
                            float time = gdata[i].Ts;
                            float F1value = gdata[i].F1;
                            float D1value = gdata[i].D1;
                            float YB1value = gdata[i].YB1;
                            float YL1value = gdata[i].YL1;
                            float BX1value = gdata[i].BX1;

                            //显示曲线数据
                            #region Y1-X1 / Y1-X2 第一、二条曲线
                            switch (_Y1)//this.tscbY1.SelectedIndex
                            {
                                case 1:
                                    switch (_X1)
                                    {
                                        case 1: ly1_x1.Add(new PointF(time, F1value)); break;
                                        case 2: ly1_x1.Add(new PointF(D1value, F1value)); break;
                                        case 3: ly1_x1.Add(new PointF(YB1value, F1value)); break;
                                        case 4: ly1_x1.Add(new PointF(YL1value, F1value)); break;
                                        case 5: ly1_x1.Add(new PointF(BX1value, F1value)); break;
                                    }
                                    switch (_X2)
                                    {
                                        case 1: ly1_x2.Add(new PointF(time, F1value)); break;
                                        case 2: ly1_x2.Add(new PointF(D1value, F1value)); break;
                                        case 3: ly1_x2.Add(new PointF(YB1value, F1value)); break;
                                        case 4: ly1_x2.Add(new PointF(YL1value, F1value)); break;
                                        case 5: ly1_x2.Add(new PointF(BX1value, F1value)); break;
                                    }
                                    break;
                                case 2:
                                    switch (_X1)
                                    {
                                        case 1: ly1_x1.Add(new PointF(time, YL1value)); break;
                                        case 2: ly1_x1.Add(new PointF(D1value, YL1value)); break;
                                        case 3: ly1_x1.Add(new PointF(YB1value, YL1value)); break;
                                        case 5: ly1_x1.Add(new PointF(BX1value, YL1value)); break;
                                    }
                                    switch (_X2)
                                    {
                                        case 1: ly1_x2.Add(new PointF(time, YL1value)); break;
                                        case 2: ly1_x2.Add(new PointF(D1value, YL1value)); break;
                                        case 3: ly1_x2.Add(new PointF(YB1value, YL1value)); break;
                                        case 5: ly1_x2.Add(new PointF(BX1value, YL1value)); break;
                                    }
                                    break;
                                case 3:
                                    switch (_X1)
                                    {
                                        case 1: ly1_x1.Add(new PointF(time, BX1value)); break;
                                        case 2: ly1_x1.Add(new PointF(D1value, BX1value)); break;
                                        case 3: ly1_x1.Add(new PointF(YB1value, BX1value)); break;
                                        case 4: ly1_x1.Add(new PointF(YL1value, BX1value)); break;
                                    }
                                    switch (_X2)//this.tscbX2.SelectedIndex
                                    {
                                        case 1: ly1_x2.Add(new PointF(time, BX1value)); break;
                                        case 2: ly1_x2.Add(new PointF(D1value, BX1value)); break;
                                        case 3: ly1_x2.Add(new PointF(YB1value, BX1value)); break;
                                        case 4: ly1_x2.Add(new PointF(YL1value, BX1value)); break;
                                    }
                                    break;
                                case 4:
                                    switch (_X1)//this.tscbX1.SelectedIndex
                                    {
                                        case 1: ly1_x1.Add(new PointF(time, D1value)); break;
                                        case 2: break;
                                        case 3: ly1_x1.Add(new PointF(YB1value, D1value)); break;
                                        case 5: break;
                                    }
                                    switch (_X2)//this.tscbX2.SelectedIndex
                                    {
                                        case 1: ly1_x2.Add(new PointF(time, D1value)); break;
                                        case 2: break;
                                        case 3: ly1_x2.Add(new PointF(YB1value, D1value)); break;
                                    }
                                    break;
                            }
                            #endregion

                            #region Y2-X1 / Y2-X2 第三、四条曲线
                            switch (_Y2)//this.tscbY2.SelectedIndex
                            {
                                case 1:
                                    switch (_X1)//this.tscbX1.SelectedIndex
                                    {
                                        case 1:
                                            ly2_x1.Add(new PointF(time, F1value));
                                            break;
                                        case 2:
                                            ly2_x1.Add(new PointF(D1value, F1value));
                                            break;
                                        case 3:
                                            ly2_x1.Add(new PointF(YB1value, F1value));
                                            break;
                                        case 4:
                                            ly2_x1.Add(new PointF(YL1value, F1value));
                                            break;
                                        case 5:
                                            ly2_x1.Add(new PointF(BX1value, F1value));
                                            break;
                                    }
                                    switch (_X2)
                                    {
                                        case 1:
                                            ly2_x2.Add(new PointF(time, F1value));
                                            break;
                                        case 2:
                                            ly2_x2.Add(new PointF(D1value, F1value));
                                            break;
                                        case 3:
                                            ly2_x2.Add(new PointF(YB1value, F1value));
                                            break;
                                        case 4:
                                            ly2_x2.Add(new PointF(YL1value, F1value));
                                            break;
                                        case 5:
                                            ly2_x2.Add(new PointF(BX1value, F1value));
                                            break;
                                    }

                                    break;
                                case 2:
                                    switch (_X1)
                                    {
                                        case 1:
                                            ly2_x1.Add(new PointF(time, YL1value));
                                            break;
                                        case 2:
                                            ly2_x1.Add(new PointF(D1value, YL1value));
                                            break;
                                        case 3:
                                            ly2_x1.Add(new PointF(YB1value, YL1value));
                                            break;
                                        case 5:
                                            ly2_x1.Add(new PointF(BX1value, YL1value));
                                            break;

                                    }
                                    switch (_X2)
                                    {
                                        case 1:
                                            ly2_x2.Add(new PointF(time, YL1value));
                                            break;
                                        case 2:
                                            ly2_x2.Add(new PointF(D1value, YL1value));
                                            break;
                                        case 3:
                                            ly2_x2.Add(new PointF(YB1value, YL1value));
                                            break;
                                        case 5:
                                            ly2_x2.Add(new PointF(BX1value, YL1value));
                                            break;
                                    }

                                    break;
                                case 3:
                                    switch (_X1)
                                    {
                                        case 1:
                                            ly2_x1.Add(new PointF(time, BX1value));
                                            break;
                                        case 2:
                                            ly2_x1.Add(new PointF(D1value, BX1value));
                                            break;
                                        case 3:
                                            ly2_x1.Add(new PointF(YB1value, BX1value));
                                            break;
                                        case 4:
                                            ly2_x1.Add(new PointF(YL1value, BX1value));
                                            break;
                                    }
                                    switch (_X2)
                                    {
                                        case 1:
                                            ly2_x2.Add(new PointF(time, BX1value));
                                            break;
                                        case 2:
                                            ly2_x2.Add(new PointF(D1value, BX1value));
                                            break;
                                        case 3:
                                            ly2_x2.Add(new PointF(YB1value, BX1value));
                                            break;
                                        case 4:
                                            ly2_x2.Add(new PointF(YL1value, BX1value));
                                            break;
                                    }
                                    break;
                                case 4:
                                    switch (_X1)
                                    {
                                        case 1:
                                            ly2_x1.Add(new PointF(time, D1value));
                                            break;
                                        case 2:
                                            //strCurveName[0] = "位移/位移";
                                            //LineItemListEdit_2.Add(D1value, D1value);
                                            //_RPPList2.Add(D1value, D1value);
                                            break;
                                        case 3:
                                            ly2_x1.Add(new PointF(YB1value, D1value));
                                            break;
                                    }
                                    switch (_X2)
                                    {
                                        case 1:
                                            //strCurveName[0] = "位移/时间";
                                            //LineItemListEdit_3.Add(time, D1value);
                                            //.Add(time, D1value); 
                                            ly2_x2.Add(new PointF(time, D1value));
                                            break;
                                        case 2:
                                            //strCurveName[0] = "位移/位移";
                                            //LineItemListEdit_3.Add(D1value, D1value);
                                            //_RPPList3.Add(D1value, D1value);
                                            break;
                                        case 3:
                                            //strCurveName[0] = "位移/应变";
                                            //LineItemListEdit_3.Add(YB1value, D1value);
                                            //_RPPList3.Add(YB1value, D1value); 
                                            ly2_x2.Add(new PointF(YB1value, D1value));
                                            break;
                                    }
                                    break;
                            }
                            #endregion

                        }
                        ChangeChartXY(gdata[m_FmaxIndex]);
                        ChangeChartXY(gdata[listDataCount - 1]);
                    }
                }
                _chart.Invalidate();
            }
            catch { }
        }

        void ChangeChartXY(gdata g)
        {
            try
            {
                switch (_X1)// -请选择-   时间,s  位移,μm  应变,μm 应力,MPa
                {
                    case 1:
                        isShow103X1 = false;
                        if (g.Ts > c_x1maximum)
                            c_x1maximum = ((int)(1.2 * g.Ts) / 50) * 50f + 50f;
                        break;
                    case 2:
                        isShow103X1 = true;
                        if (Math.Abs(g.D1) >= 1000.0)
                            m_LabelX1 = "位移(mm)";
                        if (Math.Abs(g.D1) > Math.Abs(c_x1maximum))
                        {
                            c_x1maximum = ((int)(1.2 * g.D1) / 50) * 50f + 50f;
                            if (Math.Abs(c_x1maximum) >= 1000.0)
                            {
                                c_x1maximum = ((int)c_x1maximum / 500) * 500f + 500f;
                                m_LabelX1 = "位移(mm)";
                            }
                        }
                        break;
                    case 3:
                        isShow103X1 = false;
                        if (Math.Abs(g.YB1) > Math.Abs(c_x1maximum))
                        {
                            c_x1maximum = (int)(g.YB1 / 0.5) * 0.5f + 0.5f;
                        }
                        break;
                    case 4:
                        isShow103X1 = false;
                        if (Math.Abs(g.YL1) > Math.Abs(c_x1maximum))
                        {
                            c_x1maximum = ((int)(1.2 * g.YL1) / 50) * 50 + 50;
                        }
                        break;

                    case 5:
                        isShow103X1 = true;
                        if (Math.Abs(g.BX1) >= 1000.0)
                        {
                            m_LabelX1 = "变形(mm)";
                        }
                        if (Math.Abs(g.BX1) > Math.Abs(c_x1maximum))
                        {
                            c_x1maximum = ((int)(1.2 * g.BX1) / 50) * 50 + 50;
                            if (Math.Abs(c_x1maximum) >= 1000.0)
                            {
                                c_x1maximum = ((int)c_x1maximum / 500) * 500 + 500;
                                m_LabelX1 = "变形(mm)";
                            }
                        }
                        break;

                }

                switch (_X2)// -请选择-   时间,s  位移,μm  应变,μm
                {
                    case 1:
                        isShow103X2 = false;
                        if (g.Ts > c_x2maximum)
                            c_x2maximum = ((int)(1.2 * g.Ts) / 50) * 50f + 50f;
                        break;
                    case 2:
                        isShow103X2 = true;
                        if (Math.Abs(g.D1) >= 1000.0)
                            m_LabelX2 = "位移(mm)";
                        if (Math.Abs(g.D1) > Math.Abs(c_x2maximum))
                        {
                            c_x2maximum = ((int)(1.2 * g.D1) / 50) * 50f + 50f;
                            if (Math.Abs(c_x2maximum) >= 1000.0)
                            {
                                c_x2maximum = ((int)c_x2maximum / 500) * 500f + 500f;
                                m_LabelX2 = "位移(mm)";
                            }
                        }
                        break;
                    case 3:
                        isShow103X2 = false;
                        if (Math.Abs(g.YB1) > Math.Abs(c_x2maximum))
                        {
                            c_x2maximum = (int)(g.YB1 / 0.5) * 0.5f + 0.5f;
                        }
                        break;
                    case 4:
                        isShow103X2 = false;
                        if (Math.Abs(g.YL1) > Math.Abs(c_x2maximum))
                        {
                            c_x2maximum = ((int)(1.2 * g.YL1) / 50) * 50 + 50;
                        }
                        break;

                    case 5:
                        isShow103X2 = true;
                        if (Math.Abs(g.BX1) >= 1000.0)
                        {
                            m_LabelX2 = "变形(mm)";
                        }
                        if (Math.Abs(g.BX1) > Math.Abs(c_x2maximum))
                        {
                            c_x2maximum = ((int)(1.2 * g.BX1) / 50) * 50 + 50;
                            if (Math.Abs(c_x2maximum) >= 1000.0)
                            {
                                c_x2maximum = ((int)c_x2maximum / 500) * 500 + 500;
                                m_LabelX2 = "变形(mm)";
                            }
                        }
                        break;
                }


                //{   //-请选择- 0
                //力,kN 1
                //应力,MPa 2
                //变形,μm 3
                //位移,μm 4
                //Scale sScale = _RealTimePanel.YAxisList[1].Scale;
                switch (_Y1)
                {
                    case 1:
                        isShow103Y1 = true;
                        if (Math.Abs(g.F1) >= 1000.0)
                        {
                            m_LabelY1 = "负荷(kN)";
                        }
                        if (Math.Abs(g.F1) > Math.Abs(c_y1maximum))
                        {
                            c_y1maximum = ((int)(1.2 * g.F1) / 50) * 50 + 50;
                            if (Math.Abs(c_y1maximum) >= 1000.0)
                            {
                                c_y1maximum = ((int)c_y1maximum / 500) * 500 + 500;
                                m_LabelY1 = "负荷(kN)";
                            }
                        }
                        break;
                    case 2:
                        isShow103Y1 = false;
                        if (Math.Abs(g.YL1) > Math.Abs(c_y1maximum))
                        {
                            c_y1maximum = ((int)(1.2 * g.YL1) / 50) * 50 + 50;
                        }
                        break;
                    case 3:
                        isShow103Y1 = true;
                        if (Math.Abs(g.BX1) >= 1000.0)
                        {
                            m_LabelY1 = "变形(mm)";
                        }
                        if (Math.Abs(g.BX1) > Math.Abs(c_y1maximum))
                        {
                            c_y1maximum = ((int)(1.2 * g.BX1) / 50) * 50 + 50;
                            if (Math.Abs(c_y1maximum) >= 1000.0)
                            {
                                c_y1maximum = ((int)c_y1maximum / 500) * 500 + 500;
                                m_LabelY1 = "变形(mm)";
                            }
                        }
                        break;
                    case 4:
                        isShow103Y1 = true;
                        if (Math.Abs(g.D1) >= 1000.0)
                        {
                            m_LabelY1 = "位移(mm)";
                        }
                        if (Math.Abs(g.D1) > Math.Abs(c_y1maximum))
                        {
                            c_y1maximum = ((int)(1.2 * g.D1) / 50) * 50 + 50;
                            if (Math.Abs(c_y1maximum) >= 1000.0)
                            {
                                c_y1maximum = ((int)c_y1maximum / 500) * 500 + 500;
                                m_LabelY1 = "位移(mm)";
                            }
                        }
                        break;
                }

                switch (_Y2)
                {
                    case 1:
                        isShow103Y2 = true;
                        if (Math.Abs(g.F1) >= 1000.0)
                        {
                            m_LabelY2 = "负荷(kN)";
                        }
                        if (Math.Abs(g.F1) > Math.Abs(c_y2maximum))
                        {
                            c_y2maximum = ((int)(1.2 * g.F1) / 50) * 50 + 50;
                            if (Math.Abs(c_y2maximum) >= 1000.0)
                            {
                                c_y2maximum = ((int)c_y2maximum / 500) * 500 + 500;
                                m_LabelY2 = "负荷(kN)";
                            }
                        }
                        break;
                    case 2:
                        isShow103Y2 = false;
                        if (Math.Abs(g.YL1) > Math.Abs(c_y2maximum))
                        {
                            c_y2maximum = ((int)(1.2 * g.YL1) / 50) * 50 + 50;
                        }
                        break;
                    case 3:
                        isShow103Y2 = true;
                        if (Math.Abs(g.BX1) >= 1000.0)
                        {
                            m_LabelY2 = "变形(mm)";
                        }
                        if (Math.Abs(g.BX1) > Math.Abs(c_y2maximum))
                        {
                            c_y2maximum = ((int)(1.2 * g.BX1) / 50) * 50 + 50;
                            if (Math.Abs(c_y2maximum) >= 1000.0)
                            {
                                c_y2maximum = ((int)c_y2maximum / 500) * 500 + 500;
                                m_LabelY2 = "变形(mm)";
                            }
                        }
                        break;
                    case 4:
                        isShow103Y2 = true;
                        if (Math.Abs(g.D1) >= 1000.0)
                        {
                            m_LabelY2 = "位移(mm)";
                        }
                        if (Math.Abs(g.D1) > Math.Abs(c_y2maximum))
                        {
                            c_y2maximum = ((int)(1.2 * g.D1) / 50) * 50 + 50;
                            if (Math.Abs(c_y2maximum) >= 1000.0)
                            {
                                c_y2maximum = ((int)c_y2maximum / 500) * 500 + 500;
                                m_LabelY2 = "位移(mm)";
                            }
                        }
                        break;
                }
            }
            catch (Exception ee) { MessageBox.Show(this, ee.ToString(), "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }


        delegate void delReadTestingCurve(PictureBox _chart, string _curvepath, string _curvename);
        private void ReadTestingData()
        {
            delReadTestingCurve rtc = new delReadTestingCurve(ReadTestingCurve);
            if (this.pbChart.InvokeRequired)
            {
                this.pbChart.BeginInvoke(rtc, new object[] { this.pbChart, m_path, m_TestSampleNo });
            }
            else
            {
                ReadTestingCurve(this.pbChart, m_path, m_TestSampleNo);
            }
        }



        //读取正在测试试验的曲线数据
        private void ReadTestingCurve(PictureBox _chart, string _path, string _curveName)
        {
            List<gdata> lg = ReadOneListData(_path, _curveName);
            int listDataCount = lg.Count;
            //1000条数据才保存，so，最低为1000
            gdata[] _gdata = new gdata[listDataCount + 500];
            int testingCount = _List_Testing_Data.Count;
            lg.CopyTo(_gdata);
            _List_Testing_Data.CopyTo(0, _gdata, listDataCount, testingCount);

            //int testingCount = _List_Testing_Data.Count;
            //gdata[] _gdata = new gdata[testingCount+20]; 
            //_List_Testing_Data.CopyTo(_gdata);

            ly1_x1.Clear();
            ly1_x2.Clear();
            ly2_x1.Clear();
            ly2_x2.Clear();

            int step = (testingCount) / 3000;
            if (step == 0)
                step = 1;

            int maxIndex = 0;

            for (int i = 2; i < testingCount + listDataCount; i += step)
            {
                float time = _gdata[i].Ts;
                float F1value = _gdata[i].F1;
                float D1value = _gdata[i].D1;
                float YB1value = _gdata[i].YB1;
                float YL1value = _gdata[i].YL1;
                float BX1value = _gdata[i].BX1;
                //找出负荷最大的点
                if (i > step + 2)
                {
                    if (F1value > _gdata[i - step].F1)
                    {
                        maxIndex = i;
                    }
                }
                //显示曲线数据
                #region Y1-X1 / Y1-X2 第一、二条曲线
                switch (_Y1)
                {
                    case 1:
                        switch (_X1)
                        {
                            case 1:
                                ly1_x1.Add(new PointF(time, F1value));
                                break;
                            case 2:
                                ly1_x1.Add(new PointF(D1value, F1value));
                                break;
                            case 3:
                                ly1_x1.Add(new PointF(YB1value, F1value));
                                break;
                            case 4:
                                ly1_x1.Add(new PointF(YL1value, F1value));
                                break;
                            case 5:
                                ly1_x1.Add(new PointF(BX1value, F1value));
                                break;
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:
                                ly1_x2.Add(new PointF(time, F1value));
                                break;
                            case 2:
                                ly1_x2.Add(new PointF(D1value, F1value));
                                break;
                            case 3:
                                ly1_x2.Add(new PointF(YB1value, F1value));
                                break;
                            case 4:
                                ly1_x2.Add(new PointF(YL1value, F1value));
                                break;
                            case 5:
                                ly1_x2.Add(new PointF(BX1value, F1value));
                                break;
                        }

                        break;
                    case 2:
                        switch (_X1)
                        {
                            case 1:
                                ly1_x1.Add(new PointF(time, YL1value));
                                break;
                            case 2:
                                ly1_x1.Add(new PointF(D1value, YL1value));
                                break;
                            case 3:
                                ly1_x1.Add(new PointF(YB1value, YL1value));
                                break;
                            case 5:
                                ly1_x1.Add(new PointF(BX1value, YL1value));
                                break;
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:
                                ly1_x2.Add(new PointF(time, YL1value));
                                break;
                            case 2:
                                ly1_x2.Add(new PointF(D1value, YL1value));
                                break;
                            case 3:
                                ly1_x2.Add(new PointF(YB1value, YL1value));
                                break;
                            case 5:
                                ly1_x2.Add(new PointF(BX1value, YL1value));
                                break;
                        }

                        break;
                    case 3:
                        switch (_X1)
                        {
                            case 1:
                                ly1_x1.Add(new PointF(time, BX1value));
                                break;
                            case 2:
                                ly1_x1.Add(new PointF(D1value, BX1value));
                                break;
                            case 3:
                                ly1_x1.Add(new PointF(YB1value, BX1value));
                                break;
                            case 4:
                                ly1_x1.Add(new PointF(YL1value, BX1value));
                                break;
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:
                                ly1_x2.Add(new PointF(time, BX1value));
                                break;
                            case 2:
                                ly1_x2.Add(new PointF(D1value, BX1value));
                                break;
                            case 3:
                                ly1_x2.Add(new PointF(YB1value, BX1value));
                                break;
                            case 4:
                                ly1_x2.Add(new PointF(YL1value, BX1value));
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        break;
                    case 4:
                        switch (_X1)//this.tscbX1.SelectedIndex
                        {
                            case 1:
                                ly1_x1.Add(new PointF(time, D1value));
                                break;
                            case 2:
                                break;
                            case 3:
                                ly1_x1.Add(new PointF(YB1value, D1value));
                                break;
                            case 5:

                                break;
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:
                                ly1_x2.Add(new PointF(time, D1value));
                                break;
                            case 2:
                                break;
                            case 3:
                                ly1_x2.Add(new PointF(YB1value, D1value));
                                break;
                        }
                        break;
                }
                #endregion

                #region Y2-X1 / Y2-X2 第三、四条曲线
                switch (_Y2)//this.tscbY2.SelectedIndex
                {
                    case 1:
                        switch (_X1)//this.tscbX1.SelectedIndex
                        {
                            case 1:
                                ly2_x1.Add(new PointF(time, F1value));
                                break;
                            case 2:
                                ly2_x1.Add(new PointF(D1value, F1value));
                                break;
                            case 3:
                                ly2_x1.Add(new PointF(YB1value, F1value));
                                break;
                            case 4:
                                ly2_x1.Add(new PointF(YL1value, F1value));
                                break;
                            case 5:
                                ly2_x1.Add(new PointF(BX1value, F1value));
                                break;
                        }
                        switch (_X2)
                        {
                            case 1:

                                ly2_x2.Add(new PointF(time, F1value));
                                break;
                            case 2:
                                ly2_x2.Add(new PointF(D1value, F1value));
                                break;
                            case 3:
                                ly2_x2.Add(new PointF(YB1value, F1value));
                                break;
                            case 4:
                                ly2_x2.Add(new PointF(YL1value, F1value));
                                break;
                            case 5:
                                ly2_x2.Add(new PointF(BX1value, F1value));
                                break;
                        }

                        break;
                    case 2:
                        switch (_X1)
                        {
                            case 1:

                                ly2_x1.Add(new PointF(time, YL1value));
                                break;
                            case 2:

                                ly2_x1.Add(new PointF(D1value, YL1value));
                                break;
                            case 3:

                                ly2_x1.Add(new PointF(YB1value, YL1value));
                                break;
                            case 5:
                                ly2_x1.Add(new PointF(BX1value, YL1value));
                                break;

                        }
                        switch (_X2)
                        {
                            case 1:
                                ly2_x2.Add(new PointF(time, YL1value));
                                break;
                            case 2:
                                ly2_x2.Add(new PointF(D1value, YL1value));
                                break;
                            case 3:
                                ly2_x2.Add(new PointF(YB1value, YL1value));
                                break;
                            case 5:
                                ly2_x2.Add(new PointF(BX1value, YL1value));
                                break;
                        }

                        break;
                    case 3:
                        switch (_X1)
                        {
                            case 1:
                                ly2_x1.Add(new PointF(time, BX1value));
                                break;
                            case 2:
                                ly2_x1.Add(new PointF(D1value, BX1value));
                                break;
                            case 3:
                                ly2_x1.Add(new PointF(YB1value, BX1value));
                                break;
                            case 4:
                                ly2_x1.Add(new PointF(YL1value, BX1value));
                                break;
                        }
                        switch (_X2)
                        {
                            case 1:
                                ly2_x2.Add(new PointF(time, BX1value));
                                break;
                            case 2:
                                ly2_x2.Add(new PointF(D1value, BX1value));
                                break;
                            case 3:
                                ly2_x2.Add(new PointF(YB1value, BX1value));
                                break;
                            case 4:
                                ly2_x2.Add(new PointF(YL1value, BX1value));
                                break;
                        }
                        break;
                    case 4:
                        switch (_X1)
                        {
                            case 1:
                                ly2_x1.Add(new PointF(time, D1value));
                                break;
                            case 3:
                                ly2_x1.Add(new PointF(YB1value, D1value));
                                break;
                        }
                        switch (_X2)
                        {
                            case 1:
                                ly2_x2.Add(new PointF(time, D1value));
                                break;
                            case 3:
                                ly2_x2.Add(new PointF(YB1value, D1value));
                                break;
                        }
                        break;
                }
                #endregion

            }

            if (_gdata.Length > 0)
            {
                ChangeChartXY(_gdata[maxIndex]);
                ChangeChartXY(_gdata[testingCount + listDataCount - 1]);
            }
            _chart.Invalidate();
            _chart.Refresh();
        }

        private void ReadTestingCurve1(PictureBox _chart, string _path, string _curveName)
        {
            List<gdata> lg = ReadOneListData(_path, _curveName);
            int listDataCount = lg.Count;
            gdata[] _gdata = new gdata[listDataCount];
            //int testingCount = _List_Testing_Data.Count;
            //lg.CopyTo(_gdata);
            //_List_Testing_Data.CopyTo(0, _gdata, listDataCount, testingCount);

            ly1_x1.Clear();
            ly1_x2.Clear();
            ly2_x1.Clear();
            ly2_x2.Clear();

            //int step = listDataCount / 3000;
            //if (step == 0)
            //    step = 1;

            int maxIndex = 0;
            float F1value = 0;
            float tempF1 = 0;
            for (int i = 2; i < listDataCount; i += 1)
            {
                float time = _gdata[i].Ts;
                F1value = _gdata[i].F1;
                float D1value = _gdata[i].D1;
                float YB1value = _gdata[i].YB1;
                float YL1value = _gdata[i].YL1;
                float BX1value = _gdata[i].BX1;
                //找出负荷最大的点
                if (i > 2)
                {
                    if (tempF1 < F1value)
                    {
                        tempF1 = F1value;
                        maxIndex = i;
                    }
                }
                //显示曲线数据
                #region Y1-X1 / Y1-X2 第一、二条曲线
                switch (_Y1)
                {
                    case 1:
                        switch (_X1)
                        {
                            case 1:
                                ly1_x1.Add(new PointF(time, F1value));
                                break;
                            case 2:
                                ly1_x1.Add(new PointF(D1value, F1value));
                                break;
                            case 3:
                                ly1_x1.Add(new PointF(YB1value, F1value));
                                break;
                            case 4:
                                ly1_x1.Add(new PointF(YL1value, F1value));
                                break;
                            case 5:
                                ly1_x1.Add(new PointF(BX1value, F1value));
                                break;
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:
                                ly1_x2.Add(new PointF(time, F1value));
                                break;
                            case 2:
                                ly1_x2.Add(new PointF(D1value, F1value));
                                break;
                            case 3:
                                ly1_x2.Add(new PointF(YB1value, F1value));
                                break;
                            case 4:
                                ly1_x2.Add(new PointF(YL1value, F1value));
                                break;
                            case 5:
                                ly1_x2.Add(new PointF(BX1value, F1value));
                                break;
                        }

                        break;
                    case 2:
                        switch (_X1)
                        {
                            case 1:
                                ly1_x1.Add(new PointF(time, YL1value));
                                break;
                            case 2:
                                ly1_x1.Add(new PointF(D1value, YL1value));
                                break;
                            case 3:
                                ly1_x1.Add(new PointF(YB1value, YL1value));
                                break;
                            case 5:
                                ly1_x1.Add(new PointF(BX1value, YL1value));
                                break;
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:
                                ly1_x2.Add(new PointF(time, YL1value));
                                break;
                            case 2:
                                ly1_x2.Add(new PointF(D1value, YL1value));
                                break;
                            case 3:
                                ly1_x2.Add(new PointF(YB1value, YL1value));
                                break;
                            case 5:
                                ly1_x2.Add(new PointF(BX1value, YL1value));
                                break;
                        }

                        break;
                    case 3:
                        switch (_X1)
                        {
                            case 1:
                                ly1_x1.Add(new PointF(time, BX1value));
                                break;
                            case 2:
                                ly1_x1.Add(new PointF(D1value, BX1value));
                                break;
                            case 3:
                                ly1_x1.Add(new PointF(YB1value, BX1value));
                                break;
                            case 4:
                                ly1_x1.Add(new PointF(YL1value, BX1value));
                                break;
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:
                                ly1_x2.Add(new PointF(time, BX1value));
                                break;
                            case 2:
                                ly1_x2.Add(new PointF(D1value, BX1value));
                                break;
                            case 3:
                                ly1_x2.Add(new PointF(YB1value, BX1value));
                                break;
                            case 4:
                                ly1_x2.Add(new PointF(YL1value, BX1value));
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        break;
                    case 4:
                        switch (_X1)//this.tscbX1.SelectedIndex
                        {
                            case 1:
                                ly1_x1.Add(new PointF(time, D1value));
                                break;
                            case 2:
                                break;
                            case 3:
                                ly1_x1.Add(new PointF(YB1value, D1value));
                                break;
                            case 5:

                                break;
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:
                                ly1_x2.Add(new PointF(time, D1value));
                                break;
                            case 2:
                                break;
                            case 3:
                                ly1_x2.Add(new PointF(YB1value, D1value));
                                break;
                        }
                        break;
                }
                #endregion

                #region Y2-X1 / Y2-X2 第三、四条曲线
                switch (_Y2)//this.tscbY2.SelectedIndex
                {
                    case 1:
                        switch (_X1)//this.tscbX1.SelectedIndex
                        {
                            case 1:
                                ly2_x1.Add(new PointF(time, F1value));
                                break;
                            case 2:
                                ly2_x1.Add(new PointF(D1value, F1value));
                                break;
                            case 3:
                                ly2_x1.Add(new PointF(YB1value, F1value));
                                break;
                            case 4:
                                ly2_x1.Add(new PointF(YL1value, F1value));
                                break;
                            case 5:
                                ly2_x1.Add(new PointF(BX1value, F1value));
                                break;
                        }
                        switch (_X2)
                        {
                            case 1:

                                ly2_x2.Add(new PointF(time, F1value));
                                break;
                            case 2:
                                ly2_x2.Add(new PointF(D1value, F1value));
                                break;
                            case 3:
                                ly2_x2.Add(new PointF(YB1value, F1value));
                                break;
                            case 4:
                                ly2_x2.Add(new PointF(YL1value, F1value));
                                break;
                            case 5:
                                ly2_x2.Add(new PointF(BX1value, F1value));
                                break;
                        }

                        break;
                    case 2:
                        switch (_X1)
                        {
                            case 1:

                                ly2_x1.Add(new PointF(time, YL1value));
                                break;
                            case 2:

                                ly2_x1.Add(new PointF(D1value, YL1value));
                                break;
                            case 3:

                                ly2_x1.Add(new PointF(YB1value, YL1value));
                                break;
                            case 5:
                                ly2_x1.Add(new PointF(BX1value, YL1value));
                                break;

                        }
                        switch (_X2)
                        {
                            case 1:
                                ly2_x2.Add(new PointF(time, YL1value));
                                break;
                            case 2:
                                ly2_x2.Add(new PointF(D1value, YL1value));
                                break;
                            case 3:
                                ly2_x2.Add(new PointF(YB1value, YL1value));
                                break;
                            case 5:
                                ly2_x2.Add(new PointF(BX1value, YL1value));
                                break;
                        }

                        break;
                    case 3:
                        switch (_X1)
                        {
                            case 1:
                                ly2_x1.Add(new PointF(time, BX1value));
                                break;
                            case 2:
                                ly2_x1.Add(new PointF(D1value, BX1value));
                                break;
                            case 3:
                                ly2_x1.Add(new PointF(YB1value, BX1value));
                                break;
                            case 4:
                                ly2_x1.Add(new PointF(YL1value, BX1value));
                                break;
                        }
                        switch (_X2)
                        {
                            case 1:
                                ly2_x2.Add(new PointF(time, BX1value));
                                break;
                            case 2:
                                ly2_x2.Add(new PointF(D1value, BX1value));
                                break;
                            case 3:
                                ly2_x2.Add(new PointF(YB1value, BX1value));
                                break;
                            case 4:
                                ly2_x2.Add(new PointF(YL1value, BX1value));
                                break;
                        }
                        break;
                    case 4:
                        switch (_X1)
                        {
                            case 1:
                                ly2_x1.Add(new PointF(time, D1value));
                                break;
                            case 3:
                                ly2_x1.Add(new PointF(YB1value, D1value));
                                break;
                        }
                        switch (_X2)
                        {
                            case 1:
                                ly2_x2.Add(new PointF(time, D1value));
                                break;
                            case 3:
                                ly2_x2.Add(new PointF(YB1value, D1value));
                                break;
                        }
                        break;
                }
                #endregion

            }

            if (_gdata.Length > 0)
            {
                ChangeChartXY(_gdata[maxIndex]);
                ChangeChartXY(_gdata[listDataCount - 1]);
            }
            _chart.Invalidate();
            _chart.Refresh();
        }

        private void tsbtnShowResultCurve_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == new frmSetResultCurve().ShowDialog())
            {
                ShowResultCurve();
            }
        }

        private void frmTestResult_MaximumSizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
            this.Update();
            this.panel1.Refresh();
        }

        private void frmTestResult_Resize(object sender, EventArgs e)
        {
            this.Refresh();
            this.Update();
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ShowResultPanel();
            this.tsbtn_Start.Enabled = false;
            //if (this.tvTestSample.SelectedNode != null)
            //{
            //    readFinishSample(this.dataGridView, this.tvTestSample.SelectedNode.Text);
            //}
        }

        //选择显示的曲线
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.Tag == null) return;
            switch (dataGridView.Tag.ToString())
            {
                case "GBT23615-2009TensileHeng":
                    m_path = "GBT23615-2009TensileHeng";
                    break;
                case "GBT23615-2009TensileZong":
                    m_path = "GBT23615-2009TensileZong";
                    break;
                case "GBT228-2010":
                    m_path = "GBT228-2010";
                    break;
                case "GBT7314-2005":
                    m_path = "GBT7314-2005";
                    break;
                case "YBT5349-2006":
                    m_path = "YBT5349-2006";
                    break;
                case "GBT28289-2012Tensile":
                    m_path = "GBT28289-2012Tensile";
                    break;

                case "GBT28289-2012Shear":
                    m_path = "GBT28289-2012Shear";
                    break;
                case "GBT28289-2012Twist":
                    m_path = "GBT28289-2012Twist";
                    break;
                case "GBT3354-2014":
                    m_path = "GBT3354-2014";
                    break;
                default:
                    m_path = "";
                    break;
            }

            if (e.RowIndex >= 0 && e.ColumnIndex != 2)
            {
                //选择和取消选择
                if (e.ColumnIndex != 4 && dataGridView.Tag.ToString() == "GBT3354-2014")
                {
                    this.dataGridView.Rows[e.RowIndex].Cells[0].Value = !Convert.ToBoolean(this.dataGridView.Rows[e.RowIndex].Cells[0].Value);
                    this.dataGridView.Rows[e.RowIndex].Selected = Convert.ToBoolean(this.dataGridView.Rows[e.RowIndex].Cells[0].Value);
                }
                else
                {
                    //修改失效模式
                    frmFailureMode ffm = new frmFailureMode();
                    if (DialogResult.OK == ffm.ShowDialog())
                    {
                        BLL.GBT3354_Samples bll3354 = new BLL.GBT3354_Samples();
                        Model.GBT3354_Samples m3354 = bll3354.GetModel(this.dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString());
                        m3354.failuremode = ffm._Mode;
                        if (bll3354.Update(m3354))
                        {
                            this.dataGridView.Rows[e.RowIndex].Cells[4].Value = ffm._Mode;
                        }
                    }
                }
                string selTestSampleNo = string.Empty;
                _selTestSampleArray = GetSelSample();
                //显示曲线 
                //若选择了该行，则添加一条曲线Scale
                if (this.dataGridView.Rows[e.RowIndex].Selected)
                {
                    string colorc = this.dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string sampleNo = this.dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                    LineItem li = _ResultPanel.AddCurve(sampleNo, _RPPList_ReadOne, Color.FromName(colorc), SymbolType.None);//Y1-X1 
                    li.Line.IsAntiAlias = true;
                    m_SelectTestSampleNo = this.dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                    if (_threadReadCurve == null)
                    {
                        _threadReadCurve = new Thread(new ThreadStart(ReadCurveData));
                        _threadReadCurve.Start();
                        _threadReadCurve.IsBackground = true;
                        Thread.Sleep(10);
                        _threadReadCurve = null;
                    }
                }
                else
                {
                    m_FindCurve = this.dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                    this.zedGraphControl.GraphPane.CurveList.RemoveAll(FindAllCurve);
                }
                this.zedGraphControl.RestoreScale(this.zedGraphControl.GraphPane);
                this.zedGraphControl.Refresh();
            }

            if (e.ColumnIndex == 2)
            {
                this.dataGridView.Rows[e.RowIndex].Cells[2].Value = !Convert.ToBoolean(this.dataGridView.Rows[e.RowIndex].Cells[2].Value);
                bool iseffective = Convert.ToBoolean(this.dataGridView.Rows[e.RowIndex].Cells[2].Value);
                string tno = this.dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                if (iseffective)
                {
                    this.dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.IndianRed;
                }
                else
                {
                    this.dataGridView.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
                switch (dataGridView.Tag.ToString())
                {
                    case "GBT23615-2009TensileHeng":
                        //修改状态
                        BLL.GBT236152009_TensileHeng bllheng = new HR_Test.BLL.GBT236152009_TensileHeng();
                        Model.GBT236152009_TensileHeng modheng = bllheng.GetModel(tno);
                        if (modheng != null)
                        {
                            if (iseffective)
                                modheng.isEffective = true;
                            else
                                modheng.isEffective = false;
                            if (bllheng.Update(modheng))
                            {
                                TestStandard.GBT23615_2009.TensileHeng.readFinishSample(null, this.dataGridViewSum, m_TestNo, dateTimePicker, null);
                            }
                        }
                        break;
                    case "GBT23615-2009TensileZong":
                        //修改状态
                        BLL.GBT236152009_TensileZong bllzong = new HR_Test.BLL.GBT236152009_TensileZong();
                        Model.GBT236152009_TensileZong modzong = bllzong.GetModel(tno);
                        if (modzong != null)
                        {
                            if (iseffective)
                                modzong.isEffective = true;
                            else
                                modzong.isEffective = false;
                            if (bllzong.Update(modzong))
                            {
                                TestStandard.GBT23615_2009.TensileZong.readFinishSample(null, this.dataGridViewSum, m_TestNo, dateTimePicker, null);
                            }
                        }
                        break;
                    case "GBT228-2010":
                        //修改状态
                        BLL.TestSample bllt = new HR_Test.BLL.TestSample();
                        Model.TestSample modelt = bllt.GetModel(tno);
                        if (modelt != null)
                        {
                            if (iseffective)
                                modelt.isEffective = true;
                            else
                                modelt.isEffective = false;
                            if (bllt.Update(modelt))
                            {
                                TestStandard.GBT228_2010.readFinishSample(null, this.dataGridViewSum, m_TestNo, dateTimePicker, null);
                            }
                        }
                        break;
                    case "GBT7314-2005":
                        BLL.Compress bllc = new HR_Test.BLL.Compress();
                        Model.Compress modc = bllc.GetModel(tno);
                        if (modc != null)
                        {
                            if (iseffective)
                                modc.isEffective = true;
                            else
                                modc.isEffective = false;
                            if (bllc.Update(modc))
                            {
                                TestStandard.GBT7314_2005.readFinishSample(null, this.dataGridViewSum, m_TestNo, dateTimePicker, null);
                            }
                        }
                        break;
                    case "YBT5349-2006":
                        BLL.Bend bllbend = new HR_Test.BLL.Bend();
                        Model.Bend modbend = bllbend.GetModel(tno);
                        if (modbend != null)
                        {
                            if (iseffective)
                                modbend.isEffective = true;
                            else
                                modbend.isEffective = false;
                            if (bllbend.Update(modbend))
                                TestStandard.YBT5349_2006.readFinishSample(null, this.dataGridViewSum, m_TestNo, this.dateTimePicker, null);
                        }
                        break;
                    case "GBT28289-2012Tensile":
                        BLL.GBT282892012_Tensile bll28289t = new HR_Test.BLL.GBT282892012_Tensile();
                        Model.GBT282892012_Tensile mod28289t = bll28289t.GetModel(tno);
                        if (mod28289t != null)
                        {
                            if (iseffective)
                                mod28289t.isEffective = true;
                            else
                                mod28289t.isEffective = false;
                            if (bll28289t.Update(mod28289t))
                                TestStandard.GBT28289_2012.Tensile.readFinishSample(null, this.dataGridViewSum, m_TestNo, dateTimePicker, null);
                        }
                        break;
                    case "GBT28289-2012Shear":
                        BLL.GBT282892012_Shear bll28289s = new HR_Test.BLL.GBT282892012_Shear();
                        Model.GBT282892012_Shear mod28289s = bll28289s.GetModel(tno);
                        if (mod28289s != null)
                        {
                            if (iseffective)
                                mod28289s.isEffective = true;
                            else
                                mod28289s.isEffective = false;
                            if (bll28289s.Update(mod28289s))
                                TestStandard.GBT28289_2012.Shear.readFinishSample(null, this.dataGridViewSum, m_TestNo, dateTimePicker, null);
                        }

                        break;
                    case "GBT28289-2012Twist":
                        BLL.GBT282892012_Twist bll28289tw = new HR_Test.BLL.GBT282892012_Twist();
                        Model.GBT282892012_Twist mod28289tw = bll28289tw.GetModel(tno);
                        if (mod28289tw != null)
                        {
                            if (iseffective)
                                mod28289tw.isEffective = true;
                            else
                                mod28289tw.isEffective = false;
                            if (bll28289tw.Update(mod28289tw))
                                TestStandard.GBT28289_2012.Twist.readFinishSample(null, this.dataGridViewSum, m_TestNo, dateTimePicker, null);
                        }
                        break;
                    case "GBT3354-2014":
                        BLL.GBT3354_Samples bll3354 = new HR_Test.BLL.GBT3354_Samples();
                        Model.GBT3354_Samples mod3354 = bll3354.GetModel(tno);
                        if (mod3354 != null)
                        {
                            if (iseffective)
                                mod3354.isEffective = true;
                            else
                                mod3354.isEffective = false;
                            if (bll3354.Update(mod3354))
                                TestStandard.GBT3354_2014.readFinishSample(null, this.dataGridViewSum, m_TestNo, dateTimePicker, null);
                        }
                        break;
                    default:
                        m_path = "";
                        break;
                }
            }
        }

        private static string m_FindCurve = string.Empty;
        delegate void readCurve(ZedGraph.ZedGraphControl zedg, string testSampleNo, string path);
        private string m_SelectTestSampleNo = string.Empty;

        private void ReadCurveData()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new readCurve(readCurveName), new object[] { this.zedGraphControl, this.m_SelectTestSampleNo, m_path });
            }
            else
                readCurveName(this.zedGraphControl, this.m_SelectTestSampleNo, m_path);
        }

        private void SaveCurveData(object tmpListData)
        {
            m_mutex.WaitOne();

            gdata[] list = (gdata[])tmpListData;
            string curveName = "E:\\衡新试验数据\\Curve\\" + m_path + "\\" + this.m_TestSampleNo + ".txt";
            if (File.Exists(curveName))
            {
                FileStream fs = new FileStream(curveName, FileMode.Append, FileAccess.Write);
                foreach (gdata gd in list)
                {
                    if (gd.F1 != 0)
                    {
                        utils.AddText(fs, gd.F1 + "," + gd.F2 + "," + gd.F3 + "," + gd.D1 + "," + gd.D2 + "," + gd.D3 + "," + gd.BX1 + "," + gd.BX2 + "," + gd.BX3 + "," + gd.YL1 + "," + gd.YL2 + "," + gd.YL3 + "," + gd.YB1 + "," + gd.YB2 + "," + gd.YB3 + "," + gd.Ts);
                        utils.AddText(fs, "\r\n");
                    }
                }
                fs.Flush();
                fs.Close();
                fs.Dispose();
            }
            ReadTestingData();
            m_mutex.ReleaseMutex();
        }

        // Explicit predicate delegate. 
        private static bool FindAllCurve(CurveItem ci)
        {
            if (ci.Label.Text == m_FindCurve)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void frmTestResult_Validated(object sender, EventArgs e)
        {
            this.Refresh();
        }

        #region 实时曲线相关

        private void ChangeRealTimeXYChart()
        {
            lock (m_state)
            {
                //读取曲线设定
                ReadCurveSet();
                c_axisPenDot.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                c_axisPenSolid.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                //显示实时曲线界面
                ShowCurvePanel();
                //根据曲线X,Y轴设定 初始化实时曲线
                initRealTimeChart();
                InitChartLegend();
                this.pbChart.Invalidate();
            }
        }

        /// <summary>
        /// 坐标轴X分辨率
        /// </summary>
        private float c_X1ScaleFactor
        {
            get { return ((float)(this.pbChart.Width - (1 + c_xAxisCount) * c_kXAxisIndent)) / (c_x1maximum - c_x1minimum); }
        }

        /// <summary>
        /// 坐标轴y分辨率
        /// </summary>
        private float c_Y1ScaleFactor
        {
            get
            {
                return ((float)(this.pbChart.Height - (1 + c_yAxisCount) * c_kYAxisIndent)) / (c_y1maximum - c_y1minimum);
            }
        }

        /// <summary>
        /// x实际值转换为坐标轴像素值
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int c_TranslateX1(float x)
        {
            int tmp = 0;
            tmp = this.pbChart.ClientRectangle.Left + c_yAxisCount * c_kXAxisIndent + (int)(x * c_X1ScaleFactor);
            return tmp;
        }

        /// <summary>
        /// y实际值转换为坐标轴像素值
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        private int c_TranslateY1(float y)
        {
            int tmp = 0;
            tmp = this.pbChart.ClientRectangle.Bottom - ((int)(y * c_Y1ScaleFactor) + c_xAxisCount * c_kYAxisIndent);
            return tmp;
        }

        /// <summary>
        /// 像素值转换为实际值
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int UntranslateX1(int x)
        {
            int tmp = 0;
            tmp = (int)(x / c_X1ScaleFactor) - (this.ClientRectangle.Left + c_yAxisCount * c_kXAxisIndent);
            return tmp;
        }

        /// <summary>
        /// 像素值转换为实际值
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int UntranslateY1(int y)
        {
            int tmp = 0;
            tmp = this.ClientRectangle.Bottom - ((int)(y / c_Y1ScaleFactor) + c_xAxisCount * c_kYAxisIndent);
            return tmp;
        }

        /// <summary>
        /// 坐标轴X分辨率
        /// </summary>
        private float c_X2ScaleFactor
        {
            get
            {
                return ((float)(this.pbChart.Width - (1 + c_xAxisCount) * c_kXAxisIndent)) / (c_x2maximum - c_x2minimum);
            }
        }

        /// <summary>
        /// 坐标轴y分辨率
        /// </summary>
        private float c_Y2ScaleFactor
        {
            get
            {
                return ((float)(this.pbChart.Height - (1 + c_yAxisCount) * c_kYAxisIndent)) / (c_y2maximum - c_y2minimum);
            }
        }

        /// <summary>
        /// x实际值转换为坐标轴像素值
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int c_TranslateX2(float x)
        {
            int tmp = 0;
            tmp = this.pbChart.ClientRectangle.Left + c_yAxisCount * c_kXAxisIndent + (int)(x * c_X2ScaleFactor);
            return tmp;
        }

        /// <summary>
        /// y实际值转换为坐标轴像素值
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        private int c_TranslateY2(float y)
        {
            int tmp = 0;
            tmp = this.pbChart.ClientRectangle.Bottom - ((int)(y * c_Y2ScaleFactor) + c_xAxisCount * c_kYAxisIndent);
            return tmp;
        }

        /// <summary>
        /// 像素值转换为实际值
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int UntranslateX2(int x)
        {
            int tmp = 0;
            tmp = (int)((float)x / c_X2ScaleFactor) - (this.ClientRectangle.Left + c_yAxisCount * c_kXAxisIndent);
            return tmp;
        }

        /// <summary>
        /// 像素值转换为实际值
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int UntranslateY2(int y)
        {
            int tmp = 0;
            tmp = this.ClientRectangle.Bottom - ((int)((float)y / c_Y2ScaleFactor) + c_xAxisCount * c_kYAxisIndent);
            return tmp;
        }

        /// <summary>
        /// 计算数据轴间隔值
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private float CalculateIncrement(float min, float max)
        {
            float increment = (max - min) / _ticksPerAxis;
            return increment;
        }

        private void DrawX1Axis(Graphics g, Pen _axisPen, Brush b)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            Rectangle rec = this.pbChart.ClientRectangle;
            //坐标轴线 + c_yAxisCount * c_kXAxisIndent
            g.DrawLine(_axisPen, rec.Left + 2 * c_kXAxisIndent - c_kXAxisoffset, rec.Bottom - c_xAxisCount * c_kYAxisIndent + c_kYAxisoffset, rec.Right - c_kXAxisIndent, rec.Bottom - c_xAxisCount * c_kYAxisIndent + c_kYAxisoffset);
            //坐标轴名称        
            g.DrawString(m_LabelX1, GraphFont, b, new PointF(rec.Right / 2, rec.Bottom - 2 * c_kYAxisIndent + c_kedu / 2 + c_kYAxisoffset), new StringFormat());
            try
            {
                //计算间隔
                float increment1 = CalculateIncrement(c_x1minimum, c_x1maximum);
                //画坐标轴标签
                // for (float i = _xminimum; i < _xmaximum + (_xmaximum - _xminimum) / _ticksPerAxis; i += increment)
                for (float i = 0; i <= c_x1maximum; i += increment1)
                {
                    float xcalcValue = i;
                    string strCalcValue = string.Empty;
                    if (!isShow103X1)
                    {
                        strCalcValue = xcalcValue.ToString("f1");
                    }
                    else
                    {
                        if (c_x1maximum < 1000.0) strCalcValue = xcalcValue.ToString("f1");
                        if (c_x1maximum >= 1000.0) strCalcValue = xcalcValue.ToString("0,.00");
                    }
                    //画标签
                    //如果是0点，则让标签靠右显示
                    //if(i==0)
                    //    g.DrawString(strCalcValue, GraphFont, b, c_TranslateX1(i), rec.Bottom - 2 * c_kYAxisIndent + c_kedu + c_kYAxisoffset);  
                    //else
                    g.DrawString(strCalcValue, GraphFont, b, c_TranslateX1(i) - (int)GraphFont.GetHeight(), rec.Bottom - 2 * c_kYAxisIndent + c_kYAxisoffset);//+ c_kedu  

                    //画间隔线           
                    if (i == 0 || i == 5 * increment1)
                        g.DrawLine(c_axisPenSolid, c_TranslateX1(i), rec.Bottom - c_xAxisCount * c_kYAxisIndent + c_kYAxisoffset, c_TranslateX1(i), rec.Top + c_kYAxisIndent);
                    else
                        g.DrawLine(c_axisPenDot, c_TranslateX1(i), rec.Bottom - c_xAxisCount * c_kYAxisIndent + c_kYAxisoffset, c_TranslateX1(i), rec.Top + c_kYAxisIndent);
                    //画刻度(主刻度)
                    g.DrawLine(_axisPen, c_TranslateX1(i), rec.Bottom - c_xAxisCount * c_kYAxisIndent + c_kYAxisoffset, c_TranslateX1(i), rec.Bottom - c_xAxisCount * c_kYAxisIndent - c_kedu + c_kYAxisoffset);
                    if (i > 0)//副刻度                        
                        g.DrawLine(_axisPen, c_TranslateX1(i - increment1 / 2), rec.Bottom - c_xAxisCount * c_kYAxisIndent + c_kYAxisoffset, c_TranslateX1(i - increment1 / 2), rec.Bottom - c_xAxisCount * c_kYAxisIndent - c_kedu / 2 + c_kYAxisoffset);
                }
            }
            catch (Exception e)
            {
                g.DrawString(e.Message, GraphFont, b, 10, 10);
            }
        }

        private void DrawX2Axis(Graphics g, Pen _axisPen, Brush b)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            Rectangle rec = this.pbChart.ClientRectangle;
            //坐标轴线
            g.DrawLine(_axisPen, rec.Left + 2 * c_kXAxisIndent - c_kXAxisoffset, rec.Bottom - c_kYAxisIndent + c_kYAxisoffset / 2, rec.Right - c_kXAxisIndent, rec.Bottom - c_kYAxisIndent + c_kYAxisoffset / 2);
            //末端刻度
            g.DrawLine(_axisPen, rec.Left + 2 * c_kXAxisIndent - c_kXAxisoffset, rec.Bottom - c_kYAxisIndent + c_kYAxisoffset / 2, rec.Left + 2 * c_kXAxisIndent - c_kXAxisoffset, rec.Bottom - c_kYAxisIndent + c_kYAxisoffset / 2 - c_kedu / 2);
            //坐标轴名称
            g.DrawString(m_LabelX2, GraphFont, b, new PointF(rec.Right / 2, rec.Bottom - c_kYAxisIndent + c_kedu / 2 + c_kYAxisoffset / 2), new StringFormat());
            try
            {
                float increment2 = CalculateIncrement(c_x2minimum, c_x2maximum);
                for (float i = 0; i <= c_x2maximum; i += increment2)
                {
                    float xcalcValue = i;
                    string strCalcValue = string.Empty;
                    if (!isShow103X2)
                    {
                        strCalcValue = xcalcValue.ToString("f1");
                    }
                    else
                    {
                        if (c_x2maximum < 1000.0) strCalcValue = xcalcValue.ToString("f1");
                        if (c_x2maximum >= 1000.0) strCalcValue = xcalcValue.ToString("0,.00");
                    }
                    //画标签
                    //若是0点则标签靠右显示
                    //if(i==0)
                    //    g.DrawString(strCalcValue, GraphFont, b, c_TranslateX2(i), rec.Bottom - c_kYAxisIndent + c_kedu + c_kYAxisoffset / 2);
                    //else
                    g.DrawString(strCalcValue, GraphFont, b, c_TranslateX2(i) - (int)GraphFont.GetHeight(), rec.Bottom - c_kYAxisIndent + c_kYAxisoffset / 2);// + c_kedu 

                    //画主轴刻度
                    g.DrawLine(_axisPen, c_TranslateX2(i), rec.Bottom - c_kYAxisIndent + c_kYAxisoffset / 2, c_TranslateX2(i), rec.Bottom - c_kYAxisIndent - c_kedu + c_kYAxisoffset / 2);
                    //画副刻度
                    if (i > 0)
                        g.DrawLine(_axisPen, c_TranslateX2(i - increment2 / 2), rec.Bottom - c_kYAxisIndent + c_kYAxisoffset / 2, c_TranslateX2(i - increment2 / 2), rec.Bottom - c_kYAxisIndent - c_kedu / 2 + c_kYAxisoffset / 2);
                    //画间隔线
                    //g.DrawLine(c_axisPenDot, c_TranslateX2(i), rec.Bottom - c_kYAxisIndent, c_TranslateX2(i), rec.Top + c_kYAxisIndent);
                }
            }
            catch (Exception e)
            {
                g.DrawString(e.Message, GraphFont, b, 10, 10);
            }
        }

        private void DrawY1Axis(Graphics g, Pen _axisPen, Brush b)
        {
            Rectangle rec = this.pbChart.ClientRectangle;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            g.DrawLine(_axisPen, rec.Left + 2 * c_kXAxisIndent - c_kXAxisoffset, rec.Top + c_kYAxisIndent, rec.Left + 2 * c_kXAxisIndent - c_kXAxisoffset, rec.Bottom - 2 * c_kYAxisIndent + c_kYAxisoffset);
            //坐标轴延伸线
            //g.DrawLine(c_axisPenDot, 2 * c_kXAxisIndent - c_kXAxisoffset, rec.Bottom, 2 * c_kXAxisIndent - c_kXAxisoffset, rec.Bottom - 2 * c_kYAxisIndent);
            //g.DrawPath(_graphPen, gpY);
            //定义Y轴名称的格式，此处为 文本垂直对齐
            StringFormat theFormat = new StringFormat(StringFormatFlags.DirectionVertical);
            //计算Y轴 名称的大小
            SizeF labelSize = g.MeasureString(m_LabelY1, GraphFont);
            //创建bitmap位图对象 ，大小为Y轴名称标签的尺寸 
            Bitmap stringmap = new Bitmap((int)labelSize.Height + 1, (int)labelSize.Width + 1);
            //获取位图的Graphics,画数据轴名称
            Graphics gbitmap = Graphics.FromImage(stringmap);
            //消除锯齿呈现
            gbitmap.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            //g.DrawString(m_LabelY, GraphFont, Brushes.Blue, new PointF(ClientRectangle.Left , ClientRectangle.Top + 100), theFormat);
            //改变gbitmap坐标原点
            gbitmap.TranslateTransform(0, labelSize.Width);
            //围绕原点旋转-90度
            gbitmap.RotateTransform(-90);
            //填写y轴名称
            gbitmap.DrawString(m_LabelY1, GraphFont, b, new PointF(0, 0));
            //draw the bitmap containing the rotated string to the graph
            g.DrawImage(stringmap, (float)rec.Left + 2 * c_kXAxisIndent - labelSize.Height - 14 - c_kXAxisoffset, (float)(rec.Bottom - c_kYAxisIndent - labelSize.Width) / 2);//- c_kedu
            float increment = CalculateIncrement(c_y1minimum, c_y1maximum);
            //for (float i = _yminimum; i < _ymaximum + (_ymaximum - _yminimum) / _ticksPerAxis; i += increment)
            for (float i = 0; i <= c_y1maximum; i += increment)
            {
                float ycalcValue = i;
                string strCalcValue = string.Empty;

                if (!isShow103Y1)
                {
                    strCalcValue = ycalcValue.ToString("f1");
                }
                else
                {
                    if (c_y1maximum < 1000.0) strCalcValue = ycalcValue.ToString("f1");
                    if (c_y1maximum >= 1000.0) strCalcValue = ycalcValue.ToString("0,.00");
                }
                //画标签
                //计算Y轴 名称的大小
                SizeF lblYSize = g.MeasureString(strCalcValue, GraphFont);
                //创建bitmap位图对象 ，大小为Y轴名称标签的尺寸 
                Bitmap lblYMap = new Bitmap((int)lblYSize.Height + 1, (int)lblYSize.Width + 1);
                //获取位图的Graphics,画数据轴名称
                Graphics gbmplblY = Graphics.FromImage(lblYMap);
                //消除锯齿呈现
                gbmplblY.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                //g.DrawString(m_LabelY, GraphFont, Brushes.Blue, new PointF(ClientRectangle.Left , ClientRectangle.Top + 100), theFormat);
                //改变gbitmap坐标原点
                gbmplblY.TranslateTransform(0, lblYSize.Width);
                //围绕原点旋转-90度
                gbmplblY.RotateTransform(-90);
                //填写y轴名称
                gbmplblY.DrawString(strCalcValue, GraphFont, b, new PointF(0, 0), new StringFormat(StringFormatFlags.NoClip));
                //画标签
                //若为0点则标签靠上显示
                //if(i==0)
                //    g.DrawImage(lblYMap, rec.Left + (c_yAxisCount * c_kXAxisIndent - lblYSize.Height-2) - c_kXAxisoffset, c_TranslateY1(i) - 30);
                //else
                g.DrawImage(lblYMap, rec.Left + (c_yAxisCount * c_kXAxisIndent - lblYSize.Height) - c_kXAxisoffset, c_TranslateY1(i) - 18);
                //g.DrawString(strCalcValue, _axisFont, _axisNumbersBrush, this.ClientRectangle.Left + kXAxisIndent/3 , TranslateY(i));

                //画间隔线 
                if (i == 0 || i == 5 * increment)
                    g.DrawLine(c_axisPenSolid, rec.Left + 2 * c_kXAxisIndent - c_kXAxisoffset, c_TranslateY1(i), rec.Right - c_kXAxisIndent, c_TranslateY1(i));
                else
                    g.DrawLine(c_axisPenDot, rec.Left + 2 * c_kXAxisIndent - c_kXAxisoffset, c_TranslateY1(i), rec.Right - c_kXAxisIndent, c_TranslateY1(i));
                //画主刻度线
                g.DrawLine(_axisPen, rec.Left + 2 * c_kXAxisIndent - c_kXAxisoffset, c_TranslateY1(i), rec.Left + 2 * c_kXAxisIndent + c_kedu - c_kXAxisoffset, c_TranslateY1(i));
                if (i > 0)//画副刻度线                    
                    g.DrawLine(_axisPen, rec.Left + 2 * c_kXAxisIndent - c_kXAxisoffset, c_TranslateY1(i - increment / 2), rec.Left + 2 * c_kXAxisIndent + c_kedu / 2 - c_kXAxisoffset, c_TranslateY1(i - increment / 2));
                gbmplblY.Dispose();
                lblYMap.Dispose();
            }
            gbitmap.Dispose();
            stringmap.Dispose();
        }

        private void DrawY2Axis(Graphics g, Pen _axisPen, Brush b)
        {
            Rectangle rec = this.pbChart.ClientRectangle;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            //画数据轴
            g.DrawLine(_axisPen, rec.Left + c_kXAxisIndent - c_kXAxisoffset / 2, rec.Top + c_kYAxisIndent, rec.Left + c_kXAxisIndent - c_kXAxisoffset / 2, rec.Bottom - 2 * c_kYAxisIndent + c_kYAxisoffset);
            //画末端刻度线
            g.DrawLine(_axisPen, rec.Left + c_kXAxisIndent - c_kXAxisoffset / 2, rec.Bottom - 2 * c_kYAxisIndent + c_kYAxisoffset, rec.Left + c_kXAxisIndent - c_kXAxisoffset / 2 + c_kedu / 2, rec.Bottom - 2 * c_kYAxisIndent + c_kYAxisoffset);
            //g.DrawPath(_graphPen, gpY);
            //定义Y轴名称的格式，此处为 文本垂直对齐
            StringFormat theFormat = new StringFormat(StringFormatFlags.DirectionVertical);
            //计算Y轴 名称的大小
            SizeF labelSize = g.MeasureString(m_LabelY2, GraphFont);
            //创建bitmap位图对象 ，大小为Y轴名称标签的尺寸 
            Bitmap stringmap = new Bitmap((int)labelSize.Height + 1, (int)labelSize.Width + 1);
            //获取位图的Graphics,画数据轴名称
            Graphics gbitmap = Graphics.FromImage(stringmap);
            //消除锯齿呈现
            gbitmap.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            //g.DrawString(m_LabelY, GraphFont, Brushes.Blue, new PointF(ClientRectangle.Left , ClientRectangle.Top + 100), theFormat);
            //改变gbitmap坐标原点
            gbitmap.TranslateTransform(0, labelSize.Width);
            //围绕原点旋转-90度
            gbitmap.RotateTransform(-90);
            //填写y轴名称
            gbitmap.DrawString(m_LabelY2, GraphFont, b, new PointF(0, 0));
            //draw the bitmap containing the rotated string to the graph
            g.DrawImage(stringmap, (float)rec.Left + c_kXAxisIndent - labelSize.Height - 14 - c_kXAxisoffset / 2, (float)(rec.Bottom - c_kYAxisIndent - labelSize.Width) / 2); //- c_kedu
            float increment = CalculateIncrement(c_y2minimum, c_y2maximum);
            //for (float i = _yminimum; i < _ymaximum + (_ymaximum - _yminimum) / _ticksPerAxis; i += increment)
            for (float i = 0; i <= c_y2maximum; i += increment)
            {
                float ycalcValue = i;
                string strCalcValue = string.Empty;

                if (!isShow103Y2)
                {
                    strCalcValue = ycalcValue.ToString("f1");
                }
                else
                {
                    if (c_y2maximum < 1000.0) strCalcValue = ycalcValue.ToString("f1");
                    if (c_y2maximum >= 1000.0) strCalcValue = ycalcValue.ToString("0,.00");
                }
                //画标签
                //计算Y轴 名称的大小
                SizeF lblYSize = g.MeasureString(strCalcValue, GraphFont);
                //创建bitmap位图对象 ，大小为Y轴名称标签的尺寸 
                Bitmap lblYMap = new Bitmap((int)lblYSize.Height + 1, (int)lblYSize.Width + 1);
                //获取位图的Graphics,画数据轴名称
                Graphics gbmplblY = Graphics.FromImage(lblYMap);
                gbmplblY.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                //g.DrawString(m_LabelY, GraphFont, Brushes.Blue, new PointF(ClientRectangle.Left , ClientRectangle.Top + 100), theFormat);
                //改变gbitmap坐标原点
                gbmplblY.TranslateTransform(0, lblYSize.Width);
                //围绕原点旋转-90度
                gbmplblY.RotateTransform(-90);
                //填写y轴名称
                gbmplblY.DrawString(strCalcValue, GraphFont, b, new PointF(0, 0), new StringFormat(StringFormatFlags.NoClip));
                //画标签若为0点则靠上显示
                //if(i==0)
                //    g.DrawImage(lblYMap, rec.Left + c_kXAxisIndent - lblYSize.Height-2 - c_kXAxisoffset / 2, c_TranslateY2(i) - 30);
                //else
                g.DrawImage(lblYMap, rec.Left + c_kXAxisIndent - lblYSize.Height - c_kXAxisoffset / 2, c_TranslateY2(i) - 18);
                //g.DrawString(strCalcValue, _axisFont, _axisNumbersBrush, this.ClientRectangle.Left + kXAxisIndent/3 , TranslateY(i));
                //画间隔线 
                //if (i > 0)
                //    g.DrawLine(c_axisPenDot, rec.Left + 2 * c_kXAxisIndent, c_TranslateY1(i), rec.Right - c_kXAxisIndent, c_TranslateY1(i));
                //画主刻度线
                g.DrawLine(_axisPen, rec.Left + c_kXAxisIndent - c_kXAxisoffset / 2, c_TranslateY2(i), rec.Left + c_kXAxisIndent + c_kedu - c_kXAxisoffset / 2, c_TranslateY2(i));
                //画副刻度
                if (i > 0)
                    g.DrawLine(_axisPen, rec.Left + c_kXAxisIndent - c_kXAxisoffset / 2, c_TranslateY2(i - increment / 2), rec.Left + c_kXAxisIndent + c_kedu / 2 - c_kXAxisoffset / 2, c_TranslateY2(i - increment / 2));
                gbmplblY.Dispose();
                lblYMap.Dispose();
            }
            gbitmap.Dispose();
            stringmap.Dispose();
        }

        /// <summary>
        /// 画曲线Legend
        /// </summary>
        /// <param name="g"></param>
        private void DrawLegend(Graphics g)
        {
            Rectangle rec = this.pbChart.ClientRectangle;
            SizeF size = g.MeasureString("负荷-位移", GraphFont);
            float w = size.Width;
            float h = size.Height / 2 - 1;
            PointF currentp = new PointF(rec.Left + 2 * c_kXAxisIndent, rec.Top + 5);

            if (!string.IsNullOrEmpty(y1_x1))
            {
                g.DrawLine(py1_x1, new PointF(currentp.X, currentp.Y + h), new PointF(currentp.X + 25, currentp.Y + h));
                currentp.X += 25;
                g.DrawString(y1_x1, GraphFont, Brushes.Purple, new PointF(currentp.X, currentp.Y));
                currentp.X += size.Width;
                currentp.X += 20;
            }
            if (!string.IsNullOrEmpty(y1_x2))
            {
                g.DrawLine(py1_x2, new PointF(currentp.X, currentp.Y + h), new PointF(currentp.X + 25, currentp.Y + h));
                currentp.X += 25;
                g.DrawString(y1_x2, GraphFont, Brushes.Crimson, new PointF(currentp.X, currentp.Y));
                currentp.X += size.Width;
                currentp.X += 20;
            }
            if (!string.IsNullOrEmpty(y2_x1))
            {
                g.DrawLine(py2_x1, new PointF(currentp.X, currentp.Y + h), new PointF(currentp.X + 25, currentp.Y + h));
                currentp.X += 25;
                g.DrawString(y2_x1, GraphFont, Brushes.Blue, new PointF(currentp.X, currentp.Y));
                currentp.X += size.Width;
                currentp.X += 20;
            }
            if (!string.IsNullOrEmpty(y2_x2))
            {
                g.DrawLine(py2_x2, new PointF(currentp.X, currentp.Y + h), new PointF(currentp.X + 25, currentp.Y + h));
                currentp.X += 25;
                g.DrawString(y2_x2, GraphFont, Brushes.DarkGreen, new PointF(currentp.X, currentp.Y));
                currentp.X += size.Width;
                currentp.X += 20;
            }
        }

        /// <summary>
        /// 初始化各轴名称及初始轴最小最大值
        /// </summary>
        private void initRealTimeChart()
        {
            py1_x1.Width = 2f;
            py2_x1.Width = 2f;
            py1_x2.Width = 2f;
            py2_x2.Width = 2f;
            x1Pen.Width = 1f;
            x2Pen.Width = 1f;
            y1Pen.Width = 1f;
            y2Pen.Width = 1f;
            ly1_x1.Clear();
            ly1_x2.Clear();
            ly2_x1.Clear();
            ly2_x2.Clear();

            #region 初始化坐标轴最大最小值

            switch (_X1)
            {//1时间，2位移，3应变，4应力,5变形
                case 1:
                    c_x1maximum = 50.0f;
                    m_LabelX1 = "时间(s)";
                    c_isshowx1 = true;
                    break;
                case 2:
                    c_x1maximum = 200.0f;
                    m_LabelX1 = "位移(μm)";
                    c_isshowx1 = true;
                    break;
                case 3:
                    c_x1maximum = 1.0f;
                    m_LabelX1 = "应变(%)";
                    c_isshowx1 = true;
                    break;
                case 4:
                    c_x1maximum = 100.0f;
                    m_LabelX1 = "应力(MPa)";
                    c_isshowx1 = true;
                    break;
                case 5:
                    c_x1maximum = 100.0f;
                    m_LabelX1 = "变形(μm)";
                    c_isshowx1 = true;
                    break;
                default:
                    c_x1maximum = 1.0f;
                    m_LabelX1 = "";
                    c_isshowx1 = false;
                    break;
            }

            switch (_X2)
            {//1时间，2位移，3应变，4应力,5变形
                case 1:
                    c_x2maximum = 50.0f;
                    m_LabelX2 = "时间(s)";
                    c_isshowx2 = true;
                    break;
                case 2:
                    c_x2maximum = 200.0f;
                    m_LabelX2 = "位移(μm)";
                    c_isshowx2 = true;
                    break;
                case 3:
                    c_x2maximum = 1.0f;
                    m_LabelX2 = "应变(%)";
                    c_isshowx2 = true;
                    break;
                case 4:
                    c_x2maximum = 100.0f;
                    m_LabelX2 = "应力(MPa)";
                    c_isshowx2 = true;
                    break;
                case 5:
                    c_x2maximum = 100.0f;
                    m_LabelX2 = "变形(μm)";
                    c_isshowx2 = true;
                    break;
                default:
                    c_x2maximum = 1.0f;
                    m_LabelX2 = "";
                    c_isshowx2 = false;
                    break;
            }

            switch (_Y1)
            {//负荷,应力,变形,位移,应变
                case 1:
                    if (m_LoadSensorCount > 0)
                        c_y1maximum = GetScale((ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale) / 100;
                    else
                        c_y1maximum = 500.0f;
                    if (c_y1maximum > 1000.0f)
                        c_y1maximum = 800.0f;
                    m_LabelY1 = "负荷(N)";
                    c_isshowy1 = true;
                    break;
                case 2:
                    c_y1maximum = 100.0f;
                    m_LabelY1 = "应力(MPa)";
                    c_isshowy1 = true;
                    break;
                case 3:
                    if (m_ElongateSensorCount > 0)
                        c_y1maximum = GetScale((ushort)m_SensorArray[m_ESensorArray[0].SensorIndex].scale) / 50;
                    else
                        c_y1maximum = 100.0f;
                    m_LabelY1 = "变形(μm)";
                    c_isshowy1 = true;
                    break;
                case 4:
                    c_y1maximum = 200.0f;
                    m_LabelY1 = "位移(μm)";
                    c_isshowy1 = true;
                    break;
                case 5:
                    c_y1maximum = 1.0f;
                    m_LabelY1 = "应变(%)";
                    c_isshowy1 = true;
                    break;
                default:
                    c_y1maximum = 1.0f;
                    m_LabelY1 = "";
                    c_isshowy1 = false;
                    break;
            }

            switch (_Y2)
            {//负荷,应力,变形,位移,应变
                case 1:
                    if (m_LoadSensorCount > 0)
                        c_y2maximum = GetScale((ushort)m_SensorArray[m_LSensorArray[0].SensorIndex].scale) / 100;
                    else
                        c_y2maximum = 500.0f;
                    if (c_y2maximum > 1000.0f)
                        c_y2maximum = 800.0f;
                    m_LabelY2 = "负荷(N)";
                    c_isshowy2 = true;
                    break;
                case 2:
                    c_y2maximum = 100.0f;
                    m_LabelY2 = "应力(MPa)";
                    c_isshowy2 = true;
                    break;
                case 3:
                    if (m_ElongateSensorCount > 0)
                        c_y2maximum = GetScale((ushort)m_SensorArray[m_ESensorArray[0].SensorIndex].scale) / 50;
                    else
                        c_y2maximum = 100.0f;
                    m_LabelY2 = "变形(μm)";
                    c_isshowy2 = true;
                    break;
                case 4:
                    c_y2maximum = 200.0f;
                    m_LabelY2 = "位移(μm)";
                    c_isshowy2 = true;
                    break;
                case 5:
                    c_y2maximum = 1.0f;
                    m_LabelY2 = "应变(%)";
                    c_isshowy2 = true;
                    break;
                default:
                    c_y2maximum = 1.0f;
                    m_LabelY2 = "";
                    c_isshowy2 = false;
                    break;
            }
            #endregion
        }

        /// <summary>
        /// 曲线Legend： x1-y1 x1-y2 x2-y2 x2-y1 的显示名称
        /// </summary>

        private void InitChartLegend()
        {

            //          //Y1    0-请选择-    1力,kN       2应力,MPa      3变形,μm    4位移,μm
            //          //Y2    0-请选择-    1力,kN       2应力,MPa      3变形,μm    4位移,μm

            //          //X1    0-请选择-    1时间,s      2位移,μm       3应变,%     4应力,MPa  5变形,μm
            //          //X2    0-请选择-    1时间,s      2位移,μm       3应变,%     4应力,MPa  5变形,μm

            //总共 4 条曲线
            //Y1-X1 Y1-X2 Y2-X1 Y2-X2;

            #region Y1-X1 / Y1-X2 第一、二条曲线
            switch (_Y1)
            {
                case 1:
                    //y1-x1
                    switch (_X1)
                    {
                        case 1:
                            y1_x1 = "负荷-时间";
                            //修改轴的初始值
                            break;
                        case 2:
                            y1_x1 = "负荷-位移";
                            break;
                        case 3:
                            y1_x1 = "负荷-应变";
                            break;
                        case 4:
                            y1_x1 = "负荷-应力";
                            break;
                        case 5:
                            y1_x1 = "负荷-变形";
                            break;
                        default:
                            y1_x1 = "";
                            break;
                    }

                    //y1-x2
                    switch (_X2)
                    {
                        case 1:
                            y1_x2 = "负荷-时间";
                            break;
                        case 2:
                            y1_x2 = "负荷-位移";
                            break;
                        case 3:
                            y1_x2 = "负荷-应变";
                            break;
                        case 4:
                            y1_x2 = "负荷-应力";
                            break;
                        case 5:
                            y1_x2 = "负荷-变形";
                            break;
                        default:
                            y1_x2 = "";
                            break;
                    }

                    break;

                case 2:
                    //y1-x1
                    switch (_X1)
                    {
                        case 1:
                            y1_x1 = "应力-时间";
                            break;
                        case 2:
                            y1_x1 = "应力-位移";
                            break;
                        case 3:
                            y1_x1 = "应力-应变";
                            break;
                        case 4:
                            y1_x1 = "";
                            break;
                        case 5:
                            y1_x1 = "应力-变形";
                            break;
                        default:
                            y1_x1 = "";
                            break;
                    }
                    //y1-x2
                    switch (_X2)
                    {
                        case 1:
                            y1_x2 = "应力-时间";
                            break;
                        case 2:
                            y1_x2 = "应力-位移";
                            break;
                        case 3:
                            y1_x2 = "应力-应变";
                            break;
                        case 4:
                            y1_x2 = "";
                            break;
                        case 5:
                            y1_x2 = "应力-变形";
                            break;
                        default:
                            y1_x2 = "";
                            break;
                    }

                    break;


                case 3:
                    //y1-x1
                    switch (_X1)
                    {
                        case 1:
                            y1_x1 = "变形-时间";
                            break;
                        case 2:
                            y1_x1 = "变形-位移";
                            break;
                        case 3:
                            y1_x1 = "变形-应变";
                            break;
                        case 4:
                            y1_x1 = "变形-应力";
                            break;
                        default:
                            y1_x1 = "";
                            break;
                    }
                    //y1-x2
                    switch (_X2)
                    {
                        case 1:
                            y1_x2 = "变形-时间";
                            break;
                        case 2:
                            y1_x2 = "变形-位移";
                            break;
                        case 3:
                            y1_x2 = "变形-应变";
                            break;
                        case 4:
                            y1_x2 = "变形-应力";
                            break;
                        default:
                            y1_x2 = "";
                            break;
                    }
                    break;
                case 4:
                    //y1-x1
                    switch (_X1)
                    {
                        case 1:
                            y1_x1 = "位移-时间";
                            break;
                        case 2:
                            y1_x1 = "";
                            break;
                        case 3:
                            y1_x1 = "位移-应变";
                            break;
                        case 4:
                            y1_x1 = "位移-应力";
                            break;
                        default:
                            y1_x1 = "";
                            break;
                    }

                    //y1-x2
                    switch (_X2)
                    {
                        case 1:
                            y1_x2 = "位移-时间";
                            break;
                        case 2:
                            y1_x2 = "";
                            break;
                        case 3:
                            y1_x2 = "位移-应变";
                            break;
                        case 4:
                            y1_x2 = "位移-应力";
                            break;
                        default:
                            y1_x2 = "";
                            break;
                    }
                    break;
                case 5:
                    //y1_x1 y1为应变
                    switch (_X1)
                    {
                        case 1:
                            y1_x1 = "应变-时间";
                            break;
                        case 2:
                            y1_x1 = "";
                            break;
                        case 3:
                            y1_x1 = "应变-应变";
                            break;
                        case 4:
                            y1_x1 = "应变-应力";
                            break;
                        default:
                            y1_x1 = "";
                            break;
                    }

                    //y1-x2
                    switch (_X2)
                    {
                        case 1:
                            y1_x2 = "应变-时间";
                            break;
                        case 2:
                            y1_x2 = "";
                            break;
                        case 3:
                            y1_x2 = "应变-应变";
                            break;
                        case 4:
                            y1_x2 = "应变-应力";
                            break;
                        default:
                            y1_x2 = "";
                            break;
                    }
                    break;
                default:
                    y1_x1 = "";
                    y1_x2 = "";
                    break;
            }
            #endregion

            #region Y2-X1 / Y2-X2 第三、四条曲线
            switch (_Y2)
            {
                case 1:
                    //y2-x1
                    switch (_X1)
                    {
                        case 1: y2_x1 = "负荷-时间"; break;
                        case 2: y2_x1 = "负荷-位移"; break;
                        case 3: y2_x1 = "负荷-应变"; break;
                        case 4: y2_x1 = "负荷-应力"; break;
                        case 5: y2_x1 = "负荷-变形"; break;
                        default: y2_x1 = ""; break;
                    }
                    //y2-x2
                    switch (_X2)
                    {
                        case 1: y2_x2 = "负荷-时间"; break;
                        case 2: y2_x2 = "负荷-位移"; break;
                        case 3: y2_x2 = "负荷-应变"; break;
                        case 4: y2_x2 = "负荷-应力"; break;
                        case 5: y2_x2 = "负荷-变形"; break;
                        default: y2_x2 = ""; break;
                    }

                    break;
                case 2:
                    //y2-x1
                    switch (_X1)
                    {
                        case 1: y2_x1 = "应力-时间"; break;
                        case 2: y2_x1 = "应力-位移"; break;
                        case 3: y2_x1 = "应力-应变"; break;
                        case 4: y2_x1 = "应力-应力"; break;
                        case 5: y2_x1 = "应力-变形"; break;
                        default: y2_x1 = ""; break;
                    }
                    //y2-x2
                    switch (_X2)
                    {
                        case 1: y2_x2 = "应力-时间"; break;
                        case 2: y2_x2 = "应力-位移"; break;
                        case 3: y2_x2 = "应力-应变"; break;
                        case 4: y2_x2 = ""; break;
                        case 5: y2_x2 = "应力-变形"; break;
                        default: y2_x2 = ""; break;
                    }

                    break;
                case 3:
                    //y2-x1
                    switch (_X1)
                    {
                        case 1: y2_x1 = "变形-时间"; break;
                        case 2: y2_x1 = "变形-位移"; break;
                        case 3: y2_x1 = "变形-应变"; break;
                        case 4: y2_x1 = "变形-应力"; break;
                        default: y2_x1 = ""; break;
                    }
                    //y2-x2
                    switch (_X2)
                    {
                        case 1: y2_x2 = "变形-时间"; break;
                        case 2: y2_x2 = "变形-位移"; break;
                        case 3: y2_x2 = "变形-应变"; break;
                        case 4: y2_x2 = "变形-应力"; break;
                        default: y2_x2 = ""; break;
                    }
                    break;
                case 4:
                    //y2-x1
                    switch (_X1)
                    {
                        case 1: y2_x1 = "位移-时间"; break;
                        case 2: y2_x1 = ""; break;
                        case 3: y2_x1 = "位移-应变"; break;
                        case 4: y2_x1 = "位移-应力"; break;
                        default: y2_x1 = ""; break;
                    }
                    //y2-x2
                    switch (_X2)
                    {
                        case 1: y2_x2 = "位移-时间"; break;
                        case 2: y2_x2 = ""; break;
                        case 3: y2_x2 = "位移-应变"; break;
                        case 4: y2_x2 = "位移-应力"; break;
                        default: y2_x2 = ""; break;
                    }
                    break;
                case 5:
                    //y2-x1
                    switch (_X1)
                    {
                        case 1: y2_x1 = "应变-时间"; break;
                        case 2: y2_x1 = ""; break;
                        case 3: y2_x1 = "应变-应变"; break;
                        case 4: y2_x1 = "应变-应力"; break;
                        default: y2_x1 = ""; break;
                    }
                    //y2-x2
                    switch (_X2)
                    {
                        case 1: y2_x2 = "应变-时间"; break;
                        case 2: y2_x2 = ""; break;
                        case 3: y2_x2 = "应变-应变"; break;
                        case 4: y2_x2 = "应变-应力"; break;
                        default: y2_x2 = ""; break;
                    }
                    break;
                default:
                    y2_x1 = "";
                    y2_x2 = "";
                    break;
            }
            #endregion
        }

        /// <summary>
        /// 绘制曲线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbChart_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            #region 画各坐标轴
            if (c_isshowy1)
            {
                if (c_isshowx1)
                    DrawX1Axis(g, x1Pen, Brushes.Blue);
                if (c_isshowx2)
                    DrawX2Axis(g, x2Pen, Brushes.Crimson);
            }
            else
            {
                SizeF s = g.MeasureString("无显示曲线,请点击“曲线显示”进行设置!", GraphFont);
                g.DrawString("无显示曲线,请点击“曲线显示”进行设置!", GraphFont, Brushes.Crimson, new Point((int)(this.pbChart.ClientRectangle.Right - s.Width) / 2, this.pbChart.ClientRectangle.Bottom / 2));
            }
            if (c_isshowx1)
            {
                if (c_isshowy1)
                    DrawY1Axis(g, y1Pen, Brushes.Purple);
                if (c_isshowy2)
                    DrawY2Axis(g, y2Pen, Brushes.DarkGreen);
            }
            else
            {
                SizeF s = g.MeasureString("无显示曲线,请点击“曲线显示”进行设置!", GraphFont);
                g.DrawString("无显示曲线,请点击“曲线显示”进行设置!", GraphFont, Brushes.Crimson, new Point((int)(this.pbChart.ClientRectangle.Right - s.Width) / 2, this.pbChart.ClientRectangle.Bottom / 2));
            }

            DrawLegend(g);
            #endregion

            #region 第1条曲线
            if (ly1_x1.Count > 0)
            {
                int count = ly1_x1.Count;
                //自动截取3000个点
                //int step = count / 3000;
                //if (step == 0) step = 1;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                for (int i = 1; i < count; i++)
                {
                    g.DrawLine(py1_x1, c_TranslateX1(ly1_x1[i - 1].X), c_TranslateY1(ly1_x1[i - 1].Y), c_TranslateX1(ly1_x1[i].X), c_TranslateY1(ly1_x1[i].Y));
                }
            }
            #endregion

            #region 第2条曲线
            if (ly1_x2.Count > 0)
            {
                int count = ly1_x2.Count;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //自动截取3000个点
                //int step = count / 3000;
                //if (step == 0) step = 1;
                for (int i = 1; i < count; i++)
                {
                    g.DrawLine(py1_x2, c_TranslateX2(ly1_x2[i - 1].X), c_TranslateY1(ly1_x2[i - 1].Y), c_TranslateX2(ly1_x2[i].X), c_TranslateY1(ly1_x2[i].Y));
                }
            }
            #endregion

            #region 第3条曲线
            if (ly2_x1.Count > 0)
            {
                int count = ly2_x1.Count;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //自动截取3000个点
                //int step = count / 3000;
                //if (step == 0) step = 1;
                for (int i = 1; i < count; i++)
                {
                    g.DrawLine(py2_x1, c_TranslateX1(ly2_x1[i - 1].X), c_TranslateY2(ly2_x1[i - 1].Y), c_TranslateX1(ly2_x1[i].X), c_TranslateY2(ly2_x1[i].Y));
                }
            }
            #endregion

            #region 第4条曲线
            if (ly2_x2.Count > 0)
            {
                int count = ly2_x2.Count;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //自动截取3000个点
                //int step = count / 3000;
                //if (step == 0) step = 1;
                for (int i = 1; i < count; i++)
                {
                    g.DrawLine(py2_x2, c_TranslateX2(ly2_x2[i - 1].X), c_TranslateY2(ly2_x2[i - 1].Y), c_TranslateX2(ly2_x2[i].X), c_TranslateY2(ly2_x2[i].Y));
                }
            }
            #endregion

            #region 画Rp线和标注 , 如果试验结果有Rp
            if (!isTest && m_FrIndex != 0 && m_calSuccess && m_isSelRp)
            {
                DrawResult(g, m_l, m_k, m_a, m_fr05index, m_fr01index);
            }

            #endregion

            #region 画试验结果
            if (isShowResult)
            {
                StringBuilder strResult = new StringBuilder();
                switch (dataGridView.Tag.ToString())
                {
                    case "GBT228-2010":
                        if (m_l.Count == 0) return;
                        double dFm = Convert.ToDouble((m_l[m_FmIndex].F1).ToString("G5"));
                        double dRm = Convert.ToDouble(m_l[m_FmIndex].YL1.ToString("G5"));

                        if (m_isSelFm)
                        {
                            if (dFm < 1000.0d)
                                strResult.Append("Fm:" + dFm.ToString() + " N\r\n");
                            if (dFm >= 1000.0d)
                                strResult.Append("Fm:" + (dFm / 1000.0d).ToString("G5") + " kN\r\n");
                        }

                        if (m_isSelRm)
                            strResult.Append("Rm:" + dRm.ToString() + " MPa\r\n");
                        if (m_isSelReH)
                            strResult.Append("ReH:" + Convert.ToDouble((m_FRH / m_S0).ToString("G5")).ToString() + " MPa\r\n");
                        if (m_isSelReL)
                            strResult.Append("ReL:" + Convert.ToDouble((m_FRL / m_S0).ToString("G5")) + " MPa\r\n");
                        if (m_isSelRp)
                            strResult.Append("Rp:" + m_Rp + " MPa\r\n");
                        if (m_isSelE)
                            strResult.Append("E:" + m_Eb + " GPa\r\n");
                        break;
                    case "GBT28289-2012Tensile":
                        if (m_Fmax < 1000.0d)
                            strResult.Append("FQmax:" + m_Fmax + " N\r\n" + "Q:" + m_Rp + " N/mm");
                        else
                            strResult.Append("FQmax:" + (m_Fmax / 1000.0).ToString("G6") + " kN\r\n" + "Q:" + m_Rp + " N/mm");
                        break;
                    case "GBT28289-2012Shear":
                        if (m_Fmax < 1000.0d)
                            strResult.Append("FTmax:" + m_Fmax + " N\r\n" + "T:" + m_Rp + " N/mm");
                        else
                            strResult.Append("FTmax:" + (m_Fmax / 1000.0).ToString("G6") + " kN\r\n" + "T:" + m_Rp + " N/mm");
                        break;
                    case "GBT28289-2012Twist":
                        if (m_Fmax < 1000.0d)
                            strResult.Append("FMmax:" + m_Fmax + " N\r\n" + "M:" + m_Rp + " kN·mm");
                        else
                            strResult.Append("FMmax:" + (m_Fmax / 1000.0).ToString("G6") + " kN\r\n" + "M:" + m_Rp + " kN·mm");
                        break;
                    case "GBT23615-2009TensileZong":
                        if (m_Fmax < 1000.0d)
                            strResult.Append("Fmax:" + m_Fmax + " N\r\n" + "T2:" + m_Rp + " MPa");
                        else
                            strResult.Append("Fmax:" + (m_Fmax / 1000.0).ToString("G6") + " kN\r\n" + "T2:" + m_Rp + " MPa");
                        break;
                    case "GBT23615-2009TensileHeng":
                        if (m_Fmax < 1000.0d)
                            strResult.Append("Fmax:" + m_Fmax + " N\r\n" + "T1:" + m_Rp + " MPa");
                        else
                            strResult.Append("Fmax:" + (m_Fmax / 1000.0).ToString("G6") + " kN\r\n" + "T1:" + m_Rp + " MPa");
                        break;
                    case "YBT5349-2006":
                        if (m_isSelFbb)
                        {
                            if (m_Fbb < 1000.0d)
                                strResult.Append("Fbb:" + m_Fbb + " N\r\n");
                            else
                                strResult.Append("Fbb:" + (m_Fbb / 1000.0).ToString("G6") + " kN\r\n");
                        }
                        if (m_isSelσbb)
                        {
                            strResult.Append("σbb:" + m_σbb + " MPa\r\n");
                        }

                        if (m_isSelEb)
                        {
                            strResult.Append("Eb:" + m_Eb + " MPa\r\n");
                        }

                        //规定非比例弯曲力
                        if (m_isSelFpb)
                        {
                            if (m_Fpb < 1000.0)
                                strResult.Append("Fpb:" + m_Fpb + " N\r\nσpb:" + m_Rp);
                            else
                                strResult.Append("Fpb:" + (m_Fpb / 1000.0).ToString("G6") + " kN\r\nσpb:" + m_Rp);
                        }
                        break;
                    case "GBT7314-2005":
                        double dFmc = Convert.ToDouble((m_l[m_FmaxIndex].F1).ToString("f2"));
                        double dRmc = Convert.ToDouble(m_l[m_FmaxIndex].YL1.ToString("f2"));
                        string fmc = string.Empty;
                        string rmc = string.Empty;
                        string rehc = string.Empty;
                        string relc = string.Empty;
                        string rpc = string.Empty;

                        if (m_isSelFm)
                        {
                            if (dFmc < 1000.0d)
                                strResult.Append("Fmc:" + dFmc.ToString("f2") + " N\r\n");
                            if (dFmc >= 1000.0d)
                                strResult.Append("Fmc:" + (dFmc / 1000.0d).ToString("f4") + " kN\r\n");
                        }

                        if (m_isSelRm)
                            strResult.Append("Rmc:" + dRmc.ToString() + " MPa\r\n");
                        if (m_isSelReH)
                            strResult.Append("ReHc:" + Convert.ToDouble((m_FRH / m_S0).ToString("G5")).ToString() + " MPa\r\n");
                        if (m_isSelReL)
                            strResult.Append("ReLc:" + Convert.ToDouble((m_FRL / m_S0).ToString("G5")) + " MPa\r\n");
                        if (m_isSelRp)
                            strResult.Append("Rpc:" + m_Rp + " MPa\r\n");
                        break;
                }
                Rectangle rec = e.ClipRectangle;
                int px = (rec.Right - 3 * c_kXAxisIndent - c_kXAxisoffset) * 4 / 5 + 2 * c_kXAxisIndent + c_kXAxisoffset;
                int py = (rec.Bottom - 3 * c_kYAxisIndent) * 2 / 5 + c_kYAxisIndent;
                Point p = new Point(px, py);
                g.DrawString(strResult.ToString(), GraphFont, Brushes.Navy, p);
            }
            #endregion
        }

        private delegate void dlgShowCurveFucChart(PictureBox p);
        private void AddChartDataLoop()
        {
            while (isTest)
            {
                this.pbChart.BeginInvoke(new dlgShowCurveFucChart(AddChartData), new object[] { this.pbChart });
                Thread.Sleep(80);
            }
        }

        private void AddChartData(PictureBox p)
        {
            float time = m_Time;
            //力
            float F1value = m_Load;
            //应力
            float YL1value = m_YingLi;
            //位移
            float D1value = m_Displacement;
            //变形
            float BX1value = m_Elongate;
            //应变
            float YB1value = m_YingBian;

            if (F1value > -50)
            {
                //显示曲线数据
                #region Y1-X1 / Y1-X2 第一、二条曲线
                switch (_Y1)
                {
                    case 1:
                        switch (_X1)
                        {
                            case 1:
                                ly1_x1.Add(new PointF(time, F1value));
                                break;
                            case 2:
                                ly1_x1.Add(new PointF(D1value, F1value));
                                break;
                            case 3:
                                ly1_x1.Add(new PointF(YB1value, F1value));
                                break;
                            case 4:
                                ly1_x1.Add(new PointF(YL1value, F1value));
                                break;
                            case 5:
                                ly1_x1.Add(new PointF(BX1value, F1value));
                                break;
                            default:
                                //strCurveName[0] = "";                           
                                break;
                        }
                        switch (_X2)
                        {
                            case 1:
                                ly1_x2.Add(new PointF(time, F1value));
                                break;
                            case 2:
                                ly1_x2.Add(new PointF(D1value, F1value));
                                break;
                            case 3:
                                ly1_x2.Add(new PointF(YB1value, F1value));
                                break;
                            case 4:
                                ly1_x2.Add(new PointF(YL1value, F1value));
                                break;
                            case 5:
                                ly1_x2.Add(new PointF(BX1value, F1value));
                                break;
                            default:
                                //strCurveName[0] = "";                           
                                break;

                        }

                        break;
                    case 2:
                        switch (_X1)//this.tscbX1.SelectedIndex
                        {
                            case 1:
                                ly1_x1.Add(new PointF(time, YL1value));
                                break;
                            case 2:
                                ly1_x1.Add(new PointF(D1value, YL1value));
                                break;
                            case 3:
                                ly1_x1.Add(new PointF(YB1value, YL1value));
                                break;
                            case 5:
                                ly1_x1.Add(new PointF(BX1value, YL1value));
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        switch (_X2)
                        {
                            case 1:
                                ly1_x2.Add(new PointF(time, YL1value));
                                break;
                            case 2:
                                ly1_x2.Add(new PointF(D1value, YL1value));
                                break;
                            case 3:
                                ly1_x2.Add(new PointF(YB1value, YL1value));
                                break;
                            case 5:
                                ly1_x2.Add(new PointF(BX1value, YL1value));
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }

                        break;
                    case 3:
                        switch (_X1)//this.tscbX1.SelectedIndex
                        {
                            case 1:
                                ly1_x1.Add(new PointF(time, BX1value));
                                break;
                            case 2:
                                // ly1_x1.Add(new PointF(D1value, BX1value);
                                break;
                            case 3:
                                // ly1_x1.Add(new PointF(YB1value, BX1value);
                                break;
                            case 4:
                                ly1_x1.Add(new PointF(YL1value, BX1value));
                                break;
                            case 5:

                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:
                                ly1_x2.Add(new PointF(time, BX1value));
                                break;
                            case 2:
                                // ly1_x2.Add(new PointF(D1value, BX1value);
                                break;
                            case 3:
                                // ly1_x2.Add(new PointF(YB1value, BX1value);
                                break;
                            case 4:
                                ly1_x2.Add(new PointF(YL1value, BX1value));
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        break;
                    case 4:
                        switch (_X1)//this.tscbX1.SelectedIndex
                        {
                            case 1:
                                ly1_x1.Add(new PointF(time, D1value));
                                break;
                            case 2:
                                break;
                            case 3:
                                ly1_x1.Add(new PointF(YB1value, D1value));
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:
                                ly1_x2.Add(new PointF(time, D1value));
                                break;
                            case 2:
                                break;
                            case 3:
                                ly1_x2.Add(new PointF(YB1value, D1value));
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        break;

                    case 5:
                        switch (_X1)//this.tscbX1.SelectedIndex
                        {
                            case 1:
                                ly1_x1.Add(new PointF(time, YB1value));
                                break;
                            case 2:
                                break;
                            case 3:
                                // ly1_x1.Add(new PointF(YB1value, YB1value);
                                break;
                            case 5:
                                ly1_x1.Add(new PointF(BX1value, YB1value));
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:
                                ly1_x2.Add(new PointF(time, YB1value));
                                break;
                            case 2:
                                break;
                            case 3:
                                // ly1_x2.Add(new PointF(YB1value, YB1value);
                                break;
                            case 5:
                                ly1_x2.Add(new PointF(BX1value, YB1value));
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

                #region Y2-X1 / Y2-X2 第三、四条曲线
                switch (_Y2)//this.tscbY2.SelectedIndex
                {
                    case 1:
                        switch (_X1)//this.tscbX1.SelectedIndex
                        {
                            case 1:
                                ly2_x1.Add(new PointF(time, F1value));
                                break;
                            case 2:
                                ly2_x1.Add(new PointF(D1value, F1value));
                                break;
                            case 3:
                                ly2_x1.Add(new PointF(YB1value, F1value));
                                break;
                            case 4:
                                ly2_x1.Add(new PointF(YL1value, F1value));
                                break;
                            case 5:
                                ly2_x1.Add(new PointF(BX1value, F1value));
                                break;
                            default:
                                //strCurveName[0] = "";                           
                                break;
                        }
                        switch (_X2)
                        {
                            case 1:
                                ly2_x2.Add(new PointF(time, F1value));
                                break;
                            case 2:
                                ly2_x2.Add(new PointF(D1value, F1value));
                                break;
                            case 3:
                                ly2_x2.Add(new PointF(YB1value, F1value));
                                break;
                            case 4:
                                ly2_x2.Add(new PointF(YL1value, F1value));
                                break;
                            case 5:
                                ly2_x2.Add(new PointF(BX1value, F1value));
                                break;
                            default:
                                //strCurveName[0] = "";                           
                                break;
                        }

                        break;
                    case 2:
                        switch (_X1)
                        {
                            case 1:
                                ly2_x1.Add(new PointF(time, YL1value));
                                break;
                            case 2:
                                ly2_x1.Add(new PointF(D1value, YL1value));
                                break;
                            case 3:
                                ly2_x1.Add(new PointF(YB1value, YL1value));
                                break;
                            case 5:
                                ly2_x1.Add(new PointF(BX1value, YL1value));
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        switch (_X2)
                        {
                            case 1:
                                ly2_x2.Add(new PointF(time, YL1value));
                                break;
                            case 2:
                                ly2_x2.Add(new PointF(D1value, YL1value));
                                break;
                            case 3:
                                ly2_x2.Add(new PointF(YB1value, YL1value));
                                break;
                            case 5:
                                ly2_x2.Add(new PointF(BX1value, YL1value));
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }

                        break;
                    case 3:
                        switch (_X1)
                        {
                            case 1:
                                ly2_x1.Add(new PointF(time, BX1value));
                                break;
                            case 2:
                                // ly2_x1.Add(new PointF(D1value, BX1value);
                                break;
                            case 3:
                                // ly2_x1.Add(new PointF(YB1value, BX1value);
                                break;
                            case 4:
                                ly2_x1.Add(new PointF(YL1value, BX1value));
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        switch (_X2)
                        {
                            case 1:
                                ly2_x2.Add(new PointF(time, BX1value));
                                break;
                            case 2:
                                // ly2_x2.Add(new PointF(D1value, BX1value);
                                break;
                            case 3:
                                // ly2_x2.Add(new PointF(YB1value, BX1value);
                                break;
                            case 4:
                                ly2_x2.Add(new PointF(YL1value, BX1value));
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        break;
                    case 4:
                        switch (_X1)
                        {
                            case 1:
                                ly2_x1.Add(new PointF(time, D1value));
                                break;
                            case 2:
                                //strCurveName[0] = "位移/位移";
                                //LineItemListEdit_2.Add(D1value, D1value);
                                //_RPPList2.Add(D1value, D1value);
                                break;
                            case 3:
                                ly2_x1.Add(new PointF(YB1value, D1value));
                                break;
                            case 4:
                                ly2_x1.Add(new PointF(YL1value, D1value));
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        switch (_X2)
                        {
                            case 1:
                                ly2_x2.Add(new PointF(time, D1value));
                                break;
                            case 2:
                                //strCurveName[0] = "位移/位移";
                                //LineItemListEdit_3.Add(D1value, D1value);
                                //_RPPList3.Add(D1value, D1value);
                                break;
                            case 3:
                                ly2_x2.Add(new PointF(YB1value, D1value));
                                break;
                            case 4:
                                ly2_x2.Add(new PointF(YL1value, D1value));
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        break;
                    case 5:
                        switch (_X1)
                        {
                            case 1:
                                ly2_x1.Add(new PointF(time, YB1value));
                                break;
                            case 2:
                                //strCurveName[0] = "位移/位移";
                                //LineItemListEdit_2.Add(D1value, D1value);
                                //_RPPList2.Add(D1value, D1value);
                                break;
                            case 3:
                                // ly2_x1.Add(new PointF(YB1value, YB1value);
                                break;
                            case 4:
                                // ly2_x1.Add(new PointF(YL1value, YB1value);
                                break;
                            case 5:
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        switch (_X2)
                        {
                            case 1:
                                ly2_x2.Add(new PointF(time, YB1value));
                                break;
                            case 2:
                                //strCurveName[0] = "位移/位移";
                                //LineItemListEdit_3.Add(D1value, D1value);
                                //_RPPList3.Add(D1value, D1value);
                                break;
                            case 3:
                                // ly2_x2.Add(new PointF(YB1value, YB1value);
                                break;
                            case 4:
                                // ly2_x2.Add(new PointF(YL1value, YB1value);
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        break;
                    default:
                        //strCurveName[2] = "";
                        //strCurveName[3] = "";
                        break;
                }
                #endregion

            }
            p.Invalidate();
        }

        private void pbChart_Invalidated(object sender, EventArgs e)
        {
            try
            {
                if (isTest)
                {
                    double x1max = 0, x2max = 0, y1max = 0, y2max = 0;
                    if (c_x1maximum < 1000)
                        x1max = c_x1maximum * 10 / 7 + 5;
                    else
                        x1max = c_x1maximum * 10 / 7 + 500;

                    if (c_x2maximum < 1000)
                        x2max = c_x2maximum * 10 / 7 + 5;
                    else
                        x2max = c_x2maximum * 10 / 7 + 500;

                    if (c_y1maximum < 1000)
                        y1max = c_y1maximum * 10 / 7 + 5;
                    else
                        y1max = c_y1maximum * 10 / 7 + 500;

                    if (c_y2maximum < 1000)
                        y2max = c_y2maximum * 10 / 7 + 5;
                    else
                        y2max = c_y2maximum * 10 / 7 + 500;

                    switch (_X1)// -请选择-   时间,s  位移,μm  应变,μm 应力,MPa
                    {
                        case 1:
                            if (m_Time > c_x1maximum)
                            {
                                c_x1maximum = (int)(x1max / 5) * 5;// ((int)(1.45 * m_Time) / 50) * 50 + 50;
                                isShow103X1 = false;
                            }
                            break;
                        case 2:
                            if (Math.Abs(m_Displacement) > Math.Abs(c_x1maximum))
                            {
                                c_x1maximum = (int)(x1max / 5) * 5;
                                isShow103X1 = true;
                                if (Math.Abs(c_x1maximum) >= 1000.0)
                                {
                                    c_x1maximum = (int)(x1max / 500) * 500;
                                    m_LabelX1 = "位移(mm)";
                                }
                            }

                            break;
                        case 3:
                            if (Math.Abs(m_YingBian) > Math.Abs(c_x1maximum))
                            {
                                c_x1maximum = c_x1maximum + 0.5f;
                                isShow103X1 = false;
                            }
                            break;
                        case 4:
                            if (Math.Abs(m_YingLi) > Math.Abs(c_x1maximum))
                            {
                                c_x1maximum = ((int)(1.45 * m_YingLi) / 50) * 50 + 50; //(int)(x1max / 500) * 500;//
                                isShow103X1 = false;
                            }
                            break;

                        case 5:
                            c_x1maximum = ((int)(1.45 * m_Elongate) / 50) * 50 + 50;//(int)(x1max / 5) * 5;  //
                            isShow103X1 = true;
                            if (Math.Abs(c_x1maximum) >= 1000.0)
                            {
                                //c_x1maximum = (int)(x1max / 500) * 500;
                                m_LabelX1 = "变形(mm)";
                            }
                            break;


                    }

                    switch (_X2)// -请选择-   时间,s  位移,μm  应变,μm
                    {
                        case 1:
                            if (m_Time > c_x2maximum)
                            {
                                c_x2maximum = (int)(x2max / 5) * 5; // ((int)(1.45 * m_Time) / 50) * 50 + 50;
                                isShow103X2 = false;
                            }
                            break;
                        case 2:
                            if (Math.Abs(m_Displacement) > Math.Abs(c_x2maximum))
                            {
                                c_x2maximum = (int)(x2max / 5) * 5; // ((int)(1.45 * m_Displacement) / 50) * 50 + 50;
                                isShow103X2 = true;
                                if (Math.Abs(c_x2maximum) >= 1000.0)
                                {
                                    c_x2maximum = (int)(x2max / 500) * 500;
                                    m_LabelX2 = "位移(mm)";
                                }
                            }
                            break;
                        case 3:
                            if (Math.Abs(m_YingBian) > Math.Abs(c_x2maximum))
                            {
                                c_x2maximum = c_x2maximum + 0.5f;
                                isShow103X2 = false;
                            }
                            break;
                        case 4:
                            if (Math.Abs(m_YingLi) > Math.Abs(c_x2maximum))
                            {
                                c_x2maximum = (int)(x2max / 5) * 5;// ((int)(1.45 * m_YingLi) / 50) * 50 + 50;
                                isShow103X2 = false;
                            }
                            break;
                        case 5:
                            if (Math.Abs(m_Elongate) > Math.Abs(c_x2maximum))
                            {
                                c_x2maximum = ((int)(1.45 * m_Elongate) / 50) * 50 + 50;// (int)(x2max / 5) * 5;
                                isShow103X2 = true;
                                if (Math.Abs(c_x2maximum) >= 1000.0)
                                {
                                    //c_x2maximum = ((int)m_Elongate / 500) * 500 + 500;
                                    m_LabelX2 = "变形(mm)";
                                }
                            }
                            break;
                    }

                    //if (_RealTimePanel.YAxisList[1] != null)
                    //{   //-请选择- 0
                    //力,kN 1
                    //应力,MPa 2
                    //变形,μm 3
                    //位移,μm 4
                    //Scale sScale = _RealTimePanel.YAxisList[1].Scale;
                    switch (_Y1)
                    {

                        case 1:
                            if (Math.Abs(m_Load) > Math.Abs(c_y1maximum))
                            {
                                c_y1maximum = (int)(y1max / 5) * 5;// ((int)(1.45 * m_Load) / 50) * 50 + 50;
                                isShow103Y1 = true;
                                if (Math.Abs(c_y1maximum) >= 1000.0)
                                {
                                    c_y1maximum = (int)(y1max / 500) * 500;
                                    m_LabelY1 = "负荷(kN)";
                                }
                            }
                            break;
                        case 2:
                            if (Math.Abs(m_YingLi) > Math.Abs(c_y1maximum))
                            {
                                c_y1maximum = (int)(y1max / 5) * 5;//((int)(1.45 * m_YingLi) / 50) * 50 + 50;
                                isShow103Y1 = false;
                            }


                            break;
                        case 3:
                            if (Math.Abs(m_Elongate) > Math.Abs(c_y1maximum))
                            {
                                c_y1maximum = (int)(y1max / 5) * 5;// ((int)(1.45 * m_Elongate) / 50) * 50 + 50;
                                isShow103Y1 = true;
                                if (Math.Abs(c_y1maximum) >= 1000.0)
                                {
                                    c_y1maximum = (int)(y1max / 500) * 500;
                                    m_LabelY1 = "变形(mm)";
                                }
                            }
                            break;
                        case 4:
                            if (Math.Abs(m_Displacement) > Math.Abs(c_y1maximum))
                            {
                                c_y1maximum = (int)(y1max / 5) * 5;//((int)(1.45 * m_Displacement) / 50) * 50 + 50;
                                isShow103Y1 = true;
                                if (Math.Abs(c_y1maximum) >= 1000.0)
                                {
                                    c_y1maximum = (int)(y1max / 500) * 500;
                                    m_LabelY1 = "位移(mm)";
                                }
                            }
                            break;
                    }

                    switch (_Y2)
                    {
                        case 1:
                            if (Math.Abs(m_Load) > Math.Abs(c_y2maximum))
                            {
                                c_y2maximum = (int)(y2max / 5) * 5;//((int)(1.45 * m_Load) / 50) * 50 + 50;
                                isShow103Y2 = true;
                                if (Math.Abs(c_y2maximum) >= 1000.0)
                                {
                                    c_y2maximum = (int)(y2max / 500) * 500;
                                    m_LabelY2 = "负荷(kN)";
                                }
                            }
                            break;
                        case 2:
                            if (Math.Abs(m_YingLi) > Math.Abs(c_y2maximum))
                            {
                                c_y2maximum = (int)(y2max / 5) * 5;// ((int)(1.45 * m_YingLi) / 50) * 50 + 50;
                                isShow103Y2 = false;
                            }

                            break;
                        case 3:
                            if (Math.Abs(m_Elongate) > Math.Abs(c_y2maximum))
                            {
                                c_y2maximum = (int)(y2max / 5) * 5;// ((int)(1.45 * m_Elongate) / 50) * 50 + 50;
                                isShow103Y2 = true;
                                if (Math.Abs(c_y2maximum) >= 1000.0)
                                {
                                    c_y2maximum = (int)(y2max / 500) * 500;
                                    m_LabelY2 = "变形(mm)";
                                }
                            }
                            break;
                        case 4:
                            if (Math.Abs(m_Displacement) > Math.Abs(c_y2maximum))
                            {
                                c_y2maximum = (int)(y2max / 5) * 5;//((int)(1.45 * m_Displacement) / 50) * 50 + 50;
                                isShow103Y2 = true;
                                if (Math.Abs(c_y2maximum) >= 1000.0)
                                {
                                    c_y2maximum = (int)(y2max / 500) * 500;
                                    m_LabelY2 = "位移(mm)";
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ee) { MessageBox.Show(this, ee.ToString(), "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        #endregion

        /// <summary>
        /// 用递归遍历的方法查找TreeNode中的指定节点
        /// </summary>
        /// <param name="tnParent">TreeNode</param>
        /// <param name="strValue">节点名</param>
        /// <returns>TreeNode</returns>
        public TreeNode FindNodeByValue(TreeNode tnParent, string strValue)
        {
            if (tnParent == null) return null;
            if (tnParent.Text == strValue) return tnParent;
            TreeNode tnRet = null;
            foreach (TreeNode tn in tnParent.Nodes)
            {
                tnRet = FindNodeByValue(tn, strValue);
                if (tnRet != null) break;
            }
            return tnRet;
        }

        //删除剪切试验数据
        private void delShear_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    switch (dataGridView.Tag.ToString())
            //    {
            //        case "GBT28289-2012Shear":
            //            cmshear.Show(this.dataGridView,e.Location);
            //            break;
            //    }
            //}
        }


        private void tsbtn_Zero_Click(object sender, EventArgs e)
        {
            //TEST
            if (m_LoadSensorCount == 0)
            {
                MessageBox.Show(this, "未连接设备", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lock (m_state)
            {
                Thread.Sleep(50);
                // TODO: 在此添加控件通知处理程序代码
                byte[] buf = new byte[5];// char buf[5];
                int ret;
                buf[0] = 0x04;									//命令字节
                buf[1] = 0xf4;									//清零命令
                buf[2] = 0;										//
                buf[3] = 0;
                buf[4] = 0;
                ret = RwUsb.WriteData1582(1, buf, 5, 1000);				//发送写命令
                Thread.Sleep(50);
            }
        }

        private void toolStrip1_MouseHover(object sender, EventArgs e)
        {
            pbChart.Invalidate();
        }

        private void tsbtnShowResultCurve_MouseEnter(object sender, EventArgs e)
        {
            pbChart.Invalidate();
        }

        private void tsbtnShowResultCurve_MouseLeave(object sender, EventArgs e)
        {
            pbChart.Invalidate();
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            m_path = "GBT228-2010";
            m_testType = "tensile";
            this.dataGridView.Tag = "GBT228-2010";
            BLL.TestSample bllTensile = new HR_Test.BLL.TestSample();
            Model.TestSample modelTensile = bllTensile.GetModel("6-22-11-01");
            m_S0 = (float)modelTensile.S0;
            m_L0 = (float)modelTensile.L0;
            m_Lc = (float)modelTensile.Lc;
            m_Le = (float)modelTensile.Le;
            m_Ep = (float)modelTensile.εp;
            m_Et = (float)modelTensile.εt;
            m_Er = (float)modelTensile.εr;
            m_l = ReadOneListData("GBT228-2010", "6-22-11-01");
            CalcData(m_l, true, true);
            int tempIndex = 0;
            int count = 0;
            m_FrIndex = m_FmIndex;
            do
            {
                tempIndex = m_FrIndex;

                if (GetFp02Index(m_l, tempIndex, out m_FrIndex, out m_a, out m_k, out m_fr05index, out m_fr01index, out m_ep02L0))
                {
                    count++;
                    //MessageBox.Show(count.ToString() + ":" + m_FrIndex.ToString() + "," + tempIndex.ToString());                                    
                    // mts.Rp = double.Parse((m_l[m_FrIndex].YL1).ToString("G5"));
                    m_Rp = float.Parse((m_l[m_FrIndex].YL1).ToString("G5"));
                    m_calSuccess = true;
                    if (count > 500)
                    {
                        MessageBox.Show(this, "计算失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        m_calSuccess = false;
                        //mts.Rp = 0;
                        m_Rp = 0;
                        //mts.isUseExtensometer = true;
                        break;
                    }
                }
                else
                {
                    m_calSuccess = false;
                    //mts.Rp = 0;
                    m_Rp = 0f;
                    //mts.isUseExtensometer = true;
                    MessageBox.Show(this, "计算失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            while (m_FrIndex > tempIndex + 2 || m_FrIndex < tempIndex - 2);
            MessageBox.Show(m_Rp.ToString());
        }

        private void tsbtnYSJ_Click(object sender, EventArgs e)
        {
            //取引伸计暂停时的值
            SendPauseTest();
            //m_hold_data.BX1 = m_Elongate;
            //m_hold_data.D1 = m_Displacement;
            //m_hold_data.F1 = m_Load;
            _useExten = false;
            m_holdPause = true;
            m_holdContinue = false;
            btnZeroS.Enabled = false;
            //弹出取引伸计框
            //Thread.Sleep(50);
            m_fh.Show();
            m_fh.TopMost = true;
        }

        bool isShowBX2 = false;
        private void btnBXShow2_Click(object sender, EventArgs e)
        {
            isShowBX2 = !isShowBX2;
            if (isShowBX2)
            {
                this.lbltime.Text = "变形μ";
                this.btnBXShow2.Text = "时间";
                this.lbltum.Text = "μm";
                this.btnZeroBx2.Visible = true;
                this.lblUseExten2.Visible = true;
                this.lblBXShow2.Visible = true;
                this.lblTimeShow.Visible = false;
            }
            else
            {
                this.lbltime.Text = "时间";
                this.lbltum.Text = "s";
                this.btnBXShow2.Text = "变形";
                this.btnZeroBx2.Visible = false;
                this.lblUseExten2.Visible = false;
                this.lblBXShow2.Visible = false;
                this.lblTimeShow.Visible = true;
            }


        }

        private void btnZeroBx2_Click(object sender, EventArgs e)
        {
            if (m_ElongateSensorCount == 0)
            {
                MessageBox.Show(this, "未连接设备!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // TODO: 在此添加控件通知处理程序代码
            lock (m_state)
            {
                Thread.Sleep(30);
                byte[] buf = new byte[5];
                int ret;
                buf[0] = 0x04;									//命令字节
                buf[1] = 0xde;									//清零命令
                buf[2] = m_ESensorArray[1].SensorIndex;			//因为是简单测试程序，所以只取了零号索引。
                buf[3] = 0;
                buf[4] = 0;
                ret = RwUsb.WriteData1582(1, buf, 5, 1000);				//发送写命令
                Thread.Sleep(30);
            }
            //试验过程中使用引伸计
            _useExten2 = true;
            //画曲线标志
            m_useExten2 = true;
            if (_useExten2)
            {
                this.lblUseExten2.Text = "使用引伸计";
                this.lblUseExten2.ForeColor = Color.Green;
                //this.tsbtnYSJ.Enabled = true;
                this.lblUseExten2.Refresh();
                this.btnAverayBX.Visible = true;
            }
        }
    }
}