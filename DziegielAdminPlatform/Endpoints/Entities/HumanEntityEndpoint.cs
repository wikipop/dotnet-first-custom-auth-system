using DziegielAdminPlatform.Data;
using DziegielAdminPlatform.Models;

namespace DziegielAdminPlatform.Endpoints.Entities;

public static class HumanEntityEndpoint
{
    private static readonly PathString BasePath = "/human";
    
    public static void MapHumanEndpointGroup(this RouteGroupBuilder entitiesEndpoint)
    {
        var group = entitiesEndpoint.MapGroup(BasePath);
        
        group.MapPost("/create", CreateHuman);
        group.MapGet("/{id}", GetHuman);
        group.MapGet("/page/{page}/{pageSize}", GetHumanPage);
    }
    
    private static IResult CreateHuman(ApplicationDbContext db, HumanEntity humanEntity)
    {
        db.HumanEntities.Add(humanEntity);
        db.SaveChanges();
        return Results.Created($"/objects/{humanEntity.Id}", humanEntity);
    }
    
    private static IResult GetHuman(ApplicationDbContext db, int id)
    {
        var humanEntity = db.HumanEntities.Find(id);
        return humanEntity is null ? Results.NotFound() : Results.Ok(humanEntity);
    }
    
    private static IResult GetHumanPage(ApplicationDbContext db, int page = 0, int pageSize = 10)
    {
        var humanEntities = db.HumanEntities.Skip(page * pageSize).Take(pageSize).ToList();
        return Results.Ok(humanEntities);
    }
}