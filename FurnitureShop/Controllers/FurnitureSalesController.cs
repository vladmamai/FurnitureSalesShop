using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FurnitureShopApp.DAL.Models;
using FurnitureShopApp.DAL.Interfaces;
using System.Collections.Generic;
using System;

namespace FurnitureShopApp.Controllers
{
    public class FurnitureSalesController : Controller
    {
        private readonly IEmployeeRepository _employeerepository;
        private readonly ICustomerRepository _customerrepository;
        private readonly IFurnitureSaleRepository _furnituresalerepository;

        public FurnitureSalesController(IEmployeeRepository employeerepository, ICustomerRepository customerrepository, IFurnitureSaleRepository furnituresalerepository)
        {
            _employeerepository = employeerepository;
            _customerrepository = customerrepository;
            _furnituresalerepository = furnituresalerepository;
        }

        // GET: FurnitureSales
        public IActionResult Index()
        {
            var furnitureSaleContext = _furnituresalerepository.GetFurnitureSalesInfo();
            return View(furnitureSaleContext);
        }

        // GET: FurnitureSales/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnitureSale = _furnituresalerepository.GetFurnitureSalesInfo().FirstOrDefault(m => m.CheckId == id);
            if (furnitureSale == null)
            {
                return NotFound();
            }

            return View(furnitureSale);
        }

        // GET: FurnitureSales/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_customerrepository.GetAll(), "CustomerId", "CustFullName");
            ViewData["EmployeeId"] = new SelectList(_employeerepository.GetAll(), "EmployeeId", "EmpFullName");
            return View();
        }

        // POST: FurnitureSales/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CheckId,CustomerId,EmployeeId,BuyingDate,TotalPrice")] FurnitureSale furnitureSale)
        {
            if (ModelState.IsValid)
            {
                _furnituresalerepository.Create(furnitureSale);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_customerrepository.GetAll(), "CustomerId", "CustFullName", furnitureSale.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_employeerepository.GetAll(), "EmployeeId", "EmpFullName", furnitureSale.EmployeeId);
            return View(furnitureSale);
        }

        // GET: FurnitureSale/Search
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search([Bind("BuyingDate")] FurnitureSale checkInfo)
        {
            return RedirectToAction("IndexSingle", new { buyingDate = checkInfo.BuyingDate });
        }

        public IActionResult IndexSingle(DateTime buyingDate)
        {
            IEnumerable<FurnitureSale> furnitureSaleInfo =
                _furnituresalerepository.GetFurnitureSalesInfoByDate(buyingDate);

            if(furnitureSaleInfo == null)
            {
               return View();
            }

            return View(furnitureSaleInfo);
        }

        // GET: FurnitureSales/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnitureSale = _furnituresalerepository.Get(id);
            if (furnitureSale == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_customerrepository.GetAll(), "CustomerId", "CustFullName", furnitureSale.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_employeerepository.GetAll(), "EmployeeId", "EmpFullName", furnitureSale.EmployeeId);
            return View(furnitureSale);
        }

        // POST: FurnitureSales/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CheckId,CustomerId,EmployeeId,BuyingDate,TotalPrice")] FurnitureSale furnitureSale)
        {
            if (id != furnitureSale.CheckId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _furnituresalerepository.Update(furnitureSale);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_customerrepository.GetAll(), "CustomerId", "CustFullName", furnitureSale.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_employeerepository.GetAll(), "EmployeeId", "EmpFullName", furnitureSale.EmployeeId);
            return View(furnitureSale);
        }

        // GET: FurnitureSales/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnitureSale = _furnituresalerepository.GetFurnitureSalesInfo().FirstOrDefault(m => m.CheckId == id);
            if (furnitureSale == null)
            {
                return NotFound();
            }

            return View(furnitureSale);
        }

        // POST: FurnitureSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _furnituresalerepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
