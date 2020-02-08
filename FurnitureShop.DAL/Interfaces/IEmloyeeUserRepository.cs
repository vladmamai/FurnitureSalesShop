using FurnitureShopApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureShopApp.DAL.Interfaces
{
    public interface IEmployeeUserRepository : IRepository<EmployeeUser>
    {
        EmployeeUser UserValidation(string login, string password);
        EmployeeUser LoadUserInfo(int? userId);
    }
}
