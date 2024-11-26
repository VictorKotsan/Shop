using DAL.Concrete;
using DTO;
using BusinessLogic;
using Microsoft.Extensions.Configuration;
using System.IO;
using AutoMapper;
using System.Runtime.CompilerServices;
using System.Data.Common;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(@"C:\\Users\\User\\source\\repos\\Shop\\config.json")
    .Build();

string connectionString = configuration.GetConnectionString("Shop") ?? "";

Console.WriteLine("Welcome to the shop!");

char option = 's';

while (true)
{
    Console.WriteLine("Please enter\n" +
        "'1' to get all Categories\n" +
        "'2' to get products by categoryID\n" +
        "'3' to get all CartItems\n" +
        "'4' to add cartItem\n" +
        "'5' to remove from cart\n" +
        "'6' to update a cart\n" +
        "Q to quit.\n");
    string entryLine = Console.ReadLine() ?? "";
    if (string.IsNullOrWhiteSpace(entryLine) || entryLine.Length > 1)
    {
        Console.WriteLine("Incorrect option!");
        continue;
    }
    option = Convert.ToChar(entryLine.ToLower());

    switch (option)
    {
        case '1':
            GetAllCategories();
            break;
        case '2':
            GetProductsByCategory();
            break;
        case '3':
            GetCartItems();
            break;
        case '4':
            AddToCart();
            break;
        case '5':
            RemoveFromCart();
            break;
        case '6':
            UpdateCart();
            break;
        case 'q':
            return;
        default:
            Console.WriteLine("Incorrect option!");
            break;
    }
}

void AddToCart()
{
    Console.WriteLine("Please enter cartItem product id:");
    int productId = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Please enter quantity:");
    int quantity = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Please enter user Id:");
    int userId = Convert.ToInt32(Console.ReadLine());

    var cartItem = new CartItem
    {
        ProductId = productId,
        Quantity = quantity,
        UserId = userId
    };

    var dal = new CartItemDal(connectionString);

    cartItem = dal.AddToCart(productId, quantity, userId);
    Console.WriteLine($"Added cartItem. Id = {cartItem.CartItemId}");
}

void GetAllCategories()
{
    var dal = new CategoryDal(connectionString);
    List<Category> genres = dal.GetAllCategories();

    foreach (var item in genres)
    {
        Console.WriteLine($"{item.CategoryId}. {item.Name}. {item.Description}");
    }
}
void GetCartItems()
{
    var dal = new CartItemDal(connectionString);
    List<CartItem> cartItems = dal.GetCartItems();

    foreach (var item in cartItems)
    {
        Console.WriteLine($"{item.ProductId}. {item.Quantity}. {item.UserId}");
    }
}
void GetProductsByCategory()
{
    Console.WriteLine("Please enter category id:");
    int id = Convert.ToInt32(Console.ReadLine());

    var dal = new ProductDal(connectionString);
    List<Product> products = dal.GetProductsByCategory(id);

    if (products.Count > 0)
    {
        Console.WriteLine($"Products in category {id}:");
        foreach (var product in products)
        {
            Console.WriteLine($"- ID: {product.ProductId}, Name: {product.Name}, Price: {product.Price}, Description: {product.Description}");
        }
    }
    else
    {
        Console.WriteLine($"No products found for category ID: {id}");
    }
}

void RemoveFromCart()
{
    Console.WriteLine("Please enter cartItem id you wish to delete:");
    int id = Convert.ToInt32(Console.ReadLine());

    var dal = new CartItemDal(connectionString);
    dal.RemoveFromCart(id);
}

void UpdateCart()
{
    Console.WriteLine("Please enter the cart item ID you wish to update:");
    int cartItemId = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Enter the new Product ID:");
    int productId = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Enter the new Quantity:");
    int quantity = Convert.ToInt32(Console.ReadLine());

    var dal = new CartItemDal(connectionString);
    dal.UpdateCart(cartItemId, productId, quantity);

    Console.WriteLine("Cart item updated successfully!");
}