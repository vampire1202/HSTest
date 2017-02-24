using System;
namespace HR_Test.Model
{
	/// <summary>
	/// StandardResultItemInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class StandardResultItemInfo
	{
		public StandardResultItemInfo()
		{}
		#region Model
		private int _id;
		private string _standardno;
		private string _testtype;
		private string _paramname;
		private string _unit;
		private string _paramdiscrible;
		private bool _ischeck= false;
		private string _sign;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 标准号
		/// </summary>
		public string standardNo
		{
			set{ _standardno=value;}
			get{return _standardno;}
		}
		/// <summary>
		/// 试验类型
		/// </summary>
		public string testType
		{
			set{ _testtype=value;}
			get{return _testtype;}
		}
		/// <summary>
		/// 参数名
		/// </summary>
		public string paramName
		{
			set{ _paramname=value;}
			get{return _paramname;}
		}
		/// <summary>
		/// 单位
		/// </summary>
		public string unit
		{
			set{ _unit=value;}
			get{return _unit;}
		}
		/// <summary>
		/// 参数描述
		/// </summary>
		public string paramDiscrible
		{
			set{ _paramdiscrible=value;}
			get{return _paramdiscrible;}
		}
		/// <summary>
		/// 是否默认选择
		/// </summary>
		public bool isCheck
		{
			set{ _ischeck=value;}
			get{return _ischeck;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string sign
		{
			set{ _sign=value;}
			get{return _sign;}
		}
		#endregion Model

	}
}

