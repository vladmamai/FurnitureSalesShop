using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace FurnitureShopApp.DAL.Repositories
{
    public class FurnitureInStorageRepository : Repository<FurnitureInStorage, FurnitureSaleContext>, IFurnitureInStorageRepository
    {
        public FurnitureInStorageRepository(FurnitureSaleContext context)
           : base(context)
        {
        }

        public IEnumerable<FurnitureInStorage> GetFurnitureInStorageInfo()
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

            IEnumerable<FurnitureInStorage> furnitureList = Context.FurnitureInStorage
              .Include(f => f.Catalog)
              .Include(f => f.Storage)
              .Where(c => c.Storage.ShopId == shopId).ToList();

            return furnitureList;
        }

        public FurnitureInStorage GetFurnitureInStorageForBillById(int furnitureId)
        {
            return Context.FurnitureInStorage.Where(f => f.FurnitureId == furnitureId).SingleOrDefault();
        }

        public IEnumerable<FurnitureInStorage> GetFurnitureInStorageForEmployee()
        {
            string directory = Directory.GetCurrentDirectory() + "\\tempFiles\\";
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(int));
            int? userId = null;

            using (FileStream fs = new FileStream(directory + "Seller.json", FileMode.Open))
            {
                userId = (int)jsonFormatter.ReadObject(fs);
            }

            var shopId = Context.EmployeeUser.Include(f => f.Employee)
                .Where(c => c.UserId == userId)
                .Select(f => f.Employee.ShopId).SingleOrDefault();


            IEnumerable<FurnitureInStorage> furnitureList = Context.FurnitureInStorage
                .Include(f => f.Catalog)
                .Include(f => f.Storage)
                .Where(c => c.Storage.ShopId == shopId).ToList();

            return furnitureList;
        }

        public IEnumerable<FurnitureInStorage> GetFurnitureByCatalogIdAndStorageId(int catalogId, int storageId)
        {
            return Context.FurnitureInStorage.Include(f => f.Catalog)
                .Include(f => f.Storage)
                .Where(f => f.CatalogId == catalogId && f.StorageId == storageId).ToList();
        }

        public IEnumerable<FurnitureInStorage> GetFurnitureInStorageById(int furnitureId)
        {
            return Context.FurnitureInStorage.Include(f => f.Catalog)
                .Include(f => f.Storage)
                .Where(f => f.FurnitureId == furnitureId).ToList(); 
        }
    }
}
