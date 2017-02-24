using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.IO;
using ZedGraph;
namespace HR_Test.TestStandard
{
    class GBT7314_2005
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
            BLL.Compress bllTs = new HR_Test.BLL.Compress();

            if (!string.IsNullOrEmpty(testNo))
            {
                //if (testNo.Contains('-'))
                //    testNo = testNo.Substring(0, testNo.LastIndexOf("-"));
                //获取不重复的试验编号列表
                DataSet ds = bllTs.GetNotOverlapList(" testNo='" + testNo + "' and testDate =#" + dtp.Value.Date + "#");
                DataSet dsMax = bllTs.GetMaxFmc(" testNo='" + testNo + "' and isEffective=false and testDate =#" + dtp.Value.Date + "#");
                double maxValue = 0;
                if (dsMax != null)
                {
                    if(!string.IsNullOrEmpty(dsMax.Tables[0].Rows[0]["Fmc"].ToString()))
                        maxValue = Convert.ToDouble(dsMax.Tables[0].Rows[0]["Fmc"].ToString());
                }


                DataTable dt = ds.Tables[0];
                int ab = 0;
                if (dt != null)
                {
                    //矩形
                    if (dt.Rows[0]["a"].ToString() != "0")
                        ab = 1;
                    //圆柱形
                    if (dt.Rows[0]["d"].ToString() != "0")
                        ab = 2;
                }

                DataSet ds1 = bllTs.GetNotOverlapList1(" testNo='" + testNo + "' and testDate =#" + dtp.Value.Date + "#");
                DataTable dt1 = ds1.Tables[0];
                string methodName = dt1.Rows[0]["testMethodName"].ToString();
                if (dt1 != null)
                {
                    StringBuilder[] tst = strSql_T(methodName, maxValue);
                    if (!string.IsNullOrEmpty(tst[0].ToString()) && dg != null)
                        dg.DataSource = CreateView(tst[0].ToString(), dt1.Rows[0]["testNo"].ToString(), ab);
                    if (!string.IsNullOrEmpty(tst[1].ToString()))
                        dgSum.DataSource = CreateAverageView(tst[0], tst[1], dt1.Rows[0]["testNo"].ToString(), ab);
                }
            }
            else
            {
                DataSet ds = bllTs.GetFinishList(" testNo='" + testNo + "' and testDate=#" + dtp.Value.Date + "#",0);
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
                if (dg != null)
                    dg.DataSource = dt;
                ds.Dispose();
            }


            //DataGridViewDisableCheckBoxColumn chkcol = new DataGridViewDisableCheckBoxColumn();
            DataGridViewCheckBoxColumn chkcol = new DataGridViewCheckBoxColumn();
            chkcol.Name = "选择";
            chkcol.MinimumWidth = 50;
            chkcol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
            c.Name = "";
            if (dg != null)
            {
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
            //}
            //catch (Exception ee) { throw ee; }
        }

