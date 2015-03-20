using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using API.Areas.AppManager.Models;
using API.Areas.RoleManager.Models;
using API.RoleManager.Models;

namespace API.Providers
{
    public class AttributeProvider : AuthorizeAttribute
    {
        private readonly string _apiKey;
        public AttributeProvider(string apiKey)
        {
            _apiKey = apiKey;
        }
        protected bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Get the headers
            var headers = httpContext.Request.Headers;

            var appContext = new AppManagerContext();
            var roleManagerContext = new RoleManagerContext();

            var permission = roleManagerContext.Permissions.First(perm => perm.Name == _apiKey);

            var user = appContext.Apps.FirstOrDefault(a => a.Id == _apiKey);

            //ADD LOGIC TO CONNECT USER TO ROLE!!!
            var role = new Role();

            if (role.Permissions.Contains(permission))
                return true;

            return false;
        }
    }
}