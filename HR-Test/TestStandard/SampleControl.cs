using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
namespace HR_Test.TestStandard
{
    class SampleControl
    {
        //读取指定日期的所有试验
        public static List<TreeNode> ReadSample(DateTime dtp)
        {
            //从 standard 数据库中读取，此处如果有重复的表则要不重复读取
            BLL.Standard bllS = new HR_Test.BLL.Standard();
            DataSet ds = bllS.GetAllListDistinctResultTableName(); //bllS.GetAllList();
            int count = ds.Tables[0].Rows.Count;
            List<TreeNode> ltn = new List<TreeNode>();
            if (ds != null)
            { 
                ltn.Clear();
                for (int i = 0; i < count; i++)
                {
                    string resultTbName = ds.Tables[0].Rows[i]["resultTableName"].ToString();
                    switch (resultTbName)
                    {
                        case "tb_TestSample":
                           ltn.AddRange(ReadGBT228Samples(dtp));
                            break;
                        case "tb_Compress":
                            ltn.AddRange( ReadGBT7314Samples(dtp));
                            break;
                        case "tb_Bend":
                            ltn.AddRange( ReadYBT5349Samples(dtp));
                            break;
                        case "tb_GBT282892012_Tensile":
                            ltn.AddRange(ReadGBT28289TensileSamples(dtp));
                            break;
                        case "tb_GBT282892012_Shear":
                            ltn.AddRange( ReadGBT28289ShearSamples(dtp));
                            break;
                        case "tb_GBT282892012_Twist":
                            ltn.AddRange(ReadGBT28289TwistSamples(dtp));
                            break;
                        case "tb_GBT236152009_TensileHeng":
                            ltn.AddRange(ReadGBT23615HengSamples(dtp));
                            break;
                        case "tb_GBT236152009_TensileZong":
                             ltn.AddRange( ReadGBT23615ZongSamples(dtp));
                            break;
                        case "tb_GBT3354_Samples":
                            ltn.AddRange(ReadGBT3354Samples(dtp));
                            break;
                        default:
                            break;
                    }                  
                }                
            }
            if (ltn != null)
            {
                return ltn;
                //foreach (TreeNode t in ltn)
                //{
                //    tv.Nodes.Add(t);
                //}
            }
            else
                return null;
            //if (tv.Nodes.Count == 0)
            //    tv.Nodes.Add("无");
            //tv.ExpandAll();

        }
        private static List<TreeNode> ReadGBT3354Samples(DateTime dtp)
        {
            BLL.GBT3354_Samples bllTs = new HR_Test.BLL.GBT3354_Samples();
            //查询不重复项
            DataSet ds = bllTs.GetNotOverlapList1(" testDate = #" + dtp.Date + "#");//distinct testNo,testMethodName
            int rCount = ds.Tables[0].Rows.Count;
            List<TreeNode> ltn = new List<TreeNode>();
            for (int i = 0; i < rCount; i++)
            {
                DataSet _ds = bllTs.GetList(" testNo='" + ds.Tables[0].Rows[i]["testNo"].ToString() + "' and testMethodName='" + ds.Tables[0].Rows[i]["testMethodName"].ToString() + "' and testDate=#" + dtp.Date + "#");
                TreeNode tn = new TreeNode();
                tn.Text = ds.Tables[0].Rows[i]["testNo"].ToString();
                tn.Name = "GBT3354-2014";
                tn.ImageIndex = 0;

                foreach (DataRow dr in _ds.Tables[0].Rows)
                {
                    if (Convert.ToBoolean(dr["isFinish"].ToString()) == true)
                    {
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 1;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "GBT3354-2014_c";
                        ftn.Tag = dr;
                        tn.Nodes.Add(ftn);
                    }
                    else
                    {
                        //左侧node未完成试验的图标
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 2;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Tag = dr;
                        ftn.Name = "GBT3354-2014_c";
                        tn.Nodes.Add(ftn);
                    }
                }
                _ds.Dispose();
                ltn.Add(tn);
            }
            ds.Dispose();
            return ltn;
        }
        private static List<TreeNode> ReadGBT23615ZongSamples(DateTime dtp)
        {
            BLL.GBT236152009_TensileZong bllTs = new HR_Test.BLL.GBT236152009_TensileZong();
            //查询不重复项
            DataSet ds = bllTs.GetNotOverlapList(" testDate = #" + dtp.Date + "#");//distinct testNo,testMethodName
            int rCount = ds.Tables[0].Rows.Count;
            List<TreeNode> ltn = new List<TreeNode>();
            for (int i = 0; i < rCount; i++)
            {
                DataSet _ds = bllTs.GetList(" testNo='" + ds.Tables[0].Rows[i]["testNo"].ToString() + "' and testMethodName='" + ds.Tables[0].Rows[i]["testMethodName"].ToString() + "' and testDate=#" + dtp.Date + "#");
                TreeNode tn = new TreeNode();
                tn.Text = ds.Tables[0].Rows[i]["testNo"].ToString();
                tn.Name = "GBT23615-2009TensileZong";
                tn.ImageIndex = 0;

                foreach (DataRow dr in _ds.Tables[0].Rows)
                {
                    if (Convert.ToBoolean(dr["isFinish"].ToString()) == true)
                    {
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 1;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "GBT23615-2009TensileZong_c";
                        ftn.Tag = dr;
                        tn.Nodes.Add(ftn);
                    }
                    else
                    {
                        //左侧node未完成试验的图标
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 2;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Tag = dr;
                        ftn.Name = "GBT23615-2009TensileZong_c";
                        tn.Nodes.Add(ftn);
                    }
                }
                _ds.Dispose();
                ltn.Add(tn);
            }
            ds.Dispose();
            return ltn;
        }

