using System;
using App.Factory;
using App.Interface;
using App.Model;
using App.Service;
using App.Validator;
using Moq;
using NUnit.Framework;

namespace App.Tests.Service
{
    class CustomerServiceTest
    {
        private Mock<ICustomerDataAccessFactory> _customerDataAccessFactory;
        private Mock<ICompanyRepository> _companyRepository;
        private Mock<ICustomerValidator> _customerValidator;
        private Mock<ICustomerCreditLimitService> _customerCreditLimitService;

        private CustomerFactory _customerFactory;

        private CustomerService _customerService;

        private CreditLimitValidator _creditLimitValidator;

        [SetUp]
        public void Setup()
        {
            _customerDataAccessFactory = new Mock<ICustomerDataAccessFactory>();
            _customerFactory = new CustomerFactory();
            _companyRepository = new Mock<ICompanyRepository>();
            _customerValidator = new Mock<ICustomerValidator>();
            _customerCreditLimitService = new Mock<ICustomerCreditLimitService>();
            _creditLimitValidator = new CreditLimitValidator();
        }

        [Test]
        public void CustomerWith_Invalid_Email_ReturnsFalse()
        {
            _customerService = new CustomerService();
            string invalidEmail = "ananthkgmailcom";
            var result = _customerService.AddCustomer("Ananth", "Kadekodi", invalidEmail, new DateTime(1980, 3, 27), 100);
            Assert.IsFalse(result);
        }

        [Test]
        public void CustomerWith_Null_Surname_ReturnsFalse()
        {
            _customerService = new CustomerService();
            var result = _customerService.AddCustomer("Ananth", null, "ananthk@gmail.com", new DateTime(1980, 3, 27), 100);
            Assert.IsFalse(result);
        }


        [Test]
        public void CustomerWith_Null_FirstName_ReturnsFalse()
        {
            _customerService = new CustomerService();
            var result = _customerService.AddCustomer(null, "Kadekodi", "ananthk@gmail.com", new DateTime(1980, 3, 27), 100);
            Assert.IsFalse(result);
        }

        [Test]
        public void CustomerWith_Null_First_And_Last_Name_ReturnsFalse()
        {
            _customerService = new CustomerService();
            var result = _customerService.AddCustomer(null, null, "ananthk@gmail.com", new DateTime(1980, 3, 27), 100);
            Assert.IsFalse(result);
        }

        [Test]
        public void CustomerWith_Age_Less_Than_21_ReturnsFalse()
        {
            _customerService = new CustomerService();
            var result = _customerService.AddCustomer(null, null, "ananthk@gmail.com", new DateTime(2015, 3, 27), 100);
            Assert.IsFalse(result);
        }
        
        [Test]
        public void CustomerWith_Valid_Input_Values_Returns_True()
        {
            var customerValidator = new CustomerValidator();
            _customerService = new CustomerService(_customerDataAccessFactory.Object, _customerFactory, _companyRepository.Object, customerValidator, _customerCreditLimitService.Object, _creditLimitValidator);
            var importantClient = Company.ImportantClient;
            var creditLimit = 5000;

            _companyRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Company
            {
                Name = importantClient
            });

            _customerCreditLimitService.Setup(x => x.RetrieveCreditLimit(It.IsAny<Customer>())).Returns(creditLimit);
            var result = _customerService.AddCustomer("Ananth", "Kadekodi", "ananthk@gmail.com", new DateTime(1980, 3, 27), 100);
            Assert.IsTrue(result);
        }

        [Test]
        public void WhenImportantCustomerAndAboveCreditLimit_ThenAddCustomerReturnsTrue()
        {
            _customerService = new CustomerService(_customerDataAccessFactory.Object, _customerFactory, _companyRepository.Object, _customerValidator.Object, _customerCreditLimitService.Object, _creditLimitValidator);
            var importantClient = Company.ImportantClient;
            var creditLimit = 5000;

            _customerValidator.Setup(x =>
                    x.ValidateCustomer(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                        It.IsAny<DateTime>()))
                .Returns(true);

            _companyRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Company
            {
                Name = importantClient
            });

            _customerCreditLimitService.Setup(x => x.RetrieveCreditLimit(It.IsAny<Customer>())).Returns(creditLimit);

            var result = _customerService.AddCustomer("Ananth", "kadekodi", "ananthk@gmail.com", new DateTime(1995, 3, 27), 100);
            Assert.IsTrue(result);
        }

        [Test]
        public void WhenVeryImportantCustomerAndBelowCreditLimit_ThenAddCustomerReturnsFalse()
        {
            _customerService = new CustomerService(_customerDataAccessFactory.Object, _customerFactory, _companyRepository.Object, _customerValidator.Object, _customerCreditLimitService.Object, _creditLimitValidator);
            var importantClient = Company.VeryImportantClient;
            var creditLimit = 400;

            _customerValidator.Setup(x =>
                    x.ValidateCustomer(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                        It.IsAny<DateTime>()))
                .Returns(true);

            _companyRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Company
            {
                Name = importantClient
            });

            _customerCreditLimitService.Setup(x => x.AssessCreditLimit(It.IsAny<string>())).Returns(true);
            _customerCreditLimitService.Setup(x => x.RetrieveCreditLimit(It.IsAny<Customer>())).Returns(creditLimit);

            var result = _customerService.AddCustomer("Ananth", "kadekodi", "ananthk@gmail.com", new DateTime(1995, 3, 27), 100);
            Assert.IsFalse(result);
        }

        [Test]
        public void WhenVeryImportantCustomerAndAboveCreditLimit_ThenAddCustomerReturnsTrue()
        {
            _customerService = new CustomerService(_customerDataAccessFactory.Object, _customerFactory, _companyRepository.Object, _customerValidator.Object, _customerCreditLimitService.Object, _creditLimitValidator);
            var importantClient = Company.VeryImportantClient;
            var creditLimit = 600;

            _customerValidator.Setup(x =>
                    x.ValidateCustomer(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                        It.IsAny<DateTime>()))
                .Returns(true);

            _companyRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Company
            {
                Name = importantClient
            });

            _customerCreditLimitService.Setup(x => x.AssessCreditLimit(It.IsAny<string>())).Returns(true);
            _customerCreditLimitService.Setup(x => x.RetrieveCreditLimit(It.IsAny<Customer>())).Returns(creditLimit);

            var result = _customerService.AddCustomer("Ananth", "kadekodi", "ananthk@gmail.com", new DateTime(1995, 3, 27), 100);
            Assert.IsTrue(result);
        }

    }
}
