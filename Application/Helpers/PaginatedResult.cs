namespace Application.Helpers;

public class PaginatedResult<T>
{
    public PaginatedResult()
    {
        Data = new List<T>();
    }

    public PaginatedResult(List<T> data, int totalItems, int pageSize, int currentPage)
    {
        Data = data;
        TotalItems = totalItems;
        PageSize = pageSize;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
    }

    public List<T> Data { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}
