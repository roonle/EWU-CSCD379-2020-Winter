using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace SecretSanta.Business.Tester
{
    [TestClass]
    public class GiftTests
    {
        [TestMethod]

        public void Create_Gift_Success()
        {
            //arrange
            List<Gift> gifts = new List<Gift>();
            User user = new User(2, "John", "Doe", gifts);
            int id = 5;
            string title = "Dreams";
            string description = "Book";
            string url = "Amazon.com";


            //act
            Gift gift = new Gift(id, title, description, url, user);
            //assert
            Assert.AreEqual(id, gift.Id, "Id value was unexpected");
            Assert.AreEqual<string>(title, gift.Title, "Title value was unexpected");
            Assert.AreEqual<string>(description, gift.Description, "Title value was unexpected");
            Assert.AreEqual<string>(url, gift.Url, "Title value was unexpected");
        }

      
    }
}
