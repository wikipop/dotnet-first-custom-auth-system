using Microsoft.AspNetCore.Http.HttpResults;

namespace DziegielAdminPlatform.Models;

public static class PlatformHttpErrors
{
    public static ProblemHttpResult PlatformLoginValidationError()
    {
        return TypedResults.Problem(statusCode: 401, detail: "Invalid username or password");
    }
    
    public static ProblemHttpResult PlatformUserNotAuthenticatedError()
    {
        return TypedResults.Problem(statusCode: 401, detail: "User is not authenticated");
    }
    
    public static ProblemHttpResult UserNotAuthorizedError()
    {
        return TypedResults.Problem(statusCode: 403, detail: "User is not authorized to access this resource");
    }
}