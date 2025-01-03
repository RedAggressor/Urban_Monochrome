namespace Catalog.Host.Services.Abstractions
{
    public interface IItemSpecificationService
    {
        Task<DataResponse<int>> AddSpecificationAsync(DataRequest<UniqueItemResponse>? request);
        Task<DataResponse<UniqueItemResponse>> GetSpecifictionById(DataRequest<int>? request);
        Task<DataResponse<string>> DeleteSpecificationAsync(DataRequest<int>? request);
        Task<DataResponse<UniqueItemResponse>> UpdateSpecificationAsync(DataRequest<UniqueItemResponse>? request);
    }
}
