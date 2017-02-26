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

        //拉伸试验其他参数
        public string _sendCompany;
        public string _temperature;
        public string _sampleCharacter;
        public string _hotStatus;
        public string _humidity; 
        public string _condition;
        public string _controlMode;
        public double _Lt;
        public double _L01;

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

            if (rdoRect.Checked)
            {
                if (this._a0.Text.Trim().Length == 0)
                {
                    strErr += "'a0'不能为空！\r\n\r\n";
                }
                if (this._b0.Text.Trim().Length == 0)
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
                if (this._a0.Text.Trim().Length == 0)
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


            if (this._L0.Text.Trim().Length == 0)
            {
                strErr += "'L0'不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this._L0.Text.Trim()) == 0d)
                {
                    strErr += "'L0'不能为0！\r\n\r\n";
                }
            }


            if (this._Le.Text.Trim().Length == 0)
            {
                strErr += "'Le'不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this._Le.Text.Trim()) == 0d)
                {
                    strErr += "'Le'不能为0！\r\n\r\n";
                }
            }


            if (this._Lc.Text.Trim().Length == 0)
            {
                strErr += "'Lc'不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this._Lc.Text.Trim()) == 0d)
                {
                    strErr += "'Lc'不能为0！\r\n\r\n";
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
            string a0 = (!string.IsNullOrEmpty(this._a0.Text) ? this._a0.Text : "0"); 
            string b0 = (!string.IsNullOrEmpty(this._b0.Text) ? this._b0.Text : "0"); 
            string d0 = (!string.IsNullOrEmpty(this._d0.Text) ? this._d0.Text : "0"); 
            string Do = (!string.IsNullOrEmpty(this._Do.Text) ? this._Do.Text : "0");
            string L0 = (!string.IsNullOrEmpty(this._L0.Text) ? this._L0.Text : "0");
            double L01 = this._L01;
            string Lc = (!string.IsNullOrEmpty(this._Lc.Text) ? this._Lc.Text : "0");
            string Le = (!string.IsNullOrEmpty(this._Le.Text)? this._Le.Text : "0");
            double Lt = this._Lt;
           
            string S0 = this._S0.Text;
           
            string εp = (!string.IsNullOrEmpty(this._εp.Text) ? this._εp.Text : "0");
            string εt = (!string.IsNullOrEmpty(this._εt.Text) ? this._εt.Text : "0");
            string εr = (!string.IsNullOrEmpty(this._εr.Text) ? this._εr.Text : "0");
            

            Model.TestSample model = new Model.TestSample(); 
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
            model.sign = this._sign.Text;
            model.a0 = double.Parse(a0);
            model.au = 0;
            model.b0 = double.Parse(b0);
            model.bu = 0;
            model.d0 = double.Parse(d0);
            model.du = 0;
            model.Do = double.Parse(Do);
            model.L0 = double.Parse(L0);
            model.L01 = 0;//L01;
            model.Lc = double.Parse(Lc);
            model.Le = double.Parse(Le);
            model.Lt = Lt;
            model.Lu = 0;// double.Parse(Lu);
            model.Lu1 = 0;// Lu1;
            model.S0 = double.Parse(S0);
            model.Su = 0;// Su;
            model.k = 0;// k;
            model.Fm = 0;// Fm;
            model.Rm = 0;// Rm;
            model.ReH = 0;// ReH;
            model.ReL = 0;// ReL;
            model.Rp = 0;// Rp;
            model.Rt = 0;// Rt;
            model.Rr = 0;// Rr;
            model.εp = double.Parse(εp);
            model.εt = double.Parse(εt);
            model.εr = double.Parse(εr);
            model.E = 0;// E;
            model.m = 0;// m;
            model.mE = 0;// mE;
            model.A = 0;//A;
            model.Aee = 0;//Aee;
            model.Agg = 0;// Agg;
            model.Att = 0;// Att;
            model.Aggtt = 0;//Aggtt;
            model.Awnwn = 0;// Awnwn;
            model.Lm = 0;//Lm;
            model.deltaLm = 0;//deltaLm
            model.Lf = 0;//Lf;
            model.Z = 0;//Z;
            model.Avera = 0;// Avera;
            model.SS = 0;//SS;
            model.Avera1 = 0;//Avera1;
            model.isFinish = false;
            //是否使用引申计根据试验方法确定
            model.isUseExtensometer = false;
            model.isEffective = false;

            model.testDate = this._testDate.Value.Date;
            model.condition = this._condition;
            model.controlmode = this._controlMode;
            BLL.TestSample bll = new BLL.TestSample();
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
                    BLL.TestSample bllts = new HR_Test.BLL.TestSample();
                    bllts.Delete(_methodName);
                    //TestStandard.MethodControl.ReadMethod(treeviewTestMethod);
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

        private void btnMore6_Click(object sender, EventArgs e)
        {
            //frmTensileOther2 fto1 = new frmTensileOther2(this);
            //if (DialogResult.OK == fto1.ShowDialog())
            //{
            //    this._Lt = double.Parse(fto1._Lt.Text);
            //    this._L01 = double.Parse(fto1._L01.Text);
            //}
        }

        public void ReadMethodInfo(string methodName)
        {
            BLL.ControlMethod bllts = new HR_Test.BLL.ControlMethod();
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
                _L0.Text = ds.Tables[0].Rows[0]["L0"].ToString();
                _L01 = double.Parse(ds.Tables[0].Rows[0]["L01"].ToString());
                _Lc.Text = ds.Tables[0].Rows[0]["Lc"].ToString();
                _Le.Text = ds.Tables[0].Rows[0]["Le"].ToString();
                _Lt = double.Parse(ds.Tables[0].Rows[0]["Lt"].ToString());

                _getSample.Text = ds.Tables[0].Rows[0]["getSample"].ToString();
                _a0.Text = "";
                _b0.Text = "";
                _d0.Text = "";
                _Do.Text = "";
                _S0.Text = "";

                _εp.Text = ds.Tables[0].Rows[0]["au"].ToString();//对应 au
                _εt.Text = ds.Tables[0].Rows[0]["bu"].ToString();//bu
                _εr.Text = ds.Tables[0].Rows[0]["du"].ToString();//du

                _mathineType.Text = ds.Tables[0].Rows[0]["mathineType"].ToString();
                _tester.Text = ds.Tables[0].Rows[0]["tester"].ToString();
                _testMethod.Text = ds.Tables[0].Rows[0]["testMethod"].ToString();

                _condition = ds.Tables[0].Rows[0]["condition"].ToString();
                _controlMode = ds.Tables[0].Rows[0]["controlmode"].ToString();
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
                _a0.Enabled = true;
                _b0.Enabled = true;
                _d0.Enabled = false;
                _Do.Enabled = false;
                _a0.Text = _b0.Text = _Do.Text = _d0.Text = _S0.Text = "";
            }
        }

        private void rdoCircle_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCircle.Checked)
            {
                _a0.Enabled = false;
                _b0.Enabled = false;
                _d0.Enabled = true;
                _Do.Enabled = false;
                _a0.Text = _b0.Text = _Do.Text = _d0.Text = _S0.Text = "";
            }
        }

        private void rdoPipe_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPipe.Checked)
            {
                _a0.Enabled = true;
                _b0.Enabled = false;
                _d0.Enabled = false;
                _Do.Enabled = true;
                _a0.Text = _b0.Text = _Do.Text = _d0.Text = _S0.Text = "";
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

            if (this._Lc.Text.Trim().Length == 0)
            {
                strErr += "'Lc'不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this._Lc.Text.Trim()) == 0d)
                {
                    strErr += "'Lc'不能为0！\r\n\r\n";
                }
            }

            if (this._Le.Text.Trim().Length == 0)
            {
                strErr += "'Le'不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this._Le.Text.Trim()) == 0d)
                {
                    strErr += "'Le'不能为0！\r\n\r\n";
                }
            }

            if (this._L0.Text.Trim().Length == 0)
            {
                strErr += "'L0'不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this._L0.Text.Trim()) == 0d)
                {
                    strErr += "'L0'不能为0！\r\n\r\n";
                }
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            string testNo = this._testNo.Text;
            string testSampleNo = this._testSampleNo.Text;
            //string reportNo = this._reportNo.Text;
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
            //string testCondition = this._testCondition.Text;
            string sampleCharacter = this._sampleCharacter;
            string getSample = this._getSample.Text;
            string tester = this._tester.Text;
            string a0 = (this._a0.Text.Trim().Length > 0 ? this._a0.Text : "0");
            string b0 = (this._b0.Text.Trim().Length > 0 ? this._b0.Text : "0");
            string d0 = (this._d0.Text.Trim().Length > 0 ? this._d0.Text : "0");
            string Do = (this._Do.Text.Trim().Length > 0 ? this._Do.Text : "0");
            string L0 = (this._L0.Text.Trim().Length > 0 ? this._L0.Text : "0");
            double L01 = this._L01;
            string Lc = (this._Lc.Text.Trim().Length > 0 ? this._Lc.Text : "0");
            string Le = (this._Le.Text.Trim().Length > 0 ? this._Le.Text : "0");
            double Lt = this._Lt;

            string S0 = this._S0.Text;

            string εp = (this._εp.Text.Trim().Length > 0 ? this._εp.Text : "0");
            string εt = (this._εt.Text.Trim().Length > 0 ? this._εt.Text : "0");
            string εr = (this._εr.Text.Trim().Length > 0 ? this._εr.Text : "0");

            BLL.ControlMethod bllcm = new HR_Test.BLL.ControlMethod();
            Model.ControlMethod model = bllcm.GetModel(this._testMethod.Text);
            //model.testMethodID = testMethodID;

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
            model.au = double.Parse(εp);
            model.bu = double.Parse(εt);
            model.du = double.Parse(εr);

            //"L0 double," + //原始标距
            model.L0 = double.Parse(L0);
            //"L01 double" + //测定Awn的原始标距
            model.L01 = L01;
            //"Lc double," + //平行长度
            model.Lc = double.Parse(Lc);
            //"Le double," + //引伸计标距
            model.Le = double.Parse(Le);
            //"Lt double," + //试样总长度
            model.Lt = Lt;
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
            if (bllcm.Update(model))
            {
                MessageBox.Show(this, "更新成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
        }

        private void gbtnCalculate_Click(object sender, EventArgs e)
        {
            //矩形试样
            if (rdoRect.Checked)
            {
                if (_a0.Text.Trim().Length > 0 && _b0.Text.Trim().Length > 0)
                {
                    this._S0.Text = (double.Parse(_a0.Text.Trim()) * double.Parse(_b0.Text.Trim())).ToString("0.0000");
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
                if (_a0.Text.Trim().Length > 0 && _Do.Text.Trim().Length > 0)
                {
                    this._S0.Text = ((double.Parse(this._Do.Text) - double.Parse(this._a0.Text)) * double.Parse(this._a0.Text) * Math.PI).ToString("0.0000");
                }
            }
        }
        private bool boolLashen2 = false;
        private void btnMore2_Click(object sender, EventArgs e)
        {
            boolLashen2 = !boolLashen2;
            if (boolLashen2)
            {
                pallashen2.Visible = true;
            }
            else
            {
                pallashen2.Visible = false;
            } 
        }

        private void GBT228_2010Tensile_SizeChanged(object sender, EventArgs e)
        {
            this.gbTensileC.Size = new Size(946, 450);
        }
    }
}
