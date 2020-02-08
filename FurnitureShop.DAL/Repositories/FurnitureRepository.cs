using System.Collections.Generic;
using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System;
using System.Runtime.Serialization;

namespace FurnitureShopApp.DAL.Repositories
{
    public class FurnitureRepository : Repository<Furniture, FurnitureSaleContext>, IFurnitureRepository
    {
        public FurnitureRepository(FurnitureSaleContext context)
           : base(context)
        {

        }

        public IEnumerable<FurnitureBillingDataModel> GetFurnitureRelatedToShop()
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


            IEnumerable<FurnitureBillingDataModel> furnitureList = Context.FurnitureInStorage
                .Include(f => f.Catalog)
                .Include(f => f.Storage)
                .Where(c => c.Storage.ShopId == shopId && c.QuantityInStorage > 0)
                .Select(x => new FurnitureBillingDataModel
                {
                    FurnitureId = x.FurnitureId,
                    FurnitureName = x.Catalog.FurnitureName,
                    FurnitureNameWithColor = x.Catalog.FurnitureName + " ("+ x.Catalog.Color +")"
                }).ToList();

            return furnitureList;
        }

        public IEnumerable<FurnitureBillingDataModel> GetInfoOfBilling()
        {
            return Context.CheckDetails
                .Include(c => c.Check)
                .Include(c => c.Furniture.Catalog)
                .Where(c => c.CheckId == null)
                .Select(x => new FurnitureBillingDataModel
                {
                    CheckDetailsId = x.CheckDetailsId,
                    FurnitureId = x.FurnitureId,
                    FurnitureName = x.Furniture.Catalog.FurnitureName,
                    QuantitySelected = x.QuantitySelected,    
                    RetailPrice = x.Furniture.RetailPrice,
                    Color = x.Furniture.Catalog.Color
                }).ToList();
        }

        public IEnumerable<FurnitureBillingDataModel> GetInfoOfBilling1()
        {
            string directory = Directory.GetCurrentDirectory() + "\\tempFiles\\";
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(int));
            int? userId = null;

            using (FileStream fs = new FileStream(directory + "Seller.json", FileMode.OpenOrCreate))
            {
                userId = (int)jsonFormatter.ReadObject(fs);
            }
            DataContractJsonSerializer jsonFormatterList = new DataContractJsonSerializer(typeof(List<CheckDetails>));

            List<CheckDetails> listOfBilling = null;

            using (FileStream fs = new FileStream(directory + "Billing_Employee_" + userId + ".json", FileMode.OpenOrCreate))
            {
                try
                {
                    listOfBilling = (List<CheckDetails>)jsonFormatterList.ReadObject(fs);
                }
                catch(SerializationException ex)
                {
                    listOfBilling = null;
                    List<FurnitureBillingDataModel> billingList1 = new List<FurnitureBillingDataModel>();
                    return billingList1;
                }
            }

            List<FurnitureBillingDataModel> billingList = new List<FurnitureBillingDataModel>();

            var checkDetailsId = Context.CheckDetails.Max(x => x.CheckDetailsId);

            for (int i = 0; i < listOfBilling.Count; i++)
            {
                billingList.Add(Context.FurnitureInStorage
                .Include(c => c.Catalog)
                .Where(c => c.FurnitureId == listOfBilling[i].FurnitureId)
                .Select(x => new FurnitureBillingDataModel
                {
                    CheckDetailsId = checkDetailsId + 1,
                    FurnitureId = x.FurnitureId,
                    FurnitureName = x.Catalog.FurnitureName,
                    QuantitySelected = listOfBilling[i].QuantitySelected,
                    RetailPrice = x.RetailPrice,
                    Color = x.Catalog.Color
                }).SingleOrDefault());
            }

            var asdfg = billingList;

            return billingList.AsEnumerable();
        }

        public IEnumerable<Furniture> GetFurniturewithShopAndSubtype()
        {
            
            return Context.Furniture.Include(f => f.Company).Include(f => f.Subtype);
        }

        public IEnumerable<Furniture> GetFurnitureByTypeIdAndSubtypeId(int typeId, int subtypeId)
        {
            return Context.Furniture.Include(f => f.Company).Include(f => f.Subtype)
                .Where(f => f.SubtypeId == subtypeId && f.Subtype.TypeId == typeId);
        }

        public IEnumerable<Furniture> GetNotAddedFurnitureToStorage()
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

            IEnumerable<Furniture> furnitureListStorage = Context.FurnitureInStorage
              .Include(f => f.Catalog)
              .Include(f => f.Storage)
              .Where(c => c.Storage.ShopId == shopId)
              .Select(x => new Furniture{ CatalogId = x.CatalogId, FurnitureName = x.Catalog.FurnitureName }) 
              .ToList();

            IEnumerable<Furniture> furnitureListCatalog = Context.Furniture
                .Select(x => new Furniture{ CatalogId = x.CatalogId, FurnitureName = x.FurnitureName + " (" + x.Color + ")" })
                .ToList();
            IEnumerable<Furniture> furnitureListCatalogNew = furnitureListCatalog.Except(furnitureListStorage, new FurnitureComparer()).ToList();

            return furnitureListCatalogNew;
        }

        public IEnumerable<Furniture> GetFurnitureInStorageByShopForAdmin()
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

            IEnumerable<Furniture> furnitureListStorage = Context.FurnitureInStorage
              .Include(f => f.Catalog)
              .Include(f => f.Storage)
              .Where(c => c.Storage.ShopId == shopId)
              .Select(x => new Furniture { CatalogId = x.CatalogId, FurnitureName = x.Catalog.FurnitureName + "(" + x.Catalog.Color + ")" })
              .ToList();

            return furnitureListStorage;
        }
    }

    public class FurnitureComparer : IEqualityComparer<Furniture>
    {
        bool IEqualityComparer<Furniture>.Equals(Furniture x, Furniture y)
        {
            return (x.CatalogId.Equals(y.CatalogId));
        }

        int IEqualityComparer<Furniture>.GetHashCode(Furniture obj)
        {
            if (Object.ReferenceEquals(obj, null))
                return 0;

            return obj.CatalogId.GetHashCode();
        }
    }
}
