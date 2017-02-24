using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
    /// <summary>
    /// 数据访问类:SelTestResult_C
    /// </summary>
    public partial class SelTestResult_C
    {
        public SelTestResult_C()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_SelTestResult_C");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
            parameters[0].Value = ID;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(HR_Test.Model.SelTestResult_C model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_SelTestResult_C(");
            strSql.Append("methodName,Fmc,Fpc,Ftc,FeHc,FeLc,Rmc,Rpc,Rtc,ReHc,ReLc,Ec,Mean,SD,Mid,CV,saveCurve)");
            strSql.Append(" values (");
            strSql.Append("@methodName,@Fmc,@Fpc,@Ftc,@FeHc,@FeLc,@Rmc,@Rpc,@Rtc,@ReHc,@ReLc,@Ec,@Mean,@SD,@Mid,@CV,@saveCurve)");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,100),
					new OleDbParameter("@Fmc", OleDbType.Boolean,1),
					new OleDbParameter("@Fpc", OleDbType.Boolean,1),
					new OleDbParameter("@Ftc", OleDbType.Boolean,1),
					new OleDbParameter("@FeHc", OleDbType.Boolean,1),
					new OleDbParameter("@FeLc", OleDbType.Boolean,1),
					new OleDbParameter("@Rmc", OleDbType.Boolean,1),
					new OleDbParameter("@Rpc", OleDbType.Boolean,1),
					new OleDbParameter("@Rtc", OleDbType.Boolean,1),
					new OleDbParameter("@ReHc", OleDbType.Boolean,1),
					new OleDbParameter("@ReLc", OleDbType.Boolean,1),
					new OleDbParameter("@Ec", OleDbType.Boolean,1),
					new OleDbParameter("@Mean", OleDbType.Boolean,1),
					new OleDbParameter("@SD", OleDbType.Boolean,1),
					new OleDbParameter("@Mid", OleDbType.Boolean,1),
					new OleDbParameter("@CV", OleDbType.Boolean,1),
					new OleDbParameter("@saveCurve", OleDbType.Boolean,1)};
            parameters[0].Value = model.methodName;
            parameters[1].Value = model.Fmc;
            parameters[2].Value = model.Fpc;
            parameters[3].Value = model.Ftc;
            parameters[4].Value = model.FeHc;
            parameters[5].Value = model.FeLc;
            parameters[6].Value = model.Rmc;
            parameters[7].Value = model.Rpc;
            parameters[8].Value = model.Rtc;
            parameters[9].Value = model.ReHc;
            parameters[10].Value = model.ReLc;
            parameters[11].Value = model.Ec;
            parameters[12].Value = model.Mean;
            parameters[13].Value = model.SD;
            parameters[14].Value = model.Mid;
            parameters[15].Value = model.CV;
            parameters[16].Value = model.saveCurve;

            int rows = DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(HR_Test.Model.SelTestResult_C model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_SelTestResult_C set ");
            strSql.Append("methodName=@methodName,");
            strSql.Append("Fmc=@Fmc,");
            strSql.Append("Fpc=@Fpc,");
            strSql.Append("Ftc=@Ftc,");
            strSql.Append("FeHc=@FeHc,");
            strSql.Append("FeLc=@FeLc,");
            strSql.Append("Rmc=@Rmc,");
            strSql.Append("Rpc=@Rpc,");
            strSql.Append("Rtc=@Rtc,");
            strSql.Append("ReHc=@ReHc,");
            strSql.Append("ReLc=@ReLc,");
            strSql.Append("Ec=@Ec,");
            strSql.Append("Mean=@Mean,");
            strSql.Append("SD=@SD,");
            strSql.Append("Mid=@Mid,");
            strSql.Append("CV=@CV,");
            strSql.Append("saveCurve=@saveCurve");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,100),
					new OleDbParameter("@Fmc", OleDbType.Boolean,1),
					new OleDbParameter("@Fpc", OleDbType.Boolean,1),
					new OleDbParameter("@Ftc", OleDbType.Boolean,1),
					new OleDbParameter("@FeHc", OleDbType.Boolean,1),
					new OleDbParameter("@FeLc", OleDbType.Boolean,1),
					new OleDbParameter("@Rmc", OleDbType.Boolean,1),
					new OleDbParameter("@Rpc", OleDbType.Boolean,1),
					new OleDbParameter("@Rtc", OleDbType.Boolean,1),
					new OleDbParameter("@ReHc", OleDbType.Boolean,1),
					new OleDbParameter("@ReLc", OleDbType.Boolean,1),
					new OleDbParameter("@Ec", OleDbType.Boolean,1),
					new OleDbParameter("@Mean", OleDbType.Boolean,1),
					new OleDbParameter("@SD", OleDbType.Boolean,1),
					new OleDbParameter("@Mid", OleDbType.Boolean,1),
					new OleDbParameter("@CV", OleDbType.Boolean,1),
					new OleDbParameter("@saveCurve", OleDbType.Boolean,1),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
            parameters[0].Value = model.methodName;
            parameters[1].Value = model.Fmc;
            parameters[2].Value = model.Fpc;
            parameters[3].Value = model.Ftc;
            parameters[4].Value = model.FeHc;
            parameters[5].Value = model.FeLc;
            parameters[6].Value = model.Rmc;
            parameters[7].Value = model.Rpc;
            parameters[8].Value = model.Rtc;
            parameters[9].Value = model.ReHc;
            parameters[10].Value = model.ReLc;
            parameters[11].Value = model.Ec;
            parameters[12].Value = model.Mean;
            parameters[13].Value = model.SD;
            parameters[14].Value = model.Mid;
            parameters[15].Value = model.CV;
            parameters[16].Value = model.saveCurve;
            parameters[17].Value = model.ID;

            int rows = DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_SelTestResult_C ");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
            parameters[0].Value = ID;

            int rows = DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string methodName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_SelTestResult_C ");
            strSql.Append(" where methodName=@methodName");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar)
			};
            parameters[0].Value = methodName;

            int rows = DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_SelTestResult_C ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperOleDb.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HR_Test.Model.SelTestResult_C GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,methodName,Fmc,Fpc,Ftc,FeHc,FeLc,Rmc,Rpc,Rtc,ReHc,ReLc,Ec,Mean,SD,Mid,CV,saveCurve from tb_SelTestResult_C ");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
            parameters[0].Value = ID;

            HR_Test.Model.SelTestResult_C model = new HR_Test.Model.SelTestResult_C();
            DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HR_Test.Model.SelTestResult_C GetModel(string methodName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,methodName,Fmc,Fpc,Ftc,FeHc,FeLc,Rmc,Rpc,Rtc,ReHc,ReLc,Ec,Mean,SD,Mid,CV,saveCurve from tb_SelTestResult_C ");
            strSql.Append(" where methodName=@methodName");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar)
			};
            parameters[0].Value = methodName;

            HR_Test.Model.SelTestResult_C model = new HR_Test.Model.SelTestResult_C();
            DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HR_Test.Model.SelTestResult_C DataRowToModel(DataRow row)
        {
            HR_Test.Model.SelTestResult_C model = new HR_Test.Model.SelTestResult_C();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["methodName"] != null)
                {
                    model.methodName = row["methodName"].ToString();
                }
                if (row["Fmc"] != null && row["Fmc"].ToString() != "")
                {
                    if ((row["Fmc"].ToString() == "1") || (row["Fmc"].ToString().ToLower() == "true"))
                    {
                        model.Fmc = true;
                    }
                    else
                    {
                        model.Fmc = false;
                    }
                }
                if (row["Fpc"] != null && row["Fpc"].ToString() != "")
                {
                    if ((row["Fpc"].ToString() == "1") || (row["Fpc"].ToString().ToLower() == "true"))
                    {
                        model.Fpc = true;
                    }
                    else
                    {
                        model.Fpc = false;
                    }
                }
                if (row["Ftc"] != null && row["Ftc"].ToString() != "")
                {
                    if ((row["Ftc"].ToString() == "1") || (row["Ftc"].ToString().ToLower() == "true"))
                    {
                        model.Ftc = true;
                    }
                    else
                    {
                        model.Ftc = false;
                    }
                }
                if (row["FeHc"] != null && row["FeHc"].ToString() != "")
                {
                    if ((row["FeHc"].ToString() == "1") || (row["FeHc"].ToString().ToLower() == "true"))
                    {
                        model.FeHc = true;
                    }
                    else
                    {
                        model.FeHc = false;
                    }
                }
                if (row["FeLc"] != null && row["FeLc"].ToString() != "")
                {
                    if ((row["FeLc"].ToString() == "1") || (row["FeLc"].ToString().ToLower() == "true"))
                    {
                        model.FeLc = true;
                    }
                    else
                    {
                        model.FeLc = false;
                    }
                }
                if (row["Rmc"] != null && row["Rmc"].ToString() != "")
                {
                    if ((row["Rmc"].ToString() == "1") || (row["Rmc"].ToString().ToLower() == "true"))
                    {
                        model.Rmc = true;
                    }
                    else
                    {
                        model.Rmc = false;
                    }
                }
                if (row["Rpc"] != null && row["Rpc"].ToString() != "")
                {
                    if ((row["Rpc"].ToString() == "1") || (row["Rpc"].ToString().ToLower() == "true"))
                    {
                        model.Rpc = true;
                    }
                    else
                    {
                        model.Rpc = false;
                    }
                }
                if (row["Rtc"] != null && row["Rtc"].ToString() != "")
                {
                    if ((row["Rtc"].ToString() == "1") || (row["Rtc"].ToString().ToLower() == "true"))
                    {
                        model.Rtc = true;
                    }
                    else
                    {
                        model.Rtc = false;
                    }
                }
                if (row["ReHc"] != null && row["ReHc"].ToString() != "")
                {
                    if ((row["ReHc"].ToString() == "1") || (row["ReHc"].ToString().ToLower() == "true"))
                    {
                        model.ReHc = true;
                    }
                    else
                    {
                        model.ReHc = false;
                    }
                }
                if (row["ReLc"] != null && row["ReLc"].ToString() != "")
                {
                    if ((row["ReLc"].ToString() == "1") || (row["ReLc"].ToString().ToLower() == "true"))
                    {
                        model.ReLc = true;
                    }
                    else
                    {
                        model.ReLc = false;
                    }
                }
                if (row["Ec"] != null && row["Ec"].ToString() != "")
                {
                    if ((row["Ec"].ToString() == "1") || (row["Ec"].ToString().ToLower() == "true"))
                    {
                        model.Ec = true;
                    }
                    else
                    {
                        model.Ec = false;
                    }
                }
                if (row["Mean"] != null && row["Mean"].ToString() != "")
                {
                    if ((row["Mean"].ToString() == "1") || (row["Mean"].ToString().ToLower() == "true"))
                    {
                        model.Mean = true;
                    }
                    else
                    {
                        model.Mean = false;
                    }
                }
                if (row["SD"] != null && row["SD"].ToString() != "")
                {
                    if ((row["SD"].ToString() == "1") || (row["SD"].ToString().ToLower() == "true"))
                    {
                        model.SD = true;
                    }
                    else
                    {
                        model.SD = false;
                    }
                }
                if (row["Mid"] != null && row["Mid"].ToString() != "")
                {
                    if ((row["Mid"].ToString() == "1") || (row["Mid"].ToString().ToLower() == "true"))
                    {
                        model.Mid = true;
                    }
                    else
                    {
                        model.Mid = false;
                    }
                }
                if (row["CV"] != null && row["CV"].ToString() != "")
                {
                    if ((row["CV"].ToString() == "1") || (row["CV"].ToString().ToLower() == "true"))
                    {
                        model.CV = true;
                    }
                    else
                    {
                        model.CV = false;
                    }
                }
                if (row["saveCurve"] != null && row["saveCurve"].ToString() != "")
                {
                    if ((row["saveCurve"].ToString() == "1") || (row["saveCurve"].ToString().ToLower() == "true"))
                    {
                        model.saveCurve = true;
                    }
                    else
                    {
                        model.saveCurve = false;
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,methodName,Fmc,Fpc,Ftc,FeHc,FeLc,Rmc,Rpc,Rtc,ReHc,ReLc,Ec,Mean,SD,Mid,CV,saveCurve ");
            strSql.Append(" FROM tb_SelTestResult_C ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM tb_SelTestResult_C ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from tb_SelTestResult_C T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperOleDb.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            OleDbParameter[] parameters = {
                    new OleDbParameter("@tblName", OleDbType.VarChar, 255),
                    new OleDbParameter("@fldName", OleDbType.VarChar, 255),
                    new OleDbParameter("@PageSize", OleDbType.Integer),
                    new OleDbParameter("@PageIndex", OleDbType.Integer),
                    new OleDbParameter("@IsReCount", OleDbType.Boolean),
                    new OleDbParameter("@OrderType", OleDbType.Boolean),
                    new OleDbParameter("@strWhere", OleDbType.VarChar,1000),
                    };
            parameters[0].Value = "tb_SelTestResult_C";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperOleDb.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

