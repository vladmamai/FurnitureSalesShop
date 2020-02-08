using FurnitureShopApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureShopApp.DAL.Interfaces
{
    public interface IFurnitureInStorageRepository : IRepository<FurnitureInStorage>
    {
        IEnumerable<FurnitureInStorage> GetFurnitureInStorageInfo();
        FurnitureInStorage GetFurnitureInStorageForBillById(int furnitureId);
        IEnumerable<FurnitureInStorage> GetFurnitureInStorageForEmployee();
        IEnumerable<FurnitureInStorage> GetFurnitureByCatalogIdAndStorageId(int catalogId, int storageId);
        IEnumerable<FurnitureInStorage> GetFurnitureInStorageById(int furnitureId);
    }
}
