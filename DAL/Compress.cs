using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
    /// <summary>
    /// 数据访问类:Compress
    /// </summary>
    public partial class Compress
    {
        public Compress()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string testSampleNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_Compress");
            strSql.Append(" where testSampleNo=@testSampleNo ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@testSampleNo", OleDbType.VarChar,100)};
            parameters[0].Value = testSampleNo;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(HR_Test.Model.Compress model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_Compress(");
            strSql.Append("testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,a,b,d,L,L0,H,hh,S0,deltaL,εpc,εtc,n,F0,Ff,Fpc,Ftc,FeHc,FeLc,Fmc,Rpc,Rtc,ReHc,ReLc,Rmc,Ec,Avera,Avera1,isFinish,testDate,condition,controlmode,isUseExtensometer,isEffective)");
            strSql.Append(" values (");
            strSql.Append("@testMethodName,@testNo,@testSampleNo,@reportNo,@sendCompany,@stuffCardNo,@stuffSpec,@stuffType,@hotStatus,@temperature,@humidity,@testStandard,@testMethod,@mathineType,@testCondition,@sampleCharacter,@getSample,@tester,@assessor,@sign,@a,@b,@d,@L,@L0,@H,@hh,@S0,@deltaL,@εpc,@εtc,@n,@F0,@Ff,@Fpc,@Ftc,@FeHc,@FeLc,@Fmc,@Rpc,@Rtc,@ReHc,@ReLc,@Rmc,@Ec,@Avera,@Avera1,@isFinish,@testDate,@condition,@controlmode,@isUseExtensometer,@isEffective)");
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
					new OleDbParameter("@a", OleDbType.Double),
					new OleDbParameter("@b", OleDbType.Double),
					new OleDbParameter("@d", OleDbType.Double),
					new OleDbParameter("@L", OleDbType.Double),
					new OleDbParameter("@L0", OleDbType.Double),
					new OleDbParameter("@H", OleDbType.Double),
					new OleDbParameter("@hh", OleDbType.Double),
					new OleDbParameter("@S0", OleDbType.Double),
					new OleDbParameter("@deltaL", OleDbType.Double),
					new OleDbParameter("@εpc", OleDbType.Double),
					new OleDbParameter("@εtc", OleDbType.Double),
					new OleDbParameter("@n", OleDbType.Double),
					new OleDbParameter("@F0", OleDbType.Double),
					new OleDbParameter("@Ff", OleDbType.Double),
					new OleDbParameter("@Fpc", OleDbType.Double),
					new OleDbParameter("@Ftc", OleDbType.Double),
					new OleDbParameter("@FeHc", OleDbType.Double),
					new OleDbParameter("@FeLc", OleDbType.Double),
					new OleDbParameter("@Fmc", OleDbType.Double),
					new OleDbParameter("@Rpc", OleDbType.Double),
					new OleDbParameter("@Rtc", OleDbType.Double),
					new OleDbParameter("@ReHc", OleDbType.Double),
					new OleDbParameter("@ReLc", OleDbType.Double),
					new OleDbParameter("@Rmc", OleDbType.Double),
					new OleDbParameter("@Ec", OleDbType.Double),
					new OleDbParameter("@Avera", OleDbType.Double),
					new OleDbParameter("@Avera1", OleDbType.Double),
					new OleDbParameter("@isFinish", OleDbType.Boolean,1),
					new OleDbParameter("@testDate", OleDbType.DBDate),
                                          new OleDbParameter("@condition", OleDbType.VarChar,255),
                                          new OleDbParameter("@controlmode", OleDbType.VarChar,255),
                                          new OleDbParameter("@isUseExtensometer", OleDbType.Boolean) ,
                                          new OleDbParameter("@isEffective", OleDbType.Boolean)
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
            parameters[20].Value = model.a;
            parameters[21].Value = model.b;
            parameters[22].Value = model.d;
            parameters[23].Value = model.L;
            parameters[24].Value = model.L0;
            parameters[25].Value = model.H;
            parameters[26].Value = model.hh;
            parameters[27].Value = model.S0;
            parameters[28].Value = model.deltaL;
            parameters[29].Value = model.εpc;
            parameters[30].Value = model.εtc;
            parameters[31].Value = model.n;
            parameters[32].Value = model.F0;
            parameters[33].Value = model.Ff;
            parameters[34].Value = model.Fpc;
            parameters[35].Value = model.Ftc;
            parameters[36].Value = model.FeHc;
            parameters[37].Value = model.FeLc;
            parameters[38].Value = model.Fmc;
            parameters[39].Value = model.Rpc;
            parameters[40].Value = model.Rtc;
            parameters[41].Value = model.ReHc;
            parameters[42].Value = model.ReLc;
            parameters[43].Value = model.Rmc;
            parameters[44].Value = model.Ec;
            parameters[45].Value = model.Avera;
            parameters[46].Value = model.Avera1;
            parameters[47].Value = model.isFinish;
            parameters[48].Value = model.testDate;
            parameters[49].Value = model.condition;
            parameters[50].Value = model.controlMode;
            parameters[51].Value = model.isUseExtensometer;
            parameters[52].Value = model.isEffective;
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
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Compress model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Compress set ");
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
            strSql.Append("a=@a,");
            strSql.Append("b=@b,");
            strSql.Append("d=@d,");
            strSql.Append("L=@L,");
            strSql.Append("L0=@L0,");
            strSql.Append("H=@H,");
            strSql.Append("hh=@hh,");
            strSql.Append("S0=@S0,");
            strSql.Append("deltaL=@deltaL,");
            strSql.Append("εpc=@εpc,");
            strSql.Append("εtc=@εtc,");
            strSql.Append("n=@n,");
            strSql.Append("F0=@F0,");
            strSql.Append("Ff=@Ff,");
            strSql.Append("Fpc=@Fpc,");
            strSql.Append("Ftc=@Ftc,");
            strSql.Append("FeHc=@FeHc,");
            strSql.Append("FeLc=@FeLc,");
            strSql.Append("Fmc=@Fmc,");
            strSql.Append("Rpc=@Rpc,");
            strSql.Append("Rtc=@Rtc,");
            strSql.Append("ReHc=@ReHc,");
            strSql.Append("ReLc=@ReLc,");
            strSql.Append("Rmc=@Rmc,");
            strSql.Append("Ec=@Ec,");
            strSql.Append("Avera=@Avera,");
            strSql.Append("Avera1=@Avera1,");
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
					new OleDbParameter("@a", OleDbType.Double),
					new OleDbParameter("@b", OleDbType.Double),
					new OleDbParameter("@d", OleDbType.Double),
					new OleDbParameter("@L", OleDbType.Double),
					new OleDbParameter("@L0", OleDbType.Double),
					new OleDbParameter("@H", OleDbType.Double),
					new OleDbParameter("@hh", OleDbType.Double),
					new OleDbParameter("@S0", OleDbType.Double),
					new OleDbParameter("@deltaL", OleDbType.Double),
					new OleDbParameter("@εpc", OleDbType.Double),
					new OleDbParameter("@εtc", OleDbType.Double),
					new OleDbParameter("@n", OleDbType.Double),
					new OleDbParameter("@F0", OleDbType.Double),
					new OleDbParameter("@Ff", OleDbType.Double),
					new OleDbParameter("@Fpc", OleDbType.Double),
					new OleDbParameter("@Ftc", OleDbType.Double),
					new OleDbParameter("@FeHc", OleDbType.Double),
					new OleDbParameter("@FeLc", OleDbType.Double),
					new OleDbParameter("@Fmc", OleDbType.Double),
					new OleDbParameter("@Rpc", OleDbType.Double),
					new OleDbParameter("@Rtc", OleDbType.Double),
					new OleDbParameter("@ReHc", OleDbType.Double),
					new OleDbParameter("@ReLc", OleDbType.Double),
					new OleDbParameter("@Rmc", OleDbType.Double),
					new OleDbParameter("@Ec", OleDbType.Double),
					new OleDbParameter("@Avera", OleDbType.Double),
					new OleDbParameter("@Avera1", OleDbType.Double),
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
            parameters[20].Value = model.a;
            parameters[21].Value = model.b;
            parameters[22].Value = model.d;
            parameters[23].Value = model.L;
            parameters[24].Value = model.L0;
            parameters[25].Value = model.H;
            parameters[26].Value = model.hh;
            parameters[27].Value = model.S0;
            parameters[28].Value = model.deltaL;
            parameters[29].Value = model.εpc;
            parameters[30].Value = model.εtc;
            parameters[31].Value = model.n;
            parameters[32].Value = model.F0;
            parameters[33].Value = model.Ff;
            parameters[34].Value = model.Fpc;
            parameters[35].Value = model.Ftc;
            parameters[36].Value = model.FeHc;
            parameters[37].Value = model.FeLc;
            parameters[38].Value = model.Fmc;
            parameters[39].Value = model.Rpc;
            parameters[40].Value = model.Rtc;
            parameters[41].Value = model.ReHc;
            parameters[42].Value = model.ReLc;
            parameters[43].Value = model.Rmc;
            parameters[44].Value = model.Ec;
            parameters[45].Value = model.Avera;
            parameters[46].Value = model.Avera1;
            parameters[47].Value = model.isFinish;
            parameters[48].Value = model.testDate;
            parameters[49].Value = model.condition;
            parameters[50].Value = model.controlMode;
            parameters[51].Value = model.isUseExtensometer;
            parameters[52].Value = model.isEffective;
            parameters[53].Value = model.ID;

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
        public bool Delete(string testSampleNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_Compress ");
            strSql.Append(" where testSampleNo=@testSampleNo ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@testSampleNo", OleDbType.VarChar,100)};
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
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string testSampleNolist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_Compress ");
            strSql.Append(" where testSampleNo in (" + testSampleNolist + ")  ");
            int rows = DbHelperOleDb.ExecuteSql(strSql.ToString());
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
        public HR_Test.Model.Compress GetModel(string testSampleNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,a,b,d,L,L0,H,hh,S0,deltaL,εpc,εtc,n,F0,Ff,Fpc,Ftc,FeHc,FeLc,Fmc,Rpc,Rtc,ReHc,ReLc,Rmc,Ec,Avera,Avera1,isFinish,testDate,condition,controlmode,isUseExtensometer,isEffective from tb_Compress ");
            strSql.Append(" where testSampleNo=@testSampleNo ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@testSampleNo", OleDbType.VarChar,100)};
            parameters[0].Value = testSampleNo;

            HR_Test.Model.Compress model = new HR_Test.Model.Compress();
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
                if (ds.Tables[0].Rows[0]["a"] != null && ds.Tables[0].Rows[0]["a"].ToString() != "")
                {
                    model.a = double.Parse(ds.Tables[0].Rows[0]["a"].ToString());
                }
                if (ds.Tables[0].Rows[0]["b"] != null && ds.Tables[0].Rows[0]["b"].ToString() != "")
                {
                    model.b = double.Parse(ds.Tables[0].Rows[0]["b"].ToString());
                }
                if (ds.Tables[0].Rows[0]["d"] != null && ds.Tables[0].Rows[0]["d"].ToString() != "")
                {
                    model.d = double.Parse(ds.Tables[0].Rows[0]["d"].ToString());
                }
                if (ds.Tables[0].Rows[0]["L"] != null && ds.Tables[0].Rows[0]["L"].ToString() != "")
                {
                    model.L = double.Parse(ds.Tables[0].Rows[0]["L"].ToString());
                }
                if (ds.Tables[0].Rows[0]["L0"] != null && ds.Tables[0].Rows[0]["L0"].ToString() != "")
                {
                    model.L0 = double.Parse(ds.Tables[0].Rows[0]["L0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["H"] != null && ds.Tables[0].Rows[0]["H"].ToString() != "")
                {
                    model.H = double.Parse(ds.Tables[0].Rows[0]["H"].ToString());
                }
                if (ds.Tables[0].Rows[0]["hh"] != null && ds.Tables[0].Rows[0]["hh"].ToString() != "")
                {
                    model.hh = double.Parse(ds.Tables[0].Rows[0]["hh"].ToString());
                }
                if (ds.Tables[0].Rows[0]["S0"] != null && ds.Tables[0].Rows[0]["S0"].ToString() != "")
                {
                    model.S0 = double.Parse(ds.Tables[0].Rows[0]["S0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["deltaL"] != null && ds.Tables[0].Rows[0]["deltaL"].ToString() != "")
                {
                    model.deltaL = double.Parse(ds.Tables[0].Rows[0]["deltaL"].ToString());
                }
                if (ds.Tables[0].Rows[0]["εpc"] != null && ds.Tables[0].Rows[0]["εpc"].ToString() != "")
                {
                    model.εpc = double.Parse(ds.Tables[0].Rows[0]["εpc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["εtc"] != null && ds.Tables[0].Rows[0]["εtc"].ToString() != "")
                {
                    model.εtc = double.Parse(ds.Tables[0].Rows[0]["εtc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["n"] != null && ds.Tables[0].Rows[0]["n"].ToString() != "")
                {
                    model.n = double.Parse(ds.Tables[0].Rows[0]["n"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F0"] != null && ds.Tables[0].Rows[0]["F0"].ToString() != "")
                {
                    model.F0 = double.Parse(ds.Tables[0].Rows[0]["F0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ff"] != null && ds.Tables[0].Rows[0]["Ff"].ToString() != "")
                {
                    model.Ff = double.Parse(ds.Tables[0].Rows[0]["Ff"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fpc"] != null && ds.Tables[0].Rows[0]["Fpc"].ToString() != "")
                {
                    model.Fpc = double.Parse(ds.Tables[0].Rows[0]["Fpc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ftc"] != null && ds.Tables[0].Rows[0]["Ftc"].ToString() != "")
                {
                    model.Ftc = double.Parse(ds.Tables[0].Rows[0]["Ftc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FeHc"] != null && ds.Tables[0].Rows[0]["FeHc"].ToString() != "")
                {
                    model.FeHc = double.Parse(ds.Tables[0].Rows[0]["FeHc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FeLc"] != null && ds.Tables[0].Rows[0]["FeLc"].ToString() != "")
                {
                    model.FeLc = double.Parse(ds.Tables[0].Rows[0]["FeLc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fmc"] != null && ds.Tables[0].Rows[0]["Fmc"].ToString() != "")
                {
                    model.Fmc = double.Parse(ds.Tables[0].Rows[0]["Fmc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rpc"] != null && ds.Tables[0].Rows[0]["Rpc"].ToString() != "")
                {
                    model.Rpc = double.Parse(ds.Tables[0].Rows[0]["Rpc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rtc"] != null && ds.Tables[0].Rows[0]["Rtc"].ToString() != "")
                {
                    model.Rtc = double.Parse(ds.Tables[0].Rows[0]["Rtc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReHc"] != null && ds.Tables[0].Rows[0]["ReHc"].ToString() != "")
                {
                    model.ReHc = double.Parse(ds.Tables[0].Rows[0]["ReHc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReLc"] != null && ds.Tables[0].Rows[0]["ReLc"].ToString() != "")
                {
                    model.ReLc = double.Parse(ds.Tables[0].Rows[0]["ReLc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rmc"] != null && ds.Tables[0].Rows[0]["Rmc"].ToString() != "")
                {
                    model.Rmc = double.Parse(ds.Tables[0].Rows[0]["Rmc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ec"] != null && ds.Tables[0].Rows[0]["Ec"].ToString() != "")
                {
                    model.Ec = double.Parse(ds.Tables[0].Rows[0]["Ec"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Avera"] != null && ds.Tables[0].Rows[0]["Avera"].ToString() != "")
                {
                    model.Avera = double.Parse(ds.Tables[0].Rows[0]["Avera"].ToString());
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
                    model.controlMode = ds.Tables[0].Rows[0]["controlmode"].ToString();
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
                        model.isUseExtensometer = true;
                    }
                    else
                    {
                        model.isUseExtensometer = false;
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
        /// 得到一个对象实体
        /// </summary>
        public HR_Test.Model.Compress GetModel(long ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,a,b,d,L,L0,H,hh,S0,deltaL,εpc,εtc,n,F0,Ff,Fpc,Ftc,FeHc,FeLc,Fmc,Rpc,Rtc,ReHc,ReLc,Rmc,Ec,Avera,Avera1,isFinish,testDate,condition,controlmode,isUseExtensometer,isEffective from tb_Compress ");
            strSql.Append(" where ID=@ID ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.BigInt,8)};
            parameters[0].Value = ID;

            HR_Test.Model.Compress model = new HR_Test.Model.Compress();
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
                if (ds.Tables[0].Rows[0]["a"] != null && ds.Tables[0].Rows[0]["a"].ToString() != "")
                {
                    model.a = double.Parse(ds.Tables[0].Rows[0]["a"].ToString());
                }
                if (ds.Tables[0].Rows[0]["b"] != null && ds.Tables[0].Rows[0]["b"].ToString() != "")
                {
                    model.b = double.Parse(ds.Tables[0].Rows[0]["b"].ToString());
                }
                if (ds.Tables[0].Rows[0]["d"] != null && ds.Tables[0].Rows[0]["d"].ToString() != "")
                {
                    model.d = double.Parse(ds.Tables[0].Rows[0]["d"].ToString());
                }
                if (ds.Tables[0].Rows[0]["L"] != null && ds.Tables[0].Rows[0]["L"].ToString() != "")
                {
                    model.L = double.Parse(ds.Tables[0].Rows[0]["L"].ToString());
                }
                if (ds.Tables[0].Rows[0]["L0"] != null && ds.Tables[0].Rows[0]["L0"].ToString() != "")
                {
                    model.L0 = double.Parse(ds.Tables[0].Rows[0]["L0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["H"] != null && ds.Tables[0].Rows[0]["H"].ToString() != "")
                {
                    model.H = double.Parse(ds.Tables[0].Rows[0]["H"].ToString());
                }
                if (ds.Tables[0].Rows[0]["hh"] != null && ds.Tables[0].Rows[0]["hh"].ToString() != "")
                {
                    model.hh = double.Parse(ds.Tables[0].Rows[0]["hh"].ToString());
                }
                if (ds.Tables[0].Rows[0]["S0"] != null && ds.Tables[0].Rows[0]["S0"].ToString() != "")
                {
                    model.S0 = double.Parse(ds.Tables[0].Rows[0]["S0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["deltaL"] != null && ds.Tables[0].Rows[0]["deltaL"].ToString() != "")
                {
                    model.deltaL = double.Parse(ds.Tables[0].Rows[0]["deltaL"].ToString());
                }
                if (ds.Tables[0].Rows[0]["εpc"] != null && ds.Tables[0].Rows[0]["εpc"].ToString() != "")
                {
                    model.εpc = double.Parse(ds.Tables[0].Rows[0]["εpc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["εtc"] != null && ds.Tables[0].Rows[0]["εtc"].ToString() != "")
                {
                    model.εtc = double.Parse(ds.Tables[0].Rows[0]["εtc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["n"] != null && ds.Tables[0].Rows[0]["n"].ToString() != "")
                {
                    model.n = double.Parse(ds.Tables[0].Rows[0]["n"].ToString());
                }
                if (ds.Tables[0].Rows[0]["F0"] != null && ds.Tables[0].Rows[0]["F0"].ToString() != "")
                {
                    model.F0 = double.Parse(ds.Tables[0].Rows[0]["F0"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ff"] != null && ds.Tables[0].Rows[0]["Ff"].ToString() != "")
                {
                    model.Ff = double.Parse(ds.Tables[0].Rows[0]["Ff"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fpc"] != null && ds.Tables[0].Rows[0]["Fpc"].ToString() != "")
                {
                    model.Fpc = double.Parse(ds.Tables[0].Rows[0]["Fpc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ftc"] != null && ds.Tables[0].Rows[0]["Ftc"].ToString() != "")
                {
                    model.Ftc = double.Parse(ds.Tables[0].Rows[0]["Ftc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FeHc"] != null && ds.Tables[0].Rows[0]["FeHc"].ToString() != "")
                {
                    model.FeHc = double.Parse(ds.Tables[0].Rows[0]["FeHc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FeLc"] != null && ds.Tables[0].Rows[0]["FeLc"].ToString() != "")
                {
                    model.FeLc = double.Parse(ds.Tables[0].Rows[0]["FeLc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fmc"] != null && ds.Tables[0].Rows[0]["Fmc"].ToString() != "")
                {
                    model.Fmc = double.Parse(ds.Tables[0].Rows[0]["Fmc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rpc"] != null && ds.Tables[0].Rows[0]["Rpc"].ToString() != "")
                {
                    model.Rpc = double.Parse(ds.Tables[0].Rows[0]["Rpc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rtc"] != null && ds.Tables[0].Rows[0]["Rtc"].ToString() != "")
                {
                    model.Rtc = double.Parse(ds.Tables[0].Rows[0]["Rtc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReHc"] != null && ds.Tables[0].Rows[0]["ReHc"].ToString() != "")
                {
                    model.ReHc = double.Parse(ds.Tables[0].Rows[0]["ReHc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ReLc"] != null && ds.Tables[0].Rows[0]["ReLc"].ToString() != "")
                {
                    model.ReLc = double.Parse(ds.Tables[0].Rows[0]["ReLc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Rmc"] != null && ds.Tables[0].Rows[0]["Rmc"].ToString() != "")
                {
                    model.Rmc = double.Parse(ds.Tables[0].Rows[0]["Rmc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Ec"] != null && ds.Tables[0].Rows[0]["Ec"].ToString() != "")
                {
                    model.Ec = double.Parse(ds.Tables[0].Rows[0]["Ec"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Avera"] != null && ds.Tables[0].Rows[0]["Avera"].ToString() != "")
                {
                    model.Avera = double.Parse(ds.Tables[0].Rows[0]["Avera"].ToString());
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
                    model.controlMode = ds.Tables[0].Rows[0]["controlmode"].ToString();
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM tb_Compress ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by testSampleNo ");

            return DbHelperOleDb.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetResultRow(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select testSampleNo,Fmc,Fpc,Ftc,FeHc,FeLc,Rmc,Rpc,Rtc,ReHc,ReLc,Ec  ");
            strSql.Append(" FROM tb_Compress ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetNotOverlapList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Distinct testNo,a,b,d ");
            strSql.Append(" FROM tb_Compress ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetMaxF(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MAX([Fmc]) as [Fmc] ");
            strSql.Append(" FROM tb_Compress ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetNotOverlapList1(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Distinct testNo,testMethodName ");
            strSql.Append(" FROM tb_Compress ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }

        //获取完成试验列表，求平均
        public DataSet GetFinishSumList1(string selColAver, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select");
            strSql.Append(selColAver);
            strSql.Append(" FROM tb_Compress ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }


        //获取完成试验列表1
        public DataSet GetFinishList1(string selCol, string strWhere, int ab)
        {
            StringBuilder strSql = new StringBuilder();
            switch (ab)
            {
                case 1:
                    strSql.Append("select isEffective as [试验无效], testSampleNo as [试样编号],[a] as [a(mm)],[b] as [b(mm)]");
                    break;
                case 2:
                    strSql.Append("select isEffective as [试验无效], testSampleNo as [试样编号],[d] as [d(mm)]");
                    break;
            }

            if (!string.IsNullOrEmpty(selCol))//Fm,Rm,ReH,ReL,E,A,Z,SS as [S],FORMAT(testDate,'YYYY-MM-DD') as [试验日期] 
            {
                strSql.Append("," + selCol);
            }
            strSql.Append("  FROM tb_Compress ");// ,FORMAT(testDate,'YYYY-MM-DD') as [试验日期]
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
                    strSql.Append("select  testSampleNo as [试样编号],[a] as [a(mm)],[b] as [b(mm)]");
                    break;
                case 2:
                    strSql.Append("select  testSampleNo as [试样编号],[d] as [d(mm)]");
                    break;
            }

            if (!string.IsNullOrEmpty(selCol))//Fm,Rm,ReH,ReL,E,A,Z,SS as [S],FORMAT(testDate,'YYYY-MM-DD') as [试验日期] 
            {
                strSql.Append("," + selCol);
            }
            strSql.Append("  FROM tb_Compress ");// ,FORMAT(testDate,'YYYY-MM-DD') as [试验日期]
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

        /// <summary>
        /// 获得Finish数据列表
        /// </summary>
        public DataSet GetListFinish(string strWhere, double maxValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select testSampleNo as [试样编号],");
            int dotvalue = Dotvalue(maxValue);
            if (maxValue < 1000.0)
            {
                switch (dotvalue)
                {
                    case 2:
                        strSql.Append(" FORMAT([Fmc],'0.00') as [Fmc(N)],");
                        strSql.Append(" FORMAT([Fpc],'0.00') as [Fpc(N)],");
                        strSql.Append(" FORMAT([Ftc],'0.00') as [Ftc(N)],");
                        strSql.Append(" FORMAT([FeHc],'0.00') as [FeHc(N)],");
                        strSql.Append(" FORMAT([FeLc],'0.00') as [FeLc(N)],");
                        break;
                    case 3:
                        strSql.Append(" FORMAT([Fmc],'0.000') as [Fmc(N)],");
                        strSql.Append(" FORMAT([Fpc],'0.000') as [Fpc(N)],");
                        strSql.Append(" FORMAT([Ftc],'0.000') as [Ftc(N)],");
                        strSql.Append(" FORMAT([FeHc],'0.000') as [FeHc(N)],");
                        strSql.Append(" FORMAT([FeLc],'0.000') as [FeLc(N)],");
                        break;
                    case 4:
                        strSql.Append(" FORMAT([Fmc],'0.0000') as [Fmc(N)],");
                        strSql.Append(" FORMAT([Fpc],'0.0000') as [Fpc(N)],");
                        strSql.Append(" FORMAT([Ftc],'0.0000') as [Ftc(N)],");
                        strSql.Append(" FORMAT([FeHc],'0.0000') as [FeHc(N)],");
                        strSql.Append(" FORMAT([FeLc],'0.0000') as [FeLc(N)],");
                        break;
                    case 5:
                        strSql.Append(" FORMAT([Fmc],'0.00000') as [Fmc(N)],");
                        strSql.Append(" FORMAT([Fpc],'0.00000') as [Fpc(N)],");
                        strSql.Append(" FORMAT([Ftc],'0.00000') as [Ftc(N)],");
                        strSql.Append(" FORMAT([FeHc],'0.00000') as [FeHc(N)],");
                        strSql.Append(" FORMAT([FeLc],'0.00000') as [FeLc(N)],");
                        break;
                    default:
                        strSql.Append(" FORMAT([Fmc],'0.00') as [Fmc(N)],");
                        strSql.Append(" FORMAT([Fpc],'0.00') as [Fpc(N)],");
                        strSql.Append(" FORMAT([Ftc],'0.00') as [Ftc(N)],");
                        strSql.Append(" FORMAT([FeHc],'0.00') as [FeHc(N)],");
                        strSql.Append(" FORMAT([FeLc],'0.00') as [FeLc(N)],");
                        break;
                }

            }
            if (maxValue >= 1000.0)
            {
                switch (dotvalue)
                {
                    case 2:
                        strSql.Append(" FORMAT([Fmc]/1000.0,'0.00') as [Fmc(N)],");
                        strSql.Append(" FORMAT([Fpc]/1000.0,'0.00') as [Fpc(N)],");
                        strSql.Append(" FORMAT([Ftc]/1000.0,'0.00') as [Ftc(N)],");
                        strSql.Append(" FORMAT([FeHc]/1000.0,'0.00') as [FeHc(N)],");
                        strSql.Append(" FORMAT([FeLc]/1000.0,'0.00') as [FeLc(N)],");
                        break;
                    case 3:
                        strSql.Append(" FORMAT([Fmc]/1000.0,'0.000') as [Fmc(N)],");
                        strSql.Append(" FORMAT([Fpc]/1000.0,'0.000') as [Fpc(N)],");
                        strSql.Append(" FORMAT([Ftc]/1000.0,'0.000') as [Ftc(N)],");
                        strSql.Append(" FORMAT([FeHc]/1000.0,'0.000') as [FeHc(N)],");
                        strSql.Append(" FORMAT([FeLc]/1000.0,'0.000') as [FeLc(N)],");
                        break;
                    case 4:
                        strSql.Append(" FORMAT([Fmc]/1000.0,'0.0000') as [Fmc(N)],");
                        strSql.Append(" FORMAT([Fpc]/1000.0,'0.0000') as [Fpc(N)],");
                        strSql.Append(" FORMAT([Ftc]/1000.0,'0.0000') as [Ftc(N)],");
                        strSql.Append(" FORMAT([FeHc]/1000.0,'0.0000') as [FeHc(N)],");
                        strSql.Append(" FORMAT([FeLc]/1000.0,'0.0000') as [FeLc(N)],");
                        break;
                    case 5:
                        strSql.Append(" FORMAT([Fmc]/1000.0,'0.00000') as [Fmc(N)],");
                        strSql.Append(" FORMAT([Fpc]/1000.0,'0.00000') as [Fpc(N)],");
                        strSql.Append(" FORMAT([Ftc]/1000.0,'0.00000') as [Ftc(N)],");
                        strSql.Append(" FORMAT([FeHc]/1000.0,'0.00000') as [FeHc(N)],");
                        strSql.Append(" FORMAT([FeLc]/1000.0,'0.00000') as [FeLc(N)],");
                        break;
                    default:
                        strSql.Append(" FORMAT([Fmc]/1000.0,'0.00') as [Fmc(N)],");
                        strSql.Append(" FORMAT([Fpc]/1000.0,'0.00') as [Fpc(N)],");
                        strSql.Append(" FORMAT([Ftc]/1000.0,'0.00') as [Ftc(N)],");
                        strSql.Append(" FORMAT([FeHc]/1000.0,'0.00') as [FeHc(N)],");
                        strSql.Append(" FORMAT([FeLc]/1000.0,'0.00') as [FeLc(N)],");
                        break;
                }
            }

            strSql.Append("Rmc,Rpc,ReHc,ReLc,Ec  FROM tb_Compress ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by testSampleNo ");
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
            parameters[0].Value = "tb_Compress";
            parameters[1].Value = "testSampleNo";
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

