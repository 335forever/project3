using Microsoft.AspNetCore.Identity;
using QuanLyCongViec.Models.Core;

namespace QuanLyCongViec.SeedData
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var roleNames = new[] { "Admin", "Producer", "Designer", "Saler" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    var role = new Role
                    {
                        Id = Guid.NewGuid(),  
                        Name = roleName  
                    };

                    await roleManager.CreateAsync(role);
                }
            }

            var adminUser = await userManager.FindByEmailAsync("admin@domain.com");
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = "admin@domain.com",
                    Email = "admin@domain.com"
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123*");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
