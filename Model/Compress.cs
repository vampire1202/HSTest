using System;
namespace HR_Test.Model
{
	/// <summary>
	/// Compress:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Compress
	{
		public Compress()
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
		private double? _a;
		private double? _b;
		private double? _d;
		private double? _l;
		private double? _l0;
		private double? _h;
		private double? _hh;
		private double? _s0;
		private double? _deltal;
		private double? _εpc;
		private double? _εtc;
		private double? _n;
		private double? _f0;
		private double? _ff;
		private double? _fpc;
		private double? _ftc;
		private double? _fehc;
		private double? _felc;
		private double? _fmc;
		private double? _rpc;
		private double? _rtc;
		private double? _rehc;
		private double? _relc;
		private double? _rmc;
		private double? _ec;
		private double? _avera;
		private double? _avera1;
		private bool _isfinish;
		private DateTime _testdate;
        private string _condition;
        private string _controlmode;
        private bool _isUseExtensometer;
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
		/// 
		/// </summary>
		public string testMethodName
		{
			set{ _testmethodname=value;}
			get{return _testmethodname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string testNo
		{
			set{ _testno=value;}
			get{return _testno;}
		}
		/// <summary>
		/// 
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
		/// 
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
		/// 
		/// </summary>
		public string hotStatus
		{
			set{ _hotstatus=value;}
			get{return _hotstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? temperature
		{
			set{ _temperature=value;}
			get{return _temperature;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? humidity
		{
			set{ _humidity=value;}
			get{return _humidity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string testStandard
		{
			set{ _teststandard=value;}
			get{return _teststandard;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string testMethod
		{
			set{ _testmethod=value;}
			get{return _testmethod;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mathineType
		{
			set{ _mathinetype=value;}
			get{return _mathinetype;}
		}
		/// <summary>
		/// 
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
		/// 
		/// </summary>
		public string getSample
		{
			set{ _getsample=value;}
			get{return _getsample;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string tester
		{
			set{ _tester=value;}
			get{return _tester;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string assessor
		{
			set{ _assessor=value;}
			get{return _assessor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string sign
		{
			set{ _sign=value;}
			get{return _sign;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? a
		{
			set{ _a=value;}
			get{return _a;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? b
		{
			set{ _b=value;}
			get{return _b;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? d
		{
			set{ _d=value;}
			get{return _d;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? L
		{
			set{ _l=value;}
			get{return _l;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? L0
		{
			set{ _l0=value;}
			get{return _l0;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? H
		{
			set{ _h=value;}
			get{return _h;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? hh
		{
			set{ _hh=value;}
			get{return _hh;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? S0
		{
			set{ _s0=value;}
			get{return _s0;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? deltaL
		{
			set{ _deltal=value;}
			get{return _deltal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? εpc
		{
			set{ _εpc=value;}
			get{return _εpc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? εtc
		{
			set{ _εtc=value;}
			get{return _εtc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? n
		{
			set{ _n=value;}
			get{return _n;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? F0
		{
			set{ _f0=value;}
			get{return _f0;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Ff
		{
			set{ _ff=value;}
			get{return _ff;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Fpc
		{
			set{ _fpc=value;}
			get{return _fpc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Ftc
		{
			set{ _ftc=value;}
			get{return _ftc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? FeHc
		{
			set{ _fehc=value;}
			get{return _fehc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? FeLc
		{
			set{ _felc=value;}
			get{return _felc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Fmc
		{
			set{ _fmc=value;}
			get{return _fmc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Rpc
		{
			set{ _rpc=value;}
			get{return _rpc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Rtc
		{
			set{ _rtc=value;}
			get{return _rtc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? ReHc
		{
			set{ _rehc=value;}
			get{return _rehc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? ReLc
		{
			set{ _relc=value;}
			get{return _relc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Rmc
		{
			set{ _rmc=value;}
			get{return _rmc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Ec
		{
			set{ _ec=value;}
			get{return _ec;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Avera
		{
			set{ _avera=value;}
			get{return _avera;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Avera1
		{
			set{ _avera1=value;}
			get{return _avera1;}
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
		public DateTime testDate
		{
			set{ _testdate=value;}
			get{return _testdate;}
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
        public string controlMode
        {
            set { _controlmode = value; }
            get { return _controlmode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool isUseExtensometer
        {
            set { _isUseExtensometer = value; }
            get { return _isUseExtensometer; }
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

