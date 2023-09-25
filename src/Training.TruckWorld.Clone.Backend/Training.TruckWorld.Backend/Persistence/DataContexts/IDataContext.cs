using FileBaseContext.Abstractions.Models.FileSet;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Persistence.DataContexts;

public interface IDataContext
{
    IFileSet<User, Guid> Users { get; }
    IFileSet<Component, Guid> Components { get; }
    IFileSet<ComponentCategory, Guid> ComponentsCategories { get; }

}
