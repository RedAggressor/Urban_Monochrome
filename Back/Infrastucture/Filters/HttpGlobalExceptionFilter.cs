using Infrastucture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Infrastucture.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is Exception ex)
            {
                var problemDetails = new BaseResponse()
                {
                    ErrorMessage = ex.Message,
                };

                context.Result = new OkObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.ExceptionHandled = true;
                _logger.LogError($"{context.Exception} it`s global filter");
            }
            else
            {
                _logger.LogError(
                    new EventId(context.Exception.HResult),
                    $"{context.Exception} it`s global filter",
                    context.Exception.Message);
            }
        }
    }
}
