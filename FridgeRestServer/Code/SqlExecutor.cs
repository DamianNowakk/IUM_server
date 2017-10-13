using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using FridgeRestServer.Models;

namespace FridgeRestServer.Controllers
{
    public class SqlExecutor
    {
        private IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);

        public Person Get(int id)
        {
            var sqlQuery = $"SELECT * FROM Person WHERE id = {id}";
            return this.db.Query<Person>(sqlQuery).SingleOrDefault();
        }

        public List<Person> GetAll()
        {
            const string sqlQuery = "SELECT * FROM Person";
            return this.db.Query<Person>(sqlQuery).ToList();
        }

        public void AddPerson(Person person)
        {
            const string sqlQuery = "INSERT INTO Person(Login,Password) values(@Login,@Password); SELECT CAST(SCOPE_IDENTITY() as int)";
            var returnId = this.db.Query<int>(sqlQuery, person).SingleOrDefault();
            person.Id = returnId;
        }
    }
}