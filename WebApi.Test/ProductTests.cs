using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using DevopsLesson3;
using System.Net.Http.Json;
using Entities;

namespace WebApi.Test
{

    /// Bura Task Olan Hisse deyil 
    //WepApi.Test => ProductServiceTeste Get Task Oradadi.
    [TestFixture]
    public class ProductTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;


        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(b =>
            {
                b.ConfigureServices(ser =>
                {

                });
            });
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task GetProducts_ReturnOkResponse()
        {
            var res = await _client.GetAsync("/api/Product/Top?count=0");
            res.EnsureSuccessStatusCode();

            var products = await res.Content.ReadFromJsonAsync<List<Product>>();
            Assert.That(products is not null);
        }

        [Test]
        public async Task GetProductsWithTop_ReturnCorrectProduct()
        {
            var count = 3;
            var res = await _client.GetAsync($"/api/Product/Top?count={count}");
            var prod = await res.Content.ReadFromJsonAsync<List<Product>>();

            res.EnsureSuccessStatusCode();

            Assert.That(prod != null);
            Assert.That(prod?.Count <= count);
        }

        [Test]

        public async Task PostProduct_AddNewProduct()
        {
            var newProd = new Product
            {
                Name = "new Prod",
                Price = 900
            };

            var res = await _client.PostAsJsonAsync("/api/Product", newProd);
            res.EnsureSuccessStatusCode();
            var created = await res.Content.ReadFromJsonAsync<Product>();
            Assert.That(created != null);
            Assert.That(newProd.Name, Is.EqualTo(created.Name));
        }
        [Test]

        public async Task GetProductById_GetByIdProduct()
        {
            var id = 2;
            var res = await _client.GetAsync($"/api/Product?id={id}");
            var prod = await res.Content.ReadFromJsonAsync<Product>();
            res.EnsureSuccessStatusCode();
            Assert.That(prod != null);
        }
        [Test]
        public async Task DeleteProduct_DeleteByIdProduct()
        {
            var id = 3;
            var res = await _client.DeleteAsync($"/api/Product/{id}");
            res.EnsureSuccessStatusCode();

            var result = await res.Content.ReadFromJsonAsync<bool>();
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task Update_UpdateProd()
        {
            var id = 1;

            var getRes = await _client.GetAsync($"/api/Product?id={id}");
            getRes.EnsureSuccessStatusCode();

            var data = await getRes.Content.ReadFromJsonAsync<Product>();
            Assert.That(data, Is.Not.Null);

            //data.Id = await _client.GetAsync("/api/Product/Top?count=1");
            data.Name = "UpdatedFake";
            data.Price = 1234;

            var putResponse = await _client.PutAsJsonAsync($"/api/Product?id={id}", data);
            putResponse.EnsureSuccessStatusCode();

            var updated = await putResponse.Content.ReadFromJsonAsync<Product>();

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated.Name, Is.EqualTo("UpdatedFake"));
            Assert.That(updated.Price, Is.EqualTo(1234));
        }

    }
}
