using App.Interface;
using App.Model;

namespace App.Validator
{
    public class CreditLimitValidator : ICreditLimitValidator
    {
        public bool HasSufficientCreditLimit(Customer customer)
        {
            if (customer.HasCreditLimit && customer.CreditLimit < 500)
            {
                return false;
            }

            return true;
        }
    }
}
