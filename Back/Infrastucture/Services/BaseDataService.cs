﻿using Infrastucture.Models;
using Infrastucture.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
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
            using var transaction = await _dbContextWrapper.BeginTransactionAsycn(cancellationToken);

            try
            {
                await action(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "transaction is rollbacked");
            }
        }
        
        private async Task<TResult> ExecuteSafeAsync<TResult>(Func<CancellationToken, Task<TResult>> action,
            CancellationToken cancellationToken = default)
            where TResult : BaseResponse, new()        
        {
            using var transaction = await _dbContextWrapper.BeginTransactionAsycn(cancellationToken);

            try
            {                
                var result = await action(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return result;
            }
            catch(Exception ex) 
            { 
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(ex, "transaction is rollbacked");
                throw new Exception($"{ex.Message}");                
            }
        }
    }
}
