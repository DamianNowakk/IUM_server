﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Dapper;
using DbExtensions;
using FridgeRestServer.Models;

namespace FridgeRestServer.Controllers
{
    public class SqlExecutorProduct
    {
        private string _connectionStrings = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        private IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString); // TO DO

        public Product GetProduct(int id, User user)
        {

            var sqlQuery = $"SELECT * FROM Product WHERE  id = {id} AND accountLogin = '{user.Login}'";
            return this.db.Query<Product>(sqlQuery).SingleOrDefault();
        }

        public List<Product> GetAllProducts(User user)
        {
            var sqlQuery = $"SELECT * FROM Product WHERE accountLogin = '{user.Login}'";
            return this.db.Query<Product>(sqlQuery).ToList();
        }

        public void AddProduct(Product product)
        {
            const string sqlQuery = "INSERT INTO Product(accountLogin,name,price,amount) values(@UserLogin,@Name,@Price,@Amount); SELECT CAST(SCOPE_IDENTITY() as int)";
            var returnId = this.db.Query<int>(sqlQuery, product).SingleOrDefault();
            product.Id = returnId;
        }

        public void UpdateProduct(Product product)
        {
            var sqlQuery = SQL
                .UPDATE("Product");

            if (product.UserLogin != null)
                sqlQuery.SET("accountLogin = @UserLogin");
            if (product.Name != null)
                sqlQuery.SET("name = @Name");
            if (product.Price != null)
                sqlQuery.SET("price = @Price");
            if (product.Amount != null)
                sqlQuery.SET("amount = @Amount");

            sqlQuery.WHERE("id=@Id");

            this.db.Query(sqlQuery.ToString(), product);
        }

        public void DeleteProduct(int id)
        {
            var sqlQuery = $"DELETE FROM Product WHERE id={id}";
            this.db.Query(sqlQuery);
        }
    }
}