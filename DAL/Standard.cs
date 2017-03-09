using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
    /// <summary>
    /// 数据访问类:Standard
    /// </summary>
    public partial class Standard
    {
        public Standard()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_Standard");
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
        public bool Add(HR_Test.Model.Standard model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_Standard(");
            strSql.Append("standardNo,standardName,testType,resultTableName,selTableName,methodTableName,sign)");
            strSql.Append(" values (");
            strSql.Append("@standardNo,@standardName,@testType,@resultTableName,@selTableName,@methodTableName,@sign)");
            OleDbParameter[] parameters = {
					new OleDbParameter("@standardNo", OleDbType.VarChar,255),
					new OleDbParameter("@standardName", OleDbType.VarChar,255),
					new OleDbParameter("@testType", OleDbType.VarChar,255),
					new OleDbParameter("@resultTableName", OleDbType.VarChar,255),
					new OleDbParameter("@selTableName", OleDbType.VarChar,255),
					new OleDbParameter("@methodTableName", OleDbType.VarChar,255),
					new OleDbParameter("@sign", OleDbType.VarChar,0)};
            parameters[0].Value = model.standardNo;
            parameters[1].Value = model.standardName;
            parameters[2].Value = model.testType;
            parameters[3].Value = model.resultTableName;
            parameters[4].Value = model.selTableName;
            parameters[5].Value = model.methodTableName;
            parameters[6].Value = model.sign;

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
        public bool Update(HR_Test.Model.Standard model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Standard set ");
            strSql.Append("standardNo=@standardNo,");
            strSql.Append("standardName=@standardName,");
            strSql.Append("testType=@testType,");
            strSql.Append("resultTableName=@resultTableName,");
            strSql.Append("selTableName=@selTableName,");
            strSql.Append("methodTableName=@methodTableName,");
            strSql.Append("sign=@sign");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@standardNo", OleDbType.VarChar,255),
					new OleDbParameter("@standardName", OleDbType.VarChar,255),
					new OleDbParameter("@testType", OleDbType.VarChar,255),
					new OleDbParameter("@resultTableName", OleDbType.VarChar,255),
					new OleDbParameter("@selTableName", OleDbType.VarChar,255),
					new OleDbParameter("@methodTableName", OleDbType.VarChar,255),
					new OleDbParameter("@sign", OleDbType.VarChar,0),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
            parameters[0].Value = model.standardNo;
            parameters[1].Value = model.standardName;
            parameters[2].Value = model.testType;
            parameters[3].Value = model.resultTableName;
            parameters[4].Value = model.selTableName;
            parameters[5].Value = model.methodTableName;
            parameters[6].Value = model.sign;
            parameters[7].Value = model.ID;

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
            strSql.Append("delete from tb_Standard ");
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
            strSql.Append("delete from tb_Standard ");
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
        public HR_Test.Model.Standard GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,standardNo,standardName,testType,resultTableName,selTableName,methodTableName,sign from tb_Standard ");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
            parameters[0].Value = ID;

            HR_Test.Model.Standard model = new HR_Test.Model.Standard();
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
        public HR_Test.Model.Standard DataRowToModel(DataRow row)
        {
            HR_Test.Model.Standard model = new HR_Test.Model.Standard();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["standardNo"] != null)
                {
                    model.standardNo = row["standardNo"].ToString();
                }
                if (row["standardName"] != null)
                {
                    model.standardName = row["standardName"].ToString();
                }
                if (row["testType"] != null)
                {
                    model.testType = row["testType"].ToString();
                }
                if (row["resultTableName"] != null)
                {
                    model.resultTableName = row["resultTableName"].ToString();
                }
                if (row["selTableName"] != null)
                {
                    model.selTableName = row["selTableName"].ToString();
                }
                if (row["methodTableName"] != null)
                {
                    model.methodTableName = row["methodTableName"].ToString();
                }
                if (row["sign"] != null)
                {
                    model.sign = row["sign"].ToString();
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
            strSql.Append("select ID,standardNo,standardName,testType,resultTableName,selTableName,methodTableName,sign ");
            strSql.Append(" FROM tb_Standard ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }


        public DataSet GetListDistinctStandardNo()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct(standardNo),methodTableName,selTableName,standardName ");
            strSql.Append(" FROM tb_Standard order by standardNo");
            return DbHelperOleDb.Query(strSql.ToString());
        }

        public DataSet GetListDistinctTestType()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct(testType) ");
            strSql.Append(" FROM tb_Standard order by testType");
            return DbHelperOleDb.Query(strSql.ToString());
        }

        public DataSet GetListDistinctResultTableName()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct(resultTableName) ");
            strSql.Append(" FROM tb_Standard ");
            return DbHelperOleDb.Query(strSql.ToString());
        }


        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM tb_Standard ");
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
            strSql.Append(")AS Row, T.*  from tb_Standard T ");
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
            parameters[0].Value = "tb_Standard";
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

