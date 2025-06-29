using CTMS.Model.Entities.AdminMaster;
using CTMS.Model.Entities.Master;
using Dapper;
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
    public class MasterRepository: SMSRepositoryBase,IMasterRepository
    {
        public MasterRepository(ISMSConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public async Task<List<BrandMaster>> GetAllBrand()
        {
            DynamicParameters? objParam = new DynamicParameters();
            objParam.Add("@P_Command", "B");
            var results = await Connection.QueryAsync<BrandMaster>("SP_Master", objParam, commandType: CommandType.StoredProcedure);
            return results.ToList();
        }

        public async Task<List<ProductCategoriesMaster>>GetAllCategories()
        {
            DynamicParameters? objParam = new DynamicParameters();
            objParam.Add("@P_Command", "C");
            var results = await Connection.QueryAsync<ProductCategoriesMaster>("SP_Master", objParam, commandType: CommandType.StoredProcedure);
            return results.ToList();
        }

        public async Task<List<FlavorMaster>> GetAllFlavors()
        {
            DynamicParameters? objParam = new DynamicParameters();
            objParam.Add("@P_Command", "F");
            var results = await Connection.QueryAsync<FlavorMaster>("SP_Master", objParam, commandType: CommandType.StoredProcedure);
            return results.ToList();
        }

        public async Task<List<PackageTypeMaster>> GetAllPackageType()
        {
            DynamicParameters? objParam = new DynamicParameters();
            objParam.Add("@P_Command", "PT");
            var results = await Connection.QueryAsync<PackageTypeMaster>("SP_Master", objParam, commandType: CommandType.StoredProcedure);
            return results.ToList();
        }

        public async Task<List<PackSizesMaster>> GetAllPackSize(int unitid)
        {
            DynamicParameters? objParam = new DynamicParameters();
            objParam.Add("@P_Command", "PS");
            objParam.Add("@P_UnitID", unitid);
            var results = await Connection.QueryAsync<PackSizesMaster>("SP_Master", objParam, commandType: CommandType.StoredProcedure);
            return results.ToList();
        }

        public async Task<List<ProductsMaster>> GetAllProducts(int subcategoryid, int brandid)
        {
            DynamicParameters? objParam = new DynamicParameters();
            objParam.Add("@P_Command", "P");
            objParam.Add("@P_subcategoryid", subcategoryid);
            objParam.Add("@P_brandid", brandid);
            var results = await Connection.QueryAsync<ProductsMaster>("SP_Master", objParam, commandType: CommandType.StoredProcedure);
            return results.ToList();
        }

        public async Task<List<ProductSubCategoriesMaster>>GetAllSubCategories(int categoryid)
        {
            DynamicParameters? objParam = new DynamicParameters();
            objParam.Add("@P_Command", "SC");
            objParam.Add("@P_categoryid", categoryid);
            var results = await Connection.QueryAsync<ProductSubCategoriesMaster>("SP_Master", objParam, commandType: CommandType.StoredProcedure);
            return results.ToList();
        }

        public async Task<List<UnitMaster>> GetAllUnits()
        {
            DynamicParameters? objParam = new DynamicParameters();
            objParam.Add("@P_Command", "U");
            var results = await Connection.QueryAsync<UnitMaster>("SP_Master", objParam, commandType: CommandType.StoredProcedure);
            return results.ToList();
        }
        public async Task<List<ImageMaster>> GetAllImage()
        {
            DynamicParameters? objParam = new DynamicParameters();
            objParam.Add("@P_Command", "IM");
            var results = await Connection.QueryAsync<ImageMaster>("SP_Master", objParam, commandType: CommandType.StoredProcedure);
            return results.ToList();
        }
    }
}
