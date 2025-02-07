using EStore.Application.IRepositories;
using EStore.Infrastructure.Data;
using EStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EStore.Infrastructure
{
    public static class DependencyInjection
    {
        //public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        //{
        //    services.AddDbContext<EStoreDbContext>(options =>
        //    {
        //        options.UseSqlServer("Server=10.90.1.27;Initial Catalog=EStoreClothing;User Id=pidc2225;Password=sql@shaik123;TrustServerCertificate=True;");
        //    });
        //    services.AddScoped<IProductRepository, ProductRepository>();

        //    return services;
        //}
    }
}
