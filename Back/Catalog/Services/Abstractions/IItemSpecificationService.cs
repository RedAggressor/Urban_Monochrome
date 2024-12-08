using Catalog.Host.Models.Dto;

namespace Catalog.Host.Services.Abstractions
{
    public interface IItemSpecificationService
    {
        Task<DataResponse<int>> AddSpecificationAsync(DataRequest<ItemSpecification>? request);
        Task<DataResponse<ItemSpecification>> GetSpecifictionById(DataRequest<int>? request);
        Task<DataResponse<string>> DeleteSpecificationAsync(DataRequest<int>? request);
        Task<DataResponse<ItemSpecification>> UpdateSpecificationAsync(DataRequest<ItemSpecification>? request);
    }
}
