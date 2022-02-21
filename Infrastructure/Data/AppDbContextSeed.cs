using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
  public class AppDbContextSeed
  {
        //Other way to seed the DataBase
    public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory)
    {
            try
            {
                if (!context.StoreRoles.Any())
                {

                    var RolesData = File.ReadAllText("../Infrastructure/Data/SeedData/roles.json");
                    var roles = JsonSerializer.Deserialize<List<StoreRoles>>(RolesData);

                    foreach (var item in roles)
                    {
                        item.CreatedAt = DateTime.Now;
                        item.UpdateAt = DateTime.Now;
                        context.StoreRoles.Add(item);
                    }

                   await context.SaveChangesAsync();
                }
            }
            catch (System.Exception ex)
            {
                var logger = loggerFactory.CreateLogger<AppDbContextSeed>();
                logger.LogError(ex.Message);
            }
    }
  }
}
