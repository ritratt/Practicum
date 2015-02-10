using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Areas.ToDoList.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }
    }
}