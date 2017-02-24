using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using HR_Test.Model;
namespace HR_Test.BLL
{
	/// <summary>
	/// GBT236152009_TensileHeng
	/// </summary>
	public partial class GBT236152009_TensileHeng
	{
		private readonly HR_Test.DAL.GBT236152009_TensileHeng dal=new HR_Test.DAL.GBT236152009_TensileHeng();
		public GBT236152009_TensileHeng()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(HR_Test.Model.GBT236152009_TensileHeng model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(HR_Test.Model.GBT236152009_TensileHeng model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string testNo)
        {

            return dal.Delete(testNo);
        }


        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public bool DeleteList(string IDlist )
        //{
        //    return dal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(IDlist,0) );
        //}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public HR_Test.Model.GBT236152009_TensileHeng GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HR_Test.Model.GBT236152009_TensileHeng GetModel(string testSampleNo)
        {

            return dal.GetModel(testSampleNo);
        }



		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public HR_Test.Model.GBT236152009_TensileHeng GetModelByCache(int ID)
		{
			
			string CacheKey = "GBT236152009_TensileHengModel-" + ID;
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
			return (HR_Test.Model.GBT236152009_TensileHeng)objModel;
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
		public DataSet GetFmax(string strWhere)
		{
			return dal.GetFmax(strWhere);
		}
        
        /// <summary>
		/// 获得数据列表
		/// </summary>
        public DataSet GetNotOverlapList(string strWhere)
		{
			return dal.GetNotOverlapList(strWhere);
		}

/// <summary>
		/// 获得数据列表
		/// </summary>
        public DataSet GetFinishAvg(string strWhere)
		{
			return dal.GetFinishAvg(strWhere);
		}

/// <summary>
		/// 获得数据列表
		/// </summary>
        public DataSet GetFinishReport(string strWhere,double maxValue)
		{
			return dal.GetFinishReport(strWhere,maxValue);
		}

/// <summary>
		/// 获得数据列表
		/// </summary>
        public DataSet GetFinishList(string selCol,string strWhere)
		{
			return dal.GetFinishList(selCol,strWhere);
		}
        /// <summary>
		/// 获得数据列表
		/// </summary>
        public DataSet GetFinishListReport(string selCol,string strWhere)
		{
			return dal.GetFinishListReport(selCol,strWhere);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HR_Test.Model.GBT236152009_TensileHeng> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<HR_Test.Model.GBT236152009_TensileHeng> DataTableToList(DataTable dt)
		{
			List<HR_Test.Model.GBT236152009_TensileHeng> modelList = new List<HR_Test.Model.GBT236152009_TensileHeng>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				HR_Test.Model.GBT236152009_TensileHeng model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
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
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

