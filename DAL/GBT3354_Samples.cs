using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
    /// <summary>
    /// 数据访问类:GBT3354_Samples
    /// </summary>
    public partial class GBT3354_Samples
    {
        public GBT3354_Samples()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_GBT3354_Samples");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
            parameters[0].Value = ID;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }

        public DataSet GetNotOverlapList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct testNo,testMethodName,w,h,d0,Do ");
            strSql.Append(" FROM tb_GBT3354_Samples ");
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
            strSql.Append(" FROM tb_GBT3354_Samples ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }

        public DataSet GetMaxFm(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select MAX([Pmax]) as [Pmax] ");//
            strSql.Append(" FROM tb_GBT3354_Samples ");
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
            strSql.Append(" FROM tb_GBT3354_Samples ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }

        //获取完成试验列表1
        public DataSet GetFinishListReport(string selCol, string strWhere, int ab)
        {
            StringBuilder strSql = new StringBuilder();
            switch (ab)
            {
                case 1:
                    strSql.Append("select testSampleNo as [试样编号],[w] as [a(mm)],[h] as [b(mm)],[lL] as [lL(mm)]");
                    break;
                case 2:
                    strSql.Append("select testSampleNo as [试样编号],[d0] as [d(mm)],[lL] as [lL(mm)]");
                    break;
                case 3:
                    strSql.Append("select testSampleNo as [试样编号],[w] as [w(mm)],[Do] as [D0(mm)],[lL] as [lL(mm)]");
                    break;
                default:
                    strSql.Append("select testSampleNo as [试样编号],[w] as [w(mm)],[h] as [h(mm)],[lL] as [lL(mm)]");
                    break;
            }

            if (!string.IsNullOrEmpty(selCol))//Fm,Rm,ReH,ReL,E,A,Z,SS as [S],FORMAT(testDate,'YYYY-MM-DD') as [试验日期] 
            {
                strSql.Append("," + selCol);
            }
            strSql.Append("  FROM tb_GBT3354_Samples ");// ,FORMAT(testDate,'YYYY-MM-DD') as [试验日期]
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by testSampleNo ");
            return DbHelperOleDb.Query(strSql.ToString());
        }

        public DataSet GetFinishList(string strWhere, double maxValue)
        {
            StringBuilder strSql = new StringBuilder();//εp as [Ep],εt as [Et],εr as [Er],E,m,mE,Rt,Rr,Aee as Ae,Agg as [Ag],Att as [At],Aggtt as [Agt],Awnwn as [Awn],Lm as [△Lm],Lf as [△Lf],SS as [S],
            strSql.Append("select testSampleNo as [试样编号],[w] as [w(mm)],[h] as [h(mm)],");//[d0] as [d(mm)],

            int dotvalue = Dotvalue(maxValue);
            if (maxValue < 1000.0)
            {
                switch (dotvalue)
                {
                    case 2:
                        strSql.Append(" Format([Pmax],'0.00') as [Pmax(N)],");
                        break;
                    case 3:
                        strSql.Append(" Format([Pmax],'0.000') as [Pmax(N)],");
                        break;
                    case 4:
                        strSql.Append(" Format([Pmax],'0.0000') as [Pmax(N)],");
                        break;
                    case 5:
                        strSql.Append(" Format([Pmax],'0.00000') as [Pmax(N)],");
                        break;
                    default:
                        strSql.Append(" Format([Pmax],'0.00') as [Pmax(N)],");
                        break;
                }

            }
            if (maxValue >= 1000.0)
            {
                switch (dotvalue)
                {
                    case 2:
                        strSql.Append(" Format([Pmax]/1000.0,'0.00') as [Pmax(kN)],");
                        break;
                    case 3:
                        strSql.Append(" Format([Pmax]/1000.0,'0.000') as [Pmax(kN)],");
                        break;
                    case 4:
                        strSql.Append(" Format([Pmax]/1000.0,'0.0000') as [Pmax(kN)],");
                        break;
                    case 5:
                        strSql.Append(" Format([Pmax]/1000.0,'0.00000') as [Pmax(kN)],");
                        break;
                    default:
                        strSql.Append(" Format([Pmax]/1000.0,'0.00') as [Pmax(kN)],");
                        break;
                }
            }
            strSql.Append("[σt] as [σt(MPa)],[Et] as [Et(MPa)],[μ12] as [μ12(MPa)],[ε1t] as [ε1t(mm/mm)],failuremode as [失效模式],testDate as [试验日期]  FROM tb_GBT3354_Samples ");
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
        public DataSet GetFinishList1(string selCol, string strWhere, int ab)
        {
            StringBuilder strSql = new StringBuilder();
            switch (ab)
            {
                case 1:
                    strSql.Append("select isEffective as [试验无效], testSampleNo as [试样编号],failuremode as [失效模式],[w] as [w(mm)],[h] as [h(mm)],[lL] as [lL(mm)]");
                    break;
                case 2:
                    strSql.Append("select isEffective as [试验无效], testSampleNo as [试样编号],failuremode as [失效模式],[d0] as [d(mm)],[lL] as [lL(mm)]");
                    break;
                case 3:
                    strSql.Append("select isEffective as [试验无效], testSampleNo as [试样编号],failuremode as [失效模式],[w] as [w(mm)],[Do] as [D0(mm)],[lL] as [lL(mm)]");
                    break;
                default:
                    strSql.Append("select isEffective as [试验无效], testSampleNo as [试样编号],failuremode as [失效模式],[w] as [w(mm)],[h] as [h(mm)],[lL] as [lL(mm)]");
                    break;
            }

            if (!string.IsNullOrEmpty(selCol))//Fm,Rm,ReH,ReL,E,A,Z,SS as [S],FORMAT(testDate,'YYYY-MM-DD') as [试验日期] 
            {
                strSql.Append("," + selCol);
            }
            strSql.Append("  FROM tb_GBT3354_Samples ");// ,FORMAT(testDate,'YYYY-MM-DD') as [试验日期]
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
        public bool Add(HR_Test.Model.GBT3354_Samples model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_GBT3354_Samples(");
            strSql.Append("testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffSpec,getSample,strengthPlate,adhesive,sampleState,temperature,humidity,testStandard,testMethod,mathineType,tester,assessor,testDate,testCondition,controlmode,sampleShape,w,h,d0,Do,S0,lL,lT,εz1,failuremode,Pmax,σt,Et,μ12,ε1t,isFinish,isUseExtensometer1,isUseExtensometer2,isEffective,sign,εz2)");
            strSql.Append(" values (");
            strSql.Append("@testMethodName,@testNo,@testSampleNo,@reportNo,@sendCompany,@stuffSpec,@getSample,@strengthPlate,@adhesive,@sampleState,@temperature,@humidity,@testStandard,@testMethod,@mathineType,@tester,@assessor,@testDate,@testCondition,@controlmode,@sampleShape,@w,@h,@d0,@Do,@S0,@lL,@lT,@εz1,@failuremode,@Pmax,@σt,@Et,@μ12,@ε1t,@isFinish,@isUseExtensometer1,@isUseExtensometer2,@isEffective,@sign,@εz2)");
            OleDbParameter[] parameters = {
					new OleDbParameter("@testMethodName", OleDbType.VarChar,100),
					new OleDbParameter("@testNo", OleDbType.VarChar,100),
					new OleDbParameter("@testSampleNo", OleDbType.VarChar,100),
					new OleDbParameter("@reportNo", OleDbType.VarChar,100),
					new OleDbParameter("@sendCompany", OleDbType.VarChar,100),
					new OleDbParameter("@stuffSpec", OleDbType.VarChar,255),
					new OleDbParameter("@getSample", OleDbType.VarChar,100),
					new OleDbParameter("@strengthPlate", OleDbType.VarChar,100),
					new OleDbParameter("@adhesive", OleDbType.VarChar,100),
					new OleDbParameter("@sampleState", OleDbType.VarChar,100),
					new OleDbParameter("@temperature", OleDbType.Double),
					new OleDbParameter("@humidity", OleDbType.Double),
					new OleDbParameter("@testStandard", OleDbType.VarChar,100),
					new OleDbParameter("@testMethod", OleDbType.VarChar,100),
					new OleDbParameter("@mathineType", OleDbType.VarChar,100),
					new OleDbParameter("@tester", OleDbType.VarChar,100),
					new OleDbParameter("@assessor", OleDbType.VarChar,100),
					new OleDbParameter("@testDate", OleDbType.Date),
					new OleDbParameter("@testCondition", OleDbType.VarChar,100),
					new OleDbParameter("@controlmode", OleDbType.VarChar,255),
					new OleDbParameter("@sampleShape", OleDbType.VarChar,255),
					new OleDbParameter("@w", OleDbType.Double),
					new OleDbParameter("@h", OleDbType.Double),
					new OleDbParameter("@d0", OleDbType.Double),
					new OleDbParameter("@Do", OleDbType.Double),
					new OleDbParameter("@S0", OleDbType.Double),
					new OleDbParameter("@lL", OleDbType.Double),
					new OleDbParameter("@lT", OleDbType.Double),
					new OleDbParameter("@εz1", OleDbType.Double),
					new OleDbParameter("@failuremode", OleDbType.VarChar,255),
					new OleDbParameter("@Pmax", OleDbType.Double),
					new OleDbParameter("@σt", OleDbType.Double),
					new OleDbParameter("@Et", OleDbType.Double),
					new OleDbParameter("@μ12", OleDbType.Double),
					new OleDbParameter("@ε1t", OleDbType.Double),
					new OleDbParameter("@isFinish", OleDbType.Boolean,1),
					new OleDbParameter("@isUseExtensometer1", OleDbType.Boolean,1),
					new OleDbParameter("@isUseExtensometer2", OleDbType.Boolean,1),
					new OleDbParameter("@isEffective", OleDbType.Boolean,1),
					new OleDbParameter("@sign", OleDbType.VarChar,255),
                    new OleDbParameter("@εz2", OleDbType.Double)
                                          };
            parameters[0].Value = model.testMethodName;
            parameters[1].Value = model.testNo;
            parameters[2].Value = model.testSampleNo;
            parameters[3].Value = model.reportNo;
            parameters[4].Value = model.sendCompany;
            parameters[5].Value = model.stuffSpec;
            parameters[6].Value = model.getSample;
            parameters[7].Value = model.strengthPlate;
            parameters[8].Value = model.adhesive;
            parameters[9].Value = model.sampleState;
            parameters[10].Value = model.temperature;
            parameters[11].Value = model.humidity;
            parameters[12].Value = model.testStandard;
            parameters[13].Value = model.testMethod;
            parameters[14].Value = model.mathineType;
            parameters[15].Value = model.tester;
            parameters[16].Value = model.assessor;
            parameters[17].Value = model.testDate;
            parameters[18].Value = model.testCondition;
            parameters[19].Value = model.controlmode;
            parameters[20].Value = model.sampleShape;
            parameters[21].Value = model.w;
            parameters[22].Value = model.h;
            parameters[23].Value = model.d0;
            parameters[24].Value = model.Do;
            parameters[25].Value = model.S0;
            parameters[26].Value = model.lL;
            parameters[27].Value = model.lT;
            parameters[28].Value = model.εz1;
            parameters[29].Value = model.failuremode;
            parameters[30].Value = model.Pmax;
            parameters[31].Value = model.σt;
            parameters[32].Value = model.Et;
            parameters[33].Value = model.μ12;
            parameters[34].Value = model.ε1t;
            parameters[35].Value = model.isFinish;
            parameters[36].Value = model.isUseExtensometer1;
            parameters[37].Value = model.isUseExtensometer2;
            parameters[38].Value = model.isEffective;
            parameters[39].Value = model.sign;
            parameters[40].Value = model.εz2;
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
        public bool Update(HR_Test.Model.GBT3354_Samples model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_GBT3354_Samples set ");
            strSql.Append("testMethodName=@testMethodName,");
            strSql.Append("testNo=@testNo,");
            strSql.Append("testSampleNo=@testSampleNo,");
            strSql.Append("reportNo=@reportNo,");
            strSql.Append("sendCompany=@sendCompany,");
            strSql.Append("stuffSpec=@stuffSpec,");
            strSql.Append("getSample=@getSample,");
            strSql.Append("strengthPlate=@strengthPlate,");
            strSql.Append("adhesive=@adhesive,");
            strSql.Append("sampleState=@sampleState,");
            strSql.Append("temperature=@temperature,");
            strSql.Append("humidity=@humidity,");
            strSql.Append("testStandard=@testStandard,");
            strSql.Append("testMethod=@testMethod,");
            strSql.Append("mathineType=@mathineType,");
            strSql.Append("tester=@tester,");
            strSql.Append("assessor=@assessor,");
            strSql.Append("testDate=@testDate,");
            strSql.Append("testCondition=@testCondition,");
            strSql.Append("controlmode=@controlmode,");
            strSql.Append("sampleShape=@sampleShape,");
            strSql.Append("w=@w,");
            strSql.Append("h=@h,");
            strSql.Append("d0=@d0,");
            strSql.Append("Do=@Do,");
            strSql.Append("S0=@S0,");
            strSql.Append("lL=@lL,");
            strSql.Append("lT=@lT,");
            strSql.Append("εz1=@εz1,");
            strSql.Append("failuremode=@failuremode,");
            strSql.Append("Pmax=@Pmax,");
            strSql.Append("σt=@σt,");
            strSql.Append("Et=@Et,");
            strSql.Append("μ12=@μ12,");
            strSql.Append("ε1t=@ε1t,");
            strSql.Append("isFinish=@isFinish,");
            strSql.Append("isUseExtensometer1=@isUseExtensometer1,");
            strSql.Append("isUseExtensometer2=@isUseExtensometer2,");
            strSql.Append("isEffective=@isEffective,");
            strSql.Append("sign=@sign,");
            strSql.Append("εz2=@εz2");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@testMethodName", OleDbType.VarChar,100),
					new OleDbParameter("@testNo", OleDbType.VarChar,100),
					new OleDbParameter("@testSampleNo", OleDbType.VarChar,100),
					new OleDbParameter("@reportNo", OleDbType.VarChar,100),
					new OleDbParameter("@sendCompany", OleDbType.VarChar,100),
					new OleDbParameter("@stuffSpec", OleDbType.VarChar,255),
					new OleDbParameter("@getSample", OleDbType.VarChar,100),
					new OleDbParameter("@strengthPlate", OleDbType.VarChar,100),
					new OleDbParameter("@adhesive", OleDbType.VarChar,100),
					new OleDbParameter("@sampleState", OleDbType.VarChar,100),
					new OleDbParameter("@temperature", OleDbType.Double),
					new OleDbParameter("@humidity", OleDbType.Double),
					new OleDbParameter("@testStandard", OleDbType.VarChar,100),
					new OleDbParameter("@testMethod", OleDbType.VarChar,100),
					new OleDbParameter("@mathineType", OleDbType.VarChar,100),
					new OleDbParameter("@tester", OleDbType.VarChar,100),
					new OleDbParameter("@assessor", OleDbType.VarChar,100),
					new OleDbParameter("@testDate", OleDbType.Date),
					new OleDbParameter("@testCondition", OleDbType.VarChar,100),
					new OleDbParameter("@controlmode", OleDbType.VarChar,255),
					new OleDbParameter("@sampleShape", OleDbType.VarChar,255),
					new OleDbParameter("@w", OleDbType.Double),
					new OleDbParameter("@h", OleDbType.Double),
					new OleDbParameter("@d0", OleDbType.Double),
					new OleDbParameter("@Do", OleDbType.Double),
					new OleDbParameter("@S0", OleDbType.Double),
					new OleDbParameter("@lL", OleDbType.Double),
					new OleDbParameter("@lT", OleDbType.Double),
					new OleDbParameter("@εz1", OleDbType.Double),
					new OleDbParameter("@failuremode", OleDbType.VarChar,255),
					new OleDbParameter("@Pmax", OleDbType.Double),
					new OleDbParameter("@σt", OleDbType.Double),
					new OleDbParameter("@Et", OleDbType.Double),
					new OleDbParameter("@μ12", OleDbType.Double),
					new OleDbParameter("@ε1t", OleDbType.Double),
					new OleDbParameter("@isFinish", OleDbType.Boolean,1),
					new OleDbParameter("@isUseExtensometer1", OleDbType.Boolean,1),
					new OleDbParameter("@isUseExtensometer2", OleDbType.Boolean,1),
					new OleDbParameter("@isEffective", OleDbType.Boolean,1),
					new OleDbParameter("@sign", OleDbType.VarChar,255),
                    new OleDbParameter("@εz2", OleDbType.Double),
					new OleDbParameter("@ID", OleDbType.Integer,4)
                                          };
            parameters[0].Value = model.testMethodName;
            parameters[1].Value = model.testNo;
            parameters[2].Value = model.testSampleNo;
            parameters[3].Value = model.reportNo;
            parameters[4].Value = model.sendCompany;
            parameters[5].Value = model.stuffSpec;
            parameters[6].Value = model.getSample;
            parameters[7].Value = model.strengthPlate;
            parameters[8].Value = model.adhesive;
            parameters[9].Value = model.sampleState;
            parameters[10].Value = model.temperature;
            parameters[11].Value = model.humidity;
            parameters[12].Value = model.testStandard;
            parameters[13].Value = model.testMethod;
            parameters[14].Value = model.mathineType;
            parameters[15].Value = model.tester;
            parameters[16].Value = model.assessor;
            parameters[17].Value = model.testDate;
            parameters[18].Value = model.testCondition;
            parameters[19].Value = model.controlmode;
            parameters[20].Value = model.sampleShape;
            parameters[21].Value = model.w;
            parameters[22].Value = model.h;
            parameters[23].Value = model.d0;
            parameters[24].Value = model.Do;
            parameters[25].Value = model.S0;
            parameters[26].Value = model.lL;
            parameters[27].Value = model.lT;
            parameters[28].Value = model.εz1;
            parameters[29].Value = model.failuremode;
            parameters[30].Value = model.Pmax;
            parameters[31].Value = model.σt;
            parameters[32].Value = model.Et;
            parameters[33].Value = model.μ12;
            parameters[34].Value = model.ε1t;
            parameters[35].Value = model.isFinish;
            parameters[36].Value = model.isUseExtensometer1;
            parameters[37].Value = model.isUseExtensometer2;
            parameters[38].Value = model.isEffective;
            parameters[39].Value = model.sign;
            parameters[40].Value = model.εz2;
            parameters[41].Value = model.ID;

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
            strSql.Append("delete from tb_GBT3354_Samples ");
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

        public bool Delete(string  methodName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_GBT3354_Samples ");
            strSql.Append(" where methodName=@methodName");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar)
			};
            parameters[0].Value = methodName;

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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_GBT3354_Samples ");
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
        public HR_Test.Model.GBT3354_Samples GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffSpec,getSample,strengthPlate,adhesive,sampleState,temperature,humidity,testStandard,testMethod,mathineType,tester,assessor,testDate,testCondition,controlmode,sampleShape,w,h,d0,Do,S0,lL,lT,εz1,failuremode,Pmax,σt,Et,μ12,ε1t,isFinish,isUseExtensometer1,isUseExtensometer2,isEffective,sign,εz2 from tb_GBT3354_Samples ");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
            parameters[0].Value = ID;

            HR_Test.Model.GBT3354_Samples model = new HR_Test.Model.GBT3354_Samples();
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
        public HR_Test.Model.GBT3354_Samples GetModel(string testSampleNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffSpec,getSample,strengthPlate,adhesive,sampleState,temperature,humidity,testStandard,testMethod,mathineType,tester,assessor,testDate,testCondition,controlmode,sampleShape,w,h,d0,Do,S0,lL,lT,εz1,failuremode,Pmax,σt,Et,μ12,ε1t,isFinish,isUseExtensometer1,isUseExtensometer2,isEffective,sign,εz2 from tb_GBT3354_Samples ");
            strSql.Append(" where testSampleNo=@testSampleNo");
            OleDbParameter[] parameters = {
					new OleDbParameter("@testSampleNo", OleDbType.VarChar)
			};
            parameters[0].Value = testSampleNo;

            HR_Test.Model.GBT3354_Samples model = new HR_Test.Model.GBT3354_Samples();
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
        public HR_Test.Model.GBT3354_Samples DataRowToModel(DataRow row)
        {
            HR_Test.Model.GBT3354_Samples model = new HR_Test.Model.GBT3354_Samples();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["testMethodName"] != null)
                {
                    model.testMethodName = row["testMethodName"].ToString();
                }
                if (row["testNo"] != null)
                {
                    model.testNo = row["testNo"].ToString();
                }
                if (row["testSampleNo"] != null)
                {
                    model.testSampleNo = row["testSampleNo"].ToString();
                }
                if (row["reportNo"] != null)
                {
                    model.reportNo = row["reportNo"].ToString();
                }
                if (row["sendCompany"] != null)
                {
                    model.sendCompany = row["sendCompany"].ToString();
                }
                if (row["stuffSpec"] != null)
                {
                    model.stuffSpec = row["stuffSpec"].ToString();
                }
                if (row["getSample"] != null)
                {
                    model.getSample = row["getSample"].ToString();
                }
                if (row["strengthPlate"] != null)
                {
                    model.strengthPlate = row["strengthPlate"].ToString();
                }
                if (row["adhesive"] != null)
                {
                    model.adhesive = row["adhesive"].ToString();
                }
                if (row["sampleState"] != null)
                {
                    model.sampleState = row["sampleState"].ToString();
                }
                if (row["temperature"] != null)
                {
                    model.temperature = double.Parse(row["temperature"].ToString());
                }

                //model.temperature=row["temperature"].ToString();
                //model.humidity=row["humidity"].ToString();
                if (row["humidity"] != null)
                {
                    model.humidity = double.Parse(row["humidity"].ToString());
                }
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
                if (row["tester"] != null)
                {
                    model.tester = row["tester"].ToString();
                }
                if (row["assessor"] != null)
                {
                    model.assessor = row["assessor"].ToString();
                }
                if (row["testDate"] != null && row["testDate"].ToString() != "")
                {
                    model.testDate = DateTime.Parse(row["testDate"].ToString());
                }
                if (row["testCondition"] != null)
                {
                    model.testCondition = row["testCondition"].ToString();
                }
                if (row["controlmode"] != null)
                {
                    model.controlmode = row["controlmode"].ToString();
                }
                if (row["sampleShape"] != null)
                {
                    model.sampleShape = row["sampleShape"].ToString();
                }
                //model.w=row["w"].ToString();
                if (row["w"] != null)
                {
                    model.w =double.Parse( row["w"].ToString());
                }
                //model.h=row["h"].ToString();
                if (row["h"] != null)
                {
                    model.h = double.Parse(row["h"].ToString());
                }
                //model.d0=row["d0"].ToString();
                if (row["d0"] != null)
                {
                    model.d0 = double.Parse(row["d0"].ToString());
                }
                //model.Do=row["Do"].ToString();
                if (row["Do"] != null)
                {
                    model.Do = double.Parse(row["Do"].ToString());
                }
                //model.S0=row["S0"].ToString();
                if (row["S0"] != null)
                {
                    model.S0 = double.Parse(row["S0"].ToString());
                }
                //model.lL=row["lL"].ToString();
                if (row["lL"] != null)
                {
                    model.lL = double.Parse(row["lL"].ToString());
                }
                //model.lT=row["lT"].ToString();
                if (row["lT"] != null)
                {
                    model.lT = double.Parse(row["lT"].ToString());
                }
                if (row["εz1"] != null)
                {
                    model.εz1 = double.Parse(row["εz1"].ToString());
                }
                if (row["εz2"] != null)
                {
                    model.εz2 = double.Parse(row["εz2"].ToString());
                }
                if (row["failuremode"] != null)
                {
                    model.failuremode = row["failuremode"].ToString();
                }
                //model.Pmax=row["Pmax"].ToString();
                if (row["Pmax"] != null)
                {
                    model.Pmax = double.Parse(row["Pmax"].ToString());
                }
                //model.σt=row["σt"].ToString();
                if (row["σt"] != null)
                {
                    model.σt = double.Parse(row["σt"].ToString());
                }
                //model.Et=row["Et"].ToString();
                if (row["Et"] != null)
                {
                    model.Et = double.Parse(row["Et"].ToString());
                }
                //model.μ12=row["μ12"].ToString();
                if (row["μ12"] != null)
                {
                    model.μ12 = double.Parse(row["μ12"].ToString());
                }
                //model.ε1t=row["ε1t"].ToString();
                if (row["ε1t"] != null)
                {
                    model.ε1t = double.Parse(row["ε1t"].ToString());
                }
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
                if (row["isUseExtensometer1"] != null && row["isUseExtensometer1"].ToString() != "")
                {
                    if ((row["isUseExtensometer1"].ToString() == "1") || (row["isUseExtensometer1"].ToString().ToLower() == "true"))
                    {
                        model.isUseExtensometer1 = true;
                    }
                    else
                    {
                        model.isUseExtensometer1 = false;
                    }
                }
                if (row["isUseExtensometer2"] != null && row["isUseExtensometer2"].ToString() != "")
                {
                    if ((row["isUseExtensometer2"].ToString() == "1") || (row["isUseExtensometer2"].ToString().ToLower() == "true"))
                    {
                        model.isUseExtensometer2 = true;
                    }
                    else
                    {
                        model.isUseExtensometer2 = false;
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
                if (row["sign"] != null)
                {
                    model.sign = row["sign"].ToString();
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
            strSql.Append("select ID,testMethodName,testNo,testSampleNo,reportNo,sendCompany,stuffSpec,getSample,strengthPlate,adhesive,sampleState,temperature,humidity,testStandard,testMethod,mathineType,tester,assessor,testDate,testCondition,controlmode,sampleShape,w,h,d0,Do,S0,lL,lT,εz1,failuremode,Pmax,σt,Et,μ12,ε1t,isFinish,isUseExtensometer1,isUseExtensometer2,isEffective,sign,εz2 ");
            strSql.Append(" FROM tb_GBT3354_Samples ");
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
            strSql.Append("select count(1) FROM tb_GBT3354_Samples ");
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
            strSql.Append(")AS Row, T.*  from tb_GBT3354_Samples T ");
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
            parameters[0].Value = "tb_GBT3354_Samples";
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

