using NUnit.Framework;
using DAL.Concrete;
using DTO;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;

[TestFixture]
public class CategoryDalTests
{
    private string _connectionString;
    private CategoryDal _categoryDal;

    [SetUp]
    public void SetUp()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"C:\\Users\\User\\source\\repos\\Shop\\config.json")
            .Build();

        _connectionString = configuration.GetConnectionString("Shop") ?? throw new InvalidOperationException("Connection string is missing.");

        _categoryDal = new CategoryDal(_connectionString);
    }

    [Test]
    public void GetAllCategories_ShouldReturnNonEmptyList()
    {
        var categories = _categoryDal.GetAllCategories();

        Assert.IsNotNull(categories);
        Assert.IsInstanceOf<List<Category>>(categories);
        Assert.IsTrue(categories.Count > 0);
    }

    [Test]
    public void GetAllCategories_ShouldReturnCategories()
    {
        var categories = _categoryDal.GetAllCategories();

        Assert.IsNotNull(categories);
        foreach (var category in categories)
        {
            Assert.IsNotNull(category.Name);
            Assert.IsTrue(category.CategoryId > 0);
        }
    }
}