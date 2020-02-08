using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShopApp.DAL.Repositories
{
    public class FurnitureSaleRepository : Repository<FurnitureSale, FurnitureSaleContext>, IFurnitureSaleRepository
    {
        public FurnitureSaleRepository(FurnitureSaleContext context)
           : base(context)
        {
        }

        public IEnumerable<FurnitureSale> GetFurnitureSalesInfo()
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

            return Context.FurnitureSale.Include(f => f.Customer).Include(f => f.Employee).Where(f => f.Employee.ShopId == shopId);
        }

        public FurnitureSale GetInfoAboutConfirmedBill(FurnitureSale customerCheck)
        {
            return Context.FurnitureSale.Include(f => f.Customer)
                .Include(f => f.Employee)
                .Include(f => f.CheckDetails)
                .Where(c => c.CustomerId == customerCheck.CustomerId &&
                           c.BuyingDate == customerCheck.BuyingDate &&
                           c.EmployeeId == customerCheck.EmployeeId).SingleOrDefault();
        }

        public IEnumerable<FurnitureSale> GetFurnitureSalesInfoByDate(DateTime buyingDate)
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

            return Context.FurnitureSale.Include(f => f.Customer)
                .Include(f => f.Employee).Where(f => f.BuyingDate.ToString("dd/MM/yyyy") == buyingDate.ToString("dd/MM/yyyy") && f.Employee.ShopId == shopId);
        }
    }
}