
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SMS.Domain;
using SMS.Model.Login;
using SMS.Repository.Login;
using System;
using System.Threading.Tasks;

namespace SMS.Controllers
{
    public class LoginController : Controller
    {
        public ILoginRepository _loginRepository;
        MessageEF objobjmodel = new MessageEF();
        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        public IActionResult Index(LoginEF loginEF)
        {
            try
            {
                #region Validation
                if (string.IsNullOrEmpty(loginEF.Username))
                {
                    ModelState.AddModelError("UserName", "User Name is Required");
                }
                if (string.IsNullOrEmpty(loginEF.password))
                {
                    ModelState.AddModelError("Password", "Password is Required");
                }
                if (string.IsNullOrEmpty(loginEF.captcha))
                {
                    ModelState.AddModelError("Captcha", "Captcha is Required");
                }

                #endregion

                LoginEF lsLogin = new LoginEF();
                Random randomno = new Random();

                int intrandomno = randomno.Next(100000, 999999);
                string captchaEntered = loginEF.captcha.ToString();

                {
                    HttpContext.Session.SetInt32("Userid", lsLogin.Userid);
                    HttpContext.Session.SetInt32("RoleId", lsLogin.RoleId);//Added BY yasmin
                    HttpContext.Session.SetString("payment_type", "");
                    HttpContext.Session.SetString("ContactPersonNAme", lsLogin.CONTACT_PERSON);
                    HttpContext.Session.SetInt32("UserType", lsLogin.USER_TYPE);
                    HttpContext.Session.SetInt32("TokenNo", lsLogin.intSessionToken);
                    // HttpContext.Session.SetString("VendorId", lsLogin.Vendor_Id);
                    HttpContext.Session.SetInt32("intCircleid", lsLogin.intcricleid);
                    HttpContext.Session.SetInt32("BitCommitteeMember", Convert.ToInt32(lsLogin.bitCommitteeMember));

                    return RedirectToAction("Home", "Dashboard");
                }


            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


            
        

        public JsonResult CaptchaImage()
        {
            //var rand = new Random((int)DateTime.Now.Ticks);
            //int a = rand.Next(10, 99);
            //int b = rand.Next(0, 9);
            //var captcha = GetRandomText();
            // return Json(captcha);
            #region Mathematical Captcha
            Random ran = new Random();
            int firstNumber = ran.Next(1, 9);
            int secondNumber = ran.Next(1, 9);
            int calcValue = firstNumber + secondNumber;
            string captcha = $"{firstNumber.ToString()} + {secondNumber.ToString()} = ?";
            string encryptstring = "";
           // HttpContext.Session.SetString("CV", encryptstring);
            //return Json(captcha);
            return Json(new
            {
                captcha,
                calcValue
            });
            #endregion
        }

        private string GetRandomText()
        {
            //StringBuilder randomText = new StringBuilder();
            //string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
            //Random r = new Random();
            //for (int j = 0; j <= 5; j++)
            //{
            //    randomText.Append(alphabets[r.Next(alphabets.Length)]);
            //}
            string inputNumberString = "";
            Random r = new Random();
            char[] str = { '*', '-', '+' };
            int[] array = new int[3];
            int ranlen = r.Next(str.Length);
            int a = 0, b = 0, c = 0, d = 0;
            int flag = 0;
            switch (ranlen)
            {
                case 0:// FOr Multiplication.
                    a = r.Next(1, 10);
                    b = r.Next(1, 10);
                    c = a * b;
                    inputNumberString = a.ToString() + " * " + b.ToString() + " = ";
                    break;
                case 1:// FOr Subtraction.
                    a = r.Next(10, 99);
                    b = r.Next(0, 10);
                    c = a - b;
                    inputNumberString = a.ToString() + " - " + b.ToString() + " = ";
                    break;
                case 2:// FOr Addition.
                    a = r.Next(1, 10);
                    b = r.Next(1, 99);
                    c = a + b;
                    inputNumberString = a.ToString() + " + " + b.ToString() + " = ";
                    break;

            }
            //string encryptstring = SymCrypto.TDES.Encrypt(c.ToString(), "|}]A24s&¦"); ;
            //HttpContext.Session.SetString("CV", encryptstring);
            return inputNumberString.ToString();
        }

        #region Get user type
        //public async Task<JsonResult> ChkUserType(string userName)
        //{
        //    if (!string.IsNullOrEmpty(userName))
        //    {
        //        try
        //        {
        //            var messageEF = await _loginRepository.GetUserType(new LoginEF() { Username = userName });
        //            if (!string.IsNullOrEmpty(messageEF.password))
        //            {
        //                #region SMS
        //                bool isSMSSend = false;
        //                var smsDtls = await _objprovider.Get_SMS_Template("OTP_for_Registration");
        //                if (!string.IsNullOrEmpty(smsDtls.templateid))
        //                {
        //                    smsDtls.mobileNo = messageEF.MobileNo;
        //                    smsDtls.message = smsDtls.message.Replace("{#var#}", messageEF.password);
        //                    isSMSSend = await _objprovider.SendSMS(smsDtls);
        //                }
        //                #endregion
        //                //#region Email
        //                bool isMailSend = false;
        //                var param = new Email_Model
        //                {
        //                    Alert_Name = "OTP_for_Registration",
        //                    Operation = "Common"
        //                };
        //                var emailDtls = await _objprovider.Get_Email_Template(param);
        //                if (!string.IsNullOrEmpty(emailDtls.SMTP_Host_Address))
        //                {
        //                    emailDtls.Email_To = (string)messageEF.Mail;
        //                    emailDtls.EmailConf_Body = emailDtls.EmailConf_Body.Replace("{{var}}", messageEF.password).Replace("{{deptName}}", "Directorate of Minor Minerals");
        //                    isMailSend = await _objprovider.SendEmail(emailDtls);
        //                }

        //                //#endregion
        //                if (isSMSSend || isMailSend)
        //                    return Json(messageEF);
        //                else
        //                    return Json("");
        //            }
        //            else
        //            {
        //                return Json("");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return Json("");
        //        }
        //    }
        //    else
        //    {
        //        return Json("");
        //    }
        //}
        //#endregion

        //public ActionResult LogOff()
        //{
        //    HttpContext.Response.Cookies.Delete(".AspNet.SharedCookie");
        //    //HttpContext.Response.Cookies.Delete("Identity.Application");
        //    HttpContext.SignOutAsync("Identity.Application");
        //    HttpContext.Session.Clear();
        //    return RedirectToAction("Index", "Login");
        //}
        //public ActionResult Error()
        //{
        //    return View();
        //}
        //public IActionResult ForgotPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> ForgotPassword(LoginEF forgotPasswordModel)
        //{
        //    //try
        //    //{

        //    //    if (!string.IsNullOrEmpty(forgotPasswordModel.EncryptUserId))
        //    //        forgotPasswordModel.Userid = Convert.ToInt32(ApplicationMainModel.DecryptStringAES(forgotPasswordModel.EncryptUserId, forgotPasswordModel.randomNumber));

        //    //    if (!string.IsNullOrEmpty(forgotPasswordModel.EncryptNewPassword))
        //    //        forgotPasswordModel.NewPassword = ApplicationMainModel.ReturnStrDecryptCode(forgotPasswordModel.EncryptNewPassword);

        //    //    if (!string.IsNullOrEmpty(forgotPasswordModel.EncryptConfirmPassword))
        //    //        forgotPasswordModel.ConfirmPassword = ApplicationMainModel.ReturnStrDecryptCode(forgotPasswordModel.EncryptConfirmPassword);


        //    //    if (forgotPasswordModel.NewPassword == forgotPasswordModel.ConfirmPassword)
        //    //    {
        //    //        forgotPasswordModel.EncryptPassword = ApplicationMainModel.ComputeSha256Hash(forgotPasswordModel.NewPassword).ToUpper();
        //    //        objobjmodel = _loginRepository.ResetPassword(forgotPasswordModel);

        //    //        if (objobjmodel.Satus == "1")
        //    //        {

        //    //            TempData["msg"] = "Password Reset Successfully";
        //    //            return Redirect("/login/index");
        //    //        }
        //    //        else
        //    //        {
        //    //            TempData["msg"] = "Something Went Wrong";
        //    //            return View();
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        TempData["msg"] = "Password does not matched";
        //    //        return View();
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //    throw ex;
        //    //}
        //    //finally
        //    //{

        //    //}

        //    try
        //    {
        //        if (!string.IsNullOrEmpty(forgotPasswordModel.EncryptUserId))
        //            forgotPasswordModel.Userid = Convert.ToInt32(ApplicationMainModel.DecryptStringAES(forgotPasswordModel.EncryptUserId, forgotPasswordModel.randomNumber)); 
        //        //forgotPasswordModel.msg = "VO";
        //        var objResult = _objprovider.VerifyForgotOTP(forgotPasswordModel.Userid,forgotPasswordModel.OTP);
        //        if (objResult.msg == "SUCCESS")
        //        {
        //            if (!string.IsNullOrEmpty(forgotPasswordModel.EncryptNewPassword))
        //                forgotPasswordModel.NewPassword = ApplicationMainModel.DecryptStringAES(forgotPasswordModel.EncryptNewPassword, forgotPasswordModel.randomNumber);

        //            if (!string.IsNullOrEmpty(forgotPasswordModel.EncryptConfirmPassword))
        //                forgotPasswordModel.ConfirmPassword = ApplicationMainModel.DecryptStringAES(forgotPasswordModel.EncryptConfirmPassword, forgotPasswordModel.randomNumber);

        //            if (forgotPasswordModel.NewPassword == forgotPasswordModel.ConfirmPassword)
        //            {
        //                forgotPasswordModel.EncryptPassword = ApplicationMainModel.ComputeSha256Hash(forgotPasswordModel.NewPassword).ToUpper();
        //                objobjmodel = _loginRepository.ResetPassword(forgotPasswordModel);

        //                if (objobjmodel.Satus == "1")
        //                {
        //                    TempData["msg"] = "Password Reset Successfully";
        //                    //return Redirect("/home/index");
        //                    return Redirect("/login/index");
        //                }
        //                else
        //                {
        //                    TempData["msg"] = "Something Went Wrong";
        //                    TempData["alertType"] = "error";
        //                    return View();
        //                }
        //            }
        //            else
        //            {
        //                TempData["msg"] = "Password does not matched";
        //                TempData["alertType"] = "warning";
        //                return View();
        //            }
        //        }
        //        else
        //        {
        //            TempData["msg"] = "This is an invalid OTP. Please send another OTP and try again !";
        //            TempData["alertType"] = "warning";
        //            return View();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}

        //[HttpPost]
        //public async Task<JsonResult> passwordencryption(string oldPassword, string newPassword, string confirmPassword)
        //{
        //    try
        //    {
        //        string encoldPassword = string.Empty;
        //        string encnewPassword = string.Empty;
        //        string encconfirmPassword = string.Empty;

        //        if (!string.IsNullOrEmpty(oldPassword))
        //            encoldPassword = ApplicationMainModel.ReturnStrEncryptCode(oldPassword);

        //        if (!string.IsNullOrEmpty(newPassword))
        //            encnewPassword = ApplicationMainModel.ReturnStrEncryptCode(newPassword);

        //        if (!string.IsNullOrEmpty(confirmPassword))
        //            encconfirmPassword = ApplicationMainModel.ReturnStrEncryptCode(confirmPassword);

        //        return Json(new
        //        {
        //            eoPassword = encoldPassword,
        //            enPassword = encnewPassword,
        //            ecPassword = encconfirmPassword
        //        });
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {

        //    }
        //}

        
     
        //public IActionResult ChangePassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        
        //public IActionResult ChangePassword(ChangePasswordEF objChangePasswordEF)
        //{
        //    #region Validation
        //    if (string.IsNullOrEmpty(objChangePasswordEF.EncryptOldPassword))
        //    {
        //        ModelState.AddModelError("EncryptOldPassword", "Old Password Field is required");
        //    }
        //    else if (string.IsNullOrEmpty(objChangePasswordEF.EncryptNewPassword))
        //    {
        //        ModelState.AddModelError("EncryptNewPassword", "New Password Field is required");
        //    }
        //    else if (string.IsNullOrEmpty(objChangePasswordEF.EncryptConfirmPassword))
        //    {
        //        ModelState.AddModelError("EncryptConfirmPassword", "Confirm Password Field is required");
        //    }
        //    #endregion
        //    if (ModelState.IsValid)
        //    {
        //        objChangePasswordEF.OldPassword = ApplicationMainModel.DecryptStringAES(objChangePasswordEF.EncryptOldPassword, objChangePasswordEF.randomNumber);
        //        objChangePasswordEF.NewPassword = ApplicationMainModel.DecryptStringAES(objChangePasswordEF.EncryptNewPassword, objChangePasswordEF.randomNumber);
        //        objChangePasswordEF.ConfirmPassword = ApplicationMainModel.DecryptStringAES(objChangePasswordEF.EncryptConfirmPassword, objChangePasswordEF.randomNumber);

        //        if (objChangePasswordEF.NewPassword == objChangePasswordEF.ConfirmPassword)
        //        {
        //            objChangePasswordEF.UserId = HttpContext.Session.GetInt32("UserId");
        //            string curpassword = HttpContext.Session.GetString("CurPassword");
        //            objChangePasswordEF.EncryptPassword = ApplicationMainModel.ComputeSha256Hash(objChangePasswordEF.NewPassword).ToUpper();
        //            if (curpassword != objChangePasswordEF.OldPassword)
        //            {
        //                TempData["Message"] = "2";
        //            }
        //            else
        //            {
        //                objobjmodel = _loginRepository.ChangePassword(objChangePasswordEF);
        //                TempData["Message"] = objobjmodel.Satus;
        //            }
        //        }
        //    }
        //    return View(objChangePasswordEF);
        //}
    }
}
#endregion

