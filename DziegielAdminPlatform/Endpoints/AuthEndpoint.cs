using Microsoft.AspNetCore.Http.HttpResults;

public static class AuthEndpoint
{
    private static readonly PathString BasePath = new PathString("/auth");

    public static void MapAuthEndpoint(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup(BasePath);

        group.MapPost("/register", RegisterUserAsync);
    }

    private static async Task<Results<Ok, ValidationProblem>> RegisterUserAsync()
    {
        return TypedResults.Ok();
    }
}