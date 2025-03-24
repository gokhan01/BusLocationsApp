using BusLocationsApp.Models.ViewModels;
using System.Net;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace BusLocationsApp.Helpers.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "...");

            context.Response.ContentType = Application.Json;
            var response = context.Response;

            var errorResponse = new ErrorResponse
            {
                Success = false
            };

            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            errorResponse.Message = "Internal server error!";
            if (context.RequestServices.GetService<IWebHostEnvironment>().IsDevelopment())
            {
                errorResponse.Details = exception.ToString();
                errorResponse.StackTrace = exception.StackTrace;
            }

            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);
        }
    }
}
