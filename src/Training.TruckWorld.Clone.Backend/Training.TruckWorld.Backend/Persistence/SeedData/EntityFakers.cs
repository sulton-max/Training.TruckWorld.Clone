using Bogus;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Enums;
using Training.TruckWorld.Backend.Infrastructure.Accounts.Services;
using Training.TruckWorld.Backend.Persistence.DataContexts;

namespace Training.TruckWorld.Backend.Persistence.SeedData;

public static class EntityFakers
{
    public static Faker<User> GetUserFaker(IDataContext context)
    {
        return new Faker<User>()
            .RuleFor(user => user.Id, Guid.NewGuid)
            .RuleFor(user => user.FirstName, faker => faker.Person.FirstName)
            .RuleFor(user => user.LastName, faker => faker.Person.LastName)
            .RuleFor(user => user.EmailAddress, faker => faker.Person.Email);
    }
    
    public static Faker<UserCredentials> GetUserCredentialsFaker(IDataContext context)
    {
        var usersId = new Stack<Guid>(context.Users.Select(user => user.Id));
        return new Faker<UserCredentials>()
            .RuleFor(userCredentials => userCredentials.Id, Guid.NewGuid())
            .RuleFor(userCredentials => userCredentials.UserId, () => usersId.Pop())
            .RuleFor(userCredentials => userCredentials.Password, faker => new PasswordHasherService().Hash(faker.Internet.Password(8, false)));
    }

    public static Faker<Truck> GetTruckFaker(IDataContext context)
    {
        var random = new Random();
        return new Faker<Truck>()
            .RuleFor(truck => truck.Id, Guid.NewGuid)
            .RuleFor(truck => truck.UserId, faker => faker.PickRandom(context.Users.Select(user => user.Id)))
            .RuleFor(truck => truck.SerialNumber, faker => faker.Lorem.Word())
            .RuleFor(truck => truck.Manufacturer, faker => faker.Company.CompanyName())
            .RuleFor(truck => truck.Model, faker => faker.Lorem.Word())
            .RuleFor(truck => truck.CategoryId, faker => faker.PickRandom(context.TruckCategories.Select(componentCategory => componentCategory.Id)))
            .RuleFor(truck => truck.Year, random.Next(1900, 2025))
            .RuleFor(truck => truck.Condition, (TruckCondition)random.Next(0, 4))
            .RuleFor(truck => truck.Description, faker => faker.Lorem.Text())
            .RuleFor(truck => truck.Price, random.Next(1000, 10000))
            .RuleFor(truck => truck.Odometer, random.Next(1000, 10000))
            .RuleFor(truck => truck.ListingType, (ListingType)random.Next(0, 2))
            .RuleFor(truck => truck.ContactId, faker => faker.PickRandom(context.Contacts.Select(contact => contact.Id)));
    }

    public static Faker<TruckCategory> GetTruckCategoryFaker()
    {
        var truckCategories = new List<string>
        {
            "Asphalt / Concrete Trucks",
            "Beavertail Trucks",
            "Beverage Trucks",
            "Cab / Chasis Trucks",
            "Cherry Picker",
            "Hay and Forage Equipment",
            "Flail Mowers",
            "Spreaders & Sprayers",
            "Sprayers",
            "Pull Type",
            "Farm Trailers",
            "Material Handling Trailers",
            "Utility Vehicles",
            "Utility",
            "Camper Trailers",
            "Soft-Sided",
            "Motorhomes",
            "Gas",
            "Off Road Caravans",
        };

        return new Faker<TruckCategory>()
            .RuleFor(selector => selector.Id, Guid.NewGuid)
            .RuleFor(selectore => selectore.Name, source => source.PickRandom(truckCategories));
    }

    public static Faker<Component> GetComponentFaker(IDataContext context)
    {
        var random = new Random();
        return new Faker<Component>()
            .RuleFor(component => component.Id, Guid.NewGuid)
            .RuleFor(component => component.UserId, faker => faker.PickRandom(context.Users.Select(user => user.Id)))
            .RuleFor(component => component.SerialNumber, faker => faker.Lorem.Word())
            .RuleFor(component => component.Manufacturer, faker => faker.Company.CompanyName())
            .RuleFor(component => component.Model, faker => faker.Lorem.Word())
            .RuleFor(component => component.CategoryId, faker => faker.PickRandom(context.ComponentsCategories.Select(componentCategory => componentCategory.Id)))
            .RuleFor(component => component.Year, random.Next(1900, 2025))
            .RuleFor(component => component.Condition, (ComponentCondition)random.Next(0, 5))
            .RuleFor(component => component.Description, faker => faker.Lorem.Text())
            .RuleFor(component => component.Price, random.Next(1000, 10000))
            .RuleFor(component => component.ListingType, (ListingType)random.Next(0, 2))
            .RuleFor(component => component.Quantity, random.Next(1, 100))
            .RuleFor(component => component.Weight, random.Next(1, 100))
            .RuleFor(component => component.ContactId, Faker => Faker.PickRandom(context.Contacts.Select(contact => contact.Id)));

    }

    public static Faker<ComponentCategory> GetComponentCategoryFaker()
    {
        var componentCategories = new List<string>
        {
            "Fifth Wheel",
            "Fuel Pump",
            "Fuel Tank",
            "Grill",
            "Hood",
            "Ramps",
            "Reefer Unit",
            "Seat",
            "Tool Box",
            "Transmission",
            "Turbo",
            "Wheel",
            "Fleet",
            "GPS",
        };

        return new Faker<ComponentCategory>()
            .RuleFor(selector => selector.Id, Guid.NewGuid)
            .RuleFor(selector => selector.Name, source => source.PickRandom(componentCategories));
    }

    public static Faker<ContactDetails> GetContactFaker(IDataContext contex)
    {
        return new Faker<ContactDetails>()
            .RuleFor(contact => contact.Id, Guid.NewGuid)
            .RuleFor(contact => contact.FirstName, source => source.Person.FirstName)
            .RuleFor(contact => contact.LastName, source => source.Person.LastName)
            .RuleFor(contact => contact.Email, source => source.Person.Email)
            .RuleFor(contact => contact.PhoneNumber, faker => faker.Person.Phone)
            .RuleFor(contact => contact.Country, faker => faker.Address.Country())
            .RuleFor(contact => contact.City, faker => faker.Address.City());
    }
    public static Faker<Email> GetEmailFaker(IDataContext context)
    {
        return new Faker<Email>()
            .RuleFor(selector => selector.Id, Guid.NewGuid)
            .RuleFor(selector => selector.SenderAddress,
                source => source.PickRandom(context.Users.Select(user => user.EmailAddress)))
            .RuleFor(selector => selector.ReceiverAddress,
                source => source.PickRandom(context.Users.Select(user => user.EmailAddress)))
            .RuleFor(selector => selector.Subject,
                source => source.PickRandom(context.EmailTemplates.Select(template => template.Subject)))
            .RuleFor(selector => selector.Body,
                source => source.PickRandom(context.EmailTemplates.Select(template => template.Body)))
            .RuleFor(selector => selector.SentTime, source => source.Date.Past())
            .RuleFor(selector => selector.IsSent, source => source.PickRandom(new bool[] { true, false }));
    }
}