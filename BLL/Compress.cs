using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using HR_Test.Model;
namespace HR_Test.BLL
{
	/// <summary>
	/// Compress
	/// </summary>
	public partial class Compress
	{
		private readonly HR_Test.DAL.Compress dal=new HR_Test.DAL.Compress();
		public Compress()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string testSampleNo)
		{
			return dal.Exists(testSampleNo);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(HR_Test.Model.Compress model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(HR_Test.Model.Compress model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string testSampleNo)
		{
			
			return dal.Delete(testSampleNo);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string testSampleNolist )
		{
			return dal.DeleteList(testSampleNolist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public HR_Test.Model.Compress GetModel(string testSampleNo)
		{
			
			return dal.GetModel(testSampleNo);
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HR_Test.Model.Compress GetModel(long ID)
        {

            return dal.GetModel(ID);
        }


		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public HR_Test.Model.Compress GetModelByCache(string testSampleNo)
		{
			
			string CacheKey = "CompressModel-" + testSampleNo;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(testSampleNo);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (HR_Test.Model.Compress)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

        public DataSet GetResultRow(string strWhere)
        {
            return dal.GetResultRow(strWhere);
        }

        public DataSet GetFinishSumList1(string selColAver, string strWhere)
        {
            return dal.GetFinishSumList1(selColAver, strWhere);
        }

        /// <summary>
        /// 获得数据列表1
        /// </summary>
        public DataSet GetFinishList1(string strSelCol, string strWhere, int ab)
        {
            return dal.GetFinishList1(strSelCol, strWhere, ab);
        }

  /// <summary>
        /// 获得数据列表1
        /// </summary>
        public DataSet GetFinishListReport(string strSelCol, string strWhere, int ab)
        {
            return dal.GetFinishListReport(strSelCol, strWhere, ab);
        }
        public DataSet GetNotOverlapList(string strWhere)
        {
            return dal.GetNotOverlapList(strWhere);
        }

        public DataSet GetMaxFmc(string strWhere)
        {
            return dal.GetMaxF(strWhere);
        }

        public DataSet GetNotOverlapList1(string strWhere)
        {
            return dal.GetNotOverlapList1(strWhere);
        }


        public DataSet GetFinishList(string strWhere,double maxValue)
        {
            return dal.GetListFinish(strWhere,maxValue);
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HR_Test.Model.Compress> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HR_Test.Model.Compress> DataTableToList(DataTable dt)
		{
			List<HR_Test.Model.Compress> modelList = new List<HR_Test.Model.Compress>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				HR_Test.Model.Compress model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new HR_Test.Model.Compress();
					if(dt.Rows[n]["ID"]!=null && dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					if(dt.Rows[n]["testMethodName"]!=null && dt.Rows[n]["testMethodName"].ToString()!="")
					{
					model.testMethodName=dt.Rows[n]["testMethodName"].ToString();
					}
					if(dt.Rows[n]["testNo"]!=null && dt.Rows[n]["testNo"].ToString()!="")
					{
					model.testNo=dt.Rows[n]["testNo"].ToString();
					}
					if(dt.Rows[n]["testSampleNo"]!=null && dt.Rows[n]["testSampleNo"].ToString()!="")
					{
					model.testSampleNo=dt.Rows[n]["testSampleNo"].ToString();
					}
					if(dt.Rows[n]["reportNo"]!=null && dt.Rows[n]["reportNo"].ToString()!="")
					{
					model.reportNo=dt.Rows[n]["reportNo"].ToString();
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
					if(dt.Rows[n]["sign"]!=null && dt.Rows[n]["sign"].ToString()!="")
					{
					model.sign=dt.Rows[n]["sign"].ToString();
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
					if(dt.Rows[n]["deltaL"]!=null && dt.Rows[n]["deltaL"].ToString()!="")
					{
					//model.deltaL=dt.Rows[n]["deltaL"].ToString();
					}
					if(dt.Rows[n]["εpc"]!=null && dt.Rows[n]["εpc"].ToString()!="")
					{
					//model.εpc=dt.Rows[n]["εpc"].ToString();
					}
					if(dt.Rows[n]["εtc"]!=null && dt.Rows[n]["εtc"].ToString()!="")
					{
					//model.εtc=dt.Rows[n]["εtc"].ToString();
					}
					if(dt.Rows[n]["n"]!=null && dt.Rows[n]["n"].ToString()!="")
					{
					//model.n=dt.Rows[n]["n"].ToString();
					}
					if(dt.Rows[n]["F0"]!=null && dt.Rows[n]["F0"].ToString()!="")
					{
					//model.F0=dt.Rows[n]["F0"].ToString();
					}
					if(dt.Rows[n]["Ff"]!=null && dt.Rows[n]["Ff"].ToString()!="")
					{
					//model.Ff=dt.Rows[n]["Ff"].ToString();
					}
					if(dt.Rows[n]["Fpc"]!=null && dt.Rows[n]["Fpc"].ToString()!="")
					{
					//model.Fpc=dt.Rows[n]["Fpc"].ToString();
					}
					if(dt.Rows[n]["Ftc"]!=null && dt.Rows[n]["Ftc"].ToString()!="")
					{
					//model.Ftc=dt.Rows[n]["Ftc"].ToString();
					}
					if(dt.Rows[n]["FeHc"]!=null && dt.Rows[n]["FeHc"].ToString()!="")
					{
					//model.FeHc=dt.Rows[n]["FeHc"].ToString();
					}
					if(dt.Rows[n]["FeLc"]!=null && dt.Rows[n]["FeLc"].ToString()!="")
					{
					//model.FeLc=dt.Rows[n]["FeLc"].ToString();
					}
					if(dt.Rows[n]["Fmc"]!=null && dt.Rows[n]["Fmc"].ToString()!="")
					{
					//model.Fmc=dt.Rows[n]["Fmc"].ToString();
					}
					if(dt.Rows[n]["Rpc"]!=null && dt.Rows[n]["Rpc"].ToString()!="")
					{
					//model.Rpc=dt.Rows[n]["Rpc"].ToString();
					}
					if(dt.Rows[n]["Rtc"]!=null && dt.Rows[n]["Rtc"].ToString()!="")
					{
					//model.Rtc=dt.Rows[n]["Rtc"].ToString();
					}
					if(dt.Rows[n]["ReHc"]!=null && dt.Rows[n]["ReHc"].ToString()!="")
					{
					//model.ReHc=dt.Rows[n]["ReHc"].ToString();
					}
					if(dt.Rows[n]["ReLc"]!=null && dt.Rows[n]["ReLc"].ToString()!="")
					{
					//model.ReLc=dt.Rows[n]["ReLc"].ToString();
					}
					if(dt.Rows[n]["Rmc"]!=null && dt.Rows[n]["Rmc"].ToString()!="")
					{
					//model.Rmc=dt.Rows[n]["Rmc"].ToString();
					}
					if(dt.Rows[n]["Ec"]!=null && dt.Rows[n]["Ec"].ToString()!="")
					{
					//model.Ec=dt.Rows[n]["Ec"].ToString();
					}
					if(dt.Rows[n]["Avera"]!=null && dt.Rows[n]["Avera"].ToString()!="")
					{
					//model.Avera=dt.Rows[n]["Avera"].ToString();
					}
					if(dt.Rows[n]["Avera1"]!=null && dt.Rows[n]["Avera1"].ToString()!="")
					{
					//model.Avera1=dt.Rows[n]["Avera1"].ToString();
					}
					if(dt.Rows[n]["isFinish"]!=null && dt.Rows[n]["isFinish"].ToString()!="")
					{
						if((dt.Rows[n]["isFinish"].ToString()=="1")||(dt.Rows[n]["isFinish"].ToString().ToLower()=="true"))
						{
						model.isFinish=true;
						}
						else
						{
							model.isFinish=false;
						}
					}
					if(dt.Rows[n]["testDate"]!=null && dt.Rows[n]["testDate"].ToString()!="")
					{
						model.testDate=Convert.ToDateTime(dt.Rows[n]["testDate"].ToString());
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

