using DataflowWebservice.Database;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataflowWebservice.Models;

namespace DataflowWebservice.Controllers
{
    public class ShirtUserController : ApiController
    {
        private DatabaseController databaseController;
        public ShirtUserController()
        {
            databaseController = new DatabaseController();
        }
        // GET: api/ShirtUser
        public IEnumerable<ShirtUser> Get()
        {
            var results = databaseController.FindAll<ShirtUser>(typeof(ShirtUser));
            return results;
        }

        // GET: api/ShirtUser/5
        public ShirtUser Get(int id)
        {
            var found = databaseController.Find(new ShirtUser() { id = id });
            return found;
        }

        // POST: api/ShirtUser
        public void Post([FromBody]ShirtUser shirtUser)
        {
            databaseController.Persist(shirtUser);
        }

        // PUT: api/ShirtUser/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/ShirtUser/5
        //public void Delete(int id)
        //{
        //}
    }
}
