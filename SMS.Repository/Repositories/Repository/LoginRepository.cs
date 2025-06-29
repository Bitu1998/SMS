using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Model.Login;
using System.Data;
using SMS.Repository.Login;
using SMS.Repository.BaseRepository;
using Dapper;
using SMS.Repository.Factory;
using SMS.Domain;

namespace SMS.Repository.Repositories.Repository
{
    public class LoginRepository : SMSRepositoryBase, ILoginRepository
    {
        public LoginRepository(ISMSConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
        public LoginEF authentication(LoginEF objLoginpara, string strIpAddress, int intSessionToken)
        {
			LoginEF objLogin = new LoginEF();
			try
            {
                var paramList = new
                {
                    P_CHR_ACTION = "V",
                    P_VCH_USER_NAME = objLoginpara.Username,
                    P_VCH_PASSWORD = objLoginpara.password,
                    P_VCH_IP_ADDRESS = strIpAddress,
                    P_INT_SESSION_TOKEN = intSessionToken
                };
                DynamicParameters param = new DynamicParameters();
                DataTable dt = new DataTable();
                var Output = Connection.ExecuteReader("eAuction_USP_LOGIN", paramList, commandType: CommandType.StoredProcedure);
				dt.Load(Output);				
				foreach (DataRow item in dt.Rows)
				{
					if (item.IsNull("AccountLocked"))
					{
						objLogin.BIT_AccountLocked = false;
					}
					else
					{
						objLogin.BIT_AccountLocked = Convert.ToBoolean(item["AccountLocked"]);
					}
					if (item.IsNull("ISAUTHENTICATE"))
					{
						objLogin.BIT_ISAUTHENTICATE = false;
					}
					else
					{
						objLogin.BIT_ISAUTHENTICATE = Convert.ToBoolean(item["ISAUTHENTICATE"]);
					}
					if (!item.IsNull("LoginFailedAttemptsCount"))
					{
						objLogin.LoginFailedAttemptsCount = Convert.ToInt32(item["LoginFailedAttemptsCount"]);
					}
					if (!item.IsNull("LoginBlockTill"))
					{
						objLogin.LoginBlockTill = Convert.ToString(item["LoginBlockTill"]);
					}
					if (!objLogin.BIT_AccountLocked && objLogin.BIT_ISAUTHENTICATE)
					{
						if (!item.IsNull("INT_USER_ID"))
						{
							objLogin.Userid = Convert.ToInt32(item["INT_USER_ID"]);
						}
                        //-----Added By Yasmin-------
                        if (!item.IsNull("INT_USER_ID"))
                        {
                            objLogin.RoleId = Convert.ToInt32(item["RoleId"]);
                        }
                        //-----End--------------------
                        if (!item.IsNull("VCH_COMPANY_NAME"))
						{
							objLogin.COMPANY_NAME = Convert.ToString(item["VCH_COMPANY_NAME"]);
						}
						if (!item.IsNull("VCH_CONTACT_PERSON"))
						{
							objLogin.CONTACT_PERSON = Convert.ToString(item["VCH_CONTACT_PERSON"]);
						}
						if (!item.IsNull("INT_USER_TYPE"))
						{
							objLogin.USER_TYPE = Convert.ToInt32(item["INT_USER_TYPE"]);
						}
						//if (!item.IsNull("INT_VENDOR_ID"))
						//{
						//	objLogin.Vendor_Id = item["INT_VENDOR_ID"].ToString();
						//}
						//if (!item.IsNull("INT_VENDOR_ID"))
						//{
						//	objLogin.BIT_CHANGE_PASS = Convert.ToBoolean(item["BIT_CHANGE_PASS"]);
						//}
						//if (item.IsNull("BIT_EMPANELLED"))
						//{
						//	objLogin.BIT_EMPANELLED = null;
						//}
						//else
						//{
						//	objLogin.BIT_EMPANELLED = Convert.ToBoolean(item["BIT_EMPANELLED"]);
						//}
						//if (!item.IsNull("INT_SESSION_TOKEN"))
						//{
						//	objLogin.intSessionToken = Convert.ToInt32(item["INT_SESSION_TOKEN"]);
						//}
						//if (!item.IsNull("REGISTRATION_FEE"))
						//{
						//	objLogin.Int_regFee_Count = Convert.ToInt32(item["REGISTRATION_FEE"]);
						//}
						//if (!item.IsNull("REGISTRATION_FEE_AMOUNT"))
						//{
						//	objLogin.REGISTRATION_FEE_AMOUNT = Convert.ToString(item["REGISTRATION_FEE_AMOUNT"]);
						//}
						//if (!item.IsNull("TRANS_DETAILS"))
						//{
						//	objLogin.Int_trans_details = Convert.ToInt32(item["TRANS_DETAILS"]);
						//}
						if (!item.IsNull("VCH_USER_NAME"))
						{
							objLogin.Username = Convert.ToString(item["VCH_USER_NAME"]);
						}
						if (!item.IsNull("UserType"))
						{
							objLogin.USER_TYPE_STR = Convert.ToString(item["UserType"]);
						}
						if (!item.IsNull("intcricleid"))
						{
							objLogin.intcricleid = Convert.ToInt32(item["intcricleid"]);
						}
						if (!item.IsNull("BitCommitteeMember"))
						{
							objLogin.bitCommitteeMember = Convert.ToBoolean(item["BitCommitteeMember"]);
						}
						if (!item.IsNull("IsPasswordChange"))
						 {
							objLogin.IsPasswordChange = Convert.ToInt32(item["IsPasswordChange"]);
						}
						if (!item.IsNull("password"))
						{
							objLogin.password = Convert.ToString(item["password"]);
						}

					}


				}
				//return objLogin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
				//objLogin = null;
				Connection.Close();
			}
			return objLogin;
		}

		public LoginEF GetUserDetails(LoginEF objLoginpara)
		{
			LoginEF objLogin = new LoginEF();
			try
			{
				var paramList = new
				{
					P_CHR_ACTION = "A",
					P_VCH_USER_NAME = objLoginpara.Username,
					P_VCH_PASSWORD = objLoginpara.password,
					P_VCH_IP_ADDRESS = "",
					P_INT_SESSION_TOKEN = ""
				};
				DynamicParameters param = new DynamicParameters();
				DataTable dt = new DataTable();
				//var Output = Connection.ExecuteReader("USP_LOGIN", paramList, commandType: System.Data.CommandType.StoredProcedure);
				var Output = Connection.ExecuteReader("eAuction_USP_LOGIN", paramList, commandType: CommandType.StoredProcedure);
				dt.Load(Output);
				foreach (DataRow item in dt.Rows)
				{
					if (item.IsNull("AccountLocked"))
					{
						objLogin.BIT_AccountLocked = false;
					}
					else
					{
						objLogin.BIT_AccountLocked = Convert.ToBoolean(item["AccountLocked"]);
					}
					if (item.IsNull("ISAUTHENTICATE"))
					{
						objLogin.BIT_ISAUTHENTICATE = false;
					}
					else
					{
						objLogin.BIT_ISAUTHENTICATE = Convert.ToBoolean(item["ISAUTHENTICATE"]);
					}

					if (!objLogin.BIT_AccountLocked && objLogin.BIT_ISAUTHENTICATE)
					{
						if (!item.IsNull("INT_USER_ID"))
						{
							objLogin.Userid = Convert.ToInt32(item["INT_USER_ID"]);
						}
						if (!item.IsNull("VCH_COMPANY_NAME"))
						{
							objLogin.COMPANY_NAME = Convert.ToString(item["VCH_COMPANY_NAME"]);
						}
						if (!item.IsNull("VCH_CONTACT_PERSON"))
						{
							objLogin.CONTACT_PERSON = Convert.ToString(item["VCH_CONTACT_PERSON"]);
						}
						if (!item.IsNull("INT_USER_TYPE"))
						{
							objLogin.USER_TYPE = Convert.ToInt32(item["INT_USER_TYPE"]);
						}
						if (!item.IsNull("INT_VENDOR_ID"))
						{
							objLogin.Vendor_Id = item["INT_VENDOR_ID"].ToString();
						}
						if (!item.IsNull("INT_VENDOR_ID"))
						{
							objLogin.BIT_CHANGE_PASS = Convert.ToBoolean(item["BIT_CHANGE_PASS"]);
						}
						if (item.IsNull("BIT_EMPANELLED"))
						{
							objLogin.BIT_EMPANELLED = null;
						}
						else
						{
							objLogin.BIT_EMPANELLED = Convert.ToBoolean(item["BIT_EMPANELLED"]);
						}
						if (!item.IsNull("INT_SESSION_TOKEN"))
						{
							objLogin.intSessionToken = Convert.ToInt32(item["INT_SESSION_TOKEN"]);
						}
						if (!item.IsNull("REGISTRATION_FEE"))
						{
							objLogin.Int_regFee_Count = Convert.ToInt32(item["REGISTRATION_FEE"]);
						}
						if (!item.IsNull("REGISTRATION_FEE_AMOUNT"))
						{
							objLogin.REGISTRATION_FEE_AMOUNT = Convert.ToString(item["REGISTRATION_FEE_AMOUNT"]);
						}
						if (!item.IsNull("TRANS_DETAILS"))
						{
							objLogin.Int_trans_details = Convert.ToInt32(item["TRANS_DETAILS"]);
						}
					}


				}
				return objLogin;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				//objLogin = null;
				Connection.Close();
			}
		}



		#region Get User Type
		public async Task<LoginEF> GetUserType(LoginEF loginModel)
		{
			var messageEF = new LoginEF();
			try
			{
				var p = new DynamicParameters();
				p.Add("@userName", loginModel.Username);
				var result = await Connection.QueryAsync<LoginEF>("USP_CHECK_USER_TYPE", p, commandType: CommandType.StoredProcedure);
				if (result.Count() > 0)
				{
					messageEF = result.FirstOrDefault();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return messageEF;
		}

        public MessageEF ResetPassword(LoginEF forgotPasswordModel)
        {
			LoginEF objforgotPasswordModel = new LoginEF();
			MessageEF messageEF = new MessageEF();
			try
			{
				var paramlist = new
				{
					Check = 5,
					ProfileUserId = forgotPasswordModel.Userid,
					SubUserID = forgotPasswordModel.Userid,
					UserLoginId = 0,
					Password = forgotPasswordModel.ConfirmPassword,
					forgotPasswordModel.EncryptPassword
				};
				var result = Connection.Query<MessageEF>("uspValidateUser", paramlist, commandType: CommandType.StoredProcedure);
				if (result.Count() > 0)
				{
					messageEF.Satus = "1";
				}
			}
			catch (Exception)
			{
				throw;
			}
			return messageEF;
		}

		public string ErrorList(LogEntry objLogEntry)
		{
			try
			{
				var paramList = new
				{
					objLogEntry.Controller
					,
					objLogEntry.Action
					,
					objLogEntry.ReturnType
					,
					objLogEntry.ErrorMessage
					,
					objLogEntry.StackTrace
					,
					UserId = objLogEntry.UserID
					,
					objLogEntry.UserLoginID
				};
				var result = Connection.Execute("ReportErrorLog", paramList, commandType: CommandType.StoredProcedure);
				return "1";
			}
			catch (Exception ex)
			{

				throw ex;
			}
		}

        public MessageEF ChangePassword(ChangePasswordEF ChangePasswordModel)
        {
			ChangePasswordEF objforgotPasswordModel = new ChangePasswordEF();
			MessageEF messageEF = new MessageEF(); 
			try
			{
				var p = new DynamicParameters();
				p.Add("ProfileUserId", ChangePasswordModel.UserId);
				p.Add("SubUserID", ChangePasswordModel.UserId);
				p.Add("Password", ChangePasswordModel.NewPassword);
				p.Add("EncryptPassword", ChangePasswordModel.EncryptPassword);
				p.Add("Check", "5");
				var result = Connection.Query<MessageEF>("uspValidateUser", p, commandType: CommandType.StoredProcedure);
				 
				if (result.Count() > 0)
                {
                    messageEF.Satus = "1";
                }
            }
			catch (Exception ex)
			{
				throw ex;
			}
			return messageEF;
		}




        #endregion
    }
}
