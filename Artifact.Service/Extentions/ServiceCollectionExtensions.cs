using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Artifact.Infrastructure.Services;
using Azure.Identity;

namespace Artifact.Service.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IHostEnvironment environment, IConfigurationManager configuration)
        {

            configuration
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();

            return services;
        }

public static IServiceCollection AddAzureBlobStorage(this IServiceCollection services, IConfiguration configuration)
        {
            var tenantId = configuration.GetValue<string>("AzureBlobStorage:TenantId");
            var clientId = configuration.GetValue<string>("AzureBlobStorage:ClientId");
            var clientSecret = configuration.GetValue<string>("AzureBlobStorage:ClientSecret");
            var blobServiceUri = configuration.GetValue<string>("AzureBlobStorage:BlobServiceUri");
            var containerName = configuration.GetValue<string>("AzureBlobStorage:ContainerName");

            var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            var blobServiceClient = new BlobServiceClient(new Uri(blobServiceUri), clientSecretCredential);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

            services.AddSingleton(blobContainerClient);
            services.AddScoped<IStorageService, AzureBlobService>();

            return services;
        }
    }
}