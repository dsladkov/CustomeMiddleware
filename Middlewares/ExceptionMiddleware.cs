namespace CustomMiddleware.Middleware;

public class ExceptionMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionMiddleware> _logger;
  public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
  {
    _next = next;
    _logger = logger;
  }

  public async Task InvokeAsync (HttpContext context)
  {
    try
    {
      await _next.Invoke(context);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, ex.Message);
       context.Response.ContentType = "application/json";
          context.Response.StatusCode = StatusCodes.Status500InternalServerError;
          await context.Response.WriteAsJsonAsync(ex.Message);

    }
  }
}

public static class ExceptionMiddlewareExtensions
{
  public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder buidler)
  {
    return buidler.UseMiddleware<ExceptionMiddleware>();
  }
}