using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
	/// <summary>
	/// 数据访问类:GBT282892012_TensileSel
	/// </summary>
	public partial class GBT282892012_TensileSel
	{
		public GBT282892012_TensileSel()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperOleDb.GetMaxID("ID", "tb_GBT282892012_TensileSel"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tb_GBT282892012_TensileSel");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			return DbHelperOleDb.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(HR_Test.Model.GBT282892012_TensileSel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_GBT282892012_TensileSel(");
			strSql.Append("methodName,FQmax,Q,Q_,SQ,Qc,isSaveCurve)");
			strSql.Append(" values (");
			strSql.Append("@methodName,@FQmax,@Q,@Q_,@SQ,@Qc,@isSaveCurve)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,255),
					new OleDbParameter("@FQmax", OleDbType.Boolean,1),
					new OleDbParameter("@Q", OleDbType.Boolean,1),
					new OleDbParameter("@Q_", OleDbType.Boolean,1),
					new OleDbParameter("@SQ", OleDbType.Boolean,1),
					new OleDbParameter("@Qc", OleDbType.Boolean,1),
					new OleDbParameter("@isSaveCurve", OleDbType.Boolean,1)};
			parameters[0].Value = model.methodName;
			parameters[1].Value = model.FQmax;
			parameters[2].Value = model.Q;
			parameters[3].Value = model.Q_;
			parameters[4].Value = model.SQ;
			parameters[5].Value = model.Qc;
			parameters[6].Value = model.isSaveCurve;

			int rows=DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Update(HR_Test.Model.GBT282892012_TensileSel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_GBT282892012_TensileSel set ");
			strSql.Append("methodName=@methodName,");
			strSql.Append("FQmax=@FQmax,");
			strSql.Append("Q=@Q,");
			strSql.Append("Q_=@Q_,");
			strSql.Append("SQ=@SQ,");
			strSql.Append("Qc=@Qc,");
			strSql.Append("isSaveCurve=@isSaveCurve");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,255),
					new OleDbParameter("@FQmax", OleDbType.Boolean,1),
					new OleDbParameter("@Q", OleDbType.Boolean,1),
					new OleDbParameter("@Q_", OleDbType.Boolean,1),
					new OleDbParameter("@SQ", OleDbType.Boolean,1),
					new OleDbParameter("@Qc", OleDbType.Boolean,1),
					new OleDbParameter("@isSaveCurve", OleDbType.Boolean,1),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
			parameters[0].Value = model.methodName;
			parameters[1].Value = model.FQmax;
			parameters[2].Value = model.Q;
			parameters[3].Value = model.Q_;
			parameters[4].Value = model.SQ;
			parameters[5].Value = model.Qc;
			parameters[6].Value = model.isSaveCurve;
			parameters[7].Value = model.ID;

			int rows=DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_GBT282892012_TensileSel ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			int rows=DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
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
            strSql.Append("delete from tb_GBT282892012_TensileSel ");
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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_GBT282892012_TensileSel ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperOleDb.ExecuteSql(strSql.ToString());
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
		public HR_Test.Model.GBT282892012_TensileSel GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,methodName,FQmax,Q,Q_,SQ,Qc,isSaveCurve from tb_GBT282892012_TensileSel ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			HR_Test.Model.GBT282892012_TensileSel model=new HR_Test.Model.GBT282892012_TensileSel();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
        public HR_Test.Model.GBT282892012_TensileSel GetModel(string methodName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,methodName,FQmax,Q,Q_,SQ,Qc,isSaveCurve from tb_GBT282892012_TensileSel ");
            strSql.Append(" where methodName=@methodName");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar)
			};
            parameters[0].Value = methodName;

            HR_Test.Model.GBT282892012_TensileSel model = new HR_Test.Model.GBT282892012_TensileSel();
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
		public HR_Test.Model.GBT282892012_TensileSel DataRowToModel(DataRow row)
		{
			HR_Test.Model.GBT282892012_TensileSel model=new HR_Test.Model.GBT282892012_TensileSel();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["methodName"]!=null)
				{
					model.methodName=row["methodName"].ToString();
				}
				if(row["FQmax"]!=null && row["FQmax"].ToString()!="")
				{
					if((row["FQmax"].ToString()=="1")||(row["FQmax"].ToString().ToLower()=="true"))
					{
						model.FQmax=true;
					}
					else
					{
						model.FQmax=false;
					}
				}
				if(row["Q"]!=null && row["Q"].ToString()!="")
				{
					if((row["Q"].ToString()=="1")||(row["Q"].ToString().ToLower()=="true"))
					{
						model.Q=true;
					}
					else
					{
						model.Q=false;
					}
				}
				if(row["Q_"]!=null && row["Q_"].ToString()!="")
				{
					if((row["Q_"].ToString()=="1")||(row["Q_"].ToString().ToLower()=="true"))
					{
						model.Q_=true;
					}
					else
					{
						model.Q_=false;
					}
				}
				if(row["SQ"]!=null && row["SQ"].ToString()!="")
				{
					if((row["SQ"].ToString()=="1")||(row["SQ"].ToString().ToLower()=="true"))
					{
						model.SQ=true;
					}
					else
					{
						model.SQ=false;
					}
				}
				if(row["Qc"]!=null && row["Qc"].ToString()!="")
				{
					if((row["Qc"].ToString()=="1")||(row["Qc"].ToString().ToLower()=="true"))
					{
						model.Qc=true;
					}
					else
					{
						model.Qc=false;
					}
				}
				if(row["isSaveCurve"]!=null && row["isSaveCurve"].ToString()!="")
				{
					if((row["isSaveCurve"].ToString()=="1")||(row["isSaveCurve"].ToString().ToLower()=="true"))
					{
						model.isSaveCurve=true;
					}
					else
					{
						model.isSaveCurve=false;
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,methodName,FQmax,Q,Q_,SQ,Qc,isSaveCurve ");
			strSql.Append(" FROM tb_GBT282892012_TensileSel ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM tb_GBT282892012_TensileSel ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from tb_GBT282892012_TensileSel T ");
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
			parameters[0].Value = "tb_GBT282892012_TensileSel";
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

