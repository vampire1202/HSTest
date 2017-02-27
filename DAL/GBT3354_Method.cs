using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
    /// <summary>
    /// 数据访问类:GBT3354_Method
    /// </summary>
    public partial class GBT3354_Method
    {
        public GBT3354_Method()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_GBT3354_Method");
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
        public bool Add(HR_Test.Model.GBT3354_Method model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_GBT3354_Method(");
            strSql.Append("methodName,xmlPath,selResultID,isProLoad,proLoadType,proLoadValue,proLoadControlType,proLoadSpeed,isLxqf,controlType1,controlType2,controlType3,controlType4,controlType5,controlType6,controlType7,controlType8,controlType9,controlType10,controlType11,controlType12,circleNum,stopValue,isTakeDownExten,extenChannel,extenValue,sendCompany,getSample,strengthPlate,adhesive,sampleState,temperature,humidity,testStandard,testMethod,mathineType,testCondition,tester,assessor,testDate,sampleShape,w,h,d0,Do,S0,lL,lT,εz,sign)");
            strSql.Append(" values (");
            strSql.Append("@methodName,@xmlPath,@selResultID,@isProLoad,@proLoadType,@proLoadValue,@proLoadControlType,@proLoadSpeed,@isLxqf,@controlType1,@controlType2,@controlType3,@controlType4,@controlType5,@controlType6,@controlType7,@controlType8,@controlType9,@controlType10,@controlType11,@controlType12,@circleNum,@stopValue,@isTakeDownExten,@extenChannel,@extenValue,@sendCompany,@getSample,@strengthPlate,@adhesive,@sampleState,@temperature,@humidity,@testStandard,@testMethod,@mathineType,@testCondition,@tester,@assessor,@testDate,@sampleShape,@w,@h,@d0,@Do,@S0,@lL,@lT,@εz,@sign)");
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
					new OleDbParameter("@getSample", OleDbType.VarChar,100),
					new OleDbParameter("@strengthPlate", OleDbType.VarChar,100),
					new OleDbParameter("@adhesive", OleDbType.VarChar,100),
					new OleDbParameter("@sampleState", OleDbType.VarChar,100),
					new OleDbParameter("@temperature", OleDbType.Double),
					new OleDbParameter("@humidity", OleDbType.Double),
					new OleDbParameter("@testStandard", OleDbType.VarChar,100),
					new OleDbParameter("@testMethod", OleDbType.VarChar,100),
					new OleDbParameter("@mathineType", OleDbType.VarChar,100),
					new OleDbParameter("@testCondition", OleDbType.VarChar,100),
					new OleDbParameter("@tester", OleDbType.VarChar,100),
					new OleDbParameter("@assessor", OleDbType.VarChar,100),
					new OleDbParameter("@testDate", OleDbType.Date),
					new OleDbParameter("@sampleShape", OleDbType.VarChar,255),
					new OleDbParameter("@w", OleDbType.Double),
					new OleDbParameter("@h", OleDbType.Double),
					new OleDbParameter("@d0", OleDbType.Double),
					new OleDbParameter("@Do", OleDbType.Double),
					new OleDbParameter("@S0", OleDbType.Double),
					new OleDbParameter("@lL", OleDbType.Double),
					new OleDbParameter("@lT", OleDbType.Double),
					new OleDbParameter("@εz", OleDbType.VarChar,100),
					new OleDbParameter("@sign", OleDbType.VarChar,0)};
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
            parameters[27].Value = model.getSample;
            parameters[28].Value = model.strengthPlate;
            parameters[29].Value = model.adhesive;
            parameters[30].Value = model.sampleState;
            parameters[31].Value = model.temperature;
            parameters[32].Value = model.humidity;
            parameters[33].Value = model.testStandard;
            parameters[34].Value = model.testMethod;
            parameters[35].Value = model.mathineType;
            parameters[36].Value = model.testCondition;
            parameters[37].Value = model.tester;
            parameters[38].Value = model.assessor;
            parameters[39].Value = model.testDate;
            parameters[40].Value = model.sampleShape;
            parameters[41].Value = model.w;
            parameters[42].Value = model.h;
            parameters[43].Value = model.d0;
            parameters[44].Value = model.Do;
            parameters[45].Value = model.S0;
            parameters[46].Value = model.lL;
            parameters[47].Value = model.lT;
            parameters[48].Value = model.εz;
            parameters[49].Value = model.sign;

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
        public bool Update(HR_Test.Model.GBT3354_Method model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_GBT3354_Method set ");
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
            strSql.Append("getSample=@getSample,");
            strSql.Append("strengthPlate=@strengthPlate,");
            strSql.Append("adhesive=@adhesive,");
            strSql.Append("sampleState=@sampleState,");
            strSql.Append("temperature=@temperature,");
            strSql.Append("humidity=@humidity,");
            strSql.Append("testStandard=@testStandard,");
            strSql.Append("testMethod=@testMethod,");
            strSql.Append("mathineType=@mathineType,");
            strSql.Append("testCondition=@testCondition,");
            strSql.Append("tester=@tester,");
            strSql.Append("assessor=@assessor,");
            strSql.Append("testDate=@testDate,");
            strSql.Append("sampleShape=@sampleShape,");
            strSql.Append("w=@w,");
            strSql.Append("h=@h,");
            strSql.Append("d0=@d0,");
            strSql.Append("Do=@Do,");
            strSql.Append("S0=@S0,");
            strSql.Append("lL=@lL,");
            strSql.Append("lT=@lT,");
            strSql.Append("εz=@εz,");
            strSql.Append("sign=@sign");
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
					new OleDbParameter("@getSample", OleDbType.VarChar,100),
					new OleDbParameter("@strengthPlate", OleDbType.VarChar,100),
					new OleDbParameter("@adhesive", OleDbType.VarChar,100),
					new OleDbParameter("@sampleState", OleDbType.VarChar,100),
					new OleDbParameter("@temperature", OleDbType.Double),
					new OleDbParameter("@humidity", OleDbType.Double),
					new OleDbParameter("@testStandard", OleDbType.VarChar,100),
					new OleDbParameter("@testMethod", OleDbType.VarChar,100),
					new OleDbParameter("@mathineType", OleDbType.VarChar,100),
					new OleDbParameter("@testCondition", OleDbType.VarChar,100),
					new OleDbParameter("@tester", OleDbType.VarChar,100),
					new OleDbParameter("@assessor", OleDbType.VarChar,100),
					new OleDbParameter("@testDate", OleDbType.Date),
					new OleDbParameter("@sampleShape", OleDbType.VarChar,255),
					new OleDbParameter("@w", OleDbType.Double),
					new OleDbParameter("@h", OleDbType.Double),
					new OleDbParameter("@d0", OleDbType.Double),
					new OleDbParameter("@Do", OleDbType.Double),
					new OleDbParameter("@S0", OleDbType.Double),
					new OleDbParameter("@lL", OleDbType.Double),
					new OleDbParameter("@lT", OleDbType.Double),
					new OleDbParameter("@εz", OleDbType.VarChar,100),
					new OleDbParameter("@sign", OleDbType.VarChar,0),
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
            parameters[27].Value = model.getSample;
            parameters[28].Value = model.strengthPlate;
            parameters[29].Value = model.adhesive;
            parameters[30].Value = model.sampleState;
            parameters[31].Value = model.temperature;
            parameters[32].Value = model.humidity;
            parameters[33].Value = model.testStandard;
            parameters[34].Value = model.testMethod;
            parameters[35].Value = model.mathineType;
            parameters[36].Value = model.testCondition;
            parameters[37].Value = model.tester;
            parameters[38].Value = model.assessor;
            parameters[39].Value = model.testDate;
            parameters[40].Value = model.sampleShape;
            parameters[41].Value = model.w;
            parameters[42].Value = model.h;
            parameters[43].Value = model.d0;
            parameters[44].Value = model.Do;
            parameters[45].Value = model.S0;
            parameters[46].Value = model.lL;
            parameters[47].Value = model.lT;
            parameters[48].Value = model.εz;
            parameters[49].Value = model.sign;
            parameters[50].Value = model.ID;

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
            strSql.Append("delete from tb_GBT3354_Method ");
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_GBT3354_Method ");
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
        public HR_Test.Model.GBT3354_Method GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,methodName,xmlPath,selResultID,isProLoad,proLoadType,proLoadValue,proLoadControlType,proLoadSpeed,isLxqf,controlType1,controlType2,controlType3,controlType4,controlType5,controlType6,controlType7,controlType8,controlType9,controlType10,controlType11,controlType12,circleNum,stopValue,isTakeDownExten,extenChannel,extenValue,sendCompany,getSample,strengthPlate,adhesive,sampleState,temperature,humidity,testStandard,testMethod,mathineType,testCondition,tester,assessor,testDate,sampleShape,w,h,d0,Do,S0,lL,lT,εz,sign from tb_GBT3354_Method ");
            strSql.Append(" where ID=@ID");
            OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
            parameters[0].Value = ID;

            HR_Test.Model.GBT3354_Method model = new HR_Test.Model.GBT3354_Method();
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

        public HR_Test.Model.GBT3354_Method GetModel(string methodName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,methodName,xmlPath,selResultID,isProLoad,proLoadType,proLoadValue,proLoadControlType,proLoadSpeed,isLxqf,controlType1,controlType2,controlType3,controlType4,controlType5,controlType6,controlType7,controlType8,controlType9,controlType10,controlType11,controlType12,circleNum,stopValue,isTakeDownExten,extenChannel,extenValue,sendCompany,getSample,strengthPlate,adhesive,sampleState,temperature,humidity,testStandard,testMethod,mathineType,testCondition,tester,assessor,testDate,sampleShape,w,h,d0,Do,S0,lL,lT,εz,sign from tb_GBT3354_Method ");
            strSql.Append(" where methodName=@methodName");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar)
			};
            parameters[0].Value = methodName;

            HR_Test.Model.GBT3354_Method model = new HR_Test.Model.GBT3354_Method();
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
        public HR_Test.Model.GBT3354_Method DataRowToModel(DataRow row)
        {
            HR_Test.Model.GBT3354_Method model = new HR_Test.Model.GBT3354_Method();
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
                if (row["proLoadValue"] !=null && row["proLoadValue"].ToString()!="")
                {
                    model.proLoadValue = double.Parse( row["proLoadValue"].ToString());
                } 
                if (row["proLoadControlType"] != null && row["proLoadControlType"].ToString() != "")
                {
                    model.proLoadControlType = int.Parse(row["proLoadControlType"].ToString());
                }
                if (row["proLoadSpeed"] != null && row["proLoadSpeed"].ToString() != "")
                {
                    model.proLoadSpeed = double.Parse(row["proLoadSpeed"].ToString());
                } 
                //model.proLoadSpeed=row["proLoadSpeed"].ToString();
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
                //model.stopValue=row["stopValue"].ToString();
                if (row["stopValue"] != null && row["stopValue"].ToString() != "")
                {
                    model.stopValue = double.Parse(row["stopValue"].ToString());
                } 
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
                //model.extenValue=row["extenValue"].ToString();
                if (row["extenValue"] != null && row["extenValue"].ToString() != "")
                {
                    model.extenValue = double.Parse(row["extenValue"].ToString());
                } 
                if (row["sendCompany"] != null)
                {
                    model.sendCompany = row["sendCompany"].ToString();
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
                //model.temperature=row["temperature"].ToString();
                if (row["temperature"] != null && row["temperature"].ToString() != "")
                {
                    model.temperature = double.Parse(row["temperature"].ToString());
                } 
                //model.humidity=row["humidity"].ToString();
                if (row["humidity"] != null && row["humidity"].ToString() != "")
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
                if (row["testCondition"] != null)
                {
                    model.testCondition = row["testCondition"].ToString();
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
                if (row["sampleShape"] != null)
                {
                    model.sampleShape = row["sampleShape"].ToString();
                }
                //model.w=row["w"].ToString();
                if (row["w"] != null && row["w"].ToString() != "")
                {
                    model.w = double.Parse(row["w"].ToString());
                } 
                //model.h=row["h"].ToString();
                if (row["h"] != null && row["h"].ToString() != "")
                {
                    model.h = double.Parse(row["h"].ToString());
                } 
                //model.d0=row["d0"].ToString();
                if (row["d0"] != null && row["d0"].ToString() != "")
                {
                    model.d0 = double.Parse(row["d0"].ToString());
                } 
                //model.Do=row["Do"].ToString();
                if (row["Do"] != null && row["Do"].ToString() != "")
                {
                    model.Do = double.Parse(row["Do"].ToString());
                } 
                //model.S0=row["S0"].ToString();
                if (row["S0"] != null && row["S0"].ToString() != "")
                {
                    model.S0 = double.Parse(row["S0"].ToString());
                } 
                //model.lL=row["lL"].ToString();
                if (row["lL"] != null && row["lL"].ToString() != "")
                {
                    model.lL = double.Parse(row["lL"].ToString());
                } 
                //model.lT=row["lT"].ToString();
                if (row["lT"] != null && row["lT"].ToString() != "")
                {
                    model.lT = double.Parse(row["lT"].ToString());
                } 
                //model.εz=row["εz"].ToString();
                if (row["εz"] != null && row["εz"].ToString() != "")
                {
                    model.εz = row["εz"].ToString();
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
            strSql.Append("select ID,methodName,xmlPath,selResultID,isProLoad,proLoadType,proLoadValue,proLoadControlType,proLoadSpeed,isLxqf,controlType1,controlType2,controlType3,controlType4,controlType5,controlType6,controlType7,controlType8,controlType9,controlType10,controlType11,controlType12,circleNum,stopValue,isTakeDownExten,extenChannel,extenValue,sendCompany,getSample,strengthPlate,adhesive,sampleState,temperature,humidity,testStandard,testMethod,mathineType,testCondition,tester,assessor,testDate,sampleShape,w,h,d0,Do,S0,lL,lT,εz,sign ");
            strSql.Append(" FROM tb_GBT3354_Method ");
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
            strSql.Append("select count(1) FROM tb_GBT3354_Method ");
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
            strSql.Append(")AS Row, T.*  from tb_GBT3354_Method T ");
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
            parameters[0].Value = "tb_GBT3354_Method";
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

