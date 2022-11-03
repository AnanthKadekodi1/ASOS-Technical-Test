using System;
using App.Model;

namespace App.Interface
{
    public interface ICustomerFactory
    {
        Customer CreateCustomer(string firstname, string surname, string emailAddress, DateTime dateOfBirth,
            Company company);
    }
}