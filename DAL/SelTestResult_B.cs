using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
    /// <summary>
    /// 数据访问类:SelTestResult_B
    /// </summary>
    public partial class SelTestResult_B
    {
        public SelTestResult_B()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_SelTestResult_B");
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
        public bool Add(HR_Test.Model.SelTestResult_B model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_SelTestResult_B(");
            strSql.Append("methodName,f_bb,f_n,f_n1,f_rb,y,Fo,Fpb,Frb,Fbb,Fn,Fn1,Z,S,W,I,Eb,σpb,σrb,σbb,U,Mean,SD,Mid,CV,saveCurve)");
            strSql.Append(" values (");
            strSql.Append("@methodName,@f_bb,@f_n,@f_n1,@f_rb,@y,@Fo,@Fpb,@Frb,@Fbb,@Fn,@Fn1,@Z,@S,@W,@I,@Eb,@σpb,@σrb,@σbb,@U,@Mean,@SD,@Mid,@CV,@saveCurve)");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,100),
					new OleDbParameter("@f_bb", OleDbType.Boolean,1),
					new OleDbParameter("@f_n", OleDbType.Boolean,1),
					new OleDbParameter("@f_n1", OleDbType.Boolean,1),
					new OleDbParameter("@f_rb", OleDbType.Boolean,1),
					new OleDbParameter("@y", OleDbType.Boolean,1),
					new OleDbParameter("@Fo", OleDbType.Boolean,1),
					new OleDbParameter("@Fpb", OleDbType.Boolean,1),
					new OleDbParameter("@Frb", OleDbType.Boolean,1),
					new OleDbParameter("@Fbb", OleDbType.Boolean,1),
					new OleDbParameter("@Fn", OleDbType.Boolean,1),
					new OleDbParameter("@Fn1", OleDbType.Boolean,1),
					new OleDbParameter("@Z", OleDbType.Boolean,1),
					new OleDbParameter("@S", OleDbType.Boolean,1),
					new OleDbParameter("@W", OleDbType.Boolean,1),
					new OleDbParameter("@I", OleDbType.Boolean,1),
					new OleDbParameter("@Eb", OleDbType.Boolean,1),
					new OleDbParameter("@σpb", OleDbType.Boolean,1),
					new OleDbParameter("@σrb", OleDbType.Boolean,1),
					new OleDbParameter("@σbb", OleDbType.Boolean,1),
					new OleDbParameter("@U", OleDbType.Boolean,1),
					new OleDbParameter("@Mean", OleDbType.Boolean,1),
					new OleDbParameter("@SD", OleDbType.Boolean,1),
					new OleDbParameter("@Mid", OleDbType.Boolean,1),
					new OleDbParameter("@CV", OleDbType.Boolean,1),
					new OleDbParameter("@saveCurve", OleDbType.Boolean,1)};
            parameters[0].Value = model.methodName;
            parameters[1].Value = model.f_bb;
            parameters[2].Value = model.f_n;
            parameters[3].Value = model.f_n1;
            parameters[4].Value = model.f_rb;
            parameters[5].Value = model.y;
            parameters[6].Value = model.Fo;
            parameters[7].Value = model.Fpb;
            parameters[8].Value = model.Frb;
            parameters[9].Value = model.Fbb;
            parameters[10].Value = model.Fn;
            parameters[11].Value = model.Fn1;
            parameters[12].Value = model.Z;
            parameters[13].Value = model.S;
            parameters[14].Value = model.W;
            parameters[15].Value = model.I;
            parameters[16].Value = model.Eb;
            parameters[17].Value = model.σpb;
            parameters[18].Value = model.σrb;
            parameters[19].Value = model.σbb;
            parameters[20].Value = model.U;
            parameters[21].Value = model.Mean;
            parameters[22].Value = model.SD;
            parameters[23].Value = model.Mid;
            parameters[24].Value = model.CV;
            parameters[25].Value = model.saveCurve;

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
        public bool Update(HR_Test.Model.SelTestResult_B model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_SelTestResult_B set ");
            strSql.Append("methodName=@methodName,");
            strSql.Append("f_bb=@f_bb,");
            strSql.Append("f_n=@f_n,");
            strSql.Append("f_n1=@f_n1,");
            strSql.Append("f_rb=@f_rb,");
            strSql.Append("y=@y,");
            strSql.Append("Fo=@Fo,");
            strSql.Append("Fpb=@Fpb,");
            strSql.Append("Frb=@Frb,");
            strSql.Append("Fbb=@Fbb,");
            strSql.Append("Fn=@Fn,");
            strSql.Append("Fn1=@Fn1,");
            strSql.Append("Z=@Z,");
            strSql.Append("S=@S,");
            strSql.Append("W=@W,");
            strSql.Append("I=@I,");
            strSql.Append("Eb=@Eb,");
            strSql.Append("σpb=@σpb,");
            strSql.Append("σrb=@σrb,");
            strSql.Append("σbb=@σbb,");
            strSql.Append("U=@U,");
            strSql.Append("Mean=@Mean,");
            strSql.Append("SD=@SD,");
            strSql.Append("Mid=@Mid,");
            strSql.Append("CV=@CV,");
            strSql.Append("saveCurve=@saveCurve");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,100),
					new OleDbParameter("@f_bb", OleDbType.Boolean,1),
					new OleDbParameter("@f_n", OleDbType.Boolean,1),
					new OleDbParameter("@f_n1", OleDbType.Boolean,1),
					new OleDbParameter("@f_rb", OleDbType.Boolean,1),
					new OleDbParameter("@y", OleDbType.Boolean,1),
					new OleDbParameter("@Fo", OleDbType.Boolean,1),
					new OleDbParameter("@Fpb", OleDbType.Boolean,1),
					new OleDbParameter("@Frb", OleDbType.Boolean,1),
					new OleDbParameter("@Fbb", OleDbType.Boolean,1),
					new OleDbParameter("@Fn", OleDbType.Boolean,1),
					new OleDbParameter("@Fn1", OleDbType.Boolean,1),
					new OleDbParameter("@Z", OleDbType.Boolean,1),
					new OleDbParameter("@S", OleDbType.Boolean,1),
					new OleDbParameter("@W", OleDbType.Boolean,1),
					new OleDbParameter("@I", OleDbType.Boolean,1),
					new OleDbParameter("@Eb", OleDbType.Boolean,1),
					new OleDbParameter("@σpb", OleDbType.Boolean,1),
					new OleDbParameter("@σrb", OleDbType.Boolean,1),
					new OleDbParameter("@σbb", OleDbType.Boolean,1),
					new OleDbParameter("@U", OleDbType.Boolean,1),
					new OleDbParameter("@Mean", OleDbType.Boolean,1),
					new OleDbParameter("@SD", OleDbType.Boolean,1),
					new OleDbParameter("@Mid", OleDbType.Boolean,1),
					new OleDbParameter("@CV", OleDbType.Boolean,1),
					new OleDbParameter("@saveCurve", OleDbType.Boolean,1),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
            parameters[0].Value = model.methodName;
            parameters[1].Value = model.f_bb;
            parameters[2].Value = model.f_n;
            parameters[3].Value = model.f_n1;
            parameters[4].Value = model.f_rb;
            parameters[5].Value = model.y;
            parameters[6].Value = model.Fo;
            parameters[7].Value = model.Fpb;
            parameters[8].Value = model.Frb;
            parameters[9].Value = model.Fbb;
            parameters[10].Value = model.Fn;
            parameters[11].Value = model.Fn1;
            parameters[12].Value = model.Z;
            parameters[13].Value = model.S;
            parameters[14].Value = model.W;
            parameters[15].Value = model.I;
            parameters[16].Value = model.Eb;
            parameters[17].Value = model.σpb;
            parameters[18].Value = model.σrb;
            parameters[19].Value = model.σbb;
            parameters[20].Value = model.U;
            parameters[21].Value = model.Mean;
            parameters[22].Value = model.SD;
            parameters[23].Value = model.Mid;
            parameters[24].Value = model.CV;
            parameters[25].Value = model.saveCurve;
            parameters[26].Value = model.ID;

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
            strSql.Append("delete from tb_SelTestResult_B ");
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
        public bool Delete(string methodname)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_SelTestResult_B ");
            strSql.Append(" where methodname=@methodname");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodname", OleDbType.VarChar,200)
			};
            parameters[0].Value = methodname;

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
            strSql.Append("delete from tb_SelTestResult_B ");
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
        public HR_Test.Model.SelTestResult_B GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,methodName,f_bb,f_n,f_n1,f_rb,y,Fo,Fpb,Frb,Fbb,Fn,Fn1,Z,S,W,I,Eb,σpb,σrb,σbb,U,Mean,SD,Mid,CV,saveCurve from tb_SelTestResult_B ");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
            parameters[0].Value = ID;

            HR_Test.Model.SelTestResult_B model = new HR_Test.Model.SelTestResult_B();
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
        public HR_Test.Model.SelTestResult_B GetModel(string methodName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,methodName,f_bb,f_n,f_n1,f_rb,y,Fo,Fpb,Frb,Fbb,Fn,Fn1,Z,S,W,I,Eb,σpb,σrb,σbb,U,Mean,SD,Mid,CV,saveCurve from tb_SelTestResult_B ");
            strSql.Append(" where methodName=@methodName");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,200)
			};
            parameters[0].Value = methodName;

            HR_Test.Model.SelTestResult_B model = new HR_Test.Model.SelTestResult_B();
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
        public HR_Test.Model.SelTestResult_B DataRowToModel(DataRow row)
        {
            HR_Test.Model.SelTestResult_B model = new HR_Test.Model.SelTestResult_B();
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
                if (row["f_bb"] != null && row["f_bb"].ToString() != "")
                {
                    if ((row["f_bb"].ToString() == "1") || (row["f_bb"].ToString().ToLower() == "true"))
                    {
                        model.f_bb = true;
                    }
                    else
                    {
                        model.f_bb = false;
                    }
                }
                if (row["f_n"] != null && row["f_n"].ToString() != "")
                {
                    if ((row["f_n"].ToString() == "1") || (row["f_n"].ToString().ToLower() == "true"))
                    {
                        model.f_n = true;
                    }
                    else
                    {
                        model.f_n = false;
                    }
                }
                if (row["f_n1"] != null && row["f_n1"].ToString() != "")
                {
                    if ((row["f_n1"].ToString() == "1") || (row["f_n1"].ToString().ToLower() == "true"))
                    {
                        model.f_n1 = true;
                    }
                    else
                    {
                        model.f_n1 = false;
                    }
                }
                if (row["f_rb"] != null && row["f_rb"].ToString() != "")
                {
                    if ((row["f_rb"].ToString() == "1") || (row["f_rb"].ToString().ToLower() == "true"))
                    {
                        model.f_rb = true;
                    }
                    else
                    {
                        model.f_rb = false;
                    }
                }
                if (row["y"] != null && row["y"].ToString() != "")
                {
                    if ((row["y"].ToString() == "1") || (row["y"].ToString().ToLower() == "true"))
                    {
                        model.y = true;
                    }
                    else
                    {
                        model.y = false;
                    }
                }
                if (row["Fo"] != null && row["Fo"].ToString() != "")
                {
                    if ((row["Fo"].ToString() == "1") || (row["Fo"].ToString().ToLower() == "true"))
                    {
                        model.Fo = true;
                    }
                    else
                    {
                        model.Fo = false;
                    }
                }
                if (row["Fpb"] != null && row["Fpb"].ToString() != "")
                {
                    if ((row["Fpb"].ToString() == "1") || (row["Fpb"].ToString().ToLower() == "true"))
                    {
                        model.Fpb = true;
                    }
                    else
                    {
                        model.Fpb = false;
                    }
                }
                if (row["Frb"] != null && row["Frb"].ToString() != "")
                {
                    if ((row["Frb"].ToString() == "1") || (row["Frb"].ToString().ToLower() == "true"))
                    {
                        model.Frb = true;
                    }
                    else
                    {
                        model.Frb = false;
                    }
                }
                if (row["Fbb"] != null && row["Fbb"].ToString() != "")
                {
                    if ((row["Fbb"].ToString() == "1") || (row["Fbb"].ToString().ToLower() == "true"))
                    {
                        model.Fbb = true;
                    }
                    else
                    {
                        model.Fbb = false;
                    }
                }
                if (row["Fn"] != null && row["Fn"].ToString() != "")
                {
                    if ((row["Fn"].ToString() == "1") || (row["Fn"].ToString().ToLower() == "true"))
                    {
                        model.Fn = true;
                    }
                    else
                    {
                        model.Fn = false;
                    }
                }
                if (row["Fn1"] != null && row["Fn1"].ToString() != "")
                {
                    if ((row["Fn1"].ToString() == "1") || (row["Fn1"].ToString().ToLower() == "true"))
                    {
                        model.Fn1 = true;
                    }
                    else
                    {
                        model.Fn1 = false;
                    }
                }
                if (row["Z"] != null && row["Z"].ToString() != "")
                {
                    if ((row["Z"].ToString() == "1") || (row["Z"].ToString().ToLower() == "true"))
                    {
                        model.Z = true;
                    }
                    else
                    {
                        model.Z = false;
                    }
                }
                if (row["S"] != null && row["S"].ToString() != "")
                {
                    if ((row["S"].ToString() == "1") || (row["S"].ToString().ToLower() == "true"))
                    {
                        model.S = true;
                    }
                    else
                    {
                        model.S = false;
                    }
                }
                if (row["W"] != null && row["W"].ToString() != "")
                {
                    if ((row["W"].ToString() == "1") || (row["W"].ToString().ToLower() == "true"))
                    {
                        model.W = true;
                    }
                    else
                    {
                        model.W = false;
                    }
                }
                if (row["I"] != null && row["I"].ToString() != "")
                {
                    if ((row["I"].ToString() == "1") || (row["I"].ToString().ToLower() == "true"))
                    {
                        model.I = true;
                    }
                    else
                    {
                        model.I = false;
                    }
                }
                if (row["Eb"] != null && row["Eb"].ToString() != "")
                {
                    if ((row["Eb"].ToString() == "1") || (row["Eb"].ToString().ToLower() == "true"))
                    {
                        model.Eb = true;
                    }
                    else
                    {
                        model.Eb = false;
                    }
                }
                if (row["σpb"] != null && row["σpb"].ToString() != "")
                {
                    if ((row["σpb"].ToString() == "1") || (row["σpb"].ToString().ToLower() == "true"))
                    {
                        model.σpb = true;
                    }
                    else
                    {
                        model.σpb = false;
                    }
                }
                if (row["σrb"] != null && row["σrb"].ToString() != "")
                {
                    if ((row["σrb"].ToString() == "1") || (row["σrb"].ToString().ToLower() == "true"))
                    {
                        model.σrb = true;
                    }
                    else
                    {
                        model.σrb = false;
                    }
                }
                if (row["σbb"] != null && row["σbb"].ToString() != "")
                {
                    if ((row["σbb"].ToString() == "1") || (row["σbb"].ToString().ToLower() == "true"))
                    {
                        model.σbb = true;
                    }
                    else
                    {
                        model.σbb = false;
                    }
                }
                if (row["U"] != null && row["U"].ToString() != "")
                {
                    if ((row["U"].ToString() == "1") || (row["U"].ToString().ToLower() == "true"))
                    {
                        model.U = true;
                    }
                    else
                    {
                        model.U = false;
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
            strSql.Append("select ID,methodName,f_bb,f_n,f_n1,f_rb,y,Fo,Fpb,Frb,Fbb,Fn,Fn1,Z,S,W,I,Eb,σpb,σrb,σbb,U,Mean,SD,Mid,CV,saveCurve ");
            strSql.Append(" FROM tb_SelTestResult_B ");
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
            strSql.Append("select count(1) FROM tb_SelTestResult_B ");
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
            strSql.Append(")AS Row, T.*  from tb_SelTestResult_B T ");
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
            parameters[0].Value = "tb_SelTestResult_B";
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

