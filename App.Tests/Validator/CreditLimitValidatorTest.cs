using System;
using App.Model;
using App.Validator;
using NUnit.Framework;

namespace App.Tests.Validator
{
    class CreditLimitValidatorTest
    {
        private CreditLimitValidator _creditLimitValidator;
        private Company _company;
        private Customer _customer;

        [SetUp]
        public void Setup()
        {
            _creditLimitValidator = new CreditLimitValidator();
        }

        [Test]
        public void Return_False_Customer_Has_Credit_Limit_And_Less_Than_500()
        {
            _customer = new Customer("Ananth", "Kadekodi", "abc@gmail.com", new DateTime(1980, 3, 27), _company);
            _customer.HasCreditLimit = true;
            _customer.CreditLimit = 400;

            var result = _creditLimitValidator.HasSufficientCreditLimit(_customer);
            Assert.IsFalse(result);
        }

        [Test]
        public void Return_True_Customer_Has_No_Credit_Limit_And_Less_Than_500()
        {
            _customer = new Customer("Ananth", "Kadekodi", "abc@gmail.com", new DateTime(1980, 3, 27), _company);
            _customer.HasCreditLimit = false;
            _customer.CreditLimit = 400;

            var result = _creditLimitValidator.HasSufficientCreditLimit(_customer);
            Assert.IsTrue(result);
        }

        [Test]
        public void Return_True_Customer_Has_Credit_Limit_And_More_Than_500()
        {
            _customer = new Customer("Ananth", "Kadekodi", "abc@gmail.com", new DateTime(1980, 3, 27), _company);
            _customer.HasCreditLimit = true;
            _customer.CreditLimit = 550;

            var result = _creditLimitValidator.HasSufficientCreditLimit(_customer);
            Assert.IsTrue(result);
        }
    }
}
