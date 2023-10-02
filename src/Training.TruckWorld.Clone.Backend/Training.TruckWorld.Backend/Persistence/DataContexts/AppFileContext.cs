using FileBaseContext.Abstractions.Models.Entity;
using FileBaseContext.Abstractions.Models.FileContext;
using FileBaseContext.Abstractions.Models.FileSet;
using FileBaseContext.Context.Models.Configurations;
using FileBaseContext.Context.Models.FileContext;
using Training.TruckWorld.Backend.Domain.Entities;

namespace Training.TruckWorld.Backend.Persistence.DataContexts;

public class AppFileContext : FileContext, IDataContext
{
    public IFileSet<User, Guid> Users => Set<User, Guid>(nameof(Users));

    public IFileSet<Component, Guid> Components => Set<Component, Guid>(nameof(Components));

    public IFileSet<ComponentCategory, Guid> ComponentsCategories => Set<ComponentCategory, Guid>(nameof(ComponentsCategories));
    public IFileSet<Truck, Guid> Trucks => Set<Truck, Guid>(nameof(Truck));

    public IFileSet<TruckCategory, Guid> TruckCategories => Set<TruckCategory, Guid>(nameof(TruckCategory));

    public IFileSet<UserCredentials, Guid> UserCredentials => Set<UserCredentials, Guid>(nameof(UserCredentials));

    public IFileSet<EmailTemplate, Guid> EmailTemplates => Set<EmailTemplate, Guid>(nameof(EmailTemplate));

    public IFileSet<Email, Guid> Emails => Set<Email, Guid>(nameof(Emails));

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
