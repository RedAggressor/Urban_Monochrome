namespace Catalog.Host.Services.Abstractions
{
    public interface ITypeService
    {
        Task<DataResponse<int>> AddTypeAsync(DataRequest<string> request);
        Task<DataResponse<string>> DeleteTypeAsync(DataRequest<int> request);
        Task<DataResponse<TypeDto>> GetTypeById(DataRequest<int> request);
        Task<DataResponse<TypeDto>> UpdateTypeAsync(DataRequest<TypeDto> request);
    }
}
