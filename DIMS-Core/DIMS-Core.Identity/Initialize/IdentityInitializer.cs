using DIMS_Core.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.Identity.Initialize
{
    public class IdentityInitializer
    {
        public static async Task Initialize(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            await CheckAndCreateRole(roleManager, "admin");
            await CheckAndCreateRole(roleManager, "mentor");
            await CheckAndCreateRole(roleManager, "member");

            #region Test users
            await CheckAndCreateUser(userManager, "admin@gmail.com", "test123", "admin");
            await CheckAndCreateUser(userManager, "mentor@gmail.com", "test123", "mentor");
            #endregion
        }

        private static async Task CheckAndCreateRole(
            RoleManager<Role> roleManager, 
            string name)
        {
            if (await roleManager.FindByNameAsync(name) is null)
            {
                await roleManager.CreateAsync(new Role(name));
            }
        }

        private static async Task CheckAndCreateUser(
            UserManager<User> userManager, 
            string email, 
            string password, 
            string role)
        {
            if (await userManager.FindByNameAsync(email) is null)
            {
                var user = new User()
                {
                    Email = email,
                    UserName = email
                };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
