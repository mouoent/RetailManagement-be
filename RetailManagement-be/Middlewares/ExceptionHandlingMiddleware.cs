using RetailManagement_be.Shared.Exceptions;

namespace RetailManagement_be.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlingMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        response.StatusCode = ex switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound, 
            _ => StatusCodes.Status500InternalServerError
        };

        return response.WriteAsJsonAsync(new { error = ex.Message });
    }
}
