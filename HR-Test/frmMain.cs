using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
namespace HR_Test
{

    public partial class frmMain : Form
    {
        int m_NuIni;
        public int m_SensorCount;

        public Struc.SensorArray[] m_DSensorArray = new Struc.SensorArray[10];
        public Struc.SensorArray[] m_LSensorArray = new Struc.SensorArray[10];
        public Struc.SensorArray[] m_ESensorArray = new Struc.SensorArray[10];

        public byte SensorArrayFlag;

        public Struc.sensor[] m_SensorArray = new Struc.sensor[30];

        public int m_LoadSensorCount;
        public int m_DisplacementSensorCount;
        public int m_ElongateSensorCount; 

        public Struc.ctrlcommand[] m_CtrlCommandArray = new Struc.ctrlcommand[10];

        public string m_String = string.Empty;

        int m_ControlCount;

        public static string _dataPath = @"E:\衡新试验数据\" + "HR-TestData.mdb";
        public string _conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _dataPath + ";Jet OLEDB:Database password=Password1;Jet OLEDB:Engine Type=5";


        frmTestResult ftr;
        frmInput fi;
        public frmMain()
        {
            InitializeComponent(); 
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //CreateAccessDb();  
            ftr = new frmTestResult(this);
            fi = new frmInput(this);

            //复制数据库
            if (!Directory.Exists(@"E:\衡新试验数据\Curve"))
            {
                Directory.CreateDirectory(@"E:\衡新试验数据\Curve");
            }

            if (!File.Exists(@"E:\衡新试验数据\HR-TestData.mdb"))
            {
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "DataBase\\HR-TestData.mdb", @"E:\衡新试验数据\HR-TestData.mdb");
            }

            //创建密匙
            //string sSecretKey;
            this.MaximizedBounds = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            this.WindowState = FormWindowState.Maximized;   

        }

        protected override void WndProc(ref   Message m)
        {
            base.WndProc(ref   m);
            if (m.Msg == 0x84)     //不让拖动标题栏  
            {
                if ((IntPtr)2 == m.Result)
                    m.Result = (IntPtr)1;
                if (m.Msg == 0x00A3)       //双击标题栏无反应  
                    m.WParam = System.IntPtr.Zero;
            }
        }

