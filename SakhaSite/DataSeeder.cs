using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SakhaSite.Constants;
using SakhaSite.Models.DataModels;

namespace SakhaSite
{
    public class DataSeeder
    {
        public static void SeedRoles(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var manager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>())
                {
                    var roles = new List<string>()
                    {
                        RoleConstants.Admin,
                        RoleConstants.User
                    };

                    foreach (var item in roles)
                    {
                        if (!manager.RoleExistsAsync(item).Result)
                        {
                            manager.CreateAsync(new IdentityRole()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = item,
                            }).Wait();
                        }
                    }
                }
            }
        }


        public static void SeedUsers(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var manager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>())
                {
                    if (manager.Users.Any())
                        return;
                    var user = new AppUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = "Admin",
                        Email = "admin@MyShop.ir",
                    };
                    manager.CreateAsync(user, "12345").Wait();
                    manager.AddClaimAsync(user, new Claim(ClaimConstants.FullName, "الهام الهی")).Wait();
                    manager.AddToRoleAsync(user, RoleConstants.Admin).Wait();
                }
            }
        }
    }
}
