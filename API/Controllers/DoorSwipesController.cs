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

        // PUT: api/DoorSwipes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDoorSwipe(string id, DoorSwipe doorSwipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != doorSwipe.C_TRAN_ID_)
            {
                return BadRequest();
            }

            db.Entry(doorSwipe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoorSwipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DoorSwipes
        [ResponseType(typeof(DoorSwipe))]
        public IHttpActionResult PostDoorSwipe(DoorSwipe doorSwipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DoorSwipes.Add(doorSwipe);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DoorSwipeExists(doorSwipe.C_TRAN_ID_))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = doorSwipe.C_TRAN_ID_ }, doorSwipe);
        }

        // DELETE: api/DoorSwipes/5
        [ResponseType(typeof(DoorSwipe))]
        public IHttpActionResult DeleteDoorSwipe(string id)
        {
            DoorSwipe doorSwipe = db.DoorSwipes.Find(id);
            if (doorSwipe == null)
            {
                return NotFound();
            }

            db.DoorSwipes.Remove(doorSwipe);
            db.SaveChanges();

            return Ok(doorSwipe);
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