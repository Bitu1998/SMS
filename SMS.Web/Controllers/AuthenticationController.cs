
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SMS.Core;
using SMS.Model.DTOs;
using SMS.Repository.Repositories.Interfaces;
using System.Security.Claims;
using System.Xml.Linq;
#pragma warning disable SCS0016, CS1998
namespace SMS.Web
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationRepository _AuthRepository;
        private readonly IWebHostEnvironment _env;
        public AuthenticationController(IAuthenticationRepository AuthRepository, IWebHostEnvironment env)
        {
            _AuthRepository = AuthRepository;
            _env = env;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn()
        {
            try
            {
                this.Request.HttpContext.Session.Clear();
                await HttpContext.SignOutAsync(SysManageAuthAttribute.SysManageAuthScheme);
                return View();
            }
            catch (Exception ex)
            {
                CommonHelper.LogError(ex, "Loginload", Path.Combine(_env.WebRootPath, "Log"));
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginDto user)
        {
            try
            {
                string captcha = AESEncrytDecry.DecryptStringAES(user.captcha!);
                if (!GenerateCaptcha.ValidateCaptchaCode(captcha, HttpContext))
                {
                    return Json(new AjaxResult { state = ResultType.warning.ToString(), message = "Invalid Captcha, please re-enter!", data = 300 });
                }
                else
                {
                    int retVal = 0;
                    user.USERNAME = AESEncrytDecry.DecryptStringAES(user.USERNAME!).ToString();
                    user.UPASSWORD = AESEncrytDecry.DecryptStringAES(user.UPASSWORD!).ToString();
                    var loginDetail = _AuthRepository.Login(user, out retVal);
                    if (loginDetail != null)
                    {
                        
                        if (loginDetail.Password == user.UPASSWORD)
                        {
                            await generateClaim(loginDetail);
                            return Json(new AjaxResult { state = ResultType.success.ToString(), message = "Sign in successful.", data = 200 });
                        }
                        else
                        {
                            return Json(new AjaxResult { state = ResultType.warning.ToString(), message = "Invalid Password !", data = 101 });
                        }
                    }
                    else
                    {
                        return Content(new AjaxResult { state = ResultType.warning.ToString(), message = "Invalid Username !" }.ToJson());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonHelper.LogError(ex, "Login", Path.Combine(_env.WebRootPath, "Log"));
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }
        public async Task generateClaim(UsersDto user)
        {
            var identity = new ClaimsIdentity(SysManageAuthAttribute.SysManageAuthScheme);  // Specify the authentication type
            List<Claim> claims = new List<Claim>(){
                            new Claim("MOBILENO",user.MobileNo!.ToString()),
                            new Claim("Email" , user.Email!.ToString()),
                            new Claim(ClaimTypes.Role, user.IsAdmin.ToString()),
                           
                            new Claim("Userid", user.UserID!.ToString()),
                            new Claim("Uid", user.ID.ToString()),
                            new Claim("FULLNAME", user.FullName !.ToString()),
                            
                            new Claim("Address",user.Address !.ToString()),
                        };
            var isSystem = false;
            identity.AddClaims(claims);
            identity.AddClaim(new Claim(ClaimTypes.IsPersistent, isSystem.ToString()));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(SysManageAuthAttribute.SysManageAuthScheme, principal);
        }
        [HttpGet]
        public async Task<IActionResult> OutLogin()
        {

            this.Request.HttpContext.Session.Clear();
            Response.Cookies.Delete(CookieRequestCultureProvider.DefaultCookieName);
            await HttpContext.SignOutAsync(SysManageAuthAttribute.SysManageAuthScheme);
            if (!Response.Headers.ContainsKey("Cache-Control"))
            {
                Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1
            }
            if (!Response.Headers.ContainsKey("Pragma"))
            {
                Response.Headers.Add("Pragma", "no-cache"); // HTTP 1.0
            }
            if (!Response.Headers.ContainsKey("Expires"))
            {
                Response.Headers.Add("Expires", "0"); // Proxies
            }
            return RedirectToAction("LogIn", "Authentication");
        }
        [Route("get-captcha-image")]
        public IActionResult GetImage()
        {
            try
            {
                int width = 90;
                int height = 25;

                var captchaCode = GenerateCaptcha.Captcha();
                var captchaByteData = GenerateCaptcha.GenerateCaptchaImage(width, height, captchaCode);

                HttpContext.Session.SetString("CaptchaCode", captchaCode);

                return File(captchaByteData, "image/png");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(UsersDto user)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                if (User.FindFirst("UID")?.Value != null)
                {
                    try
                    {
                        user.CreatedBy =User.FindFirst("FULLNAME")?.Value;
                        int retval = await _AuthRepository.Registration(user);
                        if (retval == 1)
                        {
                            return Content(new AjaxResult { state = ResultType.success.ToString(), message = "User Details Added Successfully", data = retval }.ToJson());
                        }
                        else if (retval == 0)
                        {
                            return Content(new AjaxResult { state = ResultType.success.ToString(), message = "Record Already Exists!!!", data = retval }.ToJson());
                        }
                        else
                        {
                            return Content(new AjaxResult { state = ResultType.success.ToString(), message = "Error In Adding Troupe Details", data = retval }.ToJson());
                        }
                    }
                    catch (Exception ex)
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
                    }
                }
                else
                {
                    return View("UnauthorizedAccess");
                }
            }
            else
            {
                return RedirectToAction("SessionOut", "Home");
            }

        }
        [HttpGet]
        public IActionResult ViewNewRegistration()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                if (User.FindFirst("UID")?.Value != null)
                {
                    
                    return View();
                }
                else
                {
                    return View("UnauthorizedAccess");
                }
            }
            else
            {
                return RedirectToAction("SessionOut", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ViewNewRegistration(UsersDto TRV)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                if (User.FindFirst("UID")?.Value != null)
                {
                    try
                    {
                        IList<UsersDto> objDistlist = await _AuthRepository.ViewNewRegistration();
                        if (objDistlist.Count != 0)
                        {

                            return Content(new AjaxResult { state = ResultType.success.ToString(), data = objDistlist }.ToJson());
                        }
                        else
                        {
                            return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Error occurred while showing", data = "" }.ToJson());
                        }
                    }
                    catch (Exception ex)
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
                    }
                }
                else
                {
                    return View("UnauthorizedAccess");
                }
            }
            else
            {
                return RedirectToAction("SessionOut", "Home");
            }

        }

    }
}
