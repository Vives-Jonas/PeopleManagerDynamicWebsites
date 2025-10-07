namespace Vives.Services.Model
{
    public class ServiceMessage
    {
        public required string Code { get; set; }
        public required string Description { get; set; }
        public string? PropertyName { get; set; }
        public ServiceMessageType Type { get; set; }
    }
}
