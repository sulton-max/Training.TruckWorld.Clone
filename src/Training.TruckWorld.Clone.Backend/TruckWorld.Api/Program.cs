using TruckWorld.Api.Configurations;
using TruckWorld.Application.Common.Notifications.Services;
using TruckWorld.Infrastructure.Common.Notifications.Services;
using TruckWorld.Persistence.Repositories;
using TruckWorld.Persistence.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);


await builder.ConfigureAsync();


var app = builder.Build();

await app.ConfigureAsync();

await app.RunAsync();