using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Domain;
using SMS.Model.Login;
using SMS.Repository.BaseRepository;

namespace SMS.Repository.Login
{
    public interface ILoginRepository : IDisposable, ISMSRepositoryBase
    {
        public LoginEF authentication(LoginEF objLoginpara, string strIpAddress, int intSessionToken);

        public LoginEF GetUserDetails(LoginEF objLoginpara);

        Task<LoginEF> GetUserType(LoginEF loginModel);

        MessageEF ResetPassword(LoginEF forgotPasswordModel);
        string ErrorList(LogEntry objLogEntry);
        MessageEF ChangePassword(ChangePasswordEF ChangePasswordModel);
    }
}
