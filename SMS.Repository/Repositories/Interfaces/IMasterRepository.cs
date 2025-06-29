using SMS.Model.Entities.Master;

namespace SMS.Repository.Repositories.Interfaces
{
    public interface IMasterRepository
    {
        Task<List<BrandMaster>> GetAllBrand();
        Task<List<FlavorMaster>> GetAllFlavors();  //e.g like flavour masala,kola,lemon
        Task<List<PackageTypeMaster>> GetAllPackageType(); //e.g like glass,plastic bottle
        Task<List<PackSizesMaster>> GetAllPackSize(int unitid); //depend on unit like 100ml,1l
        Task<List<ProductCategoriesMaster>> GetAllCategories(); // e.g like baverage,snacks 
        Task<List<ProductsMaster>> GetAllProducts(int subcategoryid, int brandid); //depend and subcategory
        Task<List<ProductSubCategoriesMaster>> GetAllSubCategories(int categoryid); //depeend upon categories
        Task<List<UnitMaster>> GetAllUnits(); //e.g like ml,g,liter,kg
        Task<List<ImageMaster>> GetAllImage(); //e.g like image
    }
}
