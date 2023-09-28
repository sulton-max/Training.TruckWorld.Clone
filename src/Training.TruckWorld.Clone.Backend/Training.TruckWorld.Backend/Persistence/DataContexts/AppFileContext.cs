using FileBaseContext.Abstractions.Models.Entity;
using FileBaseContext.Abstractions.Models.FileContext;
using FileBaseContext.Abstractions.Models.FileSet;
using FileBaseContext.Context.Models.Configurations;
using FileBaseContext.Context.Models.FileContext;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Persistence.DataContexts;

public class AppFileContext : FileContext, IDataContext
{
    public IFileSet<User, Guid> Users => Set<User>(nameof(Users));

    public IFileSet<Component, Guid> Components => Set<Component>(nameof(Components));

    public IFileSet<ComponentCategory, Guid> ComponentsCategories => Set<ComponentCategory>(nameof(ComponentsCategories));
    public IFileSet<Truck, Guid> Trucks => Set<Truck>(nameof(Truck));

    public IFileSet<TruckCategory, Guid> TruckCategories => Set<TruckCategory>(nameof(TruckCategory));

    public IFileSet<UserCredentials, Guid> UserCredentials => Set<UserCredentials>(nameof(UserCredentials));

    public IFileSet<EmailTemplate, Guid> EmailTemplates => Set<EmailTemplate>(nameof(EmailTemplate));

    public IFileSet<Email, Guid> Emails => Set<Email>(nameof(Emails));

    public AppFileContext(IFileContextOptions<IFileContext> fileContextOptions) : base(fileContextOptions)
    {
        OnSaveChanges += AddPrimaryKeys;
    }

    public virtual ValueTask AddPrimaryKeys(IEnumerable<IFileSetBase> fileSets)
    {
        foreach (var fileSetBase in fileSets)
        {
            if (fileSetBase is not IFileSet<IFileSetEntity<Guid>, Guid> fileSet) continue;

            foreach (var entry in fileSet.Where(entry => entry.Id == default))
                entry.Id = Guid.NewGuid();
        }

        return new ValueTask(Task.CompletedTask);
    }
}
