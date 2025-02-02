using Infrastucture.Extensions;
using Infrastucture.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nitifacation.Host.Models;
using Nitifacation.Host.Services.Interfaces;

namespace Nitifacation.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
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
            var to = HttpContext.GetUserClaimValueByType("email");
            var response = await _sendPulseService.SendMailAsync(sendMail, to);
            return Ok(response);
        }
    }
}
