using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace FurnitureShopApp.DAL.Repositories
{
    public class CompanyDeveloperRepository : Repository<CompanyDeveloper,FurnitureSaleContext>, ICompanyDeveloperRepository
    {
        public CompanyDeveloperRepository(FurnitureSaleContext context)
            : base(context)
        {
        }

        public IEnumerable<CompanyDeveloper> GetCompanyDevByName(string companyName)
        {
            return Context.CompanyDeveloper
              .Where(c => c.ComName == companyName)
              .ToList();
        }
    }
}
