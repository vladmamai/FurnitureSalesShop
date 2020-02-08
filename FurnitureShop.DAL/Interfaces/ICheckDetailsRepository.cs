using FurnitureShopApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureShopApp.DAL.Interfaces
{
    public interface ICheckDetailsRepository : IRepository<CheckDetails>
    {
        IEnumerable<CheckDetails> GetCheckDetailsInfo();
        CheckDetails GetSingleCheckDetailsInfo(Func<CheckDetails, bool> predicate);
        IEnumerable<CheckDetails> GetMultipleCheckDetailsInfo(Func<CheckDetails, bool> predicate);
        IEnumerable<CheckDetails> GetCheckDetailsInfoByCheckId(int checkId);
        int CountOfFurniture(CheckDetails checkDetails);
      //  void WriteBillingProduct(CheckDetails product);
      //  CheckDetails GetInfoCheckDetailsFromFileById(int? checkDetailsId);
    }
}
