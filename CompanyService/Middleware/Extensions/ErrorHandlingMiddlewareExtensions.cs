namespace CompanyService.Middleware.Extensions;

public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder) 
        => builder.UseMiddleware<ErrorHandlingMiddleware>(); 
}