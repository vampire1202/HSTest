using System;
namespace HR_Test.Model
{
    /// <summary>
    /// GBT3354_Method:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GBT3354_Method
    {
        public GBT3354_Method()
        { }
        #region Model
        private int _id;
        private string _methodname;
        private string _xmlpath;
        private int? _selresultid;
        private bool _isproload;
        private int? _proloadtype;
        private double? _proloadvalue;
        private int? _proloadcontroltype;
        private double? _proloadspeed;
        private int? _islxqf;
        private string _controltype1;
        private string _controltype2;
        private string _controltype3;
        private string _controltype4;
        private string _controltype5;
        private string _controltype6;
        private string _controltype7;
        private string _controltype8;
        private string _controltype9;
        private string _controltype10;
        private string _controltype11;
        private string _controltype12;
        private int? _circlenum;
        private double? _stopvalue;
        private bool _istakedownexten;
        private int? _extenchannel;
        private double? _extenvalue;
        private string _sendcompany;
        private string _getsample;
        private string _strengthplate;
        private string _adhesive;
        private string _samplestate;
        private double? _temperature;
        private double? _humidity;
        private string _teststandard;
        private string _testmethod;
        private string _mathinetype;
        private string _testcondition;
        private string _tester;
        private string _assessor;
        private DateTime? _testdate;
        private string _sampleshape;
        private double? _w;
        private double? _h;
        private double? _d0;
        private double? _do;
        private double? _s0;
        private double? _ll;
        private double? _lt;
        private double? _εz1;
        private double? _εz2;
        private string _sign;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string methodName
        {
            set { _methodname = value; }
            get { return _methodname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string xmlPath
        {
            set { _xmlpath = value; }
            get { return _xmlpath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? selResultID
        {
            set { _selresultid = value; }
            get { return _selresultid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool isProLoad
        {
            set { _isproload = value; }
            get { return _isproload; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? proLoadType
        {
            set { _proloadtype = value; }
            get { return _proloadtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? proLoadValue
        {
            set { _proloadvalue = value; }
            get { return _proloadvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? proLoadControlType
        {
            set { _proloadcontroltype = value; }
            get { return _proloadcontroltype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? proLoadSpeed
        {
            set { _proloadspeed = value; }
            get { return _proloadspeed; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isLxqf
        {
            set { _islxqf = value; }
            get { return _islxqf; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string controlType1
        {
            set { _controltype1 = value; }
            get { return _controltype1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string controlType2
        {
            set { _controltype2 = value; }
            get { return _controltype2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string controlType3
        {
            set { _controltype3 = value; }
            get { return _controltype3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string controlType4
        {
            set { _controltype4 = value; }
            get { return _controltype4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string controlType5
        {
            set { _controltype5 = value; }
            get { return _controltype5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string controlType6
        {
            set { _controltype6 = value; }
            get { return _controltype6; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string controlType7
        {
            set { _controltype7 = value; }
            get { return _controltype7; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string controlType8
        {
            set { _controltype8 = value; }
            get { return _controltype8; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string controlType9
        {
            set { _controltype9 = value; }
            get { return _controltype9; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string controlType10
        {
            set { _controltype10 = value; }
            get { return _controltype10; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string controlType11
        {
            set { _controltype11 = value; }
            get { return _controltype11; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string controlType12
        {
            set { _controltype12 = value; }
            get { return _controltype12; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? circleNum
        {
            set { _circlenum = value; }
            get { return _circlenum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? stopValue
        {
            set { _stopvalue = value; }
            get { return _stopvalue; }
        }
        /// <summary>
        /// 是否取引伸计
        /// </summary>
        public bool isTakeDownExten
        {
            set { _istakedownexten = value; }
            get { return _istakedownexten; }
        }
        /// <summary>
        /// 引伸计通道
        /// </summary>
        public int? extenChannel
        {
            set { _extenchannel = value; }
            get { return _extenchannel; }
        }
        /// <summary>
        /// 取引伸计的值
        /// </summary>
        public double? extenValue
        {
            set { _extenvalue = value; }
            get { return _extenvalue; }
        }
        /// <summary>
        /// 送检单位
        /// </summary>
        public string sendCompany
        {
            set { _sendcompany = value; }
            get { return _sendcompany; }
        }
        /// <summary>
        /// 试样来源
        /// </summary>
        public string getSample
        {
            set { _getsample = value; }
            get { return _getsample; }
        }
        /// <summary>
        /// 加强片:织物,无纬布增强复合材料,铝合金板,无
        /// </summary>
        public string strengthPlate
        {
            set { _strengthplate = value; }
            get { return _strengthplate; }
        }
        /// <summary>
        /// 胶粘剂规格
        /// </summary>
        public string adhesive
        {
            set { _adhesive = value; }
            get { return _adhesive; }
        }
        /// <summary>
        /// 试样状态：干态试样，湿态试样
        /// </summary>
        public string sampleState
        {
            set { _samplestate = value; }
            get { return _samplestate; }
        }
        /// <summary>
        /// 环境温度
        /// </summary>
        public double? temperature
        {
            set { _temperature = value; }
            get { return _temperature; }
        }
        /// <summary>
        /// 环境湿度
        /// </summary>
        public double? humidity
        {
            set { _humidity = value; }
            get { return _humidity; }
        }
        /// <summary>
        /// 执行标准
        /// </summary>
        public string testStandard
        {
            set { _teststandard = value; }
            get { return _teststandard; }
        }
        /// <summary>
        /// 试验方法
        /// </summary>
        public string testMethod
        {
            set { _testmethod = value; }
            get { return _testmethod; }
        }
        /// <summary>
        /// 试验设备
        /// </summary>
        public string mathineType
        {
            set { _mathinetype = value; }
            get { return _mathinetype; }
        }
        /// <summary>
        /// 试验条件
        /// </summary>
        public string testCondition
        {
            set { _testcondition = value; }
            get { return _testcondition; }
        }
        /// <summary>
        /// 试验人员
        /// </summary>
        public string tester
        {
            set { _tester = value; }
            get { return _tester; }
        }
        /// <summary>
        /// 审核人员
        /// </summary>
        public string assessor
        {
            set { _assessor = value; }
            get { return _assessor; }
        }
        /// <summary>
        /// 试验日期
        /// </summary>
        public DateTime? testDate
        {
            set { _testdate = value; }
            get { return _testdate; }
        }
        /// <summary>
        /// 试样形状
        /// </summary>
        public string sampleShape
        {
            set { _sampleshape = value; }
            get { return _sampleshape; }
        }
        /// <summary>
        /// 试样宽度(mm)
        /// </summary>
        public double? w
        {
            set { _w = value; }
            get { return _w; }
        }
        /// <summary>
        /// 试样厚度(mm)
        /// </summary>
        public double? h
        {
            set { _h = value; }
            get { return _h; }
        }
        /// <summary>
        /// 内直径(直径)
        /// </summary>
        public double? d0
        {
            set { _d0 = value; }
            get { return _d0; }
        }
        /// <summary>
        /// 外直径
        /// </summary>
        public double? Do
        {
            set { _do = value; }
            get { return _do; }
        }
        /// <summary>
        /// 试样面积
        /// </summary>
        public double? S0
        {
            set { _s0 = value; }
            get { return _s0; }
        }
        /// <summary>
        /// 纵向引伸计标距(mm)
        /// </summary>
        public double? lL
        {
            set { _ll = value; }
            get { return _ll; }
        }
        /// <summary>
        /// 横向引伸计标距(mm)
        /// </summary>
        public double? lT
        {
            set { _lt = value; }
            get { return _lt; }
        }
        /// <summary>
        /// 纵向应变范围
        /// </summary>
        public double? εz1
        {
            set { _εz1 = value; }
            get { return _εz1; }
        }
        /// <summary>
        /// 纵向应变范围
        /// </summary>
        public double? εz2
        {
            set { _εz2 = value; }
            get { return _εz2; }
        }
        /// <summary>
        /// 试验备注
        /// </summary>
        public string sign
        {
            set { _sign = value; }
            get { return _sign; }
        }
        string _sampleSpec;
        /// <summary>
        /// 材料规格
        /// </summary>
        public string sampleSpec
        {
            set { _sampleSpec = value; }
            get { return _sampleSpec; }
        }
        #endregion Model

    }
}

