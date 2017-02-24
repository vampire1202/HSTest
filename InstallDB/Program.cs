using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace InstallDB
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Directory.Exists(@"E:\衡新试验数据\Curve"))
            {
                Directory.CreateDirectory(@"E:\衡新试验数据\Curve");
            }
          
            if (!File.Exists(@"E:\衡新试验数据\HR-TestData.mdb"))
            {
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "HR-TestData.mdb", @"E:\衡新试验数据\HR-TestData.mdb");
            }
            //string dinfo = AppDomain.CurrentDomain.BaseDirectory + "Curve";
            //string tinfo =  @"E:\衡新试验数据\Curve";

            //CopyFiles(dinfo, tinfo);

            //else
            //{
            //    if (DialogResult.OK == MessageBox.Show("确认覆盖旧数据库?这将导致所有历史数据被清空,请慎重!", "警告", MessageBoxButtons.OKCancel))
            //    {
            //        File.Copy(AppDomain.CurrentDomain.BaseDirectory + "HR-TestData.mdb", @"E:\衡新试验数据\HR-TestData.mdb");
            //    }
            //}
        }

        private static void CopyFiles(string varFromDirectory, string varToDirectory)
        {
            Directory.CreateDirectory(varToDirectory);

            if (!Directory.Exists(varFromDirectory)) return;

            string[] directories = Directory.GetDirectories(varFromDirectory);

            if (directories.Length > 0)
            {
                foreach (string d in directories)
                {
                    CopyFiles(d, varToDirectory + d.Substring(d.LastIndexOf("\\")));
                }
            }

            string[] files = Directory.GetFiles(varFromDirectory);

            if (files.Length > 0)
            {
                foreach (string s in files)
                {
                    File.Copy(s, varToDirectory + s.Substring(s.LastIndexOf("\\")),true);
                }
            }
        }
    }
}
