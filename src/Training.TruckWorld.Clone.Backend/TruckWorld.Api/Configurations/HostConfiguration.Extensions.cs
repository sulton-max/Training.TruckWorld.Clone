using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TruckWorld.Application.Common.Brokers;
using TruckWorld.Application.Common.Services;
using TruckWorld.Application.Common.Settings;
using TruckWorld.Infrastructure.Common.Notifications.Brokers;
using TruckWorld.Infrastructure.Common.Notifications.Services;
using TruckWorld.Infrastructure.Common.Settings;
using TruckWorld.Persistence.DataContext;

namespace TruckWorld.Api.Configurations;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies;
    static HostConfiguration()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Register NotificationInfrastructure
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        // register configurations 
        builder.Services
            .Configure<ValidationSettings>(builder.Configuration.GetSection(nameof(ValidationSettings)))
            .Configure<SmtpEmailSenderSettings>(builder.Configuration.GetSection(nameof(SmtpEmailSenderSettings)))
            .Configure<TwilioSmsSenderSettings>(builder.Configuration.GetSection(nameof(TwilioSmsSenderSettings)));

        // register brokers
        builder.Services
            .AddScoped<ISmsSenderBroker, TwilioSmsSenderBroker>()
            .AddScoped<IEmailSenderBroker, SmtpEmailSenderBroker>();

        // register helper foundation services
        builder.Services
            .AddScoped<ISmsSenderService, SmsSenderService>()
            .AddScoped<IEmailSenderService, EmailSenderService>();

        builder.Services.AddValidatorsFromAssemblies(Assemblies);

        return builder;
    }

    /// <summary>
    /// Registers NotificationDbContext in DI 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<NotificationsDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("TruckWorldDatabaseConnection")));

        return builder;
    }

    /// <summary>
    /// Configures exposers including controllers
    /// </summary>
    /// <param name="builder">Application builder</param>
    /// <returns></returns>
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }

    /// <summary>
    /// Configures devTools including controllers
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    /// <summary>
    /// Add Controller middleWhere
    /// </summary>
    /// <param name="app">Application host</param>
    /// <returns>Application host</returns>
    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }

    /// <summary>
    /// Add Controller middleWhere
    /// </summary>
    /// <param name="app">Application host</param>
    /// <returns>Application host</returns>
    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}