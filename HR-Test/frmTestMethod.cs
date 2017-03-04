using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
namespace HR_Test
{
    public partial class frmTestMethod : Form
    {

        private string[] methodItemControlName = new string[12];
        private frmMain _fmMain;
        public frmTestMethod(frmMain fmMain)
        {
            InitializeComponent();
            _fmMain = fmMain;
            Task t = tskReadMethod();
        }



        private void frmTestMethod_Load(object sender, EventArgs e)
        {
            chkAllow_CheckedChanged(sender, e);
            chkYinShen_CheckedChanged(sender, e);
            customTabControl1.TabPages[1].BringToFront();
            _cmbChangeType.SelectedIndex = 0;
            _cmbControlType.SelectedIndex = 0;
            cmbTestType_TextChanged(sender, e);
            customPanel1.Dock = DockStyle.Fill;
            customPanel1.BringToFront();
            InitDbView(dbViewMethod);
            addYsChl(this.cmbYsChl);
            TestStandard.SampleControl.ReadTestStandard(this.cmbTestStandard);
            TestStandard.SampleControl.ReadTestType(this.cmbTestType);
        }

        async Task tskReadMethod()
        {
            var t = Task<List<TreeNode>>.Run(() =>
            {
                return TestStandard.MethodControl.ReadMethodList();
            });
            await t;
            this.tvTestMethod.Nodes.Clear();
            this.tvTestMethod.Nodes.Add("试验方法");
            if (t.Result != null)
            {
                List<TreeNode> lsttn = (List<TreeNode>)t.Result;
                foreach (TreeNode tn in lsttn)
                    this.tvTestMethod.Nodes[0].Nodes.Add(tn);
            }
            this.tvTestMethod.ExpandAll();
        }

        private void addYsChl(ComboBox cmbYsChl)
        {
            cmbYsChl.Items.Clear();
            for (int i = 0; i < _fmMain.m_ElongateSensorCount; i++)
            {
                cmbYsChl.Items.Add(_fmMain.m_ESensorArray[i].SensorIndex);
            }
        }

        private void InitDbView(DataGridView dg)
        {
            dg.RowHeadersVisible = false;
            dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            DataGridViewCheckBoxColumn chkCell = new DataGridViewCheckBoxColumn();
            chkCell.Name = "选择";
            chkCell.Width = 50;
            chkCell.FlatStyle = FlatStyle.Standard;

            DataGridViewTextBoxColumn number = new DataGridViewTextBoxColumn();
            number.Width = 80;
            number.Name = "序号";

            DataGridViewTextBoxColumn ctlTypeNo = new DataGridViewTextBoxColumn();
            ctlTypeNo.Width = 150;
            ctlTypeNo.Name = "控制方式序号";

            DataGridViewTextBoxColumn ctlType = new DataGridViewTextBoxColumn();
            ctlType.Width = 150;
            ctlType.Name = "控制方式";

            DataGridViewTextBoxColumn ctlSpeed = new DataGridViewTextBoxColumn();
            ctlSpeed.Width = 150;
            ctlSpeed.Name = "控制速度";

            DataGridViewTextBoxColumn changeDotNo = new DataGridViewTextBoxColumn();
            changeDotNo.Width = 150;
            changeDotNo.Name = "转换点序号";

            DataGridViewTextBoxColumn changeDot = new DataGridViewTextBoxColumn();
            changeDot.Width = 150;
            changeDot.Name = "转换类型";

            DataGridViewTextBoxColumn changeDotValue = new DataGridViewTextBoxColumn();
            changeDotValue.Width = 150;
            changeDotValue.Name = "转换值";

            //DataGridViewButtonColumn btnCell = new DataGridViewButtonColumn();          
            //btnCell.Width = 50;
            //btnCell.FlatStyle = FlatStyle.Standard;
            //btnCell.DefaultCellStyle.BackColor = Color.Yellow;
            //btnCell.Text = "删除";

            //DataGridViewButtonColumn btnCellEdit = new DataGridViewButtonColumn();
            //btnCellEdit.Width = 50;
            //btnCellEdit.FlatStyle = FlatStyle.Standard;
            //btnCellEdit.DefaultCellStyle.BackColor = Color.Yellow;
            //btnCellEdit.Text = "修改";

            dg.Columns.Insert(0, chkCell);
            dg.Columns.Insert(1, number);
            dg.Columns.Insert(2, ctlTypeNo);
            dg.Columns.Insert(3, ctlType);
            dg.Columns.Insert(4, ctlSpeed);
            dg.Columns.Insert(5, changeDotNo);
            dg.Columns.Insert(6, changeDot);
            dg.Columns.Insert(7, changeDotValue);
            //dg.Columns.Insert(8, btnCell);
            //dg.Columns.Insert(8, btnCellEdit);  
            dg.Columns[2].Visible = false;
            dg.Columns[5].Visible = false;

            foreach (DataGridViewColumn dc in dg.Columns)
            {
                dc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }


        private void glassButton9_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected override void WndProc(ref   System.Windows.Forms.Message m)
        {
            base.WndProc(ref   m);//基类执行 
            if (m.Msg == 132)//鼠标的移动消息（包括非窗口的移动） 
            {
                //基类执行后m有了返回值,鼠标在窗口的每个地方的返回值都不同 
                if ((IntPtr)2 == m.Result)//如果返回值是2，则说明鼠标是在标题拦 
                {
                    //将返回值改为1(窗口的客户区)，这样系统就以为是 
                    //在客户区拖动的鼠标，窗体就不会移动 
                    m.Result = (IntPtr)1;
                }
            }
        }


        //private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    Brush _textBrush;
        //    Brush _bgcolor;

        //    // Get the item from the collection.
        //    TabPage _tabPage = tabControl1.TabPages[e.Index];

        //    // Get the real bounds for the tab rectangle.
        //    Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

        //    if (e.State == DrawItemState.Selected)
        //    {
        //        // Draw a different background color, and don't paint a focus rectangle.
        //        _textBrush = new SolidBrush(Color.FromKnownColor(KnownColor.Black));
        //        _bgcolor = new SolidBrush(Color.FromKnownColor(KnownColor.WhiteSmoke));
        //        g.FillRectangle(_bgcolor, e.Bounds); 
        //    }
        //    else
        //    {
        //        _textBrush = new SolidBrush(Color.FromKnownColor(KnownColor.Black));
        //        _bgcolor = new SolidBrush(Color.FromKnownColor(KnownColor.MenuBar));
        //        g.FillRectangle(_bgcolor, e.Bounds); 
        //    }
        //    // Use our own font.
        //    Font _tabFont = new Font("宋体", (float)12.0, FontStyle.Bold, GraphicsUnit.Point);
        //    // Draw string. Center the text.
        //    StringFormat _stringFlags = new StringFormat();
        //    _stringFlags.Alignment = StringAlignment.Center;
        //    _stringFlags.LineAlignment = StringAlignment.Center;
        //    g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        //}

        private void chkAllow_CheckedChanged(object sender, EventArgs e)
        {
            if (chkisProLoad.Checked == true)
            {
                this.gbAllow.Enabled = true;
            }
            else
            {
                this.gbAllow.Enabled = false;
            }
        }

        private void cmbYZValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbProLoadValueType.SelectedIndex)
            {
                case 0:
                    this.lblkN.Text = "kN";
                    this.lblkNs.Text = "kN/s";
                    this.txtproLoadValue.Enabled = true;
                    this.txtproLoadSpeed.Enabled = true;
                    break;
                case 1:
                    this.lblkN.Text = "mm";
                    this.lblkNs.Text = "mm/min";
                    this.txtproLoadValue.Enabled = true;
                    this.txtproLoadSpeed.Enabled = true;
                    break;
                default:
                    this.txtproLoadValue.Enabled = false;
                    this.txtproLoadSpeed.Enabled = false;
                    break;
            }
        }
        private void frmTestMethod_SizeChanged(object sender, EventArgs e)
        {
            //this.pbTestTitle.Left = this.Width / 2 - this.pbTestTitle.Width / 2;
        }

