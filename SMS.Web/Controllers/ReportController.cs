using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SMS.Core;
using SMS.Model.Entities.Master;
using SMS.Repository.Repositories.Interfaces;
using SMS.Repository.Repositories.Repository;

namespace SMS.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportRepository _ReportRepository;
        public ReportController(IReportRepository ReportRepository)
        {
            _ReportRepository = ReportRepository;
        }
        public IActionResult ShipmentReport()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetShipmentDetails()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                if (User.FindFirst("UID")?.Value != null)
                {
                    try
                    {
                        var objDistlist = await _ReportRepository.GetAllShipmentDetails();
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
