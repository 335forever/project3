﻿namespace QuanLyCongViec.SeedData
{
	public class SeedData
	{
		public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			var roleNames = new[] { "Admin", "Producer", "Designer", "Saler" };

			foreach (var roleName in roleNames)
			{
				var roleExist = await roleManager.RoleExistsAsync(roleName);
				if (!roleExist)
				{
					await roleManager.CreateAsync(new IdentityRole(roleName));
				}
			}

			var adminUser = await userManager.FindByEmailAsync("admin@domain.com");
			if (adminUser == null)
			{
				adminUser = new ApplicationUser
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

