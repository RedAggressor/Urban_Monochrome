using Microsoft.AspNetCore.Mvc;
using Nitifacation.Host.Models;
using Nitifacation.Host.Services.Interfaces;

namespace Nitifacation.Host.Controllers
{
    [ApiController]
    [ValidateRequestBody]
    [Route(ComponentDefaults.DefaultRoute)]
    public class MailNotifyController : ControllerBase
    {
        private readonly ISendPulseService _sendPulseService;

        public MailNotifyController(ISendPulseService sendPulseService)
        {
            _sendPulseService = sendPulseService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SendMailMessage(SendMailRequest sendMail)
        {
            // email costumer from identity
            var response = await _sendPulseService.SendMailAsync(sendMail);
            return Ok(response);
        }
    }
}
