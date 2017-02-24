using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using HR_Test.Model;
namespace HR_Test.BLL
{
	/// <summary>
	/// SelTestResult
	/// </summary>
	public partial class SelTestResult
	{
		private readonly HR_Test.DAL.SelTestResult dal=new HR_Test.DAL.SelTestResult();
		public SelTestResult()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string methodName)
		{
			return dal.Exists(methodName);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(HR_Test.Model.SelTestResult model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(HR_Test.Model.SelTestResult model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string methodName)
		{
			
			return dal.Delete(methodName);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string methodNamelist )
		{
			return dal.DeleteList(methodNamelist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public HR_Test.Model.SelTestResult GetModel(string methodName)
		{
			
			return dal.GetModel(methodName);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public HR_Test.Model.SelTestResult GetModelByCache(string methodName)
		{
			
			string CacheKey = "SelTestResultModel-" + methodName;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(methodName);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (HR_Test.Model.SelTestResult)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HR_Test.Model.SelTestResult> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HR_Test.Model.SelTestResult> DataTableToList(DataTable dt)
		{
			List<HR_Test.Model.SelTestResult> modelList = new List<HR_Test.Model.SelTestResult>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				HR_Test.Model.SelTestResult model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new HR_Test.Model.SelTestResult();
					if(dt.Rows[n]["ID"]!=null && dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					if(dt.Rows[n]["methodName"]!=null && dt.Rows[n]["methodName"].ToString()!="")
					{
					model.methodName=dt.Rows[n]["methodName"].ToString();
					}
					if(dt.Rows[n]["Fm"]!=null && dt.Rows[n]["Fm"].ToString()!="")
					{
						if((dt.Rows[n]["Fm"].ToString()=="1")||(dt.Rows[n]["Fm"].ToString().ToLower()=="true"))
						{
						model.Fm=true;
						}
						else
						{
							model.Fm=false;
						}
					}
					if(dt.Rows[n]["Rm"]!=null && dt.Rows[n]["Rm"].ToString()!="")
					{
						if((dt.Rows[n]["Rm"].ToString()=="1")||(dt.Rows[n]["Rm"].ToString().ToLower()=="true"))
						{
						model.Rm=true;
						}
						else
						{
							model.Rm=false;
						}
					}
					if(dt.Rows[n]["ReH"]!=null && dt.Rows[n]["ReH"].ToString()!="")
					{
						if((dt.Rows[n]["ReH"].ToString()=="1")||(dt.Rows[n]["ReH"].ToString().ToLower()=="true"))
						{
						model.ReH=true;
						}
						else
						{
							model.ReH=false;
						}
					}
					if(dt.Rows[n]["ReL"]!=null && dt.Rows[n]["ReL"].ToString()!="")
					{
						if((dt.Rows[n]["ReL"].ToString()=="1")||(dt.Rows[n]["ReL"].ToString().ToLower()=="true"))
						{
						model.ReL=true;
						}
						else
						{
							model.ReL=false;
						}
					}
					if(dt.Rows[n]["Rp"]!=null && dt.Rows[n]["Rp"].ToString()!="")
					{
						if((dt.Rows[n]["Rp"].ToString()=="1")||(dt.Rows[n]["Rp"].ToString().ToLower()=="true"))
						{
						model.Rp=true;
						}
						else
						{
							model.Rp=false;
						}
					}
					if(dt.Rows[n]["Rt"]!=null && dt.Rows[n]["Rt"].ToString()!="")
					{
						if((dt.Rows[n]["Rt"].ToString()=="1")||(dt.Rows[n]["Rt"].ToString().ToLower()=="true"))
						{
						model.Rt=true;
						}
						else
						{
							model.Rt=false;
						}
					}
					if(dt.Rows[n]["Rr"]!=null && dt.Rows[n]["Rr"].ToString()!="")
					{
						if((dt.Rows[n]["Rr"].ToString()=="1")||(dt.Rows[n]["Rr"].ToString().ToLower()=="true"))
						{
						model.Rr=true;
						}
						else
						{
							model.Rr=false;
						}
					}					
					if(dt.Rows[n]["E"]!=null && dt.Rows[n]["E"].ToString()!="")
					{
						if((dt.Rows[n]["E"].ToString()=="1")||(dt.Rows[n]["E"].ToString().ToLower()=="true"))
						{
						model.E=true;
						}
						else
						{
							model.E=false;
						}
					}
					if(dt.Rows[n]["m"]!=null && dt.Rows[n]["m"].ToString()!="")
					{
						if((dt.Rows[n]["m"].ToString()=="1")||(dt.Rows[n]["m"].ToString().ToLower()=="true"))
						{
						model.m=true;
						}
						else
						{
							model.m=false;
						}
					}
					if(dt.Rows[n]["mE"]!=null && dt.Rows[n]["mE"].ToString()!="")
					{
						if((dt.Rows[n]["mE"].ToString()=="1")||(dt.Rows[n]["mE"].ToString().ToLower()=="true"))
						{
						model.mE=true;
						}
						else
						{
							model.mE=false;
						}
					}
					if(dt.Rows[n]["A"]!=null && dt.Rows[n]["A"].ToString()!="")
					{
						if((dt.Rows[n]["A"].ToString()=="1")||(dt.Rows[n]["A"].ToString().ToLower()=="true"))
						{
						model.A=true;
						}
						else
						{
							model.A=false;
						}
					}
					if(dt.Rows[n]["Aee"]!=null && dt.Rows[n]["Aee"].ToString()!="")
					{
						if((dt.Rows[n]["Aee"].ToString()=="1")||(dt.Rows[n]["Aee"].ToString().ToLower()=="true"))
						{
						model.Aee=true;
						}
						else
						{
							model.Aee=false;
						}
					}
					if(dt.Rows[n]["Agg"]!=null && dt.Rows[n]["Agg"].ToString()!="")
					{
						if((dt.Rows[n]["Agg"].ToString()=="1")||(dt.Rows[n]["Agg"].ToString().ToLower()=="true"))
						{
						model.Agg=true;
						}
						else
						{
							model.Agg=false;
						}
					}
					if(dt.Rows[n]["Att"]!=null && dt.Rows[n]["Att"].ToString()!="")
					{
						if((dt.Rows[n]["Att"].ToString()=="1")||(dt.Rows[n]["Att"].ToString().ToLower()=="true"))
						{
						model.Att=true;
						}
						else
						{
							model.Att=false;
						}
					}
					if(dt.Rows[n]["Aggtt"]!=null && dt.Rows[n]["Aggtt"].ToString()!="")
					{
						if((dt.Rows[n]["Aggtt"].ToString()=="1")||(dt.Rows[n]["Aggtt"].ToString().ToLower()=="true"))
						{
						model.Aggtt=true;
						}
						else
						{
							model.Aggtt=false;
						}
					}
					if(dt.Rows[n]["Awnwn"]!=null && dt.Rows[n]["Awnwn"].ToString()!="")
					{
						if((dt.Rows[n]["Awnwn"].ToString()=="1")||(dt.Rows[n]["Awnwn"].ToString().ToLower()=="true"))
						{
						model.Awnwn=true;
						}
						else
						{
							model.Awnwn=false;
						}
					}
					if(dt.Rows[n]["Lm"]!=null && dt.Rows[n]["Lm"].ToString()!="")
					{
						if((dt.Rows[n]["Lm"].ToString()=="1")||(dt.Rows[n]["Lm"].ToString().ToLower()=="true"))
						{
						model.Lm=true;
						}
						else
						{
							model.Lm=false;
						}
					}

                    if (dt.Rows[n]["deltaLm"] != null && dt.Rows[n]["deltaLm"].ToString() != "")
                    {
                        if ((dt.Rows[n]["deltaLm"].ToString() == "1") || (dt.Rows[n]["deltaLm"].ToString().ToLower() == "true"))
                        {
                            model.deltaLm = true;
                        }
                        else
                        {
                            model.deltaLm = false;
                        }
                    }

					if(dt.Rows[n]["Lf"]!=null && dt.Rows[n]["Lf"].ToString()!="")
					{
						if((dt.Rows[n]["Lf"].ToString()=="1")||(dt.Rows[n]["Lf"].ToString().ToLower()=="true"))
						{
						model.Lf=true;
						}
						else
						{
							model.Lf=false;
						}
					}
					if(dt.Rows[n]["Z"]!=null && dt.Rows[n]["Z"].ToString()!="")
					{
						if((dt.Rows[n]["Z"].ToString()=="1")||(dt.Rows[n]["Z"].ToString().ToLower()=="true"))
						{
						model.Z=true;
						}
						else
						{
							model.Z=false;
						}
					}
					if(dt.Rows[n]["Avera"]!=null && dt.Rows[n]["Avera"].ToString()!="")
					{
						if((dt.Rows[n]["Avera"].ToString()=="1")||(dt.Rows[n]["Avera"].ToString().ToLower()=="true"))
						{
						model.Avera=true;
						}
						else
						{
							model.Avera=false;
						}
					}
					if(dt.Rows[n]["SS"]!=null && dt.Rows[n]["SS"].ToString()!="")
					{
						if((dt.Rows[n]["SS"].ToString()=="1")||(dt.Rows[n]["SS"].ToString().ToLower()=="true"))
						{
						model.SS=true;
						}
						else
						{
							model.SS=false;
						}
					}
					if(dt.Rows[n]["Avera1"]!=null && dt.Rows[n]["Avera1"].ToString()!="")
					{
						if((dt.Rows[n]["Avera1"].ToString()=="1")||(dt.Rows[n]["Avera1"].ToString().ToLower()=="true"))
						{
						model.Avera1=true;
						}
						else
						{
							model.Avera1=false;
						}
					}
                    if (dt.Rows[n]["CV"] != null && dt.Rows[n]["CV"].ToString() != "")
                    {
                        if ((dt.Rows[n]["CV"].ToString() == "1") || (dt.Rows[n]["CV"].ToString().ToLower() == "true"))
                        {
                            model.CV = true;
                        }
                        else
                        {
                            model.CV = false;
                        }
                    }
                    if (dt.Rows[n]["Handaz"] != null && dt.Rows[n]["Handaz"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Handaz"].ToString() == "1") || (dt.Rows[n]["Handaz"].ToString().ToLower() == "true"))
                        {
                            model.Handaz = true;
                        }
                        else
                        {
                            model.Handaz = false;
                        }
                    }
                    if (dt.Rows[n]["Savecurve"] != null && dt.Rows[n]["Savecurve"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Savecurve"].ToString() == "1") || (dt.Rows[n]["Savecurve"].ToString().ToLower() == "true"))
                        {
                            model.Savecurve = true;
                        }
                        else
                        {
                            model.Savecurve = false;
                        }
                    }
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method
	}
}

