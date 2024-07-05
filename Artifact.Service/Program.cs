using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Artifact.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Artifact.Service.Extentions;


var builder = WebApplication.CreateBuilder(args);


var environment = builder.Environment;


builder.Services.AddConfigurations(builder.Environment, builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddAzureBlobStorage(builder.Configuration);


var app = builder.Build();

app.UseCors("AllowedOrigins");
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();


Console.WriteLine($"Environment: {environment.EnvironmentName}");

app.Run();

public partial class Program { }