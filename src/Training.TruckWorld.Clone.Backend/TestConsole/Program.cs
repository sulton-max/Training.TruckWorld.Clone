using System.Security.Cryptography;
using FileBaseContext.Context.Models.Configurations;
using Training.TruckWorld.Backend.Persistence.DataContexts;
using Training.TruckWorld.Backend.Persistence.SeedData;

var options = new FileContextOptions<AppFileContext>
{
    StorageRootPath = Path.Combine(
        Directory.GetParent(
            Directory.GetCurrentDirectory()
        ).Parent.Parent.ToString(), "Data"
    )
};

var context = new AppFileContext(options);

await context.InitializeSeedDataAsync();