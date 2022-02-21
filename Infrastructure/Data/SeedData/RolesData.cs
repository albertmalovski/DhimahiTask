using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.SeedData
{
    public static class RolesData
    {

        private static readonly string[] roles = new[] {
            "Admin",
            "User",
        };

        public static async Task SeedRoles(RoleManager<StoreRoles> roleManager)
        {

            foreach (var role in roles)
            {

               
                    var create = await roleManager.CreateAsync(new StoreRoles
                    {
                       
                        Description=role,
                        CreatedAt = DateTime.Today.Date,
                        UpdateAt = DateTime.Today.Date,
                        Name = role,
                        NormalizedName = role,
                    }) ;

                    if (!create.Succeeded)
                    {

                        throw new System.Exception("Failed to create role");
                    }
                }
        }
    }
}
