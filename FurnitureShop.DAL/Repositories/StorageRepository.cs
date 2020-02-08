using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShopApp.DAL.Repositories
{
    public class StorageRepository : Repository<Storage, FurnitureSaleContext>, IStorageRepository
    {
        public StorageRepository(FurnitureSaleContext context)
           : base(context)
        {
        }

        public IEnumerable<Storage> GetStorageWithShop()
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

            return Context.Storage.Include(s => s.Shop).Where(s => s.Shop.ShopId == shopId).ToList();
        }

        public Storage GetStorageInfoById(int storageId)
        {
            return Context.Storage.Include(s => s.Shop).Where(s => s.StorageId == storageId).SingleOrDefault();
        }
    }
}
