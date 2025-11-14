namespace AiManagementApp.Models.DTOs;

public class PagedHateoasResponse<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }
    public List<LinkDTO> Links { get; set; } = new();
    public IEnumerable<T> Items { get; set; } = new List<T>();
}