using Edenred.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace EdenredAPI
{
    public static class DependenciesRegistration
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<EdenredDbContext>(e => e.UseSqlServer(Configuration.GetConnectionString("TechStoreConnection")));

            //Services

            //Repositories
        }
    }
}
