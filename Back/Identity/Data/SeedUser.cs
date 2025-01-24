using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace IdentityServer.Data
{
    public class SeedUser
    {
        public static void SeedUserAsync(IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
                context.Database.EnsureDeleted();
                context.Database.Migrate();

                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var testUser = userMgr.FindByNameAsync("testuser").Result;

                if (testUser == null)
                {
                    var password = "11111";
                    var user = new IdentityUser
                    {
                        UserName = "testuser",
                        Email = "test@mail.com",
                        EmailConfirmed = true,
                    };

                    var result = userMgr.CreateAsync(user, password).Result;
                    if(result.Succeeded)
                    {
                        Console.WriteLine("test user create");
                    }
                }
                else
                {
                    Console.WriteLine("tetsUser exist");
                }
            }
        }
    }
}
