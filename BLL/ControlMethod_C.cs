using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using HR_Test.Model;
namespace HR_Test.BLL
{
	/// <summary>
	/// ControlMethod_C
	/// </summary>
	public partial class ControlMethod_C
	{
		private readonly HR_Test.DAL.ControlMethod_C dal=new HR_Test.DAL.ControlMethod_C();
		public ControlMethod_C()
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
		public bool Add(HR_Test.Model.ControlMethod_C model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(HR_Test.Model.ControlMethod_C model)
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
		public HR_Test.Model.ControlMethod_C GetModel(string methodName)
		{
			
			return dal.GetModel(methodName);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public HR_Test.Model.ControlMethod_C GetModelByCache(string methodName)
		{
			
			string CacheKey = "ControlMethod_CModel-" + methodName;
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
			return (HR_Test.Model.ControlMethod_C)objModel;
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
		public List<HR_Test.Model.ControlMethod_C> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HR_Test.Model.ControlMethod_C> DataTableToList(DataTable dt)
		{
			List<HR_Test.Model.ControlMethod_C> modelList = new List<HR_Test.Model.ControlMethod_C>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				HR_Test.Model.ControlMethod_C model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new HR_Test.Model.ControlMethod_C();
					if(dt.Rows[n]["ID"]!=null && dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					if(dt.Rows[n]["methodName"]!=null && dt.Rows[n]["methodName"].ToString()!="")
					{
					model.methodName=dt.Rows[n]["methodName"].ToString();
					}
					if(dt.Rows[n]["xmlPath"]!=null && dt.Rows[n]["xmlPath"].ToString()!="")
					{
					model.xmlPath=dt.Rows[n]["xmlPath"].ToString();
					}
					if(dt.Rows[n]["selResultID"]!=null && dt.Rows[n]["selResultID"].ToString()!="")
					{
						model.selResultID=int.Parse(dt.Rows[n]["selResultID"].ToString());
					}
					if(dt.Rows[n]["isProLoad"]!=null && dt.Rows[n]["isProLoad"].ToString()!="")
					{
						if((dt.Rows[n]["isProLoad"].ToString()=="1")||(dt.Rows[n]["isProLoad"].ToString().ToLower()=="true"))
						{
						model.isProLoad=true;
						}
						else
						{
							model.isProLoad=false;
						}
					}
					if(dt.Rows[n]["proLoadType"]!=null && dt.Rows[n]["proLoadType"].ToString()!="")
					{
						model.proLoadType=int.Parse(dt.Rows[n]["proLoadType"].ToString());
					}
					if(dt.Rows[n]["proLoadValue"]!=null && dt.Rows[n]["proLoadValue"].ToString()!="")
					{
					//model.proLoadValue=dt.Rows[n]["proLoadValue"].ToString();
					}
					if(dt.Rows[n]["proLoadControlType"]!=null && dt.Rows[n]["proLoadControlType"].ToString()!="")
					{
						model.proLoadControlType=int.Parse(dt.Rows[n]["proLoadControlType"].ToString());
					}
					if(dt.Rows[n]["proLoadSpeed"]!=null && dt.Rows[n]["proLoadSpeed"].ToString()!="")
					{
					//model.proLoadSpeed=dt.Rows[n]["proLoadSpeed"].ToString();
					}
					if(dt.Rows[n]["isLxqf"]!=null && dt.Rows[n]["isLxqf"].ToString()!="")
					{
                        model.isLxqf = int.Parse(dt.Rows[n]["isLxqf"].ToString()); 
					}
					if(dt.Rows[n]["controlType1"]!=null && dt.Rows[n]["controlType1"].ToString()!="")
					{
					model.controlType1=dt.Rows[n]["controlType1"].ToString();
					}
					if(dt.Rows[n]["controlType2"]!=null && dt.Rows[n]["controlType2"].ToString()!="")
					{
					model.controlType2=dt.Rows[n]["controlType2"].ToString();
					}
					if(dt.Rows[n]["controlType3"]!=null && dt.Rows[n]["controlType3"].ToString()!="")
					{
					model.controlType3=dt.Rows[n]["controlType3"].ToString();
					}
					if(dt.Rows[n]["controlType4"]!=null && dt.Rows[n]["controlType4"].ToString()!="")
					{
					model.controlType4=dt.Rows[n]["controlType4"].ToString();
					}
					if(dt.Rows[n]["controlType5"]!=null && dt.Rows[n]["controlType5"].ToString()!="")
					{
					model.controlType5=dt.Rows[n]["controlType5"].ToString();
					}
					if(dt.Rows[n]["controlType6"]!=null && dt.Rows[n]["controlType6"].ToString()!="")
					{
					model.controlType6=dt.Rows[n]["controlType6"].ToString();
					}
					if(dt.Rows[n]["controlType7"]!=null && dt.Rows[n]["controlType7"].ToString()!="")
					{
					model.controlType7=dt.Rows[n]["controlType7"].ToString();
					}
					if(dt.Rows[n]["controlType8"]!=null && dt.Rows[n]["controlType8"].ToString()!="")
					{
					model.controlType8=dt.Rows[n]["controlType8"].ToString();
					}
					if(dt.Rows[n]["controlType9"]!=null && dt.Rows[n]["controlType9"].ToString()!="")
					{
					model.controlType9=dt.Rows[n]["controlType9"].ToString();
					}
					if(dt.Rows[n]["controlType10"]!=null && dt.Rows[n]["controlType10"].ToString()!="")
					{
					model.controlType10=dt.Rows[n]["controlType10"].ToString();
					}
					if(dt.Rows[n]["controlType11"]!=null && dt.Rows[n]["controlType11"].ToString()!="")
					{
					model.controlType11=dt.Rows[n]["controlType11"].ToString();
					}
					if(dt.Rows[n]["controlType12"]!=null && dt.Rows[n]["controlType12"].ToString()!="")
					{
					model.controlType12=dt.Rows[n]["controlType12"].ToString();
					}
					if(dt.Rows[n]["circleNum"]!=null && dt.Rows[n]["circleNum"].ToString()!="")
					{
						model.circleNum=int.Parse(dt.Rows[n]["circleNum"].ToString());
					}
					if(dt.Rows[n]["stopValue"]!=null && dt.Rows[n]["stopValue"].ToString()!="")
					{
					//model.stopValue=dt.Rows[n]["stopValue"].ToString();
					}
					if(dt.Rows[n]["isTakeDownExten"]!=null && dt.Rows[n]["isTakeDownExten"].ToString()!="")
					{
						if((dt.Rows[n]["isTakeDownExten"].ToString()=="1")||(dt.Rows[n]["isTakeDownExten"].ToString().ToLower()=="true"))
						{
						model.isTakeDownExten=true;
						}
						else
						{
							model.isTakeDownExten=false;
						}
					}
					if(dt.Rows[n]["extenChannel"]!=null && dt.Rows[n]["extenChannel"].ToString()!="")
					{
						model.extenChannel=int.Parse(dt.Rows[n]["extenChannel"].ToString());
					}
					if(dt.Rows[n]["extenValue"]!=null && dt.Rows[n]["extenValue"].ToString()!="")
					{
					//model.extenValue=dt.Rows[n]["extenValue"].ToString();
					}
					if(dt.Rows[n]["sendCompany"]!=null && dt.Rows[n]["sendCompany"].ToString()!="")
					{
					model.sendCompany=dt.Rows[n]["sendCompany"].ToString();
					}
					if(dt.Rows[n]["stuffCardNo"]!=null && dt.Rows[n]["stuffCardNo"].ToString()!="")
					{
					model.stuffCardNo=dt.Rows[n]["stuffCardNo"].ToString();
					}
					if(dt.Rows[n]["stuffSpec"]!=null && dt.Rows[n]["stuffSpec"].ToString()!="")
					{
					model.stuffSpec=dt.Rows[n]["stuffSpec"].ToString();
					}
					if(dt.Rows[n]["stuffType"]!=null && dt.Rows[n]["stuffType"].ToString()!="")
					{
					model.stuffType=dt.Rows[n]["stuffType"].ToString();
					}
					if(dt.Rows[n]["hotStatus"]!=null && dt.Rows[n]["hotStatus"].ToString()!="")
					{
					model.hotStatus=dt.Rows[n]["hotStatus"].ToString();
					}
					if(dt.Rows[n]["temperature"]!=null && dt.Rows[n]["temperature"].ToString()!="")
					{
					//model.temperature=dt.Rows[n]["temperature"].ToString();
					}
					if(dt.Rows[n]["humidity"]!=null && dt.Rows[n]["humidity"].ToString()!="")
					{
					//model.humidity=dt.Rows[n]["humidity"].ToString();
					}
					if(dt.Rows[n]["testStandard"]!=null && dt.Rows[n]["testStandard"].ToString()!="")
					{
					model.testStandard=dt.Rows[n]["testStandard"].ToString();
					}
					if(dt.Rows[n]["testMethod"]!=null && dt.Rows[n]["testMethod"].ToString()!="")
					{
					model.testMethod=dt.Rows[n]["testMethod"].ToString();
					}
					if(dt.Rows[n]["mathineType"]!=null && dt.Rows[n]["mathineType"].ToString()!="")
					{
					model.mathineType=dt.Rows[n]["mathineType"].ToString();
					}
					if(dt.Rows[n]["testCondition"]!=null && dt.Rows[n]["testCondition"].ToString()!="")
					{
					model.testCondition=dt.Rows[n]["testCondition"].ToString();
					}
					if(dt.Rows[n]["sampleCharacter"]!=null && dt.Rows[n]["sampleCharacter"].ToString()!="")
					{
					model.sampleCharacter=dt.Rows[n]["sampleCharacter"].ToString();
					}
					if(dt.Rows[n]["getSample"]!=null && dt.Rows[n]["getSample"].ToString()!="")
					{
					model.getSample=dt.Rows[n]["getSample"].ToString();
					}
					if(dt.Rows[n]["tester"]!=null && dt.Rows[n]["tester"].ToString()!="")
					{
					model.tester=dt.Rows[n]["tester"].ToString();
					}
					if(dt.Rows[n]["assessor"]!=null && dt.Rows[n]["assessor"].ToString()!="")
					{
					model.assessor=dt.Rows[n]["assessor"].ToString();
					}
					if(dt.Rows[n]["a"]!=null && dt.Rows[n]["a"].ToString()!="")
					{
					//model.a=dt.Rows[n]["a"].ToString();
					}
					if(dt.Rows[n]["b"]!=null && dt.Rows[n]["b"].ToString()!="")
					{
					//model.b=dt.Rows[n]["b"].ToString();
					}
					if(dt.Rows[n]["d"]!=null && dt.Rows[n]["d"].ToString()!="")
					{
					//model.d=dt.Rows[n]["d"].ToString();
					}
					if(dt.Rows[n]["L"]!=null && dt.Rows[n]["L"].ToString()!="")
					{
					//model.L=dt.Rows[n]["L"].ToString();
					}
					if(dt.Rows[n]["L0"]!=null && dt.Rows[n]["L0"].ToString()!="")
					{
					//model.L0=dt.Rows[n]["L0"].ToString();
					}
					if(dt.Rows[n]["H"]!=null && dt.Rows[n]["H"].ToString()!="")
					{
					//model.H=dt.Rows[n]["H"].ToString();
					}
					if(dt.Rows[n]["hh"]!=null && dt.Rows[n]["hh"].ToString()!="")
					{
					//model.hh=dt.Rows[n]["hh"].ToString();
					}
					if(dt.Rows[n]["S0"]!=null && dt.Rows[n]["S0"].ToString()!="")
					{
					//model.S0=dt.Rows[n]["S0"].ToString();
					}
					if(dt.Rows[n]["sign"]!=null && dt.Rows[n]["sign"].ToString()!="")
					{
					model.sign=dt.Rows[n]["sign"].ToString();
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

