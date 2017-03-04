using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO; 
using System.Threading;
using System.Threading.Tasks;

namespace HR_Test
{
    public partial class frmInput : Form
    {
        Input.GBT23615_2009TensileZong input_GBT23615Zong;
        Input.GBT23615_2009TensileHeng input_GBT23615Heng;
        Input.GBT228_2010Tensile input_GBT228;
        Input.GBT7314_2005Compress input_GBT7314;
        Input.YBT5349_2006Bend input_YBT5349;
        Input.GBT28289_2012Tensile input_GBT28289Tensile;
        Input.GBT28289_2012Shear input_GBT28289Shear;
        Input.GBT28289_2012Twist input_GBT28289Twist;
        Input.GBT3354_2014 input_GBT3354;
        private frmMain _fmMain;
        public frmInput(frmMain fmMain)
        {
            InitializeComponent();
            _fmMain = fmMain;
            Task t = tskReadMethod();
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
            if(t.Result!=null)
            {
                List<TreeNode> lsttn = (List<TreeNode>)t.Result;
                foreach (TreeNode tn in lsttn)
                    this.tvTestMethod.Nodes[0].Nodes.Add(tn);
            }
            this.tvTestMethod.ExpandAll();
        }


        private void frmInput_Load(object sender, EventArgs e)
        {
            AddInputGBT228Control("");
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

        private void tsbtnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void tvTestMethod_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //根据 e.tag（标准号） 读取试验方法的信息

            if (e.Node.Tag!=null)
            {
                switch (e.Node.Tag.ToString())
                {
                    case "GB/T 23615.1-2009":
                        switch (e.Node.Name)
                        {
                            case "纵向拉伸":
                                AddInputGBT23615Zong(e.Node.Text);
                                break;
                            case "横向拉伸":
                                AddInputGBT23615Heng(e.Node.Text);
                                break;
                        }
                        break;

                    case "GB/T 28289-2012":
                        switch (e.Node.Name)
                        {
                            case "拉伸试验":
                                AddInputGBT28289Tensile(e.Node.Text);
                                break;
                            case "剪切试验":
                                AddInputGBT28289Shear(e.Node.Text);
                                break;
                            case "扭转试验":
                                AddInputGBT28289Twist(e.Node.Text);
                                break;
                        }
                        break;


                    case "GB/T 228-2010":
                        AddInputGBT228Control(e.Node.Text);
                        break;
                    case "GB/T 7314-2005":
                        AddInputGBT7314Control(e.Node.Text);
                        break;
                    case "YB/T 5349-2006":
                        AddInputYBT5349Control(e.Node.Text);
                        break;

                    case "GB/T 3354-2014":
                        AddInputGBT3354Control(e.Node.Text);
                        break;
                    
                }
            }
        } 
        private void AddInputGBT3354Control(string _methodName)
        {
            if (input_GBT3354 == null)
            {
                input_GBT3354 = new HR_Test.Input.GBT3354_2014();
                input_GBT3354._MethodName = _methodName;
                this.palInput.Controls.Add(input_GBT3354);
                //先添加控件，然后调整位置
                int left = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_GBT3354.gbTensileC.Width) / 2;
                int top = (Screen.PrimaryScreen.WorkingArea.Height - input_GBT3354.gbTensileC.Height - 195) / 2;
                input_GBT3354.gbTensileC.Location = new Point(left, top);
                input_GBT3354.Dock = DockStyle.Fill;
                int mleft = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_GBT3354.palMethodShow.Width) / 2;
                int mtop = (input_GBT3354.palBottom.Height - input_GBT3354.palMethodShow.Height) / 2;
                input_GBT3354.palMethodShow.Location = new Point(mleft, mtop);
                input_GBT3354.BringToFront();
            }
            else
            {
                input_GBT3354.BringToFront();
                input_GBT3354.ReadMethodInfo(_methodName);
            }
        }

        /// <summary>
        /// 添加 GBT23615 heng 参数输入控件
        /// </summary>
        /// <param name="_methodName">方法名称</param>
        private void AddInputGBT23615Heng(string _methodName)
        {
            if (input_GBT23615Heng == null)
            {
                input_GBT23615Heng = new HR_Test.Input.GBT23615_2009TensileHeng();
                input_GBT23615Heng._MethodName = _methodName;
                this.palInput.Controls.Add(input_GBT23615Heng);
                //先添加控件，然后调整位置
                int left = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_GBT23615Heng.gbTensileC.Width) / 2;
                int top = (Screen.PrimaryScreen.WorkingArea.Height - input_GBT23615Heng.gbTensileC.Height - 195) / 2;
                input_GBT23615Heng.gbTensileC.Location = new Point(left, top);
                input_GBT23615Heng.Dock = DockStyle.Fill;
                int mleft = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_GBT23615Heng.palMethodShow.Width) / 2;
                int mtop = (input_GBT23615Heng.palBottom.Height - input_GBT23615Heng.palMethodShow.Height) / 2;
                input_GBT23615Heng.palMethodShow.Location = new Point(mleft, mtop);
                input_GBT23615Heng.BringToFront();
            }
            else
            {
                input_GBT23615Heng.BringToFront();
                input_GBT23615Heng.ReadMethodInfo(_methodName);
            }
        }

        /// <summary>
        /// 添加 GBT23615 zong 参数输入控件
        /// </summary>
        /// <param name="_methodName">方法名称</param>
        private void AddInputGBT23615Zong(string _methodName)
        {
            if (input_GBT23615Zong == null)
            {
                input_GBT23615Zong = new HR_Test.Input.GBT23615_2009TensileZong();
                input_GBT23615Zong._MethodName = _methodName;
                this.palInput.Controls.Add(input_GBT23615Zong);
                //先添加控件，然后调整位置
                int left = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_GBT23615Zong.gbTensileC.Width) / 2;
                int top = (Screen.PrimaryScreen.WorkingArea.Height - input_GBT23615Zong.gbTensileC.Height - 195) / 2;
                input_GBT23615Zong.gbTensileC.Location = new Point(left, top);
                input_GBT23615Zong.Dock = DockStyle.Fill;
                int mleft = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_GBT23615Zong.palMethodShow.Width) / 2;
                int mtop = (input_GBT23615Zong.palBottom.Height - input_GBT23615Zong.palMethodShow.Height) / 2;
                input_GBT23615Zong.palMethodShow.Location = new Point(mleft, mtop);
                input_GBT23615Zong.BringToFront();
            }
            else
            {
                input_GBT23615Zong.BringToFront();
                input_GBT23615Zong.ReadMethodInfo(_methodName);
            }
        }


        /// <summary>
        /// 添加 GBT228 参数输入控件
        /// </summary>
        /// <param name="_methodName">方法名称</param>
        private void AddInputGBT228Control(string _methodName)
        {
            if (input_GBT228 == null)
            {
                input_GBT228 = new HR_Test.Input.GBT228_2010Tensile();
                input_GBT228._MethodName = _methodName; 
                this.palInput.Controls.Add(input_GBT228);
                //先添加控件，然后调整位置
                int left = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_GBT228.gbTensileC.Width) / 2;
                int top = (Screen.PrimaryScreen.WorkingArea.Height - input_GBT228.gbTensileC.Height - 195) / 2;
                input_GBT228.gbTensileC.Location = new Point(left, top);
                input_GBT228.Dock = DockStyle.Fill;
                int mleft = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_GBT228.palMethodShow.Width) / 2;
                int mtop = (input_GBT228.palBottom.Height - input_GBT228.palMethodShow.Height) / 2;
                input_GBT228.palMethodShow.Location = new Point(mleft, mtop);
                input_GBT228.BringToFront();
            }
            else
            {
                input_GBT228.BringToFront();
                input_GBT228.ReadMethodInfo(_methodName);
            }
        }

        /// <summary>
        /// 添加 GBT7314 参数输入控件
        /// </summary>
        /// <param name="_methodName">方法名称</param>
        private void AddInputGBT7314Control(string _methodName)
        {
            if (input_GBT7314 == null)
            {
                input_GBT7314 = new HR_Test.Input.GBT7314_2005Compress();
                input_GBT7314._MethodName = _methodName; 
                this.palInput.Controls.Add(input_GBT7314);
                //先添加控件，然后调整位置
                int left = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_GBT7314.gbCompressC.Width) / 2;
                int top = (Screen.PrimaryScreen.WorkingArea.Height - input_GBT7314.gbCompressC.Height - 195) / 2;
                input_GBT7314.gbCompressC.Location = new Point(left, top);
                input_GBT7314.Dock = DockStyle.Fill;
                int mleft = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_GBT7314.palMethodShow.Width) / 2;
                int mtop = (input_GBT7314.palBottom.Height - input_GBT7314.palMethodShow.Height) / 2;
                input_GBT7314.palMethodShow.Location = new Point(mleft, mtop);
                input_GBT7314.BringToFront();
                input_GBT7314.ReadMethodInfo(_methodName);
            }
            else
            {
                input_GBT7314.BringToFront();
                input_GBT7314.ReadMethodInfo(_methodName);
            }
        }

        /// <summary>
        /// GBT28289
        /// </summary>
        /// <param name="_methodName"></param>
        private void AddInputGBT28289Tensile(string _methodName)
        {
            if (input_GBT28289Tensile == null)
            {
                input_GBT28289Tensile = new HR_Test.Input.GBT28289_2012Tensile();
                input_GBT28289Tensile._MethodName = _methodName;
                this.palInput.Controls.Add(input_GBT28289Tensile);
                //先添加控件，然后调整位置
                int left = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_GBT28289Tensile.gbTensileC.Width) / 2;
                int top = (Screen.PrimaryScreen.WorkingArea.Height - input_GBT28289Tensile.gbTensileC.Height - 195) / 2;
                input_GBT28289Tensile.gbTensileC.Location = new Point(left, top);
                input_GBT28289Tensile.Dock = DockStyle.Fill;
                int mleft = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_GBT28289Tensile.palMethodShow.Width) / 2;
                int mtop = (input_GBT28289Tensile.palBottom.Height - input_GBT28289Tensile.palMethodShow.Height) / 2;
                input_GBT28289Tensile.palMethodShow.Location = new Point(mleft, mtop);
                input_GBT28289Tensile.BringToFront();
            }
            else
            {
                input_GBT28289Tensile.BringToFront();
                input_GBT28289Tensile.ReadMethodInfo(_methodName);
            }
        }

        /// <summary>
        /// GBT28289
        /// </summary>
        /// <param name="_methodName"></param>
        private void AddInputGBT28289Shear(string _methodName)
        {
            if (input_GBT28289Shear == null)
            {
                input_GBT28289Shear = new HR_Test.Input.GBT28289_2012Shear();
                input_GBT28289Shear._MethodName = _methodName;
                this.palInput.Controls.Add(input_GBT28289Shear);
                //先添加控件，然后调整位置
                int left = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_GBT28289Shear.gbTensileC.Width) / 2;
                int top = (Screen.PrimaryScreen.WorkingArea.Height - input_GBT28289Shear.gbTensileC.Height - 195) / 2;
                input_GBT28289Shear.gbTensileC.Location = new Point(left, top);
                input_GBT28289Shear.Dock = DockStyle.Fill;
                int mleft = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_GBT28289Shear.palMethodShow.Width) / 2;
                int mtop = (input_GBT28289Shear.palBottom.Height - input_GBT28289Shear.palMethodShow.Height) / 2;
                input_GBT28289Shear.palMethodShow.Location = new Point(mleft, mtop);
                input_GBT28289Shear.BringToFront();
            }
            else
            {
                input_GBT28289Shear.BringToFront();
                input_GBT28289Shear.ReadMethodInfo(_methodName);
            }
        }

        /// <summary>
        /// GBT28289
        /// </summary>
        /// <param name="_methodName"></param>
        private void AddInputGBT28289Twist(string _methodName)
        {
            if (input_GBT28289Twist == null)
            {
                input_GBT28289Twist = new HR_Test.Input.GBT28289_2012Twist();
                input_GBT28289Twist._MethodName = _methodName;
                this.palInput.Controls.Add(input_GBT28289Twist);
                //先添加控件，然后调整位置
                int left = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_GBT28289Twist.gbTensileC.Width) / 2;
                int top = (Screen.PrimaryScreen.WorkingArea.Height - input_GBT28289Twist.gbTensileC.Height - 195) / 2;
                input_GBT28289Twist.gbTensileC.Location = new Point(left, top);
                input_GBT28289Twist.Dock = DockStyle.Fill;
                int mleft = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_GBT28289Twist.palMethodShow.Width) / 2;
                int mtop = (input_GBT28289Twist.palBottom.Height - input_GBT28289Twist.palMethodShow.Height) / 2;
                input_GBT28289Twist.palMethodShow.Location = new Point(mleft, mtop);
                input_GBT28289Twist.BringToFront();
            }
            else
            {
                input_GBT28289Twist.BringToFront();
                input_GBT28289Twist.ReadMethodInfo(_methodName);
            }
        }

        /// <summary>
        /// YBT5349
        /// </summary>
        /// <param name="_methodName"></param>
        private void AddInputYBT5349Control(string _methodName)
        {
            if (input_YBT5349 == null)
            {
                input_YBT5349 = new HR_Test.Input.YBT5349_2006Bend();
                input_YBT5349._MethodName = _methodName;
                this.palInput.Controls.Add(input_YBT5349);
                //先添加控件，然后调整位置
                int left = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_YBT5349.gbBendC.Width) / 2;
                int top = (Screen.PrimaryScreen.WorkingArea.Height - input_YBT5349.gbBendC.Height - 195) / 2;
                input_YBT5349.gbBendC.Location = new Point(left, top);
                input_YBT5349.Dock = DockStyle.Fill;
                int mleft = (Screen.PrimaryScreen.WorkingArea.Width - 180 - input_YBT5349.palMethodShow.Width) / 2;
                int mtop = (input_YBT5349.palBottom.Height - input_YBT5349.palMethodShow.Height) / 2;
                input_YBT5349.palMethodShow.Location = new Point(mleft, mtop);
                input_YBT5349.BringToFront();
            }
            else
            {
                input_YBT5349.BringToFront();
                input_YBT5349.ReadMethodInfo(_methodName);
            }
        }

        private void _a0_TextChanged(object sender, EventArgs e)
        {
            TextBox uf = (TextBox)sender;
            if (!utils.IsNumeric(uf.Text))
                uf.Text = "";
        }

        private void tsbtnMinimize_Click(object sender, EventArgs e)
        {
            _fmMain.WindowState = FormWindowState.Minimized;
        }     
    }
}
