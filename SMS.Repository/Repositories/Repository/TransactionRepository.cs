using Dapper;
using SMS.Model.DTOs;
using SMS.Model.Entities.Master;
using SMS.Repository.BaseRepository;
using SMS.Repository.Factory;
using SMS.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Repository.Repositories.Repository
{
    public class TransactionRepository : SMSRepositoryBase, ITransactionRepository
    {
        public TransactionRepository(ISMSConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
        public async Task<List<TransactionMaster>> GetAllProduct()
        {
            DynamicParameters? objParam = new DynamicParameters();
            objParam.Add("@P_Command", "GAD");
            objParam.Add("@IsSuccess", dbType: DbType.Int32, direction: ParameterDirection.Output);
            objParam.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
            var results = await Connection.QueryAsync<TransactionMaster>("SP_Transaction", objParam, commandType: CommandType.StoredProcedure);
            return results.ToList();
        }
        public async Task<int> AddProduct(TransactionMaster user)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@P_Command", "I");
                p.Add("@ProductID", user.ProductID);
                p.Add("@PackSizeID", user.PackSizeID);
                p.Add("@FlavorId", user.FlavorId);
                p.Add("@PackageTypeID", user.PackageTypeID);
                p.Add("@PurchasePrice", decimal.TryParse(user.PurchasePrice, out var purchasePrice) ? purchasePrice : 0);
                p.Add("@SellPrice", decimal.TryParse(user.SellPrice, out var sellPrice) ? sellPrice : 0);
                p.Add("@BatchNo", user.BatchNo);

                p.Add("@MFGDate", DateTime.TryParse(user.MFGDate, out var mfgDate) ? mfgDate : (object)DBNull.Value);
                p.Add("@EXPDate", DateTime.TryParse(user.EXPDate, out var expDate) ? expDate : (object)DBNull.Value);
                p.Add("@AvailableQuantity", int.TryParse(user.AvailableQuantity, out var qty) ? qty : 0);

               
                p.Add("@ImageID", user.ImageID);
                p.Add("@CreatedBy", user.CreatedBy);
                p.Add("@IsSuccess", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                await Connection.ExecuteAsync("SP_Transaction", p, commandType: CommandType.StoredProcedure);
                int returnVal = p.Get<int>("@IsSuccess");
                return returnVal;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Shipment>> Get_Product()
        {
            DynamicParameters? objParam = new DynamicParameters();
            objParam.Add("@P_Command", "GAS");
            objParam.Add("@IsSuccess", dbType: DbType.Int32, direction: ParameterDirection.Output);
            objParam.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
            var results = await Connection.QueryAsync<Shipment>("SP_Shipment", objParam, commandType: CommandType.StoredProcedure);
            return results.ToList();
        }
        public async Task<List<ShipmentDetails>> getproductdetails(Shipment user)
        {
            DynamicParameters? p = new DynamicParameters();
            p.Add("@P_Command", "GPD");
            p.Add("@ProductID", user.ProductID);
            p.Add("@PackSizeID", user.PackSizeID);
            p.Add("@FlavorId", user.FlavorID);
            p.Add("@PackageTypeID", user.PackageTypeID);
            p.Add("@IsSuccess", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
            var results = await Connection.QueryAsync<ShipmentDetails>("SP_Shipment", p, commandType: CommandType.StoredProcedure);
            return results.ToList();
        }
        public async Task<int> ShipProduct(ShipmentBatchCompact user)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@P_Command", "S"); // 'S' = Ship logic in SP
                p.Add("@VariantShipString", user.VariantShipString); // e.g., "12_100,11_50"
                p.Add("@CreatedBy", user.CreatedBy);
                p.Add("@IsSuccess", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);

                await Connection.ExecuteAsync("SP_Transaction", p, commandType: CommandType.StoredProcedure);

                int returnVal = p.Get<int>("@IsSuccess");
                return returnVal;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
