using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using ZedGraph;
using System.IO;

namespace HR_Test.TestStandard
{
    class GBT3354_2014
    {
        /// <summary>
        /// 原始曲线数据
        /// </summary>
        private static List<gdata> _listData;
        public static List<gdata> _ListData
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

        public static void readFinishSample(DataGridView dg, DataGridView dgSum, string testNo, DateTimePicker dtp, ZedGraph.ZedGraphControl zed)
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
            BLL.GBT3354_Samples bllTs = new HR_Test.BLL.GBT3354_Samples();
            if (!string.IsNullOrEmpty(testNo))
            {
                //if (testNo.Contains('-'))
                //    testNo = testNo.Substring(0, testNo.LastIndexOf("-"));
                //获取不重复的试验编号列表
                DataSet ds = bllTs.GetNotOverlapList(" testNo='" + testNo + "' and testDate =#" + dtp.Value.Date + "#");
                DataTable dt = ds.Tables[0];
                int ab = 0;
                if (dt != null)
                {
                    if (dt.Rows[0]["w"].ToString() != "0")
                        ab = 1;
                    if (dt.Rows[0]["d0"].ToString() != "0")
                        ab = 2;
                    if (dt.Rows[0]["Do"].ToString() != "0")
                        ab = 3;
                }

                DataSet ds1 = bllTs.GetNotOverlapList1(" testNo='" + testNo + "' and testDate =#" + dtp.Value.Date + "#");
                DataTable dt1 = ds1.Tables[0];

                DataSet dsmax = bllTs.GetMaxFm(" testNo='" + testNo + "' and testDate =#" + dtp.Value.Date + "#");
                double maxvalue = 0;
                if (dsmax != null)
                {
                    if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["Pmax"].ToString()))
                        maxvalue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["Pmax"].ToString());
                }
                string methodName = dt1.Rows[0]["testMethodName"].ToString();
                if (dt1 != null)
                {
                    StringBuilder[] tst = strSql_T(methodName, maxvalue);
                    if (!string.IsNullOrEmpty(tst[0].ToString()) && dg != null)
                        dg.DataSource = CreateView_T(tst[0].ToString(), dt1.Rows[0]["testNo"].ToString(), ab);
                    if (!string.IsNullOrEmpty(tst[1].ToString()))
                        dgSum.DataSource = CreateAverageView(tst[0], tst[1], dt1.Rows[0]["testNo"].ToString(), ab);
                }
            }
            else
            {
                DataSet ds = bllTs.GetFinishList(" testNo='" + testNo + "' and testDate=#" + dtp.Value.Date + "#", 0);
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
                if (dg != null)
                    dg.DataSource = dt;
                ds.Dispose();
            } 

            DataGridViewCheckBoxColumn chkcol = new DataGridViewCheckBoxColumn();
            chkcol.Name = "选择";
            chkcol.MinimumWidth = 50;
            chkcol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //按钮
            //DataGridViewButtonColumn btncol = new DataGridViewButtonColumn();
            //btncol.Name = "失效模式";
            //btncol.MinimumWidth = 100;
            //btncol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            

            if (dg != null)
            {
                DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
                c.Name = "";
                dg.Columns.Insert(0, chkcol);
                dg.Columns.Insert(1, c);
                //dg.Columns.Insert(4, btncol);

                dg.Name = "tensile"; 
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
                if (dg.Columns.Count > 3)
                {
                    dg.Columns[0].Frozen = true;
                    dg.Columns[1].Frozen = true;
                    dg.Columns[2].Frozen = true;
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


        //以选择的结果为准
        public static StringBuilder[] strSql_T(string methodName, double maxValue)
        {
            BLL.GBT3354_Sel bllSel = new HR_Test.BLL.GBT3354_Sel();
            Model.GBT3354_Sel mSel = bllSel.GetModel(methodName);
            StringBuilder[] strSql_t = new StringBuilder[2];
            //所选择的试验结果列
            StringBuilder strSelcol = new StringBuilder();
            //平均值
            StringBuilder strSelcolAver = new StringBuilder();
            //生成"平均值"的 查询语句 
            if (mSel != null)
            {
                int dotvalue = utils.Dotvalue(maxValue);
                if (mSel.Pmax == true)
                {
                    if (maxValue < 1000.0)
                    {
                        switch (dotvalue)
                        {
                            case 2:
                                strSelcol.Append(" Format([Pmax],'0.00') as [Pmax(N)],");
                                strSelcolAver.Append(" Format(Avg([Pmax]),'0.00') as [Pmax(N)],");
                                break;
                            case 3:
                                strSelcol.Append(" Format([Pmax],'0.000') as [Pmax(N)],");
                                strSelcolAver.Append(" Format(Avg([Pmax]),'0.000') as [Pmax(N)],");
                                break;
                            case 4:
                                strSelcol.Append(" Format([Pmax],'0.0000') as [Pmax(N)],");
                                strSelcolAver.Append(" Format(Avg([Pmax]),'0.0000') as [Pmax(N)],");
                                break;
                            case 5:
                                strSelcol.Append(" Format([Pmax],'0.00000') as [Pmax(N)],");
                                strSelcolAver.Append(" Format(Avg([Pmax]),'0.00000') as [Pmax(N)],");
                                break;
                            default:
                                strSelcol.Append(" Format([Pmax],'0.00') as [Pmax(N)],");
                                strSelcolAver.Append(" Format(Avg([Pmax]),'0.00') as [Pmax(N)],");
                                break;
                        }

                    }
                    if (maxValue >= 1000.0)
                    {
                        switch (dotvalue)
                        {
                            case 2:
                                strSelcol.Append(" Format([Pmax]/1000.0,'0.00') as [Pmax(kN)],");
                                strSelcolAver.Append(" Format(Avg([Pmax])/1000.0,'0.00') as [Pmax(kN)],");
                                break;
                            case 3:
                                strSelcol.Append(" Format([Pmax]/1000.0,'0.000') as [Pmax(kN)],");
                                strSelcolAver.Append(" Format(Avg([Pmax])/1000.0,'0.000') as [Pmax(kN)],");
                                break;
                            case 4:
                                strSelcol.Append(" Format([Pmax]/1000.0,'0.0000') as [Pmax(kN)],");
                                strSelcolAver.Append(" Format(Avg([Pmax])/1000.0,'0.0000') as [Pmax(kN)],");
                                break;
                            case 5:
                                strSelcol.Append(" Format([Pmax]/1000.0,'0.00000') as [Pmax(kN)],");
                                strSelcolAver.Append(" Format(Avg([Pmax])/1000.0,'0.00000') as [Pmax(kN)],");
                                break;
                            default:
                                strSelcol.Append(" Format([Pmax]/1000.0,'0.00') as [Pmax(kN)],");
                                strSelcolAver.Append(" Format(Avg([Pmax])/1000.0,'0.00') as [Pmax(kN)],");
                                break;
                        }
                    }
                }

                if (mSel.σt == true)
                {
                    strSelcol.Append(" round([σt],2) as [σt(MPa)],");
                    strSelcolAver.Append(" round(Avg([σt]),2) as [σt(MPa)],");
                }
                if (mSel.Et == true)
                {
                    strSelcol.Append(" [Et] as [Et(GPa)],");
                    strSelcolAver.Append(" round(Avg([Et]),2) as [Et(GPa)],"); //strSelcolSD.Append("0.001,");strSelcolSD.Append(" round((SUM([ReH])-MAX([ReH])-MIN([ReH]))/(COUNT(*)-2),2) as [ReH],");
                }

                if (mSel.μ12 == true)
                {
                    strSelcol.Append(" [μ12] as [μ12],");
                    strSelcolAver.Append(" round(Avg([μ12]),2) as [μ12],");
                }//strSelcolSD.Append("0.001,");strSelcolSD.Append(" round((SUM([ReL])-MAX([ReL])-MIN([ReL]))/(COUNT(*)-2),2) as [ReL],");
                if (mSel.ε1t == true)
                {
                    strSelcol.Append(" [ε1t] as [ε1t],");
                    strSelcolAver.Append(" round(Avg([ε1t]),2) as [ε1t],");
                }// strSelcolSD.Append("0.001,");strSelcolSD.Append(" round((SUM([Rp])-MAX([Rp])-MIN([Rp]))/(COUNT(*)-2),2) as [Rp],"); }

                _isSelSD = mSel.SD;
                _isSelMid = mSel.Mid;
                _isSelCV = mSel.CV;

            }

            if (strSelcolAver.Length > 0)
                strSelcolAver = strSelcolAver.Remove(strSelcolAver.Length - 1, 1);

            if (strSelcol.Length > 0)
                strSelcol = strSelcol.Remove(strSelcol.Length - 1, 1);

            //if (strSelcolSD != null)
            //    strSelcolSD = strSelcolSD.Remove(strSelcolSD.Length - 1, 1);

            strSql_t[0] = strSelcol;
            strSql_t[1] = strSelcolAver;
            //这里应该修改为 C.V.变异系数的计算结果
            //strSql_t[2] = strSelcolSD;
            return strSql_t;

        }

        //列表
        private static DataTable CreateView_T(string _selCol, string _strTestNo, int ab)
        {
            BLL.GBT3354_Samples bllts = new HR_Test.BLL.GBT3354_Samples();
            DataSet dst = bllts.GetFinishList1(_selCol, " testNo = '" + _strTestNo + "' and isFinish=true ", ab);
            return dst.Tables[0];
        }

        //求取平均值 X~,S,V
        private static DataTable CreateAverageView(StringBuilder _selCol, StringBuilder _selSqlAver, string _strTestNo, int ab)
        {
            BLL.GBT3354_Samples bllts = new HR_Test.BLL.GBT3354_Samples();
            string aver = _selSqlAver.ToString();
            //求平均
            DataSet dsaver = bllts.GetFinishSumList1(aver, " testNo = '" + _strTestNo + "' and isFinish=true and isEffective=false ");

            //获取选择试样的数量和试样编号

            string selCol = _selCol.ToString();
            DataSet dsfinish = bllts.GetFinishList1(selCol.ToString(), " testNo = '" + _strTestNo + "' and isFinish=true  and isEffective=false ", ab);

            int fcount = dsfinish.Tables[0].Rows.Count;

            //变异系数 = sd/mean 
            StringBuilder strCV = new StringBuilder();

            //中间值
            StringBuilder strMid = new StringBuilder();

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
                    objmean.Add(dsaver.Tables[0].Rows[0][i].ToString());
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
            for (int i = 0; i < dsaver.Tables[0].Columns.Count; i++)
            {
                for (int j = 0; j < dsfinish.Tables[0].Rows.Count; j++)
                {
                    //  求和 (真实值-平均值)^2 
                    if (ab == 1 || ab == 3)
                        ctrue = Convert.ToDouble(dsfinish.Tables[0].Rows[j][i + 6].ToString());
                    if (ab == 2)
                        ctrue = Convert.ToDouble(dsfinish.Tables[0].Rows[j][i + 5].ToString());
                    caver = Convert.ToDouble(dsaver.Tables[0].Rows[0][i].ToString());
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


        public static void CreateCurveFile(string _testpath, Model.GBT3354_Samples modelTensile)
        {
            FileStream fs = new FileStream(_testpath, FileMode.Create, FileAccess.ReadWrite);
            utils.AddText(fs, "testType,testSampleNo,S0,lL,lT,εz1,εz2");
            utils.AddText(fs, "\r\n");
            utils.AddText(fs, "tensile," + modelTensile.testSampleNo + "," + modelTensile.S0 + "," + modelTensile.lL + "," + modelTensile.lT + "," + modelTensile.εz1 + "," + modelTensile.εz2);
            utils.AddText(fs, "\r\n");
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

        //列表
        public static DataTable CreateViewReport(string selCol, string strTestSampleNo, int ab)
        {
            BLL.GBT3354_Samples bllts = new HR_Test.BLL.GBT3354_Samples();
            selCol = selCol.Substring(0, selCol.LastIndexOf(','));
            DataSet dst = bllts.GetFinishListReport(selCol, " testSampleNo in ('" + strTestSampleNo + ") and isFinish=true ", ab);
            return dst.Tables[0];
        }

        public static DataTable CreateAverageViewReport(StringBuilder _selSqlAver, StringBuilder _strSelcol, string _arrTestSample, int ab)
        {

            BLL.GBT3354_Samples bllts = new HR_Test.BLL.GBT3354_Samples();
            //求平均
            DataSet dsaver = bllts.GetFinishSumList1(_selSqlAver.ToString(), " testSampleNo in ('" + _arrTestSample + ") and isFinish=true ");

            //获取选择试样的数量和试样编号 
            string selCol = _strSelcol.ToString();
            selCol = selCol.Substring(0, selCol.LastIndexOf(','));

            DataSet dsfinish = bllts.GetFinishList1(selCol, " testSampleNo in ('" + _arrTestSample + ") and isFinish=true ", ab);

            int fcount = dsfinish.Tables[0].Rows.Count;

            //变异系数 = sd/mean 
            StringBuilder strCV = new StringBuilder();

            //中间值
            StringBuilder strMid = new StringBuilder();


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
                if (double.Parse(dsaver.Tables[0].Rows[0][i].ToString()) != 0)
                    objmean.Add(dsaver.Tables[0].Rows[0][i].ToString());
                else
                    objmean.Add("-");
            }
            dsaverto.Tables[0].Rows.Add(objmean.ToArray());
            double sumsd = 0;
            double sd = 0;
            double cv = 0;
            double ctrue = 0;
            double caver = 0;
            double temp = 1000000;
            double mid = 0;

            List<object> objsd = new List<object>();
            objsd.Add("S.D.:");
            List<object> objcv = new List<object>();
            objcv.Add("C.V.:");
            List<object> objmid = new List<object>();
            objmid.Add("Mid.:");


            //计算标准偏差
            for (int i = 0; i < dsaver.Tables[0].Columns.Count; i++)
            {
                for (int j = 0; j < dsfinish.Tables[0].Rows.Count; j++)
                {
                    //  求和 (真实值-平均值)^2 
                    if (ab == 2)
                    {
                        ctrue = Convert.ToDouble(dsfinish.Tables[0].Rows[j][i + 5].ToString());
                        caver = Convert.ToDouble(dsaver.Tables[0].Rows[0][i].ToString());
                    }
                    else
                    {
                        ctrue = Convert.ToDouble(dsfinish.Tables[0].Rows[j][i + 6].ToString());
                        caver = Convert.ToDouble(dsaver.Tables[0].Rows[0][i].ToString());
                    }
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


                temp = 1000000;
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


        public static void readFinishSampleReport(DataGridView dg, string testNo, string testSampleNo, DateTimePicker dtpfrom, DateTimePicker dtpto, ZedGraph.ZedGraphControl zed)
        {
            BLL.GBT3354_Samples bllTs = new HR_Test.BLL.GBT3354_Samples();
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(testNo))
            {
                strWhere += " and testNo like '%" + testNo + "%'";
            }

            if (!string.IsNullOrEmpty(testSampleNo))
            {
                strWhere += " and testSampleNo like '%" + testSampleNo + "%'";
            }

            double maxValue = 0;
            DataSet dsmax = bllTs.GetMaxFm("isFinish=true and isEffective=false and testDate>=#" + dtpfrom.Value.Date + "# and testDate<=#" + dtpto.Value.Date + "#" + strWhere);
            if (dsmax != null)
            {
                if (dsmax.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["Pmax"].ToString()))
                        maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["Pmax"].ToString());
                }
            }

            DataSet ds = bllTs.GetFinishList("isFinish=true and isEffective=false and testDate>=#" + dtpfrom.Value.Date + "# and testDate<=#" + dtpto.Value.Date + "#" + strWhere, maxValue);
            DataTable dt = ds.Tables[0];
            dg.DataSource = dt;

            DataGridViewCheckBoxColumn chkcol = new DataGridViewCheckBoxColumn();
            //DataGridViewDisableCheckBoxColumn chkcol = new DataGridViewDisableCheckBoxColumn();            
            chkcol.Name = "选择";

            DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
            c.Visible = true;
            c.Name = "  ";
            dg.Columns.Insert(0, c);
            dg.Columns.Insert(0, chkcol);

            dg.Columns[1].Frozen = true;
            dg.Columns[2].Frozen = true;

            //dg.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);  
            dg.Columns[0].Width = 50;
            dg.Columns[1].Width = 50;
            dg.Columns[2].Width = 100;
            if (dg.ColumnCount > 0)
            {
                foreach (DataGridViewColumn dgvc in dg.Columns)
                {
                    dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvc.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            dg.Refresh();

            for (int i = 0; i < dg.Rows.Count; i++)
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
                    dg.Rows[i].Cells[1].Style.SelectionForeColor = Color.FromName(_Color_Array[i]);
                    dg.Rows[i].Cells[1].Style.SelectionBackColor = Color.FromName(_Color_Array[i]);
                    dg.Rows[i].Cells[1].Value = _Color_Array[i].ToString();
                }
            }

            foreach (CurveItem ci in zed.GraphPane.CurveList)
            {
                ci.Clear();
                ci.Label.Text = "";
            }
            zed.AxisChange();
            zed.Refresh();
            ds.Dispose();
        }

        public static void CalcResult(List<gdata> lGdata,double z1,double z2, out double m_Fmax, out int FmaxIndex,out int indexYB1,out int indexYB2)
        {
            m_Fmax = 0;
            FmaxIndex = 0;
            indexYB1 = 0;
            indexYB2 = 0;
            if (lGdata != null)
            {
                int lCount = lGdata.Count;
                for (int i = 1; i < lCount; i++)
                {
                    float load = (float)lGdata[i].F1;

                    if (z1 > lGdata[i].YB1)
                        indexYB1 = i;
                    if (z2 > lGdata[i].YB1)
                        indexYB2 = i;

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

     }
}
