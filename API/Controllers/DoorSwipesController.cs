using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.Pkcs;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using API.Areas.AppManager.Models;
using API.Models;
using API.Providers;
using API.RoleManager.Models;

namespace API.Controllers
{
    public class DoorSwipesController : ApiController
    {
        private DoorSwipesEntities db = new DoorSwipesEntities();
        private readonly IUtilProvider _utilProvider;

        public DoorSwipesController()
        {
            _utilProvider = new UtilProvider(new AppManagerContext(), new RoleManagerContext());
        }

        // GET: api/DoorSwipes/5
        [ResponseType(typeof(DoorSwipe))]
        public IHttpActionResult GetDoorSwipe(string id)
        {
            DoorSwipe doorSwipe = db.DoorSwipes.Find(id);

            if (doorSwipe == null)
            {
                return NotFound();
            }

            return Ok(doorSwipe);
        }

        // GET: api/DoorSwipes/GetDoorSwipeByGtId/gburdell2
        [Route("api/DoorSwipes/GetDoorSwipesByGtId/{gtid}")]
        public IHttpActionResult GetDoorSwipesByGtId(String gtid)
        {
            var custId = _utilProvider.CustIdResolver(gtid);
            var apiKey = HttpContext.Current.Request.Headers["apikey"];
            
            if ((_utilProvider.HasPermissionForId(apiKey, custId)) || _utilProvider.IsAdmin(apiKey))
            {
                var Swipes = db.DoorSwipes
               .Where(s => s.C_CUST_ID_ == custId);

                var swipeColumns = _utilProvider.SwipeColumninator(Swipes);

                if (swipeColumns.Count == 0)
                    return Ok("No Results found");
                else
                    return Ok(swipeColumns);
            }

            return Ok("You do not have permission to access door swipes for this user.");
           
        }

        // GET: api/DoorSwipes/GetDoorSwipeByLocation/123
        [Route("api/DoorSwipes/GetDoorSwipeByLocation/{LocationId}")]
        public IHttpActionResult GetDoorSwipesByLocation(String LocationId)
        {
            if (!(_utilProvider.HasPermission("Read_Location")))
                return Ok("You do not have permission.");

            var Swipes = db.DoorSwipes
                .Where(s => s.C_DOOR_ID_ == LocationId);
            
            var swipeColumns = _utilProvider.SwipeColumninator(Swipes);

            if (swipeColumns.Count == 0)
                return Ok("No Results found");
            else
                return Ok(swipeColumns);
        }

        //GET: api/DoorSwipes/GetDoorSwipesByDoorName/{DoorName}
        [Route("api/DoorSwipes/GetDoorSwipesByDoorName/{DoorName}")]
        public IHttpActionResult GetDoorSwipesByDoorName(String DoorName)
        {
            /*Optimize this function*/

            var Doors = db.Doors
                .Where(d => d.C_DESCRIPTION_.Contains(DoorName))
                .Select(d => d.C_DOOR_ID_)
                .ToList();

            var Swipes = db.DoorSwipes
                .Where(d => Doors.Contains(d.C_DOOR_ID_));

            var swipeColumns = _utilProvider.SwipeColumninator(Swipes);

            if (swipeColumns.Count == 0)
                return Ok("No Results found");
            else
                return Ok(swipeColumns);
        }

        //GET: api/DoorSwipes/GetDoorSwipesByTimeStamp/{Date}
        [Route("api/DoorSwipes/GetDoorSwipesByTimeStamp/{dateTimeStamp}")]
        public IHttpActionResult GetDoorSwipesByTimeStamp(String dateTimeStamp)
        {
            var inputDate = _utilProvider.DateConverter(dateTimeStamp);
            
            var Swipes =  db.DoorSwipes
                .Where(s => s.C_ACTUALDATETIME_ == inputDate.ToString());
            
            var swipeColumns = _utilProvider.SwipeColumninator(Swipes);

            if (swipeColumns.Count == 0)
                return Ok("No Results found");
                
            return Ok(swipeColumns);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DoorSwipeExists(string id)
        {
            return db.DoorSwipes.Count(e => e.C_TRAN_ID_ == id) > 0;
        }
    }
}