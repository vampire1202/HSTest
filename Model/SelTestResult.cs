using System;
namespace HR_Test.Model
{
	/// <summary>
	/// SelTestResult:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SelTestResult
	{
		public SelTestResult()
		{}
		#region Model
		private int _id;
		private string _methodname;
		private bool _fm;
		private bool _rm;
		private bool _reh;
		private bool _rel;
		private bool _rp;
		private bool _rt;
		private bool _rr;
        //private bool _εp;
        //private bool _εt;
        //private bool _εr;
		private bool _e;
		private bool _m;
		private bool _me;
		private bool _a;
		private bool _aee;
		private bool _agg;
		private bool _att;
		private bool _aggtt;
		private bool _awnwn;
		private bool _deltalm;
		private bool _lf;
		private bool _z;
		private bool _avera;
		private bool _ss;
		private bool _avera1;
        private bool _cv;
        private bool _handaz;
        private bool _savecurve;
        private bool _lm;
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
		public bool Fm
		{
			set{ _fm=value;}
			get{return _fm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Rm
		{
			set{ _rm=value;}
			get{return _rm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool ReH
		{
			set{ _reh=value;}
			get{return _reh;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool ReL
		{
			set{ _rel=value;}
			get{return _rel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Rp
		{
			set{ _rp=value;}
			get{return _rp;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Rt
		{
			set{ _rt=value;}
			get{return _rt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Rr
		{
			set{ _rr=value;}
			get{return _rr;}
		}
        ///// <summary>
        ///// 
        ///// </summary>
        //public bool εp
        //{
        //    set{ _εp=value;}
        //    get{return _εp;}
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public bool εt
        //{
        //    set{ _εt=value;}
        //    get{return _εt;}
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public bool εr
        //{
        //    set{ _εr=value;}
        //    get{return _εr;}
        //}
		/// <summary>
		/// 
		/// </summary>
		public bool E
		{
			set{ _e=value;}
			get{return _e;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool m
		{
			set{ _m=value;}
			get{return _m;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool mE
		{
			set{ _me=value;}
			get{return _me;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool A
		{
			set{ _a=value;}
			get{return _a;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Aee
		{
			set{ _aee=value;}
			get{return _aee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Agg
		{
			set{ _agg=value;}
			get{return _agg;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Att
		{
			set{ _att=value;}
			get{return _att;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Aggtt
		{
			set{ _aggtt=value;}
			get{return _aggtt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Awnwn
		{
			set{ _awnwn=value;}
			get{return _awnwn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Lm
		{
			set{ _lm=value;}
			get{return _lm;}
		}
        /// <summary>
        /// 
        /// </summary>
        public bool deltaLm
        {
            set { _deltalm = value; }
            get { return _deltalm; }
        }
		/// <summary>
		/// 
		/// </summary>
		public bool Lf
		{
			set{ _lf=value;}
			get{return _lf;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Z
		{
			set{ _z=value;}
			get{return _z;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Avera
		{
			set{ _avera=value;}
			get{return _avera;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool SS
		{
			set{ _ss=value;}
			get{return _ss;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Avera1
		{
			set{ _avera1=value;}
			get{return _avera1;}
		}
        /// <summary>
        /// 
        /// </summary>
        public bool CV
        {
            set { _cv = value; }
            get { return _cv; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Handaz
        {
            set { _handaz = value; }
            get { return _handaz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Savecurve
        {
            set { _savecurve = value; }
            get { return _savecurve; }
        }
		#endregion Model

	}
}

