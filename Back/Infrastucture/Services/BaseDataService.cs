using Infrastucture.Models;
using Infrastucture.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Infrastucture.Services
{
    public abstract class BaseDataService<T>
        where T : DbContext
    {
        private readonly IDbContextWrapper<T> _dbContextWrapper;
        private readonly ILogger<BaseDataService<T>> _logger;

        protected BaseDataService(
            IDbContextWrapper<T> dbContextWrapper,
            ILogger<BaseDataService<T>> logger)
        {
            _dbContextWrapper = dbContextWrapper;
            _logger = logger;
        }

        protected Task ExecuteSafeAsync(
            Func<Task> action,
            CancellationToken cancellationToken = default) =>
            ExecuteSafeAsync(token => action(), cancellationToken);

        protected Task<TResult> ExecuteSafeAsync<TResult>(
            Func<Task<TResult>> action,
            CancellationToken cancellationToken = default)
            where TResult : BaseResponse, new() =>
            ExecuteSafeAsync(token => action(), cancellationToken);

        private async Task ExecuteSafeAsync(Func<CancellationToken, Task> action,
            CancellationToken cancellationToken = default)
        {
            IDbContextTransaction transaction = null;            

            try
            {
                transaction = await _dbContextWrapper.BeginTransactionAsycn(cancellationToken);

                await action(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "transaction is rollbacked");
            }
            finally
            {
                transaction.Dispose();
            }
        }
        
        private async Task<TResult> ExecuteSafeAsync<TResult>(Func<CancellationToken, Task<TResult>> action,
            CancellationToken cancellationToken = default)
            where TResult : BaseResponse, new()        
        {
            IDbContextTransaction transaction = null;            

            try
            {
                transaction = await _dbContextWrapper.BeginTransactionAsycn(cancellationToken);
                var result = await action(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return result;
            }
            catch(Exception ex) 
            { 
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "transaction is rollbacked");

                return new TResult() 
                { 
                    ErrorMessage = ex.Message,
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
