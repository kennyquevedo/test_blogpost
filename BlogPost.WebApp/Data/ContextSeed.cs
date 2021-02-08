using BlogPost.Common;
using BlogPost.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.WebApp.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed
            await roleManager.CreateAsync(new IdentityRole(RoleValues.Viewer));
            await roleManager.CreateAsync(new IdentityRole(RoleValues.Super));
            await roleManager.CreateAsync(new IdentityRole(RoleValues.Editor));
            await roleManager.CreateAsync(new IdentityRole(RoleValues.Writer));
        }

        public static async Task SeedSuperAdminAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new AppUser
            {
                UserName = "admin",
                Email = "admin@fake.email.com",
                FirstName = "John",
                LastName = "Wick",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Password.123");
                    await userManager.AddToRoleAsync(defaultUser, RoleValues.Editor);
                    await userManager.AddToRoleAsync(defaultUser, RoleValues.Super);
                    await userManager.AddToRoleAsync(defaultUser, RoleValues.Viewer);
                    await userManager.AddToRoleAsync(defaultUser, RoleValues.Writer);
                }

            }
        }
    }
}
