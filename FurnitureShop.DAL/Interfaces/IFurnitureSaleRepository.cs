using FurnitureShopApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureShopApp.DAL.Interfaces
{
    public interface IFurnitureSaleRepository : IRepository<FurnitureSale>
    {
        IEnumerable<FurnitureSale> GetFurnitureSalesInfo();
        FurnitureSale GetInfoAboutConfirmedBill(FurnitureSale customerCheck);
        IEnumerable<FurnitureSale> GetFurnitureSalesInfoByDate(DateTime buyingDate);
    }
}
