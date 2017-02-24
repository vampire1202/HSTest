using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HR_Test.Input
{
    /// <summary>
    /// 金属室温拉伸试验标准 GB/T 228-2010 
    /// </summary>
    public partial class GBT28289_2012Twist : UserControl
    {

        //拉伸试验其他参数
        public string _sendCompany;
        public string _temperature;
        public string _sampleCharacter;
        public string _hotStatus;
        public string _humidity; 
        public string _condition;
        public string _controlMode; 

        private TreeView treeviewTestMethod;
        public TreeView TreeviewTestMethod
        {
            get { return treeviewTestMethod; }
            set { treeviewTestMethod = value; }
        }


        private string _methodName;
        public string _MethodName
        {
            get { return _methodName; }
            set { _methodName = value; }
        }

        public GBT28289_2012Twist()
        {
            InitializeComponent();
        }

        private void gBtnAddTestSample_Click(object sender, EventArgs e)
        {
            string strErr = "";

            if (this._testNo.Text.Trim().Length == 0)
            {
                strErr += "'试验编号'不能为空！\r\n\r\n";
            }
            if (this._testSampleNo.Text.Trim().Length == 0)
            {
                strErr += "'试样编号'不能为空！\r\n\r\n";
            }

            if (string.IsNullOrEmpty(this._sendCompany))
            {
                strErr += "'送检单位'不能为空！\r\n\r\n";
            }
            if (this._stuffCardNo.Text.Trim().Length == 0)
            {
                strErr += "'材料牌号'不能为空！\r\n\r\n";
            }
            if (this._stuffSpec.Text.Trim().Length == 0)
            {
                strErr += "'材料规格'不能为空！\r\n\r\n";
            }
            if (this._stuffType.Text.Trim().Length == 0)
            {
                strErr += "'试样类型'不能为空！\r\n\r\n";
            }

            if (string.IsNullOrEmpty(this._temperature))
            {
                strErr += "'试验温度'不能为空！\r\n\r\n";
            }

            if (string.IsNullOrEmpty(this._condition))
            {
                strErr += "'试验条件'不能为空！\r\n\r\n";
            }

            if (string.IsNullOrEmpty(this._controlMode))
            {
                strErr += "'控制方式'不能为空！\r\n\r\n";
            }

            if (this._testMethod.Text.Trim().Length == 0)
            {
                strErr += "'试验方法'不能为空！\r\n\r\n";
            }

            if (string.IsNullOrEmpty(this._sampleCharacter))
            {
                strErr += "'试样标识'不能为空！\r\n\r\n";
            }
            if (this._getSample.Text.Trim().Length == 0)
            {
                strErr += "'试样取样'不能为空！\r\n\r\n";
            }
            if (this._tester.Text.Trim().Length == 0)
            {
                strErr += "'试验员'不能为空！\r\n\r\n";
            }

            if (this._L.Text.Trim().Length == 0)
            {
                strErr += "'L'不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this._L.Text.Trim()) == 0d)
                {
                    strErr += "'L'不能为0！\r\n\r\n";
                }
            }

            if (this._L1.Text.Trim().Length == 0)
            {
                strErr += "'L1'不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this._L1.Text.Trim()) == 0d)
                {
                    strErr += "'L1'不能为0！\r\n\r\n";
                }
            }

            if (this._L2.Text.Trim().Length == 0)
            {
                strErr += "'L2'不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this._L2.Text.Trim()) == 0d)
                {
                    strErr += "'L2'不能为0！\r\n\r\n";
                }
            }
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            string testNo = this._testNo.Text;
            string testSampleNo = this._testSampleNo.Text; 
            string sendCompany = this._sendCompany;
            string stuffCardNo = this._stuffCardNo.Text;
            string stuffSpec = this._stuffSpec.Text;
            string stuffType = this._stuffType.Text;
            string hotStatus = this._hotStatus;
            string temperature = this._temperature;
            string humidity = this._humidity;
            string testStandard = this._testStandard.Text;
            string testMethod = this._testMethod.Text;
            string mathineType = this._mathineType.Text; 
            string sampleCharacter = this._sampleCharacter;
            string getSample = this._getSample.Text;
            string tester = this._tester.Text;

            Model.GBT282892012_Twist model = new HR_Test.Model.GBT282892012_Twist(); 
            model.testMethodName = this._testMethod.Text; 
            model.testNo = testNo;
            model.testSampleNo = testSampleNo;
            model.reportNo = "-";
            model.sendCompany = sendCompany;
            model.stuffCardNo = stuffCardNo;
            model.stuffSpec = stuffSpec;
            model.stuffType = stuffType;
            model.hotStatus = hotStatus;
            model.temperature = double.Parse(this._temperature);
            model.humidity = 0;
            model.testStandard = testStandard;
            model.testMethod = testMethod;
            model.mathineType = mathineType;
            model.testCondition = "-";
            model.sampleCharacter = sampleCharacter;
            model.getSample = getSample;
            model.tester = tester;
            model.assessor = "-";
            model.L = Convert.ToDouble(this._L.Text.Trim());
            model.L1 = Convert.ToDouble(this._L1.Text.Trim());
            model.L2 = Convert.ToDouble(this._L2.Text.Trim());
            model.L0 =  model.L1 / 2 + model.L2;//力矩
            model.S0 = 0;
            model.FMmax = 0;
            model.M = 0;
            model.sign = this._sign.Text;
            model.isEffective = false;
            model.isFinish = false;
            //是否使用引申计根据试验方法确定
            model.isUseExtensometer = false; 
            model.testDate = this._testDate.Value.Date;
            model.condition = this._condition;
            model.controlmode = this._controlMode;
            BLL.GBT282892012_Twist bll = new BLL.GBT282892012_Twist();
            if (bll.GetList("testSampleNo ='" + this._testSampleNo.Text + "'").Tables[0].Rows.Count == 0)
            {
                bll.Add(model);
                this._testSampleNo.Items.Remove(this._testSampleNo.SelectedItem);
                MessageBox.Show(this, "添加试样信息成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, "已经存在相同的编号，请重新设置!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }            
        }

        private void gBtnDelete_Click(object sender, EventArgs e)
        {
            if (_methodName != null)
            {
                if (DialogResult.OK == MessageBox.Show("是否删除该试验组?", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    BLL.GBT282892012_Twist bllts = new HR_Test.BLL.GBT282892012_Twist();
                    bllts.Delete(_methodName); 
                }
            }
        }

        private void btnMore1_Click(object sender, EventArgs e)
        {
            frmOtherParam fto = new frmOtherParam();
            fto._SendCompany = this._sendCompany;
            fto._Temperature = this._temperature;
            fto._SampleCharacter = this._sampleCharacter;
            fto._HotStatus = this._hotStatus;
            fto._Humidity = this._humidity;
            fto._Condition = this._condition;
            fto._Controlmode = this._controlMode;
            if (DialogResult.OK == fto.ShowDialog())
            {
                this._sendCompany = fto._SendCompany;
                this._temperature = fto._Temperature;
                this._sampleCharacter = fto._SampleCharacter;
                this._hotStatus = fto._HotStatus;
                this._humidity = fto._Humidity;
                this._condition = fto._Condition;
                this._controlMode = fto._Controlmode;
            }
        }

        public void ReadMethodInfo(string methodName)
        {
            BLL.GBT282892012_Method bllts = new HR_Test.BLL.GBT282892012_Method();
            DataSet ds = bllts.GetList("MethodName ='" + methodName + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                _sendCompany = ds.Tables[0].Rows[0]["sendCompany"].ToString();
                _stuffCardNo.Text = ds.Tables[0].Rows[0]["stuffCardNo"].ToString();
                _stuffSpec.Text = ds.Tables[0].Rows[0]["stuffSpec"].ToString();
                _stuffType.Text = ds.Tables[0].Rows[0]["stuffType"].ToString();
                _hotStatus = ds.Tables[0].Rows[0]["hotStatus"].ToString();
                _temperature = ds.Tables[0].Rows[0]["temperature"].ToString();
                _humidity = ds.Tables[0].Rows[0]["humidity"].ToString();
                _sampleCharacter = ds.Tables[0].Rows[0]["sampleCharacter"].ToString();
                _testStandard.Text = ds.Tables[0].Rows[0]["testStandard"].ToString();
                _sign.Text = ds.Tables[0].Rows[0]["sign"].ToString(); 
                _getSample.Text = ds.Tables[0].Rows[0]["getSample"].ToString();
                _mathineType.Text = ds.Tables[0].Rows[0]["mathineType"].ToString();
                _tester.Text = ds.Tables[0].Rows[0]["tester"].ToString();
                _testMethod.Text = ds.Tables[0].Rows[0]["testMethod"].ToString();
                _condition = ds.Tables[0].Rows[0]["condition"].ToString();
                _controlMode = ds.Tables[0].Rows[0]["controlmode"].ToString();
            }
            ds.Dispose();
        }

        private void GBT28289_2012Tensile_Load(object sender, EventArgs e)
        {
            if (_methodName != null)
            {
                ReadMethodInfo(_methodName);
            }
        }

        private void _testNo_TextChanged(object sender, EventArgs e)
        {
            this._testSampleNo.Items.Clear();
            this._testSampleNo.Text = "";
            if (!string.IsNullOrEmpty(this._testNo.Text))
            {
                if (utils.IsNumeric(this._TestNum.Text) && !string.IsNullOrEmpty(this._TestNum.Text))
                {
                    int num = int.Parse(this._TestNum.Text);
                    for (int i = 0; i < num; i++)
                    {
                        this._testSampleNo.Items.Add(this._testNo.Text + "-" + (i + 1).ToString("00"));
                    }
                }
                else
                {
                    this._TestNum.Text = "";
                }
            }
        }

        private void _TestNum_TextChanged(object sender, EventArgs e)
        {
            this._testSampleNo.Items.Clear();
            this._testSampleNo.Text = "";
            if (!string.IsNullOrEmpty(this._TestNum.Text))
            {
                if (utils.IsNumeric(this._TestNum.Text) && !string.IsNullOrEmpty(this._TestNum.Text))
                {
                    int num = int.Parse(this._TestNum.Text);
                    for (int i = 0; i < num; i++)
                    {
                        this._testSampleNo.Items.Add(this._testNo.Text + "-" + (i + 1).ToString("00"));
                    }
                }
                else
                {
                    this._TestNum.Text = "";
                }
            }
        }

      
        private void _a0_TextChanged(object sender, EventArgs e)
        {
             TextBox uf = (TextBox)sender;
             if (!utils.IsNumeric(uf.Text))
                 uf.Text = "";
        }

        private void gBtnAddToMethod1_Click(object sender, EventArgs e)
        {
            string strErr = "";

            if (string.IsNullOrEmpty(this._sendCompany))
            {
                strErr += "送检单位不能为空！\r\n\r\n";
            }
            if (this._stuffCardNo.Text.Trim().Length == 0)
            {
                strErr += "材料牌号不能为空！\r\n\r\n";
            }
            if (this._stuffSpec.Text.Trim().Length == 0)
            {
                strErr += "材料规格不能为空！\r\n\r\n";
            }
            if (this._stuffType.Text.Trim().Length == 0)
            {
                strErr += "试样类型不能为空！\r\n\r\n";
            }

            if (string.IsNullOrEmpty(this._temperature))
            {
                strErr += "试验温度不能为空！\r\n\r\n";
            }

            if (this._testMethod.Text.Trim().Length == 0)
            {
                strErr += "试验方法不能为空！\r\n\r\n";
            }

            if (string.IsNullOrEmpty(this._condition))
            {
                strErr += "试验条件不能为空！\r\n\r\n";
            }

            if (string.IsNullOrEmpty(this._controlMode))
            {
                strErr += "控制方式不能为空！\r\n\r\n";
            }

            if (string.IsNullOrEmpty(this._sampleCharacter))
            {
                strErr += "试样标识不能为空！\r\n\r\n";
            }
            if (this._getSample.Text.Trim().Length == 0)
            {
                strErr += "试样取样不能为空！\r\n\r\n";
            }
            if (this._tester.Text.Trim().Length == 0)
            {
                strErr += "试验员不能为空！\r\n\r\n";
            } 

         
            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            string testNo = this._testNo.Text;
            string testSampleNo = this._testSampleNo.Text; 
            string sendCompany = this._sendCompany;
            string stuffCardNo = this._stuffCardNo.Text;
            string stuffSpec = this._stuffSpec.Text;
            string stuffType = this._stuffType.Text;
            string hotStatus = this._hotStatus;
            string temperature = this._temperature;
            string humidity = this._humidity;
            string testStandard = this._testStandard.Text;
            string testMethod = this._testMethod.Text;
            string mathineType = this._mathineType.Text; 
            string sampleCharacter = this._sampleCharacter;
            string getSample = this._getSample.Text;
            string tester = this._tester.Text;

            BLL.GBT282892012_Method bllcm = new HR_Test.BLL.GBT282892012_Method();
            Model.GBT282892012_Method model = bllcm.GetModel(this._testMethod.Text); 

            model.sendCompany = sendCompany;
            model.stuffCardNo = stuffCardNo;
            model.stuffSpec = stuffSpec;
            model.stuffType = stuffType;
            model.hotStatus = hotStatus;
            model.temperature = double.Parse(this._temperature);
            model.humidity = double.Parse(this._humidity);
            model.testStandard = testStandard;
            model.testMethod = testMethod;
            model.mathineType = mathineType;
            model.testCondition = "-";
            model.sampleCharacter = sampleCharacter;
            model.getSample = getSample;
            model.tester = tester;
            model.condition = this._condition;
            model.controlmode = this._controlMode;
            model.assessor = "-";
            model.sign = this._sign.Text;  

            if (bllcm.Update(model))
            {
                MessageBox.Show(this, "更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
        }
    }
}
