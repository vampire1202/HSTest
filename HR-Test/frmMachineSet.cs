using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HR_Test
{
    public partial class frmMachineSet : Form
    {
        private frmMain _fmMain;

        //private string[] _DSensorDescribe = new string[10];//位移传感器描述信息  Displacement
        //private string[] _LSensorDescribe = new string[10];//负荷传感器描述信息  Load
        //private string[] _ESensorDescribe = new string[10];//变形传感器描述信息  Elongate

        public frmMachineSet(frmMain fmMain)
        {
            InitializeComponent();
            _fmMain = fmMain;
        } 
 
        private void tsbtnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        } 

        private void frmMachineSet_Load(object sender, EventArgs e)
        {
            CreateSensorArray();
            CreateSensorsTable(this.listViewShow);
            CreateSensorsTable(this.listViewCtlFH);
            CreateSensorsTable(this.listViewCtlWY);
            CreateSensorsTable(this.listViewCtlBX);
            this.listViewCtlFH.Height = this.listViewCtlBX.Height = this.listViewCtlWY.Height = (this.btnSendSetControl.Bottom - this.listViewCtlFH.Top) / 3 - 3;
            this.listViewCtlWY.Top = this.listViewCtlFH.Height + this.listViewCtlFH.Top + 2;
            this.listViewCtlBX.Top = this.listViewCtlWY.Height + this.listViewCtlWY.Top + 2;
            this.panel3.Left = (this.groupBox3.Width - this.panel3.Width) / 2;
            this.panel3.Top = (this.groupBox3.Height - this.panel3.Height) / 2;
            this.panel4.Left = (this.groupBox4.Width - this.panel4.Width) / 2;
            this.panel4.Top = (this.groupBox4.Height - this.panel4.Height) / 2;
            this.palBXZengyi.Left = (this.panel5.Width - this.palBXZengyi.Width) / 2;
            this.palBXZengyi.Top = (this.panel5.Height - this.palBXZengyi.Height) / 2;

            string machinetype = RWconfig.GetAppSettings("machineType");
            this.txtMinLoad.Text = RWconfig.GetAppSettings("minLoad");
            this.txtBXZengyi.Text = RWconfig.GetAppSettings("BXZengyi");
            if (machinetype == "0")
                this.rbtnSet1.Checked = true;
            else
                this.rbtnSet2.Checked = true; 

        }


        private void CreateSensorArray()
        {
            int m_index = 0;
            int m_value = 0;
            int dSensorCount = 0;
            int lSensorCount = 0;
            int eSensorCount = 0;
            for (int i = 0; i < _fmMain.m_SensorCount; i++)
            {
                m_index = i;
                string[] _arrSensors = new string[4]; // 序号，传感器类型，最大值，通道
                _arrSensors[0] = m_index.ToString();
                switch (_fmMain.m_SensorArray[i].type)
                {
                    case 0x80:                        
                        _arrSensors[1] = "位移";
                        m_value = _fmMain.m_SensorArray[i].scale;
                        m_value = m_value >> 8;
                        m_value = (int)(m_value * Math.Pow(10.0, _fmMain.m_SensorArray[i].scale & 0x000f));
                        _arrSensors[2] = (m_value/1000).ToString() +" (mm)";
                        _arrSensors[3] = _fmMain.m_SensorArray[i].sockt.ToString();
                        //_DSensorDescribe[dSensorCount] = _arrSensors[0] +","+ _arrSensors[1]+",量程:" + _arrSensors[2] + "通道:"+ _arrSensors[3];
                        dSensorCount++;
                        //this.cmbDisplacementShowChl.Items.Add(_arrSensors[0]);
                        //this.cmbDisplacementControlChl.Items.Add(_arrSensors[0]);
                        AddSensorToListview(_arrSensors, this.listViewCtlWY);
                        break;
                    case 0x81:
                        _arrSensors[1] = "负荷";
                        m_value = _fmMain.m_SensorArray[i].scale;
                        m_value = m_value >> 8;
                        m_value = (int)(m_value * Math.Pow(10.0, _fmMain.m_SensorArray[i].scale & 0x000f));
                        _arrSensors[2] = (m_value / 1000).ToString() + " (kN)";
                        _arrSensors[3] = _fmMain.m_SensorArray[i].sockt.ToString();
                        //_LSensorDescribe[lSensorCount] = _arrSensors[0] + "," + _arrSensors[1] + ",量程:" + _arrSensors[2] + "通道:" + _arrSensors[3];
                        lSensorCount++;
                        //this.cmbLoadControlChl.Items.Add(_arrSensors[0]);
                        //this.cmbLoadShowChl.Items.Add(_arrSensors[0]);
                        AddSensorToListview(_arrSensors, this.listViewCtlFH);
                        break;
                    case 0x82:
                        _arrSensors[1] = "变形";
                        m_value = _fmMain.m_SensorArray[i].scale;
                        m_value = m_value >> 8;
                        m_value = (int)(m_value * Math.Pow(10.0, _fmMain.m_SensorArray[i].scale & 0x000f));
                        _arrSensors[2] = (m_value / 1000).ToString() + " (mm)";
                        _arrSensors[3] = _fmMain.m_SensorArray[i].sockt.ToString();
                        //_ESensorDescribe[eSensorCount] = _arrSensors[0] + "," + _arrSensors[1] + ",量程:" + _arrSensors[2] + "通道:" + _arrSensors[3];
                        eSensorCount++;
                        //this.cmbElongateControlChl.Items.Add(_arrSensors[0]);
                        //this.cmbElongateShowChl.Items.Add(_arrSensors[0]);
                        AddSensorToListview(_arrSensors, this.listViewCtlBX);
                        break;
                    default:
                        break;
                }

                AddSensorToListview(_arrSensors, this.listViewShow);
            }
        }

        private void AddSensorToListview(string[] arrSensorInfo, ListView lv)
        {
            ListViewItem item1 = new ListViewItem(arrSensorInfo[0], 0);
            item1.SubItems.Add(arrSensorInfo[1].ToString());
            item1.SubItems.Add(arrSensorInfo[2].ToString());
            item1.SubItems.Add(arrSensorInfo[3].ToString()); 
            item1.ForeColor = Color.Black;
            item1.BackColor = Color.Gold;
            lv.Items.AddRange(new ListViewItem[] { item1 });
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
            lv.CheckBoxes = true; 
            
            // Select the item and subitems when selection is made.
            lv.FullRowSelect = true;
            // Display grid lines.
            lv.GridLines = true;
            // Sort the items in the list in ascending order.
            lv.Sorting = SortOrder.None;
            lv.Columns.Add("序号", 100, HorizontalAlignment.Left);
            lv.Columns.Add("类型", 100, HorizontalAlignment.Left);
            lv.Columns.Add("量程", 200, HorizontalAlignment.Left);
            lv.Columns.Add("通道", 200, HorizontalAlignment.Left);
            lv.Columns.Add("描述", -2, HorizontalAlignment.Left);
        } 

        private void CreateMyListView_C(ListView listView1)
        {
            // Set the view to show details.
            listView1.View = View.Details;
            // Allow the user to edit item text.
            listView1.LabelEdit = false;
            // Allow the user to rearrange columns.
            listView1.AllowColumnReorder = true;
            // Display check boxes.
            listView1.CheckBoxes = true;
            // Select the item and subitems when selection is made.
            listView1.FullRowSelect = true;
            // Display grid lines.
            listView1.GridLines = true;
            // Sort the items in the list in ascending order.
            listView1.Sorting = SortOrder.Ascending;

            // Create three items and three sets of subitems for each item.
            ListViewItem item1 = new ListViewItem("", 0);
            // Place a check mark next to the item.
            item1.Checked = true;
            item1.SubItems.Add("负荷控制通道1"); 
            item1.SubItems.Add(" ");
            item1.SubItems.Add("");

            ListViewItem item2 = new ListViewItem("", 0);
            item2.SubItems.Add("位移控制通道1");
            item2.SubItems.Add(" ");
            item2.SubItems.Add(" ");

            ListViewItem item3 = new ListViewItem("", 0);
            // Place a check mark next to the item.
            item3.Checked = true;
            item3.SubItems.Add("变形控制通道2");
            item3.SubItems.Add(" ");
            item3.SubItems.Add(" ");

            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            listView1.Columns.Add("", -1, HorizontalAlignment.Center);
            listView1.Columns.Add("名称", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("范围", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("参数描述", -2, HorizontalAlignment.Left);

            //Add the items to the ListView.
            listView1.Items.AddRange(new ListViewItem[] { item1, item2, item3 }); 
        }


        private void CreateMyListView(ListView listView1)
        {
            // Set the view to show details.
            listView1.View = View.Details;
            // Allow the user to edit item text.
            listView1.LabelEdit = false;
            // Allow the user to rearrange columns.
            listView1.AllowColumnReorder = true;
            // Display check boxes.
            listView1.CheckBoxes = true;
            // Select the item and subitems when selection is made.
            listView1.FullRowSelect = true;
            // Display grid lines.
            listView1.GridLines = true;
            // Sort the items in the list in ascending order.
            listView1.Sorting = SortOrder.Ascending;

            // Create three items and three sets of subitems for each item.
            ListViewItem item1 = new ListViewItem("", 0);
            // Place a check mark next to the item.
            item1.Checked = true;
            item1.SubItems.Add("负荷传感器");
            item1.SubItems.Add(" ");
            item1.SubItems.Add(""); 

            ListViewItem item2 = new ListViewItem("",0);
            item2.SubItems.Add("位移传感器");
            item2.SubItems.Add(" ");
            item2.SubItems.Add(" "); 

            ListViewItem item3 = new ListViewItem("", 0); 
            // Place a check mark next to the item.
            item3.Checked = true;
            item3.SubItems.Add("引伸计");
            item3.SubItems.Add(" ");
            item3.SubItems.Add(" "); 

            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            listView1.Columns.Add("",-1, HorizontalAlignment.Center);
            listView1.Columns.Add("名称", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("范围", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("参数描述", -2, HorizontalAlignment.Left); 

            //Add the items to the ListView.
            listView1.Items.AddRange(new ListViewItem[] { item1, item2, item3 }); 
        }

        private void tsbtnMinimize_Click(object sender, EventArgs e)
        {
            _fmMain.WindowState = FormWindowState.Minimized;
        }

        private void btnShowSet_Click(object sender, EventArgs e)
        {

        }

        private void btnSendSetShow_Click(object sender, EventArgs e)
        {

        }

        private void rbtnSet1_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnSet1.Checked)
                RWconfig.SetAppSettings("machineType", "0");
        }

        private void rbtnSet2_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnSet2.Checked)
                RWconfig.SetAppSettings("machineType", "1");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMinLoad.Text))
            {
                try
                {
                    float minload = float.Parse(txtMinLoad.Text);
                    if (minload < 0.05 || minload> 2)
                    { 
                        MessageBox.Show("请输入 0.05 - 2 之间的数值,已恢复默认值!"); 
                       return;
                    }
                    else
                    { 
                        RWconfig.SetAppSettings("minLoad", txtMinLoad.Text);
                        MessageBox.Show("保存成功!");
                    }
                }
                catch 
                {
                    MessageBox.Show("输入不正确,已恢复成默认值!");
                }
            }

        }

        private void txtMinLoad_TextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(this.txtMinLoad.Text))
            {
                MessageBox.Show("请输入数字!");
                this.txtMinLoad.Text = "0.1"; 
            } 
        }

        private void btnSendSetControl_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveBXZengyi_Click(object sender, EventArgs e)
        {
            if(txtBXZengyi.Text!=string.Empty)
            {
                if(double.Parse(txtBXZengyi.Text.Trim())!=0)
                {
                    RWconfig.SetAppSettings("BXZengyi", txtBXZengyi.Text);
                    MessageBox.Show("保存成功!");
                }
                else
                {
                    MessageBox.Show("请输入不为0数字");
                }
            }
            else
            {
                MessageBox.Show("请输入数字");
            }
        }

        private void txtBXZengyi_TextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(this.txtBXZengyi.Text))
            {
                MessageBox.Show("请输入数字!");
                this.txtBXZengyi.Text = "1";
            } 
        } 
    }
}
