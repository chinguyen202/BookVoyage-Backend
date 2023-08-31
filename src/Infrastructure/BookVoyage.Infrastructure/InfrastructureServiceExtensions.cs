using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookVoyage.Application.Common.Interfaces;
using BookVoyage.Infrastructure.Services;

namespace BookVoyage.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton(x => new BlobServiceClient(
            configuration["ConnectionStrings:StorageAccount"]));
        services.AddScoped<IBlobService, BlobService>();
        return services;
    }
}