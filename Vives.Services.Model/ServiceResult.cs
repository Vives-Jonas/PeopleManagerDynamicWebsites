namespace Vives.Services.Model
{
    public class ServiceResult
    {
        public IList<ServiceMessage> Messages { get; set; } = new List<ServiceMessage>();

        public bool IsSucces =>
            Messages.All(m => m.Type != ServiceMessageType.Error && m.Type != ServiceMessageType.Critical);


        //Andere manier van schrijven voor zelfde logica:
        //public bool IsSucces =>
        //    !Messages.Any(m => m.Type == ServiceMessageType.Error || m.Type == ServiceMessageType.Critical);
    }
}
