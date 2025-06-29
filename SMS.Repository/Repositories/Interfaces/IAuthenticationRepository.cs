using CTMS.Model.Entities.Master;
using SMS.Model.DTOs;
using SMS.Model.Entities.Registration;
namespace SMS.Repository.Repositories.Interfaces
{
    public interface IAuthenticationRepository
    {
        
        UsersDto? Login(LoginDto Log, out int returnVal);
        Task<int> Registration(UsersDto user);
        Task<List<UsersDto>> ViewNewRegistration();
        
    }
}
