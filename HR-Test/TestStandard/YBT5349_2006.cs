using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using ZedGraph;
namespace HR_Test.TestStandard
{
    class YBT5349_2006
    {
        /// <summary>
        /// 原始曲线数据
        /// </summary>
        private List<gdata> _listData;
        public List<gdata> _ListData
        {
            get { return _listData; }
            set { _listData = value; }
        }

        private static bool _isSelSD;
        public static bool _IsSelSD
        {
            get { return _isSelSD; }
            set { _isSelSD = value; }
        }

        private static bool _isSelCV;
        public static bool _IsSelCV
        {
            get { return _isSelCV; }
            set { _isSelCV = value; }
        }

        private static bool _isSelMid;
        public static bool _IsSelMid
        {
            get { return _isSelMid; }
            set { _isSelMid = value; }
        }

        private static string[] _Color_Array = { "Crimson", "Green", "Blue", "Teal", "DarkOrange", "Chocolate", "BlueViolet", "Indigo", "Magenta", "LightCoral", "LawnGreen", "Aqua", "DarkViolet", "DeepPink", "DeepSkyblue", "HotPink", "SpringGreen", "GreenYellow", "Peru", "Black" };

        //获取 弯曲试验 结果的 DataSource
        public static void readFinishSample(DataGridView dg,DataGridView dataGridViewSum, string testNo,DateTimePicker dtime,ZedGraph.ZedGraphControl zed)
        {
            //try
            //{
                //dg.MultiSelect = true;  
            if (dg != null)
            {
                dg.DataSource = null;
                dg.Columns.Clear();
                dg.RowHeadersVisible = false;
            }
                BLL.Bend bllTs = new HR_Test.BLL.Bend();
                double maxvalue = 0;
                DataSet dsmax = bllTs.GetFbbMax(" testNo='" + testNo + "' and testDate =#" + dtime.Value.Date + "#");
                if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["Fbb"].ToString()))
                {
                    maxvalue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["Fbb"].ToString());
                }
                if (!string.IsNullOrEmpty(testNo))
                {
                    //if (testNo.Contains('-'))
                    //    testNo = testNo.Substring(0, testNo.LastIndexOf("-"));
                    //获取不重复的试验编号列表
                    DataSet ds = bllTs.GetNotOverlapList(" testNo='" + testNo + "' and testDate =#" + dtime.Value.Date + "#");

                    DataSet ds1 = bllTs.GetNotOverlapList(" testNo='" + testNo + "' and testDate =#" + dtime.Value.Date + "#");
                    DataTable dt1 = ds1.Tables[0];
                    string methodName = dt1.Rows[0]["testMethod"].ToString();
                    if (dt1 != null)
                    {
                        StringBuilder[] tst = strSql_B(methodName, maxvalue);
                        if (!string.IsNullOrEmpty(tst[0].ToString()) && dg!=null)
                            dg.DataSource = CreateView_B(tst[0].ToString(), dt1.Rows[0]["testNo"].ToString());

                        if (!string.IsNullOrEmpty(tst[1].ToString()))
                            dataGridViewSum.DataSource = CreateAverageView_B(tst[0], tst[1], dt1.Rows[0]["testNo"].ToString());
                    }
                }
                else
                {
                    DataSet ds = bllTs.GetFinishListDefault(" testNo='" + testNo + "' and testDate=#" + dtime.Value.Date + "#", maxvalue);
                    DataTable _dt = ds.Tables[0];
                    DataRow dr = _dt.NewRow();
                    _dt.Rows.Add(dr);
                    if(dg!=null)
                        dg.DataSource = _dt;
                    ds.Dispose();
                }

                if (dg != null)
                {
                    DataGridViewCheckBoxColumn chkcol = new DataGridViewCheckBoxColumn();
                    chkcol.Name = "选择";
                    chkcol.MinimumWidth = 50;
                    chkcol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
                    c.Name = "";
                    dg.Columns.Insert(0, chkcol);
                    dg.Columns.Insert(1, c);
                    dg.Name = "tensile";
                    //dg.Rows[0].Cells[0].Value = false;
                    //dg.Rows[0].Cells[0].Selected = false;
                    int rCount = dg.Rows.Count;
                    for (int i = 0; i < rCount; i++)
                    {
                        if (!string.IsNullOrEmpty(dg.Rows[i].Cells[2].Value.ToString()))
                        {
                            if (Convert.ToBoolean(dg.Rows[i].Cells[2].Value.ToString()) == true)
                                dg.Rows[i].DefaultCellStyle.BackColor = Color.IndianRed;
                        }
                        if (i > 19)
                        {
                            dg.Rows[i].Cells[1].Style.BackColor = Color.FromName(_Color_Array[i % 20]);
                            dg.Rows[i].Cells[1].Style.ForeColor = Color.FromName(_Color_Array[i % 20]);
                            dg.Rows[i].Cells[1].Style.SelectionForeColor = Color.FromName(_Color_Array[i % 20]);
                            dg.Rows[i].Cells[1].Style.SelectionBackColor = Color.FromName(_Color_Array[i % 20]);
                            dg.Rows[i].Cells[1].Value = _Color_Array[i % 20].ToString();
                        }
                        else
                        {
                            dg.Rows[i].Cells[1].Style.BackColor = Color.FromName(_Color_Array[i]);
                            dg.Rows[i].Cells[1].Style.ForeColor = Color.FromName(_Color_Array[i]);
                            dg.Rows[i].Cells[1].Style.SelectionBackColor = Color.FromName(_Color_Array[i]);
                            dg.Rows[i].Cells[1].Style.SelectionForeColor = Color.FromName(_Color_Array[i]);
                            dg.Rows[i].Cells[1].Value = _Color_Array[i].ToString();
                        }
                    }

                    foreach (DataGridViewColumn dgvc in dg.Columns)
                    {
                        dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    if (dg.Columns.Count > 1)
                    {
                        dg.Columns[0].Frozen = true;
                        dg.Columns[1].Frozen = true;
                        dg.Columns[1].Width = 10;
                    }
                    if (dg.Columns.Count > 2)
                        dg.Columns[2].Frozen = true;
                    dg.Refresh();
                }

                if (zed != null)
                {
                    //clear all curves
                    foreach (CurveItem ci in zed.GraphPane.CurveList)
                    {
                        ci.Clear();
                        ci.Label.Text = "";
                    }

                    zed.AxisChange();
                    zed.Refresh();
                }
                dsmax.Dispose(); 
            //}
            //catch (Exception ee) { MessageBox.Show(this, ee.ToString(), "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        //列表
        public static DataTable CreateView_B(string _selCol, string _strTestNo)
        {
            BLL.Bend bllts = new HR_Test.BLL.Bend();
            string s = _selCol;
            DataSet dst = bllts.GetFinishList1(_selCol, " testNo = '" + _strTestNo + "' and isFinish=true ");
            return dst.Tables[0];
        }

        public static StringBuilder[] strSql_B(string methodName, double maxValue)
        {
            BLL.SelTestResult_B bllSel = new HR_Test.BLL.SelTestResult_B();
            Model.SelTestResult_B mSel = bllSel.GetModel(methodName);
            StringBuilder[] strSql_b = new StringBuilder[2];
            //strSql.Append("methodName,f_bb,f_n,f_n1,f_rb,y,Fo,Fpb,Frb,Fbb,Fn,Fn1,Z,S,W,I,Eb,σpb,σrb,σbb,U,Mean,SD,Mid,CV,saveCurve)");
            //所选择的试验结果列
            StringBuilder strSelcol = new StringBuilder();
            //平均值
            StringBuilder strSelcolAver = new StringBuilder();
            //生成"平均值"的 查询语句 
            if (mSel != null)
            {
                //断裂挠度
                if (mSel.f_bb == true) { strSelcol.Append(" [f_bb] as [fbb(mm)],"); strSelcolAver.Append(" round(Avg([f_bb]),2) as [fbb(mm)],"); }
                if (mSel.f_n == true) { strSelcol.Append(" [fn] as [fn(mm)],"); strSelcolAver.Append(" round(Avg([fn]),2) as [fn(mm)],"); }
                if (mSel.f_n1 == true) { strSelcol.Append(" [f_n1] as [fn1(mm)],"); strSelcolAver.Append(" round(Avg([f_n1]),2) as [fn1(mm)],"); }
                if (mSel.f_rb == true) { strSelcol.Append(" [f_rb] as [frb(mm)],"); strSelcolAver.Append(" round(Avg([f_rb]),2) as [frb(mm)],"); }
                if (mSel.y == true) { strSelcol.Append(" [y] as [y(mm)],"); strSelcolAver.Append(" round(Avg([y]),2) as [y(mm)],"); }
                if (mSel.Fo == true) { strSelcol.Append(" [Fo] as [Fo(N)],"); strSelcolAver.Append(" round(Avg([Fo]),2) as [Fo(N)],"); }
                int dotValue = utils.Dotvalue(maxValue);
                if (maxValue < 1000.0)
                {
                    switch (dotValue)
                    {
                        case 2:
                            if (mSel.Fbb == true)
                            {
                                strSelcol.Append(" FORMAT([Fbb],'0.00') as [Fbb(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fbb]),'0.00') as [Fbb(N)],");
                            }
                            if (mSel.Fpb == true)
                            {
                                strSelcol.Append(" FORMAT([Fpb],'0.00') as [Fpb(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpb]),'0.00') as [Fpb(N)],");
                            }
                            if (mSel.Frb == true)
                            {
                                strSelcol.Append(" FORMAT([Frb],'0.00') as [Frb(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Frb]),'0.00') as [Frb(N)],");
                            }
                            break;
                        case 3:
                            if (mSel.Fbb == true)
                            {
                                strSelcol.Append(" FORMAT([Fbb],'0.000') as [Fbb(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fbb]),'0.000') as [Fbb(N)],");
                            }
                            if (mSel.Fpb == true)
                            {
                                strSelcol.Append(" FORMAT([Fpb],'0.000') as [Fpb(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpb]),'0.000') as [Fpb(N)],");
                            }
                            if (mSel.Frb == true)
                            {
                                strSelcol.Append(" FORMAT([Frb],'0.000') as [Frb(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Frb]),'0.000') as [Frb(N)],");
                            }
                            break;
                        case 4:
                            if (mSel.Fbb == true)
                            {
                                strSelcol.Append(" FORMAT([Fbb],'0.0000') as [Fbb(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fbb]),'0.0000') as [Fbb(N)],");
                            }
                            if (mSel.Fpb == true)
                            {
                                strSelcol.Append(" FORMAT([Fpb],'0.0000') as [Fpb(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpb]),'0.0000') as [Fpb(N)],");
                            }
                            if (mSel.Frb == true)
                            {
                                strSelcol.Append(" FORMAT([Frb],'0.0000') as [Frb(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Frb]),'0.0000') as [Frb(N)],");
                            }
                            break;
                        case 5:
                            if (mSel.Fbb == true)
                            {
                                strSelcol.Append(" FORMAT([Fbb],'0.00000') as [Fbb(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fbb]),'0.00000') as [Fbb(N)],");
                            }
                            if (mSel.Fpb == true)
                            {
                                strSelcol.Append(" FORMAT([Fpb],'0.00000') as [Fpb(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpb]),'0.00000') as [Fpb(N)],");
                            }
                            if (mSel.Frb == true)
                            {
                                strSelcol.Append(" FORMAT([Frb],'0.00000') as [Frb(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Frb]),'0.00000') as [Frb(N)],");
                            }
                            break;
                        default:
                            if (mSel.Fbb == true)
                            {
                                strSelcol.Append(" FORMAT([Fbb],'0.00') as [Fbb(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fbb]),'0.00') as [Fbb(N)],");
                            }
                            if (mSel.Fpb == true)
                            {
                                strSelcol.Append(" FORMAT([Fpb],'0.00') as [Fpb(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpb]),'0.00') as [Fpb(N)],");
                            }
                            if (mSel.Frb == true)
                            {
                                strSelcol.Append(" FORMAT([Frb],'0.00') as [Frb(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Frb]),'0.00') as [Frb(N)],");
                            }
                            break;
                    }
                }

                if (maxValue >= 1000.0)
                {
                    switch(dotValue)
                    {
                        case 2:
                            if (mSel.Fbb == true)
                            {
                                strSelcol.Append(" FORMAT([Fbb]/1000.0,'0.00') as [Fbb(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fbb]/1000.0),'0.00') as [Fbb(kN)],");
                            }
                            if (mSel.Fpb == true)
                            {
                                strSelcol.Append(" FORMAT([Fpb]/1000.0,'0.00') as [Fpb(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpb]/1000.0),'0.00') as [Fpb(kN)],");
                            }
                            if (mSel.Frb == true)
                            {
                                strSelcol.Append(" FORMAT([Frb]/1000.0,'0.00') as [Frb(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Frb]/1000.0),'0.00') as [Frb(kN)],");
                            }
                            break;
                        case 3:
                            if (mSel.Fbb == true)
                            {
                                strSelcol.Append(" FORMAT([Fbb]/1000.0,'0.000') as [Fbb(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fbb]/1000.0),'0.000') as [Fbb(kN)],");
                            }
                            if (mSel.Fpb == true)
                            {
                                strSelcol.Append(" FORMAT([Fpb]/1000.0,'0.000') as [Fpb(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpb]/1000.0),'0.000') as [Fpb(kN)],");
                            }
                            if (mSel.Frb == true)
                            {
                                strSelcol.Append(" FORMAT([Frb]/1000.0,'0.000') as [Frb(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Frb]/1000.0),'0.000') as [Frb(kN)],");
                            }
                            break;
                        case 4:
                            if (mSel.Fbb == true)
                            {
                                strSelcol.Append(" FORMAT([Fbb]/1000.0,'0.0000') as [Fbb(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fbb]/1000.0),'0.0000') as [Fbb(kN)],");
                            }
                            if (mSel.Fpb == true)
                            {
                                strSelcol.Append(" FORMAT([Fpb]/1000.0,'0.0000') as [Fpb(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpb]/1000.0),'0.0000') as [Fpb(kN)],");
                            }
                            if (mSel.Frb == true)
                            {
                                strSelcol.Append(" FORMAT([Frb]/1000.0,'0.0000') as [Frb(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Frb]/1000.0),'0.0000') as [Frb(kN)],");
                            }
                            break;
                        case 5:
                            if (mSel.Fbb == true)
                            {
                                strSelcol.Append(" FORMAT([Fbb]/1000.0,'0.00000') as [Fbb(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fbb]/1000.0),'0.00000') as [Fbb(kN)],");
                            }
                            if (mSel.Fpb == true)
                            {
                                strSelcol.Append(" FORMAT([Fpb]/1000.0,'0.00000') as [Fpb(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpb]/1000.0),'0.00000') as [Fpb(kN)],");
                            }
                            if (mSel.Frb == true)
                            {
                                strSelcol.Append(" FORMAT([Frb]/1000.0,'0.00000') as [Frb(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Frb]/1000.0),'0.00000') as [Frb(kN)],");
                            }
                            break;
                        default:
                            if (mSel.Fbb == true)
                            {
                                strSelcol.Append(" FORMAT([Fbb]/1000.0,'0.00') as [Fbb(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fbb]/1000.0),'0.00') as [Fbb(kN)],");
                            }
                            if (mSel.Fpb == true)
                            {
                                strSelcol.Append(" FORMAT([Fpb]/1000.0,'0.00') as [Fpb(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpb]/1000.0),'0.00') as [Fpb(kN)],");
                            }
                            if (mSel.Frb == true)
                            {
                                strSelcol.Append(" FORMAT([Frb]/1000.0,'0.00') as [Frb(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Frb]/1000.0),'0.00') as [Frb(kN)],");
                            }
                            break;
                    }
                }

                if (mSel.Fn == true) { strSelcol.Append(" round([Fn],2) as [Fn(kN)],"); strSelcolAver.Append(" round(Avg([Fn]),2) as [Fn(kN)],"); }
                if (mSel.Fn1 == true) { strSelcol.Append(" round([Fn1],2) as [Fn1(kN)],"); strSelcolAver.Append(" round(Avg([Fn1]),2) as [Fn1(kN)],"); }
                if (mSel.Z == true) { strSelcol.Append(" [Z] as [Z],"); strSelcolAver.Append(" round(Avg([Z]),2) as [Z],"); }
                if (mSel.S == true) { strSelcol.Append(" [S] as [S],"); strSelcolAver.Append(" round(Avg([S]),2) as [S],"); }
                if (mSel.W == true) { strSelcol.Append(" [W] as [W],"); strSelcolAver.Append(" round(Avg([W]),2) as [W],"); }
                if (mSel.I == true) { strSelcol.Append(" [I] as [I],"); strSelcolAver.Append(" round(Avg([I]),2) as [I],"); }
                if (mSel.Eb == true) { strSelcol.Append(" round([Eb],2) as [Eb(MPa)],"); strSelcolAver.Append(" round(Avg([Eb]),2) as [Eb(MPa)],"); }
                if (mSel.σpb == true) { strSelcol.Append(" round([σpb],2) as [σpb(MPa)],"); strSelcolAver.Append(" round(Avg([σpb]),2) as [σpb(MPa)],"); }
                if (mSel.σrb == true) { strSelcol.Append(" round([σrb],2) as [σrb(MPa)],"); strSelcolAver.Append(" round(Avg([σrb]),2) as [σrb(MPa)],"); }
                if (mSel.σbb == true) { strSelcol.Append(" round([σbb],2) as [σbb(MPa)],"); strSelcolAver.Append(" round(Avg([σbb]),2) as [σbb(MPa)],"); }
                if (mSel.U == true) { strSelcol.Append(" [U] as [U(J)],"); strSelcolAver.Append(" round(Avg([U]),2) as [U(J)],"); }

                _isSelSD = mSel.SD;
                _isSelMid = mSel.Mid;
                _isSelCV = mSel.CV;
            }

            if (strSelcolAver.Length > 0)
                strSelcolAver = strSelcolAver.Remove(strSelcolAver.Length - 1, 1);

            if (strSelcol.Length > 0)
                strSelcol = strSelcol.Remove(strSelcol.Length - 1, 1);

            strSql_b[0] = strSelcol;
            strSql_b[1] = strSelcolAver;//
            //这里应该修改为 C.V.变异系数的计算结果
            //strSql_t[2] = strSelcolSD;
            return strSql_b;

        }

        //求取平均值 X~,S,V
        private static DataTable CreateAverageView_B(StringBuilder _selCol, StringBuilder _selSqlAver, string _strTestNo)
        {
            BLL.Bend bllts = new HR_Test.BLL.Bend();
            //求平均
            DataSet dsaver = bllts.GetFinishSumList1(_selSqlAver.ToString(), " testNo = '" + _strTestNo + "' and isFinish=true and isEffective=false ");

            //获取选择试样的数量和试样编号
            string _sel = _selCol.ToString();

            DataSet dsfinish = bllts.GetFinishListReport(_sel, " testNo = '" + _strTestNo + "' and isFinish=true and isEffective=false ");

            int fcount = dsfinish.Tables[0].Rows.Count;

            //变异系数 = sd/mean 
            StringBuilder strCV = new StringBuilder();

            //中间值
            StringBuilder strMid = new StringBuilder();

            ////添加新列
            //DataColumn dc = new DataColumn();
            //dc = dsaver.Tables[0].Columns.Add("数量(" + fcount + ")", System.Type.GetType("System.String"));

            ////设置为第0列
            //dsaver.Tables[0].Columns["数量(" + fcount + ")"].SetOrdinal(0);

            ////平均值已经有一行，不用添加，直接修改其显示名称
            //dsaver.Tables[0].Rows[0][0] = "Mean:";
            ////S.D.标准偏差行
            //if (_isSelSD) { dsaver.Tables[0].Rows.Add("S.D.:"); }
            ////变异系数行
            //if (_isSelCV) { dsaver.Tables[0].Rows.Add("C.V.:"); }
            ////中间值行
            //if (_isSelMid) { dsaver.Tables[0].Rows.Add("Mid.:"); }
            //创建新的数据计算结果表
            DataSet dsaverto = new DataSet();
            dsaverto.Tables.Add();
            dsaverto.Tables[0].Columns.Add("数量(" + fcount + ")").SetOrdinal(0);
            for (int i = 0; i < dsaver.Tables[0].Columns.Count; i++)
            {
                dsaverto.Tables[0].Columns.Add(dsaver.Tables[0].Columns[i].Caption).SetOrdinal(i + 1);
            }
            //给新数据表添加平均值列
            List<object> objmean = new List<object>();
            objmean.Add("Mean.:");
            for (int i = 0; i < dsaver.Tables[0].Columns.Count; i++)
            {
                if (!string.IsNullOrEmpty(dsaver.Tables[0].Rows[0][i].ToString()))
                {
                    if(double.Parse(dsaver.Tables[0].Rows[0][i].ToString())!=0)
                        objmean.Add(dsaver.Tables[0].Rows[0][i].ToString()); 
                    else
                        objmean.Add("-");
                }
                else
                    objmean.Add("-");
            }
            dsaverto.Tables[0].Rows.Add(objmean.ToArray());

            double sumsd = 0;
            double sd = 0;
            double cv = 0;
            double ctrue = 0;
            double caver = 0;
            double temp = double.MaxValue;
            double mid = 0;

            List<object> objsd = new List<object>();
            objsd.Add("S.D.:");
            List<object> objcv = new List<object>();
            objcv.Add("C.V.:");
            List<object> objmid = new List<object>();
            objmid.Add("Mid.:");  

            //计算标准偏差
            int averColCount = dsaver.Tables[0].Columns.Count;
            int finishRowsCount = dsfinish.Tables[0].Rows.Count;
            for (int i = 0; i < averColCount; i++)
            {
                for (int j = 0; j < finishRowsCount; j++)
                {
                    //  求和 (真实值-平均值)^2 
                    string strtrue = dsfinish.Tables[0].Rows[j][i +2].ToString();
                    string straver = dsaver.Tables[0].Rows[0][i].ToString();
                    ctrue = Convert.ToDouble(strtrue);
                    caver = Convert.ToDouble(straver);
                    double c1 = (ctrue - caver) * (ctrue - caver);
                    sumsd += c1;
                    //查找中间值，最接近平均值的                   
                    double d_value = Math.Abs(ctrue - caver);
                    if (d_value < temp)
                    {
                        temp = d_value;
                        mid = ctrue;
                    }
                }

                //标准偏差
                if (fcount - 1 != 0)
                    sd = Math.Sqrt(sumsd / (fcount - 1));
                else
                    sd = 0;

                //变异系数 =( 标准偏差 / 平均值 ) *100
                if (caver != 0)
                    cv = (sd / caver) * 100;
                else
                    cv = 0;

                if (sd != 0)
                    objsd.Add(sd.ToString("f4"));
                else
                    objsd.Add("-");
                if (cv != 0)
                    objcv.Add(cv.ToString("f4"));
                else
                    objcv.Add("-");
                if (mid != 0)
                    objmid.Add(mid.ToString("f4"));
                else
                    objmid.Add("-");
             
                temp = double.MaxValue;
                sumsd = 0;
            }
            //S.D.标准偏差行
            if (_isSelSD) { dsaverto.Tables[0].Rows.Add(objsd.ToArray()); }
            //变异系数行
            if (_isSelCV) { dsaverto.Tables[0].Rows.Add(objcv.ToArray()); }
            //中间值行
            if (_isSelMid) { dsaverto.Tables[0].Rows.Add(objmid.ToArray()); }

            dsfinish.Dispose();
            return dsaverto.Tables[0];
        }

        //求取平均值 X~,S,V
        public static DataTable CreateAverageViewReport(StringBuilder _selCol, StringBuilder _selSqlAver, string _strTestNo)
        {
            BLL.Bend bllts = new HR_Test.BLL.Bend();
            //求平均
            DataSet dsaver = bllts.GetFinishSumList1(_selSqlAver.ToString(), " testSampleNo in ('" + _strTestNo + ") and isFinish=true and isEffective=false ");

            //获取选择试样的数量和试样编号
            string _sel = _selCol.ToString();

            DataSet dsfinish = bllts.GetFinishListReport(_sel, " testSampleNo in ('" + _strTestNo + ") and isFinish=true and isEffective=false ");

            int fcount = dsfinish.Tables[0].Rows.Count;

            //变异系数 = sd/mean 
            StringBuilder strCV = new StringBuilder();

            //中间值
            StringBuilder strMid = new StringBuilder();

            ////添加新列
            //DataColumn dc = new DataColumn();
            //dc = dsaver.Tables[0].Columns.Add("数量(" + fcount + ")", System.Type.GetType("System.String"));

            ////设置为第0列
            //dsaver.Tables[0].Columns["数量(" + fcount + ")"].SetOrdinal(0);

            ////平均值已经有一行，不用添加，直接修改其显示名称
            //dsaver.Tables[0].Rows[0][0] = "Mean:";
            ////S.D.标准偏差行
            //if (_isSelSD) { dsaver.Tables[0].Rows.Add("S.D.:"); }
            ////变异系数行
            //if (_isSelCV) { dsaver.Tables[0].Rows.Add("C.V.:"); }
            ////中间值行
            //if (_isSelMid) { dsaver.Tables[0].Rows.Add("Mid.:"); }
            //创建新的数据计算结果表
            DataSet dsaverto = new DataSet();
            dsaverto.Tables.Add();
            dsaverto.Tables[0].Columns.Add("数量(" + fcount + ")").SetOrdinal(0);
            for (int i = 0; i < dsaver.Tables[0].Columns.Count; i++)
            {
                dsaverto.Tables[0].Columns.Add(dsaver.Tables[0].Columns[i].Caption).SetOrdinal(i + 1);
            }
            //给新数据表添加平均值列
            List<object> objmean = new List<object>();
            objmean.Add("Mean.:");
            for (int i = 0; i < dsaver.Tables[0].Columns.Count; i++)
            {
                if (!string.IsNullOrEmpty(dsaver.Tables[0].Rows[0][i].ToString()))
                {
                    if (double.Parse(dsaver.Tables[0].Rows[0][i].ToString()) != 0)
                        objmean.Add(dsaver.Tables[0].Rows[0][i].ToString());
                    else
                        objmean.Add("-");
                }
                else
                    objmean.Add("-");
            }
            dsaverto.Tables[0].Rows.Add(objmean.ToArray());

            double sumsd = 0;
            double sd = 0;
            double cv = 0;
            double ctrue = 0;
            double caver = 0;
            double temp = double.MaxValue;
            double mid = 0;

            List<object> objsd = new List<object>();
            objsd.Add("S.D.:");
            List<object> objcv = new List<object>();
            objcv.Add("C.V.:");
            List<object> objmid = new List<object>();
            objmid.Add("Mid.:");

            //计算标准偏差
            int averColCount = dsaver.Tables[0].Columns.Count;
            int finishRowsCount = dsfinish.Tables[0].Rows.Count;
            for (int i = 0; i < averColCount; i++)
            {
                for (int j = 0; j < finishRowsCount; j++)
                {
                    //  求和 (真实值-平均值)^2 
                    string strtrue = dsfinish.Tables[0].Rows[j][i + 2].ToString();
                    string straver = dsaver.Tables[0].Rows[0][i].ToString();
                    ctrue = Convert.ToDouble(strtrue);
                    caver = Convert.ToDouble(straver);
                    double c1 = (ctrue - caver) * (ctrue - caver);
                    sumsd += c1;
                    //查找中间值，最接近平均值的                   
                    double d_value = Math.Abs(ctrue - caver);
                    if (d_value < temp)
                    {
                        temp = d_value;
                        mid = ctrue;
                    }
                }

                //标准偏差
                if (fcount - 1 != 0)
                    sd = Math.Sqrt(sumsd / (fcount - 1));
                else
                    sd = 0;

                //变异系数 =( 标准偏差 / 平均值 ) *100
                if (caver != 0)
                    cv = (sd / caver) * 100;
                else
                    cv = 0;

                if (sd != 0)
                    objsd.Add(sd.ToString("f4"));
                else
                    objsd.Add("-");
                if (cv != 0)
                    objcv.Add(cv.ToString("f4"));
                else
                    objcv.Add("-");
                if (mid != 0)
                    objmid.Add(mid.ToString("f4"));
                else
                    objmid.Add("-");

                temp = double.MaxValue;
                sumsd = 0;
            }
            //S.D.标准偏差行
            if (_isSelSD) { dsaverto.Tables[0].Rows.Add(objsd.ToArray()); }
            //变异系数行
            if (_isSelCV) { dsaverto.Tables[0].Rows.Add(objcv.ToArray()); }
            //中间值行
            if (_isSelMid) { dsaverto.Tables[0].Rows.Add(objmid.ToArray()); }

            dsfinish.Dispose();
            return dsaverto.Tables[0];
        }

        //列表
        public static DataTable CreateViewReport(string _selCol, string _strTestNo)
        {
            BLL.Bend bllts = new HR_Test.BLL.Bend();
            DataSet dst = bllts.GetFinishListReport(_selCol, " testSampleNo in ('" + _strTestNo + ") and isFinish=true ");
            return dst.Tables[0];
        } 

        //public static StringBuilder[] strSql_B(string methodName, double maxValue)
        //{
        //    BLL.SelTestResult_B bllSel = new HR_Test.BLL.SelTestResult_B();
        //    Model.SelTestResult_B mSel = bllSel.GetModel(methodName);
        //    StringBuilder[] strSql_b = new StringBuilder[2];
        //    //strSql.Append("methodName,f_bb,f_n,f_n1,f_rb,y,Fo,Fpb,Frb,Fbb,Fn,Fn1,Z,S,W,I,Eb,σpb,σrb,σbb,U,Mean,SD,Mid,CV,saveCurve)");
        //    //所选择的试验结果列
        //    StringBuilder strSelcol = new StringBuilder();
        //    //平均值
        //    StringBuilder strSelcolAver = new StringBuilder();
        //    //生成"平均值"的 查询语句 
        //    if (mSel != null)
        //    {
        //        //断裂挠度
        //        if (mSel.f_bb == true) { strSelcol.Append(" [f_bb] as [fbb(mm)],"); strSelcolAver.Append(" round(Avg([f_bb]),2) as [fbb(mm)],"); }
        //        if (mSel.f_n == true) { strSelcol.Append(" [fn] as [fn(mm)],"); strSelcolAver.Append(" round(Avg([fn]),2) as [fn(mm)],"); }
        //        if (mSel.f_n1 == true) { strSelcol.Append(" [f_n1] as [fn1(mm)],"); strSelcolAver.Append(" round(Avg([f_n1]),2) as [fn1(mm)],"); }
        //        if (mSel.f_rb == true) { strSelcol.Append(" [f_rb] as [frb(mm)],"); strSelcolAver.Append(" round(Avg([f_rb]),2) as [frb(mm)],"); }
        //        if (mSel.y == true) { strSelcol.Append(" [y] as [y(mm)],"); strSelcolAver.Append(" round(Avg([y]),2) as [y(mm)],"); }
        //        if (mSel.Fo == true) { strSelcol.Append(" [Fo] as [Fo(N)],"); strSelcolAver.Append(" round(Avg([Fo]),2) as [Fo(N)],"); }
        //       // if (mSel.Fpb == true) { strSelcol.Append(" [Fpb] as [Fpb(kN)],"); strSelcolAver.Append(" round(Avg([Fpb]),2) as [Fpb(kN)],"); }
        //       // if (mSel.Frb == true) { strSelcol.Append(" [Frb] as [Frb(kN)],"); strSelcolAver.Append(" round(Avg([Frb]),2) as [Frb(kN)],"); }
        //        if (maxValue < 10000.0)
        //        {
        //            if (mSel.Fbb == true)
        //            {
        //                strSelcol.Append(" FORMAT([Fbb],'0.00') as [Fbb(N)],");
        //                strSelcolAver.Append(" FORMAT(Avg([Fbb]),'0.00') as [Fbb(N)],");
        //            }
        //            if (mSel.Fpb == true)
        //            {
        //                strSelcol.Append(" FORMAT([Fpb],'0.00') as [Fpb(N)],");
        //                strSelcolAver.Append(" FORMAT(Avg([Fpb]),'0.00') as [Fpb(N)],");
        //            }
        //            if (mSel.Frb == true)
        //            {
        //                strSelcol.Append(" FORMAT([Frb],'0.00') as [Frb(N)],");
        //                strSelcolAver.Append(" FORMAT(Avg([Frb]),'0.00') as [Frb(N)],");
        //            }
        //        }

        //        if (maxValue > 10000.0 && maxValue < 100000.0)
        //        {
        //            if (mSel.Fbb == true)
        //            {
        //                strSelcol.Append(" FORMAT([Fbb]/1000.0,'0.0000') as [Fbb(kN)],");
        //                strSelcolAver.Append(" FORMAT(Avg([Fbb]/1000.0),'0.0000') as [Fbb(kN)],");
        //            }
        //            if (mSel.Fpb == true)
        //            {
        //                strSelcol.Append(" FORMAT([Fpb]/1000.0,'0.0000') as [Fpb(kN)],");
        //                strSelcolAver.Append(" FORMAT(Avg([Fpb]/1000.0),'0.0000') as [Fpb(kN)],");
        //            }
        //            if (mSel.Frb == true)
        //            {
        //                strSelcol.Append(" FORMAT([Frb]/1000.0,'0.0000') as [Frb(kN)],");
        //                strSelcolAver.Append(" FORMAT(Avg([Frb]/1000.0),'0.0000') as [Frb(kN)],");
        //            }
        //        }

        //        if (maxValue > 100000.0)
        //        {
        //            if (mSel.Fbb == true)
        //            {
        //                strSelcol.Append(" FORMAT([Fbb]/1000.0,'0.000') as [Fbb(kN)],");
        //                strSelcolAver.Append(" FORMAT(Avg([Fbb]/1000.0),'0.000') as [Fbb(kN)],");
        //            }
        //            if (mSel.Fpb == true)
        //            {
        //                strSelcol.Append(" FORMAT([Fpb]/1000.0,'0.000') as [Fpb(kN)],");
        //                strSelcolAver.Append(" FORMAT(Avg([Fpb]/1000.0),'0.000') as [Fpb(kN)],");
        //            }
        //            if (mSel.Frb == true)
        //            {
        //                strSelcol.Append(" FORMAT([Frb]/1000.0,'0.000') as [Frb(kN)],");
        //                strSelcolAver.Append(" FORMAT(Avg([Frb]/1000.0),'0.000') as [Frb(kN)],");
        //            }
        //        }

        //        if (mSel.Fn == true) { strSelcol.Append(" [Fn] as [Fn(kN)],"); strSelcolAver.Append(" round(Avg([Fn]),2) as [Fn(kN)],"); }
        //        if (mSel.Fn1 == true) { strSelcol.Append(" [Fn1] as [Fn1(kN)],"); strSelcolAver.Append(" round(Avg([Fn1]),2) as [Fn1(kN)],"); }
        //        if (mSel.Z == true) { strSelcol.Append(" [Z] as [Z],"); strSelcolAver.Append(" round(Avg([Z]),2) as [Z],"); }
        //        if (mSel.S == true) { strSelcol.Append(" [S] as [S],"); strSelcolAver.Append(" round(Avg([S]),2) as [S],"); }
        //        if (mSel.W == true) { strSelcol.Append(" [W] as [W],"); strSelcolAver.Append(" round(Avg([W]),2) as [W],"); }
        //        if (mSel.I == true) { strSelcol.Append(" [I] as [I],"); strSelcolAver.Append(" round(Avg([I]),2) as [I],"); }
        //        if (mSel.Eb == true) { strSelcol.Append(" [Eb] as [Eb(MPa)],"); strSelcolAver.Append(" round(Avg([Eb]),2) as [Eb(MPa)],"); }
        //        if (mSel.σpb == true) { strSelcol.Append(" [σpb] as [σpb(MPa)],"); strSelcolAver.Append(" round(Avg([σpb]),2) as [σpb(MPa)],"); }
        //        if (mSel.σrb == true) { strSelcol.Append(" [σrb] as [σrb(MPa)],"); strSelcolAver.Append(" round(Avg([σrb]),2) as [σrb(MPa)],"); }
        //        if (mSel.σbb == true) { strSelcol.Append(" [σbb] as [σbb(MPa)],"); strSelcolAver.Append(" round(Avg([σbb]),2) as [σbb(MPa)],"); }
        //        if (mSel.U == true) { strSelcol.Append(" [U] as [U(J)],"); strSelcolAver.Append(" round(Avg([U]),2) as [U(J)],"); }
        //    }

        //    if (strSelcol.Length > 0)
        //        strSelcol = strSelcol.Remove(strSelcol.Length - 1, 1);

        //    if (strSelcolAver.Length > 0)
        //        strSelcolAver = strSelcolAver.Remove(strSelcolAver.Length - 1, 1);

        //    strSql_b[0] = strSelcol;
        //    strSql_b[1] = strSelcolAver;//
        //    //这里应该修改为 C.V.变异系数的计算结果
        //    //strSql_t[2] = strSelcolSD;
        //    return strSql_b;
        //}
    }
}
