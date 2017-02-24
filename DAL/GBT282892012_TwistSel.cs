using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
	/// <summary>
	/// 数据访问类:GBT282892012_TwistSel
	/// </summary>
	public partial class GBT282892012_TwistSel
	{
		public GBT282892012_TwistSel()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperOleDb.GetMaxID("ID", "tb_GBT282892012_TwistSel"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tb_GBT282892012_TwistSel");
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
		public bool Add(HR_Test.Model.GBT282892012_TwistSel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_GBT282892012_TwistSel(");
			strSql.Append("methodName,FMmax,M,M_,SM,Mc,saveCurve)");
			strSql.Append(" values (");
			strSql.Append("@methodName,@FMmax,@M,@M_,@SM,@Mc,@saveCurve)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,255),
					new OleDbParameter("@FMmax", OleDbType.Boolean,1),
					new OleDbParameter("@M", OleDbType.Boolean,1),
					new OleDbParameter("@M_", OleDbType.Boolean,1),
					new OleDbParameter("@SM", OleDbType.Boolean,1),
					new OleDbParameter("@Mc", OleDbType.Boolean,1),
					new OleDbParameter("@saveCurve", OleDbType.Boolean,1)};
			parameters[0].Value = model.methodName;
			parameters[1].Value = model.FMmax;
			parameters[2].Value = model.M;
			parameters[3].Value = model.M_;
			parameters[4].Value = model.SM;
			parameters[5].Value = model.Mc;
			parameters[6].Value = model.saveCurve;

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
		public bool Update(HR_Test.Model.GBT282892012_TwistSel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_GBT282892012_TwistSel set ");
			strSql.Append("methodName=@methodName,");
			strSql.Append("FMmax=@FMmax,");
			strSql.Append("M=@M,");
			strSql.Append("M_=@M_,");
			strSql.Append("SM=@SM,");
			strSql.Append("Mc=@Mc,");
			strSql.Append("saveCurve=@saveCurve");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,255),
					new OleDbParameter("@FMmax", OleDbType.Boolean,1),
					new OleDbParameter("@M", OleDbType.Boolean,1),
					new OleDbParameter("@M_", OleDbType.Boolean,1),
					new OleDbParameter("@SM", OleDbType.Boolean,1),
					new OleDbParameter("@Mc", OleDbType.Boolean,1),
					new OleDbParameter("@saveCurve", OleDbType.Boolean,1),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
			parameters[0].Value = model.methodName;
			parameters[1].Value = model.FMmax;
			parameters[2].Value = model.M;
			parameters[3].Value = model.M_;
			parameters[4].Value = model.SM;
			parameters[5].Value = model.Mc;
			parameters[6].Value = model.saveCurve;
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
        public bool Delete(string methodName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_GBT282892012_TwistSel ");
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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_GBT282892012_TwistSel ");
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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_GBT282892012_TwistSel ");
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
		public HR_Test.Model.GBT282892012_TwistSel GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,methodName,FMmax,M,M_,SM,Mc,saveCurve from tb_GBT282892012_TwistSel ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			HR_Test.Model.GBT282892012_TwistSel model=new HR_Test.Model.GBT282892012_TwistSel();
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
        public HR_Test.Model.GBT282892012_TwistSel GetModel(string methodName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,methodName,FMmax,M,M_,SM,Mc,saveCurve from tb_GBT282892012_TwistSel ");
            strSql.Append(" where methodName=@methodName");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar)
			};
            parameters[0].Value = methodName;

            HR_Test.Model.GBT282892012_TwistSel model = new HR_Test.Model.GBT282892012_TwistSel();
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
		public HR_Test.Model.GBT282892012_TwistSel DataRowToModel(DataRow row)
		{
			HR_Test.Model.GBT282892012_TwistSel model=new HR_Test.Model.GBT282892012_TwistSel();
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
				if(row["FMmax"]!=null && row["FMmax"].ToString()!="")
				{
					if((row["FMmax"].ToString()=="1")||(row["FMmax"].ToString().ToLower()=="true"))
					{
						model.FMmax=true;
					}
					else
					{
						model.FMmax=false;
					}
				}
				if(row["M"]!=null && row["M"].ToString()!="")
				{
					if((row["M"].ToString()=="1")||(row["M"].ToString().ToLower()=="true"))
					{
						model.M=true;
					}
					else
					{
						model.M=false;
					}
				}
				if(row["M_"]!=null && row["M_"].ToString()!="")
				{
					if((row["M_"].ToString()=="1")||(row["M_"].ToString().ToLower()=="true"))
					{
						model.M_=true;
					}
					else
					{
						model.M_=false;
					}
				}
				if(row["SM"]!=null && row["SM"].ToString()!="")
				{
					if((row["SM"].ToString()=="1")||(row["SM"].ToString().ToLower()=="true"))
					{
						model.SM=true;
					}
					else
					{
						model.SM=false;
					}
				}
				if(row["Mc"]!=null && row["Mc"].ToString()!="")
				{
					if((row["Mc"].ToString()=="1")||(row["Mc"].ToString().ToLower()=="true"))
					{
						model.Mc=true;
					}
					else
					{
						model.Mc=false;
					}
				}
				if(row["saveCurve"]!=null && row["saveCurve"].ToString()!="")
				{
					if((row["saveCurve"].ToString()=="1")||(row["saveCurve"].ToString().ToLower()=="true"))
					{
						model.saveCurve=true;
					}
					else
					{
						model.saveCurve=false;
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
			strSql.Append("select ID,methodName,FMmax,M,M_,SM,Mc,saveCurve ");
			strSql.Append(" FROM tb_GBT282892012_TwistSel ");
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
			strSql.Append("select count(1) FROM tb_GBT282892012_TwistSel ");
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
			strSql.Append(")AS Row, T.*  from tb_GBT282892012_TwistSel T ");
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
			parameters[0].Value = "tb_GBT282892012_TwistSel";
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

