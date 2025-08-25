using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Moduls.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Presistance
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext storeDbContext;
        private readonly StoreIdentityDbContext storeIdentityDbContext;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public DbInitializer(StoreDbContext storeDbContext,
                             StoreIdentityDbContext storeIdentityDbContext,
                             RoleManager<IdentityRole> roleManager,
                             UserManager<AppUser> userManager)
        {
            this.storeDbContext = storeDbContext;
            this.storeIdentityDbContext = storeIdentityDbContext;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task InitializeAsync()
        {
            if (storeDbContext.Database.GetPendingMigrations().Any())
            {
                await storeDbContext.Database.MigrateAsync();
            }
        }

        public async Task InitializeIdentityAsync()
        {
            if (storeIdentityDbContext.Database.GetPendingMigrations().Any())
            {
                await storeIdentityDbContext.Database.MigrateAsync();
            }
            string[] roles = new string[] { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            var AdminEmail = "admin@example.com";
            var AdminUser =await userManager.FindByEmailAsync(AdminEmail);
            if (AdminUser == null)
            {
                AdminUser = new AppUser
                {
                    Email = AdminEmail,
                    UserName = "admin",
                    DisplayName = "Admin"
                };
               
                try
                {
                    await userManager.CreateAsync(AdminUser, "Admin1#");
                    await userManager.AddToRoleAsync(AdminUser, "Admin");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                        Console.WriteLine(ex.InnerException.Message);
                }

            }
        }
    }
}
