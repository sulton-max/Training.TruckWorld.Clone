using FileBaseContext.Abstractions.Models.Entity;
using FileBaseContext.Abstractions.Models.FileContext;
using FileBaseContext.Abstractions.Models.FileEntry;
using FileBaseContext.Abstractions.Models.FileSet;
using FileBaseContext.Context.Models.Configurations;
using FileBaseContext.Context.Models.FileContext;
using Training.TruckWorld.Backend.Domain.Common;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Persistence.DataContexts;

public class AppFileContext : FileContext, IDataContext
{
    public IFileSet<User, Guid> Users => Set<User, Guid>(nameof(Users));

    public IFileSet<Component, Guid> Components => Set<Component, Guid>(nameof(Components));

    public IFileSet<ComponentCategory, Guid> ComponentsCategories =>
        Set<ComponentCategory, Guid>(nameof(ComponentsCategories));

    public IFileSet<Truck, Guid> Trucks => Set<Truck, Guid>(nameof(Trucks));

    public IFileSet<TruckCategory, Guid> TruckCategories => Set<TruckCategory, Guid>(nameof(TruckCategories));

    public IFileSet<UserCredentials, Guid> UserCredentials => Set<UserCredentials, Guid>(nameof(UserCredentials));

    public IFileSet<EmailTemplate, Guid> EmailTemplates => Set<EmailTemplate, Guid>(nameof(EmailTemplates));

    public IFileSet<Email, Guid> Emails => Set<Email, Guid>(nameof(Emails));

    public AppFileContext(IFileContextOptions<IFileContext> fileContextOptions) : base(fileContextOptions)
    {
        OnSaveChanges += AddPrimaryKeys;
        OnSaveChanges += AddAuditableDetails;
        OnSaveChanges += AddSoftDeletionDetails;
    }

    public ValueTask AddPrimaryKeys(IEnumerable<IFileSetBase> fileSets)
    {
        foreach (var fileSet in fileSets)
        foreach (var entry in fileSet.GetEntries())
        {
            if (entry is not IFileEntityEntry<IEntity> entityEntry) continue;

            if (entityEntry.Entity is User) continue;

            if (entityEntry.State == FileEntityState.Added)
                entityEntry.Entity.Id = Guid.NewGuid();

            if (entry is not IFileEntityEntry<IFileSetEntity<Guid>> fileSetEntry) continue;
        }

        return new ValueTask(Task.CompletedTask);
    }

    public ValueTask AddAuditableDetails(IEnumerable<IFileSetBase> fileSets)
    {
        foreach (var fileSet in fileSets)
        foreach (var entry in fileSet.GetEntries())
        {
            if (entry is not IFileEntityEntry<IAuditableEntity> entityEntry) continue;

            if (entityEntry.State == FileEntityState.Added)
                entityEntry.Entity.CreatedDate = DateTimeOffset.Now;

            if (entityEntry.State == FileEntityState.Modified)
                entityEntry.Entity.ModifiedDate = DateTimeOffset.Now;

            if (entry is not IFileEntityEntry<IFileSetEntity<Guid>> fileSetEntry) continue;
        }

        return new ValueTask(Task.CompletedTask);
    }

    public ValueTask AddSoftDeletionDetails(IEnumerable<IFileSetBase> fileSets)
    {
        foreach (var fileSet in fileSets)
        foreach (var entry in fileSet.GetEntries())
        {
            if (entry is not IFileEntityEntry<ISoftDeletedEntity>
                {
                    State: FileEntityState.Deleted
                } entityEntry) continue;


            entityEntry.Entity.IsDeleted = true;
            entityEntry.Entity.DeletedDate = DateTimeOffset.Now;
            entityEntry.State = FileEntityState.MarkedDeleted;
        }

        return new ValueTask(Task.CompletedTask);
    }
}