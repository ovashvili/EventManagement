namespace EventManagement.Infrastructure.Middlewares.RequestResponseLogger.Models
{
    public class RequestDetails
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string IP { get; set; }
        public string Schema { get; set; }
        public string JWTToken { get; set; }
        public string Host { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string Body { get; set; }
        public bool IsSecured { get; set; }
        public DateTime RequestTime => DateTime.Now;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    }
}
