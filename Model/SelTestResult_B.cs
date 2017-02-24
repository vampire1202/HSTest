using System;
namespace HR_Test.Model
{
    /// <summary>
    /// SelTestResult_B:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class SelTestResult_B
    {
        public SelTestResult_B()
        { }
        #region Model
        private int _id;
        private string _methodname;
        private bool _f_bb = false;
        private bool _f_n = false;
        private bool _f_n1 = false;
        private bool _f_rb = false;
        private bool _y = false;
        private bool _fo = false;
        private bool _fpb = false;
        private bool _frb = false;
        private bool _fbb = false;
        private bool _fn = false;
        private bool _fn1 = false;
        private bool _z = false;
        private bool _s = false;
        private bool _w = false;
        private bool _i = false;
        private bool _eb = false;
        private bool _σpb = false;
        private bool _σrb = false;
        private bool _σbb = false;
        private bool _u = false;     
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
        /// 断裂扰度
        /// </summary>
        public bool f_bb
        {
            set { _f_bb = value; }
            get { return _f_bb; }
        }
        /// <summary>
        /// 最后一次施力并将其卸除后的残余扰度
        /// </summary>
        public bool f_n
        {
            set { _f_n = value; }
            get { return _f_n; }
        }
        /// <summary>
        /// 最后前一次施力并将其卸除后的残余扰度
        /// </summary>
        public bool f_n1
        {
            set { _f_n1 = value; }
            get { return _f_n1; }
        }
        /// <summary>
        /// 达到规定残余弯曲应变时的残余扰度
        /// </summary>
        public bool f_rb
        {
            set { _f_rb = value; }
            get { return _f_rb; }
        }
        /// <summary>
        /// 试样弯曲时中性面至弯曲外表面的最大距离
        /// </summary>
        public bool y
        {
            set { _y = value; }
            get { return _y; }
        }
        /// <summary>
        /// 预弯曲力
        /// </summary>
        public bool Fo
        {
            set { _fo = value; }
            get { return _fo; }
        }
        /// <summary>
        /// 规定非比例弯曲力
        /// </summary>
        public bool Fpb
        {
            set { _fpb = value; }
            get { return _fpb; }
        }
        /// <summary>
        /// 规定残余弯曲力
        /// </summary>
        public bool Frb
        {
            set { _frb = value; }
            get { return _frb; }
        }
        /// <summary>
        /// 最大弯曲力
        /// </summary>
        public bool Fbb
        {
            set { _fbb = value; }
            get { return _fbb; }
        }
        /// <summary>
        /// 最后一次施加的弯曲力
        /// </summary>
        public bool Fn
        {
            set { _fn = value; }
            get { return _fn; }
        }
        /// <summary>
        /// 最后前一次施加的弯曲力
        /// </summary>
        public bool Fn1
        {
            set { _fn1 = value; }
            get { return _fn1; }
        }
        /// <summary>
        /// 力轴每毫米代表的力值
        /// </summary>
        public bool Z
        {
            set { _z = value; }
            get { return _z; }
        }
        /// <summary>
        /// 弯曲试验曲线下包围的面积
        /// </summary>
        public bool S
        {
            set { _s = value; }
            get { return _s; }
        }
        /// <summary>
        /// 试样截面系数
        /// </summary>
        public bool W
        {
            set { _w = value; }
            get { return _w; }
        }
        /// <summary>
        /// 试样截面惯性矩
        /// </summary>
        public bool I
        {
            set { _i = value; }
            get { return _i; }
        }
        /// <summary>
        /// 弯曲弹性模量
        /// </summary>
        public bool Eb
        {
            set { _eb = value; }
            get { return _eb; }
        }
        /// <summary>
        /// 规定非比例弯曲应力
        /// </summary>
        public bool σpb
        {
            set { _σpb = value; }
            get { return _σpb; }
        }
        /// <summary>
        /// 规定残余弯曲应力
        /// </summary>
        public bool σrb
        {
            set { _σrb = value; }
            get { return _σrb; }
        }
        /// <summary>
        /// 抗弯强度
        /// </summary>
        public bool σbb
        {
            set { _σbb = value; }
            get { return _σbb; }
        }
        /// <summary>
        /// 弯曲断裂能量
        /// </summary>
        public bool U
        {
            set { _u = value; }
            get { return _u; }
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

