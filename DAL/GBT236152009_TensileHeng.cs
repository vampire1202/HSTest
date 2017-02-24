using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
	/// <summary>
	/// 数据访问类:GBT236152009_TensileHeng
	/// </summary>
	public partial class GBT236152009_TensileHeng
	{
		public GBT236152009_TensileHeng()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperOleDb.GetMaxID("ID", "tb_GBT236152009_TensileHeng"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tb_GBT236152009_TensileHeng");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			return DbHelperOleDb.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetNotOverlapList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct testNo,testMethodName ");
            strSql.Append(" FROM tb_GBT236152009_TensileHeng ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }

        public DataSet GetFmax(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Max([Fmax]) as [Fmax] ");
            strSql.Append("  FROM tb_GBT236152009_TensileHeng ");// ,FORMAT(testDate,'YYYY-MM-DD') as [试验日期]
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }
        
        //获取完成试验列表，求平均
        public DataSet GetFinishAvg(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();//Avg(Rt) as Rt,Avg(Rr) as Rr, Avg(εp) as Ep,Avg(εt) as Et,Avg(εr) as Er,Avg(E) as E,Avg(m) as m,Avg(mE) as mE,Avg(Aee) as Ae,Avg(Agg) as [Ag],Avg(Att) as [At],Avg(Aggtt) as [Agt],Avg(Awnwn) as [Awn],Avg(Lm) as [△Lm],Avg(Lf) as [△Lf],,Avg(SS) as [S]
            strSql.Append("select Round(Avg([T1]),2) as [结果值] ");
            strSql.Append(" FROM tb_GBT236152009_TensileHeng ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }

        public DataSet GetFinishList(string selCol, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select isEffective as [试验无效], testSampleNo as [试样编号],[L] as [长度L(mm)],[t] as [厚度t(mm)]");

            if (!string.IsNullOrEmpty(selCol))//Fm,Rm,ReH,ReL,E,A,Z,SS as [S],FORMAT(testDate,'YYYY-MM-DD') as [试验日期] 
            {
                strSql.Append("," + selCol);
            }
            strSql.Append("  FROM tb_GBT236152009_TensileHeng ");// ,FORMAT(testDate,'YYYY-MM-DD') as [试验日期]
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by testSampleNo ");
            return DbHelperOleDb.Query(strSql.ToString());
        }

        public DataSet GetFinishListReport(string selCol, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  testSampleNo as [试样编号],[L] as [长度L(mm)],[t] as [厚度t(mm)]");

            if (!string.IsNullOrEmpty(selCol))//Fm,Rm,ReH,ReL,E,A,Z,SS as [S],FORMAT(testDate,'YYYY-MM-DD') as [试验日期] 
            {
                strSql.Append("," + selCol);
            }
            strSql.Append("  FROM tb_GBT236152009_TensileHeng ");// ,FORMAT(testDate,'YYYY-MM-DD') as [试验日期]
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by testSampleNo ");
            return DbHelperOleDb.Query(strSql.ToString());
        }


        public static int Dotvalue(double _value)
        {
            int _dotValue = 0;
            int _ten = (int)Math.Log10(_value);
            if (_value < 1000.0)
                _dotValue = 6 - _ten - 1;
            if (_value > 1000.0)
            {
                _dotValue = 6 - (_ten - 3) - 1;
            }
            if (_dotValue < 2)
                _dotValue = 2;
            return _dotValue;
        }

        public DataSet GetFinishReport(string strWhere,double maxValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select testSampleNo as [试样编号],[L] as [长度L(mm)],[t] as [厚度t(mm)],");
            int dotValue =  Dotvalue(maxValue);
            if (maxValue < 1000.0)
            {
                switch (dotValue)
                {
                    case 2: strSql.Append(" FORMAT([Fmax],'0.00') as [Fmax(N)],");
                        break;
                    case 3: strSql.Append(" FORMAT([Fmax],'0.000') as [Fmax(N)],");
                        break;
                    case 4: strSql.Append(" FORMAT([Fmax],'0.0000') as [Fmax(N)],");
                        break;
                    case 5: strSql.Append(" FORMAT([Fmax],'0.00000') as [Fmax(N)],");
                        break;
                    default: strSql.Append(" FORMAT([Fmax],'0.00') as [Fmax(N)],");
                        break;
                }
            }
            if (maxValue >= 1000.0)
            {
                switch (dotValue)
                {
                    case 2: strSql.Append(" FORMAT([Fmax]/1000.0,'0.00') as [Fmax(kN)],");
                        break;
                    case 3: strSql.Append(" FORMAT([Fmax]/1000.0,'0.000') as [Fmax(kN)],");
                        break;
                    case 4: strSql.Append(" FORMAT([Fmax]/1000.0,'0.0000') as [Fmax(kN)],");
                        break;
                    case 5: strSql.Append(" FORMAT([Fmax]/1000.0,'0.00000') as [Fmax(kN)],");
                        break;
                    default: strSql.Append(" FORMAT([Fmax]/1000.0,'0.00') as [Fmax(kN)],");
                        break;
                }
            }

            strSql.Append(" round([T1],2) as [T1(MPa)]  FROM tb_GBT236152009_TensileHeng ");// ,FORMAT(testDate,'YYYY-MM-DD') as [试验日期]
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by testSampleNo ");
            return DbHelperOleDb.Query(strSql.ToString());
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(HR_Test.Model.GBT236152009_TensileHeng model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_GBT236152009_TensileHeng(");
            strSql.Append("testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,testType,L,t,S0,Fmax,T1,isFinish,testDate,condition,controlmode,isUseExtensometer,isEffective)");
			strSql.Append(" values (");
            strSql.Append("@testMethodName,@testNo,@testSampleNo,@reportNo,@sendCompany,@stuffCardNo,@stuffSpec,@stuffType,@hotStatus,@temperature,@humidity,@testStandard,@testMethod,@mathineType,@testCondition,@sampleCharacter,@getSample,@tester,@assessor,@sign,@testType,@L,@t,@S0,@Fmax,@T1,@isFinish,@testDate,@condition,@controlmode,@isUseExtensometer,@isEffective)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@testMethodName", OleDbType.VarChar,100),
					new OleDbParameter("@testNo", OleDbType.VarChar,100),
					new OleDbParameter("@testSampleNo", OleDbType.VarChar,100),
					new OleDbParameter("@reportNo", OleDbType.VarChar,100),
					new OleDbParameter("@sendCompany", OleDbType.VarChar,100),
					new OleDbParameter("@stuffCardNo", OleDbType.VarChar,100),
					new OleDbParameter("@stuffSpec", OleDbType.VarChar,100),
					new OleDbParameter("@stuffType", OleDbType.VarChar,100),
					new OleDbParameter("@hotStatus", OleDbType.VarChar,100),
					new OleDbParameter("@temperature", OleDbType.Double),
					new OleDbParameter("@humidity", OleDbType.Double),
					new OleDbParameter("@testStandard", OleDbType.VarChar,100),
					new OleDbParameter("@testMethod", OleDbType.VarChar,100),
					new OleDbParameter("@mathineType", OleDbType.VarChar,100),
					new OleDbParameter("@testCondition", OleDbType.VarChar,100),
					new OleDbParameter("@sampleCharacter", OleDbType.VarChar,100),
					new OleDbParameter("@getSample", OleDbType.VarChar,100),
					new OleDbParameter("@tester", OleDbType.VarChar,100),
					new OleDbParameter("@assessor", OleDbType.VarChar,100),
					new OleDbParameter("@sign", OleDbType.VarChar,255),
					new OleDbParameter("@testType", OleDbType.VarChar,255),
					new OleDbParameter("@L", OleDbType.Double),
					new OleDbParameter("@t", OleDbType.Double),
					new OleDbParameter("@S0", OleDbType.Double),
					new OleDbParameter("@Fmax", OleDbType.Double),
					new OleDbParameter("@T1", OleDbType.Double),
					new OleDbParameter("@isFinish", OleDbType.Boolean,1),
					new OleDbParameter("@testDate", OleDbType.Date),
					new OleDbParameter("@condition", OleDbType.VarChar,255),
					new OleDbParameter("@controlmode", OleDbType.VarChar,255),
					new OleDbParameter("@isUseExtensometer", OleDbType.Boolean,1),
                    new OleDbParameter("@isEffective", OleDbType.Boolean,1)
                                          };
			parameters[0].Value = model.testMethodName;
			parameters[1].Value = model.testNo;
			parameters[2].Value = model.testSampleNo;
			parameters[3].Value = model.reportNo;
			parameters[4].Value = model.sendCompany;
			parameters[5].Value = model.stuffCardNo;
			parameters[6].Value = model.stuffSpec;
			parameters[7].Value = model.stuffType;
			parameters[8].Value = model.hotStatus;
			parameters[9].Value = model.temperature;
			parameters[10].Value = model.humidity;
			parameters[11].Value = model.testStandard;
			parameters[12].Value = model.testMethod;
			parameters[13].Value = model.mathineType;
			parameters[14].Value = model.testCondition;
			parameters[15].Value = model.sampleCharacter;
			parameters[16].Value = model.getSample;
			parameters[17].Value = model.tester;
			parameters[18].Value = model.assessor;
			parameters[19].Value = model.sign;
			parameters[20].Value = model.testType;
			parameters[21].Value = model.L;
			parameters[22].Value = model.t;
			parameters[23].Value = model.S0;
			parameters[24].Value = model.Fmax;
			parameters[25].Value = model.T1;
			parameters[26].Value = model.isFinish;
			parameters[27].Value = model.testDate;
			parameters[28].Value = model.condition;
			parameters[29].Value = model.controlmode;
			parameters[30].Value = model.isUseExtensometer;
            parameters[31].Value = model.isEffective;

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
        public bool Update( Model.GBT236152009_TensileHeng model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_GBT236152009_TensileHeng set ");
            strSql.Append("testMethodName=@testMethodName,");
            strSql.Append("testNo=@testNo,");
            strSql.Append("testSampleNo=@testSampleNo,");
            strSql.Append("reportNo=@reportNo,");
            strSql.Append("sendCompany=@sendCompany,");
            strSql.Append("stuffCardNo=@stuffCardNo,");
            strSql.Append("stuffSpec=@stuffSpec,");
            strSql.Append("stuffType=@stuffType,");
            strSql.Append("hotStatus=@hotStatus,");
            strSql.Append("temperature=@temperature,");
            strSql.Append("humidity=@humidity,");
            strSql.Append("testStandard=@testStandard,");
            strSql.Append("testMethod=@testMethod,");
            strSql.Append("mathineType=@mathineType,");
            strSql.Append("testCondition=@testCondition,");
            strSql.Append("sampleCharacter=@sampleCharacter,");
            strSql.Append("getSample=@getSample,");
            strSql.Append("tester=@tester,");
            strSql.Append("assessor=@assessor,");
            strSql.Append("sign=@sign,");
            strSql.Append("testType=@testType,");
            strSql.Append("L=@L,");
            strSql.Append("t=@t,");
            strSql.Append("S0=@S0,");
            strSql.Append("Fmax=@Fmax,");
            strSql.Append("T1=@T1,");
            strSql.Append("isFinish=@isFinish,");
            strSql.Append("testDate=@testDate,");
            strSql.Append("condition=@condition,");
            strSql.Append("controlmode=@controlmode,");
            strSql.Append("isUseExtensometer=@isUseExtensometer,");
            strSql.Append("isEffective=@isEffective");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@testMethodName", OleDbType.VarChar,100),
					new OleDbParameter("@testNo", OleDbType.VarChar,100),
					new OleDbParameter("@testSampleNo", OleDbType.VarChar,100),
					new OleDbParameter("@reportNo", OleDbType.VarChar,100),
					new OleDbParameter("@sendCompany", OleDbType.VarChar,100),
					new OleDbParameter("@stuffCardNo", OleDbType.VarChar,100),
					new OleDbParameter("@stuffSpec", OleDbType.VarChar,100),
					new OleDbParameter("@stuffType", OleDbType.VarChar,100),
					new OleDbParameter("@hotStatus", OleDbType.VarChar,100),
					new OleDbParameter("@temperature", OleDbType.Double),
					new OleDbParameter("@humidity", OleDbType.Double),
					new OleDbParameter("@testStandard", OleDbType.VarChar,100),
					new OleDbParameter("@testMethod", OleDbType.VarChar,100),
					new OleDbParameter("@mathineType", OleDbType.VarChar,100),
					new OleDbParameter("@testCondition", OleDbType.VarChar,100),
					new OleDbParameter("@sampleCharacter", OleDbType.VarChar,100),
					new OleDbParameter("@getSample", OleDbType.VarChar,100),
					new OleDbParameter("@tester", OleDbType.VarChar,100),
					new OleDbParameter("@assessor", OleDbType.VarChar,100),
					new OleDbParameter("@sign", OleDbType.VarChar,255),
					new OleDbParameter("@testType", OleDbType.VarChar,255),
					new OleDbParameter("@L", OleDbType.Double),
					new OleDbParameter("@t", OleDbType.Double),
					new OleDbParameter("@S0", OleDbType.Double),
					new OleDbParameter("@Fmax", OleDbType.Double),
					new OleDbParameter("@T1", OleDbType.Double),
					new OleDbParameter("@isFinish", OleDbType.Boolean,1),
					new OleDbParameter("@testDate", OleDbType.Date),
					new OleDbParameter("@condition", OleDbType.VarChar,255),
					new OleDbParameter("@controlmode", OleDbType.VarChar,255),
					new OleDbParameter("@isUseExtensometer", OleDbType.Boolean,1),
					new OleDbParameter("@isEffective", OleDbType.Boolean,1),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
            parameters[0].Value = model.testMethodName;
            parameters[1].Value = model.testNo;
            parameters[2].Value = model.testSampleNo;
            parameters[3].Value = model.reportNo;
            parameters[4].Value = model.sendCompany;
            parameters[5].Value = model.stuffCardNo;
            parameters[6].Value = model.stuffSpec;
            parameters[7].Value = model.stuffType;
            parameters[8].Value = model.hotStatus;
            parameters[9].Value = model.temperature;
            parameters[10].Value = model.humidity;
            parameters[11].Value = model.testStandard;
            parameters[12].Value = model.testMethod;
            parameters[13].Value = model.mathineType;
            parameters[14].Value = model.testCondition;
            parameters[15].Value = model.sampleCharacter;
            parameters[16].Value = model.getSample;
            parameters[17].Value = model.tester;
            parameters[18].Value = model.assessor;
            parameters[19].Value = model.sign;
            parameters[20].Value = model.testType;
            parameters[21].Value = model.L;
            parameters[22].Value = model.t;
            parameters[23].Value = model.S0;
            parameters[24].Value = model.Fmax;
            parameters[25].Value = model.T1;
            parameters[26].Value = model.isFinish;
            parameters[27].Value = model.testDate;
            parameters[28].Value = model.condition;
            parameters[29].Value = model.controlmode;
            parameters[30].Value = model.isUseExtensometer;
            parameters[31].Value = model.isEffective;
            parameters[32].Value = model.ID;

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
			strSql.Append("delete from tb_GBT236152009_TensileHeng ");
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
        public bool Delete(string testNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_GBT236152009_TensileHeng ");
            strSql.Append(" where testNo=@testNo and isFinish=false ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@testNo", OleDbType.VarChar)
			};
            parameters[0].Value = testNo;

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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_GBT236152009_TensileHeng ");
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
		public HR_Test.Model.GBT236152009_TensileHeng GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select ID,testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,testType,L,t,S0,Fmax,T1,isFinish,testDate,condition,controlmode,isUseExtensometer,isEffective from tb_GBT236152009_TensileHeng ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			HR_Test.Model.GBT236152009_TensileHeng model=new HR_Test.Model.GBT236152009_TensileHeng();
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
        public HR_Test.Model.GBT236152009_TensileHeng GetModel(string testSampleNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,testType,L,t,S0,Fmax,T1,isFinish,testDate,condition,controlmode,isUseExtensometer,isEffective from tb_GBT236152009_TensileHeng ");
            strSql.Append(" where testSampleNo=@testSampleNo");
            OleDbParameter[] parameters = {
					new OleDbParameter("@testSampleNo", OleDbType.VarChar)
			};
            parameters[0].Value = testSampleNo;

            HR_Test.Model.GBT236152009_TensileHeng model = new HR_Test.Model.GBT236152009_TensileHeng();
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
		public HR_Test.Model.GBT236152009_TensileHeng DataRowToModel(DataRow row)
		{
			HR_Test.Model.GBT236152009_TensileHeng model=new HR_Test.Model.GBT236152009_TensileHeng();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["testMethodName"]!=null)
				{
					model.testMethodName=row["testMethodName"].ToString();
				}
				if(row["testNo"]!=null)
				{
					model.testNo=row["testNo"].ToString();
				}
				if(row["testSampleNo"]!=null)
				{
					model.testSampleNo=row["testSampleNo"].ToString();
				}
				if(row["reportNo"]!=null)
				{
					model.reportNo=row["reportNo"].ToString();
				}
				if(row["sendCompany"]!=null)
				{
					model.sendCompany=row["sendCompany"].ToString();
				}
				if(row["stuffCardNo"]!=null)
				{
					model.stuffCardNo=row["stuffCardNo"].ToString();
				}
				if(row["stuffSpec"]!=null)
				{
					model.stuffSpec=row["stuffSpec"].ToString();
				}
				if(row["stuffType"]!=null)
				{
					model.stuffType=row["stuffType"].ToString();
				}
				if(row["hotStatus"]!=null)
				{
					model.hotStatus=row["hotStatus"].ToString();
				}
                model.temperature =Convert.ToDouble(  row["temperature"].ToString());
                model.humidity =Convert.ToDouble(  row["humidity"].ToString());
				if(row["testStandard"]!=null)
				{
					model.testStandard=row["testStandard"].ToString();
				}
				if(row["testMethod"]!=null)
				{
					model.testMethod=row["testMethod"].ToString();
				}
				if(row["mathineType"]!=null)
				{
					model.mathineType=row["mathineType"].ToString();
				}
				if(row["testCondition"]!=null)
				{
					model.testCondition=row["testCondition"].ToString();
				}
				if(row["sampleCharacter"]!=null)
				{
					model.sampleCharacter=row["sampleCharacter"].ToString();
				}
				if(row["getSample"]!=null)
				{
					model.getSample=row["getSample"].ToString();
				}
				if(row["tester"]!=null)
				{
					model.tester=row["tester"].ToString();
				}
				if(row["assessor"]!=null)
				{
					model.assessor=row["assessor"].ToString();
				}
				if(row["sign"]!=null)
				{
					model.sign=row["sign"].ToString();
				}
				if(row["testType"]!=null)
				{
					model.testType=row["testType"].ToString();
				}
                model.L = Convert.ToDouble( row["L"].ToString());
                model.t = Convert.ToDouble( row["t"].ToString());
                model.S0 =Convert.ToDouble(  row["S0"].ToString());
                model.Fmax =Convert.ToDouble(  row["Fmax"].ToString());
                model.T1 =Convert.ToDouble(  row["T1"].ToString());
				if(row["isFinish"]!=null && row["isFinish"].ToString()!="")
				{
					if((row["isFinish"].ToString()=="1")||(row["isFinish"].ToString().ToLower()=="true"))
					{
						model.isFinish=true;
					}
					else
					{
						model.isFinish=false;
					}
				}
				if(row["testDate"]!=null && row["testDate"].ToString()!="")
				{
					model.testDate=DateTime.Parse(row["testDate"].ToString());
				}
				if(row["condition"]!=null)
				{
					model.condition=row["condition"].ToString();
				}
				if(row["controlmode"]!=null)
				{
					model.controlmode=row["controlmode"].ToString();
				}
				if(row["isUseExtensometer"]!=null && row["isUseExtensometer"].ToString()!="")
				{
					if((row["isUseExtensometer"].ToString()=="1")||(row["isUseExtensometer"].ToString().ToLower()=="true"))
					{
						model.isUseExtensometer=true;
					}
					else
					{
						model.isUseExtensometer=false;
					}
				}
                if (row["isEffective"] != null && row["isEffective"].ToString() != "")
				{
                    if ((row["isEffective"].ToString() == "1") || (row["isEffective"].ToString().ToLower() == "true"))
					{
                        model.isEffective = true;
					}
					else
					{
                        model.isEffective = false;
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
            strSql.Append("select ID,testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,testType,L,t,S0,Fmax,T1,isFinish,testDate,condition,controlmode,isUseExtensometer,isEffective ");
			strSql.Append(" FROM tb_GBT236152009_TensileHeng ");
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
			strSql.Append("select count(1) FROM tb_GBT236152009_TensileHeng ");
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
			strSql.Append(")AS Row, T.*  from tb_GBT236152009_TensileHeng T ");
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
			parameters[0].Value = "tb_GBT236152009_TensileHeng";
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

