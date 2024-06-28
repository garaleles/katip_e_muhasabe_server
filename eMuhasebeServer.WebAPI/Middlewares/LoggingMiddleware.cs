using System.Text;

namespace eMuhasebeServer.WebAPI.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering(); // Allow multiple reads

        var requestBodyStream = new MemoryStream();
        await context.Request.Body.CopyToAsync(requestBodyStream);
        requestBodyStream.Seek(0, SeekOrigin.Begin);

        var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();
        requestBodyStream.Seek(0, SeekOrigin.Begin);
        context.Request.Body = requestBodyStream; // Reset the stream position to ensure the request can be read again by the next middleware

        _logger.LogInformation("Request Body: {RequestBody}", requestBodyText);

        await _next(context);
    }
}