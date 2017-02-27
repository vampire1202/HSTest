using System;
namespace HR_Test.Model
{
    /// <summary>
    /// GBT3354_Sel:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GBT3354_Sel
    {
        public GBT3354_Sel()
        { }
        #region Model
        private int _id;
        private string _methodname;
        private bool _pmax = false;
        private bool _σt = false;
        private bool _et = false;
        private bool _μ12 = false;
        private bool _ε1t = false;
        private bool _mean = false;
        private bool _sd = false;
        private bool _mid = false;
        private bool _cv = false;
        private bool _savecurve = false;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string methodName
        {
            set { _methodname = value; }
            get { return _methodname; }
        }
        /// <summary>
        /// 最大载荷N
        /// </summary>
        public bool Pmax
        {
            set { _pmax = value; }
            get { return _pmax; }
        }
        /// <summary>
        /// 拉伸强度MPa
        /// </summary>
        public bool σt
        {
            set { _σt = value; }
            get { return _σt; }
        }
        /// <summary>
        /// 弹性模量(MPa)
        /// </summary>
        public bool Et
        {
            set { _et = value; }
            get { return _et; }
        }
        /// <summary>
        /// 泊松比
        /// </summary>
        public bool μ12
        {
            set { _μ12 = value; }
            get { return _μ12; }
        }
        /// <summary>
        /// 破坏应变mm/mm = deltalb/l
        /// </summary>
        public bool ε1t
        {
            set { _ε1t = value; }
            get { return _ε1t; }
        }
        /// <summary>
        /// 平均值
        /// </summary>
        public bool Mean
        {
            set { _mean = value; }
            get { return _mean; }
        }
        /// <summary>
        /// 标准偏差
        /// </summary>
        public bool SD
        {
            set { _sd = value; }
            get { return _sd; }
        }
        /// <summary>
        /// 中间值
        /// </summary>
        public bool Mid
        {
            set { _mid = value; }
            get { return _mid; }
        }
        /// <summary>
        /// 变异系数
        /// </summary>
        public bool CV
        {
            set { _cv = value; }
            get { return _cv; }
        }
        /// <summary>
        /// 保存曲线
        /// </summary>
        public bool saveCurve
        {
            set { _savecurve = value; }
            get { return _savecurve; }
        }
        #endregion Model

    }
}

