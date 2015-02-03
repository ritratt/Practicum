using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;

namespace API.Controllers
{
    public class DoorSwipesController : ApiController
    {
        private DoorSwipesEntities db = new DoorSwipesEntities();

        // GET: api/DoorSwipes
        public IQueryable<DoorSwipe> GetDoorSwipes()
        {
            return db.DoorSwipes;
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

        // GET: api/DoorSwipes/GetDoorSwipeByCustId/123
        [Route("api/DoorSwipes/GetDoorSwipeByCustId/{CustId}")]
        public IHttpActionResult GetDoorSwipeByCustId(String CustId)
        {
            var Swipes = db.DoorSwipes
                .Where(s => s.C_CUST_ID_ == CustId)
                .ToList();

            if (Swipes.Count == 0)
                return Ok("No Results found");
            else
                return Ok(Swipes);
        }

        // GET: api/DoorSwipes/GetDoorSwipeByLocation/123
        [Route("api/DoorSwipes/GetDoorSwipeByLocation/{LocationId}")]
        public IHttpActionResult GetDoorSwipeByLocation(String LocationId)
        {
            var Swipes = db.DoorSwipes
                .Where(s => s.C_DOOR_ID_ == LocationId)
                .ToList();

            if (Swipes == null)
            {
                return NotFound();
            }

            return Ok(Swipes);
        }

        /*// GET: api/DoorSwipes/GetDoorSwipeByName/123
        [Route("api/DoorSwipes/GetDoorSwipeByName/{Name}")]
        public IHttpActionResult GetDoorSwipeByName(String Name)
        {
            
        }*/

        //GET: ap/DoorSwipes/GetDoorSwipesByDoorName/{DoorName}
        [Route("api/DoorSwipes/GetDoorSwipesByDoorName/{DoorName}")]
        public IHttpActionResult GetDoorSwipesByDoorName(String DoorName)
        {
            /*Optimize this function*/

            var Doors = db.Doors
                .Where(d => d.C_DESCRIPTION_.Contains("DoorName"))
                .Select(d => d.C_DOOR_ID_)
                .ToList();

            var Swipes = db.DoorSwipes
                .Where(d => Doors.Contains(d.C_DOOR_ID_))
                .ToList();

            if (Swipes.Count == 0)
            {
                return Ok("No results found");
            }

            return Ok(Swipes);
        }

        //GET: ap/DoorSwipes/GetDoorSwipesByDate/{Date}
        [Route("api/DoorSwipes/GetDoorSwipesByDate/{Date}")]
        public IHttpActionResult GetDoorSwipesByDate(String Date)
        {
            //FIX!!
            DateTime Date_Formatted = DateTime.Parse(Date);

            var Swipes = db.DoorSwipes
                .Where(s => s.C_ACTUALDATETIME_ == Date);

            return Ok(Swipes);
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