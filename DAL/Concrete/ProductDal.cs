using DAL.Interface;
using DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class ProductDal : IProductDal
    {
        private readonly string _connectionString;

        public ProductDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            var products = new List<Product>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
            SELECT 
                Products.ProductId,
                Products.Name,
                Products.Price,
                Products.Description,
                Products.CategoryId
            FROM 
                Products
            INNER JOIN 
                Categories ON Products.CategoryId = Categories.CategoryId
            WHERE 
                Products.CategoryId = @CategoryId";

                command.Parameters.AddWithValue("@CategoryId", categoryId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description"))
                                ? null
                                : reader.GetString(reader.GetOrdinal("Description")),
                            CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId"))
                        });
                    }
                }
            }

            return products;
        }
    }
}
