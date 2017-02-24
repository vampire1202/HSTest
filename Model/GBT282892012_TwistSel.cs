using System;
namespace HR_Test.Model
{
	/// <summary>
	/// GBT282892012_TwistSel:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GBT282892012_TwistSel
	{
		public GBT282892012_TwistSel()
		{}
		#region Model
		private int _id;
		private string _methodname;
		private bool _fmmax= false;
		private bool _m= false;
		private bool _m_= false;
		private bool _sm= false;
		private bool _mc= false;
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
		/// 
		/// </summary>
		public string methodName
		{
			set{ _methodname=value;}
			get{return _methodname;}
		}
		/// <summary>
		/// 最大力值
		/// </summary>
		public bool FMmax
		{
			set{ _fmmax=value;}
			get{return _fmmax;}
		}
		/// <summary>
		/// 抗扭力矩
		/// </summary>
		public bool M
		{
			set{ _m=value;}
			get{return _m;}
		}
		/// <summary>
		/// 10个试样抗扭力矩平均值
		/// </summary>
		public bool M_
		{
			set{ _m_=value;}
			get{return _m_;}
		}
		/// <summary>
		/// 10个试样抗扭力矩标准偏差
		/// </summary>
		public bool SM
		{
			set{ _sm=value;}
			get{return _sm;}
		}
		/// <summary>
		/// 抗扭力矩特征值
		/// </summary>
		public bool Mc
		{
			set{ _mc=value;}
			get{return _mc;}
		}
		/// <summary>
		/// 保存曲线
		/// </summary>
		public bool saveCurve
		{
			set{ _savecurve=value;}
			get{return _savecurve;}
		}
		#endregion Model

	}
}

