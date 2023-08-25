using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

using BookVoyage.Application.Common.Behaviors;
using BookVoyage.Application.Common.Mappings;

namespace BookVoyage.Application.Common.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(AssemblyReference.ApplicationAssembly);
        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        services.AddValidatorsFromAssembly(AssemblyReference.ApplicationAssembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        return services;
    }
}
