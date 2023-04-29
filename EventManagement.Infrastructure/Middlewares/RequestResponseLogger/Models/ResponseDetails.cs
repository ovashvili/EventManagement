namespace EventManagement.Infrastructure.Middlewares.RequestResponseLogger.Models
{
    public class ResponseDetails
    {
        public string? Body { get; set; }

        public DateTime ResponseTime => DateTime.Now;
    }
}
