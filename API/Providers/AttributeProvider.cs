using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Areas.AppManager.Models;
using API.Areas.RoleManager.Models;
using API.RoleManager.Models;

namespace API.Providers
{
    public class AttributeProvider : Attribute
    {
        private String apiKey;

        public AttributeProvider(String permission)
        {
            var apiKey = HttpContext.Current.Request.Headers["APIKey"];

            var appContext = new AppManagerContext();

            var user = appContext.Apps
                .Where(a => a.Id == apiKey);

            var role = new Role();
        }
    }
}