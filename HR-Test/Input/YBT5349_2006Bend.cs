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
    public partial class YBT5349_2006Bend : UserControl
    {
        /// <summary>
        /// 金属弯曲试验方法 YB/T 5349-2006
        /// </summary>
        /// 
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

        public string _sendCompany;
        public string _temperature;
        public string _sampleCharacter;
        public string _hotStatus;
        public string _humidity;
        public string _condition;
        public string _controlMode;
        public double _b_Ds;
        public double _b_Da;
        public double _b_R;
        public int _b_m;
        public double _b_a;
        public double _b_n;


        public YBT5349_2006Bend()
        {
            InitializeComponent();
        }

        private void YBT5349_2006Bend_Load(object sender, EventArgs e)
        {
            rbtnB1_CheckedChanged(sender, e);
            if (!string.IsNullOrEmpty(_methodName))
            {
                ReadMethodInfo(_methodName);
            }
        }

        public void ReadMethodInfo(string _methodName)
        {
            BLL.ControlMethod_B bllts_B = new HR_Test.BLL.ControlMethod_B();
            Model.ControlMethod_B mb = bllts_B.GetModel(_methodName);
            if (mb != null)
            {
                _sendCompany = mb.sendCompany;
                _hotStatus = mb.hotStatus;
                _condition = mb.condition;
                _controlMode = mb.controlmode;
                _temperature = mb.temperature.ToString();
                _humidity = mb.humidity.ToString();
                _sampleCharacter = mb.sampleCharacter;

                this._b_a = (double)mb.a;
                this._b_Da = (double)mb.Da;
                this._b_Ds = (double)mb.Ds;
                this._b_m = (int)mb.m;
                this._b_n = (double)mb.n;
                this._b_R = (double)mb.R;

                this.b_stuffCardNo.Text = mb.stuffCardNo;
                this.b_stuffSpec.Text = mb.stuffSpec;
                this.b_stuffType.Text = mb.stuffType;
                this._testStandard_B.Text = mb.testStandard;
                this.b_sign.Text = mb.sign;
                this.b_getSample.Text = mb.getSample;
                this.b_mathineType.Text = mb.mathineType;
                this.b_tester.Text = mb.tester;
                this.b_testMethod.Text = mb.testMethod;
                this.cmbBendType.Text = mb.testType;
                this.b_Ls.Text = mb.Ls.ToString();
                this.b_ll.Text = mb.l_l.ToString();
                this.b_Le.Text = mb.Le.ToString();
                this.b_εpb.Text = mb.εpb.ToString();
                this.b_εrb.Text = mb.εrb.ToString();

            }
        }

        private void btnBendOther1_Click(object sender, EventArgs e)
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

        private void btnBendOther2_Click(object sender, EventArgs e)
        {
            frmBendOther2 fbo2 = new frmBendOther2(this);
            if (DialogResult.OK == fbo2.ShowDialog())
            {
                _b_Ds = fbo2._B_Ds;
                _b_Da = fbo2._B_Da;
                _b_R = fbo2._B_R;
                _b_m = fbo2._B_m;
                _b_a = fbo2._B_a;
            }
        }

        private void rbtnB1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnB1.Checked)
            {
                this.b_b.Enabled = true;
                this.b_h.Enabled = true;
                this.b_L.Enabled = true;
                this.b_d.Enabled = false;
                this.b_d.Text = "0";
            }
        }

        private void rbtnB2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnB2.Checked)
            {
                this.b_b.Enabled = false;
                this.b_h.Enabled = false;
                this.b_b.Text = "0";
                this.b_h.Text = "0";
                this.b_L.Enabled = true;
                this.b_d.Enabled = true;
            }
        }

        private void cmbBendType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbBendType.SelectedIndex)
            {
                case 0:
                    this.b_ll.Enabled = false;
                    break;
                case 1:
                    this.b_ll.Enabled = true;
                    break;
            }
        }

        private void b_testNo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.b_testNo.Text))
            {
                if (utils.IsNumeric(this.b_testNum.Text) && !string.IsNullOrEmpty(this.b_testNum.Text))
                {
                    this.b_testSampleNo.Items.Clear();
                    int num = int.Parse(this.b_testNum.Text);
                    for (int i = 0; i < num; i++)
                    {
                        this.b_testSampleNo.Items.Add(this.b_testNo.Text + "-" + (i + 1).ToString("00"));
                    }
                }
                else
                {
                    this.b_testNum.Text = "";
                }
            }
        }

        private void b_testNum_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.b_testNum.Text))
            {
                if (utils.IsNumeric(this.b_testNum.Text) && !string.IsNullOrEmpty(this.b_testNum.Text))
                {
                    this.b_testSampleNo.Items.Clear();
                    int num = int.Parse(this.b_testNum.Text);
                    for (int i = 0; i < num; i++)
                    {
                        this.b_testSampleNo.Items.Add(this.b_testNo.Text + "-" + (i + 1).ToString("00"));
                    }
                }
                else
                {
                    this.b_testNum.Text = "";
                }
            }
        }

        private void b_Ls_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (!utils.IsNumeric(t.Text))
                t.Text = "0";
        }

        //private void b_caculate_Click(object sender, EventArgs e)
        //{
        //    //矩形试样
        //    if (rbtnB1.Checked)
        //    {
        //        if (b_h.Text.Trim().Length > 0 && b_b.Text.Trim().Length > 0)
        //        {
        //            this.b_S0.Text = (double.Parse(b_h.Text.Trim()) * double.Parse(b_b.Text.Trim())).ToString("0.0000");
        //        }
        //    }
        //}

        private void gBtnAddB_Click(object sender, EventArgs e)
        {
            string strErr = "";

            if (this.b_testNo.Text.Trim().Length == 0)
            {
                strErr += "试验编号不能为空！\r\n";
            }
            if (this.b_testSampleNo.Text.Trim().Length == 0)
            {
                strErr += "试样编号不能为空！\r\n";
            }

            if (string.IsNullOrEmpty(this._sendCompany))
            {
                strErr += "送检单位不能为空！\r\n";
            }
            if (this.b_stuffCardNo.Text.Trim().Length == 0)
            {
                strErr += "材料牌号不能为空！\r\n";
            }
            if (this.b_stuffSpec.Text.Trim().Length == 0)
            {
                strErr += "材料规格不能为空！\r\n";
            }
            if (this.b_stuffType.Text.Trim().Length == 0)
            {
                strErr += "材料类型不能为空！\r\n";
            }

            if (string.IsNullOrEmpty(this._temperature))
            {
                strErr += "试验温度不能为空！\r\n";
            }

            if (string.IsNullOrEmpty(this._condition))
            {
                strErr += "试验条件不能为空！\r\n";
            }

            if (string.IsNullOrEmpty(this._controlMode))
            {
                strErr += "控制方式不能为空！\r\n";
            }

            if (this.b_testMethod.Text.Trim().Length == 0)
            {
                strErr += "试验方法不能为空！\r\n";
            }

            if (string.IsNullOrEmpty(this._sampleCharacter))
            {
                strErr += "试样标识不能为空！\r\n";
            }
            if (this.b_getSample.Text.Trim().Length == 0)
            {
                strErr += "试样取样不能为空！\r\n";
            }
            if (this.b_tester.Text.Trim().Length == 0)
            {
                strErr += "试验员不能为空！\r\n";
            }

            if (rbtnB1.Checked)
            {
                if (this.b_h.Text.Trim().Length == 0)
                {
                    strErr += "h不能为空！\r\n";
                }
                else
                {

                    if (double.Parse(this.b_h.Text.Trim()) == 0d)
                    {
                        strErr += "h 不能为0！\r\n";
                    }
                }
                if (this.b_b.Text.Trim().Length == 0)
                {
                    strErr += "b不能为空！\r\n";
                }
                else
                {

                    if (double.Parse(this.b_b.Text.Trim()) == 0d)
                    {
                        strErr += "b 不能为0！\r\n";
                    }
                }
            }

            if (rbtnB2.Checked)
            {
                if (this.b_d.Text.Trim().Length == 0)
                {
                    strErr += "d不能为空！\r\n";
                }
                else
                {
                    if (double.Parse(this.b_d.Text.Trim()) == 0d)
                    {
                        strErr += "d 不能为0！\r\n";
                    }
                }
            }

            if (this.b_L.Text.Trim().Length == 0)
            {
                strErr += "L 不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this.b_L.Text.Trim()) == 0d)
                {
                    strErr += "L 不能为0！\r\n\r\n";
                }
            }

            if (this.b_Ls.Text.Trim().Length == 0)
            {
                strErr += "Ls 不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this.b_Ls.Text.Trim()) == 0d)
                {
                    strErr += "Ls 不能为0！\r\n\r\n";
                }
            }

            if (this.b_n.Text.Trim().Length == 0)
            {
                strErr += "n 不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this.b_n.Text.Trim()) == 0d)
                {
                    strErr += "n 不能为0！\r\n\r\n";
                }
            }

             if (this.b_εpb.Text.Trim().Length == 0)
            {
                strErr += "εpb 不能为空！\r\n\r\n";
            }
            else
            {
                //if (double.Parse(this.b_εpb.Text.Trim()) == 0d)
                //{
                //    strErr += "εpb 不能为0！\r\n\r\n";
                //}
            }

             if (this.b_εrb.Text.Trim().Length == 0)
             {
                 strErr += "εrb 不能为空！\r\n\r\n";
             }
             else
             {
                 //if (double.Parse(this.b_εrb.Text.Trim()) == 0d)
                 //{
                 //    strErr += "εrb 不能为0！\r\n\r\n";
                 //}
             }
     



            if (this.cmbBendType.SelectedIndex == -1)
            {
                strErr += "请选择弯曲类型!\r\n;";
            }

            //if (this.b_S0.Text.Trim().Length == 0)
            //{
            //    strErr += "S0不能为空！\r\n\r\n";
            //}
            //else
            //{
            //    if (double.Parse(this.b_S0.Text.Trim()) == 0d)
            //    {
            //        strErr += "S0不能为0！\r\n\r\n";
            //    }
            //}

            switch (cmbBendType.SelectedIndex)
            {
                case 0://三点弯曲                    
                    break;
                case 1://四点弯曲
                    if (this.b_ll.Text.Trim().Length == 0)
                    {
                        strErr += "力臂不能为空！\r\n\r\n";
                    }
                    else
                    {
                        if (double.Parse(this.b_ll.Text.Trim()) == 0d)
                        {
                            strErr += "力臂不能为0！\r\n\r\n";
                        }
                    }
                    break;
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            HR_Test.Model.Bend model = new HR_Test.Model.Bend();
            model.testNo = this.b_testNo.Text;
            model.testSampleNo = this.b_testSampleNo.Text;
            model.testMethodID = 0;
            model.sendCompany = this._sendCompany;
            model.stuffCardNo = this.b_stuffCardNo.Text;
            model.stuffSpec = b_stuffSpec.Text;
            model.stuffType = b_stuffType.Text;
            model.hotStatus = this._hotStatus;
            model.temperature = double.Parse(this._temperature);
            model.humidity = double.Parse(this._humidity);
            model.testStandard = this._testStandard_B.Text;
            model.testMethod = b_testMethod.Text;
            model.mathineType = b_mathineType.Text;
            model.testCondition = "-";
            model.sampleCharacter = this._sampleCharacter;
            model.getSample = b_getSample.Text;
            model.tester = b_tester.Text;
            model.assessor = "-";
            model.sign = this.b_sign.Text;

            model.testType = this.cmbBendType.Text;
            model.d = Convert.ToDouble(!string.IsNullOrEmpty(this.b_d.Text) ? this.b_d.Text : "0");
            model.b = Convert.ToDouble(!string.IsNullOrEmpty(this.b_b.Text) ? this.b_b.Text : "0");
            model.h = Convert.ToDouble(!string.IsNullOrEmpty(this.b_h.Text) ? this.b_h.Text : "0");
            model.L = Convert.ToDouble(!string.IsNullOrEmpty(this.b_L.Text) ? this.b_L.Text : "0");
            model.Ds = this._b_Ds;
            model.Da = this._b_Da;
            model.R = this._b_R;
            model.t = 0;
            model.Ls = Convert.ToDouble(!string.IsNullOrEmpty(this.b_Ls.Text) ? this.b_Ls.Text : "0");
            model.Le = Convert.ToDouble(!string.IsNullOrEmpty(this.b_Le.Text) ? this.b_Le.Text : "0");
            model.l_l = Convert.ToDouble(!string.IsNullOrEmpty(this.b_ll.Text) ? this.b_ll.Text : "0");
            model.lt = 0;
            model.m = this._b_m;
            model.n = Convert.ToDouble(!string.IsNullOrEmpty(this.b_n.Text) ? this.b_n.Text : "0");
            model.a = this._b_a;
            model.εpb = Convert.ToDouble(!string.IsNullOrEmpty(this.b_εpb.Text) ? this.b_εpb.Text : "0");
            model.εrb = Convert.ToDouble(!string.IsNullOrEmpty(this.b_εrb.Text) ? this.b_εrb.Text : "0");
            model.y = 0; 
            //试验结果
            if (this.rbtnB1.Checked)
            {
                model.sampleType = "矩形";
                model.W = (double)Math.Round((decimal)(model.b * model.h * model.h / 6.0), 3);//bh²/6
                model.I = (double)Math.Round((decimal)(model.b * model.h * model.h * model.h / 12), 3);//bh³/12
            }
            else if (this.rbtnB2.Checked)
            {
                model.sampleType = "圆柱形";
                model.W = (double)Math.Round((decimal)(Math.PI * model.d * model.d * model.d / 32), 3);
                model.I = (double)Math.Round((decimal)(Math.PI * model.d * model.d * model.d * model.d / 64.0), 3);
            }

            model.f_bb = 0;
            model.f_n = 0;
            model.f_n1 = 0;
            model.f_rb = 0;
            model.Fo = 0;
            model.Fpb = 0;
            model.Frb = 0;
            model.Fbb = 0;
            model.Fn = 0;
            model.Fn1 = 0;
            model.Z = 0;
            model.S = 0;
            model.Eb = 0;
            model.σpb = 0;
            model.σrb = 0;
            model.σbb = 0;
            model.U = 0;
            //-----------
            model.isFinish = false;
            model.isConformity = false;
            model.testDate = this.b_testDate.Value.Date;
            model.condition = this._condition;
            model.controlmode = this._controlMode;
            model.isUseExtensometer = false;
            model.isEffective = false;

            BLL.Bend bllc = new HR_Test.BLL.Bend();
            if (bllc.GetList("testSampleNo ='" + this.b_testSampleNo.Text + "'").Tables[0].Rows.Count == 0)
            {
                bllc.Add(model);
                this.b_testSampleNo.Items.Remove(this.b_testSampleNo.SelectedItem);
                this.b_h.Text = "0";
                this.b_b.Text = "0";
                MessageBox.Show(this, "添加试样信息成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, "已经存在相同的编号，请重新设置!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //TestStandard.MethodControl.ReadMethod(this.treeviewTestMethod);
        }

        private void gBtnDelB_Click(object sender, EventArgs e)
        {
            BLL.Bend bllBend = new HR_Test.BLL.Bend();
            bllBend.Delete(this._methodName);
            TestStandard.MethodControl.ReadMethodList(this.treeviewTestMethod);
        }

        private void gBtnAddToMethod3_Click(object sender, EventArgs e)
        {
            string strErr = "";


            if (this._sendCompany.Trim().Length == 0)
            {
                strErr += "送检单位不能为空！\r\n\r\n";
            }
            if (this.b_stuffCardNo.Text.Trim().Length == 0)
            {
                strErr += "材料牌号不能为空！\r\n\r\n";
            }
            if (this.b_stuffSpec.Text.Trim().Length == 0)
            {
                strErr += "材料规格不能为空！\r\n\r\n";
            }
            if (this.b_stuffType.Text.Trim().Length == 0)
            {
                strErr += "材料类型不能为空！\r\n\r\n";
            }

            if (this._temperature.Trim().Length == 0)
            {
                strErr += "试验温度不能为空！\r\n\r\n";
            }

            if (this.b_testMethod.Text.Trim().Length == 0)
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

            if (this.b_getSample.Text.Trim().Length == 0)
            {
                strErr += "试样取样不能为空！\r\n\r\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }
            BLL.ControlMethod_B bllcm_B = new HR_Test.BLL.ControlMethod_B();
            HR_Test.Model.ControlMethod_B model = bllcm_B.GetModel(this.b_testMethod.Text);

            model.sendCompany = this._sendCompany;
            model.stuffCardNo = this.b_stuffCardNo.Text;
            model.stuffSpec = b_stuffSpec.Text;
            model.stuffType = b_stuffType.Text;
            model.hotStatus = this._hotStatus;
            model.temperature = double.Parse(this._temperature);
            model.humidity = double.Parse(this._humidity);
            model.testStandard = this._testStandard_B.Text;
            model.testMethod = b_testMethod.Text;
            model.mathineType = b_mathineType.Text;
            model.testCondition = "";
            model.sampleCharacter = this._sampleCharacter;
            model.getSample = b_getSample.Text;
            model.tester = b_tester.Text;
            model.condition = this._condition;
            model.controlmode = this._controlMode;
            model.assessor = "";
            model.sign = this.b_sign.Text;

            model.testType = this.cmbBendType.Text;
            model.Ds = this._b_Ds;
            model.Da = this._b_Da;
            model.R = this._b_R;
            model.Ls = Convert.ToDouble(this.b_Ls.Text.Trim());
            model.Le = Convert.ToDouble(this.b_Le.Text.Trim());
            model.l_l = Convert.ToDouble(this.b_ll.Text.Trim());
            model.m = this._b_m;
            model.n = Convert.ToDouble(this.b_n.Text.Trim());
            model.a = this._b_a;
            model.εpb = Convert.ToDouble(this.b_εpb.Text.Trim());
            model.εrb = Convert.ToDouble(this.b_εrb.Text.Trim());


            if (bllcm_B.Update(model))
            {
                MessageBox.Show(this, "更新试验方法基本参数成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
