using BookVoyage.Domain.Entities;
using BookVoyage.Persistence.Data;

namespace BookVoyage.WebApi.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();
        services.AddAuthentication();
        return services;
    }
}