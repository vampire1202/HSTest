using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HR_Test
{
    public class flag
    {
        public bool m_pause = false;   //暂停标志
        public bool m_holdPause = false; //取引伸计的暂停标志
        public bool m_holdContinue = false;//取引伸计后的继续标志

        public void SendPauseTest()
        {
            m_pause = !m_pause;

            byte[] buf = new byte[5];
            int ret;

            buf[0] = 0x04;									//命令字节
            buf[1] = 0xf3;							        //试验保持命令
            buf[2] = 0;
            buf[3] = 0;
            buf[4] = 0;

            ret = RwUsb.WriteData1582(1, buf, 5, 1000);				//发送写命令
        }
    }
}
