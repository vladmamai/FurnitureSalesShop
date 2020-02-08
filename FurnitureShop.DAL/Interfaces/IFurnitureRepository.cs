using FurnitureShopApp.DAL.Models;
using System.Collections.Generic;

namespace FurnitureShopApp.DAL.Interfaces
{
    public interface IFurnitureRepository: IRepository<Furniture>
    {
        IEnumerable<Furniture> GetFurniturewithShopAndSubtype();
        IEnumerable<FurnitureBillingDataModel> GetFurnitureRelatedToShop();
        IEnumerable<FurnitureBillingDataModel> GetInfoOfBilling();
       // IEnumerable<FurnitureBillingDataModel> GetInfoOfBilling1();
        IEnumerable<Furniture> GetFurnitureByTypeIdAndSubtypeId(int typeId, int subtypeId);
        IEnumerable<Furniture> GetNotAddedFurnitureToStorage();
        IEnumerable<Furniture> GetFurnitureInStorageByShopForAdmin();
    }
}
