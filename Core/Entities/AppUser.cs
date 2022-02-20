using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surename { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateBorn { get; set; }
        public bool Verified { get; set; }
        public string Token { get; set; }
        public string StoreRoleId { get; set; }
        [ForeignKey(nameof(StoreRoleId))]
        public StoreRoles Roles { get; set; }
    }
}