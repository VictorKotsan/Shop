using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using DTO;

namespace DAL.Concrete
{
    public class CartItemDal : ICartItemDal
    {
        private readonly string _connectionString;

        public CartItemDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CartItem AddToCart(int ProductId, int Quantity, int UserId)
        {
            var cartItem = new CartItem();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
            INSERT INTO CartItems (ProductId, Quantity, UserId) 
            OUTPUT inserted.CartItemId 
            VALUES (@ProductId, @Quantity, @UserId)";

                command.Parameters.AddWithValue("@ProductId", ProductId);
                command.Parameters.AddWithValue("@Quantity", Quantity);
                command.Parameters.AddWithValue("@UserId", UserId);

                connection.Open();
                cartItem.CartItemId = (int)command.ExecuteScalar();
            }

            return cartItem;
        }

        public List<CartItem> GetCartItems()
        {
            var cartItems = new List<CartItem>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM CartItems";

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cartItems.Add(new CartItem()
                        {
                            CartItemId = reader.GetInt32(reader.GetOrdinal("CartItemId")),
                            ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId"))
                        });
                    }
                }
            }

            return cartItems;
        }

        public void RemoveFromCart(int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
        DELETE FROM CartItems
        WHERE CartItemId = @CartItemId";

                command.Parameters.AddWithValue("@CartItemId", productId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateCart(int cartItemId, int productId, int quantity)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
            UPDATE CartItems 
            SET ProductId = @ProductId, Quantity = @Quantity 
            WHERE CartItemId = @CartItemId";

                command.Parameters.AddWithValue("@CartItemId", cartItemId);
                command.Parameters.AddWithValue("@ProductId", productId);
                command.Parameters.AddWithValue("@Quantity", quantity);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}