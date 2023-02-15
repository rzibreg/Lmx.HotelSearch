using System.Net;
using System.Text.Json;

namespace Lmx.HotelSearch.API.Middleware
{
    public class ApiKeyAuth
    {
        private readonly RequestDelegate _next;
        public const string APIKEY = "ApiKey";
        private string _allowedKey = string.Empty;

        public ApiKeyAuth(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Verify ApiKey
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            if (!context.Request.Headers.TryGetValue(APIKEY, out var extractedApiKey))
            {
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync(GetResponse("ApiKey was not provided.", response.StatusCode));

                return;
            }

            if (string.IsNullOrWhiteSpace(_allowedKey))
            {
                var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
                _allowedKey = appSettings.GetValue<string>(APIKEY);
            }

            if (_allowedKey != extractedApiKey)
            {
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync(GetResponse("Unauthorized ApiKey.", response.StatusCode));

                return;
            }

            await _next(context);
        }

        private string GetResponse(string error, int statusCode)
        {
            return JsonSerializer.Serialize(new
            {
                title = error,
                status = statusCode
            });
        }
    }
}
