using DiscountManagement.Applicatiom.Contract.CustomerDiscount;
using DiscountManagement.Application;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EfCore;
using DiscountManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace DiscountManagement.Configuration
{
    public static class DiscountManagementBooststrapper
    {
        public static IServiceCollection AddDiscountManagementServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
            services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();


            services.AddDbContext<DiscountContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
