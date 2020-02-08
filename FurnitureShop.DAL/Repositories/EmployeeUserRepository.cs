using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FurnitureShopApp.DAL.Repositories
{
    public class EmployeeUserRepository : Repository<EmployeeUser, FurnitureSaleContext>, IEmployeeUserRepository
    {
        public EmployeeUserRepository(FurnitureSaleContext context)
            : base(context)
        {
        }

        public EmployeeUser UserValidation(string login, string password)
        {
            return Context.EmployeeUser.Include(f => f.Employee.Position)
                .FirstOrDefault(m => m.Login == login && m.Password == password);
        }

        public EmployeeUser LoadUserInfo(int? userId)
        {
            return Context.EmployeeUser.Include(f => f.Employee.Position)
                .FirstOrDefault(m => m.UserId == userId);
        }
    }
}
