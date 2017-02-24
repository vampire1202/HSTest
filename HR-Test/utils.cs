using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D; 
using System.Xml;
using System.Xml.XPath;
namespace HR_Test
{
    class utils
    {
        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        public static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        public static int Dotvalue(double _value)
        {
            int _dotValue = 0;
            int _ten =(int)Math.Log10(_value);
            if (_value < 1000.0)
                _dotValue = 6 - _ten - 1 ;
            if (_value > 1000.0)
            {
                _dotValue = 6 - (_ten - 3)-1;            
            }
            if (_dotValue < 2)
                _dotValue = 2;
            return _dotValue; 
        }


        /// <summary>
        /// 获取缩小后的图片
        /// </summary>
        /// <param name="bm">要缩小的图片</param>
        /// <param name="times">要缩小的倍数</param>
        /// <returns></returns>
        public static Bitmap GetSmallBmp(Bitmap bm, double times)
        {
            int nowWidth = (int)(bm.Width / times);
            int nowHeight = (int)(bm.Height / times);
            Bitmap newbm = new Bitmap(nowWidth, nowHeight);//新建一个放大后大小的图片

            if (times >= 1 && times <= 1.1)
            {
                newbm = bm;
            }
            else
            {
                Graphics g = Graphics.FromImage(newbm);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(bm, new Rectangle(0, 0, nowWidth, nowHeight), new Rectangle(0, 0, bm.Width, bm.Height), GraphicsUnit.Pixel);
                g.Dispose();
            }
            return newbm;
        }

 
         /// <summary>
        /// 将图片Image转换成Byte[]
        /// </summary>
        /// <param name="Image">image对象</param>
        /// <param name="imageFormat">后缀名</param>
        /// <returns></returns>
        public static byte[] ImageToBytes(Image Image, System.Drawing.Imaging.ImageFormat imageFormat)
        {

            if (Image == null) { return null; }

            byte[] data = null;

            using (MemoryStream ms= new MemoryStream())
            {

                 using (Bitmap Bitmap = new Bitmap(Image))
                {

                    Bitmap.Save(ms, imageFormat);

                    ms.Position = 0;

                    data =  new byte[ms.Length];

                    ms.Read(data, 0, Convert.ToInt32(ms.Length));

                    ms.Flush();

                }

            }

            return data;

        }


