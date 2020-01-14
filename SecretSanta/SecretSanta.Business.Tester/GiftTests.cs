using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace SecretSanta.Business.Tester
{
    [TestClass]
    public class GiftTests
    {
        [TestMethod]
#pragma warning disable CA1707 // Identifiers should not contain underscores
        public void Create_Gift_Success()
#pragma warning restore CA1707 // Identifiers should not contain underscores comment: Unit Test are okay
        {
            //arrange
            ArrayList gifts = new ArrayList();
            User user = new User(2, "John", "Doe", gifts);
            int Id = 5;
            string Title = "Dreams";
            string Description = "Book";
            string Url = "Amazon.com";


            //act
            Gift gift = new Gift(Id, Title, Description, Url, user);
            //assert
            // Assert.AreEqual(Id, gift.Id, "Id value was unexpected");
            //Assert.AreEqual<string>(Title, gift.Title, "Title value was unexpected");
        }
    }
}
