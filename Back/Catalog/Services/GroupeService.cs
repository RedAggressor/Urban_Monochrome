using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Services.Abstractions;

namespace Catalog.Host.Services
{
    public class GroupeService : BaseDataService<CatalogDbContext>, IGroupeService
    {
        private readonly IGroupeRepository _groupeRepository;

        public GroupeService(
            IGroupeRepository groupeRepository,
            IDbContextWrapper<CatalogDbContext> dbContextWrapper,
            ILogger<BaseDataService<CatalogDbContext>> logger)
            : base(dbContextWrapper, logger)
        {
            _groupeRepository = groupeRepository;
        }

        public async Task<DataResponse<int>> AddGroupeAsync(DataRequest<string> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var name = request.Data!;

                var result = await _groupeRepository.AddGroupeAsync(name);

                return new DataResponse<int>()
                {
                    Data = result
                };
            });
        }

        public async Task<DataResponse<string>> DeleteGroupeAsync(DataRequest<int> request)
        {
            return await ExecuteSafeAsync(async () => 
            { 
                var id = request.Data!;

                var result = await _groupeRepository.DeleteGroupeAsync(id);

                return new DataResponse<string> 
                {
                    Data = result
                };            
            });
        }

        public async Task<DataResponse<GroupeDto>> GetGroupeByIdAsync(DataRequest<int> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var id = request.Data;

                var result = await _groupeRepository.GetGroupeByIdAsync(id);
                return new DataResponse<GroupeDto>
                {
                    Data = new GroupeDto
                    {
                        Id = result.Id,
                        Name = result.Name
                    }
                };
            });
        }

        public async Task<DataResponse<GroupeDto>> UpdateGroupeAsync(DataRequest<GroupeDto> request)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var entityUpdate = new GroupeEntity
                {
                    Id = request.Data!.Id,
                    Name = request.Data.Name!
                };
                var result = await _groupeRepository.UpdateGroupeAsync(entityUpdate);

                return new DataResponse<GroupeDto>
                {
                    Data = new GroupeDto
                    {
                        Id = result.Id,
                        Name = result.Name
                    }
                };                
            });
        }
    }
}
