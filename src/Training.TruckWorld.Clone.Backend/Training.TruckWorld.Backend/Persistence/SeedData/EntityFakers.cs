using Bogus;
using Training.TruckWorld.Backend.Domain.Entities;
using Training.TruckWorld.Backend.Domain.Enums;
using Training.TruckWorld.Backend.Persistence.DataContexts;

namespace Training.TruckWorld.Backend.Persistence.SeedData;

internal static class EntityFakers
{
    internal static Faker<User> GetUserFaker(IDataContext context)
    {
        return new Faker<User>()
            .RuleFor(user => user.Id, Guid.NewGuid())
            .RuleFor(user => user.FirstName, faker => faker.Person.FirstName)
            .RuleFor(user => user.LastName, faker => faker.Person.LastName)
            .RuleFor(user => user.EmailAddress, faker => faker.Person.Email);
    }
    internal static Faker<UserCredentials> GetUserCredentialsFaker(IDataContext context)
    {
        var usersId = new Stack<Guid>(context.Users.Select(user => user.Id));
        return new Faker<UserCredentials>()
            .RuleFor(userCredentials => userCredentials.Id, Guid.NewGuid())
            .RuleFor(userCredentials => userCredentials.UserId, () => usersId.Pop())
            .RuleFor(userCredentials => userCredentials.Password, faker => faker.Internet.Password(8, false));
    }
    internal static Faker<Truck> GetTruckFaker(IDataContext context)
    {
        var random = new Random();
        var user = new Faker().PickRandom(context.Users.Select(user => user));
        return new Faker<Truck>()
            .RuleFor(truck => truck.Id, Guid.NewGuid())
            .RuleFor(truck => truck.UserId, user.Id)
            .RuleFor(truck => truck.SerialNumber, faker => faker.Lorem.Word())
            .RuleFor(truck => truck.Manufacturer, faker => faker.Company.CompanyName())
            .RuleFor(truck => truck.Model, faker => faker.Lorem.Word())
            .RuleFor(truck => truck.Category, faker => faker.PickRandom(context.TruckCategories.Select(truckCategory => truckCategory)))
            .RuleFor(truck => truck.Year, random.Next(1900, 2025))
            .RuleFor(truck => truck.Condition, (TruckCondition)random.Next(0, 4))
            .RuleFor(truck => truck.Description, faker => faker.Lorem.Text())
            .RuleFor(truck => truck.Price, random.Next(1000, 10000))
            .RuleFor(truck => truck.Odometer, random.Next(1000, 10000))
            .RuleFor(truck => truck.ListingType, (ListingType)random.Next(0, 2))
            .RuleFor(truck => truck.ContactUser, faker => new Faker<ContactUser>()
            .RuleFor(contact => contact.Id, Guid.NewGuid())
            .RuleFor(contact => contact.UserId, user.Id)
            .RuleFor(context => context.FirstName, user.FirstName)
            .RuleFor(contact => contact.LastName, user.LastName)
            .RuleFor(contact => contact.EmailAddress, user.EmailAddress)
            .RuleFor(contact => contact.PhoneNumber, faker => faker.Person.Phone)
            .RuleFor(contact => contact.Location, new Faker<Location>()
            .RuleFor(location => location.Country, faker => faker.Address.Country())
            .RuleFor(location => location.City, faker => faker.Address.City())
            .RuleFor(location => location.Street, faker => faker.Address.StreetName())
            .Generate())
            .Generate());
    }
    internal static Faker<Component> GetComponentFaker(IDataContext context)
    {
        var random = new Random();
        var user = new Faker().PickRandom(context.Users.Select(user => user));
        return new Faker<Component>()
            .RuleFor(component => component.Id, Guid.NewGuid())
            .RuleFor(component => component.UserId, user.Id)
            .RuleFor(component => component.SerialNumber, faker => faker.Lorem.Word())
            .RuleFor(component => component.Manufacturer, faker => faker.Company.CompanyName())
            .RuleFor(component => component.Model, faker => faker.Lorem.Word())
            .RuleFor(component => component.Category, faker => faker.PickRandom(context.ComponentsCategories.Select(componentCategory => componentCategory)))
            .RuleFor(component => component.Year, random.Next(1900, 2025))
            .RuleFor(component => component.Condition, (ComponentCondition)random.Next(0, 5))
            .RuleFor(component => component.Description, faker => faker.Lorem.Text())
            .RuleFor(truck => truck.Price, random.Next(1000, 10000))
            .RuleFor(truck => truck.ListingType, (ListingType)random.Next(0, 2))
            .RuleFor(component => component.Quantity, random.Next(1, 100))
            .RuleFor(component => component.Weight, random.Next(1, 100))
            .RuleFor(component => component.Contact, faker => new Faker<ContactUser>()
            .RuleFor(contact => contact.Id, Guid.NewGuid())
            .RuleFor(contact => contact.UserId, user.Id)
            .RuleFor(context => context.FirstName, user.FirstName)
            .RuleFor(contact => contact.LastName, user.LastName)
            .RuleFor(contact => contact.EmailAddress, user.EmailAddress)
            .RuleFor(contact => contact.PhoneNumber, faker => faker.Person.Phone)
            .RuleFor(contact => contact.Location, new Faker<Location>()
            .RuleFor(location => location.Country, faker => faker.Address.Country())
            .RuleFor(location => location.City, faker => faker.Address.City())
            .RuleFor(location => location.Street, faker => faker.Address.StreetName())
            .Generate())
            .Generate());
    }

}
