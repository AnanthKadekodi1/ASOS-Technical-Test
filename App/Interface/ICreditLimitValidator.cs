using App.Model;

namespace App.Interface
{
    public interface ICreditLimitValidator
    {
        bool HasSufficientCreditLimit(Customer customer);
    }
}