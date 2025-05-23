using DevopsLesson3.Data;
using DevopsLesson3.Services;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApi.Test
{
    [TestFixture]
    public class ProductServiceTest
    {
        private ProductService _service;
        private ApplicationDbContext _context;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
          .AddJsonFile("appsettings.Test.json")
          .Build();

            var connStr = configuration.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connStr)
                .Options;

            _context = new ApplicationDbContext(options);
            var repo = new ProductRepository(_context);
            _service = new ProductService(repo);
        }
        [Test]
        public void GetTopProducts_ShouldReturnCorrectCount()
        {
            _service.Add(new Product { Name = "p1", Price = 100 });
            _service.Add(new Product { Name = "p2", Price = 200 });
            _service.Add(new Product { Name = "p3", Price = 300 });

            var result = _service.GetProducts(2).ToList();

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Price, Is.GreaterThanOrEqualTo(result[1].Price));
        }

        [Test]
        public void AddProduct_Should_Save_And_Return()
        {
            var prod = new Product { Name = "Test Product", Price = 100 };
            var result = _service.Add(prod);

            Assert.That(result.Id, Is.GreaterThan(0));
            Assert.That(result.Name, Is.EqualTo("Test Product"));
        }

        [Test]
        public void GetProductById_ShouldReturnProduct()
        {
            var prod = _service.Add(new Product { Name = "Another", Price = 200 });
            var fetched = _service.GetProductById(prod.Id);
            Assert.That(fetched, Is.Not.Null);
        }

        [Test]
        public void UpdateProduct_ShouldChangeData()
        {
            var prod = _service.Add(new Product { Name = "ToUpdate", Price = 50 });
            prod.Name = "Updated";
            prod.Price = 60;

            var updated = _service.Update(prod);
            Assert.That(updated.Name, Is.EqualTo("Updated"));
        }

        [Test]
        public void DeleteProduct_ShouldRemoveProduct()
        {
            var prod = _service.Add(new Product { Name = "ToDelete", Price = 10 });
            var result = _service.Delete(prod.Id);
            Assert.That(result, Is.True);
        }

    }
}
