using Training.TruckWorld.Backend.Infrastructure.Filters.Models;

namespace Training.TruckWorld.Backend.Infrastructure.Products.Models.Filters;

public class ProductFilterModel : FilterPagination
{
    public string? Keyword { get; set; }
}