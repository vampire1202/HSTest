using System;
namespace HR_Test.Model
{
    /// <summary>
    /// SelTestResult_C:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class SelTestResult_C
    {
        public SelTestResult_C()
        { }
        #region Model
        private int _id;
        private string _methodname;
        private bool _fmc;
        private bool _fpc;
        private bool _ftc;
        private bool _fehc;
        private bool _felc;
        private bool _rmc;
        private bool _rpc;
        private bool _rtc;
        private bool _rehc;
        private bool _relc;
        private bool _ec;
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
        /// 脆性材料最大压缩力;或塑性材料规定应变条件下的压缩力
        /// </summary>
        public bool Fmc
        {
            set { _fmc = value; }
            get { return _fmc; }
        }
        /// <summary>
        /// 规定非比例压缩变形的实际压缩力
        /// </summary>
        public bool Fpc
        {
            set { _fpc = value; }
            get { return _fpc; }
        }
        /// <summary>
        /// 规定总压缩变形的实际压缩力
        /// </summary>
        public bool Ftc
        {
            set { _ftc = value; }
            get { return _ftc; }
        }
        /// <summary>
        /// 屈服时实际上屈服力
        /// </summary>
        public bool FeHc
        {
            set { _fehc = value; }
            get { return _fehc; }
        }
        /// <summary>
        /// 屈服时实际下屈服力
        /// </summary>
        public bool FeLc
        {
            set { _felc = value; }
            get { return _felc; }
        }
        /// <summary>
        /// 脆性材料的抗压强度;或塑性材料规定应变条件下的压缩应力
        /// </summary>
        public bool Rmc
        {
            set { _rmc = value; }
            get { return _rmc; }
        }
        /// <summary>
        /// 规定非比例压缩强度
        /// </summary>
        public bool Rpc
        {
            set { _rpc = value; }
            get { return _rpc; }
        }
        /// <summary>
        /// 规定总压缩强度
        /// </summary>
        public bool Rtc
        {
            set { _rtc = value; }
            get { return _rtc; }
        }
        /// <summary>
        /// 上屈服强度
        /// </summary>
        public bool ReHc
        {
            set { _rehc = value; }
            get { return _rehc; }
        }
        /// <summary>
        /// 下屈服强度
        /// </summary>
        public bool ReLc
        {
            set { _relc = value; }
            get { return _relc; }
        }
        /// <summary>
        /// 弹性模量
        /// </summary>
        public bool Ec
        {
            set { _ec = value; }
            get { return _ec; }
        }
        /// <summary>
        /// 是否计算平均值
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
        /// 是否保存曲线
        /// </summary>
        public bool saveCurve
        {
            set { _savecurve = value; }
            get { return _savecurve; }
        }
        #endregion Model

    }
}

