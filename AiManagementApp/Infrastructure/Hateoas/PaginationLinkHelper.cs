using AiManagementApp.Models.DTOs;

namespace AiManagementApp.Infrastructure.Hateoas;

public class PaginationLinkHelper
{
    public static List<LinkDTO> CreatePageLinks(LinkGenerator lg, HttpContext http, int page, int size, bool hasNext,
        bool hasPrevious)
    {
        var links = new List<LinkDTO>
        {
            new(lg.GetPathByName(http, "GetLogsPaginatedList", new { pageNumber = page, pageSize = size })!, "self",
                "GET")
        };
        
        if(hasNext)
            links.Add(
                new(lg.GetPathByName(http, "GetLogsPaginatedList", new { pageNumber = page + 1, pageSize = size})!, "next", "GET")
                );
        
        if (hasPrevious)
            links.Add(
                new(lg.GetPathByName(http, "GetLogsPaginatedList", new { pageNumber = page - 1, pageSize = size })!, "prev", "GET")
                );

        return links;
    }
}