namespace LibraryManagement2.Shared.Pagination;

public class PaginationParams
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    private const int MaxPageSize = 50;

    public void Normalize()
    {
        if (PageNumber <= 0) PageNumber = 1;
        if (PageSize <= 0 || PageSize > MaxPageSize) PageSize = MaxPageSize;
    }
}
