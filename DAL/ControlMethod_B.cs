using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
    /// <summary>
    /// 数据访问类:ControlMethod_B
    /// </summary>
    public partial class ControlMethod_B
    {
        public ControlMethod_B()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_ControlMethod_B");
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
        public bool Add(HR_Test.Model.ControlMethod_B model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_ControlMethod_B(");
            strSql.Append("methodName,xmlPath,selResultID,isProLoad,proLoadType,proLoadValue,proLoadControlType,proLoadSpeed,isLxqf,controlType1,controlType2,controlType3,controlType4,controlType5,controlType6,controlType7,controlType8,controlType9,controlType10,controlType11,controlType12,circleNum,stopValue,isTakeDownExten,extenChannel,extenValue,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,testType,Ds,Da,R,Ls,Le,l_l,m,n,a,εpb,εrb,sign,condition,controlmode)");
            strSql.Append(" values (");
            strSql.Append("@methodName,@xmlPath,@selResultID,@isProLoad,@proLoadType,@proLoadValue,@proLoadControlType,@proLoadSpeed,@isLxqf,@controlType1,@controlType2,@controlType3,@controlType4,@controlType5,@controlType6,@controlType7,@controlType8,@controlType9,@controlType10,@controlType11,@controlType12,@circleNum,@stopValue,@isTakeDownExten,@extenChannel,@extenValue,@sendCompany,@stuffCardNo,@stuffSpec,@stuffType,@hotStatus,@temperature,@humidity,@testStandard,@testMethod,@mathineType,@testCondition,@sampleCharacter,@getSample,@tester,@assessor,@testType,@Ds,@Da,@R,@Ls,@Le,@l_l,@m,@n,@a,@εpb,@εrb,@sign,@condition,@controlmode)");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,100),
					new OleDbParameter("@xmlPath", OleDbType.VarChar,50),
					new OleDbParameter("@selResultID", OleDbType.Integer,4),
					new OleDbParameter("@isProLoad", OleDbType.Boolean,1),
					new OleDbParameter("@proLoadType", OleDbType.Integer,4),
					new OleDbParameter("@proLoadValue", OleDbType.Double),
					new OleDbParameter("@proLoadControlType", OleDbType.Integer,4),
					new OleDbParameter("@proLoadSpeed", OleDbType.Double),
					new OleDbParameter("@isLxqf", OleDbType.Integer,4),
					new OleDbParameter("@controlType1", OleDbType.VarChar,50),
					new OleDbParameter("@controlType2", OleDbType.VarChar,50),
					new OleDbParameter("@controlType3", OleDbType.VarChar,50),
					new OleDbParameter("@controlType4", OleDbType.VarChar,50),
					new OleDbParameter("@controlType5", OleDbType.VarChar,50),
					new OleDbParameter("@controlType6", OleDbType.VarChar,50),
					new OleDbParameter("@controlType7", OleDbType.VarChar,50),
					new OleDbParameter("@controlType8", OleDbType.VarChar,50),
					new OleDbParameter("@controlType9", OleDbType.VarChar,50),
					new OleDbParameter("@controlType10", OleDbType.VarChar,50),
					new OleDbParameter("@controlType11", OleDbType.VarChar,50),
					new OleDbParameter("@controlType12", OleDbType.VarChar,50),
					new OleDbParameter("@circleNum", OleDbType.Integer,4),
					new OleDbParameter("@stopValue", OleDbType.Double),
					new OleDbParameter("@isTakeDownExten", OleDbType.Boolean,1),
					new OleDbParameter("@extenChannel", OleDbType.Integer,4),
					new OleDbParameter("@extenValue", OleDbType.Double),
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
					new OleDbParameter("@testType", OleDbType.VarChar,255),
					new OleDbParameter("@Ds", OleDbType.Double),
					new OleDbParameter("@Da", OleDbType.Double),
					new OleDbParameter("@R", OleDbType.Double),
					new OleDbParameter("@Ls", OleDbType.Double),
					new OleDbParameter("@Le", OleDbType.Double),
					new OleDbParameter("@l_l", OleDbType.Double),
					new OleDbParameter("@m", OleDbType.Double),
					new OleDbParameter("@n", OleDbType.Double),
					new OleDbParameter("@a", OleDbType.Double),
					new OleDbParameter("@εpb", OleDbType.Double),
					new OleDbParameter("@εrb", OleDbType.Double),
					new OleDbParameter("@sign", OleDbType.VarChar,255),
					new OleDbParameter("@condition", OleDbType.VarChar,255),
					new OleDbParameter("@controlmode", OleDbType.VarChar,255)};
            parameters[0].Value = model.methodName;
            parameters[1].Value = model.xmlPath;
            parameters[2].Value = model.selResultID;
            parameters[3].Value = model.isProLoad;
            parameters[4].Value = model.proLoadType;
            parameters[5].Value = model.proLoadValue;
            parameters[6].Value = model.proLoadControlType;
            parameters[7].Value = model.proLoadSpeed;
            parameters[8].Value = model.isLxqf;
            parameters[9].Value = model.controlType1;
            parameters[10].Value = model.controlType2;
            parameters[11].Value = model.controlType3;
            parameters[12].Value = model.controlType4;
            parameters[13].Value = model.controlType5;
            parameters[14].Value = model.controlType6;
            parameters[15].Value = model.controlType7;
            parameters[16].Value = model.controlType8;
            parameters[17].Value = model.controlType9;
            parameters[18].Value = model.controlType10;
            parameters[19].Value = model.controlType11;
            parameters[20].Value = model.controlType12;
            parameters[21].Value = model.circleNum;
            parameters[22].Value = model.stopValue;
            parameters[23].Value = model.isTakeDownExten;
            parameters[24].Value = model.extenChannel;
            parameters[25].Value = model.extenValue;
            parameters[26].Value = model.sendCompany;
            parameters[27].Value = model.stuffCardNo;
            parameters[28].Value = model.stuffSpec;
            parameters[29].Value = model.stuffType;
            parameters[30].Value = model.hotStatus;
            parameters[31].Value = model.temperature;
            parameters[32].Value = model.humidity;
            parameters[33].Value = model.testStandard;
            parameters[34].Value = model.testMethod;
            parameters[35].Value = model.mathineType;
            parameters[36].Value = model.testCondition;
            parameters[37].Value = model.sampleCharacter;
            parameters[38].Value = model.getSample;
            parameters[39].Value = model.tester;
            parameters[40].Value = model.assessor;
            parameters[41].Value = model.testType;
            parameters[42].Value = model.Ds;
            parameters[43].Value = model.Da;
            parameters[44].Value = model.R;
            parameters[45].Value = model.Ls;
            parameters[46].Value = model.Le;
            parameters[47].Value = model.l_l;
            parameters[48].Value = model.m;
            parameters[49].Value = model.n;
            parameters[50].Value = model.a;
            parameters[51].Value = model.εpb;
            parameters[52].Value = model.εrb;
            parameters[53].Value = model.sign;
            parameters[54].Value = model.condition;
            parameters[55].Value = model.controlmode;

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
        public bool Update(HR_Test.Model.ControlMethod_B model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_ControlMethod_B set ");
            strSql.Append("methodName=@methodName,");
            strSql.Append("xmlPath=@xmlPath,");
            strSql.Append("selResultID=@selResultID,");
            strSql.Append("isProLoad=@isProLoad,");
            strSql.Append("proLoadType=@proLoadType,");
            strSql.Append("proLoadValue=@proLoadValue,");
            strSql.Append("proLoadControlType=@proLoadControlType,");
            strSql.Append("proLoadSpeed=@proLoadSpeed,");
            strSql.Append("isLxqf=@isLxqf,");
            strSql.Append("controlType1=@controlType1,");
            strSql.Append("controlType2=@controlType2,");
            strSql.Append("controlType3=@controlType3,");
            strSql.Append("controlType4=@controlType4,");
            strSql.Append("controlType5=@controlType5,");
            strSql.Append("controlType6=@controlType6,");
            strSql.Append("controlType7=@controlType7,");
            strSql.Append("controlType8=@controlType8,");
            strSql.Append("controlType9=@controlType9,");
            strSql.Append("controlType10=@controlType10,");
            strSql.Append("controlType11=@controlType11,");
            strSql.Append("controlType12=@controlType12,");
            strSql.Append("circleNum=@circleNum,");
            strSql.Append("stopValue=@stopValue,");
            strSql.Append("isTakeDownExten=@isTakeDownExten,");
            strSql.Append("extenChannel=@extenChannel,");
            strSql.Append("extenValue=@extenValue,");
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
            strSql.Append("testType=@testType,");
            strSql.Append("Ds=@Ds,");
            strSql.Append("Da=@Da,");
            strSql.Append("R=@R,");
            strSql.Append("Ls=@Ls,");
            strSql.Append("Le=@Le,");
            strSql.Append("l_l=@l_l,");
            strSql.Append("m=@m,");
            strSql.Append("n=@n,");
            strSql.Append("a=@a,");
            strSql.Append("εpb=@εpb,");
            strSql.Append("εrb=@εrb,");
            strSql.Append("sign=@sign,");
            strSql.Append("condition=@condition,");
            strSql.Append("controlmode=@controlmode");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,100),
					new OleDbParameter("@xmlPath", OleDbType.VarChar,50),
					new OleDbParameter("@selResultID", OleDbType.Integer,4),
					new OleDbParameter("@isProLoad", OleDbType.Boolean,1),
					new OleDbParameter("@proLoadType", OleDbType.Integer,4),
					new OleDbParameter("@proLoadValue", OleDbType.Double),
					new OleDbParameter("@proLoadControlType", OleDbType.Integer,4),
					new OleDbParameter("@proLoadSpeed", OleDbType.Double),
					new OleDbParameter("@isLxqf", OleDbType.Integer,4),
					new OleDbParameter("@controlType1", OleDbType.VarChar,50),
					new OleDbParameter("@controlType2", OleDbType.VarChar,50),
					new OleDbParameter("@controlType3", OleDbType.VarChar,50),
					new OleDbParameter("@controlType4", OleDbType.VarChar,50),
					new OleDbParameter("@controlType5", OleDbType.VarChar,50),
					new OleDbParameter("@controlType6", OleDbType.VarChar,50),
					new OleDbParameter("@controlType7", OleDbType.VarChar,50),
					new OleDbParameter("@controlType8", OleDbType.VarChar,50),
					new OleDbParameter("@controlType9", OleDbType.VarChar,50),
					new OleDbParameter("@controlType10", OleDbType.VarChar,50),
					new OleDbParameter("@controlType11", OleDbType.VarChar,50),
					new OleDbParameter("@controlType12", OleDbType.VarChar,50),
					new OleDbParameter("@circleNum", OleDbType.Integer,4),
					new OleDbParameter("@stopValue", OleDbType.Double),
					new OleDbParameter("@isTakeDownExten", OleDbType.Boolean,1),
					new OleDbParameter("@extenChannel", OleDbType.Integer,4),
					new OleDbParameter("@extenValue", OleDbType.Double),
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
					new OleDbParameter("@testType", OleDbType.VarChar,255),
					new OleDbParameter("@Ds", OleDbType.Double),
					new OleDbParameter("@Da", OleDbType.Double),
					new OleDbParameter("@R", OleDbType.Double),
					new OleDbParameter("@Ls", OleDbType.Double),
					new OleDbParameter("@Le", OleDbType.Double),
					new OleDbParameter("@l_l", OleDbType.Double),
					new OleDbParameter("@m", OleDbType.Double),
					new OleDbParameter("@n", OleDbType.Double),
					new OleDbParameter("@a", OleDbType.Double),
					new OleDbParameter("@εpb", OleDbType.Double),
					new OleDbParameter("@εrb", OleDbType.Double),
					new OleDbParameter("@sign", OleDbType.VarChar,255),
					new OleDbParameter("@condition", OleDbType.VarChar,255),
					new OleDbParameter("@controlmode", OleDbType.VarChar,255),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
            parameters[0].Value = model.methodName;
            parameters[1].Value = model.xmlPath;
            parameters[2].Value = model.selResultID;
            parameters[3].Value = model.isProLoad;
            parameters[4].Value = model.proLoadType;
            parameters[5].Value = model.proLoadValue;
            parameters[6].Value = model.proLoadControlType;
            parameters[7].Value = model.proLoadSpeed;
            parameters[8].Value = model.isLxqf;
            parameters[9].Value = model.controlType1;
            parameters[10].Value = model.controlType2;
            parameters[11].Value = model.controlType3;
            parameters[12].Value = model.controlType4;
            parameters[13].Value = model.controlType5;
            parameters[14].Value = model.controlType6;
            parameters[15].Value = model.controlType7;
            parameters[16].Value = model.controlType8;
            parameters[17].Value = model.controlType9;
            parameters[18].Value = model.controlType10;
            parameters[19].Value = model.controlType11;
            parameters[20].Value = model.controlType12;
            parameters[21].Value = model.circleNum;
            parameters[22].Value = model.stopValue;
            parameters[23].Value = model.isTakeDownExten;
            parameters[24].Value = model.extenChannel;
            parameters[25].Value = model.extenValue;
            parameters[26].Value = model.sendCompany;
            parameters[27].Value = model.stuffCardNo;
            parameters[28].Value = model.stuffSpec;
            parameters[29].Value = model.stuffType;
            parameters[30].Value = model.hotStatus;
            parameters[31].Value = model.temperature;
            parameters[32].Value = model.humidity;
            parameters[33].Value = model.testStandard;
            parameters[34].Value = model.testMethod;
            parameters[35].Value = model.mathineType;
            parameters[36].Value = model.testCondition;
            parameters[37].Value = model.sampleCharacter;
            parameters[38].Value = model.getSample;
            parameters[39].Value = model.tester;
            parameters[40].Value = model.assessor;
            parameters[41].Value = model.testType;
            parameters[42].Value = model.Ds;
            parameters[43].Value = model.Da;
            parameters[44].Value = model.R;
            parameters[45].Value = model.Ls;
            parameters[46].Value = model.Le;
            parameters[47].Value = model.l_l;
            parameters[48].Value = model.m;
            parameters[49].Value = model.n;
            parameters[50].Value = model.a;
            parameters[51].Value = model.εpb;
            parameters[52].Value = model.εrb;
            parameters[53].Value = model.sign;
            parameters[54].Value = model.condition;
            parameters[55].Value = model.controlmode;
            parameters[56].Value = model.ID;

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
            strSql.Append("delete from tb_ControlMethod_B ");
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
        public bool Delete(string methodName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_ControlMethod_B ");
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
            strSql.Append("delete from tb_ControlMethod_B ");
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
        public HR_Test.Model.ControlMethod_B GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,methodName,xmlPath,selResultID,isProLoad,proLoadType,proLoadValue,proLoadControlType,proLoadSpeed,isLxqf,controlType1,controlType2,controlType3,controlType4,controlType5,controlType6,controlType7,controlType8,controlType9,controlType10,controlType11,controlType12,circleNum,stopValue,isTakeDownExten,extenChannel,extenValue,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,testType,Ds,Da,R,Ls,Le,l_l,m,n,a,εpb,εrb,sign,condition,controlmode from tb_ControlMethod_B ");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
            parameters[0].Value = ID;

            HR_Test.Model.ControlMethod_B model = new HR_Test.Model.ControlMethod_B();
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
        public HR_Test.Model.ControlMethod_B GetModel(string methodName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,methodName,xmlPath,selResultID,isProLoad,proLoadType,proLoadValue,proLoadControlType,proLoadSpeed,isLxqf,controlType1,controlType2,controlType3,controlType4,controlType5,controlType6,controlType7,controlType8,controlType9,controlType10,controlType11,controlType12,circleNum,stopValue,isTakeDownExten,extenChannel,extenValue,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,testType,Ds,Da,R,Ls,Le,l_l,m,n,a,εpb,εrb,sign,condition,controlmode from tb_ControlMethod_B ");
            strSql.Append(" where methodName=@methodName");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar)
			};
            parameters[0].Value = methodName;

            //HR_Test.Model.ControlMethod_B model = new HR_Test.Model.ControlMethod_B();
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
        public HR_Test.Model.ControlMethod_B DataRowToModel(DataRow row)
        {
            HR_Test.Model.ControlMethod_B model = new HR_Test.Model.ControlMethod_B();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["methodName"] != null)
                {
                    model.methodName = row["methodName"].ToString();
                }
                if (row["xmlPath"] != null)
                {
                    model.xmlPath = row["xmlPath"].ToString();
                }
                if (row["selResultID"] != null && row["selResultID"].ToString() != "")
                {
                    model.selResultID = int.Parse(row["selResultID"].ToString());
                }
                if (row["isProLoad"] != null && row["isProLoad"].ToString() != "")
                {
                    if ((row["isProLoad"].ToString() == "1") || (row["isProLoad"].ToString().ToLower() == "true"))
                    {
                        model.isProLoad = true;
                    }
                    else
                    {
                        model.isProLoad = false;
                    }
                }
                if (row["proLoadType"] != null && row["proLoadType"].ToString() != "")
                {
                    model.proLoadType = int.Parse(row["proLoadType"].ToString());
                }
                model.proLoadValue=Convert.ToDouble( row["proLoadValue"].ToString());
                if (row["proLoadControlType"] != null && row["proLoadControlType"].ToString() != "")
                {
                    model.proLoadControlType = int.Parse(row["proLoadControlType"].ToString());
                }
                model.proLoadSpeed=Convert.ToDouble( row["proLoadSpeed"].ToString());
                if (row["isLxqf"] != null && row["isLxqf"].ToString() != "")
                {
                    model.isLxqf = int.Parse(row["isLxqf"].ToString());
                }
                if (row["controlType1"] != null)
                {
                    model.controlType1 = row["controlType1"].ToString();
                }
                if (row["controlType2"] != null)
                {
                    model.controlType2 = row["controlType2"].ToString();
                }
                if (row["controlType3"] != null)
                {
                    model.controlType3 = row["controlType3"].ToString();
                }
                if (row["controlType4"] != null)
                {
                    model.controlType4 = row["controlType4"].ToString();
                }
                if (row["controlType5"] != null)
                {
                    model.controlType5 = row["controlType5"].ToString();
                }
                if (row["controlType6"] != null)
                {
                    model.controlType6 = row["controlType6"].ToString();
                }
                if (row["controlType7"] != null)
                {
                    model.controlType7 = row["controlType7"].ToString();
                }
                if (row["controlType8"] != null)
                {
                    model.controlType8 = row["controlType8"].ToString();
                }
                if (row["controlType9"] != null)
                {
                    model.controlType9 = row["controlType9"].ToString();
                }
                if (row["controlType10"] != null)
                {
                    model.controlType10 = row["controlType10"].ToString();
                }
                if (row["controlType11"] != null)
                {
                    model.controlType11 = row["controlType11"].ToString();
                }
                if (row["controlType12"] != null)
                {
                    model.controlType12 = row["controlType12"].ToString();
                }
                if (row["circleNum"] != null && row["circleNum"].ToString() != "")
                {
                    model.circleNum = int.Parse(row["circleNum"].ToString());
                }
                 model.stopValue=Convert.ToDouble( row["stopValue"].ToString());
                if (row["isTakeDownExten"] != null && row["isTakeDownExten"].ToString() != "")
                {
                    if ((row["isTakeDownExten"].ToString() == "1") || (row["isTakeDownExten"].ToString().ToLower() == "true"))
                    {
                        model.isTakeDownExten = true;
                    }
                    else
                    {
                        model.isTakeDownExten = false;
                    }
                }
                if (row["extenChannel"] != null && row["extenChannel"].ToString() != "")
                {
                    model.extenChannel = int.Parse(row["extenChannel"].ToString());
                }
                model.extenValue=Convert.ToDouble(row["extenValue"].ToString());
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
                model.temperature=Convert.ToDouble(row["temperature"].ToString());
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
                if (row["testType"] != null)
                {
                    model.testType = row["testType"].ToString();
                }
                model.Ds =Convert.ToDouble( row["Ds"].ToString());
                model.Da =Convert.ToDouble( row["Da"].ToString());
                model.R =Convert.ToDouble( row["R"].ToString());
                model.Ls =Convert.ToDouble( row["Ls"].ToString());
                model.Le =Convert.ToDouble( row["Le"].ToString());
                model.l_l = Convert.ToDouble(row["l_l"].ToString());
                model.m = Convert.ToDouble(row["m"].ToString());
                model.n = Convert.ToDouble(row["n"].ToString());
                model.a = Convert.ToDouble(row["a"].ToString());
                model.εpb =Convert.ToDouble( row["εpb"].ToString());
                model.εrb =Convert.ToDouble( row["εrb"].ToString());
                if (row["sign"] != null)
                {
                    model.sign = row["sign"].ToString();
                }
                if (row["condition"] != null)
                {
                    model.condition = row["condition"].ToString();
                }
                if (row["controlmode"] != null)
                {
                    model.controlmode = row["controlmode"].ToString();
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
            strSql.Append("select ID,methodName,xmlPath,selResultID,isProLoad,proLoadType,proLoadValue,proLoadControlType,proLoadSpeed,isLxqf,controlType1,controlType2,controlType3,controlType4,controlType5,controlType6,controlType7,controlType8,controlType9,controlType10,controlType11,controlType12,circleNum,stopValue,isTakeDownExten,extenChannel,extenValue,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,testType,Ds,Da,R,Ls,Le,l_l,m,n,a,εpb,εrb,sign,condition,controlmode ");
            strSql.Append(" FROM tb_ControlMethod_B ");
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
            strSql.Append("select count(1) FROM tb_ControlMethod_B ");
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
            strSql.Append(")AS Row, T.*  from tb_ControlMethod_B T ");
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
            parameters[0].Value = "tb_ControlMethod_B";
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

