using FileBaseContext.Abstractions.Models.FileSet;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Persistence.DataContexts;

public interface IDataContext
{
    IFileSet<User, Guid> Users { get; }
    IFileSet<Component, Guid> Components { get; }
    IFileSet<ComponentCategory, Guid> ComponentsCategories { get; }
    IFileSet<Truck, Guid> Trucks { get; }
    IFileSet<TruckCategory, Guid> TruckCategories { get; }
    IFileSet<UserCredentials, Guid> UserCredentials { get; }
    IFileSet<EmailTemplate, Guid> EmailTemplates { get; }
    IFileSet<Email, Guid> Emails { get; }
    ValueTask SaveChangesAsync();
}
