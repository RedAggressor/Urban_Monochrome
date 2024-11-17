namespace Nitifacation.Host.Services.Interfaces
{
    public interface ISendPulseService
    {
        Task SendMailAsync(string to, string subject, string body);
    }
}
