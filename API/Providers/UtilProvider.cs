using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Areas.AppManager.Models;
using API.Areas.RoleManager.Models;
using API.Models;
using API.RoleManager.Models;

namespace API.Providers
{
    public class UtilProvider : IUtilProvider
    {
        public List<SwipeColumns> SwipeColumninator(IQueryable<DoorSwipe> rawSwipes)
        {
            return rawSwipes
                .Select(d => new SwipeColumns
                {
                    CustId = d.C_CUST_ID_,
                    DoorAccessPostDateTime = d.C_DoorAccessPostDateTime_,
                    TransactionId = d.C_TRAN_ID_,
                    ActualDateTime = d.C_ACTUALDATETIME_,
                    PostDateTime = d.C_POSTDATETIME_,
                    DoorId = d.C_DOOR_ID_
                })
                .ToList();
        }

        public double DateConverter(string dirtyDateTime)
        {
            var splitDirtyDateTime = dirtyDateTime.Split(' ');
            var parsedDate = splitDirtyDateTime[0];
            var parsedTime = splitDirtyDateTime[1].Replace('-', ':');
            var parsedDateTime = DateTime.Parse(parsedDate + " " + parsedTime);
            var baseDateTime = new DateTime(1900, 01, 01, 00, 00, 00);
            var diff = parsedDateTime - baseDateTime;
            var diffAsdays = diff.TotalDays;
            return diffAsdays;
        }

        public bool HasPermission(string p)
        {
            var apiKey = HttpContext.Current.Request.Headers["APIKey"];

            var appContext = new AppManagerContext();
            var roleManagerContext = new RoleManagerContext();

            var permission = roleManagerContext.Permissions.First(perm => perm.Name == p);

            var user = appContext.Apps.FirstOrDefault(a => a.Id == apiKey);

            //ADD LOGIC TO CONNECT USER TO ROLE!!!
            var role = new Role();

            if (role.Permissions.Contains(permission))
                return true;
            
            return false;
        }
    }

    public class SwipeColumns
    {
        public String CustId { get; set; }

        public String DoorAccessPostDateTime { get; set; }

        public String TransactionId { get; set; }

        public String ActualDateTime { get; set; }

        public String PostDateTime { get; set; }

        public String DoorId { get; set; }
    }
}