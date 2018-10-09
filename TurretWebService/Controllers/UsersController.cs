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
using TurretWebService.Data;
using TurretWebService.Models;
using TurretWebService.Params;

namespace TurretWebService.Controllers
{
    //[Authorize]
    public class UsersController : ApiController
    {
        private TurretDBContext db = new TurretDBContext();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/Users/getbyname/{name}
        [HttpGet()]
        [Route("api/Users/getbyname/{name?}")]
        public IHttpActionResult GetUsersByName(string name)
        {
            var users = GetUsersByNameOrContain(name, UsersSearchParam.ByName);
            if (users.Count == 0) return NotFound();
            return Ok(users);
        }

        // GET: api/Users/getifcontain/{substring}
        [HttpGet()]
        [Route("api/Users/getifcontain/{substring?}")]
        public IHttpActionResult GetIfContain(string substring)
        {
            var users = GetUsersByNameOrContain(substring, UsersSearchParam.IfContain);
            if (users.Count == 0) return NotFound();
            return Ok(users);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }

        private List<User> GetUsersByNameOrContain(string nameOrContain, UsersSearchParam searchParam)
        {
            StringComparison comparison = StringComparison.OrdinalIgnoreCase;
            switch (searchParam)
            {
                case UsersSearchParam.ByName: return db.Users.ToList().FindAll((u) => u.Name.IndexOf(nameOrContain, comparison) == 0);
                case UsersSearchParam.IfContain: return db.Users.ToList().FindAll((u) => u.Name.IndexOf(nameOrContain, comparison) >= 0);
                default: return null;
            }
        }
    }
}