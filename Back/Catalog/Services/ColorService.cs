using Catalog.Host.Data;
using Catalog.Host.Models.Dto;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Abstractions;

namespace Catalog.Host.Services
{
    public class ColorService : BaseDataService<CatalogDbContext>, IColorService
    {
        private readonly IColorRepository _colorRepository;
        public ColorService(
            IColorRepository colorRepository,
            IDbContextWrapper<CatalogDbContext> dbContextWrapper,
            ILogger<BaseDataService<CatalogDbContext>> logger)
            : base(dbContextWrapper, logger)
        { 
            _colorRepository = colorRepository;        
        }

        public async Task<DataResponse<int>> AddColorAsync(DataRequest<string> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _colorRepository.AddColorAsync(request.Data!);

                return new DataResponse<int>
                {
                    Data = result
                };
            });
        }

        public async Task<DataResponse<ColorDto>> GetColorByIdAsync(DataRequest<int> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _colorRepository.GetColorByIdAsync(request.Data!);

                return new DataResponse<ColorDto>
                {
                    Data = new ColorDto
                    {
                        Id = result.Id,
                        Name = result.Name,
                    }
                };
            });
        }

        public async Task<DataResponse<ColorDto>> UpdateColorAsync(DataRequest<UpdateDto> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _colorRepository.UpdateColorAsync(request.Data!.Id ?? 0, request.Data.Name!);

                return new DataResponse<ColorDto>
                {
                    Data = new ColorDto
                    {
                        Id = result.Id,
                        Name = result.Name,
                    }
                };
            });
        }

        public async Task<DataResponse<string>> DeleteColorAsync(DataRequest<int> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _colorRepository.DeleteColorAsync(request.Data);

                return new DataResponse<string>
                {
                    Data = result
                };
            });
        }
    }
}
