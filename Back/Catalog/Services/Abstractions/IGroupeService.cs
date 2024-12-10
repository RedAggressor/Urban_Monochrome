namespace Catalog.Host.Services.Abstractions
{
    public interface IGroupeService
    {
        Task<DataResponse<int>> AddGroupeAsync(DataRequest<string> request);
        Task<DataResponse<string>> DeleteGroupeAsync(DataRequest<int> request);
        Task<DataResponse<GroupeDto>> GetGroupeByIdAsync(DataRequest<int> request);
        Task<DataResponse<GroupeDto>> UpdateGroupeAsync(DataRequest<GroupeDto> request);
    }
}
