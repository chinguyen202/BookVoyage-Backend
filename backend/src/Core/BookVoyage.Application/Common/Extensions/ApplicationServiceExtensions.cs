using MediatR;
using Microsoft.Extensions.DependencyInjection;

using BookVoyage.Application.Categories.Queries;
using BookVoyage.Application.Common.Mappings;

namespace BookVoyage.Application.Common.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(GetAllCategoriesQueryHandler));
        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        return services;
    }
}