        private void glassButton9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            this.palMain.Left = this.Width / 2 - this.palMain.Width / 2;
            this.palMain.Top = this.Height / 2 - this.palMain.Height / 2;
        }

        private void gbtnParameter_Click(object sender, EventArgs e)
        {
            fi.TopLevel = false;
            fi.Parent = this.panelContainer;
            fi.BringToFront();
            fi.Size = this.panelContainer.Size;
            Task t = tskReadMethod();
            fi.Show();
        }

        async Task tskReadMethod()
        {
            var t = Task<List<TreeNode>>.Run(() =>
            {
                return TestStandard.MethodControl.ReadMethodList();
            });
            await t;
            fi.tvTestMethod.Nodes.Clear();
            fi.tvTestMethod.Nodes.Add("试验方法");
            if (t.Result != null)
            {
                List<TreeNode> lsttn = (List<TreeNode>)t.Result;
                foreach (TreeNode tn in lsttn)
                    fi.tvTestMethod.Nodes[0].Nodes.Add(tn);
            }
            fi.tvTestMethod.ExpandAll();
        }


        private void gbtnTestMethod_Click(object sender, EventArgs e)
        {
            //if (m_ElongateSensorCount == 0)
            //{
            //    MessageBox.Show("请先连接设备!");
            //    return;
            //}
            frmTestMethod ftm = new frmTestMethod(this);
            ftm.TopLevel = false;
            ftm.Parent = this.panelContainer;
            ftm.BringToFront();
            ftm.Size = this.panelContainer.Size;
            ftm.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void gbtnSet_Click(object sender, EventArgs e)
        {
            frmMachineSet fms = new frmMachineSet(this);
            fms.TopLevel = false;
            fms.Parent = this.panelContainer;
            fms.BringToFront();
            fms.Size = this.panelContainer.Size;
            fms.Show();
        }

        private void gbtnTest_Click(object sender, EventArgs e)
        {
            ftr.TopLevel = false;
            ftr.Name = "c_testResult";
            ftr.GetTestValue();
            ftr.dateTimePicker_ValueChanged(sender, e);
            ftr._showThreadFlag = true;
            ftr.ThreadSendOrder();           
            ftr.Parent = this.panelContainer; 
            ftr.BringToFront();
            ftr.Size = this.panelContainer.Size;
            if (m_ElongateSensorCount > 1)
                ftr.btnBXShow2.Enabled = true;
            else
                ftr.btnBXShow2.Enabled = false;
            ftr.Show();
        }

        private void gbtnReport_Click(object sender, EventArgs e)
        {
            frmFind ff = new frmFind(this);
            ff.TopLevel = false;
            ff.Parent = this.panelContainer;
            ff.BringToFront();
            ff.Size = this.panelContainer.Size; 
            ff.Show();
        }

        private void gbtnTestMethod_MouseEnter(object sender, EventArgs e)
        {
            Control gb = (Control)sender;
            gb.ForeColor = Color.FromArgb(1,78,133);
        }

        private void gbtnTestMethod_MouseLeave(object sender, EventArgs e)
        {
            Control gb = (Control)sender;
           gb.ForeColor = Color.White; 
        }


        /// <summary>
        /// Listview初始化
        /// </summary>
        private void CreateSensorsTable(ListView lv)
        {
            // Set the view to show details.
            lv.View = View.Details;
            // Allow the user to edit item text.
            lv.LabelEdit = false;
            // Allow the user to rearrange columns.
            lv.AllowColumnReorder = false;
            // Display check boxes.
            lv.CheckBoxes = false;
            // Select the item and subitems when selection is made.
            lv.FullRowSelect = true;
            // Display grid lines.
            lv.GridLines = true;
            // Sort the items in the list in ascending order.
            lv.Sorting = SortOrder.None;
            lv.Columns.Add("序号", 50, HorizontalAlignment.Left);
            lv.Columns.Add("类型", 50, HorizontalAlignment.Left);
            lv.Columns.Add("量程", 100, HorizontalAlignment.Left);
            lv.Columns.Add("通道", 100, HorizontalAlignment.Left);
            lv.Columns.Add("其他", -2, HorizontalAlignment.Left);
        }

        private void btnAccess_Click(object sender, EventArgs e)
        {
            /*
		    在实际的应用程序中，应设置一配置（读取全部传感器信息）完成全局标志。
		    因为如果pc机程序没有配置，而随便发出控制命令，可能造成发给主机的命令
		    不正确而失控！
	        */

            byte[] buf = new byte[203];
            int ret;
            int offset = 0;
            int len = 0;
            int m_value = 0;
            byte i = 0; 
            int m_index = 0;
            byte[] m_temp = new byte[20];

            buf[0] = 0x01;									                //命令字节
            buf[1] = Convert.ToByte(offset / 256);			                //偏移量
            buf[2] = Convert.ToByte(offset % 256);
            buf[3] = Convert.ToByte(len / 256);								//每次读的长度
            buf[4] = Convert.ToByte(len % 256);

            ret = RwUsb.WriteData1582(1, buf, 5, 1000);				        //发送读命令
          
            len = 203;
            ret = RwUsb.ReadData1582(2, buf, len, 5000);				    //读数据
           
            m_ControlCount = len;
            m_NuIni = buf[1];
            m_NuIni = m_NuIni << 8;
            m_NuIni = m_NuIni | buf[0];
            m_SensorCount = buf[2];
            //查找所有传感器
            for (i = 0; i < m_SensorCount; i++)
            {
                m_SensorArray[i].sockt = buf[i * 10 + 3];
                m_SensorArray[i].type = buf[i * 10 + 4];
                m_SensorArray[i].volt = buf[i * 10 + 5];
                m_SensorArray[i].direct = buf[i * 10 + 6];

                m_SensorArray[i].scale = buf[i * 10 + 8];
                m_SensorArray[i].scale = m_SensorArray[i].scale << 8;
                m_SensorArray[i].scale |= buf[i * 10 + 7];

                m_SensorArray[i].balance = buf[i * 10 + 10];
                m_SensorArray[i].balance = m_SensorArray[i].balance << 8;
                m_SensorArray[i].balance |= buf[i * 10 + 9];

                m_SensorArray[i].gain = buf[i * 10 + 12];
                m_SensorArray[i].gain = m_SensorArray[i].gain << 8;
                m_SensorArray[i].gain |= buf[i * 10 + 11];
            }

            m_LoadSensorCount = 0;
            m_DisplacementSensorCount = 0;
            m_ElongateSensorCount = 0;
            //传感器序号及ID
            for (i = 0; i <= m_SensorCount; i++)
            {
                if (m_SensorArray[i].type == 0x80)
                {
                    m_DSensorArray[m_DisplacementSensorCount].SensorIndex =i;
                    
                    m_DisplacementSensorCount++;
                }
                else if (m_SensorArray[i].type == 0x81)
                {
                    m_LSensorArray[m_LoadSensorCount].SensorIndex =i;
                    m_LoadSensorCount++;
                }
                else if (m_SensorArray[i].type == 0x82)
                {
                    m_ESensorArray[m_ElongateSensorCount].SensorIndex = i;
                    m_ElongateSensorCount++;
                }
            }

            SensorArrayFlag = 1;

            if (m_SensorCount > 0)
            {
                //this.tsbtnAccess.BackColor = Color.Gray;
                this.tsbtnAccess.Text = "已连接";
                this.tsbtnAccess.ForeColor = Color.Green;
            }
            else
            {
                //this.tsbtnAccess.BackColor = Color.Gray;
                this.tsbtnAccess.Text = "连接";
                this.tsbtnAccess.ForeColor = Color.Blue;
            }

            //生成传感器列表
            //    m_GridConfig.put_Rows(m_SensorCount+1); 

            for (i = 0; i < m_SensorCount; i++)
            {
                m_index = i;
                string[] _arrSensors = new string[4];
                _arrSensors[0] = m_index.ToString();
                switch (m_SensorArray[i].type)
                {
                    case 0x80:
                        _arrSensors[1] = "位移";
                        m_value = m_SensorArray[i].scale;
                        m_value = m_value >> 8;
                        m_value = (int)(m_value * Math.Pow(10.0, m_SensorArray[i].scale & 0x000f));
                        _arrSensors[2] = m_value.ToString();
                        _arrSensors[3] = m_SensorArray[i].sockt.ToString();
                        break;
                    case 0x81:
                        _arrSensors[1] = "负荷";
                        m_value = m_SensorArray[i].scale;
                        m_value = m_value >> 8;
                        m_value = (int)(m_value * Math.Pow(10.0, m_SensorArray[i].scale & 0x000f));
                        _arrSensors[2] = m_value.ToString();
                        _arrSensors[3] = m_SensorArray[i].sockt.ToString();
                        break;
                    case 0x82:
                        _arrSensors[1] = "变形";
                        m_value = m_SensorArray[i].scale;
                        m_value = m_value >> 8;
                        m_value = (int)(m_value * Math.Pow(10.0, m_SensorArray[i].scale & 0x000f));
                        _arrSensors[2] = m_value.ToString();
                        _arrSensors[3] = m_SensorArray[i].sockt.ToString();
                        break;
                    default:
                        break;
                }

                //AddSensorToListview(_arrSensors, this.listView1);
            }
            ftr.M_DisplacementSensorCount = this.m_DisplacementSensorCount;
            ftr.M_DSensorArray = this.m_DSensorArray;
            ftr.M_ElongateSensorCount = this.m_ElongateSensorCount;
        
            ftr.M_ESensorArray = this.m_ESensorArray;
            ftr.M_LoadSensorCount = this.m_LoadSensorCount;
            ftr.M_LSensorArray = this.m_LSensorArray;
            ftr.M_SensorArray = this.m_SensorArray;
            ftr.M_SensorArrayFlag = this.SensorArrayFlag;
            ftr.M_SensorCount = this.m_SensorCount;
        }

        private void tsbtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsbtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
