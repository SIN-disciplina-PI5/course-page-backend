using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UNICAP.SiteCurso.Application.Interfaces;
using UNICAP.SiteCurso.Infrastructure.Context;

namespace UNICAP.SiteCurso.Infrastructure.IoC
{
    public static class InfrastructureExtensions
    {
        public static void InfrastructureRegister(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EFContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("SqlConnection"));
                //options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
                options.UseLazyLoadingProxies();
            });

            services.AddTransient<IEFContext, EFContext>();
        }
    }
}
