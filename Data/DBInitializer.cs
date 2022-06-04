﻿using Microsoft.AspNetCore.Identity;
<<<<<<< HEAD
using UserApi.Data.Enum;
=======
>>>>>>> AuthController
using UserApi.Settings;

namespace UserApi.Data
{
    public class DBInitializer
    {
        public static async Task<bool> Initialize(UserApiContext context, UserManager<ApiUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();


            if (context.Roles.Any() || context.Users.Any()) return false;


            //Adding roles
            var roles = Roles.GetAllRoles();

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var resultAddRole = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!resultAddRole.Succeeded)
                        throw new ApplicationException("Adding role '" + role + "' failed with error(s): " + resultAddRole.Errors);
                }
            }

            //create admin
<<<<<<< HEAD
            ApiUser admin = new ApiUser
            {
                UserName = "Dercraker",
                Email = "152125692618735616",
                EmailConfirmed = true,
                Prenom = "JeanJack",
                Nom = "Goldman",
                Sexe = "Home",
                CreatedAt = DateTime.Now,
                Argent = 0,
                Permis = PermisName.NA,
                Points = 0,
                NbSessions = 0,
                NbSessionsPermis = 0,
                NbSessionsPolice = 0,
=======
            var admin = new ApiUser()
            {
                UserName = "Dercraker",
                Email = "152125692618735616"
>>>>>>> AuthController
            };

            string pwd = "NMdRx$HqyT8jX6";

            IdentityResult? resultAddUser = await userManager.CreateAsync(admin, pwd);
            if (!resultAddUser.Succeeded)
                throw new ApplicationException("Adding user '" + admin.UserName + "' failed with error(s): " + resultAddUser.Errors);

            var resultAddRoleToUser = await userManager.AddToRoleAsync(admin, Roles.Admin);
            if (!resultAddRoleToUser.Succeeded)
                throw new ApplicationException("Adding user '" + admin.UserName + "' to role '" + Roles.Admin + "' failed with error(s): " + resultAddRoleToUser.Errors);


            await context.SaveChangesAsync();

            return true;
        }
    }
}
