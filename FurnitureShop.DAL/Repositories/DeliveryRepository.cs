using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace FurnitureShopApp.DAL.Repositories
{
    public class DeliveryRepository : Repository<Delivery, FurnitureSaleContext>, IDeliveryRepository
    {
        public DeliveryRepository(FurnitureSaleContext context)
           : base(context)
        {

        }

        public IEnumerable<Delivery> GetDeliveryInfoByShippingDate(DateTime shippingDate)
        {
            return Context.Delivery.Include(d => d.Check)
                .Where(f => f.ShippingDate.ToString("dd/MM/yyyy") == shippingDate.ToString("dd/MM/yyyy"));
        }

        public IEnumerable<Delivery> GetDeliveryWithCheck()
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

            return Context.Delivery.Include(d => d.Check).Where(d => d.Check.Employee.ShopId == shopId);
        }
    }
}
