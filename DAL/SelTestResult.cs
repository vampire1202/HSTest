using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
	/// <summary>
	/// 数据访问类:SelTestResult
	/// </summary>
	public partial class SelTestResult
	{
		public SelTestResult()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string methodName)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tb_SelTestResult");
			strSql.Append(" where methodName=@methodName ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,100)};
			parameters[0].Value = methodName;

			return DbHelperOleDb.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(HR_Test.Model.SelTestResult model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_SelTestResult(");
			strSql.Append("methodName,Fm,Rm,ReH,ReL,Rp,Rt,Rr,E,m,mE,A,Aee,Agg,Att,Aggtt,Awnwn,deltaLm,Lf,Z,Avera,SS,Avera1,CV,Handaz,Savecurve,Lm)");
			strSql.Append(" values (");
			strSql.Append("@methodName,@Fm,@Rm,@ReH,@ReL,@Rp,@Rt,@Rr,@E,@m,@mE,@A,@Aee,@Agg,@Att,@Aggtt,@Awnwn,@deltaLm,@Lf,@Z,@Avera,@SS,@Avera1,@CV,@Handaz,@Savecurve,@Lm)");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,100),
					new OleDbParameter("@Fm", OleDbType.Boolean,1),
					new OleDbParameter("@Rm", OleDbType.Boolean,1),
					new OleDbParameter("@ReH", OleDbType.Boolean,1),
					new OleDbParameter("@ReL", OleDbType.Boolean,1),
					new OleDbParameter("@Rp", OleDbType.Boolean,1),
					new OleDbParameter("@Rt", OleDbType.Boolean,1),
					new OleDbParameter("@Rr", OleDbType.Boolean,1),					
					new OleDbParameter("@E", OleDbType.Boolean,1),
					new OleDbParameter("@m", OleDbType.Boolean,1),
					new OleDbParameter("@mE", OleDbType.Boolean,1),
					new OleDbParameter("@A", OleDbType.Boolean,1),
					new OleDbParameter("@Aee", OleDbType.Boolean,1),
					new OleDbParameter("@Agg", OleDbType.Boolean,1),
					new OleDbParameter("@Att", OleDbType.Boolean,1),
					new OleDbParameter("@Aggtt", OleDbType.Boolean,1),
					new OleDbParameter("@Awnwn", OleDbType.Boolean,1),
					new OleDbParameter("@deltaLm", OleDbType.Boolean,1),
					new OleDbParameter("@Lf", OleDbType.Boolean,1),
					new OleDbParameter("@Z", OleDbType.Boolean,1),
					new OleDbParameter("@Avera", OleDbType.Boolean,1),
					new OleDbParameter("@SS", OleDbType.Boolean,1),
					new OleDbParameter("@Avera1", OleDbType.Boolean,1),
                    new OleDbParameter("@CV", OleDbType.Boolean,1),
                     new OleDbParameter("@Handaz", OleDbType.Boolean,1),
                      new OleDbParameter("@Savecurve", OleDbType.Boolean,1),
                       new OleDbParameter("@Lm", OleDbType.Boolean,1)
                                          };
			parameters[0].Value = model.methodName;
			parameters[1].Value = model.Fm;
			parameters[2].Value = model.Rm;
			parameters[3].Value = model.ReH;
			parameters[4].Value = model.ReL;
			parameters[5].Value = model.Rp;
			parameters[6].Value = model.Rt;
			parameters[7].Value = model.Rr;
			parameters[8].Value = model.E;
			parameters[9].Value = model.m;
			parameters[10].Value = model.mE;
			parameters[11].Value = model.A;
			parameters[12].Value = model.Aee;
			parameters[13].Value = model.Agg;
			parameters[14].Value = model.Att;
			parameters[15].Value = model.Aggtt;
			parameters[16].Value = model.Awnwn;
			parameters[17].Value = model.deltaLm;
			parameters[18].Value = model.Lf;
			parameters[19].Value = model.Z;
			parameters[20].Value = model.Avera;
			parameters[21].Value = model.SS;
			parameters[22].Value = model.Avera1;
            parameters[23].Value = model.CV;
            parameters[24].Value = model.Handaz;
            parameters[25].Value = model.Savecurve;
            parameters[26].Value = model.Lm;

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
		public bool Update(HR_Test.Model.SelTestResult model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_SelTestResult set ");
			strSql.Append("Fm=@Fm,");
			strSql.Append("Rm=@Rm,");
			strSql.Append("ReH=@ReH,");
			strSql.Append("ReL=@ReL,");
			strSql.Append("Rp=@Rp,");
			strSql.Append("Rt=@Rt,");
			strSql.Append("Rr=@Rr,");
			strSql.Append("E=@E,");
			strSql.Append("m=@m,");
			strSql.Append("mE=@mE,");
			strSql.Append("A=@A,");
			strSql.Append("Aee=@Aee,");
			strSql.Append("Agg=@Agg,");
			strSql.Append("Att=@Att,");
			strSql.Append("Aggtt=@Aggtt,");
			strSql.Append("Awnwn=@Awnwn,");
			strSql.Append("deltaLm=@deltaLm,");
			strSql.Append("Lf=@Lf,");
			strSql.Append("Z=@Z,");
			strSql.Append("Avera=@Avera,");
			strSql.Append("SS=@SS,");
			strSql.Append("Avera1=@Avera1,");
            strSql.Append("CV=@CV,");
            strSql.Append("Handaz=@Handaz,");
            strSql.Append("Savecurve=@Savecurve,");
            strSql.Append("Lm=@Lm");
			strSql.Append(" where methodName=@methodName ");

			OleDbParameter[] parameters = {
					new OleDbParameter("@Fm", OleDbType.Boolean,1),
					new OleDbParameter("@Rm", OleDbType.Boolean,1),
					new OleDbParameter("@ReH", OleDbType.Boolean,1),
					new OleDbParameter("@ReL", OleDbType.Boolean,1),
					new OleDbParameter("@Rp", OleDbType.Boolean,1),
					new OleDbParameter("@Rt", OleDbType.Boolean,1),
					new OleDbParameter("@Rr", OleDbType.Boolean,1),
					new OleDbParameter("@E", OleDbType.Boolean,1),
					new OleDbParameter("@m", OleDbType.Boolean,1),
					new OleDbParameter("@mE", OleDbType.Boolean,1),
					new OleDbParameter("@A", OleDbType.Boolean,1),
					new OleDbParameter("@Aee", OleDbType.Boolean,1),
					new OleDbParameter("@Agg", OleDbType.Boolean,1),
					new OleDbParameter("@Att", OleDbType.Boolean,1),
					new OleDbParameter("@Aggtt", OleDbType.Boolean,1),
					new OleDbParameter("@Awnwn", OleDbType.Boolean,1),
					new OleDbParameter("@deltaLm", OleDbType.Boolean,1),
					new OleDbParameter("@Lf", OleDbType.Boolean,1),
					new OleDbParameter("@Z", OleDbType.Boolean,1),
					new OleDbParameter("@Avera", OleDbType.Boolean,1),
					new OleDbParameter("@SS", OleDbType.Boolean,1),
					new OleDbParameter("@Avera1", OleDbType.Boolean,1),
                    new OleDbParameter("@CV", OleDbType.Boolean,1),
                    new OleDbParameter("@Handaz", OleDbType.Boolean,1),
                    new OleDbParameter("@Savecurve", OleDbType.Boolean,1),
                    new OleDbParameter("@Lm", OleDbType.Boolean,1),
					//new OleDbParameter("@ID", OleDbType.Integer,4),
					new OleDbParameter("@methodName", OleDbType.VarChar,100)};
			parameters[0].Value = model.Fm;
			parameters[1].Value = model.Rm;
			parameters[2].Value = model.ReH;
			parameters[3].Value = model.ReL;
			parameters[4].Value = model.Rp;
			parameters[5].Value = model.Rt;
			parameters[6].Value = model.Rr;
			parameters[7].Value = model.E;
			parameters[8].Value = model.m;
			parameters[9].Value = model.mE;
			parameters[10].Value = model.A;
			parameters[11].Value = model.Aee;
			parameters[12].Value = model.Agg;
			parameters[13].Value = model.Att;
			parameters[14].Value = model.Aggtt;
			parameters[15].Value = model.Awnwn;
			parameters[16].Value = model.deltaLm;
			parameters[17].Value = model.Lf;
			parameters[18].Value = model.Z;
			parameters[19].Value = model.Avera;
			parameters[20].Value = model.SS;
			parameters[21].Value = model.Avera1;
			//parameters[25].Value = model.ID;
            parameters[22].Value = model.CV;
            parameters[23].Value = model.Handaz;
            parameters[24].Value = model.Savecurve;
            parameters[25].Value = model.Lm;
			parameters[26].Value = model.methodName;
           
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
			strSql.Append("delete from tb_SelTestResult ");
			strSql.Append(" where methodName=@methodName ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,100)};
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
		public bool DeleteList(string methodNamelist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_SelTestResult ");
			strSql.Append(" where methodName in ("+methodNamelist + ")  ");
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
		public HR_Test.Model.SelTestResult GetModel(string methodName)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * from tb_SelTestResult ");//ID,methodName,Fm,Rm,ReH,ReL,Rp,Rt,Rr,E,m,mE,A,Aee,Agg,Att,Aggtt,Awnwn,deltaLm,Lf,Z,Avera,SS,Avera1,CV,Handaz,Savecurve,Lm 
			strSql.Append(" where methodName=@methodName ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,100)};
			parameters[0].Value = methodName;

			HR_Test.Model.SelTestResult model=new HR_Test.Model.SelTestResult();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["methodName"]!=null && ds.Tables[0].Rows[0]["methodName"].ToString()!="")
				{
					model.methodName=ds.Tables[0].Rows[0]["methodName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Fm"]!=null && ds.Tables[0].Rows[0]["Fm"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Fm"].ToString()=="1")||(ds.Tables[0].Rows[0]["Fm"].ToString().ToLower()=="true"))
					{
						model.Fm=true;
					}
					else
					{
						model.Fm=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Rm"]!=null && ds.Tables[0].Rows[0]["Rm"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Rm"].ToString()=="1")||(ds.Tables[0].Rows[0]["Rm"].ToString().ToLower()=="true"))
					{
						model.Rm=true;
					}
					else
					{
						model.Rm=false;
					}
				}
				if(ds.Tables[0].Rows[0]["ReH"]!=null && ds.Tables[0].Rows[0]["ReH"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["ReH"].ToString()=="1")||(ds.Tables[0].Rows[0]["ReH"].ToString().ToLower()=="true"))
					{
						model.ReH=true;
					}
					else
					{
						model.ReH=false;
					}
				}
				if(ds.Tables[0].Rows[0]["ReL"]!=null && ds.Tables[0].Rows[0]["ReL"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["ReL"].ToString()=="1")||(ds.Tables[0].Rows[0]["ReL"].ToString().ToLower()=="true"))
					{
						model.ReL=true;
					}
					else
					{
						model.ReL=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Rp"]!=null && ds.Tables[0].Rows[0]["Rp"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Rp"].ToString()=="1")||(ds.Tables[0].Rows[0]["Rp"].ToString().ToLower()=="true"))
					{
						model.Rp=true;
					}
					else
					{
						model.Rp=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Rt"]!=null && ds.Tables[0].Rows[0]["Rt"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Rt"].ToString()=="1")||(ds.Tables[0].Rows[0]["Rt"].ToString().ToLower()=="true"))
					{
						model.Rt=true;
					}
					else
					{
						model.Rt=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Rr"]!=null && ds.Tables[0].Rows[0]["Rr"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Rr"].ToString()=="1")||(ds.Tables[0].Rows[0]["Rr"].ToString().ToLower()=="true"))
					{
						model.Rr=true;
					}
					else
					{
						model.Rr=false;
					}
				}
				
				if(ds.Tables[0].Rows[0]["E"]!=null && ds.Tables[0].Rows[0]["E"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["E"].ToString()=="1")||(ds.Tables[0].Rows[0]["E"].ToString().ToLower()=="true"))
					{
						model.E=true;
					}
					else
					{
						model.E=false;
					}
				}
				if(ds.Tables[0].Rows[0]["m"]!=null && ds.Tables[0].Rows[0]["m"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["m"].ToString()=="1")||(ds.Tables[0].Rows[0]["m"].ToString().ToLower()=="true"))
					{
						model.m=true;
					}
					else
					{
						model.m=false;
					}
				}
				if(ds.Tables[0].Rows[0]["mE"]!=null && ds.Tables[0].Rows[0]["mE"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["mE"].ToString()=="1")||(ds.Tables[0].Rows[0]["mE"].ToString().ToLower()=="true"))
					{
						model.mE=true;
					}
					else
					{
						model.mE=false;
					}
				}
				if(ds.Tables[0].Rows[0]["A"]!=null && ds.Tables[0].Rows[0]["A"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["A"].ToString()=="1")||(ds.Tables[0].Rows[0]["A"].ToString().ToLower()=="true"))
					{
						model.A=true;
					}
					else
					{
						model.A=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Aee"]!=null && ds.Tables[0].Rows[0]["Aee"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Aee"].ToString()=="1")||(ds.Tables[0].Rows[0]["Aee"].ToString().ToLower()=="true"))
					{
						model.Aee=true;
					}
					else
					{
						model.Aee=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Agg"]!=null && ds.Tables[0].Rows[0]["Agg"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Agg"].ToString()=="1")||(ds.Tables[0].Rows[0]["Agg"].ToString().ToLower()=="true"))
					{
						model.Agg=true;
					}
					else
					{
						model.Agg=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Att"]!=null && ds.Tables[0].Rows[0]["Att"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Att"].ToString()=="1")||(ds.Tables[0].Rows[0]["Att"].ToString().ToLower()=="true"))
					{
						model.Att=true;
					}
					else
					{
						model.Att=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Aggtt"]!=null && ds.Tables[0].Rows[0]["Aggtt"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Aggtt"].ToString()=="1")||(ds.Tables[0].Rows[0]["Aggtt"].ToString().ToLower()=="true"))
					{
						model.Aggtt=true;
					}
					else
					{
						model.Aggtt=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Awnwn"]!=null && ds.Tables[0].Rows[0]["Awnwn"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Awnwn"].ToString()=="1")||(ds.Tables[0].Rows[0]["Awnwn"].ToString().ToLower()=="true"))
					{
						model.Awnwn=true;
					}
					else
					{
						model.Awnwn=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Lm"]!=null && ds.Tables[0].Rows[0]["Lm"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Lm"].ToString()=="1")||(ds.Tables[0].Rows[0]["Lm"].ToString().ToLower()=="true"))
					{
						model.Lm=true;
					}
					else
					{
						model.Lm=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Lf"]!=null && ds.Tables[0].Rows[0]["Lf"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Lf"].ToString()=="1")||(ds.Tables[0].Rows[0]["Lf"].ToString().ToLower()=="true"))
					{
						model.Lf=true;
					}
					else
					{
						model.Lf=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Z"]!=null && ds.Tables[0].Rows[0]["Z"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Z"].ToString()=="1")||(ds.Tables[0].Rows[0]["Z"].ToString().ToLower()=="true"))
					{
						model.Z=true;
					}
					else
					{
						model.Z=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Avera"]!=null && ds.Tables[0].Rows[0]["Avera"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Avera"].ToString()=="1")||(ds.Tables[0].Rows[0]["Avera"].ToString().ToLower()=="true"))
					{
						model.Avera=true;
					}
					else
					{
						model.Avera=false;
					}
				}
				if(ds.Tables[0].Rows[0]["SS"]!=null && ds.Tables[0].Rows[0]["SS"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["SS"].ToString()=="1")||(ds.Tables[0].Rows[0]["SS"].ToString().ToLower()=="true"))
					{
						model.SS=true;
					}
					else
					{
						model.SS=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Avera1"]!=null && ds.Tables[0].Rows[0]["Avera1"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["Avera1"].ToString()=="1")||(ds.Tables[0].Rows[0]["Avera1"].ToString().ToLower()=="true"))
					{
						model.Avera1=true;
					}
					else
					{
						model.Avera1=false;
					}
				}

                if (ds.Tables[0].Rows[0]["CV"] != null && ds.Tables[0].Rows[0]["CV"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["CV"].ToString() == "1") || (ds.Tables[0].Rows[0]["CV"].ToString().ToLower() == "true"))
                    {
                        model.CV = true;
                    }
                    else
                    {
                        model.CV = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Handaz"] != null && ds.Tables[0].Rows[0]["Handaz"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Handaz"].ToString() == "1") || (ds.Tables[0].Rows[0]["Handaz"].ToString().ToLower() == "true"))
                    {
                        model.Handaz = true;
                    }
                    else
                    {
                        model.Handaz = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["Savecurve"] != null && ds.Tables[0].Rows[0]["Savecurve"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Savecurve"].ToString() == "1") || (ds.Tables[0].Rows[0]["Savecurve"].ToString().ToLower() == "true"))
                    {
                        model.Savecurve = true;
                    }
                    else
                    {
                        model.Savecurve = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["deltaLm"] != null && ds.Tables[0].Rows[0]["deltaLm"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["deltaLm"].ToString() == "1") || (ds.Tables[0].Rows[0]["deltaLm"].ToString().ToLower() == "true"))
                    {
                        model.deltaLm = true;
                    }
                    else
                    {
                        model.deltaLm = false;
                    }
                }

				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select ID,methodName,Fm,Rm,ReH,ReL,Rp,Rt,Rr,E,m,mE,A,Aee,Agg,Att,Aggtt,Awnwn,Lm,deltaLm,Lf,Z,Avera,SS,Avera1,CV,Handaz,Savecurve ");
			strSql.Append(" FROM tb_SelTestResult ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
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
			parameters[0].Value = "tb_SelTestResult";
			parameters[1].Value = "methodName";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperOleDb.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