            /// <summary>
            /// byte[]转换成Image
            /// </summary>
            /// <param name="byteArrayIn">二进制图片流</param>
            /// <returns>Image</returns>
            public static System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
            {
                if (byteArrayIn == null)
                    return null;
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArrayIn))
                {
                    System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                    ms.Flush();
                    return returnImage;
                }
            }

 

        //byte[] 转换 Bitmap
        public static Bitmap BytesToBitmap(byte[] Bytes) 
        { 
            MemoryStream stream = null; 
            try 
            { 
                stream = new MemoryStream(Bytes); 
                return new Bitmap((Image)new Bitmap(stream)); 
            } 
            catch (ArgumentNullException ex) 
            { 
                throw ex; 
            } 
            catch (ArgumentException ex) 
            { 
                throw ex; 
            } 
            finally 
            { 
                stream.Close(); 
            } 
        }  
 
        //Bitmap转byte[]  
        public static byte[] BitmapToBytes(Bitmap Bitmap) 
        { 
            MemoryStream ms = null; 
            try 
            { 
                ms = new MemoryStream(); 
                Bitmap.Save(ms, Bitmap.RawFormat); 
                byte[] byteImage = new Byte[ms.Length]; 
                byteImage = ms.ToArray(); 
                return byteImage; 
            } 
            catch (ArgumentNullException ex) 
            { 
                throw ex; 
            } 
            finally 
            { 
                ms.Close(); 
            } 
        }  



        /// <summary>
        /// 创建系统用户文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="xmlFileName"></param>
        //public static void CreateXmlFile(string xmlFileName)
        //{
        //    //loopNum =int.Parse(Cls.RWconfig.GetAppSettings("loopNum").ToString());
        //    XmlWriterSettings settings = new XmlWriterSettings();
        //    settings.Indent = true;
        //    settings.IndentChars = ("   ");
        //    using (XmlWriter writer = XmlWriter.Create(xmlFileName, settings))
        //    {
        //        // Write XML data. 
        //        writer.WriteStartElement("root");
        //        //系统管理员
        //        writer.WriteStartElement("user");
        //        writer.WriteAttributeString("code", "admin");
        //        writer.WriteAttributeString("name", "系统管理员");
        //        writer.WriteAttributeString("telephone", "13888888888");
        //        writer.WriteAttributeString("password", Cls.DESS.Encode("godexadmin001"));
        //        writer.WriteAttributeString("role", "SystemManager");
        //        writer.WriteEndElement();
        //        //普通用户
        //        writer.WriteStartElement("user");
        //        writer.WriteAttributeString("code", "CommonUser");
        //        writer.WriteAttributeString("name", "张三");
        //        writer.WriteAttributeString("telephone", "13888888888");
        //        writer.WriteAttributeString("password", Cls.DESS.Encode("commonuser"));
        //        writer.WriteAttributeString("role", "CommonUser");
        //        writer.WriteEndElement();
        //        //观察员
        //        writer.WriteStartElement("user");
        //        writer.WriteAttributeString("code", "Observer");
        //        writer.WriteAttributeString("name", "李四");
        //        writer.WriteAttributeString("telephone", "13888888888");
        //        writer.WriteAttributeString("password", Cls.DESS.Encode("observer"));
        //        writer.WriteAttributeString("role", "Observer");
        //        writer.WriteEndElement();
        //        writer.WriteEndElement();
        //        writer.Flush();
        //    }
        //}

        public static Color GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);
            //  为了在白色背景上显示，尽量生成深色
            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;
            return Color.FromArgb(int_Red, int_Green, int_Blue);
        }     

    /// <summary> 
    /// summary description for strhelper. 
    /// 命名缩写： 
    /// str: Unicode string 
    /// arr: Unicode array 
    /// hex: 二进制数据 
    /// hexbin: 二进制数据用ASCII字符表示 例 字符1的hex是0x31表示为hexbin是 31 
    /// asc: ASCII 
    /// uni: Unicode 
    /// </summary> 
 
        public static void hexbin2hex(byte[] bhexbin, byte[] bhex, int nlen)
        {
            for (int i = 0; i < nlen / 2; i++)
            {
                if (bhexbin[2 * i] < 0x41)
                {
                    bhex[i] = Convert.ToByte(((bhexbin[2 * i] - 0x30) << 4) & 0xf0);
                }
                else
                {
                    bhex[i] = Convert.ToByte(((bhexbin[2 * i] - 0x37) << 4) & 0xf0);
                }

                if (bhexbin[2 * i + 1] < 0x41)
                {
                    bhex[i] |= Convert.ToByte((bhexbin[2 * i + 1] - 0x30) & 0x0f);
                }
                else
                {
                    bhex[i] |= Convert.ToByte((bhexbin[2 * i + 1] - 0x37) & 0x0f);
                }
            }
        }

        public static byte[] hexbin2hex(byte[] bhexbin, int nlen)
        {
            if (nlen % 2 != 0)
                return null;
            byte[] bhex = new byte[nlen / 2];
            hexbin2hex(bhexbin, bhex, nlen);
            return bhex;
        }

        public static void hex2hexbin(byte[] bhex, byte[] bhexbin, int nlen)
        {
            byte c;
            for (int i = 0; i < nlen; i++)
            {
                c = Convert.ToByte((bhex[i] >> 4) & 0x0f);
                if (c < 0x0a)
                {
                    bhexbin[2 * i] = Convert.ToByte(c + 0x30);
                }
                else
                {
                    bhexbin[2 * i] = Convert.ToByte(c + 0x37);
                }
                c = Convert.ToByte(bhex[i] & 0x0f);
                if (c < 0x0a)
                {
                    bhexbin[2 * i + 1] = Convert.ToByte(c + 0x30);
                }
                else
                {
                    bhexbin[2 * i + 1] = Convert.ToByte(c + 0x37);
                }
            }
        }

        public static byte[] hex2hexbin(byte[] bhex, int nlen)
        {
            byte[] bhexbin = new byte[nlen * 2];
            hex2hexbin(bhex, bhexbin, nlen);
            return bhexbin;
        }

        public static byte[] str2arr(string s)
        {
            return (new UnicodeEncoding()).GetBytes(s);
        }

        public static string arr2str(byte[] buffer)
        {
            return (new UnicodeEncoding()).GetString(buffer, 0, buffer.Length);
        }

        public static byte[] str2ascarr(string s)
        {
            return System.Text.UnicodeEncoding.Convert(System.Text.Encoding.Unicode,
            System.Text.Encoding.ASCII,
            str2arr(s));
        }

        public static byte[] str2hexascarr(string s)
        {
            byte[] hex = str2ascarr(s);
            byte[] hexbin = hex2hexbin(hex, hex.Length);
            return hexbin;
        }

        public static string ascarr2str(byte[] b)
        {
            return System.Text.UnicodeEncoding.Unicode.GetString(
            System.Text.ASCIIEncoding.Convert(System.Text.Encoding.ASCII,System.Text.Encoding.Unicode,b));
        }

        public static string hexascarr2str(byte[] buffer)
        {
            byte[] b = hex2hexbin(buffer, buffer.Length);
            return ascarr2str(b);
        }

        public static Int32 GetRoundEbOfBend(double _eb)
        {
            int eb = 0;
            //结果修约
            //≤150000 修约到 500MPa,>150000修约到1000MPa
            if (_eb <= 150000)
                eb = (int)(_eb / 500) * 500;
            else if (_eb > 150000)
                eb = (int)(_eb / 1000) * 1000;
            return eb;
        }

        public static Int32 GetRoundStressOfBend(double _stress)
        {
            int stress = 0;
            if (_stress <= 200)
                stress = (int)_stress;
            else if (_stress > 200 && _stress <= 1000)
                stress = (int)(_stress / 5) * 5;
            else if (_stress > 1000)
                stress = (int)(_stress / 10) * 10;
            return stress;
        }

       

    }



}
