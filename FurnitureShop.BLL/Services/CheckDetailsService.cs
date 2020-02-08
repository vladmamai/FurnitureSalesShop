using FurnitureShopApp.BLL.Interfaces;
using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace FurnitureShopApp.BLL.Services
{
    public class CheckDetailsService : ICheckDetailsService
    {
        private IFurnitureShopUnitOfWork furnitureShopUnitOfWork;
        private IFurnitureRepository furnitureRepository;
        private IFurnitureSaleRepository furnitureSaleRepository;
        private ICustomerRepository customerRepository;
        private ICheckDetailsRepository checkDetailsRepository;
        private IFurnitureInStorageRepository furnitureInStorageRepository;

        public CheckDetailsService(
            IFurnitureShopUnitOfWork furnitureShopUnitOfWork,
            IFurnitureRepository furnitureRepository,
            IFurnitureSaleRepository furnitureSaleRepository,
            ICustomerRepository customerRepository,
            ICheckDetailsRepository checkDetailsRepository,
            IFurnitureInStorageRepository furnitureInStorageRepository)
        {
            this.furnitureShopUnitOfWork = furnitureShopUnitOfWork;
            this.furnitureRepository = furnitureRepository;
            this.furnitureSaleRepository = furnitureSaleRepository;
            this.customerRepository = customerRepository;
            this.checkDetailsRepository = checkDetailsRepository;
            this.furnitureInStorageRepository = furnitureInStorageRepository;
        }

        public decimal CalculateTotalSumOfBill()
        {
            IEnumerable<FurnitureBillingDataModel> list = furnitureRepository.GetInfoOfBilling();
            List<FurnitureBillingDataModel> listOfBill = list.ToList();
            decimal sumOfBill = 0;

            for (int i = 0; i< listOfBill.Count; i++)
            {
                sumOfBill += listOfBill[i].RetailPrice * listOfBill[i].QuantitySelected;
            }

            return Math.Round(sumOfBill,2);
        }

        public decimal CalculateTotalSumOfBillWithDiscount(int? customerId)
        {
            IEnumerable<FurnitureBillingDataModel> list = furnitureRepository.GetInfoOfBilling();
            List<FurnitureBillingDataModel> listOfBill = list.ToList();
            decimal sumOfBill = 0;

            for (int i = 0; i < listOfBill.Count; i++)
            {
                sumOfBill += listOfBill[i].RetailPrice * listOfBill[i].QuantitySelected;
            }

            Customer customer = customerRepository.Get(customerId);

            sumOfBill = sumOfBill - (sumOfBill * (decimal)customer.Discount);

            return Math.Round(sumOfBill, 2);
        }

        public FurnitureSale FormBillForClient()
        {
            string directory = Directory.GetCurrentDirectory() + "\\tempFiles\\";
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(int));
            int employeeId = 0;

            using (FileStream fs = new FileStream(directory + "Seller.json", FileMode.Open))
            {
                employeeId = (int)jsonFormatter.ReadObject(fs);
            }

            decimal totalSum = CalculateTotalSumOfBill();

            FurnitureSale customerCheck = new FurnitureSale
            {
                BuyingDate = DateTime.Now,
                EmployeeId = employeeId,
                TotalPrice = totalSum
            };

            furnitureSaleRepository.Create(customerCheck);

            IEnumerable<CheckDetails> list = checkDetailsRepository.GetMultipleCheckDetailsInfo(m => m.CheckId == null);
            List<CheckDetails> listOfBill = list.ToList();

            List<FurnitureInStorage> furnitureInStorageList = new List<FurnitureInStorage>();

            for (int i = 0; i < listOfBill.Count; i++)
            {

                FurnitureInStorage furniture = furnitureInStorageRepository.GetFurnitureInStorageForBillById(listOfBill[i].FurnitureId);

                furnitureInStorageList.Add(furniture);

                listOfBill[i].CheckId = customerCheck.CheckId;
                furnitureInStorageList[i].QuantityInStorage -= listOfBill[i].QuantitySelected;

                checkDetailsRepository.Update(listOfBill[i]);
                furnitureInStorageRepository.Update(furnitureInStorageList[i]);
            }

            return furnitureSaleRepository.GetInfoAboutConfirmedBill(customerCheck);
        }

        public FurnitureSale FormBillForClientWithDiscount()
        {
            string directory = Directory.GetCurrentDirectory() + "\\tempFiles\\";
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(int));
            int? customerId = null;
            int employeeId = 0;

            using (FileStream fs = new FileStream(directory + "Customer.json", FileMode.Open))
            {
                customerId = (int)jsonFormatter.ReadObject(fs);
            }

            using (FileStream fs = new FileStream(directory + "Seller.json", FileMode.Open))
            {
                employeeId = (int)jsonFormatter.ReadObject(fs);
            }

            decimal totalSum = CalculateTotalSumOfBillWithDiscount(customerId);

            FurnitureSale customerCheck = new FurnitureSale
            {
                BuyingDate = DateTime.Now,
                CustomerId = customerId,
                EmployeeId = employeeId,
                TotalPrice = totalSum
            };

            furnitureSaleRepository.Create(customerCheck);

            IEnumerable<CheckDetails> list = checkDetailsRepository.GetMultipleCheckDetailsInfo(m => m.CheckId == null);
            List<CheckDetails> listOfBill = list.ToList();

            List<FurnitureInStorage> furnitureInStorageList = new List<FurnitureInStorage>();

            for (int i = 0; i < listOfBill.Count; i++)
            {
                FurnitureInStorage furniture = furnitureInStorageRepository.GetFurnitureInStorageForBillById(listOfBill[i].FurnitureId);

                furnitureInStorageList.Add(furniture);

                listOfBill[i].CheckId = customerCheck.CheckId;
                furnitureInStorageList[i].QuantityInStorage -= listOfBill[i].QuantitySelected;

                checkDetailsRepository.Update(listOfBill[i]);
                furnitureInStorageRepository.Update(furnitureInStorageList[i]);
            }

            return furnitureSaleRepository.GetInfoAboutConfirmedBill(customerCheck);
        }
    }
}
