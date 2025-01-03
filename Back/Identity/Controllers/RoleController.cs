using IdentityServer.Services.Interfaces;
using Infrastucture;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService) 
        {
            _roleService = roleService;
        }
        [HttpPost]
        public async Task<IActionResult> AssignRoleToUserAsync(string email, string roleName)
        {
            await _roleService.AssignRoleToUserAsync(email, roleName);

            return Ok(new { Message = "the Role assignes succesfull "});
        }
    }
}
