using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model.Entities.Master
{
    public class TransactionMaster
    {
        public int VariantID { get; set; }
        public int BrandID { get; set; }
        public string? BrandName { get; set; }
        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public int SubCategoryID { get; set; }
        public string? SubCategoryName { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int FlavorId { get; set; }
        public string? FlavorName { get; set; }
        public int PackSizeID { get; set; }
        public int unitid { get; set; }
        public string? Unit { get; set; }
        public int PackageTypeID { get; set; }
        public string? PackageTypeName { get; set; }
        public int ImageID { get; set; }
        public string? ImageName { get; set; }
        public string? ImageGeneratedName { get; set; }
        public string? PurchasePrice { get; set; }
        public string? SellPrice { get; set; }
        public string? BatchNo { get; set; }
        public string? MFGDate { get; set; }
        public string? EXPDate { get; set; }
        public string? AvailableQuantity { get; set; }

        public string? CreatedBy { get; set; }
    }
}
