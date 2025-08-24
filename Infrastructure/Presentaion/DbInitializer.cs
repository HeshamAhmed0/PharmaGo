using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Presistance
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext storeDbContext;
        private readonly StoreIdentityDbContext storeIdentityDbContext;

        public DbInitializer(StoreDbContext storeDbContext, StoreIdentityDbContext storeIdentityDbContext)
        {
            this.storeDbContext = storeDbContext;
            this.storeIdentityDbContext = storeIdentityDbContext;
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
                await storeDbContext.Database.MigrateAsync();
            }
        }
    }
}
