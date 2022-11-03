using App.Interface;
using System;

namespace App.Validator
{
    public class CustomerValidator : ICustomerValidator
    {
        public bool ValidateCustomer(string firstname, string surname, string email, DateTime dateOfBirth)
        {
            if(!ValidateName(firstname, surname) || !ValidateDoB(dateOfBirth) || !ValidateEmail(email))
            {
                return false;
            }

            return true;
        }

        private bool ValidateName(string firstname, string surname)
        {
            if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(surname))
            {
                return false;
            }

            return true;
        }

        private bool ValidateEmail(string email)
        {
            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            return true;
        }

        private bool ValidateDoB(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day) age--;

            if (age < 21)
            {
                return false;
            }

            return true;
        }
    }
}
