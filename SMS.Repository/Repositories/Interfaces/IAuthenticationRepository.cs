
using SMS.Model.DTOs;
namespace SMS.Repository.Repositories.Interfaces
{
    public interface IAuthenticationRepository
    {
        
        UsersDto? Login(LoginDto Log, out int returnVal);
        Task<int> Registration(UsersDto user);
        Task<List<UsersDto>> ViewNewRegistration();
        
    }
}
