using FurnitureShopApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureShopApp.DAL.Interfaces
{
    public interface ICompanyDeveloperRepository : IRepository<CompanyDeveloper>
    {
        IEnumerable<CompanyDeveloper> GetCompanyDevByName(string companyName);
    }
}
