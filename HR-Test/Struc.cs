using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HR_Test
{
    public class Struc
    {
        //传感器结构体
        public struct sensor
        {
            public byte sockt;
            public byte type;
            public byte volt;
            public byte direct;
            public int balance;
            public int gain;
            public int scale;
        };

        public class Sensors
        {
            private byte _sockt;          
            private byte _type;
            private byte _volt;
            private byte _direct;
            private int _balance;
            private int _gain;
            private int _scale;
            public byte sockt
            {
                get { return this._sockt; }
                set { this._sockt = value; }
            }

            public byte type
            {
                get { return this._type; }
                set { this._type = value; }
            }

            public byte volt
            {
                get { return this._volt; }
                set { this._volt = value; }
            }

            public byte direct
            {
                get { return this._direct; }
                set { this._direct = value; }
            }

            public int  balance
            {
                get { return this._balance; }
                set { this._balance = value; }
            }

            public int gain
            {
                get { return this._gain; }
                set { this._gain = value; }
            }

            public int scale
            {
                get { return this._scale; }
                set { this._scale = value; }
            }

        }

        //传感器数组结构体
        public struct SensorArray
        {
            public byte SensorIndex;
        };

        //控制命令结构体
        public struct ctrlcommand
        {
            public byte m_CtrlType;
            public byte m_CtrlChannel;
            public int m_CtrlSpeed;
            public byte m_StopPointType;
            public byte m_StopPointChannel;
            public int m_StopPoint;
        };

        //控制命令结构体
        public struct ctrlCommandInfo
        {
            public int _CtrlType;
            public int _CtrlChannel;
            public double _CtrlSpeed;
            public int _StopPointType;
            public int _StopPointChannel;
            public double _StopPoint;
        };

        public class ControlCommand
        {
            private byte _ctrlType;
            private byte _ctrlChannel;
            private int _ctrlSpeed;
            private byte _stopPointType;
            private byte _stopPointChannel;
            private int _stopPoint;

            public byte m_CtrlType
            {
                get { return this._ctrlType; }
                set { this._ctrlType = value; }
            }

            public byte m_CtrlChannel
            {
                get { return this._ctrlChannel; }
                set { this._ctrlChannel = value; }
            }

            public int m_CtrlSpeed
            {
                get { return this._ctrlSpeed; }
                set { this._ctrlSpeed = value; }
            }

            public byte m_StopPointType
            {
                get { return this._stopPointType; }
                set { this._stopPointType = value; }
            }

            public byte m_StopPointChannel
            {
                get { return this._stopPointChannel; }
                set { this._stopPointChannel = value; }
            }

            public int m_StopPoint
            {
                get { return this._stopPoint; }
                set { this._stopPoint = value; }
            }
        }



    }
}
