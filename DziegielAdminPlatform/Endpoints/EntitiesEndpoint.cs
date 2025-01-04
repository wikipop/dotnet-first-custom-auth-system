using DziegielAdminPlatform.Endpoints.Entities;

namespace DziegielAdminPlatform.Endpoints;

public static class EntitiesEndpoint
{
    private static readonly PathString BasePath = "/objects";

    public static void MapEntitiesEndpoint(this WebApplication app)
    {
        var group = app.MapGroup(BasePath);
        group.MapHumanEndpointGroup();
    }
}