using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using ZedGraph;
using System.IO;
namespace HR_Test.TestStandard
{
    class GBT23615_2009
    {
        private static string[] _Color_Array = { "Crimson", "Green", "Blue", "Teal", "DarkOrange", "Chocolate", "BlueViolet", "Indigo", "Magenta", "LightCoral", "LawnGreen", "Aqua", "DarkViolet", "DeepPink", "DeepSkyblue", "HotPink", "SpringGreen", "GreenYellow", "Peru", "Black" };

        /// <summary>
        /// 计算试验结果
        /// </summary>
        /// <param name="lGdata"></param>
        public static void CalcFmax(List<gdata> lGdata, out double m_Fmax, out int FmaxIndex)
        {
            m_Fmax = 0;
            FmaxIndex = 0;
            if (lGdata != null)
            {
                int lCount = lGdata.Count;
                for (int i = 1; i < lCount; i++)
                {
                    float load = (float)lGdata[i].F1;
                    //求取最大值的点 
                    //实时得值
                    if (load > m_Fmax)
                    {
                        m_Fmax = load;
                        FmaxIndex = i;
                    }
                }
            }
        }

        /// <summary>
        /// 横向拉伸试验
        /// </summary>
        public class TensileHeng
        {
            public TensileHeng(){} 
            //平均值
            private static bool _isSelT1_;
            //标准差
            private static bool _isSelS;
            //横向拉伸特征值
            private static bool _isSelT1c;

            public static void readFinishSample(DataGridView dg, DataGridView dgAvg, string testNo, DateTimePicker dtp, ZedGraph.ZedGraphControl zed)
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
                BLL.GBT236152009_TensileHeng bllTs = new HR_Test.BLL.GBT236152009_TensileHeng();

                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(testNo))
                {
                    strWhere += " and testNo = '" + testNo + "'";
                }

                strWhere += "and testDate=#" + dtp.Value.Date + "#";


                //if (testNo.Contains('-'))
                //    testNo = testNo.Substring(0, testNo.LastIndexOf("-"));
                //获取不重复的试验编号列表
                DataSet ds = bllTs.GetNotOverlapList(" isFinish=true " + strWhere);
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    string methodName = dt.Rows[0]["testMethodName"].ToString();
                    if(dg!=null)
                        dg.DataSource = CreateFinishView_T(methodName, dt.Rows[0]["testNo"].ToString());
                    dgAvg.DataSource = null;
                    dgAvg.Refresh();
                    CreateAverageView_T(methodName, dt.Rows[0]["testNo"].ToString(), dgAvg);
                }
                else
                {
                    ds = bllTs.GetFinishReport(" isFinish=true " + strWhere,0);
                    dt = ds.Tables[0];
                    //DataRow dr = dt.NewRow();
                    //dt.Rows.Add(dr);
                    if(dg!=null)
                        dg.DataSource = dt;
                    ds.Dispose();
                }

                if (dg != null)
                {
                    //DataGridViewDisableCheckBoxColumn chkcol = new DataGridViewDisableCheckBoxColumn();
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
                    dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
                //}
                //catch (Exception ee) { throw ee; }
            }

            public static void readFinishSampleReport(DataGridView dg, string testNo, string testSampleNo, DateTimePicker dtpfrom, DateTimePicker dtpto, ZedGraph.ZedGraphControl zed)
            {
                dg.DataSource = null;
                dg.Columns.Clear();
                dg.RowHeadersVisible = false;
                BLL.GBT236152009_TensileHeng bllTs = new HR_Test.BLL.GBT236152009_TensileHeng();

                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(testNo))
                {
                    strWhere += " and testNo like '%" + testNo + "%'";
                }

                if (!string.IsNullOrEmpty(testSampleNo))
                {
                    strWhere += " and testSampleNo like '%" + testSampleNo + "%'";
                }

                strWhere += "and testDate>=#" + dtpfrom.Value.Date + "# and testDate<=#" + dtpto.Value.Date + "#  ";

