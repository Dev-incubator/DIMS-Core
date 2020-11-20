﻿using DIMS_Core.Identity.Entities;
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
            if (await roleManager.FindByNameAsync("admin") is null)
            {
                await roleManager.CreateAsync(new Role("admin"));
            }
            if (await roleManager.FindByNameAsync("mentor") is null)
            {
                await roleManager.CreateAsync(new Role("mentor"));
            }
            if (await roleManager.FindByNameAsync("member") is null)
            {
                await roleManager.CreateAsync(new Role("member"));
            }

            #region Test users
            const string defaultAdminMail = "admin@gmail.com";
            const string defaultAdminPassword = "test123";

            const string defaultMentorMail = "mentor@gmail.com";
            const string defaultMentorPassword = "test123";

            if (await userManager.FindByNameAsync(defaultAdminMail) is null)
            {
                var admin = new User
                {
                    Email = defaultAdminMail, 
                    UserName = defaultAdminMail
                };
                var result = await userManager.CreateAsync(admin, defaultAdminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
            if (await userManager.FindByNameAsync(defaultMentorMail) is null)
            {
                var mentor = new User
                {
                    Email = defaultMentorMail, 
                    UserName = defaultMentorMail
                };
                var result = await userManager.CreateAsync(mentor, defaultMentorPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(mentor, "mentor");
                }
            }
            #endregion
        }
    }
}