using DziegielAdminPlatform.Data;
using DziegielAdminPlatform.Endpoints.Entities;
using DziegielAdminPlatform.Models;

namespace DziegielAdminPlatform.Endpoints;

public static class EntitiesEndpoint
{
    private static readonly PathString BasePath = "/objects";
    
    public static void MapEntitiesEndpoint(this WebApplication app)
    {
        var group = app.MapGroup(BasePath);
        group.RequireAuthorization();
        group.MapHumanEndpointGroup();
    }
}