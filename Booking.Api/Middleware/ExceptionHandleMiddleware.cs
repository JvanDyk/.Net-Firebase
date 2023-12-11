namespace Booking.Api.Middleware;

public class ExceptionHandleMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandleMiddleware(RequestDelegate next, ILogger<ExceptionHandleMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException notFoundException)
        {
            _logger.LogError(notFoundException, "NotFound exception during processing of request");
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsync(notFoundException.Message);
        }
        catch (BadRequestException badRequestException)
        {
            _logger.LogError(badRequestException, "BadRequest exception during processing of request");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(badRequestException.Message);
        }
        catch (UnauthorizedException unauthorizedException)
        {
            _logger.LogError(unauthorizedException, "UnauthorizedException exception during processing of request");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(unauthorizedException.Message);
        }
        catch (ForbiddenException forbiddenException)
        {
            _logger.LogError(forbiddenException, "Forbidden exception during processing of request");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(forbiddenException.Message);
        }
        catch (TaskCanceledException TaskCanceledException)
        {
            _logger.LogError(TaskCanceledException, "TaskCaceled exception during processing of request");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(TaskCanceledException.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected exception during processing of request");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(ex.Message);
        }
    }
}
