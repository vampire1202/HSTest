using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
	/// <summary>
	/// 数据访问类:TestSample
	/// </summary>
	public partial class TestSample
	{
		public TestSample()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(HR_Test.Model.TestSample model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_TestSample(");
            strSql.Append("testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,a0,au,b0,bu,d0,du,Do,L0,L01,Lc,Le,Lt,Lu,Lu1,S0,Su,k,Fm,Rm,ReH,ReL,Rp,Rt,Rr,εp,εt,εr,E,m,mE,A,Aee,Agg,Att,Aggtt,Awnwn,Lm,Lf,Z,Avera,SS,Avera1,isFinish,testDate,condition,controlmode,isUseExtensometer,isEffective,deltaLm)");
			strSql.Append(" values (");
            strSql.Append("@testMethodName,@testNo,@testSampleNo,@reportNo,@sendCompany,@stuffCardNo,@stuffSpec,@stuffType,@hotStatus,@temperature,@humidity,@testStandard,@testMethod,@mathineType,@testCondition,@sampleCharacter,@getSample,@tester,@assessor,@sign,@a0,@au,@b0,@bu,@d0,@du,@Do,@L0,@L01,@Lc,@Le,@Lt,@Lu,@Lu1,@S0,@Su,@k,@Fm,@Rm,@ReH,@ReL,@Rp,@Rt,@Rr,@εp,@εt,@εr,@E,@m,@mE,@A,@Aee,@Agg,@Att,@Aggtt,@Awnwn,@Lm,@Lf,@Z,@Avera,@SS,@Avera1,@isFinish,@testDate,@condition,@controlmode,@isUseExtensometer,@isEffective,@deltaLm)");
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
					new OleDbParameter("@a0", OleDbType.Double),
					new OleDbParameter("@au", OleDbType.Double),
					new OleDbParameter("@b0", OleDbType.Double),
					new OleDbParameter("@bu", OleDbType.Double),
					new OleDbParameter("@d0", OleDbType.Double),
					new OleDbParameter("@du", OleDbType.Double),
					new OleDbParameter("@Do", OleDbType.Double),
					new OleDbParameter("@L0", OleDbType.Double),
					new OleDbParameter("@L01", OleDbType.Double),
					new OleDbParameter("@Lc", OleDbType.Double),
					new OleDbParameter("@Le", OleDbType.Double),
					new OleDbParameter("@Lt", OleDbType.Double),
					new OleDbParameter("@Lu", OleDbType.Double),
					new OleDbParameter("@Lu1", OleDbType.Double),
					new OleDbParameter("@S0", OleDbType.Double),
					new OleDbParameter("@Su", OleDbType.Double),
					new OleDbParameter("@k", OleDbType.Double),
					new OleDbParameter("@Fm", OleDbType.Double),
					new OleDbParameter("@Rm", OleDbType.Double),
					new OleDbParameter("@ReH", OleDbType.Double),
					new OleDbParameter("@ReL", OleDbType.Double),
					new OleDbParameter("@Rp", OleDbType.Double),
					new OleDbParameter("@Rt", OleDbType.Double),
					new OleDbParameter("@Rr", OleDbType.Double),
					new OleDbParameter("@εp", OleDbType.Double),
					new OleDbParameter("@εt", OleDbType.Double),
					new OleDbParameter("@εr", OleDbType.Double),
					new OleDbParameter("@E", OleDbType.Double),
					new OleDbParameter("@m", OleDbType.Double),
					new OleDbParameter("@mE", OleDbType.Double),
					new OleDbParameter("@A", OleDbType.Double),
					new OleDbParameter("@Aee", OleDbType.Double),
					new OleDbParameter("@Agg", OleDbType.Double),
					new OleDbParameter("@Att", OleDbType.Double),
					new OleDbParameter("@Aggtt", OleDbType.Double),
					new OleDbParameter("@Awnwn", OleDbType.Double),
					new OleDbParameter("@Lm", OleDbType.Double),
					new OleDbParameter("@Lf", OleDbType.Double),
					new OleDbParameter("@Z", OleDbType.Double),
					new OleDbParameter("@Avera", OleDbType.Double),
					new OleDbParameter("@SS", OleDbType.Double),
					new OleDbParameter("@Avera1", OleDbType.Double),
					new OleDbParameter("@isFinish", OleDbType.Boolean,1),
					new OleDbParameter("@testDate", OleDbType.DBDate),
                    new OleDbParameter("@conditon", OleDbType.VarChar,255),
                    new OleDbParameter("@controlmode", OleDbType.VarChar,255),  
                    new OleDbParameter("@isUseExtensometer", OleDbType.Boolean,1), 
                     new OleDbParameter("@isEffective", OleDbType.Boolean,1),   
                     new OleDbParameter("@deltaLm", OleDbType.Double), 
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
			parameters[20].Value = model.a0;
			parameters[21].Value = model.au;
			parameters[22].Value = model.b0;
			parameters[23].Value = model.bu;
			parameters[24].Value = model.d0;
			parameters[25].Value = model.du;
			parameters[26].Value = model.Do;
			parameters[27].Value = model.L0;
			parameters[28].Value = model.L01;
			parameters[29].Value = model.Lc;
			parameters[30].Value = model.Le;
			parameters[31].Value = model.Lt;
			parameters[32].Value = model.Lu;
			parameters[33].Value = model.Lu1;
			parameters[34].Value = model.S0;
			parameters[35].Value = model.Su;
			parameters[36].Value = model.k;
			parameters[37].Value = model.Fm;
			parameters[38].Value = model.Rm;
			parameters[39].Value = model.ReH;
			parameters[40].Value = model.ReL;
			parameters[41].Value = model.Rp;
			parameters[42].Value = model.Rt;
			parameters[43].Value = model.Rr;
			parameters[44].Value = model.εp;
			parameters[45].Value = model.εt;
			parameters[46].Value = model.εr;
			parameters[47].Value = model.E;
			parameters[48].Value = model.m;
			parameters[49].Value = model.mE;
			parameters[50].Value = model.A;
			parameters[51].Value = model.Aee;
			parameters[52].Value = model.Agg;
			parameters[53].Value = model.Att;
			parameters[54].Value = model.Aggtt;
			parameters[55].Value = model.Awnwn;
			parameters[56].Value = model.Lm;
			parameters[57].Value = model.Lf;
			parameters[58].Value = model.Z;
			parameters[59].Value = model.Avera;
			parameters[60].Value = model.SS;
			parameters[61].Value = model.Avera1;
			parameters[62].Value = model.isFinish;
			parameters[63].Value = model.testDate;
            parameters[64].Value = model.condition;
            parameters[65].Value = model.controlmode;
            parameters[66].Value = model.isUseExtensometer;
            parameters[67].Value = model.isEffective;
            parameters[68].Value = model.deltaLm;
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
        public bool Update(Model.TestSample model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_TestSample set ");
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
            strSql.Append("a0=@a0,");
            strSql.Append("au=@au,");
            strSql.Append("b0=@b0,");
            strSql.Append("bu=@bu,");
            strSql.Append("d0=@d0,");
            strSql.Append("du=@du,");
            strSql.Append("Do=@Do,");
            strSql.Append("L0=@L0,");
            strSql.Append("L01=@L01,");
            strSql.Append("Lc=@Lc,");
            strSql.Append("Le=@Le,");
            strSql.Append("Lt=@Lt,");
            strSql.Append("Lu=@Lu,");
            strSql.Append("Lu1=@Lu1,");
            strSql.Append("S0=@S0,");
            strSql.Append("Su=@Su,");
            strSql.Append("k=@k,");
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
            strSql.Append("Lm=@Lm,");
            strSql.Append("Lf=@Lf,");
            strSql.Append("Z=@Z,");
            strSql.Append("Avera=@Avera,");
            strSql.Append("SS=@SS,");
            strSql.Append("Avera1=@Avera1,");
            strSql.Append("εp=@εp,");
            strSql.Append("εt=@εt,");
            strSql.Append("εr=@εr,");
            strSql.Append("isFinish=@isFinish,");
            strSql.Append("testDate=@testDate,");
            strSql.Append("condition=@condition,");
            strSql.Append("controlmode=@controlmode,");
            strSql.Append("isUseExtensometer=@isUseExtensometer,");
            strSql.Append("isEffective=@isEffective,");
            strSql.Append("deltaLm=@deltaLm");
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
					new OleDbParameter("@a0", OleDbType.Double),
					new OleDbParameter("@au", OleDbType.Double),
					new OleDbParameter("@b0", OleDbType.Double),
					new OleDbParameter("@bu", OleDbType.Double),
					new OleDbParameter("@d0", OleDbType.Double),
					new OleDbParameter("@du", OleDbType.Double),
					new OleDbParameter("@Do", OleDbType.Double),
					new OleDbParameter("@L0", OleDbType.Double),
					new OleDbParameter("@L01", OleDbType.Double),
					new OleDbParameter("@Lc", OleDbType.Double),
					new OleDbParameter("@Le", OleDbType.Double),
					new OleDbParameter("@Lt", OleDbType.Double),
					new OleDbParameter("@Lu", OleDbType.Double),
					new OleDbParameter("@Lu1", OleDbType.Double),
					new OleDbParameter("@S0", OleDbType.Double),
					new OleDbParameter("@Su", OleDbType.Double),
					new OleDbParameter("@k", OleDbType.Double),
					new OleDbParameter("@Fm", OleDbType.Double),
					new OleDbParameter("@Rm", OleDbType.Double),
					new OleDbParameter("@ReH", OleDbType.Double),
					new OleDbParameter("@ReL", OleDbType.Double),
					new OleDbParameter("@Rp", OleDbType.Double),
					new OleDbParameter("@Rt", OleDbType.Double),
					new OleDbParameter("@Rr", OleDbType.Double),
					new OleDbParameter("@E", OleDbType.Double),
					new OleDbParameter("@m", OleDbType.Double),
					new OleDbParameter("@mE", OleDbType.Double),
					new OleDbParameter("@A", OleDbType.Double),
					new OleDbParameter("@Aee", OleDbType.Double),
					new OleDbParameter("@Agg", OleDbType.Double),
					new OleDbParameter("@Att", OleDbType.Double),
					new OleDbParameter("@Aggtt", OleDbType.Double),
					new OleDbParameter("@Awnwn", OleDbType.Double),
					new OleDbParameter("@Lm", OleDbType.Double),
					new OleDbParameter("@Lf", OleDbType.Double),
					new OleDbParameter("@Z", OleDbType.Double),
					new OleDbParameter("@Avera", OleDbType.Double),
					new OleDbParameter("@SS", OleDbType.Double),
					new OleDbParameter("@Avera1", OleDbType.Double),
					new OleDbParameter("@εp", OleDbType.Double),
					new OleDbParameter("@εt", OleDbType.Double),
					new OleDbParameter("@εr", OleDbType.Double),
					new OleDbParameter("@isFinish", OleDbType.Boolean,1),
					new OleDbParameter("@testDate", OleDbType.Date),
					new OleDbParameter("@condition", OleDbType.VarChar,255),
					new OleDbParameter("@controlmode", OleDbType.VarChar,255),
					new OleDbParameter("@isUseExtensometer", OleDbType.Boolean,1),
					new OleDbParameter("@isEffective", OleDbType.Boolean,1),
                    new OleDbParameter("@deltaLm", OleDbType.Double),
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
            parameters[20].Value = model.a0;
            parameters[21].Value = model.au;
            parameters[22].Value = model.b0;
            parameters[23].Value = model.bu;
            parameters[24].Value = model.d0;
            parameters[25].Value = model.du;
            parameters[26].Value = model.Do;
            parameters[27].Value = model.L0;
            parameters[28].Value = model.L01;
            parameters[29].Value = model.Lc;
            parameters[30].Value = model.Le;
            parameters[31].Value = model.Lt;
            parameters[32].Value = model.Lu;
            parameters[33].Value = model.Lu1;
            parameters[34].Value = model.S0;
            parameters[35].Value = model.Su;
            parameters[36].Value = model.k;
            parameters[37].Value = model.Fm;
            parameters[38].Value = model.Rm;
            parameters[39].Value = model.ReH;
            parameters[40].Value = model.ReL;
            parameters[41].Value = model.Rp;
            parameters[42].Value = model.Rt;
            parameters[43].Value = model.Rr;
            parameters[44].Value = model.E;
            parameters[45].Value = model.m;
            parameters[46].Value = model.mE;
            parameters[47].Value = model.A;
            parameters[48].Value = model.Aee;
            parameters[49].Value = model.Agg;
            parameters[50].Value = model.Att;
            parameters[51].Value = model.Aggtt;
            parameters[52].Value = model.Awnwn;
            parameters[53].Value = model.Lm;
            parameters[54].Value = model.Lf;
            parameters[55].Value = model.Z;
            parameters[56].Value = model.Avera;
            parameters[57].Value = model.SS;
            parameters[58].Value = model.Avera1;
            parameters[59].Value = model.εp;
            parameters[60].Value = model.εt;
            parameters[61].Value = model.εr;
            parameters[62].Value = model.isFinish;
            parameters[63].Value = model.testDate;
            parameters[64].Value = model.condition;
            parameters[65].Value = model.controlmode;
            parameters[66].Value = model.isUseExtensometer;
            parameters[67].Value = model.isEffective;
            parameters[68].Value = model.deltaLm;
            parameters[69].Value = model.ID;
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
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_TestSample ");
			strSql.Append(" where ");
			OleDbParameter[] parameters = {
};

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
        public bool Delete(string testSampleNo)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_TestSample ");
            strSql.Append(" where testSampleNo=@testSampleNo ");
            OleDbParameter[] parameters = {new OleDbParameter("@testSampleNo", OleDbType.VarChar,100),
};
            parameters[0].Value = testSampleNo;

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

        public DataSet GetMaxFm(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select MAX([Fm]) as [Fm] ");//
            strSql.Append(" FROM tb_TestSample ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }



		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public HR_Test.Model.TestSample GetModel(long  ID)
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select ID,testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,a0,au,b0,bu,d0,du,Do,L0,L01,Lc,Le,Lt,Lu,Lu1,S0,Su,k,Fm,Rm,ReH,ReL,Rp,Rt,Rr,εp,εt,εr,E,m,mE,A,Aee,Agg,Att,Aggtt,Awnwn,Lm,Lf,Z,Avera,SS,Avera1,isFinish,testDate,condition,controlmode,isUseExtensometer,isEffective,deltaLm from tb_TestSample ");
            strSql.Append(" where ID=@ID ");
            OleDbParameter[] parameters = { new OleDbParameter("@ID", OleDbType.BigInt) };
            parameters[0].Value = ID;

			HR_Test.Model.TestSample model=new HR_Test.Model.TestSample();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["testMethodName"]!=null && ds.Tables[0].Rows[0]["testMethodName"].ToString()!="")
				{
					model.testMethodName=ds.Tables[0].Rows[0]["testMethodName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["testNo"]!=null && ds.Tables[0].Rows[0]["testNo"].ToString()!="")
				{
					model.testNo=ds.Tables[0].Rows[0]["testNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["testSampleNo"]!=null && ds.Tables[0].Rows[0]["testSampleNo"].ToString()!="")
				{
					model.testSampleNo=ds.Tables[0].Rows[0]["testSampleNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["reportNo"]!=null && ds.Tables[0].Rows[0]["reportNo"].ToString()!="")
				{
					model.reportNo=ds.Tables[0].Rows[0]["reportNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["sendCompany"]!=null && ds.Tables[0].Rows[0]["sendCompany"].ToString()!="")
				{
					model.sendCompany=ds.Tables[0].Rows[0]["sendCompany"].ToString();
				}
				if(ds.Tables[0].Rows[0]["stuffCardNo"]!=null && ds.Tables[0].Rows[0]["stuffCardNo"].ToString()!="")
				{
					model.stuffCardNo=ds.Tables[0].Rows[0]["stuffCardNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["stuffSpec"]!=null && ds.Tables[0].Rows[0]["stuffSpec"].ToString()!="")
				{
					model.stuffSpec=ds.Tables[0].Rows[0]["stuffSpec"].ToString();
				}
				if(ds.Tables[0].Rows[0]["stuffType"]!=null && ds.Tables[0].Rows[0]["stuffType"].ToString()!="")
				{
					model.stuffType=ds.Tables[0].Rows[0]["stuffType"].ToString();
				}
				if(ds.Tables[0].Rows[0]["hotStatus"]!=null && ds.Tables[0].Rows[0]["hotStatus"].ToString()!="")
				{
					model.hotStatus=ds.Tables[0].Rows[0]["hotStatus"].ToString();
				}
				if(ds.Tables[0].Rows[0]["temperature"]!=null && ds.Tables[0].Rows[0]["temperature"].ToString()!="")
				{
					model.temperature=double.Parse(ds.Tables[0].Rows[0]["temperature"].ToString());
				}
				if(ds.Tables[0].Rows[0]["humidity"]!=null && ds.Tables[0].Rows[0]["humidity"].ToString()!="")
				{
					model.humidity=double.Parse(ds.Tables[0].Rows[0]["humidity"].ToString());
				}
				if(ds.Tables[0].Rows[0]["testStandard"]!=null && ds.Tables[0].Rows[0]["testStandard"].ToString()!="")
				{
					model.testStandard=ds.Tables[0].Rows[0]["testStandard"].ToString();
				}
				if(ds.Tables[0].Rows[0]["testMethod"]!=null && ds.Tables[0].Rows[0]["testMethod"].ToString()!="")
				{
					model.testMethod=ds.Tables[0].Rows[0]["testMethod"].ToString();
				}
				if(ds.Tables[0].Rows[0]["mathineType"]!=null && ds.Tables[0].Rows[0]["mathineType"].ToString()!="")
				{
					model.mathineType=ds.Tables[0].Rows[0]["mathineType"].ToString();
				}
				if(ds.Tables[0].Rows[0]["testCondition"]!=null && ds.Tables[0].Rows[0]["testCondition"].ToString()!="")
				{
					model.testCondition=ds.Tables[0].Rows[0]["testCondition"].ToString();
				}
				if(ds.Tables[0].Rows[0]["sampleCharacter"]!=null && ds.Tables[0].Rows[0]["sampleCharacter"].ToString()!="")
				{
					model.sampleCharacter=ds.Tables[0].Rows[0]["sampleCharacter"].ToString();
				}
				if(ds.Tables[0].Rows[0]["getSample"]!=null && ds.Tables[0].Rows[0]["getSample"].ToString()!="")
				{
					model.getSample=ds.Tables[0].Rows[0]["getSample"].ToString();
				}
				if(ds.Tables[0].Rows[0]["tester"]!=null && ds.Tables[0].Rows[0]["tester"].ToString()!="")
				{
					model.tester=ds.Tables[0].Rows[0]["tester"].ToString();
				}
				if(ds.Tables[0].Rows[0]["assessor"]!=null && ds.Tables[0].Rows[0]["assessor"].ToString()!="")
				{
					model.assessor=ds.Tables[0].Rows[0]["assessor"].ToString();
				}
				if(ds.Tables[0].Rows[0]["sign"]!=null && ds.Tables[0].Rows[0]["sign"].ToString()!="")
				{
					model.sign=ds.Tables[0].Rows[0]["sign"].ToString();
				}
				if(ds.Tables[0].Rows[0]["a0"]!=null && ds.Tables[0].Rows[0]["a0"].ToString()!="")
				{
                    model.a0 = double.Parse(ds.Tables[0].Rows[0]["a0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["au"] != null && ds.Tables[0].Rows[0]["au"].ToString() != "")
                {
                    model.au = double.Parse(ds.Tables[0].Rows[0]["au"].ToString());
                }
                if (ds.Tables[0].Rows[0]["b0"] != null && ds.Tables[0].Rows[0]["b0"].ToString() != "")
                {
                    model.b0 = double.Parse(ds.Tables[0].Rows[0]["b0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["bu"] != null && ds.Tables[0].Rows[0]["bu"].ToString() != "")
                {
                    model.bu = double.Parse(ds.Tables[0].Rows[0]["bu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["d0"] != null && ds.Tables[0].Rows[0]["d0"].ToString() != "")
                {
                    model.d0 = double.Parse(ds.Tables[0].Rows[0]["d0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["du"] != null && ds.Tables[0].Rows[0]["du"].ToString() != "")
                {
                    model.du = double.Parse(ds.Tables[0].Rows[0]["du"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Do"] != null && ds.Tables[0].Rows[0]["Do"].ToString() != "")
                {
                    model.Do = double.Parse(ds.Tables[0].Rows[0]["Do"].ToString());
                }
                if (ds.Tables[0].Rows[0]["L0"] != null && ds.Tables[0].Rows[0]["L0"].ToString() != "")
                {
                    model.L0 = double.Parse(ds.Tables[0].Rows[0]["L0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["L01"] != null && ds.Tables[0].Rows[0]["L01"].ToString() != "")
                {
                    model.L01 = double.Parse(ds.Tables[0].Rows[0]["L01"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Lc"] != null && ds.Tables[0].Rows[0]["Lc"].ToString() != "")
                {
                    model.Lc = double.Parse(ds.Tables[0].Rows[0]["Lc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Le"] != null && ds.Tables[0].Rows[0]["Le"].ToString() != "")
                {
                    model.Le = double.Parse(ds.Tables[0].Rows[0]["Le"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Lt"] != null && ds.Tables[0].Rows[0]["Lt"].ToString() != "")
                {
                    model.Lt = double.Parse(ds.Tables[0].Rows[0]["Lt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Lu"] != null && ds.Tables[0].Rows[0]["Lu"].ToString() != "")
                {
                    model.Lu = double.Parse(ds.Tables[0].Rows[0]["Lu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Lu1"] != null && ds.Tables[0].Rows[0]["Lu1"].ToString() != "")
                {
                    model.Lu1 =double.Parse( ds.Tables[0].Rows[0]["Lu1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["S0"] != null && ds.Tables[0].Rows[0]["S0"].ToString() != "")
                {
                    model.S0 = double.Parse(ds.Tables[0].Rows[0]["S0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Su"] != null && ds.Tables[0].Rows[0]["Su"].ToString() != "")
                {
                    model.Su = double.Parse(ds.Tables[0].Rows[0]["Su"].ToString());
                }
                if (ds.Tables[0].Rows[0]["k"] != null && ds.Tables[0].Rows[0]["k"].ToString() != "")
                {
                    model.k = double.Parse(ds.Tables[0].Rows[0]["k"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fm"] != null && ds.Tables[0].Rows[0]["Fm"].ToString() != "")
                {
                    model.Fm = double.Parse(ds.Tables[0].Rows[0]["Fm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rm"] != null && ds.Tables[0].Rows[0]["Rm"].ToString() != "")
                {
                    model.Rm = double.Parse(ds.Tables[0].Rows[0]["Rm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReH"] != null && ds.Tables[0].Rows[0]["ReH"].ToString() != "")
                {
                    model.ReH =double.Parse( ds.Tables[0].Rows[0]["ReH"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReL"] != null && ds.Tables[0].Rows[0]["ReL"].ToString() != "")
                {
                    model.ReL = double.Parse(ds.Tables[0].Rows[0]["ReL"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rp"] != null && ds.Tables[0].Rows[0]["Rp"].ToString() != "")
                {
                    model.Rp = double.Parse(ds.Tables[0].Rows[0]["Rp"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rt"] != null && ds.Tables[0].Rows[0]["Rt"].ToString() != "")
                {
                    model.Rt = double.Parse(ds.Tables[0].Rows[0]["Rt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rr"] != null && ds.Tables[0].Rows[0]["Rr"].ToString() != "")
                {
                    model.Rr =double.Parse( ds.Tables[0].Rows[0]["Rr"].ToString());
                }
                if (ds.Tables[0].Rows[0]["εp"] != null && ds.Tables[0].Rows[0]["εp"].ToString() != "")
                {
                    model.εp = double.Parse(ds.Tables[0].Rows[0]["εp"].ToString());
                }
                if (ds.Tables[0].Rows[0]["εt"] != null && ds.Tables[0].Rows[0]["εt"].ToString() != "")
                {
                    model.εt = double.Parse(ds.Tables[0].Rows[0]["εt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["εr"] != null && ds.Tables[0].Rows[0]["εr"].ToString() != "")
                {
                    model.εr = double.Parse(ds.Tables[0].Rows[0]["εr"].ToString());
                }
                if (ds.Tables[0].Rows[0]["E"] != null && ds.Tables[0].Rows[0]["E"].ToString() != "")
                {
                    model.E = double.Parse(ds.Tables[0].Rows[0]["E"].ToString());
                }
                if (ds.Tables[0].Rows[0]["m"] != null && ds.Tables[0].Rows[0]["m"].ToString() != "")
                {
                    model.m = double.Parse(ds.Tables[0].Rows[0]["m"].ToString());
                }
                if (ds.Tables[0].Rows[0]["mE"] != null && ds.Tables[0].Rows[0]["mE"].ToString() != "")
                {
                    model.mE = double.Parse(ds.Tables[0].Rows[0]["mE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["A"] != null && ds.Tables[0].Rows[0]["A"].ToString() != "")
                {
                    model.A = double.Parse(ds.Tables[0].Rows[0]["A"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Aee"] != null && ds.Tables[0].Rows[0]["Aee"].ToString() != "")
                {
                    model.Aee = double.Parse(ds.Tables[0].Rows[0]["Aee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Agg"] != null && ds.Tables[0].Rows[0]["Agg"].ToString() != "")
                {
                    model.Agg =double.Parse(ds.Tables[0].Rows[0]["Agg"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Att"] != null && ds.Tables[0].Rows[0]["Att"].ToString() != "")
                {
                    model.Att =double.Parse( ds.Tables[0].Rows[0]["Att"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Aggtt"] != null && ds.Tables[0].Rows[0]["Aggtt"].ToString() != "")
                {
                    model.Aggtt =double.Parse( ds.Tables[0].Rows[0]["Aggtt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Awnwn"] != null && ds.Tables[0].Rows[0]["Awnwn"].ToString() != "")
                {
                    model.Awnwn =double.Parse( ds.Tables[0].Rows[0]["Awnwn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Lm"] != null && ds.Tables[0].Rows[0]["Lm"].ToString() != "")
                {
                    model.Lm = double.Parse(ds.Tables[0].Rows[0]["Lm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Lf"] != null && ds.Tables[0].Rows[0]["Lf"].ToString() != "")
                {
                    model.Lf =double.Parse( ds.Tables[0].Rows[0]["Lf"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Z"] != null && ds.Tables[0].Rows[0]["Z"].ToString() != "")
                {
                    model.Z =double.Parse(ds.Tables[0].Rows[0]["Z"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Avera"] != null && ds.Tables[0].Rows[0]["Avera"].ToString() != "")
                {
                    model.Avera =double.Parse(ds.Tables[0].Rows[0]["Avera"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SS"] != null && ds.Tables[0].Rows[0]["SS"].ToString() != "")
                {
                    model.SS = double.Parse(ds.Tables[0].Rows[0]["SS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Avera1"] != null && ds.Tables[0].Rows[0]["Avera1"].ToString() != "")
                {
                    model.Avera1 = double.Parse(ds.Tables[0].Rows[0]["Avera1"].ToString());
				}
				if(ds.Tables[0].Rows[0]["isFinish"]!=null && ds.Tables[0].Rows[0]["isFinish"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["isFinish"].ToString()=="1")||(ds.Tables[0].Rows[0]["isFinish"].ToString().ToLower()=="true"))
					{
						model.isFinish=true;
					}
					else
					{
						model.isFinish=false;
					}
				}
				if(ds.Tables[0].Rows[0]["testDate"]!=null && ds.Tables[0].Rows[0]["testDate"].ToString()!="")
				{
                    model.testDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["testDate"].ToString());
				}
                if (ds.Tables[0].Rows[0]["condition"] != null && ds.Tables[0].Rows[0]["condition"].ToString() != "")
                {
                    model.condition = ds.Tables[0].Rows[0]["condition"].ToString();
                }
                if (ds.Tables[0].Rows[0]["controlmode"] != null && ds.Tables[0].Rows[0]["controlmode"].ToString() != "")
                {
                    model.controlmode = ds.Tables[0].Rows[0]["controlmode"].ToString();
                }

                if (ds.Tables[0].Rows[0]["isUseExtensometer"] != null && ds.Tables[0].Rows[0]["isUseExtensometer"].ToString() != "")
				{
                    if ((ds.Tables[0].Rows[0]["isUseExtensometer"].ToString() == "1") || (ds.Tables[0].Rows[0]["isUseExtensometer"].ToString().ToLower() == "true"))
					{
                        model.isUseExtensometer = true;
					}
					else
					{
                        model.isUseExtensometer = false;
					}
				}
                if (ds.Tables[0].Rows[0]["isEffective"] != null && ds.Tables[0].Rows[0]["isEffective"].ToString() != "")
				{
                    if ((ds.Tables[0].Rows[0]["isEffective"].ToString() == "1") || (ds.Tables[0].Rows[0]["isEffective"].ToString().ToLower() == "true"))
					{
                        model.isEffective = true;
					}
					else
					{
                        model.isEffective = false;
					}
				}
                if (ds.Tables[0].Rows[0]["deltaLm"] != null && ds.Tables[0].Rows[0]["deltaLm"].ToString() != "")
                {
                    model.deltaLm = double.Parse(ds.Tables[0].Rows[0]["deltaLm"].ToString());
                }

				return model;
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HR_Test.Model.TestSample GetModel(string testSampleNo)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,a0,au,b0,bu,d0,du,Do,L0,L01,Lc,Le,Lt,Lu,Lu1,S0,Su,k,Fm,Rm,ReH,ReL,Rp,Rt,Rr,εp,εt,εr,E,m,mE,A,Aee,Agg,Att,Aggtt,Awnwn,Lm,Lf,Z,Avera,SS,Avera1,isFinish,testDate,condition,controlmode,isUseExtensometer,isEffective,deltaLm from tb_TestSample ");
            strSql.Append(" where testSampleNo=@testSampleNo ");
            OleDbParameter[] parameters = { new OleDbParameter("@testSampleNo", OleDbType.VarChar) };
            parameters[0].Value = testSampleNo;

            HR_Test.Model.TestSample model = new HR_Test.Model.TestSample();
            DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["testMethodName"] != null && ds.Tables[0].Rows[0]["testMethodName"].ToString() != "")
                {
                    model.testMethodName = ds.Tables[0].Rows[0]["testMethodName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["testNo"] != null && ds.Tables[0].Rows[0]["testNo"].ToString() != "")
                {
                    model.testNo = ds.Tables[0].Rows[0]["testNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["testSampleNo"] != null && ds.Tables[0].Rows[0]["testSampleNo"].ToString() != "")
                {
                    model.testSampleNo = ds.Tables[0].Rows[0]["testSampleNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["reportNo"] != null && ds.Tables[0].Rows[0]["reportNo"].ToString() != "")
                {
                    model.reportNo = ds.Tables[0].Rows[0]["reportNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sendCompany"] != null && ds.Tables[0].Rows[0]["sendCompany"].ToString() != "")
                {
                    model.sendCompany = ds.Tables[0].Rows[0]["sendCompany"].ToString();
                }
                if (ds.Tables[0].Rows[0]["stuffCardNo"] != null && ds.Tables[0].Rows[0]["stuffCardNo"].ToString() != "")
                {
                    model.stuffCardNo = ds.Tables[0].Rows[0]["stuffCardNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["stuffSpec"] != null && ds.Tables[0].Rows[0]["stuffSpec"].ToString() != "")
                {
                    model.stuffSpec = ds.Tables[0].Rows[0]["stuffSpec"].ToString();
                }
                if (ds.Tables[0].Rows[0]["stuffType"] != null && ds.Tables[0].Rows[0]["stuffType"].ToString() != "")
                {
                    model.stuffType = ds.Tables[0].Rows[0]["stuffType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["hotStatus"] != null && ds.Tables[0].Rows[0]["hotStatus"].ToString() != "")
                {
                    model.hotStatus = ds.Tables[0].Rows[0]["hotStatus"].ToString();
                }
                if (ds.Tables[0].Rows[0]["temperature"] != null && ds.Tables[0].Rows[0]["temperature"].ToString() != "")
                {
                    model.temperature = double.Parse(ds.Tables[0].Rows[0]["temperature"].ToString());
                }
                if (ds.Tables[0].Rows[0]["humidity"] != null && ds.Tables[0].Rows[0]["humidity"].ToString() != "")
                {
                    model.humidity = double.Parse(ds.Tables[0].Rows[0]["humidity"].ToString());
                }
                if (ds.Tables[0].Rows[0]["testStandard"] != null && ds.Tables[0].Rows[0]["testStandard"].ToString() != "")
                {
                    model.testStandard = ds.Tables[0].Rows[0]["testStandard"].ToString();
                }
                if (ds.Tables[0].Rows[0]["testMethod"] != null && ds.Tables[0].Rows[0]["testMethod"].ToString() != "")
                {
                    model.testMethod = ds.Tables[0].Rows[0]["testMethod"].ToString();
                }
                if (ds.Tables[0].Rows[0]["mathineType"] != null && ds.Tables[0].Rows[0]["mathineType"].ToString() != "")
                {
                    model.mathineType = ds.Tables[0].Rows[0]["mathineType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["testCondition"] != null && ds.Tables[0].Rows[0]["testCondition"].ToString() != "")
                {
                    model.testCondition = ds.Tables[0].Rows[0]["testCondition"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sampleCharacter"] != null && ds.Tables[0].Rows[0]["sampleCharacter"].ToString() != "")
                {
                    model.sampleCharacter = ds.Tables[0].Rows[0]["sampleCharacter"].ToString();
                }
                if (ds.Tables[0].Rows[0]["getSample"] != null && ds.Tables[0].Rows[0]["getSample"].ToString() != "")
                {
                    model.getSample = ds.Tables[0].Rows[0]["getSample"].ToString();
                }
                if (ds.Tables[0].Rows[0]["tester"] != null && ds.Tables[0].Rows[0]["tester"].ToString() != "")
                {
                    model.tester = ds.Tables[0].Rows[0]["tester"].ToString();
                }
                if (ds.Tables[0].Rows[0]["assessor"] != null && ds.Tables[0].Rows[0]["assessor"].ToString() != "")
                {
                    model.assessor = ds.Tables[0].Rows[0]["assessor"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sign"] != null && ds.Tables[0].Rows[0]["sign"].ToString() != "")
                {
                    model.sign = ds.Tables[0].Rows[0]["sign"].ToString();
                }
                if (ds.Tables[0].Rows[0]["a0"] != null && ds.Tables[0].Rows[0]["a0"].ToString() != "")
                {
                    model.a0 = double.Parse(ds.Tables[0].Rows[0]["a0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["au"] != null && ds.Tables[0].Rows[0]["au"].ToString() != "")
                {
                    model.au = double.Parse(ds.Tables[0].Rows[0]["au"].ToString());
                }
                if (ds.Tables[0].Rows[0]["b0"] != null && ds.Tables[0].Rows[0]["b0"].ToString() != "")
                {
                    model.b0 = double.Parse(ds.Tables[0].Rows[0]["b0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["bu"] != null && ds.Tables[0].Rows[0]["bu"].ToString() != "")
                {
                    model.bu = double.Parse(ds.Tables[0].Rows[0]["bu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["d0"] != null && ds.Tables[0].Rows[0]["d0"].ToString() != "")
                {
                    model.d0 = double.Parse(ds.Tables[0].Rows[0]["d0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["du"] != null && ds.Tables[0].Rows[0]["du"].ToString() != "")
                {
                    model.du = double.Parse(ds.Tables[0].Rows[0]["du"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Do"] != null && ds.Tables[0].Rows[0]["Do"].ToString() != "")
                {
                    model.Do = double.Parse(ds.Tables[0].Rows[0]["Do"].ToString());
                }
                if (ds.Tables[0].Rows[0]["L0"] != null && ds.Tables[0].Rows[0]["L0"].ToString() != "")
                {
                    model.L0 = double.Parse(ds.Tables[0].Rows[0]["L0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["L01"] != null && ds.Tables[0].Rows[0]["L01"].ToString() != "")
                {
                    model.L01 = double.Parse(ds.Tables[0].Rows[0]["L01"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Lc"] != null && ds.Tables[0].Rows[0]["Lc"].ToString() != "")
                {
                    model.Lc = double.Parse(ds.Tables[0].Rows[0]["Lc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Le"] != null && ds.Tables[0].Rows[0]["Le"].ToString() != "")
                {
                    model.Le = double.Parse(ds.Tables[0].Rows[0]["Le"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Lt"] != null && ds.Tables[0].Rows[0]["Lt"].ToString() != "")
                {
                    model.Lt = double.Parse(ds.Tables[0].Rows[0]["Lt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Lu"] != null && ds.Tables[0].Rows[0]["Lu"].ToString() != "")
                {
                    model.Lu = double.Parse(ds.Tables[0].Rows[0]["Lu"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Lu1"] != null && ds.Tables[0].Rows[0]["Lu1"].ToString() != "")
                {
                    model.Lu1 = double.Parse(ds.Tables[0].Rows[0]["Lu1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["S0"] != null && ds.Tables[0].Rows[0]["S0"].ToString() != "")
                {
                    model.S0 = double.Parse(ds.Tables[0].Rows[0]["S0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Su"] != null && ds.Tables[0].Rows[0]["Su"].ToString() != "")
                {
                    model.Su = double.Parse(ds.Tables[0].Rows[0]["Su"].ToString());
                }
                if (ds.Tables[0].Rows[0]["k"] != null && ds.Tables[0].Rows[0]["k"].ToString() != "")
                {
                    model.k = double.Parse(ds.Tables[0].Rows[0]["k"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fm"] != null && ds.Tables[0].Rows[0]["Fm"].ToString() != "")
                {
                    model.Fm = double.Parse(ds.Tables[0].Rows[0]["Fm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rm"] != null && ds.Tables[0].Rows[0]["Rm"].ToString() != "")
                {
                    model.Rm = double.Parse(ds.Tables[0].Rows[0]["Rm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReH"] != null && ds.Tables[0].Rows[0]["ReH"].ToString() != "")
                {
                    model.ReH = double.Parse(ds.Tables[0].Rows[0]["ReH"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReL"] != null && ds.Tables[0].Rows[0]["ReL"].ToString() != "")
                {
                    model.ReL = double.Parse(ds.Tables[0].Rows[0]["ReL"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rp"] != null && ds.Tables[0].Rows[0]["Rp"].ToString() != "")
                {
                    model.Rp = double.Parse(ds.Tables[0].Rows[0]["Rp"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rt"] != null && ds.Tables[0].Rows[0]["Rt"].ToString() != "")
                {
                    model.Rt = double.Parse(ds.Tables[0].Rows[0]["Rt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rr"] != null && ds.Tables[0].Rows[0]["Rr"].ToString() != "")
                {
                    model.Rr = double.Parse(ds.Tables[0].Rows[0]["Rr"].ToString());
                }
                if (ds.Tables[0].Rows[0]["εp"] != null && ds.Tables[0].Rows[0]["εp"].ToString() != "")
                {
                    model.εp = double.Parse(ds.Tables[0].Rows[0]["εp"].ToString());
                }
                if (ds.Tables[0].Rows[0]["εt"] != null && ds.Tables[0].Rows[0]["εt"].ToString() != "")
                {
                    model.εt = double.Parse(ds.Tables[0].Rows[0]["εt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["εr"] != null && ds.Tables[0].Rows[0]["εr"].ToString() != "")
                {
                    model.εr = double.Parse(ds.Tables[0].Rows[0]["εr"].ToString());
                }
                if (ds.Tables[0].Rows[0]["E"] != null && ds.Tables[0].Rows[0]["E"].ToString() != "")
                {
                    model.E = double.Parse(ds.Tables[0].Rows[0]["E"].ToString());
                }
                if (ds.Tables[0].Rows[0]["m"] != null && ds.Tables[0].Rows[0]["m"].ToString() != "")
                {
                    model.m = double.Parse(ds.Tables[0].Rows[0]["m"].ToString());
                }
                if (ds.Tables[0].Rows[0]["mE"] != null && ds.Tables[0].Rows[0]["mE"].ToString() != "")
                {
                    model.mE = double.Parse(ds.Tables[0].Rows[0]["mE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["A"] != null && ds.Tables[0].Rows[0]["A"].ToString() != "")
                {
                    model.A = double.Parse(ds.Tables[0].Rows[0]["A"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Aee"] != null && ds.Tables[0].Rows[0]["Aee"].ToString() != "")
                {
                    model.Aee = double.Parse(ds.Tables[0].Rows[0]["Aee"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Agg"] != null && ds.Tables[0].Rows[0]["Agg"].ToString() != "")
                {
                    model.Agg = double.Parse(ds.Tables[0].Rows[0]["Agg"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Att"] != null && ds.Tables[0].Rows[0]["Att"].ToString() != "")
                {
                    model.Att = double.Parse(ds.Tables[0].Rows[0]["Att"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Aggtt"] != null && ds.Tables[0].Rows[0]["Aggtt"].ToString() != "")
                {
                    model.Aggtt = double.Parse(ds.Tables[0].Rows[0]["Aggtt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Awnwn"] != null && ds.Tables[0].Rows[0]["Awnwn"].ToString() != "")
                {
                    model.Awnwn = double.Parse(ds.Tables[0].Rows[0]["Awnwn"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Lm"] != null && ds.Tables[0].Rows[0]["Lm"].ToString() != "")
                {
                    model.Lm = double.Parse(ds.Tables[0].Rows[0]["Lm"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Lf"] != null && ds.Tables[0].Rows[0]["Lf"].ToString() != "")
                {
                    model.Lf = double.Parse(ds.Tables[0].Rows[0]["Lf"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Z"] != null && ds.Tables[0].Rows[0]["Z"].ToString() != "")
                {
                    model.Z = double.Parse(ds.Tables[0].Rows[0]["Z"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Avera"] != null && ds.Tables[0].Rows[0]["Avera"].ToString() != "")
                {
                    model.Avera = double.Parse(ds.Tables[0].Rows[0]["Avera"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SS"] != null && ds.Tables[0].Rows[0]["SS"].ToString() != "")
                {
                    model.SS = double.Parse(ds.Tables[0].Rows[0]["SS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Avera1"] != null && ds.Tables[0].Rows[0]["Avera1"].ToString() != "")
                {
                    model.Avera1 = double.Parse(ds.Tables[0].Rows[0]["Avera1"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isFinish"] != null && ds.Tables[0].Rows[0]["isFinish"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["isFinish"].ToString() == "1") || (ds.Tables[0].Rows[0]["isFinish"].ToString().ToLower() == "true"))
                    {
                        model.isFinish = true;
                    }
                    else
                    {
                        model.isFinish = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["testDate"] != null && ds.Tables[0].Rows[0]["testDate"].ToString() != "")
                {
                    model.testDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["testDate"].ToString());
                }

                if (ds.Tables[0].Rows[0]["condition"] != null && ds.Tables[0].Rows[0]["condition"].ToString() != "")
                {
                    model.condition = ds.Tables[0].Rows[0]["condition"].ToString();
                }

                if (ds.Tables[0].Rows[0]["controlmode"] != null && ds.Tables[0].Rows[0]["controlmode"].ToString() != "")
                {
                    model.controlmode = ds.Tables[0].Rows[0]["controlmode"].ToString();
                }

                if (ds.Tables[0].Rows[0]["isUseExtensometer"] != null && ds.Tables[0].Rows[0]["isUseExtensometer"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["isUseExtensometer"].ToString() == "1") || (ds.Tables[0].Rows[0]["isUseExtensometer"].ToString().ToLower() == "true"))
                    {
                        model.isUseExtensometer = true;
                    }
                    else
                    {
                        model.isUseExtensometer = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["isEffective"] != null && ds.Tables[0].Rows[0]["isEffective"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["isEffective"].ToString() == "1") || (ds.Tables[0].Rows[0]["isEffective"].ToString().ToLower() == "true"))
                    {
                        model.isEffective = true;
                    }
                    else
                    {
                        model.isEffective = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["deltaLm"] != null && ds.Tables[0].Rows[0]["deltaLm"].ToString() != "")
                {
                    model.deltaLm = double.Parse(ds.Tables[0].Rows[0]["deltaLm"].ToString());
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
			//strSql.Append("select ID,testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,a0,au,b0,bu,d0,du,Do,L0,L01,Lc,Le,Lt,Lu,Lu1,S0,Su,k,Fm,Rm,ReH,ReL,Rp,Rt,Rr,εp,εt,εr,E,m,mE,A,Aee,Agg,Att,Aggtt,Awnwn,Lm,Lf,Z,Avera,SS,Avera1,isFinish,testDate ");
            strSql.Append(" select *  "); 
            strSql.Append(" FROM tb_TestSample ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" order by testSampleNo ");
			return DbHelperOleDb.Query(strSql.ToString());
		}

        //获得不重复项的列表
        public DataSet GetNotOverlapList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select ID,testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,a0,au,b0,bu,d0,du,Do,L0,L01,Lc,Le,Lt,Lu,Lu1,S0,Su,k,Fm,Rm,ReH,ReL,Rp,Rt,Rr,εp,εt,εr,E,m,mE,A,Aee,Agg,Att,Aggtt,Awnwn,Lm,Lf,Z,Avera,SS,Avera1,isFinish,testDate ");
            strSql.Append(" select Distinct testNo,testMethodName,a0,b0,d0,Do ");//
            strSql.Append(" FROM tb_TestSample ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            } 
            return DbHelperOleDb.Query(strSql.ToString());
        }

        //获得不重复项的列表
        public DataSet GetNotOverlapList1(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select ID,testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,a0,au,b0,bu,d0,du,Do,L0,L01,Lc,Le,Lt,Lu,Lu1,S0,Su,k,Fm,Rm,ReH,ReL,Rp,Rt,Rr,εp,εt,εr,E,m,mE,A,Aee,Agg,Att,Aggtt,Awnwn,Lm,Lf,Z,Avera,SS,Avera1,isFinish,testDate ");
            strSql.Append(" select Distinct testNo,testMethodName ");//
            strSql.Append(" FROM tb_TestSample ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }



        //获取完成试验列表
        public DataSet GetFinishList(string strWhere,double maxValue)
        {
            StringBuilder strSql = new StringBuilder();//εp as [Ep],εt as [Et],εr as [Er],E,m,mE,Rt,Rr,Aee as Ae,Agg as [Ag],Att as [At],Aggtt as [Agt],Awnwn as [Awn],Lm as [△Lm],Lf as [△Lf],SS as [S],
            strSql.Append("select testSampleNo as [试样编号],[a0] as [a(mm)],[b0] as [b(mm)],[d0] as [d(mm)],");

            int dotvalue = Dotvalue(maxValue); 
            if (maxValue < 1000.0)
            {
                switch (dotvalue)
                {
                    case 2:
                        strSql.Append(" Format([Fm],'0.00') as [Fm(N)],"); 
                        break;
                    case 3:
                        strSql.Append(" Format([Fm],'0.000') as [Fm(N)],"); 
                        break;
                    case 4:
                        strSql.Append(" Format([Fm],'0.0000') as [Fm(N)],"); 
                        break;
                    case 5:
                        strSql.Append(" Format([Fm],'0.00000') as [Fm(N)],"); 
                        break;
                    default:
                        strSql.Append(" Format([Fm],'0.00') as [Fm(N)],"); 
                        break;
                }

            }
            if (maxValue >= 1000.0)
            {
                switch (dotvalue)
                {
                    case 2:
                        strSql.Append(" Format([Fm]/1000.0,'0.00') as [Fm(kN)],"); 
                        break;
                    case 3:
                        strSql.Append(" Format([Fm]/1000.0,'0.000') as [Fm(kN)],"); 
                        break;
                    case 4:
                        strSql.Append(" Format([Fm]/1000.0,'0.0000') as [Fm(kN)],"); 
                        break;
                    case 5:
                        strSql.Append(" Format([Fm]/1000.0,'0.00000') as [Fm(kN)],"); 
                        break;
                    default:
                        strSql.Append(" Format([Fm]/1000.0,'0.00') as [Fm(kN)],"); 
                        break;
                }
            } 
            strSql.Append("Rm as [Rm(MPa)],ReH as [ReH(MPa)],ReL as [ReL(MPa)],Rp as [Rp(MPa)],A as [A(%)],Z as [Z(%)],testDate as [试验日期]  FROM tb_TestSample ");
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


        //获取完成试验列表1
        public DataSet GetFinishList1(string selCol, string strWhere,int ab)
        {
            StringBuilder strSql = new StringBuilder();
            switch(ab)
            {
                case 1:
                    strSql.Append("select isEffective as [试验无效], testSampleNo as [试样编号],[a0] as [a(mm)],[b0] as [b(mm)],[Lu] as [Lu(mm)]");
                    break;
                case 2:
                    strSql.Append("select isEffective as [试验无效], testSampleNo as [试样编号],[d0] as [d(mm)],[Lu] as [Lu(mm)]");
                    break;
                case 3:
                    strSql.Append("select isEffective as [试验无效], testSampleNo as [试样编号],[a0] as [a(mm)],[Do] as [D0(mm)],[Lu] as [Lu(mm)]");
                    break;
                default:
                    strSql.Append("select isEffective as [试验无效], testSampleNo as [试样编号],[a0] as [a(mm)],[b0] as [b(mm)],[Lu] as [Lu(mm)]");
                    break;
            }                

            if (!string.IsNullOrEmpty(selCol))//Fm,Rm,ReH,ReL,E,A,Z,SS as [S],FORMAT(testDate,'YYYY-MM-DD') as [试验日期] 
            {  
                strSql.Append("," + selCol);
            }
            strSql.Append("  FROM tb_TestSample ");// ,FORMAT(testDate,'YYYY-MM-DD') as [试验日期]
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by testSampleNo ");
            return DbHelperOleDb.Query(strSql.ToString());
        }

        //获取完成试验列表1
        public DataSet GetFinishListReport(string selCol, string strWhere, int ab)
        {
            StringBuilder strSql = new StringBuilder();
            switch (ab)
            {
                case 1:
                    strSql.Append("select testSampleNo as [试样编号],[a0] as [a(mm)],[b0] as [b(mm)],[Lu] as [Lu(mm)]");
                    break;
                case 2:
                    strSql.Append("select testSampleNo as [试样编号],[d0] as [d(mm)],[Lu] as [Lu(mm)]");
                    break;
                case 3:
                    strSql.Append("select testSampleNo as [试样编号],[a0] as [a(mm)],[Do] as [D0(mm)],[Lu] as [Lu(mm)]");
                    break;
                default:
                    strSql.Append("select testSampleNo as [试样编号],[a0] as [a(mm)],[b0] as [b(mm)],[Lu] as [Lu(mm)]");
                    break;
            }

            if (!string.IsNullOrEmpty(selCol))//Fm,Rm,ReH,ReL,E,A,Z,SS as [S],FORMAT(testDate,'YYYY-MM-DD') as [试验日期] 
            {
                strSql.Append("," + selCol);
            }
            strSql.Append("  FROM tb_TestSample ");// ,FORMAT(testDate,'YYYY-MM-DD') as [试验日期]
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by testSampleNo ");
            return DbHelperOleDb.Query(strSql.ToString());
        }


        //获取完成试验列表，求平均
        public DataSet GetFinishSumList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();//Avg(Rt) as Rt,Avg(Rr) as Rr, Avg(εp) as Ep,Avg(εt) as Et,Avg(εr) as Er,Avg(E) as E,Avg(m) as m,Avg(mE) as mE,Avg(Aee) as Ae,Avg(Agg) as [Ag],Avg(Att) as [At],Avg(Aggtt) as [Agt],Avg(Awnwn) as [Awn],Avg(Lm) as [△Lm],Avg(Lf) as [△Lf],,Avg(SS) as [S]
            strSql.Append("select Avg(Fm) as Fm,Avg(Rm) as Rm,Avg(ReH) as ReH,Avg(ReL) as ReL,Avg(Rp) as Rp,");
            strSql.Append(" Avg(A) as [A%],Avg(Z) as [Z%] ");
            strSql.Append(" FROM tb_TestSample ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }


        //获取完成试验列表，求平均
        public DataSet GetFinishSumList1(string selColAver,string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select");
            strSql.Append(selColAver); 
            strSql.Append(" FROM tb_TestSample ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        } 



        //获取完成试验列表
        public DataSet GetFinishAverage(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select testSampleNo as [试样编号],Fm,Rm,ReH,ReL,Rp,Rt,Rr,εp as [Ep],εt as [Et],εr as [Er],E,m,mE,A as [A%],Aee as [Ae],Agg as [Ag],Att as [At],Aggtt as [Agt],Awnwn as [Awn],Lm,Lf as [△Lf],Z as [Z%],SS as [S],testDate as [试验日期] ");
            strSql.Append(" FROM tb_TestSample ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
			parameters[0].Value = "tb_TestSample";
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

