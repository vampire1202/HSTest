using System;
namespace HR_Test.Model
{
	/// <summary>
	/// GBT236152009_TensileZong:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GBT236152009_TensileZong
	{
		public GBT236152009_TensileZong()
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
		private string _testtype;
		private double? _l0;
		private double? _b2;
		private double? _t;
		private double? _s0;
		private double? _fmax;
		private double? _t2;
		private double? _z;
		private double? _e;
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
		/// 横向拉伸或纵向拉伸
		/// </summary>
		public string testType
		{
			set{ _testtype=value;}
			get{return _testtype;}
		}
		/// <summary>
		/// 标距
		/// </summary>
		public double? L0
		{
			set{ _l0=value;}
			get{return _l0;}
		}
		/// <summary>
		/// 中间水平部分宽度
		/// </summary>
		public double? b2
		{
			set{ _b2=value;}
			get{return _b2;}
		}
		/// <summary>
		/// 试样厚度
		/// </summary>
		public double? t
		{
			set{ _t=value;}
			get{return _t;}
		}
		/// <summary>
		/// 面积
		/// </summary>
		public double? S0
		{
			set{ _s0=value;}
			get{return _s0;}
		}
		/// <summary>
		/// 最大力值
		/// </summary>
		public double? Fmax
		{
			set{ _fmax=value;}
			get{return _fmax;}
		}
		/// <summary>
		/// 抗拉强度
		/// </summary>
		public double? T2
		{
			set{ _t2=value;}
			get{return _t2;}
		}
		/// <summary>
		/// 断裂伸长率
		/// </summary>
		public double? Z
		{
			set{ _z=value;}
			get{return _z;}
		}
		/// <summary>
		/// 弹性模量
		/// </summary>
		public double? E
		{
			set{ _e=value;}
			get{return _e;}
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

