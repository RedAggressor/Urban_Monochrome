namespace IdentityServer.Services.Interfaces
{
    public interface IRoleService
    {
        Task CreateRoleAsync();
        Task AssignRoleToUserAsync(string email, string roleName);
    }
}
