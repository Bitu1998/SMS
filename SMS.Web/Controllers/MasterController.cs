using CTMS.Model.Entities.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMS.Core;
using SMS.Model.Entities.Master;
using SMS.Repository.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SMS.Web.Controllers
{
    public class MasterController : Controller
    {
        private readonly IMasterRepository _masterService;
        private readonly IMemCache _memCache;
        public MasterController(IMasterRepository masterService, IMemCache memCache)
        {
            _masterService = masterService;
            _memCache = memCache;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBrand()
        {
            try
            {
                var BrandcacheKey = "BrandList";
                List<BrandMaster> objCacheDistlist = _memCache.GetCache<List<BrandMaster>>(BrandcacheKey);
                if (objCacheDistlist != null)
                {
                    return Content(new AjaxResult { state = ResultType.success.ToString(), data = objCacheDistlist }.ToJson());
                }
                else
                {
                    var objDistlist =await _masterService.GetAllBrand();
                    if (objDistlist != null)
                    {
                        _memCache.SetCache(objDistlist, BrandcacheKey);
                        return Content(new AjaxResult { state = ResultType.success.ToString(), data = objDistlist }.ToJson());
                    }
                    else
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Error occurred while showing" }.ToJson());
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> GetCategory()
        {

            try
            {
                var CatcacheKey = "CatList";
                List<ProductCategoriesMaster> objCacheDistlist = _memCache.GetCache<List<ProductCategoriesMaster>>(CatcacheKey);
                if (objCacheDistlist != null)
                {
                    return Content(new AjaxResult { state = ResultType.success.ToString(), data = objCacheDistlist }.ToJson());
                }
                else
                {
                    var objDistlist =await _masterService.GetAllCategories();
                    if (objDistlist != null)
                    {
                        _memCache.SetCache(objDistlist, CatcacheKey);
                        return Content(new AjaxResult { state = ResultType.success.ToString(), data = objDistlist }.ToJson());
                    }
                    else
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Error occurred while showing" }.ToJson());
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }

        }
        [AllowAnonymous]
        public async Task<IActionResult> GetFlavors()
        {
            try
            {
                var FlavcacheKey = "FlavList";
                List<FlavorMaster> objCacheDistlist = _memCache.GetCache<List<FlavorMaster>>(FlavcacheKey);
                if (objCacheDistlist != null)
                {
                    return Content(new AjaxResult { state = ResultType.success.ToString(), data = objCacheDistlist }.ToJson());
                }
                else
                {
                    var objDistlist = await _masterService.GetAllFlavors();
                    if (objDistlist != null)
                    {
                        _memCache.SetCache(objDistlist, FlavcacheKey);
                        return Content(new AjaxResult { state = ResultType.success.ToString(), data = objDistlist }.ToJson());
                    }
                    else
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Error occurred while showing" }.ToJson());
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }

        }
        [AllowAnonymous]
        public async Task<IActionResult> GetUnits()
        {
            try
            {
                var UnitcacheKey = "UnitList";
                List<UnitMaster> objCacheDistlist = _memCache.GetCache<List<UnitMaster>>(UnitcacheKey);
                if (objCacheDistlist != null)
                {
                    return Content(new AjaxResult { state = ResultType.success.ToString(), data = objCacheDistlist }.ToJson());
                }
                else
                {
                    var objDistlist = await _masterService.GetAllUnits();
                    if (objDistlist != null)
                    {
                        _memCache.SetCache(objDistlist, UnitcacheKey);
                        return Content(new AjaxResult { state = ResultType.success.ToString(), data = objDistlist }.ToJson());
                    }
                    else
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Error occurred while showing" }.ToJson());
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }

        }
        [AllowAnonymous]
        public async Task<IActionResult> GetPackageType()
        {
            try
            {
                var PackageTypecacheKey = "PackageTypeList";
                List<PackageTypeMaster> objCacheDistlist = _memCache.GetCache<List<PackageTypeMaster>>(PackageTypecacheKey);
                if (objCacheDistlist != null)
                {
                    return Content(new AjaxResult { state = ResultType.success.ToString(), data = objCacheDistlist }.ToJson());
                }
                else
                {
                    var objDistlist = await _masterService.GetAllPackageType();
                    if (objDistlist != null)
                    {
                        _memCache.SetCache(objDistlist, PackageTypecacheKey);
                        return Content(new AjaxResult { state = ResultType.success.ToString(), data = objDistlist }.ToJson());
                    }
                    else
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Error occurred while showing" }.ToJson());
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }

        }
        [AllowAnonymous]
        public async Task<IActionResult> GetSubCategory(int CatId = 0)
        {

            try
            {
                var SubCatcacheKey = CatId.ToString();
                List<ProductSubCategoriesMaster> objCacheDistlist = _memCache.GetCache<List<ProductSubCategoriesMaster>>(SubCatcacheKey);
                if (objCacheDistlist != null && objCacheDistlist.Count != 0)
                {
                    return Content(new AjaxResult { state = ResultType.success.ToString(), data = objCacheDistlist }.ToJson());
                }
                else
                {
                    var objDistlist =await _masterService.GetAllSubCategories(CatId);
                    if (objDistlist != null)
                    {
                        _memCache.SetCache(objDistlist, SubCatcacheKey);
                        return Content(new AjaxResult { state = ResultType.success.ToString(), data = objDistlist }.ToJson());
                    }
                    else
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Error occurred while showing" }.ToJson());
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }

        }
        [AllowAnonymous]
        public async Task<IActionResult> GetPackSize(int UnitId = 0)
        {

            try
            {
                var PacksizeCatcacheKey = UnitId.ToString();
                List<PackSizesMaster> objCacheDistlist = _memCache.GetCache<List<PackSizesMaster>>(PacksizeCatcacheKey);
                if (objCacheDistlist != null && objCacheDistlist.Count != 0)
                {
                    return Content(new AjaxResult { state = ResultType.success.ToString(), data = objCacheDistlist }.ToJson());
                }
                else
                {
                    var objDistlist = await _masterService.GetAllPackSize(UnitId);
                    if (objDistlist != null)
                    {
                        _memCache.SetCache(objDistlist, PacksizeCatcacheKey);
                        return Content(new AjaxResult { state = ResultType.success.ToString(), data = objDistlist }.ToJson());
                    }
                    else
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Error occurred while showing" }.ToJson());
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }

        }
        [AllowAnonymous]
        public async Task<IActionResult> GetProduct(int subcategory = 0,int brandid=0)
        {

            try
            {
                var ProductCatcacheKey = subcategory.ToString();
                List<ProductsMaster> objCacheDistlist = _memCache.GetCache<List<ProductsMaster>>(ProductCatcacheKey);
                if (objCacheDistlist != null && objCacheDistlist.Count != 0)
                {
                    return Content(new AjaxResult { state = ResultType.success.ToString(), data = objCacheDistlist }.ToJson());
                }
                else
                {
                    var objDistlist = await _masterService.GetAllProducts(subcategory,brandid);
                    if (objDistlist != null)
                    {
                        _memCache.SetCache(objDistlist, ProductCatcacheKey);
                        return Content(new AjaxResult { state = ResultType.success.ToString(), data = objDistlist }.ToJson());
                    }
                    else
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Error occurred while showing" }.ToJson());
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }

        }
        [AllowAnonymous]
        public async Task<IActionResult> GetImage()
        {
            try
            {
                var ImagecacheKey = "ImageList";
                List<ImageMaster> objCacheDistlist = _memCache.GetCache<List<ImageMaster>>(ImagecacheKey);
                if (objCacheDistlist != null)
                {
                    return Content(new AjaxResult { state = ResultType.success.ToString(), data = objCacheDistlist }.ToJson());
                }
                else
                {
                    var objDistlist = await _masterService.GetAllImage();
                    if (objDistlist != null)
                    {
                        _memCache.SetCache(objDistlist, ImagecacheKey);
                        return Content(new AjaxResult { state = ResultType.success.ToString(), data = objDistlist }.ToJson());
                    }
                    else
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Error occurred while showing" }.ToJson());
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }

        }
    }
}
