using _0_Framework.Infrastructure;
using _01_LampshadeQuery.Contracts.Inventory;
using _01_LampshadeQuery.Query;
using InventoryManagement.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Configuration.Permissions;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Configuration
{
    public static class InventoryBootstrapper
    {
        public static IServiceCollection AddInventoryManagementServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IInventoryApplication, InventoryApplication>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();

            services.AddTransient<IInventoryQuery, InventoryQuery>();

            services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();

            services.AddDbContext<InventoryContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
