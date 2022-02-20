using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class StoreRoles : IdentityRole
    {
        public StoreRoles(){}
        public StoreRoles(string roleName) : base(roleName) {}
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.Date)]
        public DateTime UpdateAt { get; set; }
        public string Description { get; set; }
    }
}
