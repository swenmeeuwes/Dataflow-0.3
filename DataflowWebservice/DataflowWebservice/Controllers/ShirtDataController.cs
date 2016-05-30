using DataflowWebservice.Database;
using DataflowWebservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DataflowWebservice.Controllers
{
    public class ShirtDataController : ApiController
    {
        DatabaseController databaseController;
        public ShirtDataController()
        {
            databaseController = new DatabaseController();
        }
        // GET: api/ShirtData
        public IEnumerable<ShirtData> Get()
        {
            return databaseController.FindAll<ShirtData>(typeof(ShirtData));
        }

        // GET: api/ShirtData/5
        public ShirtData Get(int timestamp)
        {
            return databaseController.Find(new ShirtData() { timestamp = timestamp });
        }

        // POST: api/ShirtData
        public void Post([FromBody]ShirtData shirtData)
        {
            databaseController.Persist(shirtData);
        }

        //// PUT: api/ShirtData/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/ShirtData/5
        //public void Delete(int id)
        //{
        //}
    }
}
