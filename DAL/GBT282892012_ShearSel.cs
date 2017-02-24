using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
	/// <summary>
	/// 数据访问类:GBT282892012_ShearSel
	/// </summary>
	public partial class GBT282892012_ShearSel
	{
		public GBT282892012_ShearSel()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperOleDb.GetMaxID("ID", "tb_GBT282892012_ShearSel"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tb_GBT282892012_ShearSel");
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
		public bool Add(HR_Test.Model.GBT282892012_ShearSel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_GBT282892012_ShearSel(");
			strSql.Append("methodName,FTmax,T,C1,C1R_,C1cR,C1L_,C1cL,C1H_,C1cH,T_,ST,Tc,saveCurve)");
			strSql.Append(" values (");
			strSql.Append("@methodName,@FTmax,@T,@C1,@C1R_,@C1cR,@C1L_,@C1cL,@C1H_,@C1cH,@T_,@ST,@Tc,@saveCurve)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,255),
					new OleDbParameter("@FTmax", OleDbType.Boolean,1),
					new OleDbParameter("@T", OleDbType.Boolean,1),
					new OleDbParameter("@C1", OleDbType.Boolean,1),
					new OleDbParameter("@C1R_", OleDbType.Boolean,1),
					new OleDbParameter("@C1cR", OleDbType.Boolean,1),
					new OleDbParameter("@C1L_", OleDbType.Boolean,1),
					new OleDbParameter("@C1cL", OleDbType.Boolean,1),
					new OleDbParameter("@C1H_", OleDbType.Boolean,1),
					new OleDbParameter("@C1cH", OleDbType.Boolean,1),
					new OleDbParameter("@T_", OleDbType.Boolean,1),
					new OleDbParameter("@ST", OleDbType.Boolean,1),
					new OleDbParameter("@Tc", OleDbType.Boolean,1),
					new OleDbParameter("@saveCurve", OleDbType.Boolean,1)};
			parameters[0].Value = model.methodName;
			parameters[1].Value = model.FTmax;
			parameters[2].Value = model.T;
			parameters[3].Value = model.C1;
			parameters[4].Value = model.C1R_;
			parameters[5].Value = model.C1cR;
			parameters[6].Value = model.C1L_;
			parameters[7].Value = model.C1cL;
			parameters[8].Value = model.C1H_;
			parameters[9].Value = model.C1cH;
			parameters[10].Value = model.T_;
			parameters[11].Value = model.ST;
			parameters[12].Value = model.Tc;
			parameters[13].Value = model.saveCurve;

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
		public bool Update(HR_Test.Model.GBT282892012_ShearSel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_GBT282892012_ShearSel set ");
			strSql.Append("methodName=@methodName,");
			strSql.Append("FTmax=@FTmax,");
			strSql.Append("T=@T,");
			strSql.Append("C1=@C1,");
			strSql.Append("C1R_=@C1R_,");
			strSql.Append("C1cR=@C1cR,");
			strSql.Append("C1L_=@C1L_,");
			strSql.Append("C1cL=@C1cL,");
			strSql.Append("C1H_=@C1H_,");
			strSql.Append("C1cH=@C1cH,");
			strSql.Append("T_=@T_,");
			strSql.Append("ST=@ST,");
			strSql.Append("Tc=@Tc,");
			strSql.Append("saveCurve=@saveCurve");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,255),
					new OleDbParameter("@FTmax", OleDbType.Boolean,1),
					new OleDbParameter("@T", OleDbType.Boolean,1),
					new OleDbParameter("@C1", OleDbType.Boolean,1),
					new OleDbParameter("@C1R_", OleDbType.Boolean,1),
					new OleDbParameter("@C1cR", OleDbType.Boolean,1),
					new OleDbParameter("@C1L_", OleDbType.Boolean,1),
					new OleDbParameter("@C1cL", OleDbType.Boolean,1),
					new OleDbParameter("@C1H_", OleDbType.Boolean,1),
					new OleDbParameter("@C1cH", OleDbType.Boolean,1),
					new OleDbParameter("@T_", OleDbType.Boolean,1),
					new OleDbParameter("@ST", OleDbType.Boolean,1),
					new OleDbParameter("@Tc", OleDbType.Boolean,1),
					new OleDbParameter("@saveCurve", OleDbType.Boolean,1),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
			parameters[0].Value = model.methodName;
			parameters[1].Value = model.FTmax;
			parameters[2].Value = model.T;
			parameters[3].Value = model.C1;
			parameters[4].Value = model.C1R_;
			parameters[5].Value = model.C1cR;
			parameters[6].Value = model.C1L_;
			parameters[7].Value = model.C1cL;
			parameters[8].Value = model.C1H_;
			parameters[9].Value = model.C1cH;
			parameters[10].Value = model.T_;
			parameters[11].Value = model.ST;
			parameters[12].Value = model.Tc;
			parameters[13].Value = model.saveCurve;
			parameters[14].Value = model.ID;

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
            strSql.Append("delete from tb_GBT282892012_ShearSel ");
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
			strSql.Append("delete from tb_GBT282892012_ShearSel ");
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
			strSql.Append("delete from tb_GBT282892012_ShearSel ");
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
		public HR_Test.Model.GBT282892012_ShearSel GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,methodName,FTmax,T,C1,C1R_,C1cR,C1L_,C1cL,C1H_,C1cH,T_,ST,Tc,saveCurve from tb_GBT282892012_ShearSel ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			HR_Test.Model.GBT282892012_ShearSel model=new HR_Test.Model.GBT282892012_ShearSel();
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
        public HR_Test.Model.GBT282892012_ShearSel GetModel(string methodName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,methodName,FTmax,T,C1,C1R_,C1cR,C1L_,C1cL,C1H_,C1cH,T_,ST,Tc,saveCurve from tb_GBT282892012_ShearSel ");
            strSql.Append(" where methodName=@methodName");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar)
			};
            parameters[0].Value = methodName;

            HR_Test.Model.GBT282892012_ShearSel model = new HR_Test.Model.GBT282892012_ShearSel();
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
		public HR_Test.Model.GBT282892012_ShearSel DataRowToModel(DataRow row)
		{
			HR_Test.Model.GBT282892012_ShearSel model=new HR_Test.Model.GBT282892012_ShearSel();
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
				if(row["FTmax"]!=null && row["FTmax"].ToString()!="")
				{
					if((row["FTmax"].ToString()=="1")||(row["FTmax"].ToString().ToLower()=="true"))
					{
						model.FTmax=true;
					}
					else
					{
						model.FTmax=false;
					}
				}
				if(row["T"]!=null && row["T"].ToString()!="")
				{
					if((row["T"].ToString()=="1")||(row["T"].ToString().ToLower()=="true"))
					{
						model.T=true;
					}
					else
					{
						model.T=false;
					}
				}
				if(row["C1"]!=null && row["C1"].ToString()!="")
				{
					if((row["C1"].ToString()=="1")||(row["C1"].ToString().ToLower()=="true"))
					{
						model.C1=true;
					}
					else
					{
						model.C1=false;
					}
				}
				if(row["C1R_"]!=null && row["C1R_"].ToString()!="")
				{
					if((row["C1R_"].ToString()=="1")||(row["C1R_"].ToString().ToLower()=="true"))
					{
						model.C1R_=true;
					}
					else
					{
						model.C1R_=false;
					}
				}
				if(row["C1cR"]!=null && row["C1cR"].ToString()!="")
				{
					if((row["C1cR"].ToString()=="1")||(row["C1cR"].ToString().ToLower()=="true"))
					{
						model.C1cR=true;
					}
					else
					{
						model.C1cR=false;
					}
				}
				if(row["C1L_"]!=null && row["C1L_"].ToString()!="")
				{
					if((row["C1L_"].ToString()=="1")||(row["C1L_"].ToString().ToLower()=="true"))
					{
						model.C1L_=true;
					}
					else
					{
						model.C1L_=false;
					}
				}
				if(row["C1cL"]!=null && row["C1cL"].ToString()!="")
				{
					if((row["C1cL"].ToString()=="1")||(row["C1cL"].ToString().ToLower()=="true"))
					{
						model.C1cL=true;
					}
					else
					{
						model.C1cL=false;
					}
				}
				if(row["C1H_"]!=null && row["C1H_"].ToString()!="")
				{
					if((row["C1H_"].ToString()=="1")||(row["C1H_"].ToString().ToLower()=="true"))
					{
						model.C1H_=true;
					}
					else
					{
						model.C1H_=false;
					}
				}
				if(row["C1cH"]!=null && row["C1cH"].ToString()!="")
				{
					if((row["C1cH"].ToString()=="1")||(row["C1cH"].ToString().ToLower()=="true"))
					{
						model.C1cH=true;
					}
					else
					{
						model.C1cH=false;
					}
				}
				if(row["T_"]!=null && row["T_"].ToString()!="")
				{
					if((row["T_"].ToString()=="1")||(row["T_"].ToString().ToLower()=="true"))
					{
						model.T_=true;
					}
					else
					{
						model.T_=false;
					}
				}
				if(row["ST"]!=null && row["ST"].ToString()!="")
				{
					if((row["ST"].ToString()=="1")||(row["ST"].ToString().ToLower()=="true"))
					{
						model.ST=true;
					}
					else
					{
						model.ST=false;
					}
				}
				if(row["Tc"]!=null && row["Tc"].ToString()!="")
				{
					if((row["Tc"].ToString()=="1")||(row["Tc"].ToString().ToLower()=="true"))
					{
						model.Tc=true;
					}
					else
					{
						model.Tc=false;
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
			strSql.Append("select ID,methodName,FTmax,T,C1,C1R_,C1cR,C1L_,C1cL,C1H_,C1cH,T_,ST,Tc,saveCurve ");
			strSql.Append(" FROM tb_GBT282892012_ShearSel ");
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
			strSql.Append("select count(1) FROM tb_GBT282892012_ShearSel ");
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
			strSql.Append(")AS Row, T.*  from tb_GBT282892012_ShearSel T ");
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
			parameters[0].Value = "tb_GBT282892012_ShearSel";
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

