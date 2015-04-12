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
        private AppManagerContext _appContext;
        private RoleManagerContext _roleManagerContext;

        public UtilProvider(AppManagerContext appManagerContext, RoleManagerContext roleManagerContext)
        {
            _appContext = appManagerContext;
            _roleManagerContext = roleManagerContext;
        }

        public List<SwipeColumns> SwipeColumninator(IQueryable<DoorSwipe> rawSwipes)
        {
            //This function selects the columns the user is allowed to view and returns them as a new object.
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
            //This function takes a timestamp from user and converts it into the number of days since 1st Jan 1900 as a float.
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
            //This function takes a permission as argument and returns true or false if the user has the permission.
            var apiKey = HttpContext.Current.Request.Headers["APIKey"];

            var permission = _roleManagerContext.Permissions.FirstOrDefault(perm => perm.Name == p);

            var gtid = GetGtId(apiKey);
            if (gtid == null)
            {
                //return error?
            }

            var roles = _roleManagerContext.Users
                .Where(u => u.GTAccount == gtid)
                .Select(u => u.Roles)
                .FirstOrDefault();

            foreach (var role in roles)
            {
                if (role.Permissions.Contains(permission))
                    return true;
            }
            
            return false;
        }

        public Boolean HasPermissionForId(String apiKey, String id)
        {
            var GtId = _appContext.Apps
                .Where(a => a.Id == apiKey)
                .Select(r => r.GTAccount)
                .FirstOrDefault();

            var Id = CustIdResolver(GtId);
            if (Id == id)
            {
                return true;
            }
            return false;
        }

        public Boolean IsAdmin(String apiKey)
        {
            var gtId = GetGtId(apiKey);

            var roles = _roleManagerContext.Users
                .Where(u => u.GTAccount == gtId)
                .Select(u => u.Roles)
                .FirstOrDefault();

            foreach (var role in roles)
            {
                if (role.Name == "Admin")
                {
                    return true;
                }
            }
            return false;
        }

        //Util within utils! Yo dawg!
        private String GetGtId(String apiKey)
        {
            var gtid = _appContext.Apps
                .Where(a => a.Id == apiKey)
                .Select(u => u.GTAccount)
                .FirstOrDefault();

            return gtid;
        }

        public String CustIdResolver(string gtid)
        {
            switch (gtid)
            {
                case "gtadmin":
                    return "310620";

                case "rsatpute3":
                    return "291192";

                case "gburdell":
                    return "3114433";
            }

            return "315741";
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