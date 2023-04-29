#nullable enable
using EventManagement.Application.Commmon.Enums;


namespace EventManagement.Application.Commmon.Models
{
    public class CommandResponse<TData>
    {
        public StatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public TData? Data { get; set; }

        public CommandResponse(StatusCode statusCode, string? message = null, TData? data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
    }
}
