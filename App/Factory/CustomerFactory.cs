using System;
using App.Interface;
using App.Model;

namespace App.Factory
{
    public class CustomerFactory : ICustomerFactory
    {
        public Customer CreateCustomer(string firstname, string surname, string emailAddress, DateTime dateOfBirth,
            Company company)
        {
            return new Customer(firstname, surname, emailAddress, dateOfBirth, company);
        }
    }
}
