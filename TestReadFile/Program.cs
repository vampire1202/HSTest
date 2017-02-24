using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace TestReadFile
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    List<HR_Test.gdata> l = new List<HR_Test.gdata>();
        //    long dt = DateTime.Now.Ticks;
        //    string file = @"D:\工作相关\开发项目\HR-Test20130524(xy)\TestReadFile\bin\Debug\20130508-1-04.txt";
        //    string[] lines = System.IO.File.ReadAllLines(file);
        //    for (int i = 3; i < lines.Length; i++)
        //    {
        //        HR_Test.gdata _gdata = new HR_Test.gdata();
        //        string[] gdataArray = lines[i].Split(',');
        //        _gdata.F1 = float.Parse(gdataArray[0]);
        //        _gdata.F2 = float.Parse(gdataArray[1]);
        //        _gdata.F3 = float.Parse(gdataArray[2]);
        //        _gdata.D1 = float.Parse(gdataArray[3]);
        //        _gdata.D2 = float.Parse(gdataArray[4]);
        //        _gdata.D3 = float.Parse(gdataArray[5]);
        //        _gdata.BX1 = float.Parse(gdataArray[6]);
        //        _gdata.BX2 = float.Parse(gdataArray[7]);
        //        _gdata.BX3 = float.Parse(gdataArray[8]);
        //        _gdata.YL1 = float.Parse(gdataArray[9]);
        //        _gdata.YL2 = float.Parse(gdataArray[10]);
        //        _gdata.YL3 = float.Parse(gdataArray[11]);
        //        _gdata.YB1 = float.Parse(gdataArray[12]);
        //        _gdata.YB2 = float.Parse(gdataArray[13]);
        //        _gdata.YB3 = float.Parse(gdataArray[14]);
        //        _gdata.Ts = float.Parse(gdataArray[15]);
        //        l.Add(_gdata);
        //    }
        //    long dt2 = DateTime.Now.Ticks - dt;
        //    Console.WriteLine(dt2.ToString());
        //    Console.ReadLine();
        //}
        static void Main(string[] args)
        {
            //读取曲线 
            List<HR_Test.gdata> _List_DataReadOne = new List<HR_Test.gdata>();
            long dt = DateTime.Now.Ticks;
            string file = @"D:\工作相关\开发项目\HR-Test20130524(xy)\TestReadFile\bin\Debug\20130508-1-04.txt";
            using (StreamReader srLine = new StreamReader(file))
            {
                string[] testSampleInfo1 = srLine.ReadLine().Split(',');
                string[] testSampleInfo2 = srLine.ReadLine().Split(',');
                if (srLine.ReadLine() != null)
                { string[] testSampleInfo3 = srLine.ReadLine().Split(','); }
                String line;
                // Read and display lines from the file until the end of
                while ((line = srLine.ReadLine()) != null)
                {
                    string[] gdataArray = line.Split(',');
                    HR_Test.gdata _gdata = new HR_Test.gdata();
                    _gdata.F1 = float.Parse(gdataArray[0]);
                    _gdata.F2 = float.Parse(gdataArray[1]);
                    _gdata.F3 = float.Parse(gdataArray[2]);
                    _gdata.D1 = float.Parse(gdataArray[3]);
                    _gdata.D2 = float.Parse(gdataArray[4]);
                    _gdata.D3 = float.Parse(gdataArray[5]);
                    _gdata.BX1 = float.Parse(gdataArray[6]);
                    _gdata.BX2 = float.Parse(gdataArray[7]);
                    _gdata.BX3 = float.Parse(gdataArray[8]);
                    _gdata.YL1 = float.Parse(gdataArray[9]);
                    _gdata.YL2 = float.Parse(gdataArray[10]);
                    _gdata.YL3 = float.Parse(gdataArray[11]);
                    _gdata.YB1 = float.Parse(gdataArray[12]);
                    _gdata.YB2 = float.Parse(gdataArray[13]);
                    _gdata.YB3 = float.Parse(gdataArray[14]);
                    _gdata.Ts = float.Parse(gdataArray[15]);
                    _List_DataReadOne.Add(_gdata);
                }
                srLine.Close();
                srLine.Dispose();
            }
            long dt2 = DateTime.Now.Ticks - dt;
            Console.WriteLine(dt2.ToString());
            Console.ReadLine();
        }


    }
}
