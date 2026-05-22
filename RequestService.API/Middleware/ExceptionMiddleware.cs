using RequestService.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace RequestService.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "Необработанное исключение");
            (_, string errorCode, string message) = ex switch
            {
                BusinessException be => ((int)HttpStatusCode.BadRequest, be.ErrorCode, be.Message),
                KeyNotFoundException => ((int)HttpStatusCode.NotFound, "NOT_FOUND", ex.Message),
                _ => ((int)HttpStatusCode.InternalServerError, "INTERNAL_ERROR", "Произошла внутренняя ошибка")
            };
            var response = new { error = new { code = errorCode, message } };
            JsonSerializerOptions options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
    }
}
