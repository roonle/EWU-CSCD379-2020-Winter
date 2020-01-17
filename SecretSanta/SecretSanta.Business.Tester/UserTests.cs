using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SecretSanta.Business.Tester
{
    [TestClass]
    public class UserTests
    {

        [TestMethod]

        public void Create_newUser_Success()
        {
            //arrange
            List<Gift> gifts = new List<Gift>();
            
            int id = 5;
            string firstName = "John";
            string lastName = "Doe";



            //act
            User user = new User(id, firstName, lastName, gifts); 
            //assert
            Assert.AreEqual(id, user.Id, "Id value was unexpected");
            Assert.AreEqual<string>(firstName, user.FirstName, "firstname value was unexpected");
            Assert.AreEqual<string>(lastName, user.LastName, "Title value was unexpected");
            Assert.AreEqual<List<Gift>>(gifts, user.Gifts, "gifts value was unexpected");

        }
    }
}
