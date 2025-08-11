using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Presistance
{
    public class DbInitializer(StoreDbContext storeDbContext) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            if (storeDbContext.Database.GetPendingMigrations().Any())
            {
                await storeDbContext.Database.MigrateAsync();
            }
        }
    }
}
