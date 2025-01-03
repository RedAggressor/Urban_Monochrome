using Catalog.Host.Models.Dto;

namespace Catalog.Host.Services.Abstractions
{
    public interface IColorService
    {
        Task<DataResponse<int>> AddColorAsync(DataRequest<string> request);
        Task<DataResponse<ColorDto>> GetColorByIdAsync(DataRequest<int> request);
        Task<DataResponse<ColorDto>> UpdateColorAsync(DataRequest<UpdateDto> request);
        Task<DataResponse<string>> DeleteColorAsync(DataRequest<int> request);
    }
}
