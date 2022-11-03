using App.Model;

namespace App.Interface
{
    public interface ICompanyRepository
    {
        Company GetById(int id);
    }
}