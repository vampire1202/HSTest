using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace HR_Test.TestStandard
{
    class MethodControl
    {
        public static void ReadMethodList(TreeView tv)
        {
            tv.Nodes.Clear();
            tv.Nodes.Add("试验方法");

            BLL.GBT236152009_Method bll23615 = new HR_Test.BLL.GBT236152009_Method();
            DataSet ds23615 = bll23615.GetAllList();
            for (int k = 0; k < ds23615.Tables[0].Rows.Count; k++)
            {
                TreeNode tn = new TreeNode();
                tn.Tag = "GB/T 23615.1-2009";
                tn.Text = ds23615.Tables[0].Rows[k]["methodName"].ToString();
                tn.Name = ds23615.Tables[0].Rows[k]["xmlPath"].ToString();
                tv.Nodes[0].Nodes.Add(tn);
            }

            BLL.GBT282892012_Method bll28289 = new HR_Test.BLL.GBT282892012_Method();
            DataSet ds28289 = bll28289.GetAllList();
            for (int k = 0; k < ds28289.Tables[0].Rows.Count; k++)
            {
                TreeNode tn = new TreeNode();
                tn.Tag = "GB/T 28289-2012";// ds28289.Tables[0].Rows[k]["testStandard"].ToString();
                tn.Text = ds28289.Tables[0].Rows[k]["methodName"].ToString();
                tn.Name = ds28289.Tables[0].Rows[k]["xmlPath"].ToString();
                tv.Nodes[0].Nodes.Add(tn);
            }

            BLL.ControlMethod bllCm = new HR_Test.BLL.ControlMethod();
            DataSet ds = bllCm.GetAllList();
            for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
            {
                TreeNode tn = new TreeNode();
                tn.Tag = "GB/T 228-2010";// ds.Tables[0].Rows[k]["testStandard"].ToString();
                tn.Text = ds.Tables[0].Rows[k]["methodName"].ToString();
                tn.Name = ds.Tables[0].Rows[k]["xmlPath"].ToString();
                tv.Nodes[0].Nodes.Add(tn);
            }

            BLL.ControlMethod_C bllCm_C = new HR_Test.BLL.ControlMethod_C();
            DataSet ds_C = bllCm_C.GetAllList();
            for (int k = 0; k < ds_C.Tables[0].Rows.Count; k++)
            {
                TreeNode tn = new TreeNode();
                tn.Tag = "GB/T 7314-2005";//ds_C.Tables[0].Rows[k]["testStandard"].ToString();
                tn.Text = ds_C.Tables[0].Rows[k]["methodName"].ToString();
                tn.Name = ds_C.Tables[0].Rows[k]["xmlPath"].ToString();
                tv.Nodes[0].Nodes.Add(tn);
            }
            BLL.ControlMethod_B bllCm_B = new HR_Test.BLL.ControlMethod_B();
            DataSet ds_B = bllCm_B.GetAllList();
            for (int k = 0; k < ds_B.Tables[0].Rows.Count; k++)
            {
                TreeNode tn = new TreeNode();
                tn.Tag = "YB/T 5349-2006";// ds_B.Tables[0].Rows[k]["testStandard"].ToString();
                tn.Text = ds_B.Tables[0].Rows[k]["methodName"].ToString();
                tn.Name = ds_B.Tables[0].Rows[k]["xmlPath"].ToString();
                tv.Nodes[0].Nodes.Add(tn);
            }
            BLL.GBT3354_Method bll3354 = new HR_Test.BLL.GBT3354_Method();
            DataSet ds3354 = bll3354.GetAllList();
            for (int k = 0; k < ds3354.Tables[0].Rows.Count; k++)
            {
                TreeNode tn = new TreeNode();
                tn.Tag = "GB/T 3354-2014";
                tn.Text = ds3354.Tables[0].Rows[k]["methodName"].ToString();
                tn.Name = ds3354.Tables[0].Rows[k]["xmlPath"].ToString();
                tv.Nodes[0].Nodes.Add(tn);
            }
            tv.ExpandAll();
        }
    }
}
