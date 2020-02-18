using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SecretSanta.Data;
using SecretSanta.Data.Tests;
using System.Text.Json;
using System.Text;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests
    {
#pragma warning disable CS8618 // justification its get intiailized in TestSetup.
        private SecretSantaWebApplicationFactory Factory { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        [TestInitialize]

        public void TestSetup()
        {
            Factory = new SecretSantaWebApplicationFactory();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Factory.Dispose();
        }

        [TestMethod]

        public async Task Get_ReturnsGifts()
        {
            //Arrange
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Database.EnsureCreated();
            Data.Gift im = SampleData.CreateBatmanGift();
            context.Gifts.Add(im);
            context.SaveChanges();
            HttpClient client = Factory.CreateClient();

            //Act
            //justification using a string is okay in this project other then uri object, by design
#pragma warning disable CA2234 // Pass system uri objects instead of strings
            HttpResponseMessage response = await client.GetAsync("api/Gift");
#pragma warning restore CA2234 // Pass system uri objects instead of strings

            //Assert
            response.EnsureSuccessStatusCode();
            string jsonData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            
            };
            Business.Dto.Gift[] gifts = JsonSerializer.Deserialize<Business.Dto.Gift[]>(jsonData, options);

            Assert.AreEqual(1, gifts.Length);
            Assert.AreEqual(im.Id, gifts[0].Id);
            Assert.AreEqual(im.Title, gifts[0].Title);
            Assert.AreEqual(im.Url, gifts[0].Url);
            Assert.AreEqual(im.UserId, gifts[0].UserId);
            
        

        }
    }
}
