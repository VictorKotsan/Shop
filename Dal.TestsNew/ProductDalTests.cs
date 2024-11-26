using NUnit.Framework;
using DAL.Concrete;
using DTO;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;

[TestFixture]
public class ProductDalTests
{
    private string _connectionString;
    private ProductDal _productDal;

    [SetUp]
    public void SetUp()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"C:\\Users\\User\\source\\repos\\Shop\\config.json")
            .Build();

        _connectionString = configuration.GetConnectionString("Shop") ?? throw new InvalidOperationException("Connection string is missing.");

        _productDal = new ProductDal(_connectionString);
    }

    [Test]
    public void GetProductsByCategory_ShouldReturnProducts()
    {
        int categoryId = 1;

        var products = _productDal.GetProductsByCategory(categoryId);

        Assert.IsNotNull(products);
        Assert.IsInstanceOf<List<Product>>(products);
        Assert.IsTrue(products.Count > 0);

        foreach (var product in products)
        {
            Assert.That(product.CategoryId, Is.EqualTo(categoryId));
            Assert.IsNotNull(product.Name);
        }
    }

    [Test]
    public void GetProductsByCategory_ShouldReturnEmptyList_WhenCategoryDoesNotExist()
    {
        int nonExistentCategoryId = -1;

        var products = _productDal.GetProductsByCategory(nonExistentCategoryId);

        Assert.IsNotNull(products);
        Assert.IsInstanceOf<List<Product>>(products);
        Assert.That(products.Count, Is.EqualTo(0));
    }
}