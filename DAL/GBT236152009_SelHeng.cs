using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
	/// <summary>
	/// 数据访问类:GBT236152009_SelHeng
	/// </summary>
	public partial class GBT236152009_SelHeng
	{
		public GBT236152009_SelHeng()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperOleDb.GetMaxID("ID", "tb_GBT236152009_SelHeng"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tb_GBT236152009_SelHeng");
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
		public bool Add(HR_Test.Model.GBT236152009_SelHeng model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_GBT236152009_SelHeng(");
			strSql.Append("methodName,Fmax,T1,T1_,S,T1c,isSaveCurve)");
			strSql.Append(" values (");
			strSql.Append("@methodName,@Fmax,@T1,@T1_,@S,@T1c,@isSaveCurve)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,255),
					new OleDbParameter("@Fmax", OleDbType.Boolean,1),
					new OleDbParameter("@T1", OleDbType.Boolean,1),
					new OleDbParameter("@T1_", OleDbType.Boolean,1),
					new OleDbParameter("@S", OleDbType.Boolean,1),
					new OleDbParameter("@T1c", OleDbType.Boolean,1),
					new OleDbParameter("@isSaveCurve", OleDbType.Boolean,1)};
			parameters[0].Value = model.methodName;
			parameters[1].Value = model.Fmax;
			parameters[2].Value = model.T1;
			parameters[3].Value = model.T1_;
			parameters[4].Value = model.S;
			parameters[5].Value = model.T1c;
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
		public bool Update(HR_Test.Model.GBT236152009_SelHeng model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_GBT236152009_SelHeng set ");
			strSql.Append("methodName=@methodName,");
			strSql.Append("Fmax=@Fmax,");
			strSql.Append("T1=@T1,");
			strSql.Append("T1_=@T1_,");
			strSql.Append("S=@S,");
			strSql.Append("T1c=@T1c,");
			strSql.Append("isSaveCurve=@isSaveCurve");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,255),
					new OleDbParameter("@Fmax", OleDbType.Boolean,1),
					new OleDbParameter("@T1", OleDbType.Boolean,1),
					new OleDbParameter("@T1_", OleDbType.Boolean,1),
					new OleDbParameter("@S", OleDbType.Boolean,1),
					new OleDbParameter("@T1c", OleDbType.Boolean,1),
					new OleDbParameter("@isSaveCurve", OleDbType.Boolean,1),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
			parameters[0].Value = model.methodName;
			parameters[1].Value = model.Fmax;
			parameters[2].Value = model.T1;
			parameters[3].Value = model.T1_;
			parameters[4].Value = model.S;
			parameters[5].Value = model.T1c;
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
			strSql.Append("delete from tb_GBT236152009_SelHeng ");
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_GBT236152009_SelHeng ");
            strSql.Append(" where methodName=@methodName");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar)
			};
            parameters[0].Value = methodName;

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
			strSql.Append("delete from tb_GBT236152009_SelHeng ");
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
		public HR_Test.Model.GBT236152009_SelHeng GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,methodName,Fmax,T1,T1_,S,T1c,isSaveCurve from tb_GBT236152009_SelHeng ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			HR_Test.Model.GBT236152009_SelHeng model=new HR_Test.Model.GBT236152009_SelHeng();
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
		public HR_Test.Model.GBT236152009_SelHeng GetModel(string methodName)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,methodName,Fmax,T1,T1_,S,T1c,isSaveCurve from tb_GBT236152009_SelHeng ");
            strSql.Append(" where methodName=@methodName");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar)
			};
            parameters[0].Value = methodName;

			HR_Test.Model.GBT236152009_SelHeng model=new HR_Test.Model.GBT236152009_SelHeng();
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
		public HR_Test.Model.GBT236152009_SelHeng DataRowToModel(DataRow row)
		{
			HR_Test.Model.GBT236152009_SelHeng model=new HR_Test.Model.GBT236152009_SelHeng();
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
				if(row["Fmax"]!=null && row["Fmax"].ToString()!="")
				{
					if((row["Fmax"].ToString()=="1")||(row["Fmax"].ToString().ToLower()=="true"))
					{
						model.Fmax=true;
					}
					else
					{
						model.Fmax=false;
					}
				}
				if(row["T1"]!=null && row["T1"].ToString()!="")
				{
					if((row["T1"].ToString()=="1")||(row["T1"].ToString().ToLower()=="true"))
					{
						model.T1=true;
					}
					else
					{
						model.T1=false;
					}
				}
				if(row["T1_"]!=null && row["T1_"].ToString()!="")
				{
					if((row["T1_"].ToString()=="1")||(row["T1_"].ToString().ToLower()=="true"))
					{
						model.T1_=true;
					}
					else
					{
						model.T1_=false;
					}
				}
				if(row["S"]!=null && row["S"].ToString()!="")
				{
					if((row["S"].ToString()=="1")||(row["S"].ToString().ToLower()=="true"))
					{
						model.S=true;
					}
					else
					{
						model.S=false;
					}
				}
				if(row["T1c"]!=null && row["T1c"].ToString()!="")
				{
					if((row["T1c"].ToString()=="1")||(row["T1c"].ToString().ToLower()=="true"))
					{
						model.T1c=true;
					}
					else
					{
						model.T1c=false;
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
			strSql.Append("select ID,methodName,Fmax,T1,T1_,S,T1c,isSaveCurve ");
			strSql.Append(" FROM tb_GBT236152009_SelHeng ");
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
			strSql.Append("select count(1) FROM tb_GBT236152009_SelHeng ");
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
			strSql.Append(")AS Row, T.*  from tb_GBT236152009_SelHeng T ");
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
			parameters[0].Value = "tb_GBT236152009_SelHeng";
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

