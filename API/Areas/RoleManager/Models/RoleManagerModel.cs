using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Areas.RoleManager.Models
{
    public class User
    {
        [Key]
        public String GTAccount { get; set; }

        public List<Role> Roles { get; set; }

    }

    public class Role
    {
        [Key]
        public String Name { get; set; }

        public List<Permission> Permissions { get; set; }
    }

    public class Permission
    {
        [Key]
        public String Name { get; set; }
    }
}