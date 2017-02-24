using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using HR_Test.Model;
using Maticsoft.DBUtility;//Please add references
using System.Text;
namespace HR_Test.BLL
{
    /// <summary>
    /// Bend
    /// </summary>
    public partial class Bend
    {
        private readonly HR_Test.DAL.Bend dal = new HR_Test.DAL.Bend();
        public Bend()
        { }
        #region  BasicMethod
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
        public bool Add(HR_Test.Model.Bend model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(HR_Test.Model.Bend model)
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
        public bool Delete(string testsampleNo)
        {
            return dal.Delete(testsampleNo);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HR_Test.Model.Bend GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HR_Test.Model.Bend GetModel(string testSampleNo)
        {

            return dal.GetModel(testSampleNo);
        }



        public DataSet GetNotOverlapList(string strWhere)
        {
            return dal.GetNotOverlapList(strWhere);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public HR_Test.Model.Bend GetModelByCache(int ID)
        {

            string CacheKey = "BendModel-" + ID;
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
                catch { }
            }
            return (HR_Test.Model.Bend)objModel;
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
        public DataSet GetFinishListDefault(string strWhere,double maxValue)
        {
            return dal.GetFinishListDefault(strWhere,maxValue);
        }

        public DataSet GetFbbMax(string strWhere)
        {
            return dal.GetFbbMax(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetFinishSumList1(string selColAver, string strWhere)
        {
            return dal.GetFinishSumList1(selColAver, strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetFinishList1(string selcol,string strWhere)
        {
            return dal.GetFinishList1(selcol,strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetFinishListReport(string selcol, string strWhere)
        {
            return dal.GetFinishListReport(selcol, strWhere);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<HR_Test.Model.Bend> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<HR_Test.Model.Bend> DataTableToList(DataTable dt)
        {
            List<HR_Test.Model.Bend> modelList = new List<HR_Test.Model.Bend>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                HR_Test.Model.Bend model;
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

        public DataSet GetMaxFbb(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select MAX([Fbb]) as [Fbb] ");//
            strSql.Append(" FROM tb_Bend ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
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

