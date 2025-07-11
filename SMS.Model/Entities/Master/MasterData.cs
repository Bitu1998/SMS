namespace SMS.Model.Entities.Master
{
    public class MasterData
    {
       
    }
    public class BrandMaster
    {
        public int BrandID { get; set; }
        public string? BrandName { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class ProductCategoriesMaster
    {
        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class ProductSubCategoriesMaster
    {
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public string? SubCategoryName { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class FlavorMaster
    {
        public int FlavorID { get; set; }
        public string? FlavorName { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class UnitMaster
    {
        public int UnitID { get; set; }
        public string? UnitName { get; set; }
    }
    public class PackageTypeMaster 
    {
        public int PackageTypeID { get; set; }
        public string? PackageTypeName { get; set; }
    }
    public class PackSizesMaster
    {
        public int PackSizeID { get; set; } 
        public decimal Quantity { get; set; } 
    }
    public class ProductsMaster
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
    }
    public class ImageMaster
    {
        public int ImageID { get; set; }
        public string? ImageName { get; set; }
        public string? ImageGeneratedName { get; set; }
        public string? CreatedBy { get; set; }
    }

}

