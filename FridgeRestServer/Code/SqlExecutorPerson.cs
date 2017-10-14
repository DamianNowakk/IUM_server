using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using FridgeRestServer.Models;

namespace FridgeRestServer.Controllers
{
    public class SqlExecutorPerson
    {
        private IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);

        public Person GetPerson(string login, string password)
        {
            var sqlQuery = $"SELECT * FROM Person WHERE login = '{login}' AND password = '{password}'";
            return this.db.Query<Person>(sqlQuery).SingleOrDefault();
        }

        public void AddPerson(Person person)
        {
            const string sqlQuery = "INSERT INTO Person(Login,Password) values(@Login,@Password)";
            this.db.Query<Person>(sqlQuery, person);
        }
    }
}