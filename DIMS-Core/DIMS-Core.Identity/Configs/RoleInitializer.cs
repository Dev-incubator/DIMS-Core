using DIMS_Core.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DIMS_Core.Identity.Configs
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "qwerty";

            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new Role()
                {
                    Name = "Admin"
                });
            }

            if (await roleManager.FindByNameAsync("Member") == null)
            {
                await roleManager.CreateAsync(new Role()
                {
                    Name = "Member"
                });
            }

            if (await roleManager.FindByNameAsync("Mentor") == null)
            {
                await roleManager.CreateAsync(new Role()
                {
                    Name = "Mentor"
                });
            }

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
            if (await userManager.FindByNameAsync("mentor@gmail.com") == null)
            {
                User mentor = new User { Email="mentor@gmail.com", UserName="mentor@gmail.com"};
                IdentityResult result = await userManager.CreateAsync(mentor, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(mentor, "Mentor");
                }

            }
            if (await userManager.FindByNameAsync("member@gmail.com") == null)
            {

                User member = new User {Email="member@gmail.com", UserName="member@gmail.com" };
                IdentityResult result = await userManager.CreateAsync(member, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(member, "Member");
                }
            
            }
        }
    }
}