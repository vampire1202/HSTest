using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HR_Test
{
    public class Struc
    {
        //传感器结构体
        public class sensor
        {
            public byte sockt;
            public byte type;
            public byte volt;
            public byte direct;
            public int balance;
            public int gain;
            public int scale;
        };
 

        //传感器数组结构体
        public class SensorArray
        {
            public byte SensorIndex;
        };

        //控制命令结构体
        public class ctrlcommand
        {
            public byte m_CtrlType;
            public byte m_CtrlChannel;
            public int m_CtrlSpeed;
            public byte m_StopPointType;
            public byte m_StopPointChannel;
            public int m_StopPoint;
        };

    }
}
