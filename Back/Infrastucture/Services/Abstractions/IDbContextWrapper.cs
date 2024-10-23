using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastucture.Services.Abstractions
{
    public interface IDbContextWrapper<T>
    {
        T DbContext { get; }

        Task<IDbContextTransaction> BeginTransactionAsycn(CancellationToken cancellationToken);
    }
}
