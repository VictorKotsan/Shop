using System;
using System.Collections.Generic;
using System.Windows;
using DAL.Concrete;
using DTO;
using Microsoft.Extensions.Configuration;

namespace ShopApp
{
    public partial class MainWindow : Window
    {
        private string _connectionString;

        public MainWindow()
        {
            InitializeComponent();

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(@"C:\\Users\\User\\source\\repos\\Shop\\config.json")
                .Build();

            _connectionString = configuration.GetConnectionString("Shop") ?? throw new InvalidOperationException("Connection string is missing.");
        }

        private void GetAllCategories_Click(object sender, RoutedEventArgs e)
        {
            var dal = new CategoryDal(_connectionString);
            List<Category> categories = dal.GetAllCategories();

            DataGridItems.ItemsSource = categories;
        }

        private void GetProductsByCategory_Click(object sender, RoutedEventArgs e)
        {
            int categoryId;
            if (!int.TryParse(Prompt("Enter Category ID:"), out categoryId))
            {
                MessageBox.Show("Invalid input.");
                return;
            }

            var dal = new ProductDal(_connectionString);
            List<Product> products = dal.GetProductsByCategory(categoryId);

            if (products.Count == 0)
                MessageBox.Show("No products found.");
            else
                DataGridItems.ItemsSource = products;
        }

        private void GetCartItems_Click(object sender, RoutedEventArgs e)
        {
            var dal = new CartItemDal(_connectionString);
            List<CartItem> cartItems = dal.GetCartItems();

            DataGridItems.ItemsSource = cartItems;
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            int productId, quantity, userId;

            if (!int.TryParse(Prompt("Enter Product ID:"), out productId) ||
                !int.TryParse(Prompt("Enter Quantity:"), out quantity) ||
                !int.TryParse(Prompt("Enter User ID:"), out userId))
            {
                MessageBox.Show("Invalid input.");
                return;
            }

            var dal = new CartItemDal(_connectionString);
            var cartItem = dal.AddToCart(productId, quantity, userId);

            MessageBox.Show($"CartItem added with ID: {cartItem.CartItemId}");
        }

        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            int cartItemId;

            if (!int.TryParse(Prompt("Enter Cart Item ID to remove:"), out cartItemId))
            {
                MessageBox.Show("Invalid input.");
                return;
            }

            var dal = new CartItemDal(_connectionString);
            dal.RemoveFromCart(cartItemId);

            MessageBox.Show("Cart item removed successfully.");
        }

        private void UpdateCart_Click(object sender, RoutedEventArgs e)
        {
            int cartItemId, productId, quantity;

            if (!int.TryParse(Prompt("Enter Cart Item ID to update:"), out cartItemId) ||
                !int.TryParse(Prompt("Enter new Product ID:"), out productId) ||
                !int.TryParse(Prompt("Enter new Quantity:"), out quantity))
            {
                MessageBox.Show("Invalid input.");
                return;
            }

            var dal = new CartItemDal(_connectionString);
            dal.UpdateCart(cartItemId, productId, quantity);

            MessageBox.Show("Cart item updated successfully.");
        }

        private string Prompt(string message)
        {
            return Microsoft.VisualBasic.Interaction.InputBox(message, "Input Required", "");
        }

        private void StackPanel_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}