using System;

namespace App.Interface
{
    public interface ICustomerValidator
    {
        bool ValidateCustomer(string firstname, string surname, string email, DateTime dateOfBirth);
    }
}