using System;
namespace HR_Test.Model
{
	/// <summary>
	/// GBT282892012_ShearSel:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GBT282892012_ShearSel
	{
		public GBT282892012_ShearSel()
		{}
		#region Model
		private int _id;
		private string _methodname;
		private bool _ftmax= false;
		private bool _t= false;
		private bool _c1= false;
		private bool _c1r_= false;
		private bool _c1cr= false;
		private bool _c1l_= false;
		private bool _c1cl= false;
		private bool _c1h_= false;
		private bool _c1ch= false;
		private bool _t_= false;
		private bool _st= false;
		private bool _tc= false;
		private bool _savecurve= false;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 方法名称
		/// </summary>
		public string methodName
		{
			set{ _methodname=value;}
			get{return _methodname;}
		}
		/// <summary>
		/// 最大剪切力
		/// </summary>
		public bool FTmax
		{
			set{ _ftmax=value;}
			get{return _ftmax;}
		}
		/// <summary>
		/// 单位长度上所承受的最大剪切力
		/// </summary>
		public bool T
		{
			set{ _t=value;}
			get{return _t;}
		}
		/// <summary>
		/// 弹性系数
		/// </summary>
		public bool C1
		{
			set{ _c1=value;}
			get{return _c1;}
		}
		/// <summary>
		/// 10个试样室温的弹性系数平均值
		/// </summary>
		public bool C1R_
		{
			set{ _c1r_=value;}
			get{return _c1r_;}
		}
		/// <summary>
		/// 室温的弹性系数特征值
		/// </summary>
		public bool C1cR
		{
			set{ _c1cr=value;}
			get{return _c1cr;}
		}
		/// <summary>
		/// 10个试样低温的弹性系数平均值
		/// </summary>
		public bool C1L_
		{
			set{ _c1l_=value;}
			get{return _c1l_;}
		}
		/// <summary>
		/// 低温弹性系数特征值
		/// </summary>
		public bool C1cL
		{
			set{ _c1cl=value;}
			get{return _c1cl;}
		}
		/// <summary>
		/// 10个试样高温的弹性系数平均值
		/// </summary>
		public bool C1H_
		{
			set{ _c1h_=value;}
			get{return _c1h_;}
		}
		/// <summary>
		/// 高温弹性系数特征值
		/// </summary>
		public bool C1cH
		{
			set{ _c1ch=value;}
			get{return _c1ch;}
		}
		/// <summary>
		/// 10个试样单位长度上所承受最大剪切力的平均值
		/// </summary>
		public bool T_
		{
			set{ _t_=value;}
			get{return _t_;}
		}
		/// <summary>
		/// 10个试样单位长度上所承受的最大剪切力标准差
		/// </summary>
		public bool ST
		{
			set{ _st=value;}
			get{return _st;}
		}
		/// <summary>
		/// 抗剪特征值
		/// </summary>
		public bool Tc
		{
			set{ _tc=value;}
			get{return _tc;}
		}
		/// <summary>
		/// 是否保存曲线
		/// </summary>
		public bool saveCurve
		{
			set{ _savecurve=value;}
			get{return _savecurve;}
		}
		#endregion Model

	}
}

