using System;
namespace HR_Test.Model
{
	/// <summary>
	/// GBT236152009_SelHeng:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GBT236152009_SelHeng
	{
		public GBT236152009_SelHeng()
		{}
		#region Model
		private int _id;
		private string _methodname;
		private bool _fmax= false;
		private bool _t1= false;
		private bool _t1_= false;
		private bool _s= false;
		private bool _t1c= false;
		private bool _issavecurve= false;
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
		/// 最大力值
		/// </summary>
		public bool Fmax
		{
			set{ _fmax=value;}
			get{return _fmax;}
		}
		/// <summary>
		/// 抗拉强度
		/// </summary>
		public bool T1
		{
			set{ _t1=value;}
			get{return _t1;}
		}
		/// <summary>
		/// 最大抗拉强度平均值
		/// </summary>
		public bool T1_
		{
			set{ _t1_=value;}
			get{return _t1_;}
		}
		/// <summary>
		/// 标准偏差
		/// </summary>
		public bool S
		{
			set{ _s=value;}
			get{return _s;}
		}
		/// <summary>
		/// 横向抗拉特征值
		/// </summary>
		public bool T1c
		{
			set{ _t1c=value;}
			get{return _t1c;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool isSaveCurve
		{
			set{ _issavecurve=value;}
			get{return _issavecurve;}
		}
		#endregion Model

	}
}

