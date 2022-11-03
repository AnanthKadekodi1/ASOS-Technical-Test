using NUnit.Framework;
using System;
using App.Validator;

namespace App.Tests.Validator
{
    class CustomerValidatorTest
    {
        private CustomerValidator _customerValidator;

        [SetUp]
        public void Setup()
        {
            _customerValidator = new CustomerValidator();
        }

        [Test]
        public void Customer_With_Null_First_Name_Returns_False()
        {
            var result = _customerValidator.ValidateCustomer(null, "Kadekodi", "abc@gmail.com", new DateTime(1980, 3, 27));
            Assert.IsFalse(result);
        }

        [Test]
        public void Customer_With_Null_Last_Name_Returns_False()
        {
            var result = _customerValidator.ValidateCustomer("Kadekodi", null, "abc@gmail.com", new DateTime(1980, 3, 27));
            Assert.IsFalse(result);
        }

        [Test]
        public void Customer_With_Both_Null_First_And_Last_Name_Returns_False()
        {
            var result = _customerValidator.ValidateCustomer(null, null, "abc@gmail.com", new DateTime(1980, 3, 27));
            Assert.IsFalse(result);
        }

        [Test]
        public void Customer_With_Incorrect_Email_Returns_False()
        {
            var result = _customerValidator.ValidateCustomer("Ananth", "Kadekodi", "abcgmailcom", new DateTime(1980, 3, 27));
            Assert.IsFalse(result);
        }

        [Test]
        public void Customer_With_Age_Less_Than_21_Returns_False()
        {
            var result = _customerValidator.ValidateCustomer("Ananth", "Kadekodi", "abcgmailcom", new DateTime(2020, 3, 27));
            Assert.IsFalse(result);
        }

        [Test]
        public void Customer_With_Correct_Values_Returns_True()
        {
            var result = _customerValidator.ValidateCustomer("Ananth", "Kadekodi", "abc@gmail.com", new DateTime(1999, 3, 27));
            Assert.IsTrue(result);
        }
    }
}
