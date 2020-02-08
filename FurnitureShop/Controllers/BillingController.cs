using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FurnitureShopApp.DAL.Models;
using FurnitureShopApp.DAL.Interfaces;
using FurnitureShopApp.BLL.Interfaces;
using System.IO;
using System.Runtime.Serialization.Json;

namespace FurnitureShopApp.Controllers
{
    public class BillingController : Controller
    {
        private readonly ICheckDetailsRepository _checkDetailsRepository;
        private readonly IFurnitureRepository _furnitureRepository;
        private readonly ICustomerRepository _customerrepository;
        private readonly ICheckDetailsService _checkDetailsService;

        public BillingController(IFurnitureRepository furnitureRepository, 
            ICheckDetailsRepository checkDetailsRepository, 
            ICustomerRepository customerrepository,
            ICheckDetailsService checkDetailsService)
        {
            _furnitureRepository = furnitureRepository;
            _checkDetailsRepository = checkDetailsRepository;
            _customerrepository = customerrepository;
            _checkDetailsService = checkDetailsService;
        }

        // GET: Billing
        public IActionResult Index()
        {
            var furnitureSaleContext = _furnitureRepository.GetInfoOfBilling();
            return View(furnitureSaleContext);
        }

        // GET: Billing/Create
        public IActionResult Create()
        {
            ViewData["FurnitureId"] = new SelectList(_furnitureRepository.GetFurnitureRelatedToShop(), "FurnitureId", "FurnitureNameWithColor");
            return View();
        }

        // POST: Billing/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CheckDetailsId,QuantitySelected,FurnitureId")] CheckDetails checkDetails)
        {
            if (ModelState.IsValid)
            {
                if (checkDetails.QuantitySelected >_checkDetailsRepository.CountOfFurniture(checkDetails))
                {
                    this.ModelState["QuantitySelected"].Errors.Clear();
                    this.ModelState["QuantitySelected"].Errors.Add("Кількість вибраного товару не може бути більша, ніж є наявна в магазині! Залишилось одиниць товару :" + _checkDetailsRepository.CountOfFurniture(checkDetails));
                    ViewData["FurnitureId"] = new SelectList(_furnitureRepository.GetFurnitureRelatedToShop(), "FurnitureId", "FurnitureNameWithColor", checkDetails.FurnitureId);
                    return View(checkDetails);
                }
                _checkDetailsRepository.Create(checkDetails);
                return RedirectToAction(nameof(Index));
            }

            ViewData["FurnitureId"] = new SelectList(_furnitureRepository.GetFurnitureRelatedToShop(), "FurnitureId", "FurnitureNameWithColor", checkDetails.FurnitureId);
            return View(checkDetails);
        }

        // GET: CheckDetails/Details
        public IActionResult Details()
        {        
            if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "\\tempFiles\\Customer.json"))
            {
                System.IO.File.Delete(Directory.GetCurrentDirectory() + "\\tempFiles\\Customer.json");
            }

            decimal totalSum = _checkDetailsService.CalculateTotalSumOfBill();

            if(totalSum == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["SumOfBill"] = totalSum;
            var checkDetails = _furnitureRepository.GetInfoOfBilling();
            return View(checkDetails);
        }

        // GET: Billing/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkDetails = _checkDetailsRepository.Get(id);
            if (checkDetails == null)
            {
                return NotFound();
            }
            ViewData["FurnitureId"] = new SelectList(_furnitureRepository.GetFurnitureRelatedToShop(), "FurnitureId", "FurnitureNameWithColor", checkDetails.FurnitureId);
            return View(checkDetails);
        }

        // POST: Billing/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CheckDetailsId,QuantitySelected,FurnitureId")] CheckDetails checkDetails)
        {
            if (id != checkDetails.CheckDetailsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (checkDetails.QuantitySelected >_checkDetailsRepository.CountOfFurniture(checkDetails))
                {
                    this.ModelState["QuantitySelected"].Errors.Clear();
                    this.ModelState["QuantitySelected"].Errors.Add("Кількість вибраного товару не може бути більша, ніж є наявна в магазині! Залишилось одиниць товару :" + _checkDetailsRepository.CountOfFurniture(checkDetails));
                    ViewData["FurnitureId"] = new SelectList(_furnitureRepository.GetFurnitureRelatedToShop(), "FurnitureId", "FurnitureNameWithColor", checkDetails.FurnitureId);
                    return View(checkDetails);
                }
                _checkDetailsRepository.Update(checkDetails);
                return RedirectToAction(nameof(Index));
            }

            ViewData["FurnitureId"] = new SelectList(_furnitureRepository.GetFurnitureRelatedToShop(), "FurnitureId", "FurnitureNameWithColor", checkDetails.FurnitureId);
            return View(checkDetails);
        }

        // GET: Billing/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkDetails = _checkDetailsRepository.GetSingleCheckDetailsInfo(m => m.CheckDetailsId == id);
            if (checkDetails == null)
            {
                return NotFound();
            }

            ViewData["FurnitureId"] = new SelectList(_furnitureRepository.GetFurnitureRelatedToShop(), "FurnitureId", "FurnitureNameWithColor", checkDetails.FurnitureId);
            return View(checkDetails);
        }

        // POST: Billing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _checkDetailsRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Discount()
        {
            ViewData["CustomerId"] = new SelectList(_customerrepository.GetAll(), "CustomerId", "CustFullName");
            return View();
        }

        // POST: Billing/Discount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Discount([Bind("CustomerId,CustFullName")] Customer customer)
        {
            string directory = Directory.GetCurrentDirectory() + "\\tempFiles\\";
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(int));

            using (FileStream fs = new FileStream(directory + "Customer.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, customer.CustomerId);
            }

            return RedirectToAction("DetailsWithDiscount", new { customerId = customer.CustomerId });
        }

        // GET: Billing/DetailsWithDiscount
        public IActionResult DetailsWithDiscount(int customerId)
        {
            ViewData["SumOfBill"] = _checkDetailsService.CalculateTotalSumOfBill();
            ViewData["SumOfBillWithDiscount"] = _checkDetailsService.CalculateTotalSumOfBillWithDiscount(customerId);
            Customer customer = _customerrepository.Get(customerId);
            ViewData["Customer"] = customer.CustFullName;
            var checkDetails = _furnitureRepository.GetInfoOfBilling();
            return View(checkDetails);
        }

        // GET: Billing/BillingConfirm
        public IActionResult BillingConfirm()
        {
            var checkDetails = _checkDetailsService.FormBillForClient();
            return View(checkDetails);
        }

        // GET: Billing/BillingConfirmDiscount
        public IActionResult BillingConfirmDiscount()
        {
            var checkDetails = _checkDetailsService.FormBillForClientWithDiscount();
            ViewBag.DiscountForPage = checkDetails.Customer.Discount * 100;
            return View(checkDetails);
        }
    }
}
