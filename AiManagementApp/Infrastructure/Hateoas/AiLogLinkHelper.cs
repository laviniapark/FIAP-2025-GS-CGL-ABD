using AiManagementApp.Models.DTOs;

namespace AiManagementApp.Infrastructure.Hateoas;

public class AiLogLinkHelper
{
    public static List<LinkDTO> CreateItemLinks(Guid id, LinkGenerator lg, HttpContext http) => new()
    {
        new(lg.GetPathByName(http, "GetLogById", new { id })!, "self", "GET"),
        new(lg.GetPathByName(http, "UpdateLog", new { id })!, "update", "PUT"),
        new(lg.GetPathByName(http, "DeleteLog", new { id })!, "delete", "DELETE")
    };
}