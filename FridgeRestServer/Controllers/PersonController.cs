using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FridgeRestServer;
using FridgeRestServer.Models;

namespace FridgeRestServer.Controllers
{
    public class PersonController : ApiController
    {
        private readonly SqlExecutor _sqlExecutor;

        public PersonController()
        {
            _sqlExecutor = new SqlExecutor();
        }

        // GET: api/Person
        public IEnumerable<Person> Get()
        {
            return _sqlExecutor.GetAll();
        }

        // GET: api/Person/5
        public Person Get(int id)
        {
            return _sqlExecutor.Get(id);
        }

        // POST: api/Person
        public HttpResponseMessage Post([FromBody]Person person)
        {
            _sqlExecutor.AddPerson(person);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, $"person/{person.Id}");
            return response;
        }

        // PUT: api/Person/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Person/5
        public void Delete(int id)
        {
        }
    }
}
