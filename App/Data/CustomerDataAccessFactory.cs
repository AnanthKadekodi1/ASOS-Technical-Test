using App.Interface;
using App.Model;

namespace App.Data
{
    public class CustomerDataAccessFactory : ICustomerDataAccessFactory
    {
        public void AddCustomer(Customer customer)
        {
            CustomerDataAccess.AddCustomer(customer);
        }
    }
}
