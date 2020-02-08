using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace FurnitureShopApp.DAL.Repositories
{
    public class CheckDetailsRepository : Repository<CheckDetails, FurnitureSaleContext>, ICheckDetailsRepository
    {
        public CheckDetailsRepository(FurnitureSaleContext context)
           : base(context)
        {

        }

        public IEnumerable<CheckDetails> GetCheckDetailsInfo()
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

            return Context.CheckDetails.Include(c => c.Check.Customer).Include(c => c.Furniture.Catalog).Where(c => c.Check.Employee.ShopId == shopId);
        }

        public CheckDetails GetSingleCheckDetailsInfo(Func<CheckDetails, bool> predicate)
        {
            return Context.CheckDetails.Include(c => c.Check.Customer).Include(c => c.Furniture.Catalog)
                .Where(predicate).SingleOrDefault();
        }

        public IEnumerable<CheckDetails> GetMultipleCheckDetailsInfo(Func<CheckDetails, bool> predicate)
        {
            return Context.CheckDetails.Include(c => c.Check.Customer)
                .Include(c => c.Furniture.Catalog)
                .Where(predicate);
        }

        public IEnumerable<CheckDetails> GetCheckDetailsInfoByCheckId(int checkId)
        {
            return Context.CheckDetails.Include(c => c.Check.Customer)
                .Include(c => c.Furniture.Catalog)
                .Where(c => c.CheckId == checkId);
        }

      /*  public void WriteBillingProduct(CheckDetails product)
        {
            string directory = Directory.GetCurrentDirectory() + "\\tempFiles\\";
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(int));
            int? userId = null;

            using (FileStream fs = new FileStream(directory + "Seller.json", FileMode.OpenOrCreate))
            {
                userId = (int)jsonFormatter.ReadObject(fs);
            }

            jsonFormatter = new DataContractJsonSerializer(typeof(List<CheckDetails>));

            List<CheckDetails> listOfBilling = new List<CheckDetails>();

            using (FileStream fs = new FileStream(directory + "Billing_Employee_" + userId + ".json", FileMode.OpenOrCreate))
            {
                try
                {
                    listOfBilling = (List<CheckDetails>)jsonFormatter.ReadObject(fs);
                }
                catch (SerializationException ex)
                {
                    
                }
            }

            listOfBilling.Add(product);

            using (FileStream fs = new FileStream(directory + "Billing_Employee_" + userId + ".json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, listOfBilling);
            }
        }*/

       /* public CheckDetails GetInfoCheckDetailsFromFileById(int? checkDetailsId)
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
                catch (SerializationException ex)
                {
                    CheckDetails checkDetailsNull = new CheckDetails();
                    return checkDetailsNull;
                }
            }

            CheckDetails checkDetails = listOfBilling.Where(c => c.CheckDetailsId == checkDetailsId).SingleOrDefault();
            return checkDetails;
        }*/

        public int CountOfFurniture(CheckDetails checkDetails)
        {
            FurnitureInStorage furniture = Context.FurnitureInStorage
                .Where(c => c.FurnitureId == checkDetails.FurnitureId).SingleOrDefault();

            return furniture.QuantityInStorage;
        }
    }
}
