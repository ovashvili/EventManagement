using System.Text;
using EventManagement.Infrastructure.Middlewares.RequestResponseLogger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EventManagement.Infrastructure.Middlewares.RequestResponseLogger
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(ILogger<RequestResponseLoggingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequestAsync(context.Request).ConfigureAwait(false);

            var originalBodyStream = context.Response.Body;
            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context).ConfigureAwait(false);

            await LogResponseAsync(context.Response).ConfigureAwait(false);

            await responseBody.CopyToAsync(originalBodyStream).ConfigureAwait(false);
        }

        private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.EnableBuffering();

            var buffer = new byte[request.ContentLength ?? 0];

            await request.Body.ReadAsync(buffer).ConfigureAwait(false);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body.Position = 0;

            return bodyAsText;
        }

        private async Task LogRequestAsync(HttpRequest request)
        {
            var requestDetails = new RequestDetails
            {
                IP = request.HttpContext.Connection.RemoteIpAddress.ToString(),
                Schema = request.Scheme,
                Host = request.Host.ToString(),
                Method = request.Method,
                Path = request.Path,
                IsSecured = request.IsHttps,
                QueryString = request.QueryString.ToString(),
                Body = await ReadRequestBodyAsync(request).ConfigureAwait(false),
            };

            if (request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                requestDetails.JWTToken = authHeader;
            }

            var toLog = $"{Environment.NewLine}" +
                        $"REQUEST - {Environment.NewLine}" +
                        $"Time = {requestDetails.RequestTime} {Environment.NewLine}" +
                        $"IP = {requestDetails.IP} {Environment.NewLine}" +
                        $"Address = {requestDetails.Schema} {Environment.NewLine}" +
                        $"Method = {requestDetails.Method} {Environment.NewLine}" +
                        $"Path = {requestDetails.Path} {Environment.NewLine}" +
                        $"IsSecured = {requestDetails.IsSecured} {Environment.NewLine}" +
                        $"QueryString = {requestDetails.QueryString} {Environment.NewLine}" +
                        $"RequestBody = {requestDetails.Body} {Environment.NewLine}" +
                        $"AuthHeader = {requestDetails.JWTToken} {Environment.NewLine}";

            _logger.LogInformation(toLog);
        }

        private static async Task<string> ReadResponseBodyAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            var bodyAsttext = await new StreamReader(response.Body).ReadToEndAsync().ConfigureAwait(false);

            response.Body.Seek(0, SeekOrigin.Begin);

            return bodyAsttext;
        }

        private async Task LogResponseAsync(HttpResponse response)
        {
            var responseDetails = new ResponseDetails
            {
                Body = await ReadResponseBodyAsync(response).ConfigureAwait(false),
            };

            var toLog = $"{Environment.NewLine}" +
                    $"RESPONSE - {Environment.NewLine}" +
                    $"Time = {responseDetails.ResponseTime} {Environment.NewLine}" +
                    $"ResponseBody = {responseDetails.Body} {Environment.NewLine}";

            _logger.LogInformation(toLog);
        }
    }
}