        private static List<TreeNode> ReadGBT23615HengSamples(DateTime dtp)
        {
            BLL.GBT236152009_TensileHeng bllTs = new HR_Test.BLL.GBT236152009_TensileHeng();
            //查询不重复项
            DataSet ds = bllTs.GetNotOverlapList(" testDate = #" + dtp.Date + "#");
            int rCount = ds.Tables[0].Rows.Count;
            List<TreeNode> ltn = new List<TreeNode>();
            for (int i = 0; i < rCount; i++)
            {
                DataSet _ds = bllTs.GetList(" testNo='" + ds.Tables[0].Rows[i]["testNo"].ToString() + "' and testMethodName='" + ds.Tables[0].Rows[i]["testMethodName"].ToString() + "' and testDate=#" + dtp.Date + "#");
                TreeNode tn = new TreeNode();
                tn.Text = ds.Tables[0].Rows[i]["testNo"].ToString();
                tn.Name = "GBT23615-2009TensileHeng";
                tn.ImageIndex = 0;

                foreach (DataRow dr in _ds.Tables[0].Rows)
                {
                    if (Convert.ToBoolean(dr["isFinish"].ToString()) == true)
                    {
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 1;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "GBT23615-2009TensileHeng_c";
                        ftn.Tag = dr;
                        tn.Nodes.Add(ftn);
                    }
                    else
                    {
                        //左侧node未完成试验的图标
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 2;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Tag = dr;
                        ftn.Name = "GBT23615-2009TensileHeng_c";
                        tn.Nodes.Add(ftn);
                    }
                }
                _ds.Dispose();
                ltn.Add(tn);
            }
            ds.Dispose();
            return ltn;
        }

        private static List<TreeNode> ReadGBT228Samples(DateTime dtp)
        {
            BLL.TestSample bllTs = new HR_Test.BLL.TestSample();
            //查询不重复项
            DataSet ds = bllTs.GetNotOverlapList1(" testDate = #" + dtp.Date + "#");
            int rCount = ds.Tables[0].Rows.Count;
            List<TreeNode> ltn = new List<TreeNode>();
            for (int i = 0; i < rCount; i++)
            {
                DataSet _ds = bllTs.GetList(" testNo='" + ds.Tables[0].Rows[i]["testNo"].ToString() + "' and testMethodName='" + ds.Tables[0].Rows[i]["testMethodName"].ToString() + "' and testDate=#" + dtp.Date + "#");
                TreeNode tn = new TreeNode();
                tn.Text = ds.Tables[0].Rows[i]["testNo"].ToString();
                tn.Name = "GBT228-2010";
                tn.ImageIndex = 0;

                foreach (DataRow dr in _ds.Tables[0].Rows)
                {
                    if (Convert.ToBoolean(dr["isFinish"].ToString()) == true)
                    {
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 1;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "GBT228-2010_c";
                        ftn.Tag = dr;
                        tn.Nodes.Add(ftn);
                    }
                    else
                    {
                        //左侧node未完成试验的图标
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 2;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Tag = dr;
                        ftn.Name = "GBT228-2010_c";
                        tn.Nodes.Add(ftn);
                    }
                }
                _ds.Dispose();
                ltn.Add(tn);
            }
            ds.Dispose();
            return ltn;
        }

