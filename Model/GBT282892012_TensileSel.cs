using System;
namespace HR_Test.Model
{
	/// <summary>
	/// GBT282892012_TensileSel:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GBT282892012_TensileSel
	{
		public GBT282892012_TensileSel()
		{}
		#region Model
		private int _id;
		private string _methodname;
		private bool _fqmax= false;
		private bool _q= false;
		private bool _q_= false;
		private bool _sq= false;
		private bool _qc= false;
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
		public bool FQmax
		{
			set{ _fqmax=value;}
			get{return _fqmax;}
		}
		/// <summary>
		/// 单位长度拉伸力
		/// </summary>
		public bool Q
		{
			set{ _q=value;}
			get{return _q;}
		}
		/// <summary>
		/// 10个试样单位长度上承受的最大拉伸力平均值
		/// </summary>
		public bool Q_
		{
			set{ _q_=value;}
			get{return _q_;}
		}
		/// <summary>
		/// 标准偏差
		/// </summary>
		public bool SQ
		{
			set{ _sq=value;}
			get{return _sq;}
		}
		/// <summary>
		/// 横向拉伸特征值
		/// </summary>
		public bool Qc
		{
			set{ _qc=value;}
			get{return _qc;}
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

