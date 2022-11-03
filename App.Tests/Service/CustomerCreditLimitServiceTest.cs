using System;
using App.Enum;
using App.Model;
using App.Service;
using NUnit.Framework;

namespace App.Tests.Service
{
    public class CustomerCreditLimitServiceTest
    {
        private Customer _customer;
        private Company _company;
        private CustomerCreditLimitService customerCreditLimitService;

        [SetUp]
        public void Setup()
        {
            customerCreditLimitService = new CustomerCreditLimitService();
        }

        [Test]
        public void Assess_Credit_Limit_Important_Client()
        {
            int credit = 500;
            _company = new Company{Id = 0, Classification = Classification.Gold, Name = Company.VeryImportantClient};
            _customer = new Customer("Ananth", "Kadekodi", "abc@gmail.com", new DateTime(1980, 3, 27), _company);
            _customer.CreditLimit = 500;
            var result = customerCreditLimitService.RetrieveCreditLimit(_customer);
            Assert.AreEqual(credit, result);
        }
    }
}
