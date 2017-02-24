using System;
namespace HR_Test.Model
{
	/// <summary>
	/// TestSample:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TestSample
	{
		public TestSample()
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
		private double? _a0;
		private double? _au;
		private double? _b0;
		private double? _bu;
		private double? _d0;
		private double? _du;
		private double? _do;
		private double? _l0;
		private double? _l01;
		private double? _lc;
		private double? _le;
		private double? _lt;
		private double? _lu;
		private double? _lu1;
		private double? _s0;
		private double? _su;
		private double? _k;
		private double? _fm;
		private double? _rm;
		private double? _reh;
		private double? _rel;
		private double? _rp;
		private double? _rt;
		private double? _rr;
		private double? _εp;
		private double? _εt;
		private double? _εr;
		private double? _e;
		private double? _m;
		private double? _me;
		private double? _a;
		private double? _aee;
		private double? _agg;
		private double? _att;
		private double? _aggtt;
		private double? _awnwn;
		private double? _lm;
        private double? _deltalm;
		private double? _lf;
		private double? _z;
		private double? _avera;
		private double? _ss;
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
		public double? a0
		{
			set{ _a0=value;}
			get{return _a0;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? au
		{
			set{ _au=value;}
			get{return _au;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? b0
		{
			set{ _b0=value;}
			get{return _b0;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? bu
		{
			set{ _bu=value;}
			get{return _bu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? d0
		{
			set{ _d0=value;}
			get{return _d0;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? du
		{
			set{ _du=value;}
			get{return _du;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Do
		{
			set{ _do=value;}
			get{return _do;}
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
		public double? L01
		{
			set{ _l01=value;}
			get{return _l01;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Lc
		{
			set{ _lc=value;}
			get{return _lc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Le
		{
			set{ _le=value;}
			get{return _le;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Lt
		{
			set{ _lt=value;}
			get{return _lt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Lu
		{
			set{ _lu=value;}
			get{return _lu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Lu1
		{
			set{ _lu1=value;}
			get{return _lu1;}
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
		public double? Su
		{
			set{ _su=value;}
			get{return _su;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? k
		{
			set{ _k=value;}
			get{return _k;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Fm
		{
			set{ _fm=value;}
			get{return _fm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Rm
		{
			set{ _rm=value;}
			get{return _rm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? ReH
		{
			set{ _reh=value;}
			get{return _reh;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? ReL
		{
			set{ _rel=value;}
			get{return _rel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Rp
		{
			set{ _rp=value;}
			get{return _rp;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Rt
		{
			set{ _rt=value;}
			get{return _rt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Rr
		{
			set{ _rr=value;}
			get{return _rr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? εp
		{
			set{ _εp=value;}
			get{return _εp;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? εt
		{
			set{ _εt=value;}
			get{return _εt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? εr
		{
			set{ _εr=value;}
			get{return _εr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? E
		{
			set{ _e=value;}
			get{return _e;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? m
		{
			set{ _m=value;}
			get{return _m;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? mE
		{
			set{ _me=value;}
			get{return _me;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? A
		{
			set{ _a=value;}
			get{return _a;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Aee
		{
			set{ _aee=value;}
			get{return _aee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Agg
		{
			set{ _agg=value;}
			get{return _agg;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Att
		{
			set{ _att=value;}
			get{return _att;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Aggtt
		{
			set{ _aggtt=value;}
			get{return _aggtt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Awnwn
		{
			set{ _awnwn=value;}
			get{return _awnwn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Lm
		{
			set{ _lm=value;}
			get{return _lm;}
		}

        /// <summary>
        /// 
        /// </summary>
        public double? deltaLm
        {
            set { _deltalm = value; }
            get { return _deltalm; }
        }

		/// <summary>
		/// 
		/// </summary>
		public double? Lf
		{
			set{ _lf=value;}
			get{return _lf;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? Z
		{
			set{ _z=value;}
			get{return _z;}
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
		public double? SS
		{
			set{ _ss=value;}
			get{return _ss;}
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
        public string controlmode
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

