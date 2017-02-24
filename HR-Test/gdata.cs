using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HR_Test
{
    struct gdata
    {
        /// <summary>
        /// 通道1 力值
        /// </summary>
        private float _f1;
        public float F1
        {
            get { return this._f1; }
            set { this._f1 = value; }
        }
        /// <summary>
        /// 通道2 力值
        /// </summary>
        private float _f2;
        public float F2
        {
            get { return this._f2; }
            set { this._f2 = value; }
        }
        /// <summary>
        /// 通道3 力值
        /// </summary>
        private float _f3;
        public float F3
        {
            get { return this._f3; }
            set { this._f3 = value; }
        }

        /// <summary>
        /// 通道1 位移
        /// </summary>
        private float _d1;
        public float D1
        {
            get { return this._d1; }
            set { this._d1 = value; }
        }
        /// <summary>
        /// 通道2 位移
        /// </summary>
        private float _d2;
        public float D2
        {
            get { return this._d2; }
            set { this._d2 = value; }
        }
        /// <summary>
        /// 通道3 位移
        /// </summary>
        private float _d3;
        public float D3
        {
            get { return this._d3; }
            set { this._d3 = value; }
        }

        /// <summary>
        /// 通道1 变形
        /// </summary>
        private float _bx1;
        public float BX1
        {
            get { return this._bx1; }
            set { this._bx1 = value; }
        }

        /// <summary>
        /// 通道2 变形
        /// </summary>
        private float _bx2;
        public float BX2
        {
            get { return this._bx2; }
            set { this._bx2 = value; }
        }

        /// <summary>
        /// 通道3 变形
        /// </summary>
        private float _bx3;
        public float BX3
        {
            get { return this._bx3; }
            set { this._bx3 = value; }
        }

        /// <summary>
        /// 通道1 应力
        /// </summary>
        private float _yl1;
        public float YL1
        {
            get { return this._yl1; }
            set { this._yl1 = value; }
        }

        /// <summary>
        /// 通道2 应力
        /// </summary>
        private float _yl2;
        public float YL2
        {
            get { return this._yl2; }
            set { this._yl2 = value; }
        }

        /// <summary>
        /// 通道3 应力
        /// </summary>
        private float _yl3;
        public float YL3
        {
            get { return this._yl3; }
            set { this._yl3 = value; }
        }

        /// <summary>
        /// 通道1应变
        /// </summary>
        private float _yb1;
        public float YB1
        {
            get { return this._yb1; }
            set { this._yb1 = value; }
        }

        /// <summary>
        /// 通道2应变
        /// </summary>
        private float _yb2;
        public float YB2
        {
            get { return this._yb2; }
            set { this._yb2 = value; }
        }

        /// <summary>
        /// 通道3应变
        /// </summary>
        private float _yb3;
        public float YB3
        {
            get { return this._yb3; }
            set { this._yb3 = value; }
        }

        /// <summary>
        /// 时间
        /// </summary>
        private float _t;
        public float Ts
        {
            get { return this._t; }
            set { this._t = value; }
        }

    }

    class CalcGData
    {
        //计算斜率的点
        private int[] Get0501k(int minIndex, int maxIndex)
        {
            int[] _tenValue = new int[10];
            int i = 0;
            if (maxIndex > minIndex + 10)
            {
                for (int j = 0; j < 10; j++)
                {
                    _tenValue[j] = minIndex + ((maxIndex - minIndex) / 10) * i;
                    i++;
                }
            }
            return _tenValue;
        } 

        //位移求FP02
        private bool GetFp02Index(List<gdata> List_Data,double m_L0,double m_Ep, int _FRInIndex, out int _FROutIndex, out double _a, out double _k, out int Fr05Index, out int Fr01Index, out double ep02L0)
        {
            double m_FR05=0; 
            double m_LR05=0; 
            double m_FR01=0; 
            double m_LR01=0;
            double Fr = List_Data[_FRInIndex].F1;
            //查找Fr 0.5的点
            //求出Fr05和 Fr01点
            _FROutIndex = 0;
            _a = 0;
            _k = 0;
            Fr05Index = 0;
            Fr01Index = 0;
            ep02L0 = m_L0 * 10 * m_Ep;
            for (int m = 0; m < _FRInIndex; m++)
            {
                if (List_Data[m].F1 >= Fr * 0.6)
                {
                    m_FR05 = List_Data[m].F1;
                    m_LR05 = List_Data[m].D1;
                    Fr05Index = m;
                    break;
                }
            }

            for (int n = 0; n < _FRInIndex; n++)
            {
                if (List_Data[n].F1 >= Fr * 0.3)
                {
                    m_FR01 = List_Data[n].F1;
                    m_LR01 = List_Data[n].D1;
                    Fr01Index = n;
                    break;
                }
            }

            //计算斜率,在 0.5 和 0.1之间取10点

            int[] kdot = Get0501k(Fr01Index, Fr05Index);
            double sumk = 0;

            for (int i = 0; i < kdot.Length - 1; i++)
            {
                double kone = (List_Data[kdot[i + 1]].F1 - List_Data[kdot[i]].F1) / (List_Data[kdot[i + 1]].D1 - List_Data[kdot[i]].D1);
                sumk += kone;
            }
            _k = sumk / (kdot.Length - 1);

            //计算偏移量
            _a = m_LR05 - (m_FR05 / _k);

            //计算出的Li值，注：100为 L0的 0。2%,此处假设为 50mm * 1000 * 0.2%
            //double Li = a + Fr / k + 100; 
            for (int i = 0; i < List_Data.Count; i++)
            {
                double Lii = _a + ep02L0 + List_Data[i].F1 / _k;
                if (Lii <= List_Data[i].D1)
                {
                    _FROutIndex = i;
                    break;
                }
            }

            if (_FRInIndex != 0 && _a != 0 && _k != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //变形求Fp02
        private bool GetFp02IndexOnE(List<gdata> List_Data, double m_L0, double m_Ep, int _FRInIndex, out int _FROutIndex, out double _a, out double _k, out int Fr05Index, out int Fr01Index, out double ep02L0)
        {
            double m_FR05 = 0;
            double m_LR05 = 0;
            double m_FR01 = 0;
            double m_LR01 = 0;
            double Fr = List_Data[_FRInIndex].F1;
            //查找Fr 0.5的点
            //求出Fr05和 Fr01点
            _FROutIndex = 0;
            _a = 0;
            _k = 0;
            Fr05Index = 0;
            Fr01Index = 0;
            ep02L0 = m_L0 * 10 * m_Ep;
            for (int m = 0; m < _FRInIndex; m++)
            {
                if (List_Data[m].F1 >= Fr * 0.6)
                {
                    m_FR05 = List_Data[m].F1;
                    m_LR05 = List_Data[m].BX1;
                    Fr05Index = m;
                    break;
                }
            }

            for (int n = 0; n < _FRInIndex; n++)
            {
                if (List_Data[n].F1 >= Fr * 0.3)
                {
                    m_FR01 = List_Data[n].F1;
                    m_LR01 = List_Data[n].BX1;
                    Fr01Index = n;
                    break;
                }
            }

            //计算斜率,在 0.5 和 0.1之间取10点

            int[] kdot = Get0501k(Fr01Index, Fr05Index);
            double sumk = 0;

            for (int i = 0; i < kdot.Length - 1; i++)
            {
                double kone = (List_Data[kdot[i + 1]].F1 - List_Data[kdot[i]].F1) / (List_Data[kdot[i + 1]].BX1 - List_Data[kdot[i]].BX1);
                sumk += kone;
            }
            _k = sumk / (kdot.Length - 1);

            //计算偏移量
            _a = m_LR05 - (m_FR05 / _k);

            //计算出的Li值，注：100为 L0的 0。2%,此处假设为 50mm * 1000 * 0.2%
            //double Li = a + Fr / k + 100; 
            for (int i = 0; i < List_Data.Count; i++)
            {
                double Lii = _a + ep02L0 + List_Data[i].F1 / _k;
                if (Lii <= List_Data[i].BX1)
                {
                    _FROutIndex = i;
                    break;
                }
            }

            if (_FRInIndex != 0 && _a != 0 && _k != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    
    }
}
