namespace Nitifacation.Host.Models
{
    public class SendMailRequest
    {
        public string? To { get; set; }
        public string? Subject {  get; set; }
        public string? Body { get; set; }
    }
}
