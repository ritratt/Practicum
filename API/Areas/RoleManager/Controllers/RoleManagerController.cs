using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Areas.RoleManager.Controllers
{
    public class RoleManagerController : Controller
    {
        // GET: RoleManager/RoleManager
        public ActionResult Index()
        {
            return View();
        }
    }
}