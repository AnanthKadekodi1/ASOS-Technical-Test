using App.Model;

namespace App.Interface
{
    public interface ICustomerCreditLimitService
    {
        bool AssessCreditLimit(string companyName);

        int RetrieveCreditLimit(Customer customer);
    }
}