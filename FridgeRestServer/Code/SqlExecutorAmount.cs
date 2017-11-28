using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using DbExtensions;
using FridgeRestServer.Models;

namespace FridgeRestServer.Code
{
    public class SqlExecutorAmount
    {
        private string _connectionStrings = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        private IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString); // TO DO

        public List<Amount> GetAmounts(int? id)
        {
            var sqlQuery = $"SELECT * FROM Amount WHERE productId = {id}";
            var amounts = this.db.Query<Amount>(sqlQuery).ToList();

            return amounts;
        }

        public Amount GetAmount(int? id, string guid)
        {
            var sqlQuery = $"SELECT * FROM Amount WHERE guid='{guid}' AND productId={id}";
            var amount = this.db.Query<Amount>(sqlQuery).SingleOrDefault();

            return amount;
        }


        public void CreateAmount(int? id, string guid)
        {
            var amount = new Amount()
            {
                Guid = guid,
                ProductId = id,
                Value = 0
            };
            CreateAmount(amount);
        }

        public void CreateAmount(Amount amount)
        {
            const string sqlQuery = "INSERT INTO Amount(guid,productId,value) values(@Guid,@ProductId,@Value);";
            this.db.Query<int>(sqlQuery, amount);
        }

        public void UpdateAmount(Amount amount)
        {
            const string sqlQuery = "UPDATE Amount SET value = @Value WHERE productId = @ProductId AND guid = @Guid;";
            this.db.Query(sqlQuery.ToString(), amount);
        }
    }
}