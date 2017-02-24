using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
	/// <summary>
	/// 数据访问类:GBT236152009_SelZong
	/// </summary>
	public partial class GBT236152009_SelZong
	{
		public GBT236152009_SelZong()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperOleDb.GetMaxID("ID", "tb_GBT236152009_SelZong"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tb_GBT236152009_SelZong");
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
		public bool Add(HR_Test.Model.GBT236152009_SelZong model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_GBT236152009_SelZong(");
			strSql.Append("methodName,Fmax,T2,Z,E,T2_,S,T2c,isSaveCurve)");
			strSql.Append(" values (");
			strSql.Append("@methodName,@Fmax,@T2,@Z,@E,@T2_,@S,@T2c,@isSaveCurve)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,255),
					new OleDbParameter("@Fmax", OleDbType.Boolean,1),
					new OleDbParameter("@T2", OleDbType.Boolean,1),
					new OleDbParameter("@Z", OleDbType.Boolean,1),
					new OleDbParameter("@E", OleDbType.Boolean,1),
					new OleDbParameter("@T2_", OleDbType.Boolean,1),
					new OleDbParameter("@S", OleDbType.Boolean,1),
					new OleDbParameter("@T2c", OleDbType.Boolean,1),
					new OleDbParameter("@isSaveCurve", OleDbType.Boolean,1)};
			parameters[0].Value = model.methodName;
			parameters[1].Value = model.Fmax;
			parameters[2].Value = model.T2;
			parameters[3].Value = model.Z;
			parameters[4].Value = model.E;
			parameters[5].Value = model.T2_;
			parameters[6].Value = model.S;
			parameters[7].Value = model.T2c;
			parameters[8].Value = model.isSaveCurve;

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
		public bool Update(HR_Test.Model.GBT236152009_SelZong model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_GBT236152009_SelZong set ");
			strSql.Append("methodName=@methodName,");
			strSql.Append("Fmax=@Fmax,");
			strSql.Append("T2=@T2,");
			strSql.Append("Z=@Z,");
			strSql.Append("E=@E,");
			strSql.Append("T2_=@T2_,");
			strSql.Append("S=@S,");
			strSql.Append("T2c=@T2c,");
			strSql.Append("isSaveCurve=@isSaveCurve");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,255),
					new OleDbParameter("@Fmax", OleDbType.Boolean,1),
					new OleDbParameter("@T2", OleDbType.Boolean,1),
					new OleDbParameter("@Z", OleDbType.Boolean,1),
					new OleDbParameter("@E", OleDbType.Boolean,1),
					new OleDbParameter("@T2_", OleDbType.Boolean,1),
					new OleDbParameter("@S", OleDbType.Boolean,1),
					new OleDbParameter("@T2c", OleDbType.Boolean,1),
					new OleDbParameter("@isSaveCurve", OleDbType.Boolean,1),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
			parameters[0].Value = model.methodName;
			parameters[1].Value = model.Fmax;
			parameters[2].Value = model.T2;
			parameters[3].Value = model.Z;
			parameters[4].Value = model.E;
			parameters[5].Value = model.T2_;
			parameters[6].Value = model.S;
			parameters[7].Value = model.T2c;
			parameters[8].Value = model.isSaveCurve;
			parameters[9].Value = model.ID;

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
			strSql.Append("delete from tb_GBT236152009_SelZong ");
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
			strSql.Append("delete from tb_GBT236152009_SelZong ");
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
			strSql.Append("delete from tb_GBT236152009_SelZong ");
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
		public HR_Test.Model.GBT236152009_SelZong GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,methodName,Fmax,T2,Z,E,T2_,S,T2c,isSaveCurve from tb_GBT236152009_SelZong ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			HR_Test.Model.GBT236152009_SelZong model=new HR_Test.Model.GBT236152009_SelZong();
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
        public HR_Test.Model.GBT236152009_SelZong GetModel(string methodName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,methodName,Fmax,T2,Z,E,T2_,S,T2c,isSaveCurve from tb_GBT236152009_SelZong ");
            strSql.Append(" where methodName=@methodName");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar)
			};
            parameters[0].Value = methodName;

            HR_Test.Model.GBT236152009_SelZong model = new HR_Test.Model.GBT236152009_SelZong();
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
		public HR_Test.Model.GBT236152009_SelZong DataRowToModel(DataRow row)
		{
			HR_Test.Model.GBT236152009_SelZong model=new HR_Test.Model.GBT236152009_SelZong();
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
				if(row["T2"]!=null && row["T2"].ToString()!="")
				{
					if((row["T2"].ToString()=="1")||(row["T2"].ToString().ToLower()=="true"))
					{
						model.T2=true;
					}
					else
					{
						model.T2=false;
					}
				}
				if(row["Z"]!=null && row["Z"].ToString()!="")
				{
					if((row["Z"].ToString()=="1")||(row["Z"].ToString().ToLower()=="true"))
					{
						model.Z=true;
					}
					else
					{
						model.Z=false;
					}
				}
				if(row["E"]!=null && row["E"].ToString()!="")
				{
					if((row["E"].ToString()=="1")||(row["E"].ToString().ToLower()=="true"))
					{
						model.E=true;
					}
					else
					{
						model.E=false;
					}
				}
				if(row["T2_"]!=null && row["T2_"].ToString()!="")
				{
					if((row["T2_"].ToString()=="1")||(row["T2_"].ToString().ToLower()=="true"))
					{
						model.T2_=true;
					}
					else
					{
						model.T2_=false;
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
				if(row["T2c"]!=null && row["T2c"].ToString()!="")
				{
					if((row["T2c"].ToString()=="1")||(row["T2c"].ToString().ToLower()=="true"))
					{
						model.T2c=true;
					}
					else
					{
						model.T2c=false;
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
			strSql.Append("select ID,methodName,Fmax,T2,Z,E,T2_,S,T2c,isSaveCurve ");
			strSql.Append(" FROM tb_GBT236152009_SelZong ");
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
			strSql.Append("select count(1) FROM tb_GBT236152009_SelZong ");
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
			strSql.Append(")AS Row, T.*  from tb_GBT236152009_SelZong T ");
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
			parameters[0].Value = "tb_GBT236152009_SelZong";
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

