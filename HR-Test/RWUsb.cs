using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace HR_Test
{
    class RwUsb
    {
        //int __stdcall ReadData1582(int pipenum,unsigned char *recbuffer,int len,int waittime=-1);
        //int __stdcall WriteData1582(int pipenum,unsigned char *sendbuffer,int len,int waittime=-1);


        [DllImport("EasyUSB1582.dll", EntryPoint = "ReadData1582", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadData1582(Int32 pipenum, Byte[] recbuffer, Int32 len, Int32 waittime);

        [DllImport("EasyUSB1582.dll", EntryPoint = "WriteData1582", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteData1582(Int32 pipenum, Byte[] sendbuffer, Int32 len, Int32 waittime);


    }
}
