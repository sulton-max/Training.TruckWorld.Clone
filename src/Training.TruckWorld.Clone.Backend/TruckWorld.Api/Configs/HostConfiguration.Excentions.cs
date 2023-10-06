using FileBaseContext.Context.Models.Configurations;
using Training.TruckWorld.Backend.Application.Accounts.Services;
using Training.TruckWorld.Backend.Application.Components.Services;
using Training.TruckWorld.Backend.Application.Notifications.Services;
using Training.TruckWorld.Backend.Application.Products.Services;
using Training.TruckWorld.Backend.Application.Trucks.Services;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Infrastructure.Accounts.Services;
using Training.TruckWorld.Backend.Infrastructure.Components.Services;
using Training.TruckWorld.Backend.Infrastructure.Notifications.Services;
using Training.TruckWorld.Backend.Infrastructure.Products.Services;
using Training.TruckWorld.Backend.Infrastructure.Trucks.Services;
using Training.TruckWorld.Backend.Persistence.DataContexts;
using Training.TruckWorld.Backend.Persistence.SeedData;

namespace TruckWorld.Api.Configs;

public static partial class HostConfiguration
{
    //Web Application Builder configuration
    public static WebApplicationBuilder AddDataContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IDataContext, AppFileContext>(_ =>
        {
            var options = new FileContextOptions<AppFileContext>
            {
                StorageRootPath = Path.Combine(builder.Environment.ContentRootPath, "Data", "DataStorage")
            };

            var context = new AppFileContext(options);
            context.FetchAsync().AsTask().Wait();

            return context;
        });
        return builder;
    }

    public static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        return builder;
    }

    public static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        return builder;
    }

    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddSingleton<IPasswordHasherService, PasswordHasherService>();
        builder.Services.AddScoped<IUserCredentialsService, UserCredentialsService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddSingleton<IValidationService, ValidationService>();
        builder.Services.AddScoped<IComponentCategoryService, ComponentCategoryService>();
        builder.Services.AddScoped<IComponentService, ComponentService>();
        builder.Services.AddScoped<IContactService, ContactService>();
        
        builder.Services.AddScoped<IEmailManagementService, EmailManagementService>()
            .AddScoped<IEmailMessageService, EmailMessageService>()
            .AddScoped<IEmailPlaceholderService, EmailPlaceholderService>()
            .AddScoped<IEmailSenderService, EmailSenderService>()
            .AddScoped<IEmailService, EmailService>()
            .AddScoped<IEmailTemplateService, EmailTemplateService>();

        builder.Services.AddScoped<IFilterService, FilterService>();
        builder.Services.AddScoped<ITruckCategoryService, TruckCategoryService>();
        builder.Services.AddScoped<ITruckService, TruckService>();


        return builder;
    }

    public static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        return builder;
    }

    //Web Application configuration

    public static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();

        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.MapControllers();

        return app;
    }

    public static async ValueTask<WebApplication> SeedDataAsync(this WebApplication app)
    {
        await app.Services.CreateAsyncScope().ServiceProvider.GetRequiredService<IDataContext>()
            .InitializeSeedDataAsync();
        return app;
    }
}