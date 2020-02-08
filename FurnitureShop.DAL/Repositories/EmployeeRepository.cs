using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShopApp.DAL.Repositories
{
    public class EmployeeRepository : Repository<Employee, FurnitureSaleContext>, IEmployeeRepository
    {
        public EmployeeRepository(FurnitureSaleContext context)
           : base(context)
        {
        }

        public IEnumerable<Employee> GetEmployeesExtendedInfo()
        {
            string directory = Directory.GetCurrentDirectory() + "\\tempFiles\\";
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(int));

            int? userId = null;

            using (FileStream fs = new FileStream(directory + "Admin.json", FileMode.Open))
            {
                userId = (int)jsonFormatter.ReadObject(fs);
            }

            var shopId = Context.EmployeeUser.Include(f => f.Employee)
               .Where(c => c.UserId == userId)
               .Select(f => f.Employee.ShopId).SingleOrDefault();

            return Context.Employee.Include(e => e.Position).Include(e => e.Shop).Where(e => e.ShopId == shopId);
        }

        public Employee GetEmployeeInfoById(int employeeId)
        {
            return Context.Employee.Include(e => e.Position).Include(e => e.Shop)
                .Where(e => e.EmployeeId == employeeId).SingleOrDefault();
        }
    }
}
