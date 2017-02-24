using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Maticsoft.DBUtility;//Please add references
namespace HR_Test.DAL
{
	/// <summary>
	/// 数据访问类:ControlMethod
	/// </summary>
	public partial class ControlMethod
	{
		public ControlMethod()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string methodName)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tb_ControlMethod");
			strSql.Append(" where methodName=@methodName ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,100)};
			parameters[0].Value = methodName;

			return DbHelperOleDb.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(HR_Test.Model.ControlMethod model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_ControlMethod(");
			strSql.Append("methodName,xmlPath,selResultID,isProLoad,proLoadType,proLoadValue,proLoadControlType,proLoadSpeed,isLxqf,controlType1,controlType2,controlType3,controlType4,controlType5,controlType6,controlType7,controlType8,controlType9,controlType10,controlType11,controlType12,circleNum,stopValue,isTakeDownExten,extenChannel,extenValue,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,a0,au,b0,bu,d0,du,Do,L0,L01,Lc,Le,Lt,Lu,Lu1,S0,Su,k,sign,condition,controlmode)");
			strSql.Append(" values (");
			strSql.Append("@methodName,@xmlPath,@selResultID,@isProLoad,@proLoadType,@proLoadValue,@proLoadControlType,@proLoadSpeed,@isLxqf,@controlType1,@controlType2,@controlType3,@controlType4,@controlType5,@controlType6,@controlType7,@controlType8,@controlType9,@controlType10,@controlType11,@controlType12,@circleNum,@stopValue,@isTakeDownExten,@extenChannel,@extenValue,@sendCompany,@stuffCardNo,@stuffSpec,@stuffType,@hotStatus,@temperature,@humidity,@testStandard,@testMethod,@mathineType,@testCondition,@sampleCharacter,@getSample,@tester,@assessor,@a0,@au,@b0,@bu,@d0,@du,@Do,@L0,@L01,@Lc,@Le,@Lt,@Lu,@Lu1,@S0,@Su,@k,@sign,@condition,@controlmode)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,100),
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
					new OleDbParameter("@sign", OleDbType.VarChar,255),
                     new OleDbParameter("@condition", OleDbType.VarChar,255),
                     new OleDbParameter("@controlmode", OleDbType.VarChar,255),
                                          };
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
			parameters[41].Value = model.a0;
			parameters[42].Value = model.au;
			parameters[43].Value = model.b0;
			parameters[44].Value = model.bu;
			parameters[45].Value = model.d0;
			parameters[46].Value = model.du;
			parameters[47].Value = model.Do;
			parameters[48].Value = model.L0;
			parameters[49].Value = model.L01;
			parameters[50].Value = model.Lc;
			parameters[51].Value = model.Le;
			parameters[52].Value = model.Lt;
			parameters[53].Value = model.Lu;
			parameters[54].Value = model.Lu1;
			parameters[55].Value = model.S0;
			parameters[56].Value = model.Su;
			parameters[57].Value = model.k;
			parameters[58].Value = model.sign;
            parameters[59].Value = model.condition;
            parameters[60].Value = model.controlmode;
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
		public bool Update(HR_Test.Model.ControlMethod model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_ControlMethod set ");
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
			strSql.Append("sign=@sign,");
            strSql.Append("condition=@condition,");
            strSql.Append("controlmode=@controlmode");
			strSql.Append(" where methodName=@methodName ");
			OleDbParameter[] parameters = {
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
					new OleDbParameter("@sign", OleDbType.VarChar,255),
                    new OleDbParameter("@condition", OleDbType.VarChar,255),
                    new OleDbParameter("@controlmode", OleDbType.VarChar,255),
					new OleDbParameter("@methodName", OleDbType.VarChar,100)};
			parameters[0].Value = model.xmlPath;
			parameters[1].Value = model.selResultID;
			parameters[2].Value = model.isProLoad;
			parameters[3].Value = model.proLoadType;
			parameters[4].Value = model.proLoadValue;
			parameters[5].Value = model.proLoadControlType;
			parameters[6].Value = model.proLoadSpeed;
			parameters[7].Value = model.isLxqf;
			parameters[8].Value = model.controlType1;
			parameters[9].Value = model.controlType2;
			parameters[10].Value = model.controlType3;
			parameters[11].Value = model.controlType4;
			parameters[12].Value = model.controlType5;
			parameters[13].Value = model.controlType6;
			parameters[14].Value = model.controlType7;
			parameters[15].Value = model.controlType8;
			parameters[16].Value = model.controlType9;
			parameters[17].Value = model.controlType10;
			parameters[18].Value = model.controlType11;
			parameters[19].Value = model.controlType12;
			parameters[20].Value = model.circleNum;
			parameters[21].Value = model.stopValue;
			parameters[22].Value = model.isTakeDownExten;
			parameters[23].Value = model.extenChannel;
			parameters[24].Value = model.extenValue;
			parameters[25].Value = model.sendCompany;
			parameters[26].Value = model.stuffCardNo;
			parameters[27].Value = model.stuffSpec;
			parameters[28].Value = model.stuffType;
			parameters[29].Value = model.hotStatus;
			parameters[30].Value = model.temperature;
			parameters[31].Value = model.humidity;
			parameters[32].Value = model.testStandard;
			parameters[33].Value = model.testMethod;
			parameters[34].Value = model.mathineType;
			parameters[35].Value = model.testCondition;
			parameters[36].Value = model.sampleCharacter;
			parameters[37].Value = model.getSample;
			parameters[38].Value = model.tester;
			parameters[39].Value = model.assessor;
			parameters[40].Value = model.a0;
			parameters[41].Value = model.au;
			parameters[42].Value = model.b0;
			parameters[43].Value = model.bu;
			parameters[44].Value = model.d0;
			parameters[45].Value = model.du;
			parameters[46].Value = model.Do;
			parameters[47].Value = model.L0;
			parameters[48].Value = model.L01;
			parameters[49].Value = model.Lc;
			parameters[50].Value = model.Le;
			parameters[51].Value = model.Lt;
			parameters[52].Value = model.Lu;
			parameters[53].Value = model.Lu1;
			parameters[54].Value = model.S0;
			parameters[55].Value = model.Su;
			parameters[56].Value = model.k;
			parameters[57].Value = model.sign;
            parameters[58].Value = model.condition;
            parameters[59].Value = model.controlmode;
			parameters[60].Value = model.methodName;

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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_ControlMethod ");
			strSql.Append(" where methodName=@methodName ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,100)};
			parameters[0].Value = methodName;

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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string methodNamelist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_ControlMethod ");
			strSql.Append(" where methodName in ("+methodNamelist + ")  ");
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
		public HR_Test.Model.ControlMethod GetModel(string methodName)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,methodName,xmlPath,selResultID,isProLoad,proLoadType,proLoadValue,proLoadControlType,proLoadSpeed,isLxqf,controlType1,controlType2,controlType3,controlType4,controlType5,controlType6,controlType7,controlType8,controlType9,controlType10,controlType11,controlType12,circleNum,stopValue,isTakeDownExten,extenChannel,extenValue,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,a0,au,b0,bu,d0,du,Do,L0,L01,Lc,Le,Lt,Lu,Lu1,S0,Su,k,sign,condition,controlmode from tb_ControlMethod ");
			strSql.Append(" where methodName=@methodName ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@methodName", OleDbType.VarChar,100)};
			parameters[0].Value = methodName;

			HR_Test.Model.ControlMethod model=new HR_Test.Model.ControlMethod();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["methodName"]!=null && ds.Tables[0].Rows[0]["methodName"].ToString()!="")
				{
					model.methodName=ds.Tables[0].Rows[0]["methodName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["xmlPath"]!=null && ds.Tables[0].Rows[0]["xmlPath"].ToString()!="")
				{
					model.xmlPath=ds.Tables[0].Rows[0]["xmlPath"].ToString();
				}
				if(ds.Tables[0].Rows[0]["selResultID"]!=null && ds.Tables[0].Rows[0]["selResultID"].ToString()!="")
				{
					model.selResultID=int.Parse(ds.Tables[0].Rows[0]["selResultID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["isProLoad"]!=null && ds.Tables[0].Rows[0]["isProLoad"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["isProLoad"].ToString()=="1")||(ds.Tables[0].Rows[0]["isProLoad"].ToString().ToLower()=="true"))
					{
						model.isProLoad=true;
					}
					else
					{
						model.isProLoad=false;
					}
				}
				if(ds.Tables[0].Rows[0]["proLoadType"]!=null && ds.Tables[0].Rows[0]["proLoadType"].ToString()!="")
				{
					model.proLoadType=int.Parse(ds.Tables[0].Rows[0]["proLoadType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["proLoadValue"]!=null && ds.Tables[0].Rows[0]["proLoadValue"].ToString()!="")
				{
					model.proLoadValue=double.Parse(ds.Tables[0].Rows[0]["proLoadValue"].ToString());
				}
				if(ds.Tables[0].Rows[0]["proLoadControlType"]!=null && ds.Tables[0].Rows[0]["proLoadControlType"].ToString()!="")
				{
					model.proLoadControlType=int.Parse(ds.Tables[0].Rows[0]["proLoadControlType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["proLoadSpeed"]!=null && ds.Tables[0].Rows[0]["proLoadSpeed"].ToString()!="")
				{
                    model.proLoadSpeed=double.Parse(ds.Tables[0].Rows[0]["proLoadSpeed"].ToString());
				}
				if(ds.Tables[0].Rows[0]["isLxqf"]!=null && ds.Tables[0].Rows[0]["isLxqf"].ToString()!="")
				{
                    model.isLxqf = int.Parse(ds.Tables[0].Rows[0]["isLxqf"].ToString()); 
				}
				if(ds.Tables[0].Rows[0]["controlType1"]!=null && ds.Tables[0].Rows[0]["controlType1"].ToString()!="")
				{
					model.controlType1=ds.Tables[0].Rows[0]["controlType1"].ToString();
				}
				if(ds.Tables[0].Rows[0]["controlType2"]!=null && ds.Tables[0].Rows[0]["controlType2"].ToString()!="")
				{
					model.controlType2=ds.Tables[0].Rows[0]["controlType2"].ToString();
				}
				if(ds.Tables[0].Rows[0]["controlType3"]!=null && ds.Tables[0].Rows[0]["controlType3"].ToString()!="")
				{
					model.controlType3=ds.Tables[0].Rows[0]["controlType3"].ToString();
				}
				if(ds.Tables[0].Rows[0]["controlType4"]!=null && ds.Tables[0].Rows[0]["controlType4"].ToString()!="")
				{
					model.controlType4=ds.Tables[0].Rows[0]["controlType4"].ToString();
				}
				if(ds.Tables[0].Rows[0]["controlType5"]!=null && ds.Tables[0].Rows[0]["controlType5"].ToString()!="")
				{
					model.controlType5=ds.Tables[0].Rows[0]["controlType5"].ToString();
				}
				if(ds.Tables[0].Rows[0]["controlType6"]!=null && ds.Tables[0].Rows[0]["controlType6"].ToString()!="")
				{
					model.controlType6=ds.Tables[0].Rows[0]["controlType6"].ToString();
				}
				if(ds.Tables[0].Rows[0]["controlType7"]!=null && ds.Tables[0].Rows[0]["controlType7"].ToString()!="")
				{
					model.controlType7=ds.Tables[0].Rows[0]["controlType7"].ToString();
				}
				if(ds.Tables[0].Rows[0]["controlType8"]!=null && ds.Tables[0].Rows[0]["controlType8"].ToString()!="")
				{
					model.controlType8=ds.Tables[0].Rows[0]["controlType8"].ToString();
				}
				if(ds.Tables[0].Rows[0]["controlType9"]!=null && ds.Tables[0].Rows[0]["controlType9"].ToString()!="")
				{
					model.controlType9=ds.Tables[0].Rows[0]["controlType9"].ToString();
				}
				if(ds.Tables[0].Rows[0]["controlType10"]!=null && ds.Tables[0].Rows[0]["controlType10"].ToString()!="")
				{
					model.controlType10=ds.Tables[0].Rows[0]["controlType10"].ToString();
				}
				if(ds.Tables[0].Rows[0]["controlType11"]!=null && ds.Tables[0].Rows[0]["controlType11"].ToString()!="")
				{
					model.controlType11=ds.Tables[0].Rows[0]["controlType11"].ToString();
				}
				if(ds.Tables[0].Rows[0]["controlType12"]!=null && ds.Tables[0].Rows[0]["controlType12"].ToString()!="")
				{
					model.controlType12=ds.Tables[0].Rows[0]["controlType12"].ToString();
				}
				if(ds.Tables[0].Rows[0]["circleNum"]!=null && ds.Tables[0].Rows[0]["circleNum"].ToString()!="")
				{
					model.circleNum=int.Parse(ds.Tables[0].Rows[0]["circleNum"].ToString());
				}
				if(ds.Tables[0].Rows[0]["stopValue"]!=null && ds.Tables[0].Rows[0]["stopValue"].ToString()!="")
				{
					model.stopValue=double.Parse(ds.Tables[0].Rows[0]["stopValue"].ToString());
				}
				if(ds.Tables[0].Rows[0]["isTakeDownExten"]!=null && ds.Tables[0].Rows[0]["isTakeDownExten"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["isTakeDownExten"].ToString()=="1")||(ds.Tables[0].Rows[0]["isTakeDownExten"].ToString().ToLower()=="true"))
					{
						model.isTakeDownExten=true;
					}
					else
					{
						model.isTakeDownExten=false;
					}
				}
				if(ds.Tables[0].Rows[0]["extenChannel"]!=null && ds.Tables[0].Rows[0]["extenChannel"].ToString()!="")
				{
					model.extenChannel=int.Parse(ds.Tables[0].Rows[0]["extenChannel"].ToString());
				}
				if(ds.Tables[0].Rows[0]["extenValue"]!=null && ds.Tables[0].Rows[0]["extenValue"].ToString()!="")
				{
					model.extenValue=double.Parse(ds.Tables[0].Rows[0]["extenValue"].ToString());
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
                if (ds.Tables[0].Rows[0]["sign"] != null && ds.Tables[0].Rows[0]["sign"].ToString() != "")
				{
					model.sign=ds.Tables[0].Rows[0]["sign"].ToString();
				}
                if (ds.Tables[0].Rows[0]["condition"] != null && ds.Tables[0].Rows[0]["condition"].ToString() != "")
                {
                    model.condition = ds.Tables[0].Rows[0]["condition"].ToString();
                }
                if (ds.Tables[0].Rows[0]["controlmode"] != null && ds.Tables[0].Rows[0]["controlmode"].ToString() != "")
                {
                    model.controlmode = ds.Tables[0].Rows[0]["controlmode"].ToString();
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
        public HR_Test.Model.ControlMethod GetModel(long ID)
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from tb_ControlMethod ");
            strSql.Append(" where ID=@ID ");

            OleDbParameter[] parameters = { new OleDbParameter("@ID", OleDbType.BigInt) };
            parameters[0].Value = ID;

            HR_Test.Model.ControlMethod model = new HR_Test.Model.ControlMethod();
            DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["methodName"] != null && ds.Tables[0].Rows[0]["methodName"].ToString() != "")
                {
                    model.methodName = ds.Tables[0].Rows[0]["methodName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["xmlPath"] != null && ds.Tables[0].Rows[0]["xmlPath"].ToString() != "")
                {
                    model.xmlPath = ds.Tables[0].Rows[0]["xmlPath"].ToString();
                }
                if (ds.Tables[0].Rows[0]["selResultID"] != null && ds.Tables[0].Rows[0]["selResultID"].ToString() != "")
                {
                    model.selResultID = int.Parse(ds.Tables[0].Rows[0]["selResultID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isProLoad"] != null && ds.Tables[0].Rows[0]["isProLoad"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["isProLoad"].ToString() == "1") || (ds.Tables[0].Rows[0]["isProLoad"].ToString().ToLower() == "true"))
                    {
                        model.isProLoad = true;
                    }
                    else
                    {
                        model.isProLoad = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["proLoadType"] != null && ds.Tables[0].Rows[0]["proLoadType"].ToString() != "")
                {
                    model.proLoadType = int.Parse(ds.Tables[0].Rows[0]["proLoadType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["proLoadValue"] != null && ds.Tables[0].Rows[0]["proLoadValue"].ToString() != "")
                {
                    model.proLoadValue = double.Parse(ds.Tables[0].Rows[0]["proLoadValue"].ToString());
                }
                if (ds.Tables[0].Rows[0]["proLoadControlType"] != null && ds.Tables[0].Rows[0]["proLoadControlType"].ToString() != "")
                {
                    model.proLoadControlType = int.Parse(ds.Tables[0].Rows[0]["proLoadControlType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["proLoadSpeed"] != null && ds.Tables[0].Rows[0]["proLoadSpeed"].ToString() != "")
                {
                    model.proLoadSpeed = double.Parse(ds.Tables[0].Rows[0]["proLoadSpeed"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isLxqf"] != null && ds.Tables[0].Rows[0]["isLxqf"].ToString() != "")
                {
                    model.isLxqf = int.Parse(ds.Tables[0].Rows[0]["isLxqf"].ToString());
                }
                if (ds.Tables[0].Rows[0]["controlType1"] != null && ds.Tables[0].Rows[0]["controlType1"].ToString() != "")
                {
                    model.controlType1 = ds.Tables[0].Rows[0]["controlType1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["controlType2"] != null && ds.Tables[0].Rows[0]["controlType2"].ToString() != "")
                {
                    model.controlType2 = ds.Tables[0].Rows[0]["controlType2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["controlType3"] != null && ds.Tables[0].Rows[0]["controlType3"].ToString() != "")
                {
                    model.controlType3 = ds.Tables[0].Rows[0]["controlType3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["controlType4"] != null && ds.Tables[0].Rows[0]["controlType4"].ToString() != "")
                {
                    model.controlType4 = ds.Tables[0].Rows[0]["controlType4"].ToString();
                }
                if (ds.Tables[0].Rows[0]["controlType5"] != null && ds.Tables[0].Rows[0]["controlType5"].ToString() != "")
                {
                    model.controlType5 = ds.Tables[0].Rows[0]["controlType5"].ToString();
                }
                if (ds.Tables[0].Rows[0]["controlType6"] != null && ds.Tables[0].Rows[0]["controlType6"].ToString() != "")
                {
                    model.controlType6 = ds.Tables[0].Rows[0]["controlType6"].ToString();
                }
                if (ds.Tables[0].Rows[0]["controlType7"] != null && ds.Tables[0].Rows[0]["controlType7"].ToString() != "")
                {
                    model.controlType7 = ds.Tables[0].Rows[0]["controlType7"].ToString();
                }
                if (ds.Tables[0].Rows[0]["controlType8"] != null && ds.Tables[0].Rows[0]["controlType8"].ToString() != "")
                {
                    model.controlType8 = ds.Tables[0].Rows[0]["controlType8"].ToString();
                }
                if (ds.Tables[0].Rows[0]["controlType9"] != null && ds.Tables[0].Rows[0]["controlType9"].ToString() != "")
                {
                    model.controlType9 = ds.Tables[0].Rows[0]["controlType9"].ToString();
                }
                if (ds.Tables[0].Rows[0]["controlType10"] != null && ds.Tables[0].Rows[0]["controlType10"].ToString() != "")
                {
                    model.controlType10 = ds.Tables[0].Rows[0]["controlType10"].ToString();
                }
                if (ds.Tables[0].Rows[0]["controlType11"] != null && ds.Tables[0].Rows[0]["controlType11"].ToString() != "")
                {
                    model.controlType11 = ds.Tables[0].Rows[0]["controlType11"].ToString();
                }
                if (ds.Tables[0].Rows[0]["controlType12"] != null && ds.Tables[0].Rows[0]["controlType12"].ToString() != "")
                {
                    model.controlType12 = ds.Tables[0].Rows[0]["controlType12"].ToString();
                }
                if (ds.Tables[0].Rows[0]["circleNum"] != null && ds.Tables[0].Rows[0]["circleNum"].ToString() != "")
                {
                    model.circleNum = int.Parse(ds.Tables[0].Rows[0]["circleNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["stopValue"] != null && ds.Tables[0].Rows[0]["stopValue"].ToString() != "")
                {
                    model.stopValue = double.Parse(ds.Tables[0].Rows[0]["stopValue"].ToString());
                }
                if (ds.Tables[0].Rows[0]["isTakeDownExten"] != null && ds.Tables[0].Rows[0]["isTakeDownExten"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["isTakeDownExten"].ToString() == "1") || (ds.Tables[0].Rows[0]["isTakeDownExten"].ToString().ToLower() == "true"))
                    {
                        model.isTakeDownExten = true;
                    }
                    else
                    {
                        model.isTakeDownExten = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["extenChannel"] != null && ds.Tables[0].Rows[0]["extenChannel"].ToString() != "")
                {
                    model.extenChannel = int.Parse(ds.Tables[0].Rows[0]["extenChannel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["extenValue"] != null && ds.Tables[0].Rows[0]["extenValue"].ToString() != "")
                {
                    model.extenValue = double.Parse(ds.Tables[0].Rows[0]["extenValue"].ToString());
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
                if (ds.Tables[0].Rows[0]["sign"] != null && ds.Tables[0].Rows[0]["sign"].ToString() != "")
                {
                    model.sign = ds.Tables[0].Rows[0]["sign"].ToString();
                }
                if (ds.Tables[0].Rows[0]["condition"] != null && ds.Tables[0].Rows[0]["condition"].ToString() != "")
                {
                    model.condition = ds.Tables[0].Rows[0]["condition"].ToString();
                }
                if (ds.Tables[0].Rows[0]["controlmode"] != null && ds.Tables[0].Rows[0]["controlmode"].ToString() != "")
                {
                    model.controlmode = ds.Tables[0].Rows[0]["controlmode"].ToString();
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
			strSql.Append("select ID,methodName,xmlPath,selResultID,isProLoad,proLoadType,proLoadValue,proLoadControlType,proLoadSpeed,isLxqf,controlType1,controlType2,controlType3,controlType4,controlType5,controlType6,controlType7,controlType8,controlType9,controlType10,controlType11,controlType12,circleNum,stopValue,isTakeDownExten,extenChannel,extenValue,sendCompany,stuffCardNo,stuffSpec,stuffType,hotStatus,temperature,humidity,testStandard,testMethod,mathineType,testCondition,sampleCharacter,getSample,tester,assessor,a0,au,b0,bu,d0,du,Do,L0,L01,Lc,Le,Lt,Lu,Lu1,S0,Su,k,sign,condition,controlmode ");
			strSql.Append(" FROM tb_ControlMethod ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			parameters[0].Value = "tb_ControlMethod";
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

