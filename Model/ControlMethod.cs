using System;
namespace HR_Test.Model
{
	/// <summary>
	/// ControlMethod:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ControlMethod
	{
		public ControlMethod()
		{}
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
		private string _sign;
        private string _condition;
        private string _controlmode;
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
		public string methodName
		{
			set{ _methodname=value;}
			get{return _methodname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string xmlPath
		{
			set{ _xmlpath=value;}
			get{return _xmlpath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? selResultID
		{
			set{ _selresultid=value;}
			get{return _selresultid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool isProLoad
		{
			set{ _isproload=value;}
			get{return _isproload;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? proLoadType
		{
			set{ _proloadtype=value;}
			get{return _proloadtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? proLoadValue
		{
			set{ _proloadvalue=value;}
			get{return _proloadvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? proLoadControlType
		{
			set{ _proloadcontroltype=value;}
			get{return _proloadcontroltype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? proLoadSpeed
		{
			set{ _proloadspeed=value;}
			get{return _proloadspeed;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isLxqf
		{
			set{ _islxqf=value;}
			get{return _islxqf;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string controlType1
		{
			set{ _controltype1=value;}
			get{return _controltype1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string controlType2
		{
			set{ _controltype2=value;}
			get{return _controltype2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string controlType3
		{
			set{ _controltype3=value;}
			get{return _controltype3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string controlType4
		{
			set{ _controltype4=value;}
			get{return _controltype4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string controlType5
		{
			set{ _controltype5=value;}
			get{return _controltype5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string controlType6
		{
			set{ _controltype6=value;}
			get{return _controltype6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string controlType7
		{
			set{ _controltype7=value;}
			get{return _controltype7;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string controlType8
		{
			set{ _controltype8=value;}
			get{return _controltype8;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string controlType9
		{
			set{ _controltype9=value;}
			get{return _controltype9;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string controlType10
		{
			set{ _controltype10=value;}
			get{return _controltype10;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string controlType11
		{
			set{ _controltype11=value;}
			get{return _controltype11;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string controlType12
		{
			set{ _controltype12=value;}
			get{return _controltype12;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? circleNum
		{
			set{ _circlenum=value;}
			get{return _circlenum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? stopValue
		{
			set{ _stopvalue=value;}
			get{return _stopvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool isTakeDownExten
		{
			set{ _istakedownexten=value;}
			get{return _istakedownexten;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? extenChannel
		{
			set{ _extenchannel=value;}
			get{return _extenchannel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public double? extenValue
		{
			set{ _extenvalue=value;}
			get{return _extenvalue;}
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
		public string sign
		{
			set{ _sign=value;}
			get{return _sign;}
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

		#endregion Model

	}
}

