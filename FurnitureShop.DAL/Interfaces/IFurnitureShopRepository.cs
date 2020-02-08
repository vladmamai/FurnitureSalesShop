using System.Collections.Generic;
using FurnitureShopApp.DAL.Models;

namespace FurnitureShopApp.DAL.Interfaces
{
    public interface IFurnitureShopRepository : IRepository<FurnitureShop>
    {
         IEnumerable<FurnitureShop> GetShopInfoForAdmin();
    }
}
