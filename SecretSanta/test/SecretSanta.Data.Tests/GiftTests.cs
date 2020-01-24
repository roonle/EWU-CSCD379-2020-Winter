using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Data.Tests
{
    [TestClass]
    public class GiftTests : TestBase
    {
        [TestMethod]
        public async Task AddGift_WithUser_ShouldCreateForeignRelationship()
        {
            var gift = new Gift
            {
                Title = "Drinkery",
                Description = "Book",
                Url = "Amazon.com",

            };
            var user = new User
            {
                FirstName = "Wan",
                LastName = "He",
                Gifts = new List<Gift>(),
                UserAndGroup = new List<UserAndGroup>()
            };
          
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                gift.User = user;

                dbContext.Gifts.Add(gift);

                await dbContext.SaveChangesAsync();
            }

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options))
            {
                var gifts = await dbContext.Gifts.Include(g => g.User).ToListAsync();
                
                Assert.AreEqual(1, gifts.Count);
                Assert.AreEqual(gift.Title, gifts[0].Title);
                Assert.AreNotEqual(0, gifts[0].Id);
                
            }
        }
    }
}