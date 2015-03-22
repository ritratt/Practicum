using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using API.Models;

namespace API.Providers
{
    interface IUtilProvider
    {
        List<SwipeColumns> SwipeColumninator(IQueryable<DoorSwipe> rawSwipes);

        double DateConverter(string dirtyDate);

        Boolean HasPermission(String p);

        Boolean HasPermissionForId(String apiKey, String id);
    }
}
