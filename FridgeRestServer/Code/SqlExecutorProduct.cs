using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Dapper;
using DbExtensions;
using FridgeRestServer.Code;
using FridgeRestServer.Models;

namespace FridgeRestServer.Controllers
{
    public class SqlExecutorProduct
    {
        private string _connectionStrings = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        private IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString); // TO DO
        private SqlExecutorAmount sqlExecutorAmount;

        public SqlExecutorProduct()
        {
            sqlExecutorAmount = new SqlExecutorAmount();
        }

        private void FillProductAmount(Product product)
        {
            var amounts = sqlExecutorAmount.GetAmounts(product.Id);
            product.Amount = Amount.getAmount(amounts);
        }

        public Product GetProduct(int id)
        {
            var sqlQuery = $"SELECT * FROM Product WHERE  id = {id}";
            var product = this.db.Query<Product>(sqlQuery).SingleOrDefault();
            FillProductAmount(product);

            return product;
        }

        public List<Product> GetAllProducts(User user)
        {
            var sqlQuery = $"SELECT * FROM Product WHERE userLogin = '{user.Login}'";
            var products = this.db.Query<Product>(sqlQuery).ToList();
            foreach (var product in products)
            {
                FillProductAmount(product);
            }
            return products;
        }

        public void AddProduct(Product product, string guid)
        {
            const string sqlQuery = "INSERT INTO Product(userLogin,name,price) values(@UserLogin,@Name,@Price); SELECT CAST(SCOPE_IDENTITY() as int)";
            var returnId = this.db.Query<int>(sqlQuery, product).SingleOrDefault();
            sqlExecutorAmount.CreateAmount(returnId, guid, product.Amount);
            product.Id = returnId;
        }

        public void UpdateProduct(Product product)
        {
            var sqlQuery = SQL
                .UPDATE("Product");

            if (product.UserLogin != null)
                sqlQuery.SET("userLogin = @UserLogin");
            if (product.Name != null)
                sqlQuery.SET("name = @Name");
            if (product.Price != null)
                sqlQuery.SET("price = @Price");

            sqlQuery.WHERE("id=@Id");

            this.db.Query(sqlQuery.ToString(), product);
        }

        public void UpdateAmount(int id, int value, string guid)
        {
            var amount = sqlExecutorAmount.GetAmount(id, guid);
            if (amount == null)
            {
                amount = new Amount()
                {
                    Guid = guid,
                    ProductId = id,
                    Value = value
                };
                sqlExecutorAmount.CreateAmount(amount);
            }
            else
            {
                amount.Value = value;
                sqlExecutorAmount.UpdateAmount(amount);
            }
        }

        public void DeleteProduct(int id)
        {
            var sqlQuery = $"DELETE FROM Product WHERE id={id}";
            this.db.Query(sqlQuery);
        }
    }
}