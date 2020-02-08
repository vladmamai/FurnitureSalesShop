using FurnitureShopApp.DAL.Models;
using System.Collections.Generic;

namespace FurnitureShopApp.DAL.Interfaces
{
    public interface IEmployeeRepository: IRepository<Employee>
    {
        IEnumerable<Employee> GetEmployeesExtendedInfo();
        Employee GetEmployeeInfoById(int employeeId);
    }
}
