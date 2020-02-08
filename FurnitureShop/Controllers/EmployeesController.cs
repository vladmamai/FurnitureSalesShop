using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FurnitureShopApp.DAL.Models;
using FurnitureShopApp.DAL.Interfaces;
using System.Collections.Generic;

namespace FurnitureShopApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeePositionRepository _emppositionrepository;
        private readonly IFurnitureShopRepository _shoprepository;
        private readonly IEmployeeRepository _employeerepository;

        public EmployeesController(IEmployeeRepository employeerepository, IFurnitureShopRepository shoprepository, IEmployeePositionRepository emppositionrepository)
        {
            _employeerepository = employeerepository;
            _shoprepository = shoprepository;
            _emppositionrepository = emppositionrepository;
        }

        // GET: Employees
        public IActionResult Index()
        {
            var furnitureSaleContext = _employeerepository.GetEmployeesExtendedInfo();
            return View(furnitureSaleContext);
        }

        // GET: Employees/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeerepository.GetEmployeesExtendedInfo().FirstOrDefault(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["PositionId"] = new SelectList(_emppositionrepository.GetAll(), "PositionId", "PositionName");
            ViewData["ShopId"] = new SelectList(_shoprepository.GetShopInfoForAdmin(), "ShopId", "ShopAddress");
            return View();
        }

        // GET: Employees/Search
        public IActionResult Search()
        {
            ViewData["EmployeeId"] = new SelectList(_employeerepository.GetEmployeesExtendedInfo(), "EmployeeId", "EmpFullName");
            return View();
        }

        [HttpPost]
        public IActionResult Search([Bind("EmployeeId")] Employee employee)
        {
            return RedirectToAction("IndexSingle", new { employeeId = employee.EmployeeId });
        }

        public IActionResult IndexSingle(int employeeId)
        {
            IEnumerable<Employee> employeeInfo = new List<Employee>
                {
                    _employeerepository.GetEmployeeInfoById(employeeId)
                };
            return View(employeeInfo);
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EmployeeId,EmpName,EmpSurname,Passport,EmploymentDate,PositionId,ShopId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeerepository.Create(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PositionId"] = new SelectList(_emppositionrepository.GetAll(), "PositionId", "PositionName", employee.PositionId);
            ViewData["ShopId"] = new SelectList(_shoprepository.GetShopInfoForAdmin(), "ShopId", "ShopAddress", employee.ShopId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeerepository.Get(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["PositionId"] = new SelectList(_emppositionrepository.GetAll(), "PositionId", "PositionName", employee.PositionId);
            ViewData["ShopId"] = new SelectList(_shoprepository.GetShopInfoForAdmin(), "ShopId", "ShopAddress", employee.ShopId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("EmployeeId,EmpName,EmpSurname,Passport,EmploymentDate,PositionId,ShopId")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _employeerepository.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PositionId"] = new SelectList(_emppositionrepository.GetAll(), "PositionId", "PositionName", employee.PositionId);
            ViewData["ShopId"] = new SelectList(_shoprepository.GetShopInfoForAdmin(), "ShopId", "ShopAddress", employee.ShopId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _employeerepository.GetEmployeesExtendedInfo().FirstOrDefault(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _employeerepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
