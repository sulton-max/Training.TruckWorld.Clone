namespace Training.TruckWorld.Backend.Infrastructure.Filters.Models;

public abstract class FilterPagination
{
    public int PageSize { get; set; } = 20;
    public int PageToken { get; set; } = 1;
}