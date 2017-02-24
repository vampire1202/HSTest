using System;
namespace HR_Test.Model
{
	/// <summary>
	/// GBT282892012_Twist:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GBT282892012_Twist
	{
		public GBT282892012_Twist()
		{}
		#region Model
		private int _id;
		private string _testmethodname;
		private string _testno;
		private string _testsampleno;
		private string _reportno;
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
		private double? _l;
		private double? _s0;
		private double? _l0;
		private double? _l1;
		private double? _l2;
		private double? _fmmax;
        private double? _m;
		private bool _isfinish;
		private DateTime? _testdate;
		private string _condition;
		private string _controlmode;
		private bool _isuseextensometer;
        private bool _isEffective;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 试验方法名称
		/// </summary>
		public string testMethodName
		{
			set{ _testmethodname=value;}
			get{return _testmethodname;}
		}
		/// <summary>
		/// 试验编号
		/// </summary>
		public string testNo
		{
			set{ _testno=value;}
			get{return _testno;}
		}
		/// <summary>
		/// 试样编号
		/// </summary>
		public string testSampleNo
		{
			set{ _testsampleno=value;}
			get{return _testsampleno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string reportNo
		{
			set{ _reportno=value;}
			get{return _reportno;}
		}
		/// <summary>
		/// 送检单位
		/// </summary>
		public string sendCompany
		{
			set{ _sendcompany=value;}
			get{return _sendcompany;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string stuffCardNo
		{
			set{ _stuffcardno=value;}
			get{return _stuffcardno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string stuffSpec
		{
			set{ _stuffspec=value;}
			get{return _stuffspec;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string stuffType
		{
			set{ _stufftype=value;}
			get{return _stufftype;}
		}
		/// <summary>
		/// 热处理状态
		/// </summary>
		public string hotStatus
		{
			set{ _hotstatus=value;}
			get{return _hotstatus;}
		}
		/// <summary>
		/// 温度
		/// </summary>
		public double? temperature
		{
			set{ _temperature=value;}
			get{return _temperature;}
		}
		/// <summary>
		/// 湿度
		/// </summary>
		public double? humidity
		{
			set{ _humidity=value;}
			get{return _humidity;}
		}
		/// <summary>
		/// 标准号
		/// </summary>
		public string testStandard
		{
			set{ _teststandard=value;}
			get{return _teststandard;}
		}
		/// <summary>
		/// 试验方法名称
		/// </summary>
		public string testMethod
		{
			set{ _testmethod=value;}
			get{return _testmethod;}
		}
		/// <summary>
		/// 试验设备
		/// </summary>
		public string mathineType
		{
			set{ _mathinetype=value;}
			get{return _mathinetype;}
		}
		/// <summary>
		/// 试验环境
		/// </summary>
		public string testCondition
		{
			set{ _testcondition=value;}
			get{return _testcondition;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string sampleCharacter
		{
			set{ _samplecharacter=value;}
			get{return _samplecharacter;}
		}
		/// <summary>
		/// 试样取样
		/// </summary>
		public string getSample
		{
			set{ _getsample=value;}
			get{return _getsample;}
		}
		/// <summary>
		/// 试验员
		/// </summary>
		public string tester
		{
			set{ _tester=value;}
			get{return _tester;}
		}
		/// <summary>
		/// 审核员
		/// </summary>
		public string assessor
		{
			set{ _assessor=value;}
			get{return _assessor;}
		}
		/// <summary>
		/// 备注----------------------------
		/// </summary>
		public string sign
		{
			set{ _sign=value;}
			get{return _sign;}
		}
		/// <summary>
		/// 试验长度
		/// </summary>
		public double? L
		{
			set{ _l=value;}
			get{return _l;}
		}
		/// <summary>
		/// 横截面面积
		/// </summary>
		public double? S0
		{
			set{ _s0=value;}
			get{return _s0;}
		}
		/// <summary>
		/// 力矩
		/// </summary>
		public double? L0
		{
			set{ _l0=value;}
			get{return _l0;}
		}
		/// <summary>
		/// 隔热材料高度mm
		/// </summary>
		public double? L1
		{
			set{ _l1=value;}
			get{return _l1;}
		}
		/// <summary>
		/// 受力点到隔热材料与铝合金型材相接位置的距离mm
		/// </summary>
		public double? L2
		{
			set{ _l2=value;}
			get{return _l2;}
		}
		/// <summary>
		/// 最大负荷
		/// </summary>
		public double? FMmax
		{
			set{ _fmmax=value;}
			get{return _fmmax;}
		}
		/// <summary>
		/// 抗扭力矩 kN*mm
		/// </summary>
        public double? M
		{
			set{ _m=value;}
			get{return _m;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool isFinish
		{
			set{ _isfinish=value;}
			get{return _isfinish;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? testDate
		{
			set{ _testdate=value;}
			get{return _testdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string condition
		{
			set{ _condition=value;}
			get{return _condition;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string controlmode
		{
			set{ _controlmode=value;}
			get{return _controlmode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool isUseExtensometer
		{
			set{ _isuseextensometer=value;}
			get{return _isuseextensometer;}
		}
        
        /// <summary>
		/// 
		/// </summary>
        public bool isEffective
		{
            set { _isEffective = value; }
            get { return _isEffective; }
		}
		#endregion Model

	}
}

