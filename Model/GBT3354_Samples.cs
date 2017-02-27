using System;
namespace HR_Test.Model
{
    /// <summary>
    /// GBT3354_Samples:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GBT3354_Samples
    {
        public GBT3354_Samples()
        { }
        #region Model
        private int _id;
        private string _testmethodname;
        private string _testno;
        private string _testsampleno;
        private string _reportno;
        private string _sendcompany;
        private string _stuffspec;
        private string _getsample;
        private string _strengthplate;
        private string _adhesive;
        private string _samplestate;
        private double? _temperature;
        private double? _humidity;
        private string _teststandard;
        private string _testmethod;
        private string _mathinetype;
        private string _tester;
        private string _assessor;
        private DateTime? _testdate;
        private string _testcondition;
        private string _controlmode;
        private string _sampleshape;
        private double? _w;
        private double? _h;
        private double? _d0;
        private double? _do;
        private double? _s0;
        private double? _ll;
        private double? _lt;
        private string _εz;
        private string _failuremode;
        private double? _pmax;
        private double? _σt;
        private double? _et;
        private double? _μ12;
        private double? _ε1t;
        private bool _isfinish;
        private bool _isuseextensometer1 = false;
        private bool _isuseextensometer2;
        private bool _iseffective = false;
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
        /// 试验项目名称
        /// </summary>
        public string testMethodName
        {
            set { _testmethodname = value; }
            get { return _testmethodname; }
        }
        /// <summary>
        /// 实验编号
        /// </summary>
        public string testNo
        {
            set { _testno = value; }
            get { return _testno; }
        }
        /// <summary>
        /// 试样编号
        /// </summary>
        public string testSampleNo
        {
            set { _testsampleno = value; }
            get { return _testsampleno; }
        }
        /// <summary>
        /// 报告
        /// </summary>
        public string reportNo
        {
            set { _reportno = value; }
            get { return _reportno; }
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
        /// 材料规格
        /// </summary>
        public string stuffSpec
        {
            set { _stuffspec = value; }
            get { return _stuffspec; }
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
        /// 试验条件
        /// </summary>
        public string testCondition
        {
            set { _testcondition = value; }
            get { return _testcondition; }
        }
        /// <summary>
        /// 控制方式
        /// </summary>
        public string controlmode
        {
            set { _controlmode = value; }
            get { return _controlmode; }
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
        public string εz
        {
            set { _εz = value; }
            get { return _εz; }
        }
        /// <summary>
        /// 失效模式
        /// </summary>
        public string failuremode
        {
            set { _failuremode = value; }
            get { return _failuremode; }
        }
        /// <summary>
        /// 最大载荷N
        /// </summary>
        public double? Pmax
        {
            set { _pmax = value; }
            get { return _pmax; }
        }
        /// <summary>
        /// 拉伸强度MPa
        /// </summary>
        public double? σt
        {
            set { _σt = value; }
            get { return _σt; }
        }
        /// <summary>
        /// 弹性模量(MPa)
        /// </summary>
        public double? Et
        {
            set { _et = value; }
            get { return _et; }
        }
        /// <summary>
        /// 泊松比
        /// </summary>
        public double? μ12
        {
            set { _μ12 = value; }
            get { return _μ12; }
        }
        /// <summary>
        /// 破坏应变mm/mm = deltalb/l
        /// </summary>
        public double? ε1t
        {
            set { _ε1t = value; }
            get { return _ε1t; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool isFinish
        {
            set { _isfinish = value; }
            get { return _isfinish; }
        }
        /// <summary>
        /// 是否使用引伸计1
        /// </summary>
        public bool isUseExtensometer1
        {
            set { _isuseextensometer1 = value; }
            get { return _isuseextensometer1; }
        }
        /// <summary>
        /// 是否使用引伸计2
        /// </summary>
        public bool isUseExtensometer2
        {
            set { _isuseextensometer2 = value; }
            get { return _isuseextensometer2; }
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool isEffective
        {
            set { _iseffective = value; }
            get { return _iseffective; }
        }
        /// <summary>
        /// 试验备注
        /// </summary>
        public string sign
        {
            set { _sign = value; }
            get { return _sign; }
        }
        #endregion Model

    }
}

