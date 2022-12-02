using System.Runtime.CompilerServices;

using Api.Interfaces;
using Api.Services;
using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
             services.AddDbContext<DataContext>(opt =>
              {
                  opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
              });

            services.AddCors();
             services.AddScoped<ITokenService , TokenService>();
             return services;

        }
        
    }
}