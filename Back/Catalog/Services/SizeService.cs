using Catalog.Host.Data;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Abstractions;

namespace Catalog.Host.Services
{
    public class SizeService : BaseDataService<CatalogDbContext>, ISizeService
    {
        private readonly ISizeRepository _sizeRepository;
        public SizeService(
            ISizeRepository sizeRepository,
            ILogger<BaseDataService<CatalogDbContext>> logger,
            IDbContextWrapper<CatalogDbContext> dbContextWrapper)
            : base(dbContextWrapper, logger)
        { 
            _sizeRepository = sizeRepository;
        }

        public async Task<DataResponse<int>> AddSizeAsync(DataRequest<string> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _sizeRepository.AddSizeAsync(request.Data!);

                return new DataResponse<int>
                {
                    Data = result
                };
            });
        }

        public async Task<DataResponse<SizeDto>> GetSizeByIdAsync(DataRequest<int> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _sizeRepository.GetSizeByIdAsync(request.Data!);

                return new DataResponse<SizeDto>
                {
                    Data = new SizeDto
                    {
                        Id = result.Id,
                        Name = result.Name
                    }
                };
            });
        }

        public async Task<DataResponse<string>> DeleteSizeByIdAsync(DataRequest<int> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _sizeRepository.DeleteSizeByIdAsync(request.Data!);

                return new DataResponse<string>
                {
                    Data = result
                };
            });
        }

        public async Task<DataResponse<SizeDto>>UpdateSizeAsync(DataRequest<SizeDto> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _sizeRepository.UpdateSizeAsync(request.Data!.Id, request.Data.Name!);

                return new DataResponse<SizeDto>
                {
                    Data = new SizeDto
                    {
                        Id = result!.Id,
                        Name = result.Name
                    }
                };
            });
        }
    }
}
