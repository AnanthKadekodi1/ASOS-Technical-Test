using App.Data;
using System;
using App.Factory;
using App.Interface;
using App.Model;
using App.Validator;

namespace App.Service
{
    public class CustomerService
    {
        private readonly ICustomerDataAccessFactory _customerDataAccessFactory;
        private readonly ICustomerFactory _customerFactory;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICustomerValidator _customerValidator;
        private readonly ICustomerCreditLimitService _customerCreditLimitService;
        private Company _company;
        private Customer _customer;

        private readonly ICreditLimitValidator _creditLimitValidator;
        public CustomerService()
        {
            _customerDataAccessFactory = new CustomerDataAccessFactory();
            _customerFactory = new CustomerFactory();
            _companyRepository = new CompanyRepository();
            _customerValidator = new CustomerValidator();
            _customerCreditLimitService = new CustomerCreditLimitService();
            _creditLimitValidator = new CreditLimitValidator();
        }

        public CustomerService(ICustomerDataAccessFactory customerDataAccessFactory, ICustomerFactory customerFactory, 
            ICompanyRepository companyRepository, ICustomerValidator customerValidator, 
            ICustomerCreditLimitService customerCreditLimitService, ICreditLimitValidator creditLimitValidator)
        {
            _customerDataAccessFactory = customerDataAccessFactory;
            _customerFactory = customerFactory;
            _companyRepository = companyRepository;
            _customerValidator = customerValidator;
            _customerCreditLimitService = customerCreditLimitService;
            _creditLimitValidator = creditLimitValidator;
        }

        public bool AddCustomer(string firstname, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            if (!_customerValidator.ValidateCustomer(firstname, surname, email, dateOfBirth))
            {
                return false;
            }

            try
            {
                _company = _companyRepository.GetById(companyId);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to get company ID - {e.Message}");
                throw;
            }

            try
            {
                _customer = _customerFactory.CreateCustomer(firstname, surname, email, dateOfBirth, _company);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Creating Customer - {e.Message}");
                throw;
            }
            
            _customer.HasCreditLimit = _customerCreditLimitService.AssessCreditLimit(_company.Name);
            _customer.CreditLimit = _customerCreditLimitService.RetrieveCreditLimit(_customer);

            if (!_creditLimitValidator.HasSufficientCreditLimit(_customer))
            {
                return false;
            }

            try
            {
                _customerDataAccessFactory.AddCustomer(_customer);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error adding customer - {e.Message}");
                throw;
            }

            return true;
        }
    }
}
