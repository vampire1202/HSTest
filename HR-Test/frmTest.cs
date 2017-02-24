using System;
using System.Collections.Generic;
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

namespace HR_Test
{
    public partial class frmTest : Form
    {
        private Struc.SensorArray[] m_DSensorArray = null;
        private Struc.SensorArray[] m_LSensorArray = null;
        private Struc.SensorArray[] m_ESensorArray = null;

        private Struc.sensor[] m_SensorArray = new Struc.sensor[20];

        private int m_DisplacementSensorCount;//位移传感器数量
        private int m_LoadSensorCount;//负荷传感器数量
        private int m_ElongateSensorCount;//变形传感器数量
        private long m_startCount = 0;

        //控制命令
        private Struc.ctrlcommand[] m_CtrlCommandArray = new Struc.ctrlcommand[10];
        private List<Struc.ctrlcommand> m_CtrlCommandList = new List<Struc.ctrlcommand>();
        private int loopCount = 0;
        private string m_methodContent = string.Empty;

        private bool _showThreadFlag = true;

        //发送命令和获得数据线程
        //private System.Timers.Timer _mtimer_T;
        //Thread _threadSendOrder; 

        ////采集数据线程
        //Thread _threadGatherData;  

        //实时显示曲线线程
        //private Multimedia.Timer _mtimer_GatherData;
        Thread _threadShowCurve;

        //读取测试中的数据线程
        Thread _threadReadData;

        //初始化数据轴线程
        Thread _threadInitRealCurve;

        Thread _threadShowData;

        //实时采集数据委托
        private delegate void dlgShowRealData(System.Windows.Forms.Label lbl_LoadShow,//力
            System.Windows.Forms.Label lbl_DisplacementShow,//位移
            System.Windows.Forms.Label lbl_ElongateShow,//变形
            System.Windows.Forms.Label lbl_TimeShow,//时间
            System.Windows.Forms.Label lbl_StressShow,//应力
            System.Windows.Forms.Label lbl_StrainShow,//应变
            System.Windows.Forms.Label lbl_kN,
            System.Windows.Forms.Label lbl_mm1,
            System.Windows.Forms.Label lbl_mm2,
        System.Windows.Forms.Label lbl_mm3);

        //private delegate void dlgGatherData(float  _Load,//力
        //float _Displacement,//位移
        //float _Elongate,//变形
        //float _Time,//时间
        //float _Stress,//应力
        //float _Strain//应变
        //);

        private string m_String = string.Empty;

        //显示值
        float m_Displacement = 0.0f;//位移
        float m_Elongate = 0.0f;//变形
        float m_Load = 0.0f;//力
        float m_Time = 0.0f;//时间
        float m_Stress = 0.0f;//应力
        float m_Strain = 0.0f;//应变 
        double m_S0;

        byte _SensorArrayFlag;
        int _m_SensorCount;
        byte[] buf;
        bool isTest = false;

        //显示曲线
        private RollingPointPairList _RPPList0;
        private RollingPointPairList _RPPList1;
        private RollingPointPairList _RPPList2;
        private RollingPointPairList _RPPList3;


        LineItem LineItem0;
        LineItem LineItem1;
        LineItem LineItem2;
        LineItem LineItem3;

        IPointListEdit LineItemListEdit_0;
        IPointListEdit LineItemListEdit_1;
        IPointListEdit LineItemListEdit_2;
        IPointListEdit LineItemListEdit_3;

        //计算结果用到的 力-时间
        private RollingPointPairList _RPPF_T;

        string _testpath = string.Empty;

        string[] _Color_Array = { "Crimson", "Green", "Blue", "Teal", "DarkOrange", "Chocolate", "BlueViolet", "Indigo", "Magenta", "LightCoral", "LawnGreen", "Aqua", "DarkViolet", "DeepPink", "DeepSkyblue", "HotPink", "SpringGreen", "GreenYellow", "Peru", "Black" };

        // "Fm bit," + //最大力
        //"Rm bit," + //最大应力
        //"ReH bit," + //上屈服强度
        //"ReL bit," + //下屈服强度
        //"Rp bit," + //规定塑性延伸强度
        //"Rt bit," + //规定总延伸强度
        //"Rr bit," + //规定残余延伸强度
        //"εp bit," +//ε
        //"εt bit," +//
        //"εr bit," +//
        //"E bit," + //弹性模量
        //"m bit," + //应力-延伸率曲线在给定试验时刻的斜率
        //"mE bit," + //应力-延伸率曲线在弹性部分的斜率
        //"A bit," + //断后伸长率               
        //"Aee bit," + //屈服点延伸率
        //"Agg bit," + //最大力Fm塑性延伸率
        //"Att bit," + //断裂总延伸率 
        //"Aggtt bit," + //最大力Fm总延伸率
        //"Awnwn bit," + //无缩颈塑性伸长率
        //"Lm bit," +
        //"Lf bit," +
        //"Z bit," + //断面收缩率 
        //"Avera bit," + //平均值
        //"SS bit," + //标准偏差
        //"Avera1 bit" + //去掉最大最小值的平均值

        string[] _lblTensile_Result = { "-", "-", "Fm", "Rm", "ReH", "ReL", "Rp", "Rt", "Rr", "εp", "εt", "εr", "E", "m", "mE", "A", "Ae", "Ag", "At", "Agt", "Awn", "Lm", "Lf", "Z", "X", "S", "X￣" };
        //"deltaL bit," + //原始标距段受力后的变形
        //      "εpc bit," + //规定非比例压缩应变
        //      "εtc bit," + //规定总压缩应变
        //      "n bit," + //变形放大倍数
        //      "F0 bit," + //试样上端所受得力
        //      "Ff bit," + //摩擦力                
        //      "Fpc bit," + //规定非比例压缩变形的实际压缩力
        //      "Ftc bit," + //规定总压缩变形的实际压缩力
        //      "FeHc bit," + //屈服时的实际上屈服压缩力
        //      "FeLc bit," + //屈服时的实际下屈服压缩力
        //      "Fmc bit," + //试样破坏过程中最大的压缩力
        //      "Rpc bit," + //规定非比例压缩强度
        //      "Rtc bit," +//规定总压缩强度
        //      "ReHc bit," +//上屈服压缩强度
        //      "ReLc bit," +//下屈服压缩强度
        //      "Rmc bit," + //抗压强度
        //      "Ec bit," + //压缩弹性模量
        //      "Avera bit," + //平均值
        //      "Avera1 bit" + //去掉最大最小值的平均值
        string[] _lblCompress_Result = { "-", "-", "△L", "εpc", "εtc", "n", "F0", "Ff", "Fpc", "Ftc", "FeHc", "FeLc", "Fmc", "Rpc", "Rtc", "ReHc", "ReLc", "Rmc", "Ec", "X", "X￣" };
        //"α bit," + //弯曲角度
        //    "r bit," + //试样弯曲后的弯曲半径
        //    "f bit" + //弯曲压头的移动距离
        string[] _lblBend_Result = { "-", "-", "α", "r", "f" };


        //存储采集数据
        private List<gdata> _List_Data;
        private List<gdata> _List_DataReadOne;
        //private double _S0 = 0;
        //显示切换
        private bool _showYL = false;
        private bool _showYB = false;
        //曲线名数组
        private string[] strCurveName = new string[4];
        ZedGraph.GraphPane _RealTimePanel;
        ZedGraph.GraphPane _ResultPanel;

        //开始时间
        private double tickStart = 0;

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
        private string _selTestSampleGroup = string.Empty;
        private int _sampleCount = 0;
        private frmMain _fmMain;
        //GatherData _gatherData;

        //显示实时曲线设置参数
        int _X1 = 0;
        int _X2 = 0;
        int _Y1 = 0;
        int _Y2 = 0; 
        int _ShowX = 0;
        int _ShowY = 0;
        //dlgShowRealData ShowRlData;
        //实时显示曲线的函数 

