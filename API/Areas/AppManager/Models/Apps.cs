using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Areas.AppManager.Models
{
    public class Apps
    {
        [Key]
        public String Id { get; set; }

        public String GTAccount { get; set; }
    }
}