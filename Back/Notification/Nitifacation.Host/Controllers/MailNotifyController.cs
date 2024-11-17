using Microsoft.AspNetCore.Mvc;
using Nitifacation.Host.Services.Interfaces;

namespace Nitifacation.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailNotifyController : ControllerBase
    {
        private readonly ISendPulseService _sendPulseService;

        public MailNotifyController(ISendPulseService sendPulseService)
        {
            _sendPulseService = sendPulseService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMailMessage(string to, string subject, string body)
        {
            await _sendPulseService.SendMailAsync(to, subject, body);
            return Ok();
        }
    }
}
