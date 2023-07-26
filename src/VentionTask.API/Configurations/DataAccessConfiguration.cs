using Microsoft.EntityFrameworkCore;
using System;
using VentionTask.DAL.AppDbContexts;

namespace VentionTask.API.Configurations
{
    public static class DataAccessConfiguration
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DatabaseConnection")!;
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Scoped);
        }
    }
}
