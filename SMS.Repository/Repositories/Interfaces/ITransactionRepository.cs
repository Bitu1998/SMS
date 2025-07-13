using SMS.Model.DTOs;
using SMS.Model.Entities.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Repository.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<TransactionMaster>> GetAllProduct();
        Task<List<Shipment>> Get_Product();
        Task<List<ShipmentDetails>> getproductdetails(Shipment user);
        Task<int> AddProduct(TransactionMaster user);
        Task<int> ShipProduct(ShipmentBatchCompact user);
    }
}
