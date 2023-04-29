using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventManagement.Infrastructure.Middlewares
{
    public class ApiError : ProblemDetails
    {
        public const string UnhandlerErrorCode = "UnhandledError";
        private readonly HttpContext _httpContext;
        private readonly Exception _exception;
        public LogLevel LogLevel { get; set; }
        public string Code { get; set; }

        public ApiError(HttpContext httpContext, Exception exception)
        {
            _httpContext = httpContext;
            _exception = exception;

            TraceId = httpContext.TraceIdentifier;

            //default
            Code = UnhandlerErrorCode;
            Status = (int)HttpStatusCode.InternalServerError;
            Title = exception.Message;
            LogLevel = LogLevel.Error;
            Instance = httpContext.Request.Path;

            //HandleException(exception);
            HandleException((dynamic)exception);
        }

        public string? TraceId
        {
            get
            {
                if (Extensions.TryGetValue("TraceId", out var traceId))
                {
                    return (string?)traceId;
                }

                return null;
            }

            set => Extensions["TraceId"] = value;
        }
        private void HandleException(ArgumentNullException exception)
        {
            Code = "ArgumentNullException";
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = "ArgumentNullException;";
            LogLevel = LogLevel.Information;
        }
        private void HandleException(InvalidDataException exception)
        {
            Code = "InvalidDataException";
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = "InvalidDataException";
            LogLevel = LogLevel.Information;
        }

        private void HandleException(Exception exception) { }
    }
}