        private void dlgShowCurveFuc(object o)
        {
            ZedGraph.ZedGraphControl _zgControl = (ZedGraph.ZedGraphControl)o;
            while (isTest)
            {
                Thread.Sleep(60);
                //采集数据
                //时间
                double time = m_Time;
                //力
                double F1value = m_Load;
                //应力
                double R1value = m_Stress;
                //位移
                double D1value = m_Displacement;
                //变形
                double BX1value = m_Elongate;
                //应变
                double YB1value = m_Strain;
                
                m_startCount++;
                if (m_startCount > 5)
                {
                    //显示曲线数据
                    #region Y1-X1 / Y1-X2 第一、二条曲线
                    switch (_Y1)//this.tscbY1.SelectedIndex
                    {
                        case 1:
                            switch (_X1)//this.tscbX1.SelectedIndex
                            {
                                case 1:
                                    //strCurveName[0] = "力/时间";
                                    //LineItemListEdit_0.Add(time, F1value);
                                    _RPPList0.Add(time, F1value);
                                    break;
                                case 2:
                                    //strCurveName[0] = "力/位移";
                                    //LineItemListEdit_0.Add(D1value, F1value);
                                    _RPPList0.Add(D1value, F1value);
                                    break;
                                case 3:
                                    //strCurveName[0] = "力/应变";
                                    //LineItemListEdit_0.Add(YB1value, F1value);
                                    _RPPList0.Add(YB1value, F1value);
                                    break;
                                default:
                                    //strCurveName[0] = "";                           
                                    break;
                            }
                            switch (_X2)//this.tscbX2.SelectedIndex
                            {
                                case 1:
                                    //strCurveName[0] = "力/时间";
                                    //LineItemListEdit_1.Add(time, F1value);
                                    _RPPList1.Add(time, F1value);
                                    break;
                                case 2:
                                    //strCurveName[0] = "力/位移";
                                    //LineItemListEdit_1.Add(D1value, F1value);
                                    _RPPList1.Add(D1value, F1value);
                                    break;
                                case 3:
                                    //strCurveName[0] = "力/应变";
                                    //LineItemListEdit_1.Add(YB1value, F1value);
                                    _RPPList1.Add(YB1value, F1value);
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
                                    //strCurveName[0] = "应力/时间";
                                    //LineItemListEdit_0.Add(time, R1value);
                                    _RPPList0.Add(time, R1value);
                                    break;
                                case 2:
                                    //strCurveName[0] = "应力/位移";
                                    //LineItemListEdit_0.Add(D1value, R1value);
                                    _RPPList0.Add(D1value, R1value);
                                    break;
                                case 3:
                                    //strCurveName[0] = "应力/应变";
                                    //LineItemListEdit_0.Add(YB1value, R1value);
                                    _RPPList0.Add(YB1value, R1value);
                                    break;
                                default:
                                    //strCurveName[0] = "";
                                    break;
                            }
                            switch (_X2)//this.tscbX2.SelectedIndex
                            {
                                case 1:
                                    //strCurveName[0] = "应力/时间";
                                    //LineItemListEdit_1.Add(time, R1value);
                                    _RPPList1.Add(time, R1value);
                                    break;
                                case 2:
                                    //strCurveName[0] = "应力/位移";
                                    //LineItemListEdit_1.Add(D1value, R1value);
                                    _RPPList1.Add(D1value, R1value);
                                    break;
                                case 3:
                                    //strCurveName[0] = "应力/应变";
                                    //LineItemListEdit_1.Add(YB1value, R1value);
                                    _RPPList1.Add(YB1value, R1value);
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
                                    //strCurveName[0] = "变形/时间";
                                    //LineItemListEdit_0.Add(time, BX1value);
                                    _RPPList0.Add(time, BX1value);
                                    break;
                                case 2:
                                    //strCurveName[0] = "变形/位移";
                                    //LineItemListEdit_0.Add(D1value, BX1value);
                                    _RPPList0.Add(D1value, BX1value);
                                    break;
                                case 3:
                                    //strCurveName[0] = "变形/应变";
                                    //LineItemListEdit_0.Add(YB1value, BX1value);
                                    _RPPList0.Add(YB1value, BX1value);
                                    break;
                                default:
                                    //strCurveName[0] = "";
                                    break;
                            }
                            switch (_X2)//this.tscbX2.SelectedIndex
                            {
                                case 1:
                                    //strCurveName[0] = "变形/时间";
                                    //LineItemListEdit_1.Add(time, BX1value);
                                    _RPPList1.Add(time, BX1value);
                                    break;
                                case 2:
                                    //strCurveName[0] = "变形/位移";
                                    //LineItemListEdit_1.Add(D1value, BX1value);
                                    _RPPList1.Add(D1value, BX1value);
                                    break;
                                case 3:
                                    //strCurveName[0] = "变形/应变";
                                    //LineItemListEdit_1.Add(YB1value, BX1value);
                                    _RPPList1.Add(YB1value, BX1value);
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
                                    //strCurveName[0] = "位移/时间";
                                    //LineItemListEdit_0.Add(time, D1value);
                                    _RPPList0.Add(time, D1value);
                                    break;
                                case 2:
                                    //strCurveName[0] = "位移/位移";
                                    //LineItemListEdit_0.Add(D1value, D1value);
                                    //_RPPList0.Add(D1value, D1value);
                                    break;
                                case 3:
                                    //strCurveName[0] = "位移/应变";
                                    //LineItemListEdit_0.Add(YB1value, D1value);
                                    _RPPList0.Add(YB1value, D1value);
                                    break;
                                default:
                                    //strCurveName[0] = "";
                                    break;
                            }
                            switch (_X2)//this.tscbX2.SelectedIndex
                            {
                                case 1:
                                    //strCurveName[0] = "位移/时间";
                                    //LineItemListEdit_1.Add(time, D1value);
                                    _RPPList1.Add(time, D1value);
                                    break;
                                case 2:
                                    //strCurveName[0] = "位移/位移";
                                    //LineItemListEdit_1.Add(D1value, D1value);
                                    //_RPPList1.Add(D1value, D1value);
                                    break;
                                case 3:
                                    //strCurveName[0] = "位移/应变";
                                    //LineItemListEdit_1.Add(YB1value, D1value);
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
                    switch (_Y2)//this.tscbY2.SelectedIndex
                    {
                        case 1:
                            switch (_X1)//this.tscbX1.SelectedIndex
                            {
                                case 1:
                                    //strCurveName[0] = "力/时间";
                                    //LineItemListEdit_2.Add(time, F1value);
                                    _RPPList2.Add(time, F1value);
                                    break;
                                case 2:
                                    //strCurveName[0] = "力/位移";
                                    //LineItemListEdit_2.Add(D1value, F1value);
                                    _RPPList2.Add(D1value, F1value);
                                    break;
                                case 3:
                                    //strCurveName[0] = "力/应变";
                                    //LineItemListEdit_2.Add(YB1value, F1value);
                                    _RPPList2.Add(YB1value, F1value);
                                    break;
                                default:
                                    //strCurveName[0] = "";                           
                                    break;
                            }
                            switch (_X2)
                            {
                                case 1:
                                    //strCurveName[0] = "力/时间";
                                    //LineItemListEdit_3.Add(time, F1value);
                                    _RPPList3.Add(time, F1value);
                                    break;
                                case 2:
                                    //strCurveName[0] = "力/位移";
                                    //LineItemListEdit_3.Add(D1value, F1value);
                                    _RPPList3.Add(D1value, F1value);
                                    break;
                                case 3:
                                    //strCurveName[0] = "力/应变";
                                    //LineItemListEdit_3.Add(YB1value, F1value);
                                    _RPPList3.Add(YB1value, F1value);
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
                                    //strCurveName[0] = "应力/时间";
                                    //LineItemListEdit_2.Add(time, R1value);
                                    _RPPList2.Add(time, R1value);
                                    break;
                                case 2:
                                    //strCurveName[0] = "应力/位移";
                                    //LineItemListEdit_2.Add(D1value, R1value);
                                    _RPPList2.Add(D1value, R1value);
                                    break;
                                case 3:
                                    //strCurveName[0] = "应力/应变";
                                    //LineItemListEdit_2.Add(YB1value, R1value);
                                    _RPPList2.Add(YB1value, R1value);
                                    break;
                                default:
                                    //strCurveName[0] = "";
                                    break;
                            }
                            switch (_X2)
                            {
                                case 1:
                                    //strCurveName[0] = "应力/时间";
                                    //LineItemListEdit_3.Add(time, R1value);
                                    _RPPList3.Add(time, R1value);
                                    break;
                                case 2:
                                    //strCurveName[0] = "应力/位移";
                                    //LineItemListEdit_3.Add(D1value, R1value);
                                    _RPPList3.Add(D1value, R1value);
                                    break;
                                case 3:
                                    //strCurveName[0] = "应力/应变";
                                    //LineItemListEdit_3.Add(YB1value, R1value);
                                    _RPPList3.Add(YB1value, R1value);
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
                                    //strCurveName[0] = "变形/时间";
                                    //LineItemListEdit_2.Add(time, BX1value);
                                    _RPPList2.Add(time, BX1value);
                                    break;
                                case 2:
                                    //strCurveName[0] = "变形/位移";
                                    //LineItemListEdit_2.Add(D1value, BX1value);
                                    _RPPList2.Add(D1value, BX1value);
                                    break;
                                case 3:
                                    //strCurveName[0] = "变形/应变";
                                    //LineItemListEdit_2.Add(YB1value, BX1value);
                                    _RPPList2.Add(YB1value, BX1value);
                                    break;
                                default:
                                    //strCurveName[0] = "";
                                    break;
                            }
                            switch (_X2)
                            {
                                case 1:
                                    //strCurveName[0] = "变形/时间";
                                    //LineItemListEdit_3.Add(time, BX1value);
                                    _RPPList3.Add(time, BX1value);
                                    break;
                                case 2:
                                    //strCurveName[0] = "变形/位移";
                                    //LineItemListEdit_3.Add(D1value, BX1value);
                                    _RPPList3.Add(D1value, BX1value);
                                    break;
                                case 3:
                                    //strCurveName[0] = "变形/应变";
                                    // LineItemListEdit_3.Add(YB1value, BX1value);
                                    _RPPList3.Add(YB1value, BX1value);
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
                                    //strCurveName[0] = "位移/时间";
                                    //LineItemListEdit_2.Add(time, D1value);
                                    _RPPList2.Add(time, D1value);
                                    break;
                                case 2:
                                    //strCurveName[0] = "位移/位移";
                                    //LineItemListEdit_2.Add(D1value, D1value);
                                    //_RPPList2.Add(D1value, D1value);
                                    break;
                                case 3:
                                    //strCurveName[0] = "位移/应变";
                                    //LineItemListEdit_2.Add(YB1value, D1value);
                                    _RPPList2.Add(YB1value, D1value);
                                    break;
                                default:
                                    //strCurveName[0] = "";
                                    break;
                            }
                            switch (_X2)
                            {
                                case 1:
                                    //strCurveName[0] = "位移/时间";
                                    //LineItemListEdit_3.Add(time, D1value);
                                    _RPPList3.Add(time, D1value);
                                    break;
                                case 2:
                                    //strCurveName[0] = "位移/位移";
                                    //LineItemListEdit_3.Add(D1value, D1value);
                                    //_RPPList3.Add(D1value, D1value);
                                    break;
                                case 3:
                                    //strCurveName[0] = "位移/应变";
                                    //LineItemListEdit_3.Add(YB1value, D1value);
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

                    //调用AxisChange()方法更新X和Y轴的范围
                    //_zgControl.AxisChange();
                    //调用Invalidate()方法更新图表 
                    _zgControl.Invalidate();
                }
            }
        }

        public frmTest(frmMain fmMain)
        {
            InitializeComponent();          
            _fmMain = fmMain;
        }

        void zedGraphControl_Invalidated(object sender, InvalidateEventArgs e)
        {
            if (this.zedGraphControl.GraphPane.XAxis != null)
            {
                Scale sScale = _ResultPanel.XAxis.Scale;

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
                if (_ResultPanel.XAxis.Scale.Max > 100)
                {
                    _ResultPanel.XAxis.Scale.Max = ((int)_ResultPanel.XAxis.Scale.Max / 100) * 100 + 100;
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

                if (_ResultPanel.YAxis.Scale.Max > 100)
                {
                    _ResultPanel.YAxis.Scale.Max = ((int)_ResultPanel.YAxis.Scale.Max / 100) * 100 + 100;
                    _ResultPanel.YAxis.Scale.Min = 0;
                }

                _ResultPanel.YAxis.Scale.MajorStep = (_ResultPanel.YAxis.Scale.Max - _ResultPanel.YAxis.Scale.Min) / 5;
                _ResultPanel.YAxis.Scale.MinorStep = _ResultPanel.YAxis.Scale.MajorStep / 5;
            }
        }

        private void ThreadSendOrder()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Thread.CurrentThread.Priority = ThreadPriority.Highest;

                while (_showThreadFlag)
                {
                    Thread.Sleep(40);
                    BeginInvoke(new Action(() =>
                    {
                        int ret;
                        int offset = 0;
                        int len = 2;
                        int m_value = 0;
                        int m_valueS = 0;
                        int m_index = 0;

                        if ((_SensorArrayFlag == 1) && (_m_SensorCount != 0))
                        {
                            buf[0] = 0x03;									//命令字节
                            buf[1] = Convert.ToByte(offset / 256);							//偏移量
                            buf[2] = Convert.ToByte(offset % 256);
                            buf[3] = Convert.ToByte(len / 256);								//每次读的长度
                            buf[4] = Convert.ToByte(len % 256);

                            ret = RwUsb.WriteData1582(1, buf, 5, 1000);				//发送读命令
                            //		if (ret != 5) 	
                            //		{	
                            //			buf[0] = 0x03;
                            //			WriteData1582(1,buf,1,1000);				//发送停止命令
                            //			return -1;
                            //		}

                            len = (_m_SensorCount + 1) * 4;

                            ret = RwUsb.ReadData1582(4, buf, len, 1000);				//读数据
                            //		if (ret != len)  
                            //		{
                            //			buf[0] = 0x03;
                            //			WriteData1582(1,buf,5,1000);				//发送停止命令
                            //			return -1;
                            //		}

                            /*---------------------m_Load display----------------*/

                            if (m_LoadSensorCount != 0)
                            {
                                m_index = m_LSensorArray[0].SensorIndex * 4 + 3;
                                m_value = buf[m_index];

                                m_value = m_value << 8;
                                m_index = m_LSensorArray[0].SensorIndex * 4 + 2;
                                m_value |= buf[m_index];

                                m_value = m_value << 8;
                                m_index = m_LSensorArray[0].SensorIndex * 4 + 1;
                                m_value |= buf[m_index];

                                m_value = m_value << 8;
                                m_index = m_LSensorArray[0].SensorIndex * 4 + 0;
                                m_value |= buf[m_index];

                                m_Load = (float)(m_value * 5.0 / 12.0);

                                //this.lblFShow.Text = m_Load.ToString();
                            }

                            /*---------------------m_Displacement display----------------*/

                            if (m_DisplacementSensorCount != 0)
                            {
                                m_index = m_DSensorArray[0].SensorIndex * 4 + 3;
                                m_value = buf[m_index];

                                m_value = m_value << 8;
                                m_index = m_DSensorArray[0].SensorIndex * 4 + 2;
                                m_value |= buf[m_index];

                                m_value = m_value << 8;
                                m_index = m_DSensorArray[0].SensorIndex * 4 + 1;
                                m_value |= buf[m_index];

                                m_value = m_value << 8;
                                m_index = m_DSensorArray[0].SensorIndex * 4 + 0;
                                m_value |= buf[m_index];

                                m_Displacement = (float)(m_value);
                                //this.lblDShow.Text = m_Displacement.ToString();

                            }

                            /*---------------------elongate display----------------*/

                            if (m_ElongateSensorCount != 0)
                            {
                                m_index = m_ESensorArray[0].SensorIndex * 4 + 3;
                                m_value = buf[m_index];

                                m_value = m_value << 8;
                                m_index = m_ESensorArray[0].SensorIndex * 4 + 2;
                                m_value |= buf[m_index];

                                m_value = m_value << 8;
                                m_index = m_ESensorArray[0].SensorIndex * 4 + 1;
                                m_value |= buf[m_index];

                                m_value = m_value << 8;
                                m_index = m_ESensorArray[0].SensorIndex * 4 + 0;
                                m_value |= buf[m_index];

                                m_Elongate = (float)(m_value / 12.0);

                                //this.lblBXShow.Text = m_Elongate.ToString();
                            }
                            /*-------------------time display-----------------------*/
                            //		(m_SensorCount+1)*4
                            //		m_valueS=buf[m_SensorCount*4+2];	 //+3

                            m_valueS = buf[_m_SensorCount * 4 + 2];     //+2
                            m_valueS = m_valueS << 4;
                            m_value = buf[_m_SensorCount * 4 + 1] & 0x0f0;//+1
                            m_value = m_value >> 4;
                            m_valueS |= (m_value & 0x0f);

                            m_value = buf[_m_SensorCount * 4 + 1] & 0x0f;  //+1
                            m_value = m_value << 8;
                            m_value |= buf[_m_SensorCount * 4 + 0];      //+0

                            m_Time = (float)((m_valueS * 1000.0 + m_value) / 1000.0);

                            m_Stress = (float)(m_Load / m_S0); 
                        }
                        ////Test Curve Data
                        //m_Load += 2f;
                        //m_Time = (float)(DateTime.Now - dt).TotalSeconds;
                        //m_Displacement+=5f;
                        //m_Elongate = (float)Math.Log(Math.Abs(m_Load));
                        //m_Strain = (float)Math.Sqrt(m_Displacement);
                        //m_Stress = (float)Math.Log10(m_Elongate);

                        //_showdata();

                        if (isTest)
                        {
                            //_dlgGatherData(this.m_Load, this.m_Displacement, this.m_Elongate, this.m_Time, this.m_Stress, this.m_Strain);
                            //dlgShowCurveFuc(this.realTimeZedGraph);
                            gdata gd = new gdata();
                            gd.F1 = Math.Round(m_Load, 3);
                            gd.F2 = 0;
                            gd.F3 = 0;
                            gd.D1 = Math.Round(m_Displacement, 3);
                            gd.D2 = 0;
                            gd.D3 = 0;
                            gd.BX1 = Math.Round(m_Elongate, 3);
                            gd.BX2 = 0;
                            gd.BX3 = 0;
                            gd.Ts = Math.Round(m_Time, 3);
                            gd.YL1 = Math.Round(m_Stress, 3);
                            gd.YL2 = 0;
                            gd.YL3 = 0;
                            gd.YB1 = Math.Round(m_Strain, 3);
                            gd.YB2 = 0;
                            gd.YB3 = 0;

                            //using (StreamWriter sw = new StreamWriter(_testpath, true, Encoding.Default))
                            //{ 
                            //    sw.WriteLine(gd.F1 + "," + gd.F2 + "," + gd.F3 + "," + gd.D1 + "," + gd.D2 + "," + gd.D3 + "," + gd.BX1 + "," + gd.BX2 + "," + gd.BX3 + "," + gd.YL1 + "," + gd.YL2 + "," + gd.YL3 + "," + gd.YB1 + "," + gd.YB2 + "," + gd.YB3 + "," + gd.Ts);
                            //    sw.Close();
                            //}

                            _List_Data.Add(gd);
                            //测试用于计算最大值的曲线数组
                            _RPPF_T.Add(m_Time, m_Load);
                        }
                    }));
                }
            }).Start();
        }
        /*
        private void ThreadLoadShow()
        {
            
                //显示力线程
                new Thread(() =>
                {
                    while (_showThreadFlag)
                    { 
                        Thread.Sleep(60);
                        BeginInvoke(new Action(() =>
                        {
                            if (Math.Abs(m_Load) > 1000)
                            {
                                lblFShow.Text = (m_Load / 1000.0).ToString("f3");
                                lblkN.Text = "kN";
                                lblFShow.Refresh();
                                lblkN.Refresh();
                            }
                            else
                            {
                                lblFShow.Text = m_Load.ToString("f2");
                                lblkN.Text = "N";
                                lblFShow.Refresh();
                                lblkN.Refresh();
                            }
                        }));
                       
                    }
                }).Start(); 
        }

        private void ThreadDisplacementShow()
        {
            
                //显示位移线程
                new Thread(() =>
                {
                    while (_showThreadFlag)
                    {
                        Thread.Sleep(60);
                        BeginInvoke(new Action(() =>
                        {
                            //位移
                            if (Math.Abs(m_Displacement) > 1000.0)
                            {
                                lblDShow.Text = (m_Displacement / 1000.0).ToString("f3");
                                lblmm1.Text = "mm";
                                lblDShow.Refresh();
                                lblmm1.Refresh();
                            }
                            else
                            {
                                lblDShow.Text = m_Displacement.ToString("f2");
                                lblmm1.Text = "μm";
                                lblDShow.Refresh();
                                lblmm1.Refresh();
                            }
                        }));
                       
                    }
                }).Start(); 
        }

        private void ThreadElongateShow()
        {
            //显示变形线程
            new Thread(() =>
            {
                while (_showThreadFlag)
                { 
                    Thread.Sleep(60);
                    BeginInvoke(new Action(() =>
                    {
                        if (Math.Abs(m_Elongate) > 1000 && lblBXShow.Visible == true)
                        {
                            lblBXShow.Text = (m_Elongate / 1000.0).ToString("f3");
                            lblmm2.Text = "mm";
                            lblBXShow.Refresh();
                            lblmm2.Refresh();
                        }
                        else
                        {
                            lblBXShow.Text = m_Elongate.ToString("f2");
                            lblmm2.Text = "μm";
                            lblBXShow.Refresh();
                            lblmm2.Refresh();
                        }
                    }));
                   
                }
            }).Start();
        }

        private void ThreadStrainShow()
        {
            //显示应变线程
            new Thread(() =>
            {
                while (_showThreadFlag)
                { 
                    Thread.Sleep(60);
                    BeginInvoke(new Action(() =>
                    {
                        //应变
                        if (Math.Abs(m_Strain) > 1000 && lblYBShow.Visible == true)
                        {
                            lblYBShow.Text = (m_Strain / 1000.0).ToString("f3");
                            lblmm3.Text = "mm";
                            lblYBShow.Refresh();
                            lblmm3.Refresh();
                        }
                        else
                        {
                            lblYBShow.Text = m_Strain.ToString("f2");
                            lblmm3.Text = "μm";
                            lblYBShow.Refresh();
                            lblmm3.Refresh();
                        }
                    }));
                   
                }
            }).Start();
        }

        private void ThreadStressShow()
        {
            //显示应力线程
            new Thread(() =>
            {
                while (_showThreadFlag)
                { 
                    Thread.Sleep(60);
                    BeginInvoke(new Action(() =>
                    {
                        //应力
                        lblYLShow.Text = m_Stress.ToString("f2");
                        lblYLShow.Refresh();
                    }));
                   
                }
            }).Start();
        }

        private void ThreadTimeShow()
        {
            //显示应力线程
            new Thread(() =>
            {
                while (_showThreadFlag)
                { 
                    Thread.Sleep(60);
                    BeginInvoke(new Action(() =>
                    {
                        //应力
                        lblTimeShow.Text = m_Time.ToString("f2");
                        lblTimeShow.Refresh();
                    }));
                   
                }
            }).Start();
        }
        **/
        private void ThreadShowData()
        {
            //显示所有数据线程
            new Thread(() =>
            {
                _showdata();
            }).Start();
        }

        private void _showdata()
        {
            while (_showThreadFlag)
            {
                Thread.Sleep(50);
                if (this.InvokeRequired)
                {
                    this.Invoke(new dlgShowRealData(showData), new object[] { this.lblFShow, this.lblDShow, this.lblBXShow, this.lblTimeShow, this.lblYLShow, this.lblYBShow, this.lblkN, this.lblmm1, this.lblmm2, this.lblmm3 });
                }
                else
                {
                    showData(this.lblFShow, this.lblDShow, this.lblBXShow, this.lblTimeShow, this.lblYLShow, this.lblYBShow, this.lblkN, this.lblmm1, this.lblmm2, this.lblmm3);
                }
            }
        }

        void _mtimer_T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int ret;
            int offset = 0;
            int len = 2;
            int m_value = 0;
            int m_valueS = 0;
            int m_index = 0;

            if ((_SensorArrayFlag == 1) && (_m_SensorCount != 0))
            {
                buf[0] = 0x03;									//命令字节
                buf[1] = Convert.ToByte(offset / 256);							//偏移量
                buf[2] = Convert.ToByte(offset % 256);
                buf[3] = Convert.ToByte(len / 256);								//每次读的长度
                buf[4] = Convert.ToByte(len % 256);

                ret = RwUsb.WriteData1582(1, buf, 5, 50);				//发送读命令
                //		if (ret != 5) 	
                //		{	
                //			buf[0] = 0x03;
                //			WriteData1582(1,buf,1,1000);				//发送停止命令
                //			return -1;
                //		}

                len = (_m_SensorCount + 1) * 4;

                ret = RwUsb.ReadData1582(4, buf, len, 50);				//读数据
                //		if (ret != len)  
                //		{
                //			buf[0] = 0x03;
                //			WriteData1582(1,buf,5,1000);				//发送停止命令
                //			return -1;
                //		}

                /*---------------------m_Load display----------------*/

                if (m_LoadSensorCount != 0)
                {
                    m_index = m_LSensorArray[0].SensorIndex * 4 + 3;
                    m_value = buf[m_index];

                    m_value = m_value << 8;
                    m_index = m_LSensorArray[0].SensorIndex * 4 + 2;
                    m_value |= buf[m_index];

                    m_value = m_value << 8;
                    m_index = m_LSensorArray[0].SensorIndex * 4 + 1;
                    m_value |= buf[m_index];

                    m_value = m_value << 8;
                    m_index = m_LSensorArray[0].SensorIndex * 4 + 0;
                    m_value |= buf[m_index];

                    m_Load = (float)(m_value * 5.0 / 12.0);

                    //this.lblFShow.Text = m_Load.ToString();
                }

                /*---------------------m_Displacement display----------------*/

                if (m_DisplacementSensorCount != 0)
                {
                    m_index = m_DSensorArray[0].SensorIndex * 4 + 3;
                    m_value = buf[m_index];

                    m_value = m_value << 8;
                    m_index = m_DSensorArray[0].SensorIndex * 4 + 2;
                    m_value |= buf[m_index];

                    m_value = m_value << 8;
                    m_index = m_DSensorArray[0].SensorIndex * 4 + 1;
                    m_value |= buf[m_index];

                    m_value = m_value << 8;
                    m_index = m_DSensorArray[0].SensorIndex * 4 + 0;
                    m_value |= buf[m_index];

                    m_Displacement = (float)(m_value);
                    //this.lblDShow.Text = m_Displacement.ToString();

                }

                /*---------------------elongate display----------------*/

                if (m_ElongateSensorCount != 0)
                {
                    m_index = m_ESensorArray[0].SensorIndex * 4 + 3;
                    m_value = buf[m_index];

                    m_value = m_value << 8;
                    m_index = m_ESensorArray[0].SensorIndex * 4 + 2;
                    m_value |= buf[m_index];

                    m_value = m_value << 8;
                    m_index = m_ESensorArray[0].SensorIndex * 4 + 1;
                    m_value |= buf[m_index];

                    m_value = m_value << 8;
                    m_index = m_ESensorArray[0].SensorIndex * 4 + 0;
                    m_value |= buf[m_index];

                    m_Elongate = (float)(m_value / 12.0);

                    //this.lblBXShow.Text = m_Elongate.ToString();
                }
                /*-------------------time display-----------------------*/
                //		(m_SensorCount+1)*4
                //		m_valueS=buf[m_SensorCount*4+2];	 //+3

                m_valueS = buf[_m_SensorCount * 4 + 2];     //+2
                m_valueS = m_valueS << 4;
                m_value = buf[_m_SensorCount * 4 + 1] & 0x0f0;//+1
                m_value = m_value >> 4;
                m_valueS |= (m_value & 0x0f);

                m_value = buf[_m_SensorCount * 4 + 1] & 0x0f;  //+1
                m_value = m_value << 8;
                m_value |= buf[_m_SensorCount * 4 + 0];      //+0

                m_Time = (float)((m_valueS * 1000.0 + m_value) / 1000.0);

                m_Stress = (float)(m_Load / m_S0);
            }
            ////Test Curve Data
            m_Load += 1.5f;

            m_Time = (float)(DateTime.Now - dt).TotalSeconds;
            m_Displacement++;
            m_Elongate = (float)Math.Log(m_Displacement);
            m_Strain = (float)Math.Sqrt(m_Displacement);
            m_Stress = (float)Math.Log10(m_Elongate);

            if (isTest)
            {
                _dlgGatherData(this.m_Load, this.m_Displacement, this.m_Elongate, this.m_Time, this.m_Stress, this.m_Strain);
            }
        }

        //private void _showDataTimer_Tick(object sender, EventArgs e)
        //{
        //    showData(this.lblFShow, this.lblDShow, this.lblBXShow, this.lblTimeShow, this.lblYLShow, this.lblYBShow, this.lblkN, this.lblmm1, this.lblmm2, this.lblmm3);
        //}


        ////Revise data and show 
        private void showData(System.Windows.Forms.Label lbl_LoadShow,//力
            System.Windows.Forms.Label lbl_DisplacementShow,//位移
            System.Windows.Forms.Label lbl_ElongateShow,//变形
            System.Windows.Forms.Label lbl_TimeShow,//时间
            System.Windows.Forms.Label lbl_StressShow,//应力
            System.Windows.Forms.Label lbl_StrainShow,//应变 
            System.Windows.Forms.Label lbl_kN,
            System.Windows.Forms.Label lbl_mm1,
            System.Windows.Forms.Label lbl_mm2,
            System.Windows.Forms.Label lbl_mm3)
        {
            Thread.Sleep(5);

            if (Math.Abs(m_Load) > 1000)
            {
                lblFShow.Text = (m_Load / 1000.0).ToString("f3");
                lblkN.Text = "kN";
                lblFShow.Refresh();
                lblkN.Refresh();
            }
            else
            {
                lblFShow.Text = m_Load.ToString("f0");
                lblkN.Text = "N";
                lblFShow.Refresh();
                lblkN.Refresh();
            }

            //位移
            if (Math.Abs(m_Displacement) > 1000.0)
            {
                lblDShow.Text = (m_Displacement / 1000.0).ToString("f3");
                lblmm1.Text = "mm";
                lblDShow.Refresh();
                lblmm1.Refresh();
            }
            else
            {
                lblDShow.Text = m_Displacement.ToString();
                lblmm1.Text = "μm";
                lblDShow.Refresh();
                lblmm1.Refresh();
            }

            //变形
            if (Math.Abs(m_Elongate) > 1000 && lblBXShow.Visible == true)
            {
                lblBXShow.Text = (m_Elongate / 1000.0).ToString("f3");
                lblmm2.Text = "mm";
                lblBXShow.Refresh();
                lblmm2.Refresh();
            }
            else
            {
                lblBXShow.Text = m_Elongate.ToString("f2");
                lblmm2.Text = "μm";
                lblBXShow.Refresh();
                lblmm2.Refresh();
            }

            //应变
            if (Math.Abs(m_Strain) > 1000 && lblYBShow.Visible == true)
            {
                lblYBShow.Text = (m_Strain / 1000.0).ToString("f3");
                lblmm3.Text = "mm";
                lblYBShow.Refresh();
                lblmm3.Refresh();
            }
            else
            {
                lblYBShow.Text = m_Strain.ToString("f2");
                lblmm3.Text = "μm";
                lblYBShow.Refresh();
                lblmm3.Refresh();
            }
            //应力

            lbl_StressShow.Text = m_Stress.ToString("f2");
            lbl_StressShow.Refresh();
            lbl_TimeShow.Text = m_Time.ToString("f2");
            lbl_TimeShow.Refresh();
        }

        //拉伸和压缩试验采集数据处理过程
        private void _mtimer_T_Tick(object sender, EventArgs e)
        {

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
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Curve"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Curve");
            }
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Curve\\Tensile"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Curve\\Tensile");
            }
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Curve\\Compress"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Curve\\Compress");
            }
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Curve\\Bend"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Curve\\Bend");
            }
        }


        DateTime dt = DateTime.Now;
        delegate void delInitMsChart(System.Windows.Forms.DataVisualization.Charting.Chart _chart);
        private void frmTestResult_Load(object sender, EventArgs e)
        {
            //传递主窗口硬件参数
            _SensorArrayFlag = _fmMain.SensorArrayFlag;
            _m_SensorCount = _fmMain.m_SensorCount;
            m_LoadSensorCount = _fmMain.m_LoadSensorCount;
            m_DisplacementSensorCount = _fmMain.m_DisplacementSensorCount;
            m_ElongateSensorCount = _fmMain.m_ElongateSensorCount;
            //获取传感器数组
            m_SensorArray = _fmMain.m_SensorArray;
            m_DSensorArray = _fmMain.m_DSensorArray;
            m_LSensorArray = _fmMain.m_LSensorArray;
            m_ESensorArray = _fmMain.m_ESensorArray;

            if (m_DSensorArray == null || m_LSensorArray == null || m_ESensorArray == null)
            {
                MessageBox.Show("未连接设备");
                return;
            }
            buf = new byte[203];
            this.zedGraphControl.Invalidated += new InvalidateEventHandler(zedGraphControl_Invalidated);

            //Control.CheckForIllegalCrossThreadCalls = false;
            //ms间隔

            initResultCurve(this.zedGraphControl);            
            ShowResultPanel();
            ShowResultCurve();

            if (_threadInitRealCurve == null)
            {
                _threadInitRealCurve = new Thread(new ThreadStart(InitRealTimeThreadPro));
                _threadInitRealCurve.Start();
                _threadInitRealCurve.IsBackground = true;
                //_threadInitRealCurve.Join();
                _threadInitRealCurve = null;
            }
            //InitRealTimeThreadPro();
            //initRealTimeCurve(this.realTimeZedGraph);
            

            //initRealTimeChart(this.chart1);

            ReadSample(this.tvTestSample);
            readFinishSample(this.dataGridView, "");
            CreateFolder();

            //数据传输
            //_mtimer_T = new System.Timers.Timer();
            //_mtimer_T.Interval = 50;
            //_mtimer_T.Enabled = true;
            //_mtimer_T.AutoReset = true;
            //_mtimer_T.Elapsed += new System.Timers.ElapsedEventHandler(_mtimer_T_Elapsed);
            //_mtimer_T.Start();

            while (!this.IsHandleCreated)
            {
                ;
            }
            //发送命令
            ThreadSendOrder();
            //显示力线程
            //ThreadLoadShow();
            ////显示位移线程
            //ThreadDisplacementShow();
            ////显示变形线程
            //ThreadElongateShow();

            //ThreadStrainShow();

            //ThreadStressShow();

            //ThreadTimeShow(); 

            //ThreadShowData();

            _threadShowData = new Thread(new ThreadStart(_showdata));
            _threadShowData.Start();
            _threadShowData.IsBackground = true;


            //this.realTimeZedGraph.Invalidated += new InvalidateEventHandler(realTimeZedGraph_Invalidated);
            this.chart1.Invalidated += new InvalidateEventHandler(chart1_Invalidated);
            //this.zedGraphControl.Invalidated += new InvalidateEventHandler(zedGraphControl_Invalidated);

            //GraphPane pane = this.zedGraphControl.GraphPane;
            //pane.Chart.Rect = new RectangleF(10, 10, this.splitContainer1.Panel1.Width,this.splitContainer1.Panel1.Height);
            //TextObj testObj = new TextObj("X Axis Additional Text",55,5);
            //zedGraphControl.GraphPane.GraphObjList.Add(testObj);
            //zedGraphControl.Refresh(); 
        }

     

        //显示实时曲线界面
        private void ShowCurvePanel()
        {
            //this.chart1.Visible = true;             
            //this.chart1.Parent = this.panel3;
            //this.chart1.Dock = DockStyle.Fill;           
            this.chart1.Visible = true;
            this.tsbtn_Exit.Visible = false;
            this.tsbtnReturn.Visible = true;
            this.toolStrip1.Refresh();
            this.panelChart.Visible = true;
            this.panelChart.Parent = this.panel3;
            this.panelChart.Dock = DockStyle.Fill;
            this.lblX2.Left = this.chart1.Width / 2 - lblX2.Width /2;
            this.lblX2.Top = 0;
            this.tsbtnSetRealtimeCurve.Visible = true;
            this.splitContainer1.Visible = false;
            this.tsbtnShowResultCurve.Visible = false;         
        }

        //显示试验结果界面 this.splitContainer1.Visible = true;
        private void ShowResultPanel()
        {
            //this.realTimeZedGraph.Visible = false;
            this.tsbtn_Exit.Visible = true;
            this.tsbtnReturn.Visible = false;
            this.toolStrip1.Refresh();
            this.panelChart.Visible = false;
            this.splitContainer1.Visible = true;
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Parent = this.panel3;
            this.tsbtnSetRealtimeCurve.Visible = false;
            this.tsbtnShowResultCurve.Visible = true;
            this.Refresh();
        }


        private void btnChangeYL_Click(object sender, EventArgs e)
        {

        }

        private void tsbtn_Exit_Click(object sender, EventArgs e)
        {
            //this._mtimer_T.Stop();
            //this._mtimer_T.Dispose(); 
            if(_threadShowData != null)
                _threadShowData.Abort();
            _showThreadFlag = false;
            Thread.Sleep(200);
            //while (this._mtimer_GatherData.IsRunning)
            //{ this._mtimer_GatherData.Stop(); }
            //this._mtimer_GatherData.Dispose();
            //this._showDataTimer.Stop();
            this.Close();
            this.Dispose();
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

            _RPPList0 = new RollingPointPairList(50000);

            //ZedGraph  
            //zedGraphControl1.IsAntiAlias = true; 
            Legend l = zgControl.GraphPane.Legend;
            l.IsShowLegendSymbols = true;
            l.Gap = 0;
            l.Position = LegendPos.InsideTopLeft;
            l.FontSpec.Size = 18;
            l.IsVisible = false;

            // Set the titles and axis labels
            _ResultPanel = zgControl.GraphPane;
            _ResultPanel.Margin.All = 8;
            _ResultPanel.Margin.Top = 15;
            _ResultPanel.Title.Text = "";
            _ResultPanel.Title.IsVisible = false;
            _ResultPanel.IsFontsScaled = false;
            zgControl.IsZoomOnMouseCenter = false;

            //XAxis
            //最后的显示值隐藏
            _ResultPanel.XAxis.Scale.FontSpec.Size = 17;
            _ResultPanel.XAxis.Title.FontSpec.Size = 17;
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
            _ResultPanel.XAxis.Scale.Max = 100;
            _ResultPanel.XAxis.Scale.MaxAuto = true;
            _ResultPanel.XAxis.MajorGrid.IsVisible = true;

            _ResultPanel.YAxis.Title.Text = "Y";
            _ResultPanel.YAxis.Title.Gap = -0.5f;
            _ResultPanel.YAxis.Scale.FontSpec.FontColor = Color.FromArgb(34, 67, 108);
            _ResultPanel.YAxis.Title.FontSpec.FontColor = Color.FromArgb(34, 67, 108);
            _ResultPanel.YAxis.Scale.FontSpec.Size = 17;
            _ResultPanel.YAxis.Title.FontSpec.Size = 17;
            _ResultPanel.YAxis.Scale.Format = "0.0";
            _ResultPanel.YAxis.Scale.LabelGap = 0;
            // Align the Y2 axis labels so they are flush to the axis 
            _ResultPanel.YAxis.Scale.AlignH = AlignH.Center;
            _ResultPanel.YAxis.Scale.Min = 0;
            _ResultPanel.YAxis.Scale.MinAuto = false;
            _ResultPanel.YAxis.Scale.Max = 100;
            _ResultPanel.YAxis.Scale.MaxAuto = true;
            _ResultPanel.YAxis.MajorGrid.IsVisible = true;

            zgControl.AxisChange();
        }

        //private void CreateRealtimeZedGraph()
        //{
        //    if (this.realTimeZedGraph.GraphPane.CurveList.Count > 0)
        //    {
        //        foreach (CurveItem ci in this.realTimeZedGraph.GraphPane.CurveList)
        //        {
        //            realTimeZedGraph.GraphPane.CurveList.Remove(ci);
        //        }
        //    }

        //    //realTimeZedGraph = new ZedGraphControl();

        //    realTimeZedGraph.Paint += new PaintEventHandler(realTimeZedGraph_Paint);

        //    //realTimeZedGraph.Invalidated += new InvalidateEventHandler(realTimeZedGraph_Invalidated);
        //    realTimeZedGraph.Visible = true;
        //    realTimeZedGraph.Parent = this.panel3;
        //    realTimeZedGraph.BringToFront();
        //    realTimeZedGraph.Dock = DockStyle.Fill;
        //}

        void realTimeZedGraph_Invalidated(object sender, InvalidateEventArgs e)
        {
            if (_RealTimePanel.XAxis != null)
            {
                Scale sScale = _RealTimePanel.XAxis.Scale;

                switch (_X1)// -请选择-   时间,s  位移,μm  应变,μm
                {
                    case 1:
                        if (m_Time > sScale.Max)
                        {
                            sScale.Max = 2 * sScale.Max;
                        }
                        break;
                    case 2:
                        if (m_Displacement > sScale.Max)
                        {
                            sScale.Max = 2 * sScale.Max; // += (250 + 50 * _ScaleMaxIndex[1]);
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
                        }
                        break;
                    case 3:
                        if (m_Strain > sScale.Max)
                        {
                            sScale.Max = 2 * sScale.Max; ;
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
                        }
                        break;
                }
                _RealTimePanel.XAxis.Scale.MajorStep = (_RealTimePanel.XAxis.Scale.Max - _RealTimePanel.XAxis.Scale.Min) / 5;
                _RealTimePanel.XAxis.Scale.MinorStep = _RealTimePanel.XAxis.Scale.MajorStep / 5;
            }

            if (_RealTimePanel.X2Axis != null)
            {
                Scale sScale = _RealTimePanel.X2Axis.Scale;
                switch (_X2)// -请选择-   时间,s  位移,μm  应变,μm
                {
                    case 1:
                        if (m_Time > sScale.Max)
                        {
                            sScale.Max = 2 * sScale.Max;
                        }
                        break;
                    case 2:
                        if (m_Displacement > sScale.Max)
                        {
                            sScale.Max = 2 * sScale.Max;
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
                        }
                        break;
                    case 3:
                        if (m_Strain > sScale.Max)
                        {
                            sScale.Max = 2 * sScale.Max;
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
                        }
                        break;
                }

                _RealTimePanel.X2Axis.Scale.MajorStep = (_RealTimePanel.X2Axis.Scale.Max - _RealTimePanel.X2Axis.Scale.Min) / 5;
                _RealTimePanel.X2Axis.Scale.MinorStep = _RealTimePanel.X2Axis.Scale.MajorStep / 5;
            }

            if (_RealTimePanel.YAxisList[1] != null)
            {   //-请选择- 0
                //力,kN 1
                //应力,MPa 2
                //变形,μm 3
                //位移,μm 4
                Scale sScale = _RealTimePanel.YAxisList[1].Scale;
                switch (_Y1)
                {
                    case 1:
                        if (m_Load > sScale.Max)
                        {
                            sScale.Max = 2 * sScale.Max;
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
                        }
                        break;
                    case 2:
                        if (m_Stress > sScale.Max)
                        {
                            sScale.Max = 2 * sScale.Max;
                        }
                        break;
                    case 3:
                        if (m_Elongate > sScale.Max)
                        {
                            sScale.Max = 2 * sScale.Max;
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
                        }
                        break;
                    case 4:
                        if (m_Displacement > sScale.Max)
                        {
                            sScale.Max = 2 * sScale.Max;
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
                        }
                        break;
                }
                _RealTimePanel.YAxisList[1].Scale.MajorStep = (_RealTimePanel.YAxisList[1].Scale.Max - _RealTimePanel.YAxisList[1].Scale.Min) / 5;
                _RealTimePanel.YAxisList[1].Scale.MinorStep = _RealTimePanel.YAxisList[1].Scale.MajorStep / 5;
            }

            if (_RealTimePanel.YAxis != null)
            {
                Scale sScale = _RealTimePanel.YAxis.Scale;
                switch (_Y2)
                {
                    case 1:
                        if (m_Load > sScale.Max)
                        {
                            sScale.Max = 2 * sScale.Max;
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
                        }
                        break;
                    case 2:
                        if (m_Stress > sScale.Max)
                        {
                            sScale.Max = 2 * sScale.Max;
                        }
                        break;
                    case 3:
                        if (m_Elongate > sScale.Max)
                        {
                            sScale.Max = 2 * sScale.Max;
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
                        }
                        break;
                    case 4:
                        if (m_Displacement > sScale.Max)
                        {
                            sScale.Max = 2 * sScale.Max;
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
                        }
                        break;
                }
                _RealTimePanel.YAxis.Scale.MajorStep = (_RealTimePanel.YAxis.Scale.Max - _RealTimePanel.YAxis.Scale.Min) / 5;
                _RealTimePanel.YAxis.Scale.MinorStep = _RealTimePanel.YAxis.Scale.MajorStep / 5;
            }

            //throw new NotImplementedException();
        }
        //void zedGraphControl_Invalidated(object sender, InvalidateEventArgs e)
        //{
        //    if (_ResultPanel.XAxis != null)
        //    {
        //        Scale sScale = _RealTimePanel.XAxis.Scale;

        //        switch (_ShowX)// -请选择-   时间,s  位移,μm  应变,μm
        //        {
        //            case 1:
        //                if (m_Time > sScale.Max)
        //                {
        //                    sScale.Max = 2 * sScale.Max; // += 50 * _ScaleMaxIndex[0];
        //                    //sScale.Min = sScale.Max / 2;
        //                }
        //                break;
        //            case 2:
        //                //if (m_Displacement > sScale.Max)
        //                //{
        //                //    sScale.Max = 2 * sScale.Max; // += (250 + 50 * _ScaleMaxIndex[1]);
        //                if (sScale.Max > 1000)
        //                {
        //                    sScale.Mag = 3;
        //                    sScale.Format = "0.00";
        //                }
        //                else
        //                {
        //                    sScale.Mag = 0;
        //                    sScale.Format = "0.0";
        //                }
        //                //sScale.Min = sScale.Max / 2;
        //                //}
        //                break;
        //            case 3:
        //                //if (m_Strain > sScale.Max)
        //                //{
        //                //sScale.Max = 2 * sScale.Max; // += (250 + 50 * _ScaleMaxIndex[2]);
        //                //sScale.Min = sScale.Max / 2;
        //                if (sScale.Max > 1000)
        //                {
        //                    sScale.Mag = 3;
        //                    sScale.Format = "0.00";
        //                }
        //                else
        //                {
        //                    sScale.Mag = 0;
        //                    sScale.Format = "0.0";
        //                }
        //                //}
        //                break;
        //        }
        //        _ResultPanel.XAxis.Scale.MajorStep = (_RealTimePanel.XAxis.Scale.Max - _RealTimePanel.XAxis.Scale.Min) / 5;
        //        _ResultPanel.XAxis.Scale.MinorStep = _RealTimePanel.XAxis.Scale.MajorStep / 5;
        //    }

        //    if (_ResultPanel.YAxis != null)
        //    {
        //        Scale sScale = _RealTimePanel.YAxis.Scale;
        //        switch (_ShowY)
        //        {
        //            case 1:
        //                //if (m_Load > sScale.Max)
        //                //{
        //                //sScale.Max = 2 * sScale.Max; // += 250 + 50 * _ScaleMaxIndex[10];
        //                //sScale.Min =  50 * _ScaleMaxIndex[10];
        //                if (sScale.Max > 1000)
        //                {
        //                    sScale.Mag = 3;
        //                    sScale.Format = "0.00";
        //                }
        //                else
        //                {
        //                    sScale.Mag = 0;
        //                    sScale.Format = "0.0";
        //                }
        //                //}
        //                break;
        //            case 2:
        //                if (m_Stress > sScale.Max)
        //                {
        //                    sScale.Max = 2 * sScale.Max; // += 250 + _ScaleMaxIndex[11] * 50;
        //                    //sScale.Min =  _ScaleMaxIndex[11] * 50;
        //                }
        //                break;
        //            case 3:
        //                //if (m_Elongate > sScale.Max)
        //                //{
        //                //sScale.Max = 1.5 * sScale.Max; // += 250 + 50 * _ScaleMaxIndex[12];
        //                //sScale.Min =  50 * _ScaleMaxIndex[12];
        //                if (sScale.Max > 1000)
        //                {
        //                    sScale.Mag = 3;
        //                    sScale.Format = "0.00";
        //                }
        //                else
        //                {
        //                    sScale.Mag = 0;
        //                    sScale.Format = "0.0";
        //                }
        //                //}
        //                break;
        //            case 4:
        //                //if (m_Displacement > sScale.Max)
        //                //{
        //                //sScale.Max = 2 * sScale.Max; 
        //                //sScale.Min = 50 * _ScaleMaxIndex[13];
        //                if (sScale.Max > 1000)
        //                {
        //                    sScale.Mag = 3;
        //                    sScale.Format = "0.00";
        //                }
        //                else
        //                {
        //                    sScale.Mag = 0;
        //                    sScale.Format = "0.0";
        //                }
        //                //}
        //                break;
        //        }
        //        _ResultPanel.YAxis.Scale.MajorStep = (_RealTimePanel.YAxis.Scale.Max - _RealTimePanel.YAxis.Scale.Min) / 5;
        //        _ResultPanel.YAxis.Scale.MinorStep = _RealTimePanel.YAxis.Scale.MajorStep / 5;
        //    }

        //    //if (_RealTimePanel.Y2Axis != null)
        //    //{
        //    //    Scale sScale = _RealTimePanel.Y2Axis.Scale;
        //    //    switch (_Y3)
        //    //    {
        //    //        case 1:
        //    //            if (m_Load > sScale.Max)
        //    //            {
        //    //                _ScaleMaxIndex[14]++;
        //    //                sScale.Max += 250 + 50 * _ScaleMaxIndex[14];
        //    //            }
        //    //            break;
        //    //        case 2:
        //    //            if (m_Stress > sScale.Max)
        //    //            {
        //    //                _ScaleMaxIndex[15]++;
        //    //                sScale.Max += 250 + 50 * _ScaleMaxIndex[15];
        //    //            }
        //    //            break;
        //    //        case 3:
        //    //            if (m_Elongate > sScale.Max)
        //    //            {
        //    //                _ScaleMaxIndex[16]++;
        //    //                sScale.Max += 250 + 50 * _ScaleMaxIndex[16];
        //    //            }
        //    //            break;
        //    //        case 4:
        //    //            if (m_Displacement > sScale.Max)
        //    //            {
        //    //                _ScaleMaxIndex[17]++;
        //    //                sScale.Max += 250 + 50 * _ScaleMaxIndex[17];
        //    //            }
        //    //            break;
        //    //    }
        //    //    _RealTimePanel.Y2Axis.Scale.MajorStep = (_RealTimePanel.Y2Axis.Scale.Max - _RealTimePanel.Y2Axis.Scale.Min) / 5;
        //    //    _RealTimePanel.Y2Axis.Scale.MinorStep = _RealTimePanel.Y2Axis.Scale.MajorStep / 5;
        //    //}

        //    //throw new NotImplementedException();
        //}
        void realTimeZedGraph_Paint(object sender, PaintEventArgs e)
        {
            //if (_RealTimePanel.XAxis != null)
            //{
            //    int max = (int)_RealTimePanel.XAxis.Scale.Max / 100;
            //    double mod = _RealTimePanel.XAxis.Scale.Max % 100;
            //    if (mod == 0.0)
            //    {
            //        _RealTimePanel.XAxis.Scale.Max = (max * 100);
            //    }
            //    else
            //    {
            //        _RealTimePanel.XAxis.Scale.Max = (max * 100) + 100;
            //    }
            //    _RealTimePanel.XAxis.Scale.MajorStep = (_RealTimePanel.XAxis.Scale.Max - _RealTimePanel.XAxis.Scale.Min) / 5;
            //    _RealTimePanel.XAxis.Scale.MinorStep = _RealTimePanel.XAxis.Scale.MajorStep / 5;
            //}

            //if (_RealTimePanel.X2Axis != null)
            //{

            //    _RealTimePanel.X2Axis.Scale.MajorStep = (_RealTimePanel.X2Axis.Scale.Max - _RealTimePanel.X2Axis.Scale.Min) / 5;
            //    _RealTimePanel.X2Axis.Scale.MinorStep = _RealTimePanel.X2Axis.Scale.MajorStep / 5;
            //}

            //if (_RealTimePanel.YAxisList[1] != null)
            //{
            //    _RealTimePanel.YAxisList[1].Scale.MajorStep = (_RealTimePanel.YAxisList[1].Scale.Max - _RealTimePanel.YAxisList[1].Scale.Min) / 5;
            //    _RealTimePanel.YAxisList[1].Scale.MinorStep = _RealTimePanel.YAxisList[1].Scale.MajorStep / 5;
            //}

            //if (_RealTimePanel.YAxis != null)
            //{
            //    _RealTimePanel.YAxis.Scale.MajorStep = (_RealTimePanel.YAxis.Scale.Max - _RealTimePanel.YAxis.Scale.Min) / 5;
            //    _RealTimePanel.YAxis.Scale.MinorStep = _RealTimePanel.YAxis.Scale.MajorStep / 5;
            //}

            //if (_RealTimePanel.Y2Axis != null)
            //{

            //    _RealTimePanel.Y2Axis.Scale.MajorStep = (_RealTimePanel.Y2Axis.Scale.Max - _RealTimePanel.Y2Axis.Scale.Min) / 5;
            //    _RealTimePanel.Y2Axis.Scale.MinorStep = _RealTimePanel.Y2Axis.Scale.MajorStep / 5;
            //}

            //this.realTimeZedGraph.AxisChange();
            //throw new NotImplementedException();
        }

        //Init realtime show curveList
        private void initRealTimeCurve(object o)
        {

            ZedGraph.ZedGraphControl zgControlRealTime = (ZedGraph.ZedGraphControl)o;
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

            _RPPList0 = new RollingPointPairList(50000);
            _RPPList1 = new RollingPointPairList(50000);
            _RPPList2 = new RollingPointPairList(50000);
            _RPPList3 = new RollingPointPairList(50000);
            _RPPF_T = new RollingPointPairList(50000);

            //zgControlRealTime.BorderStyle = BorderStyle.FixedSingle;
            //zgControlRealTime.GraphPane.Chart.Border.IsVisible = false;

            //zedGraphControl1.IsAntiAlias = true; 
            Legend l = zgControlRealTime.GraphPane.Legend;
            l.IsVisible = true;
            l.Gap = 0;
            l.Position = LegendPos.TopFlushLeft;
            l.FontSpec.Size = 15;
            l.Border.IsVisible = false;
            l.FontSpec.StringAlignment = StringAlignment.Center;
            // Set the titles and axis labels
            zgControlRealTime.IsZoomOnMouseCenter = false;
            _RealTimePanel = zgControlRealTime.GraphPane;
            _RealTimePanel.Margin.All = 0;
            //_RealTimePanel.Margin.Top = 0;
            //_RealTimePanel.Margin.Left = 0; 
            _RealTimePanel.Title.Text = "";
            _RealTimePanel.Title.IsVisible = false;
            _RealTimePanel.IsFontsScaled = false;
            _RealTimePanel.IsPenWidthScaled = false;
            _RealTimePanel.Margin.Right = 0;

            // _RealTimePanel.Fill = new Fill(Color.FromArgb(236, 236, 236), Color.FromArgb(204, 204, 204), 0f);

            //XAxis

            //最后的显示值隐藏
            //_RealTimePanel.XAxis.Scale.IsSkipLastLabel = true;
            //_RealTimePanel.XAxis.Scale.IsLabelsInside = true;  

            _RealTimePanel.XAxis.Title.FontSpec.Size = 17;
            _RealTimePanel.XAxis.Title.Gap = -0.5f;
            _RealTimePanel.XAxis.Scale.FontSpec.Size = 17;
            _RealTimePanel.XAxis.Scale.FontSpec.IsBold = true;
            //_RealTimePanel.XAxis.Title.FontSpec.FontColor = Color.FromArgb(34, 67, 108);
            //_RealTimePanel.XAxis.Scale.FontSpec.FontColor = Color.FromArgb(34, 67, 108);
            //_RealTimePanel.XAxis.MajorTic.IsOpposite = false;
            //_RealTimePanel.XAxis.MinorTic.IsOpposite = false;
            _RealTimePanel.XAxis.Title.Text = "X1";
            _RealTimePanel.XAxis.Scale.AlignH = AlignH.Center;
            _RealTimePanel.XAxis.Scale.IsLabelsInside = false;
            _RealTimePanel.XAxis.Scale.LabelGap = 0;
            _RealTimePanel.XAxis.AxisGap = 0;
            _RealTimePanel.XAxis.Scale.Format = "0.0";
            _RealTimePanel.XAxis.Scale.Min = 0;
            _RealTimePanel.XAxis.Scale.Max = 100;
            //_RealTimePanel.XAxis.Scale.MaxGrace = 0.5;
            //_RealTimePanel.XAxis.Scale.MinGrace = 0.5;
            _RealTimePanel.XAxis.Scale.MaxAuto = false;
            _RealTimePanel.XAxis.MajorGrid.IsVisible = true;

            //自动改变标题和坐标轴
            //_RealTimePanel.XAxis.Title.IsOmitMag = true;
            //_RealTimePanel.XAxis.Scale.Mag = 3; 
            //_RealTimePanel.XAxis.Scale.MagAuto = true;         
            //_RealTimePanel.XAxis.Scale.IsUseTenPower = true;

            _RealTimePanel.XAxis.MajorGrid.Color = Color.Purple;
            _RealTimePanel.XAxis.MajorTic.IsOpposite = false;
            _RealTimePanel.XAxis.MinorTic.IsOutside = false;
            _RealTimePanel.XAxis.Color = Color.Purple;
            _RealTimePanel.XAxis.Title.FontSpec.FontColor = Color.Purple;
            _RealTimePanel.XAxis.Scale.FontSpec.FontColor = Color.Purple;


            _RealTimePanel.X2Axis.Title.Text = "X2";
            _RealTimePanel.X2Axis.Title.Gap = -0.5f;
            _RealTimePanel.X2Axis.IsVisible = true;
            _RealTimePanel.X2Axis.AxisGap = 0;
            //_RealTimePanel.X2Axis.Scale.FontSpec.FontColor =  Color.FromArgb(34, 67, 108);
            //_RealTimePanel.X2Axis.Title.FontSpec.FontColor =  Color.FromArgb(34, 67, 108);
            _RealTimePanel.X2Axis.Scale.FontSpec.IsBold = true;
            _RealTimePanel.X2Axis.Scale.IsLabelsInside = false;
            _RealTimePanel.X2Axis.Scale.FontSpec.Size = 17;
            _RealTimePanel.X2Axis.Title.FontSpec.Size = 17;
            _RealTimePanel.X2Axis.Scale.AlignH = AlignH.Center;
            _RealTimePanel.X2Axis.Scale.Format = "0.0";
            _RealTimePanel.X2Axis.Scale.LabelGap = 0;
            //_RealTimePanel.X2Axis.Scale.FontSpec.Angle = 225;
            //_RealTimePanel.X2Axis.Scale.MaxGrace = 0.5;
            //_RealTimePanel.X2Axis.Scale.MinGrace = 0.5; 
            _RealTimePanel.X2Axis.Scale.Min = 0;
            _RealTimePanel.X2Axis.Scale.Max = 100;
            _RealTimePanel.X2Axis.Scale.MaxAuto = false;
            _RealTimePanel.X2Axis.MajorGrid.IsVisible = true;
            _RealTimePanel.X2Axis.Scale.IsUseTenPower = false;
            _RealTimePanel.X2Axis.Color = Color.Green;
            _RealTimePanel.X2Axis.Title.FontSpec.FontColor = Color.Green;
            _RealTimePanel.X2Axis.Scale.FontSpec.FontColor = Color.Green;

            //_RealTimePanel.X2Axis.Scale.IsSkipFirstLabel = true;

            _RealTimePanel.AddYAxis("1");
            _RealTimePanel.AddYAxis("2");

            //YAxis
            _RealTimePanel.YAxis.Scale.FontSpec.Size = 17;
            _RealTimePanel.YAxis.Title.FontSpec.Size = 17;
            //_RealTimePanel.YAxis.Scale.FontSpec.FontColor =  Color.FromArgb(34, 67, 108);
            //_RealTimePanel.YAxis.Title.FontSpec.FontColor =  Color.FromArgb(34, 67, 108); 
            _RealTimePanel.YAxis.Title.Text = "Y2";
            _RealTimePanel.YAxis.Title.Gap = -1.0f;
            //_RealTimePanel.YAxis.MinSpace = 1;
            _RealTimePanel.YAxis.Color = Color.Blue;
            _RealTimePanel.YAxis.Scale.FontSpec.IsBold = true;
            _RealTimePanel.YAxis.Scale.AlignH = AlignH.Left;
            //_RealTimePanel.YAxis.Scale.IsLabelsInside = true;
            _RealTimePanel.YAxis.Scale.Format = "0.0";
            _RealTimePanel.YAxis.Scale.Min = 0;
            _RealTimePanel.YAxis.Scale.Max = 100;
            //_RealTimePanel.YAxis.Scale.MaxGrace = 0.0;
            //_RealTimePanel.YAxis.Scale.MinGrace = 0.0;
            _RealTimePanel.YAxis.Scale.MaxAuto = false;
            _RealTimePanel.YAxis.MajorGrid.IsVisible = true;
            _RealTimePanel.YAxis.Scale.IsUseTenPower = false;
            _RealTimePanel.YAxis.Scale.IsSkipFirstLabel = false;
            _RealTimePanel.YAxis.Scale.IsReverse = false;
            //_RealTimePanel.YAxis.MajorTic.IsOutside = false;
            //_RealTimePanel.YAxis.MajorTic.IsInside = true;
            //_RealTimePanel.YAxis.MinorTic.IsOutside = false;
            //_RealTimePanel.YAxis.MinorTic.IsInside = true;
            _RealTimePanel.YAxis.Title.FontSpec.FontColor = Color.Blue;
            _RealTimePanel.YAxis.Scale.FontSpec.FontColor = Color.Blue;
            _RealTimePanel.YAxis.Scale.FontSpec.Angle = 180;
            _RealTimePanel.YAxis.Scale.LabelGap = 0.6f;


            _RealTimePanel.YAxisList[1].Title.Text = "Y1";
            _RealTimePanel.YAxisList[1].Title.Gap = -1.0f;
            _RealTimePanel.YAxisList[1].AxisGap = 5.0f;
            _RealTimePanel.YAxisList[1].Scale.LabelGap = 0;
            _RealTimePanel.YAxisList[1].MajorTic.IsBetweenLabels = true;
            //_RealTimePanel.YAxisList[1].Scale.FontSpec.FontColor =  Color.FromArgb(34, 67, 108);
            //_RealTimePanel.YAxisList[1].Title.FontSpec.FontColor =  Color.FromArgb(34, 67, 108);
            _RealTimePanel.YAxisList[1].Scale.FontSpec.Size = 17;
            _RealTimePanel.YAxisList[1].Title.FontSpec.Size = 17;
            _RealTimePanel.YAxisList[1].Scale.FontSpec.IsBold = true;
            _RealTimePanel.YAxisList[1].Scale.AlignH = AlignH.Left;
            //_RealTimePanel.YAxisList[1].Scale.IsLabelsInside = false; 
            _RealTimePanel.YAxisList[1].Scale.Format = "0.0";
            //_RealTimePanel.YAxisList[1].Scale.MaxGrace = 0.0;
            //_RealTimePanel.YAxisList[1].Scale.MinGrace = 0.0;
            _RealTimePanel.YAxisList[1].Scale.Min = 0;
            _RealTimePanel.YAxisList[1].Scale.Max = 100;
            _RealTimePanel.YAxisList[1].Scale.MaxAuto = false;
            _RealTimePanel.YAxisList[1].MajorGrid.IsVisible = true;
            _RealTimePanel.YAxisList[1].Scale.IsUseTenPower = false;
            //_RealTimePanel.YAxisList[1].Scale.IsSkipFirstLabel = true;
            //_RealTimePanel.YAxisList[1].MajorTic.IsOutside = true;
            //_RealTimePanel.YAxisList[1].MajorTic.IsInside = false;
            //_RealTimePanel.YAxisList[1].MinorTic.IsOutside = true;
            //_RealTimePanel.YAxisList[1].MinorTic.IsInside = false;
            _RealTimePanel.YAxisList[1].Color = Color.FromArgb(230, 0, 0);
            _RealTimePanel.YAxisList[1].Title.FontSpec.FontColor = Color.FromArgb(230, 0, 0);
            _RealTimePanel.YAxisList[1].Scale.FontSpec.FontColor = Color.FromArgb(230, 0, 0);
            _RealTimePanel.YAxisList[1].Scale.LabelGap = 0.6f;
            _RealTimePanel.YAxisList[1].Scale.FontSpec.Angle = 180;

            //_RealTimePanel.YAxis.MinorGrid.IsVisible = true;
            //_RealTimePanel.YAxis.Scale.MajorStep = (_RealTimePanel.YAxis.Scale.Max - _RealTimePanel.YAxis.Scale.Min) / 5;
            //_RealTimePanel.YAxis.Scale.MinorStep = _RealTimePanel.YAxis.Scale.MajorStep / 5;

            //_List_Data = new List<gdata>(100000);
            //开始，增加的线是没有数据点的(也就是list为空)
            //增加无数据的空线条，确定各线条显示的轴。
            LineItem CurveList0 = _RealTimePanel.AddCurve("", _RPPList0, Color.FromArgb(230, 0, 0), SymbolType.None);//Y1-X1   
            CurveList0.Line.Width = 2f;
            CurveList0.YAxisIndex = 1;
            CurveList0.Line.IsAntiAlias = true;
            //CurveList0.Symbol.Size = 2f; 
            //CurveList0.Symbol.IsAntiAlias = true;
            //CurveList0.Symbol.Fill.Type = ZedGraph.FillType.Solid;
            CurveList0.Line.IsVisible = true;


            LineItem CurveList1 = _RealTimePanel.AddCurve("", _RPPList1, Color.Purple, SymbolType.None);//Y1-X2
            CurveList1.Line.Width = 2f;
            CurveList1.YAxisIndex = 1;
            CurveList1.IsX2Axis = true;
            CurveList1.Line.IsAntiAlias = true;
            CurveList1.Line.IsVisible = true;
            //CurveList1.Symbol.Size = 2f;
            //CurveList1.Symbol.IsAntiAlias = true;
            //CurveList1.Symbol.Fill.Type = ZedGraph.FillType.Solid;


            LineItem CurveList2 = _RealTimePanel.AddCurve("", _RPPList2, Color.Green, SymbolType.None);//Y2-X1 
            CurveList2.Line.Width = 2f;
            CurveList2.Line.IsAntiAlias = true;
            CurveList2.Line.IsVisible = true;
            //CurveList2.Symbol.Size = 2f;
            //CurveList2.Symbol.IsAntiAlias = true;
            //CurveList2.Symbol.Fill.Type = ZedGraph.FillType.Solid;


            LineItem CurveList3 = _RealTimePanel.AddCurve("", _RPPList3, Color.Blue, SymbolType.None);//Y2-X2 
            CurveList3.Line.Width = 2f;
            CurveList3.IsX2Axis = true;
            CurveList3.Line.IsAntiAlias = true;
            CurveList3.Line.IsVisible = true;
            //CurveList3.Symbol.Size = 2f;
            //CurveList3.Symbol.IsAntiAlias = true;
            //CurveList3.Symbol.Fill.Type = ZedGraph.FillType.Solid;


            LineItem0 = zgControlRealTime.GraphPane.CurveList[0] as LineItem;
            LineItem1 = zgControlRealTime.GraphPane.CurveList[1] as LineItem;
            LineItem2 = zgControlRealTime.GraphPane.CurveList[2] as LineItem;
            LineItem3 = zgControlRealTime.GraphPane.CurveList[3] as LineItem;

            if (LineItem0 == null)
                return;
            if (LineItem1 == null)
                return;
            if (LineItem2 == null)
                return;
            if (LineItem3 == null)
                return;

            //第二步:在CurveItem中访问PointPairList(或者其它的IPointList)，根据自己的需要增加新数据或修改已存在的数据
            //LineItemListEdit_0 = LineItem0.Points as IPointListEdit;
            //if (LineItemListEdit_0 == null)
            //    return;
            //LineItemListEdit_1 = LineItem1.Points as IPointListEdit;
            //if (LineItemListEdit_1 == null)
            //    return;
            //LineItemListEdit_2 = LineItem2.Points as IPointListEdit;
            //if (LineItemListEdit_2 == null)
            //    return;
            //LineItemListEdit_3 = LineItem3.Points as IPointListEdit;
            //if (LineItemListEdit_3 == null)
            //    return;
            zgControlRealTime.AxisChange();
            zgControlRealTime.Invalidate();
            //zgControlRealTime.Refresh(); 
        }

        //改变数据轴重新初始化  实时曲线的名称,并确定哪个数据轴的显示与隐藏
        private void initShowRealXY(ZedGraph.ZedGraphControl zedGraph1)
        {
            foreach (LineItem li in zedGraph1.GraphPane.CurveList)
            {
                li.Label.Text = "";
            }

            //          //Y1    0-请选择-    1力,kN       2应力,MPa      3变形,mm    4位移,mm
            //          //Y2    0-请选择-    1力,kN       2应力,MPa      3变形,mm    4位移,mm
            //          //Y3    0-请选择-    1力,kN       2应力,MPa      3变形,mm    4位移,mm

            //          //X1    0-请选择-    1时间,s      2位移,mm       3应变,mm
            //          //X2    0-请选择-    1时间,s      2位移,mm       3应变,mm

            //总共 6 条曲线
            //Y1-X1 Y1-X2 Y2-X1 Y2-X2 Y3-X1 Y3-X2;

            #region Y1-X1 / Y1-X2 第一、二条曲线
            switch (_Y1)
            {
                case 1:
                    zedGraph1.GraphPane.YAxisList[1].IsVisible = true;
                    //zedGraph1.GraphPane.YAxisList[1].Scale.IsLabelsInside = false;
                    //zedGraph1.GraphPane.YAxisList[1].Title.IsTitleAtCross = true;

                    //y1-x1
                    switch (_X1)
                    {
                        case 1:
                            strCurveName[0] = "力/时间";
                            zedGraph1.GraphPane.XAxis.IsVisible = true;
                            break;
                        case 2:
                            strCurveName[0] = "力/位移";
                            zedGraph1.GraphPane.XAxis.IsVisible = true;
                            break;
                        case 3:
                            strCurveName[0] = "力/应变";
                            zedGraph1.GraphPane.XAxis.IsVisible = true;
                            break;
                        default:
                            strCurveName[0] = "";
                            zedGraph1.GraphPane.XAxis.IsVisible = false;
                            break;
                    }

                    //y1-x2
                    switch (_X2)
                    {
                        case 1:
                            strCurveName[1] = "力/时间";
                            zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            break;
                        case 2:
                            strCurveName[1] = "力/位移";
                            zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            break;
                        case 3:
                            strCurveName[1] = "力/应变";
                            zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            break;
                        default:
                            strCurveName[1] = "";
                            zedGraph1.GraphPane.X2Axis.IsVisible = false;
                            break;
                    }

                    break;

                case 2:
                    zedGraph1.GraphPane.YAxisList[1].IsVisible = true;
                    //zedGraph1.GraphPane.YAxisList[1].Scale.IsLabelsInside = true;
                    //y1-x1
                    switch (_X1)
                    {
                        case 1:
                            strCurveName[0] = "应力/时间";
                            zedGraph1.GraphPane.XAxis.IsVisible = true;
                            break;
                        case 2:
                            strCurveName[0] = "应力/位移";
                            zedGraph1.GraphPane.XAxis.IsVisible = true;
                            break;
                        case 3:
                            strCurveName[0] = "应力/应变";
                            zedGraph1.GraphPane.XAxis.IsVisible = true;
                            break;
                        default:
                            strCurveName[0] = "";
                            zedGraph1.GraphPane.XAxis.IsVisible = false;
                            break;
                    }
                    //y1-x2
                    switch (_X2)
                    {
                        case 1:
                            strCurveName[1] = "应力/时间"; zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            break;
                        case 2:
                            strCurveName[1] = "应力/位移"; zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            break;
                        case 3:
                            strCurveName[1] = "应力/应变"; zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            break;
                        default:
                            strCurveName[1] = ""; zedGraph1.GraphPane.X2Axis.IsVisible = false;
                            break;
                    }

                    break;


                case 3:
                    zedGraph1.GraphPane.YAxisList[1].IsVisible = true;
                    //zedGraph1.GraphPane.YAxisList[1].Scale.IsLabelsInside = true;
                    //y1-x1
                    switch (_X1)
                    {
                        case 1:
                            strCurveName[0] = "变形/时间"; zedGraph1.GraphPane.XAxis.IsVisible = true;
                            break;
                        case 2:
                            strCurveName[0] = "变形/位移"; zedGraph1.GraphPane.XAxis.IsVisible = true;
                            break;
                        case 3:
                            strCurveName[0] = "变形/应变"; zedGraph1.GraphPane.XAxis.IsVisible = true;
                            break;
                        default:
                            strCurveName[0] = ""; zedGraph1.GraphPane.XAxis.IsVisible = false;
                            break;
                    }
                    //y1-x2
                    switch (_X2)
                    {
                        case 1:
                            strCurveName[1] = "变形/时间"; zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            break;
                        case 2:
                            strCurveName[1] = "变形/位移"; zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            break;
                        case 3:
                            strCurveName[1] = "变形/应变"; zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            break;
                        default:
                            strCurveName[1] = ""; zedGraph1.GraphPane.X2Axis.IsVisible = false;
                            break;
                    }
                    break;
                case 4:
                    zedGraph1.GraphPane.YAxisList[1].IsVisible = true;
                    //zedGraph1.GraphPane.YAxisList[1].Scale.IsLabelsInside = true;
                    //y1-x1
                    switch (_X1)
                    {
                        case 1:
                            strCurveName[0] = "位移/时间";
                            zedGraph1.GraphPane.XAxis.IsVisible = true;
                            break;
                        case 2:
                            strCurveName[0] = "";
                            zedGraph1.GraphPane.XAxis.IsVisible = true;
                            break;
                        case 3:
                            strCurveName[0] = "位移/应变";
                            zedGraph1.GraphPane.XAxis.IsVisible = true;
                            break;
                        default:
                            strCurveName[0] = "";
                            zedGraph1.GraphPane.XAxis.IsVisible = false;
                            break;
                    }

                    //y1-x2
                    switch (_X2)
                    {
                        case 1:
                            strCurveName[1] = "位移/时间";
                            zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            break;
                        case 2:
                            strCurveName[1] = "";
                            zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            break;
                        case 3:
                            strCurveName[1] = "位移/应变";
                            zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            break;
                        default:
                            strCurveName[1] = "";
                            zedGraph1.GraphPane.X2Axis.IsVisible = false;
                            break;
                    }
                    break;
                default:
                    zedGraph1.GraphPane.YAxisList[1].IsVisible = false;
                    strCurveName[0] = "";
                    strCurveName[1] = "";
                    break;
            }
            #endregion

            #region Y2-X1 / Y2-X2 第三、四条曲线
            switch (_Y2)
            {
                case 1:
                    zedGraph1.GraphPane.YAxis.IsVisible = true;
                    //zedGraph1.GraphPane.YAxis.Scale.IsLabelsInside = true;
                    //y2-x1
                    switch (_X1)
                    {
                        case 1: zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "力/时间";
                            break;
                        case 2: zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "力/位移";
                            break;
                        case 3: zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "力/应变";
                            break;
                        default: zedGraph1.GraphPane.XAxis.IsVisible = false;
                            strCurveName[2] = "";
                            break;
                    }
                    //y2-x2
                    switch (_X2)
                    {
                        case 1: zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "力/时间";
                            break;
                        case 2: zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "力/位移";
                            break;
                        case 3: zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "力/应变";
                            break;
                        default: zedGraph1.GraphPane.X2Axis.IsVisible = false;
                            strCurveName[3] = "";
                            break;
                    }

                    break;
                case 2:
                    zedGraph1.GraphPane.YAxis.IsVisible = true;
                    //zedGraph1.GraphPane.YAxis.Scale.IsLabelsInside = true;
                    //y2-x1
                    switch (_X1)
                    {
                        case 1: zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "应力/时间";
                            break;
                        case 2: zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "应力/位移";
                            break;
                        case 3: zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "应力/应变";
                            break;
                        default: zedGraph1.GraphPane.XAxis.IsVisible = false;
                            strCurveName[2] = "";
                            break;
                    }
                    //y2-x2
                    switch (_X2)
                    {
                        case 1: zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "应力/时间";
                            break;
                        case 2: zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "应力/位移";
                            break;
                        case 3: zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "应力/应变";
                            break;
                        default: zedGraph1.GraphPane.X2Axis.IsVisible = false;
                            strCurveName[3] = "";
                            break;
                    }

                    break;
                case 3:
                    zedGraph1.GraphPane.YAxis.IsVisible = true;
                    //zedGraph1.GraphPane.YAxis.Scale.IsLabelsInside = true;
                    //y2-x1
                    switch (_X1)
                    {
                        case 1: zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "变形/时间";
                            break;
                        case 2: zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "变形/位移";
                            break;
                        case 3: zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "变形/应变";
                            break;
                        default: zedGraph1.GraphPane.XAxis.IsVisible = false;
                            strCurveName[2] = "";
                            break;
                    }
                    //y2-x2
                    switch (_X2)
                    {
                        case 1: zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "变形/时间";
                            break;
                        case 2: zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "变形/位移";
                            break;
                        case 3: zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "变形/应变";
                            break;
                        default: zedGraph1.GraphPane.X2Axis.IsVisible = false;
                            strCurveName[3] = "";
                            break;
                    }
                    break;
                case 4:
                    zedGraph1.GraphPane.YAxis.IsVisible = true;
                    //zedGraph1.GraphPane.YAxis.Scale.IsLabelsInside = true;
                    //y2-x1
                    switch (_X1)
                    {
                        case 1: zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "位移/时间";
                            break;
                        case 2: zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "";
                            break;
                        case 3: zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "位移/应变";
                            break;
                        default: zedGraph1.GraphPane.XAxis.IsVisible = false;
                            strCurveName[2] = "";
                            break;
                    }
                    //y2-x2
                    switch (_X2)
                    {
                        case 1: zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "位移/时间";
                            break;
                        case 2: zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "";
                            break;
                        case 3: zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "位移/应变";
                            break;
                        default: zedGraph1.GraphPane.X2Axis.IsVisible = false;
                            strCurveName[3] = "";
                            break;
                    }
                    break;
                default: zedGraph1.GraphPane.YAxis.IsVisible = false;
                    strCurveName[2] = "";
                    strCurveName[3] = "";
                    break;
            }
            #endregion

            //#region Y3-X1 / Y3-X2 第五、六条曲线
            //switch (_Y3)
            //{
            //    case 1:zedGraph1.GraphPane.Y2Axis.IsVisible = true;
            //        switch (_X1)
            //        {
            //            case 1:zedGraph1.GraphPane.XAxis.IsVisible = true;
            //                strCurveName[4] = "力/时间";
            //                break;
            //            case 2:zedGraph1.GraphPane.XAxis.IsVisible = true;
            //                strCurveName[4] = "力/位移";
            //                break;
            //            case 3:zedGraph1.GraphPane.XAxis.IsVisible = true;
            //                strCurveName[4] = "力/应变";
            //                break;
            //            default:zedGraph1.GraphPane.XAxis.IsVisible = false;
            //                strCurveName[4] = "";
            //                break;
            //        }
            //        switch (_X2)
            //        {
            //            case 1:zedGraph1.GraphPane.X2Axis.IsVisible = true;
            //                strCurveName[5] = "力/时间";
            //                break;
            //            case 2:zedGraph1.GraphPane.X2Axis.IsVisible = true;
            //                strCurveName[5] = "力/位移";
            //                break;
            //            case 3:zedGraph1.GraphPane.X2Axis.IsVisible = true;
            //                strCurveName[5] = "力/应变";
            //                break;
            //            default:zedGraph1.GraphPane.X2Axis.IsVisible = false;
            //                strCurveName[5] = "";
            //                break;
            //        }

            //        break;
            //    case 2:
            //        zedGraph1.GraphPane.Y2Axis.IsVisible = true;
            //        switch (_X1)
            //        {
            //            case 1:zedGraph1.GraphPane.XAxis.IsVisible = true;
            //                strCurveName[4] = "应力/时间";
            //                break;
            //            case 2:zedGraph1.GraphPane.XAxis.IsVisible = true;
            //                strCurveName[4] = "应力/位移";
            //                break;
            //            case 3:zedGraph1.GraphPane.XAxis.IsVisible = true;
            //                strCurveName[4] = "应力/应变";
            //                break;
            //            default:zedGraph1.GraphPane.XAxis.IsVisible = false;
            //                strCurveName[4] = "";
            //                break;
            //        }
            //    switch (_X2)
            //        {
            //            case 1:zedGraph1.GraphPane.X2Axis.IsVisible = true;
            //                strCurveName[5] = "应力/时间";
            //                break;
            //            case 2:zedGraph1.GraphPane.X2Axis.IsVisible = true;
            //                strCurveName[5] = "应力/位移";
            //                break;
            //            case 3:zedGraph1.GraphPane.X2Axis.IsVisible = true;
            //                strCurveName[5] = "应力/应变";
            //                break;
            //            default:zedGraph1.GraphPane.X2Axis.IsVisible = false;
            //                strCurveName[5] = "";
            //                break;
            //        }

            //        break;
            //    case 3:
            //        zedGraph1.GraphPane.Y2Axis.IsVisible = true;
            //        switch (_X1)
            //        {
            //            case 1:zedGraph1.GraphPane.XAxis.IsVisible = true;
            //                strCurveName[4] = "变形/时间";
            //                break;
            //            case 2:zedGraph1.GraphPane.XAxis.IsVisible = true;
            //                strCurveName[4] = "变形/位移";
            //                break;
            //            case 3:zedGraph1.GraphPane.XAxis.IsVisible = true;
            //                strCurveName[4] = "变形/应变";
            //                break;
            //            default:zedGraph1.GraphPane.XAxis.IsVisible = false;
            //                strCurveName[4] = "";
            //                break;
            //        }
            //        switch (_X2)
            //        {
            //            case 1:zedGraph1.GraphPane.X2Axis.IsVisible = true;
            //                strCurveName[5] = "变形/时间";
            //                break;
            //            case 2:zedGraph1.GraphPane.X2Axis.IsVisible = true;
            //                strCurveName[5] = "变形/位移";
            //                break;
            //            case 3:zedGraph1.GraphPane.X2Axis.IsVisible = true;
            //                strCurveName[5] = "变形/应变";
            //                break;
            //            default:zedGraph1.GraphPane.X2Axis.IsVisible = false;
            //                strCurveName[5] = "";
            //                break;
            //        }
            //        break;
            //    case 4:
            //        zedGraph1.GraphPane.Y2Axis.IsVisible = true;
            //        switch (_X1)
            //        {
            //            case 1:zedGraph1.GraphPane.XAxis.IsVisible = true;
            //                strCurveName[4] = "位移/时间";
            //                break;
            //            case 2:zedGraph1.GraphPane.XAxis.IsVisible = true;
            //                strCurveName[4] = "位移/位移";
            //                break;
            //            case 3:zedGraph1.GraphPane.XAxis.IsVisible = true;
            //                strCurveName[4] = "位移/应变";
            //                break;
            //            default:zedGraph1.GraphPane.XAxis.IsVisible = false;
            //                strCurveName[4] = "";
            //                break;
            //        }
            //        switch (_X2)
            //        {
            //            case 1:zedGraph1.GraphPane.X2Axis.IsVisible = true;
            //                strCurveName[5] = "位移/时间";
            //                break;
            //            case 2:zedGraph1.GraphPane.X2Axis.IsVisible = true;
            //                strCurveName[5] = "位移/位移";
            //                break;
            //            case 3:zedGraph1.GraphPane.X2Axis.IsVisible = true;
            //                strCurveName[5] = "位移/应变";
            //                break;
            //            default:zedGraph1.GraphPane.X2Axis.IsVisible = false;
            //                strCurveName[5] = "";
            //                break;
            //        }
            //        break;
            //    default:zedGraph1.GraphPane.Y2Axis.IsVisible = false;
            //        strCurveName[4] = "";
            //        strCurveName[5] = "";
            //        break;
            //}
            //#endregion


            for (int i = 0; i < 4; i++)
            {
                zedGraph1.GraphPane.CurveList[i].Label.Text = strCurveName[i].ToString();
            }
            string[] strtest = strCurveName;
            zedGraph1.AxisChange();
            //zedGraph1.Refresh();
        }

        //读取指定日期的所有试验
        private void ReadSample(TreeView tv)
        {
            tv.Nodes.Clear();

            #region 拉伸试验
            BLL.TestSample bllTs = new HR_Test.BLL.TestSample();
            //查询不重复项
            DataSet ds = bllTs.GetNotOverlapList(" testDate = #" + this.dateTimePicker.Value.Date + "#");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TreeNode tn = new TreeNode();
                tn.Text = ds.Tables[0].Rows[i]["testNo"].ToString();
                DataSet _ds = bllTs.GetList(" testNo='" + ds.Tables[0].Rows[i]["testNo"].ToString() + "' and testDate=#" + this.dateTimePicker.Value.Date + "#");
                tn.Name = "tensile";
                tn.ImageIndex = 0;
                tv.Nodes.Add(tn);
                foreach (DataRow dr in _ds.Tables[0].Rows)
                {
                    if (Convert.ToBoolean(dr["isFinish"].ToString()) == true)
                    {
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 1;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "tensile";
                        tv.Nodes[i].Nodes.Add(ftn);
                    }
                    else
                    {
                        //左侧node未完成试验的图标
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 2;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "tensile";
                        tv.Nodes[i].Nodes.Add(ftn);
                    }
                }
                _ds.Dispose();
            }
            ds.Dispose();
            #endregion

            #region 压缩试验
            BLL.Compress bllC = new HR_Test.BLL.Compress();
            DataSet dsc = bllC.GetNotOverlapList(" testDate = #" + this.dateTimePicker.Value.Date + "#");

            for (int j = 0; j < dsc.Tables[0].Rows.Count; j++)
            {
                TreeNode tn = new TreeNode();
                tn.Text = dsc.Tables[0].Rows[j]["testNo"].ToString();
                DataSet _dsc = bllC.GetList(" testNo='" + dsc.Tables[0].Rows[j]["testNo"].ToString() + "' and testDate=#" + this.dateTimePicker.Value.Date + "#");
                tn.Name = "compress";
                tn.ImageIndex = 0;
                tv.Nodes.Add(tn);
                foreach (DataRow dr in _dsc.Tables[0].Rows)
                {
                    if (Convert.ToBoolean(dr["isFinish"].ToString()) == true)
                    {
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 1;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "compress";
                        tv.Nodes[tv.Nodes.Count - 1].Nodes.Add(ftn);
                    }
                    else
                    {
                        //左侧node未完成试验的图标 
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 2;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "compress";
                        tv.Nodes[tv.Nodes.Count - 1].Nodes.Add(ftn);
                    }
                }
                _dsc.Dispose();
            }
            dsc.Dispose();
            #endregion

            #region 弯曲试验
            BLL.Bend bllb = new HR_Test.BLL.Bend();
            DataSet dsb = bllb.GetNotOverlapList("testDate=#" + this.dateTimePicker.Value.Date + "#");
            for (int j = 0; j < dsb.Tables[0].Rows.Count; j++)
            {
                TreeNode tn = new TreeNode();
                tn.Text = dsb.Tables[0].Rows[j]["testNo"].ToString();
                DataSet _dsb = bllb.GetList(" testNo='" + dsb.Tables[0].Rows[j]["testNo"].ToString() + "' and testDate=#" + this.dateTimePicker.Value.Date + "#");
                tn.Name = "bend";
                tn.ImageIndex = 0;
                tv.Nodes.Add(tn);
                foreach (DataRow dr in _dsb.Tables[0].Rows)
                {
                    if (Convert.ToBoolean(dr["isFinish"].ToString()) == true)
                    {
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 1;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "bend";
                        tv.Nodes[tv.Nodes.Count - 1].Nodes.Add(ftn);
                    }
                    else
                    {
                        //左侧node未完成试验的图标
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 2;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "bend";
                        tv.Nodes[tv.Nodes.Count - 1].Nodes.Add(ftn);
                    }
                }
                _dsb.Dispose();
            }
            dsb.Dispose();
            #endregion

            //tv.ExpandAll();
        }


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

        //获取 拉伸试验 结果的 DataSource
        private void readFinishSample(DataGridView dg, string testNo)
        {
            //dg.MultiSelect = true;  
            dg.DataSource = null;
            dg.Columns.Clear();
            dg.RowHeadersVisible = false;
            BLL.TestSample bllTs = new HR_Test.BLL.TestSample();
            if (string.IsNullOrEmpty(testNo))
            {
                DataSet ds = bllTs.GetFinishList(" isFinish=true and testDate =#" + this.dateTimePicker.Value.Date + "#");
                DataTable dt = ds.Tables[0];
                dg.DataSource = dt;
                ds.Dispose();
            }
            else
            {
                DataSet ds = bllTs.GetFinishList(" isFinish=true and testNo='" + testNo + "' and testDate=#" + this.dateTimePicker.Value.Date + "#");
                DataTable dt = ds.Tables[0];
                dg.DataSource = dt;
                ds.Dispose();
            }


            DataGridViewDisableCheckBoxColumn chkcol = new DataGridViewDisableCheckBoxColumn();
            chkcol.Name = "选择";
            chkcol.MinimumWidth = 50;
            chkcol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
            c.Name = "   ";
            dg.Columns.Insert(0, chkcol);
            dg.Columns.Insert(1, c);
            dg.Name = "tensile";

            for (int i = 0; i < this.dataGridView.Rows.Count; i++)
            {
                if (i > 19)
                {
                    this.dataGridView.Rows[i].Cells[1].Style.BackColor = Color.FromName(_Color_Array[i % 20]);
                    this.dataGridView.Rows[i].Cells[1].Value =  _Color_Array[i % 20] ;
                }
                else
                {
                    this.dataGridView.Rows[i].Cells[1].Style.BackColor = Color.FromName(_Color_Array[i]);
                    this.dataGridView.Rows[i].Cells[1].Value = _Color_Array[i];
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

            //clear all curves
            foreach (CurveItem ci in this.zedGraphControl.GraphPane.CurveList)
            {
                ci.Clear();
                ci.Label.Text = "";
            }

            //select all curves and show
            //_selTestSampleArray = new string[dg.Rows.Count];
            //_selColorArray = new string[dg.Rows.Count];
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                //_selTestSampleArray[i] = dg.Rows[i].Cells[2].Value.ToString();
                //_selColorArray[i] = dg.Rows[i].Cells[1].Value.ToString();
                dataGridView_CellClick((object)this.dataGridView, new DataGridViewCellEventArgs(0, i));
            }

            //InitCurveCount(this.zedGraphControl, _selTestSampleArray, "Tensile", _selColorArray);

            this.zedGraphControl.AxisChange();
            this.zedGraphControl.Refresh();
        }

        /****************************************************************************
        //求取平均值 X~,S,V
        private void CreateAverageView_T(ListView listView1)
        {
            // Set the view to show details.
            listView1.View = View.Details;
            // Allow the user to edit item text.
            listView1.LabelEdit = false;
            // Allow the user to rearrange columns.
            listView1.AllowColumnReorder = true;
            // Display check boxes.
            listView1.CheckBoxes = false;
            // Select the item and subitems when selection is made.
            listView1.FullRowSelect = true;
            // Display grid lines.
            listView1.GridLines = true;
            // Sort the items in the list in ascending order.
            listView1.Sorting = SortOrder.Ascending;

            // Create three items and three sets of subitems for each item.
            ListViewItem item1 = new ListViewItem("X￣", 0);
            // Place a check mark next to the item.
            item1.Checked = true;
            item1.SubItems.Add("X");
            item1.SubItems.Add(" ");
            item1.SubItems.Add("");

            ListViewItem item2 = new ListViewItem("S", 0);
            item2.SubItems.Add("2");
            item2.SubItems.Add(" ");
            item2.SubItems.Add(" ");

            ListViewItem item3 = new ListViewItem("V", 0);
            // Place a check mark next to the item.
            item3.Checked = true;
            item3.SubItems.Add("3.2");
            item3.SubItems.Add(" ");
            item3.SubItems.Add(" ");

            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            listView1.Columns.Add(" ", -1, HorizontalAlignment.Center);
            listView1.Columns.Add("Fm", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("Fp0.2", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("Rp0.2", -2, HorizontalAlignment.Left);
            //Add the items to the ListView.
            listView1.Items.AddRange(new ListViewItem[] { item1, item2, item3 });
        }
         ******************************************************************************/

        //求取平均值 X~,S,V
        private void CreateAverageView_T(DataGridView dv, string testNo)
        {
            dv.Columns.Clear();
            BLL.TestSample bllts = new HR_Test.BLL.TestSample();
            //求平均
            DataSet dst = bllts.GetFinishSumList(" testNo='" + testNo + "'and isFinish=true ");
            //获取试样数量
            DataSet ds = bllts.GetFinishList(" testNo='" + testNo + "' and isFinish=true ");
            //添加新列
            DataColumn dc = new DataColumn();
            dc.ColumnName = "试样数量-(" + ds.Tables[0].Rows.Count + ")";
            dst.Tables[0].Columns.Add(dc);
            //设置为第0列
            dst.Tables[0].Columns["试样数量-(" + ds.Tables[0].Rows.Count + ")"].SetOrdinal(0);
            dst.Tables[0].Rows[0][0] = "X";

            dv.DataSource = dst.Tables[0];
            dv.Columns[0].Frozen = true;
            dv.ReadOnly = true;
            dv.Refresh();
            ds.Dispose();
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
                DataSet ds = bllTs.GetFinishList(" isFinish=true and testDate =#" + this.dateTimePicker.Value.Date + "#");
                DataTable dt = ds.Tables[0];
                dg.DataSource = dt;
                ds.Dispose();
            }
            else
            {
                DataSet ds = bllTs.GetFinishList(" isFinish=true and testNo='" + testNo + "' and testDate=#" + this.dateTimePicker.Value.Date + "#");
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

            for (int i = 0; i < this.dataGridView.Rows.Count; i++)
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

        //获取 弯曲试验 结果的 DataSource
        private void readFinishSample_B(DataGridView dg, string testNo)
        {
            //dg.MultiSelect = true;  
            dg.DataSource = null;
            dg.Columns.Clear();
            dg.RowHeadersVisible = false;
            BLL.Bend bllTs = new HR_Test.BLL.Bend();
            if (string.IsNullOrEmpty(testNo))
            {
                DataSet ds = bllTs.GetFinishList(" isFinish=true and testDate =#" + this.dateTimePicker.Value.Date + "#");
                DataTable dt = ds.Tables[0];
                dg.DataSource = dt;
                ds.Dispose();
            }
            else
            {
                DataSet ds = bllTs.GetFinishList(" isFinish=true and testNo='" + testNo + "' and testDate=#" + this.dateTimePicker.Value.Date + "#");
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

            for (int i = 0; i < this.dataGridView.Rows.Count; i++)
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

        //底部数码显示
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

            string testSampleNo = string.Empty;
            tvTestSample.SelectedNode = e.Node;
            //MessageBox.Show(e.Node.Name);
            _selTestSampleArray = null;
            m_CtrlCommandArray = null;
            this.txtMethod.Text = "";

            if (m_CtrlCommandList.Count > 0)
            {
                m_CtrlCommandList.Clear();
            }

            string testNo = string.Empty;
            if (e.Node.Text.Contains('-'))
            {
                testNo = e.Node.Text.Substring(0, e.Node.Text.LastIndexOf('-'));
                testSampleNo = e.Node.Text;
            }
            else
            {
                testNo = e.Node.Text;
                testSampleNo = string.Empty;
            }

            switch (e.Node.Name)
            {
                case "tensile":
                    if (e.Node.ImageIndex == 1)//表示已完成的试验
                    {
                        // backgroundWorker1.RunWorkerAsync(testNo); 
                        //ShowResultPanel();
                        tsbtn_Start.Enabled = false;

                        //读取试验结果 
                        readFinishSample(this.dataGridView, testNo);
                        CreateAverageView_T(this.dataGridViewSum, testNo);
                        //InitCurveCount(this.zedGraphControl, _selTestSampleArray, "Tensile", _selColorArray);

                    }
                    else if (e.Node.ImageIndex == 2)//未完成的试验
                    {
                        tsbtn_Start.Enabled = true;
                        //ShowCurvePanel();
                        if (e.Node.Text.Contains('-'))
                            testSampleNo = e.Node.Text;

                        _testpath = Application.StartupPath + "\\Curve\\Tensile\\" + testSampleNo + ".txt";

                        //创建曲线文件.txt
                        if (!File.Exists(_testpath))
                            File.CreateText(_testpath);

                        //读取试验方法生成控制命令数组 
                        if (!string.IsNullOrEmpty(testSampleNo))
                        {
                            BLL.TestSample bllTensile = new HR_Test.BLL.TestSample();
                            Model.TestSample modelTensile = bllTensile.GetModel(testSampleNo);
                            if (ReadMethod("tensile", modelTensile.testMethodName, out m_CtrlCommandArray, out loopCount, out m_methodContent))
                            {
                                //写入命令数组 
                                if (loopCount == 0) loopCount = 1;
                                WriteControlCommand(m_CtrlCommandArray, m_CtrlCommandArray.Length, loopCount);
                                //this.txtMethod.Text = m_methodContent;
                            }
                        }

                        this.dataGridView.Name = "tensile";
                        ChangeRealTimeXYChart();
                    }
                    else
                    { }
                    break;
                case "compress":
                    readFinishSample_C(this.dataGridView, testNo);
                    this.dataGridView.Name = "compress";
                    break;
                case "bend":
                    readFinishSample_B(this.dataGridView, testNo);
                    this.dataGridView.Name = "bend";
                    break;
                default:
                    break;
            }


        }

        private void WriteControlCommand(Struc.ctrlcommand[] comArry, int commandCount, int loopCount)
        {
            /*--------------------------------------------------------------*/

            buf[0] = Convert.ToByte(commandCount);
            buf[1] = Convert.ToByte(loopCount);
            Int32 m_Value;

            for (int i = 0; i < comArry.Length; i++)
            {
                /*--------------------------------------------*/
                buf[i * 12 + 2] = m_CtrlCommandArray[i].m_CtrlType;
                buf[i * 12 + 3] = m_CtrlCommandArray[i].m_CtrlChannel;

                m_Value = m_CtrlCommandArray[i].m_CtrlSpeed;
                buf[i * 12 + 4] = (byte)m_Value;
                m_Value = m_Value >> 8;
                buf[i * 12 + 5] = (byte)m_Value;
                m_Value = m_Value >> 8;
                buf[i * 12 + 6] = (byte)m_Value;
                m_Value = m_Value >> 8;
                buf[i * 12 + 7] = (byte)m_Value;

                buf[i * 12 + 8] = m_CtrlCommandArray[i].m_StopPointType;
                buf[i * 12 + 9] = m_CtrlCommandArray[i].m_StopPointChannel;

                m_Value = m_CtrlCommandArray[i].m_StopPoint;
                buf[i * 12 + 10] = (byte)m_Value;
                m_Value = m_Value >> 8;
                buf[i * 12 + 11] = (byte)m_Value;
                m_Value = m_Value >> 8;
                buf[i * 12 + 12] = (byte)m_Value;
                m_Value = m_Value >> 8;
                buf[i * 12 + 13] = (byte)m_Value;

                /*---------------------------------------------*/
            }

            int len = 122;

            int ret = RwUsb.WriteData1582(3, buf, len, 3000);			//写数据   

        }

        //读取试验方法，生成 m_CtrlCommandArray 控制命令数组
        private bool ReadMethod(string testType, string testMethodName, out Struc.ctrlcommand[] m_CtrlCommandArray, out int loopCount, out string methodContent)
        {
            int m_ScaleValue = 0;
            m_CtrlCommandArray = null;
            loopCount = 0;
            methodContent = string.Empty;
            switch (testType)
            {
                case "tensile":
                    BLL.ControlMethod bllControlMethod = new HR_Test.BLL.ControlMethod();
                    Model.ControlMethod modelControlMethod = bllControlMethod.GetModel(testMethodName);

                    if (modelControlMethod != null)
                    {

                        //methodContent += "试验方法:" + testMethodName + "\r\n";
                        //methodContent += "--------\r\n";

                        TestMethodDis(this.txtMethod, "试验方法:" + testMethodName + "\r\n", Color.Black);
                        TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);


                        #region ///////////////////////是 否 预 载?///////////////////////////////

                        if (modelControlMethod.isProLoad)
                        {
                            Struc.ctrlcommand m_Command = new Struc.ctrlcommand();
                            //methodContent += "是否预载:是;\r\n";
                            TestMethodDis(this.txtMethod, "是否预载:是;\r\n", Color.Black);

                            //控制预载 位移或负荷 
                            switch (modelControlMethod.proLoadControlType)
                            {
                                case 0://位移
                                    //泛型命令组

                                    m_Command.m_CtrlType = 0x80;
                                    m_Command.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                    m_Command.m_CtrlSpeed = (int)(modelControlMethod.proLoadSpeed * 1000);//位移控制速度

                                    //methodContent += "预载控制:位移; 速度:" + modelControlMethod.proLoadSpeed + " mm/min\r\n";
                                    TestMethodDis(this.txtMethod, "预载控制:位移; 速度:" + modelControlMethod.proLoadSpeed + " mm/min\r\n", Color.Crimson);

                                    break;
                                case 1://负荷
                                    //泛型命令组
                                    m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                    m_ScaleValue = m_ScaleValue >> 8;
                                    m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));

                                    m_Command.m_CtrlType = 0x81;
                                    m_Command.m_CtrlChannel = m_LSensorArray[0].SensorIndex;
                                    m_Command.m_CtrlSpeed = (int)(modelControlMethod.proLoadSpeed * 1000 / (m_ScaleValue / 120000));

                                    //methodContent += "预载控制:负荷; 速度:" + modelControlMethod.proLoadSpeed + " kN/s\r\n";
                                    TestMethodDis(this.txtMethod, "预载控制:负荷; 速度:" + modelControlMethod.proLoadSpeed + " kN/s\r\n", Color.Crimson);


                                    break;
                            }

                            //预载值停止点类型 变形或负荷
                            switch (modelControlMethod.proLoadType)
                            {
                                case 0://变形 

                                    //停止的值， 变形
                                    m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                    m_ScaleValue = m_ScaleValue >> 8;
                                    m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));
                                    //泛型命令组
                                    m_Command.m_StopPointType = 0x82;
                                    m_Command.m_StopPointChannel = m_ESensorArray[0].SensorIndex;
                                    m_Command.m_StopPoint = (int)(modelControlMethod.proLoadValue * 1000.0 / (m_ScaleValue / 120000.0));

                                    //methodContent += "预载停止点:变形; 值:" + modelControlMethod.proLoadValue + " mm\r\n";
                                    TestMethodDis(this.txtMethod, "预载停止点:变形; 值:" + modelControlMethod.proLoadValue + " mm\r\n", Color.Crimson);

                                    break;
                                case 1: //负荷 
                                    //停止的值， 负荷
                                    m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                    m_ScaleValue = m_ScaleValue >> 8;
                                    m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));

                                    //泛型命令组
                                    m_Command.m_StopPointType = 0x81;
                                    m_Command.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                    m_Command.m_StopPoint = (int)(modelControlMethod.proLoadValue * 1000.0 / (m_ScaleValue / 120000.0));

                                    //methodContent += "预载停止点:负荷; 值:" + modelControlMethod.proLoadValue + " mm\r\n";
                                    TestMethodDis(this.txtMethod, "预载停止点:负荷; 值:" + modelControlMethod.proLoadValue + " mm\r\n", Color.Crimson);

                                    break;
                            }
                            m_CtrlCommandList.Add(m_Command);
                        }
                        #endregion

                        #region///////////////////////不连续屈服/////////////////////////////////


                        if (modelControlMethod.isLxqf == 1)
                        {

                            //methodContent += "试验类型:不连续屈服\r\n";
                            //methodContent += "--------\r\n";
                            TestMethodDis(this.txtMethod, "试验类型:不连续屈服\r\n--------\r\n", Color.Black);

                            #region//////////////////弹性阶段//////////////////

                            Struc.ctrlcommand m_Command1 = new Struc.ctrlcommand();
                            string[] controlType1 = modelControlMethod.controlType1.Split(',');
                            if (controlType1.Length == 4)
                            {
                                //控制方式：位移,eLe,应力,eLc
                                switch (controlType1[0])
                                {
                                    case "0"://0,5.3,1,20.68
                                        m_Command1.m_CtrlType = 0x80;
                                        m_Command1.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command1.m_CtrlSpeed = (int)(float.Parse(controlType1[1]) * 1000.0);//位移控制速度

                                        //methodContent += "弹性阶段控制:位移\r\n";
                                        //methodContent += "速度:" + float.Parse(controlType1[1]) + " mm/min\r\n";
                                        TestMethodDis(this.txtMethod, "弹性阶段控制:位移\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "速度:" + float.Parse(controlType1[1]) + " mm/min\r\n", Color.Crimson);

                                        break;
                                    case "1":
                                        m_ScaleValue = m_SensorArray[m_ESensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_ESensorArray[0].SensorIndex].scale & 0x000f));

                                        //泛型命令组
                                        m_Command1.m_StopPointType = 0x81;
                                        m_Command1.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        m_Command1.m_StopPoint = (int)(float.Parse(controlType1[3]) * 1000.0 / (m_ScaleValue / 120000.0));

                                        //methodContent += "弹性阶段控制:eLe\r\n";
                                        //methodContent += "速度:" + float.Parse(controlType1[3]) + " mm/min\r\n";
                                        TestMethodDis(this.txtMethod, "弹性阶段控制:eLe\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "速度:" + float.Parse(controlType1[3]) + " mm/min\r\n", Color.Crimson);


                                        break;
                                    case "2":
                                        break;
                                    case "3":
                                        break;
                                }
                                //停止转换点：位移 0x80,负荷 0x81,应力,应变,变形 0x82 
                                switch (controlType1[2])
                                {
                                    case "0":
                                        m_Command1.m_StopPointType = 0x80;
                                        m_Command1.m_StopPointChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command1.m_StopPoint = (int)(float.Parse(controlType1[3]) * 1000.0);

                                        //methodContent += "弹性阶段结束:位移\r\n";
                                        //methodContent += "结束值:" + float.Parse(controlType1[3]) + " mm\r\n";

                                        TestMethodDis(this.txtMethod, "弹性阶段结束:位移\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + float.Parse(controlType1[3]) + " mm\r\n", Color.Crimson);




                                        break;
                                    case "1":
                                        //停止的值， 负荷
                                        m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));

                                        //泛型命令组
                                        m_Command1.m_StopPointType = 0x81;
                                        m_Command1.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        m_Command1.m_StopPoint = (int)(float.Parse(controlType1[3]) * 1000.0 / (m_ScaleValue / 120000.0));

                                        //methodContent += "弹性阶段结束:负荷\r\n";
                                        //methodContent += "结束值:" + float.Parse(controlType1[3]) + " kN\r\n";


                                        TestMethodDis(this.txtMethod, "弹性阶段结束:负荷\r\n", Color.Crimson);
                                        TestMethodDis(this.txtMethod, "结束值:" + float.Parse(controlType1[3]) + " kN\r\n", Color.Crimson);


                                        break;
                                    case "2":
                                        break;
                                    case "3":
                                        break;
                                    case "4":
                                        break;
                                }
                                m_CtrlCommandList.Add(m_Command1);
                            }

                            #endregion

                            #region//////////////////屈服阶段//////////////////
                            //methodContent += "--------\r\n";

                            TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                            Struc.ctrlcommand m_Command2 = new Struc.ctrlcommand();
                            string[] controlType2 = modelControlMethod.controlType2.Split(',');
                            if (controlType2.Length == 4)
                            {
                                //控制方式：位移,eLe,应力
                                switch (controlType2[0])
                                {// 0,5.3,1,20.68
                                    case "0":
                                        m_Command2.m_CtrlType = 0x80;
                                        m_Command2.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command2.m_CtrlSpeed = (int)(float.Parse(controlType2[1]) * 1000.0);//位移控制速度

                                        //methodContent += "屈服阶段控制:位移\r\n";
                                        //methodContent += "速度:" + float.Parse(controlType2[1]) + " mm/min \r\n";

                                        TestMethodDis(this.txtMethod, "屈服阶段控制:位移\r\n", Color.Blue);
                                        TestMethodDis(this.txtMethod, "速度:" + float.Parse(controlType2[1]) + " mm/min \r\n", Color.Blue);


                                        break;
                                    case "1":

                                        break;
                                    case "2":
                                        break;
                                }
                                //停止转换点：位移 0x80,负荷 0x81,应力,应变,变形 0x82 
                                switch (controlType2[2])
                                {
                                    case "0":
                                        m_Command2.m_StopPointType = 0x80;
                                        m_Command2.m_StopPointChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command2.m_StopPoint = (int)(float.Parse(controlType2[3]) * 1000.0);

                                        //methodContent += "屈服阶段结束:位移\r\n";
                                        //methodContent += "结束值:" + float.Parse(controlType2[3]) + " mm\r\n";

                                        TestMethodDis(this.txtMethod, "屈服阶段结束:位移\r\n", Color.Blue);
                                        TestMethodDis(this.txtMethod, "结束值:" + float.Parse(controlType2[3]) + " mm\r\n", Color.Blue);


                                        break;
                                    case "1":
                                        //停止的值， 负荷
                                        m_ScaleValue = m_SensorArray[m_LSensorArray[0].SensorIndex].scale;
                                        m_ScaleValue = m_ScaleValue >> 8;
                                        m_ScaleValue = (int)(m_ScaleValue * Math.Pow(10.0, m_SensorArray[m_LSensorArray[0].SensorIndex].scale & 0x000f));

                                        //泛型命令组
                                        m_Command2.m_StopPointType = 0x81;
                                        m_Command2.m_StopPointChannel = m_LSensorArray[0].SensorIndex;
                                        m_Command2.m_StopPoint = (int)(float.Parse(controlType2[3]) * 1000.0 / (m_ScaleValue / 120000.0));

                                        //methodContent += "屈服阶段结束:负荷\r\n";
                                        //methodContent += "结束值:" + float.Parse(controlType2[3]) + " kN\r\n";

                                        TestMethodDis(this.txtMethod, "屈服阶段结束:负荷\r\n", Color.Blue);
                                        TestMethodDis(this.txtMethod, "结束值:" + float.Parse(controlType2[3]) + " kN\r\n", Color.Blue);

                                        break;
                                    case "2":
                                        break;
                                    case "3":
                                        break;
                                    case "4":
                                        break;
                                }
                                m_CtrlCommandList.Add(m_Command2);
                            }
                            #endregion

                            #region//////////////////加工硬化阶段//////////////////
                            // methodContent += "--------\r\n";
                            TestMethodDis(this.txtMethod, "--------\r\n", Color.Black);

                            Struc.ctrlcommand m_Command3 = new Struc.ctrlcommand();
                            string[] controlType3 = modelControlMethod.controlType3.Split(',');
                            if (controlType3.Length == 2)
                            {
                                //控制方式：位移,eLc
                                switch (controlType3[0])
                                {// 0,5.3,1,20.68
                                    case "0":
                                        m_Command3.m_CtrlType = 0x80;
                                        m_Command3.m_CtrlChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command3.m_CtrlSpeed = (int)(float.Parse(controlType3[1]) * 1000.0);//位移控制速度
                                        m_Command3.m_StopPointType = 0x80;
                                        m_Command3.m_StopPointChannel = m_DSensorArray[0].SensorIndex;
                                        m_Command3.m_StopPoint = (int)(300.0 * 1000.0);//加工硬化阶段停止点为量程   

                                        //methodContent += "加工硬化阶段控制:位移\r\n";
                                        //methodContent += "速度:" + float.Parse(controlType3[1]) + " mm/min\r\n";
                                        TestMethodDis(this.txtMethod, "加工硬化阶段控制:位移\r\n", Color.Purple);
                                        TestMethodDis(this.txtMethod, "速度:" + float.Parse(controlType3[1]) + " mm/min\r\n", Color.Purple);

                                        break;
                                    case "1":

                                        break;
                                }
                                m_CtrlCommandList.Add(m_Command3);
                            }
                            #endregion

                        }

                        #endregion

                        m_CtrlCommandArray = m_CtrlCommandList.ToArray();
                        loopCount = (int)modelControlMethod.circleNum;
                        m_methodContent = methodContent;
                    }
                    break;
                case "compress":
                    break;
                case "bend":
                    break;
            }

            if (m_CtrlCommandArray != null)
            {
                return true;
            }
            else
            {
                return false;
            }

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


        /// <summary>
        /// 生成控制命令
        /// </summary>
        /// <param name="m_CtrlType">控制类型</param>
        /// <param name="m_CtrlChannel">控制通道</param>
        /// <param name="m_CtrlSpeed">控制速度</param>
        /// <param name="m_StopPointType">停止点类型</param>
        /// <param name="m_StopPointChannel">停止点通道</param>
        /// <param name="m_StopPoint">停止点值</param>
        private void CreateCommand(byte m_CtrlType, byte m_CtrlChannel, int m_CtrlSpeed, byte m_StopPointType, byte m_StopPointChannel, int m_StopPoint)
        {

        }

        //简易日期查询
        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            ReadSample(this.tvTestSample);
        }



        delegate void delInitCurveCount(ZedGraph.ZedGraphControl zedGraph, string[] selarry, string sampleMode, string[] colorarray);
        //选择显示的曲线
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                this.dataGridView.Rows[e.RowIndex].Cells[0].Value = !Convert.ToBoolean(this.dataGridView.Rows[e.RowIndex].Cells[0].Value);
                this.dataGridView.Rows[e.RowIndex].Selected = Convert.ToBoolean(this.dataGridView.Rows[e.RowIndex].Cells[0].Value);

                string selTestSampleNo = string.Empty;

                //int selCount = 0;
                //selCount = SelCount();

                for (int i = 0; i < this.dataGridView.Rows.Count; i++)
                {
                    //this.dataGridView.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    if (Convert.ToBoolean(this.dataGridView.Rows[i].Cells[0].Value) == true)
                    {
                        selTestSampleNo += this.dataGridView.Rows[i].Cells[2].Value.ToString() + ",";
                        this.dataGridView.Rows[i].Cells[0].Style.BackColor = Color.DeepSkyBlue;
                    }
                    else
                    {
                        this.dataGridView.Rows[i].Cells[0].Style.BackColor = Color.White;
                    }
                }

                //显示曲线 

                switch (dataGridView.Name)
                {
                    case "tensile":
                        _path = "Tensile";
                        if (!string.IsNullOrEmpty(selTestSampleNo))
                        {
                            selTestSampleNo = selTestSampleNo.Remove(selTestSampleNo.LastIndexOf(','));
                            //曲线数组名
                            _selTestSampleArray = selTestSampleNo.Split(',');
                        }

                        //若选择了该行，则添加一条曲线Scale
                        if (this.dataGridView.Rows[e.RowIndex].Selected)
                        {
                            string colorc = this.dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                            string sampleNo = this.dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                            _RPPList_ReadOne = new RollingPointPairList(100000);
                            LineItem CurveList = _ResultPanel.AddCurve(sampleNo, _RPPList_ReadOne, Color.FromName(colorc), SymbolType.None);//Y1-X1 
                            readCurveName(this.dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(), _path, sampleNo);
                        }
                        else
                        {
                            CurveItem ci = this.zedGraphControl.GraphPane.CurveList[this.dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString()];
                            this.zedGraphControl.GraphPane.CurveList.Remove(ci);
                        }
                        //InitCurveCount(this.zedGraphControl, _selTestSampleArray, "Tensile", _selColorArray);
                        this.zedGraphControl.RestoreScale(this.zedGraphControl.GraphPane);
                        break; 
                    //case "compress":
                    //    _path = "Compress";
                    //    selTestSampleNo = selTestSampleNo.Remove(selTestSampleNo.LastIndexOf(','));
                    //    //曲线数组名
                    //    _selTestSampleArray = selTestSampleNo.Split(',');
                    //    colorName = colorName.Remove(colorName.LastIndexOf(','));
                    //    _selColorArray = colorName.Split(',');
                    //    _sampleCount = _selTestSampleArray.Length;

                    //    delInitCurveCount delICC_C = new delInitCurveCount(InitCurveCount);
                    //    this.zedGraphControl.BeginInvoke(delICC_C, new object[] { this.zedGraphControl, _selTestSampleArray, "Compress", _selColorArray });


                    //    //InitCurveCount(this.zedGraphControl, _selTestSampleArray, "Compress", _selColorArray);
                    //    //this.zedGraphControl.RestoreScale(this.zedGraphControl.GraphPane);
                    //    break;
                    //case "bend":
                    //    _path = "Bend";
                    //    selTestSampleNo = selTestSampleNo.Remove(selTestSampleNo.LastIndexOf(','));
                    //    //曲线数组名
                    //    _selTestSampleArray = selTestSampleNo.Split(',');
                    //    colorName = colorName.Remove(colorName.LastIndexOf(','));
                    //    _selColorArray = colorName.Split(',');
                    //    _sampleCount = _selTestSampleArray.Length;
                    //    //InitCurveCount(this.zedGraphControl, _selTestSampleArray, "Bend", _selColorArray);
                    //    //this.zedGraphControl.RestoreScale(this.zedGraphControl.GraphPane);
                    //    delInitCurveCount delICC_B = new delInitCurveCount(InitCurveCount);
                    //    this.zedGraphControl.BeginInvoke(delICC_B, new object[] { this.zedGraphControl, _selTestSampleArray, "Bend", _selColorArray });

                    //    break;
                    default:
                        _path = "";
                        break;
                }
            }
        }

        //读取曲线文件
        private void readCurveName(string curveName, string path,string curvename)
        {
            //若曲线存在
            //string curvePath = AppDomain.CurrentDomain.BaseDirectory + "Curve\\" + path + "\\" + curveName + ".lin";
            string curvePath = AppDomain.CurrentDomain.BaseDirectory + "Curve\\" + path + "\\" + curveName + ".txt";
            if (File.Exists(curvePath))
            {
                string outputFile = curvePath;
                ////先解密文件
                //string outputFile = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HRData.txt";
                //string sSecretKey;
                //string[] key = RWconfig.GetAppSettings("code").ToString().Split('-');
                //byte[] keyee = new byte[8];
                ////转换为 key byte数组
                //for (int j = 0; j < key.Length; j++)
                //{
                //    keyee[j] = Byte.Parse(key[j], System.Globalization.NumberStyles.HexNumber);
                //}
                //sSecretKey = ASCIIEncoding.ASCII.GetString(keyee);
                //GCHandle gch = GCHandle.Alloc(sSecretKey, GCHandleType.Pinned);
                //Safe.DecryptFile(curvePath, outputFile, sSecretKey);
                //Safe.ZeroMemory(gch.AddrOfPinnedObject(), sSecretKey.Length * 2);
                //gch.Free();

                //读取曲线 
                _List_DataReadOne = new List<gdata>(100000);
                //建立曲线点 
                //_RPPList_Read[index] = new RollingPointPairList(100000); 

                using (StreamReader srLine = new StreamReader(outputFile))
                {
                    string[] testSampleInfo1 = srLine.ReadLine().Split(',');
                    string[] testSampleInfo2 = srLine.ReadLine().Split(',');
                    //this.zedGraphControl.PrintDocument.DocumentName = testSampleInfo2[0].ToString() + " 试验曲线";
                    //this.zedGraphControl.GraphPane.Title.IsVisible = true;
                    String line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    while ((line = srLine.ReadLine()) != null)
                    {
                        string[] gdataArray = line.Split(',');
                        gdata _gdata = new gdata();
                        _gdata.F1 = double.Parse(gdataArray[0]);
                        _gdata.F2 = double.Parse(gdataArray[1]);
                        _gdata.F3 = double.Parse(gdataArray[2]);
                        _gdata.D1 = double.Parse(gdataArray[3]);
                        _gdata.D2 = double.Parse(gdataArray[4]);
                        _gdata.D3 = double.Parse(gdataArray[5]);
                        _gdata.BX1 = double.Parse(gdataArray[6]);
                        _gdata.BX2 = double.Parse(gdataArray[7]);
                        _gdata.BX3 = double.Parse(gdataArray[8]);
                        _gdata.YL1 = double.Parse(gdataArray[9]);
                        _gdata.YL2 = double.Parse(gdataArray[10]);
                        _gdata.YL3 = double.Parse(gdataArray[11]);
                        _gdata.YB1 = double.Parse(gdataArray[12]);
                        _gdata.YB2 = double.Parse(gdataArray[13]);
                        _gdata.YB3 = double.Parse(gdataArray[14]);
                        _gdata.Ts = double.Parse(gdataArray[15]);
                        _List_DataReadOne.Add(_gdata);
                    }
                    srLine.Close();
                    srLine.Dispose();
                    //显示曲线
                    showCurve(_List_DataReadOne, this.zedGraphControl,curvename );
                }
            }
        }

        //显示一条曲线
        private void showCurve(List<gdata> listGData, ZedGraph.ZedGraphControl zgControl,string curvename )
        {
            LineItem LineItem0 = zgControl.GraphPane.CurveList[curvename] as LineItem;
            if (LineItem0 == null)
                return;

            //第二步:在CurveItem中访问PointPairList(或者其它的IPointList)，根据自己的需要增加新数据或修改已存在的数据
            IPointListEdit LineItemListEdit_0 = LineItem0.Points as IPointListEdit;
            if (LineItemListEdit_0 == null)
                return;

            for (Int32 i = 0; i < listGData.Count; i++)
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
                                _RPPList_ReadOne.Add(time, F1value);
                                break;
                            case 2:
                                //strCurveName[0] = "力/位移";
                                LineItemListEdit_0.Add(D1value, F1value);
                                _RPPList_ReadOne.Add(D1value, F1value);
                                break;
                            case 3:
                                //strCurveName[0] = "力/应变";
                                LineItemListEdit_0.Add(YB1value, F1value);
                                _RPPList_ReadOne.Add(YB1value, F1value);
                                break;
                            case 4:
                                LineItemListEdit_0.Add(BX1value, F1value);
                                _RPPList_ReadOne.Add(BX1value, F1value);
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
                                _RPPList_ReadOne.Add(time, R1value);
                                break;
                            case 2:
                                //strCurveName[0] = "应力/位移";
                                LineItemListEdit_0.Add(D1value, R1value);
                                _RPPList_ReadOne.Add(D1value, R1value);
                                break;
                            case 3:
                                //strCurveName[0] = "应力/应变";
                                LineItemListEdit_0.Add(YB1value, R1value);
                                _RPPList_ReadOne.Add(YB1value, R1value);
                                break;
                            case 4:
                                LineItemListEdit_0.Add(BX1value, R1value);
                                _RPPList_ReadOne.Add(BX1value, R1value);
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
                                _RPPList_ReadOne.Add(time, BX1value);
                                break;
                            case 2:
                                //strCurveName[0] = "变形/位移";
                                LineItemListEdit_0.Add(D1value, BX1value);
                                _RPPList_ReadOne.Add(D1value, BX1value);
                                break;
                            case 3:
                                //strCurveName[0] = "变形/应变";
                                LineItemListEdit_0.Add(YB1value, BX1value);
                                _RPPList_ReadOne.Add(YB1value, BX1value);
                                break;
                            case 4:
                                LineItemListEdit_0.Add(BX1value, BX1value);
                                _RPPList_ReadOne.Add(BX1value, BX1value);
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
                                _RPPList_ReadOne.Add(time, D1value);
                                break;
                            case 2:
                                //strCurveName[0] = "位移/位移";
                                LineItemListEdit_0.Add(D1value, D1value);
                                _RPPList_ReadOne.Add(D1value, D1value);
                                break;
                            case 3:
                                //strCurveName[0] = "位移/应变";
                                LineItemListEdit_0.Add(YB1value, D1value);
                                _RPPList_ReadOne.Add(YB1value, D1value);
                                break;
                            case 4:
                                LineItemListEdit_0.Add(BX1value, D1value);
                                _RPPList_ReadOne.Add(BX1value, D1value);
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
            }

            //if (this._RPPList0 != null && this._RPPList0[sampleIndex].Count > 0)
            //{
            //    zgControl.GraphPane.CurveList[0 + 6 * sampleIndex].Label.Text += " Max:(" + getFm(_RPPList0[sampleIndex]).Y + "/" + getFm(_RPPList0[sampleIndex]).X + ")";
            //}
            //if (this._RPPList1 != null && this._RPPList1[sampleIndex].Count > 0)
            //{
            //    zgControl.GraphPane.CurveList[1 + 6 * sampleIndex].Label.Text += " Max:(" + getFm(_RPPList1[sampleIndex]).Y + "/" + getFm(_RPPList1[sampleIndex]).X + ")";
            //}
            //if (this._RPPList2 != null && this._RPPList2[sampleIndex-1].Count > 0)
            //{
            //    zgControl.GraphPane.CurveList[2 + 6 * sampleIndex].Label.Text += " Max:(" + getFm(_RPPList2[sampleIndex-1]).Y + "/" + getFm(_RPPList2[sampleIndex-1]).X + ")";
            //}
            //if (this._RPPList3 != null && this._RPPList3[sampleIndex-1].Count > 0)
            //{
            //    zgControl.GraphPane.CurveList[3 + 6 * sampleIndex].Label.Text += " Max:(" + getFm(_RPPList3[sampleIndex-1]).Y + "/" + getFm(_RPPList3[sampleIndex-1]).X + ")";
            //}
            //if (this._RPPList4 != null && this._RPPList4[sampleIndex-1].Count > 0)
            //{
            //    zgControl.GraphPane.CurveList[4 + 6 * sampleIndex].Label.Text += " Max:(" + getFm(_RPPList4[sampleIndex-1]).Y + "/" + getFm(_RPPList4[sampleIndex-1]).X + ")";
            //}
            //if (this._RPPList5 != null && this._RPPList5[sampleIndex-1].Count > 0)
            //{
            //    zgControl.GraphPane.CurveList[5 + 6 * sampleIndex].Label.Text += " Max:(" + getFm(_RPPList5[sampleIndex-1]).Y + "/" + getFm(_RPPList5[sampleIndex-1]).X + ")";
            //} 
        }

        ////初始化曲线控件上的曲线数量及名称
        //private void InitCurveCount(ZedGraph.ZedGraphControl zgControl, string[] lineNameArray, string path, string[] colorArray)
        //{
        //    //for (int j = 0; j < zgControl.GraphPane.CurveList.Count; j++)
        //    //{
        //    //    zgControl.GraphPane.CurveList.RemoveAt(j);
        //    //}
        //    if (lineNameArray != null)
        //    {
        //        _RPPList_Read = new RollingPointPairList[lineNameArray.Length];

        //        _RealTimePanel = zgControl.GraphPane;
        //        zgControl.GraphPane.CurveList.RemoveRange(0, zgControl.GraphPane.CurveList.Count);

        //        if (_List_Data != null)
        //            _List_Data = null;

        //        foreach (CurveItem ci in zgControl.GraphPane.CurveList)
        //        {
        //            ci.Clear();
        //        }

        //        for (int i = 0; i < lineNameArray.Length; i++)
        //        {
        //            LineItem CurveList = _RealTimePanel.AddCurve(lineNameArray[i].ToString(), _RPPList_Read[i], Color.FromName(colorArray[i].ToString()), SymbolType.None);//Y1-X1 

        //            readCurveName(lineNameArray[i].ToString(), path, i);

        //        }
        //    }

        //    //MessageBox.Show(zgControl.GraphPane.CurveList.Count.ToString());
        //    //初始化曲线名称即 试样编号的名称 
        //    zgControl.RestoreScale(zgControl.GraphPane);
        //    //zgControl.Invalidate();
        //    //zgControl.Refresh();
        //}

        void gGatherData()
        {
            //object lockthis = new object();
            while (isTest)
            {
                //lock (lockthis)
                //{ 
                Thread.Sleep(40);
                _dlgGatherData(this.m_Load, this.m_Displacement, this.m_Elongate, this.m_Time, this.m_Stress, this.m_Strain);

                //}
            }
        }

        void _dlgGatherData(float _Load,//力
                              float _Displacement,//位移
                              float _Elongate,//变形
                              float _Time,//时间
                              float _Stress,//应力
                              float _Strain//应变
            )
        {
            //采集数据
            Thread.Sleep(1);
            if (m_startCount < 3)
                m_startCount++;

            if (m_startCount > 2)
            {
                gdata gd = new gdata();
                gd.F1 = Math.Round(_Load, 3);
                gd.F2 = 0;
                gd.F3 = 0;
                gd.D1 = Math.Round(_Displacement, 3);
                gd.D2 = 0;
                gd.D3 = 0;
                gd.BX1 = Math.Round(_Elongate, 3);
                gd.BX2 = 0;
                gd.BX3 = 0;
                gd.Ts = Math.Round(_Time, 3);
                gd.YL1 = Math.Round(_Stress, 3);
                gd.YL2 = 0;
                gd.YL3 = 0;
                gd.YB1 = Math.Round(_Strain, 3);
                gd.YB2 = 0;
                gd.YB3 = 0;

                //using (StreamWriter sw = new StreamWriter(_testpath, true, Encoding.Default))
                //{ 
                //    sw.WriteLine(gd.F1 + "," + gd.F2 + "," + gd.F3 + "," + gd.D1 + "," + gd.D2 + "," + gd.D3 + "," + gd.BX1 + "," + gd.BX2 + "," + gd.BX3 + "," + gd.YL1 + "," + gd.YL2 + "," + gd.YL3 + "," + gd.YB1 + "," + gd.YB2 + "," + gd.YB3 + "," + gd.Ts);
                //    sw.Close();
                //}

                _List_Data.Add(gd);
                //测试用于计算最大值的曲线数组
                _RPPF_T.Add(_Time, _Load);
            }
        }



        private void tsbtn_Start_Click(object sender, EventArgs e)
        {
            string testSampleNo = string.Empty;
            ShowCurvePanel();
            if (this.tvTestSample.SelectedNode != null)
            {
                testSampleNo = this.tvTestSample.SelectedNode.Text;
            }
            else
            {
                MessageBox.Show("请选择试样编号");
                return;
            }
            //确保CurveList不为空
            //if (this.realTimeZedGraph.GraphPane.CurveList.Count <= 0)
            //{
            //    MessageBox.Show("无显示曲线!");
            //    return;
            //}

            if (this.chart1.Series.Count == 0)
            {
                MessageBox.Show("无显示曲线!");
                return;
            }

            this.chart1.Series[0].Points.Clear();
            this.chart1.Series[1].Points.Clear();
            this.chart1.Series[2].Points.Clear();
            this.chart1.Series[3].Points.Clear(); 

            switch (this.tvTestSample.SelectedNode.Name)
            {
                case "tensile":
                    BLL.TestSample bllTs = new HR_Test.BLL.TestSample();
                    Model.TestSample modelTs = bllTs.GetModel(testSampleNo);
                    m_S0 = (double)modelTs.S0;
                    if (modelTs.S0 <= 0)
                    {
                        MessageBox.Show("横截面积不能为0");
                        return;
                    }
                    else
                    {
                        _List_Data = new List<gdata>();
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

                        //initShowRealXY(this.realTimeZedGraph);

                        //foreach (LineItem li in this.realTimeZedGraph.GraphPane.CurveList)
                        //{
                        //    li.Clear();
                        //}

                        //foreach (System.Windows.Forms.DataVisualization.Charting.Series mss in this.chart1.Series)
                        //{
                        //    mss.Points.Clear();
                        //}

                        SendStartTest();

                        isTest = true;

                        //if (_threadGatherData == null)
                        //{
                        //    _threadGatherData = new Thread(new ThreadStart(gGatherData));
                        //    _threadGatherData.IsBackground = true;
                        //    _threadGatherData.Start();
                        //    //_threadGatherData.Join();
                        //} 
                        //_mtimer_GatherData.Start();

                        //if (_threadShowCurve == null)
                        //{
                        //    _threadShowCurve = new Thread(new ParameterizedThreadStart(dlgShowCurveFuc));
                        //    _threadShowCurve.IsBackground = true;
                        //    _threadShowCurve.Start(this.realTimeZedGraph);
                        //    //_threadShowCurve.Join();
                        //}
                        if (_threadShowCurve == null)
                        {
                            _threadShowCurve = new Thread(new ThreadStart(AddChartDataLoop));
                            _threadShowCurve.IsBackground = true;
                            _threadShowCurve.Start();
                            //_threadShowCurve.Join();
                        } 

                    }
                    break;
                case "compress":
                    BLL.Compress bllc = new HR_Test.BLL.Compress();
                    Model.Compress modelc = bllc.GetModel(testSampleNo);
                    if (modelc.S0 <= 0)
                    {
                        MessageBox.Show("横截面积不能为0");
                        return;
                    }
                    else
                    {
                        _List_Data = new List<gdata>();
                        //各控件 Enable设置
                        this.tsbtn_Start.Enabled = false;
                        this.tsbtn_Stop.Enabled = true;
                        this.tsbtn_Pause.Enabled = true;
                        this.panel1.Enabled = false;
                        this.tsbtn_Curve.Enabled = false;
                        this.tsbtn_Exit.Enabled = false;
                        this.tsbtn_Return.Enabled = false;
                        this.tsbtn_Zero.Enabled = false;
                        this.btnZeroD.Enabled = this.btnZeroF.Enabled = this.btnZeroS.Enabled = false;
                        //this.tscbX1.Enabled = this.tscbX2.Enabled = this.tscbY1.Enabled = this.tscbY2.Enabled = this.tscbY3.Enabled = false;

                        tickStart = Environment.TickCount;
                        //initShowRealXY(this.realTimeZedGraph);
                        //foreach (LineItem li in this.realTimeZedGraph.GraphPane.CurveList)
                        //{
                        //    li.Clear();
                        //}
                        isTest = true;
                        SendStartTest();
                        //_mtimer_T.Start();
                    }
                    break;
                case "bend":
                    BLL.Bend bllb = new HR_Test.BLL.Bend();
                    Model.Bend modelb = bllb.GetModel(testSampleNo);
                    //各控件 Enable设置
                    this.tsbtn_Start.Enabled = false;
                    this.tsbtn_Stop.Enabled = true;
                    this.tsbtn_Pause.Enabled = true;
                    this.panel1.Enabled = false;
                    this.tsbtn_Curve.Enabled = false;
                    this.tsbtn_Exit.Enabled = false;
                    this.tsbtn_Return.Enabled = false;
                    this.tsbtn_Zero.Enabled = false;
                    this.btnZeroD.Enabled = this.btnZeroF.Enabled = this.btnZeroS.Enabled = false;
                    //this.tscbX1.Enabled = this.tscbX2.Enabled = this.tscbY1.Enabled = this.tscbY2.Enabled = this.tscbY3.Enabled = false;
                    _List_Data = new List<gdata>();
                    tickStart = Environment.TickCount;
                    //initShowRealXY(this.realTimeZedGraph);
                    //foreach (LineItem li in this.realTimeZedGraph.GraphPane.CurveList)
                    //{
                    //    li.Clear();
                    //}
                    isTest = true;
                    SendStartTest();
                    //_mtimer_B.Start();
                    break;
                default:
                    break;
            }
        }

        private void SendStartTest()
        {
            byte[] buf = new byte[5];
            int ret;
            buf[0] = 0x04;									//命令字节
            buf[1] = 0xf1;							        //试验启动命令
            buf[2] = 0x00;
            buf[3] = 0x00;
            buf[4] = 0x00;
            ret = RwUsb.WriteData1582(1, buf, 5, 1000);				//发送写命令 
        }

        private void tsbtn_Stop_Click(object sender, EventArgs e)
        {
            //_mtimer_GatherData.Stop();
            //_threadGatherData.Abort();
            //_threadGatherData = null;

            //_threadShowCurve.Abort(); 
            _threadShowCurve = null;

            m_S0 = 0;

            SendStopTest();

            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.BringToFront();
            //停止拉伸试验
            isTest = false;
            m_startCount = 0;

            MessageBox.Show(_List_Data.Count.ToString());

            //各控件 Enable设置
            this.tsbtn_Start.Enabled = true;
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
            //this.tscbX1.Enabled = this.tscbX2.Enabled = this.tscbY1.Enabled = this.tscbY2.Enabled = this.tscbY3.Enabled = true;


            //计算最大值             
            //if (this._RPPList0 != null && this._RPPList0.Count > 0)
            //{
            //    this.realTimeZedGraph.GraphPane.CurveList[0].Label.Text += " Max:(" + getFm(_RPPList0).Y.ToString("f4") + "/" + getFm(_RPPList0).X.ToString("f4") + ")";
            //}
            //if (this._RPPList1 != null && this._RPPList1.Count > 0)
            //{
            //    this.realTimeZedGraph.GraphPane.CurveList[1].Label.Text += " Max:(" + getFm(_RPPList1).Y.ToString("f4") + "/" + getFm(_RPPList1).X.ToString("f4") + ")";
            //}
            //if (this._RPPList2 != null && this._RPPList2.Count > 0)
            //{
            //    this.realTimeZedGraph.GraphPane.CurveList[2].Label.Text += " Max:(" + getFm(_RPPList2).Y.ToString("f4") + "/" + getFm(_RPPList2).X.ToString("f4") + ")";
            //}
            //if (this._RPPList3 != null && this._RPPList3.Count > 0)
            //{
            //    this.realTimeZedGraph.GraphPane.CurveList[3].Label.Text += " Max:(" + getFm(_RPPList3).Y.ToString("f4") + "/" + getFm(_RPPList3).X.ToString("f4") + ")";
            //}
            //if (this._RPPList4 != null && this._RPPList4.Count > 0)
            //{
            //    this.realTimeZedGraph.GraphPane.CurveList[4].Label.Text += " Max:(" + getFm(_RPPList4).Y.ToString("f4") + "/" + getFm(_RPPList4).X.ToString("f4") + ")";
            //}
            //if (this._RPPList5 != null && this._RPPList5.Count > 0)
            //{
            //    this.realTimeZedGraph.GraphPane.CurveList[5].Label.Text += " Max:(" + getFm(_RPPList5).Y.ToString("f4") + "/" + getFm(_RPPList5).X.ToString("f4") + ")";
            //}

            //确认试验类型 ,写入试验结果
            string strContain = this.tvTestSample.SelectedNode.Name;
            string strTestNo = this.tvTestSample.SelectedNode.Text.Substring(0, this.tvTestSample.SelectedNode.Text.LastIndexOf('-'));

            //int intContain = 0;
            //if (strContain.Contains("extend"))
            //    intContain = 1;
            //if (strContain.Contains("compress"))
            //    intContain = 2;
            //if (strContain.Contains("bend"))
            //    intContain = 3;

            switch (strContain)
            {
                #region 停止拉伸
                case "tensile":
                    if (this.tvTestSample.SelectedNode.Text.Length > 0)
                    {
                        BLL.TestSample bllts = new HR_Test.BLL.TestSample();
                        Model.TestSample mts = bllts.GetModel(this.tvTestSample.SelectedNode.Text);
                        //保存曲线
                        string curveName = AppDomain.CurrentDomain.BaseDirectory + "Curve\\Tensile\\" + mts.testSampleNo + ".txt";
                        StreamWriter sw = new StreamWriter(curveName);
                        sw.WriteLine("testType,testSampleNo,S0,L0");
                        sw.WriteLine("tensile," + mts.testSampleNo + "," + mts.S0 + "," + mts.L0);
                        foreach (gdata gd in _List_Data)
                        {
                            sw.WriteLine(gd.F1 + "," + gd.F2 + "," + gd.F3 + "," + gd.D1 + "," + gd.D2 + "," + gd.D3 + "," + gd.BX1 + "," + gd.BX2 + "," + gd.BX3 + "," + gd.YL1 + "," + gd.YL2 + "," + gd.YL3 + "," + gd.YB1 + "," + gd.YB2 + "," + gd.YB3 + "," + gd.Ts);
                            sw.Flush();
                        }
                        sw.Close();
                        sw.Dispose();
                        //safeCurveFile(curveName, curveName.Replace(".txt", ".lin"));
                        MessageBox.Show(_List_Data.Count.ToString());
                        //给曲线添加文本框
                        //TextObj text = new TextObj(this.realTimeZedGraph.GraphPane.CurveList[0].Label.Text + " Max:(" + getFm(_RPPList0).Y + "/" + getFm(_RPPList0).X + ") " + getFeH(this._RPPF_T).Y, 0, 0, CoordType.AxisXYScale);
                        //text.Location.AlignH = AlignH.Left;
                        //text.Location.AlignV = AlignV.Top;
                        //text.FontSpec.Size = 18;
                        //text.FontSpec.FontColor = this.realTimeZedGraph.GraphPane.CurveList[0].Color;
                        //text.FontSpec.StringAlignment = StringAlignment.Near;
                        //this.realTimeZedGraph.GraphPane.GraphObjList.Add(text); 

                        //写入最大值 Fm Rm A Z


                        mts.Fm = Math.Round(getFm(this._RPPF_T).Y, 2);
                        mts.Rm = Math.Round((double)(mts.Fm / mts.S0), 4);
                        //上屈服强度(力首次下降前对应的应力)
                        mts.ReH = Math.Round((double)(getFeH(this._RPPF_T).Y / mts.S0), 4);
                        //下屈服强度(不计初始瞬时效应时屈服阶段中的最小力对应的应力) 

                        mts.testCondition = "-";// this.lblTestMethod.Text;
                        mts.isFinish = true;
                        bllts.Update(mts);
                        readFinishSample(this.dataGridView, strTestNo);
                        ReadSample(this.tvTestSample);
                        //MessageBox.Show(_List_Data.Count.ToString());  
                        this.btnZeroD.Enabled = this.btnZeroF.Enabled = this.btnZeroS.Enabled = true;
                        //this.tscbX1.Enabled = this.tscbX2.Enabled = this.tscbY1.Enabled = this.tscbY2.Enabled = this.tscbY3.Enabled = true;
                        // File.Create(AppDomain.CurrentDomain.BaseDirectory + "data.txt");
                    }
                    break;
                #endregion

                #region 停止压缩
                case "compress":
                    if (this.tvTestSample.SelectedNode.Text.Length > 0)
                    {
                        BLL.Compress bllts = new HR_Test.BLL.Compress();
                        Model.Compress mts = bllts.GetModel(this.tvTestSample.SelectedNode.Text);

                        //给曲线添加文本框
                        //TextObj text = new TextObj(this.zedGraphControl.GraphPane.CurveList[0].Label.Text + " Max:(" + getFm(_RPPList0).Y + "/" + getFm(_RPPList0).X + ")", 0, 0, CoordType.PaneFraction);
                        //text.Location.AlignH = AlignH.Left;
                        //text.Location.AlignV = AlignV.Top;
                        //text.FontSpec.Size = 5;
                        //text.FontSpec.FontColor = this.zedGraphControl.GraphPane.CurveList[0].Color;
                        //text.FontSpec.StringAlignment = StringAlignment.Far;
                        //this.zedGraphControl.GraphPane.GraphObjList.Add(text); 

                        //写入最大值 Fm Rm A Z
                        mts.Fmc = getFm(this._RPPF_T).Y;
                        mts.Rmc = Math.Round((double)(mts.Fmc / mts.S0), 4);
                        mts.testCondition = "-";// this.lblTestMethod.Text;
                        mts.isFinish = true;
                        bllts.Update(mts);
                        readFinishSample_C(this.dataGridView, strTestNo);
                        ReadSample(this.tvTestSample);
                        MessageBox.Show(_List_Data.Count.ToString());
                        this.btnZeroD.Enabled = this.btnZeroF.Enabled = this.btnZeroS.Enabled = true;
                        //this.tscbX1.Enabled = this.tscbX2.Enabled = this.tscbY1.Enabled = this.tscbY2.Enabled = this.tscbY3.Enabled = true;
                        // File.Create(AppDomain.CurrentDomain.BaseDirectory + "data.txt");
                        //保存曲线
                        string curveName = AppDomain.CurrentDomain.BaseDirectory + "Curve\\Compress\\" + mts.testSampleNo + ".txt";
                        StreamWriter sw = new StreamWriter(curveName);
                        sw.WriteLine("testType,testSampleNo,S0,L0");
                        sw.WriteLine("compress," + mts.testSampleNo + "," + mts.S0 + "," + mts.L0);
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
                #endregion

                #region 停止弯曲
                case "bend":
                    if (this.tvTestSample.SelectedNode.Text.Length > 0)
                    {
                        BLL.Bend bllts = new HR_Test.BLL.Bend();
                        Model.Bend mts = bllts.GetModel(this.tvTestSample.SelectedNode.Text);

                        //给曲线添加文本框
                        //TextObj text = new TextObj(this.zedGraphControl.GraphPane.CurveList[0].Label.Text + " Max:(" + getFm(_RPPList0).Y + "/" + getFm(_RPPList0).X + ")", 0, 0, CoordType.PaneFraction);
                        //text.Location.AlignH = AlignH.Left;
                        //text.Location.AlignV = AlignV.Top;
                        //text.FontSpec.Size = 5;
                        //text.FontSpec.FontColor = this.zedGraphControl.GraphPane.CurveList[0].Color;
                        //text.FontSpec.StringAlignment = StringAlignment.Far;
                        //this.zedGraphControl.GraphPane.GraphObjList.Add(text);  
                        //写入 变形或位移 
                        mts.testCondition = "-";// this.lblTestMethod.Text;
                        mts.isFinish = true;
                        bllts.Update(mts);
                        readFinishSample_B(this.dataGridView, strTestNo);
                        ReadSample(this.tvTestSample);
                        MessageBox.Show(_List_Data.Count.ToString());
                        this.btnZeroD.Enabled = this.btnZeroF.Enabled = this.btnZeroS.Enabled = true;
                        //this.tscbX1.Enabled = this.tscbX2.Enabled = this.tscbY1.Enabled = this.tscbY2.Enabled = this.tscbY3.Enabled = true;
                        // File.Create(AppDomain.CurrentDomain.BaseDirectory + "data.txt");
                        //保存曲线
                        string curveName = AppDomain.CurrentDomain.BaseDirectory + "Curve\\Bend\\" + mts.testSampleNo + ".txt";
                        StreamWriter sw = new StreamWriter(curveName);
                        sw.WriteLine("testType,testSampleNo,a,b,L,ll,D");
                        sw.WriteLine("bend," + mts.testSampleNo + "," + mts.a + "," + mts.b + "," + mts.L + "," + mts.ll + "," + mts.D);
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
                #endregion

                default:
                    break;
            }

            //清空曲线数组
            _RPPF_T = null;
            _RPPList0 = null;
            _RPPList1 = null;
            _RPPList2 = null;
            _RPPList3 = null;

            _RPPList0 = new RollingPointPairList(50000);
            _RPPList1 = new RollingPointPairList(50000);
            _RPPList2 = new RollingPointPairList(50000);
            _RPPList3 = new RollingPointPairList(50000);
            _RPPF_T = new RollingPointPairList(50000);
            _List_Data = null;
            //ShowResultPanel();
        }

        private void SendStopTest()
        {
            byte[] buf = new byte[5];
            int ret;
            buf[0] = 0x04;									//命令字节
            buf[1] = 0xf2;									//试验停止命令
            buf[2] = 0;
            buf[3] = 0;
            buf[4] = 0;
            ret = RwUsb.WriteData1582(1, buf, 5, 1000);				//发送写命令
        }
        //给曲线加密
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

        //取最大值的点
        private PointD getFm(RollingPointPairList rppl)
        {
            PointD fm = new PointD();
            Int32 i = 0;
            for (i = 0; i < rppl.Count; i++)
            {
                if (fm.Y <= rppl[i].Y)
                {
                    fm.Y = rppl[i].Y;
                    fm.X = rppl[i].X;
                }
            }
            return fm;
        } 

        //求取上屈服强度(首次下降前的力值)
        private PointD getFeH(RollingPointPairList rppl)
        {
            PointD FeH = new PointD();
            Int32 i = 0;
            for (i = 0; i < rppl.Count; i++)
            {
                if (FeH.Y <= rppl[i].Y)
                {
                    FeH.Y = rppl[i].Y;
                    FeH.X = rppl[i].X;
                }
                else
                {
                    break;
                }
            }
            return FeH;
        }

        private void ChangeRealTimeXYChart()
        {
            ReadCurveSet();                             //Read Show Curve's Set 
            //MessageBox.Show(_Y1.ToString() + _Y2.ToString() + _X1.ToString() + _X2.ToString());

            //realTimeZedGraph.GraphPane.CurveList.Clear();
            //realTimeZedGraph.GraphPane.YAxisList.Clear();
            //realTimeZedGraph.GraphPane.GraphObjList.Clear();
            ShowCurvePanel();

            //if (_threadInitRealCurve == null)
            //{
            //    _threadInitRealCurve = new Thread(new ThreadStart(InitRealTimeThreadPro));
            //    _threadInitRealCurve.Start();
            //    _threadInitRealCurve.IsBackground = true;
            //    _threadInitRealCurve.Join();
            //    _threadInitRealCurve = null;
            //}
            InitRealTimeThreadPro();
            //initShowRealXY(this.realTimeZedGraph);
            InitShowChartLegend(this.chart1);

            //力：Red 
            #region change scale axis's Title and set
            switch (_Y1)
            {
                case 0:
                    //_RealTimePanel.YAxisList[1].Title.Text = "Y1";
                    //chart1.ChartAreas[0].AxisY.Title = "Y1";
                    chart1.Series[0].Enabled = false;
                    chart1.Series[1].Enabled = false;
                    lblY1.Text = "";
                    palY1.Visible = false;
                    break;
                case 1:
                    lblY1.Text = "负荷N";palY1.Visible = true;
                    lblY1.Refresh();
                    //chart1.ChartAreas[0].AxisY.Title = "负荷,N";
                    break;
                case 2:
                    lblY1.Text = "应力MPa"; palY1.Visible = true;
                    lblY1.Refresh();
                    break;
                case 3:
                    lblY1.Text = "变形μm"; palY1.Visible = true;
                    lblY1.Refresh();
                    break;
                case 4:
                    lblY1.Text = "位移μm"; palY1.Visible = true;
                    lblY1.Refresh();
                    break;
            }

            switch (_Y2)
            {
                case 0:
                    //chart1.ChartAreas[0].AxisY2.Title = "Y2";
                    chart1.Series[2].Enabled = false;
                    chart1.Series[3].Enabled = false;
                    palY2.Visible = false;
                    break;
                case 1:
                    lblY2.Text = "负荷N"; palY2.Visible = true;
                    lblY2.Refresh();
                    //chart1.ChartAreas[0].AxisY2.Title = "负荷,N";
                    break;
                case 2:
                    lblY2.Text = "应力MPa"; palY2.Visible = true;
                    lblY2.Refresh();
                    break;
                case 3:
                    lblY2.Text = "变形μm"; palY2.Visible = true;
                    lblY2.Refresh();
                    break;
                case 4:
                    lblY2.Text = "位移μm"; palY2.Visible = true;
                    lblY2.Refresh();
                    break;
            }



            switch (_X1)
            {
                case 0:
                    //chart1.ChartAreas[0].AxisX.Title = "X1";
                    chart1.Series[0].Enabled = false;
                    chart1.Series[2].Enabled = false;
                    palX1.Visible = false;
                    break;
                case 1:
                    lblX1.Text = "时间s"; palX1.Visible = true;
                    lblX1.Refresh();
                    //chart1.ChartAreas[0].AxisX.Title = "时间,s";
                    break;
                case 2:
                    lblX1.Text = "位移μm"; palX1.Visible = true;
                    lblX1.Refresh();
                    break;
                case 3:
                    lblX1.Text = "应变μm"; palX1.Visible = true;
                    lblX1.Refresh();
                    break; 
            }
            switch (_X2)
            {
                case 0:
                    //chart1.ChartAreas[0].AxisX2.Title = "X2";
                    lblX2.Text = "";
                    chart1.Series[1].Enabled = false;
                    chart1.Series[3].Enabled = false;
                    break;
                case 1:
                    lblX2.Text = "时间s";
                    //chart1.ChartAreas[0].AxisX2.Title = "时间,s";
                    lblX2.Refresh();
                    break;
                case 2:
                    lblX2.Text = "位移μm";
                    lblX2.Refresh();
                    break;

                case 3:
                    lblX2.Text = "应变μm";
                    lblX2.Refresh();
                    break;
            }
            #endregion

            this.chart1.Invalidate(); 

            //this.realTimeZedGraph.AxisChange();
            //this.realTimeZedGraph.Refresh();
        }

        private void InitRealTimeThreadPro()
        {
            //初始化数据轴的线程
            delInitMsChart delIms = new delInitMsChart(initRealTimeChart);
            if (chart1.InvokeRequired)
            {
                chart1.BeginInvoke(delIms, new object[] { this.chart1 });
            }
            else
            {
                initRealTimeChart(this.chart1);
            }
        }

        private void tsbtn_Pause_Click(object sender, EventArgs e)
        {
            SendPauseTest();
        }

        private void SendPauseTest()
        {
            byte[] buf = new byte[5];
            int ret;

            buf[0] = 0x04;									//命令字节
            buf[1] = 0xf3;							        //试验保持命令
            buf[2] = 0;
            buf[3] = 0;
            buf[4] = 0;

            ret = RwUsb.WriteData1582(1, buf, 5, 1000);				//发送写命令
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
            switch (_ShowY)
            {
                case 0:
                    _ResultPanel.YAxis.Title.Text = "Y1";
                    break;
                case 1:
                    _ResultPanel.YAxis.Title.Text = "力,N";
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
                    //_ResultPanel.XAxis.Scale.MaxAuto = true;
                    //_ResultPanel.XAxis.Scale.MajorStep = _ResultPanel.XAxis.Scale.Max / 5;
                    break;
                case 1:
                    _ResultPanel.XAxis.Title.Text = "时间,s";
                    //_ResultPanel.XAxis.Scale.Max = 100;                   
                    //_ResultPanel.XAxis.Scale.MaxAuto = true;
                    //_ResultPanel.XAxis.Scale.MajorStep = _ResultPanel.XAxis.Scale.Max / 5;
                    break;
                case 2:
                    _ResultPanel.XAxis.Title.Text = "位移,μm";
                    //_ResultPanel.XAxis.Scale.Max = 100;
                    //_ResultPanel.XAxis.Scale.LabelGap = 0;
                    //_ResultPanel.XAxis.Scale.MaxAuto = true;
                    //_ResultPanel.XAxis.Scale.MajorStep = _ResultPanel.XAxis.Scale.Max / 5;
                    break;
                case 3:
                    _ResultPanel.XAxis.Title.Text = "应变,μm";
                    //_ResultPanel.XAxis.Scale.Max = 100; 
                    //_ResultPanel.XAxis.Scale.MaxAuto = true;
                    //_ResultPanel.XAxis.Scale.MajorStep = _ResultPanel.XAxis.Scale.Max / 5;
                    break;
                case 4:
                    _ResultPanel.XAxis.Title.Text = "变形,μm";
                    //_ResultPanel.XAxis.Scale.Max = 100; 
                    //_ResultPanel.XAxis.Scale.MaxAuto = true;
                    //_ResultPanel.XAxis.Scale.MajorStep = _ResultPanel.XAxis.Scale.Max / 5;
                    break;
                default:
                    _ResultPanel.XAxis.Title.Text = "X1";
                    //_ResultPanel.XAxis.Scale.MaxAuto = true;
                    //_ResultPanel.XAxis.Scale.MajorStep = _ResultPanel.XAxis.Scale.Max / 5;
                    break;
            }

            // RWconfig.SetAppSettings("ShowX", this.cmbXr.SelectedIndex.ToString());
            switch (dataGridView.Name)
            {
                case "tensile":
                    //InitCurveCount(this.zedGraphControl, _selTestSampleArray, "Tensile", _selColorArray);

                    break;
                case "compress":
                    //InitCurveCount(this.zedGraphControl, _selTestSampleArray, "Compress", _selColorArray);
                    break;
                case "bend":
                    //InitCurveCount(this.zedGraphControl, _selTestSampleArray, "Bend", _selColorArray);
                    break;
            }

            //button1_Click(this.button1, new EventArgs());
        }

        //private void cmbY_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    switch (cmbYr.SelectedIndex)
        //    {
        //        case 0:
        //            _ResultPanel.YAxis.Title.Text = "Y1";
        //            _ResultPanel.YAxis.Scale.LabelGap = 0;
        //            _ResultPanel.YAxis.Scale.MaxAuto = true;
        //            break;
        //        case 1:
        //            _ResultPanel.YAxis.Title.Text = "力,N";
        //            _ResultPanel.YAxis.Scale.LabelGap = 0;
        //            _ResultPanel.YAxis.Scale.MaxAuto = true;
        //            break;
        //        case 2:
        //            _ResultPanel.YAxis.Title.Text = "应力,MPa";
        //            _ResultPanel.YAxis.Scale.LabelGap = 0;
        //            _ResultPanel.YAxis.Scale.MaxAuto = true;
        //            break;
        //        case 3:
        //            _ResultPanel.YAxis.Title.Text = "变形,μm";
        //            _ResultPanel.YAxis.Scale.LabelGap = 0;
        //            _ResultPanel.YAxis.Scale.MaxAuto = true;
        //            break;
        //        case 4:
        //            _ResultPanel.YAxis.Title.Text = "位移,μm";
        //            _ResultPanel.YAxis.Scale.LabelGap = 0;
        //            _ResultPanel.YAxis.Scale.MaxAuto = true;
        //            break;
        //    }            
        //    this.zedGraphControl.AxisChange();
        //    this.zedGraphControl.Refresh();
        //    RWconfig.SetAppSettings("ShowY", this.cmbYr.SelectedIndex.ToString());
        //    switch (dataGridView.Name)
        //    {
        //        case "tensile":
        //            InitCurveCount(this.zedGraphControl, _selTestSampleArray, "Tensile", _selColorArray);
        //            break;
        //        case "compress":
        //            InitCurveCount(this.zedGraphControl, _selTestSampleArray, "Compress", _selColorArray);
        //            break;
        //        case "bend":
        //            InitCurveCount(this.zedGraphControl, _selTestSampleArray, "Bend", _selColorArray);
        //            break;
        //    }
        //}

        //private void cmbX_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    switch (cmbXr.SelectedIndex)
        //    {
        //        case 0:
        //            _ResultPanel.XAxis.Title.Text = "X1";
        //            _ResultPanel.XAxis.Scale.MaxAuto = true;
        //            break;
        //        case 1:
        //            _ResultPanel.XAxis.Title.Text = "时间,s";
        //            _ResultPanel.XAxis.Scale.MaxAuto = true;
        //            break;
        //        case 2:
        //            _ResultPanel.XAxis.Title.Text = "位移,μm";
        //            _ResultPanel.XAxis.Scale.LabelGap = 0;
        //            _ResultPanel.XAxis.Scale.MaxAuto = true;
        //            break;
        //        case 3:
        //            _ResultPanel.XAxis.Title.Text = "应变,μm";
        //            _ResultPanel.XAxis.Scale.MaxAuto = true;
        //            break;
        //        case 4:
        //            _ResultPanel.XAxis.Title.Text = "变形,μm";
        //            _ResultPanel.XAxis.Scale.MaxAuto = true;
        //            break;
        //        default:
        //            _ResultPanel.XAxis.Title.Text = "X1";
        //            _ResultPanel.XAxis.Scale.MaxAuto = true;
        //            break;
        //    }
        //    this.zedGraphControl.AxisChange();
        //    this.zedGraphControl.Refresh();
        //    RWconfig.SetAppSettings("ShowX", this.cmbXr.SelectedIndex.ToString());
        //    switch (dataGridView.Name)
        //    {
        //        case "tensile":
        //            InitCurveCount(this.zedGraphControl, _selTestSampleArray, "Tensile", _selColorArray);
        //            break;
        //        case "compress":
        //            InitCurveCount(this.zedGraphControl, _selTestSampleArray, "Compress", _selColorArray);
        //            break;
        //        case "bend":
        //            InitCurveCount(this.zedGraphControl, _selTestSampleArray, "Bend", _selColorArray);
        //            break;
        //    }
        //}

        private void tsbtn_Curve_Click(object sender, EventArgs e)
        {
            if (_selTestSampleArray != null)
            {
                switch (dataGridView.Name)
                {
                    case "tensile":
                        if (!string.IsNullOrEmpty(_selTestSampleArray[_selTestSampleArray.Length - 1]))
                        {
                            frmAnalysiseCurve fac = new frmAnalysiseCurve(_fmMain);
                            fac.WindowState = FormWindowState.Maximized;
                            fac.tslblSampleNo.Text = _selTestSampleArray[_selTestSampleArray.Length - 1];
                            fac._CurveName = AppDomain.CurrentDomain.BaseDirectory + "Curve\\Tensile\\" + _selTestSampleArray[_selTestSampleArray.Length - 1] + ".lin";
                            fac._TestType = "tensile";
                            fac._LineColor = _selColorArray[_selColorArray.Length - 1];
                            //读取试样结果  
                            BLL.TestSample bllts = new HR_Test.BLL.TestSample();
                            Model.TestSample modelTs = bllts.GetModel(_selTestSampleArray[_selTestSampleArray.Length - 1]);
                            DataSet dsTestSample = bllts.GetList(" testSampleNo='" + _selTestSampleArray[_selTestSampleArray.Length - 1] + "'");

                            //读取选择试验结果表
                            BLL.SelTestResult bllSt = new HR_Test.BLL.SelTestResult();
                            DataSet dsSt = bllSt.GetList(" methodName='" + modelTs.testMethodName + "'");

                            if (dsSt != null)
                            {
                                for (int i = 2; i < dsSt.Tables[0].Columns.Count; i++)
                                {
                                    if (Convert.ToBoolean(dsSt.Tables[0].Rows[0][i].ToString()) == true)
                                    {
                                        UC.Result ucResult = new HR_Test.UC.Result();
                                        ucResult._FieldName = _lblTensile_Result[i] + ":";
                                        ucResult.Name = (i - 2).ToString();
                                        ucResult.txtFiledContent.Text = dsTestSample.Tables[0].Rows[0][i + 36].ToString();
                                        fac.flowLayoutPanel1.Controls.Add(ucResult);
                                    }
                                    else
                                    {
                                        UC.Result ucResult = new HR_Test.UC.Result();
                                        ucResult._FieldName = _lblTensile_Result[i] + ":";
                                        ucResult.txtFiledContent.Enabled = false;
                                        ucResult.Name = (i - 2).ToString();
                                        ucResult.txtFiledContent.Text = dsTestSample.Tables[0].Rows[0][i + 36].ToString();
                                        fac.flowLayoutPanel1.Controls.Add(ucResult);
                                    }
                                }
                            }
                            dsSt.Dispose();
                            dsTestSample.Dispose();
                            fac.ShowDialog();
                        }
                        break;
                    case "compress":
                        if (!string.IsNullOrEmpty(_selTestSampleArray[_selTestSampleArray.Length - 1]))
                        {
                            frmAnalysiseCurve fac = new frmAnalysiseCurve(_fmMain);
                            fac.WindowState = FormWindowState.Maximized;
                            fac.tslblSampleNo.Text = _selTestSampleArray[_selTestSampleArray.Length - 1];
                            fac._CurveName = AppDomain.CurrentDomain.BaseDirectory + "Curve\\Compress\\" + _selTestSampleArray[_selTestSampleArray.Length - 1] + ".lin";
                            fac._TestType = "compress";
                            fac._LineColor = _selColorArray[_selColorArray.Length - 1];
                            //读取选择试验结果表
                            BLL.Compress bllts = new HR_Test.BLL.Compress();
                            Model.Compress modelTs = bllts.GetModel(_selTestSampleArray[_selTestSampleArray.Length - 1]);
                            DataSet dsCompress = bllts.GetList(" testSampleNo='" + _selTestSampleArray[_selTestSampleArray.Length - 1] + "'");

                            BLL.SelTestResult_C bllSt = new HR_Test.BLL.SelTestResult_C();
                            DataSet ds_C = bllSt.GetList(" methodName='" + modelTs.testMethodName + "'");

                            if (ds_C != null)
                            {
                                for (int i = 2; i < ds_C.Tables[0].Columns.Count; i++)
                                {
                                    if (Convert.ToBoolean(ds_C.Tables[0].Rows[0][i].ToString()) == true)
                                    {
                                        UC.Result ucResult = new HR_Test.UC.Result();
                                        ucResult._FieldName = _lblCompress_Result[i] + ":";
                                        ucResult.Name = (i - 2).ToString();
                                        ucResult.txtFiledContent.Text = dsCompress.Tables[0].Rows[0][i + 27].ToString();
                                        fac.flowLayoutPanel1.Controls.Add(ucResult);
                                    }
                                    else
                                    {
                                        UC.Result ucResult = new HR_Test.UC.Result();
                                        ucResult._FieldName = _lblCompress_Result[i] + ":";
                                        ucResult.Name = (i - 2).ToString();
                                        ucResult.txtFiledContent.Text = dsCompress.Tables[0].Rows[0][i + 27].ToString();
                                        ucResult.Enabled = false;
                                        fac.flowLayoutPanel1.Controls.Add(ucResult);
                                    }
                                }
                            }
                            dsCompress.Dispose();
                            ds_C.Dispose();
                            fac.ShowDialog();
                        }
                        break;
                    case "bend":
                        if (!string.IsNullOrEmpty(_selTestSampleArray[_selTestSampleArray.Length - 1]))
                        {
                            frmAnalysiseCurve fac = new frmAnalysiseCurve(_fmMain);
                            fac.WindowState = FormWindowState.Maximized;
                            fac.tslblSampleNo.Text = _selTestSampleArray[_selTestSampleArray.Length - 1];
                            fac._CurveName = AppDomain.CurrentDomain.BaseDirectory + "Curve\\Bend\\" + _selTestSampleArray[_selTestSampleArray.Length - 1] + ".lin";
                            fac._TestType = "bend";
                            fac._LineColor = _selColorArray[_selColorArray.Length - 1];
                            //读取选择试验结果表
                            BLL.Bend bllts = new HR_Test.BLL.Bend();
                            DataSet dsBend = bllts.GetList(" testSampleNo='" + _selTestSampleArray[_selTestSampleArray.Length - 1] + "'");

                            Model.Bend modelTs = bllts.GetModel(_selTestSampleArray[_selTestSampleArray.Length - 1]);
                            BLL.SelTestResult_B bllSt = new HR_Test.BLL.SelTestResult_B();
                            DataSet ds_B = bllSt.GetList(" methodName='" + modelTs.testMethodName + "'");

                            if (ds_B != null)
                            {
                                for (int i = 2; i < ds_B.Tables[0].Columns.Count; i++)
                                {
                                    if (Convert.ToBoolean(ds_B.Tables[0].Rows[0][i].ToString()) == true)
                                    {
                                        UC.Result ucResult = new HR_Test.UC.Result();
                                        ucResult._FieldName = _lblBend_Result[i] + ":";
                                        ucResult.Name = (i - 2).ToString();
                                        ucResult.txtFiledContent.Text = dsBend.Tables[0].Rows[0][i + 26].ToString();
                                        fac.flowLayoutPanel1.Controls.Add(ucResult);
                                    }
                                    else
                                    {
                                        UC.Result ucResult = new HR_Test.UC.Result();
                                        ucResult._FieldName = _lblBend_Result[i] + ":";
                                        ucResult.Name = (i - 2).ToString();
                                        ucResult.txtFiledContent.Text = dsBend.Tables[0].Rows[0][i + 26].ToString();
                                        ucResult.Enabled = false;
                                        fac.flowLayoutPanel1.Controls.Add(ucResult);
                                    }
                                }
                            }
                            System.Windows.Forms.Label lblSf = new System.Windows.Forms.Label();
                            lblSf.Text = "是否合格:";
                            System.Windows.Forms.ComboBox cmbSf = new ComboBox();
                            cmbSf.Name = "cmbSf";
                            cmbSf.Items.Add("-");
                            cmbSf.Items.Add("是");
                            cmbSf.Items.Add("否");
                            cmbSf.DropDownStyle = ComboBoxStyle.DropDownList;
                            if (Convert.ToBoolean(dsBend.Tables[0].Rows[0][33].ToString()) == true)
                            {
                                cmbSf.SelectedIndex = 1;
                            }
                            else
                            {
                                cmbSf.SelectedIndex = 2;
                            }
                            fac.flowLayoutPanel1.Controls.Add(lblSf);
                            fac.flowLayoutPanel1.Controls.Add(cmbSf);
                            dsBend.Dispose();
                            ds_B.Dispose();
                            fac.ShowDialog();
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("请选择曲线");
                return;
            }
        }

        private void ClearRealTime()
        {
            //foreach (CurveItem ci in this.realTimeZedGraph.GraphPane.CurveList)
            //{
            //    ci.Clear();
            //    ci.Label.Text = "";
            //    //this.realTimeZedGraph.GraphPane.CurveList.Remove(ci);
            //}
            //this.realTimeZedGraph.AxisChange();
            //this.realTimeZedGraph.Refresh();
        }

        private void btnZeroF_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[5];
            int ret;

            buf[0] = 0x04;									//命令字节
            buf[1] = 0xde;									//清零命令
            buf[2] = m_LSensorArray[0].SensorIndex;			//因为是简单测试程序，所以只取了零号索引。
            buf[3] = 0;
            buf[4] = 0;

            ret = RwUsb.WriteData1582(1, buf, 5, 1000);		//发送写命令

            //ClearRealTime();
        }

        private void btnZeroS_Click(object sender, EventArgs e)
        {
            // TODO: 在此添加控件通知处理程序代码
            byte[] buf = new byte[5];
            int ret;

            buf[0] = 0x04;									//命令字节
            buf[1] = 0xde;									//清零命令
            buf[2] = m_ESensorArray[0].SensorIndex;			//因为是简单测试程序，所以只取了零号索引。
            buf[3] = 0;
            buf[4] = 0;

            ret = RwUsb.WriteData1582(1, buf, 5, 1000);				//发送写命令
            ClearRealTime();
        }

        private void tsbtnMinimize_Click_1(object sender, EventArgs e)
        {
            //_fmMain.WindowState = FormWindowState.Minimized;
            this.WindowState = FormWindowState.Minimized;
        }

        private void tsbtn_Zero_Click(object sender, EventArgs e)
        {

        }

        private void btnZeroD_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[5];
            int ret;

            buf[0] = 0x04;									//命令字节
            buf[1] = 0xde;									//清零命令
            buf[2] = m_DSensorArray[0].SensorIndex;			//因为是简单测试程序，所以只取了零号索引。
            buf[3] = 0;
            buf[4] = 0;

            ret = RwUsb.WriteData1582(1, buf, 5, 1000);				//发送写命令
        }

        private void tsbtn_Return_Click(object sender, EventArgs e)
        {
            // TODO: 在此添加控件通知处理程序代码
            byte[] buf = new byte[5];// char buf[5];
            int ret;

            buf[0] = 0x04;									//命令字节
            buf[1] = 0xf6;									//清零命令
            buf[2] = 0;										//
            buf[3] = 0;
            buf[4] = 0;

            ret = RwUsb.WriteData1582(1, buf, 5, 1000);				//发送写命令
        }

        private void zedGraphControl_Load(object sender, EventArgs e)
        {

        } 
        private void tsbtnSetRealtimeCurve_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == new frmSetRealtimeCurve().ShowDialog())
            {
                ChangeRealTimeXYChart();
                //if isTest, reload the points to the curveItem
                if (isTest)
                {
                    //do { _threadShowCurve.Suspend(); }
                    //while (_threadShowCurve.ThreadState == ThreadState.Running);

                    if (_threadReadData == null)
                    {
                        _threadReadData = new Thread(new ThreadStart(ReadTestingData));
                        _threadReadData.Start();
                        _threadReadData.IsBackground = true; 
                        Thread.Sleep(10);
                        _threadReadData.Join();                      
                        _threadReadData = null;  
                    }

                    //do { _threadGatherData.Resume(); } while (_threadGatherData.ThreadState == ThreadState.Suspended);
                    //Thread.Sleep(50);

                    //do { _threadShowCurve.Resume(); } while (_threadShowCurve.ThreadState == ThreadState.Suspended);
                    //MessageBox.Show(this.realTimeZedGraph.GraphPane.CurveList.Count.ToString());
                }                 
            }
        }

        delegate void delReadTestingCurve(System.Windows.Forms.DataVisualization.Charting.Chart _chart);

        private void ReadTestingData()
        {
            delReadTestingCurve rtc = new delReadTestingCurve(ReadTestingCurve);
            if (chart1.InvokeRequired)
            {
                chart1.BeginInvoke(rtc, new object[] { this.chart1 });
            }
            else
            {
                ReadTestingCurve(this.chart1);
            }
        }

        private void ReadTestingCurve(System.Windows.Forms.DataVisualization.Charting.Chart _chart)
        {
            //System.Windows.Forms.DataVisualization.Charting.Chart _chart = (System.Windows.Forms.DataVisualization.Charting.Chart)o;
            //ZedGraph.ZedGraphControl _zedGraphControl = (ZedGraph.ZedGraphControl)o;
            //集合已修改            
            //MessageBox.Show(_Y1.ToString() + _Y2.ToString() + _X1.ToString() + _X2.ToString());
            List<gdata> _List_data_Temp = null; 
            _List_data_Temp = _List_Data;
            int listDataCount = _List_data_Temp.Count;
            _chart.Series[0].Points.Clear();
            _chart.Series[1].Points.Clear();
            _chart.Series[2].Points.Clear();
            _chart.Series[3].Points.Clear(); 
            for (int i = 0; i < listDataCount; i++) 
            {
                double time = _List_data_Temp[i].Ts; 
                double F1value = _List_data_Temp[i].F1;
                double D1value = _List_data_Temp[i].D1;
                double YB1value = _List_data_Temp[i].YB1;
                double R1value = _List_data_Temp[i].YL1;
                double BX1value = _List_data_Temp[i].BX1;

                //m_Time =(float)time;
                //m_Load = (float)F1value;
                //m_Displacement = (float)D1value;
                //m_Elongate = (float)BX1value;//变形
                //m_Strain = (float)YB1value;//应变
                //m_Stress = (float)R1value;
               
                //显示曲线数据
                #region Y1-X1 / Y1-X2 第一、二条曲线
                switch (_Y1)//this.tscbY1.SelectedIndex
                {
                    case 1:
                        switch (_X1)//this.tscbX1.SelectedIndex
                        {
                            case 1:
                                _chart.Series[0].Points.AddXY(time, F1value);
                                break;
                            case 2:
                                _chart.Series[0].Points.AddXY(D1value, F1value);
                                break;
                            case 3:
                                _chart.Series[0].Points.AddXY(YB1value, F1value);
                                break;                          
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:
                                _chart.Series[1].Points.AddXY(time, F1value);
                                break;
                            case 2:
                                _chart.Series[1].Points.AddXY(D1value, F1value);
                                break;
                            case 3:
                                _chart.Series[1].Points.AddXY(YB1value, F1value);
                                break;
                        }

                        break;
                    case 2:
                        switch (_X1)
                        {
                            case 1:                                
                                _chart.Series[0].Points.Add(time, R1value);
                                break;
                            case 2:                               
                                _chart.Series[0].Points.AddXY(D1value, R1value);
                                break;
                            case 3:                               
                                _chart.Series[0].Points.AddXY(YB1value, R1value);
                                break;                          
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:
                                _chart.Series[1].Points.AddXY(time, R1value);
                                break;
                            case 2:                              
                                _chart.Series[1].Points.AddXY(D1value, R1value);
                                break;
                            case 3:                                
                                _chart.Series[1].Points.AddXY(YB1value, R1value);
                                break;                         
                        }

                        break;
                    case 3:
                        switch (_X1)
                        {
                            case 1:                                
                                _chart.Series[0].Points.AddXY(time, BX1value);
                                break;
                            case 2:                               
                                _chart.Series[0].Points.AddXY(D1value, BX1value);
                                break;
                            case 3:                               
                                _chart.Series[0].Points.AddXY(YB1value, BX1value);
                                break;                        
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:                               
                                _chart.Series[1].Points.AddXY(time, BX1value);
                                break;
                            case 2:                              
                                _chart.Series[1].Points.AddXY(D1value, BX1value);
                                break;
                            case 3:                               
                                _chart.Series[1].Points.AddXY(YB1value, BX1value);
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
                                _chart.Series[0].Points.AddXY(time, D1value);
                                break;
                            case 2:                               
                                break;
                            case 3:                              
                                _chart.Series[0].Points.AddXY(YB1value, D1value);
                                break;                          
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:                               
                                _chart.Series[1].Points.AddXY(time, D1value);
                                break;
                            case 2:                              
                                break;
                            case 3:                               
                                _chart.Series[1].Points.AddXY(YB1value, D1value);
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
                                _chart.Series[2].Points.AddXY(time, F1value);
                                break;
                            case 2:
                                
                                _chart.Series[2].Points.AddXY(D1value, F1value);
                                break;
                            case 3:
                               
                                _chart.Series[2].Points.AddXY(YB1value, F1value);
                                break; 
                        }
                        switch (_X2)
                        {
                            case 1: 
                               
                                _chart.Series[3].Points.AddXY(time, F1value);
                                break;
                            case 2:
                                  
                                _chart.Series[3].Points.AddXY(D1value, F1value);
                                break;
                            case 3:
                               
                                _chart.Series[3].Points.AddXY(YB1value, F1value);
                                break;
                           
                        }

                        break;
                    case 2:
                        switch (_X1)
                        {
                            case 1:
                                
                                _chart.Series[2].Points.AddXY(time, R1value);
                                break;
                            case 2:
 
                                _chart.Series[2].Points.AddXY(D1value, R1value);
                                break;
                            case 3:
                                
                                _chart.Series[2].Points.AddXY(YB1value, R1value);
                                break;
                        
                        }
                        switch (_X2)
                        {
                            case 1:                               
                                _chart.Series[3].Points.AddXY(time, R1value);
                                break;
                            case 2:                                
                                _chart.Series[3].Points.AddXY(D1value, R1value);
                                break;
                            case 3:                                 
                                _chart.Series[3].Points.AddXY(YB1value, R1value);
                                break;                           
                        }

                        break;
                    case 3:
                        switch (_X1)
                        {
                            case 1:                                
                                _chart.Series[2].Points.AddXY(time, BX1value);
                                break;
                            case 2:                              
                                _chart.Series[2].Points.AddXY(D1value, BX1value);
                                break;
                            case 3:                               
                                _chart.Series[2].Points.AddXY(YB1value, BX1value);
                                break;                        
                        }
                        switch (_X2)
                        {
                            case 1:
                                _chart.Series[3].Points.AddXY(time, BX1value);
                                break;
                            case 2:                               
                                _chart.Series[3].Points.AddXY(D1value, BX1value);
                                break;
                            case 3:                               
                                _chart.Series[3].Points.AddXY(YB1value, BX1value);
                                break;                          
                        }
                        break;
                    case 4:
                        switch (_X1)
                        {
                            case 1:
                              _chart.Series[2].Points.AddXY(time, D1value);
                                break;
                            case 2:
                                //strCurveName[0] = "位移/位移";
                                //LineItemListEdit_2.Add(D1value, D1value);
                                //_RPPList2.Add(D1value, D1value);
                                break;
                            case 3:                                
                                _chart.Series[2].Points.AddXY(YB1value, D1value);
                                break;                           
                        }
                        switch (_X2)
                        {
                            case 1:
                                //strCurveName[0] = "位移/时间";
                                //LineItemListEdit_3.Add(time, D1value);
                                //.Add(time, D1value); 
                                _chart.Series[3].Points.AddXY(time, D1value);
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
                                _chart.Series[3].Points.AddXY(YB1value, D1value);
                                break;                         
                        }
                        break; 
                }
                #endregion         

            }
            
        }

        private void tsbtnShowResultCurve_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == new frmSetResultCurve().ShowDialog())
            {
                ShowResultCurve();
            }
        }

        private void zedGraphControl_Paint(object sender, PaintEventArgs e)
        {
            if (this._ResultPanel.XAxis != null)
            {
                _ResultPanel.XAxis.Scale.MajorStep = (_ResultPanel.XAxis.Scale.Max - _ResultPanel.XAxis.Scale.Min) / 5.0;
                _ResultPanel.XAxis.Scale.MinorStep = _ResultPanel.XAxis.Scale.MajorStep / 5.0;
            }

            if (_ResultPanel.YAxisList[1] != null)
            {
                _ResultPanel.YAxisList[1].Scale.MajorStep = (_ResultPanel.YAxisList[1].Scale.Max - _RealTimePanel.YAxisList[1].Scale.Min) / 5.0;
                _ResultPanel.YAxisList[1].Scale.MinorStep = _ResultPanel.YAxisList[1].Scale.MajorStep / 5.0;
            }
        }

        private void rescale_Click(object sender, EventArgs e)
        {
            if (_RealTimePanel.XAxis != null)
            {
                Scale sScale = _RealTimePanel.XAxis.Scale;
                sScale.Min = 0;
            }

            if (_RealTimePanel.X2Axis != null)
            {
                Scale sScale = _RealTimePanel.X2Axis.Scale;
                sScale.Min = 0;
            }

            if (_RealTimePanel.YAxisList[1] != null)
            {   //-请选择- 0
                //力,kN 1
                //应力,MPa 2
                //变形,μm 3
                //位移,μm 4
                Scale sScale = _RealTimePanel.YAxisList[1].Scale;
                sScale.Min = 0;
            }

            if (_RealTimePanel.YAxis != null)
            {
                Scale sScale = _RealTimePanel.YAxis.Scale;
                sScale.Min = 0;
            }

            if (_RealTimePanel.Y2Axis != null)
            {
                Scale sScale = _RealTimePanel.Y2Axis.Scale;
                sScale.Min = 0;
            }
            //realTimeZedGraph.AxisChange();
            //realTimeZedGraph.Invalidate();
            //realTimeZedGraph.Refresh();
        }

        private void lblFShow_Click(object sender, EventArgs e)
        {

        }

        private void frmTestResult_MaximumSizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
            this.Update();
            this.panel1.Refresh();
        }



        delegate void readfsample(DataGridView dgv, string sampleNo);
        delegate void CreateAView(DataGridView dgv, string sampleNo);
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            readfsample rf = new readfsample(readFinishSample);
            CreateAView av = new CreateAView(CreateAverageView_T);

            this.dataGridView.BeginInvoke(rf, new object[] { this.dataGridView, e.Argument.ToString() });
            this.dataGridViewSum.BeginInvoke(av, new object[] { this.dataGridViewSum, e.Argument.ToString() });

            //CreateAverageView_T(this.dataGridViewSum, e.Argument.ToString());    

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void frmTestResult_Resize(object sender, EventArgs e)
        {
            this.Refresh();
            this.Update();
        }

        //invalidate mschart
        private void initRealTimeChart(System.Windows.Forms.DataVisualization.Charting.Chart realTimeChart)
        {
            _RPPF_T = new RollingPointPairList(50000); 
            realTimeChart.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.Graphics;
            realTimeChart.Series.Clear();
            realTimeChart.Series.Add("Y1-X1");
            realTimeChart.Series.Add("Y1-X2");
            realTimeChart.Series.Add("Y2-X1");
            realTimeChart.Series.Add("Y2-X2");

            System.Windows.Forms.DataVisualization.Charting.SeriesCollection msChartSeries = realTimeChart.Series;

            msChartSeries[0].Color = Color.FromArgb(230, 0, 0);
            msChartSeries[1].Color = Color.Blue;
            msChartSeries[2].Color = Color.Purple;
            msChartSeries[3].Color = Color.Green;

            /*  
             * realTimeChart.Series.Add("");
            realTimeChart.Series.Add("");
            realTimeChart.Series.Add("");
            realTimeChart.Series.Add("");
            msChartSeries[4].Color = Color.FromArgb(230, 0, 0);
            msChartSeries[5].Color = Color.Blue;
            msChartSeries[6].Color = Color.Purple;
            msChartSeries[7].Color = Color.Green;
            msChartSeries[4].BorderWidth = 2;
            msChartSeries[5].BorderWidth = 2;
            msChartSeries[6].BorderWidth = 2;
            msChartSeries[7].BorderWidth = 2;  
             * 
             * msChartSeries[4].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
            msChartSeries[4].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
            msChartSeries[4].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine; 
             * 
             * msChartSeries[5].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            msChartSeries[5].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
            msChartSeries[5].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine; 
             * 
             * msChartSeries[6].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
            msChartSeries[6].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            msChartSeries[6].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
             *  msChartSeries[7].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            msChartSeries[7].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            msChartSeries[7].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;.
             *   System.Windows.Forms.DataVisualization.Charting.Legend msLegend2 = realTimeChart.Legends.Add("");
            msChartSeries[4].Legend = msLegend2.Name;
            msChartSeries[5].Legend = msLegend2.Name;
            msChartSeries[6].Legend = msLegend2.Name;
            msChartSeries[7].Legend = msLegend2.Name;
            msLegend2.Enabled = false;
             *   realTimeChart.Series[4].Points.AddXY(0, 0);
            realTimeChart.Series[5].Points.AddXY(0, 0);
            realTimeChart.Series[6].Points.AddXY(0, 0);
            realTimeChart.Series[7].Points.AddXY(0, 0);
            */

            msChartSeries[0].BorderWidth = 2;
            msChartSeries[1].BorderWidth = 2;
            msChartSeries[2].BorderWidth = 2;
            msChartSeries[3].BorderWidth = 2;
           

            //Y1-X1
            msChartSeries[0].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
            msChartSeries[0].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
            msChartSeries[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine; 

          


            //msChartSeries[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None;
            //msChartSeries[0].MarkerSize = 2;
          
            //Y1-X2
            msChartSeries[1].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            msChartSeries[1].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
            msChartSeries[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;

           

            //msChartSeries[1].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None; 
            //msChartSeries[1].MarkerSize = 2;
            //Y2-X1
            msChartSeries[2].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Primary;
            msChartSeries[2].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            msChartSeries[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;

          
            //msChartSeries[2].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None; 
            //msChartSeries[2].MarkerSize = 2;
            //Y2-X2
            msChartSeries[3].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            msChartSeries[3].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            msChartSeries[3].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;

           
            //msChartSeries[3].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None; 
            //msChartSeries[3].MarkerSize = 2; 

            System.Windows.Forms.DataVisualization.Charting.Legend msLegend = realTimeChart.Legends[0];
            msLegend.Position.Auto = false;
            msLegend.Position.X = 0f;
            msLegend.Position.Y = 0.1f;
            msLegend.Position.Width = 35.0f;
            msLegend.Position.Height = 3.5f;            
            msLegend.Enabled = true;
            msLegend.Font = new Font("宋体", 11f);
            msLegend.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            msLegend.Alignment = StringAlignment.Near;
            msLegend.TitleAlignment = StringAlignment.Near;

          
             
            //// Create cursor object
            //System.Windows.Forms.DataVisualization.Charting.Cursor cursorX = null;
            //System.Windows.Forms.DataVisualization.Charting.Cursor cursorY = null;
            //// Set cursor object
            //cursorX = realTimeChart.ChartAreas[0].CursorX;
            //cursorY = realTimeChart.ChartAreas[0].CursorY;
            //// Set cursor properties 
            //cursorX.LineWidth = 2;
            //cursorX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            //cursorX.LineColor = Color.Red;
            //cursorX.SelectionColor = Color.Yellow;
            //// Set cursor properties 
            //cursorY.LineWidth = 2;
            //cursorY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            //cursorY.LineColor = Color.Red;
            //cursorY.SelectionColor = Color.Yellow;
            //// Set cursor selection color of X axis cursor
            //realTimeChart.ChartAreas[0].CursorX.SelectionColor = Color.Yellow; 
            System.Windows.Forms.DataVisualization.Charting.ChartArea msChartArea = realTimeChart.ChartAreas[0];
            msChartArea.BackColor = Color.White;
            msChartArea.Position.Auto = false;
            msChartArea.Position.Width = 100;
            msChartArea.Position.Height = 100;            
            msChartArea.Position.X = 0;
            msChartArea.Position.Y = 0;

            msChartArea.InnerPlotPosition.Auto = false;
            msChartArea.InnerPlotPosition.Width = 96.5f;
            msChartArea.InnerPlotPosition.Height = 86.0f;
            msChartArea.InnerPlotPosition.X = 2f;
            msChartArea.InnerPlotPosition.Y = 9.5f;

            //msChartArea.AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.Position;

            msChartArea.AxisX.LabelStyle.Font = new Font("宋体", 12f, FontStyle.Bold); 
            msChartArea.AxisX.LabelStyle.ForeColor = Color.Purple;
            msChartArea.AxisX.IsMarginVisible = false;
            msChartArea.AxisX.LabelStyle.Format = "f0";
            msChartArea.AxisX.TitleFont = new Font("宋体",12f, FontStyle.Bold);
            msChartArea.AxisX.TitleForeColor = Color.Purple;
            msChartArea.AxisX.MinorTickMark.Enabled = true;
            msChartArea.AxisX.MinorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.AcrossAxis;
            msChartArea.AxisX.MajorTickMark.Size = 0.5f;
            msChartArea.AxisX.MinorTickMark.Size = 0.25f;
            msChartArea.AxisX.Minimum = 0.0d;
            msChartArea.AxisX.Maximum = 100.0d;         


            msChartArea.AxisX2.LabelStyle.Font = new Font("宋体", 12f, FontStyle.Bold);
            //msChartArea.AxisX2.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap;
            msChartArea.AxisX2.LabelStyle.ForeColor = Color.Green;
            msChartArea.AxisX2.IsMarginVisible = false;
            msChartArea.AxisX2.TitleFont = new Font("宋体", 12f, FontStyle.Bold);
            msChartArea.AxisX2.TitleForeColor = Color.Green;
            msChartArea.AxisX2.MinorTickMark.Enabled = true;
            msChartArea.AxisX2.MajorTickMark.Size = 0.5f;
            msChartArea.AxisX2.MinorTickMark.Size = 0.25f;
            msChartArea.AxisX2.MinorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.AcrossAxis;
            //msChartArea.AxisX2.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.StaggeredLabels;
            msChartArea.AxisX2.IsStartedFromZero = true;
            msChartArea.AxisX2.Minimum = 0.0d;
            msChartArea.AxisX2.Maximum = 100.0d;
            //msChartArea.AxisX2.IsLabelAutoFit = true; 
            //msChartArea.AxisX2.LabelStyle.IsEndLabelVisible = false;
            msChartArea.AxisX2.LabelStyle.Format = "f0";

            msChartArea.AxisY.LabelStyle.Font = new Font("宋体", 12f, FontStyle.Bold);
            msChartArea.AxisY.LabelStyle.ForeColor = Color.FromArgb(230,0,0);
            msChartArea.AxisY.LabelStyle.Angle = -90;
            msChartArea.AxisY.TitleFont = new Font("宋体", 12f, FontStyle.Bold);
            msChartArea.AxisY.TitleForeColor = Color.FromArgb(230, 0, 0);
            msChartArea.AxisY.IsMarginVisible = false;
            msChartArea.AxisY.MinorTickMark.Enabled = true;
            msChartArea.AxisY.MinorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.AcrossAxis;
            msChartArea.AxisY.MajorTickMark.Size = 0.5f;
            msChartArea.AxisY.MinorTickMark.Size = 0.25f;
            msChartArea.AxisY.MajorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.AcrossAxis;
            msChartArea.AxisY.Minimum = 0.0d;
            msChartArea.AxisY.Maximum = 100.0d;
            msChartArea.AxisY.LabelStyle.Format = "f0";

            msChartArea.AxisY2.LabelStyle.Font = new Font("宋体", 12f, FontStyle.Bold);
            msChartArea.AxisY2.LabelStyle.ForeColor = Color.Blue;          
            msChartArea.AxisY2.LabelStyle.Angle = -90;
            msChartArea.AxisY2.TitleFont = new Font("宋体", 12f, FontStyle.Bold);
            msChartArea.AxisY2.TitleForeColor = Color.Blue;
            msChartArea.AxisY2.IsMarginVisible = false;
            msChartArea.AxisY2.MinorTickMark.Enabled = true;
            msChartArea.AxisY2.MajorTickMark.Size = 0.5f;
            msChartArea.AxisY2.MinorTickMark.Size = 0.25f;
            msChartArea.AxisY2.MinorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.AcrossAxis;
            msChartArea.AxisY2.MajorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.AcrossAxis;
            msChartArea.AxisY2.Minimum = 0.0d;
            msChartArea.AxisY2.Maximum = 100.0d;
            msChartArea.AxisY2.LabelStyle.Format = "f0";

            //msChartArea.BackColor = Color.Black;
            //msChartArea.ShadowOffset = 0;
            //msChartArea.Position.X = 0;
            //msChartArea.Position.Y = 0; 

            //msChartArea.AxisX.MajorGrid.Enabled = false;
            //msChartArea.AxisY.MajorGrid.Enabled = false;
            //msChartArea.AxisX2.MajorGrid.Enabled = false;
            //msChartArea.AxisY2.MajorGrid.Enabled = false; 

            msChartArea.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            msChartArea.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            msChartArea.AxisY2.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            msChartArea.AxisX2.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;             
        
            //msChartArea.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.FixedCount;
            //msChartArea.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.FixedCount;
            //msChartArea.AxisX2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.FixedCount;
            //msChartArea.AxisY2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.FixedCount;  

            //msChartArea.AxisX.Interval = msChartArea.AxisX.Maximum / 5;
            //msChartArea.AxisX2.Interval = msChartArea.AxisX2.Maximum / 5;
            //msChartArea.AxisY.Interval = msChartArea.AxisY.Maximum / 5;
            //msChartArea.AxisY2.Interval = msChartArea.AxisY2.Maximum / 5;
          
            //realTimeChart.Invalidate();

            realTimeChart.Series[0].Points.AddXY(0, 0);
            realTimeChart.Series[1].Points.AddXY(0, 0);
            realTimeChart.Series[2].Points.AddXY(0, 0);
            realTimeChart.Series[3].Points.AddXY(0, 0);

        }

       
        void chart1_Invalidated(object sender, InvalidateEventArgs e)
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea cArea = chart1.ChartAreas[0];
            if (isTest)
            {
                switch (_X1)// -请选择-   时间,s  位移,μm  应变,μm
                {
                    case 1:
                        if (m_Time > cArea.AxisX.Maximum)
                        {
                            cArea.AxisX.Maximum = 2 * cArea.AxisX.Maximum;

                            cArea.AxisX.Interval = cArea.AxisX.Maximum / 5.0d;
                        }
                        break;
                    case 2:
                        if (Math.Abs(m_Displacement) > Math.Abs(cArea.AxisX.Maximum))
                        {
                            cArea.AxisX.Maximum = 2 * cArea.AxisX.Maximum;
                            cArea.AxisX.Interval = cArea.AxisX.Maximum / 5.0d;

                            if (Math.Abs(cArea.AxisX.Maximum) > 1000.0)
                            {
                                cArea.AxisX.Maximum = ((int)cArea.AxisX.Maximum / 1000) * 1000;
                                cArea.AxisX.Interval = cArea.AxisX.Maximum / 5.0d;

                                cArea.AxisX.LabelStyle.Format = string.Format("0,.0##");
                                lblX1.Text = lblX1.Text.Replace("μm", "mm");
                                lblX1.Refresh();
                            }
                            //else
                            //{
                            //    cArea.AxisX.LabelStyle.Format = "f1";
                            //    lblX1.Text = lblX1.Text.Replace("mm", "μm");
                            //    lblX1.Refresh();
                            //}
                        }

                        break;
                    case 3:
                        if (Math.Abs(m_Strain) > Math.Abs(cArea.AxisX.Maximum))
                        {
                            cArea.AxisX.Maximum = 2 * cArea.AxisX.Maximum;
                            cArea.AxisX.Interval = cArea.AxisX.Maximum / 5.0d;
                            if (Math.Abs(cArea.AxisX.Maximum) > 1000.0)
                            {
                                cArea.AxisX.Maximum = ((int)cArea.AxisX.Maximum / 1000) * 1000;
                                cArea.AxisX.Interval = cArea.AxisX.Maximum / 5.0d;
                                cArea.AxisX.LabelStyle.Format = string.Format("0,.0##");
                                lblX1.Text = lblX1.Text.Replace("μm", "mm");
                                lblX1.Refresh();
                            }
                            //else
                            //{
                            //    cArea.AxisX.LabelStyle.Format = "f1";
                            //    lblX1.Text = lblX1.Text.Replace("mm", "μm");
                            //    lblX1.Refresh();
                            //}
                        }
                        break;
                }

                switch (_X2)// -请选择-   时间,s  位移,μm  应变,μm
                {
                    case 1:
                        if (m_Time > cArea.AxisX2.Maximum)
                        {
                            cArea.AxisX2.Maximum = 2 * cArea.AxisX2.Maximum;
                            cArea.AxisX2.Interval = cArea.AxisX2.Maximum / 5.0;
                        }
                        break;
                    case 2:
                        if (Math.Abs(m_Displacement) > Math.Abs(cArea.AxisX2.Maximum))
                        {
                            cArea.AxisX2.Maximum = 2 * cArea.AxisX2.Maximum;
                            cArea.AxisX2.Interval = cArea.AxisX2.Maximum / 5.0;
                            if (Math.Abs(cArea.AxisX2.Maximum) > 1000.0)
                            {
                                cArea.AxisX2.Maximum = ((int)cArea.AxisX2.Maximum / 1000) * 1000;
                                cArea.AxisX2.Interval = cArea.AxisX2.Maximum / 5.0d;

                                cArea.AxisX2.LabelStyle.Format = string.Format("0,.0##");
                                lblX2.Text = lblX2.Text.Replace("μm", "mm");
                                lblX2.Refresh();
                            }
                            //else
                            //{
                            //    cArea.AxisX2.LabelStyle.Format = "f1";
                            //    lblX2.Text = lblX2.Text.Replace("mm", "μm");
                            //    lblX2.Refresh();
                            //}
                        }
                        break;
                    case 3:
                        if (Math.Abs(m_Strain) > Math.Abs(cArea.AxisX2.Maximum))
                        {
                            cArea.AxisX2.Maximum = 2 * cArea.AxisX2.Maximum;
                            cArea.AxisX2.Interval = cArea.AxisX2.Maximum / 5.0;
                            if (Math.Abs(cArea.AxisX2.Maximum) > 1000.0)
                            {
                                cArea.AxisX2.Maximum = ((int)cArea.AxisX2.Maximum / 1000) * 1000;
                                cArea.AxisX2.Interval = cArea.AxisX2.Maximum / 5.0d;
                                cArea.AxisX2.LabelStyle.Format = string.Format("0,.0##");
                                lblX2.Text = lblX2.Text.Replace("μm", "mm");
                                lblX2.Refresh();
                            }
                            //else
                            //{
                            //    cArea.AxisX2.LabelStyle.Format = "f1";
                            //    lblX2.Text = lblX2.Text.Replace("mm", "μm");
                            //    lblX2.Refresh();
                            //}
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
                        if (Math.Abs(m_Load) > Math.Abs(cArea.AxisY.Maximum))
                        {
                            cArea.AxisY.Maximum = 2 * cArea.AxisY.Maximum;
                            cArea.AxisY.Interval = cArea.AxisY.Maximum / 5.0d;
                            if (Math.Abs(cArea.AxisY.Maximum) > 1000.0)
                            {
                                cArea.AxisY.Maximum = ((int)cArea.AxisY.Maximum / 1000) * 1000;
                                cArea.AxisY.Interval = cArea.AxisY.Maximum / 5.0d;

                                cArea.AxisY.LabelStyle.Format = string.Format("0,.0##");
                                lblY1.Text = lblY1.Text.Replace("荷N", "荷kN");
                                lblY1.Refresh();
                            }
                            //else
                            //{
                            //    cArea.AxisY.LabelStyle.Format = "f1";
                            //    lblY1.Text = lblY1.Text.Replace("荷kN", "荷N");
                            //    lblY1.Refresh();
                            //}
                        }
                        break;
                    case 2:
                        if (Math.Abs(m_Stress) > Math.Abs(cArea.AxisY.Maximum))
                        {
                            cArea.AxisY.Maximum = 2 * cArea.AxisY.Maximum;
                            cArea.AxisY.Interval = cArea.AxisY.Maximum / 5.0d;
                        }
                        break;
                    case 3:
                        if (Math.Abs(m_Elongate) > Math.Abs(cArea.AxisY.Maximum))
                        {

                            cArea.AxisY.Maximum = 2 * cArea.AxisY.Maximum;
                            cArea.AxisY.Interval = cArea.AxisY.Maximum / 5.0d;
                            if (Math.Abs(cArea.AxisY.Maximum) > 1000.0)
                            {
                                cArea.AxisY.Maximum = ((int)cArea.AxisY.Maximum / 1000) * 1000;
                                cArea.AxisY.Interval = cArea.AxisY.Maximum / 5.0d;
                                cArea.AxisY.LabelStyle.Format = string.Format("0,.0##");
                                lblY1.Text = lblY1.Text.Replace("μm", "mm");
                                lblY1.Refresh();
                            }
                            //else
                            //{
                            //    cArea.AxisY.LabelStyle.Format = string.Format("f1");
                            //    lblY1.Text = lblY1.Text.Replace("mm", "μm");
                            //    lblY1.Refresh();
                            //}
                        }
                        break;
                    case 4:
                        if (Math.Abs(m_Displacement) > Math.Abs(cArea.AxisY.Maximum))
                        {
                            cArea.AxisY.Maximum = 2 * cArea.AxisY.Maximum;
                            cArea.AxisY.Interval = cArea.AxisY.Maximum / 5.0d;
                            if (Math.Abs(cArea.AxisY.Maximum) > 1000.0)
                            {
                                cArea.AxisY.Maximum = ((int)cArea.AxisY.Maximum / 1000) * 1000;
                                cArea.AxisY.Interval = cArea.AxisY.Maximum / 5.0d;
                                cArea.AxisY.LabelStyle.Format = string.Format("0,.0##");
                                lblY1.Text = lblY1.Text.Replace("μm", "mm");
                                lblY1.Refresh();
                            }
                            //else
                            //{
                            //    cArea.AxisY.LabelStyle.Format = string.Format("f1");
                            //    lblY1.Text = lblY1.Text.Replace("mm", "μm");
                            //    lblY1.Refresh();
                            //}
                        }
                        break;
                }

                switch (_Y2)
                {
                    case 1:
                        if (Math.Abs(m_Load) > Math.Abs(cArea.AxisY2.Maximum))
                        {
                            cArea.AxisY2.Maximum = 2 * cArea.AxisY2.Maximum;
                            cArea.AxisY2.Interval = cArea.AxisY2.Maximum / 5.0d;
                            if (Math.Abs(cArea.AxisY2.Maximum) > 1000.0)
                            {
                                cArea.AxisY2.Maximum = ((int)cArea.AxisY2.Maximum / 1000) * 1000;
                                cArea.AxisY2.Interval = cArea.AxisY2.Maximum / 5.0d;
                                cArea.AxisY2.LabelStyle.Format = string.Format("0,.0##");
                                lblY2.Text = lblY2.Text.Replace("荷N", "荷kN");
                                lblY2.Refresh();
                            }
                            //else
                            //{
                            //    cArea.AxisY2.LabelStyle.Format = string.Format("f1");
                            //    lblY2.Text = lblY2.Text.Replace("荷kN", "荷N");
                            //    lblY2.Refresh();
                            //}
                        }
                        break;
                    case 2:
                        if (Math.Abs(m_Stress) > Math.Abs(cArea.AxisY2.Maximum))
                        {
                            cArea.AxisY2.Maximum = 2 * cArea.AxisY2.Maximum;
                            cArea.AxisY2.Interval = cArea.AxisY2.Maximum / 5.0d;
                        }
                        break;
                    case 3:
                        if (Math.Abs(m_Elongate) > Math.Abs(cArea.AxisY2.Maximum))
                        {
                            cArea.AxisY2.Maximum = 2 * cArea.AxisY2.Maximum;
                            cArea.AxisY2.Interval = cArea.AxisY2.Maximum / 5.0d;
                            if (Math.Abs(cArea.AxisY2.Maximum) > 1000.0)
                            {
                                cArea.AxisY2.Maximum = ((int)cArea.AxisY2.Maximum / 1000) * 1000;
                                cArea.AxisY2.Interval = cArea.AxisY2.Maximum / 5.0d;
                                cArea.AxisY2.LabelStyle.Format = string.Format("0,.0##");
                                lblY2.Text = lblY2.Text.Replace("μm", "mm");
                                lblY2.Refresh();
                            }
                            //else
                            //{
                            //    cArea.AxisY2.LabelStyle.Format = string.Format("f1");
                            //    lblY2.Text = lblY2.Text.Replace("mm", "μm");
                            //    lblY2.Refresh();
                            //}
                        }
                        break;
                    case 4:
                        if (Math.Abs(m_Displacement) > Math.Abs(cArea.AxisY2.Maximum))
                        {
                            cArea.AxisY2.Maximum = 2 * cArea.AxisY2.Maximum;
                            cArea.AxisY2.Interval = cArea.AxisY2.Maximum / 5.0d;
                            if (Math.Abs(cArea.AxisY2.Maximum) > 1000.0)
                            {
                                cArea.AxisY2.Maximum = ((int)cArea.AxisY2.Maximum / 1000) * 1000;
                                cArea.AxisY2.Interval = cArea.AxisY2.Maximum / 5.0d;
                                cArea.AxisY2.LabelStyle.Format = string.Format("0,.0##");
                                lblY2.Text = lblY2.Text.Replace("μm", "mm");
                                lblY2.Refresh();
                            }
                            //else
                            //{
                            //    cArea.AxisY2.LabelStyle.Format = string.Format("f1");
                            //    lblY2.Text = lblY2.Text.Replace("mm", "μm");
                            //    lblY2.Refresh();
                            //}
                        }
                        break;
                }
            }
        }

        private delegate void dlgShowCurveFucChart(System.Windows.Forms.DataVisualization.Charting.Chart _chart);

        private void AddChartData(System.Windows.Forms.DataVisualization.Charting.Chart _chart)
        {

            double time = m_Time;
            //力
            double F1value = m_Load;
            //应力
            double R1value = m_Stress;
            //位移
            double D1value = m_Displacement;
            //变形
            double BX1value = m_Elongate;
            //应变
            double YB1value = m_Strain;
            Thread.Sleep(10);
            m_startCount++;
            if (m_startCount > 5)
            {
                //显示曲线数据
                #region Y1-X1 / Y1-X2 第一、二条曲线
                switch (_Y1)//this.tscbY1.SelectedIndex
                {
                    case 1:
                        switch (_X1)//this.tscbX1.SelectedIndex
                        {
                            case 1: 
                                //delAddDataToChart deladt = new delAddDataToChart(AddDataToChart);
                                //_chart.BeginInvoke(new delAddDataToChart(AddDataToChart), new object[]{0,time,F1value});
                                _chart.Series[0].Points.AddXY(time, F1value);
                                break;
                            case 2: 
                                _chart.Series[0].Points.AddXY(D1value, F1value);
                                break;
                            case 3: 
                                _chart.Series[0].Points.AddXY(YB1value, F1value);
                                break;
                            default:
                                //strCurveName[0] = "";                           
                                break;
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:
                                //strCurveName[0] = "力/时间";
                                //LineItemListEdit_1.Add(time, F1value);
                                //_RPPList1.Add(time, F1value);
                                _chart.Series[1].Points.AddXY(time, F1value); 
                                break;
                            case 2:
                                //strCurveName[0] = "力/位移";
                                //LineItemListEdit_1.Add(D1value, F1value);
                                //_RPPList1.Add(D1value, F1value);
                                _chart.Series[1].Points.AddXY(D1value, F1value);
                                break;
                            case 3:
                                //strCurveName[0] = "力/应变";
                                //LineItemListEdit_1.Add(YB1value, F1value);
                                //_RPPList1.Add(YB1value, F1value);
                                _chart.Series[1].Points.AddXY(YB1value, F1value);
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
                                //strCurveName[0] = "应力/时间";
                                //LineItemListEdit_0.Add(time, R1value);
                                //_RPPList0.Add(time, R1value);
                                _chart.Series[0].Points.Add(time, R1value);
                                break;
                            case 2:
                                //strCurveName[0] = "应力/位移";
                                //LineItemListEdit_0.Add(D1value, R1value);
                                //_RPPList0.Add(D1value, R1value);
                                _chart.Series[0].Points.AddXY(D1value, R1value);
                                break;
                            case 3:
                                //strCurveName[0] = "应力/应变";
                                //LineItemListEdit_0.Add(YB1value, R1value);
                                //_RPPList0.Add(YB1value, R1value);
                                _chart.Series[0].Points.AddXY(YB1value, R1value);
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:
                                //strCurveName[0] = "应力/时间";
                                //LineItemListEdit_1.Add(time, R1value);
                                //_RPPList1.Add(time, R1value);
                                _chart.Series[1].Points.AddXY(time, R1value);
                                break;
                            case 2:
                                //strCurveName[0] = "应力/位移";
                                //LineItemListEdit_1.Add(D1value, R1value);
                                //_RPPList1.Add(D1value, R1value);
                                _chart.Series[1].Points.AddXY(D1value, R1value);
                                break;
                            case 3:
                                //strCurveName[0] = "应力/应变";
                                //LineItemListEdit_1.Add(YB1value, R1value);
                                //_RPPList1.Add(YB1value, R1value);
                                _chart.Series[1].Points.AddXY(YB1value, R1value);
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
                                //strCurveName[0] = "变形/时间";
                                //LineItemListEdit_0.Add(time, BX1value);
                                //_RPPList0.Add(time, BX1value);
                                _chart.Series[0].Points.AddXY(time, BX1value);
                                break;
                            case 2:
                                //strCurveName[0] = "变形/位移";
                                //LineItemListEdit_0.Add(D1value, BX1value);
                                //_RPPList0.Add(D1value, BX1value);
                                _chart.Series[0].Points.AddXY(D1value, BX1value);
                                break;
                            case 3:
                                //strCurveName[0] = "变形/应变";
                                //LineItemListEdit_0.Add(YB1value, BX1value);
                                //_RPPList0.Add(YB1value, BX1value);
                                _chart.Series[0].Points.AddXY(YB1value, BX1value);
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:
                                //strCurveName[0] = "变形/时间";
                                //LineItemListEdit_1.Add(time, BX1value);
                                //_RPPList1.Add(time, BX1value);
                                _chart.Series[1].Points.AddXY(time, BX1value);
                                break;
                            case 2:
                                //strCurveName[0] = "变形/位移";
                                //LineItemListEdit_1.Add(D1value, BX1value);
                                //_RPPList1.Add(D1value, BX1value);
                                _chart.Series[1].Points.AddXY(D1value, BX1value);
                                break;
                            case 3:
                                //strCurveName[0] = "变形/应变";
                                //LineItemListEdit_1.Add(YB1value, BX1value);
                                //_RPPList1.Add(YB1value, BX1value);
                                _chart.Series[1].Points.AddXY(YB1value, BX1value);
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
                                //strCurveName[0] = "位移/时间";
                                //LineItemListEdit_0.Add(time, D1value);
                                //_RPPList0.Add(time, D1value);
                                _chart.Series[0].Points.AddXY(time, D1value);
                                break;
                            case 2:
                                //strCurveName[0] = "位移/位移";
                                //LineItemListEdit_0.Add(D1value, D1value);
                                //_RPPList0.Add(D1value, D1value);
                                break;
                            case 3:
                                //strCurveName[0] = "位移/应变";
                                //LineItemListEdit_0.Add(YB1value, D1value);
                                //_RPPList0.Add(YB1value, D1value);
                                _chart.Series[0].Points.AddXY(YB1value, D1value);
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        switch (_X2)//this.tscbX2.SelectedIndex
                        {
                            case 1:
                                //strCurveName[0] = "位移/时间";
                                //LineItemListEdit_1.Add(time, D1value);
                                //_RPPList1.Add(time, D1value);
                                _chart.Series[1].Points.AddXY(time, D1value);
                                break;
                            case 2:
                                //strCurveName[0] = "位移/位移";
                                //LineItemListEdit_1.Add(D1value, D1value);
                                //_RPPList1.Add(D1value, D1value);
                                break;
                            case 3:
                                //strCurveName[0] = "位移/应变";
                                //LineItemListEdit_1.Add(YB1value, D1value);
                                //_RPPList1.Add(YB1value, D1value); 
                                _chart.Series[1].Points.AddXY(YB1value, D1value);
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
                switch (_Y2)//this.tscbY2.SelectedIndex
                {
                    case 1:
                        switch (_X1)//this.tscbX1.SelectedIndex
                        {
                            case 1:
                                //strCurveName[0] = "力/时间";
                                //LineItemListEdit_2.Add(time, F1value);
                                //_RPPList2.Add(time, F1value);  
                                _chart.Series[2].Points.AddXY(time, F1value);
                                break;
                            case 2:
                                //strCurveName[0] = "力/位移";
                                //LineItemListEdit_2.Add(D1value, F1value);
                                //_RPPList2.Add(D1value, F1value); 
                                _chart.Series[2].Points.AddXY(D1value, F1value);
                                break;
                            case 3:
                                //strCurveName[0] = "力/应变";
                                //LineItemListEdit_2.Add(YB1value, F1value);
                                //_RPPList2.Add(YB1value, F1value); 
                                _chart.Series[2].Points.AddXY(YB1value, F1value);
                                break;
                            default:
                                //strCurveName[0] = "";                           
                                break;
                        }
                        switch (_X2)
                        {
                            case 1:
                                //strCurveName[0] = "力/时间";
                                //LineItemListEdit_3.Add(time, F1value);
                                //_RPPList3.Add(time, F1value);
                                _chart.Series[3].Points.AddXY(time, F1value);
                                break;
                            case 2:
                                //strCurveName[0] = "力/位移";
                                //LineItemListEdit_3.Add(D1value, F1value);
                                //_RPPList3.Add(D1value, F1value); 
                                _chart.Series[3].Points.AddXY(D1value, F1value);
                                break;
                            case 3:
                                //strCurveName[0] = "力/应变";
                                //LineItemListEdit_3.Add(YB1value, F1value);
                                //_RPPList3.Add(YB1value, F1value); 
                                _chart.Series[3].Points.AddXY(YB1value, F1value);
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
                                //strCurveName[0] = "应力/时间";
                                //LineItemListEdit_2.Add(time, R1value);
                                //_RPPList2.Add(time, R1value);
                                _chart.Series[2].Points.AddXY(time, R1value);
                                break;
                            case 2:
                                //strCurveName[0] = "应力/位移";
                                //LineItemListEdit_2.Add(D1value, R1value);
                                //_RPPList2.Add(D1value, R1value); 
                                _chart.Series[2].Points.AddXY(D1value, R1value);
                                break;
                            case 3:
                                //strCurveName[0] = "应力/应变";
                                //LineItemListEdit_2.Add(YB1value, R1value);
                                //_RPPList2.Add(YB1value, R1value);
                                _chart.Series[2].Points.AddXY(YB1value, R1value);
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        switch (_X2)
                        {
                            case 1:
                                //strCurveName[0] = "应力/时间";
                                //LineItemListEdit_3.Add(time, R1value);
                                //_RPPList3.Add(time, R1value); 
                                _chart.Series[3].Points.AddXY(time, R1value);
                                break;
                            case 2:
                                //strCurveName[0] = "应力/位移";
                                //LineItemListEdit_3.Add(D1value, R1value);
                                //_RPPList3.Add(D1value, R1value); 
                                _chart.Series[3].Points.AddXY(D1value, R1value);
                                break;
                            case 3:
                                //strCurveName[0] = "应力/应变";
                                //LineItemListEdit_3.Add(YB1value, R1value);
                                //_RPPList3.Add(YB1value, R1value);
                                _chart.Series[3].Points.AddXY(YB1value, R1value);
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
                                //strCurveName[0] = "变形/时间";
                                //LineItemListEdit_2.Add(time, BX1value);
                                //_RPPList2.Add(time, BX1value);
                                _chart.Series[2].Points.AddXY(time, BX1value);
                                break;
                            case 2:
                                //strCurveName[0] = "变形/位移";
                                //LineItemListEdit_2.Add(D1value, BX1value);
                                //_RPPList2.Add(D1value, BX1value);
                                _chart.Series[2].Points.AddXY(D1value, BX1value);
                                break;
                            case 3:
                                //strCurveName[0] = "变形/应变";
                                //LineItemListEdit_2.Add(YB1value, BX1value);
                                //_RPPList2.Add(YB1value, BX1value);
                                _chart.Series[2].Points.AddXY(YB1value, BX1value);
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        switch (_X2)
                        {
                            case 1:
                                //strCurveName[0] = "变形/时间";
                                //LineItemListEdit_3.Add(time, BX1value);
                                //_RPPList3.Add(time, BX1value); 
                                _chart.Series[3].Points.AddXY(time, BX1value);
                                break;
                            case 2:
                                //strCurveName[0] = "变形/位移";
                                //LineItemListEdit_3.Add(D1value, BX1value);
                                //_RPPList3.Add(D1value, BX1value);
                                _chart.Series[3].Points.AddXY(D1value, BX1value);
                                break;
                            case 3:
                                //strCurveName[0] = "变形/应变";
                                // LineItemListEdit_3.Add(YB1value, BX1value);
                               // _RPPList3.Add(YB1value, BX1value);
                                _chart.Series[3].Points.AddXY(YB1value, BX1value);
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
                                //strCurveName[0] = "位移/时间";
                                //LineItemListEdit_2.Add(time, D1value);
                                //_RPPList2.Add(time, D1value);
                                _chart.Series[2].Points.AddXY(time, D1value);
                                break;
                            case 2:
                                //strCurveName[0] = "位移/位移";
                                //LineItemListEdit_2.Add(D1value, D1value);
                                //_RPPList2.Add(D1value, D1value);
                                break;
                            case 3:
                                //strCurveName[0] = "位移/应变";
                                //LineItemListEdit_2.Add(YB1value, D1value);
                                //.Add(YB1value, D1value);
                                _chart.Series[2].Points.AddXY(YB1value, D1value);
                                break;
                            default:
                                //strCurveName[0] = "";
                                break;
                        }
                        switch (_X2)
                        {
                            case 1:
                                //strCurveName[0] = "位移/时间";
                                //LineItemListEdit_3.Add(time, D1value);
                                //.Add(time, D1value); 
                                _chart.Series[3].Points.AddXY(time, D1value);
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
                                _chart.Series[3].Points.AddXY(YB1value, D1value);
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

                //调用Invalidate()方法更新图表
               // _chart.Invalidate();
            }

        }

        delegate void delAddDataToChart(int seriesIndex,double x ,double y);
        void AddDataToChart(int seriesIndex,double x,double y)
        {
            this.chart1.Series[seriesIndex].Points.AddXY(x, y);
        }

        private void InitShowChartLegend(System.Windows.Forms.DataVisualization.Charting.Chart _chart)
        {

            //          //Y1    0-请选择-    1力,kN       2应力,MPa      3变形,mm    4位移,mm
            //          //Y2    0-请选择-    1力,kN       2应力,MPa      3变形,mm    4位移,mm

            //          //X1    0-请选择-    1时间,s      2位移,mm       3应变,mm
            //          //X2    0-请选择-    1时间,s      2位移,mm       3应变,mm

            //总共 6 条曲线
            //Y1-X1 Y1-X2 Y2-X1 Y2-X2 Y3-X1 Y3-X2;

            #region Y1-X1 / Y1-X2 第一、二条曲线
            switch (_Y1)
            {
                case 1:                   
                    //y1-x1
                    switch (_X1)
                    {
                        case 1:
                            strCurveName[0] = "负荷-时间"; 
                            _chart.Series[0].Enabled = true;
                            break;
                        case 2:
                            strCurveName[0] = "负荷-位移";
                            _chart.Series[0].Enabled = true;
                            break;
                        case 3:
                            strCurveName[0] = "负荷-应变";
                            _chart.Series[0].Enabled = true;
                            break;
                        default:
                            strCurveName[0] = "";
                            _chart.Series[0].Enabled = false;
                            break;
                    }

                    //y1-x2
                    switch (_X2)
                    {
                        case 1:
                            strCurveName[1] = "负荷-时间";
                            _chart.Series[1].Enabled = true; 
                            break;
                        case 2:
                            strCurveName[1] = "负荷-位移";
                            _chart.Series[1].Enabled = true; 
                            break;
                        case 3:
                            strCurveName[1] = "负荷-应变";
                            _chart.Series[1].Enabled = true; 
                            break;
                        default:
                            strCurveName[1] = "";
                            _chart.Series[1].Enabled = false; 
                            break;
                    }

                    break;

                case 2: 
                    switch (_X1)
                    {
                        case 1:
                            strCurveName[0] = "应力-时间";
                            _chart.Series[0].Enabled = true; 
                            break;
                        case 2:
                            strCurveName[0] = "应力-位移";
                            _chart.Series[0].Enabled = true; 
                            break;
                        case 3:
                            strCurveName[0] = "应力-应变";
                            _chart.Series[0].Enabled = true; 
                            break;
                        default:
                            strCurveName[0] = "";
                            _chart.Series[1].Enabled = false; 
                            break;
                    }
                    //y1-x2
                    switch (_X2)
                    {
                        case 1:
                            strCurveName[1] = "应力-时间"; _chart.Series[1].Enabled = true; 
                            break;
                        case 2:
                            strCurveName[1] = "应力-位移"; _chart.Series[1].Enabled = true; 
                            break;
                        case 3:
                            strCurveName[1] = "应力-应变"; _chart.Series[1].Enabled = true; 
                            break;
                        default:
                            strCurveName[1] = ""; 
                            _chart.Series[1].Enabled = false; 
                            break;
                    }

                    break;


                case 3: 
                    //zedGraph1.GraphPane.YAxisList[1].Scale.IsLabelsInside = true;
                    //y1-x1
                    switch (_X1)
                    {
                        case 1:
                            strCurveName[0] = "变形-时间";
                            _chart.Series[0].Enabled = true; 
                            break;
                        case 2:
                            strCurveName[0] = "变形-位移"; _chart.Series[0].Enabled = true; 
                            break;
                        case 3:
                            strCurveName[0] = "变形-应变"; _chart.Series[0].Enabled = true; 
                            break;
                        default:
                            strCurveName[0] = "";
                            _chart.Series[0].Enabled = false;
                            break;
                    }
                    //y1-x2
                    switch (_X2)
                    {
                        case 1:
                            strCurveName[1] = "变形-时间"; _chart.Series[1].Enabled = true; 
                            break;
                        case 2:
                            strCurveName[1] = "变形-位移"; _chart.Series[1].Enabled = true; 
                            break;
                        case 3:
                            strCurveName[1] = "变形-应变"; _chart.Series[1].Enabled = true; 
                            break;
                        default:
                            strCurveName[1] = "";
                            _chart.Series[1].Enabled = false;
                            break;
                    }
                    break;
                case 4: 
                    //y1-x1
                    switch (_X1)
                    {
                        case 1:
                            strCurveName[0] = "位移-时间"; _chart.Series[0].Enabled = true; 
                            break;
                        case 2:
                            strCurveName[0] = "位移-位移";
                            _chart.Series[0].Enabled = false;
                            break;
                        case 3:
                            strCurveName[0] = "位移-应变"; _chart.Series[0].Enabled = true; 
                            break;
                        default:
                            strCurveName[0] = "";
                            _chart.Series[0].Enabled = false;
                            break;
                    }

                    //y1-x2
                    switch (_X2)
                    {
                        case 1:
                            strCurveName[1] = "位移-时间"; _chart.Series[1].Enabled = true; 
                            break;
                        case 2:
                            strCurveName[1] = "";
                            _chart.Series[1].Enabled = false;
                            break;
                        case 3:
                            strCurveName[1] = "位移-应变"; _chart.Series[1].Enabled = true; 
                            break;
                        default:
                            strCurveName[1] = "";
                            _chart.Series[1].Enabled = false;
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
            switch (_Y2)
            {
 
                case 1: 
                    //y2-x1
                    switch (_X1)
                    {
                        case 1: 
                            //zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "负荷-时间"; _chart.Series[2].Enabled = true;
                            break;
                        case 2: 
                            //zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "负荷-位移"; _chart.Series[2].Enabled = true;
                            break;
                        case 3: 
                            //zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "负荷-应变"; _chart.Series[2].Enabled = true;
                            break;
                        default: 
                            //zedGraph1.GraphPane.XAxis.IsVisible = false;
                            strCurveName[2] = "";
                            _chart.Series[2].Enabled = false;
                            break;
                    }
                    //y2-x2
                    switch (_X2)
                    {
                        case 1: 
                            //zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "负荷-时间"; _chart.Series[3].Enabled = true;
                            break;
                        case 2: 
                            //zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "负荷-位移"; _chart.Series[3].Enabled = true;
                            break;
                        case 3: 
                            //zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "负荷-应变"; _chart.Series[3].Enabled = true;
                            break;
                        default: 
                            //zedGraph1.GraphPane.X2Axis.IsVisible = false;
                            strCurveName[3] = "";
                            _chart.Series[3].Enabled = false;
                            break;
                    }

                    break;
                case 2: 
                    //zedGraph1.GraphPane.YAxis.IsVisible = true;
                    //zedGraph1.GraphPane.YAxis.Scale.IsLabelsInside = true;
                    //y2-x1
                    switch (_X1)
                    {
                        case 1: 
                            //zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "应力-时间"; _chart.Series[2].Enabled = true;
                            break;
                        case 2: 
                            //zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "应力-位移"; _chart.Series[2].Enabled = true;
                            break;
                        case 3: 
                            ///zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "应力-应变"; _chart.Series[2].Enabled = true;
                            break;
                        default: 
                            //zedGraph1.GraphPane.XAxis.IsVisible = false;
                            strCurveName[2] = "";
                            _chart.Series[2].Enabled = false; 
                            break;
                    }
                    //y2-x2
                    switch (_X2)
                    {
                        case 1: 
                            //zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "应力-时间"; _chart.Series[3].Enabled = true;
                            break;
                        case 2: 
                            //zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "应力-位移"; _chart.Series[3].Enabled = true;
                            break;
                        case 3: 
                            //zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "应力-应变"; _chart.Series[3].Enabled = true;
                            break;
                        default: 
                            //zedGraph1.GraphPane.X2Axis.IsVisible = false;
                            strCurveName[3] = "";
                            _chart.Series[3].Enabled = false; 
                            break;
                    }

                    break;
                case 3: 
                    //zedGraph1.GraphPane.YAxis.IsVisible = true;
                    //zedGraph1.GraphPane.YAxis.Scale.IsLabelsInside = true;
                    //y2-x1
                    switch (_X1)
                    {
                        case 1: 
                            //zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "变形-时间"; _chart.Series[2].Enabled = true;
                            break;
                        case 2: 
                            //zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "变形-位移"; _chart.Series[2].Enabled = true;
                            break;
                        case 3: 
                            //zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "变形-应变"; _chart.Series[2].Enabled = true;
                            break;
                        default: 
                            //zedGraph1.GraphPane.XAxis.IsVisible = false;
                            strCurveName[2] = "";
                            _chart.Series[2].Enabled = false; 
                            break;
                    }
                    //y2-x2
                    switch (_X2)
                    {
                        case 1: 
                            //zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "变形-时间"; _chart.Series[3].Enabled = true;
                            break;
                        case 2: 
                            //zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "变形-位移"; _chart.Series[3].Enabled = true;
                            break;
                        case 3: 
                            //zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "变形-应变"; _chart.Series[3].Enabled = true;
                            break;
                        default: 
                            //zedGraph1.GraphPane.X2Axis.IsVisible = false;
                            strCurveName[3] = "";
                            _chart.Series[3].Enabled = false; 
                            break;
                    }
                    break;
                case 4:
                    _chart.Series[2].Enabled = true;
                    _chart.Series[3].Enabled = true;
                    //zedGraph1.GraphPane.YAxis.IsVisible = true;
                    //zedGraph1.GraphPane.YAxis.Scale.IsLabelsInside = true;
                    //y2-x1
                    switch (_X1)
                    {
                        case 1: 
                            //zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "位移-时间"; _chart.Series[2].Enabled = true;
                            break;
                        case 2: 
                            //zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "";
                            _chart.Series[2].Enabled = false; 
                            break;
                        case 3: 
                            //zedGraph1.GraphPane.XAxis.IsVisible = true;
                            strCurveName[2] = "位移-应变"; _chart.Series[2].Enabled = true;
                            break;
                        default: 
                            //zedGraph1.GraphPane.XAxis.IsVisible = false;
                            strCurveName[2] = "";
                            _chart.Series[2].Enabled = false; 
                            break;
                    }
                    //y2-x2
                    switch (_X2)
                    {
                        case 1: 
                            //zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "位移-时间"; _chart.Series[3].Enabled = true;
                            break;
                        case 2: 
                            //zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "";
                            _chart.Series[3].Enabled = false; 
                            break;
                        case 3: 
                            //zedGraph1.GraphPane.X2Axis.IsVisible = true;
                            strCurveName[3] = "位移-应变"; _chart.Series[3].Enabled = true;
                            break;
                        default: 
                            //zedGraph1.GraphPane.X2Axis.IsVisible = false;
                            strCurveName[3] = "";
                            _chart.Series[3].Enabled = false; 
                            break;
                    }
                    break;
                default: 
                    //zedGraph1.GraphPane.YAxis.IsVisible = false;
                    strCurveName[2] = "";
                    strCurveName[3] = "";
            break;
            }
            #endregion

            //foreach (Control ct in this.panelTop.Controls)
            //{
            //    if(ct.Name.Contains("chk"))
            //        ct.Dispose();
            //}

            for (int i = 0; i < 4; i++)
            {
                _chart.Series[i].LegendText = strCurveName[i].ToString();
            }   
        }

        private void AddChartDataLoop()
        {
            while (isTest)
            {
                Thread.Sleep(55);
                chart1.BeginInvoke(new dlgShowCurveFucChart(AddChartData), new object[] { this.chart1 });
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ShowResultPanel();
            readFinishSample(this.dataGridView, this.tvTestSample.SelectedNode.Text);
        }
    }
}
