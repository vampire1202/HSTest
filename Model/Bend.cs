using System;
namespace HR_Test.Model
{
    /// <summary>
    /// Bend:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Bend
    {
        public Bend()
        { }
        #region Model
        private int _id;
        private int? _testmethodid;
        private string _testno;
        private string _testsampleno;
        private string _sendcompany;
        private string _stuffcardno;
        private string _stuffspec;
        private string _stufftype;
        private string _hotstatus;
        private double? _temperature;
        private double? _humidity;
        private string _teststandard;
        private string _testmethod;
        private string _mathinetype;
        private string _testcondition;
        private string _samplecharacter;
        private string _getsample;
        private string _tester;
        private string _assessor;
        private string _sign;
        private string _sampletype;
        private string _testtype;
        private double? _d;
        private double? _b;
        private double? _h;
        private double? _l;
        private double? _ds;
        private double? _da;
        private double? _r;
        private double? _t;
        private double? _ls;
        private double? _le;
        private double? _l_l;
        private double? _lt;
        private int? _m;
        private double? _n;
        private double? _a;
        private double? _f_bb;
        private double? _f_n;
        private double? _f_n1;
        private double? _f_rb;
        private double? _y;
        private double? _fo;
        private double? _fpb;
        private double? _frb;
        private double? _fbb;
        private double? _fn;
        private double? _fn1;
        private double? _z;
        private double? _s;
        private double? _w;
        private double? _i;
        private double? _eb;
        private double? _σpb;
        private double? _σrb;
        private double? _σbb;
        private double? _u;
        private double? _εpb;
        private double? _εrb;
        private bool _isfinish;
        private bool _isconformity;
        private DateTime? _testdate;
        private string _condition;
        private string _controlmode;
        private bool _isUseExtensometer;
        private bool _isEffective;
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
        public int? testMethodID
        {
            set { _testmethodid = value; }
            get { return _testmethodid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string testNo
        {
            set { _testno = value; }
            get { return _testno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string testSampleNo
        {
            set { _testsampleno = value; }
            get { return _testsampleno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string sendCompany
        {
            set { _sendcompany = value; }
            get { return _sendcompany; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string stuffCardNo
        {
            set { _stuffcardno = value; }
            get { return _stuffcardno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string stuffSpec
        {
            set { _stuffspec = value; }
            get { return _stuffspec; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string stuffType
        {
            set { _stufftype = value; }
            get { return _stufftype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string hotStatus
        {
            set { _hotstatus = value; }
            get { return _hotstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? temperature
        {
            set { _temperature = value; }
            get { return _temperature; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? humidity
        {
            set { _humidity = value; }
            get { return _humidity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string testStandard
        {
            set { _teststandard = value; }
            get { return _teststandard; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string testMethod
        {
            set { _testmethod = value; }
            get { return _testmethod; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string mathineType
        {
            set { _mathinetype = value; }
            get { return _mathinetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string testCondition
        {
            set { _testcondition = value; }
            get { return _testcondition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string sampleCharacter
        {
            set { _samplecharacter = value; }
            get { return _samplecharacter; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string getSample
        {
            set { _getsample = value; }
            get { return _getsample; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string tester
        {
            set { _tester = value; }
            get { return _tester; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string assessor
        {
            set { _assessor = value; }
            get { return _assessor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string sign
        {
            set { _sign = value; }
            get { return _sign; }
        }
        /// <summary>
        /// 试样形状
        /// </summary>
        public string sampleType
        {
            set { _sampletype = value; }
            get { return _sampletype; }
        }
        /// <summary>
        /// 弯曲试验类型:三点弯曲或四点弯曲
        /// </summary>
        public string testType
        {
            set { _testtype = value; }
            get { return _testtype; }
        }
        /// <summary>
        /// 试样直径
        /// </summary>
        public double? d
        {
            set { _d = value; }
            get { return _d; }
        }
        /// <summary>
        /// 试样宽度
        /// </summary>
        public double? b
        {
            set { _b = value; }
            get { return _b; }
        }
        /// <summary>
        /// 试样高度
        /// </summary>
        public double? h
        {
            set { _h = value; }
            get { return _h; }
        }
        /// <summary>
        /// 试样长度
        /// </summary>
        public double? L
        {
            set { _l = value; }
            get { return _l; }
        }
        /// <summary>
        /// 支撑滚柱直径
        /// </summary>
        public double? Ds
        {
            set { _ds = value; }
            get { return _ds; }
        }
        /// <summary>
        /// 施力滚柱直径
        /// </summary>
        public double? Da
        {
            set { _da = value; }
            get { return _da; }
        }
        /// <summary>
        /// 刀刃半径
        /// </summary>
        public double? R
        {
            set { _r = value; }
            get { return _r; }
        }
        /// <summary>
        /// 矩形横截面试样45°角倒棱的宽度
        /// </summary>
        public double? t
        {
            set { _t = value; }
            get { return _t; }
        }
        /// <summary>
        /// 跨距
        /// </summary>
        public double? Ls
        {
            set { _ls = value; }
            get { return _ls; }
        }
        /// <summary>
        /// 扰度计跨距
        /// </summary>
        public double? Le
        {
            set { _le = value; }
            get { return _le; }
        }
        /// <summary>
        /// 力臂
        /// </summary>
        public double? l_l
        {
            set { _l_l = value; }
            get { return _l_l; }
        }
        /// <summary>
        /// 实际力臂
        /// </summary>
        public double? lt
        {
            set { _lt = value; }
            get { return _lt; }
        }
        /// <summary>
        /// 弯曲力扰度数据对的数目
        /// </summary>
        public int? m
        {
            set { _m = value; }
            get { return _m; }
        }
        /// <summary>
        /// 扰度放大倍数
        /// </summary>
        public double? n
        {
            set { _n = value; }
            get { return _n; }
        }
        /// <summary>
        /// 倒棱修正系数------------------------------以下是试验结果的字段
        /// </summary>
        public double? a
        {
            set { _a = value; }
            get { return _a; }
        }
        /// <summary>
        /// 断裂扰度
        /// </summary>
        public double? f_bb
        {
            set { _f_bb = value; }
            get { return _f_bb; }
        }
        /// <summary>
        /// 最后一次施力并将其卸除后的残余扰度
        /// </summary>
        public double? f_n
        {
            set { _f_n = value; }
            get { return _f_n; }
        }
        /// <summary>
        /// 最后前一次施力并将其卸除后的残余扰度
        /// </summary>
        public double? f_n1
        {
            set { _f_n1 = value; }
            get { return _f_n1; }
        }
        /// <summary>
        /// 达到规定残余弯曲应变时的残余扰度
        /// </summary>
        public double? f_rb
        {
            set { _f_rb = value; }
            get { return _f_rb; }
        }
        /// <summary>
        /// 试样弯曲时中性面至弯曲外表面的最大距离
        /// </summary>
        public double? y
        {
            set { _y = value; }
            get { return _y; }
        }
        /// <summary>
        /// 预弯曲力
        /// </summary>
        public double? Fo
        {
            set { _fo = value; }
            get { return _fo; }
        }
        /// <summary>
        /// 规定非比例弯曲力
        /// </summary>
        public double? Fpb
        {
            set { _fpb = value; }
            get { return _fpb; }
        }
        /// <summary>
        /// 规定残余弯曲力
        /// </summary>
        public double? Frb
        {
            set { _frb = value; }
            get { return _frb; }
        }
        /// <summary>
        /// 最大弯曲力
        /// </summary>
        public double? Fbb
        {
            set { _fbb = value; }
            get { return _fbb; }
        }
        /// <summary>
        /// 最后一次施加的弯曲力
        /// </summary>
        public double? Fn
        {
            set { _fn = value; }
            get { return _fn; }
        }
        /// <summary>
        /// 最后前一次施加的弯曲力
        /// </summary>
        public double? Fn1
        {
            set { _fn1 = value; }
            get { return _fn1; }
        }
        /// <summary>
        /// 力轴每毫米代表的力值
        /// </summary>
        public double? Z
        {
            set { _z = value; }
            get { return _z; }
        }
        /// <summary>
        /// 弯曲试验曲线下包围的面积
        /// </summary>
        public double? S
        {
            set { _s = value; }
            get { return _s; }
        }
        /// <summary>
        /// 试样截面系数
        /// </summary>
        public double? W
        {
            set { _w = value; }
            get { return _w; }
        }
        /// <summary>
        /// 试样截面惯性矩
        /// </summary>
        public double? I
        {
            set { _i = value; }
            get { return _i; }
        }
        /// <summary>
        /// 弯曲弹性模量
        /// </summary>
        public double? Eb
        {
            set { _eb = value; }
            get { return _eb; }
        }
        /// <summary>
        /// 规定非比例弯曲应力
        /// </summary>
        public double? σpb
        {
            set { _σpb = value; }
            get { return _σpb; }
        }
        /// <summary>
        /// 规定残余弯曲应力
        /// </summary>
        public double? σrb
        {
            set { _σrb = value; }
            get { return _σrb; }
        }
        /// <summary>
        /// 抗弯强度
        /// </summary>
        public double? σbb
        {
            set { _σbb = value; }
            get { return _σbb; }
        }
        /// <summary>
        /// 弯曲断裂能量
        /// </summary>
        public double? U
        {
            set { _u = value; }
            get { return _u; }
        }
        /// <summary>
        /// 规定非比例弯曲应变
        /// </summary>
        public double? εpb
        {
            set { _εpb = value; }
            get { return _εpb; }
        }
        /// <summary>
        /// 规定残余弯曲应变
        /// </summary>
        public double? εrb
        {
            set { _εrb = value; }
            get { return _εrb; }
        }
        /// <summary>
        /// ----------------------------------------------------
        /// </summary>
        public bool isFinish
        {
            set { _isfinish = value; }
            get { return _isfinish; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool isConformity
        {
            set { _isconformity = value; }
            get { return _isconformity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? testDate
        {
            set { _testdate = value; }
            get { return _testdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string condition
        {
            set { _condition = value; }
            get { return _condition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string controlmode
        {
            set { _controlmode = value; }
            get { return _controlmode; }
        }

        /// <summary>
        /// ----------------------------------------------------
        /// </summary>
        public bool isUseExtensometer
        {
            set { _isUseExtensometer = value; }
            get { return _isUseExtensometer; }
        }

        /// <summary>
        /// ----------------------------------------------------
        /// </summary>
        public bool isEffective
        {
            set { _isEffective = value; }
            get { return _isEffective; }
        }

        #endregion Model

    }
}

