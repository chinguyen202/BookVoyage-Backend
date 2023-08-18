using BookVoyage.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookVoyage.Persistence.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDataContext(config);
    }

    public static void AddDataContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString,
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        });
    }
}