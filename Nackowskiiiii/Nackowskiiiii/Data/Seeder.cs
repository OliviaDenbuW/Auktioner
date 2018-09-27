using Microsoft.AspNetCore.Identity;
using Nackowskiiiii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nackowskiiiii.Data
{
    public class Seeder
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Regular").Result)
            {
                IdentityRole newRole = new IdentityRole();
                newRole.Name = "Regular";

                IdentityResult roleResult = roleManager.CreateAsync(newRole).Result;
            }

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole newRole = new IdentityRole();
                newRole.Name = "Admin";

                IdentityResult roleResult = roleManager.CreateAsync(newRole).Result;
            }
        }

        public static void SeedAdminRoleToUser(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser user = userManager.FindByEmailAsync("admin@nackowskis.com").Result;

            if (user != null)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}
