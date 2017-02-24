using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using ZedGraph;
using System.Windows.Forms;
using System.Drawing;

namespace HR_Test.TestStandard
{
    class GBT28289_2012
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
        /// 拉伸试验
        /// </summary>
        public static class Tensile
        {
            //平均值
            private static bool _isSelQ_;
            //标准差
            private static bool _isSelSQ;
            //横向拉伸特征值
            private static bool _isSelQc;

            public static void readFinishSample(DataGridView dg, DataGridView dgAvg, string testNo,DateTimePicker dtp, ZedGraph.ZedGraphControl zed)
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
                BLL.GBT282892012_Tensile bllTs = new HR_Test.BLL.GBT282892012_Tensile();

                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(testNo))
                {
                    strWhere += " and testNo = '" + testNo + "'";
                }
 
                strWhere += "and testDate=#" + dtp.Value.Date + "#";

               
                    //if (testNo.Contains('-'))
                    //    testNo = testNo.Substring(0, testNo.LastIndexOf("-"));
                    //获取不重复的试验编号列表
                    DataSet ds = bllTs.GetNotOverlapList(" isFinish=true "+ strWhere);
                    DataTable dt = ds.Tables[0];
                    double maxValue = 0;
                    BLL.GBT282892012_Tensile bllts = new HR_Test.BLL.GBT282892012_Tensile();
                    DataSet dsmax = bllts.GetFQmax("isFinish=true "+ strWhere);
                    if (dsmax != null)
                    {
                        if (dsmax.Tables[0].Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["FQmax"].ToString()))
                                maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["FQmax"].ToString());
                        }
                    }

                    if (dt.Rows.Count>0)
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
                        ds = bllTs.GetFinishReport(" isFinish=true "+strWhere,maxValue);
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
                //clear all curves
                    if (zed != null)
                    {
                        foreach (CurveItem ci in zed.GraphPane.CurveList)
                        {
                            ci.Clear();
                            ci.Label.Text = "";
                        }

                        zed.AxisChange();
                        zed.Refresh();
                    } 
            }

            public static void readFinishSampleReport(DataGridView dg,  string testNo, string testSampleNo, DateTimePicker dtpfrom, DateTimePicker dtpto, ZedGraph.ZedGraphControl zed)
            {
                //try
                //{
                //dg.MultiSelect = true;  
                dg.DataSource = null;
                dg.Columns.Clear();
                dg.RowHeadersVisible = false;
                BLL.GBT282892012_Tensile bllTs = new HR_Test.BLL.GBT282892012_Tensile();

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
                    BLL.GBT282892012_Tensile bllts = new HR_Test.BLL.GBT282892012_Tensile();
                    DataSet dsmax = bllts.GetFQmax(" isFinish=true and isEffective=false " + strWhere);
                    if (dsmax != null)
                    {
                        if (dsmax.Tables[0].Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["FQmax"].ToString()))
                                maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["FQmax"].ToString());
                        }
                    }

                DataSet   ds = bllTs.GetFinishReport(" isFinish=true and isEffective=false " + strWhere,maxValue); 
                DataTable  dt = ds.Tables[0];
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
                //dg.Rows[0].Cells[0].Value = false;
                //dg.Rows[0].Cells[0].Selected = false;
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
                //}
                //catch (Exception ee) { throw ee; }
            }

            public static DataTable CreateFinishView_T(string methodName, string _strTestNo)
            {
                //试验结果选择项
                BLL.GBT282892012_TensileSel bllSel = new HR_Test.BLL.GBT282892012_TensileSel();
                Model.GBT282892012_TensileSel mSel = bllSel.GetModel(methodName);
                double maxValue = 0;
                BLL.GBT282892012_Tensile bllts = new HR_Test.BLL.GBT282892012_Tensile();
                DataSet dsmax = bllts.GetFQmax(" testNo = '" + _strTestNo + "' and isFinish=true "); 
                if (dsmax != null)
                {
                    if (dsmax.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["FQmax"].ToString()))
                            maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["FQmax"].ToString());
                    }
                }
                StringBuilder strSelcol = new StringBuilder();
                if (mSel != null)
                {
                    int dotValue = utils.Dotvalue(maxValue);
                        //strSelcol.Append(" round([FQmax],2) as [FQmax(kN)],");
                        if (mSel.FQmax == true)
                        {
                            if (maxValue < 1000.0)
                            {
                                switch (dotValue)
                                {
                                    case 2: strSelcol.Append(" FORMAT([FQmax],'0.00') as [FQmax(N)],");
                                        break;
                                    case 3: strSelcol.Append(" FORMAT([FQmax],'0.000') as [FQmax(N)],");
                                        break;
                                    case 4: strSelcol.Append(" FORMAT([FQmax],'0.0000') as [FQmax(N)],");
                                        break;
                                    case 5: strSelcol.Append(" FORMAT([FQmax],'0.00000') as [FQmax(N)],");
                                        break;
                                    default: strSelcol.Append(" FORMAT([FQmax],'0.00') as [FQmax(N)],");
                                        break;
                                }                              
                            }
                            if (maxValue >= 1000.0)
                            {
                                switch (dotValue)
                                {
                                    case 2: strSelcol.Append(" FORMAT([FQmax]/1000.0,'0.00') as [FQmax(kN)],");
                                        break;
                                    case 3: strSelcol.Append(" FORMAT([FQmax]/1000.0,'0.000') as [FQmax(kN)],");
                                        break;
                                    case 4: strSelcol.Append(" FORMAT([FQmax]/1000.0,'0.0000') as [FQmax(kN)],");
                                        break;
                                    case 5: strSelcol.Append(" FORMAT([FQmax]/1000.0,'0.00000') as [FQmax(kN)],");
                                        break;
                                    default: strSelcol.Append(" FORMAT([FQmax]/1000.0,'0.00') as [FQmax(kN)],");
                                        break;
                                }
                            }
                        } 

                    if (mSel.Q == true)
                    {
                        strSelcol.Append(" round([Q],2) as [Q(N/mm)],");
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
                BLL.GBT282892012_TensileSel bllSel = new HR_Test.BLL.GBT282892012_TensileSel();
                Model.GBT282892012_TensileSel mSel = bllSel.GetModel(methodName); 

                double maxValue = 0;
                BLL.GBT282892012_Tensile bllts = new HR_Test.BLL.GBT282892012_Tensile();
                DataSet dsmax = bllts.GetFQmax(strWhere);
                if (dsmax != null)
                {
                    if (dsmax.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["FQmax"].ToString()))
                            maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["FQmax"].ToString());
                    }
                }
                StringBuilder strSelcol = new StringBuilder();
                int dotValue = utils.Dotvalue(maxValue);
                if (mSel != null)
                {                  
                    if (mSel.FQmax == true)
                    {
                        if (maxValue < 1000.0)
                        {
                            switch (dotValue)
                            {
                                case 2: strSelcol.Append(" FORMAT([FQmax],'0.00') as [FQmax(N)],");
                                    break;
                                case 3: strSelcol.Append(" FORMAT([FQmax],'0.000') as [FQmax(N)],");
                                    break;
                                case 4: strSelcol.Append(" FORMAT([FQmax],'0.0000') as [FQmax(N)],");
                                    break;
                                case 5: strSelcol.Append(" FORMAT([FQmax],'0.00000') as [FQmax(N)],");
                                    break;
                                default: strSelcol.Append(" FORMAT([FQmax],'0.00') as [FQmax(N)],");
                                    break;
                            }
                        }
                        if (maxValue >= 1000.0)
                        {
                            switch (dotValue)
                            {
                                case 2: strSelcol.Append(" FORMAT([FQmax]/1000.0,'0.00') as [FQmax(kN)],");
                                    break;
                                case 3: strSelcol.Append(" FORMAT([FQmax]/1000.0,'0.000') as [FQmax(kN)],");
                                    break;
                                case 4: strSelcol.Append(" FORMAT([FQmax]/1000.0,'0.0000') as [FQmax(kN)],");
                                    break;
                                case 5: strSelcol.Append(" FORMAT([FQmax]/1000.0,'0.00000') as [FQmax(kN)],");
                                    break;
                                default: strSelcol.Append(" FORMAT([FQmax]/1000.0,'0.00') as [FQmax(kN)],");
                                    break;
                            }
                        } 
                    } 

                    if (mSel.Q == true)
                    {
                        strSelcol.Append(" round([Q],2) as [Q(N/mm)],");
                    }
                }

                string _selCol = strSelcol.ToString();
                if (_selCol.Length > 0)
                    _selCol = _selCol.Remove(_selCol.Length - 1, 1);

                DataSet dst = bllts.GetFinishListReport(_selCol,strWhere);
                return dst.Tables[0];
            }

            public static void CreateAverageViewReport(string methodName, string strWhere, DataGridView datagridview)
            {
                //试验结果选择项

                double avgQ_ = 0;
                double SQ = 0;
                double Qc = 0;
                int fcount = 0;
                BLL.GBT282892012_TensileSel bllSel = new HR_Test.BLL.GBT282892012_TensileSel();
                Model.GBT282892012_TensileSel mSel = bllSel.GetModel(methodName);

                if (mSel != null)
                {
                    _isSelQ_ = mSel.Q_;
                    _isSelSQ = mSel.SQ;
                    _isSelQc = mSel.Qc;
                }

                BLL.GBT282892012_Tensile bllts = new HR_Test.BLL.GBT282892012_Tensile();

                //查询 TestNo下 试验完成的试样Q—的平均值
                DataSet dsavg = bllts.GetFinishAvg(strWhere);
                DataSet dsavgView = new DataSet();
                dsavgView.Tables.Add();
                if (dsavg.Tables[0].Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dsavg.Tables[0].Rows[0][0].ToString()))
                        return;
                    avgQ_ = Convert.ToDouble(dsavg.Tables[0].Rows[0][0].ToString());
                }

                //求标准偏差
                DataSet dsfinish = bllts.GetList(strWhere);
                fcount = dsfinish.Tables[0].Rows.Count;

                double sumsq = 0;
                double ctrue = 0;

                //计算标准偏差
                if (avgQ_ != 0)
                {
                    for (int i = 0; i < dsfinish.Tables[0].Rows.Count; i++)
                    {
                        ctrue = Convert.ToDouble(dsfinish.Tables[0].Rows[i]["Q"].ToString());
                        double c1 = (ctrue - avgQ_) * (ctrue - avgQ_);
                        sumsq += c1;
                    }
                    //标准偏差
                    if (fcount - 1 != 0)
                        SQ = Math.Sqrt(sumsq / (fcount - 1));
                    else
                        SQ = 0;

                    //计算Qc
                    Qc = avgQ_ - 2.02d * SQ;
                }
                List<object> lsto = new List<object>();
                dsavgView.Tables[0].Columns.Add("试样数量", System.Type.GetType("System.String")).SetOrdinal(0);
                lsto.Add(fcount.ToString());
                dsavgView.Tables[0].Columns.Add("Qˉ(N/mm)", System.Type.GetType("System.String")).SetOrdinal(1);
                lsto.Add(avgQ_.ToString("f2"));
                if (_isSelSQ)
                {
                    dsavgView.Tables[0].Columns.Add("SQ(N/mm)", System.Type.GetType("System.String"));
                    lsto.Add(SQ.ToString("f2"));
                }
                if (_isSelQc)
                {
                    dsavgView.Tables[0].Columns.Add("Qc(N/mm)", System.Type.GetType("System.String"));
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

                double avgQ_ = 0;
                double SQ = 0;
                double Qc = 0;
                int fcount = 0;
                BLL.GBT282892012_TensileSel bllSel = new HR_Test.BLL.GBT282892012_TensileSel();
                Model.GBT282892012_TensileSel mSel = bllSel.GetModel(methodName);

                if (mSel != null)
                {
                    _isSelQ_ = mSel.Q_;
                    _isSelSQ = mSel.SQ;
                    _isSelQc = mSel.Qc;
                }

                BLL.GBT282892012_Tensile bllts = new HR_Test.BLL.GBT282892012_Tensile();

                //查询 TestNo下 试验完成的试样Q—的平均值
                DataSet dsavg = bllts.GetFinishAvg(" testNo='" + _strTestNo + "'  and isFinish=true and isEffective=false ");
                DataSet dsavgView = new DataSet();
                dsavgView.Tables.Add();
                if (dsavg.Tables[0].Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dsavg.Tables[0].Rows[0][0].ToString()))
                        return;
                    avgQ_ = Convert.ToDouble(dsavg.Tables[0].Rows[0][0].ToString());
                }

                //求标准偏差
                DataSet dsfinish = bllts.GetList(" testNo = '" + _strTestNo + "' and isFinish=true and isEffective=false ");
                fcount = dsfinish.Tables[0].Rows.Count;

                //添加第0列
                dsavgView.Tables[0].Columns.Add("试样数量", System.Type.GetType("System.String"));
                //设置为第0列
                dsavgView.Tables[0].Columns["试样数量"].SetOrdinal(0);

                //添加第1列
                dsavgView.Tables[0].Columns.Add("Qˉ(N/mm)", System.Type.GetType("System.String"));
                //设置为第1列
                dsavgView.Tables[0].Columns["Qˉ(N/mm)"].SetOrdinal(1);

                //添加第0列
                dsavgView.Tables[0].Columns.Add("SQ(N/mm)", System.Type.GetType("System.String"));
                //设置为第0列
                dsavgView.Tables[0].Columns["SQ(N/mm)"].SetOrdinal(2);

                //添加第1列
                dsavgView.Tables[0].Columns.Add("Qc(N/mm)", System.Type.GetType("System.String"));
                //设置为第1列
                dsavgView.Tables[0].Columns["Qc(N/mm)"].SetOrdinal(3);

                double sumsq = 0;
                double ctrue = 0;

                //计算标准偏差
                if (avgQ_ != 0)
                {
                    for (int i = 0; i < dsfinish.Tables[0].Rows.Count; i++)
                    {
                        ctrue = Convert.ToDouble(dsfinish.Tables[0].Rows[i]["Q"].ToString());
                        double c1 = (ctrue - avgQ_) * (ctrue - avgQ_);
                        sumsq += c1;
                    }
                    //标准偏差
                    if (fcount - 1 != 0)
                        SQ = Math.Sqrt(sumsq / (fcount - 1));
                    else
                        SQ = 0;

                    //计算Qc
                    Qc = avgQ_ - 2.02d * SQ;
                }


                //平均值行
                object[] row = { fcount.ToString(), avgQ_.ToString("f2"), SQ.ToString("f2"), Qc.ToString("f2") };
                dsavgView.Tables[0].Rows.Add(row);
                datagridview.DataSource = dsavgView.Tables[0];

                //S.D.标准偏差行 
                if (!_isSelSQ) { datagridview.Columns[2].Visible = false; }
                //横向拉伸特征值
                if (!_isSelQc) { datagridview.Columns[3].Visible = false; }
                datagridview.Update();
                datagridview.Refresh();
                dsfinish.Dispose();
            }

            public static void CreateCurveFile(string _testpath, Model.GBT282892012_Tensile modelTensile)
            {
                FileStream fs = new FileStream(_testpath, FileMode.Create, FileAccess.ReadWrite);
                utils.AddText(fs, "testType,testSampleNo,L");
                utils.AddText(fs, "\r\n");
                utils.AddText(fs, "tensile," + modelTensile.testSampleNo + "," + modelTensile.L);
                utils.AddText(fs, "\r\n");
                fs.Flush();
                fs.Close();
                fs.Dispose();
            } 
        }

        /// <summary>
        /// 剪切试验
        /// </summary>
        public static class Shear
        {
            //弹性系数平均值
            private static bool _isSelC1R_;
            //室温弹性系数
            private static bool _isSelC1cR;
            //低温弹性系数平均值 
            private static bool _isSelC1L_;
            //低温弹性系数特征值
            private static bool _isSelC1cL;
            //高温弹性系数平均值
            private static bool _isSelC1H_;
            //高温弹性系数特征值
            private static bool _isSelC1cH;
            //单位长度承受最大力平均值 T_
            private static bool _isSelT_;
            //标准差
            private static bool _isSelST;
            //抗剪特征值
            private static bool _isSelTc;

            public static void readFinishSampleReport(DataGridView dg, string testNo, string testSampleNo, DateTimePicker dtpfrom, DateTimePicker dtpto, ZedGraph.ZedGraphControl zed)
            {
                //try
                //{
                //dg.MultiSelect = true;  
                dg.DataSource = null;
                dg.Columns.Clear();
                dg.RowHeadersVisible = false;
                BLL.GBT282892012_Shear bllS = new HR_Test.BLL.GBT282892012_Shear();

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
                BLL.GBT282892012_Shear bllts = new HR_Test.BLL.GBT282892012_Shear();
                DataSet dsmax = bllts.GetFTmax(" isFinish=true and isEffective=false " + strWhere);
                if (dsmax != null)
                {
                    if (dsmax.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["FTmax"].ToString()))
                            maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["FTmax"].ToString());
                    }
                }

                DataSet ds = bllS.GetFinishListFind(" isFinish=true and isEffective=false " + strWhere,maxValue);
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
                //dg.Rows[0].Cells[0].Value = false;
                //dg.Rows[0].Cells[0].Selected = false;
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
                //}
                //catch (Exception ee) { throw ee; }
            }

            public static void readFinishSample(DataGridView dg, DataGridView dgAvg, string testNo, DateTimePicker dtpfrom,  ZedGraph.ZedGraphControl zed)
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
                BLL.GBT282892012_Shear bllTs = new HR_Test.BLL.GBT282892012_Shear();
                string sqlDate = string.Empty;
                if (!string.IsNullOrEmpty(testNo))
                {
                    //if (testNo.Contains('-'))
                    //    testNo = testNo.Substring(0, testNo.LastIndexOf("-"));
                    //获取不重复的试验编号列表
                    sqlDate = " testNo='" + testNo + "' and testDate=#" + dtpfrom.Value.Date + "# and isFinish=true ";
                    DataSet ds = bllTs.GetNotOverlapList(sqlDate);
                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        string methodName = dt.Rows[0]["testMethodName"].ToString();
                        if(dg!=null) 
                            dg.DataSource = CreateFinishView(methodName, dt.Rows[0]["testNo"].ToString());
                        dgAvg.DataSource = null;
                        dgAvg.Refresh();
                        CreateAverageView(methodName, dt.Rows[0]["testNo"].ToString(), dgAvg);
                    }
                  
                }  
                else
                {
                    DataSet  ds = bllTs.GetFinishListFind(sqlDate,0);
                    DataTable  dt = ds.Tables[0];
                    //DataRow dr = dt.NewRow();
                    //dt.Rows.Add(dr);
                    if(dg!=null)
                        dg.DataSource = dt;
                    ds.Dispose();
                }
                //DataGridViewDisableCheckBoxColumn chkcol = new DataGridViewDisableCheckBoxColumn();

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
                    dg.Name = "shear";
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
                //clear all curves
                if (zed != null)
                {
                    foreach (CurveItem ci in zed.GraphPane.CurveList)
                    {
                        ci.Clear();
                        ci.Label.Text = "";
                    }

                    zed.AxisChange();
                    zed.Refresh();
                }
            }

            public static DataTable CreateFinishView(string methodName, string _strTestNo)
            {
                //试验结果选择项
                BLL.GBT282892012_ShearSel bllSel = new HR_Test.BLL.GBT282892012_ShearSel();
                Model.GBT282892012_ShearSel mSel = bllSel.GetModel(methodName);
                BLL.GBT282892012_Shear bllts = new HR_Test.BLL.GBT282892012_Shear();
                DataSet dsmax = bllts.GetFTmax(" testNo = '" + _strTestNo + "' and isFinish=true  ");
                double maxValue = 0;
                if (dsmax != null)
                {
                    if (dsmax.Tables[0].Rows.Count > 0)
                    {
                        if(!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["FTmax"].ToString()))
                            maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["FTmax"].ToString());
                    }
                }

                StringBuilder strSelcol = new StringBuilder();
                if (mSel != null)
                {
                    int dotValue = utils.Dotvalue(maxValue);
                    if (mSel.FTmax == true)
                    {
                        if (maxValue < 1000.0)
                        {
                            switch (dotValue)
                            {
                                case 2: strSelcol.Append(" FORMAT([FTmax],'0.00') as [FTmax(N)],");
                                    break;
                                case 3: strSelcol.Append(" FORMAT([FTmax],'0.000') as [FTmax(N)],");
                                    break;
                                case 4: strSelcol.Append(" FORMAT([FTmax],'0.0000') as [FTmax(N)],");
                                    break;
                                case 5: strSelcol.Append(" FORMAT([FTmax],'0.00000') as [FTmax(N)],");
                                    break;
                                default: strSelcol.Append(" FORMAT([FTmax],'0.00') as [FTmax(N)],");
                                    break;
                            }
                        }
                        if (maxValue >= 1000.0)
                        {
                            switch (dotValue)
                            {
                                case 2: strSelcol.Append(" FORMAT([FTmax]/1000.0,'0.00') as [FTmax(kN)],");
                                    break;
                                case 3: strSelcol.Append(" FORMAT([FTmax]/1000.0,'0.000') as [FTmax(kN)],");
                                    break;
                                case 4: strSelcol.Append(" FORMAT([FTmax]/1000.0,'0.0000') as [FTmax(kN)],");
                                    break;
                                case 5: strSelcol.Append(" FORMAT([FTmax]/1000.0,'0.00000') as [FTmax(kN)],");
                                    break;
                                default: strSelcol.Append(" FORMAT([FTmax]/1000.0,'0.00') as [FTmax(kN)],");
                                    break;
                            }
                        }
                        //strSelcol.Append(" round([FTmax],2) as [FTmax(kN)],");
                    }

                    if (mSel.T == true)
                    {
                        strSelcol.Append(" round([T],2) as [T(N/mm)],");
                    }

                    if (mSel.C1 == true)
                    {
                        strSelcol.Append("  round([C1],2) as [C1(N/mm²)],");
                    }
                }

                string _selCol = strSelcol.ToString();
                if (_selCol.Length > 0)
                    _selCol = _selCol.Remove(_selCol.Length - 1, 1);

               
                DataSet dst = bllts.GetFinishList(_selCol, " testNo = '" + _strTestNo + "' and isFinish=true ");
                return dst.Tables[0];
            }

            public static DataTable CreateFinishViewReport(string _arrTestSampleNo, DateTimePicker dt1, DateTimePicker dt2)
            {
                StringBuilder strSelcol = new StringBuilder();
            
                string strWhere = string.Empty;
                BLL.GBT282892012_Shear bllts = new HR_Test.BLL.GBT282892012_Shear();
                if (!string.IsNullOrEmpty(_arrTestSampleNo))
                {
                    strWhere = " testSampleNo in ('" + _arrTestSampleNo + ") and isFinish=true and isEffective=false  and testDate>=#" + dt1.Value.Date + "# and testDate<=#" + dt2.Value.Date + "# ";
                }
                else
                {
                    strWhere = " isFinish=true and isEffective=false  and testDate>=#" + dt1.Value.Date + "# and testDate<=#" + dt2.Value.Date + "# ";
                }

                DataSet dsmax = bllts.GetFTmax(strWhere);
                double maxValue = 0;
                if (dsmax != null)
                {
                    if (dsmax.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["FTmax"].ToString()))
                            maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["FTmax"].ToString());
                    }
                }
                int _dotValue = utils.Dotvalue(maxValue);
                if (maxValue < 1000.0)
                {
                    switch (_dotValue)
                    {
                        case 2: strSelcol.Append(" FORMAT([FTmax],'0.00') as [FTmax(N)],");
                            break;
                        case 3: strSelcol.Append(" FORMAT([FTmax],'0.000') as [FTmax(N)],");
                            break;
                        case 4: strSelcol.Append(" FORMAT([FTmax],'0.0000') as [FTmax(N)],");
                            break;
                        case 5: strSelcol.Append(" FORMAT([FTmax],'0.00000') as [FTmax(N)],");
                            break;
                        default: strSelcol.Append(" FORMAT([FTmax],'0.00') as [FTmax(N)],");
                            break;
                    }
                }
                if (maxValue >= 1000.0)
                {
                    switch (_dotValue)
                    {
                        case 2: strSelcol.Append(" FORMAT([FTmax]/1000.0,'0.00') as [FTmax(kN)],");
                            break;
                        case 3: strSelcol.Append(" FORMAT([FTmax]/1000.0,'0.000') as [FTmax(kN)],");
                            break;
                        case 4: strSelcol.Append(" FORMAT([FTmax]/1000.0,'0.0000') as [FTmax(kN)],");
                            break;
                        case 5: strSelcol.Append(" FORMAT([FTmax]/1000.0,'0.00000') as [FTmax(kN)],");
                            break;
                        default: strSelcol.Append(" FORMAT([FTmax]/1000.0,'0.00') as [FTmax(kN)],");
                            break;
                    }
                }

                //strSelcol.Append(" round([FTmax],2) as [FTmax(kN)],");
                strSelcol.Append(" round([T],2) as [T(N/mm)],");
                strSelcol.Append("  round([C1],2) as [C1(N/mm²)],");
                string _selCol = strSelcol.ToString();
                if (_selCol.Length > 0)
                    _selCol = _selCol.Remove(_selCol.Length - 1, 1);
                DataSet dst = bllts.GetFinishReport(_selCol, strWhere);
                return dst.Tables[0];
            }


            public static void CreateAverageView(string methodName, string _strTestNo, DataGridView dgv)
            {
                //数量
                int _countLow = 0;
                int _countRoom = 0;
                int _countHigh = 0;

                //平均值
                double _avgLow = 0;
                double _avgRoom = 0;
                double _avgHigh = 0;
                //标准偏差
                double _sTLow = 0;
                double _sTRoom = 0;
                double _sTHigh = 0;
                //抗剪特征值
                double _TcLow = 0;
                double _TcRoom = 0;
                double _TcHigh = 0;
                //弹性系数平均值
                double _avgc1Low = 0;
                double _avgc1Room = 0;
                double _avgc1High = 0;
                //弹性系数特征值
                double _c1cLow = 0;
                double _c1cRoom = 0;
                double _c1cHigh = 0;

                BLL.GBT282892012_ShearSel bllSel = new HR_Test.BLL.GBT282892012_ShearSel();
                Model.GBT282892012_ShearSel mSel = bllSel.GetModel(methodName);

                if (mSel != null)
                {
                    //弹性系数平均值
                    _isSelC1R_ = mSel.C1R_;
                    //室温弹性系数
                    _isSelC1cR = mSel.C1cR;
                    //低温弹性系数平均值 
                    _isSelC1L_ = mSel.C1L_;
                    //低温弹性系数特征值
                    _isSelC1cL = mSel.C1cL;
                    //高温弹性系数平均值
                    _isSelC1H_ = mSel.C1H_;
                    //高温弹性系数特征值
                    _isSelC1cH = mSel.C1cH;
                    //单位长度承受最大力平均值 T_
                    _isSelT_ = mSel.T_;
                    //标准差
                    _isSelST = mSel.ST;
                    //抗剪特征值
                    _isSelTc = mSel.Tc;
                }

                BLL.GBT282892012_Shear bllts = new HR_Test.BLL.GBT282892012_Shear();

                string strTest = _strTestNo.Substring(0, _strTestNo.Length - 1);
                string strL = strTest + "L";
                string strH = strTest + "H";
                string strR = strTest + "R";

                //查询 TestNo下 三种试验温度的数据平均值 T_
                DataSet dsLowAvg = bllts.GetFinishAvg(" testNo='" + strL + "' and isFinish=true and isEffective=false");
                DataSet dsNormalAvg = bllts.GetFinishAvg(" testNo='" + strR + "' and isFinish=true and isEffective=false ");
                DataSet dsHighAvg = bllts.GetFinishAvg(" testNo='" + strH + "' and isFinish=true and isEffective=false");

                DataSet dsavg = new DataSet();
                dsavg.Tables.Add();

                if (dsLowAvg.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dsLowAvg.Tables[0].Rows[0][0].ToString()))
                    {
                        _avgLow = Convert.ToDouble(dsLowAvg.Tables[0].Rows[0][0].ToString());
                        _avgc1Low = Convert.ToDouble(dsLowAvg.Tables[0].Rows[0][1].ToString());
                    }
                }

                if (dsNormalAvg.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dsNormalAvg.Tables[0].Rows[0][0].ToString()))
                    {
                        _avgRoom = Convert.ToDouble(dsNormalAvg.Tables[0].Rows[0][0].ToString());
                        _avgc1Room = Convert.ToDouble(dsNormalAvg.Tables[0].Rows[0][1].ToString());
                    }
                }

                if (dsHighAvg.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dsHighAvg.Tables[0].Rows[0][0].ToString()))
                    {
                        _avgHigh = Convert.ToDouble(dsHighAvg.Tables[0].Rows[0][0].ToString());
                        _avgc1High = Convert.ToDouble(dsHighAvg.Tables[0].Rows[0][1].ToString());
                    }
                }


                DataSet dsLow = bllts.GetList(" testNo='" + strL + "'  and isFinish=true  and isEffective=false");
                DataSet dsNormal = bllts.GetList(" testNo='" + strR + "'  and isFinish=true  and isEffective=false");
                DataSet dsHigh = bllts.GetList(" testNo='" + strH + "'  and isFinish=true and isEffective=false");

                _countLow = dsLow.Tables[0].Rows.Count;
                _countRoom = dsNormal.Tables[0].Rows.Count;
                _countHigh = dsHigh.Tables[0].Rows.Count; 

                double sumsq = 0;
                double ctrue = 0;

                //计算低温标准偏差
                if (_avgLow != 0)
                {
                    for (int i = 0; i < _countLow; i++)
                    {
                        ctrue = Convert.ToDouble(dsLow.Tables[0].Rows[i]["T"].ToString());
                        double c1 = (ctrue - _avgLow) * (ctrue - _avgLow);
                        sumsq += c1;
                    }
                    //标准偏差
                    if (_countLow - 1 != 0)
                        _sTLow = Math.Sqrt(sumsq / (_countLow - 1));
                    else
                        _sTLow = 0;

                    //计算Tc
                    _TcLow = _avgLow - 2.02d * _sTLow;
                }

                //计算室温标准偏差
                if (_avgRoom != 0)
                {
                    for (int i = 0; i < _countRoom; i++)
                    {
                        ctrue = Convert.ToDouble(dsNormal.Tables[0].Rows[i]["T"].ToString());
                        double c1 = (ctrue - _avgRoom) * (ctrue - _avgRoom);
                        sumsq += c1;
                    }
                    //标准偏差
                    if (_countRoom - 1 != 0)
                        _sTRoom = Math.Sqrt(sumsq / (_countRoom - 1));
                    else
                        _sTRoom = 0;
                    //计算Tc
                    _TcRoom = _avgRoom - 2.02d * _sTRoom;
                }

                //计算高温标准偏差
                if (_avgHigh != 0)
                {
                    for (int i = 0; i < dsHigh.Tables[0].Rows.Count; i++)
                    {
                        ctrue = Convert.ToDouble(dsHigh.Tables[0].Rows[i]["T"].ToString());
                        double c1 = (ctrue - _avgHigh) * (ctrue - _avgHigh);
                        sumsq += c1;
                    }
                    //标准偏差
                    if (_countHigh - 1 != 0)
                        _sTHigh = Math.Sqrt(sumsq / (_countHigh - 1));
                    else
                        _sTHigh = 0;

                    //计算Tc
                    _TcHigh = _avgHigh - 2.02d * _sTHigh;
                }

                _c1cRoom = _avgc1Room;
                _c1cLow = (_c1cRoom + _avgc1Low) / 2.0;
                _c1cHigh = (_c1cRoom + _avgc1High) / 2.0;

                List<object> lstr = new List<object>();
                List<object> lsth = new List<object>();
                List<object> lstl = new List<object>();

                //添加第0列
                dsavg.Tables[0].Columns.Add("温度", System.Type.GetType("System.String")).SetOrdinal(0);
                lstr.Add("室温");
                lsth.Add("高温");
                lstl.Add("低温");
                dsavg.Tables[0].Columns.Add("数量", System.Type.GetType("System.String")).SetOrdinal(1);
                lstr.Add(_countRoom.ToString());
                lsth.Add(_countHigh.ToString());
                lstl.Add(_countLow.ToString());

                if (_isSelT_)
                {
                    dsavg.Tables[0].Columns.Add("剪切力均值\r\nT￣(N/mm)", System.Type.GetType("System.String"));
                    lstr.Add(_avgRoom.ToString("f2"));
                    lsth.Add(_avgHigh.ToString("f2"));
                    lstl.Add(_avgLow.ToString("f2"));
                }
                //标准差
                if (_isSelST)
                {
                    dsavg.Tables[0].Columns.Add("标准差\r\nST(N/mm)", System.Type.GetType("System.String"));
                    lstr.Add(_sTRoom.ToString("f2"));
                    lsth.Add(_sTHigh.ToString("f2"));
                    lstl.Add(_sTLow.ToString("f2"));
                }
                 //抗剪特征值
                if (_isSelTc)
                {    
                    dsavg.Tables[0].Columns.Add("抗剪特征值\r\nTc(N/mm)", System.Type.GetType("System.String"));
                    lstr.Add(_TcRoom.ToString("f2"));
                    lsth.Add(_TcHigh.ToString("f2"));
                    lstl.Add(_TcLow.ToString("f2"));
                }

                //object[] rowRoom = { "室温", _countRoom.ToString(), _avgRoom.ToString("f2"), _sTRoom.ToString("f2"), _TcRoom.ToString("f2"), _avgc1Room.ToString("f2"), _c1cRoom.ToString("f2") };
                //object[] rowLow = { "低温", _countLow.ToString(), _avgLow.ToString("f2"), _sTLow.ToString("f2"), _TcLow.ToString("f2"), _avgc1Low.ToString("f2"), _c1cLow.ToString("f2") };
                //object[] rowHigh = { "高温", _countHigh.ToString(), _avgHigh.ToString("f2"), _sTHigh.ToString("f2"), _TcHigh.ToString("f2"), _avgc1High.ToString("f2"), _c1cHigh.ToString("f2") };


                //室温弹性系数平均值
                if (_isSelC1R_)
                {
                    dsavg.Tables[0].Columns.Add("弹性系数均值\r\n(N/mm²)", System.Type.GetType("System.String"));
                    lstr.Add(_avgc1Room.ToString("f2"));
                    lsth.Add(_avgc1High.ToString("f2"));
                    lstl.Add(_avgc1Low.ToString("f2"));
                }

                if (_isSelC1cR)
                {
                    dsavg.Tables[0].Columns.Add("弹性系数特征值\r\n(N/mm²)", System.Type.GetType("System.String"));
                    lstr.Add(_c1cRoom.ToString("f2"));
                    lsth.Add(_c1cHigh.ToString("f2"));
                    lstl.Add(_c1cLow.ToString("f2"));
                } 

                dsavg.Tables[0].Rows.Add(lstr.ToArray());
                if (!string.IsNullOrEmpty(dsLowAvg.Tables[0].Rows[0][0].ToString()))
                {
                    dsavg.Tables[0].Rows.Add(lstl.ToArray());
                }
                if (!string.IsNullOrEmpty(dsHighAvg.Tables[0].Rows[0][0].ToString()))
                {
                    dsavg.Tables[0].Rows.Add(lsth.ToArray());
                }

                dgv.DataSource = dsavg.Tables[0];
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                //低温弹性系数平均值 
                _isSelC1L_ = mSel.C1L_;
                //低温弹性系数特征值
                _isSelC1cL = mSel.C1cL;
                //高温弹性系数平均值
                _isSelC1H_ = mSel.C1H_;
                //高温弹性系数特征值
                _isSelC1cH = mSel.C1cH;

                dgv.Refresh();
                dsavg.Dispose();
            }

            public static void CreateAverageViewReport(string methodName,string _arrTestSampleNo, DataGridView dgv)
            {
                //数量
                int _countLow = 0;
                int _countRoom = 0;
                int _countHigh = 0;

                //平均值
                double _avgLow = 0;
                double _avgRoom = 0;
                double _avgHigh = 0;
                //标准偏差
                double _sTLow = 0;
                double _sTRoom = 0;
                double _sTHigh = 0;
                //抗剪特征值
                double _TcLow = 0;
                double _TcRoom = 0;
                double _TcHigh = 0;
                //弹性系数平均值
                double _avgc1Low = 0;
                double _avgc1Room = 0;
                double _avgc1High = 0;
                //弹性系数特征值
                double _c1cLow = 0;
                double _c1cRoom = 0;
                double _c1cHigh = 0;

                BLL.GBT282892012_ShearSel bllSel = new HR_Test.BLL.GBT282892012_ShearSel();
                Model.GBT282892012_ShearSel mSel = bllSel.GetModel(methodName);

                if (mSel != null)
                {
                    //弹性系数平均值
                    _isSelC1R_ = mSel.C1R_;
                    //室温弹性系数
                    _isSelC1cR = mSel.C1cR;
                    //低温弹性系数平均值 
                    _isSelC1L_ = mSel.C1L_;
                    //低温弹性系数特征值
                    _isSelC1cL = mSel.C1cL;
                    //高温弹性系数平均值
                    _isSelC1H_ = mSel.C1H_;
                    //高温弹性系数特征值
                    _isSelC1cH = mSel.C1cH;
                    //单位长度承受最大力平均值 T_
                    _isSelT_ = mSel.T_;
                    //标准差
                    _isSelST = mSel.ST;
                    //抗剪特征值
                    _isSelTc = mSel.Tc;
                }

                BLL.GBT282892012_Shear bllts = new HR_Test.BLL.GBT282892012_Shear();
                //将所选择的试样编号 分类 ，分成 H R L组别的编号
                
                string[] arrsamples = _arrTestSampleNo.Split(',');
                List<string> arrH = new List<string>();
                List<string> arrR = new List<string>();
                List<string> arrL = new List<string>();

                foreach (string s in arrsamples)
                {
                    string lchar = s.Substring(s.LastIndexOf('-') - 1, 1);
                    switch (lchar)
                    {
                        case "R": arrR.Add(s); break;
                        case "H": arrH.Add(s); break;
                        case "L": arrL.Add(s); break;
                    }  
                }

                string[] _arrH = arrH.ToArray();
                string[] _arrL = arrL.ToArray();
                string[] _arrR = arrR.ToArray();
                string sqlH=string.Empty;
                string sqlL=string.Empty;
                string sqlR=string.Empty;
                if (_arrH.Length > 0)
                {
                    foreach (string s in _arrH)
                    {
                        sqlH +=s+ "','";
                    }
                    if (!string.IsNullOrEmpty(sqlH))
                        sqlH =  sqlH.Substring(0, sqlH.Length - 2);

                }
                if (_arrR.Length > 0)
                {
                    foreach (string s in _arrR)
                    {
                        sqlR += s+ "','";
                    }
                    if (!string.IsNullOrEmpty(sqlR))
                        sqlR = sqlR.Substring(0, sqlR.Length - 2);
                }
                if (_arrL.Length > 0)
                {
                    foreach (string s in _arrL)
                    {
                        sqlL += s+"','";
                    }
                    if (!string.IsNullOrEmpty(sqlL))
                        sqlL = sqlL.Substring(0, sqlL.Length - 1);
                }

                //string strTest = _strTestNo.Substring(0, _strTestNo.Length - 1);
                //string strL = strTest + "L";
                //string strH = strTest + "H";
                //string strR = strTest + "R";

                //查询 TestNo下 三种试验温度的数据平均值 T_
                 DataSet dsLowAvg=null;
                 DataSet dsNormalAvg=null;
                 DataSet dsHighAvg=null;
                if( !string.IsNullOrEmpty(sqlL))
                    dsLowAvg = bllts.GetFinishAvg(" testSampleNo in ('" + sqlL + ") and isFinish=true and isEffective=false ");
                 if( !string.IsNullOrEmpty(sqlR))
                     dsNormalAvg = bllts.GetFinishAvg(" testSampleNo in ('" + sqlR + ") and isFinish=true  and isEffective=false ");
                 if( !string.IsNullOrEmpty(sqlH))
                     dsHighAvg = bllts.GetFinishAvg(" testSampleNo in('" + sqlH + ") and isFinish=true  and isEffective=false ");

                DataSet dsavg = new DataSet();
                dsavg.Tables.Add();
                if (dsLowAvg != null)
                {
                    if (dsLowAvg.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsLowAvg.Tables[0].Rows[0][0].ToString()))
                        {
                            _avgLow = Convert.ToDouble(dsLowAvg.Tables[0].Rows[0][0].ToString());
                            _avgc1Low = Convert.ToDouble(dsLowAvg.Tables[0].Rows[0][1].ToString());
                        }
                    }
                }

                if (dsNormalAvg != null)
                {
                    if (dsNormalAvg.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsNormalAvg.Tables[0].Rows[0][0].ToString()))
                        {
                            _avgRoom = Convert.ToDouble(dsNormalAvg.Tables[0].Rows[0][0].ToString());
                            _avgc1Room = Convert.ToDouble(dsNormalAvg.Tables[0].Rows[0][1].ToString());
                        }
                    }
                }

                if (dsHighAvg != null)
                {
                    if (dsHighAvg.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsHighAvg.Tables[0].Rows[0][0].ToString()))
                        {
                            _avgHigh = Convert.ToDouble(dsHighAvg.Tables[0].Rows[0][0].ToString());
                            _avgc1High = Convert.ToDouble(dsHighAvg.Tables[0].Rows[0][1].ToString());
                        }
                    }
                }
                DataSet dsLow=null,dsNormal=null,dsHigh =null;
                if (!string.IsNullOrEmpty(sqlL)) dsLow = bllts.GetList(" testSampleNo in ('" + sqlL + ") and isFinish=true  and isEffective=false ");
                if (!string.IsNullOrEmpty(sqlR)) dsNormal = bllts.GetList(" testSampleNo in ('" + sqlR + ") and isFinish=true  and isEffective=false ");
                if (!string.IsNullOrEmpty(sqlH)) dsHigh = bllts.GetList(" testSampleNo in('" + sqlH + ") and isFinish=true  and isEffective=false ");
                if(dsLow!=null)
                    _countLow = dsLow.Tables[0].Rows.Count;
                if(dsNormal!=null)
                    _countRoom = dsNormal.Tables[0].Rows.Count;
                if(dsHigh!=null)
                    _countHigh = dsHigh.Tables[0].Rows.Count;

                double sumsq = 0;
                double ctrue = 0;

                //计算低温标准偏差
                if (_avgLow != 0)
                {
                    for (int i = 0; i < _countLow; i++)
                    {
                        ctrue = Convert.ToDouble(dsLow.Tables[0].Rows[i]["T"].ToString());
                        double c1 = (ctrue - _avgLow) * (ctrue - _avgLow);
                        sumsq += c1;
                    }
                    //标准偏差
                    if (_countLow - 1 != 0)
                        _sTLow = Math.Sqrt(sumsq / (_countLow - 1));
                    else
                        _sTLow = 0;

                    //计算Tc
                    _TcLow = _avgLow - 2.02d * _sTLow;
                }

                //计算室温标准偏差
                if (_avgRoom != 0)
                {
                    for (int i = 0; i < _countRoom; i++)
                    {
                        ctrue = Convert.ToDouble(dsNormal.Tables[0].Rows[i]["T"].ToString());
                        double c1 = (ctrue - _avgRoom) * (ctrue - _avgRoom);
                        sumsq += c1;
                    }
                    //标准偏差
                    if (_countRoom - 1 != 0)
                        _sTRoom = Math.Sqrt(sumsq / (_countRoom - 1));
                    else
                        _sTRoom = 0;
                    //计算Tc
                    _TcRoom = _avgRoom - 2.02d * _sTRoom;
                }

                //计算高温标准偏差
                if (_avgHigh != 0)
                {
                    for (int i = 0; i < dsHigh.Tables[0].Rows.Count; i++)
                    {
                        ctrue = Convert.ToDouble(dsHigh.Tables[0].Rows[i]["T"].ToString());
                        double c1 = (ctrue - _avgHigh) * (ctrue - _avgHigh);
                        sumsq += c1;
                    }
                    //标准偏差
                    if (_countHigh - 1 != 0)
                        _sTHigh = Math.Sqrt(sumsq / (_countHigh - 1));
                    else
                        _sTHigh = 0;

                    //计算Tc
                    _TcHigh = _avgHigh - 2.02d * _sTHigh;
                }

                _c1cRoom = _avgc1Room;
                _c1cLow = (_c1cRoom + _avgc1Low) / 2.0;
                _c1cHigh = (_c1cRoom + _avgc1High) / 2.0;

                //object[] rowRoom = { "室温", _countRoom.ToString(), _avgRoom.ToString("f2"), _sTRoom.ToString("f2"), _TcRoom.ToString("f2"), _avgc1Room.ToString("f2"), _c1cRoom.ToString("f2") };
                //object[] rowLow = { "低温", _countLow.ToString(), _avgLow.ToString("f2"), _sTLow.ToString("f2"), _TcLow.ToString("f2"), _avgc1Low.ToString("f2"), _c1cLow.ToString("f2") };
                //object[] rowHigh = { "高温", _countHigh.ToString(), _avgHigh.ToString("f2"), _sTHigh.ToString("f2"), _TcHigh.ToString("f2"), _avgc1High.ToString("f2"), _c1cHigh.ToString("f2") };
                List<object> lstr = new List<object>();
                List<object> lsth = new List<object>();
                List<object> lstl = new List<object>();

                //添加第0列
                dsavg.Tables[0].Columns.Add("温度", System.Type.GetType("System.String")).SetOrdinal(0);
                lstr.Add("室温");
                lsth.Add("高温");
                lstl.Add("低温");
                dsavg.Tables[0].Columns.Add("数量", System.Type.GetType("System.String")).SetOrdinal(1);
                lstr.Add(_countRoom.ToString());
                lsth.Add(_countHigh.ToString());
                lstl.Add(_countLow.ToString());

                if (_isSelT_)
                {
                    dsavg.Tables[0].Columns.Add("剪切力均值\r\nT￣(N/mm)", System.Type.GetType("System.String"));
                    lstr.Add(_avgRoom.ToString("f2"));
                    lsth.Add(_avgHigh.ToString("f2"));
                    lstl.Add(_avgLow.ToString("f2"));
                }
                //标准差
                if (_isSelST)
                {
                    dsavg.Tables[0].Columns.Add("标准差\r\nST(N/mm)", System.Type.GetType("System.String"));
                    lstr.Add(_sTRoom.ToString("f2"));
                    lsth.Add(_sTHigh.ToString("f2"));
                    lstl.Add(_sTLow.ToString("f2"));
                }
                //抗剪特征值
                if (_isSelTc)
                {
                    dsavg.Tables[0].Columns.Add("抗剪特征值\r\nTc(N/mm)", System.Type.GetType("System.String"));
                    lstr.Add(_TcRoom.ToString("f2"));
                    lsth.Add(_TcHigh.ToString("f2"));
                    lstl.Add(_TcLow.ToString("f2"));
                }

                //object[] rowRoom = { "室温", _countRoom.ToString(), _avgRoom.ToString("f2"), _sTRoom.ToString("f2"), _TcRoom.ToString("f2"), _avgc1Room.ToString("f2"), _c1cRoom.ToString("f2") };
                //object[] rowLow = { "低温", _countLow.ToString(), _avgLow.ToString("f2"), _sTLow.ToString("f2"), _TcLow.ToString("f2"), _avgc1Low.ToString("f2"), _c1cLow.ToString("f2") };
                //object[] rowHigh = { "高温", _countHigh.ToString(), _avgHigh.ToString("f2"), _sTHigh.ToString("f2"), _TcHigh.ToString("f2"), _avgc1High.ToString("f2"), _c1cHigh.ToString("f2") };


                //室温弹性系数平均值
                if (_isSelC1R_)
                {
                    dsavg.Tables[0].Columns.Add("弹性系数均值\r\n(N/mm²)", System.Type.GetType("System.String"));
                    lstr.Add(_avgc1Room.ToString("f2"));
                    lsth.Add(_avgc1High.ToString("f2"));
                    lstl.Add(_avgc1Low.ToString("f2"));
                }

                if (_isSelC1cR)
                {
                    dsavg.Tables[0].Columns.Add("弹性系数特征值\r\n(N/mm²)", System.Type.GetType("System.String"));
                    lstr.Add(_c1cRoom.ToString("f2"));
                    lsth.Add(_c1cHigh.ToString("f2"));
                    lstl.Add(_c1cLow.ToString("f2"));
                }

                dsavg.Tables[0].Rows.Add(lstr.ToArray());
                if (dsLowAvg != null)
                {
                    if (!string.IsNullOrEmpty(dsLowAvg.Tables[0].Rows[0][0].ToString()))
                    {
                        dsavg.Tables[0].Rows.Add(lstl.ToArray());
                    }
                }
                if (dsHighAvg != null)
                {
                    if (!string.IsNullOrEmpty(dsHighAvg.Tables[0].Rows[0][0].ToString()))
                    {
                        dsavg.Tables[0].Rows.Add(lsth.ToArray());
                    }
                }               

                dgv.DataSource = dsavg.Tables[0];
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
           
                //低温弹性系数平均值 
                _isSelC1L_ = mSel.C1L_;
                //低温弹性系数特征值
                _isSelC1cL = mSel.C1cL;
                //高温弹性系数平均值
                _isSelC1H_ = mSel.C1H_;
                //高温弹性系数特征值
                _isSelC1cH = mSel.C1cH;

                dgv.Refresh();
                dsavg.Dispose();
            }


            public static void CreateCurveFile(string _testpath, Model.GBT282892012_Shear modelShear)
            {
                FileStream fs = new FileStream(_testpath, FileMode.Create, FileAccess.ReadWrite);
                utils.AddText(fs, "testType,testSampleNo,L");
                utils.AddText(fs, "\r\n");
                utils.AddText(fs, "shear," + modelShear.testSampleNo + "," + modelShear.L);
                utils.AddText(fs, "\r\n");
                fs.Flush();
                fs.Close();
                fs.Dispose();
            }
        }

        /// <summary>
        /// 扭转试验
        /// </summary>
        public static class Twist
        {
            //平均值
            private static bool _isSelM_;
            //标准差
            private static bool _isSelSM;
            //横向拉伸特征值
            private static bool _isSelMc;

            public static void readFinishSample(DataGridView dg, DataGridView dgAvg, string testNo, DateTimePicker dtpfrom, ZedGraph.ZedGraphControl zed)
            {
                //try
                //{
                //dg.MultiSelect = true;  
                if (dg != null)
                {
                    dg.DataSource = null;
                    dg.Columns.Clear();
                    dg.RowHeadersVisible = false;
                    dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
                
                BLL.GBT282892012_Twist bllTs = new HR_Test.BLL.GBT282892012_Twist();
                string sqlDate = string.Empty;
                if (!string.IsNullOrEmpty(testNo))
                {
                    sqlDate = " testNo='" + testNo + "' and testDate=#" + dtpfrom.Value.Date + "# ";

                    //if (testNo.Contains('-'))
                    //    testNo = testNo.Substring(0, testNo.LastIndexOf("-"));
                    //获取不重复的试验编号列表
                    DataSet ds = bllTs.GetNotOverlapList(sqlDate);
                    DataTable dt = ds.Tables[0];

                    string methodName = dt.Rows[0]["testMethodName"].ToString();
                    if (dt != null)
                    {
                        if(dg!=null) 
                            dg.DataSource = CreateFinishView(methodName, dt.Rows[0]["testNo"].ToString());
                        dgAvg.DataSource = null;
                        dgAvg.Refresh();
                        CreateAverageView(methodName, dt.Rows[0]["testNo"].ToString(), dgAvg);
                    }
                }
                else
                {
                    DataSet ds = bllTs.GetFinishListFind( sqlDate,0);
                    DataTable dt = ds.Tables[0];
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
                    dg.Name = "twist";
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
                //try
                //{
                //dg.MultiSelect = true;  
                dg.DataSource = null;
                dg.Columns.Clear();
                dg.RowHeadersVisible = false;
                BLL.GBT282892012_Twist bllS = new HR_Test.BLL.GBT282892012_Twist();

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

                BLL.GBT282892012_Twist bllts = new HR_Test.BLL.GBT282892012_Twist();
                DataSet dsmax = bllts.GetFMmax(" isFinish=true and isEffective=false " + strWhere);
                double maxValue = 0;
                if (dsmax != null)
                {
                    if (dsmax.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["FMmax"].ToString()))
                            maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["FMmax"].ToString());
                    }
                }

                DataSet ds = bllS.GetFinishListFind(" isFinish=true and isEffective=false " + strWhere,maxValue);
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
                //dg.Rows[0].Cells[0].Value = false;
                //dg.Rows[0].Cells[0].Selected = false;
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
                //}
                //catch (Exception ee) { throw ee; }
            }


            public static DataTable CreateFinishView(string methodName, string _strTestNo)
            {
                //试验结果选择项
                BLL.GBT282892012_TwistSel bllSel = new HR_Test.BLL.GBT282892012_TwistSel();
                Model.GBT282892012_TwistSel mSel = bllSel.GetModel(methodName);
                StringBuilder strSelcol = new StringBuilder();

                BLL.GBT282892012_Twist bllts = new HR_Test.BLL.GBT282892012_Twist();
                DataSet dsmax = bllts.GetFMmax(" testNo = '" + _strTestNo + "' and isFinish=true  ");
                double maxValue = 0;
                if (dsmax != null)
                {
                    if (dsmax.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["FMmax"].ToString()))
                            maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["FMmax"].ToString());
                    }
                }
                 
                if (mSel != null)
                {
                    int dotValue = utils.Dotvalue(maxValue);
                    if (mSel.FMmax == true)
                    {
                        if (maxValue < 1000.0)
                        {
                            switch (dotValue)
                            {
                                case 2: strSelcol.Append(" FORMAT([FMmax],'0.00') as [FMmax(N)],");
                                    break;
                                case 3: strSelcol.Append(" FORMAT([FMmax],'0.000') as [FMmax(N)],");
                                    break;
                                case 4: strSelcol.Append(" FORMAT([FMmax],'0.0000') as [FMmax(N)],");
                                    break;
                                case 5: strSelcol.Append(" FORMAT([FMmax],'0.00000') as [FMmax(N)],");
                                    break;
                                default: strSelcol.Append(" FORMAT([FMmax],'0.00') as [FMmax(N)],");
                                    break;
                            }
                        }
                        if (maxValue >= 1000.0)
                        {
                            switch (dotValue)
                            {
                                case 2: strSelcol.Append(" FORMAT([FMmax]/1000.0,'0.00') as [FMmax(kN)],");
                                    break;
                                case 3: strSelcol.Append(" FORMAT([FMmax]/1000.0,'0.000') as [FMmax(kN)],");
                                    break;
                                case 4: strSelcol.Append(" FORMAT([FMmax]/1000.0,'0.0000') as [FMmax(kN)],");
                                    break;
                                case 5: strSelcol.Append(" FORMAT([FMmax]/1000.0,'0.00000') as [FMmax(kN)],");
                                    break;
                                default: strSelcol.Append(" FORMAT([FMmax]/1000.0,'0.00') as [FMmax(kN)],");
                                    break;
                            }
                        }
                        //strSelcol.Append(" round([FMmax],2) as [FMmax(kN)],");
                    }

                    if (mSel.M == true)
                    {
                        strSelcol.Append(" round([M],2) as [M(kN·mm)],");
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
                BLL.GBT282892012_TwistSel bllSel = new HR_Test.BLL.GBT282892012_TwistSel();
                Model.GBT282892012_TwistSel mSel = bllSel.GetModel(methodName);
                StringBuilder strSelcol = new StringBuilder();

                BLL.GBT282892012_Twist bllts = new HR_Test.BLL.GBT282892012_Twist();
                DataSet dsmax = bllts.GetFMmax(strWhere);
                double maxValue = 0;
                if (dsmax != null)
                {
                    if (dsmax.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["FMmax"].ToString()))
                            maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["FMmax"].ToString());
                    }
                }
                if (mSel != null)
                {
                    if (mSel.FMmax == true)
                    {
                        int dotValue = utils.Dotvalue(maxValue);
                        if (maxValue < 1000.0)
                        {
                            switch (dotValue)
                            {
                                case 2: strSelcol.Append(" FORMAT([FMmax],'0.00') as [FMmax(N)],");
                                    break;
                                case 3: strSelcol.Append(" FORMAT([FMmax],'0.000') as [FMmax(N)],");
                                    break;
                                case 4: strSelcol.Append(" FORMAT([FMmax],'0.0000') as [FMmax(N)],");
                                    break;
                                case 5: strSelcol.Append(" FORMAT([FMmax],'0.00000') as [FMmax(N)],");
                                    break;
                                default: strSelcol.Append(" FORMAT([FMmax],'0.00') as [FMmax(N)],");
                                    break;
                            }
                        }
                        if (maxValue >= 1000.0)
                        {
                            switch (dotValue)
                            {
                                case 2: strSelcol.Append(" FORMAT([FMmax]/1000.0,'0.00') as [FMmax(kN)],");
                                    break;
                                case 3: strSelcol.Append(" FORMAT([FMmax]/1000.0,'0.000') as [FMmax(kN)],");
                                    break;
                                case 4: strSelcol.Append(" FORMAT([FMmax]/1000.0,'0.0000') as [FMmax(kN)],");
                                    break;
                                case 5: strSelcol.Append(" FORMAT([FMmax]/1000.0,'0.00000') as [FMmax(kN)],");
                                    break;
                                default: strSelcol.Append(" FORMAT([FMmax]/1000.0,'0.00') as [FMmax(kN)],");
                                    break;
                            }
                        }
                        //strSelcol.Append(" round([FMmax],4) as [FMmax(kN)],");
                    }

                    if (mSel.M == true)
                    {
                        strSelcol.Append(" round([M],2) as [M(kN·mm)],");
                    }
                }

                string _selCol = strSelcol.ToString();
                if (_selCol.Length > 0)
                    _selCol = _selCol.Remove(_selCol.Length - 1, 1);
                 
                DataSet dst = bllts.GetFinishListReport(_selCol,strWhere);
                return dst.Tables[0];
            }

            public static void CreateAverageView(string methodName, string _strTestNo, DataGridView datagridview)
            {
                //试验结果选择项

                double avgM = 0;
                double SM = 0;
                double Mc = 0;
                int fcount = 0;
                BLL.GBT282892012_TwistSel bllSel = new HR_Test.BLL.GBT282892012_TwistSel();
                Model.GBT282892012_TwistSel mSel = bllSel.GetModel(methodName);

                if (mSel != null)
                {
                    _isSelM_ = mSel.M_;
                    _isSelSM = mSel.SM;
                    _isSelMc = mSel.Mc;
                }

                BLL.GBT282892012_Twist bllts = new HR_Test.BLL.GBT282892012_Twist();

                //查询 TestNo下 试验完成的试样Q—的平均值
                DataSet dsavg = bllts.GetFinishAvg(" testNo='" + _strTestNo + "'  and isFinish=true and isEffective=false ");
                DataSet dsavgView = new DataSet();
                dsavgView.Tables.Add();
                if (dsavg.Tables[0].Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dsavg.Tables[0].Rows[0][0].ToString()))
                        return;
                    avgM = Convert.ToDouble(dsavg.Tables[0].Rows[0][0].ToString());
                }

                //求标准偏差
                DataSet dsfinish = bllts.GetList(" testNo = '" + _strTestNo + "' and isFinish=true  and isEffective=false ");
                fcount = dsfinish.Tables[0].Rows.Count;

                //添加第0列
                dsavgView.Tables[0].Columns.Add("试样数量", System.Type.GetType("System.String"));
                //设置为第0列
                dsavgView.Tables[0].Columns["试样数量"].SetOrdinal(0);

                //添加第1列
                dsavgView.Tables[0].Columns.Add("Mˉ(N/mm)", System.Type.GetType("System.String"));
                //设置为第1列
                dsavgView.Tables[0].Columns["Mˉ(N/mm)"].SetOrdinal(1);

                //添加第0列
                dsavgView.Tables[0].Columns.Add("SM(N/mm)", System.Type.GetType("System.String"));
                //设置为第0列
                dsavgView.Tables[0].Columns["SM(N/mm)"].SetOrdinal(2);

                //添加第1列
                dsavgView.Tables[0].Columns.Add("Mc(N/mm)", System.Type.GetType("System.String"));
                //设置为第1列
                dsavgView.Tables[0].Columns["Mc(N/mm)"].SetOrdinal(3);

                double sumsq = 0;
                double ctrue = 0;

                //计算标准偏差
                if (avgM != 0)
                {
                    for (int i = 0; i < dsfinish.Tables[0].Rows.Count; i++)
                    {
                        ctrue = Convert.ToDouble(dsfinish.Tables[0].Rows[i]["M"].ToString());
                        double c1 = (ctrue - avgM) * (ctrue - avgM);
                        sumsq += c1;
                    }
                    //标准偏差
                    if (fcount - 1 != 0)
                        SM = Math.Sqrt(sumsq / (fcount - 1));
                    else
                        SM = 0;

                    //计算Mc
                    Mc = avgM - 2.02d * SM;
                }


                //平均值行
                object[] row = { fcount.ToString(), avgM.ToString("f2"), SM.ToString("f2"), Mc.ToString("f2") };
                dsavgView.Tables[0].Rows.Add(row);
                datagridview.DataSource = dsavgView.Tables[0];

                //S.M.标准偏差行 
                if (!_isSelSM) { datagridview.Columns[2].Visible = false; }
                //抗剪特征值
                if (!_isSelMc) { datagridview.Columns[3].Visible = false; }
                datagridview.Update();
                datagridview.Refresh();
                dsfinish.Dispose();
            }


            public static void CreateAverageViewReport(string methodName, string strWhere, DataGridView datagridview)
            {
                //试验结果选择项

                double avgM = 0;
                double SM = 0;
                double Mc = 0;
                int fcount = 0;
                BLL.GBT282892012_TwistSel bllSel = new HR_Test.BLL.GBT282892012_TwistSel();
                Model.GBT282892012_TwistSel mSel = bllSel.GetModel(methodName);

                if (mSel != null)
                {
                    _isSelM_ = mSel.M_;
                    _isSelSM = mSel.SM;
                    _isSelMc = mSel.Mc;
                }

                BLL.GBT282892012_Twist bllts = new HR_Test.BLL.GBT282892012_Twist();

                //查询 TestNo下 试验完成的试样Q—的平均值
                DataSet dsavg = bllts.GetFinishAvg(strWhere);
                DataSet dsavgView = new DataSet();
                dsavgView.Tables.Add();
                if (dsavg.Tables[0].Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dsavg.Tables[0].Rows[0][0].ToString()))
                        return;
                    avgM = Convert.ToDouble(dsavg.Tables[0].Rows[0][0].ToString());
                }

                //求标准偏差
                DataSet dsfinish = bllts.GetList(strWhere);
                fcount = dsfinish.Tables[0].Rows.Count;

                //添加第0列
                dsavgView.Tables[0].Columns.Add("试样数量", System.Type.GetType("System.String"));
                //设置为第0列
                dsavgView.Tables[0].Columns["试样数量"].SetOrdinal(0);

                //添加第1列
                dsavgView.Tables[0].Columns.Add("Mˉ(N/mm)", System.Type.GetType("System.String"));
                //设置为第1列
                dsavgView.Tables[0].Columns["Mˉ(N/mm)"].SetOrdinal(1);

                //添加第0列
                dsavgView.Tables[0].Columns.Add("SM(N/mm)", System.Type.GetType("System.String"));
                //设置为第0列
                dsavgView.Tables[0].Columns["SM(N/mm)"].SetOrdinal(2);

                //添加第1列
                dsavgView.Tables[0].Columns.Add("Mc(N/mm)", System.Type.GetType("System.String"));
                //设置为第1列
                dsavgView.Tables[0].Columns["Mc(N/mm)"].SetOrdinal(3);

                double sumsq = 0;
                double ctrue = 0;

                //计算标准偏差
                if (avgM != 0)
                {
                    for (int i = 0; i < dsfinish.Tables[0].Rows.Count; i++)
                    {
                        ctrue = Convert.ToDouble(dsfinish.Tables[0].Rows[i]["M"].ToString());
                        double c1 = (ctrue - avgM) * (ctrue - avgM);
                        sumsq += c1;
                    }
                    //标准偏差
                    if (fcount - 1 != 0)
                        SM = Math.Sqrt(sumsq / (fcount - 1));
                    else
                        SM = 0;

                    //计算Mc
                    Mc = avgM - 2.02d * SM;
                }


                //平均值行
                object[] row = { fcount.ToString(), avgM.ToString("f2"), SM.ToString("f2"), Mc.ToString("f2") };
                dsavgView.Tables[0].Rows.Add(row);
                datagridview.DataSource = dsavgView.Tables[0];

                //S.M.标准偏差行 
                if (!_isSelSM) { datagridview.Columns[2].Visible = false; }
                //抗剪特征值
                if (!_isSelMc) { datagridview.Columns[3].Visible = false; }
                datagridview.Update();
                datagridview.Refresh();
                dsfinish.Dispose();
            }



            public static void CreateCurveFile(string _testpath, Model.GBT282892012_Twist modelTwist)
            {
                FileStream fs = new FileStream(_testpath, FileMode.Create, FileAccess.ReadWrite);
                utils.AddText(fs, "testType,testSampleNo,L");
                utils.AddText(fs, "\r\n");
                utils.AddText(fs, "twist," + modelTwist.testSampleNo + "," + modelTwist.L);
                utils.AddText(fs, "\r\n");
                fs.Flush();
                fs.Close();
                fs.Dispose();
            }

        }

    }
}