        private static List<TreeNode> ReadGBT7314Samples(DateTime dtp)
        {
            BLL.Compress bllC = new HR_Test.BLL.Compress();
            DataSet dsc = bllC.GetNotOverlapList(" testDate = #" + dtp.Date + "#");
            int count = dsc.Tables[0].Rows.Count;
            List<TreeNode> ltn = new List<TreeNode>();
            for (int j = 0; j < count; j++)
            {
                DataSet _dsc = bllC.GetList(" testNo='" + dsc.Tables[0].Rows[j]["testNo"].ToString() + "' and testDate=#" + dtp.Date + "#");
                TreeNode tn = new TreeNode();
                tn.Text = dsc.Tables[0].Rows[j]["testNo"].ToString();
                tn.Name = "GBT7314-2005";
                tn.ImageIndex = 0;
                foreach (DataRow dr in _dsc.Tables[0].Rows)
                {
                    if (Convert.ToBoolean(dr["isFinish"].ToString()) == true)
                    {
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 1;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "GBT7314-2005_c";
                        ftn.Tag = dr;
                        tn.Nodes.Add(ftn);
                    }
                    else
                    {
                        //左侧node未完成试验的图标 
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 2;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "GBT7314-2005_c";
                        ftn.Tag = dr;
                        tn.Nodes.Add(ftn);
                    }
                }
                _dsc.Dispose();
                ltn.Add(tn);
            }
            dsc.Dispose();
            return ltn;
        }

        private static List<TreeNode> ReadYBT5349Samples(DateTime dtp)
        {
            BLL.Bend bllb = new HR_Test.BLL.Bend();
            DataSet dsb = bllb.GetNotOverlapList("testDate=#" + dtp.Date + "#");
            int count = dsb.Tables[0].Rows.Count;
            List<TreeNode> ltn = new List<TreeNode>();
            for (int j = 0; j < count; j++)
            {
                DataSet _dsb = bllb.GetList(" testNo='" + dsb.Tables[0].Rows[j]["testNo"].ToString() + "' and testDate=#" + dtp.Date + "#");
                TreeNode tn = new TreeNode();
                tn.Text = dsb.Tables[0].Rows[j]["testNo"].ToString();
                tn.Name = "YBT5349-2006";
                tn.ImageIndex = 0;
                foreach (DataRow dr in _dsb.Tables[0].Rows)
                {
                    if (Convert.ToBoolean(dr["isFinish"].ToString()) == true)
                    {
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 1;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "YBT5349-2006_c";
                        ftn.Tag = dr;
                        tn.Nodes.Add(ftn);
                    }
                    else
                    {
                        //左侧node未完成试验的图标 
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 2;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "YBT5349-2006_c";
                        ftn.Tag = dr;
                        tn.Nodes.Add(ftn);
                    }
                }
                _dsb.Dispose();
                ltn.Add(tn);
            }
            dsb.Dispose();
            return ltn;
        }

        private static List<TreeNode> ReadGBT28289TensileSamples(DateTime dtp)
        {
            BLL.GBT282892012_Tensile bllb = new HR_Test.BLL.GBT282892012_Tensile();
            DataSet dsb = bllb.GetNotOverlapList("testDate=#" + dtp.Date + "#");
            int count = dsb.Tables[0].Rows.Count;
            List<TreeNode> ltn = new List<TreeNode>();
            for (int j = 0; j < count; j++)
            {
                DataSet _dsb = bllb.GetList(" testNo='" + dsb.Tables[0].Rows[j]["testNo"].ToString() + "' and testDate=#" + dtp.Date + "# ");
                TreeNode tn = new TreeNode();
                tn.Text = dsb.Tables[0].Rows[j]["testNo"].ToString();
                tn.Name = "GBT28289-2012Tensile";
                tn.ImageIndex = 0;
                foreach (DataRow dr in _dsb.Tables[0].Rows)
                {
                    if (Convert.ToBoolean(dr["isFinish"].ToString()) == true)
                    {
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 1;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "GBT28289-2012Tensile_c";
                        ftn.Tag = dr;
                        tn.Nodes.Add(ftn);
                    }
                    else
                    {
                        //左侧node未完成试验的图标 
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 2;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "GBT28289-2012Tensile_c";
                        ftn.Tag = dr;
                        tn.Nodes.Add(ftn);
                    }
                }
                _dsb.Dispose();
                ltn.Add(tn);
            }
            dsb.Dispose();
            return ltn;
        }

