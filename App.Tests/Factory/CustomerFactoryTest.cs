using System;
using App.Model;
using NUnit.Framework;

namespace App.Tests.Factory
{
    class CustomerFactoryTest
    {
        private Customer _customer;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Customer_Object_Generated_Accurately()
        {
            string firstName = "Ananth";
            string lastName = "Kadekodi";
            string emailAddress = "abc@gmail.com";
            DateTime dob = new DateTime(1980, 3, 27);
            Company company = new Company();
            _customer = new Customer(firstName, lastName, emailAddress, dob, company);

            Assert.AreEqual(firstName, _customer.Firstname);
            Assert.AreEqual(lastName, _customer.Surname);
            Assert.AreEqual(emailAddress, _customer.EmailAddress);
            Assert.AreEqual(dob, _customer.DateOfBirth);
        }
    }
}