        private void tsbtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Dispose();
            }
            catch { }
        }

        private void glbtnSaveMethod_Click(object sender, EventArgs e)
        {
            #region 输入参数判断
            string strErr = "";
            if (this.txtmethodName.Text.Trim().Length == 0)
            {
                strErr += "试验方法名称不能为空!\r\n\r\n";
            }

            //选择试验类型
            if (this.cmbTestType.SelectedItem == null)
            {
                strErr += "请选择试验类型!\r\n\r\n";
            }

            //若允许预载
            if (chkisProLoad.Checked)
            {
                if (this.cmbProLoadValueType.Text.Trim().Length == 0)
                {
                    strErr += "预载方式不能为空!\r\n\r\n";
                }

                if (this.txtproLoadValue.Text.Trim().Length == 0)//|| double.Parse(this.txtproLoadValue.Text) == 0d
                {
                    strErr += "预载值不能为空!\r\n\r\n";
                }

                if (this.cmbProLoadCtlMode.Text.Trim().Length == 0)
                {
                    strErr += "预载控制模式不能为空!\r\n\r\n";
                }

                if (this.txtproLoadSpeed.Text.Trim().Length == 0)//|| double.Parse(this.txtproLoadSpeed.Text) == 0d
                {
                    strErr += "预载速度不能为空!\r\n\r\n";
                }
            }

            #region 判断不连续屈服试验参数
            if (rb_1.Checked == true)//若不连续屈服
            {

                //弹性段
                if (this.cmbCtlType1.Text.Trim().Length == 0)
                {
                    strErr += "弹性段控制模式不能为空!\r\n\r\n";
                }

                if (this.txtCtlSpeed1.Text.Trim().Length == 0)//|| double.Parse(this.txtCtlSpeed1.Text) == 0d
                {
                    strErr += "弹性段控制速度不能为空!\r\n\r\n";
                }

                if (this.cmbCtlChange1.Text.Trim().Length == 0)
                {
                    strErr += "弹性段控制转换方式不能为空!\r\n\r\n";
                }

                if (this.txtCtlChangeValue1.Text.Trim().Length == 0)//|| double.Parse(this.txtCtlChangeValue1.Text) == 0d
                {
                    strErr += "弹性段转换值不能为空!\r\n\r\n";
                }

                //屈服段
                if (this.cmbCtlType2.Text.Trim().Length == 0)
                {
                    strErr += "屈服段控制模式不能为空!\r\n\r\n";
                }

                if (this.txtCtlSpeed2.Text.Trim().Length == 0)// || double.Parse(this.txtCtlSpeed2.Text) == 0d
                {
                    strErr += "屈服段控制速度不能为空!\r\n\r\n";
                }

                if (this.cmbCtlChange2.Text.Trim().Length == 0)
                {
                    strErr += "屈服段控制转换方式不能为空!\r\n\r\n";
                }

                if (this.txtCtlChangeValue2.Text.Trim().Length == 0)// || double.Parse(this.txtCtlChangeValue1.Text) == 0d
                {
                    strErr += "屈服段转换值不能为空!\r\n\r\n";
                }

                //加工硬化段
                if (this.cmbCtlType3.Text.Trim().Length == 0)
                {
                    strErr += "加工硬化段控制模式不能为空!\r\n\r\n";
                }

                if (this.txtCtlSpeed3.Text.Trim().Length == 0)//|| double.Parse(this.txtCtlSpeed3.Text) == 0d
                {
                    strErr += "加工硬化段控制速度不能为空!\r\n\r\n";
                }

                if (this.txtStopValueNo.Text.Trim().Length == 0)// || double.Parse(this.txtStopValueNo.Text) == 0d
                {
                    strErr += "加工硬化段转换值不能为空!\r\n\r\n";
                }

                if (this.txtSpeed_bulianxu.Text.Trim().Length == 0)// || double.Parse(this.txtStopValueNo.Text) == 0d
                {
                    strErr += "返回速度值不能为空!\r\n\r\n";
                }
            }
            #endregion

            #region 判断连续屈服试验参数

            if (rb_2.Checked == true)//若连续屈服
            {
                //屈服段
                if (this.cmbCtlType4.Text.Trim().Length == 0)
                {
                    strErr += "屈服段控制模式不能为空!\r\n\r\n";
                }

                if (this.txtCtlSpeed4.Text.Trim().Length == 0)// || double.Parse(this.txtCtlSpeed4.Text) == 0d
                {
                    strErr += "屈服段控制速度不能为空!\r\n\r\n";
                }

                if (this.cmbCtlChange4.Text.Trim().Length == 0)
                {
                    strErr += "屈服段控制转换方式不能为空!\r\n\r\n";
                }

                if (this.txtCtlChangeValue4.Text.Trim().Length == 0)//|| double.Parse(this.txtCtlChangeValue4.Text) == 0d
                {
                    strErr += "屈服段转换值不能为空!\r\n\r\n";
                }

                //试验段
                if (this.cmbCtlType5.Text.Trim().Length == 0)
                {
                    strErr += "试验段控制模式不能为空!\r\n\r\n";
                }

                if (this.txtCtlSpeed5.Text.Trim().Length == 0)//|| double.Parse(this.txtCtlSpeed5.Text) == 0d
                {
                    strErr += "试验段段控制速度不能为空!\r\n\r\n";
                }

                if (this.txtStopValueYes.Text.Trim().Length == 0)// || double.Parse(this.txtStopValueYes.Text) == 0d
                {
                    strErr += "试验段段转换值不能为空!\r\n\r\n";
                }
                if (this.txtSpeed_lianxu.Text.Trim().Length == 0)// || double.Parse(this.txtStopValueNo.Text) == 0d
                {
                    strErr += "返回速度值不能为空!\r\n\r\n";
                }
            }
            #endregion

            if (chkYinShen.Checked)
            {
                if (this.cmbYsType.Text.Trim().Length == 0)
                {
                    strErr += "取引伸计方式不能为空!\r\n\r\n";
                }

                if (this.txtYsValue.Text.Trim().Length == 0)
                {
                    strErr += "取引伸计的值不能为空!\r\n\r\n";
                }

                if (this.cmbYsChl.Text.Trim().Length == 0)
                {
                    strErr += "取引伸计通道不能为空!\r\n\r\n";
                }
                if (this.txtSpeed_zidingyi.Text.Trim().Length == 0)// || double.Parse(this.txtStopValueNo.Text) == 0d
                {
                    strErr += "返回速度值不能为空!\r\n\r\n";
                }
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            // string methodName = this.txtmethodName.Text.Trim(); 
            //是否连续屈服 
            string controlType1 = "-";
            string controlType2 = "-";
            string controlType3 = "-";
            if (rb_1.Checked == true)//若不连续屈服试验
            {
                //弹性段  
                controlType1 = this.cmbCtlType1.SelectedIndex.ToString() + "," + this.txtCtlSpeed1.Text + "," + this.cmbCtlChange1.SelectedIndex.ToString() + "," + this.txtCtlChangeValue1.Text;
                //屈服段
                controlType2 = this.cmbCtlType2.SelectedIndex.ToString() + "," + this.txtCtlSpeed2.Text + "," + this.cmbCtlChange2.SelectedIndex.ToString() + "," + this.txtCtlChangeValue2.Text;
                //停止测试
                controlType3 = this.cmbCtlType3.SelectedIndex.ToString() + "," + this.txtCtlSpeed3.Text;
            }

            if (rb_2.Checked == true)//若连续屈服试验
            {
                //弹性段
                controlType1 = this.cmbCtlType4.SelectedIndex.ToString() + "," + this.txtCtlSpeed4.Text + "," + this.cmbCtlChange4.SelectedIndex.ToString() + "," + this.txtCtlChangeValue4.Text;
                //试验段
                controlType2 = this.cmbCtlType5.SelectedIndex.ToString() + "," + this.txtCtlSpeed5.Text;
                controlType3 = "-";
            }
            #endregion

            #region 根据试验类型存储到不同的方法数据库表

            DataRowView drv = (DataRowView)this.cmbTestStandard.SelectedItem;
            string strResultTableName = drv["resultTableName"].ToString();

            if (drv != null)
            {
                switch (strResultTableName)
                {
                    #region 金属室温拉伸试验
                    case "tb_TestSample":
                        SaveGBT228Method(controlType1, controlType2, controlType3);
                        break;
                    #endregion

                    #region 金属室温压缩试验
                    case "tb_Compress":
                        SaveGBT7314Method(controlType1, controlType2, controlType3);
                        break;
                    #endregion

                    #region 金属室温弯曲试验
                    case "tb_Bend":
                        SaveYBT5349Method(controlType1, controlType2, controlType3);
                        break;
                    #endregion

                    #region 铝合金拉伸试验
                    case "tb_GBT282892012_Tensile":
                        SaveGBT28289Method(controlType1, controlType2, controlType3, "tensile");
                        break;
                    #endregion

                    #region 铝合金剪切试验
                    case "tb_GBT282892012_Shear":
                        SaveGBT28289Method(controlType1, controlType2, controlType3, "shear");
                        break;
                    #endregion

                    #region 铝合金扭转试验
                    case "tb_GBT282892012_Twist":
                        SaveGBT28289Method(controlType1, controlType2, controlType3, "twist");
                        break;
                    #endregion

                    #region 隔热条纵向拉伸试验
                    case "tb_GBT236152009_TensileZong":
                        SaveGBT23615Method(controlType1, controlType2, controlType3, "tensilezong");
                        break;
                    #endregion

                    #region 隔热条横向拉伸试验
                    case "tb_GBT236152009_TensileHeng":
                        SaveGBT23615Method(controlType1, controlType2, controlType3, "tensileheng");
                        break;
                    #endregion

                    #region 定向纤维复合材料拉伸GBT3354
                    case "tb_GBT3354_Samples":
                        SaveGBT3354Method(controlType1, controlType2, controlType3, "tensile");
                        break;
                    #endregion
                }
            }
            #endregion
        }
        private void SaveGBT3354Method(string _controlType1, string _controlType2, string _controlType3, string _testType)
        {
            //查找是否同名
            BLL.GBT3354_Method bllcm = new HR_Test.BLL.GBT3354_Method();
            DataSet cmds = bllcm.GetList("methodName ='" + this.txtmethodName.Text.Trim() + "'");
            if (cmds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("存在同名的试验方法,请重新命名!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            cmds.Dispose();
            HR_Test.Model.GBT3354_Method model = new HR_Test.Model.GBT3354_Method();
            model.methodName = this.txtmethodName.Text.Trim();
            model.xmlPath = this.cmbTestType.Text.ToString();
            model.isProLoad = this.chkisProLoad.Checked;
            model.proLoadType = 0;
            model.proLoadControlType = 0;
            model.proLoadValue = 0;
            model.proLoadSpeed = 0;
            model.circleNum = 0;

            if ((!rb_1.Checked) & (!rb_2.Checked) & (!rb_3.Checked))
            {
                MessageBox.Show(this, "请选择测试类型", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            model.controlType1 = "-";
            model.controlType2 = "-";
            model.controlType3 = "-";
            model.controlType4 = "-";
            model.controlType5 = "-";
            model.controlType6 = "-";
            model.controlType7 = "-";
            model.controlType8 = "-";
            model.controlType9 = "-";
            model.controlType10 = "-";
            model.controlType11 = "-";
            model.controlType12 = "200";
            model.stopValue = 0;

            if (rb_1.Checked == true)
            {
                if (string.IsNullOrEmpty(this.txtStopValueNo.Text))
                {
                    MessageBox.Show(this, "请输入停止值!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                model.isLxqf = 1;
                model.stopValue = double.Parse(this.txtStopValueNo.Text);
                model.controlType1 = _controlType1;
                model.controlType2 = _controlType2;
                model.controlType3 = _controlType3;
                model.controlType12 = this.txtSpeed_bulianxu.Text.Trim();
            }

            if (rb_2.Checked == true)
            {
                if (string.IsNullOrEmpty(this.txtStopValueYes.Text))
                {
                    MessageBox.Show(this, "请输入停止值!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                model.isLxqf = 2;
                model.stopValue = double.Parse(this.txtStopValueYes.Text);
                model.controlType1 = _controlType1;
                model.controlType2 = _controlType2;
                model.controlType3 = _controlType3;
                model.controlType12 = this.txtSpeed_lianxu.Text.Trim();
            }

            if (rb_3.Checked == true)
            {
                model.isLxqf = 3;
                if (string.IsNullOrEmpty(this.txtStopValue.Text))
                {
                    MessageBox.Show(this, "请输入停止值!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(this.txtCircleNum.Text))
                {
                    MessageBox.Show(this, "请输入循环次数!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                model.stopValue = double.Parse(this.txtStopValue.Text);
                model.circleNum = int.Parse(this.txtCircleNum.Text);
                model.controlType12 = this.txtSpeed_zidingyi.Text.Trim();
                if (dbViewMethod.Rows.Count == 0)
                {
                    MessageBox.Show(this, "无自定义试验方法段,不允许保存!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dbViewMethod.Rows.Count > 0)
                    model.controlType1 = dbViewMethod.Rows[0].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 1)
                    model.controlType2 = dbViewMethod.Rows[1].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 2)
                    model.controlType3 = dbViewMethod.Rows[2].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 3)
                    model.controlType4 = dbViewMethod.Rows[3].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 4)
                    model.controlType5 = dbViewMethod.Rows[4].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 5)
                    model.controlType6 = dbViewMethod.Rows[5].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 6)
                    model.controlType7 = dbViewMethod.Rows[6].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 7)
                    model.controlType8 = dbViewMethod.Rows[7].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 8)
                    model.controlType9 = dbViewMethod.Rows[8].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 9)
                    model.controlType10 = dbViewMethod.Rows[9].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 10)
                    model.controlType11 = dbViewMethod.Rows[10].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[7].Value.ToString();
                //if (dbViewMethod.Rows.Count > 11)
                //    model.controlType12 = dbViewMethod.Rows[11].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[7].Value.ToString();

            }
            model.sendCompany = "-";
            model.getSample = "-";
            model.strengthPlate = "-";//加强片:织物,无纬布增强复合材料,铝合金板,无
            model.adhesive = "-";//胶粘剂规格
            model.sampleState = "-";//试样状态
            model.temperature = 0;
            model.humidity = 0;
            model.testStandard = this.cmbTestStandard.Text;
            model.testMethod = this.txtmethodName.Text;
            model.mathineType = "-";
            model.testCondition = "-";
            model.tester = "-";
            model.assessor = "-";
            model.testDate = DateTime.Now;
            model.sampleShape = "-";
            model.w = 0;
            model.h = 0;
            model.d0 = 0;
            model.Do = 0;
            model.S0 = 0;
            model.lL = 0;
            model.lT = 0;
            model.εz1 =0;
            model.εz2 = 0;
            model.selResultID = 0;
            model.extenValue = 0;
            model.extenChannel = 0;
        
            model.sign = "-";

            //是否预载 
            if (chkisProLoad.Checked)
            {
                model.proLoadType = this.cmbProLoadValueType.SelectedIndex;
                model.proLoadControlType = this.cmbProLoadCtlMode.SelectedIndex;
                model.proLoadValue = double.Parse(this.txtproLoadValue.Text);
                model.proLoadSpeed = double.Parse(this.txtproLoadSpeed.Text);
            }

            //是否取引伸计       
            model.isTakeDownExten = this.chkYinShen.Checked;
            if (this.chkYinShen.Checked)
            {
                model.extenChannel = int.Parse(this.cmbYsChl.Text);
                model.extenValue = double.Parse(this.txtYsValue.Text);
                model.selResultID = this.cmbYsType.SelectedIndex;//取引伸计的方式
            }


            Model.GBT3354_Sel mSel = new HR_Test.Model.GBT3354_Sel();
            mSel.methodName = this.txtmethodName.Text;
            mSel.Pmax = this.chkResultList.GetItemChecked(0);   
            mSel.σt = this.chkResultList.GetItemChecked(1);   
            mSel.Et = this.chkResultList.GetItemChecked(2);  
            mSel.μ12 = this.chkResultList.GetItemChecked(3); 
            mSel.ε1t = this.chkResultList.GetItemChecked(4);  
            mSel.Mean = this.chkResultList.GetItemChecked(5); 
            mSel.SD = this.chkResultList.GetItemChecked(6);  
            mSel.Mid = this.chkResultList.GetItemChecked(7);  
            mSel.CV = this.chkResultList.GetItemChecked(8);
            mSel.saveCurve = this.chkResultList.GetItemChecked(9);

            BLL.GBT3354_Sel bllSel = new HR_Test.BLL.GBT3354_Sel();

            if (bllcm.Add(model) && bllSel.Add(mSel))
            {
                MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Task t = tskReadMethod();
            }
            else
            {
                MessageBox.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void SaveGBT23615Method(string _controlType1, string _controlType2, string _controlType3, string _testType)
        {
            //查找是否同名
            BLL.GBT236152009_Method bllcm = new HR_Test.BLL.GBT236152009_Method();
            DataSet cmds = bllcm.GetList("methodName ='" + this.txtmethodName.Text.Trim() + "'");
            if (cmds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("存在同名的试验方法,请重新命名!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            cmds.Dispose();
            HR_Test.Model.GBT236152009_Method model = new HR_Test.Model.GBT236152009_Method();
            model.methodName = this.txtmethodName.Text.Trim();
            //试验类型
            model.xmlPath = this.cmbTestType.Text.ToString();
            model.isProLoad = this.chkisProLoad.Checked;
            model.proLoadType = 0;
            model.proLoadControlType = 0;
            model.proLoadValue = 0;
            model.proLoadSpeed = 0;
            model.circleNum = 0;

            if ((!rb_1.Checked) & (!rb_2.Checked) & (!rb_3.Checked))
            {
                MessageBox.Show(this, "请选择测试类型", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            model.controlType1 = "-";
            model.controlType2 = "-";
            model.controlType3 = "-";
            model.controlType4 = "-";
            model.controlType5 = "-";
            model.controlType6 = "-";
            model.controlType7 = "-";
            model.controlType8 = "-";
            model.controlType9 = "-";
            model.controlType10 = "-";
            model.controlType11 = "-";
            model.controlType12 = "200";
            model.stopValue = 0;

            if (rb_1.Checked == true)
            {
                if (string.IsNullOrEmpty(this.txtStopValueNo.Text))
                {
                    MessageBox.Show(this, "请输入停止值!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                model.isLxqf = 1;
                model.stopValue = double.Parse(this.txtStopValueNo.Text);
                model.controlType1 = _controlType1;
                model.controlType2 = _controlType2;
                model.controlType3 = _controlType3;
                model.controlType12 = this.txtSpeed_bulianxu.Text.Trim();
            }

            if (rb_2.Checked == true)
            {
                if (string.IsNullOrEmpty(this.txtStopValueYes.Text))
                {
                    MessageBox.Show(this, "请输入停止值!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                model.isLxqf = 2;
                model.stopValue = double.Parse(this.txtStopValueYes.Text);
                model.controlType1 = _controlType1;
                model.controlType2 = _controlType2;
                model.controlType3 = _controlType3;
                model.controlType12 = this.txtSpeed_lianxu.Text.Trim();
            }

            if (rb_3.Checked == true)
            {
                model.isLxqf = 3;
                if (string.IsNullOrEmpty(this.txtStopValue.Text))
                {
                    MessageBox.Show(this, "请输入停止值!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(this.txtCircleNum.Text))
                {
                    MessageBox.Show(this, "请输入循环次数!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                model.stopValue = double.Parse(this.txtStopValue.Text);
                model.circleNum = int.Parse(this.txtCircleNum.Text);
                model.controlType12 = this.txtSpeed_zidingyi.Text.Trim();
                if (dbViewMethod.Rows.Count == 0)
                {
                    MessageBox.Show(this, "无自定义试验方法段,不允许保存!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dbViewMethod.Rows.Count > 0)
                    model.controlType1 = dbViewMethod.Rows[0].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 1)
                    model.controlType2 = dbViewMethod.Rows[1].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 2)
                    model.controlType3 = dbViewMethod.Rows[2].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 3)
                    model.controlType4 = dbViewMethod.Rows[3].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 4)
                    model.controlType5 = dbViewMethod.Rows[4].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 5)
                    model.controlType6 = dbViewMethod.Rows[5].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 6)
                    model.controlType7 = dbViewMethod.Rows[6].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 7)
                    model.controlType8 = dbViewMethod.Rows[7].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 8)
                    model.controlType9 = dbViewMethod.Rows[8].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 9)
                    model.controlType10 = dbViewMethod.Rows[9].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 10)
                    model.controlType11 = dbViewMethod.Rows[10].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[7].Value.ToString();
                //if (dbViewMethod.Rows.Count > 11)
                //    model.controlType12 = dbViewMethod.Rows[11].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[7].Value.ToString();

            }

            model.selResultID = 0;
            model.extenValue = 0;
            model.extenChannel = 0;
            // "sendCompany varchar(100)," +//送检单位
            model.sendCompany = "-";
            //"stuffCardNo varchar(100)," +//材料牌号
            model.stuffCardNo = "-";
            //"stuffSpec varchar(100)," +//材料规格
            model.stuffSpec = "-";
            //"stuffType varchar(100)," +//材料类型
            model.stuffType = "-";
            //"hotStatus varchar(100)," +//热处理状态
            model.hotStatus = "-";
            //"temperature double," +//试验温度
            model.temperature = 0;
            //"humidity double," +//试验湿度
            model.humidity = 0;
            //"testStandard varchar(100)," +//试验标准
            model.testStandard = this.cmbTestStandard.Text;
            //"testMethod varchar(100)," +//试验方法
            model.testMethod = this.txtmethodName.Text;
            //"mathineType varchar(100)," +//设备类型
            model.mathineType = "-";
            //"testCondition varchar(100)," +//试验条件
            model.testCondition = "-";
            //"sampleCharacter varchar(100)," +//试样标识
            model.sampleCharacter = "-";
            //"getSample varchar(100)," +//试样取样方向和位置
            model.getSample = "-";
            //"tester varchar(100)," + //试验员
            model.tester = "-";
            //"assessor varchar(100)," + //审核员 
            model.assessor = "-";
            model.sign = "-";
            model.condition = "-";
            model.controlmode = "-";
            model.testType = "-";

            //是否预载 
            if (chkisProLoad.Checked)
            {
                model.proLoadType = this.cmbProLoadValueType.SelectedIndex;
                model.proLoadControlType = this.cmbProLoadCtlMode.SelectedIndex;
                model.proLoadValue = double.Parse(this.txtproLoadValue.Text);
                model.proLoadSpeed = double.Parse(this.txtproLoadSpeed.Text);
            }

            //是否取引伸计       
            model.isTakeDownExten = this.chkYinShen.Checked;
            if (this.chkYinShen.Checked)
            {
                model.extenChannel = int.Parse(this.cmbYsChl.Text);
                model.extenValue = double.Parse(this.txtYsValue.Text);
                model.selResultID = this.cmbYsType.SelectedIndex;//取引伸计的方式
            }

            switch (_testType)
            {
                case "tensilezong"://纵向拉伸
                    Model.GBT236152009_SelZong mSel = new HR_Test.Model.GBT236152009_SelZong();
                    mSel.methodName = this.txtmethodName.Text;
                    mSel.Fmax = this.chkResultList.GetItemChecked(0);
                    mSel.T2 = this.chkResultList.GetItemChecked(1);
                    mSel.Z = this.chkResultList.GetItemChecked(2);
                    mSel.E = this.chkResultList.GetItemChecked(3);
                    mSel.T2_ = this.chkResultList.GetItemChecked(4);
                    mSel.S = this.chkResultList.GetItemChecked(5);
                    mSel.isSaveCurve = this.chkResultList.GetItemChecked(6);
                    BLL.GBT236152009_SelZong bllSel = new HR_Test.BLL.GBT236152009_SelZong();

                    if (bllcm.Add(model) && bllSel.Add(mSel))
                    {
                        MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Task t = tskReadMethod();
                    }
                    else
                    {
                        MessageBox.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case "tensileheng"://横向拉伸
                    Model.GBT236152009_SelHeng msSel = new HR_Test.Model.GBT236152009_SelHeng();
                    msSel.methodName = this.txtmethodName.Text;
                    msSel.Fmax = this.chkResultList.GetItemChecked(0);
                    msSel.T1 = this.chkResultList.GetItemChecked(1);
                    msSel.T1_ = this.chkResultList.GetItemChecked(2);
                    msSel.S = this.chkResultList.GetItemChecked(3);
                    msSel.T1c = this.chkResultList.GetItemChecked(4);
                    msSel.isSaveCurve = this.chkResultList.GetItemChecked(5);

                    BLL.GBT236152009_SelHeng bllsSel = new HR_Test.BLL.GBT236152009_SelHeng();

                    if (bllcm.Add(model) && bllsSel.Add(msSel))
                    {
                        MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Task t = tskReadMethod();
                    }
                    else
                    {
                        MessageBox.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
            }
        }

        private void SaveGBT28289Method(string _controlType1, string _controlType2, string _controlType3, string _testType)
        {
            //查找是否同名
            BLL.GBT282892012_Method bllcm = new HR_Test.BLL.GBT282892012_Method();
            DataSet cmds = bllcm.GetList("methodName ='" + this.txtmethodName.Text.Trim() + "'");
            if (cmds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("存在同名的试验方法,请重新命名!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            cmds.Dispose();
            HR_Test.Model.GBT282892012_Method model = new HR_Test.Model.GBT282892012_Method();
            model.methodName = this.txtmethodName.Text.Trim();
            model.xmlPath = this.cmbTestType.Text.ToString();
            model.isProLoad = this.chkisProLoad.Checked;
            model.proLoadType = 0;
            model.proLoadControlType = 0;
            model.proLoadValue = 0;
            model.proLoadSpeed = 0;
            model.circleNum = 0;

            if ((!rb_1.Checked) & (!rb_2.Checked) & (!rb_3.Checked))
            {
                MessageBox.Show(this, "请选择测试类型", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            model.controlType1 = "-";
            model.controlType2 = "-";
            model.controlType3 = "-";
            model.controlType4 = "-";
            model.controlType5 = "-";
            model.controlType6 = "-";
            model.controlType7 = "-";
            model.controlType8 = "-";
            model.controlType9 = "-";
            model.controlType10 = "-";
            model.controlType11 = "-";
            model.controlType12 = "200";
            model.stopValue = 0;

            if (rb_1.Checked == true)
            {
                if (string.IsNullOrEmpty(this.txtStopValueNo.Text))
                {
                    MessageBox.Show(this, "请输入停止值!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                model.isLxqf = 1;
                model.stopValue = double.Parse(this.txtStopValueNo.Text);
                model.controlType1 = _controlType1;
                model.controlType2 = _controlType2;
                model.controlType3 = _controlType3;
                model.controlType12 = this.txtSpeed_bulianxu.Text.Trim();
            }

            if (rb_2.Checked == true)
            {
                if (string.IsNullOrEmpty(this.txtStopValueYes.Text))
                {
                    MessageBox.Show(this, "请输入停止值!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                model.isLxqf = 2;
                model.stopValue = double.Parse(this.txtStopValueYes.Text);
                model.controlType1 = _controlType1;
                model.controlType2 = _controlType2;
                model.controlType3 = _controlType3;
                model.controlType12 = this.txtSpeed_lianxu.Text.Trim();
            }

            if (rb_3.Checked == true)
            {
                model.isLxqf = 3;

                if (string.IsNullOrEmpty(this.txtStopValue.Text))
                {
                    MessageBox.Show(this, "请输入停止值!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(this.txtCircleNum.Text))
                {
                    MessageBox.Show(this, "请输入循环次数!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                model.stopValue = double.Parse(this.txtStopValue.Text);
                model.circleNum = int.Parse(this.txtCircleNum.Text);
                model.controlType12 = this.txtSpeed_zidingyi.Text.Trim();
                if (dbViewMethod.Rows.Count == 0)
                {
                    MessageBox.Show(this, "无自定义试验方法段,不允许保存!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dbViewMethod.Rows.Count > 0)
                    model.controlType1 = dbViewMethod.Rows[0].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 1)
                    model.controlType2 = dbViewMethod.Rows[1].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 2)
                    model.controlType3 = dbViewMethod.Rows[2].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 3)
                    model.controlType4 = dbViewMethod.Rows[3].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 4)
                    model.controlType5 = dbViewMethod.Rows[4].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 5)
                    model.controlType6 = dbViewMethod.Rows[5].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 6)
                    model.controlType7 = dbViewMethod.Rows[6].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 7)
                    model.controlType8 = dbViewMethod.Rows[7].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 8)
                    model.controlType9 = dbViewMethod.Rows[8].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 9)
                    model.controlType10 = dbViewMethod.Rows[9].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 10)
                    model.controlType11 = dbViewMethod.Rows[10].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[7].Value.ToString();
                //if (dbViewMethod.Rows.Count > 11)
                //    model.controlType12 = dbViewMethod.Rows[11].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[7].Value.ToString();

            }

            model.selResultID = 0;
            model.extenValue = 0;
            model.extenChannel = 0;
            // "sendCompany varchar(100)," +//送检单位
            model.sendCompany = "-";
            //"stuffCardNo varchar(100)," +//材料牌号
            model.stuffCardNo = "-";
            //"stuffSpec varchar(100)," +//材料规格
            model.stuffSpec = "-";
            //"stuffType varchar(100)," +//材料类型
            model.stuffType = "-";
            //"hotStatus varchar(100)," +//热处理状态
            model.hotStatus = "-";
            //"temperature double," +//试验温度
            model.temperature = 0;
            //"humidity double," +//试验湿度
            model.humidity = 0;
            //"testStandard varchar(100)," +//试验标准
            model.testStandard = this.cmbTestStandard.Text;
            //"testMethod varchar(100)," +//试验方法
            model.testMethod = this.txtmethodName.Text;
            //"mathineType varchar(100)," +//设备类型
            model.mathineType = "-";
            //"testCondition varchar(100)," +//试验条件
            model.testCondition = "-";
            //"sampleCharacter varchar(100)," +//试样标识
            model.sampleCharacter = "-";
            //"getSample varchar(100)," +//试样取样方向和位置
            model.getSample = "-";
            //"tester varchar(100)," + //试验员
            model.tester = "-";
            //"assessor varchar(100)," + //审核员 
            model.assessor = "-";
            model.sign = "-";
            model.condition = "-";
            model.controlmode = "-";
            model.testType = "-";

            //是否预载 
            if (chkisProLoad.Checked)
            {
                model.proLoadType = this.cmbProLoadValueType.SelectedIndex;
                model.proLoadControlType = this.cmbProLoadCtlMode.SelectedIndex;
                model.proLoadValue = double.Parse(this.txtproLoadValue.Text);
                model.proLoadSpeed = double.Parse(this.txtproLoadSpeed.Text);
            }

            //是否取引伸计       
            model.isTakeDownExten = this.chkYinShen.Checked;
            if (this.chkYinShen.Checked)
            {
                model.extenChannel = int.Parse(this.cmbYsChl.Text);
                model.extenValue = double.Parse(this.txtYsValue.Text);
                model.selResultID = this.cmbYsType.SelectedIndex;//取引伸计的方式
            }

            switch (_testType)
            {
                case "tensile"://拉伸
                    Model.GBT282892012_TensileSel mSel = new HR_Test.Model.GBT282892012_TensileSel();
                    mSel.methodName = this.txtmethodName.Text;
                    mSel.FQmax = this.chkResultList.GetItemChecked(0);
                    mSel.Q = this.chkResultList.GetItemChecked(1);
                    mSel.Q_ = this.chkResultList.GetItemChecked(2);
                    mSel.SQ = this.chkResultList.GetItemChecked(3);
                    mSel.Qc = this.chkResultList.GetItemChecked(4);
                    mSel.isSaveCurve = this.chkResultList.GetItemChecked(5);
                    BLL.GBT282892012_TensileSel bllSel = new HR_Test.BLL.GBT282892012_TensileSel();

                    if (bllcm.Add(model) && bllSel.Add(mSel))
                    {
                        MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Task t = tskReadMethod();
                    }
                    else
                    {
                        MessageBox.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case "shear"://剪切
                    Model.GBT282892012_ShearSel msSel = new HR_Test.Model.GBT282892012_ShearSel();
                    msSel.methodName = this.txtmethodName.Text;
                    msSel.FTmax = this.chkResultList.GetItemChecked(0);
                    msSel.T = this.chkResultList.GetItemChecked(1);
                    msSel.C1 = this.chkResultList.GetItemChecked(2);
                    msSel.C1R_ = this.chkResultList.GetItemChecked(3);
                    msSel.C1cR = this.chkResultList.GetItemChecked(4);
                    msSel.C1L_ = this.chkResultList.GetItemChecked(5);
                    msSel.C1cL = this.chkResultList.GetItemChecked(6);
                    msSel.C1H_ = this.chkResultList.GetItemChecked(7);
                    msSel.C1cH = this.chkResultList.GetItemChecked(8);
                    msSel.T_ = this.chkResultList.GetItemChecked(9);
                    msSel.ST = this.chkResultList.GetItemChecked(10);
                    msSel.Tc = this.chkResultList.GetItemChecked(11);
                    msSel.saveCurve = this.chkResultList.GetItemChecked(12);

                    BLL.GBT282892012_ShearSel bllsSel = new HR_Test.BLL.GBT282892012_ShearSel();

                    if (bllcm.Add(model) && bllsSel.Add(msSel))
                    {
                        MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Task t = tskReadMethod();
                    }
                    else
                    {
                        MessageBox.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                case "twist"://扭转
                    Model.GBT282892012_TwistSel mtSel = new HR_Test.Model.GBT282892012_TwistSel();
                    mtSel.methodName = this.txtmethodName.Text;
                    mtSel.FMmax = this.chkResultList.GetItemChecked(0);
                    mtSel.M = this.chkResultList.GetItemChecked(1);
                    mtSel.M_ = this.chkResultList.GetItemChecked(2);
                    mtSel.SM = this.chkResultList.GetItemChecked(3);
                    mtSel.Mc = this.chkResultList.GetItemChecked(4);
                    mtSel.saveCurve = this.chkResultList.GetItemChecked(5);
                    BLL.GBT282892012_TwistSel blltSel = new HR_Test.BLL.GBT282892012_TwistSel();
                    if (bllcm.Add(model) && blltSel.Add(mtSel))
                    {
                        MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Task t = tskReadMethod();
                    }
                    else
                    {
                        MessageBox.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
            }
        }

        private void SaveGBT228Method(string _controlType1, string _controlType2, string _controlType3)
        {
            //查找是否同名
            BLL.ControlMethod bllcm = new HR_Test.BLL.ControlMethod();
            DataSet cmds = bllcm.GetList("methodName ='" + this.txtmethodName.Text.Trim() + "'");
            if (cmds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("存在同名的试验方法,请重新命名!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            cmds.Dispose();
            HR_Test.Model.ControlMethod model = new HR_Test.Model.ControlMethod();
            model.methodName = this.txtmethodName.Text.Trim();
            model.xmlPath = this.cmbTestType.Text.ToString();
            model.isProLoad = this.chkisProLoad.Checked;
            model.proLoadType = 0;
            model.proLoadControlType = 0;
            model.proLoadValue = 0;
            model.proLoadSpeed = 0;
            model.circleNum = 0;

            if ((!rb_1.Checked) & (!rb_2.Checked) & (!rb_3.Checked))
            {
                MessageBox.Show(this, "请选择测试类型", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            model.controlType1 = "-";
            model.controlType2 = "-";
            model.controlType3 = "-";
            model.controlType4 = "-";
            model.controlType5 = "-";
            model.controlType6 = "-";
            model.controlType7 = "-";
            model.controlType8 = "-";
            model.controlType9 = "-";
            model.controlType10 = "-";
            model.controlType11 = "-";
            model.controlType12 = "200";
            model.stopValue = 0;

            if (rb_1.Checked == true)
            {
                if (string.IsNullOrEmpty(this.txtStopValueNo.Text))
                {
                    MessageBox.Show(this, "请输入停止值!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                model.isLxqf = 1;
                model.stopValue = double.Parse(this.txtStopValueNo.Text);
                model.controlType1 = _controlType1;
                model.controlType2 = _controlType2;
                model.controlType3 = _controlType3;
                model.controlType12 = this.txtSpeed_bulianxu.Text.Trim();
            }

            if (rb_2.Checked == true)
            {
                if (string.IsNullOrEmpty(this.txtStopValueYes.Text))
                {
                    MessageBox.Show(this, "请输入停止值!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                model.isLxqf = 2;
                model.stopValue = double.Parse(this.txtStopValueYes.Text);
                model.controlType1 = _controlType1;
                model.controlType2 = _controlType2;
                model.controlType3 = _controlType3;
                model.controlType12 = this.txtSpeed_lianxu.Text.Trim();
            }

            if (rb_3.Checked == true)
            {
                model.isLxqf = 3;
                if (string.IsNullOrEmpty(this.txtStopValue.Text))
                {
                    MessageBox.Show(this, "请输入停止值!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(this.txtCircleNum.Text))
                {
                    MessageBox.Show(this, "请输入循环次数!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                model.stopValue = double.Parse(this.txtStopValue.Text);
                model.circleNum = int.Parse(this.txtCircleNum.Text);
                model.controlType12 = this.txtSpeed_zidingyi.Text.Trim();
                if (dbViewMethod.Rows.Count == 0)
                {
                    MessageBox.Show(this, "无自定义试验方法段,不允许保存!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dbViewMethod.Rows.Count > 0)
                    model.controlType1 = dbViewMethod.Rows[0].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 1)
                    model.controlType2 = dbViewMethod.Rows[1].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 2)
                    model.controlType3 = dbViewMethod.Rows[2].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 3)
                    model.controlType4 = dbViewMethod.Rows[3].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 4)
                    model.controlType5 = dbViewMethod.Rows[4].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 5)
                    model.controlType6 = dbViewMethod.Rows[5].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 6)
                    model.controlType7 = dbViewMethod.Rows[6].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 7)
                    model.controlType8 = dbViewMethod.Rows[7].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 8)
                    model.controlType9 = dbViewMethod.Rows[8].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 9)
                    model.controlType10 = dbViewMethod.Rows[9].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 10)
                    model.controlType11 = dbViewMethod.Rows[10].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[7].Value.ToString();
                //if (dbViewMethod.Rows.Count > 11)
                //    model.controlType12 = dbViewMethod.Rows[11].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[7].Value.ToString();

            }
            model.selResultID = 0;
            model.extenValue = 0;
            model.extenChannel = 0;
            // "sendCompany varchar(100)," +//送检单位
            model.sendCompany = "-";
            //"stuffCardNo varchar(100)," +//材料牌号
            model.stuffCardNo = "-";
            //"stuffSpec varchar(100)," +//材料规格
            model.stuffSpec = "-";
            //"stuffType varchar(100)," +//材料类型
            model.stuffType = "-";
            //"hotStatus varchar(100)," +//热处理状态
            model.hotStatus = "-";
            //"temperature double," +//试验温度
            model.temperature = 0;
            //"humidity double," +//试验湿度
            model.humidity = 0;
            //"testStandard varchar(100)," +//试验标准
            model.testStandard = this.cmbTestStandard.Text;
            //"testMethod varchar(100)," +//试验方法
            model.testMethod = this.txtmethodName.Text;
            //"mathineType varchar(100)," +//设备类型
            model.mathineType = "-";
            //"testCondition varchar(100)," +//试验条件
            model.testCondition = "-";
            //"sampleCharacter varchar(100)," +//试样标识
            model.sampleCharacter = "-";
            //"getSample varchar(100)," +//试样取样方向和位置
            model.getSample = "-";
            //"tester varchar(100)," + //试验员
            model.tester = "-";
            //"assessor varchar(100)," + //审核员 
            model.assessor = "-";
            //"a0 double," + //矩形横截面试样原始厚度或原始管壁厚度
            model.a0 = 0;
            //"au double," + //
            model.au = 0;
            //"b0 double," + //矩形横截面试样平行长度的原始宽度或管的纵向剖条宽度或扁丝原始宽度
            model.b0 = 0;
            //"bu double," + //
            model.bu = 0;
            //"d0 double," + //圆形横截面试样平行长度的原始直径或圆丝原始直径的原始内径
            model.d0 = 0;
            //"du double," + //
            model.du = 0;
            //"Do double," + //管原始外直径
            model.Do = 0;
            //"L0 double," + //原始标距
            model.L0 = 0;
            //"L01 double" + //测定Awn的原始标距
            model.L01 = 0;
            //"Lc double," + //平行长度
            model.Lc = 0;
            //"Le double," + //引伸计标距
            model.Le = 0;
            //"Lt double," + //试样总长度
            model.Lt = 0;
            //"Lu double," + //断后标距
            model.Lu = 0;
            //"Lu1 double," + //测量Awn的断后标距
            model.Lu1 = 0;
            //"S0 double," + //原始横截面面积
            model.S0 = 0;
            //"Su double," + //断后最小横截面面积
            model.Su = 0;
            //"k double," +//比例系数 
            model.k = 0;
            model.sign = "-";
            model.condition = "-";
            model.controlmode = "-";

            //是否预载 
            if (chkisProLoad.Checked)
            {
                model.proLoadType = this.cmbProLoadValueType.SelectedIndex;
                model.proLoadControlType = this.cmbProLoadCtlMode.SelectedIndex;
                model.proLoadValue = double.Parse(this.txtproLoadValue.Text);
                model.proLoadSpeed = double.Parse(this.txtproLoadSpeed.Text);
            }

            //是否取引伸计       
            model.isTakeDownExten = this.chkYinShen.Checked;
            if (this.chkYinShen.Checked)
            {
                model.extenChannel = int.Parse(this.cmbYsChl.Text);
                model.extenValue = double.Parse(this.txtYsValue.Text);
                model.selResultID = this.cmbYsType.SelectedIndex;//取引伸计的方式
            }


            Model.SelTestResult mSel = new HR_Test.Model.SelTestResult();
            mSel.methodName = this.txtmethodName.Text;
            mSel.Fm = this.chkResultList.GetItemChecked(0);   //"             Fm               kN                 最大力",
            mSel.Rm = this.chkResultList.GetItemChecked(1);   //"             Rm               MPa                抗拉强度",
            mSel.ReH = this.chkResultList.GetItemChecked(2);  //"             ReH              MPa                上屈服强度",
            mSel.ReL = this.chkResultList.GetItemChecked(3);  //"             ReL              MPa                下屈服强度",
            mSel.Rp = this.chkResultList.GetItemChecked(4);   //"             Rp               MPa                规定塑性延伸强度",
            mSel.Rt = this.chkResultList.GetItemChecked(5);   //"             Rt               MPa                规定总延伸强度",
            mSel.Rr = this.chkResultList.GetItemChecked(6);   //"             Rr               MPa                规定残余延伸强度",
            mSel.E = this.chkResultList.GetItemChecked(7);    //"             E                GPa                弹性模量",
            mSel.m = this.chkResultList.GetItemChecked(8);    //"             m                MPa                应力-延伸率曲线在给定试验时刻的斜率",
            mSel.mE = this.chkResultList.GetItemChecked(9);   //"             mE               MPa                应力-延伸率曲线弹性部分的斜率",
            mSel.A = this.chkResultList.GetItemChecked(10);    //"             A                %                  断后伸长率",  
            mSel.Aee = this.chkResultList.GetItemChecked(11);  //"             Ae               %                  屈服点延伸率",
            mSel.Agg = this.chkResultList.GetItemChecked(12);  //"             Ag               %                  最大力Fm塑性延伸率",
            mSel.Att = this.chkResultList.GetItemChecked(13);  //"             At               %                  断裂总延伸率",            
            mSel.Aggtt = this.chkResultList.GetItemChecked(14);//"             Agt              %                  最大力Fm总延伸率",
            mSel.Awnwn = this.chkResultList.GetItemChecked(15);//"             Awn              %                  无缩颈塑性伸长率", 
            mSel.deltaLm = this.chkResultList.GetItemChecked(16);   //"             △Lm             mm                 最大力总延伸",
            mSel.Lf = this.chkResultList.GetItemChecked(17);   //"             △Lf             mm                 断裂总延伸", 
            mSel.Z = this.chkResultList.GetItemChecked(18);    //"             Z                %                  断面收缩率",
            mSel.Avera = this.chkResultList.GetItemChecked(19);//"             X                MPa                平均值",
            mSel.SS = this.chkResultList.GetItemChecked(20);   //"             S                %                  标准偏差",
            mSel.Avera1 = this.chkResultList.GetItemChecked(21);//"             X￣              MPa               中间值", 
            mSel.CV = this.chkResultList.GetItemChecked(22);  //CV
            mSel.Handaz = this.chkResultList.GetItemChecked(23);
            mSel.Savecurve = this.chkResultList.GetItemChecked(24);
            mSel.Lm = this.chkResultList.GetItemChecked(25);

            BLL.SelTestResult bllSel = new HR_Test.BLL.SelTestResult();

            if (bllcm.Add(model) && bllSel.Add(mSel))
            {
                MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Task t = tskReadMethod();
            }
            else
            {
                MessageBox.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void SaveGBT7314Method(string _controlType1, string _controlType2, string _controlType3)
        {
            //查找是否同名
            BLL.ControlMethod_C bllcm_C = new HR_Test.BLL.ControlMethod_C();
            DataSet cmds_C = bllcm_C.GetList("methodName ='" + this.txtmethodName.Text.Trim() + "'");
            if (cmds_C.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show(this, "存在同名的试验方法,请重新命名!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            cmds_C.Dispose();
            HR_Test.Model.ControlMethod_C model_C = new HR_Test.Model.ControlMethod_C();
            model_C.methodName = this.txtmethodName.Text.Trim();
            model_C.xmlPath = this.cmbTestType.Text.ToString();
            model_C.isProLoad = this.chkisProLoad.Checked;
            model_C.proLoadType = 0;
            model_C.proLoadControlType = 0;
            model_C.proLoadValue = 0;
            model_C.proLoadSpeed = 0;
            model_C.circleNum = 0;
            if (rb_1.Checked == true)
            {
                model_C.isLxqf = 1;
                model_C.stopValue = double.Parse(this.txtStopValueNo.Text);
                model_C.controlType1 = _controlType1;
                model_C.controlType2 = _controlType2;
                model_C.controlType3 = _controlType3;
                model_C.controlType4 = "-";
                model_C.controlType5 = "-";
                model_C.controlType6 = "-";
                model_C.controlType7 = "-";
                model_C.controlType8 = "-";
                model_C.controlType9 = "-";
                model_C.controlType10 = "-";
                model_C.controlType11 = "-";
                model_C.controlType12 = this.txtSpeed_bulianxu.Text.Trim();
            }

            if (rb_2.Checked == true)
            {
                model_C.isLxqf = 2;
                model_C.stopValue = double.Parse(this.txtStopValueYes.Text);
                model_C.controlType1 = _controlType1;
                model_C.controlType2 = _controlType2;
                model_C.controlType3 = _controlType3;
                model_C.controlType4 = "-";
                model_C.controlType5 = "-";
                model_C.controlType6 = "-";
                model_C.controlType7 = "-";
                model_C.controlType8 = "-";
                model_C.controlType9 = "-";
                model_C.controlType10 = "-";
                model_C.controlType11 = "-";
                model_C.controlType12 = this.txtSpeed_lianxu.Text.Trim();
            }

            if (rb_3.Checked == true)
            {
                model_C.isLxqf = 3;
                model_C.stopValue = double.Parse(this.txtStopValue.Text);
                model_C.circleNum = int.Parse(this.txtCircleNum.Text);
                model_C.controlType1 = "-";
                model_C.controlType2 = "-";
                model_C.controlType3 = "-";
                model_C.controlType4 = "-";
                model_C.controlType5 = "-";
                model_C.controlType6 = "-";
                model_C.controlType7 = "-";
                model_C.controlType8 = "-";
                model_C.controlType9 = "-";
                model_C.controlType10 = "-";
                model_C.controlType11 = "-";
                model_C.controlType12 = this.txtSpeed_zidingyi.Text.Trim();

                if (dbViewMethod.Rows.Count > 0)
                    model_C.controlType1 = dbViewMethod.Rows[0].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 1)
                    model_C.controlType2 = dbViewMethod.Rows[1].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 2)
                    model_C.controlType3 = dbViewMethod.Rows[2].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 3)
                    model_C.controlType4 = dbViewMethod.Rows[3].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 4)
                    model_C.controlType5 = dbViewMethod.Rows[4].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 5)
                    model_C.controlType6 = dbViewMethod.Rows[5].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 6)
                    model_C.controlType7 = dbViewMethod.Rows[6].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 7)
                    model_C.controlType8 = dbViewMethod.Rows[7].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 8)
                    model_C.controlType9 = dbViewMethod.Rows[8].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 9)
                    model_C.controlType10 = dbViewMethod.Rows[9].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 10)
                    model_C.controlType11 = dbViewMethod.Rows[10].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[7].Value.ToString();
                //if (dbViewMethod.Rows.Count > 11)
                //    model_C.controlType12 = dbViewMethod.Rows[11].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[7].Value.ToString();

            }

            model_C.selResultID = 0;
            model_C.extenValue = 0;
            model_C.extenChannel = 0;

            // "sendCompany varchar(100)," +//送检单位
            model_C.sendCompany = "-";
            //"stuffCardNo varchar(100)," +//材料牌号
            model_C.stuffCardNo = "-";
            //"stuffSpec varchar(100)," +//材料规格
            model_C.stuffSpec = "-";
            //"stuffType varchar(100)," +//材料类型
            model_C.stuffType = "-";
            //"hotStatus varchar(100)," +//热处理状态
            model_C.hotStatus = "-";
            //"temperature double," +//试验温度
            model_C.temperature = 0;
            //"humidity double," +//试验湿度
            model_C.humidity = 0;
            //"testStandard varchar(100)," +//试验标准
            model_C.testStandard = this.cmbTestStandard.Text;
            //"testMethod varchar(100)," +//试验方法
            model_C.testMethod = this.txtmethodName.Text;
            //"mathineType varchar(100)," +//设备类型
            model_C.mathineType = "-";
            //"testCondition varchar(100)," +//试验条件
            model_C.testCondition = "-";
            //"sampleCharacter varchar(100)," +//试样标识
            model_C.sampleCharacter = "-";
            //"getSample varchar(100)," +//试样取样方向和位置
            model_C.getSample = "-";
            //"tester varchar(100)," + //试验员
            model_C.tester = "-";
            //"assessor varchar(-100)," + //审核员 
            model_C.assessor = "-";
            model_C.Ff = 0;
            model_C.sign = "-";
            model_C.condition = "-";
            model_C.controlmode = "-";

            //"a double," + // 试样 原始 厚度 
            model_C.a = 0;
            //  "b double," + // 试样 原始宽度 
            model_C.b = 0;
            //  "d double," + // 试样 原始直径  
            model_C.d = 0;
            //  "L double," + // 试样 长度 
            model_C.L = 0;
            //  "L0 double," + //试样原始标距
            model_C.L0 = 0;
            //  "H double," + //约束装置高度
            model_C.H = 0;
            //  "hh double," + //板材试样无约束部分的长度  
            model_C.hh = 0;
            //  "S0 double," + //原始横截面面积  
            model_C.S0 = 0;
            model_C.Ff = 0;

            //是否预载 
            if (chkisProLoad.Checked)
            {
                model_C.proLoadType = this.cmbProLoadValueType.SelectedIndex;
                model_C.proLoadControlType = this.cmbProLoadCtlMode.SelectedIndex;
                model_C.proLoadValue = double.Parse(this.txtproLoadValue.Text);
                model_C.proLoadSpeed = double.Parse(this.txtproLoadSpeed.Text);
            }

            //是否取引伸计       
            model_C.isTakeDownExten = this.chkYinShen.Checked;
            if (this.chkYinShen.Checked)
            {
                model_C.extenChannel = int.Parse(this.cmbYsChl.Text);
                model_C.extenValue = double.Parse(this.txtYsValue.Text);
                model_C.selResultID = this.cmbYsType.SelectedIndex;//取引伸计的方式
            }


            Model.SelTestResult_C mSel_C = new HR_Test.Model.SelTestResult_C();
            mSel_C.methodName = this.txtmethodName.Text.Trim();
            mSel_C.Fmc = this.chkResultList.GetItemChecked(0);
            mSel_C.Fpc = this.chkResultList.GetItemChecked(1);
            mSel_C.Ftc = this.chkResultList.GetItemChecked(2);
            mSel_C.FeHc = this.chkResultList.GetItemChecked(3);
            mSel_C.FeLc = this.chkResultList.GetItemChecked(4);
            mSel_C.Rmc = this.chkResultList.GetItemChecked(5);
            mSel_C.Rpc = this.chkResultList.GetItemChecked(6);
            mSel_C.Rtc = this.chkResultList.GetItemChecked(7);
            mSel_C.ReHc = this.chkResultList.GetItemChecked(8);
            mSel_C.ReLc = this.chkResultList.GetItemChecked(9);
            mSel_C.Ec = this.chkResultList.GetItemChecked(10);
            mSel_C.Mean = this.chkResultList.GetItemChecked(11);
            mSel_C.SD = this.chkResultList.GetItemChecked(12);
            mSel_C.Mid = this.chkResultList.GetItemChecked(13);
            mSel_C.CV = this.chkResultList.GetItemChecked(14);
            mSel_C.saveCurve = this.chkResultList.GetItemChecked(15);

            BLL.SelTestResult_C bllSel_C = new HR_Test.BLL.SelTestResult_C();
            if (bllSel_C.Add(mSel_C) && bllcm_C.Add(model_C))
            {
                MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Task t = tskReadMethod();
            }
            else
            {
                MessageBox.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaveYBT5349Method(string _controlType1, string _controlType2, string _controlType3)
        {
            //查找是否同名
            BLL.ControlMethod_B bllcm_B = new HR_Test.BLL.ControlMethod_B();
            DataSet cmds_B = bllcm_B.GetList(" methodName ='" + this.txtmethodName.Text.Trim() + "'");
            if (cmds_B.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show(this, "存在同名的试验方法,请重新命名!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            cmds_B.Dispose();

            HR_Test.Model.ControlMethod_B model_B = new HR_Test.Model.ControlMethod_B();
            model_B.methodName = this.txtmethodName.Text.Trim();
            //试验类型
            model_B.xmlPath = this.cmbTestType.Text.ToString();
            model_B.isProLoad = this.chkisProLoad.Checked;
            model_B.proLoadType = 0;
            model_B.proLoadControlType = 0;
            model_B.proLoadValue = 0;
            model_B.proLoadSpeed = 0;
            model_B.circleNum = 0;

            if (rb_1.Checked == true)
            {
                model_B.isLxqf = 1;
                model_B.stopValue = double.Parse(this.txtStopValueNo.Text);
                model_B.controlType1 = _controlType1;
                model_B.controlType2 = _controlType2;
                model_B.controlType3 = _controlType3;
                model_B.controlType4 = "-";
                model_B.controlType5 = "-";
                model_B.controlType6 = "-";
                model_B.controlType7 = "-";
                model_B.controlType8 = "-";
                model_B.controlType9 = "-";
                model_B.controlType10 = "-";
                model_B.controlType11 = "-";
                model_B.controlType12 = this.txtSpeed_bulianxu.Text.Trim();
            }

            if (rb_2.Checked == true)
            {
                model_B.isLxqf = 2;
                model_B.stopValue = double.Parse(this.txtStopValueYes.Text);
                model_B.controlType1 = _controlType1;
                model_B.controlType2 = _controlType2;
                model_B.controlType3 = _controlType3;
                model_B.controlType4 = "-";
                model_B.controlType5 = "-";
                model_B.controlType6 = "-";
                model_B.controlType7 = "-";
                model_B.controlType8 = "-";
                model_B.controlType9 = "-";
                model_B.controlType10 = "-";
                model_B.controlType11 = "-";
                model_B.controlType12 = this.txtSpeed_lianxu.Text.Trim();
            }

            if (rb_3.Checked == true)
            {
                model_B.isLxqf = 3;
                model_B.stopValue = double.Parse(this.txtStopValue.Text);
                model_B.circleNum = int.Parse(this.txtCircleNum.Text);
                model_B.controlType1 = "-";
                model_B.controlType2 = "-";
                model_B.controlType3 = "-";
                model_B.controlType4 = "-";
                model_B.controlType5 = "-";
                model_B.controlType6 = "-";
                model_B.controlType7 = "-";
                model_B.controlType8 = "-";
                model_B.controlType9 = "-";
                model_B.controlType10 = "-";
                model_B.controlType11 = "-";
                model_B.controlType12 = this.txtSpeed_zidingyi.Text.Trim();

                if (dbViewMethod.Rows.Count > 0)
                    model_B.controlType1 = dbViewMethod.Rows[0].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 1)
                    model_B.controlType2 = dbViewMethod.Rows[1].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 2)
                    model_B.controlType3 = dbViewMethod.Rows[2].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 3)
                    model_B.controlType4 = dbViewMethod.Rows[3].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 4)
                    model_B.controlType5 = dbViewMethod.Rows[4].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 5)
                    model_B.controlType6 = dbViewMethod.Rows[5].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 6)
                    model_B.controlType7 = dbViewMethod.Rows[6].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 7)
                    model_B.controlType8 = dbViewMethod.Rows[7].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 8)
                    model_B.controlType9 = dbViewMethod.Rows[8].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 9)
                    model_B.controlType10 = dbViewMethod.Rows[9].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[7].Value.ToString();
                if (dbViewMethod.Rows.Count > 10)
                    model_B.controlType11 = dbViewMethod.Rows[10].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[7].Value.ToString();
                //if (dbViewMethod.Rows.Count > 11)
                //    model_B.controlType12 = dbViewMethod.Rows[11].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[7].Value.ToString();

            }

            model_B.selResultID = 0;
            model_B.extenValue = 0;
            model_B.extenChannel = 0;
            model_B.sendCompany = "-";
            model_B.stuffCardNo = "-";
            model_B.stuffSpec = "-";
            model_B.stuffType = "-";
            model_B.hotStatus = "-";
            model_B.temperature = 0;
            model_B.humidity = 0;
            model_B.testStandard = this.cmbTestStandard.Text;
            model_B.testMethod = this.txtmethodName.Text;
            model_B.mathineType = "-";
            model_B.testCondition = "-";
            model_B.sampleCharacter = "-";
            model_B.getSample = "-";
            model_B.tester = "-";
            model_B.assessor = "-";
            model_B.sign = "-";
            model_B.condition = "-";
            model_B.controlmode = "-";

            model_B.Ds = 0; //支撑滚柱直径
            model_B.Da = 0; //施力滚柱直径
            model_B.R = 0; //刀刃半径 
            model_B.Ls = 0; //跨距
            model_B.Le = 0; //扰度计跨距
            model_B.l_l = 0;//力臂 
            model_B.m = 0;
            model_B.n = 0;
            model_B.a = 0;
            model_B.εrb = 0;
            model_B.εpb = 0;
            model_B.testType = "三点弯曲";

            //是否预载 
            if (chkisProLoad.Checked)
            {
                model_B.proLoadType = this.cmbProLoadValueType.SelectedIndex;
                model_B.proLoadControlType = this.cmbProLoadCtlMode.SelectedIndex;
                model_B.proLoadValue = double.Parse(this.txtproLoadValue.Text);
                model_B.proLoadSpeed = double.Parse(this.txtproLoadSpeed.Text);
            }

            //是否取引伸计       
            model_B.isTakeDownExten = this.chkYinShen.Checked;
            if (this.chkYinShen.Checked)
            {
                model_B.extenChannel = int.Parse(this.cmbYsChl.Text);
                model_B.extenValue = double.Parse(this.txtYsValue.Text);
                model_B.selResultID = this.cmbYsType.SelectedIndex;//取引伸计的方式
            }

            BLL.SelTestResult_B bllSel_B = new HR_Test.BLL.SelTestResult_B();
            Model.SelTestResult_B mSel_B = new HR_Test.Model.SelTestResult_B();

            mSel_B.methodName = this.txtmethodName.Text;
            //"             fbb              mm                 断裂扰度",
            mSel_B.f_bb = this.chkResultList.GetItemChecked(0);
            //"             fn               mm                 最后一次施力并将其卸除后的残余扰度",
            mSel_B.f_n = this.chkResultList.GetItemChecked(1);
            
            //"             fn-1             mm                 最后前一次施力并将其卸除后的残余扰度", 
            mSel_B.f_n1 = this.chkResultList.GetItemChecked(2);
            //"             frb              mm                 达到规定残余弯曲应变时的残余扰度",  
            mSel_B.f_rb = this.chkResultList.GetItemChecked(3);
            //"             y                mm                 试样弯曲时中性面至弯曲外表面的最大距离",  
            mSel_B.y = this.chkResultList.GetItemChecked(4);
            //"             Fo               N                  预弯曲力",  
            mSel_B.Fo = this.chkResultList.GetItemChecked(5);
            //"             Fpb              N                  规定非比例弯曲力",  
            mSel_B.Fpb = this.chkResultList.GetItemChecked(6);
            //"             Frb              N                  规定残余弯曲力",  
            mSel_B.Frb = this.chkResultList.GetItemChecked(7);
            //"             Fbb              N                  最大弯曲力",  
            mSel_B.Fbb = this.chkResultList.GetItemChecked(8);
            //"             Fn               N                  最后一次施加的弯曲力",  
            mSel_B.Fn = this.chkResultList.GetItemChecked(9);
            //"             Fn-1             N                  最后前一次施加的弯曲力",  
            mSel_B.Fn1 = this.chkResultList.GetItemChecked(10);
            //"             Z                N/mm               力轴每毫米代表的力值",  
            mSel_B.Z = this.chkResultList.GetItemChecked(11);
            //"             S                ㎜²                弯曲试验曲线下包围的面积", 
            mSel_B.S = this.chkResultList.GetItemChecked(12);
            //"             W                ㎜³                试样截面系数",  
            mSel_B.W = this.chkResultList.GetItemChecked(13);
            //"             I                ㎜^4               试样截面惯性矩",  
            mSel_B.I = this.chkResultList.GetItemChecked(14);
            //"             Eb               MPa                弯曲弹性模量",  
            mSel_B.Eb = this.chkResultList.GetItemChecked(15);
            //"             σpb             MPa                规定非比例弯曲应力", 
            mSel_B.σpb = this.chkResultList.GetItemChecked(16);
            //"             σrb             MPa                规定残余弯曲应力",  
            mSel_B.σrb = this.chkResultList.GetItemChecked(17);
            //"             σbb             MPa                抗弯强度",  
            mSel_B.σbb = this.chkResultList.GetItemChecked(18);
            //"             U                MPa                弯曲断裂能量",  
            mSel_B.U = this.chkResultList.GetItemChecked(19);
            //"             Mean.            -                  平均值",  
            mSel_B.Mean = this.chkResultList.GetItemChecked(20);
            //"             S.D.             -                  标准偏差",  
            mSel_B.SD = this.chkResultList.GetItemChecked(21);
            //"             Mid.             -                  中间值", 
            mSel_B.Mid = this.chkResultList.GetItemChecked(22);
            //"             C.V.             -                  变异系数", 
            mSel_B.CV = this.chkResultList.GetItemChecked(23);
            //"             曲线             -                  是否保存曲线", 
            mSel_B.saveCurve = this.chkResultList.GetItemChecked(24);


            if (bllcm_B.Add(model_B) && bllSel_B.Add(mSel_B))
            {
                MessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Task t = tskReadMethod();
            }
            else
            {
                MessageBox.Show("保存失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //鼠标点击读取试验方法
        private void tvTestMethod_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ClearMethod();
            if (e.Node.Tag != null)
            {
                this.customTabControl1.SelectedIndex = 1;
                string methodName = e.Node.Text.Trim();
                switch (e.Node.Tag.ToString())
                {
                    case "GB/T 23615.1-2009":
                        ReadGBT23615(methodName);
                        break;
                    case "GB/T 228-2010":
                        ReadGBT228(methodName);
                        break;
                    case "GB/T 7314-2005":
                        ReadGBT7314(methodName);
                        break;
                    case "YB/T 5349-2006":
                        ReadYBT5349(methodName);
                        break;
                    case "GB/T 28289-2012":
                        ReadGBT28289(methodName);
                        break;
                    case "GB/T 3354-2014":
                        ReadGBT3354(methodName);
                        break;
                }
            }
        }

        private void ClearMethod()
        {
            this.cmbCtlType1.SelectedIndex = -1;
            this.cmbCtlType2.SelectedIndex = -1;
            this.cmbCtlType3.SelectedIndex = -1;
            this.cmbCtlType4.SelectedIndex = -1;
            this.cmbCtlChange1.SelectedIndex = -1;
            this.cmbCtlChange2.SelectedIndex = -1;
            this.cmbCtlChange4.SelectedIndex = -1;
            this.txtCtlSpeed1.Text = this.txtCtlSpeed2.Text = this.txtCtlSpeed3.Text = this.txtCtlSpeed4.Text 
                = this.txtCtlSpeed5.Text = this.txtCtlChangeValue1.Text = this.txtCtlChangeValue2.Text = this.txtCtlChangeValue4.Text  
                ="";
            if (dbViewMethod.Rows.Count > 0)
                dbViewMethod.Rows.Clear();
        }

        //GB/T 23615.1-2009
        private void ReadGBT23615(string _methodName)
        {
            BLL.GBT236152009_Method bllCm_C = new HR_Test.BLL.GBT236152009_Method();
            Model.GBT236152009_Method mCm_C = bllCm_C.GetModel(_methodName);
            this.txtmethodName.Text = mCm_C.methodName;
            this.chkisProLoad.Checked = mCm_C.isProLoad;
            
            this.cmbTestType.Text = mCm_C.xmlPath;
            this.cmbTestStandard.Text = mCm_C.testStandard;
           

            //若允许预载
            if (mCm_C.isProLoad)
            {
                this.cmbProLoadValueType.SelectedIndex = int.Parse(mCm_C.proLoadType.ToString());
                this.txtproLoadValue.Text = mCm_C.proLoadValue.ToString();
                this.txtproLoadSpeed.Text = mCm_C.proLoadSpeed.ToString();
                this.cmbProLoadCtlMode.SelectedIndex = int.Parse(mCm_C.proLoadControlType.ToString());
            }

            //bool isContinued_C = mCm_C.isLxqf;
            //如果不连续屈服
            if (mCm_C.isLxqf == 1)
            {
                rb_1.Checked = true;
                string[] control1 = mCm_C.controlType1.Split(',');
                string[] control2 = mCm_C.controlType2.Split(',');
                string[] control3 = mCm_C.controlType3.Split(',');
                string stopValue = mCm_C.stopValue.ToString();
                //弹性段
                this.cmbCtlType1.SelectedIndex = int.Parse(control1[0]);
                this.txtCtlSpeed1.Text = control1[1];
                this.cmbCtlChange1.SelectedIndex = int.Parse(control1[2]);
                this.txtCtlChangeValue1.Text = control1[3];
                //屈服段
                this.cmbCtlType2.SelectedIndex = int.Parse(control2[0]);
                this.txtCtlSpeed2.Text = control2[1];
                this.cmbCtlChange2.SelectedIndex = int.Parse(control2[2]);
                this.txtCtlChangeValue2.Text = control2[3];
                //加工硬化段                    
                this.cmbCtlType3.SelectedIndex = int.Parse(control3[0]);
                this.txtCtlSpeed3.Text = control3[1];
                //停止测试
                this.txtStopValueNo.Text = stopValue;
                this.txtSpeed_bulianxu.Text = mCm_C.controlType12.ToString();
            }

            if (mCm_C.isLxqf == 2)
            {//如果连续屈服 
                rb_2.Checked = true;
                string[] control1 = mCm_C.controlType1.Split(',');
                string[] control2 = mCm_C.controlType2.Split(',');
                string stopValue = mCm_C.stopValue.ToString();
                //弹性段
                this.cmbCtlType4.SelectedIndex = int.Parse(control1[0]);
                this.txtCtlSpeed4.Text = control1[1];
                this.cmbCtlChange4.SelectedIndex = int.Parse(control1[2]);
                this.txtCtlChangeValue4.Text = control1[3];
                //加工硬化段                    
                this.cmbCtlType5.SelectedIndex = int.Parse(control2[0]);
                this.txtCtlSpeed5.Text = control2[1];
                //停止测试
                this.txtStopValueYes.Text = stopValue;
                this.txtSpeed_lianxu.Text = mCm_C.controlType12.ToString();
            }

            if (mCm_C.isLxqf == 3)
            {//如果自定义试验段 
                //先清空表格中的数据
                this.dbViewMethod.Rows.Clear();
                rb_3.Checked = true;
                this.txtStopValue.Text = mCm_C.stopValue.ToString();
                this.txtCircleNum.Text = mCm_C.circleNum.Value.ToString();
                this.txtSpeed_zidingyi.Text = mCm_C.controlType12.ToString();
                string[] control1 = null;
                string[] control2 = null;
                string[] control3 = null;
                string[] control4 = null;
                string[] control5 = null;
                string[] control6 = null;
                string[] control7 = null;
                string[] control8 = null;
                string[] control9 = null;
                string[] control10 = null;
                string[] control11 = null;

                if (mCm_C.controlType1 != "-")
                {
                    control1 = mCm_C.controlType1.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control1[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control1[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "0", int.Parse(control1[0]), strCtrol, control1[1], int.Parse(control1[2]), strChange, control1[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType2 != "-")
                {
                    control2 = mCm_C.controlType2.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control2[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control2[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "1", int.Parse(control2[0]), strCtrol, control2[1], int.Parse(control2[2]), strChange, control2[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType3 != "-")
                {
                    control3 = mCm_C.controlType3.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control3[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control3[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "2", int.Parse(control3[0]), strCtrol, control3[1], int.Parse(control3[2]), strChange, control3[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType4 != "-")
                {
                    control4 = mCm_C.controlType4.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control4[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control4[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "3", int.Parse(control4[0]), strCtrol, control4[1], int.Parse(control4[2]), strChange, control4[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType5 != "-")
                {
                    control5 = mCm_C.controlType5.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control5[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control5[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "4", int.Parse(control5[0]), strCtrol, control5[1], int.Parse(control5[2]), strChange, control5[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }
                if (mCm_C.controlType6 != "-")
                {
                    control6 = mCm_C.controlType6.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control6[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control6[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "5", int.Parse(control6[0]), strCtrol, control6[1], int.Parse(control6[2]), strChange, control6[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType7 != "-")
                {
                    control7 = mCm_C.controlType7.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control7[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control7[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "6", int.Parse(control7[0]), strCtrol, control7[1], int.Parse(control7[2]), strChange, control7[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType8 != "-")
                {
                    control8 = mCm_C.controlType8.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control8[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control8[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "7", int.Parse(control8[0]), strCtrol, control8[1], int.Parse(control8[2]), strChange, control8[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }
                if (mCm_C.controlType9 != "-")
                {
                    control9 = mCm_C.controlType9.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control9[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control9[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "8", int.Parse(control9[0]), strCtrol, control9[1], int.Parse(control9[2]), strChange, control9[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType10 != "-")
                {
                    control10 = mCm_C.controlType10.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control10[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control10[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "9", int.Parse(control10[0]), strCtrol, control10[1], int.Parse(control10[2]), strChange, control10[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType11 != "-")
                {
                    control11 = mCm_C.controlType11.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control11[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control11[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "10", int.Parse(control11[0]), strCtrol, control11[1], int.Parse(control11[2]), strChange, control11[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }
            line: ;
            }
            //this.txtCircleNum.Text = mCm_C.circleNum.Value.ToString();
            //是否取引伸计
            this.chkYinShen.Checked = mCm_C.isTakeDownExten;
            if (chkYinShen.Checked)
            {
                this.cmbYsChl.Text = mCm_C.extenChannel.ToString();
                this.cmbYsType.SelectedIndex = (int)mCm_C.selResultID;
                this.txtYsValue.Text = mCm_C.extenValue.ToString();
            }

            //读取试验结果表
            switch (mCm_C.xmlPath)
            {
                case "纵向拉伸":
                    ReadResultSel("tb_GBT236152009_SelZong");
                    BLL.GBT236152009_SelZong bllSel_T = new HR_Test.BLL.GBT236152009_SelZong();
                    DataSet dsSel_T = bllSel_T.GetList("methodName='" + this.txtmethodName.Text + "'");
                    if (dsSel_T.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 2; i < dsSel_T.Tables[0].Columns.Count; i++)
                        {
                            if (dsSel_T.Tables[0].Rows[0][i].ToString().ToLower() == "true")
                            {
                                this.chkResultList.SetItemChecked(i - 2, true);
                            }
                            else
                            {
                                this.chkResultList.SetItemChecked(i - 2, false);
                            }
                        }
                    }
                    break;
                case "横向拉伸":
                    ReadResultSel("tb_GBT236152009_SelHeng");
                    BLL.GBT236152009_SelHeng bllSel_S = new HR_Test.BLL.GBT236152009_SelHeng();
                    DataSet dsSel_S = bllSel_S.GetList("methodName='" + this.txtmethodName.Text + "'");
                    if (dsSel_S.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 2; i < dsSel_S.Tables[0].Columns.Count; i++)
                        {
                            if (dsSel_S.Tables[0].Rows[0][i].ToString().ToLower() == "true")
                            {
                                this.chkResultList.SetItemChecked(i - 2, true);
                            }
                            else
                            {
                                this.chkResultList.SetItemChecked(i - 2, false);
                            }
                        }
                    }
                    break;
            }

        }

        //读取 GB/T 228-2010 
        private void ReadGBT228(string _methodName)
        {
            BLL.ControlMethod bllCm = new HR_Test.BLL.ControlMethod();
            Model.ControlMethod mCm = bllCm.GetModel(_methodName);
            this.txtmethodName.Text = mCm.methodName;
            this.chkisProLoad.Checked = mCm.isProLoad;

            this.cmbTestType.Text = mCm.xmlPath;
            this.cmbTestStandard.Text = mCm.testStandard; 

            ReadResultSel("tb_SelTestResult");

            //若允许预载
            if (mCm.isProLoad)
            {
                this.cmbProLoadValueType.SelectedIndex = int.Parse(mCm.proLoadType.ToString());
                this.txtproLoadValue.Text = mCm.proLoadValue.ToString();
                this.txtproLoadSpeed.Text = mCm.proLoadSpeed.ToString();
                this.cmbProLoadCtlMode.SelectedIndex = int.Parse(mCm.proLoadControlType.ToString());
            }

            //如果不连续屈服
            if (mCm.isLxqf == 1)
            {
                this.txtStopValueNo.Text = mCm.stopValue.ToString();
                rb_1.Checked = true;
                string[] control1 = mCm.controlType1.Split(',');
                string[] control2 = mCm.controlType2.Split(',');
                string[] control3 = mCm.controlType3.Split(',');
                string stopValue = mCm.stopValue.ToString();
                //弹性段
                this.cmbCtlType1.SelectedIndex = int.Parse(control1[0]);
                this.txtCtlSpeed1.Text = control1[1];
                this.cmbCtlChange1.SelectedIndex = int.Parse(control1[2]);
                this.txtCtlChangeValue1.Text = control1[3];
                //屈服段
                this.cmbCtlType2.SelectedIndex = int.Parse(control2[0]);
                this.txtCtlSpeed2.Text = control2[1];
                this.cmbCtlChange2.SelectedIndex = int.Parse(control2[2]);
                this.txtCtlChangeValue2.Text = control2[3];
                //加工硬化段                    
                this.cmbCtlType3.SelectedIndex = int.Parse(control3[0]);
                this.txtCtlSpeed3.Text = control3[1];
                //停止测试
                this.txtStopValueNo.Text = stopValue;
                this.txtSpeed_bulianxu.Text = mCm.controlType12.ToString();
            }

            if (mCm.isLxqf == 2)
            {//如果连续屈服 
                rb_2.Checked = true;
                this.txtStopValueYes.Text = mCm.stopValue.ToString();
                string[] control1 = mCm.controlType1.Split(',');
                string[] control2 = mCm.controlType2.Split(',');
                string stopValue = mCm.stopValue.ToString();
                //弹性段
                this.cmbCtlType4.SelectedIndex = int.Parse(control1[0]);
                this.txtCtlSpeed4.Text = control1[1];
                this.cmbCtlChange4.SelectedIndex = int.Parse(control1[2]);
                this.txtCtlChangeValue4.Text = control1[3];
                //加工硬化段                    
                this.cmbCtlType5.SelectedIndex = int.Parse(control2[0]);
                this.txtCtlSpeed5.Text = control2[1];
                //停止测试
                this.txtStopValueYes.Text = stopValue;
                this.txtSpeed_lianxu.Text = mCm.controlType12.ToString();
            }

            if (mCm.isLxqf == 3)
            {//如果自定义试验段  
                this.txtStopValue.Text = mCm.stopValue.ToString();
                this.txtCircleNum.Text = mCm.circleNum.Value.ToString();
                this.txtSpeed_zidingyi.Text = mCm.controlType12.ToString();
                dbViewMethod.Rows.Clear();
                rb_3.Checked = true;
                string[] control1 = null;
                string[] control2 = null;
                string[] control3 = null;
                string[] control4 = null;
                string[] control5 = null;
                string[] control6 = null;
                string[] control7 = null;
                string[] control8 = null;
                string[] control9 = null;
                string[] control10 = null;
                string[] control11 = null;
                if (mCm.controlType1 != "-")
                {
                    control1 = mCm.controlType1.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control1[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control1[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "0", int.Parse(control1[0]), strCtrol, control1[1], int.Parse(control1[2]), strChange, control1[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm.controlType2 != "-")
                {
                    control2 = mCm.controlType2.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control2[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control2[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "1", int.Parse(control2[0]), strCtrol, control2[1], int.Parse(control2[2]), strChange, control2[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm.controlType3 != "-")
                {
                    control3 = mCm.controlType3.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control3[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control3[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "2", int.Parse(control3[0]), strCtrol, control3[1], int.Parse(control3[2]), strChange, control3[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm.controlType4 != "-")
                {
                    control4 = mCm.controlType4.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control4[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control4[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "3", int.Parse(control4[0]), strCtrol, control4[1], int.Parse(control4[2]), strChange, control4[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm.controlType5 != "-")
                {
                    control5 = mCm.controlType5.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control5[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control5[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "4", int.Parse(control5[0]), strCtrol, control5[1], int.Parse(control5[2]), strChange, control5[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }
                if (mCm.controlType6 != "-")
                {
                    control6 = mCm.controlType6.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control6[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control6[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "5", int.Parse(control6[0]), strCtrol, control6[1], int.Parse(control6[2]), strChange, control6[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm.controlType7 != "-")
                {
                    control7 = mCm.controlType7.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control7[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control7[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "6", int.Parse(control7[0]), strCtrol, control7[1], int.Parse(control7[2]), strChange, control7[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm.controlType8 != "-")
                {
                    control8 = mCm.controlType8.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control8[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control8[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "7", int.Parse(control8[0]), strCtrol, control8[1], int.Parse(control8[2]), strChange, control8[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }
                if (mCm.controlType9 != "-")
                {
                    control9 = mCm.controlType9.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control9[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control9[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "8", int.Parse(control9[0]), strCtrol, control9[1], int.Parse(control9[2]), strChange, control9[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm.controlType10 != "-")
                {
                    control10 = mCm.controlType10.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control10[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control10[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "9", int.Parse(control10[0]), strCtrol, control10[1], int.Parse(control10[2]), strChange, control10[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm.controlType11 != "-")
                {
                    control11 = mCm.controlType11.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control11[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control11[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "10", int.Parse(control11[0]), strCtrol, control11[1], int.Parse(control11[2]), strChange, control11[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }
            line: ;
            }


            //是否取引伸计
            this.chkYinShen.Checked = mCm.isTakeDownExten;
            if (mCm.isTakeDownExten)
            {
                this.cmbYsChl.Text = mCm.extenChannel.ToString();
                this.cmbYsType.SelectedIndex = (int)mCm.selResultID;
                this.txtYsValue.Text = mCm.extenValue.ToString();
            }

            //读取试验结果表
            BLL.SelTestResult bllSel = new HR_Test.BLL.SelTestResult(); 
            Model.SelTestResult mSel = bllSel.GetModel(this.txtmethodName.Text);
            if (mSel != null)
            {
                this.chkResultList.SetItemChecked(0, mSel.Fm);  //"             Fm               kN                 最大力",
                this.chkResultList.SetItemChecked(1, mSel.Rm);   //"             Rm               MPa                抗拉强度",
                this.chkResultList.SetItemChecked(2, mSel.ReH);  //"             ReH              MPa                上屈服强度",
                this.chkResultList.SetItemChecked(3, mSel.ReL);  //"             ReL              MPa                下屈服强度",
                this.chkResultList.SetItemChecked(4, mSel.Rp);   //"             Rp               MPa                规定塑性延伸强度",
                this.chkResultList.SetItemChecked(5, mSel.Rt);   //"             Rt               MPa                规定总延伸强度",
                this.chkResultList.SetItemChecked(6, mSel.Rr);   //"             Rr               MPa                规定残余延伸强度",
                this.chkResultList.SetItemChecked(7, mSel.E);    //"             E                GPa                弹性模量",
                this.chkResultList.SetItemChecked(8, mSel.m);    //"             m                MPa                应力-延伸率曲线在给定试验时刻的斜率",
                this.chkResultList.SetItemChecked(9, mSel.mE);   //"             mE               MPa                应力-延伸率曲线弹性部分的斜率",
                this.chkResultList.SetItemChecked(10, mSel.A);    //"             A                %                  断后伸长率",  
                this.chkResultList.SetItemChecked(11, mSel.Aee);  //"             Ae               %                  屈服点延伸率",
                this.chkResultList.SetItemChecked(12, mSel.Agg);  //"             Ag               %                  最大力Fm塑性延伸率",
                this.chkResultList.SetItemChecked(13, mSel.Att);  //"             At               %                  断裂总延伸率",            
                this.chkResultList.SetItemChecked(14, mSel.Aggtt);//"             Agt              %                  最大力Fm总延伸率",
                this.chkResultList.SetItemChecked(15, mSel.Awnwn);//"             Awn              %                  无缩颈塑性伸长率", 
                this.chkResultList.SetItemChecked(16, mSel.deltaLm);   //"             △Lm             mm                 最大力总延伸",
                this.chkResultList.SetItemChecked(17, mSel.Lf);   //"             △Lf             mm                 断裂总延伸", 
                this.chkResultList.SetItemChecked(18, mSel.Z);    //"             Z                %                  断面收缩率",
                this.chkResultList.SetItemChecked(19, mSel.Avera);//"             X                MPa                平均值",
                this.chkResultList.SetItemChecked(20, mSel.SS);   //"             S                %                  标准偏差",
                this.chkResultList.SetItemChecked(21, mSel.Avera1);//"             X￣              MPa               中间值", 
                this.chkResultList.SetItemChecked(22, mSel.CV);  //CV
                this.chkResultList.SetItemChecked(23, mSel.Handaz);
                this.chkResultList.SetItemChecked(24, mSel.Savecurve);
                this.chkResultList.SetItemChecked(25, mSel.Lm);
            }


        }

        //读取 GB/T 7314-2005 
        private void ReadGBT7314(string _methodName)
        {
            BLL.ControlMethod_C bllCm_C = new HR_Test.BLL.ControlMethod_C();
            Model.ControlMethod_C mCm_C = bllCm_C.GetModel(_methodName);
            this.txtmethodName.Text = mCm_C.methodName;
            this.chkisProLoad.Checked = mCm_C.isProLoad;

            this.cmbTestType.Text = mCm_C.xmlPath;
            this.cmbTestStandard.Text = mCm_C.testStandard;

            ReadResultSel("tb_SelTestResult_C");
            //若允许预载
            if (mCm_C.isProLoad)
            {
                this.cmbProLoadValueType.SelectedIndex = int.Parse(mCm_C.proLoadType.ToString());
                this.txtproLoadValue.Text = mCm_C.proLoadValue.ToString();
                this.txtproLoadSpeed.Text = mCm_C.proLoadSpeed.ToString();
                this.cmbProLoadCtlMode.SelectedIndex = int.Parse(mCm_C.proLoadControlType.ToString());

            }

            //bool isContinued_C = mCm_C.isLxqf;
            //如果不连续屈服
            if (mCm_C.isLxqf == 1)
            {
                rb_1.Checked = true;
                string[] control1 = mCm_C.controlType1.Split(',');
                string[] control2 = mCm_C.controlType2.Split(',');
                string[] control3 = mCm_C.controlType3.Split(',');
                string stopValue = mCm_C.stopValue.ToString();
                //弹性段
                this.cmbCtlType1.SelectedIndex = int.Parse(control1[0]);
                this.txtCtlSpeed1.Text = control1[1];
                this.cmbCtlChange1.SelectedIndex = int.Parse(control1[2]);
                this.txtCtlChangeValue1.Text = control1[3];
                //屈服段
                this.cmbCtlType2.SelectedIndex = int.Parse(control2[0]);
                this.txtCtlSpeed2.Text = control2[1];
                this.cmbCtlChange2.SelectedIndex = int.Parse(control2[2]);
                this.txtCtlChangeValue2.Text = control2[3];
                //加工硬化段                    
                this.cmbCtlType3.SelectedIndex = int.Parse(control3[0]);
                this.txtCtlSpeed3.Text = control3[1];
                //停止测试
                this.txtStopValueNo.Text = stopValue;
                this.txtSpeed_lianxu.Text = mCm_C.controlType12.ToString();
            }

            if (mCm_C.isLxqf == 2)
            {//如果连续屈服 
                rb_2.Checked = true;
                string[] control1 = mCm_C.controlType1.Split(',');
                string[] control2 = mCm_C.controlType2.Split(',');
                string stopValue = mCm_C.stopValue.ToString();
                //弹性段
                this.cmbCtlType4.SelectedIndex = int.Parse(control1[0]);
                this.txtCtlSpeed4.Text = control1[1];
                this.cmbCtlChange4.SelectedIndex = int.Parse(control1[2]);
                this.txtCtlChangeValue4.Text = control1[3];
                //加工硬化段                    
                this.cmbCtlType5.SelectedIndex = int.Parse(control2[0]);
                this.txtCtlSpeed5.Text = control2[1];
                //停止测试
                this.txtStopValueYes.Text = stopValue;
                this.txtSpeed_bulianxu.Text = mCm_C.controlType12.ToString();
            }

            if (mCm_C.isLxqf == 3)
            {//如果自定义试验段 
                //先清空表格中的数据
                this.dbViewMethod.Rows.Clear();
                rb_3.Checked = true;
                this.txtStopValue.Text = mCm_C.stopValue.ToString();
                this.txtCircleNum.Text = mCm_C.circleNum.Value.ToString();
                this.txtSpeed_zidingyi.Text = mCm_C.controlType12.ToString();
                string[] control1 = null;
                string[] control2 = null;
                string[] control3 = null;
                string[] control4 = null;
                string[] control5 = null;
                string[] control6 = null;
                string[] control7 = null;
                string[] control8 = null;
                string[] control9 = null;
                string[] control10 = null;
                string[] control11 = null;

                if (mCm_C.controlType1 != "-")
                {
                    control1 = mCm_C.controlType1.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control1[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control1[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "0", int.Parse(control1[0]), strCtrol, control1[1], int.Parse(control1[2]), strChange, control1[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType2 != "-")
                {
                    control2 = mCm_C.controlType2.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control2[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control2[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "1", int.Parse(control2[0]), strCtrol, control2[1], int.Parse(control2[2]), strChange, control2[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType3 != "-")
                {
                    control3 = mCm_C.controlType3.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control3[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control3[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "2", int.Parse(control3[0]), strCtrol, control3[1], int.Parse(control3[2]), strChange, control3[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType4 != "-")
                {
                    control4 = mCm_C.controlType4.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control4[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control4[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "3", int.Parse(control4[0]), strCtrol, control4[1], int.Parse(control4[2]), strChange, control4[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType5 != "-")
                {
                    control5 = mCm_C.controlType5.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control5[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control5[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "4", int.Parse(control5[0]), strCtrol, control5[1], int.Parse(control5[2]), strChange, control5[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }
                if (mCm_C.controlType6 != "-")
                {
                    control6 = mCm_C.controlType6.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control6[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control6[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "5", int.Parse(control6[0]), strCtrol, control6[1], int.Parse(control6[2]), strChange, control6[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType7 != "-")
                {
                    control7 = mCm_C.controlType7.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control7[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control7[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "6", int.Parse(control7[0]), strCtrol, control7[1], int.Parse(control7[2]), strChange, control7[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType8 != "-")
                {
                    control8 = mCm_C.controlType8.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control8[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control8[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "7", int.Parse(control8[0]), strCtrol, control8[1], int.Parse(control8[2]), strChange, control8[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }
                if (mCm_C.controlType9 != "-")
                {
                    control9 = mCm_C.controlType9.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control9[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control9[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "8", int.Parse(control9[0]), strCtrol, control9[1], int.Parse(control9[2]), strChange, control9[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType10 != "-")
                {
                    control10 = mCm_C.controlType10.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control10[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control10[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "9", int.Parse(control10[0]), strCtrol, control10[1], int.Parse(control10[2]), strChange, control10[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType11 != "-")
                {
                    control11 = mCm_C.controlType11.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control11[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control11[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "10", int.Parse(control11[0]), strCtrol, control11[1], int.Parse(control11[2]), strChange, control11[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }
            line: ;
            }
            //this.txtCircleNum.Text = mCm_C.circleNum.Value.ToString();
            //是否取引伸计
            this.chkYinShen.Checked = mCm_C.isTakeDownExten;
            if (chkYinShen.Checked)
            {
                this.cmbYsChl.Text = mCm_C.extenChannel.ToString();
                this.cmbYsType.SelectedIndex = (int)mCm_C.selResultID;
                this.txtYsValue.Text = mCm_C.extenValue.ToString();
            }

            //读取试验结果表
            BLL.SelTestResult_C bllSel_C = new HR_Test.BLL.SelTestResult_C();
            DataSet dsSel_C = bllSel_C.GetList("methodName='" + this.txtmethodName.Text + "'");
            if (dsSel_C.Tables[0].Rows.Count > 0)
            {
                for (int i = 2; i < dsSel_C.Tables[0].Columns.Count; i++)
                {
                    if (dsSel_C.Tables[0].Rows[0][i].ToString().ToLower() == "true")
                    {
                        this.chkResultList.SetItemChecked(i - 2, true);
                    }
                    else
                    {
                        this.chkResultList.SetItemChecked(i - 2, false);
                    }
                }
            }
        }

        //读取 YB/T 5349-2006
        private void ReadYBT5349(string _methodName)
        {
            BLL.ControlMethod_B bllCm_B = new HR_Test.BLL.ControlMethod_B();
            Model.ControlMethod_B mCm_B = bllCm_B.GetModel(_methodName);
            this.txtmethodName.Text = mCm_B.methodName;
            this.chkisProLoad.Checked = mCm_B.isProLoad;
            this.cmbTestType.Text = mCm_B.xmlPath;
            this.cmbTestStandard.Text = mCm_B.testStandard;
            ReadResultSel("tb_SelTestResult_B");
            //若允许预载
            if (mCm_B.isProLoad)
            {
                this.cmbProLoadValueType.SelectedIndex = int.Parse(mCm_B.proLoadType.ToString());
                this.txtproLoadValue.Text = mCm_B.proLoadValue.ToString();
                this.txtproLoadSpeed.Text = mCm_B.proLoadSpeed.ToString();
                this.cmbProLoadCtlMode.SelectedIndex = int.Parse(mCm_B.proLoadControlType.ToString());
            }

            //如果不连续屈服
            if (mCm_B.isLxqf == 1)
            {
                rb_1.Checked = true;
                string[] control1 = mCm_B.controlType1.Split(',');
                string[] control2 = mCm_B.controlType2.Split(',');
                string[] control3 = mCm_B.controlType3.Split(',');
                string stopValue = mCm_B.stopValue.ToString();
                //弹性段
                this.cmbCtlType1.SelectedIndex = int.Parse(control1[0]);
                this.txtCtlSpeed1.Text = control1[1];
                this.cmbCtlChange1.SelectedIndex = int.Parse(control1[2]);
                this.txtCtlChangeValue1.Text = control1[3];
                //屈服段
                this.cmbCtlType2.SelectedIndex = int.Parse(control2[0]);
                this.txtCtlSpeed2.Text = control2[1];
                this.cmbCtlChange2.SelectedIndex = int.Parse(control2[2]);
                this.txtCtlChangeValue2.Text = control2[3];
                //加工硬化段                    
                this.cmbCtlType3.SelectedIndex = int.Parse(control3[0]);
                this.txtCtlSpeed3.Text = control3[1];
                //停止测试
                this.txtStopValueNo.Text = stopValue;
                this.txtSpeed_bulianxu.Text = mCm_B.controlType12.ToString();
            }

            if (mCm_B.isLxqf == 2)
            {//如果连续屈服 
                rb_2.Checked = true;
                string[] control1 = mCm_B.controlType1.Split(',');
                string[] control2 = mCm_B.controlType2.Split(',');
                string stopValue = mCm_B.stopValue.ToString();
                //弹性段
                this.cmbCtlType4.SelectedIndex = int.Parse(control1[0]);
                this.txtCtlSpeed4.Text = control1[1];
                this.cmbCtlChange4.SelectedIndex = int.Parse(control1[2]);
                this.txtCtlChangeValue4.Text = control1[3];
                //加工硬化段                    
                this.cmbCtlType5.SelectedIndex = int.Parse(control2[0]);
                this.txtCtlSpeed5.Text = control2[1];
                //停止测试
                this.txtStopValueYes.Text = stopValue;
                this.txtSpeed_lianxu.Text = mCm_B.controlType12.ToString();
            }

            if (mCm_B.isLxqf == 3)
            {//如果自定义试验段 
                this.dbViewMethod.Rows.Clear();
                this.txtStopValue.Text = mCm_B.stopValue.ToString();
                this.txtCircleNum.Text = mCm_B.circleNum.Value.ToString();
                this.txtSpeed_zidingyi.Text = mCm_B.controlType12.ToString();
                rb_3.Checked = true;
                string[] control1 = null;
                string[] control2 = null;
                string[] control3 = null;
                string[] control4 = null;
                string[] control5 = null;
                string[] control6 = null;
                string[] control7 = null;
                string[] control8 = null;
                string[] control9 = null;
                string[] control10 = null;

                if (mCm_B.controlType1 != "-")
                {
                    control1 = mCm_B.controlType1.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control1[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control1[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "0", int.Parse(control1[0]), strCtrol, control1[1], int.Parse(control1[2]), strChange, control1[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_B.controlType2 != "-")
                {
                    control2 = mCm_B.controlType2.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control2[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control2[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "1", int.Parse(control2[0]), strCtrol, control2[1], int.Parse(control2[2]), strChange, control2[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_B.controlType3 != "-")
                {
                    control3 = mCm_B.controlType3.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control3[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control3[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "2", int.Parse(control3[0]), strCtrol, control3[1], int.Parse(control3[2]), strChange, control3[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_B.controlType4 != "-")
                {
                    control4 = mCm_B.controlType4.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control4[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control4[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "3", int.Parse(control4[0]), strCtrol, control4[1], int.Parse(control4[2]), strChange, control4[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_B.controlType5 != "-")
                {
                    control5 = mCm_B.controlType5.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control5[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control5[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "4", int.Parse(control5[0]), strCtrol, control5[1], int.Parse(control5[2]), strChange, control5[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }
                if (mCm_B.controlType6 != "-")
                {
                    control6 = mCm_B.controlType6.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control6[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control6[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "5", int.Parse(control6[0]), strCtrol, control6[1], int.Parse(control6[2]), strChange, control6[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_B.controlType7 != "-")
                {
                    control7 = mCm_B.controlType7.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control7[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control7[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "6", int.Parse(control7[0]), strCtrol, control7[1], int.Parse(control7[2]), strChange, control7[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_B.controlType8 != "-")
                {
                    control8 = mCm_B.controlType8.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control8[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control8[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "7", int.Parse(control8[0]), strCtrol, control8[1], int.Parse(control8[2]), strChange, control8[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }
                if (mCm_B.controlType9 != "-")
                {
                    control9 = mCm_B.controlType9.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control9[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control9[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "8", int.Parse(control9[0]), strCtrol, control9[1], int.Parse(control9[2]), strChange, control9[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_B.controlType10 != "-")
                {
                    control10 = mCm_B.controlType10.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control10[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control10[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "9", int.Parse(control10[0]), strCtrol, control10[1], int.Parse(control10[2]), strChange, control10[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }
            line: ;
            }
            //this.txtCircleNum.Text = mCm_B.circleNum.Value.ToString();
            //是否取引伸计
            this.chkYinShen.Checked = mCm_B.isTakeDownExten;
            if (chkYinShen.Checked)
            {
                this.cmbYsChl.Text = mCm_B.extenChannel.ToString();
                this.cmbYsType.SelectedIndex = (int)mCm_B.selResultID;
                this.txtYsValue.Text = mCm_B.extenValue.ToString();
            }

            //读取试验结果表
            BLL.SelTestResult_B bllSel_B = new HR_Test.BLL.SelTestResult_B();
            DataSet dsSel_B = bllSel_B.GetList("methodName='" + this.txtmethodName.Text + "'");
            if (dsSel_B.Tables[0].Rows.Count > 0)
            {
                for (int i = 2; i < dsSel_B.Tables[0].Columns.Count; i++)
                {
                    if (dsSel_B.Tables[0].Rows[0][i].ToString().ToLower() == "true")
                    {
                        this.chkResultList.SetItemChecked(i - 2, true);
                    }
                    else
                    {
                        this.chkResultList.SetItemChecked(i - 2, false);
                    }
                }
            }
        }

        private void ReadGBT3354(string _methodName)
        {
            BLL.GBT3354_Method bllCm_C = new HR_Test.BLL.GBT3354_Method();
            Model.GBT3354_Method mCm_C = bllCm_C.GetModel(_methodName);
            this.txtmethodName.Text = mCm_C.methodName;
            this.chkisProLoad.Checked = mCm_C.isProLoad;

            this.cmbTestType.Text = mCm_C.xmlPath;
            this.cmbTestStandard.Text = mCm_C.testStandard;


            //若允许预载
            if (mCm_C.isProLoad)
            {
                this.cmbProLoadValueType.SelectedIndex = int.Parse(mCm_C.proLoadType.ToString());
                this.txtproLoadValue.Text = mCm_C.proLoadValue.ToString();
                this.txtproLoadSpeed.Text = mCm_C.proLoadSpeed.ToString();
                this.cmbProLoadCtlMode.SelectedIndex = int.Parse(mCm_C.proLoadControlType.ToString());
            }

            //bool isContinued_C = mCm_C.isLxqf;
            //如果不连续屈服
            if (mCm_C.isLxqf == 1)
            {
                rb_1.Checked = true;
                string[] control1 = mCm_C.controlType1.Split(',');
                string[] control2 = mCm_C.controlType2.Split(',');
                string[] control3 = mCm_C.controlType3.Split(',');
                string stopValue = mCm_C.stopValue.ToString();
                //弹性段
                this.cmbCtlType1.SelectedIndex = int.Parse(control1[0]);
                this.txtCtlSpeed1.Text = control1[1];
                this.cmbCtlChange1.SelectedIndex = int.Parse(control1[2]);
                this.txtCtlChangeValue1.Text = control1[3];
                //屈服段
                this.cmbCtlType2.SelectedIndex = int.Parse(control2[0]);
                this.txtCtlSpeed2.Text = control2[1];
                this.cmbCtlChange2.SelectedIndex = int.Parse(control2[2]);
                this.txtCtlChangeValue2.Text = control2[3];
                //加工硬化段                    
                this.cmbCtlType3.SelectedIndex = int.Parse(control3[0]);
                this.txtCtlSpeed3.Text = control3[1];
                //停止测试
                this.txtStopValueNo.Text = stopValue;
                this.txtSpeed_bulianxu.Text = mCm_C.controlType12.ToString();
            }

            if (mCm_C.isLxqf == 2)
            {//如果连续屈服 
                rb_2.Checked = true;
                string[] control1 = mCm_C.controlType1.Split(',');
                string[] control2 = mCm_C.controlType2.Split(',');
                string stopValue = mCm_C.stopValue.ToString();
                //弹性段
                this.cmbCtlType4.SelectedIndex = int.Parse(control1[0]);
                this.txtCtlSpeed4.Text = control1[1];
                this.cmbCtlChange4.SelectedIndex = int.Parse(control1[2]);
                this.txtCtlChangeValue4.Text = control1[3];
                //加工硬化段                    
                this.cmbCtlType5.SelectedIndex = int.Parse(control2[0]);
                this.txtCtlSpeed5.Text = control2[1];
                //停止测试
                this.txtStopValueYes.Text = stopValue;
                this.txtSpeed_lianxu.Text = mCm_C.controlType12.ToString();
            }

            if (mCm_C.isLxqf == 3)
            {//如果自定义试验段 
                //先清空表格中的数据
                this.dbViewMethod.Rows.Clear();
                rb_3.Checked = true;
                this.txtStopValue.Text = mCm_C.stopValue.ToString();
                this.txtCircleNum.Text = mCm_C.circleNum.Value.ToString();
                this.txtSpeed_zidingyi.Text = mCm_C.controlType12.ToString();
                string[] control1 = null;
                string[] control2 = null;
                string[] control3 = null;
                string[] control4 = null;
                string[] control5 = null;
                string[] control6 = null;
                string[] control7 = null;
                string[] control8 = null;
                string[] control9 = null;
                string[] control10 = null;
                string[] control11 = null;

                if (mCm_C.controlType1 != "-")
                {
                    control1 = mCm_C.controlType1.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control1[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control1[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "0", int.Parse(control1[0]), strCtrol, control1[1], int.Parse(control1[2]), strChange, control1[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType2 != "-")
                {
                    control2 = mCm_C.controlType2.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control2[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control2[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "1", int.Parse(control2[0]), strCtrol, control2[1], int.Parse(control2[2]), strChange, control2[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType3 != "-")
                {
                    control3 = mCm_C.controlType3.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control3[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control3[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "2", int.Parse(control3[0]), strCtrol, control3[1], int.Parse(control3[2]), strChange, control3[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType4 != "-")
                {
                    control4 = mCm_C.controlType4.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control4[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control4[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "3", int.Parse(control4[0]), strCtrol, control4[1], int.Parse(control4[2]), strChange, control4[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType5 != "-")
                {
                    control5 = mCm_C.controlType5.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control5[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control5[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "4", int.Parse(control5[0]), strCtrol, control5[1], int.Parse(control5[2]), strChange, control5[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }
                if (mCm_C.controlType6 != "-")
                {
                    control6 = mCm_C.controlType6.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control6[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control6[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "5", int.Parse(control6[0]), strCtrol, control6[1], int.Parse(control6[2]), strChange, control6[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType7 != "-")
                {
                    control7 = mCm_C.controlType7.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control7[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control7[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "6", int.Parse(control7[0]), strCtrol, control7[1], int.Parse(control7[2]), strChange, control7[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType8 != "-")
                {
                    control8 = mCm_C.controlType8.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control8[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control8[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "7", int.Parse(control8[0]), strCtrol, control8[1], int.Parse(control8[2]), strChange, control8[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }
                if (mCm_C.controlType9 != "-")
                {
                    control9 = mCm_C.controlType9.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control9[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control9[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "8", int.Parse(control9[0]), strCtrol, control9[1], int.Parse(control9[2]), strChange, control9[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType10 != "-")
                {
                    control10 = mCm_C.controlType10.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control10[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control10[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "9", int.Parse(control10[0]), strCtrol, control10[1], int.Parse(control10[2]), strChange, control10[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType11 != "-")
                {
                    control11 = mCm_C.controlType11.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control11[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control11[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "10", int.Parse(control11[0]), strCtrol, control11[1], int.Parse(control11[2]), strChange, control11[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

            line: ;
            }
            //this.txtCircleNum.Text = mCm_C.circleNum.Value.ToString();
            //是否取引伸计
            this.chkYinShen.Checked = mCm_C.isTakeDownExten;
            if (chkYinShen.Checked)
            {
                this.cmbYsChl.Text = mCm_C.extenChannel.ToString();
                this.cmbYsType.SelectedIndex = (int)mCm_C.selResultID;
                this.txtYsValue.Text = mCm_C.extenValue.ToString();
            }

            //读取试验结果表           
            ReadResultSel("tb_GBT3354_Sel");
            BLL.GBT3354_Sel bllSel_T = new HR_Test.BLL.GBT3354_Sel();
            DataSet dsSel_T = bllSel_T.GetList("methodName='" + this.txtmethodName.Text + "'");
            if (dsSel_T.Tables[0].Rows.Count > 0)
            {
                for (int i = 2; i < dsSel_T.Tables[0].Columns.Count; i++)
                {
                    if (dsSel_T.Tables[0].Rows[0][i].ToString().ToLower() == "true")
                    {
                        this.chkResultList.SetItemChecked(i - 2, true);
                    }
                    else
                    {
                        this.chkResultList.SetItemChecked(i - 2, false);
                    }
                }
            }
               

        }

        private void ReadGBT28289(string _methodName)
        {
            BLL.GBT282892012_Method bllCm_C = new HR_Test.BLL.GBT282892012_Method();
            Model.GBT282892012_Method mCm_C = bllCm_C.GetModel(_methodName);
            this.txtmethodName.Text = mCm_C.methodName;
            this.chkisProLoad.Checked = mCm_C.isProLoad;

            this.cmbTestType.Text = mCm_C.xmlPath;
            this.cmbTestStandard.Text = mCm_C.testStandard;
            

            //若允许预载
            if (mCm_C.isProLoad)
            {
                this.cmbProLoadValueType.SelectedIndex = int.Parse(mCm_C.proLoadType.ToString());
                this.txtproLoadValue.Text = mCm_C.proLoadValue.ToString();
                this.txtproLoadSpeed.Text = mCm_C.proLoadSpeed.ToString();
                this.cmbProLoadCtlMode.SelectedIndex = int.Parse(mCm_C.proLoadControlType.ToString());
            }

            //bool isContinued_C = mCm_C.isLxqf;
            //如果不连续屈服
            if (mCm_C.isLxqf == 1)
            {
                rb_1.Checked = true;
                string[] control1 = mCm_C.controlType1.Split(',');
                string[] control2 = mCm_C.controlType2.Split(',');
                string[] control3 = mCm_C.controlType3.Split(',');
                string stopValue = mCm_C.stopValue.ToString();
                //弹性段
                this.cmbCtlType1.SelectedIndex = int.Parse(control1[0]);
                this.txtCtlSpeed1.Text = control1[1];
                this.cmbCtlChange1.SelectedIndex = int.Parse(control1[2]);
                this.txtCtlChangeValue1.Text = control1[3];
                //屈服段
                this.cmbCtlType2.SelectedIndex = int.Parse(control2[0]);
                this.txtCtlSpeed2.Text = control2[1];
                this.cmbCtlChange2.SelectedIndex = int.Parse(control2[2]);
                this.txtCtlChangeValue2.Text = control2[3];
                //加工硬化段                    
                this.cmbCtlType3.SelectedIndex = int.Parse(control3[0]);
                this.txtCtlSpeed3.Text = control3[1];
                //停止测试
                this.txtStopValueNo.Text = stopValue;
                this.txtSpeed_bulianxu.Text = mCm_C.controlType12.ToString();
            }

            if (mCm_C.isLxqf == 2)
            {//如果连续屈服 
                rb_2.Checked = true;
                string[] control1 = mCm_C.controlType1.Split(',');
                string[] control2 = mCm_C.controlType2.Split(',');
                string stopValue = mCm_C.stopValue.ToString();
                //弹性段
                this.cmbCtlType4.SelectedIndex = int.Parse(control1[0]);
                this.txtCtlSpeed4.Text = control1[1];
                this.cmbCtlChange4.SelectedIndex = int.Parse(control1[2]);
                this.txtCtlChangeValue4.Text = control1[3];
                //加工硬化段                    
                this.cmbCtlType5.SelectedIndex = int.Parse(control2[0]);
                this.txtCtlSpeed5.Text = control2[1];
                //停止测试
                this.txtStopValueYes.Text = stopValue;
                this.txtSpeed_lianxu.Text = mCm_C.controlType12.ToString();
            }

            if (mCm_C.isLxqf == 3)
            {//如果自定义试验段 
                //先清空表格中的数据
                this.dbViewMethod.Rows.Clear();
                rb_3.Checked = true;
                this.txtStopValue.Text = mCm_C.stopValue.ToString();
                this.txtCircleNum.Text = mCm_C.circleNum.Value.ToString();
                this.txtSpeed_zidingyi.Text = mCm_C.controlType12.ToString();
                string[] control1 = null;
                string[] control2 = null;
                string[] control3 = null;
                string[] control4 = null;
                string[] control5 = null;
                string[] control6 = null;
                string[] control7 = null;
                string[] control8 = null;
                string[] control9 = null;
                string[] control10 = null;
                string[] control11 = null;

                if (mCm_C.controlType1 != "-")
                {
                    control1 = mCm_C.controlType1.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control1[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control1[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "0", int.Parse(control1[0]), strCtrol, control1[1], int.Parse(control1[2]), strChange, control1[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType2 != "-")
                {
                    control2 = mCm_C.controlType2.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control2[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control2[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "1", int.Parse(control2[0]), strCtrol, control2[1], int.Parse(control2[2]), strChange, control2[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType3 != "-")
                {
                    control3 = mCm_C.controlType3.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control3[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control3[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "2", int.Parse(control3[0]), strCtrol, control3[1], int.Parse(control3[2]), strChange, control3[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType4 != "-")
                {
                    control4 = mCm_C.controlType4.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control4[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control4[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "3", int.Parse(control4[0]), strCtrol, control4[1], int.Parse(control4[2]), strChange, control4[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType5 != "-")
                {
                    control5 = mCm_C.controlType5.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control5[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control5[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "4", int.Parse(control5[0]), strCtrol, control5[1], int.Parse(control5[2]), strChange, control5[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }
                if (mCm_C.controlType6 != "-")
                {
                    control6 = mCm_C.controlType6.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control6[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control6[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "5", int.Parse(control6[0]), strCtrol, control6[1], int.Parse(control6[2]), strChange, control6[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType7 != "-")
                {
                    control7 = mCm_C.controlType7.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control7[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control7[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "6", int.Parse(control7[0]), strCtrol, control7[1], int.Parse(control7[2]), strChange, control7[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType8 != "-")
                {
                    control8 = mCm_C.controlType8.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control8[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control8[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "7", int.Parse(control8[0]), strCtrol, control8[1], int.Parse(control8[2]), strChange, control8[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }
                if (mCm_C.controlType9 != "-")
                {
                    control9 = mCm_C.controlType9.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control9[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control9[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "8", int.Parse(control9[0]), strCtrol, control9[1], int.Parse(control9[2]), strChange, control9[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType10 != "-")
                {
                    control10 = mCm_C.controlType10.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control10[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control10[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "9", int.Parse(control10[0]), strCtrol, control10[1], int.Parse(control10[2]), strChange, control10[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

                if (mCm_C.controlType11 != "-")
                {
                    control11 = mCm_C.controlType11.Split(',');
                    string strCtrol = string.Empty;
                    switch (int.Parse(control11[0]))
                    {
                        case 0: strCtrol = "位移控制(mm/min)"; break;
                        case 1: strCtrol = "负荷控制(kN/s)"; break;
                        case 2: strCtrol = "应力控制(MPa/s)"; break;
                        case 3: strCtrol = "ēLc控制(/s)"; break;
                        case 4: strCtrol = "ēLe控制(/s)"; break;
                        case 5: strCtrol = "变形控制(mm/s)"; break;
                    }
                    string strChange = string.Empty;
                    switch (int.Parse(control11[2]))
                    {
                        case 0: strChange = "位移(mm)"; break;
                        case 1: strChange = "负荷(kN)"; break;
                        case 2: strChange = "变形(mm)"; break;
                        case 3: strChange = "应力(MPa)"; break;
                        case 4: strChange = "应变(%)"; break;
                    }
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dgvr.CreateCells(this.dbViewMethod, new object[] { false, "10", int.Parse(control11[0]), strCtrol, control11[1], int.Parse(control11[2]), strChange, control11[3] });
                    dbViewMethod.Rows.Add(dgvr);
                }
                else
                {
                    goto line;
                }

            line: ;
            }
            //this.txtCircleNum.Text = mCm_C.circleNum.Value.ToString();
            //是否取引伸计
            this.chkYinShen.Checked = mCm_C.isTakeDownExten;
            if (chkYinShen.Checked)
            {
                this.cmbYsChl.Text = mCm_C.extenChannel.ToString();
                this.cmbYsType.SelectedIndex = (int)mCm_C.selResultID;
                this.txtYsValue.Text = mCm_C.extenValue.ToString();
            }

            //读取试验结果表
            switch (mCm_C.xmlPath)
            {
                case "拉伸试验":
                    ReadResultSel("tb_GBT282892012_TensileSel");
                    BLL.GBT282892012_TensileSel bllSel_T = new HR_Test.BLL.GBT282892012_TensileSel();
                    DataSet dsSel_T = bllSel_T.GetList("methodName='" + this.txtmethodName.Text + "'");
                    if (dsSel_T.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 2; i < dsSel_T.Tables[0].Columns.Count; i++)
                        {
                            if (dsSel_T.Tables[0].Rows[0][i].ToString().ToLower() == "true")
                            {
                                this.chkResultList.SetItemChecked(i - 2, true);
                            }
                            else
                            {
                                this.chkResultList.SetItemChecked(i - 2, false);
                            }
                        }
                    }
                    break;
                case "剪切试验":
                    ReadResultSel("tb_GBT282892012_ShearSel");
                    BLL.GBT282892012_ShearSel bllSel_S = new HR_Test.BLL.GBT282892012_ShearSel();
                    DataSet dsSel_S = bllSel_S.GetList("methodName='" + this.txtmethodName.Text + "'");
                    if (dsSel_S.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 2; i < dsSel_S.Tables[0].Columns.Count; i++)
                        {
                            if (dsSel_S.Tables[0].Rows[0][i].ToString().ToLower() == "true")
                            {
                                this.chkResultList.SetItemChecked(i - 2, true);
                            }
                            else
                            {
                                this.chkResultList.SetItemChecked(i - 2, false);
                            }
                        }
                    }
                    break;
                case "扭转试验":
                    ReadResultSel("tb_GBT282892012_TwistSel");
                    BLL.GBT282892012_TwistSel bllSel_Tw = new HR_Test.BLL.GBT282892012_TwistSel();
                    DataSet dsSel_Tw = bllSel_Tw.GetList("methodName='" + this.txtmethodName.Text + "'");
                    if (dsSel_Tw.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 2; i < dsSel_Tw.Tables[0].Columns.Count; i++)
                        {
                            if (dsSel_Tw.Tables[0].Rows[0][i].ToString().ToLower() == "true")
                            {
                                this.chkResultList.SetItemChecked(i - 2, true);
                            }
                            else
                            {
                                this.chkResultList.SetItemChecked(i - 2, false);
                            }
                        }
                    }
                    break;
            }

        }

        //保存修改各种试验方法
        private void gbtnSaveChange_Click(object sender, EventArgs e)
        {
            string strErr = "";
            if (this.txtmethodName.Text.Trim().Length == 0)
            {
                strErr += "试验方法名称不能为空!\r\n\r\n";
            }

            #region 判断输入为空
            if (chkisProLoad.Checked)
            {
                if (this.cmbProLoadValueType.Text.Trim().Length == 0)
                {
                    strErr += "预载方式不能为空!\r\n\r\n";
                }

                if (this.txtproLoadValue.Text.Trim().Length == 0)//|| double.Parse(this.txtproLoadValue.Text) == 0d)
                {
                    strErr += "预载值不能为空!\r\n\r\n";
                }

                if (this.cmbProLoadCtlMode.Text.Trim().Length == 0)
                {
                    strErr += "预载控制模式不能为空!\r\n\r\n";
                }

                if (this.txtproLoadSpeed.Text.Trim().Length == 0)//|| double.Parse(this.txtproLoadSpeed.Text) == 0d)
                {
                    strErr += "预载速度不能为空!\r\n\r\n";
                }
            }


            if (rb_1.Checked == true)//如果不连续屈服
            {
                //弹性段
                if (this.cmbCtlType1.Text.Trim().Length == 0)
                {
                    strErr += "弹性段控制模式不能为空!\r\n\r\n";
                }

                if (this.txtCtlSpeed1.Text.Trim().Length == 0)// || double.Parse(this.txtCtlSpeed1.Text) == 0d)
                {
                    strErr += "弹性段控制速度不能为空!\r\n\r\n";
                }

                if (this.cmbCtlChange1.Text.Trim().Length == 0)
                {
                    strErr += "择弹性段控制转换方式不能为空!\r\n\r\n";
                }

                if (this.txtCtlChangeValue1.Text.Trim().Length == 0)// || double.Parse(this.txtCtlChangeValue1.Text) == 0d)
                {
                    strErr += "弹性段转换值不能为空!\r\n\r\n";
                }

                //屈服段
                if (this.cmbCtlType2.Text.Trim().Length == 0)
                {
                    strErr += "屈服段控制模式不能为空!\r\n\r\n";
                }

                if (this.txtCtlSpeed2.Text.Trim().Length == 0)// || double.Parse(this.txtCtlSpeed2.Text) == 0d)
                {
                    strErr += "屈服段控制速度不能为空!\r\n\r\n";
                }

                if (this.cmbCtlChange2.Text.Trim().Length == 0)
                {
                    strErr += "屈服段控制转换方式不能为空!\r\n\r\n";
                }

                if (this.txtCtlChangeValue2.Text.Trim().Length == 0)// || double.Parse(this.txtCtlChangeValue1.Text) == 0d)
                {
                    strErr += "屈服段转换值不能为空!\r\n\r\n";
                }

                //加工硬化段
                if (this.cmbCtlType3.Text.Trim().Length == 0)
                {
                    strErr += "加工硬化段控制模式不能为空!\r\n\r\n";
                }

                if (this.txtCtlSpeed3.Text.Trim().Length == 0)//|| double.Parse(this.txtCtlSpeed3.Text) == 0d)
                {
                    strErr += "加工硬化段控制速度不能为空!\r\n\r\n";
                }

                if (this.txtStopValueNo.Text.Trim().Length == 0)// || double.Parse(this.txtStopValueNo.Text) == 0d)
                {
                    strErr += "加工硬化段转换值不能为空!\r\n\r\n";
                }
            }

            if (rb_2.Checked == true)
            {
                //屈服段
                if (this.cmbCtlType4.Text.Trim().Length == 0)
                {
                    strErr += "屈服段控制模式不能为空!\r\n\r\n";
                }

                if (this.txtCtlSpeed4.Text.Trim().Length == 0)// || double.Parse(this.txtCtlSpeed4.Text) == 0d)
                {
                    strErr += "屈服段控制速度不能为空!\r\n\r\n";
                }

                if (this.cmbCtlChange4.Text.Trim().Length == 0)
                {
                    strErr += "屈服段控制转换方式不能为空!\r\n\r\n";
                }

                if (this.txtCtlChangeValue4.Text.Trim().Length == 0)// || double.Parse(this.txtCtlChangeValue4.Text) == 0d)
                {
                    strErr += "屈服段转换值不能为空!\r\n\r\n";
                }

                //试验段
                if (this.cmbCtlType5.Text.Trim().Length == 0)
                {
                    strErr += "试验段控制模式不能为空!\r\n\r\n";
                }

                if (this.txtCtlSpeed5.Text.Trim().Length == 0)// || double.Parse(this.txtCtlSpeed5.Text) == 0d)
                {
                    strErr += "试验段段控制速度不能为空!\r\n\r\n";
                }

                if (this.txtStopValueYes.Text.Trim().Length == 0)// || double.Parse(this.txtStopValueYes.Text) == 0d)
                {
                    strErr += "试验段段转换值不能为空!\r\n\r\n";
                }
            }

            if (chkYinShen.Checked)
            {
                if (this.cmbYsType.Text.Trim().Length == 0)
                {
                    strErr += "取引伸计方式不能为空!\r\n\r\n";
                }

                if (this.txtYsValue.Text.Trim().Length == 0)
                {
                    strErr += "取引伸计的值不能为空!\r\n\r\n";
                }

                if (this.cmbYsChl.Text.Trim().Length == 0)
                {
                    strErr += "取引伸计通道不能为空!\r\n\r\n";
                }
            }

            //if (this.txtCircleNum.Text.Trim().Length == 0)
            //{
            //    strErr += "循环次数不能为空!\r\n\r\n";
            //}

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            #endregion

            if (this.tvTestMethod.SelectedNode == null)
            {
                MessageBox.Show("请选择需要修改的试验方法");
                return;
            }

            string tag = this.tvTestMethod.SelectedNode.Tag.ToString();
            if (!string.IsNullOrEmpty(tag))
            {
                switch (tag)
                {
                    case "GB/T 228-2010":
                        UpdateGBT228();
                        break;
                    case "GB/T 7314-2005":
                        UpdateGBT7314();
                        break;
                    case "YB/T 5349-2006":
                        UpdateYBT5349();
                        break;
                    case "GB/T 28289-2012":
                        UpdateGBT28289();
                        break;
                    case "GB/T 23615.1-2009":
                        UpdateGBT23615();
                        break;
                    case "GB/T 3354-2014":
                        UpdateGBT3354();
                        break;
                }
            }
        }

        private void UpdateGBT3354()
        {
            //查找存在
            BLL.GBT3354_Method bllcm = new HR_Test.BLL.GBT3354_Method();
            DataSet cmds = bllcm.GetList("methodName ='" + this.txtmethodName.Text.Trim() + "'");
            if (cmds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show(this, "不存在该试验方法名称,请添加!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {//保存修改 
                HR_Test.Model.GBT3354_Method model = bllcm.GetModel(this.txtmethodName.Text);
                model.methodName = this.txtmethodName.Text.Trim();
                model.testStandard = this.cmbTestStandard.Text;
                model.testMethod = this.txtmethodName.Text.Trim();
                //是否连续屈服 
                string controlType1 = "-";
                string controlType2 = "-";
                string controlType3 = "-";

                if (rb_1.Checked == true)//若不连续屈服
                {
                    //弹性段  
                    controlType1 = this.cmbCtlType1.SelectedIndex.ToString() + "," + this.txtCtlSpeed1.Text + "," + this.cmbCtlChange1.SelectedIndex.ToString() + "," + this.txtCtlChangeValue1.Text;
                    //屈服段
                    controlType2 = this.cmbCtlType2.SelectedIndex.ToString() + "," + this.txtCtlSpeed2.Text + "," + this.cmbCtlChange2.SelectedIndex.ToString() + "," + this.txtCtlChangeValue2.Text;
                    //停止测试
                    controlType3 = this.cmbCtlType3.SelectedIndex.ToString() + "," + this.txtCtlSpeed3.Text;

                    model.isLxqf = 1;
                    model.controlType1 = controlType1;
                    model.controlType2 = controlType2;
                    model.controlType3 = controlType3;
                    model.controlType4 = "-";
                    model.controlType5 = "-";
                    model.controlType6 = "-";
                    model.controlType7 = "-";
                    model.controlType8 = "-";
                    model.controlType9 = "-";
                    model.controlType10 = "-";
                    model.controlType11 = "-";
                    model.selResultID = 0;
                    model.circleNum = 0;
                    model.extenValue = 0;
                    model.extenChannel = 0;
                    model.stopValue = double.Parse(this.txtStopValueNo.Text);
                    model.controlType12 = this.txtSpeed_bulianxu.Text.Trim();
                }

                if (rb_2.Checked == true)//若连续屈服
                {
                    //弹性段
                    controlType1 = this.cmbCtlType4.SelectedIndex.ToString() + "," + this.txtCtlSpeed4.Text + "," + this.cmbCtlChange4.SelectedIndex.ToString() + "," + this.txtCtlChangeValue4.Text;
                    //试验段
                    controlType2 = this.cmbCtlType5.SelectedIndex.ToString() + "," + this.txtCtlSpeed5.Text;
                    controlType3 = "-";
                    model.isLxqf = 2;
                    model.controlType1 = controlType1;
                    model.controlType2 = controlType2;
                    model.controlType3 = controlType3;
                    model.controlType4 = "-";
                    model.controlType5 = "-";
                    model.controlType6 = "-";
                    model.controlType7 = "-";
                    model.controlType8 = "-";
                    model.controlType9 = "-";
                    model.controlType10 = "-";
                    model.controlType11 = "-";
                    model.controlType12 = this.txtSpeed_lianxu.Text.Trim();
                    model.selResultID = 0;
                    model.circleNum = 0;
                    model.extenValue = 0;
                    model.extenChannel = 0;
                    model.stopValue = double.Parse(this.txtStopValueYes.Text);
                }

                if (rb_3.Checked == true)
                {
                    if (string.IsNullOrEmpty(this.txtStopValue.Text))
                    {
                        MessageBox.Show(this, "请输入停止值", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrEmpty(this.txtCircleNum.Text))
                    {
                        MessageBox.Show(this, "请输入循环次数!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    model.stopValue = double.Parse(this.txtStopValue.Text);
                    model.isLxqf = 3;
                    model.circleNum = int.Parse(this.txtCircleNum.Text);
                    model.controlType1 = "-";
                    model.controlType2 = "-";
                    model.controlType3 = "-";
                    model.controlType4 = "-";
                    model.controlType5 = "-";
                    model.controlType6 = "-";
                    model.controlType7 = "-";
                    model.controlType8 = "-";
                    model.controlType9 = "-";
                    model.controlType10 = "-";
                    model.controlType11 = "-";
                    model.controlType12 = this.txtSpeed_zidingyi.Text.Trim();

                    if (dbViewMethod.Rows.Count == 0)
                    {
                        MessageBox.Show(this, "无自定义试验方法段,不允许保存!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (dbViewMethod.Rows.Count > 0)
                        model.controlType1 = dbViewMethod.Rows[0].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 1)
                        model.controlType2 = dbViewMethod.Rows[1].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 2)
                        model.controlType3 = dbViewMethod.Rows[2].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 3)
                        model.controlType4 = dbViewMethod.Rows[3].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 4)
                        model.controlType5 = dbViewMethod.Rows[4].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 5)
                        model.controlType6 = dbViewMethod.Rows[5].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 6)
                        model.controlType7 = dbViewMethod.Rows[6].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 7)
                        model.controlType8 = dbViewMethod.Rows[7].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 8)
                        model.controlType9 = dbViewMethod.Rows[8].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 9)
                        model.controlType10 = dbViewMethod.Rows[9].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 10)
                        model.controlType11 = dbViewMethod.Rows[10].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[7].Value.ToString();

                }
                //是否预载  
                model.isProLoad = this.chkisProLoad.Checked;
                if (chkisProLoad.Checked)
                {
                    model.proLoadType = this.cmbProLoadValueType.SelectedIndex;
                    model.proLoadControlType = this.cmbProLoadCtlMode.SelectedIndex;
                    model.proLoadValue = double.Parse(this.txtproLoadValue.Text);
                    model.proLoadSpeed = double.Parse(this.txtproLoadSpeed.Text);
                }

                //model.ID = int.Parse(this.tvTestMethod.SelectedNode.Name.ToString());
                //是否取引伸计       
                model.isTakeDownExten = this.chkYinShen.Checked;
                if (this.chkYinShen.Checked)
                {
                    model.extenChannel = int.Parse(this.cmbYsChl.Text);
                    model.extenValue = double.Parse(this.txtYsValue.Text);
                    model.selResultID = this.cmbYsType.SelectedIndex;//取引伸计的方式
                }
                //readMethod(this.tvTestMethod); 
                BLL.GBT3354_Sel bllSel = new HR_Test.BLL.GBT3354_Sel();
                Model.GBT3354_Sel mSel = bllSel.GetModel(this.txtmethodName.Text.Trim());
                mSel.methodName = this.txtmethodName.Text; //this.tvTestMethod.SelectedNode.Text;//  
                mSel.Pmax = this.chkResultList.GetItemChecked(0);
                mSel.σt = this.chkResultList.GetItemChecked(1);
                mSel.Et = this.chkResultList.GetItemChecked(2);
                mSel.μ12 = this.chkResultList.GetItemChecked(3);
                mSel.ε1t = this.chkResultList.GetItemChecked(4);
                mSel.Mean = this.chkResultList.GetItemChecked(5);
                mSel.SD = this.chkResultList.GetItemChecked(6);
                mSel.Mid = this.chkResultList.GetItemChecked(7);
                mSel.CV = this.chkResultList.GetItemChecked(8);
                mSel.saveCurve = this.chkResultList.GetItemChecked(9);
                if (bllSel.Update(mSel) && bllcm.Update(model))
                    MessageBox.Show("更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("更新失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            cmds.Dispose();
        }

        private void UpdateGBT23615()
        {
            //查找存在
            BLL.GBT236152009_Method bllcm = new HR_Test.BLL.GBT236152009_Method();
            DataSet cmds = bllcm.GetList("methodName ='" + this.txtmethodName.Text.Trim() + "'");
            if (cmds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show(this, "不存在该试验方法名称,请添加!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {//保存修改 
                HR_Test.Model.GBT236152009_Method model = bllcm.GetModel(this.txtmethodName.Text);
                model.methodName = this.txtmethodName.Text.Trim();
                model.testMethod = this.txtmethodName.Text.Trim();
                model.testStandard = this.cmbTestStandard.Text;
                //是否连续屈服 
                string controlType1 = "-";
                string controlType2 = "-";
                string controlType3 = "-";

                if (rb_1.Checked == true)//若不连续屈服
                {
                    //弹性段  
                    controlType1 = this.cmbCtlType1.SelectedIndex.ToString() + "," + this.txtCtlSpeed1.Text + "," + this.cmbCtlChange1.SelectedIndex.ToString() + "," + this.txtCtlChangeValue1.Text;
                    //屈服段
                    controlType2 = this.cmbCtlType2.SelectedIndex.ToString() + "," + this.txtCtlSpeed2.Text + "," + this.cmbCtlChange2.SelectedIndex.ToString() + "," + this.txtCtlChangeValue2.Text;
                    //停止测试
                    controlType3 = this.cmbCtlType3.SelectedIndex.ToString() + "," + this.txtCtlSpeed3.Text;

                    model.isLxqf = 1;
                    model.controlType1 = controlType1;
                    model.controlType2 = controlType2;
                    model.controlType3 = controlType3;
                    model.controlType4 = "-";
                    model.controlType5 = "-";
                    model.controlType6 = "-";
                    model.controlType7 = "-";
                    model.controlType8 = "-";
                    model.controlType9 = "-";
                    model.controlType10 = "-";
                    model.controlType11 = "-";
                    model.controlType12 = this.txtSpeed_bulianxu.Text.Trim();
                    model.selResultID = 0;
                    model.circleNum = 0;
                    model.extenValue = 0;
                    model.extenChannel = 0;
                    model.stopValue = double.Parse(this.txtStopValueNo.Text);
                }

                if (rb_2.Checked == true)//若连续屈服
                {
                    //弹性段
                    controlType1 = this.cmbCtlType4.SelectedIndex.ToString() + "," + this.txtCtlSpeed4.Text + "," + this.cmbCtlChange4.SelectedIndex.ToString() + "," + this.txtCtlChangeValue4.Text;
                    //试验段
                    controlType2 = this.cmbCtlType5.SelectedIndex.ToString() + "," + this.txtCtlSpeed5.Text;
                    controlType3 = "-";
                    model.isLxqf = 2;
                    model.controlType1 = controlType1;
                    model.controlType2 = controlType2;
                    model.controlType3 = controlType3;
                    model.controlType4 = "-";
                    model.controlType5 = "-";
                    model.controlType6 = "-";
                    model.controlType7 = "-";
                    model.controlType8 = "-";
                    model.controlType9 = "-";
                    model.controlType10 = "-";
                    model.controlType11 = "-";
                    model.controlType12 = this.txtSpeed_lianxu.Text.Trim();
                    model.selResultID = 0;
                    model.circleNum = 0;
                    model.extenValue = 0;
                    model.extenChannel = 0;
                    model.stopValue = double.Parse(this.txtStopValueYes.Text);
                }

                if (rb_3.Checked == true)
                {
                    if (string.IsNullOrEmpty(this.txtStopValue.Text))
                    {
                        MessageBox.Show(this, "请输入停止值", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrEmpty(this.txtCircleNum.Text))
                    {
                        MessageBox.Show(this, "请输入循环次数!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    model.stopValue = double.Parse(this.txtStopValue.Text);
                    model.isLxqf = 3;
                    model.circleNum = int.Parse(this.txtCircleNum.Text);
                    model.controlType1 = "-";
                    model.controlType2 = "-";
                    model.controlType3 = "-";
                    model.controlType4 = "-";
                    model.controlType5 = "-";
                    model.controlType6 = "-";
                    model.controlType7 = "-";
                    model.controlType8 = "-";
                    model.controlType9 = "-";
                    model.controlType10 = "-";
                    model.controlType11 = "-";
                    model.controlType12 = this.txtSpeed_zidingyi.Text.Trim();

                    if (dbViewMethod.Rows.Count == 0)
                    {
                        MessageBox.Show(this, "无自定义试验方法段,不允许保存!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (dbViewMethod.Rows.Count > 0)
                        model.controlType1 = dbViewMethod.Rows[0].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 1)
                        model.controlType2 = dbViewMethod.Rows[1].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 2)
                        model.controlType3 = dbViewMethod.Rows[2].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 3)
                        model.controlType4 = dbViewMethod.Rows[3].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 4)
                        model.controlType5 = dbViewMethod.Rows[4].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 5)
                        model.controlType6 = dbViewMethod.Rows[5].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 6)
                        model.controlType7 = dbViewMethod.Rows[6].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 7)
                        model.controlType8 = dbViewMethod.Rows[7].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 8)
                        model.controlType9 = dbViewMethod.Rows[8].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 9)
                        model.controlType10 = dbViewMethod.Rows[9].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 10)
                        model.controlType11 = dbViewMethod.Rows[10].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[7].Value.ToString();
                    //if (dbViewMethod.Rows.Count > 11)
                    //    model.controlType12 = dbViewMethod.Rows[11].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[7].Value.ToString();

                }
                //是否预载  
                model.isProLoad = this.chkisProLoad.Checked;
                if (chkisProLoad.Checked)
                {
                    model.proLoadType = this.cmbProLoadValueType.SelectedIndex;
                    model.proLoadControlType = this.cmbProLoadCtlMode.SelectedIndex;
                    model.proLoadValue = double.Parse(this.txtproLoadValue.Text);
                    model.proLoadSpeed = double.Parse(this.txtproLoadSpeed.Text);
                }

                //model.ID = int.Parse(this.tvTestMethod.SelectedNode.Name.ToString());
                //是否取引伸计       
                model.isTakeDownExten = this.chkYinShen.Checked;
                if (this.chkYinShen.Checked)
                {
                    model.extenChannel = int.Parse(this.cmbYsChl.Text);
                    model.extenValue = double.Parse(this.txtYsValue.Text);
                    model.selResultID = this.cmbYsType.SelectedIndex;//取引伸计的方式
                }

                //readMethod(this.tvTestMethod); 

                switch (this.cmbTestType.Text)
                {
                    case "纵向拉伸"://拉伸

                        BLL.GBT236152009_SelZong bllSelZong = new HR_Test.BLL.GBT236152009_SelZong();
                        Model.GBT236152009_SelZong mSel = bllSelZong.GetModel(this.txtmethodName.Text); // new HR_Test.Model.GBT282892012_TensileSel();
                        if (mSel != null)
                        {
                            mSel.methodName = this.txtmethodName.Text;
                            mSel.Fmax = this.chkResultList.GetItemChecked(0);
                            mSel.T2 = this.chkResultList.GetItemChecked(1);
                            mSel.Z = this.chkResultList.GetItemChecked(2);
                            mSel.E = this.chkResultList.GetItemChecked(3);
                            mSel.T2_ = this.chkResultList.GetItemChecked(4);
                            mSel.S = this.chkResultList.GetItemChecked(5);
                            mSel.T2c = this.chkResultList.GetItemChecked(6);
                            mSel.isSaveCurve = this.chkResultList.GetItemChecked(7);
                            BLL.GBT236152009_SelZong bllSel = new HR_Test.BLL.GBT236152009_SelZong();

                            if (bllcm.Update(model) && bllSel.Update(mSel))
                            {
                                MessageBox.Show("更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Task t = tskReadMethod();
                            }
                            else
                            {
                                MessageBox.Show("更新失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("不存在此试验类型的方法,请添加!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case "横向拉伸"://剪切
                        BLL.GBT236152009_SelHeng bllSelHeng = new HR_Test.BLL.GBT236152009_SelHeng();
                        Model.GBT236152009_SelHeng msSel = bllSelHeng.GetModel(this.txtmethodName.Text);
                        if (msSel != null)
                        {
                            msSel.methodName = this.txtmethodName.Text;
                            msSel.Fmax = this.chkResultList.GetItemChecked(0);
                            msSel.T1 = this.chkResultList.GetItemChecked(1);
                            msSel.T1_ = this.chkResultList.GetItemChecked(2);
                            msSel.S = this.chkResultList.GetItemChecked(3);
                            msSel.T1c = this.chkResultList.GetItemChecked(4);
                            msSel.isSaveCurve = this.chkResultList.GetItemChecked(5);

                            BLL.GBT236152009_SelHeng bllsSel = new HR_Test.BLL.GBT236152009_SelHeng();

                            if (bllcm.Update(model) && bllsSel.Update(msSel))
                            {
                                MessageBox.Show("更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Task t = tskReadMethod();
                            }
                            else
                            {
                                MessageBox.Show("更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("该试验类型不存在此试验方法,请添加!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                }
            }
            cmds.Dispose();
        }

        private void UpdateGBT28289()
        {
            //查找存在
            BLL.GBT282892012_Method bllcm = new HR_Test.BLL.GBT282892012_Method();
            DataSet cmds = bllcm.GetList("methodName ='" + this.txtmethodName.Text.Trim() + "'");
            if (cmds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show(this, "不存在该试验方法名称,请添加!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {//保存修改 
                HR_Test.Model.GBT282892012_Method model = bllcm.GetModel(this.txtmethodName.Text);
                model.methodName = this.txtmethodName.Text.Trim();
                model.testMethod = this.txtmethodName.Text.Trim();
                model.testStandard = this.cmbTestStandard.Text;
                //是否连续屈服 
                string controlType1 = "-";
                string controlType2 = "-";
                string controlType3 = "-";

                if (rb_1.Checked == true)//若不连续屈服
                {
                    //弹性段  
                    controlType1 = this.cmbCtlType1.SelectedIndex.ToString() + "," + this.txtCtlSpeed1.Text + "," + this.cmbCtlChange1.SelectedIndex.ToString() + "," + this.txtCtlChangeValue1.Text;
                    //屈服段
                    controlType2 = this.cmbCtlType2.SelectedIndex.ToString() + "," + this.txtCtlSpeed2.Text + "," + this.cmbCtlChange2.SelectedIndex.ToString() + "," + this.txtCtlChangeValue2.Text;
                    //停止测试
                    controlType3 = this.cmbCtlType3.SelectedIndex.ToString() + "," + this.txtCtlSpeed3.Text;

                    model.isLxqf = 1;
                    model.controlType1 = controlType1;
                    model.controlType2 = controlType2;
                    model.controlType3 = controlType3;
                    model.controlType4 = "-";
                    model.controlType5 = "-";
                    model.controlType6 = "-";
                    model.controlType7 = "-";
                    model.controlType8 = "-";
                    model.controlType9 = "-";
                    model.controlType10 = "-";
                    model.controlType11 = "-";
                    model.controlType12 = this.txtSpeed_bulianxu.Text.Trim();
                    model.selResultID = 0;
                    model.circleNum = 0;
                    model.extenValue = 0;
                    model.extenChannel = 0;
                    model.stopValue = double.Parse(this.txtStopValueNo.Text);
                }

                if (rb_2.Checked == true)//若连续屈服
                {
                    //弹性段
                    controlType1 = this.cmbCtlType4.SelectedIndex.ToString() + "," + this.txtCtlSpeed4.Text + "," + this.cmbCtlChange4.SelectedIndex.ToString() + "," + this.txtCtlChangeValue4.Text;
                    //试验段
                    controlType2 = this.cmbCtlType5.SelectedIndex.ToString() + "," + this.txtCtlSpeed5.Text;
                    controlType3 = "-";
                    model.isLxqf = 2;
                    model.controlType1 = controlType1;
                    model.controlType2 = controlType2;
                    model.controlType3 = controlType3;
                    model.controlType4 = "-";
                    model.controlType5 = "-";
                    model.controlType6 = "-";
                    model.controlType7 = "-";
                    model.controlType8 = "-";
                    model.controlType9 = "-";
                    model.controlType10 = "-";
                    model.controlType11 = "-";
                    model.controlType12 = this.txtSpeed_lianxu.Text.Trim();
                    model.selResultID = 0;
                    model.circleNum = 0;
                    model.extenValue = 0;
                    model.extenChannel = 0;
                    model.stopValue = double.Parse(this.txtStopValueYes.Text);
                }

                if (rb_3.Checked == true)
                {
                    if (string.IsNullOrEmpty(this.txtStopValue.Text))
                    {
                        MessageBox.Show(this, "请输入停止值", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrEmpty(this.txtCircleNum.Text))
                    {
                        MessageBox.Show(this, "请输入循环次数!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    model.stopValue = double.Parse(this.txtStopValue.Text);
                    model.isLxqf = 3;
                    model.circleNum = int.Parse(this.txtCircleNum.Text);
                    model.controlType1 = "-";
                    model.controlType2 = "-";
                    model.controlType3 = "-";
                    model.controlType4 = "-";
                    model.controlType5 = "-";
                    model.controlType6 = "-";
                    model.controlType7 = "-";
                    model.controlType8 = "-";
                    model.controlType9 = "-";
                    model.controlType10 = "-";
                    model.controlType11 = "-";
                    model.controlType12 = this.txtSpeed_zidingyi.Text.Trim();

                    if (dbViewMethod.Rows.Count == 0)
                    {
                        MessageBox.Show(this, "无自定义试验方法段,不允许保存!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (dbViewMethod.Rows.Count > 0)
                        model.controlType1 = dbViewMethod.Rows[0].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 1)
                        model.controlType2 = dbViewMethod.Rows[1].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 2)
                        model.controlType3 = dbViewMethod.Rows[2].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 3)
                        model.controlType4 = dbViewMethod.Rows[3].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 4)
                        model.controlType5 = dbViewMethod.Rows[4].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 5)
                        model.controlType6 = dbViewMethod.Rows[5].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 6)
                        model.controlType7 = dbViewMethod.Rows[6].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 7)
                        model.controlType8 = dbViewMethod.Rows[7].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 8)
                        model.controlType9 = dbViewMethod.Rows[8].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 9)
                        model.controlType10 = dbViewMethod.Rows[9].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 10)
                        model.controlType11 = dbViewMethod.Rows[10].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[7].Value.ToString();
                    //if (dbViewMethod.Rows.Count > 11)
                    //    model.controlType12 = dbViewMethod.Rows[11].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[7].Value.ToString();

                }
                //是否预载  
                model.isProLoad = this.chkisProLoad.Checked;
                if (chkisProLoad.Checked)
                {
                    model.proLoadType = this.cmbProLoadValueType.SelectedIndex;
                    model.proLoadControlType = this.cmbProLoadCtlMode.SelectedIndex;
                    model.proLoadValue = double.Parse(this.txtproLoadValue.Text);
                    model.proLoadSpeed = double.Parse(this.txtproLoadSpeed.Text);
                }

                //model.ID = int.Parse(this.tvTestMethod.SelectedNode.Name.ToString());
                //是否取引伸计       
                model.isTakeDownExten = this.chkYinShen.Checked;
                if (this.chkYinShen.Checked)
                {
                    model.extenChannel = int.Parse(this.cmbYsChl.Text);
                    model.extenValue = double.Parse(this.txtYsValue.Text);
                    model.selResultID = this.cmbYsType.SelectedIndex;//取引伸计的方式
                }
                //readMethod(this.tvTestMethod); 

                switch (this.cmbTestType.Text)
                {
                    case "拉伸试验"://拉伸
                        BLL.GBT282892012_TensileSel bll28289TensileSel = new HR_Test.BLL.GBT282892012_TensileSel();
                        Model.GBT282892012_TensileSel mSel = bll28289TensileSel.GetModel(this.txtmethodName.Text); // new HR_Test.Model.GBT282892012_TensileSel();
                        if (mSel != null)
                        {
                            mSel.FQmax = this.chkResultList.GetItemChecked(0);
                            mSel.Q = this.chkResultList.GetItemChecked(1);
                            mSel.Q_ = this.chkResultList.GetItemChecked(2);
                            mSel.SQ = this.chkResultList.GetItemChecked(3);
                            mSel.Qc = this.chkResultList.GetItemChecked(4);
                            mSel.isSaveCurve = this.chkResultList.GetItemChecked(5);
                            BLL.GBT282892012_TensileSel bllSel = new HR_Test.BLL.GBT282892012_TensileSel();

                            if (bllcm.Update(model) && bllSel.Update(mSel))
                            {
                                MessageBox.Show("更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Task t = tskReadMethod();
                            }
                            else
                            {
                                MessageBox.Show("更新失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("不存在此试验类型的方法,请添加!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case "剪切试验"://剪切
                        BLL.GBT282892012_ShearSel bll28289ShearSel = new HR_Test.BLL.GBT282892012_ShearSel();
                        Model.GBT282892012_ShearSel msSel = bll28289ShearSel.GetModel(this.txtmethodName.Text);
                        if (msSel != null)
                        {
                            msSel.FTmax = this.chkResultList.GetItemChecked(0);
                            msSel.T = this.chkResultList.GetItemChecked(1);
                            msSel.C1 = this.chkResultList.GetItemChecked(2);
                            msSel.C1R_ = this.chkResultList.GetItemChecked(3);
                            msSel.C1cR = this.chkResultList.GetItemChecked(4);
                            msSel.C1L_ = this.chkResultList.GetItemChecked(5);
                            msSel.C1cL = this.chkResultList.GetItemChecked(6);
                            msSel.C1H_ = this.chkResultList.GetItemChecked(7);
                            msSel.C1cH = this.chkResultList.GetItemChecked(8);
                            msSel.T_ = this.chkResultList.GetItemChecked(9);
                            msSel.ST = this.chkResultList.GetItemChecked(10);
                            msSel.Tc = this.chkResultList.GetItemChecked(11);
                            msSel.saveCurve = this.chkResultList.GetItemChecked(12);

                            BLL.GBT282892012_ShearSel bllsSel = new HR_Test.BLL.GBT282892012_ShearSel();

                            if (bllcm.Update(model) && bllsSel.Update(msSel))
                            {
                                MessageBox.Show("更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Task t = tskReadMethod();
                            }
                            else
                            {
                                MessageBox.Show("更新失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("该试验类型不存在此试验方法,请添加!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case "扭转试验"://扭转
                        BLL.GBT282892012_TwistSel bll28289TwistSel = new HR_Test.BLL.GBT282892012_TwistSel();
                        Model.GBT282892012_TwistSel mtSel = bll28289TwistSel.GetModel(this.txtmethodName.Text);
                        if (mtSel != null)
                        {
                            mtSel.FMmax = this.chkResultList.GetItemChecked(0);
                            mtSel.M = this.chkResultList.GetItemChecked(1);
                            mtSel.M_ = this.chkResultList.GetItemChecked(2);
                            mtSel.SM = this.chkResultList.GetItemChecked(3);
                            mtSel.Mc = this.chkResultList.GetItemChecked(4);
                            mtSel.saveCurve = this.chkResultList.GetItemChecked(5);

                            BLL.GBT282892012_TwistSel blltSel = new HR_Test.BLL.GBT282892012_TwistSel();
                            if (bllcm.Update(model) && blltSel.Update(mtSel))
                            {
                                MessageBox.Show("更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Task t = tskReadMethod();
                            }
                            else
                            {
                                MessageBox.Show("更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("该试验类型不存在此试验方法,请添加!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                }
            }
            cmds.Dispose();
        }

        private void UpdateGBT228()
        {
            //查找存在
            BLL.ControlMethod bllcm = new HR_Test.BLL.ControlMethod();
            DataSet cmds = bllcm.GetList("methodName ='" + this.txtmethodName.Text.Trim() + "'");
            if (cmds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show(this, "不存在该试验方法名称,请添加!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {//保存修改 
                HR_Test.Model.ControlMethod model = bllcm.GetModel(this.txtmethodName.Text);
                model.methodName = this.txtmethodName.Text.Trim();
                model.testStandard = this.cmbTestStandard.Text;
                model.testMethod = this.txtmethodName.Text.Trim();
                //是否连续屈服 
                string controlType1 = "-";
                string controlType2 = "-";
                string controlType3 = "-";

                if (rb_1.Checked == true)//若不连续屈服
                {
                    //弹性段  
                    controlType1 = this.cmbCtlType1.SelectedIndex.ToString() + "," + this.txtCtlSpeed1.Text + "," + this.cmbCtlChange1.SelectedIndex.ToString() + "," + this.txtCtlChangeValue1.Text;
                    //屈服段
                    controlType2 = this.cmbCtlType2.SelectedIndex.ToString() + "," + this.txtCtlSpeed2.Text + "," + this.cmbCtlChange2.SelectedIndex.ToString() + "," + this.txtCtlChangeValue2.Text;
                    //停止测试
                    controlType3 = this.cmbCtlType3.SelectedIndex.ToString() + "," + this.txtCtlSpeed3.Text;

                    model.isLxqf = 1;
                    model.controlType1 = controlType1;
                    model.controlType2 = controlType2;
                    model.controlType3 = controlType3;
                    model.controlType4 = "-";
                    model.controlType5 = "-";
                    model.controlType6 = "-";
                    model.controlType7 = "-";
                    model.controlType8 = "-";
                    model.controlType9 = "-";
                    model.controlType10 = "-";
                    model.controlType11 = "-";
                    model.selResultID = 0;
                    model.circleNum = 0;
                    model.extenValue = 0;
                    model.extenChannel = 0;
                    model.stopValue = double.Parse(this.txtStopValueNo.Text);
                    model.controlType12 = this.txtSpeed_bulianxu.Text.Trim();
                }

                if (rb_2.Checked == true)//若连续屈服
                {
                    //弹性段
                    controlType1 = this.cmbCtlType4.SelectedIndex.ToString() + "," + this.txtCtlSpeed4.Text + "," + this.cmbCtlChange4.SelectedIndex.ToString() + "," + this.txtCtlChangeValue4.Text;
                    //试验段
                    controlType2 = this.cmbCtlType5.SelectedIndex.ToString() + "," + this.txtCtlSpeed5.Text;
                    controlType3 = "-";
                    model.isLxqf = 2;
                    model.controlType1 = controlType1;
                    model.controlType2 = controlType2;
                    model.controlType3 = controlType3;
                    model.controlType4 = "-";
                    model.controlType5 = "-";
                    model.controlType6 = "-";
                    model.controlType7 = "-";
                    model.controlType8 = "-";
                    model.controlType9 = "-";
                    model.controlType10 = "-";
                    model.controlType11 = "-";
                    model.controlType12 = this.txtSpeed_lianxu.Text.Trim();
                    model.selResultID = 0;
                    model.circleNum = 0;
                    model.extenValue = 0;
                    model.extenChannel = 0;
                    model.stopValue = double.Parse(this.txtStopValueYes.Text);
                }

                if (rb_3.Checked == true)
                {
                    if (string.IsNullOrEmpty(this.txtStopValue.Text))
                    {
                        MessageBox.Show(this, "请输入停止值", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrEmpty(this.txtCircleNum.Text))
                    {
                        MessageBox.Show(this, "请输入循环次数!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    model.stopValue = double.Parse(this.txtStopValue.Text);
                    model.isLxqf = 3;
                    model.circleNum = int.Parse(this.txtCircleNum.Text);
                    model.controlType1 = "-";
                    model.controlType2 = "-";
                    model.controlType3 = "-";
                    model.controlType4 = "-";
                    model.controlType5 = "-";
                    model.controlType6 = "-";
                    model.controlType7 = "-";
                    model.controlType8 = "-";
                    model.controlType9 = "-";
                    model.controlType10 = "-";
                    model.controlType11 = "-";
                    model.controlType12 = this.txtSpeed_zidingyi.Text.Trim();

                    if (dbViewMethod.Rows.Count == 0)
                    {
                        MessageBox.Show(this, "无自定义试验方法段,不允许保存!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (dbViewMethod.Rows.Count > 0)
                        model.controlType1 = dbViewMethod.Rows[0].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 1)
                        model.controlType2 = dbViewMethod.Rows[1].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 2)
                        model.controlType3 = dbViewMethod.Rows[2].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 3)
                        model.controlType4 = dbViewMethod.Rows[3].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 4)
                        model.controlType5 = dbViewMethod.Rows[4].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 5)
                        model.controlType6 = dbViewMethod.Rows[5].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 6)
                        model.controlType7 = dbViewMethod.Rows[6].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 7)
                        model.controlType8 = dbViewMethod.Rows[7].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 8)
                        model.controlType9 = dbViewMethod.Rows[8].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 9)
                        model.controlType10 = dbViewMethod.Rows[9].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 10)
                        model.controlType11 = dbViewMethod.Rows[10].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[7].Value.ToString();

                }
                //是否预载  
                model.isProLoad = this.chkisProLoad.Checked;
                if (chkisProLoad.Checked)
                {
                    model.proLoadType = this.cmbProLoadValueType.SelectedIndex;
                    model.proLoadControlType = this.cmbProLoadCtlMode.SelectedIndex;
                    model.proLoadValue = double.Parse(this.txtproLoadValue.Text);
                    model.proLoadSpeed = double.Parse(this.txtproLoadSpeed.Text);
                }

                //model.ID = int.Parse(this.tvTestMethod.SelectedNode.Name.ToString());
                //是否取引伸计       
                model.isTakeDownExten = this.chkYinShen.Checked;
                if (this.chkYinShen.Checked)
                {
                    model.extenChannel = int.Parse(this.cmbYsChl.Text);
                    model.extenValue = double.Parse(this.txtYsValue.Text);
                    model.selResultID = this.cmbYsType.SelectedIndex;//取引伸计的方式
                }
                //readMethod(this.tvTestMethod); 
                BLL.SelTestResult bllSel = new HR_Test.BLL.SelTestResult();
                Model.SelTestResult mSel = bllSel.GetModel(this.txtmethodName.Text.Trim());
                mSel.methodName = this.txtmethodName.Text; //this.tvTestMethod.SelectedNode.Text;// 
                mSel.Fm = this.chkResultList.GetItemChecked(0);   //"             Fm               kN                 最大力",
                mSel.Rm = this.chkResultList.GetItemChecked(1);   //"             Rm               MPa                抗拉强度",
                mSel.ReH = this.chkResultList.GetItemChecked(2);  //"             ReH              MPa                上屈服强度",
                mSel.ReL = this.chkResultList.GetItemChecked(3);  //"             ReL              MPa                下屈服强度",
                mSel.Rp = this.chkResultList.GetItemChecked(4);   //"             Rp               MPa                规定塑性延伸强度",
                mSel.Rt = this.chkResultList.GetItemChecked(5);   //"             Rt               MPa                规定总延伸强度",
                mSel.Rr = this.chkResultList.GetItemChecked(6);   //"             Rr               MPa                规定残余延伸强度",
                //mSel.εp = this.chkResultList.GetItemChecked(7);  //"             εp                                ",
                //mSel.εt = this.chkResultList.GetItemChecked(8);  //"             εt                                ", 
                //mSel.εr = this.chkResultList.GetItemChecked(9);  //"             εr                                ",   
                mSel.E = this.chkResultList.GetItemChecked(7);    //"             E                GPa                弹性模量",
                mSel.m = this.chkResultList.GetItemChecked(8);    //"             m                MPa                应力-延伸率曲线在给定试验时刻的斜率",
                mSel.mE = this.chkResultList.GetItemChecked(9);   //"             mE               MPa                应力-延伸率曲线弹性部分的斜率",
                mSel.A = this.chkResultList.GetItemChecked(10);    //"             A                %                  断后伸长率",  
                mSel.Aee = this.chkResultList.GetItemChecked(11);  //"             Ae               %                  屈服点延伸率",
                mSel.Agg = this.chkResultList.GetItemChecked(12);  //"             Ag               %                  最大力Fm塑性延伸率",
                mSel.Att = this.chkResultList.GetItemChecked(13);  //"             At               %                  断裂总延伸率",            
                mSel.Aggtt = this.chkResultList.GetItemChecked(14);//"             Agt              %                  最大力Fm总延伸率",
                mSel.Awnwn = this.chkResultList.GetItemChecked(15);//"             Awn              %                  无缩颈塑性伸长率", 
                mSel.deltaLm = this.chkResultList.GetItemChecked(16);   //"             △Lm             mm                 最大力总延伸",
                mSel.Lf = this.chkResultList.GetItemChecked(17);   //"             △Lf             mm                 断裂总延伸", 
                mSel.Z = this.chkResultList.GetItemChecked(18);    //"             Z                %                  断面收缩率",
                mSel.Avera = this.chkResultList.GetItemChecked(19);//"             X                MPa                平均值",
                mSel.SS = this.chkResultList.GetItemChecked(20);   //"             S                %                  标准偏差",
                mSel.Avera1 = this.chkResultList.GetItemChecked(21);//"             X￣              MPa                剔除最大值最小值后的平均值", 
                mSel.CV = this.chkResultList.GetItemChecked(22);
                mSel.Handaz = this.chkResultList.GetItemChecked(23);
                mSel.Savecurve = this.chkResultList.GetItemChecked(24);
                mSel.Lm = this.chkResultList.GetItemChecked(25);
                if (bllSel.Update(mSel) && bllcm.Update(model))
                    MessageBox.Show("更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("更新失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            cmds.Dispose();
        }

        private void UpdateGBT7314()
        {
            //查找存在
            BLL.ControlMethod_C bllcm_C = new HR_Test.BLL.ControlMethod_C();
            DataSet cmds_C = bllcm_C.GetList("methodName ='" + this.txtmethodName.Text.Trim() + "'");
            if (cmds_C.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show(this, "不存在该试验方法名称,请添加!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {//保存修改 
                HR_Test.Model.ControlMethod_C model_C = bllcm_C.GetModel(this.txtmethodName.Text);
                model_C.methodName = this.txtmethodName.Text.Trim();
                model_C.testStandard = this.cmbTestStandard.Text;
                model_C.testMethod = this.txtmethodName.Text.Trim();
                //是否连续屈服 
                string controlType1 = "-";
                string controlType2 = "-";
                string controlType3 = "-";
                if (rb_1.Checked == true)//若不连续屈服
                {
                    //弹性段  
                    controlType1 = this.cmbCtlType1.SelectedIndex.ToString() + "," + this.txtCtlSpeed1.Text + "," + this.cmbCtlChange1.SelectedIndex.ToString() + "," + this.txtCtlChangeValue1.Text;
                    //屈服段
                    controlType2 = this.cmbCtlType2.SelectedIndex.ToString() + "," + this.txtCtlSpeed2.Text + "," + this.cmbCtlChange2.SelectedIndex.ToString() + "," + this.txtCtlChangeValue2.Text;
                    //停止测试
                    controlType3 = this.cmbCtlType3.SelectedIndex.ToString() + "," + this.txtCtlSpeed3.Text;

                    model_C.isLxqf = 1;
                    model_C.controlType1 = controlType1;
                    model_C.controlType2 = controlType2;
                    model_C.controlType3 = controlType3;
                    model_C.controlType4 = "-";
                    model_C.controlType5 = "-";
                    model_C.controlType6 = "-";
                    model_C.controlType7 = "-";
                    model_C.controlType8 = "-";
                    model_C.controlType9 = "-";
                    model_C.controlType10 = "-";
                    model_C.controlType11 = "-";
                    model_C.controlType12 = this.txtSpeed_bulianxu.Text.Trim();
                    model_C.circleNum = 0;

                    model_C.stopValue = double.Parse(this.txtStopValueNo.Text);
                }

                if (rb_2.Checked == true)//若连续屈服
                {
                    //弹性段
                    controlType1 = this.cmbCtlType4.SelectedIndex.ToString() + "," + this.txtCtlSpeed4.Text + "," + this.cmbCtlChange4.SelectedIndex.ToString() + "," + this.txtCtlChangeValue4.Text;
                    //试验段
                    controlType2 = this.cmbCtlType5.SelectedIndex.ToString() + "," + this.txtCtlSpeed5.Text;

                    model_C.isLxqf = 2;
                    model_C.controlType1 = controlType1;
                    model_C.controlType2 = controlType2;
                    model_C.controlType3 = "-";
                    model_C.controlType4 = "-";
                    model_C.controlType5 = "-";
                    model_C.controlType6 = "-";
                    model_C.controlType7 = "-";
                    model_C.controlType8 = "-";
                    model_C.controlType9 = "-";
                    model_C.controlType10 = "-";
                    model_C.controlType11 = "-";
                    model_C.controlType12 = this.txtSpeed_lianxu.Text.Trim();
                    model_C.circleNum = 0;
                    model_C.stopValue = double.Parse(this.txtStopValueYes.Text);
                }

                if (rb_3.Checked == true)
                {
                    model_C.isLxqf = 3;
                    model_C.stopValue = double.Parse(this.txtStopValue.Text);
                    model_C.circleNum = int.Parse(this.txtCircleNum.Text);
                    model_C.controlType1 = "-";
                    model_C.controlType2 = "-";
                    model_C.controlType3 = "-";
                    model_C.controlType4 = "-";
                    model_C.controlType5 = "-";
                    model_C.controlType6 = "-";
                    model_C.controlType7 = "-";
                    model_C.controlType8 = "-";
                    model_C.controlType9 = "-";
                    model_C.controlType10 = "-";
                    model_C.controlType11 = "-";
                    model_C.controlType12 = this.txtSpeed_zidingyi.Text.Trim();

                    if (dbViewMethod.Rows.Count > 0)
                        model_C.controlType1 = dbViewMethod.Rows[0].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 1)
                        model_C.controlType2 = dbViewMethod.Rows[1].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 2)
                        model_C.controlType3 = dbViewMethod.Rows[2].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 3)
                        model_C.controlType4 = dbViewMethod.Rows[3].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 4)
                        model_C.controlType5 = dbViewMethod.Rows[4].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 5)
                        model_C.controlType6 = dbViewMethod.Rows[5].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 6)
                        model_C.controlType7 = dbViewMethod.Rows[6].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 7)
                        model_C.controlType8 = dbViewMethod.Rows[7].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 8)
                        model_C.controlType9 = dbViewMethod.Rows[8].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 9)
                        model_C.controlType10 = dbViewMethod.Rows[9].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 10)
                        model_C.controlType11 = dbViewMethod.Rows[10].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[7].Value.ToString();
                    //if (dbViewMethod.Rows.Count > 11)
                    //    model_C.controlType12 = dbViewMethod.Rows[11].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[7].Value.ToString();

                }
                //是否预载  
                model_C.isProLoad = this.chkisProLoad.Checked;
                if (chkisProLoad.Checked)
                {
                    model_C.proLoadType = this.cmbProLoadValueType.SelectedIndex;
                    model_C.proLoadControlType = this.cmbProLoadCtlMode.SelectedIndex;
                    model_C.proLoadValue = double.Parse(this.txtproLoadValue.Text);
                    model_C.proLoadSpeed = double.Parse(this.txtproLoadSpeed.Text);
                }

                model_C.selResultID = 0;
                model_C.Ff = 0;
                //model_C.ID = int.Parse(this.tvTestMethod.SelectedNode.Name.ToString());
                //是否取引伸计       
                model_C.isTakeDownExten = this.chkYinShen.Checked;
                if (this.chkYinShen.Checked)
                {
                    model_C.extenChannel = int.Parse(this.cmbYsChl.Text);
                    model_C.extenValue = double.Parse(this.txtYsValue.Text);
                    model_C.selResultID = this.cmbYsType.SelectedIndex;//取引伸计的方式
                }
                else
                {
                    model_C.extenValue = 0;
                    model_C.extenChannel = 0;
                    model_C.selResultID = 0;
                }
                //readMethod(this.tvTestMethod); 
                BLL.SelTestResult_C bllSel_C = new HR_Test.BLL.SelTestResult_C();
                Model.SelTestResult_C mSel_C = bllSel_C.GetModel(this.txtmethodName.Text.Trim());
                mSel_C.methodName = this.txtmethodName.Text.Trim();
                mSel_C.Fmc = this.chkResultList.GetItemChecked(0);
                mSel_C.Fpc = this.chkResultList.GetItemChecked(1);
                mSel_C.Ftc = this.chkResultList.GetItemChecked(2);
                mSel_C.FeHc = this.chkResultList.GetItemChecked(3);
                mSel_C.FeLc = this.chkResultList.GetItemChecked(4);
                mSel_C.Rmc = this.chkResultList.GetItemChecked(5);
                mSel_C.Rpc = this.chkResultList.GetItemChecked(6);
                mSel_C.Rtc = this.chkResultList.GetItemChecked(7);
                mSel_C.ReHc = this.chkResultList.GetItemChecked(8);
                mSel_C.ReLc = this.chkResultList.GetItemChecked(9);
                mSel_C.Ec = this.chkResultList.GetItemChecked(10);
                mSel_C.Mean = this.chkResultList.GetItemChecked(11);
                mSel_C.SD = this.chkResultList.GetItemChecked(12);
                mSel_C.Mid = this.chkResultList.GetItemChecked(13);
                mSel_C.CV = this.chkResultList.GetItemChecked(14);
                mSel_C.saveCurve = this.chkResultList.GetItemChecked(15);
                if (bllcm_C.Update(model_C) && bllSel_C.Update(mSel_C))
                {
                    MessageBox.Show("更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("更新失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            cmds_C.Dispose();
        }

        private void UpdateYBT5349()
        {
            //查找存在
            BLL.ControlMethod_B bllcm_B = new HR_Test.BLL.ControlMethod_B();
            DataSet cmds_B = bllcm_B.GetList("methodName ='" + this.txtmethodName.Text.Trim() + "'");
            if (cmds_B.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show(this, "不存在该试验方法名称,请添加!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {//保存修改 
                HR_Test.Model.ControlMethod_B model_B = bllcm_B.GetModel(this.txtmethodName.Text.Trim());
                model_B.methodName = this.txtmethodName.Text.Trim();
                model_B.testStandard = this.cmbTestStandard.Text;
                model_B.testMethod = this.txtmethodName.Text.Trim();
                //是否连续屈服 
                string controlType1 = "-";
                string controlType2 = "-";
                string controlType3 = "-";
                if (rb_1.Checked == true)//若不连续屈服
                {
                    //弹性段  
                    controlType1 = this.cmbCtlType1.SelectedIndex.ToString() + "," + this.txtCtlSpeed1.Text + "," + this.cmbCtlChange1.SelectedIndex.ToString() + "," + this.txtCtlChangeValue1.Text;
                    //屈服段
                    controlType2 = this.cmbCtlType2.SelectedIndex.ToString() + "," + this.txtCtlSpeed2.Text + "," + this.cmbCtlChange2.SelectedIndex.ToString() + "," + this.txtCtlChangeValue2.Text;
                    //停止测试
                    controlType3 = this.cmbCtlType3.SelectedIndex.ToString() + "," + this.txtCtlSpeed3.Text;

                    model_B.isLxqf = 1;
                    model_B.controlType1 = controlType1;
                    model_B.controlType2 = controlType2;
                    model_B.controlType3 = controlType3;
                    model_B.controlType4 = "-";
                    model_B.controlType5 = "-";
                    model_B.controlType6 = "-";
                    model_B.controlType7 = "-";
                    model_B.controlType8 = "-";
                    model_B.controlType9 = "-";
                    model_B.controlType10 = "-";
                    model_B.controlType11 = "-";
                    model_B.controlType12 = this.txtSpeed_bulianxu.Text.Trim();
                    model_B.selResultID = 0;
                    model_B.circleNum = 0;// int.Parse(this.txtCircleNum.Text);

                    model_B.stopValue = double.Parse(this.txtStopValueNo.Text);
                }
                if (rb_2.Checked == true)//若连续屈服
                {
                    //弹性段
                    controlType1 = this.cmbCtlType4.SelectedIndex.ToString() + "," + this.txtCtlSpeed4.Text + "," + this.cmbCtlChange4.SelectedIndex.ToString() + "," + this.txtCtlChangeValue4.Text;
                    //试验段
                    controlType2 = this.cmbCtlType5.SelectedIndex.ToString() + "," + this.txtCtlSpeed5.Text;
                    model_B.isLxqf = 2;
                    model_B.controlType1 = controlType1;
                    model_B.controlType2 = controlType2;
                    model_B.controlType3 = "-";
                    model_B.controlType4 = "-";
                    model_B.controlType5 = "-";
                    model_B.controlType6 = "-";
                    model_B.controlType7 = "-";
                    model_B.controlType8 = "-";
                    model_B.controlType9 = "-";
                    model_B.controlType10 = "-";
                    model_B.controlType11 = "-";
                    model_B.controlType12 = this.txtSpeed_lianxu.Text.Trim();
                    model_B.selResultID = 0;
                    model_B.circleNum = 0;// int.Parse(this.txtCircleNum.Text); 
                    model_B.stopValue = double.Parse(this.txtStopValueYes.Text);
                }
                if (rb_3.Checked == true)
                {
                    model_B.isLxqf = 3;
                    model_B.controlType1 = "-";
                    model_B.controlType2 = "-";
                    model_B.controlType3 = "-";
                    model_B.controlType4 = "-";
                    model_B.controlType5 = "-";
                    model_B.controlType6 = "-";
                    model_B.controlType7 = "-";
                    model_B.controlType8 = "-";
                    model_B.controlType9 = "-";
                    model_B.controlType10 = "-";
                    model_B.controlType11 = "-";
                    model_B.controlType12 = this.txtSpeed_zidingyi.Text.Trim();
                    model_B.selResultID = 0;
                    model_B.circleNum = int.Parse(this.txtCircleNum.Text);
                    model_B.stopValue = double.Parse(this.txtStopValue.Text);

                    if (dbViewMethod.Rows.Count > 0)
                        model_B.controlType1 = dbViewMethod.Rows[0].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[0].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 1)
                        model_B.controlType2 = dbViewMethod.Rows[1].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[1].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 2)
                        model_B.controlType3 = dbViewMethod.Rows[2].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[2].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 3)
                        model_B.controlType4 = dbViewMethod.Rows[3].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[3].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 4)
                        model_B.controlType5 = dbViewMethod.Rows[4].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[4].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 5)
                        model_B.controlType6 = dbViewMethod.Rows[5].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[5].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 6)
                        model_B.controlType7 = dbViewMethod.Rows[6].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[6].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 7)
                        model_B.controlType8 = dbViewMethod.Rows[7].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[7].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 8)
                        model_B.controlType9 = dbViewMethod.Rows[8].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[8].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 9)
                        model_B.controlType10 = dbViewMethod.Rows[9].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[9].Cells[7].Value.ToString();
                    if (dbViewMethod.Rows.Count > 10)
                        model_B.controlType11 = dbViewMethod.Rows[10].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[10].Cells[7].Value.ToString();
                    //if (dbViewMethod.Rows.Count > 11)
                    //    model_B.controlType12 = dbViewMethod.Rows[11].Cells[2].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[4].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[5].Value.ToString() + "," + dbViewMethod.Rows[11].Cells[7].Value.ToString();

                }

                model_B.Ds = 0; //支撑滚柱直径
                model_B.Da = 0; //施力滚柱直径
                model_B.R = 0; //刀刃半径 
                model_B.Ls = 0; //跨距
                model_B.Le = 0; //扰度计跨距
                model_B.l_l = 0;//力臂 
                model_B.m = 0;
                model_B.n = 0;
                model_B.a = 0;
                model_B.εrb = 0;
                model_B.εpb = 0;
                model_B.testType = "三点弯曲";


                //是否预载                          
                model_B.isProLoad = this.chkisProLoad.Checked;
                if (chkisProLoad.Checked)
                {
                    model_B.proLoadType = this.cmbProLoadValueType.SelectedIndex;
                    model_B.proLoadControlType = this.cmbProLoadCtlMode.SelectedIndex;
                    model_B.proLoadValue = double.Parse(this.txtproLoadValue.Text);
                    model_B.proLoadSpeed = double.Parse(this.txtproLoadSpeed.Text);
                }

                //model_B.ID = int.Parse(this.tvTestMethod.SelectedNode.Name.ToString());
                //是否取引伸计       
                model_B.isTakeDownExten = this.chkYinShen.Checked;
                if (this.chkYinShen.Checked)
                {
                    model_B.extenChannel = int.Parse(this.cmbYsChl.Text);
                    model_B.extenValue = double.Parse(this.txtYsValue.Text);
                    model_B.selResultID = this.cmbYsType.SelectedIndex;//取引伸计的方式
                }
                else
                {
                    model_B.extenValue = 0;
                    model_B.extenChannel = 0;
                    model_B.selResultID = 0;
                }

                //readMethod(this.tvTestMethod); 
                BLL.SelTestResult_B bllSel_B = new HR_Test.BLL.SelTestResult_B();
                Model.SelTestResult_B mSel_B = bllSel_B.GetModel(this.txtmethodName.Text.Trim());

                mSel_B.methodName = this.txtmethodName.Text.Trim();
                //"             fbb              mm                 断裂扰度",
                mSel_B.f_bb = this.chkResultList.GetItemChecked(0);
                //"             fn               mm                 最后一次施力并将其卸除后的残余扰度",
                mSel_B.f_n = this.chkResultList.GetItemChecked(1);
                //"             fn-1             mm                 最后前一次施力并将其卸除后的残余扰度", 
                mSel_B.f_n1 = this.chkResultList.GetItemChecked(2);
                //"             frb              mm                 达到规定残余弯曲应变时的残余扰度",  
                mSel_B.f_rb = this.chkResultList.GetItemChecked(3);
                //"             y                mm                 试样弯曲时中性面至弯曲外表面的最大距离",  
                mSel_B.y = this.chkResultList.GetItemChecked(4);
                //"             Fo               N                  预弯曲力",  
                mSel_B.Fo = this.chkResultList.GetItemChecked(5);
                //"             Fpb              N                  规定非比例弯曲力",  
                mSel_B.Fpb = this.chkResultList.GetItemChecked(6);
                //"             Frb              N                  规定残余弯曲力",  
                mSel_B.Frb = this.chkResultList.GetItemChecked(7);
                //"             Fbb              N                  最大弯曲力",  
                mSel_B.Fbb = this.chkResultList.GetItemChecked(8);
                //"             Fn               N                  最后一次施加的弯曲力",  
                mSel_B.Fn = this.chkResultList.GetItemChecked(9);
                //"             Fn-1             N                  最后前一次施加的弯曲力",  
                mSel_B.Fn1 = this.chkResultList.GetItemChecked(10);
                //"             Z                N/mm               力轴每毫米代表的力值",  
                mSel_B.Z = this.chkResultList.GetItemChecked(11);
                //"             S                ㎜²                弯曲试验曲线下包围的面积", 
                mSel_B.S = this.chkResultList.GetItemChecked(12);
                //"             W                ㎜³                试样截面系数",  
                mSel_B.W = this.chkResultList.GetItemChecked(13);
                //"             I                ㎜^4               试样截面惯性矩",  
                mSel_B.I = this.chkResultList.GetItemChecked(14);
                //"             Eb               MPa                弯曲弹性模量",  
                mSel_B.Eb = this.chkResultList.GetItemChecked(15);
                //"             σpb             MPa                规定非比例弯曲应力", 
                mSel_B.σpb = this.chkResultList.GetItemChecked(16);
                //"             σrb             MPa                规定残余弯曲应力",  
                mSel_B.σrb = this.chkResultList.GetItemChecked(17);
                //"             σbb             MPa                抗弯强度",  
                mSel_B.σbb = this.chkResultList.GetItemChecked(18);
                //"             U                MPa                弯曲断裂能量",  
                mSel_B.U = this.chkResultList.GetItemChecked(19);

                //"             Mean.            -                  平均值",  
                mSel_B.Mean = this.chkResultList.GetItemChecked(20);
                //"             S.D.             -                  标准偏差",  
                mSel_B.SD = this.chkResultList.GetItemChecked(21);
                //"             Mid.             -                  中间值", 
                mSel_B.Mid = this.chkResultList.GetItemChecked(22);
                //"             C.V.             -                  变异系数", 
                mSel_B.CV = this.chkResultList.GetItemChecked(23);
                //"             曲线             -                  是否保存曲线", 
                mSel_B.saveCurve = this.chkResultList.GetItemChecked(24);

                if (bllcm_B.Update(model_B) && bllSel_B.Update(mSel_B))
                {
                    MessageBox.Show("更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("更新失败!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            cmds_B.Dispose();
        }

        //删除试验方法
        private void gBtnDel_Click(object sender, EventArgs e)
        {
            if (this.tvTestMethod.SelectedNode != null)
            {
                if (!string.IsNullOrEmpty(this.tvTestMethod.SelectedNode.Name))
                {
                    if (DialogResult.OK == MessageBox.Show("是否删除 " + this.tvTestMethod.SelectedNode.Text.Trim() + " 试验方法", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    {
                        //e.Node.Name 试验类型标识 1：拉伸 2：压缩 3：弯曲
                        switch (this.tvTestMethod.SelectedNode.Tag.ToString())
                        {
                            case "GB/T 23615.1-2009":
                                BLL.GBT236152009_Method bll23615 = new HR_Test.BLL.GBT236152009_Method();
                                Model.GBT236152009_Method model23615 = bll23615.GetModel(this.tvTestMethod.SelectedNode.Text.Trim());
                                if (model23615 != null)
                                {
                                    string testType = model23615.xmlPath;
                                    switch (testType)
                                    {
                                        case "纵向拉伸":
                                            BLL.GBT236152009_SelZong selZong = new HR_Test.BLL.GBT236152009_SelZong();
                                            if (bll23615.Delete(this.tvTestMethod.SelectedNode.Text.Trim()) && selZong.Delete(this.tvTestMethod.SelectedNode.Text.Trim()))
                                            {
                                                Task t = tskReadMethod();
                                            }
                                            break;
                                        case "横向拉伸":
                                            BLL.GBT236152009_SelHeng selHeng = new HR_Test.BLL.GBT236152009_SelHeng();
                                            if (bll23615.Delete(this.tvTestMethod.SelectedNode.Text.Trim()) && selHeng.Delete(this.tvTestMethod.SelectedNode.Text.Trim()))
                                            {
                                                Task t = tskReadMethod();
                                            }
                                            break;
                                    }

                                }
                                break;
                            case "GB/T 228-2010":
                                BLL.ControlMethod bllCm = new HR_Test.BLL.ControlMethod();
                                BLL.SelTestResult selCm = new HR_Test.BLL.SelTestResult();
                                if (bllCm.Delete(this.tvTestMethod.SelectedNode.Text.Trim()) && selCm.Delete(this.tvTestMethod.SelectedNode.Text.Trim()))
                                {
                                    Task t = tskReadMethod();
                                }
                                break;
                            case "GB/T 7314-2005":
                                BLL.ControlMethod_C bllCm_C = new HR_Test.BLL.ControlMethod_C();
                                BLL.SelTestResult_C selCm_C = new HR_Test.BLL.SelTestResult_C();
                                if (bllCm_C.Delete(this.tvTestMethod.SelectedNode.Text.Trim()) && selCm_C.Delete(this.tvTestMethod.SelectedNode.Text.Trim()))
                                {
                                    Task t = tskReadMethod();
                                }
                                break;

                            case "YB/T 5349-2006":
                                BLL.ControlMethod_B bllCm_B = new HR_Test.BLL.ControlMethod_B();
                                BLL.SelTestResult_B selCm_B = new HR_Test.BLL.SelTestResult_B();
                                if (bllCm_B.Delete(this.tvTestMethod.SelectedNode.Text.Trim()) && selCm_B.Delete(this.tvTestMethod.SelectedNode.Text.Trim()))
                                {
                                    Task t = tskReadMethod();
                                }
                                break;
                            case "GB/T 28289-2012":
                                BLL.GBT282892012_Method bll28289 = new HR_Test.BLL.GBT282892012_Method();
                                Model.GBT282892012_Method model28289 = bll28289.GetModel(this.tvTestMethod.SelectedNode.Text.Trim());
                                if (model28289 != null)
                                {
                                    string testType = model28289.xmlPath;
                                    switch (testType)
                                    {
                                        case "拉伸试验":
                                            BLL.GBT282892012_TensileSel selT = new HR_Test.BLL.GBT282892012_TensileSel();
                                            if (bll28289.Delete(this.tvTestMethod.SelectedNode.Text.Trim()) && selT.Delete(this.tvTestMethod.SelectedNode.Text.Trim()))
                                            {
                                                Task t = tskReadMethod();
                                            }
                                            break;
                                        case "剪切试验":
                                            BLL.GBT282892012_ShearSel selS = new HR_Test.BLL.GBT282892012_ShearSel();
                                            if (bll28289.Delete(this.tvTestMethod.SelectedNode.Text.Trim()) && selS.Delete(this.tvTestMethod.SelectedNode.Text.Trim()))
                                            {
                                                Task t = tskReadMethod();
                                            }
                                            break;
                                        case "扭转试验":
                                            BLL.GBT282892012_TwistSel selTw = new HR_Test.BLL.GBT282892012_TwistSel();
                                            if (bll28289.Delete(this.tvTestMethod.SelectedNode.Text.Trim()) && selTw.Delete(this.tvTestMethod.SelectedNode.Text.Trim()))
                                            {
                                                Task t = tskReadMethod();
                                            }
                                            break;
                                    }

                                }
                                break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的试验方法!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void txtSelAll_Click(object sender, EventArgs e)
        {
            //取消全选
            //TextBox tb = (TextBox)sender;
            //tb.SelectionStart=0;
            //tb.SelectionLength = tb.Text.Length;
        }

        private void txtCtl_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (!utils.IsNumeric(tb.Text))
                tb.Text = "";
        }

        private void cmbProLoadValueType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //变形 负荷
            switch (cmbProLoadValueType.SelectedIndex)
            {
                case 0:
                    this.lblkN.Text = "mm";
                    break;
                case 1:
                    this.lblkN.Text = "kN";
                    break;
                default:
                    this.lblkN.Text = "kN";
                    break;
            }
        }

        private void cmbProLoadCtlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //位移
            //负荷
            switch (cmbProLoadCtlMode.SelectedIndex)
            {
                case 0:
                    this.lblkNs.Text = "mm/min";
                    break;
                case 1:
                    this.lblkNs.Text = "kN/s";
                    break;
            }
        }

        private void cmbCtlType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //位移控制
            //负荷控制
            //应力控制
            //ēLc控制
            //ēLe控制
            //变形控制
            switch (cmbCtlType1.SelectedIndex)
            {
                case 0:
                    this.lbl1.Text = "mm/min";
                    break;
                case 1:
                    this.lbl1.Text = "kN/s";
                    break;
                case 2:
                    this.lbl1.Text = "MPa/s";
                    break;
                case 3:
                case 4:
                    this.lbl1.Text = "/s";
                    break;
                case 5:
                    this.lbl1.Text = "mm/s";
                    break;

            }
            this.lbl1.Refresh();
        }

        private void cmbCtlType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //位移
            //eLc
            switch (cmbCtlType2.SelectedIndex)
            {
                case 0:
                    this.lbl2.Text = "mm/min";
                    break;
                case 1:
                    this.lbl2.Text = "/s";
                    break;
            }
            this.lbl2.Refresh();
        }

        private void cmbCtlType3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //位移控制
            //ēLc控制
            switch (cmbCtlType3.SelectedIndex)
            {
                case 0:
                    this.lbl3.Text = "mm/min";
                    break;
                case 1:
                    this.lbl3.Text = "/s";
                    break;

            }
        }

        private void cmbCtlChange1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //位移
            //负荷
            //变形
            //应力
            //应变
            switch (cmbCtlChange1.SelectedIndex)
            {
                case 0:
                    this.lbl4.Text = "mm";
                    break;
                case 1:
                    this.lbl4.Text = "kN";
                    break;
                case 2:
                    this.lbl4.Text = "mm";
                    break;
                case 3:
                    this.lbl4.Text = "MPa";
                    break;
                case 4:
                    this.lbl4.Text = "%";
                    break;
            }
        }

        private void cmbCtlChange2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //位移
            //负荷
            //变形
            //应力
            //应变
            switch (cmbCtlChange2.SelectedIndex)
            {
                case 0:
                    this.lbl5.Text = "mm";
                    break;
                case 1:
                    this.lbl5.Text = "kN";
                    break;
                case 2:
                    this.lbl5.Text = "mm";
                    break;
                case 3:
                    this.lbl5.Text = "MPa";
                    break;
                case 4:
                    this.lbl5.Text = "%";
                    break;
            }
        }

        private void cmbCtlType4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //位移控制
            //负荷控制
            //应力控制
            //ēLc控制
            //ēLe控制
            //变形控制
            switch (cmbCtlType4.SelectedIndex)
            {
                case 0:
                    this.lbl6.Text = "mm/min";
                    break;
                case 1:
                    this.lbl6.Text = "kN/s";
                    break;
                case 2:
                    this.lbl6.Text = "MPa/s";
                    break;
                case 3:
                case 4:
                    this.lbl6.Text = "/s";
                    break;
                case 5:
                    this.lbl6.Text = "mm/s";
                    break;
            }
            this.lbl6.Refresh();
        }

        private void cmbCtlType5_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbCtlType5.SelectedIndex)
            {
                case 0:
                    this.lbl8.Text = "mm/min";
                    break;
                case 1:
                    this.lbl8.Text = "/s";
                    break;
            }
        }

        private void cmbCtlChange4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //位移
            //负荷
            //变形
            //应力
            //应变
            switch (cmbCtlChange4.SelectedIndex)
            {
                case 0:
                    this.lbl7.Text = "mm";
                    break;
                case 1:
                    this.lbl7.Text = "kN";
                    break;
                case 2:
                    this.lbl7.Text = "mm";
                    break;
                case 3:
                    this.lbl7.Text = "MPa";
                    break;
                case 4:
                    this.lbl7.Text = "%";
                    break;
            }
        }

        private void chkYinShen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkYinShen.Checked == true)
            {
                this.gbYs.Enabled = true;
            }
            else
            {
                this.gbYs.Enabled = false;
            }
        }

        private void chkYs_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void customTabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked == true)
            {
                rb.BackColor = Color.DarkGray; //Color.FromArgb(25, 119, 176);
                rb.ForeColor = Color.White;
                switch (rb.Name)
                {
                    case "rb_1":
                        this.customPanel1.Visible = true;
                        this.customPanel1.Dock = DockStyle.Fill;
                        this.customPanel2.Visible = false;
                        this.customPanel3.Visible = false;
                        pbMethod1.Top = customPanel1.Height / 2 - pbMethod1.Height / 2;
                        panel17.Top = pbMethod1.Top;
                        break;
                    case "rb_2":
                        this.customPanel2.Visible = true;
                        this.customPanel2.Dock = DockStyle.Fill;
                        this.customPanel1.Visible = false;
                        this.customPanel3.Visible = false;
                        pbMethod2.Top = customPanel2.Height / 2 - pbMethod1.Height / 2;
                        panel16.Top = pbMethod2.Top;

                        break;
                    case "rb_3":
                        this.customPanel3.Visible = true;
                        this.customPanel3.Dock = DockStyle.Fill;
                        this.customPanel1.Visible = false;
                        this.customPanel2.Visible = false;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                rb.BackColor = Color.Transparent;
                rb.ForeColor = Color.Black;
            }


        }

        private void btnAddMthodItem_Click(object sender, EventArgs e)
        {
            //UC.MethodItem mi = new HR_Test.UC.MethodItem();
            int i = 1;
            foreach (DataGridViewRow dr in this.dbViewMethod.Rows)
            {
                i++;
            }
            string strMethod = string.Empty;
            if (string.IsNullOrEmpty(this._cmbControlType.Text))
                strMethod += "控制方式不能为空\r\n";
            if (string.IsNullOrEmpty(this._txtControlSpeed.Text))
            {
                strMethod += "控制速度不能为空\r\n";
            }
            if (this._txtControlSpeed.Text.Length > 0)
            {
                if (double.Parse(this._txtControlSpeed.Text.Trim()) == 0)
                    strMethod += "控制速度不能为0\r\n";
            }
            if (string.IsNullOrEmpty(this._cmbChangeType.Text))
                strMethod += "转换类型不能为空\r\n";
            if (string.IsNullOrEmpty(this._txtChangeValue.Text))
                strMethod += "转换值不能为空\r\n";

            //if (this._txtChangeValue.Text.Length > 0)
            //{
            //    if (double.Parse(this._txtChangeValue.Text.Trim()) == 0)
            //        strMethod += "转换值不能为0\r\n";
            //}

            if (!string.IsNullOrEmpty(strMethod))
            {
                MessageBox.Show(strMethod, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (i < 11)
            {
                DataGridViewRow dgvr = new DataGridViewRow();
                dgvr.CreateCells(this.dbViewMethod, new object[] { false, dbViewMethod.Rows.Count, _cmbControlType.SelectedIndex.ToString(), _cmbControlType.Text + "(" + lblDw.Text + ")", _txtControlSpeed.Text, _cmbChangeType.SelectedIndex.ToString(), _cmbChangeType.Text + "(" + lblDw2.Text + ")", _txtChangeValue.Text });
                dbViewMethod.Rows.Add(dgvr);
            }
            else
            {
                MessageBox.Show(this, "最多添加10个自定义试验段!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void tsbtnMinimize_Click(object sender, EventArgs e)
        {
            _fmMain.WindowState = FormWindowState.Minimized;
        }

        private void customTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pbMethod1.Top = customPanel1.Height / 2 - pbMethod1.Height / 2;
            panel17.Top = pbMethod1.Top;
        }

        private void gbAllow_Resize(object sender, EventArgs e)
        {
            //foreach (Control c in gbAllow.Controls)
            //{
            //    //c.Left = gbAllow.Width / 2 - c.Width / 2;
            //    //c.Top = gbAllow.Height / 2 - c.Height;
            //}
        }

        private void gbYs_Resize(object sender, EventArgs e)
        {
            //foreach (Control c in gbYs.Controls)
            //{
            //    //c.Left = gbAllow.Width / 2 - c.Width / 2;
            //    //c.Top = gbYs.Height / 2 - c.Height;
            //}
        }

        private void _cmbControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //位移控制
            //负荷控制
            //应力控制
            //ēLc控制
            //ēLe控制
            switch (_cmbControlType.SelectedIndex)
            {
                case 0:
                    this.lblDw.Text = "mm/min";
                    break;
                case 1:
                    this.lblDw.Text = "kN/s";
                    break;
                case 2:
                    this.lblDw.Text = "MPa/s";
                    break;
                case 3:
                case 4:
                    this.lblDw.Text = "/s";
                    break;
                case 5:
                    this.lblDw.Text = "mm/s";
                    break;
            }
            this.lblDw.Refresh();
        }

        private void _cmbChangeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //位移
            //负荷
            //变形
            //应力
            //应变
            switch (_cmbChangeType.SelectedIndex)
            {
                case 0:
                    this.lblDw2.Text = "mm";
                    break;
                case 1:
                    this.lblDw2.Text = "kN";
                    break;
                case 2:
                    this.lblDw2.Text = "mm";
                    break;
                case 3:
                    this.lblDw2.Text = "MPa";
                    break;
                case 4:
                    this.lblDw2.Text = "%";
                    break;
            }
            this.lblDw2.Refresh();
        }

        private void _txtControlSpeed_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (!utils.IsNumeric(tb.Text))
                tb.Text = "";
        }

        private void dbViewMethod_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                for (int i = 0; i < dbViewMethod.Rows.Count; i++)
                {
                    dbViewMethod.Rows[i].Cells[0].Value = false;
                }

                dbViewMethod.Rows[e.RowIndex].Cells[0].Value = true;

                this._cmbControlType.SelectedIndex = Convert.ToInt32(dbViewMethod.Rows[e.RowIndex].Cells[2].Value.ToString());
                this._txtControlSpeed.Text = dbViewMethod.Rows[e.RowIndex].Cells[4].Value.ToString();
                this._cmbChangeType.SelectedIndex = Convert.ToInt32(dbViewMethod.Rows[e.RowIndex].Cells[5].Value.ToString());
                this._txtChangeValue.Text = dbViewMethod.Rows[e.RowIndex].Cells[7].Value.ToString();
            }
        }

        private void btnDelUserMethod_Click(object sender, EventArgs e)
        {
            int chkNo = 0;
            int rowIndex = -1;
            for (int i = 0; i < dbViewMethod.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dbViewMethod.Rows[i].Cells[0].Value) == true)
                {
                    chkNo++;
                    rowIndex = i;
                }
            }

            if (chkNo > 1)
            {
                MessageBox.Show("请确认只选择了一项!", "删除提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (chkNo == 0)
            {
                MessageBox.Show("请选择一项!", "删除提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (chkNo == 1)
            {
                if (DialogResult.OK == MessageBox.Show("确定删除该项？", "删除警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    dbViewMethod.Rows.RemoveAt(rowIndex);
                    //修改序号
                    for (int i = 0; i < dbViewMethod.Rows.Count; i++)
                    {
                        dbViewMethod.Rows[i].Cells[1].Value = i.ToString();
                    }
                }
            }
        }

        private void btnChangeMethodItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dbViewMethod.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dbViewMethod.Rows[i].Cells[0].Value) == true)
                {
                    //dgvr.CreateCells(this.dbViewMethod, new object[] { false, dbViewMethod.Rows.Count, _cmbControlType.SelectedIndex.ToString(), _cmbControlType.Text + "(" + lblDw.Text + ")", _txtControlSpeed.Text, _cmbChangeType.SelectedIndex.ToString(), _cmbChangeType.Text + "(" + lblDw2.Text + ")", _txtChangeValue.Text });
                    dbViewMethod.Rows[i].Cells[2].Value = this._cmbControlType.SelectedIndex.ToString();
                    dbViewMethod.Rows[i].Cells[3].Value = _cmbControlType.Text + "(" + lblDw.Text + ")";
                    dbViewMethod.Rows[i].Cells[4].Value = _txtControlSpeed.Text;
                    dbViewMethod.Rows[i].Cells[5].Value = _cmbChangeType.SelectedIndex.ToString();
                    dbViewMethod.Rows[i].Cells[6].Value = _cmbChangeType.Text + "(" + lblDw2.Text + ")";
                    dbViewMethod.Rows[i].Cells[7].Value = _txtChangeValue.Text;
                    break;
                }
            }
        }

        private void panelAll_Click(object sender, EventArgs e)
        {
            Panel p = (Panel)sender;
            p.Focus();
        }

        private void chkLbResult_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void cmbTestStandard_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbTestStandard.SelectedItem != null)
            //{
            //    DataRowView dv = (DataRowView)this.cmbTestStandard.SelectedItem;                
            //    BLL.Standard blls = new HR_Test.BLL.Standard();
            //    DataSet ds = blls.GetList(" standardNo='" + dv["standardNo"].ToString() + "'");
            //    this.cmbTestType.DataSource = ds.Tables[0];
            //    this.cmbTestType.DisplayMember = "testType";
            //    this.cmbTestType.ValueMember = "ID";
            //    DataRowView drv =(DataRowView)this.cmbTestType.SelectedItem;
            //    this.lblStandardTitle.Text = drv["standardName"].ToString();
            //}

            if (this.cmbTestStandard.DataSource != null)
            {
                DataRowView drv = (DataRowView)this.cmbTestStandard.SelectedItem;
                //根据选择结果表的名称，读取选择结果表的项，添加至  checklistbox中
                string seltbName = drv["selTableName"].ToString();
                this.lblStandardTitle.Text = drv["standardName"].ToString();
                ReadResultSel(seltbName);
            }

        }

        private void ReadResultSel(string _selTableName)
        {
            this.chkResultList.Items.Clear();
            BLL.StandardResultItemInfo bllsrii = new HR_Test.BLL.StandardResultItemInfo();
            DataSet ds = null;
            switch (_selTableName)
            {
                case "tb_GBT236152009_SelZong":
                    ds = bllsrii.GetList(" standardNo='GB/T 23615.1-2009' and testType='纵向拉伸'");
                    break;
                case "tb_GBT236152009_SelHeng":
                    ds = bllsrii.GetList(" standardNo='GB/T 23615.1-2009' and testType='横向拉伸'");
                    break;
                case "tb_GBT282892012_TensileSel":
                    ds = bllsrii.GetList(" standardNo='GB/T 28289-2012' and testType='拉伸试验'");
                    break;
                case "tb_GBT282892012_ShearSel":
                    ds = bllsrii.GetList(" standardNo='GB/T 28289-2012' and testType='剪切试验'");
                    break;
                case "tb_GBT282892012_TwistSel":
                    ds = bllsrii.GetList(" standardNo='GB/T 28289-2012' and testType='扭转试验'");
                    break;
                case "tb_SelTestResult":
                    ds = bllsrii.GetList(" standardNo='GB/T 228-2010' and testType='拉伸试验'");
                    break;
                case "tb_SelTestResult_C":
                    ds = bllsrii.GetList(" standardNo='GB/T 7314-2005' and testType='压缩试验'");
                    break;
                case "tb_SelTestResult_B":
                    ds = bllsrii.GetList(" standardNo='YB/T 5349-2006' and testType='弯曲试验'");
                    break;
                case "tb_GBT3354_Sel":
                    ds = bllsrii.GetList(" standardNo='GB/T 3354-2014' and testType='拉伸试验'");
                    break;                    
            }
            if (ds != null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string paraname = ds.Tables[0].Rows[i]["paramName"].ToString();
                    if (paraname.Contains("△") || paraname.Contains("σ") || paraname.Contains("μ") || paraname.Contains("ε"))
                        this.chkResultList.Items.Add("            " + ds.Tables[0].Rows[i]["paramName"].ToString().PadRight(15) + ds.Tables[0].Rows[i]["unit"].ToString().PadRight(19) + ds.Tables[0].Rows[i]["paramDiscrible"].ToString());
                    else
                        this.chkResultList.Items.Add("            " + ds.Tables[0].Rows[i]["paramName"].ToString().PadRight(16) + ds.Tables[0].Rows[i]["unit"].ToString().PadRight(19) + ds.Tables[0].Rows[i]["paramDiscrible"].ToString());
                    this.chkResultList.SetItemChecked(i, Convert.ToBoolean(ds.Tables[0].Rows[i]["isCheck"].ToString()));
                }
            }
        }



        private void cmbTestType_TextChanged(object sender, EventArgs e)
        {
            //if (this.cmbTestType.DataSource != null)
            //{
            //    DataRowView drv = (DataRowView)this.cmbTestType.SelectedItem;
            //    //根据选择结果表的名称，读取选择结果表的项，添加至  checklistbox中
            //    string seltbName = drv["selTableName"].ToString(); 
            //    ReadResultSel(seltbName);
            //}
        }

        private void txtSpeed_zidingyi_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (!utils.IsNumeric(tb.Text))
                tb.Text = "200";
        }

        private void cmbTestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTestType.SelectedItem != null)
            {
                DataRowView dv = (DataRowView)this.cmbTestType.SelectedItem;
                BLL.Standard blls = new HR_Test.BLL.Standard();
                DataSet ds = blls.GetList(" testType='" + dv["testType"].ToString() + "'");
                this.cmbTestStandard.DataSource = ds.Tables[0];
                this.cmbTestStandard.DisplayMember = "standardNo";
                this.cmbTestStandard.ValueMember = "ID";
                DataRowView drv = (DataRowView)this.cmbTestStandard.SelectedItem;
                this.lblStandardTitle.Text = drv["standardName"].ToString();
            }
        }

        private void cmbTestStandard_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveStandard_Click(object sender, EventArgs e)
        {
            //检测空
            string msg = string.Empty;
            if (string.IsNullOrEmpty(this.cmbTestStandard.Text))
                msg += "标准号不能为空;\r\n";
            if (string.IsNullOrEmpty(this.lblStandardTitle.Text))
                msg += "标准名称不能为空:";

            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg);
                return;
            }

            if (this.cmbTestType.Text == "拉伸试验" || this.cmbTestType.Text == "压缩试验" || this.cmbTestType.Text == "弯曲试验")
            {
                BLL.Standard blls = new HR_Test.BLL.Standard();
                Model.Standard mods = new HR_Test.Model.Standard();

                if (blls.GetList(" testType='" + this.cmbTestType.Text + "' and standardName='" + this.lblStandardTitle.Text + "'").Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("已经存在该标准名称,保存失败!");
                    return;
                }
                else
                {
                    switch (this.cmbTestType.Text)
                    {
                        case "拉伸试验":
                            mods.standardNo = this.cmbTestStandard.Text;
                            mods.standardName = this.lblStandardTitle.Text;
                            mods.testType = this.cmbTestType.Text;
                            mods.resultTableName = "tb_TestSample";
                            mods.selTableName = "tb_SelTestResult";
                            mods.methodTableName = "tb_ControlMethod";
                            mods.sign = "-";
                            if (blls.Add(mods))
                                TestStandard.SampleControl.ReadTestType(this.cmbTestType);
                            break;
                        case "压缩试验":
                            mods.standardNo = this.cmbTestStandard.Text;
                            mods.standardName = this.lblStandardTitle.Text;
                            mods.testType = this.cmbTestType.Text;
                            mods.resultTableName = "tb_Compress";
                            mods.selTableName = "tb_SelTestResult_C";
                            mods.methodTableName = "tb_ControlMethod_C";
                            mods.sign = "-";
                            if (blls.Add(mods))
                                TestStandard.SampleControl.ReadTestType(this.cmbTestType);
                            break;
                        case "弯曲试验":
                            mods.standardNo = this.cmbTestStandard.Text;
                            mods.standardName = this.lblStandardTitle.Text;
                            mods.testType = this.cmbTestType.Text;
                            mods.resultTableName = "tb_Bend";
                            mods.selTableName = "tb_SelTestResult_B";
                            mods.methodTableName = "tb_ControlMethod_B";
                            mods.sign = "-";
                            if (blls.Add(mods))
                                TestStandard.SampleControl.ReadTestType(this.cmbTestType);
                            break;
                    }
                }

                UnEnableAdd();
            }
            else
            {
                MessageBox.Show("请确保试验类型为 拉伸试验、压缩试验 或 弯曲试验!");
                return;
            }
        }

        private void btnAddCustom_Click(object sender, EventArgs e)
        {
            EnableAdd();
        }

        private void EnableAdd()
        {
            this.cmbTestStandard.DropDownStyle = ComboBoxStyle.DropDown;
            this.lblStandardTitle.Enabled = true;
            this.btnSaveStandard.Enabled = true;
            this.btnAddCustom.Enabled = false;
            this.panel4.Enabled = false;
            this.tvTestMethod.Enabled = false;
        }

        private void UnEnableAdd()
        {
            this.btnAddCustom.Enabled = true;
            this.btnSaveStandard.Enabled = false;
            this.cmbTestStandard.DropDownStyle = ComboBoxStyle.DropDownList;
            this.lblStandardTitle.Enabled = false;
            this.panel4.Enabled = true;
            this.tvTestMethod.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UnEnableAdd();
        }

        private void customPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
