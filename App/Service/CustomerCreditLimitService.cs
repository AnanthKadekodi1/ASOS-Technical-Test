using App.Interface;
using App.Model;

namespace App.Service
{
    public class CustomerCreditLimitService : ICustomerCreditLimitService
    {
        public bool AssessCreditLimit(string companyName)
        {
            if (companyName == Company.VeryImportantClient)
            {
                return true;
            }
            return false;
        }

        public int RetrieveCreditLimit(Customer customer)
        {
            int creditLimit;
            switch (customer.Company.Name)
            {
                case Company.VeryImportantClient:
                    creditLimit = customer.CreditLimit;
                    break;

                case Company.ImportantClient:
                    using (var customerCreditService = new CustomerCreditService())
                    {
                        var limit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth).Result;
                        limit = limit * 2;
                        creditLimit = limit;
                    }

                    break;

                default:
                    using (var customerCreditService = new CustomerCreditService())
                    {
                        var limit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth).Result;
                        creditLimit = limit;
                    }
                    break;
            }

            return creditLimit;
        }
    }
}
