using Microsoft.AspNetCore.Mvc;
using FurnitureShopApp.DAL.Models;
using FurnitureShopApp.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FurnitureShopApp.Controllers
{
    public class CompanyDevelopersController : Controller
    {
        private readonly ICompanyDeveloperRepository _repository;

        public CompanyDevelopersController(ICompanyDeveloperRepository repository)
        {
            _repository = repository;
        }

        // GET: CompanyDevelopers
        public IActionResult Index()
        {
            return View(_repository.GetAll());
        }

        // GET: CompanyDevelopers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyDeveloper = _repository.FindItem
                (m => m.CompanyId == id);
            if (companyDeveloper == null)
            {
                return NotFound();
            }

            return View(companyDeveloper);
        }

        // GET: CompanyDevelopers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanyDevelopers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CompanyId,ComName,ComAddress,ComEmail,ComPhoneNum")] CompanyDeveloper companyDeveloper)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(companyDeveloper);
                return RedirectToAction(nameof(Index));
            }
            return View(companyDeveloper);
        }

        // GET: CompanyDevelopers/Search
        public IActionResult Search()
        {
            ViewData["CompanyId"] = new SelectList(_repository.GetAll(), "CompanyId", "ComName");
            return View();
        }

        [HttpPost]
        public IActionResult Search([Bind("CompanyId")] CompanyDeveloper conpanyDeveloer)
        {
            return RedirectToAction("IndexSingle", new { companyId = conpanyDeveloer.CompanyId });
        }

        public IActionResult IndexSingle(int companyId)
        {
            IEnumerable<CompanyDeveloper> companyInfo = new List<CompanyDeveloper>
                {
                    _repository.Get(companyId)
                };
            return View(companyInfo);
        }

        // GET: CompanyDevelopers/Edit/5
        public IActionResult Edit(int id)
        {
            var companyDeveloper = _repository.Get(id);
            if (companyDeveloper == null)
            {
                return NotFound();
            }
            return View(companyDeveloper);
        }

        // POST: CompanyDevelopers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CompanyId,ComName,ComAddress,ComEmail,ComPhoneNum")] CompanyDeveloper companyDeveloper)
        {
            if (id != companyDeveloper.CompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repository.Update(companyDeveloper);
                return RedirectToAction(nameof(Index));
            }
            return View(companyDeveloper);
        }

        // GET: CompanyDevelopers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyDeveloper = _repository.FindItem(m => m.CompanyId == id);
            if (companyDeveloper == null)
            {
                return NotFound();
            }

            return View(companyDeveloper);
        }

        // POST: CompanyDevelopers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
