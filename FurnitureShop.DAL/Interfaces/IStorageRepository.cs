using FurnitureShopApp.DAL.Models;
using System.Collections.Generic;

namespace FurnitureShopApp.DAL.Interfaces
{
    public interface IStorageRepository: IRepository<Storage>
    {
        IEnumerable<Storage> GetStorageWithShop();
        Storage GetStorageInfoById(int storageId);
    }
}
