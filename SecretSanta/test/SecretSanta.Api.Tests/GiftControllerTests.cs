using SecretSanta.Api.Controllers;
using SecretSanta.Business;
using SecretSanta.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.Data.Tests;

namespace SecretSanta.Api.Tests
{
    [TestClass]
    public class GiftControllerTests
    {
        [TestMethod]
        public void Create_GiftController_Success()
        {
            //Arrange
            var service = new GiftService();

            //Act
            _ = new GiftController(service);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithoutService_Fails()
        {
            //Arrange

            //Act
            _ = new GiftController(null!);

            //Assert
        }

        [TestMethod]
        public async Task GetById_WithExistingGift_Success()
        {
            // Arrange
            var service = new GiftService();
            Gift Gift = SampleData.CreateBatmanGift();
            Gift = await service.InsertAsync(Gift);

            var controller = new GiftController(service);

            // Act
            ActionResult<Gift> rv = await controller.Get(Gift.Id);

            // Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

    }

    public class GiftService : IGiftService
    {
        private Dictionary<int, Gift> Items { get; } = new Dictionary<int, Gift>();

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Gift>> FetchAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Gift> FetchByIdAsync(int id)
        {
            if (Items.TryGetValue(id, out Gift? Gift))
            {
                Task<Gift> t1 = Task.FromResult<Gift>(Gift);
                return t1;
            }
            Task<Gift> t2 = Task.FromResult<Gift>(null!);
            return t2;
        }

        public Task<Gift> InsertAsync(Gift entity)
        {
            int id = Items.Count + 1;
            Items[id] = new TestGift(entity, id);
            return Task.FromResult(Items[id]);
        }

        public Task<Gift[]> InsertAsync(params Gift[] entity)
        {
            throw new NotImplementedException();
        }

        public Task<Gift?> UpdateAsync(int id, Gift entity)
        {
            throw new NotImplementedException();
        }
    }

    public class TestGift : Gift
    {
        public TestGift(Gift Gift, int id)
            // Justification: parameter has been validated by another method call in Gift throwing nullException there
#pragma warning disable CA1062 // Validate arguments of public methods
            : base(Gift.Title, Gift.Url,Gift.Description,Gift.User)
#pragma warning restore CA1062 // Validate arguments of public methods
        {
            Id = id;
        }
    }
}

