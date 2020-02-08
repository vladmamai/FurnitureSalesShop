using Microsoft.AspNetCore.Mvc;
using FurnitureShopApp.DAL.Models;
using FurnitureShopApp.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FurnitureShopApp.Controllers
{
    public class EmployeePositionsController : Controller
    {
        private readonly IEmployeePositionRepository _repository;

        public EmployeePositionsController(IEmployeePositionRepository repository)
        {
            _repository = repository;
        }

        // GET: EmployeePositions
        public IActionResult Index()
        {
            return View(_repository.GetAll());
        }

        // GET: EmployeePositions/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePosition = _repository.FindItem(m => m.PositionId == id);
            if (employeePosition == null)
            {
                return NotFound();
            }

            return View(employeePosition);
        }

        // GET: EmployeePositions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeePositions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PositionId,PositionName,Salary")] EmployeePosition employeePosition)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(employeePosition);
                return RedirectToAction(nameof(Index));
            }
            return View(employeePosition);
        }

        // GET: EmployeePositions/Search
        public IActionResult Search()
        {
           ViewData["PositionId"] = new SelectList(_repository.GetAll(), "PositionId", "PositionName");
            return View();
        }

        [HttpPost]
        public IActionResult Search([Bind("PositionId")] EmployeePosition employeePosition)
        {
            return RedirectToAction("IndexSingle", new { positionId = employeePosition.PositionId });
        }

        public IActionResult IndexSingle(int positionId)
        {
            IEnumerable<EmployeePosition> employeePositionInfo = new List<EmployeePosition>
                {
                    _repository.Get(positionId)
                };
            return View(employeePositionInfo);
        }

        // GET: EmployeePositions/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePosition = _repository.Get(id);
            if (employeePosition == null)
            {
                return NotFound();
            }
            return View(employeePosition);
        }

        // POST: EmployeePositions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("PositionId,PositionName,Salary")] EmployeePosition employeePosition)
        {
            if (id != employeePosition.PositionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repository.Update(employeePosition);
                return RedirectToAction(nameof(Index));
            }
            return View(employeePosition);
        }

        // GET: EmployeePositions/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeePosition = _repository.FindItem(m => m.PositionId == id);
            if (employeePosition == null)
            {
                return NotFound();
            }

            return View(employeePosition);
        }

        // POST: EmployeePositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
