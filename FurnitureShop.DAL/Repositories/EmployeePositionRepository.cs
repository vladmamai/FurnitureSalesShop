using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureShopApp.DAL.Repositories
{
    public class EmployeePositionRepository : Repository<EmployeePosition, FurnitureSaleContext>, IEmployeePositionRepository
    {
        public EmployeePositionRepository(FurnitureSaleContext context)
           : base(context)
        {
        }
    }
}
