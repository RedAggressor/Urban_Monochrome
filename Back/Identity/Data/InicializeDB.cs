using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace IdentityServer.Data
{
    public class InicializeDB
    {
        public static void CreateRoleAsync(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
                    context.Database.Migrate();

                    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    var roles = new List<string> { "Admin", "Moderator", "User" };

                    foreach (var role in roles)
                    {
                        if (!roleMgr.RoleExistsAsync(role).Result)
                        {
                            var result = roleMgr.CreateAsync(new IdentityRole(role)).Result;
                            Console.WriteLine($"role {role} create: {result.Succeeded}");
                        }
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void SeedUserAsync(IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                try
                {
                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                    var testUser = userMgr.FindByNameAsync("testuser").Result;
                    var testManager = userMgr.FindByNameAsync("testmanager").Result;

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

                        if (result.Succeeded)
                        {
                            var roleResult = userMgr.AddToRoleAsync(user, "User").Result;
                            Console.WriteLine($"test user add role: {roleResult.Succeeded}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("tets User exist");
                    }

                    if (testManager == null)
                    {
                        var password = "11111";
                        var user = new IdentityUser
                        {
                            UserName = "testmoderator",
                            Email = "testmoderator@mail.com",
                            EmailConfirmed = true,
                        };

                        var result = userMgr.CreateAsync(user, password).Result;

                        if (result.Succeeded)
                        {
                            var roleResult = userMgr.AddToRoleAsync(user, "Moderator").Result;
                            Console.WriteLine($"test Moderator add role: {roleResult.Succeeded}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("tets Moderator exist");
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
