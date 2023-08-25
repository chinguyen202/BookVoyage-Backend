using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookVoyage.Persistence.Data;
using BookVoyage.Application.Common.Interfaces;

namespace BookVoyage.Persistence.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(option => 
            option.UseNpgsql(config.GetConnectionString("DefaultConnection")));
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        return services;
    }
}