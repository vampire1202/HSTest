using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
	/// <summary>
	/// 数据访问类:GBT282892012_Method
	/// </summary>
	public partial class GBT282892012_Method
	{
		public GBT282892012_Method()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperOleDb.GetMaxID("ID", "tb_GBT282892012_Method"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tb_GBT282892012_Method");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			return DbHelperOleDb.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(HR_Test.Model.GBT282892012_Method model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_GBT282892012_Method(");
			strSql.Append("methodName,testStandard,testType,xmlPath,selResultID,isProLoad,proLoadType,proLoadValue,proLoadControlType,proLoadSpeed,isLxqf,controlType1,controlType2,controlType3,controlType4,controlType5,controlType6,controlType7,controlType8,controlType9,controlType10,controlType11,controlType12,circleNum,stopValue,isTakeDownExten,extenChannel,extenValue,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testMethod,mathineType,testCondition,sampleCharacter,getSample,condition,controlmode,tester,assessor,sign)");
			strSql.Append(" values (");
			strSql.Append("@methodName,@testStandard,@testType,@xmlPath,@selResultID,@isProLoad,@proLoadType,@proLoadValue,@proLoadControlType,@proLoadSpeed,@isLxqf,@controlType1,@controlType2,@controlType3,@controlType4,@controlType5,@controlType6,@controlType7,@controlType8,@controlType9,@controlType10,@controlType11,@controlType12,@circleNum,@stopValue,@isTakeDownExten,@extenChannel,@extenValue,@sendCompany,@stuffCardNo,@stuffSpec,@stuffType,@hotStatus,@temperature,@humidity,@testMethod,@mathineType,@testCondition,@sampleCharacter,@getSample,@condition,@controlmode,@tester,@assessor,@sign)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,100),
					new OleDbParameter("@testStandard", OleDbType.VarChar,255),
					new OleDbParameter("@testType", OleDbType.VarChar,255),
					new OleDbParameter("@xmlPath", OleDbType.VarChar,255),
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
					new OleDbParameter("@sendCompany", OleDbType.VarChar,255),
					new OleDbParameter("@stuffCardNo", OleDbType.VarChar,255),
					new OleDbParameter("@stuffSpec", OleDbType.VarChar,255),
					new OleDbParameter("@stuffType", OleDbType.VarChar,255),
					new OleDbParameter("@hotStatus", OleDbType.VarChar,255),
					new OleDbParameter("@temperature", OleDbType.Double),
					new OleDbParameter("@humidity", OleDbType.Double),
					new OleDbParameter("@testMethod", OleDbType.VarChar,100),
					new OleDbParameter("@mathineType", OleDbType.VarChar,100),
					new OleDbParameter("@testCondition", OleDbType.VarChar,100),
					new OleDbParameter("@sampleCharacter", OleDbType.VarChar,100),
					new OleDbParameter("@getSample", OleDbType.VarChar,100),
					new OleDbParameter("@condition", OleDbType.VarChar,255),
					new OleDbParameter("@controlmode", OleDbType.VarChar,255),
					new OleDbParameter("@tester", OleDbType.VarChar,100),
					new OleDbParameter("@assessor", OleDbType.VarChar,100),
					new OleDbParameter("@sign", OleDbType.VarChar,0)};
			parameters[0].Value = model.methodName;
			parameters[1].Value = model.testStandard;
			parameters[2].Value = model.testType;
			parameters[3].Value = model.xmlPath;
			parameters[4].Value = model.selResultID;
			parameters[5].Value = model.isProLoad;
			parameters[6].Value = model.proLoadType;
			parameters[7].Value = model.proLoadValue;
			parameters[8].Value = model.proLoadControlType;
			parameters[9].Value = model.proLoadSpeed;
			parameters[10].Value = model.isLxqf;
			parameters[11].Value = model.controlType1;
			parameters[12].Value = model.controlType2;
			parameters[13].Value = model.controlType3;
			parameters[14].Value = model.controlType4;
			parameters[15].Value = model.controlType5;
			parameters[16].Value = model.controlType6;
			parameters[17].Value = model.controlType7;
			parameters[18].Value = model.controlType8;
			parameters[19].Value = model.controlType9;
			parameters[20].Value = model.controlType10;
			parameters[21].Value = model.controlType11;
			parameters[22].Value = model.controlType12;
			parameters[23].Value = model.circleNum;
			parameters[24].Value = model.stopValue;
			parameters[25].Value = model.isTakeDownExten;
			parameters[26].Value = model.extenChannel;
			parameters[27].Value = model.extenValue;
			parameters[28].Value = model.sendCompany;
			parameters[29].Value = model.stuffCardNo;
			parameters[30].Value = model.stuffSpec;
			parameters[31].Value = model.stuffType;
			parameters[32].Value = model.hotStatus;
			parameters[33].Value = model.temperature;
			parameters[34].Value = model.humidity;
			parameters[35].Value = model.testMethod;
			parameters[36].Value = model.mathineType;
			parameters[37].Value = model.testCondition;
			parameters[38].Value = model.sampleCharacter;
			parameters[39].Value = model.getSample;
			parameters[40].Value = model.condition;
			parameters[41].Value = model.controlmode;
			parameters[42].Value = model.tester;
			parameters[43].Value = model.assessor;
			parameters[44].Value = model.sign;

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
		public bool Update(HR_Test.Model.GBT282892012_Method model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_GBT282892012_Method set ");
			strSql.Append("methodName=@methodName,");
			strSql.Append("testStandard=@testStandard,");
			strSql.Append("testType=@testType,");
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
			strSql.Append("testMethod=@testMethod,");
			strSql.Append("mathineType=@mathineType,");
			strSql.Append("testCondition=@testCondition,");
			strSql.Append("sampleCharacter=@sampleCharacter,");
			strSql.Append("getSample=@getSample,");
			strSql.Append("condition=@condition,");
			strSql.Append("controlmode=@controlmode,");
			strSql.Append("tester=@tester,");
			strSql.Append("assessor=@assessor,");
			strSql.Append("sign=@sign");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,100),
					new OleDbParameter("@testStandard", OleDbType.VarChar,255),
					new OleDbParameter("@testType", OleDbType.VarChar,255),
					new OleDbParameter("@xmlPath", OleDbType.VarChar,255),
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
					new OleDbParameter("@sendCompany", OleDbType.VarChar,255),
					new OleDbParameter("@stuffCardNo", OleDbType.VarChar,255),
					new OleDbParameter("@stuffSpec", OleDbType.VarChar,255),
					new OleDbParameter("@stuffType", OleDbType.VarChar,255),
					new OleDbParameter("@hotStatus", OleDbType.VarChar,255),
					new OleDbParameter("@temperature", OleDbType.Double),
					new OleDbParameter("@humidity", OleDbType.Double),
					new OleDbParameter("@testMethod", OleDbType.VarChar,100),
					new OleDbParameter("@mathineType", OleDbType.VarChar,100),
					new OleDbParameter("@testCondition", OleDbType.VarChar,100),
					new OleDbParameter("@sampleCharacter", OleDbType.VarChar,100),
					new OleDbParameter("@getSample", OleDbType.VarChar,100),
					new OleDbParameter("@condition", OleDbType.VarChar,255),
					new OleDbParameter("@controlmode", OleDbType.VarChar,255),
					new OleDbParameter("@tester", OleDbType.VarChar,100),
					new OleDbParameter("@assessor", OleDbType.VarChar,100),
					new OleDbParameter("@sign", OleDbType.VarChar,0),
					new OleDbParameter("@ID", OleDbType.Integer,4)};
			parameters[0].Value = model.methodName;
			parameters[1].Value = model.testStandard;
			parameters[2].Value = model.testType;
			parameters[3].Value = model.xmlPath;
			parameters[4].Value = model.selResultID;
			parameters[5].Value = model.isProLoad;
			parameters[6].Value = model.proLoadType;
			parameters[7].Value = model.proLoadValue;
			parameters[8].Value = model.proLoadControlType;
			parameters[9].Value = model.proLoadSpeed;
			parameters[10].Value = model.isLxqf;
			parameters[11].Value = model.controlType1;
			parameters[12].Value = model.controlType2;
			parameters[13].Value = model.controlType3;
			parameters[14].Value = model.controlType4;
			parameters[15].Value = model.controlType5;
			parameters[16].Value = model.controlType6;
			parameters[17].Value = model.controlType7;
			parameters[18].Value = model.controlType8;
			parameters[19].Value = model.controlType9;
			parameters[20].Value = model.controlType10;
			parameters[21].Value = model.controlType11;
			parameters[22].Value = model.controlType12;
			parameters[23].Value = model.circleNum;
			parameters[24].Value = model.stopValue;
			parameters[25].Value = model.isTakeDownExten;
			parameters[26].Value = model.extenChannel;
			parameters[27].Value = model.extenValue;
			parameters[28].Value = model.sendCompany;
			parameters[29].Value = model.stuffCardNo;
			parameters[30].Value = model.stuffSpec;
			parameters[31].Value = model.stuffType;
			parameters[32].Value = model.hotStatus;
			parameters[33].Value = model.temperature;
			parameters[34].Value = model.humidity;
			parameters[35].Value = model.testMethod;
			parameters[36].Value = model.mathineType;
			parameters[37].Value = model.testCondition;
			parameters[38].Value = model.sampleCharacter;
			parameters[39].Value = model.getSample;
			parameters[40].Value = model.condition;
			parameters[41].Value = model.controlmode;
			parameters[42].Value = model.tester;
			parameters[43].Value = model.assessor;
			parameters[44].Value = model.sign;
			parameters[45].Value = model.ID;

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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_GBT282892012_Method ");
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
        public bool Delete(string methodName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_GBT282892012_Method ");
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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_GBT282892012_Method ");
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
		public HR_Test.Model.GBT282892012_Method GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,methodName,testStandard,testType,xmlPath,selResultID,isProLoad,proLoadType,proLoadValue,proLoadControlType,proLoadSpeed,isLxqf,controlType1,controlType2,controlType3,controlType4,controlType5,controlType6,controlType7,controlType8,controlType9,controlType10,controlType11,controlType12,circleNum,stopValue,isTakeDownExten,extenChannel,extenValue,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testMethod,mathineType,testCondition,sampleCharacter,getSample,condition,controlmode,tester,assessor,sign from tb_GBT282892012_Method ");
			strSql.Append(" where ID=@ID");
			OleDbParameter[] parameters = {
					new OleDbParameter("@ID", OleDbType.Integer,4)
			};
			parameters[0].Value = ID;

			HR_Test.Model.GBT282892012_Method model=new HR_Test.Model.GBT282892012_Method();
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
        public HR_Test.Model.GBT282892012_Method GetModel(string methodName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,methodName,testStandard,testType,xmlPath,selResultID,isProLoad,proLoadType,proLoadValue,proLoadControlType,proLoadSpeed,isLxqf,controlType1,controlType2,controlType3,controlType4,controlType5,controlType6,controlType7,controlType8,controlType9,controlType10,controlType11,controlType12,circleNum,stopValue,isTakeDownExten,extenChannel,extenValue,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testMethod,mathineType,testCondition,sampleCharacter,getSample,condition,controlmode,tester,assessor,sign from tb_GBT282892012_Method ");
            strSql.Append(" where methodName=@methodName");
            OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar )
			};
            parameters[0].Value = methodName;

            HR_Test.Model.GBT282892012_Method model = new HR_Test.Model.GBT282892012_Method();
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
		public HR_Test.Model.GBT282892012_Method DataRowToModel(DataRow row)
		{
			HR_Test.Model.GBT282892012_Method model=new HR_Test.Model.GBT282892012_Method();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["methodName"]!=null)
				{
					model.methodName=row["methodName"].ToString();
				}
				if(row["testStandard"]!=null)
				{
					model.testStandard=row["testStandard"].ToString();
				}
				if(row["testType"]!=null)
				{
					model.testType=row["testType"].ToString();
				}
				if(row["xmlPath"]!=null)
				{
					model.xmlPath=row["xmlPath"].ToString();
				}
				if(row["selResultID"]!=null && row["selResultID"].ToString()!="")
				{
					model.selResultID=int.Parse(row["selResultID"].ToString());
				}
				if(row["isProLoad"]!=null && row["isProLoad"].ToString()!="")
				{
					if((row["isProLoad"].ToString()=="1")||(row["isProLoad"].ToString().ToLower()=="true"))
					{
						model.isProLoad=true;
					}
					else
					{
						model.isProLoad=false;
					}
				}
				if(row["proLoadType"]!=null && row["proLoadType"].ToString()!="")
				{
					model.proLoadType=int.Parse(row["proLoadType"].ToString());
				}
				model.proLoadValue= double.Parse( row["proLoadValue"].ToString());
				if(row["proLoadControlType"]!=null && row["proLoadControlType"].ToString()!="")
				{
					model.proLoadControlType=int.Parse(row["proLoadControlType"].ToString());
				}
				 model.proLoadSpeed= double.Parse( row["proLoadSpeed"].ToString());
				if(row["isLxqf"]!=null && row["isLxqf"].ToString()!="")
				{
					model.isLxqf=int.Parse(row["isLxqf"].ToString());
				}
				if(row["controlType1"]!=null)
				{
					model.controlType1=row["controlType1"].ToString();
				}
				if(row["controlType2"]!=null)
				{
					model.controlType2=row["controlType2"].ToString();
				}
				if(row["controlType3"]!=null)
				{
					model.controlType3=row["controlType3"].ToString();
				}
				if(row["controlType4"]!=null)
				{
					model.controlType4=row["controlType4"].ToString();
				}
				if(row["controlType5"]!=null)
				{
					model.controlType5=row["controlType5"].ToString();
				}
				if(row["controlType6"]!=null)
				{
					model.controlType6=row["controlType6"].ToString();
				}
				if(row["controlType7"]!=null)
				{
					model.controlType7=row["controlType7"].ToString();
				}
				if(row["controlType8"]!=null)
				{
					model.controlType8=row["controlType8"].ToString();
				}
				if(row["controlType9"]!=null)
				{
					model.controlType9=row["controlType9"].ToString();
				}
				if(row["controlType10"]!=null)
				{
					model.controlType10=row["controlType10"].ToString();
				}
				if(row["controlType11"]!=null)
				{
					model.controlType11=row["controlType11"].ToString();
				}
				if(row["controlType12"]!=null)
				{
					model.controlType12=row["controlType12"].ToString();
				}
				if(row["circleNum"]!=null && row["circleNum"].ToString()!="")
				{
					model.circleNum=int.Parse(row["circleNum"].ToString());
				}
				 model.stopValue= double.Parse( row["stopValue"].ToString());
				if(row["isTakeDownExten"]!=null && row["isTakeDownExten"].ToString()!="")
				{
					if((row["isTakeDownExten"].ToString()=="1")||(row["isTakeDownExten"].ToString().ToLower()=="true"))
					{
						model.isTakeDownExten=true;
					}
					else
					{
						model.isTakeDownExten=false;
					}
				}
				if(row["extenChannel"]!=null && row["extenChannel"].ToString()!="")
				{
					model.extenChannel=int.Parse(row["extenChannel"].ToString());
				}
					 model.extenValue=double.Parse(row["extenValue"].ToString());
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
				 model.temperature=double.Parse(row["temperature"].ToString());
				 model.humidity=double.Parse(row["humidity"].ToString());
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
				if(row["condition"]!=null)
				{
					model.condition=row["condition"].ToString();
				}
				if(row["controlmode"]!=null)
				{
					model.controlmode=row["controlmode"].ToString();
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
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,methodName,testStandard,testType,xmlPath,selResultID,isProLoad,proLoadType,proLoadValue,proLoadControlType,proLoadSpeed,isLxqf,controlType1,controlType2,controlType3,controlType4,controlType5,controlType6,controlType7,controlType8,controlType9,controlType10,controlType11,controlType12,circleNum,stopValue,isTakeDownExten,extenChannel,extenValue,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testMethod,mathineType,testCondition,sampleCharacter,getSample,condition,controlmode,tester,assessor,sign ");
			strSql.Append(" FROM tb_GBT282892012_Method ");
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
			strSql.Append("select count(1) FROM tb_GBT282892012_Method ");
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
			strSql.Append(")AS Row, T.*  from tb_GBT282892012_Method T ");
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
			parameters[0].Value = "tb_GBT282892012_Method";
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

