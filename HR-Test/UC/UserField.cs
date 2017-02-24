using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace HR_Test.UC
{
    public partial class UserField : UserControl
    {
        /// <summary>
        /// 自定义字段的名称
        /// </summary>
        public string _FieldName
        {
            get { return this.lblFiledName.Text; }
            set { this.lblFiledName.Text = value; }
        } 
        /// <summary>
        /// 自定义字段名称的内容
        /// </summary>
        public override string Text
        {
            get { return this.txtFiledContent.Text; }
            set { this.txtFiledContent.Text = value; }
        }

        /// <summary>
        /// 自定义字段名称颜色
        /// </summary>
        public Color _TitleColor
        {
            get { return this.lblFiledName.ForeColor; }
            set { this.lblFiledName.ForeColor = value; }
        }

        /// <summary>
        /// 自定义字段名称 是否只读
        /// </summary>
        public bool _ReadOnly
        {
            get { return this.txtFiledContent.ReadOnly; }
            set { this.txtFiledContent.ReadOnly = value; }
        }


        public delegate void UserHandler(object sender, System.EventArgs e);
        public event UserHandler OnTextChanged;

        
        ///// <summary>
        ///// 绑定到xml数据源的路径
        ///// </summary>
        //private string _xmlPath;
        //public string _XmlPath
        //{
        //    get { return this._xmlPath; }
        //    set { this._xmlPath = value; }
        //}

        ///// <summary>
        ///// 绑定到xml数据源的此节点下所有列表项
        ///// </summary>
        //private string _xmlNodeValue;
        //public string _XmlNodeValue
        //{
        //    get { return this._xmlNodeValue; }
        //    set { this._xmlNodeValue = value; }
        //}

        public UserField()
        {
            InitializeComponent(); 
        } 

        private void UserField_Load(object sender, EventArgs e)
        {
            //this.cmbFiledContent.Items.Clear();
            //if (!string.IsNullOrEmpty(this._xmlPath))
            //{
            //    if (!string.IsNullOrEmpty(this._xmlNodeValue))
            //    {
            //        XmlDocument xd = new XmlDocument();
            //        xd.Load(this._xmlPath);
            //        XmlNode xn = xd.SelectSingleNode("/fields/field[@value='" + this._xmlNodeValue + "']");
            //        if (xn!=null)
            //        {
            //            foreach (XmlNode xnc in xn.ChildNodes)
            //            {
            //                this.cmbFiledContent.Items.Add(xnc.Attributes["value"].Value);
            //            }
            //        }
            //    }
            //}
        }

        private void UserField_SizeChanged(object sender, EventArgs e)
        {
            this.txtFiledContent.Top = (this.Height - this.txtFiledContent.Height) / 2;
        }

        private void txtFiledContent_TextChanged(object sender, EventArgs e)
        {
            if(OnTextChanged !=null)
                OnTextChanged(sender, e);
        }
    }
}
