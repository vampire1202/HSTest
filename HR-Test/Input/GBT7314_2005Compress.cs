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
    public partial class GBT7314_2005Compress : UserControl
    {


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

        public GBT7314_2005Compress()
        {
            InitializeComponent();
        }

        public void ReadMethodInfo(string _methodName)
        {
            BLL.ControlMethod_C bllts_C = new HR_Test.BLL.ControlMethod_C();
            Model.ControlMethod_C modelc = bllts_C.GetModel(_methodName);
            if(modelc!=null)
            {
                 c_testNo.Text = "";
                c_testNum.Text = "";
                 _sendCompany = modelc.sendCompany;
                _condition = modelc.condition;
                _controlMode = modelc.controlmode;
                c_stuffCardNo.Text = modelc.stuffCardNo;
                c_stuffSpec.Text = modelc.stuffSpec;
                c_stuffType.Text = modelc.stuffType;
                _hotStatus = modelc.hotStatus;
                _temperature = modelc.temperature.ToString();
                _humidity = modelc.humidity.ToString();
                _sampleCharacter = modelc.sampleCharacter;
                _testStandard_C.Text = modelc.testStandard;
                c_sign.Text = modelc.sign;
                c_L.Text = modelc.L.ToString();
                c_L0.Text = modelc.L0.ToString();
                c_HH.Text = modelc.H.ToString();
                c_h.Text = modelc.hh.ToString();
                c_getSample.Text = modelc.getSample;
                txtn.Text = modelc.a.ToString();
                Epc.Text = modelc.b.ToString();
                Etc.Text = modelc.d.ToString();
                c_S0.Text = "";
                c_Ff.Text = modelc.Ff.ToString();
                c_mathineType.Text = modelc.mathineType;
                c_tester.Text = modelc.tester;
                c_testMethod.Text = modelc.testMethod;
            }
        }

        private void btnMore3_Click(object sender, EventArgs e)
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

        private void gBtnAddC_Click(object sender, EventArgs e)
        {
            string strErr = "";

            if (this.c_testNo.Text.Trim().Length == 0)
            {
                strErr += "试验编号不能为空！\r\n\r\n";
            }
            if (this.c_testSampleNo.Text.Trim().Length == 0)
            {
                strErr += "试样编号不能为空！\r\n\r\n";
            }

            if (this._sendCompany.Length == 0)
            {
                strErr += "送检单位不能为空！\r\n\r\n";
            }
            if (this.c_stuffCardNo.Text.Trim().Length == 0)
            {
                strErr += "材料牌号不能为空！\r\n\r\n";
            }
            if (this.c_stuffSpec.Text.Trim().Length == 0)
            {
                strErr += "材料规格不能为空！\r\n\r\n";
            }
            if (this.c_stuffType.Text.Trim().Length == 0)
            {
                strErr += "试样类型不能为空！\r\n\r\n";
            }

            if (this._temperature.Trim().Length == 0)
            {
                strErr += "试验温度不能为空！\r\n\r\n";
            }

            if (this._condition.Trim().Length == 0)
            {
                strErr += "试验条件不能为空！\r\n\r\n";
            }

            if (this._controlMode.Trim().Length == 0)
            {
                strErr += "控制方式不能为空！\r\n\r\n";
            }

            if (this._testStandard_C.Text.Trim().Length == 0)
            {
                strErr += "试验标准不能为空！\r\n\r\n";
            }

            if (this._sampleCharacter.Trim().Length == 0)
            {
                strErr += "试样标识不能为空！\r\n\r\n";
            }
            if (this.c_getSample.Text.Trim().Length == 0)
            {
                strErr += "试样取样不能为空！\r\n\r\n";
            }
            if (this.c_tester.Text.Trim().Length == 0)
            {
                strErr += "试验员不能为空！\r\n\r\n";
            }

            if (rBtnRect.Checked)
            {
                if (this.c_a.Text.Trim().Length == 0)
                {
                    strErr += "a不能为空！\r\n\r\n";
                }
                if (this.c_b.Text.Trim().Length == 0)
                {
                    strErr += "b不能为空！\r\n\r\n";
                }
            }

            if (rBtnCircle.Checked)
            {
                if (this.c_d.Text.Trim().Length == 0)
                {
                    strErr += "d不能为空！\r\n\r\n";
                }
            }

            if (this.c_S0.Text.Trim().Length == 0)
            {
                strErr += "S0不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this.c_S0.Text.Trim()) == 0d)
                {
                    strErr += "S0不能为0！\r\n\r\n";
                }
            }

            if (this.c_L0.Text.Trim().Length == 0)
            {
                strErr += " L0不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this.c_L0.Text.Trim()) == 0d)
                {
                    strErr += " L0不能为0！\r\n\r\n";
                }
            }

            if (this.Epc.Text.Trim().Length == 0)
            {
                strErr += "εpc 不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this.Epc.Text.Trim()) == 0d)
                {
                    strErr += " εpc 不能为0！\r\n\r\n";
                }
            }

            if (this.Etc.Text.Trim().Length == 0)
            {
                strErr += "εtc 不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this.Etc.Text.Trim()) == 0d)
                {
                    strErr += " εtc 不能为0！\r\n\r\n";
                }
            }

            if (this.txtn.Text.Trim().Length == 0)
            {
                strErr += "变形放大倍数 n 不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this.txtn.Text.Trim()) == 0d)
                {
                    strErr += " 变形放大倍数 n 不能为0！\r\n\r\n";
                }
            }

            if (this.c_L.Text.Trim().Length == 0)
            {
                strErr += " L不能为空！\r\n\r\n";
            }


            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            HR_Test.Model.Compress model = new HR_Test.Model.Compress();
            model.testMethodName = this.c_testMethod.Text;
            model.testNo = this.c_testNo.Text;
            model.testSampleNo = this.c_testSampleNo.Text;
            model.reportNo = "-";

            model.sendCompany = this._sampleCharacter;
            model.temperature = double.Parse(this._temperature);
            model.humidity = double.Parse(this._humidity);
            model.testStandard = this._testStandard_C.Text;
            model.sampleCharacter = this._sampleCharacter;
            model.hotStatus = this._hotStatus;
            model.condition = this._condition;
            model.controlMode = this._controlMode;


            model.stuffCardNo = this.c_stuffCardNo.Text;
            model.stuffSpec = c_stuffSpec.Text;
            model.stuffType = c_stuffType.Text;
            model.testMethod = c_testMethod.Text;
            model.mathineType = c_mathineType.Text;
            model.testCondition = "-";

            model.getSample = c_getSample.Text;
            model.tester = c_tester.Text;
            model.assessor = "-";
            model.sign = this.c_sign.Text;

            model.a = Convert.ToDouble(!string.IsNullOrEmpty(this.c_a.Text) ? this.c_a.Text : "0");
            model.b = Convert.ToDouble(!string.IsNullOrEmpty(this.c_b.Text) ? this.c_b.Text : "0");
            model.d = Convert.ToDouble(!string.IsNullOrEmpty(this.c_d.Text) ? this.c_d.Text : "0");
            model.L = Convert.ToDouble(!string.IsNullOrEmpty(this.c_L.Text) ? this.c_L.Text : "0");
            model.L0 = Convert.ToDouble(!string.IsNullOrEmpty(this.c_L0.Text) ? this.c_L0.Text : "0");
            model.H = Convert.ToDouble(!string.IsNullOrEmpty(this.c_HH.Text) ? this.c_HH.Text : "0");
            model.hh = Convert.ToDouble(!string.IsNullOrEmpty(this.c_h.Text) ? this.c_h.Text : "0");
            model.S0 = Convert.ToDouble(!string.IsNullOrEmpty(this.c_S0.Text) ? this.c_S0.Text : "0");
            model.Ff = Convert.ToDouble(!string.IsNullOrEmpty(this.c_Ff.Text) ? this.c_Ff.Text : "0");
            model.εpc = Convert.ToDouble(!string.IsNullOrEmpty(this.Epc.Text) ? this.Epc.Text : "0");
            model.εtc = Convert.ToDouble(!string.IsNullOrEmpty(this.Etc.Text) ? this.Etc.Text : "0");
            model.n = Convert.ToDouble(!string.IsNullOrEmpty(this.txtn.Text) ? this.txtn.Text : "1"); 
            model.deltaL = 0;        
            model.F0 = 0;
            model.Fpc = 0;
            model.Ftc = 0;
            model.FeHc = 0;
            model.FeLc = 0;
            model.Fmc = 0;
            model.Rpc = 0;
            model.Rtc = 0;
            model.ReHc = 0;
            model.ReLc = 0;
            model.Rmc = 0;
            model.Ec = 0;
            model.Avera = 0;
            model.Avera1 = 0;
            model.isFinish = false;
            model.isEffective = false;
            model.testDate = this.c_testDate.Value.Date;
            BLL.Compress bllc = new HR_Test.BLL.Compress();
            if (bllc.GetList("testSampleNo ='" + this.c_testSampleNo.Text + "'").Tables[0].Rows.Count == 0)
            {
                if (bllc.Add(model))
                {
                    this.c_testSampleNo.Items.Remove(this.c_testSampleNo.SelectedItem);
                    //this.c_a.Text = "";
                    //this.c_b.Text = "";
                    //this.c_d.Text = "";
                    //this.c_S0.Text = "";
                    MessageBox.Show("添加压缩试样信息成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("添加压缩试样信息失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("已经存在相同的编号，请重新设置!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //TestStandard.MethodControl.ReadMethod(this.treeviewTestMethod);
        }

        private void gBtnDelC_Click(object sender, EventArgs e)
        {
            BLL.Bend bllBend = new HR_Test.BLL.Bend();
            bllBend.Delete(_methodName);
            //TestStandard.MethodControl.ReadMethod(this.treeviewTestMethod);
        }

        private void gBtnAddToMethod2_Click(object sender, EventArgs e)
        {
            string strErr = "";

            if (this._sendCompany.Trim().Length == 0)
            {
                strErr += "送检单位不能为空！\r\n\r\n";
            }
            if (this.c_stuffCardNo.Text.Trim().Length == 0)
            {
                strErr += "材料牌号不能为空！\r\n\r\n";
            }
            if (this.c_stuffSpec.Text.Trim().Length == 0)
            {
                strErr += "材料规格不能为空！\r\n\r\n";
            }
            if (this.c_stuffType.Text.Trim().Length == 0)
            {
                strErr += "试样类型不能为空！\r\n\r\n";
            }

            if (this._temperature.Trim().Length == 0)
            {
                strErr += "试验温度不能为空！\r\n\r\n";
            }

            if (this.c_testMethod.Text.Trim().Length == 0)
            {
                strErr += "试验方法不能为空！\r\n\r\n";
            }

            if (this._sampleCharacter.Trim().Length == 0)
            {
                strErr += "试样标识不能为空！\r\n\r\n";
            }

            if (this._condition.Trim().Length == 0)
            {
                strErr += "试验条件不能为空！\r\n\r\n";

            }

            if (this._controlMode.Trim().Length == 0)
            {
                strErr += "控制方式不能为空！\r\n\r\n";
            }


            if (this.c_getSample.Text.Trim().Length == 0)
            {
                strErr += "试样取样不能为空！\r\n\r\n";
            }

            if (this.c_L0.Text.Trim().Length == 0)
            {
                strErr += "'L0'不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this.c_L0.Text.Trim()) == 0d)
                {
                    strErr += "'L0'不能为0！\r\n\r\n";
                }
            }


            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            BLL.ControlMethod_C bllcm_C = new HR_Test.BLL.ControlMethod_C();
            HR_Test.Model.ControlMethod_C model = bllcm_C.GetModel(this.c_testMethod.Text);

            model.sendCompany = this._sendCompany;
            model.stuffCardNo = this.c_stuffCardNo.Text;
            model.stuffSpec = c_stuffSpec.Text;
            model.stuffType = c_stuffType.Text;
            model.hotStatus = this._hotStatus;
            model.temperature = double.Parse(this._temperature);
            model.humidity = double.Parse(this._humidity);
            model.testStandard = this._testStandard_C.Text;
            model.testMethod = c_testMethod.Text;
            model.mathineType = c_mathineType.Text;
            model.testCondition = "-";
            model.sampleCharacter = this._sampleCharacter;
            model.getSample = c_getSample.Text;
            model.tester = c_tester.Text;
            model.condition = this._condition;
            model.controlmode = this._controlMode;
            model.assessor = "-";
            model.sign = this.c_sign.Text;

            //model.a = (this.c_a.Text.Trim().Length > 0 ? double.Parse(c_a.Text) : 0);
            //model.b = (this.c_b.Text.Trim().Length > 0 ? double.Parse(c_b.Text) : 0);
            //model.d = (this.c_d.Text.Trim().Length > 0 ? double.Parse(c_d.Text) : 0);  

            //修改为放大倍数
            model.a = (this.txtn.Text.Trim().Length > 0 ? double.Parse(txtn.Text) : 0);  
            //修改为 Epc
            model.b = (this.Epc.Text.Trim().Length > 0 ? double.Parse(Epc.Text) : 0); 
            //修改为 Etc
            model.d = (this.Etc.Text.Trim().Length > 0 ? double.Parse(Etc.Text) : 0); 

            model.L = (this.c_L.Text.Trim().Length > 0 ? double.Parse(c_L.Text) : 0);
            model.L0 = (this.c_L0.Text.Trim().Length > 0 ? double.Parse(c_L0.Text) : 0);
            model.H = (this.c_HH.Text.Trim().Length > 0 ? double.Parse(c_HH.Text) : 0);
            model.hh = (this.c_h.Text.Trim().Length > 0 ? double.Parse(c_h.Text) : 0);
            //model.S0 = double.Parse(c_S0.Text);
            model.Ff = (this.c_Ff.Text.Trim().Length > 0 ? double.Parse(c_Ff.Text) : 0);

            if (bllcm_C.Update(model))
            {
                MessageBox.Show(this, "更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
        }

        private void rBtnRect_CheckedChanged(object sender, EventArgs e)
        {
            if (rBtnRect.Checked)
            {
                this.c_a.Enabled = true;
                this.c_b.Enabled = true;
                this.c_d.Enabled = false;
                this.c_d.Text = "0";
            }
        }

        private void rBtnCircle_CheckedChanged(object sender, EventArgs e)
        {
            if (rBtnCircle.Checked)
            {
                this.c_a.Enabled = false;
                this.c_b.Enabled = false;
                this.c_d.Enabled = true;
                this.c_a.Text = this.c_b.Text = "0";
            }
        }

        private void GBT7314_2005Compress_Load(object sender, EventArgs e)
        {
            rBtnRect_CheckedChanged(sender, e);
        }

        private void c_testNo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.c_testNo.Text))
            {
                if (utils.IsNumeric(this.c_testNum.Text) && !string.IsNullOrEmpty(this.c_testNum.Text))
                {
                    this.c_testSampleNo.Items.Clear();
                    int num = int.Parse(this.c_testNum.Text);
                    for (int i = 0; i < num; i++)
                    {
                        this.c_testSampleNo.Items.Add(this.c_testNo.Text + "-" + (i + 1).ToString("00"));
                    }
                }
                else
                {
                    this.c_testNum.Text = "";
                }
            }
        }

        private void c_testNum_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.c_testNum.Text))
            {
                if (utils.IsNumeric(this.c_testNum.Text) && !string.IsNullOrEmpty(this.c_testNum.Text))
                {
                    this.c_testSampleNo.Items.Clear();
                    int num = int.Parse(this.c_testNum.Text);
                    for (int i = 0; i < num; i++)
                    {
                        this.c_testSampleNo.Items.Add(this.c_testNo.Text + "-" + (i + 1).ToString("00"));
                    }
                }
                else
                {
                    this.c_testNum.Text = "";
                }
            }
        }

        private void c_L0_TextChanged(object sender, EventArgs e)
        {
            TextBox uf = (TextBox)sender;
            if (!utils.IsNumeric(uf.Text))
                uf.Text = "";
        }

        private void gbtnCalculate_C_Click(object sender, EventArgs e)
        {
            //矩形试样
            if (rBtnRect.Checked)
            {
                if (c_a.Text.Trim().Length > 0 && c_b.Text.Trim().Length > 0)
                {
                    this.c_S0.Text = (double.Parse(c_a.Text.Trim()) * double.Parse(c_b.Text.Trim())).ToString("0.0000");
                }
            }

            //圆形试样
            if (this.rBtnCircle.Checked)
            {
                if (c_d.Text.Trim().Length > 0)
                    this.c_S0.Text = (double.Parse(this.c_d.Text) * double.Parse(this.c_d.Text) * Math.PI / 4).ToString("0.0000");
            } 
        }
    }
}
