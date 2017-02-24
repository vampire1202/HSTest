using System;
namespace HR_Test.Model
{
	/// <summary>
	/// GBT236152009_SelZong:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GBT236152009_SelZong
	{
		public GBT236152009_SelZong()
		{}
		#region Model
		private int _id;
		private string _methodname;
		private bool _fmax= false;
		private bool _t2= false;
		private bool _z= false;
		private bool _e= false;
		private bool _t2_= false;
		private bool _s= false;
		private bool _t2c= false;
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
		public bool T2
		{
			set{ _t2=value;}
			get{return _t2;}
		}
		/// <summary>
		/// 断裂伸长率
		/// </summary>
		public bool Z
		{
			set{ _z=value;}
			get{return _z;}
		}
		/// <summary>
		/// 弹性模量
		/// </summary>
		public bool E
		{
			set{ _e=value;}
			get{return _e;}
		}
		/// <summary>
		/// 10个试样承受的最大抗拉强度平均值
		/// </summary>
		public bool T2_
		{
			set{ _t2_=value;}
			get{return _t2_;}
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
		/// 抗拉特征值
		/// </summary>
		public bool T2c
		{
			set{ _t2c=value;}
			get{return _t2c;}
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

