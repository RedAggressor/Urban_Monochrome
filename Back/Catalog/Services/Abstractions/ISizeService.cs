namespace Catalog.Host.Services.Abstractions
{
    public interface ISizeService
    {
        Task<DataResponse<int>> AddSizeAsync(DataRequest<string> request);
        Task<DataResponse<SizeDto>> GetSizeByIdAsync(DataRequest<int> request);
        Task<DataResponse<string>> DeleteSizeByIdAsync(DataRequest<int> request);
        Task<DataResponse<SizeDto>> UpdateSizeAsync(DataRequest<SizeDto> request);
    }
}
