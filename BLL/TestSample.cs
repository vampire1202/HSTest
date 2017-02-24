using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using HR_Test.Model;
namespace HR_Test.BLL
{
	/// <summary>
	/// TestSample
	/// </summary>
	public partial class TestSample
	{
		private readonly HR_Test.DAL.TestSample dal=new HR_Test.DAL.TestSample();
		public TestSample()
		{}
		#region  Method

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(HR_Test.Model.TestSample model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(HR_Test.Model.TestSample model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.Delete();
		}

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string testSampleNo)
        {
            //该表无主键信息，请自定义主键/条件字段
            return dal.Delete(testSampleNo);
        }


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public HR_Test.Model.TestSample GetModel(long ID)
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel(ID);
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HR_Test.Model.TestSample GetModel(string testSampleNo)
        {
            //该表无主键信息，请自定义主键/条件字段
            return dal.GetModel(testSampleNo);
        }


		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public HR_Test.Model.TestSample GetModelByCache(long ID)
		{
			//该表无主键信息，请自定义主键/条件字段
			string CacheKey = "TestSampleModel-" ;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (HR_Test.Model.TestSample)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}

        public DataSet GetMaxFm(string strWhere)
        {
            return dal.GetMaxFm(strWhere);
        }

        //获得不重复项

        public DataSet GetNotOverlapList(string strWhere)
        {
            return dal.GetNotOverlapList(strWhere);
        }

