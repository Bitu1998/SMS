using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model.Entities.Master
{
    public class Shipment: ShipmentDetails
    {
        public int ProductID { get; set; }
        public int FlavorID { get; set; }
        public int PackSizeID { get; set; }
        public int PackageTypeID { get; set; }
        public string? DisplayName { get; set; }
        public int TotalAvailableQuantity { get; set; }
        public string? CreatedBy { get; set; }
        public int ShipQuantity { get; set; }

    }
    public class ShipmentDetails
    {
        public int VariantID { get; set; }
        public string? BatchNo { get; set; }
        public string? AvailableQuantity { get; set; }
        public string? EXPDate { get; set; }
    }
    public class ShipmentBatchCompact
    {
        public string? VariantShipString { get; set; } // "12_100,11_50"
        public string? CreatedBy { get; set; }
    }
}
