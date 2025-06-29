
using Dapper;
using SMS.Model.DTOs;
using SMS.Repository.BaseRepository;
using SMS.Repository.Factory;
using SMS.Repository.Repositories.Interfaces;
using System.Data;

namespace SMS.Repository.Repository
{
    public class AuthenticationRepository : SMSRepositoryBase, IAuthenticationRepository
    {
        public AuthenticationRepository(ISMSConnectionFactory SMSConnectionFactory) : base(SMSConnectionFactory)
        {

        }

        
        public UsersDto? Login(LoginDto Log, out int returnVal)
        {
            try
            {
                // Create the parameters
                var p = new DynamicParameters();

                p.Add("@UserID", Log.USERNAME);
                p.Add("@Password", Log.UPASSWORD);
                p.Add("@IsSuccess", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                UsersDto? userDetails = Connection.Query<UsersDto>("SP_LoginUser", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                returnVal = p.Get<int>("IsSuccess");
                return userDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> Registration(UsersDto user)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@FullName", user.FullName);
                p.Add("@MobileNo", user.MobileNo);
                p.Add("@Email", user.Email);
                p.Add("@UserID", user.UserID);
                p.Add("@Password", user.Password);
                p.Add("@Address", user.Address);
                p.Add("@CreatedBy", user.CreatedBy);
                p.Add("@IsSuccess", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                await Connection.ExecuteAsync("SP_RegisterUser", p, commandType: CommandType.StoredProcedure);
                int returnVal = p.Get<int>("@IsSuccess");
                return returnVal;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<UsersDto>> ViewNewRegistration()
        {
            try
            {
                
                DynamicParameters objParam = new DynamicParameters();
                objParam.Add("@IsSuccess", dbType: DbType.Int32, direction: ParameterDirection.Output);
                objParam.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                var results = await Connection.QueryAsync<UsersDto>("SP_GetUserDetails", objParam, commandType: CommandType.StoredProcedure);
                return results.AsList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
