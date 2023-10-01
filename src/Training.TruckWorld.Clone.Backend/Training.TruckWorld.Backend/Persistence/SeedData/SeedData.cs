using Training.TruckWorld.Backend.Domain.Common;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Persistence.DataContexts;

namespace Training.TruckWorld.Backend.Persistence.SeedData;

public static class SeedData
{
    public static async ValueTask InitializeSeedDataAsync(this IDataContext context)
    {
        if (!context.Users.Any())
            await context.AddAsync<User>(100);

        if (!context.UserCredentials.Any())
            await context.AddAsync<UserCredentials>(100);

        if (!context.Trucks.Any())
            await context.AddAsync<Truck>(1000);

        if (!context.Components.Any())
            await context.AddAsync<Component>(2000);

        if (!context.TruckCategories.Any())
            await context.AddAsync<TruckCategory>(15);

        if (!context.ComponentsCategories.Any())
            await context.AddAsync<ComponentCategory>(10);

        if (!context.EmailTemplates.Any())
            await context.AddAsync<EmailTemplate>(5);


        await context.SaveChangesAsync();

    }
    
    private static async ValueTask AddAsync<TEntity>(this IDataContext context, int count) where TEntity : IEntity
    {
        var task = typeof(TEntity) switch
        {
            { } type when type == typeof(User) => context.AddUsersAsync(count),
            { } type when type == typeof(UserCredentials) => context.AddUserCredentialsAsync(count),
            { } type when type == typeof(Truck) => context.AddTrucksAsync(count),
            { } type when type == typeof(Component) => context.AddComponentsAsync(count),
            { } type when type == typeof(TruckCategory) => context.AddTruckCategoriesAsync(count),
            { } type when type == typeof(ComponentCategory) => context.AddComponentCategoriesAsync(count),
            { } type when type == typeof(EmailTemplate) => context.AddEmailTemplatesAsync(count),
            _ => new ValueTask(Task.CompletedTask)
        };

        await task;
    }
    
    
    private static async ValueTask AddUsersAsync(this IDataContext context, int count)
    {
        var faker = EntityFakers.GetUserFaker(context);
        var users = faker.Generate(10_000).Distinct();
        await context.Users.AddRangeAsync(users.Take(count));
    }

    private static async ValueTask AddUserCredentialsAsync(this IDataContext context, int count)
    {
        var faker = EntityFakers.GetUserCredentialsFaker(context);
        var userCredentials = faker.Generate(context.Users.Count());
        await context.UserCredentials.AddRangeAsync(userCredentials.Take(count));
    }

    private static async ValueTask AddTrucksAsync(this IDataContext context, int count)
    {
        var faker = EntityFakers.GetTruckFaker(context);
        var trucks = faker.Generate(50_000);
        await context.Trucks.AddRangeAsync(trucks.Take(count));
    }

    private static async ValueTask AddComponentsAsync(this IDataContext context, int count)
    {
        var faker = EntityFakers.GetComponentFaker(context);
        var components = faker.Generate(100_000);
        await context.Components.AddRangeAsync(components);
    }

    private static async ValueTask AddTruckCategoriesAsync(this IDataContext context, int count)
    {
        var truckCategories = new List<TruckCategory>
        {
            new TruckCategory("Asphalt / Concrete Trucks"),
            new TruckCategory("Beavertail Trucks"),
            new TruckCategory("Beverage Trucks"),
            new TruckCategory("Cab / Chasis Trucks"),
            new TruckCategory("Cherry Picker"),
            new TruckCategory("Hay and Forage Equipment"),
            new TruckCategory("Flail Mowers"),
            new TruckCategory("Spreaders & Sprayers"),
            new TruckCategory("Sprayers"),
            new TruckCategory("Pull Type"),
            new TruckCategory("Farm Trailers"),
            new TruckCategory("Material Handling Trailers"),
            new TruckCategory("Utility Vehicles"),
            new TruckCategory("Utility"),
            new TruckCategory("Camper Trailers"),
            new TruckCategory("Soft-Sided"),
            new TruckCategory("Motorhomes"),
            new TruckCategory("Gas"),
            new TruckCategory("Off Road Caravans"),
        };

        await context.TruckCategories.AddRangeAsync(truckCategories.Take(count));
    }

    private static async ValueTask AddComponentCategoriesAsync(this IDataContext context, int count)
    {
        var componentCategories = new List<ComponentCategory>
        {
            new ComponentCategory("Fifth Wheel"),
            new ComponentCategory("Fuel Pump"),
            new ComponentCategory("Fuel Tank"),
            new ComponentCategory("Grill"),
            new ComponentCategory("Hood"),
            new ComponentCategory("Ramps"),
            new ComponentCategory("Reefer Unit"),
            new ComponentCategory("Seat"),
            new ComponentCategory("Tool Box"),
            new ComponentCategory("Transmission"),
            new ComponentCategory("Turbo"),
            new ComponentCategory("Wheel"),
            new ComponentCategory("Fleet"),
            new ComponentCategory("GPS"),
        };

        await context.ComponentsCategories.AddRangeAsync(componentCategories.Take(count));
    }

    private static async ValueTask AddEmailTemplatesAsync(this IDataContext context, int count)
    {
        var emailTemplates = new List<EmailTemplate>
        {
            new EmailTemplate("Welcome to our system", "Dear {{FullName}}, welcome to our system"),
            new EmailTemplate("New Message", "Hello {{FullName}}, you've got a new message."),
            new EmailTemplate("Truck Confirmation", "Dear {{FullName}}, your truck has been confirmed."),
            new EmailTemplate("Thank You", "Dear {{FullName}}, thank you for using our services."),
            new EmailTemplate("Your password has been changes", "Dear {{FullName}}, Your password has been changed."),
        };

        await context.EmailTemplates.AddRangeAsync(emailTemplates.Take(count));
    }
}