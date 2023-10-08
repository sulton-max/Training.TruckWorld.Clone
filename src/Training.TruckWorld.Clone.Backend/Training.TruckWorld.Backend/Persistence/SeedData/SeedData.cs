using Training.TruckWorld.Backend.Domain.Common;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Persistence.DataContexts;

namespace Training.TruckWorld.Backend.Persistence.SeedData;

public static class SeedData
{
    public static async ValueTask InitializeSeedDataAsync(this IDataContext context)
    {
        if (!context.ComponentsCategories.Any())
            await context.AddAsync<ComponentCategory>(50);

        if (!context.TruckCategories.Any())
            await context.AddAsync<TruckCategory>(50);

        if (!context.Contacts.Any())
            await context.AddAsync<ContactDetails>(100);

        if (!context.Users.Any())
            await context.AddAsync<User>(100);

        if (!context.UserCredentials.Any())
            await context.AddAsync<UserCredentials>(100);

        if (!context.Trucks.Any())
            await context.AddAsync<Truck>(100);

        if (!context.Components.Any())
            await context.AddAsync<Component>(100);

        if (!context.EmailTemplates.Any())
            await context.AddAsync<EmailTemplate>(20);

        if (!context.Emails.Any())
            await context.AddAsync<Email>(20);
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
            { } type when type == typeof(Email) => context.AddEmailAsync(count),
            { } type when type == typeof(ContactDetails) => context.AddContactsAsync(count),
            _ => new ValueTask(Task.CompletedTask)
        };

        await task;
        await context.SaveChangesAsync();
    }


    private static async ValueTask AddUsersAsync(this IDataContext context, int count)
    {
        var faker = EntityFakers.GetUserFaker(context);
        var users = faker.Generate(100_000).Distinct();
        await context.Users.AddRangeAsync(users.Take(count).ToList());
    }

    private static async ValueTask AddUserCredentialsAsync(this IDataContext context, int count)
    {
        var faker = EntityFakers.GetUserCredentialsFaker(context);
        var userCredentials = faker.Generate(context.Users.Count());
        await context.UserCredentials.AddRangeAsync(userCredentials.Take(count).ToList());
    }

    private static async ValueTask AddTrucksAsync(this IDataContext context, int count)
    {
        var faker = EntityFakers.GetTruckFaker(context);
        var trucks = faker.Generate(100_000);
        await context.Trucks.AddRangeAsync(trucks.Take(count).ToList());
    }

    private static async ValueTask AddComponentsAsync(this IDataContext context, int count)
    {
        var faker = EntityFakers.GetComponentFaker(context);
        var components = faker.Generate(100_000);
        await context.Components.AddRangeAsync(components.Take(count).ToArray());
    }

    private static async ValueTask AddTruckCategoriesAsync(this IDataContext context, int count)
    {
        var faker = EntityFakers.GetTruckCategoryFaker();
        var truckCategories = faker.Generate(count).DistinctBy(category => category.Name);
        await context.TruckCategories.AddRangeAsync(truckCategories.Take(count).ToList());
    }

    private static async ValueTask AddComponentCategoriesAsync(this IDataContext context, int count)
    {
        var faker = EntityFakers.GetComponentCategoryFaker();
        var categories = faker.Generate(1000);
        await context.ComponentsCategories.AddRangeAsync(categories.Take(count).ToArray());
    }

    private static async ValueTask AddContactsAsync(this IDataContext context, int count)
    {
        var faker = EntityFakers.GetContactFaker(context);
        var contacts = faker.Generate(1_000);
        await context.Contacts.AddRangeAsync(contacts.Take(count).ToArray());
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

        await context.EmailTemplates.AddRangeAsync(emailTemplates.Take(count).ToList());
    }

    private static async ValueTask AddEmailAsync(this IDataContext context, int count)
    {
        var faker = EntityFakers.GetEmailFaker(context);
        var emails = faker.Generate(1_000);
        await context.Emails.AddRangeAsync(emails.Take(count).ToList());
    }
}