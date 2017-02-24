using System;
namespace HR_Test.Model
{
    /// <summary>
    /// Standard:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Standard
    {
        public Standard()
        { }
        #region Model
        private int _id;
        private string _standardno;
        private string _standardname;
        private string _testtype;
        private string _resulttablename;
        private string _seltablename;
        private string _methodtablename;
        private string _sign;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 标准号
        /// </summary>
        public string standardNo
        {
            set { _standardno = value; }
            get { return _standardno; }
        }
        /// <summary>
        /// 标准名称
        /// </summary>
        public string standardName
        {
            set { _standardname = value; }
            get { return _standardname; }
        }
        /// <summary>
        /// 试验类型
        /// </summary>
        public string testType
        {
            set { _testtype = value; }
            get { return _testtype; }
        }
        /// <summary>
        /// 数据结果表名称
        /// </summary>
        public string resultTableName
        {
            set { _resulttablename = value; }
            get { return _resulttablename; }
        }
        /// <summary>
        /// 结果数据选择表名称
        /// </summary>
        public string selTableName
        {
            set { _seltablename = value; }
            get { return _seltablename; }
        }
        /// <summary>
        /// 试验方法表名称
        /// </summary>
        public string methodTableName
        {
            set { _methodtablename = value; }
            get { return _methodtablename; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string sign
        {
            set { _sign = value; }
            get { return _sign; }
        }
        #endregion Model

    }
}

