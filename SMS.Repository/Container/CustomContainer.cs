using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMS.Repository.Factory;
using SMS.Repository.Repositories.Interfaces;
using SMS.Repository.Repositories.Repository;
using SMS.Repository.Repository;

namespace SMS.Repository.Container
{
    public static class CustomContainer
    {
        public static void AddCustomContainer(this IServiceCollection services, IConfiguration configuration)
        {
            ISMSConnectionFactory SMSconnectionFactory = new SMSConnectionFactory(configuration.GetConnectionString("DBconnectionSMS")!);
            services.AddSingleton<ISMSConnectionFactory>(SMSconnectionFactory);
            
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IMasterRepository, MasterRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();

           

        }
    }
}
