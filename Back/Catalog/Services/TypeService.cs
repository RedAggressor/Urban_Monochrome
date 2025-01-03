using Catalog.Host.Data;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Abstractions;

namespace Catalog.Host.Services
{
    public class TypeService : BaseDataService<CatalogDbContext>, ITypeService
    {
        private readonly ITypeRepository _typeRepository;
        public TypeService(
            ITypeRepository typeRepository,
            IDbContextWrapper<CatalogDbContext> dbContextWrapper,
            ILogger<BaseDataService<CatalogDbContext>> logger)
            : base(dbContextWrapper, logger)
        {
            _typeRepository = typeRepository;
        }


        public async Task<DataResponse<int>> AddTypeAsync(DataRequest<string> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var name = request.Data;

                var result = await _typeRepository.AddTypeAsync(name!);

                return new DataResponse<int>
                {
                    Data = result
                };
            });
        }

        public async Task<DataResponse<string>> DeleteTypeAsync(DataRequest<int> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = request!.Data;

                var result = await _typeRepository.DeleteTypeByIdAsync(id!);

                return new DataResponse<string>
                {
                    Data = result
                };
            });
        }

        public async Task<DataResponse<TypeDto>> GetTypeById(DataRequest<int> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = request!.Data;

                var result = await _typeRepository.GetTypeById(id!);

                return new DataResponse<TypeDto>
                {
                    Data = new TypeDto
                    {
                        Id = result.Id,
                        Name = result.Name
                    }
                };
            });
        }

        public async Task<DataResponse<TypeDto>> UpdateTypeAsync(DataRequest<TypeDto> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = request.Data!.Id;
                var name = request!.Data!.Name;

                var result = await _typeRepository.UpdateTypeAsync(id, name!);

                return new DataResponse<TypeDto>
                {
                    Data = new TypeDto()
                    {
                        Id = result.Id,
                        Name = result.Name
                    }
                };
            });
        }        
    }
}