        public DataSet GetNotOverlapList1(string strWhere)
        {
            return dal.GetNotOverlapList1(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetFinishList(string strWhere,double maxValue)
        {
            return dal.GetFinishList(strWhere,maxValue);
        }

        /// <summary>
        /// 获得数据列表1
        /// </summary>
        public DataSet GetFinishList1(string strSelCol,string strWhere,int ab)
        {
            return dal.GetFinishList1(strSelCol,strWhere,ab);
        }

        /// <summary>
        /// 获得数据列表1
        /// </summary>
        public DataSet GetFinishListReport(string strSelCol, string strWhere, int ab)
        {
            return dal.GetFinishListReport(strSelCol, strWhere, ab);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetFinishSumList(string strWhere)
        {
            return dal.GetFinishSumList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetFinishSumList1(string selColAver,string strWhere)
        {
            return dal.GetFinishSumList1(selColAver,strWhere);
        }



		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HR_Test.Model.TestSample> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HR_Test.Model.TestSample> DataTableToList(DataTable dt)
		{
			List<HR_Test.Model.TestSample> modelList = new List<HR_Test.Model.TestSample>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				HR_Test.Model.TestSample model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new HR_Test.Model.TestSample();
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
					if(dt.Rows[n]["a0"]!=null && dt.Rows[n]["a0"].ToString()!="")
					{
					//model.a0=dt.Rows[n]["a0"].ToString();
					}
					if(dt.Rows[n]["au"]!=null && dt.Rows[n]["au"].ToString()!="")
					{
					//model.au=dt.Rows[n]["au"].ToString();
					}
					if(dt.Rows[n]["b0"]!=null && dt.Rows[n]["b0"].ToString()!="")
					{
					//model.b0=dt.Rows[n]["b0"].ToString();
					}
					if(dt.Rows[n]["bu"]!=null && dt.Rows[n]["bu"].ToString()!="")
					{
					//model.bu=dt.Rows[n]["bu"].ToString();
					}
					if(dt.Rows[n]["d0"]!=null && dt.Rows[n]["d0"].ToString()!="")
					{
					//model.d0=dt.Rows[n]["d0"].ToString();
					}
					if(dt.Rows[n]["du"]!=null && dt.Rows[n]["du"].ToString()!="")
					{
					//model.du=dt.Rows[n]["du"].ToString();
					}
					if(dt.Rows[n]["Do"]!=null && dt.Rows[n]["Do"].ToString()!="")
					{
					//model.Do=dt.Rows[n]["Do"].ToString();
					}
					if(dt.Rows[n]["L0"]!=null && dt.Rows[n]["L0"].ToString()!="")
					{
					//model.L0=dt.Rows[n]["L0"].ToString();
					}
					if(dt.Rows[n]["L01"]!=null && dt.Rows[n]["L01"].ToString()!="")
					{
					//model.L01=dt.Rows[n]["L01"].ToString();
					}
					if(dt.Rows[n]["Lc"]!=null && dt.Rows[n]["Lc"].ToString()!="")
					{
					//model.Lc=dt.Rows[n]["Lc"].ToString();
					}
					if(dt.Rows[n]["Le"]!=null && dt.Rows[n]["Le"].ToString()!="")
					{
					//model.Le=dt.Rows[n]["Le"].ToString();
					}
					if(dt.Rows[n]["Lt"]!=null && dt.Rows[n]["Lt"].ToString()!="")
					{
					//model.Lt=dt.Rows[n]["Lt"].ToString();
					}
					if(dt.Rows[n]["Lu"]!=null && dt.Rows[n]["Lu"].ToString()!="")
					{
					//model.Lu=dt.Rows[n]["Lu"].ToString();
					}
					if(dt.Rows[n]["Lu1"]!=null && dt.Rows[n]["Lu1"].ToString()!="")
					{
					//model.Lu1=dt.Rows[n]["Lu1"].ToString();
					}
					if(dt.Rows[n]["S0"]!=null && dt.Rows[n]["S0"].ToString()!="")
					{
					//model.S0=dt.Rows[n]["S0"].ToString();
					}
					if(dt.Rows[n]["Su"]!=null && dt.Rows[n]["Su"].ToString()!="")
					{
					//model.Su=dt.Rows[n]["Su"].ToString();
					}
					if(dt.Rows[n]["k"]!=null && dt.Rows[n]["k"].ToString()!="")
					{
					//model.k=dt.Rows[n]["k"].ToString();
					}
					if(dt.Rows[n]["Fm"]!=null && dt.Rows[n]["Fm"].ToString()!="")
					{
					//model.Fm=dt.Rows[n]["Fm"].ToString();
					}
					if(dt.Rows[n]["Rm"]!=null && dt.Rows[n]["Rm"].ToString()!="")
					{
					//model.Rm=dt.Rows[n]["Rm"].ToString();
					}
					if(dt.Rows[n]["ReH"]!=null && dt.Rows[n]["ReH"].ToString()!="")
					{
					//model.ReH=dt.Rows[n]["ReH"].ToString();
					}
					if(dt.Rows[n]["ReL"]!=null && dt.Rows[n]["ReL"].ToString()!="")
					{
					//model.ReL=dt.Rows[n]["ReL"].ToString();
					}
					if(dt.Rows[n]["Rp"]!=null && dt.Rows[n]["Rp"].ToString()!="")
					{
					//model.Rp=dt.Rows[n]["Rp"].ToString();
					}
					if(dt.Rows[n]["Rt"]!=null && dt.Rows[n]["Rt"].ToString()!="")
					{
					//model.Rt=dt.Rows[n]["Rt"].ToString();
					}
					if(dt.Rows[n]["Rr"]!=null && dt.Rows[n]["Rr"].ToString()!="")
					{
					//model.Rr=dt.Rows[n]["Rr"].ToString();
					}
					if(dt.Rows[n]["εp"]!=null && dt.Rows[n]["εp"].ToString()!="")
					{
					//model.εp=dt.Rows[n]["εp"].ToString();
					}
					if(dt.Rows[n]["εt"]!=null && dt.Rows[n]["εt"].ToString()!="")
					{
					//model.εt=dt.Rows[n]["εt"].ToString();
					}
					if(dt.Rows[n]["εr"]!=null && dt.Rows[n]["εr"].ToString()!="")
					{
					//model.εr=dt.Rows[n]["εr"].ToString();
					}
					if(dt.Rows[n]["E"]!=null && dt.Rows[n]["E"].ToString()!="")
					{
					//model.E=dt.Rows[n]["E"].ToString();
					}
					if(dt.Rows[n]["m"]!=null && dt.Rows[n]["m"].ToString()!="")
					{
					//model.m=dt.Rows[n]["m"].ToString();
					}
					if(dt.Rows[n]["mE"]!=null && dt.Rows[n]["mE"].ToString()!="")
					{
					//model.mE=dt.Rows[n]["mE"].ToString();
					}
					if(dt.Rows[n]["A"]!=null && dt.Rows[n]["A"].ToString()!="")
					{
					//model.A=dt.Rows[n]["A"].ToString();
					}
					if(dt.Rows[n]["Aee"]!=null && dt.Rows[n]["Aee"].ToString()!="")
					{
					//model.Aee=dt.Rows[n]["Aee"].ToString();
					}
					if(dt.Rows[n]["Agg"]!=null && dt.Rows[n]["Agg"].ToString()!="")
					{
					//model.Agg=dt.Rows[n]["Agg"].ToString();
					}
					if(dt.Rows[n]["Att"]!=null && dt.Rows[n]["Att"].ToString()!="")
					{
					//model.Att=dt.Rows[n]["Att"].ToString();
					}
					if(dt.Rows[n]["Aggtt"]!=null && dt.Rows[n]["Aggtt"].ToString()!="")
					{
					//model.Aggtt=dt.Rows[n]["Aggtt"].ToString();
					}
					if(dt.Rows[n]["Awnwn"]!=null && dt.Rows[n]["Awnwn"].ToString()!="")
					{
					//model.Awnwn=dt.Rows[n]["Awnwn"].ToString();
					}
					if(dt.Rows[n]["Lm"]!=null && dt.Rows[n]["Lm"].ToString()!="")
					{
					//model.Lm=dt.Rows[n]["Lm"].ToString();
					}
					if(dt.Rows[n]["Lf"]!=null && dt.Rows[n]["Lf"].ToString()!="")
					{
					//model.Lf=dt.Rows[n]["Lf"].ToString();
					}
					if(dt.Rows[n]["Z"]!=null && dt.Rows[n]["Z"].ToString()!="")
					{
					//model.Z=dt.Rows[n]["Z"].ToString();
					}
					if(dt.Rows[n]["Avera"]!=null && dt.Rows[n]["Avera"].ToString()!="")
					{
					//model.Avera=dt.Rows[n]["Avera"].ToString();
					}
					if(dt.Rows[n]["SS"]!=null && dt.Rows[n]["SS"].ToString()!="")
					{
					//model.SS=dt.Rows[n]["SS"].ToString();
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
						model.testDate= Convert.ToDateTime(dt.Rows[n]["testDate"].ToString());
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

