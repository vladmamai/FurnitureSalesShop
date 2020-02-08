using FurnitureShopApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureShopApp.DAL.Interfaces
{
    public interface IDeliveryRepository: IRepository<Delivery>
    {
        IEnumerable<Delivery> GetDeliveryWithCheck();
        IEnumerable<Delivery> GetDeliveryInfoByShippingDate(DateTime shippingDate);
    }
}
