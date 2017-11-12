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
    public class UserController : ApiController
    {
        private readonly SqlExecutorUser _sqlExecutorUser;

        public UserController()
        {
            _sqlExecutorUser = new SqlExecutorUser();
        }
                
        // GET: api/User
        public HttpResponseMessage Get(string login, string password)
        {
            HttpResponseMessage response;
            var user = _sqlExecutorUser.GetUser(login, password);
            if (user != null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, user);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.Unauthorized);               
            }
            return response;
        }

        // POST: api/User
        public HttpResponseMessage Post([FromBody]User user)
        {
            HttpResponseMessage response;
            try
            {
                _sqlExecutorUser.AddUser(user);
                response = Request.CreateResponse(HttpStatusCode.Created, user);
            }
            catch (Exception e) // TO DO
            {
                response = Request.CreateResponse(HttpStatusCode.Conflict);
                response.Content = new StringContent($"login: {user.Login} is exist", Encoding.Unicode);
            }
           
            return response;
        }
    }
}
