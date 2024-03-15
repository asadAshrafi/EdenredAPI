using Edenred.DataAccess.Context;
using Edenred.DataAccess.Repositories;
using Edenred.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace EdenredAPI
{
    public static class DependenciesRegistration
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<EdenredDbContext>(e => e.UseSqlServer(Configuration.GetConnectionString("TechStoreConnection")));
            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExpenditureRepository, ExpenditureRepository>();
            services.AddScoped<IBeneficiaryRepository, BeneficiaryRepository>();
            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IBeneficiaryService, BeneficiaryService>();
            services.AddScoped<CallAPIService>();
        }
    }
}