                double maxValue = 0;
                DataSet dsmax = bllTs.GetFmax(" isFinish=true and isEffective=false " + strWhere);
                if (dsmax != null)
                {
                    if (dsmax.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["Fmax"].ToString()))
                            maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["Fmax"].ToString());
                    }
                }

                DataSet ds = bllTs.GetFinishReport(" isFinish=true and isEffective=false " + strWhere, maxValue);

                DataTable dt = ds.Tables[0];
                dg.DataSource = dt;
                ds.Dispose();

                //DataGridViewDisableCheckBoxColumn chkcol = new DataGridViewDisableCheckBoxColumn();
                DataGridViewCheckBoxColumn chkcol = new DataGridViewCheckBoxColumn();
                chkcol.Name = "选择";
                chkcol.MinimumWidth = 50;
                chkcol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
                c.Name = "";
                dg.Columns.Insert(0, chkcol);
                dg.Columns.Insert(1, c);
                dg.Name = "tensile";
                int rCount = dg.Rows.Count;
                for (int i = 0; i < rCount; i++)
                {
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
                dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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

                //clear all curves
                foreach (CurveItem ci in zed.GraphPane.CurveList)
                {
                    ci.Clear();
                    ci.Label.Text = "";
                }

                zed.AxisChange();
                zed.Refresh();
            }

            public static DataTable CreateFinishView_T(string methodName, string _strTestNo)
            {
                //试验结果选择项
                BLL.GBT236152009_SelHeng bllSel = new HR_Test.BLL.GBT236152009_SelHeng();
                Model.GBT236152009_SelHeng mSel = bllSel.GetModel(methodName);

                BLL.GBT236152009_TensileHeng bllts = new HR_Test.BLL.GBT236152009_TensileHeng();
                double maxValue = 0; 
                DataSet dsmax = bllts.GetFmax(" testNo = '" + _strTestNo + "' and isFinish=true ");
                if (dsmax != null)
                {
                    if (dsmax.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["Fmax"].ToString()))
                            maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["Fmax"].ToString());
                    }
                }
                StringBuilder strSelcol = new StringBuilder();
                if (mSel != null)
                {
                    int dotValue = utils.Dotvalue(maxValue);
                        //strSelcol.Append(" round([FQmax],2) as [FQmax(kN)],");
                        if (mSel.Fmax == true)
                        {
                            if (maxValue < 1000.0)
                            {
                                switch (dotValue)
                                {
                                    case 2: strSelcol.Append(" FORMAT([Fmax],'0.00') as [Fmax(N)],");
                                        break;
                                    case 3: strSelcol.Append(" FORMAT([Fmax],'0.000') as [Fmax(N)],");
                                        break;
                                    case 4: strSelcol.Append(" FORMAT([Fmax],'0.0000') as [Fmax(N)],");
                                        break;
                                    case 5: strSelcol.Append(" FORMAT([Fmax],'0.00000') as [Fmax(N)],");
                                        break;
                                    default: strSelcol.Append(" FORMAT([Fmax],'0.00') as [Fmax(N)],");
                                        break;
                                }
                            }
                            if (maxValue >= 1000.0)
                            {
                                switch (dotValue)
                                {
                                    case 2: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.00') as [Fmax(kN)],");
                                        break;
                                    case 3: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.000') as [Fmax(kN)],");
                                        break;
                                    case 4: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.0000') as [Fmax(kN)],");
                                        break;
                                    case 5: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.00000') as [Fmax(kN)],");
                                        break;
                                    default: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.00') as [Fmax(kN)],");
                                        break;
                                }
                            }  

                        //strSelcol.Append(" round([Fmax],4) as [Fmax(kN)],");
                    }

                    if (mSel.T1 == true)
                    {
                        strSelcol.Append(" round([T1],2) as [T1(MPa)],");
                    }
                }

                string _selCol = strSelcol.ToString();
                if (_selCol.Length > 0)
                    _selCol = _selCol.Remove(_selCol.Length - 1, 1);

                
                DataSet dst = bllts.GetFinishList(_selCol, " testNo = '" + _strTestNo + "' and isFinish=true ");
                return dst.Tables[0];
            }

            public static DataTable CreateFinishViewReport(string methodName, string strWhere)
            {
                //试验结果选择项
                BLL.GBT236152009_SelHeng bllSel = new HR_Test.BLL.GBT236152009_SelHeng();
                Model.GBT236152009_SelHeng mSel = bllSel.GetModel(methodName); 
                BLL.GBT236152009_TensileHeng bllts = new HR_Test.BLL.GBT236152009_TensileHeng();
                double maxValue = 0; 
                DataSet dsmax = bllts.GetFmax(strWhere);
                if (dsmax != null)
                {
                    if (dsmax.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["Fmax"].ToString()))
                            maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["Fmax"].ToString());
                    }
                }

                StringBuilder strSelcol = new StringBuilder();
                if (mSel != null)
                { 
                    //strSelcol.Append(" round([FQmax],2) as [FQmax(kN)],");
                    if (mSel.Fmax == true)
                    {
                        int dotValue = utils.Dotvalue(maxValue);
                        if (maxValue < 1000.0)
                        {
                            switch (dotValue)
                            {
                                case 2: strSelcol.Append(" FORMAT([Fmax],'0.00') as [Fmax(N)],");
                                    break;
                                case 3: strSelcol.Append(" FORMAT([Fmax],'0.000') as [Fmax(N)],");
                                    break;
                                case 4: strSelcol.Append(" FORMAT([Fmax],'0.0000') as [Fmax(N)],");
                                    break;
                                case 5: strSelcol.Append(" FORMAT([Fmax],'0.00000') as [Fmax(N)],");
                                    break;
                                default: strSelcol.Append(" FORMAT([Fmax],'0.00') as [Fmax(N)],");
                                    break;
                            }
                        }
                        if (maxValue >= 1000.0)
                        {
                            switch (dotValue)
                            {
                                case 2: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.00') as [Fmax(kN)],");
                                    break;
                                case 3: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.000') as [Fmax(kN)],");
                                    break;
                                case 4: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.0000') as [Fmax(kN)],");
                                    break;
                                case 5: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.00000') as [Fmax(kN)],");
                                    break;
                                default: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.00') as [Fmax(kN)],");
                                    break;
                            }
                        } 
                    }

                    if (mSel.T1 == true)
                    {
                        strSelcol.Append(" round([T1],2) as [T1(MPa)],");
                    }
                }

                string _selCol = strSelcol.ToString();
                if (_selCol.Length > 0)
                    _selCol = _selCol.Remove(_selCol.Length - 1, 1);   
                  
                DataSet dst = bllts.GetFinishListReport(_selCol, strWhere);
                return dst.Tables[0];
            }

            public static void CreateAverageViewReport(string methodName, string strWhere, DataGridView datagridview)
            {
                //试验结果选择项

                double avgT_ = 0;
                double SQ = 0;
                double Qc = 0;
                int fcount = 0;
                BLL.GBT236152009_SelHeng bllSel = new HR_Test.BLL.GBT236152009_SelHeng();
                Model.GBT236152009_SelHeng mSel = bllSel.GetModel(methodName);

                if (mSel != null)
                {
                    _isSelT1_ = mSel.T1_;
                    _isSelS = mSel.S;
                    _isSelT1c = mSel.T1c;
                }

                BLL.GBT236152009_TensileHeng bllts = new HR_Test.BLL.GBT236152009_TensileHeng();

                //查询 TestNo下 试验完成的试样Q—的平均值
                DataSet dsavg = bllts.GetFinishAvg(strWhere);
                DataSet dsavgView = new DataSet();
                dsavgView.Tables.Add();
                if (dsavg.Tables[0].Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dsavg.Tables[0].Rows[0][0].ToString()))
                        return;
                    avgT_ = Convert.ToDouble(dsavg.Tables[0].Rows[0][0].ToString());
                }

                //求标准偏差
                DataSet dsfinish = bllts.GetList(strWhere);
                fcount = dsfinish.Tables[0].Rows.Count;

              
                double sumsq = 0;
                double ctrue = 0;

                //计算标准偏差
                if (avgT_ != 0)
                {
                    for (int i = 0; i < dsfinish.Tables[0].Rows.Count; i++)
                    {
                        ctrue = Convert.ToDouble(dsfinish.Tables[0].Rows[i]["T1"].ToString());
                        double c1 = (ctrue - avgT_) * (ctrue - avgT_);
                        sumsq += c1;
                    }
                    //标准偏差
                    if (fcount - 1 != 0)
                        SQ = Math.Sqrt(sumsq / (fcount - 1));
                    else
                        SQ = 0;

                    //计算Qc
                    Qc = avgT_ - 2.02d * SQ;
                }
                List<object> lsto = new List<object>();
                dsavgView.Tables[0].Columns.Add("试样数量", System.Type.GetType("System.String")).SetOrdinal(0);
                lsto.Add(fcount.ToString());
                dsavgView.Tables[0].Columns.Add("T1ˉ(MPa)", System.Type.GetType("System.String")).SetOrdinal(1);
                lsto.Add(avgT_.ToString("f2"));
                if (_isSelS)
                {
                    dsavgView.Tables[0].Columns.Add("S(MPa)", System.Type.GetType("System.String"));
                    lsto.Add(SQ.ToString("f2"));
                }
                if (_isSelT1c)
                {
                    dsavgView.Tables[0].Columns.Add("T1c(MPa)", System.Type.GetType("System.String"));
                    lsto.Add(Qc.ToString("f2"));
                }
                dsavgView.Tables[0].Rows.Add(lsto.ToArray());
                datagridview.DataSource = dsavgView.Tables[0];
                datagridview.Update();
                datagridview.Refresh();
                dsfinish.Dispose();
            }

            public static void CreateAverageView_T(string methodName, string _strTestNo, DataGridView datagridview)
            {
                //试验结果选择项

                double avgT1_ = 0;
                double S = 0;
                double T1c = 0;
                int fcount = 0;
                BLL.GBT236152009_SelHeng bllSel = new HR_Test.BLL.GBT236152009_SelHeng();
                Model.GBT236152009_SelHeng mSel = bllSel.GetModel(methodName);

                if (mSel != null)
                {
                    _isSelT1_ = mSel.T1_;
                    _isSelS = mSel.S;
                    _isSelT1c = mSel.T1c;
                }

                BLL.GBT236152009_TensileHeng bllts = new HR_Test.BLL.GBT236152009_TensileHeng();

                //查询 TestNo下 试验完成的试样Q—的平均值
                DataSet dsavg = bllts.GetFinishAvg(" testNo='" + _strTestNo + "'  and isFinish=true and isEffective=false ");
                DataSet dsavgView = new DataSet();
                dsavgView.Tables.Add();
                if (dsavg.Tables[0].Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dsavg.Tables[0].Rows[0][0].ToString()))
                        return;
                    avgT1_ = Convert.ToDouble(dsavg.Tables[0].Rows[0][0].ToString());
                }

                //求标准偏差
                DataSet dsfinish = bllts.GetList(" testNo = '" + _strTestNo + "' and isFinish=true and isEffective=false ");
                fcount = dsfinish.Tables[0].Rows.Count;

                //添加第0列
                dsavgView.Tables[0].Columns.Add("试样数量", System.Type.GetType("System.String"));
                //设置为第0列
                dsavgView.Tables[0].Columns["试样数量"].SetOrdinal(0);

                //添加第1列
                dsavgView.Tables[0].Columns.Add("T1ˉ(MPa)", System.Type.GetType("System.String"));
                //设置为第1列
                dsavgView.Tables[0].Columns["T1ˉ(MPa)"].SetOrdinal(1);

                //添加第0列
                dsavgView.Tables[0].Columns.Add("S(MPa)", System.Type.GetType("System.String"));
                //设置为第0列
                dsavgView.Tables[0].Columns["S(MPa)"].SetOrdinal(2);

                //添加第1列
                dsavgView.Tables[0].Columns.Add("T1c(MPa)", System.Type.GetType("System.String"));
                //设置为第1列
                dsavgView.Tables[0].Columns["T1c(MPa)"].SetOrdinal(3);

                double sumsq = 0;
                double ctrue = 0;

                //计算标准偏差
                if (avgT1_ != 0)
                {
                    for (int i = 0; i < dsfinish.Tables[0].Rows.Count; i++)
                    {
                        ctrue = Convert.ToDouble(dsfinish.Tables[0].Rows[i]["T1"].ToString());
                        double c1 = (ctrue - avgT1_) * (ctrue - avgT1_);
                        sumsq += c1;
                    }
                    //标准偏差
                    if (fcount - 1 != 0)
                        S = Math.Sqrt(sumsq / (fcount - 1));
                    else
                        S = 0;

                    //计算Qc
                    T1c = avgT1_ - 2.02d * S;
                }


                //平均值行
                object[] row = { fcount.ToString(), avgT1_.ToString("f2"), S.ToString("f2"), T1c.ToString("f2") };
                dsavgView.Tables[0].Rows.Add(row);
                datagridview.DataSource = dsavgView.Tables[0];

                //S.D.标准偏差行 
                if (!_isSelS) { datagridview.Columns[2].Visible = false; }
                //横向拉伸特征值
                if (!_isSelT1c) { datagridview.Columns[3].Visible = false; }
                datagridview.Update();
                datagridview.Refresh();
                dsfinish.Dispose();
            }

            public static void CreateCurveFile(string _testpath, Model.GBT236152009_TensileHeng modelTensile)
            {
                FileStream fs = new FileStream(_testpath, FileMode.Create, FileAccess.ReadWrite);
                utils.AddText(fs, "testType,testSampleNo,L,t");
                utils.AddText(fs, "\r\n");
                utils.AddText(fs, "tensileHeng," + modelTensile.testSampleNo + "," + modelTensile.L+","+modelTensile.t);
                utils.AddText(fs, "\r\n");
                fs.Flush();
                fs.Close();
                fs.Dispose();
            } 
        }

        /// <summary>
        /// 纵向拉伸试验
        /// </summary>
        public static class TensileZong
        {
            //平均值
            private static bool _isSelT2_;
            //标准差
            private static bool _isSelS;
            //横向拉伸特征值
            private static bool _isSelT2c;

            //E
            private static bool _isSelE;
            //Z
            private static bool _isSelZ;

            public static void readFinishSample(DataGridView dg, DataGridView dgAvg, string testNo, DateTimePicker dtp, ZedGraph.ZedGraphControl zed)
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
                BLL.GBT236152009_TensileZong bllTs = new HR_Test.BLL.GBT236152009_TensileZong();

                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(testNo))
                {
                    strWhere += " and testNo = '" + testNo + "'";
                }

                strWhere += "and testDate=#" + dtp.Value.Date + "#";


                //if (testNo.Contains('-'))
                //    testNo = testNo.Substring(0, testNo.LastIndexOf("-"));
                //获取不重复的试验编号列表
                DataSet ds = bllTs.GetNotOverlapList(" isFinish=true " + strWhere);
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    string methodName = dt.Rows[0]["testMethodName"].ToString();
                    if(dg!=null)
                        dg.DataSource = CreateFinishView_T(methodName, dt.Rows[0]["testNo"].ToString());
                    dgAvg.DataSource = null;
                    dgAvg.Refresh();
                    CreateAverageView_T(methodName, dt.Rows[0]["testNo"].ToString(), dgAvg);
                }
                else
                {
                    ds = bllTs.GetFinishReport(" isFinish=true " + strWhere,0);
                    dt = ds.Tables[0];
                    //DataRow dr = dt.NewRow();
                    //dt.Rows.Add(dr);
                    if(dg!=null)
                        dg.DataSource = dt;
                    ds.Dispose();
                }

                if (dg != null)
                {
                    //DataGridViewDisableCheckBoxColumn chkcol = new DataGridViewDisableCheckBoxColumn();
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
                    dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
                //}
                //catch (Exception ee) { throw ee; }
            }

            public static void readFinishSampleReport(DataGridView dg, string testNo, string testSampleNo, DateTimePicker dtpfrom, DateTimePicker dtpto, ZedGraph.ZedGraphControl zed)
            {
                dg.DataSource = null;
                dg.Columns.Clear();
                dg.RowHeadersVisible = false;
                BLL.GBT236152009_TensileZong bllTs = new HR_Test.BLL.GBT236152009_TensileZong();

                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(testNo))
                {
                    strWhere += " and testNo like '%" + testNo + "%'";
                }

                if (!string.IsNullOrEmpty(testSampleNo))
                {
                    strWhere += " and testSampleNo like '%" + testSampleNo + "%'";
                }

                strWhere += "and testDate>=#" + dtpfrom.Value.Date + "# and testDate<=#" + dtpto.Value.Date + "#  ";
                 
                double maxValue = 0;
                DataSet dsmax = bllTs.GetFmax(" isFinish=true and isEffective=false " + strWhere);
                if (dsmax != null)
                {
                    if (dsmax.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["Fmax"].ToString()))
                            maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["Fmax"].ToString());
                    }
                }

                DataSet ds = bllTs.GetFinishReport(" isFinish=true and isEffective=false " + strWhere, maxValue);
                DataTable dt = ds.Tables[0];
                dg.DataSource = dt;
                ds.Dispose();

                //DataGridViewDisableCheckBoxColumn chkcol = new DataGridViewDisableCheckBoxColumn();
                DataGridViewCheckBoxColumn chkcol = new DataGridViewCheckBoxColumn();
                chkcol.Name = "选择";
                chkcol.MinimumWidth = 50;
                chkcol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
                c.Name = "";
                dg.Columns.Insert(0, chkcol);
                dg.Columns.Insert(1, c);
                dg.Name = "tensile";
                int rCount = dg.Rows.Count;
                for (int i = 0; i < rCount; i++)
                {
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
                dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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

                //clear all curves
                foreach (CurveItem ci in zed.GraphPane.CurveList)
                {
                    ci.Clear();
                    ci.Label.Text = "";
                }

                zed.AxisChange();
                zed.Refresh();
            }

            public static DataTable CreateFinishView_T(string methodName, string _strTestNo)
            {
                //试验结果选择项
                BLL.GBT236152009_SelZong bllSel = new HR_Test.BLL.GBT236152009_SelZong();
                Model.GBT236152009_SelZong mSel = bllSel.GetModel(methodName); 

                BLL.GBT236152009_TensileZong bllts = new HR_Test.BLL.GBT236152009_TensileZong();
                double maxValue = 0; 
                DataSet dsmax = bllts.GetFmax(" testNo = '" + _strTestNo + "' and isFinish=true ");
                if (dsmax != null)
                {
                    if (dsmax.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["Fmax"].ToString()))
                            maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["Fmax"].ToString());
                    }
                }
                StringBuilder strSelcol = new StringBuilder();
                if (mSel != null)
                {
                    if (mSel.Fmax == true)
                    {
                        int dotValue = utils.Dotvalue(maxValue);
                        if (maxValue < 1000.0)
                        {
                            switch (dotValue)
                            {
                                case 2: strSelcol.Append(" FORMAT([Fmax],'0.00') as [Fmax(N)],");
                                    break;
                                case 3: strSelcol.Append(" FORMAT([Fmax],'0.000') as [Fmax(N)],");
                                    break;
                                case 4: strSelcol.Append(" FORMAT([Fmax],'0.0000') as [Fmax(N)],");
                                    break;
                                case 5: strSelcol.Append(" FORMAT([Fmax],'0.00000') as [Fmax(N)],");
                                    break;
                                default: strSelcol.Append(" FORMAT([Fmax],'0.00') as [Fmax(N)],");
                                    break;
                            }
                        }
                        if (maxValue >= 1000.0)
                        {
                            switch (dotValue)
                            {
                                case 2: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.00') as [Fmax(kN)],");
                                    break;
                                case 3: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.000') as [Fmax(kN)],");
                                    break;
                                case 4: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.0000') as [Fmax(kN)],");
                                    break;
                                case 5: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.00000') as [Fmax(kN)],");
                                    break;
                                default: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.00') as [Fmax(kN)],");
                                    break;
                            }
                        }
                        //strSelcol.Append(" round([Fmax],4) as [Fmax(kN)],");
                    }        

                    if (mSel.T2 == true)
                    {
                        strSelcol.Append(" round([T2],2) as [T2(MPa)],");
                    }

                    if (mSel.E == true)
                    {
                        strSelcol.Append(" round([E],2) as [E(MPa)],");
                    }

                    if (mSel.Z == true)
                    {
                        strSelcol.Append(" round([Z],2) as [A(%)],");
                    }
                }

                string _selCol = strSelcol.ToString();
                if (_selCol.Length > 0)
                    _selCol = _selCol.Remove(_selCol.Length - 1, 1);

                DataSet dst = bllts.GetFinishList(_selCol, " testNo = '" + _strTestNo + "' and isFinish=true ");
                return dst.Tables[0];
            }

            public static DataTable CreateFinishViewReport(string methodName, string strWhere)
            {
                //试验结果选择项
                BLL.GBT236152009_SelZong bllSel = new HR_Test.BLL.GBT236152009_SelZong();
                Model.GBT236152009_SelZong mSel = bllSel.GetModel(methodName);

                StringBuilder strSelcol = new StringBuilder();

                BLL.GBT236152009_TensileZong bllts = new HR_Test.BLL.GBT236152009_TensileZong();
                double maxValue = 0;
                DataSet dsmax = bllts.GetFmax(strWhere);
                if (dsmax != null)
                {
                    if (dsmax.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["Fmax"].ToString()))
                            maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["Fmax"].ToString());
                    }
                }

                if (mSel != null)
                {
                    int _dotValue = utils.Dotvalue(maxValue);
                    if (mSel.Fmax == true)
                    {
                        //strSelcol.Append(" round([Fmax],2) as [Fmax(kN)],");
                        if (maxValue < 1000.0)
                        {
                            switch (_dotValue)
                            {
                                case 2: strSelcol.Append(" FORMAT([Fmax],'0.00') as [Fmax(N)],");
                                    break;
                                case 3: strSelcol.Append(" FORMAT([Fmax],'0.000') as [Fmax(N)],");
                                    break;
                                case 4: strSelcol.Append(" FORMAT([Fmax],'0.0000') as [Fmax(N)],");
                                    break;
                                case 5: strSelcol.Append(" FORMAT([Fmax],'0.00000') as [Fmax(N)],");
                                    break;
                                default: strSelcol.Append(" FORMAT([Fmax],'0.00') as [Fmax(N)],");
                                    break;
                            }
                        }
                        if (maxValue >= 1000.0)
                        {
                            switch (_dotValue)
                            {
                                case 2: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.00') as [Fmax(kN)],");
                                    break;
                                case 3: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.000') as [Fmax(kN)],");
                                    break;
                                case 4: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.0000') as [Fmax(kN)],");
                                    break;
                                case 5: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.00000') as [Fmax(kN)],");
                                    break;
                                default: strSelcol.Append(" FORMAT([Fmax]/1000.0,'0.00') as [Fmax(kN)],");
                                    break;
                            }
                        } 
                    }

                    if (mSel.T2 == true)
                    {
                        strSelcol.Append(" round([T2],2) as [T2(MPa)],");
                    }

                    if (mSel.E == true)
                    {
                        strSelcol.Append(" round([E],2) as [E(MPa)],");
                    }

                    if (mSel.Z == true)
                    {
                        strSelcol.Append(" round([Z],2) as [A(%)],");
                    }
                }

                string _selCol = strSelcol.ToString();
                if (_selCol.Length > 0)
                    _selCol = _selCol.Remove(_selCol.Length - 1, 1);

                DataSet dst = bllts.GetFinishListReport(_selCol, strWhere);
                return dst.Tables[0];
            }

            public static void CreateAverageViewReport(string methodName, string strWhere, DataGridView datagridview)
            {
                //试验结果选择项

                double avgT = 0;
                double St2 = 0;
                double T2c = 0;
                int fcount = 0;
                BLL.GBT236152009_SelZong bllSel = new HR_Test.BLL.GBT236152009_SelZong();
                Model.GBT236152009_SelZong mSel = bllSel.GetModel(methodName);

                if (mSel != null)
                {
                    _isSelT2_ = mSel.T2_;
                    _isSelS = mSel.S;
                    _isSelT2c = mSel.T2c;
                    _isSelZ = mSel.Z;
                    _isSelE = mSel.E;
                }

                BLL.GBT236152009_TensileZong bllts = new HR_Test.BLL.GBT236152009_TensileZong();

                //查询 TestNo下 试验完成的试样Q—的平均值
                DataSet dsavg = bllts.GetFinishAvg(strWhere);
                DataSet dsavgView = new DataSet();
                dsavgView.Tables.Add();
                if (dsavg.Tables[0].Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dsavg.Tables[0].Rows[0][0].ToString()))
                        return;
                    avgT = Convert.ToDouble(dsavg.Tables[0].Rows[0][0].ToString());
                }

                //求标准偏差
                DataSet dsfinish = bllts.GetList(strWhere);
                fcount = dsfinish.Tables[0].Rows.Count;
             
                double sumsq = 0;
                double ctrue = 0;

                //计算标准偏差
                if (avgT != 0)
                {
                    for (int i = 0; i < dsfinish.Tables[0].Rows.Count; i++)
                    {
                        ctrue = Convert.ToDouble(dsfinish.Tables[0].Rows[i]["T2"].ToString());
                        double c1 = (ctrue - avgT) * (ctrue - avgT);
                        sumsq += c1;
                    }
                    //标准偏差
                    if (fcount - 1 != 0)
                        St2 = Math.Sqrt(sumsq / (fcount - 1));
                    else
                        St2 = 0;

                    //计算Qc
                   T2c = avgT - 2.02d * St2;
                }

                List<object> lsto = new List<object>();
                dsavgView.Tables[0].Columns.Add("试样数量", System.Type.GetType("System.String")).SetOrdinal(0);
                lsto.Add(fcount.ToString());
                dsavgView.Tables[0].Columns.Add("T2ˉ(MPa)", System.Type.GetType("System.String")).SetOrdinal(1);
                lsto.Add(avgT.ToString("f2"));
                if (_isSelS)
                {
                    dsavgView.Tables[0].Columns.Add("S(MPa)", System.Type.GetType("System.String"));
                    lsto.Add(St2.ToString("f2"));
                }
                if (_isSelT2c)
                {
                    dsavgView.Tables[0].Columns.Add("T2c(MPa)", System.Type.GetType("System.String"));
                    lsto.Add(T2c.ToString("f2"));
                }                 
                dsavgView.Tables[0].Rows.Add(lsto.ToArray());
                datagridview.DataSource = dsavgView.Tables[0];
                datagridview.Update();
                datagridview.Refresh();
                dsfinish.Dispose();
            }

            public static void CreateAverageView_T(string methodName, string _strTestNo, DataGridView datagridview)
            {
                //试验结果选择项

                double avgT2_ = 0;
                double S = 0;
                double T2c = 0;
                int fcount = 0;
                BLL.GBT236152009_SelZong bllSel = new HR_Test.BLL.GBT236152009_SelZong();
                Model.GBT236152009_SelZong mSel = bllSel.GetModel(methodName);

                if (mSel != null)
                {
                    _isSelT2_ = mSel.T2_;
                    _isSelS = mSel.S;
                    _isSelT2c = mSel.T2c;
                }

                BLL.GBT236152009_TensileZong bllts = new HR_Test.BLL.GBT236152009_TensileZong();

                //查询 TestNo下 试验完成的试样Q—的平均值
                DataSet dsavg = bllts.GetFinishAvg(" testNo='" + _strTestNo + "'  and isFinish=true and isEffective=false ");
                DataSet dsavgView = new DataSet();
                dsavgView.Tables.Add();
                if (dsavg.Tables[0].Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dsavg.Tables[0].Rows[0][0].ToString()))
                        return;
                    avgT2_ = Convert.ToDouble(dsavg.Tables[0].Rows[0][0].ToString());
                }

                //求标准偏差
                DataSet dsfinish = bllts.GetList(" testNo = '" + _strTestNo + "' and isFinish=true  and isEffective=false ");
                fcount = dsfinish.Tables[0].Rows.Count;

                //添加第0列
                dsavgView.Tables[0].Columns.Add("试样数量", System.Type.GetType("System.String"));
                //设置为第0列
                dsavgView.Tables[0].Columns["试样数量"].SetOrdinal(0);

                //添加第1列
                dsavgView.Tables[0].Columns.Add("T2ˉ(MPa)", System.Type.GetType("System.String"));
                //设置为第1列
                dsavgView.Tables[0].Columns["T2ˉ(MPa)"].SetOrdinal(1);

                //添加第0列
                dsavgView.Tables[0].Columns.Add("S(MPa)", System.Type.GetType("System.String"));
                //设置为第0列
                dsavgView.Tables[0].Columns["S(MPa)"].SetOrdinal(2);

                //添加第1列
                dsavgView.Tables[0].Columns.Add("T2c(MPa)", System.Type.GetType("System.String"));
                //设置为第1列
                dsavgView.Tables[0].Columns["T2c(MPa)"].SetOrdinal(3);

                double sumsq = 0;
                double ctrue = 0;

                //计算标准偏差
                if (avgT2_ != 0)
                {
                    for (int i = 0; i < dsfinish.Tables[0].Rows.Count; i++)
                    {
                        ctrue = Convert.ToDouble(dsfinish.Tables[0].Rows[i]["T2"].ToString());
                        double c1 = (ctrue - avgT2_) * (ctrue - avgT2_);
                        sumsq += c1;
                    }
                    //标准偏差
                    if (fcount - 1 != 0)
                        S = Math.Sqrt(sumsq / (fcount - 1));
                    else
                        S = 0;

                    //计算Qc
                    T2c = avgT2_ - 2.02d * S;
                }


                //平均值行
                object[] row = { fcount.ToString(), avgT2_.ToString("f2"), S.ToString("f2"), T2c.ToString("f2") };
                dsavgView.Tables[0].Rows.Add(row);
                datagridview.DataSource = dsavgView.Tables[0];

                //S.D.标准偏差行 
                if (!_isSelS) { datagridview.Columns[2].Visible = false; }
                //横向拉伸特征值
                if (!_isSelT2c) { datagridview.Columns[3].Visible = false; }
                datagridview.Update();
                datagridview.Refresh();
                dsfinish.Dispose();
            }

            public static void CreateCurveFile(string _testpath, Model.GBT236152009_TensileZong modelTensile)
            {
                FileStream fs = new FileStream(_testpath, FileMode.Create, FileAccess.ReadWrite);
                utils.AddText(fs, "testType,testSampleNo,Lo,t,b2");
                utils.AddText(fs, "\r\n");
                utils.AddText(fs, "tensileZong," + modelTensile.testSampleNo + "," + modelTensile.L0 + "," + modelTensile.t + "," + modelTensile.b2);
                utils.AddText(fs, "\r\n");
                fs.Flush();
                fs.Close();
                fs.Dispose();
            } 
        }


    }
}
