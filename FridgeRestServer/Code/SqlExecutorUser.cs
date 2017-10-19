using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using FridgeRestServer.Models;

namespace FridgeRestServer.Controllers
{
    public class SqlExecutorUser
    {
        private IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);

        public User GetUser(string login, string password)
        {
            var sqlQuery = $"SELECT * FROM Account WHERE login = '{login}' AND password = '{password}'";
            return this.db.Query<User>(sqlQuery).SingleOrDefault();
        }

        public void AddUser(User user)
        {
            const string sqlQuery = "INSERT INTO Account(Login,Password) values(@Login,@Password)";
            this.db.Query<User>(sqlQuery, user);
        }
    }
}