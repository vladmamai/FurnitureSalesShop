using FurnitureShopApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureShopApp.BLL.Interfaces
{
    public interface ICheckDetailsService
    {
        decimal CalculateTotalSumOfBill();
        decimal CalculateTotalSumOfBillWithDiscount(int? customerId);
        FurnitureSale FormBillForClient();
        FurnitureSale FormBillForClientWithDiscount();
    }
}
