using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMS.Core;
using SMS.Model.DTOs;
using SMS.Model.Entities.Master;
using SMS.Repository.Repositories.Interfaces;

namespace SMS.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ITransactionRepository _TransactionRepository;
        public ProductController(ITransactionRepository TransactionRepository)
        {
            _TransactionRepository= TransactionRepository;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(TransactionMaster user)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                if (User.FindFirst("UID")?.Value != null)
                {
                    try
                    {
                        user.CreatedBy = User.FindFirst("FULLNAME")?.Value;
                        int retval = await _TransactionRepository.AddProduct(user);
                        if (retval == 1)
                        {
                            return Content(new AjaxResult { state = ResultType.success.ToString(), message = "Product Added Successfully", data = retval }.ToJson());
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
        public IActionResult ViewProduct()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ViewProduct(UsersDto TRV)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                if (User.FindFirst("UID")?.Value != null)
                {
                    try
                    {
                        var objDistlist = await _TransactionRepository.GetAllProduct();
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
