using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using FridgeRestServer;
using FridgeRestServer.Models;

namespace FridgeRestServer.Controllers
{
    public class PersonController : ApiController
    {
        private readonly SqlExecutorPerson _sqlExecutorPerson;

        public PersonController()
        {
            _sqlExecutorPerson = new SqlExecutorPerson();
        }
                
        // GET: api/Person
        public HttpResponseMessage Get(string login, string password)
        {
            HttpResponseMessage response;
            if (_sqlExecutorPerson.GetPerson(login, password) != null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.Unauthorized);               
            }
            return response;
        }

        // POST: api/Person
        public HttpResponseMessage Post([FromBody]Person person)
        {
            HttpResponseMessage response;
            try
            {
                _sqlExecutorPerson.AddPerson(person);
                response = Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception e) // TO DO
            {
                response = Request.CreateResponse(HttpStatusCode.Conflict);
                response.Content = new StringContent($"login: {person.Login} is exist", Encoding.Unicode);
            }
           
            return response;
        }
    }
}
