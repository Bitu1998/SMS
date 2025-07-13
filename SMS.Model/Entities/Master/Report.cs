using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model.Entities.Master
{
    public class Report
    {
        public int VariantID { get; set; }
        public string? ProductDisplayName { get; set; }
        public string? ImageName { get; set; }
        public string? ImageGeneratedName { get; set; }
        public int PurchasePrice { get; set; }
        public int SellPrice { get; set; }
        public string? BatchNo { get; set; }
        public string? MFGDate { get; set; }
        public string? EXPDate { get; set; }
        public string? ShippedDate { get; set; }
        public string? CreatedBy { get; set; }
        public int AvailableQuantity { get; set; }
        public int ShippedQty { get; set; }
        public int TotalSellValue { get; set; }

    }
}
