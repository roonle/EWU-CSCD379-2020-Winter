using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class UserTests : TestBase
    {
        [TestMethod]
        public async Task CreateUser_ShouldSaveIntoDatabase()
        {
            int userId = -1;
            
            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                var user = new User
                {
                    FirstName = "dang",
                    LastName = "He",
                    
                };
                applicationDbContext.Users.Add(user);

                var user2 = new User
                {
                    FirstName = "Dan",
                    LastName = "Lee",
                    
                };
                applicationDbContext.Users.Add(user2);

                await applicationDbContext.SaveChangesAsync();

                userId = user.Id;
            }

           
            using (var applicationDbContext = new ApplicationDbContext(Options))
            {
                var user = await applicationDbContext.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual("dang", user.FirstName);
                Assert.AreEqual("He", user.LastName);
            }
        }

        [TestMethod]
        public async Task CreateAuthor_ShouldSetFingerPrintDataOnInitialSave()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "dang"));

            int userId = -1;
            
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user = new User
                {
                    FirstName = "dang",
                    LastName = "He",
                    Gifts = new List<Gift>(),
                    UserAndGroup = new List<UserAndGroup>(),
                    Santa = null
                };
                applicationDbContext.Users.Add(user);

                var user2 = new User
                {
                    FirstName = "Dan",
                    LastName = "Lee",
                    Gifts = new List<Gift>(),
                    UserAndGroup = new List<UserAndGroup>(),
                    Santa = null
                };
                applicationDbContext.Users.Add(user2);

                await applicationDbContext.SaveChangesAsync();

                userId = user.Id;
            }

            
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var author = await applicationDbContext.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(author);
                Assert.AreEqual("dang", author.CreatedBy);
                Assert.AreEqual("dang", author.ModifiedBy);
            }
        }

        [TestMethod]
        public async Task CreateAuthor_ShouldSetFingerPrintDataOnUpdate()
        {
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "dang"));

            int userId = -1;
            
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user = new User
                {
                    FirstName = "dang",
                    LastName = "He",
                    Gifts = new List<Gift>(),
                    UserAndGroup = new List<UserAndGroup>(),
                    Santa = null
                };
                applicationDbContext.Users.Add(user);

                var user2 = new User
                {
                    FirstName = "Dan",
                    LastName = "Lee",
                    Gifts = new List<Gift>(),
                    UserAndGroup = new List<UserAndGroup>(),
                    Santa = null
                };
                applicationDbContext.Users.Add(user2);

                await applicationDbContext.SaveChangesAsync();

                userId = user.Id;
            }

            
            httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "gramma"));
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
               
                var user = await applicationDbContext.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();
                user.FirstName = "Danny";
                user.LastName = "Ping";

                await applicationDbContext.SaveChangesAsync();
            }
          
            using (var applicationDbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var user = await applicationDbContext.Users.Where(u => u.Id == userId).SingleOrDefaultAsync();

                Assert.IsNotNull(user);
                Assert.AreEqual("dang", user.CreatedBy);
                Assert.AreEqual("gramma", user.ModifiedBy);
            }
        }
    }
}