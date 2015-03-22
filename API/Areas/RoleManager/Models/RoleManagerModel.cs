using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace API.Areas.RoleManager.Models
{
    public class User
    {
        [Key]
        public String GTAccount { get; set; }

        public virtual List<Role> Roles { get; set; }

    }

    public class Role
    {
        [Key]
        public String Name { get; set; }

        public virtual List<Permission> Permissions { get; set; }
    }

    public class Permission
    {
        [Key]
        public String Name { get; set; }
    }

    public class CreateRoleViewModel
    {
        public Role Role { get; set; }

        public IEnumerable<SelectListItem> PermissionsList { get; set; }
    }
}