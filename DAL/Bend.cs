using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
    /// <summary>
    /// 数据访问类:Bend
    /// </summary>
    public partial class Bend
    {
        public Bend()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_Bend");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
            parameters[0].Value = ID;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(HR_Test.Model.Bend model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_Bend(");
            strSql.Append("testMethodID,testNo,testSampleNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,sampleType,testType,d,b,h,L,Ds,Da,R,t,Ls,Le,l_l,lt,m,n,a,f_bb,f_n,f_n1,f_rb,y,Fo,Fpb,Frb,Fbb,Fn,Fn1,Z,S,W,I,Eb,σpb,σrb,σbb,U,εpb,εrb,isFinish,isConformity,testDate,condition,controlmode,isUseExtensometer,isEffective)");
            strSql.Append(" values (");
            strSql.Append("@testMethodID,@testNo,@testSampleNo,@sendCompany,@stuffCardNo,@stuffSpec,@stuffType,@hotStatus,@temperature,@humidity,@testStandard,@testMethod,@mathineType,@testCondition,@sampleCharacter,@getSample,@tester,@assessor,@sign,@sampleType,@testType,@d,@b,@h,@L,@Ds,@Da,@R,@t,@Ls,@Le,@l_l,@lt,@m,@n,@a,@f_bb,@f_n,@f_n1,@f_rb,@y,@Fo,@Fpb,@Frb,@Fbb,@Fn,@Fn1,@Z,@S,@W,@I,@Eb,@σpb,@σrb,@σbb,@U,@εpb,@εrb,@isFinish,@isConformity,@testDate,@condition,@controlmode,@isUseExtensometer,@isEffective)");
            OleDbParameter[] parameters = {
					new OleDbParameter("@testMethodID", OleDbType.Integer,4),
					new OleDbParameter("@testNo", OleDbType.VarChar,100),
					new OleDbParameter("@testSampleNo", OleDbType.VarChar,100),
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
					new OleDbParameter("@sampleType", OleDbType.VarChar,255),
					new OleDbParameter("@testType", OleDbType.VarChar,255),
					new OleDbParameter("@d", OleDbType.Double),
					new OleDbParameter("@b", OleDbType.Double),
					new OleDbParameter("@h", OleDbType.Double),
					new OleDbParameter("@L", OleDbType.Double),
					new OleDbParameter("@Ds", OleDbType.Double),
					new OleDbParameter("@Da", OleDbType.Double),
					new OleDbParameter("@R", OleDbType.Double),
					new OleDbParameter("@t", OleDbType.Double),
					new OleDbParameter("@Ls", OleDbType.Double),
					new OleDbParameter("@Le", OleDbType.Double),
					new OleDbParameter("@l_l", OleDbType.Double),
					new OleDbParameter("@lt", OleDbType.Double),
					new OleDbParameter("@m", OleDbType.Integer,4),
					new OleDbParameter("@n", OleDbType.Double),
					new OleDbParameter("@a", OleDbType.Double),
					new OleDbParameter("@f_bb", OleDbType.Double),
					new OleDbParameter("@f_n", OleDbType.Double),
					new OleDbParameter("@f_n1", OleDbType.Double),
					new OleDbParameter("@f_rb", OleDbType.Double),
					new OleDbParameter("@y", OleDbType.Double),
					new OleDbParameter("@Fo", OleDbType.Double),
					new OleDbParameter("@Fpb", OleDbType.Double),
					new OleDbParameter("@Frb", OleDbType.Double),
					new OleDbParameter("@Fbb", OleDbType.Double),
					new OleDbParameter("@Fn", OleDbType.Double),
					new OleDbParameter("@Fn1", OleDbType.Double),
					new OleDbParameter("@Z", OleDbType.Double),
					new OleDbParameter("@S", OleDbType.Double),
					new OleDbParameter("@W", OleDbType.Double),
					new OleDbParameter("@I", OleDbType.Double),
					new OleDbParameter("@Eb", OleDbType.Double),
					new OleDbParameter("@σpb", OleDbType.Double),
					new OleDbParameter("@σrb", OleDbType.Double),
					new OleDbParameter("@σbb", OleDbType.Double),
					new OleDbParameter("@U", OleDbType.Double),
					new OleDbParameter("@εpb", OleDbType.Double),
					new OleDbParameter("@εrb", OleDbType.Double),
					new OleDbParameter("@isFinish", OleDbType.Boolean,1),
					new OleDbParameter("@isConformity", OleDbType.Boolean,1),
					new OleDbParameter("@testDate", OleDbType.Date),
					new OleDbParameter("@condition", OleDbType.VarChar,255),
					new OleDbParameter("@controlmode", OleDbType.VarChar,255),
                    new OleDbParameter("@isUseExtensometer", OleDbType.Boolean,1),        
                    new OleDbParameter("@isEffective", OleDbType.Boolean,1),};
            parameters[0].Value = model.testMethodID;
            parameters[1].Value = model.testNo;
            parameters[2].Value = model.testSampleNo;
            parameters[3].Value = model.sendCompany;
            parameters[4].Value = model.stuffCardNo;
            parameters[5].Value = model.stuffSpec;
            parameters[6].Value = model.stuffType;
            parameters[7].Value = model.hotStatus;
            parameters[8].Value = model.temperature;
            parameters[9].Value = model.humidity;
            parameters[10].Value = model.testStandard;
            parameters[11].Value = model.testMethod;
            parameters[12].Value = model.mathineType;
            parameters[13].Value = model.testCondition;
            parameters[14].Value = model.sampleCharacter;
            parameters[15].Value = model.getSample;
            parameters[16].Value = model.tester;
            parameters[17].Value = model.assessor;
            parameters[18].Value = model.sign;
            parameters[19].Value = model.sampleType;
            parameters[20].Value = model.testType;
            parameters[21].Value = model.d;
            parameters[22].Value = model.b;
            parameters[23].Value = model.h;
            parameters[24].Value = model.L;
            parameters[25].Value = model.Ds;
            parameters[26].Value = model.Da;
            parameters[27].Value = model.R;
            parameters[28].Value = model.t;
            parameters[29].Value = model.Ls;
            parameters[30].Value = model.Le;
            parameters[31].Value = model.l_l;
            parameters[32].Value = model.lt;
            parameters[33].Value = model.m;
            parameters[34].Value = model.n;
            parameters[35].Value = model.a;
            parameters[36].Value = model.f_bb;
            parameters[37].Value = model.f_n;
            parameters[38].Value = model.f_n1;
            parameters[39].Value = model.f_rb;
            parameters[40].Value = model.y;
            parameters[41].Value = model.Fo;
            parameters[42].Value = model.Fpb;
            parameters[43].Value = model.Frb;
            parameters[44].Value = model.Fbb;
            parameters[45].Value = model.Fn;
            parameters[46].Value = model.Fn1;
            parameters[47].Value = model.Z;
            parameters[48].Value = model.S;
            parameters[49].Value = model.W;
            parameters[50].Value = model.I;
            parameters[51].Value = model.Eb;
            parameters[52].Value = model.σpb;
            parameters[53].Value = model.σrb;
            parameters[54].Value = model.σbb;
            parameters[55].Value = model.U;
            parameters[56].Value = model.εpb;
            parameters[57].Value = model.εrb;
            parameters[58].Value = model.isFinish;
            parameters[59].Value = model.isConformity;
            parameters[60].Value = model.testDate;
            parameters[61].Value = model.condition;
            parameters[62].Value = model.controlmode;
            parameters[63].Value = model.isUseExtensometer;
            parameters[64].Value = model.isEffective;
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
        public bool Update(Model.Bend model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Bend set ");
            strSql.Append("testMethodID=@testMethodID,");
            strSql.Append("testNo=@testNo,");
            strSql.Append("testSampleNo=@testSampleNo,");
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
            strSql.Append("sampleType=@sampleType,");
            strSql.Append("testType=@testType,");
            strSql.Append("d=@d,");
            strSql.Append("b=@b,");
            strSql.Append("h=@h,");
            strSql.Append("L=@L,");
            strSql.Append("Ds=@Ds,");
            strSql.Append("Da=@Da,");
            strSql.Append("R=@R,");
            strSql.Append("t=@t,");
            strSql.Append("Ls=@Ls,");
            strSql.Append("Le=@Le,");
            strSql.Append("l_l=@l_l,");
            strSql.Append("lt=@lt,");
            strSql.Append("m=@m,");
            strSql.Append("n=@n,");
            strSql.Append("a=@a,");
            strSql.Append("f_bb=@f_bb,");
            strSql.Append("f_n=@f_n,");
            strSql.Append("f_n1=@f_n1,");
            strSql.Append("f_rb=@f_rb,");
            strSql.Append("y=@y,");
            strSql.Append("Fo=@Fo,");
            strSql.Append("Fpb=@Fpb,");
            strSql.Append("Frb=@Frb,");
            strSql.Append("Fbb=@Fbb,");
            strSql.Append("Fn=@Fn,");
            strSql.Append("Fn1=@Fn1,");
            strSql.Append("Z=@Z,");
            strSql.Append("S=@S,");
            strSql.Append("W=@W,");
            strSql.Append("I=@I,");
            strSql.Append("Eb=@Eb,");
            strSql.Append("σpb=@σpb,");
            strSql.Append("σrb=@σrb,");
            strSql.Append("σbb=@σbb,");
            strSql.Append("U=@U,");
            strSql.Append("εpb=@εpb,");
            strSql.Append("εrb=@εrb,");
            strSql.Append("isFinish=@isFinish,");
            strSql.Append("isConformity=@isConformity,");
            strSql.Append("testDate=@testDate,");
            strSql.Append("condition=@condition,");
            strSql.Append("controlmode=@controlmode,");
            strSql.Append("isUseExtensometer=@isUseExtensometer,");
            strSql.Append("isEffective=@isEffective");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@testMethodID", OleDbType.Integer,4),
					new OleDbParameter("@testNo", OleDbType.VarChar,100),
					new OleDbParameter("@testSampleNo", OleDbType.VarChar,100),
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
					new OleDbParameter("@sampleType", OleDbType.VarChar,255),
					new OleDbParameter("@testType", OleDbType.VarChar,255),
					new OleDbParameter("@d", OleDbType.Double),
					new OleDbParameter("@b", OleDbType.Double),
					new OleDbParameter("@h", OleDbType.Double),
					new OleDbParameter("@L", OleDbType.Double),
					new OleDbParameter("@Ds", OleDbType.Double),
					new OleDbParameter("@Da", OleDbType.Double),
					new OleDbParameter("@R", OleDbType.Double),
					new OleDbParameter("@t", OleDbType.Double),
					new OleDbParameter("@Ls", OleDbType.Double),
					new OleDbParameter("@Le", OleDbType.Double),
					new OleDbParameter("@l_l", OleDbType.Double),
					new OleDbParameter("@lt", OleDbType.Double),
					new OleDbParameter("@m", OleDbType.Integer,4),
					new OleDbParameter("@n", OleDbType.Double),
					new OleDbParameter("@a", OleDbType.Double),
					new OleDbParameter("@f_bb", OleDbType.Double),
					new OleDbParameter("@f_n", OleDbType.Double),
					new OleDbParameter("@f_n1", OleDbType.Double),
					new OleDbParameter("@f_rb", OleDbType.Double),
					new OleDbParameter("@y", OleDbType.Double),
					new OleDbParameter("@Fo", OleDbType.Double),
					new OleDbParameter("@Fpb", OleDbType.Double),
					new OleDbParameter("@Frb", OleDbType.Double),
					new OleDbParameter("@Fbb", OleDbType.Double),
					new OleDbParameter("@Fn", OleDbType.Double),
					new OleDbParameter("@Fn1", OleDbType.Double),
					new OleDbParameter("@Z", OleDbType.Double),
					new OleDbParameter("@S", OleDbType.Double),
					new OleDbParameter("@W", OleDbType.Double),
					new OleDbParameter("@I", OleDbType.Double),
					new OleDbParameter("@Eb", OleDbType.Double),
					new OleDbParameter("@σpb", OleDbType.Double),
					new OleDbParameter("@σrb", OleDbType.Double),
					new OleDbParameter("@σbb", OleDbType.Double),
					new OleDbParameter("@U", OleDbType.Double),
					new OleDbParameter("@εpb", OleDbType.Double),
					new OleDbParameter("@εrb", OleDbType.Double),
					new OleDbParameter("@isFinish", OleDbType.Boolean,1),
					new OleDbParameter("@isConformity", OleDbType.Boolean,1),
					new OleDbParameter("@testDate", OleDbType.Date),
					new OleDbParameter("@condition", OleDbType.VarChar,255),
					new OleDbParameter("@controlmode", OleDbType.VarChar,255),
					new OleDbParameter("@isUseExtensometer", OleDbType.Boolean,1),
					new OleDbParameter("@isEffective", OleDbType.Boolean,1),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
            parameters[0].Value = model.testMethodID;
            parameters[1].Value = model.testNo;
            parameters[2].Value = model.testSampleNo;
            parameters[3].Value = model.sendCompany;
            parameters[4].Value = model.stuffCardNo;
            parameters[5].Value = model.stuffSpec;
            parameters[6].Value = model.stuffType;
            parameters[7].Value = model.hotStatus;
            parameters[8].Value = model.temperature;
            parameters[9].Value = model.humidity;
            parameters[10].Value = model.testStandard;
            parameters[11].Value = model.testMethod;
            parameters[12].Value = model.mathineType;
            parameters[13].Value = model.testCondition;
            parameters[14].Value = model.sampleCharacter;
            parameters[15].Value = model.getSample;
            parameters[16].Value = model.tester;
            parameters[17].Value = model.assessor;
            parameters[18].Value = model.sign;
            parameters[19].Value = model.sampleType;
            parameters[20].Value = model.testType;
            parameters[21].Value = model.d;
            parameters[22].Value = model.b;
            parameters[23].Value = model.h;
            parameters[24].Value = model.L;
            parameters[25].Value = model.Ds;
            parameters[26].Value = model.Da;
            parameters[27].Value = model.R;
            parameters[28].Value = model.t;
            parameters[29].Value = model.Ls;
            parameters[30].Value = model.Le;
            parameters[31].Value = model.l_l;
            parameters[32].Value = model.lt;
            parameters[33].Value = model.m;
            parameters[34].Value = model.n;
            parameters[35].Value = model.a;
            parameters[36].Value = model.f_bb;
            parameters[37].Value = model.f_n;
            parameters[38].Value = model.f_n1;
            parameters[39].Value = model.f_rb;
            parameters[40].Value = model.y;
            parameters[41].Value = model.Fo;
            parameters[42].Value = model.Fpb;
            parameters[43].Value = model.Frb;
            parameters[44].Value = model.Fbb;
            parameters[45].Value = model.Fn;
            parameters[46].Value = model.Fn1;
            parameters[47].Value = model.Z;
            parameters[48].Value = model.S;
            parameters[49].Value = model.W;
            parameters[50].Value = model.I;
            parameters[51].Value = model.Eb;
            parameters[52].Value = model.σpb;
            parameters[53].Value = model.σrb;
            parameters[54].Value = model.σbb;
            parameters[55].Value = model.U;
            parameters[56].Value = model.εpb;
            parameters[57].Value = model.εrb;
            parameters[58].Value = model.isFinish;
            parameters[59].Value = model.isConformity;
            parameters[60].Value = model.testDate;
            parameters[61].Value = model.condition;
            parameters[62].Value = model.controlmode;
            parameters[63].Value = model.isUseExtensometer;
            parameters[64].Value = model.isEffective;
            parameters[65].Value = model.ID;

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

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_Bend ");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
            parameters[0].Value = ID;

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
            strSql.Append("delete from tb_Bend ");
            strSql.Append(" where testSampleNo=@testSampleNo");
            OleDbParameter[] parameters = {
					new OleDbParameter("@testSampleNo", OleDbType.VarChar)
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

        //获得不重复项的列表
        public DataSet GetNotOverlapList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select ID,testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,a0,au,b0,bu,d0,du,Do,L0,L01,Lc,Le,Lt,Lu,Lu1,S0,Su,k,Fm,Rm,ReH,ReL,Rp,Rt,Rr,εp,εt,εr,E,m,mE,A,Aee,Agg,Att,Aggtt,Awnwn,Lm,Lf,Z,Avera,SS,Avera1,isFinish,testDate ");
            strSql.Append(" select Distinct testNo,testMethod ");//
            strSql.Append(" FROM tb_Bend ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }

        public DataSet GetFbbMax(string strWhere)
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_Bend ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        public HR_Test.Model.Bend GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,testMethodID,testNo,testSampleNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,sampleType,testType,d,b,h,L,Ds,Da,R,t,Ls,Le,l_l,lt,m,n,a,f_bb,f_n,f_n1,f_rb,y,Fo,Fpb,Frb,Fbb,Fn,Fn1,Z,S,W,I,Eb,σpb,σrb,σbb,U,εpb,εrb,isFinish,isConformity,testDate,condition,controlmode,isUseExtensometer,isEffective from tb_Bend ");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
            parameters[0].Value = ID;

            //HR_Test.Model.Bend model = new HR_Test.Model.Bend();
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
        public HR_Test.Model.Bend GetModel(string testSampleNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,testMethodID,testNo,testSampleNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,sampleType,testType,d,b,h,L,Ds,Da,R,t,Ls,Le,l_l,lt,m,n,a,f_bb,f_n,f_n1,f_rb,y,Fo,Fpb,Frb,Fbb,Fn,Fn1,Z,S,W,I,Eb,σpb,σrb,σbb,U,εpb,εrb,isFinish,isConformity,testDate,condition,controlmode,isUseExtensometer,isEffective from tb_Bend ");
            strSql.Append(" where testSampleNo=@testSampleNo");
            OleDbParameter[] parameters = {
					new OleDbParameter("@testSampleNo", OleDbType.VarChar,200)
			};
            parameters[0].Value = testSampleNo;

            HR_Test.Model.Bend model = new HR_Test.Model.Bend();
            DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model = DataRowToModel(ds.Tables[0].Rows[0]);
                return model ;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public HR_Test.Model.Bend DataRowToModel(DataRow row)
        {
            HR_Test.Model.Bend model = new HR_Test.Model.Bend();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["testMethodID"] != null && row["testMethodID"].ToString() != "")
                {
                    model.testMethodID = int.Parse(row["testMethodID"].ToString());
                }
                if (row["testNo"] != null)
                {
                    model.testNo = row["testNo"].ToString();
                }
                if (row["testSampleNo"] != null)
                {
                    model.testSampleNo = row["testSampleNo"].ToString();
                }
                if (row["sendCompany"] != null)
                {
                    model.sendCompany = row["sendCompany"].ToString();
                }
                if (row["stuffCardNo"] != null)
                {
                    model.stuffCardNo = row["stuffCardNo"].ToString();
                }
                if (row["stuffSpec"] != null)
                {
                    model.stuffSpec = row["stuffSpec"].ToString();
                }
                if (row["stuffType"] != null)
                {
                    model.stuffType = row["stuffType"].ToString();
                }
                if (row["hotStatus"] != null)
                {
                    model.hotStatus = row["hotStatus"].ToString();
                }
                model.temperature=Convert.ToDouble( row["temperature"].ToString());
                model.humidity=Convert.ToDouble(row["humidity"].ToString());
                if (row["testStandard"] != null)
                {
                    model.testStandard = row["testStandard"].ToString();
                }
                if (row["testMethod"] != null)
                {
                    model.testMethod = row["testMethod"].ToString();
                }
                if (row["mathineType"] != null)
                {
                    model.mathineType = row["mathineType"].ToString();
                }
                if (row["testCondition"] != null)
                {
                    model.testCondition = row["testCondition"].ToString();
                }
                if (row["sampleCharacter"] != null)
                {
                    model.sampleCharacter = row["sampleCharacter"].ToString();
                }
                if (row["getSample"] != null)
                {
                    model.getSample = row["getSample"].ToString();
                }
                if (row["tester"] != null)
                {
                    model.tester = row["tester"].ToString();
                }
                if (row["assessor"] != null)
                {
                    model.assessor = row["assessor"].ToString();
                }
                if (row["sign"] != null)
                {
                    model.sign = row["sign"].ToString();
                }
                if (row["sampleType"] != null)
                {
                    model.sampleType = row["sampleType"].ToString();
                }
                if (row["testType"] != null)
                {
                    model.testType = row["testType"].ToString();
                }
                model.d = Convert.ToDouble(row["d"].ToString());
                model.b = Convert.ToDouble(row["b"].ToString());
                model.h = Convert.ToDouble(row["h"].ToString());
                model.L = Convert.ToDouble(row["L"].ToString());
                model.Ds = Convert.ToDouble(row["Ds"].ToString());
                model.Da = Convert.ToDouble(row["Da"].ToString());
                model.R = Convert.ToDouble(row["R"].ToString());
                model.t = Convert.ToDouble(row["t"].ToString());
                model.Ls = Convert.ToDouble(row["Ls"].ToString());
                model.Le = Convert.ToDouble(row["Le"].ToString());
                model.l_l = Convert.ToDouble(row["l_l"].ToString());
                model.lt = Convert.ToDouble(row["lt"].ToString());
                if (row["m"] != null && row["m"].ToString() != "")
                {
                    model.m = int.Parse(row["m"].ToString());
                }
                model.n = Convert.ToDouble(row["n"].ToString());
                model.a = Convert.ToDouble(row["a"].ToString());
                model.f_bb = Convert.ToDouble(row["f_bb"].ToString());
                model.f_n = Convert.ToDouble(row["f_n"].ToString());
                model.f_n1 = Convert.ToDouble(row["f_n1"].ToString());
                model.f_rb = Convert.ToDouble(row["f_rb"].ToString());
                model.y = Convert.ToDouble(row["y"].ToString());
                model.Fo = Convert.ToDouble(row["Fo"].ToString());
                model.Fpb =Convert.ToDouble( row["Fpb"].ToString());
                model.Frb =Convert.ToDouble( row["Frb"].ToString());
                model.Fbb =Convert.ToDouble( row["Fbb"].ToString());
                model.Fn =Convert.ToDouble( row["Fn"].ToString());
                model.Fn1 = Convert.ToDouble(row["Fn1"].ToString());
                model.Z = Convert.ToDouble(row["Z"].ToString());
                model.S = Convert.ToDouble(row["S"].ToString());
                model.W =Convert.ToDouble( row["W"].ToString());
                model.I = Convert.ToDouble(row["I"].ToString());
                model.Eb = Convert.ToDouble(row["Eb"].ToString());
                model.σpb = Convert.ToDouble(row["σpb"].ToString());
                model.σrb = Convert.ToDouble(row["σrb"].ToString());
                model.σbb =Convert.ToDouble( row["σbb"].ToString());
                model.U = Convert.ToDouble(row["U"].ToString());
                model.εpb = Convert.ToDouble(row["εpb"].ToString());
                model.εrb = Convert.ToDouble(row["εrb"].ToString());
                if (row["isFinish"] != null && row["isFinish"].ToString() != "")
                {
                    if ((row["isFinish"].ToString() == "1") || (row["isFinish"].ToString().ToLower() == "true"))
                    {
                        model.isFinish = true;
                    }
                    else
                    {
                        model.isFinish = false;
                    }
                }
                if (row["isConformity"] != null && row["isConformity"].ToString() != "")
                {
                    if ((row["isConformity"].ToString() == "1") || (row["isConformity"].ToString().ToLower() == "true"))
                    {
                        model.isConformity = true;
                    }
                    else
                    {
                        model.isConformity = false;
                    }
                }
                if (row["testDate"] != null && row["testDate"].ToString() != "")
                {
                    model.testDate = DateTime.Parse(row["testDate"].ToString());
                }
                if (row["condition"] != null)
                {
                    model.condition = row["condition"].ToString();
                }
                if (row["controlmode"] != null)
                {
                    model.controlmode = row["controlmode"].ToString();
                }
                if (row["isUseExtensometer"] != null && row["isUseExtensometer"].ToString() != "")
                {
                    if ((row["isUseExtensometer"].ToString() == "1") || (row["isUseExtensometer"].ToString().ToLower() == "true"))
                    {
                        model.isUseExtensometer = true;
                    }
                    else
                    {
                        model.isUseExtensometer = false;
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,testMethodID,testNo,testSampleNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,sampleType,testType,d,b,h,L,Ds,Da,R,t,Ls,Le,l_l,lt,m,n,a,f_bb,f_n,f_n1,f_rb,y,Fo,Fpb,Frb,Fbb,Fn,Fn1,Z,S,W,I,Eb,σpb,σrb,σbb,U,εpb,εrb,isFinish,isConformity,testDate,condition,controlmode,isUseExtensometer,isEffective ");
            strSql.Append(" FROM tb_Bend ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }

        //获取完成试验列表1
        public DataSet GetFinishList1(string selCol, string strWhere, string sampleType)
        {
            StringBuilder strSql = new StringBuilder();
            switch (sampleType)
            {
                case "矩形":
                    strSql.Append("select testSampleNo as [试样编号],[b] as [b(mm)],[h] as [h(mm)],[Ls] as [Ls(mm)],");
                    break;
                case "圆柱形":
                    strSql.Append("select testSampleNo as [试样编号],[d] as [d(mm)],[Ls] as [Ls(mm)],");
                    break;              
                default:
                    strSql.Append("select testSampleNo as [试样编号],[b] as [b(mm)],[h] as [h(mm)],[Ls] as [Ls(mm)],");
                    break;
            }

            if (!string.IsNullOrEmpty(selCol))//Fm,Rm,ReH,ReL,E,A,Z,SS as [S],FORMAT(testDate,'YYYY-MM-DD') as [试验日期] 
            {
                strSql.Append(selCol);
            }
            strSql.Append(" FORMAT(testDate,'YYYY-MM-DD') as [试验日期] FROM tb_TestSample ");
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetFinishListDefault(string strWhere,double maxValue)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select ID,testMethodID,testNo,testSampleNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,sampleType,testType,d,b,h,L,Ds,Da,R,t,Ls,Le,l_l,lt,m,n,a,f_bb,f_n,f_n1,f_rb,y,Fo,Fpb,Frb,Fbb,Fn,Fn1,Z,S,W,I,Eb,σpb,σrb,σbb,U,εpb,εrb,isFinish,isConformity,testDate,condition,controlmode ");
            strSql.Append("select testSampleNo as [试样编号],testType as [弯曲类型], ");
            int _dotValue = Dotvalue(maxValue);
            if (maxValue < 1000.0)
            {
                switch (_dotValue)
                {
                    case 2:
                        strSql.Append(" FORMAT([Fbb],'0.00') as [Fbb(N)],");
                        strSql.Append(" FORMAT([Fpb],'0.00') as [Fpb(N)],");
                        strSql.Append(" FORMAT([Frb],'0.00') as [Frb(N)],");
                        break;
                    case 3:
                        strSql.Append(" FORMAT([Fbb],'0.000') as [Fbb(N)],");
                        strSql.Append(" FORMAT([Fpb],'0.000') as [Fpb(N)],");
                        strSql.Append(" FORMAT([Frb],'0.000') as [Frb(N)],");
                        break;
                    case 4:
                        strSql.Append(" FORMAT([Fbb],'0.0000') as [Fbb(N)],");
                        strSql.Append(" FORMAT([Fpb],'0.0000') as [Fpb(N)],");
                        strSql.Append(" FORMAT([Frb],'0.0000') as [Frb(N)],");
                        break;
                    case 5:
                        strSql.Append(" FORMAT([Fbb],'0.00000') as [Fbb(N)],");
                        strSql.Append(" FORMAT([Fpb],'0.00000') as [Fpb(N)],");
                        strSql.Append(" FORMAT([Frb],'0.00000') as [Frb(N)],");
                        break;
                    default:
                        strSql.Append(" FORMAT([Fbb],'0.00') as [Fbb(N)],");
                        strSql.Append(" FORMAT([Fpb],'0.00') as [Fpb(N)],");
                        strSql.Append(" FORMAT([Frb],'0.00') as [Frb(N)],");
                        break;
                }
            }

            if (maxValue >= 1000.0)
            {
                switch (_dotValue)
                {
                    case 2:
                        strSql.Append(" FORMAT([Fbb]/1000.0,'0.00') as [Fbb(kN)],");
                        strSql.Append(" FORMAT([Fpb]/1000.0,'0.00') as [Fpb(kN)],");
                        strSql.Append(" FORMAT([Frb]/1000.0,'0.00') as [Frb(kN)],"); 
                        break;
                    case 3:
                        strSql.Append(" FORMAT([Fbb]/1000.0,'0.000') as [Fbb(kN)],");
                        strSql.Append(" FORMAT([Fpb]/1000.0,'0.000') as [Fpb(kN)],");
                        strSql.Append(" FORMAT([Frb]/1000.0,'0.000') as [Frb(kN)],"); 
                        break;
                    case 4:
                        strSql.Append(" FORMAT([Fbb]/1000.0,'0.0000') as [Fbb(kN)],");
                        strSql.Append(" FORMAT([Fpb]/1000.0,'0.0000') as [Fpb(kN)],");
                        strSql.Append(" FORMAT([Frb]/1000.0,'0.0000') as [Frb(kN)],"); 
                        break;
                    case 5:
                        strSql.Append(" FORMAT([Fbb]/1000.0,'0.00000') as [Fbb(kN)],");
                        strSql.Append(" FORMAT([Fpb]/1000.0,'0.00000') as [Fpb(kN)],");
                        strSql.Append(" FORMAT([Frb]/1000.0,'0.00000') as [Frb(kN)],"); 
                        break;
                    default:
                        strSql.Append(" FORMAT([Fbb]/1000.0,'0.00') as [Fbb(kN)],");
                        strSql.Append(" FORMAT([Fpb]/1000.0,'0.00') as [Fpb(kN)],");
                        strSql.Append(" FORMAT([Frb]/1000.0,'0.00') as [Frb(kN)],"); 
                        break;
                }
            }
            strSql.Append("FORMAT([Eb],'0.00') as [Eb(MPa)],FORMAT([σpb],'0.00') as [σpb(MPa)],FORMAT([σbb],'0.00') as [σbb(MPa)]  FROM tb_Bend ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetFinishList1(string selcol,string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select ID,testMethodID,testNo,testSampleNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,sampleType,testType,d,b,h,L,Ds,Da,R,t,Ls,Le,l_l,lt,m,n,a,f_bb,f_n,f_n1,f_rb,y,Fo,Fpb,Frb,Fbb,Fn,Fn1,Z,S,W,I,Eb,σpb,σrb,σbb,U,εpb,εrb,isFinish,isConformity,testDate,condition,controlmode ");
            strSql.Append("select isEffective as [试验无效], testSampleNo as [试样编号],sampleType as [试样形状],testType as [弯曲类型],L as [长度],Ls as [跨距],l_l as [力臂],"); 
            strSql.Append(selcol);
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
        public DataSet GetFinishListReport(string selcol, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select ID,testMethodID,testNo,testSampleNo,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,sign,sampleType,testType,d,b,h,L,Ds,Da,R,t,Ls,Le,l_l,lt,m,n,a,f_bb,f_n,f_n1,f_rb,y,Fo,Fpb,Frb,Fbb,Fn,Fn1,Z,S,W,I,Eb,σpb,σrb,σbb,U,εpb,εrb,isFinish,isConformity,testDate,condition,controlmode ");
            strSql.Append("select testSampleNo as [试样编号],testType as [弯曲类型],");
            strSql.Append(selcol);
            strSql.Append(" FROM tb_Bend ");
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
            strSql.Append(" FROM tb_Bend ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        } 


        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM tb_Bend ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from tb_Bend T ");
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
            parameters[0].Value = "tb_Bend";
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

