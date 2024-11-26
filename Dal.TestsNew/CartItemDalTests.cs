using NUnit.Framework;
using DAL.Concrete;
using DTO;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;

[TestFixture]
public class CartItemDalTests
{
    private string _connectionString;
    private CartItemDal _cartItemDal;

    [SetUp]
    public void SetUp()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"C:\\Users\\User\\source\\repos\\Shop\\config.json")
            .Build();

        _connectionString = configuration.GetConnectionString("Shop") ?? throw new InvalidOperationException("Connection string is missing.");

        _cartItemDal = new CartItemDal(_connectionString);
    }

    [Test]
    public void AddToCart_ShouldAddItemSuccessfully()
    {
        int productId = 2;
        int quantity = 2;
        int userId = 1;

        var cartItem = _cartItemDal.AddToCart(productId, quantity, userId);

        Assert.IsTrue(cartItem.CartItemId > 0);
    }

    [Test]
    public void GetCartItems_ShouldReturnItems()
    {
        var cartItems = _cartItemDal.GetCartItems();

        Assert.IsNotNull(cartItems);
        Assert.IsInstanceOf<List<CartItem>>(cartItems);
    }

    [Test]
    public void RemoveFromCart_ShouldDeleteItemSuccessfully()
    {
        int productId = 1;

        _cartItemDal.RemoveFromCart(productId);
        var cartItems = _cartItemDal.GetCartItems();

        Assert.IsTrue(cartItems.Find(item => item.ProductId == productId) == null);
    }
}