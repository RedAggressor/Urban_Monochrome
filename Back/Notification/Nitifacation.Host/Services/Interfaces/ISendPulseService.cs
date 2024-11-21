using Nitifacation.Host.Models;

namespace Nitifacation.Host.Services.Interfaces
{
    public interface ISendPulseService
    {
        Task<BaseResponse> SendMailAsync(SendMailRequest sendMail);
    }
}