        //以选择的结果为准
        public static StringBuilder[] strSql_T(string methodName, double maxValue)
        {
            BLL.SelTestResult_C bllSel = new HR_Test.BLL.SelTestResult_C();
            Model.SelTestResult_C mSel = bllSel.GetModel(methodName);
            StringBuilder[] strSql_t = new StringBuilder[2];
            //所选择的试验结果列
            StringBuilder strSelcol = new StringBuilder();
            //平均值
            StringBuilder strSelcolAver = new StringBuilder();
            //生成"平均值"的 查询语句 
            if (mSel != null)
            {                
                int dotvalue = utils.Dotvalue(maxValue);
                if (maxValue < 1000.0)
                {
                    switch (dotvalue)
                    {
                        case 2:
                            if (mSel.Fmc == true)
                            {
                                strSelcol.Append(" FORMAT([Fmc],'0.00') as [Fmc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fmc]),'0.00') as [Fmc(N)],");
                            }
                            if (mSel.Fpc == true)
                            {
                                strSelcol.Append(" FORMAT([Fpc],'0.00') as [Fpc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpc]),'0.00') as [Fpc(N)],");
                            }
                            if (mSel.Ftc == true)
                            {
                                strSelcol.Append(" FORMAT([Ftc],'0.00') as [Ftc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Ftc]),'0.00') as [Ftc(N)],");
                            }
                            if (mSel.FeHc == true)
                            {
                                strSelcol.Append(" FORMAT([FeHc],'0.00') as [FeHc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeHc]),'0.00') as [FeHc(N)],");
                            }
                            if (mSel.FeLc == true)
                            {
                                strSelcol.Append(" FORMAT([FeLc],'0.00') as [FeLc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeLc]),'0.00') as [FeLc(N)],");
                            }
                            break;
                        case 3:
                            if (mSel.Fmc == true)
                            {
                                strSelcol.Append(" FORMAT([Fmc],'0.000') as [Fmc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fmc]),'0.000') as [Fmc(N)],");
                            }
                            if (mSel.Fpc == true)
                            {
                                strSelcol.Append(" FORMAT([Fpc],'0.000') as [Fpc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpc]),'0.000') as [Fpc(N)],");
                            }
                            if (mSel.Ftc == true)
                            {
                                strSelcol.Append(" FORMAT([Ftc],'0.000') as [Ftc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Ftc]),'0.000') as [Ftc(N)],");
                            }
                            if (mSel.FeHc == true)
                            {
                                strSelcol.Append(" FORMAT([FeHc],'0.000') as [FeHc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeHc]),'0.000') as [FeHc(N)],");
                            }
                            if (mSel.FeLc == true)
                            {
                                strSelcol.Append(" FORMAT([FeLc],'0.000') as [FeLc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeLc]),'0.000') as [FeLc(N)],");
                            }
                            break;
                        case 4:
                            if (mSel.Fmc == true)
                            {
                                strSelcol.Append(" FORMAT([Fmc],'0.0000') as [Fmc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fmc]),'0.0000') as [Fmc(N)],");
                            }
                            if (mSel.Fpc == true)
                            {
                                strSelcol.Append(" FORMAT([Fpc],'0.0000') as [Fpc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpc]),'0.0000') as [Fpc(N)],");
                            }
                            if (mSel.Ftc == true)
                            {
                                strSelcol.Append(" FORMAT([Ftc],'0.0000') as [Ftc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Ftc]),'0.0000') as [Ftc(N)],");
                            }
                            if (mSel.FeHc == true)
                            {
                                strSelcol.Append(" FORMAT([FeHc],'0.0000') as [FeHc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeHc]),'0.0000') as [FeHc(N)],");
                            }
                            if (mSel.FeLc == true)
                            {
                                strSelcol.Append(" FORMAT([FeLc],'0.0000') as [FeLc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeLc]),'0.0000') as [FeLc(N)],");
                            }
                            break;
                        case 5:
                            if (mSel.Fmc == true)
                            {
                                strSelcol.Append(" FORMAT([Fmc],'0.00000') as [Fmc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fmc]),'0.00000') as [Fmc(N)],");
                            }
                            if (mSel.Fpc == true)
                            {
                                strSelcol.Append(" FORMAT([Fpc],'0.00000') as [Fpc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpc]),'0.00000') as [Fpc(N)],");
                            }
                            if (mSel.Ftc == true)
                            {
                                strSelcol.Append(" FORMAT([Ftc],'0.00000') as [Ftc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Ftc]),'0.00000') as [Ftc(N)],");
                            }
                            if (mSel.FeHc == true)
                            {
                                strSelcol.Append(" FORMAT([FeHc],'0.00000') as [FeHc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeHc]),'0.00000') as [FeHc(N)],");
                            }
                            if (mSel.FeLc == true)
                            {
                                strSelcol.Append(" FORMAT([FeLc],'0.00000') as [FeLc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeLc]),'0.00000') as [FeLc(N)],");
                            }
                            break;
                        default:
                            if (mSel.Fmc == true)
                            {
                                strSelcol.Append(" FORMAT([Fmc],'0.00') as [Fmc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fmc]),'0.00') as [Fmc(N)],");
                            }
                            if (mSel.Fpc == true)
                            {
                                strSelcol.Append(" FORMAT([Fpc],'0.00') as [Fpc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpc]),'0.00') as [Fpc(N)],");
                            }
                            if (mSel.Ftc == true)
                            {
                                strSelcol.Append(" FORMAT([Ftc],'0.00') as [Ftc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([Ftc]),'0.00') as [Ftc(N)],");
                            }
                            if (mSel.FeHc == true)
                            {
                                strSelcol.Append(" FORMAT([FeHc],'0.00') as [FeHc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeHc]),'0.00') as [FeHc(N)],");
                            }
                            if (mSel.FeLc == true)
                            {
                                strSelcol.Append(" FORMAT([FeLc],'0.00') as [FeLc(N)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeLc]),'0.00') as [FeLc(N)],");
                            }
                            break;
                    }

                }
                if (maxValue >= 1000.0)
                {
                    switch (dotvalue)
                    {
                        case 2:
                            if (mSel.Fmc == true)
                            {
                                strSelcol.Append(" FORMAT([Fmc]/1000.0,'0.00') as [Fmc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fmc]/1000.0),'0.00') as [Fmc(kN)],");
                            }
                            if (mSel.Fpc == true)
                            {
                                strSelcol.Append(" FORMAT([Fpc]/1000.0,'0.00') as [Fpc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpc]/1000.0),'0.00') as [Fpc(kN)],");
                            }
                            if (mSel.Ftc == true)
                            {
                                strSelcol.Append(" FORMAT([Ftc]/1000.0,'0.00') as [Ftc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Ftc]/1000.0),'0.00') as [Ftc(kN)],");
                            }
                            if (mSel.FeHc == true)
                            {
                                strSelcol.Append(" FORMAT([FeHc]/1000.0,'0.00') as [FeHc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeHc]/1000.0),'0.00') as [FeHc(kN)],");
                            }
                            if (mSel.FeLc == true)
                            {
                                strSelcol.Append(" FORMAT([FeLc]/1000.0,'0.00') as [FeLc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeLc]/1000.0),'0.00') as [FeLc(kN)],");
                            }
                            break;
                        case 3:
                            if (mSel.Fmc == true)
                            {
                                strSelcol.Append(" FORMAT([Fmc]/1000.0,'0.000') as [Fmc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fmc]/1000.0),'0.000') as [Fmc(kN)],");
                            }
                            if (mSel.Fpc == true)
                            {
                                strSelcol.Append(" FORMAT([Fpc]/1000.0,'0.000') as [Fpc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpc]/1000.0),'0.000') as [Fpc(kN)],");
                            }
                            if (mSel.Ftc == true)
                            {
                                strSelcol.Append(" FORMAT([Ftc]/1000.0,'0.000') as [Ftc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Ftc]/1000.0),'0.000') as [Ftc(kN)],");
                            }
                            if (mSel.FeHc == true)
                            {
                                strSelcol.Append(" FORMAT([FeHc]/1000.0,'0.000') as [FeHc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeHc]/1000.0),'0.000') as [FeHc(kN)],");
                            }
                            if (mSel.FeLc == true)
                            {
                                strSelcol.Append(" FORMAT([FeLc]/1000.0,'0.000') as [FeLc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeLc]/1000.0),'0.000') as [FeLc(kN)],");
                            }
                            break;
                        case 4:
                            if (mSel.Fmc == true)
                            {
                                strSelcol.Append(" FORMAT([Fmc]/1000.0,'0.0000') as [Fmc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fmc]/1000.0),'0.0000') as [Fmc(kN)],");
                            }
                            if (mSel.Fpc == true)
                            {
                                strSelcol.Append(" FORMAT([Fpc]/1000.0,'0.0000') as [Fpc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpc]/1000.0),'0.0000') as [Fpc(kN)],");
                            }
                            if (mSel.Ftc == true)
                            {
                                strSelcol.Append(" FORMAT([Ftc]/1000.0,'0.0000') as [Ftc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Ftc]/1000.0),'0.0000') as [Ftc(kN)],");
                            }
                            if (mSel.FeHc == true)
                            {
                                strSelcol.Append(" FORMAT([FeHc]/1000.0,'0.0000') as [FeHc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeHc]/1000.0),'0.0000') as [FeHc(kN)],");
                            }
                            if (mSel.FeLc == true)
                            {
                                strSelcol.Append(" FORMAT([FeLc]/1000.0,'0.0000') as [FeLc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeLc]/1000.0),'0.0000') as [FeLc(kN)],");
                            }
                            break;
                        case 5:
                            if (mSel.Fmc == true)
                            {
                                strSelcol.Append(" FORMAT([Fmc]/1000.0,'0.00000') as [Fmc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fmc]/1000.0),'0.00') as [Fmc(kN)],");
                            }
                            if (mSel.Fpc == true)
                            {
                                strSelcol.Append(" FORMAT([Fpc]/1000.0,'0.00000') as [Fpc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpc]/1000.0),'0.00000') as [Fpc(kN)],");
                            }
                            if (mSel.Ftc == true)
                            {
                                strSelcol.Append(" FORMAT([Ftc]/1000.0,'0.00000') as [Ftc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Ftc]/1000.0),'0.00000') as [Ftc(kN)],");
                            }
                            if (mSel.FeHc == true)
                            {
                                strSelcol.Append(" FORMAT([FeHc]/1000.0,'0.00000') as [FeHc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeHc]/1000.0),'0.00000') as [FeHc(kN)],");
                            }
                            if (mSel.FeLc == true)
                            {
                                strSelcol.Append(" FORMAT([FeLc]/1000.0,'0.00000') as [FeLc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeLc]/1000.0),'0.00000') as [FeLc(kN)],");
                            }
                            break;
                        default:
                            if (mSel.Fmc == true)
                            {
                                strSelcol.Append(" FORMAT([Fmc]/1000.0,'0.00') as [Fmc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fmc]/1000.0),'0.00') as [Fmc(kN)],");
                            }
                            if (mSel.Fpc == true)
                            {
                                strSelcol.Append(" FORMAT([Fpc]/1000.0,'0.00') as [Fpc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Fpc]/1000.0),'0.00') as [Fpc(kN)],");
                            }
                            if (mSel.Ftc == true)
                            {
                                strSelcol.Append(" FORMAT([Ftc]/1000.0,'0.00') as [Ftc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([Ftc]/1000.0),'0.00') as [Ftc(kN)],");
                            }
                            if (mSel.FeHc == true)
                            {
                                strSelcol.Append(" FORMAT([FeHc]/1000.0,'0.00') as [FeHc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeHc]/1000.0),'0.00') as [FeHc(kN)],");
                            }
                            if (mSel.FeLc == true)
                            {
                                strSelcol.Append(" FORMAT([FeLc]/1000.0,'0.00') as [FeLc(kN)],");
                                strSelcolAver.Append(" FORMAT(Avg([FeLc]/1000.0),'0.00') as [FeLc(kN)],");
                            }
                            break;
                    }
                }    
             
                if (mSel.Rmc == true)
                {
                    strSelcol.Append(" round([Rmc],2) as [Rmc(MPa)],");
                    strSelcolAver.Append(" round(Avg([Rmc]),2) as [Rmc(MPa)],");
                }
                if (mSel.Rpc == true)
                {
                    strSelcol.Append(" round([Rpc],2) as [Rpc(Mpa)],");
                    strSelcolAver.Append(" round(Avg([Rpc]),2) as [Rpc(MPa)],");
                }
                if (mSel.Rtc == true)
                {
                    strSelcol.Append(" round([Rtc],2) as [Rtc(MPa)],");
                    strSelcolAver.Append(" round(Avg([Rtc]),2) as [Rtc(MPa)],");
                }
                if (mSel.ReHc == true)
                {
                    strSelcol.Append(" round([ReHc],2) as [ReHc(MPa)],");
                    strSelcolAver.Append(" round(Avg([ReHc]),2) as [ReHc(MPa)],");
                }
                if (mSel.ReLc == true)
                {
                    strSelcol.Append(" round([ReLc],2) as [ReLc(MPa)],");
                    strSelcolAver.Append(" round(Avg([ReLc]),2) as [ReLc(MPa)],");
                }
                if (mSel.Ec == true)
                {
                    strSelcol.Append(" round([Ec],2) as [Ec(MPa)],");
                    strSelcolAver.Append(" round(Avg([Ec]),2) as [Ec(MPa)],");
                }
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
        public static DataTable CreateView(string _selCol, string _strTestNo, int ab)
        {
            BLL.Compress bllts = new HR_Test.BLL.Compress();
            DataSet dst = bllts.GetFinishList1(_selCol, " testNo = '" + _strTestNo + "' and isFinish=true", ab);
            return dst.Tables[0];
        }

        //列表
        public static DataTable CreateViewReport(string _selCol, string _strTestSampleNo, int ab)
        {
            BLL.Compress bllts = new HR_Test.BLL.Compress();
            DataSet dst = bllts.GetFinishListReport(_selCol, " testSampleNo in ('" + _strTestSampleNo + ") and isFinish=true  and  isEffective=false ", ab);
            return dst.Tables[0];
        }

        //求取平均值 X~,S,V
        public static DataTable CreateAverageView(StringBuilder _selCol, StringBuilder _selSqlAver, string _strTestNo, int ab)
        {
            BLL.Compress bllts = new HR_Test.BLL.Compress();
            //求平均
            DataSet dsaver = bllts.GetFinishSumList1(_selSqlAver.ToString(), " testNo = '" + _strTestNo + "' and isFinish=true and isEffective=false ");

            //获取选择试样的数量和试样编号

            string selCol = _selCol.ToString();
            DataSet dsfinish = bllts.GetFinishList1(selCol.ToString(), " testNo = '" + _strTestNo + "' and isFinish=true and  isEffective=false ", ab);

            int fcount = dsfinish.Tables[0].Rows.Count;

            //变异系数 = sd/mean 
            StringBuilder strCV = new StringBuilder();

            //中间值
            StringBuilder strMid = new StringBuilder();
      
            DataSet dsaverto = new DataSet(); 
            dsaverto.Tables.Add();
            dsaverto.Tables[0].Columns.Add("数量(" + fcount + ")").SetOrdinal(0); 

            for (int i = 0; i < dsaver.Tables[0].Columns.Count;i++ )
            {
                dsaverto.Tables[0].Columns.Add(dsaver.Tables[0].Columns[i].Caption).SetOrdinal(i+1);
            }          
            //平均值列
            List<object> objmean = new List<object>();
            objmean.Add("Mean.:");
            for (int i = 0; i < dsaver.Tables[0].Columns.Count; i++)
            {
                if(!string.IsNullOrEmpty( dsaver.Tables[0].Rows[0][i].ToString()))
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

            List<object> objsd = new List<object>() ;
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
                    if (ab == 1)
                        ctrue = Convert.ToDouble(dsfinish.Tables[0].Rows[j][i + 4].ToString());
                    if (ab == 2)
                        ctrue = Convert.ToDouble(dsfinish.Tables[0].Rows[j][i + 3].ToString());
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

        //求取平均值 X~,S,V
        public static DataTable CreateAverageViewReport(StringBuilder _selCol, StringBuilder _selSqlAver, string _strTestSampleNo, int ab)
        {
            BLL.Compress bllts = new HR_Test.BLL.Compress();
            //求平均
            DataSet dsaver = bllts.GetFinishSumList1(_selSqlAver.ToString(), " testSampleNo in ('" + _strTestSampleNo + ") and isFinish=true  and  isEffective=false ");

            //获取选择试样的数量和试样编号

            string selCol = _selCol.ToString();
            DataSet dsfinish = bllts.GetFinishList1(selCol.ToString(), " testSampleNo in ('" + _strTestSampleNo + ") and isFinish=true  and  isEffective=false ", ab);

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
                    if (ab == 1)
                        ctrue = Convert.ToDouble(dsfinish.Tables[0].Rows[j][i + 4].ToString());
                    if (ab == 2)
                        ctrue = Convert.ToDouble(dsfinish.Tables[0].Rows[j][i + 3].ToString());
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

        public static void CreateCurveFile(string _testpath, Model.Compress modelC)
        {
            FileStream fs = new FileStream(_testpath, FileMode.Create, FileAccess.ReadWrite);
            utils.AddText(fs, "testType,testSampleNo,S0,L0,εpc,εtc");
            utils.AddText(fs, "\r\n");
            utils.AddText(fs, "compress," + modelC.testSampleNo + "," + modelC.S0 + "," + modelC.L0 + "," + +modelC.εpc + "," + modelC.εtc);
            utils.AddText(fs, "\r\n");
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

        //查询结果
        public static void ReadGBT7314(DataGridView dg, string testNo, string testSampleNo, DateTimePicker dtpFrom, DateTimePicker dtpTo, ZedGraph.ZedGraphControl zed)
        {
            BLL.Compress bllTs = new HR_Test.BLL.Compress();
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
            DataSet dsmax = bllTs.GetMaxFmc("isFinish=true and isEffective=false and testDate>=#" + dtpFrom.Value.Date + "# and testDate<=#" + dtpTo.Value.Date + "#" + strWhere);
            if (dsmax != null)
            {
                if (dsmax.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dsmax.Tables[0].Rows[0]["Fmc"].ToString()))
                        maxValue = Convert.ToDouble(dsmax.Tables[0].Rows[0]["Fmc"].ToString());
                }
            }

            DataSet ds = bllTs.GetFinishList("isFinish=true and isEffective=false and testDate>=#" + dtpFrom.Value.Date + "# and testDate<=#" + dtpTo.Value.Date + "#" + strWhere,maxValue);
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



    }
}
