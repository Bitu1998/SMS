using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model.Login
{
    public class LoginEF
    {
        //[Required(ErrorMessage = "Enter User Name")]
        //[RegularExpression(@"^[a-zA-Z0-9_-]{3,15}$", ErrorMessage = "Invalid User Name(Only Alphnumeric Allowed)")]
        //[StringLength(20, MinimumLength = 3, ErrorMessage = "User name minimum length should be 3 character")]
        public string Username { get; set; }
		//[Required(ErrorMessage = "Enter Password")]
		//[StringLength(20, MinimumLength = 3, ErrorMessage = "Password minimum length should be 3 character")]
		public string password { get; set; }
		//[Required(ErrorMessage = "Enter reCAPTCHA")]
		public string captcha { get; set; }
		public string COMPANY_NAME { get; set; }
		public int Userid { get; set; }
        public int RoleId { get; set; }//Added By Yasmin
        public int USER_TYPE { get; set; }
		public string CONTACT_PERSON { get; set; }
		public string Vendor_Id { get; set; }
		public Boolean? BIT_CHANGE_PASS { get; set; }
		public Boolean? BIT_EMPANELLED { get; set; }
		public int intSessionToken { get; set; }
		public int Int_regFee_Count { get; set; }
		public int Int_trans_details { get; set; }
		public string REGISTRATION_FEE_AMOUNT { get; set; }
		public Boolean BIT_AccountLocked { get; set; }
		public Boolean BIT_ISAUTHENTICATE { get; set; }
		public string USER_TYPE_STR { get; set; } // Added By Subhashree Chinaray on 08 May 2023
        public string MobileNo { get; set; }
        public string Mail { get; set; }
		public string msg { get; set; } 
		public string OTP { get; set; }
		public string EMAILID { get; set; }
		public string EncryptNewPassword { get; set; }
		public string EncryptConfirmPassword { get; set; }
		public string NewPassword { get; set; }
		public string ConfirmPassword { get; set; }
		public string EncryptPassword { get; set; }

		public int intcricleid { get; set; }
		public Boolean bitCommitteeMember { get; set; }
		public string randomNumber { get; set; }
        public int LoginFailedAttemptsCount { get; set; }
        public string LoginBlockTill { get; set; }


		public int IsPasswordChange { get; set; }
		public string EncryptUserId { get; set; }
	 
	}

	public class LogEntry
	{

		public string Controller { get; set; }
		public string Action { get; set; }
		public string ReturnType { get; set; }
		public string ErrorMessage { get; set; }
		public string StackTrace { get; set; }
		public string CreatedDate { get; set; }
		public string UserLoginID { get; set; }
		public string UserID { get; set; }

	}
    public class ErrorResponse<T>
    {
        public bool Status { get; set; }
        public List<string> Message { get; set; } = new List<string>();
        public T Data { get; set; }
    }


}
