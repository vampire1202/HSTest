using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
    /// <summary>
    /// 数据访问类:GBT3354_Sel
    /// </summary>
    public partial class GBT3354_Sel
    {
        public GBT3354_Sel()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_GBT3354_Sel");
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
        public bool Add(HR_Test.Model.GBT3354_Sel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_GBT3354_Sel(");
            strSql.Append("methodName,Pmax,σt,Et,μ12,ε1t,Mean,SD,Mid,CV,saveCurve)");
            strSql.Append(" values (");
            strSql.Append("@methodName,@Pmax,@σt,@Et,@μ12,@ε1t,@Mean,@SD,@Mid,@CV,@saveCurve)");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,255),
					new OleDbParameter("@Pmax", OleDbType.Boolean,1),
					new OleDbParameter("@σt", OleDbType.Boolean,1),
					new OleDbParameter("@Et", OleDbType.Boolean,1),
					new OleDbParameter("@μ12", OleDbType.Boolean,1),
					new OleDbParameter("@ε1t", OleDbType.Boolean,1),
					new OleDbParameter("@Mean", OleDbType.Boolean,1),
					new OleDbParameter("@SD", OleDbType.Boolean,1),
					new OleDbParameter("@Mid", OleDbType.Boolean,1),
					new OleDbParameter("@CV", OleDbType.Boolean,1),
					new OleDbParameter("@saveCurve", OleDbType.Boolean,1)};
            parameters[0].Value = model.methodName;
            parameters[1].Value = model.Pmax;
            parameters[2].Value = model.σt;
            parameters[3].Value = model.Et;
            parameters[4].Value = model.μ12;
            parameters[5].Value = model.ε1t;
            parameters[6].Value = model.Mean;
            parameters[7].Value = model.SD;
            parameters[8].Value = model.Mid;
            parameters[9].Value = model.CV;
            parameters[10].Value = model.saveCurve;

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
        public bool Update(HR_Test.Model.GBT3354_Sel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_GBT3354_Sel set ");
            strSql.Append("methodName=@methodName,");
            strSql.Append("Pmax=@Pmax,");
            strSql.Append("σt=@σt,");
            strSql.Append("Et=@Et,");
            strSql.Append("μ12=@μ12,");
            strSql.Append("ε1t=@ε1t,");
            strSql.Append("Mean=@Mean,");
            strSql.Append("SD=@SD,");
            strSql.Append("Mid=@Mid,");
            strSql.Append("CV=@CV,");
            strSql.Append("saveCurve=@saveCurve");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,255),
					new OleDbParameter("@Pmax", OleDbType.Boolean,1),
					new OleDbParameter("@σt", OleDbType.Boolean,1),
					new OleDbParameter("@Et", OleDbType.Boolean,1),
					new OleDbParameter("@μ12", OleDbType.Boolean,1),
					new OleDbParameter("@ε1t", OleDbType.Boolean,1),
					new OleDbParameter("@Mean", OleDbType.Boolean,1),
					new OleDbParameter("@SD", OleDbType.Boolean,1),
					new OleDbParameter("@Mid", OleDbType.Boolean,1),
					new OleDbParameter("@CV", OleDbType.Boolean,1),
					new OleDbParameter("@saveCurve", OleDbType.Boolean,1),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
            parameters[0].Value = model.methodName;
            parameters[1].Value = model.Pmax;
            parameters[2].Value = model.σt;
            parameters[3].Value = model.Et;
            parameters[4].Value = model.μ12;
            parameters[5].Value = model.ε1t;
            parameters[6].Value = model.Mean;
            parameters[7].Value = model.SD;
            parameters[8].Value = model.Mid;
            parameters[9].Value = model.CV;
            parameters[10].Value = model.saveCurve;
            parameters[11].Value = model.ID;

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
            strSql.Append("delete from tb_GBT3354_Sel ");
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_GBT3354_Sel ");
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
        public HR_Test.Model.GBT3354_Sel GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,methodName,Pmax,σt,Et,μ12,ε1t,Mean,SD,Mid,CV,saveCurve from tb_GBT3354_Sel ");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
            parameters[0].Value = ID;

            HR_Test.Model.GBT3354_Sel model = new HR_Test.Model.GBT3354_Sel();
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

        public HR_Test.Model.GBT3354_Sel GetModel(string methodName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,methodName,Pmax,σt,Et,μ12,ε1t,Mean,SD,Mid,CV,saveCurve from tb_GBT3354_Sel ");
            strSql.Append(" where methodName=@methodName");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar)
			};
            parameters[0].Value = methodName;

            HR_Test.Model.GBT3354_Sel model = new HR_Test.Model.GBT3354_Sel();
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
        public HR_Test.Model.GBT3354_Sel DataRowToModel(DataRow row)
        {
            HR_Test.Model.GBT3354_Sel model = new HR_Test.Model.GBT3354_Sel();
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
                if (row["Pmax"] != null && row["Pmax"].ToString() != "")
                {
                    if ((row["Pmax"].ToString() == "1") || (row["Pmax"].ToString().ToLower() == "true"))
                    {
                        model.Pmax = true;
                    }
                    else
                    {
                        model.Pmax = false;
                    }
                }
                if (row["σt"] != null && row["σt"].ToString() != "")
                {
                    if ((row["σt"].ToString() == "1") || (row["σt"].ToString().ToLower() == "true"))
                    {
                        model.σt = true;
                    }
                    else
                    {
                        model.σt = false;
                    }
                }
                if (row["Et"] != null && row["Et"].ToString() != "")
                {
                    if ((row["Et"].ToString() == "1") || (row["Et"].ToString().ToLower() == "true"))
                    {
                        model.Et = true;
                    }
                    else
                    {
                        model.Et = false;
                    }
                }
                if (row["μ12"] != null && row["μ12"].ToString() != "")
                {
                    if ((row["μ12"].ToString() == "1") || (row["μ12"].ToString().ToLower() == "true"))
                    {
                        model.μ12 = true;
                    }
                    else
                    {
                        model.μ12 = false;
                    }
                }
                if (row["ε1t"] != null && row["ε1t"].ToString() != "")
                {
                    if ((row["ε1t"].ToString() == "1") || (row["ε1t"].ToString().ToLower() == "true"))
                    {
                        model.ε1t = true;
                    }
                    else
                    {
                        model.ε1t = false;
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
            strSql.Append("select ID,methodName,Pmax,σt,Et,μ12,ε1t,Mean,SD,Mid,CV,saveCurve ");
            strSql.Append(" FROM tb_GBT3354_Sel ");
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
            strSql.Append("select count(1) FROM tb_GBT3354_Sel ");
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
            strSql.Append(")AS Row, T.*  from tb_GBT3354_Sel T ");
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
            parameters[0].Value = "tb_GBT3354_Sel";
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

