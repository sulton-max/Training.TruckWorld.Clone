namespace TruckWorld.Application.Common.Models.Querying;

public class FilterPagination
{
    public uint PageSize { get; set; } = 10;

    public uint PageToken { get; set; } = 1;
}
