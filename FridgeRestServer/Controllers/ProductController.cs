using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using FridgeRestServer.Models;

namespace FridgeRestServer.Controllers
{
    public class ProductController : ApiController
    {
        private readonly SqlExecutorProduct _sqlExecutorProduct;
        private readonly SqlExecutorPerson _sqlExecutorPerson;

        public ProductController()
        {
            _sqlExecutorProduct = new SqlExecutorProduct();
            _sqlExecutorPerson = new SqlExecutorPerson();
        }

        // GET: api/Product
        public IEnumerable<Product> Get(string login, string password)
        {
            var person = _sqlExecutorPerson.GetPerson(login, password);
            if (person != null)
            {
                return _sqlExecutorProduct.GetAllProducts(person);
            }

            return null;
        }

        // GET: api/Product/5
        public Product Get(int id, string login, string password)
        {
            var person = _sqlExecutorPerson.GetPerson(login, password);
            if (person != null)
            {
                return _sqlExecutorProduct.GetProduct(id, person);
            }
            return null;
        }

        // POST: api/Product
        public HttpResponseMessage Post([FromBody]Product product, string login, string password)
        {
            HttpResponseMessage response;
            var person = _sqlExecutorPerson.GetPerson(login, password);
            if (person != null)
            {
                product.PersonLogin = login;
                _sqlExecutorProduct.AddProduct(product);
                response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Request.RequestUri, $"Product/{product.Id}");
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            return response;
        }

        // PUT: api/Product/5
        public HttpResponseMessage Put([FromBody]Product product,int id, string login, string password)
        {
            HttpResponseMessage response;
            var person = _sqlExecutorPerson.GetPerson(login, password);
            if (person != null)
            {
                product.Id = id;
                _sqlExecutorProduct.UpdateProduct(product);
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            return response;
        }

        // DELETE: api/Product/5
        public HttpResponseMessage Delete(int id, string login, string password)
        {
            HttpResponseMessage response;
            var person = _sqlExecutorPerson.GetPerson(login, password);
            if (person != null)
            {             
                _sqlExecutorProduct.DeleteProduct(id);
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            return response;
        }
    }
}
