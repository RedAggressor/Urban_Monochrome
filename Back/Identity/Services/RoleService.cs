using IdentityServer.Models;
using IdentityServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RoleService(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task CreateRoleAsync()
        {
            string[] roleNames = { "Admin", "User", "Moderator", "Editor", "Manager", "Guest" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames) 
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);

                if (!roleExist) 
                {
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    Console.WriteLine($"{roleName} create succesfull");
                }
            }
        }

        public async Task AssignRoleToUserAsync(string email, string roleName)
        {
            var user = await _userManager.FindByNameAsync(email);
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if(user != null &&  roleExist)
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}
