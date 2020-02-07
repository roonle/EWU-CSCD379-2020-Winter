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
    public class GroupControllerTests
    {
        [TestMethod]
        public void Create_GroupController_Success()
        {
            //Arrange
            var service = new GroupService();

            //Act
            _ = new GroupController(service);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithoutService_Fails()
        {
            //Arrange

            //Act
            _ = new GroupController(null!);

            //Assert
        }

        [TestMethod]
        public async Task GetById_WithExistingGroup_Success()
        {
            // Arrange
            var service = new GroupService();
            Group Group = SampleData.CreateSantaGroup();
            Group = await service.InsertAsync(Group);

            var controller = new GroupController(service);

            // Act
            ActionResult<Group> rv = await controller.Get(Group.Id);

            // Assert
            Assert.IsTrue(rv.Result is OkObjectResult);
        }

    }

    public class GroupService : IGroupService
    {
        private Dictionary<int, Group> Items { get; } = new Dictionary<int, Group>();

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Group>> FetchAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Group> FetchByIdAsync(int id)
        {
            if (Items.TryGetValue(id, out Group? Group))
            {
                Task<Group> t1 = Task.FromResult<Group>(Group);
                return t1;
            }
            Task<Group> t2 = Task.FromResult<Group>(null!);
            return t2;
        }

        public Task<Group> InsertAsync(Group entity)
        {
            int id = Items.Count + 1;
            Items[id] = new TestGroup(entity, id);
            return Task.FromResult(Items[id]);
        }

        public Task<Group[]> InsertAsync(params Group[] entity)
        {
            throw new NotImplementedException();
        }

        public Task<Group?> UpdateAsync(int id, Group entity)
        {
            throw new NotImplementedException();
        }
    }

    public class TestGroup : Group
    {
        public TestGroup(Group Group, int id)
            // Justification: parameter has been validated by another method call in Group throwing nullException there
#pragma warning disable CA1062 // Validate arguments of public methods
            : base(Group.Title)
#pragma warning restore CA1062 // Validate arguments of public methods
        {
            Id = id;
        }
    }
}

