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
    public partial class frmInput : Form
    {
        //拉伸试验其他参数
        public string _sendCompany;
        public string _temperature;
        public string _sampleCharacter;
        public string _hotStatus;
        public string _humidity;
        public string _testStandard;

        private frmMain _fmMain;
        public frmInput(frmMain fmMain)
        {
            InitializeComponent();
            _fmMain = fmMain;
            readMethod(this.tvTestMethod);
            this.gbTensile.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this.gbTensile, true, null);
        }

        private void frmInput_Load(object sender, EventArgs e)
        {
            this.gbTensile.Dock = DockStyle.Fill;
            this.gbTensile.BringToFront();
            rdoRect_CheckedChanged(sender, e);
        }

        private void frmInput_SizeChanged(object sender, EventArgs e)
        {
            //this.pbInputTitle.Left = this.Width / 2 - this.pbInputTitle.Width / 2; 
        } 

        //private void readMethod(TreeView tv)
        //{
        //    tv.Nodes.Clear();
        //    tv.Nodes.Add("试验方法");
        //    BLL.ControlMethod bllCm = new HR_Test.BLL.ControlMethod();
        //    DataSet ds = bllCm.GetAllList();
        //    BLL.TestSample bllTs = new HR_Test.BLL.TestSample();
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        TreeNode tn = new TreeNode();
        //        tn.Text = ds.Tables[0].Rows[i]["methodName"].ToString();
        //        tn.Name = ds.Tables[0].Rows[i]["ID"].ToString();
        //        //for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
        //        //{
        //        //    if(!string.IsNullOrEmpty(ds.Tables[0].Rows[i][j].ToString()))
        //        //    {
        //        //        TreeNode tnc = new TreeNode();
        //        //        tnc.Text = ds.Tables[0].Rows[i][j].ToString();
        //        //        tn.Nodes.Add(tnc);
        //        //    }
        //        //}
        //        DataSet dsTs = bllTs.GetList(" testMethodName ='" + ds.Tables[0].Rows[i]["methodName"].ToString() + "'");
        //        for (int j = 0; j < dsTs.Tables[0].Rows.Count; j++)
        //        {
        //            TreeNode tnc = new TreeNode();
        //            tnc.Text = dsTs.Tables[0].Rows[j]["testSampleNo"].ToString();
        //            //tnc.Name = dsTs.Tables[0].Rows[j]["ID"].ToString();
        //            tn.Nodes.Add(tnc);
        //        }
        //        dsTs.Dispose();
        //        tv.Nodes[0].Nodes.Add(tn);
        //    }
        //    ds.Dispose();
        //    tv.ExpandAll();
        //}

        //读取压缩试验列表

        private void readMethod_C(TreeView tv)
        {
            tv.Nodes.Clear();
            tv.Nodes.Add("试验方法");
            BLL.ControlMethod bllCm = new HR_Test.BLL.ControlMethod();
            DataSet ds = bllCm.GetAllList();
            BLL.Compress bllTs = new HR_Test.BLL.Compress();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TreeNode tn = new TreeNode();
                tn.Text = ds.Tables[0].Rows[i]["methodName"].ToString();
                tn.Name = ds.Tables[0].Rows[i]["ID"].ToString();
                //for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                //{
                //    if(!string.IsNullOrEmpty(ds.Tables[0].Rows[i][j].ToString()))
                //    {
                //        TreeNode tnc = new TreeNode();
                //        tnc.Text = ds.Tables[0].Rows[i][j].ToString();
                //        tn.Nodes.Add(tnc);
                //    }
                //}
                DataSet dsTs = bllTs.GetList(" testMethodName ='" + ds.Tables[0].Rows[i]["methodName"].ToString() + "'");
                for (int j = 0; j < dsTs.Tables[0].Rows.Count; j++)
                {
                    TreeNode tnc = new TreeNode();
                    tnc.Text = dsTs.Tables[0].Rows[j]["testSampleNo"].ToString();
                    //tnc.Name = dsTs.Tables[0].Rows[j]["ID"].ToString();
                    tn.Nodes.Add(tnc);
                }
                dsTs.Dispose();
                tv.Nodes[0].Nodes.Add(tn);
            }
            ds.Dispose();
            tv.ExpandAll();
        }

        //读取弯曲试验列表
        private void readMethod_B(TreeView tv)
        {
            tv.Nodes.Clear();
            tv.Nodes.Add("试验方法");
            BLL.ControlMethod bllCm = new HR_Test.BLL.ControlMethod();
            DataSet ds = bllCm.GetAllList();
            BLL.Bend bllTs = new HR_Test.BLL.Bend();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TreeNode tn = new TreeNode();
                tn.Text = ds.Tables[0].Rows[i]["methodName"].ToString();
                tn.Name = ds.Tables[0].Rows[i]["ID"].ToString();
                //for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                //{
                //    if(!string.IsNullOrEmpty(ds.Tables[0].Rows[i][j].ToString()))
                //    {
                //        TreeNode tnc = new TreeNode();
                //        tnc.Text = ds.Tables[0].Rows[i][j].ToString();
                //        tn.Nodes.Add(tnc);
                //    }
                //}
                DataSet dsTs = bllTs.GetList(" testMethodName ='" + ds.Tables[0].Rows[i]["methodName"].ToString() + "'");
                for (int j = 0; j < dsTs.Tables[0].Rows.Count; j++)
                {
                    TreeNode tnc = new TreeNode();
                    tnc.Text = dsTs.Tables[0].Rows[j]["testSampleNo"].ToString();
                    //tnc.Name = dsTs.Tables[0].Rows[j]["ID"].ToString();
                    tn.Nodes.Add(tnc);
                }
                dsTs.Dispose();
                tv.Nodes[0].Nodes.Add(tn);
            }
            ds.Dispose();
            tv.ExpandAll();
        }

        private void readMethod(TreeView tv)
        {
            tv.Nodes.Clear();
            tv.Nodes.Add("试验方法");
            BLL.ControlMethod bllCm = new HR_Test.BLL.ControlMethod();
            DataSet ds = bllCm.GetAllList();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TreeNode tn = new TreeNode();
                tn.Text = ds.Tables[0].Rows[i]["methodName"].ToString();
                tn.Name = ds.Tables[0].Rows[i]["xmlPath"].ToString();
                //BLL.TestSample bllts = new HR_Test.BLL.TestSample();
                //DataSet bllcmList = bllts.GetList(" testMethodName ='" + ds.Tables[0].Rows[i]["methodName"].ToString() + "' and isFinish=false");
                //foreach (DataRow dr in bllcmList.Tables[0].Rows)
                //{
                //    TreeNode _tn = new TreeNode();
                //    _tn.Text = dr["testSampleNo"].ToString();
                //    _tn.Name = "tensile";
                //    tn.Nodes.Add(_tn);
                //}
                //bllcmList.Dispose();
                tv.Nodes[0].Nodes.Add(tn);
            }

            BLL.ControlMethod_C bllCm_C = new HR_Test.BLL.ControlMethod_C();
            DataSet ds_C = bllCm_C.GetAllList();
            for (int i = 0; i < ds_C.Tables[0].Rows.Count; i++)
            {
                TreeNode tn = new TreeNode();
                tn.Text = ds_C.Tables[0].Rows[i]["methodName"].ToString();
                tn.Name = ds_C.Tables[0].Rows[i]["xmlPath"].ToString();

                //BLL.Compress bllts_C = new HR_Test.BLL.Compress();
                //DataSet bllcmList = bllts_C.GetList(" testMethodName ='" + ds_C.Tables[0].Rows[i]["methodName"].ToString() + "' and isFinish=false");
                //foreach (DataRow dr in bllcmList.Tables[0].Rows)
                //{
                //    TreeNode _tn = new TreeNode();
                //    _tn.Text = dr["testSampleNo"].ToString();
                //    _tn.Name = "compress";
                //    tn.Nodes.Add(_tn);
                //}
                //bllcmList.Dispose();  
                tv.Nodes[0].Nodes.Add(tn); 
            }

            BLL.ControlMethod_B bllCm_B = new HR_Test.BLL.ControlMethod_B();
            DataSet ds_B = bllCm_B.GetAllList();
            for (int i = 0; i < ds_B.Tables[0].Rows.Count; i++)
            {
                TreeNode tn = new TreeNode();
                tn.Text = ds_B.Tables[0].Rows[i]["methodName"].ToString();
                tn.Name = ds_B.Tables[0].Rows[i]["xmlPath"].ToString();
                //BLL.Bend bllts_B = new HR_Test.BLL.Bend();
                //DataSet bllcmList = bllts_B.GetList(" testMethodName ='" + ds_B.Tables[0].Rows[i]["methodName"].ToString() + "' and isFinish=false");
                //foreach (DataRow dr in bllcmList.Tables[0].Rows)
                //{
                //    TreeNode _tn = new TreeNode();
                //    _tn.Text = dr["testSampleNo"].ToString();
                //    _tn.Name = "bend";
                //    tn.Nodes.Add(_tn);
                //}
                //bllcmList.Dispose(); 
                tv.Nodes[0].Nodes.Add(tn);
            }
            tv.ExpandAll();
        }

        private void glassButton2_Click(object sender, EventArgs e)
        {
            frmTestMethod ft = new frmTestMethod(_fmMain);
            ft.TopLevel = false;
            ft.Name = "c_testMethod";
            this.Parent.Controls.Add(ft);
            ft.BringToFront();
            //ft.WindowState = FormWindowState.Maximized;
            ft.Left = this.Width / 2 - ft.Width / 2;
            ft.Top = this.Height / 2 - ft.Height / 2 - 50;
            ft.Show();
        }

        private void gbtnDown1_Click(object sender, EventArgs e)
        {
            //this.pal1.Height += 50;
        }

        private void tsbtnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void gbtnExit_Click_1(object sender, EventArgs e)
        {

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

            if (this._sendCompany.Length == 0)
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
            
            if (this._temperature.Trim().Length == 0)
            {
                strErr += "'试验温度'不能为空！\r\n\r\n";
            }
            
            if (this._testMethod.Text.Trim().Length == 0)
            {
                strErr += "'试验方法'不能为空！\r\n\r\n";
            }
           
            if (this._sampleCharacter.Trim().Length == 0)
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
            string testStandard = this._testStandard;
            string testMethod = this._testMethod.Text;
            string mathineType = this._mathineType.Text;
            //string testCondition = this._testCondition.Text;
            string sampleCharacter = this._sampleCharacter;
            string getSample = this._getSample.Text;
            string tester = this._tester.Text;
            //string assessor = this._assessor.Text;
            //string sign = this._sign.Text;
            string a0 = (this._a0.Text.Trim().Length > 0 ? this._a0.Text : "0");
            //string au = this._au.Text;
            string b0 = (this._b0.Text.Trim().Length > 0 ? this._b0.Text : "0");
            //string bu = this._bu.Text;
            string d0 = (this._d0.Text.Trim().Length > 0 ? this._d0.Text : "0");
            //string du = this._du.Text;
            string Do = (this._Do.Text.Trim().Length > 0 ? this._Do.Text : "0");
            string L0 = (this._L0.Text.Trim().Length > 0 ? this._L0.Text : "0");
            string L01 = (this._L01.Text.Trim().Length > 0 ? this._L01.Text : "0");
            string Lc = (this._Lc.Text.Trim().Length > 0 ? this._Lc.Text : "0");
            string Le = (this._Le.Text.Trim().Length > 0 ? this._Le.Text : "0");
            string Lt = (this._Lt.Text.Trim().Length > 0 ? this._Lt.Text : "0");
            //string Lu = this._Lu.Text;
            //string Lu1 = this._Lu1.Text;
            string S0 = this._S0.Text;
            //string Su = this._Su.Text;
            //string k = this._k.Text;
            //string Fm = this._Fm.Text;
            //string Rm = this._Rm.Text;
            //string ReH = this._ReH.Text;
            //string ReL = this._ReL.Text;
            //string Rp = this._Rp.Text;
            //string Rt = this._Rt.Text;
            //string Rr = this._Rr.Text;
            string εp = (this._εp.Text.Trim().Length > 0 ? this._εp.Text : "0");
            string εt = (this._εt.Text.Trim().Length > 0 ? this._εt.Text : "0");
            string εr = (this._εr.Text.Trim().Length > 0 ? this._εr.Text : "0");
            //string E = this._E.Text;
            //string m = this._m.Text;
            //string mE = this._mE.Text;
            //string A = this._A.Text;
            //string Aee = this._Aee.Text;
            //string Agg = this._Agg.Text;
            //string Att = this._Att.Text;
            //string Aggtt = this._Aggtt.Text;
            //string Awnwn = this._Awnwn.Text;
            //string Lm = this._Lm.Text;
            //string Lf = this._Lf.Text;
            //string Z = this._Z.Text;
            //string Avera = this._Avera.Text;
            //string SS = this._SS.Text;
            //string Avera1 = this._Avera1.Text;
            //bool isFinish = this.chkisFinish.Checked; 

            Model.TestSample model = new Model.TestSample();
            //model.testMethodID = testMethodID;
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
            model.Lt = double.Parse(Lt);
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
            model.Lf = 0;//Lf;
            model.Z = 0;//Z;
            model.Avera = 0;// Avera;
            model.SS = 0;//SS;
            model.Avera1 = 0;//Avera1;
            model.isFinish = false;
            model.testDate = this._testDate.Value.Date;
            BLL.TestSample bll = new BLL.TestSample();
            if (bll.GetList("testSampleNo ='" + this._testSampleNo.Text + "'").Tables[0].Rows.Count == 0)
            {
                bll.Add(model);
                this._testSampleNo.Items.Remove(this._testSampleNo.SelectedItem);
                //this._a0.Text = "";
                //this._b0.Text = "";
                //this._d0.Text = "";
                //this._Do.Text = "";
                //this._S0.Text = "";
                MessageBox.Show("添加试样信息成功!"); 
            }
            else
            {
                MessageBox.Show("已经存在相同的编号，请重新设置!");
                return;
            }
            
           // readMethod(this.tvTestMethod);
        }

        private void _TestNum_OnTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this._TestNum.Text))
            {
                if (utils.IsNumeric(this._TestNum.Text) && !string.IsNullOrEmpty(this._TestNum.Text))
                {
                    this._testSampleNo.Items.Clear();
                    int num = int.Parse(this._TestNum.Text);
                    for (int i = 0; i < num; i++)
                    {
                        this._testSampleNo.Items.Add(this._testNo.Text + "-" + (i + 1).ToString());
                    }
                }
                else
                {
                    this._TestNum.Text = "";
                }
            }
        }

        private void _testNo_OnTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this._testNo.Text))
            {
                if (utils.IsNumeric(this._TestNum.Text) && !string.IsNullOrEmpty(this._TestNum.Text))
                {
                    this._testSampleNo.Items.Clear();
                    int num = int.Parse(this._TestNum.Text);
                    for (int i = 0; i < num; i++)
                    {
                        this._testSampleNo.Items.Add(this._testNo.Text + "-" + (i + 1).ToString());
                    }
                }
                else
                {
                    this._TestNum.Text = "";
                }
            }
        }

        private void _ReadMethod(string methodName)
        {
            BLL.ControlMethod bllts = new HR_Test.BLL.ControlMethod();
            DataSet ds = bllts.GetList("MethodName ='" + methodName + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                _testNo.Text = "";
                _TestNum.Text = "";

                _sendCompany = ds.Tables[0].Rows[0]["sendCompany"].ToString();
                _stuffCardNo.Text = ds.Tables[0].Rows[0]["stuffCardNo"].ToString();
                _stuffSpec.Text = ds.Tables[0].Rows[0]["stuffSpec"].ToString();
                _stuffType.Text = ds.Tables[0].Rows[0]["stuffType"].ToString();
                _hotStatus = ds.Tables[0].Rows[0]["hotStatus"].ToString();
                _temperature = ds.Tables[0].Rows[0]["temperature"].ToString();
                _humidity = ds.Tables[0].Rows[0]["humidity"].ToString();
                _sampleCharacter = ds.Tables[0].Rows[0]["sampleCharacter"].ToString();
                _testStandard = ds.Tables[0].Rows[0]["testStandard"].ToString();
                _sign.Text = ds.Tables[0].Rows[0]["sign"].ToString();
                _L0.Text = ds.Tables[0].Rows[0]["L0"].ToString();
                _L01.Text = ds.Tables[0].Rows[0]["L01"].ToString();
                _Lc.Text = ds.Tables[0].Rows[0]["Lc"].ToString();
                _Le.Text = ds.Tables[0].Rows[0]["Le"].ToString();
                _Lt.Text = ds.Tables[0].Rows[0]["Lt"].ToString();

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
            }
            ds.Dispose();
        }

        private void tvTestMethod_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Node.Name))
            {                
                switch (e.Node.Name)
                {
                    case "tensile": 
                        this._testMethod.Text = e.Node.Parent.Text;
                        break;
                    case "compress":
                        this.c_testMethod.Text = e.Node.Parent.Text;
                        break;
                    case "bend":
                        this.b_testMethod.Text = e.Node.Parent.Text;
                        break;
                }               
            }
  
            BLL.ControlMethod_C bllts_C = new HR_Test.BLL.ControlMethod_C();
            BLL.ControlMethod_B bllts_B = new HR_Test.BLL.ControlMethod_B();
            if (!string.IsNullOrEmpty(e.Node.Name))
            {
                this.tvTestMethod.SelectedNode = e.Node;
                //e.Node.Name 试验类型标识 1：拉伸 2：压缩 3：弯曲
                switch (e.Node.Name.ToString())
                {
                    #region 拉伸试验方法 
                    //case "lashen":
                    case "1":
                        this.gbTensile.Dock = DockStyle.Fill;
                        this.gbTensile.BringToFront();
                        _ReadMethod(e.Node.Text);
                        break;
                    #endregion

                    #region 压缩试验方法
                    case "2":
                        this.gbCompress.Dock = DockStyle.Fill;
                        this.gbCompress.BringToFront();
                        DataSet ds_C = bllts_C.GetList("MethodName ='" + e.Node.Text + "'");
                        if (ds_C.Tables[0].Rows.Count > 0)
                        {
                            c_testNo.Text = "";
                            c_testNum.Text = "";

                            c_sendCompany.Text = ds_C.Tables[0].Rows[0]["sendCompany"].ToString();
                            c_stuffCardNo.Text = ds_C.Tables[0].Rows[0]["stuffCardNo"].ToString();
                            c_stuffSpec.Text = ds_C.Tables[0].Rows[0]["stuffSpec"].ToString();
                            c_stuffType.Text = ds_C.Tables[0].Rows[0]["stuffType"].ToString();
                            c_hotStatus.Text = ds_C.Tables[0].Rows[0]["hotStatus"].ToString();
                            c_temperature.Text = ds_C.Tables[0].Rows[0]["temperature"].ToString();
                            c_humidity.Text = ds_C.Tables[0].Rows[0]["humidity"].ToString();
                            c_sampleCharacter.Text = ds_C.Tables[0].Rows[0]["sampleCharacter"].ToString();
                            c_testStandard.Text = ds_C.Tables[0].Rows[0]["testStandard"].ToString();
                            c_sign.Text = ds_C.Tables[0].Rows[0]["sign"].ToString();

                            c_L.Text = ds_C.Tables[0].Rows[0]["L"].ToString();
                            c_L0.Text = ds_C.Tables[0].Rows[0]["L0"].ToString();
                            c_HH.Text = ds_C.Tables[0].Rows[0]["H"].ToString();
                            c_h.Text = ds_C.Tables[0].Rows[0]["hh"].ToString();

                            c_getSample.Text = ds_C.Tables[0].Rows[0]["getSample"].ToString();
                            c_a.Text = "";
                            c_b.Text = "";
                            c_d.Text = "";
                            c_S0.Text = "";

                            c_Ff.Text = ds_C.Tables[0].Rows[0]["Ff"].ToString();
                            c_mathineType.Text = ds_C.Tables[0].Rows[0]["mathineType"].ToString();
                            c_tester.Text = ds_C.Tables[0].Rows[0]["tester"].ToString();
                            c_testMethod.Text = ds_C.Tables[0].Rows[0]["testMethod"].ToString();
                        }
                        ds_C.Dispose();
                        break;
                    #endregion

                    #region 弯曲试验方法
                    case "3":
                    //case "wanqu":
                        this.gbBend.Dock = DockStyle.Fill;
                        this.gbBend.BringToFront();
                            DataSet ds_B = bllts_B.GetList("MethodName ='" + e.Node.Text + "'");
                            if (ds_B.Tables[0].Rows.Count > 0)
                            {
                                b_testNo.Text = "";
                                b_testNum.Text = "";

                                b_sendCompany.Text = ds_B.Tables[0].Rows[0]["sendCompany"].ToString();
                                b_stuffCardNo.Text = ds_B.Tables[0].Rows[0]["stuffCardNo"].ToString();
                                b_stuffSpec.Text = ds_B.Tables[0].Rows[0]["stuffSpec"].ToString();
                                b_stuffType.Text = ds_B.Tables[0].Rows[0]["stuffType"].ToString();
                                b_hotStatus.Text = ds_B.Tables[0].Rows[0]["hotStatus"].ToString();
                                b_temperature.Text = ds_B.Tables[0].Rows[0]["temperature"].ToString();
                                b_humidity.Text = ds_B.Tables[0].Rows[0]["humidity"].ToString();
                                b_sampleCharacter.Text = ds_B.Tables[0].Rows[0]["sampleCharacter"].ToString();
                                b_testStandard.Text = ds_B.Tables[0].Rows[0]["testStandard"].ToString();
                                b_sign.Text = ds_B.Tables[0].Rows[0]["sign"].ToString();

                                b_L.Text = ds_B.Tables[0].Rows[0]["L"].ToString();
                                b_ll.Text = ds_B.Tables[0].Rows[0]["ll"].ToString();
                                b_D.Text = ds_B.Tables[0].Rows[0]["D"].ToString();
                                b_c.Text = ds_B.Tables[0].Rows[0]["c"].ToString();
                                b_p.Text = ds_B.Tables[0].Rows[0]["p"].ToString();

                                b_getSample.Text = ds_B.Tables[0].Rows[0]["getSample"].ToString();
                                b_a.Text = "";
                                b_b.Text = "";
                                b_S0.Text = "";

                                b_mathineType.Text = ds_B.Tables[0].Rows[0]["mathineType"].ToString();
                                b_tester.Text = ds_B.Tables[0].Rows[0]["tester"].ToString();
                                b_testMethod.Text = ds_B.Tables[0].Rows[0]["testMethod"].ToString();
                             
                            }
                            ds_B.Dispose();
                            break;
                    #endregion

                    default:
                            switch (e.Node.Parent.Name)
                            {
                                case "1":
                                    this.gbTensile.Dock = DockStyle.Fill;
                                    this.gbTensile.BringToFront();

                                    break;
                                case "2":
                                    this.gbCompress.Dock = DockStyle.Fill;
                                    this.gbCompress.BringToFront();
                                    break;
                                case "3":
                                    this.gbBend.Dock = DockStyle.Fill;
                                    this.gbBend.BringToFront();
                                    break;
                            }
                            break;
                } 
            }
        }

        private void _a0_TextChanged(object sender, EventArgs e)
        {
            TextBox uf = (TextBox)sender;
            if (!utils.IsNumeric(uf.Text))
                uf.Text = "";
        }

        private void _temperature_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(_temperature))
                _temperature = "";
        }

        private void _humidity_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(_humidity))
                _humidity = "";
        }

        private void _L0_OnTextChanged_1(object sender, EventArgs e)
        {

        }

        private void _Lc_OnTextChanged(object sender, EventArgs e)
        {

        }

        private void _Le_OnTextChanged(object sender, EventArgs e)
        {

        }

        private void _Lt_OnTextChanged(object sender, EventArgs e)
        {

        }

        private void _L01_OnTextChanged(object sender, EventArgs e)
        {

        }

        private void _εp_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(this._εp.Text))
                _εp.Text = "";
        }

        private void _εt_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(this._εt.Text))
                _εt.Text = "";
        }

        private void _εr_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(this._εr.Text))
                _εr.Text = "";
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

        private void gBtnDelete_Click(object sender, EventArgs e)
        {
            if (this.tvTestMethod.SelectedNode != null)
            {
                if (DialogResult.OK == MessageBox.Show("是否删除该编号?", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    switch (this.tvTestMethod.SelectedNode.Name)
                    {
                        case "tensile":
                            BLL.TestSample bllts = new HR_Test.BLL.TestSample();
                            bllts.Delete(this.tvTestMethod.SelectedNode.Text);
                            readMethod(this.tvTestMethod);
                            break;
                        case "compress":
                            BLL.Compress bllCp = new HR_Test.BLL.Compress();
                            bllCp.Delete(this.tvTestMethod.SelectedNode.Text);
                            readMethod(this.tvTestMethod);
                            break;
                        case "bend":
                            BLL.Bend bllBend = new HR_Test.BLL.Bend();
                            bllBend.Delete(this.tvTestMethod.SelectedNode.Text);
                            readMethod(this.tvTestMethod);
                            break;
                    }
                }
              
            }
        } 

        //private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        //{
            //switch (tabControl.SelectedIndex)
            //{
            //    case 0:
            //        readMethod(this.tvTestMethod);
            //        this.tvTestMethod.Visible = true;
            //        this.c_tvTestMethod.Visible = false;
            //        this.b_tvTestMethod.Visible = false;
            //        this.tvTestMethod.Dock = DockStyle.Fill;
            //        break;
            //    case 1:
            //        readMethod_C(this.c_tvTestMethod);
            //        this.tvTestMethod.Visible = false;
            //        this.c_tvTestMethod.Visible = true;
            //        this.b_tvTestMethod.Visible = false;
            //        this.c_tvTestMethod.Dock = DockStyle.Fill;
            //        break;
            //    case 2:
            //        readMethod_B(this.b_tvTestMethod);
            //        this.tvTestMethod.Visible = false;
            //        this.c_tvTestMethod.Visible = false;
            //        this.b_tvTestMethod.Visible = true;
            //        this.b_tvTestMethod.Dock = DockStyle.Fill;
            //        break;
            //}
        //}

        private void rBtnRect_CheckedChanged(object sender, EventArgs e)
        {
            if (rBtnRect.Checked)
            {
                this.c_a.Enabled = true;
                this.c_b.Enabled = true;
                this.c_d.Enabled = false;
                this.c_d.Text = "";
            }
        }

        private void rBtnCircle_CheckedChanged(object sender, EventArgs e)
        {
            if (rBtnCircle.Checked)
            {
                this.c_a.Enabled = false;
                this.c_b.Enabled = false;
                this.c_d.Enabled = true;
                this.c_a.Text = this.c_b.Text = "";
            }
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

            if (this.c_sendCompany.Text.Trim().Length == 0)
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

            if (this.c_temperature.Text.Trim().Length == 0)
            {
                strErr += "试验温度不能为空！\r\n\r\n";
            }

            if (this.c_testMethod.Text.Trim().Length == 0)
            {
                strErr += "试验方法不能为空！\r\n\r\n";
            }

            if (this.c_sampleCharacter.Text.Trim().Length == 0)
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
            model.sendCompany =this.c_sendCompany.Text;
            model.stuffCardNo = this.c_stuffCardNo.Text;
            model.stuffSpec = c_stuffSpec.Text;
            model.stuffType = c_stuffType.Text;
            model.hotStatus = c_hotStatus.Text;
            model.temperature = double.Parse(this.c_temperature.Text);
            model.humidity = double.Parse(c_humidity.Text);
            model.testStandard = c_testStandard.Text;
            model.testMethod = c_testMethod.Text;
            model.mathineType = c_mathineType.Text;
            model.testCondition = "-";
            model.sampleCharacter =c_sampleCharacter.Text;
            model.getSample = c_getSample.Text;
            model.tester = c_tester.Text;
            model.assessor = "-";
            model.sign =this.c_sign.Text;

            model.a = (this.c_a.Text.Trim().Length > 0 ? double.Parse(c_a.Text) : 0);
            model.b = (this.c_b.Text.Trim().Length > 0 ? double.Parse(c_b.Text) : 0);
            model.d = (this.c_d.Text.Trim().Length > 0 ? double.Parse(c_d.Text) : 0);
            model.L =(this.c_L.Text.Trim().Length > 0 ?  double.Parse(c_L.Text) : 0);
            model.L0 = (this.c_L0.Text.Trim().Length > 0 ?  double.Parse(c_L0.Text) : 0);
            model.H = (this.c_HH.Text.Trim().Length > 0 ?  double.Parse(c_HH.Text) : 0);
            model.hh =(this.c_h.Text.Trim().Length > 0 ?  double.Parse(c_h.Text) : 0);
            model.S0 = double.Parse(c_S0.Text);
            model.Ff = (this.c_Ff.Text.Trim().Length > 0 ? double.Parse(c_Ff.Text) : 0);  

            model.deltaL = 0;
            model.εpc = 0;
            model.εtc = 0;
            model.n = 0;
            model.F0 =0;
           
            model.Fpc =0;
            model.Ftc = 0;
            model.FeHc = 0;
            model.FeLc = 0;
            model.Fmc = 0;
            model.Rpc = 0;
            model.Rtc =0;
            model.ReHc = 0;
            model.ReLc = 0;
            model.Rmc =0;
            model.Ec =0;
            model.Avera =0;
            model.Avera1 =0;
            model.isFinish = false;
            model.testDate = this.c_testDate.Value.Date;
            BLL.Compress bllc = new HR_Test.BLL.Compress();
            if (bllc.GetList("testSampleNo ='" + this.c_testSampleNo.Text + "'").Tables[0].Rows.Count == 0)
            {
                if (bllc.Add(model))
                {
                    this.c_testSampleNo.Items.Remove(this.c_testSampleNo.SelectedItem);
                    this.c_a.Text = "";
                    this.c_b.Text = "";
                    this.c_d.Text = "";
                    this.c_S0.Text = "";
                    MessageBox.Show( "添加压缩试样信息成功!","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("添加压缩试样信息失败!","警告",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show( "已经存在相同的编号，请重新设置!","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //readMethod(this.tvTestMethod);
        }

        private void c_cmbTestSampleNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void c_temperature_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(c_temperature.Text))
                c_temperature.Text = "";
        }

        private void c_humidity_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(c_humidity.Text))
                c_humidity.Text = "";
        }

        private void c_L0_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(c_L0.Text))
                c_L0.Text = "";
        }

        private void c_L_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(c_L.Text))
                c_L.Text = "";
        }

        private void c_HH_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(c_HH.Text))
                c_HH.Text = "";
        }

        private void c_h_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(c_h.Text))
                c_h.Text = "";
        }

        private void c_Ff_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(c_Ff.Text))
                c_Ff.Text = "";
        }

        private void c_testNum_OnTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.c_testNum.Text))
            {
                if (utils.IsNumeric(this.c_testNum.Text) && !string.IsNullOrEmpty(this.c_testNum.Text))
                {
                    this.c_testSampleNo.Items.Clear();
                    int num = int.Parse(this.c_testNum.Text);
                    for (int i = 0; i < num; i++)
                    {
                        this.c_testSampleNo.Items.Add(this.c_testNo.Text + "-" + (i + 1).ToString());
                    }
                }
                else
                {
                    this.c_testNum.Text = "";
                }
            }
        }

        private void c_testNo_OnTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.c_testNo.Text))
            {
                if (utils.IsNumeric(this.c_testNum.Text) && !string.IsNullOrEmpty(this.c_testNum.Text))
                {
                    this.c_testSampleNo.Items.Clear();
                    int num = int.Parse(this.c_testNum.Text);
                    for (int i = 0; i < num; i++)
                    {
                        this.c_testSampleNo.Items.Add(this.c_testNo.Text + "-" + (i + 1).ToString());
                    }
                }
                else
                {
                    this.c_testNum.Text = "";
                }
            }
        }

        private void c_tvTestMethod_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Node.Name))
            {
                this.c_testMethod.Text = e.Node.Text;
            }

            if (string.IsNullOrEmpty(e.Node.Name) && e.Node.Text != "试验方法")
            {
                BLL.Compress bllts = new HR_Test.BLL.Compress();
                DataSet ds = bllts.GetList("testSampleNo ='" + e.Node.Text + "'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    c_testNo.Text = "";
                    c_testNum.Text = "";

                    c_sendCompany.Text = ds.Tables[0].Rows[0]["sendCompany"].ToString();
                    c_stuffCardNo.Text = ds.Tables[0].Rows[0]["stuffCardNo"].ToString();
                    c_stuffSpec.Text = ds.Tables[0].Rows[0]["stuffSpec"].ToString();
                    c_stuffType.Text = ds.Tables[0].Rows[0]["stuffType"].ToString();
                    c_hotStatus.Text = ds.Tables[0].Rows[0]["hotStatus"].ToString();
                    c_temperature.Text = ds.Tables[0].Rows[0]["temperature"].ToString();
                    c_humidity.Text = ds.Tables[0].Rows[0]["humidity"].ToString();
                    c_sampleCharacter.Text = ds.Tables[0].Rows[0]["sampleCharacter"].ToString();
                    c_testStandard.Text = ds.Tables[0].Rows[0]["testStandard"].ToString();
                    c_sign.Text = ds.Tables[0].Rows[0]["sign"].ToString(); 

                    c_L.Text = ds.Tables[0].Rows[0]["L"].ToString();
                    c_L0.Text = ds.Tables[0].Rows[0]["L0"].ToString();
                    c_HH.Text = ds.Tables[0].Rows[0]["H"].ToString();
                    c_h.Text = ds.Tables[0].Rows[0]["hh"].ToString();

                    c_getSample.Text = ds.Tables[0].Rows[0]["getSample"].ToString();
                    c_a.Text = "";
                    c_b.Text = "";
                    c_d.Text = "";
                    c_S0.Text = "";

                    c_Ff.Text = ds.Tables[0].Rows[0]["Ff"].ToString(); 

                    c_mathineType.Text = ds.Tables[0].Rows[0]["mathineType"].ToString();
                    c_tester.Text = ds.Tables[0].Rows[0]["tester"].ToString();
                    c_testMethod.Text = ds.Tables[0].Rows[0]["testMethod"].ToString(); 

                }
                ds.Dispose();
            }
        }

        private void gBtnDelC_Click(object sender, EventArgs e)
        {

        }

        private void b_TestNum_OnTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.b_testNum.Text))
            {
                if (utils.IsNumeric(this.b_testNum.Text) && !string.IsNullOrEmpty(this.b_testNum.Text))
                {
                    this.b_testSampleNo.Items.Clear();
                    int num = int.Parse(this.b_testNum.Text);
                    for (int i = 0; i < num; i++)
                    {
                        this.b_testSampleNo.Items.Add(this.b_testNo.Text + "-" + (i + 1).ToString());
                    }
                }
                else
                {
                    this.b_testNum.Text = "";
                }
            }
        }

        private void b_TestNo_OnTextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.b_testNo.Text))
            {
                if (utils.IsNumeric(this.b_testNum.Text) && !string.IsNullOrEmpty(this.b_testNum.Text))
                {
                    this.b_testSampleNo.Items.Clear();
                    int num = int.Parse(this.b_testNum.Text);
                    for (int i = 0; i < num; i++)
                    {
                        this.b_testSampleNo.Items.Add(this.b_testNo.Text + "-" + (i + 1).ToString());
                    }
                }
                else
                {
                    this.b_testNum.Text = "";
                }
            }
        }

        private void b_temperature_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(b_temperature.Text))
                b_temperature.Text = "";
        }

        private void b_humidity_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(b_humidity.Text))
                b_humidity.Text = "";
        }

        private void b_L_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(b_L.Text))
                b_L.Text = "";
        }

        private void b_ll_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(b_ll.Text))
                b_ll.Text = "";
        }

        private void b_D_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(b_D.Text))
                b_D.Text = "";
        }

        private void b_c_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(b_c.Text))
                b_c.Text = "";
        }

        private void b_p_OnTextChanged(object sender, EventArgs e)
        {
            if (!utils.IsNumeric(b_p.Text))
                b_p.Text = "";
        }

        private void gBtnAddB_Click(object sender, EventArgs e)
        {
            string strErr = "";

            if (this.b_testNo.Text.Trim().Length == 0)
            {
                strErr += "试验编号不能为空！\r\n\r\n";
            }
            if (this.b_testSampleNo.Text.Trim().Length == 0)
            {
                strErr += "试样编号不能为空！\r\n\r\n";
            }

            if (this.b_sendCompany.Text.Trim().Length == 0)
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

            if (this.b_temperature.Text.Trim().Length == 0)
            {
                strErr += "试验温度不能为空！\r\n\r\n";
            }

            if (this.b_testMethod.Text.Trim().Length == 0)
            {
                strErr += "试验方法不能为空！\r\n\r\n";
            }

            if (this.b_sampleCharacter.Text.Trim().Length == 0)
            {
                strErr += "试样标识不能为空！\r\n\r\n";
            }
            if (this.b_getSample.Text.Trim().Length == 0)
            {
                strErr += "试样取样不能为空！\r\n\r\n";
            }
            if (this.b_tester.Text.Trim().Length == 0)
            {
                strErr += "试验员不能为空！\r\n\r\n";
            }

            if (rbtnRectB.Checked)
            {
                if (this.b_a.Text.Trim().Length == 0)
                {
                    strErr += "a不能为空！\r\n\r\n";
                }
                if (this.b_b.Text.Trim().Length == 0)
                {
                    strErr += "b不能为空！\r\n\r\n";
                }
            }
 

            if (this.b_S0.Text.Trim().Length == 0)
            {
                strErr += "S0不能为空！\r\n\r\n";
            }
            else
            {
                if (double.Parse(this.b_S0.Text.Trim()) == 0d)
                {
                    strErr += "S0不能为0！\r\n\r\n";
                }
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            HR_Test.Model.Bend model = new HR_Test.Model.Bend();
            model.testMethodName = this.b_testMethod.Text;
            model.testNo = this.b_testNo.Text;
            model.testSampleNo = this.b_testSampleNo.Text;
            model.reportNo = "-";
            model.sendCompany = this.b_sendCompany.Text;
            model.stuffCardNo = this.b_stuffCardNo.Text;
            model.stuffSpec = b_stuffSpec.Text;
            model.stuffType = b_stuffType.Text;
            model.hotStatus = b_hotStatus.Text;
            model.temperature = double.Parse(this.b_temperature.Text);
            model.humidity = double.Parse(b_humidity.Text);
            model.testStandard = b_testStandard.Text;
            model.testMethod = b_testMethod.Text;
            model.mathineType = b_mathineType.Text;
            model.testCondition = "-";
            model.sampleCharacter = b_sampleCharacter.Text;
            model.getSample = b_getSample.Text;
            model.tester = b_tester.Text;
            model.assessor = "-";
            model.sign = this.b_sign.Text;

            model.a = (this.b_a.Text.Trim().Length > 0 ? double.Parse(b_a.Text) : 0);
            model.b = (this.b_b.Text.Trim().Length > 0 ? double.Parse(b_b.Text) : 0);           
            model.L = (this.b_L.Text.Trim().Length > 0 ? double.Parse(b_L.Text) : 0);
            model.ll = (this.b_ll.Text.Trim().Length > 0 ? double.Parse(b_ll.Text) : 0);
            model.D = (this.b_D.Text.Trim().Length > 0 ? double.Parse(b_D.Text) : 0);
            model.c = (this.b_c.Text.Trim().Length > 0 ? double.Parse(b_c.Text) : 0);
            model.p = (this.b_p.Text.Trim().Length > 0 ? double.Parse(b_p.Text) : 0);
       
            //  "α double," +//弯曲角度
            //  "r double," + //试样弯曲后的弯曲半径
            //  "f double," + //弯曲压头的移动距离 

            //  "Avera double not null," + //平均值
            //  "Avera1 double not null," + //去掉最大最小值的平均值

            //   "isFinish bit," +//是否完成试验
            //   "isConformity bit," +//是否合格
            //  "testDate date" + //试验日期 
            model.α = 0;
            model.r = 0;
            model.f = 0; 
            model.Avera = 0;
            model.Avera1 = 0;
            model.isFinish = false;
            model.testDate = this.b_testDate.Value.Date;

            BLL.Bend bllc = new HR_Test.BLL.Bend();
            if (bllc.GetList("testSampleNo ='" + this.b_testSampleNo.Text + "'").Tables[0].Rows.Count == 0)
            {
                bllc.Add(model);
                this.b_testSampleNo.Items.Remove(this.b_testSampleNo.SelectedItem);
                this.b_a.Text = "";
                this.b_b.Text = ""; 
                this.b_S0.Text = "";
                MessageBox.Show("添加弯曲试样信息成功!");
            }
            else
            {
                MessageBox.Show("已经存在相同的编号，请重新设置!");
                return;
            }
            //readMethod(this.tvTestMethod);

        }

        private void gBtnDelB_Click(object sender, EventArgs e)
        {
            //if (this.b_tvTestMethod.SelectedNode != null)
            //{
            //    if (string.IsNullOrEmpty(this.b_tvTestMethod.SelectedNode.Name) && this.b_tvTestMethod.SelectedNode.Text != "试验方法")
            //    {
            //        if (DialogResult.OK == MessageBox.Show("是否删除该弯曲试验试样编号?", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            //        {
            //            BLL.Bend bllts = new HR_Test.BLL.Bend();
            //            bllts.Delete(this.b_tvTestMethod.SelectedNode.Text);
            //            readMethod_B(this.b_tvTestMethod);
            //        }
            //    }
            //}
        }

        private void b_caculate_Click(object sender, EventArgs e)
        {
            //矩形试样
            if (rbtnRectB.Checked)
            {
                if (b_a.Text.Trim().Length > 0 && b_b.Text.Trim().Length > 0)
                {
                    this.b_S0.Text = (double.Parse(b_a.Text.Trim()) * double.Parse(b_b.Text.Trim())).ToString("0.0000");
                }
            }
        }

        private void b_tvTestMethod_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Node.Name))
            {
                this.b_testMethod.Text = e.Node.Text;
            }

            if (string.IsNullOrEmpty(e.Node.Name) && e.Node.Text != "试验方法")
            {
                BLL.Bend bllts = new HR_Test.BLL.Bend();
                DataSet ds = bllts.GetList("testSampleNo ='" + e.Node.Text + "'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    b_testNo.Text = "";
                    b_testNum.Text = "";

                    b_sendCompany.Text = ds.Tables[0].Rows[0]["sendCompany"].ToString();
                    b_stuffCardNo.Text = ds.Tables[0].Rows[0]["stuffCardNo"].ToString();
                    b_stuffSpec.Text = ds.Tables[0].Rows[0]["stuffSpec"].ToString();
                    b_stuffType.Text = ds.Tables[0].Rows[0]["stuffType"].ToString();
                    b_hotStatus.Text = ds.Tables[0].Rows[0]["hotStatus"].ToString();
                    b_temperature.Text = ds.Tables[0].Rows[0]["temperature"].ToString();
                    b_humidity.Text = ds.Tables[0].Rows[0]["humidity"].ToString();
                    b_sampleCharacter.Text = ds.Tables[0].Rows[0]["sampleCharacter"].ToString();
                    b_testStandard.Text = ds.Tables[0].Rows[0]["testStandard"].ToString();
                    b_sign.Text = ds.Tables[0].Rows[0]["sign"].ToString();

                    b_L.Text = ds.Tables[0].Rows[0]["L"].ToString();
                    b_ll.Text = ds.Tables[0].Rows[0]["ll"].ToString();
                    b_D.Text = ds.Tables[0].Rows[0]["D"].ToString();
                    b_c.Text = ds.Tables[0].Rows[0]["c"].ToString();
                    b_p.Text = ds.Tables[0].Rows[0]["p"].ToString(); 

                    b_getSample.Text = ds.Tables[0].Rows[0]["getSample"].ToString();
                    b_a.Text = "";
                    b_b.Text = "";
                    b_S0.Text = ""; 

                    b_mathineType.Text = ds.Tables[0].Rows[0]["mathineType"].ToString();
                    b_tester.Text = ds.Tables[0].Rows[0]["tester"].ToString();
                    b_testMethod.Text = ds.Tables[0].Rows[0]["testMethod"].ToString();

                }
                ds.Dispose();
            }
        } 

        //添加至拉伸试验方法表
        private void gBtnAddToMethod1_Click(object sender, EventArgs e)
        {
            string strErr = ""; 

            if (this._sendCompany.Trim().Length == 0)
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

            if (this._temperature.Trim().Length == 0)
            {
                strErr += "试验温度不能为空！\r\n\r\n";
            }

            if (this._testMethod.Text.Trim().Length == 0)
            {
                strErr += "试验方法不能为空！\r\n\r\n";
            }

            if (this._sampleCharacter.Trim().Length == 0)
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
            //string reportNo = this._reportNo.Text;
            string sendCompany = this._sendCompany;
            string stuffCardNo = this._stuffCardNo.Text;
            string stuffSpec = this._stuffSpec.Text;
            string stuffType = this._stuffType.Text;
            string hotStatus = this._hotStatus;
            string temperature = this._temperature;
            string humidity = this._humidity;
            string testStandard = this._testStandard;
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
            string L01 = (this._L01.Text.Trim().Length > 0 ? this._L01.Text : "0");
            string Lc = (this._Lc.Text.Trim().Length > 0 ? this._Lc.Text : "0");
            string Le = (this._Le.Text.Trim().Length > 0 ? this._Le.Text : "0");
            string Lt = (this._Lt.Text.Trim().Length > 0 ? this._Lt.Text : "0");
 
            string S0 = this._S0.Text;
            
            string εp = (this._εp.Text.Trim().Length > 0 ? this._εp.Text : "0");
            string εt = (this._εt.Text.Trim().Length > 0 ? this._εt.Text : "0");
            string εr = (this._εr.Text.Trim().Length > 0 ? this._εr.Text : "0"); 

            BLL.ControlMethod bllcm = new HR_Test.BLL.ControlMethod(); 
            Model.ControlMethod model =bllcm.GetModel(this._testMethod.Text);  
            //model.testMethodID = testMethodID;
            
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
            model.au = double.Parse(εp);
            model.bu = double.Parse(εt);
            model.du = double.Parse(εr);

            //"L0 double," + //原始标距
            model.L0 = double.Parse(L0);
            //"L01 double" + //测定Awn的原始标距
            model.L01 = double.Parse(L01);
            //"Lc double," + //平行长度
            model.Lc = double.Parse(Lc);
            //"Le double," + //引伸计标距
            model.Le = double.Parse(Le);
            //"Lt double," + //试样总长度
            model.Lt =double.Parse(Lt);
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
                MessageBox.Show("更新成功!");
            } 
        }

        private void gBtnAddToMethod2_Click(object sender, EventArgs e)
        {
            string strErr = ""; 

            if (this.c_sendCompany.Text.Trim().Length == 0)
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

            if (this.c_temperature.Text.Trim().Length == 0)
            {
                strErr += "试验温度不能为空！\r\n\r\n";
            }

            if (this.c_testMethod.Text.Trim().Length == 0)
            {
                strErr += "试验方法不能为空！\r\n\r\n";
            }

            if (this.c_sampleCharacter.Text.Trim().Length == 0)
            {
                strErr += "试样标识不能为空！\r\n\r\n";
            }
            if (this.c_getSample.Text.Trim().Length == 0)
            {
                strErr += "试样取样不能为空！\r\n\r\n";
            } 

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            BLL.ControlMethod_C bllcm_C = new HR_Test.BLL.ControlMethod_C(); 
            HR_Test.Model.ControlMethod_C model = bllcm_C.GetModel(this.c_testMethod.Text);
             
            model.sendCompany = this.c_sendCompany.Text;
            model.stuffCardNo = this.c_stuffCardNo.Text;
            model.stuffSpec = c_stuffSpec.Text;
            model.stuffType = c_stuffType.Text;
            model.hotStatus = c_hotStatus.Text;
            model.temperature = double.Parse(this.c_temperature.Text);
            model.humidity = double.Parse(c_humidity.Text);
            model.testStandard = c_testStandard.Text;
            model.testMethod = c_testMethod.Text;
            model.mathineType = c_mathineType.Text;
            model.testCondition = "-";
            model.sampleCharacter = c_sampleCharacter.Text;
            model.getSample = c_getSample.Text;
            model.tester = c_tester.Text;
            model.assessor = "-";
            model.sign = this.c_sign.Text;

            //model.a = (this.c_a.Text.Trim().Length > 0 ? double.Parse(c_a.Text) : 0);
            //model.b = (this.c_b.Text.Trim().Length > 0 ? double.Parse(c_b.Text) : 0);
            //model.d = (this.c_d.Text.Trim().Length > 0 ? double.Parse(c_d.Text) : 0);
            model.L = (this.c_L.Text.Trim().Length > 0 ? double.Parse(c_L.Text) : 0);
            model.L0 = (this.c_L0.Text.Trim().Length > 0 ? double.Parse(c_L0.Text) : 0);
            model.H = (this.c_HH.Text.Trim().Length > 0 ? double.Parse(c_HH.Text) : 0);
            model.hh = (this.c_h.Text.Trim().Length > 0 ? double.Parse(c_h.Text) : 0);
            //model.S0 = double.Parse(c_S0.Text);
            model.Ff = (this.c_Ff.Text.Trim().Length > 0 ? double.Parse(c_Ff.Text) : 0);

            if (bllcm_C.Update(model))
            {
                MessageBox.Show("添加成功");
            } 
        }
 
        private void gBtnAddToMethod3_Click(object sender, EventArgs e)
        {
            string strErr = "";
 

            if (this.b_sendCompany.Text.Trim().Length == 0)
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

            if (this.b_temperature.Text.Trim().Length == 0)
            {
                strErr += "试验温度不能为空！\r\n\r\n";
            }

            if (this.b_testMethod.Text.Trim().Length == 0)
            {
                strErr += "试验方法不能为空！\r\n\r\n";
            }

            if (this.b_sampleCharacter.Text.Trim().Length == 0)
            {
                strErr += "试样标识不能为空！\r\n\r\n";
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
 
            model.sendCompany = this.b_sendCompany.Text;
            model.stuffCardNo = this.b_stuffCardNo.Text;
            model.stuffSpec = b_stuffSpec.Text;
            model.stuffType = b_stuffType.Text;
            model.hotStatus = b_hotStatus.Text;
            model.temperature = double.Parse(this.b_temperature.Text);
            model.humidity = double.Parse(b_humidity.Text);
            model.testStandard = b_testStandard.Text;
            model.testMethod = b_testMethod.Text;
            model.mathineType = b_mathineType.Text;
            model.testCondition = "";
            model.sampleCharacter = b_sampleCharacter.Text;
            model.getSample = b_getSample.Text;
            model.tester = b_tester.Text;
            model.assessor = "";
            model.sign = this.b_sign.Text;

            model.a = (this.b_a.Text.Trim().Length > 0 ? double.Parse(b_a.Text) : 0);
            model.b = (this.b_b.Text.Trim().Length > 0 ? double.Parse(b_b.Text) : 0);
            model.L = (this.b_L.Text.Trim().Length > 0 ? double.Parse(b_L.Text) : 0);
            model.ll = (this.b_ll.Text.Trim().Length > 0 ? double.Parse(b_ll.Text) : 0);
            model.D = (this.b_D.Text.Trim().Length > 0 ? double.Parse(b_D.Text) : 0);
            model.c = (this.b_c.Text.Trim().Length > 0 ? double.Parse(b_c.Text) : 0);
            model.p = (this.b_p.Text.Trim().Length > 0 ? double.Parse(b_p.Text) : 0);

            //  "α double," +//弯曲角度
            //  "r double," + //试样弯曲后的弯曲半径
            //  "f double," + //弯曲压头的移动距离 

            //  "Avera double not null," + //平均值
            //  "Avera1 double not null," + //去掉最大最小值的平均值

            //   "isFinish bit," +//是否完成试验
            //   "isConformity bit," +//是否合格
            //  "testDate date" + //试验日期 
            if (bllcm_B.Update(model))
            {
                MessageBox.Show("添加成功");
            }
        }

        private void tsbtnMinimize_Click(object sender, EventArgs e)
        {
            _fmMain.WindowState = FormWindowState.Minimized;
        }

        private bool boolLashen1 = false;
        private void btnMore1_Click(object sender, EventArgs e)
        {
            frmTensileOther1 fto = new frmTensileOther1(this);
            if (DialogResult.OK == fto.ShowDialog())
            {
                this._sendCompany = fto._sendCompany.Text;
                this._temperature = fto._temperature.Text;
                this._sampleCharacter = fto._sampleCharacter.Text;
                this._hotStatus = fto._hotStatus.Text;
                this._humidity = fto._humidity.Text;
                this._testStandard = fto._testStandard.Text;
            }
        }
      
        private bool boolCompress1 = false;
        private void btnMore3_Click(object sender, EventArgs e)
        {
            boolCompress1 = !boolCompress1;
            if (boolCompress1)
            {
                palCompress1.Visible = true;
            }
            else
            {
                palCompress1.Visible = false;
            } 
        }
        private bool boolCompress2 = false;
        private void btnMore4_Click(object sender, EventArgs e)
        {
            boolCompress2 = !boolCompress2;
            if (boolCompress2)
            {
                palCompress2.Visible = true;
            }
            else
            {
                palCompress2.Visible = false;
            } 
        }
        private bool boolBend1 = false;
        private void btnMore5_Click(object sender, EventArgs e)
        {
            boolBend1 = !boolBend1;
            if (boolBend1)
            {
                palBend1.Visible = true;
            }
            else
            {
                palBend1.Visible = false;
            } 
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

      

    }
}
