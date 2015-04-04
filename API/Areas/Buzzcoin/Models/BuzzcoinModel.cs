using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Util;

namespace API.Areas.Buzzcoin.Models
{
    public class BuzzcoinUser
    {
        [Key]
        public String ApiKey { get; set; }

        public String GtId { get; set; }

        public String Balance { get; set; }

        public Swipe LastSwipe { get; set; }

        public DateTime LastUpdated { get; set; }
    }

    public class Swipe
    {
        [Key]
        public int Id { get; set; }

        public String GtId { get; set; }

        public String DoorAccessPostDateTime { get; set; }

        public String TransactionId { get; set; }

        public String ActualDateTime { get; set; }

        public String DoorId { get; set; }
    }

    public class HomeViewModel
    {
        public String GtId { get; set; }

        public String ApiKey { get; set; }
    }
}