        private static List<TreeNode> ReadGBT28289ShearSamples(DateTime dtp)
        {
            BLL.GBT282892012_Shear bllb = new HR_Test.BLL.GBT282892012_Shear();
            DataSet dsb = bllb.GetNotOverlapList("testDate=#" + dtp.Date + "#");
            int count = dsb.Tables[0].Rows.Count;
            List<TreeNode> ltn = new List<TreeNode>();
            for (int j = 0; j < count; j++)
            {
                DataSet _dsb = bllb.GetList(" testNo='" + dsb.Tables[0].Rows[j]["testNo"].ToString() + "' and testDate=#" + dtp.Date + "#");
                TreeNode tn = new TreeNode();
                tn.Text = dsb.Tables[0].Rows[j]["testNo"].ToString();
                tn.Name = "GBT28289-2012Shear";
                tn.ImageIndex = 0;
                foreach (DataRow dr in _dsb.Tables[0].Rows)
                {
                    if (Convert.ToBoolean(dr["isFinish"].ToString()) == true)
                    {
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 1;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "GBT28289-2012Shear_c";
                        ftn.Tag = dr;
                        tn.Nodes.Add(ftn);
                    }
                    else
                    {
                        //左侧node未完成试验的图标 
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 2;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "GBT28289-2012Shear_c";
                        ftn.Tag = dr;
                        tn.Nodes.Add(ftn);
                    }
                }
                _dsb.Dispose();
                ltn.Add(tn);
            }
            dsb.Dispose();
            return ltn;
        }

        private static List<TreeNode> ReadGBT28289TwistSamples(DateTime dtp)
        {
            BLL.GBT282892012_Twist bllb = new HR_Test.BLL.GBT282892012_Twist();
            DataSet dsb = bllb.GetNotOverlapList("testDate=#" + dtp.Date + "#");
            int count = dsb.Tables[0].Rows.Count;
            List<TreeNode> ltn = new List<TreeNode>();
            for (int j = 0; j < count; j++)
            {
                DataSet _dsb = bllb.GetList(" testNo='" + dsb.Tables[0].Rows[j]["testNo"].ToString() + "' and testDate=#" + dtp.Date + "#");
                TreeNode tn = new TreeNode();
                tn.Text = dsb.Tables[0].Rows[j]["testNo"].ToString();
                tn.Name = "GBT28289-2012Twist";
                tn.ImageIndex = 0;
                foreach (DataRow dr in _dsb.Tables[0].Rows)
                {
                    if (Convert.ToBoolean(dr["isFinish"].ToString()) == true)
                    {
                        //左侧node完成试验的图标
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 1;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "GBT28289-2012Twist_c";
                        ftn.Tag = dr;
                        tn.Nodes.Add(ftn);
                    }
                    else
                    {
                        //左侧node未完成试验的图标 
                        TreeNode ftn = new TreeNode();
                        ftn.ImageIndex = 2;
                        ftn.Text = dr["testSampleNo"].ToString();
                        ftn.Name = "GBT28289-2012Twist_c";
                        ftn.Tag = dr;
                        tn.Nodes.Add(ftn);
                    }
                }
                _dsb.Dispose();
                ltn.Add(tn);
            }
            dsb.Dispose();
            return ltn;
        }

        public static void ReadTestStandard(ComboBox cmb)
        {
            //cmb.Items.Clear();
            BLL.Standard bllS = new HR_Test.BLL.Standard();
            DataSet ds = bllS.GetAllListDistinctStandardNo();
            cmb.DataSource = ds.Tables[0];
            cmb.DisplayMember = "standardNo";
            cmb.ValueMember = "standardNo";
        }

        public static void ReadTestType(ComboBox cmb)
        {
            //cmb.Items.Clear();
            BLL.Standard bllS = new HR_Test.BLL.Standard();
            DataSet ds = bllS.GetAllListDistinctTestType();
            cmb.DataSource = ds.Tables[0];
            cmb.DisplayMember = "testType";
            cmb.ValueMember = "testType";
        }
    }
}
