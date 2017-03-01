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
    /// 纤维材料拉伸 GBT3354_2014
    /// </summary>
    public partial class GBT3354_2014 : UserControl
    {
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

        string _condition="-";
        string _controlMode="-";

        public GBT3354_2014()
        {
            InitializeComponent();
            this.gbTensileC.Size = new Size(946, 450);
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

            if (string.IsNullOrEmpty(this._sendCompany.Text))
            {
                strErr += "'送检单位'不能为空！\r\n\r\n";
            }
            if (this._adhesive.Text.Trim().Length == 0)
            {
                strErr += "'胶粘剂信息'不能为空！\r\n\r\n";
            }
            if (this._stuffSpec.Text.Trim().Length == 0)
            {
                strErr += "'材料规格'不能为空！\r\n\r\n";
            }
            if (this._strengthPlate.Text.Trim().Length == 0)
            {
                strErr += "'加强片信息'不能为空！\r\n\r\n";
            }

            if (string.IsNullOrEmpty(this._temperature.Text))
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

            if (this._sampleState.Text.Trim().Length == 0)
            {
                strErr += "'试样状态'不能为空！\r\n\r\n";
            }

            if (this._getSample.Text.Trim().Length == 0)
            {
                strErr += "'试样来源'不能为空！\r\n\r\n";
            }
            if (this._tester.Text.Trim().Length == 0)
            {
                strErr += "'试验员'不能为空！\r\n\r\n";
            }

            if (rdoRect.Checked)
            {
                if (this._w.Text.Trim().Length == 0)
                {
                    strErr += "'a0'不能为空！\r\n\r\n";
                }
                if (this._h.Text.Trim().Length == 0)
                {
                    strErr += "'b0'不能为空！\r\n\r\n";
                }
            }

            if (rdoCircle.Checked)
            {
                if (this._d0.Text.Trim().Length == 0)
                {
                    strErr += "'d0'不能为空！\r\n\r\n";
                }
            }


            if (rdoPipe.Checked)
            {
                if (this._w.Text.Trim().Length == 0)
                {
                    strErr += "'a0'不能为空！\r\n\r\n";
                }
                if (this._Do.Text.Trim().Length == 0)
                {
                    strErr += "'Do'不能为空！\r\n\r\n";
                }
            }

            if (this._S0.Text.Trim().Length == 0)
            {
                strErr += "'S0'不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this._S0.Text.Trim()) == 0d)
                {
                    strErr += "'S0'不能为0！\r\n\r\n";
                }
            }


            if (this._lL.Text.Trim().Length == 0)
            {
                strErr += "'纵向引伸计标距'不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this._lL.Text.Trim()) == 0d)
                {
                    strErr += "'纵向引伸计标距'不能为0！\r\n\r\n";
                }
            }


            if (this._lT.Text.Trim().Length == 0)
            {
                strErr += "'横向引伸计标距'不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this._lT.Text.Trim()) == 0d)
                {
                    strErr += "'横向引伸计标距'不能为0！\r\n\r\n";
                }
            }


            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            string testNo = this._testNo.Text;
            string testSampleNo = this._testSampleNo.Text;
            string sendCompany = this._sendCompany.Text;
            string stuffCardNo = this._adhesive.Text;
            string stuffSpec = this._stuffSpec.Text;
            string stuffType = this._strengthPlate.Text;
            string temperature = this._temperature.Text;
            string humidity = this._humidity.Text;
            string testStandard = this._testStandard.Text;
            string testMethod = this._testMethod.Text;
            string mathineType = this._mathineType.Text;
            string getSample = this._getSample.Text;
            string tester = this._tester.Text;
            string w = (!string.IsNullOrEmpty(this._w.Text) ? this._w.Text : "0");
            string h = (!string.IsNullOrEmpty(this._h.Text) ? this._h.Text : "0");
            string d0 = (!string.IsNullOrEmpty(this._d0.Text) ? this._d0.Text : "0");
            string Do = (!string.IsNullOrEmpty(this._Do.Text) ? this._Do.Text : "0");
            string lL = (!string.IsNullOrEmpty(this._lL.Text) ? this._lL.Text : "0");
            string lT = (!string.IsNullOrEmpty(this._lT.Text) ? this._lT.Text : "0");
            string S0 = this._S0.Text;
            string εz = (!string.IsNullOrEmpty(this._εz.Text) ? this._εz.Text : "0");

            Model.GBT3354_Samples model = new Model.GBT3354_Samples();
            model.adhesive = _adhesive.Text;
            model.assessor = "-";
            model.controlmode = "-";
            model.d0 = double.Parse(d0);
            model.Do = double.Parse(Do);
            model.Et = 0;
            model.failuremode = "-";
            model.getSample = getSample;
            model.h = double.Parse(h);
            model.humidity = double.Parse(humidity);
            model.isEffective = false;
            model.isFinish = false;
            model.isUseExtensometer1 = true;
            model.isUseExtensometer2 = true;
            model.lL = double.Parse(lL);
            model.lT = double.Parse(lT);
            model.mathineType = mathineType;
            model.Pmax = 0;
            model.reportNo = "-";
            model.S0 = double.Parse(S0);
            if (rdoRect.Checked)
                model.sampleShape = "矩形";
            if (rdoCircle.Checked)
                model.sampleShape = "圆柱形";
            if (rdoPipe.Checked)
                model.sampleShape = "圆管";
            model.sampleState = _sampleState.Text;
            model.sendCompany = _sendCompany.Text;
            model.sign = _sign.Text;
            model.strengthPlate = _strengthPlate.Text;
            model.stuffSpec = stuffSpec;
            model.temperature = double.Parse(temperature);

            model.testDate = this._testDate.Value.Date;
            model.tester = tester;
            model.testMethod = testMethod;
            model.testMethodName = testMethod;
            model.testNo = testNo;
            model.testSampleNo = testSampleNo;
            model.testStandard = testStandard;
            model.w = double.Parse(w);
            model.ε1t = 0;
            model.εz = _εz.Text;
            model.μ12 = 0;
            model.σt = 0;

            model.testCondition = _condition;
            model.controlmode = this._controlMode;
            BLL.GBT3354_Samples bll = new BLL.GBT3354_Samples();
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
                if (DialogResult.OK == MessageBox.Show("是否删除该编号?", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    BLL.GBT3354_Samples bllts = new HR_Test.BLL.GBT3354_Samples();
                    bllts.Delete(_methodName);
                    //TestStandard.MethodControl.ReadMethod(treeviewTestMethod);
                }
            }
        }


        public void ReadMethodInfo(string methodName)
        {
            BLL.GBT3354_Method bllts = new HR_Test.BLL.GBT3354_Method();
            DataSet ds = bllts.GetList("MethodName ='" + methodName + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                _stuffSpec.Text = ds.Tables[0].Rows[0]["sampleSpec"].ToString();
                _strengthPlate.Text = ds.Tables[0].Rows[0]["strengthPlate"].ToString();
                _adhesive.Text = ds.Tables[0].Rows[0]["adhesive"].ToString();
                _getSample.Text = ds.Tables[0].Rows[0]["getSample"].ToString();
                _testStandard.Text = ds.Tables[0].Rows[0]["testStandard"].ToString();
                _sampleState.Text = ds.Tables[0].Rows[0]["sampleState"].ToString();
                _sendCompany.Text = ds.Tables[0].Rows[0]["sendCompany"].ToString();
                _temperature.Text = ds.Tables[0].Rows[0]["temperature"].ToString();
                _humidity.Text = ds.Tables[0].Rows[0]["humidity"].ToString();
                _lL.Text = ds.Tables[0].Rows[0]["lL"].ToString();
                _lT.Text = ds.Tables[0].Rows[0]["lT"].ToString();
                _εz.Text = ds.Tables[0].Rows[0]["εz"].ToString();

                _mathineType.Text = ds.Tables[0].Rows[0]["mathineType"].ToString();
                _tester.Text = ds.Tables[0].Rows[0]["tester"].ToString();
                _testMethod.Text = ds.Tables[0].Rows[0]["testMethod"].ToString();
                _sign.Text = ds.Tables[0].Rows[0]["sign"].ToString();

                //_w.Text = "";
                //_h.Text = "";
                //_d0.Text = "";
                //_Do.Text = "";
                //_S0.Text = "";

                //_condition = ds.Tables[0].Rows[0]["condition"].ToString();
                //_controlMode = ds.Tables[0].Rows[0]["controlmode"].ToString();
            }
            ds.Dispose();
        }

        private void GBT228_2010Tensile_Load(object sender, EventArgs e)
        {
            rdoRect_CheckedChanged(sender, e);
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

        private void rdoRect_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoRect.Checked)
            {
                _w.Enabled = true;
                _h.Enabled = true;
                _d0.Enabled = false;
                _Do.Enabled = false;
                _w.Text = _h.Text = _Do.Text = _d0.Text = _S0.Text = "";
            }
        }

        private void rdoCircle_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCircle.Checked)
            {
                _w.Enabled = false;
                _h.Enabled = false;
                _d0.Enabled = true;
                _Do.Enabled = false;
                _w.Text = _h.Text = _Do.Text = _d0.Text = _S0.Text = "";
            }
        }

        private void rdoPipe_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPipe.Checked)
            {
                _w.Enabled = true;
                _h.Enabled = false;
                _d0.Enabled = false;
                _Do.Enabled = true;
                _w.Text = _h.Text = _Do.Text = _d0.Text = _S0.Text = "";
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

            //if (this._testNo.Text.Trim().Length == 0)
            //{
            //    strErr += "'试验编号'不能为空！\r\n\r\n";
            //}
            //if (this._testSampleNo.Text.Trim().Length == 0)
            //{
            //    strErr += "'试样编号'不能为空！\r\n\r\n";
            //}

            if (string.IsNullOrEmpty(this._sendCompany.Text))
            {
                strErr += "'送检单位'不能为空！\r\n\r\n";
            }
            if (this._adhesive.Text.Trim().Length == 0)
            {
                strErr += "'胶粘剂信息'不能为空！\r\n\r\n";
            }
            if (this._stuffSpec.Text.Trim().Length == 0)
            {
                strErr += "'材料规格'不能为空！\r\n\r\n";
            }
            if (this._strengthPlate.Text.Trim().Length == 0)
            {
                strErr += "'加强片信息'不能为空！\r\n\r\n";
            }

            //if (string.IsNullOrEmpty(this._temperature.Text))
            //{
            //    strErr += "'试验温度'不能为空！\r\n\r\n";
            //}

            //if (string.IsNullOrEmpty(this._condition))
            //{
            //    strErr += "'试验条件'不能为空！\r\n\r\n";
            //}

            //if (string.IsNullOrEmpty(this._controlMode))
            //{
            //    strErr += "'控制方式'不能为空！\r\n\r\n";
            //}

            if (this._testMethod.Text.Trim().Length == 0)
            {
                strErr += "'试验方法'不能为空！\r\n\r\n";
            }

            if (this._sampleState.Text.Trim().Length == 0)
            {
                strErr += "'试样状态'不能为空！\r\n\r\n";
            }

            if (this._getSample.Text.Trim().Length == 0)
            {
                strErr += "'试样来源'不能为空！\r\n\r\n";
            }
            if (this._tester.Text.Trim().Length == 0)
            {
                strErr += "'试验员'不能为空！\r\n\r\n";
            }

            //if (rdoRect.Checked)
            //{
            //    if (this._w.Text.Trim().Length == 0)
            //    {
            //        strErr += "'a0'不能为空！\r\n\r\n";
            //    }
            //    if (this._h.Text.Trim().Length == 0)
            //    {
            //        strErr += "'b0'不能为空！\r\n\r\n";
            //    }
            //}

            //if (rdoCircle.Checked)
            //{
            //    if (this._d0.Text.Trim().Length == 0)
            //    {
            //        strErr += "'d0'不能为空！\r\n\r\n";
            //    }
            //}


            //if (rdoPipe.Checked)
            //{
            //    if (this._w.Text.Trim().Length == 0)
            //    {
            //        strErr += "'a0'不能为空！\r\n\r\n";
            //    }
            //    if (this._Do.Text.Trim().Length == 0)
            //    {
            //        strErr += "'Do'不能为空！\r\n\r\n";
            //    }
            //}

            //if (this._S0.Text.Trim().Length == 0)
            //{
            //    strErr += "'S0'不能为空！\r\n\r\n";
            //}
            //else
            //{
            //    if (double.Parse(this._S0.Text.Trim()) == 0d)
            //    {
            //        strErr += "'S0'不能为0！\r\n\r\n";
            //    }
            //}


            if (this._lL.Text.Trim().Length == 0)
            {
                strErr += "'纵向引伸计标距'不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this._lL.Text.Trim()) == 0d)
                {
                    strErr += "'纵向引伸计标距'不能为0！\r\n\r\n";
                }
            }


            if (this._lT.Text.Trim().Length == 0)
            {
                strErr += "'横向引伸计标距'不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this._lT.Text.Trim()) == 0d)
                {
                    strErr += "'横向引伸计标距'不能为0！\r\n\r\n";
                }
            }


            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            string testNo = this._testNo.Text;
            string testSampleNo = this._testSampleNo.Text;
            string sendCompany = this._sendCompany.Text;
            string stuffCardNo = this._adhesive.Text;
            string stuffSpec = this._stuffSpec.Text;
            string stuffType = this._strengthPlate.Text;
            string temperature = this._temperature.Text;
            string humidity = this._humidity.Text;
            string testStandard = this._testStandard.Text;
            string testMethod = this._testMethod.Text;
            string mathineType = this._mathineType.Text;
            string getSample = this._getSample.Text;
            string tester = this._tester.Text;
            string w = (!string.IsNullOrEmpty(this._w.Text) ? this._w.Text : "0");
            string h = (!string.IsNullOrEmpty(this._h.Text) ? this._h.Text : "0");
            string d0 = (!string.IsNullOrEmpty(this._d0.Text) ? this._d0.Text : "0");
            string Do = (!string.IsNullOrEmpty(this._Do.Text) ? this._Do.Text : "0");
            string lL = (!string.IsNullOrEmpty(this._lL.Text) ? this._lL.Text : "0");
            string lT = (!string.IsNullOrEmpty(this._lT.Text) ? this._lT.Text : "0");
            string S0 = this._S0.Text;
            string εz = (!string.IsNullOrEmpty(this._εz.Text) ? this._εz.Text : "0");
            BLL.GBT3354_Method bll = new BLL.GBT3354_Method();
            Model.GBT3354_Method model = bll.GetModel(_testMethod.Text);
            if (model != null)
            {
                model.adhesive = _adhesive.Text;
                model.assessor = "-";
                //model.d0 = double.Parse(d0);
                //model.Do = double.Parse(Do);
                model.getSample = getSample;
                //model.h = double.Parse(h);
                model.humidity = double.Parse(humidity);
                model.lL = double.Parse(lL);
                model.lT = double.Parse(lT);
                model.mathineType = mathineType;
                //model.S0 = double.Parse(S0);
                if (rdoRect.Checked)
                    model.sampleShape = "矩形";
                if (rdoCircle.Checked)
                    model.sampleShape = "圆柱形";
                if (rdoPipe.Checked)
                    model.sampleShape = "圆管";
                model.sampleState = _sampleState.Text;
                model.sendCompany = _sendCompany.Text;
                model.sign = _sign.Text;
                model.strengthPlate = _strengthPlate.Text;
                model.temperature = double.Parse(temperature);
                //model.testDate = this._testDate.Value.Date;
                model.tester = tester;
                model.testMethod = testMethod;
                model.testStandard = testStandard;
                //model.w = double.Parse(w);
                model.εz = _εz.Text;
                model.testCondition = _condition;
                model.sampleSpec = stuffSpec;
            }
            if (bll.Update(model))
            {
                MessageBox.Show(this, "更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gbtnCalculate_Click(object sender, EventArgs e)
        {
            //矩形试样
            if (rdoRect.Checked)
            {
                if (_w.Text.Trim().Length > 0 && _h.Text.Trim().Length > 0)
                {
                    this._S0.Text = (double.Parse(_w.Text.Trim()) * double.Parse(_h.Text.Trim())).ToString("0.0000");
                }
            }

            //圆形试样
            if (rdoCircle.Checked)
            {
                if (_d0.Text.Trim().Length > 0)
                    this._S0.Text = (double.Parse(this._d0.Text) * double.Parse(this._d0.Text) * Math.PI / 4).ToString("0.0000");
            }

            //圆管试样
            if (rdoPipe.Checked)
            {
                if (_w.Text.Trim().Length > 0 && _Do.Text.Trim().Length > 0)
                {
                    this._S0.Text = ((double.Parse(this._Do.Text) - double.Parse(this._w.Text)) * double.Parse(this._w.Text) * Math.PI).ToString("0.0000");
                }
            }
        }


        private void GBT228_2010Tensile_SizeChanged(object sender, EventArgs e)
        {
            this.gbTensileC.Size = new Size(946, 450);
        }

        private void btnOtherShape_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
            Info.FileName = "calc.exe";
            System.Diagnostics.Process Proc = System.Diagnostics.Process.Start(Info);
        }

        private void palBottom_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
