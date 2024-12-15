using Catalog.Host.Data;
using Catalog.Host.Extensions;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Abstractions;

namespace Catalog.Host.Services
{
    public class ItemSpecificationService : BaseDataService<CatalogDbContext>, IItemSpecificationService
    {
        private readonly IItemSpecificationRepository _specificationRepository;
        public ItemSpecificationService(
            IItemSpecificationRepository specificationRepository,
            IDbContextWrapper<CatalogDbContext> dbContextWrapper,
            ILogger<BaseDataService<CatalogDbContext>> logger
            )
            : base(dbContextWrapper, logger)
        {
            _specificationRepository = specificationRepository;            
        }        

        public async Task<DataResponse<int>> AddSpecificationAsync(DataRequest<UniqueItemResponse>? request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var specificationDto = request!.Data;
                var result = await _specificationRepository.AddSpecificationAsync(specificationDto!.SpecificationMapToEntity()!);

                return new DataResponse<int>
                {
                    Data = result
                };
            });
        }

        public async Task<DataResponse<UniqueItemResponse>> GetSpecifictionById(DataRequest<int>? request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = request!.Data;
                var result = await _specificationRepository.GetSpecificationByIdAsync(id);

                return new DataResponse<UniqueItemResponse>
                {
                    Data = result.SpecificationMapToDto()
                };
            });
        }

        public async Task<DataResponse<string>> DeleteSpecificationAsync(DataRequest<int>? request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = request!.Data;
                var result = await _specificationRepository.DeleteSpecificationAsync(id);

                return new DataResponse<string>
                {
                    Data = result
                };
            });
        }

        public async Task<DataResponse<UniqueItemResponse>> UpdateSpecificationAsync(DataRequest<UniqueItemResponse>? request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var specDto = request!.Data;
                var result = await _specificationRepository.UpdateSpecificationAsync(specDto!.SpecificationMapToEntity()!);

                return new DataResponse<UniqueItemResponse>
                {
                    Data = result.SpecificationMapToDto()
                };
            });
        }
    }
}
