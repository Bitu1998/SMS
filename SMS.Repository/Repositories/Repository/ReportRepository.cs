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
    public class ReportRepository : SMSRepositoryBase,IReportRepository
    {
        public ReportRepository(ISMSConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }
        public async Task<List<Report>> GetAllShipmentDetails()
        {
            DynamicParameters? objParam = new DynamicParameters();
            objParam.Add("@P_Command", "GAS");
            objParam.Add("@IsSuccess", dbType: DbType.Int32, direction: ParameterDirection.Output);
            objParam.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
            var results = await Connection.QueryAsync<Report>("SP_Report", objParam, commandType: CommandType.StoredProcedure);
            return results.ToList();
        }
    }
}
