using Infrastucture.Models;
using Microsoft.Extensions.Logging;

namespace Infrastucture.Services
{
    public abstract class BaseService<T>
    {
        private readonly ILogger<T> _logger;
        public BaseService(ILogger<T> logger) 
        {
            _logger = logger;
        }
        protected async Task<TResult> SafeExecuteAsync<TResult>(Func<Task<TResult>> action)
            where TResult : BaseResponse, new()
        {
            try 
            {
                return await action();
            }
            catch (Exception ex) 
            {
                _logger.LogInformation(ex.Message);
                return new TResult()
                {
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
