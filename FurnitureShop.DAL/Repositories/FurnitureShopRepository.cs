using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace FurnitureShopApp.DAL.Repositories
{
    public class FurnitureShopRepository : Repository<FurnitureShop, FurnitureSaleContext>, IFurnitureShopRepository
    {
        public FurnitureShopRepository(FurnitureSaleContext context)
           : base(context)
        {
        }

        public IEnumerable<FurnitureShop> GetShopInfoForAdmin()
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

            return Context.FurnitureShop.Where(e => e.ShopId == shopId).ToList();
        }
    }
}