using Nitifacation.Host.Services.Interfaces;
using System.Net.Mail;
using Nitifacation.Host.Configs;
using Nitifacation.Host.Models;
using Microsoft.Extensions.Options;

namespace Nitifacation.Host.Services
{
    public class SendMailService : ISendPulseService
    {
        private readonly CredentialConfig _credential;            

        public SendMailService(IOptions<CredentialConfig> options)
        {            
            _credential = options.Value;
        }

        public async Task<BaseResponse> SendMailAsync(SendMailRequest sendMail, string to)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var mailSend = new MailMessage()
            {
                From = new MailAddress(_credential.Email, _credential.Name),
                Subject = sendMail.Subject,
                Body = sendMail.Body,
                IsBodyHtml = true
            };

            mailSend.To.Add(to);       

            try
            {
                using(var smtpClient = new SmtpClient())
                {
                    smtpClient.Host = _credential.Host;
                    smtpClient.Port = _credential.Port;
                    smtpClient.Credentials = new NetworkCredential(_credential.Email, _credential.Password);
                    smtpClient.EnableSsl = true;

                    await smtpClient.SendMailAsync(mailSend);
                }                    
                return new BaseResponse();
            }
            catch(SmtpException ex)
            {
                return new BaseResponse
                {
                    ErrorMessage = $"SMTP error: {ex.StatusCode} - {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
