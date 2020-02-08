using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureShopApp.DAL.Repositories
{
    public class CustomerRepository : Repository<Customer, FurnitureSaleContext>, ICustomerRepository
    {
        public CustomerRepository(FurnitureSaleContext context)
           : base(context)
        {
        }
    }
}
