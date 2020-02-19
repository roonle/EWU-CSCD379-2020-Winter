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
using AutoMapper;
using SecretSanta.Business;
using System.Net;

namespace SecretSanta.Api.Tests.Controllers
{
    [TestClass]
    public class GiftControllerTests
    {
#pragma warning disable CS8618 // justification its get intiailized in TestSetup.
        private SecretSantaWebApplicationFactory Factory { get; set; }
        private HttpClient Client { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        private IMapper Mapper { get; } = AutomapperConfigurationProfile.CreateMapper();

        

        [TestInitialize]

        public void TestSetup()
        {
            Factory = new SecretSantaWebApplicationFactory();
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Database.EnsureCreated();
            Client = Factory.CreateClient();
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
            Data.Gift im = SampleData.CreateBatmanGift();
            context.Gifts.Add(im);
            context.SaveChanges();


            //Act
            var uri = new System.Uri("api/Gift", UriKind.RelativeOrAbsolute);

            HttpResponseMessage response = await Client.GetAsync(uri);


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

        [TestMethod]
        public async Task Put_WithMissingId_NotFound()
        {
            // Arrange
            Business.Dto.GiftInput im = Mapper.Map<Gift, Business.Dto.Gift>(SampleData.CreateBatmanGift());
            string jsonData = JsonSerializer.Serialize(im);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var uri = new System.Uri("api/Gift", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Put_WithId_UpdatesEntity()
        {
            // Arrange
            var entity = SampleData.CreateBatmanGift();
            using ApplicationDbContext context = Factory.GetDbContext();
            context.Gifts.Add(entity);
            context.SaveChanges();

            Business.Dto.GiftInput im = Mapper.Map<Gift, Business.Dto.Gift>(entity);
            im.Title += "changed";
            im.Description += "changed";
            im.Url += "changed";
            

            string jsonData = JsonSerializer.Serialize(im);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //act
            var uri = new System.Uri($"api/Gift/{entity.Id}", UriKind.RelativeOrAbsolute);
            HttpResponseMessage response = await Client.PutAsync(uri, stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            string retunedJson = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Business.Dto.Gift returnedGift = JsonSerializer.Deserialize<Business.Dto.Gift>(retunedJson, options);

            Assert.AreEqual(im.Title, returnedGift.Title);
            Assert.AreEqual(im.Description, returnedGift.Description);
            Assert.AreEqual(im.Url, returnedGift.Url);
            Assert.AreEqual(im.UserId, returnedGift.UserId);
            

        }

        [TestMethod]
        [DataRow(nameof(Business.Dto.GiftInput.Title))]   
        [DataRow(nameof(Business.Dto.GiftInput.UserId))]
        public async Task Post_WithoutFirstName_BadResult(string propertyName)
        {
            // Arrange
            Data.Gift entity = SampleData.CreateBatmanGift();

            //DTO
            Business.Dto.GiftInput im = Mapper.Map<Gift, Business.Dto.Gift>(entity);
            System.Type inputType = typeof(Business.Dto.GiftInput);
            System.Reflection.PropertyInfo? propInfo = inputType.GetProperty(propertyName);
            propInfo!.SetValue(im, null);

            string jsonData = JsonSerializer.Serialize(im);

            using StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var uri = new System.Uri("api/Gift", UriKind.RelativeOrAbsolute);

            // Act
            HttpResponseMessage response = await Client.PostAsync(uri, stringContent);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }



    }
}
