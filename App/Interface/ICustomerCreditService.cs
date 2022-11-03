using System;
using System.Threading.Tasks;

namespace App.Interface
{
    public interface ICustomerCreditService
    {
        Task<int> GetCreditLimit(string firstname, string surname, DateTime dateOfBirth);
    }
}
