using Microsoft.AspNetCore.Mvc;
using FurnitureShopApp.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using FurnitureShopApp.DAL.Models;
using System.Collections.Generic;

namespace FurnitureShopApp.Controllers
{
    public class TypesController : Controller
    {
        private readonly ITypesRepository _repository;

        public TypesController(ITypesRepository repository)
        {
            _repository = repository;
        }

        // GET: Types
        public IActionResult Index()
        {
            return View(_repository.GetAll());
        }

        // GET: Types/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var types = _repository.FindItem
                (m => m.TypeId == id);
            if (types == null)
            {
                return NotFound();
            }

            return View(types);
        }

        // GET: Types/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Types/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TypeId,TypeName")] DAL.Models.Type @type)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(@type);
                return RedirectToAction(nameof(Index));
            }
            return View(@type);
        }

        // GET: Types/Search
        public IActionResult Search()
        {
            ViewData["TypeId"] = new SelectList(_repository.GetAll(), "TypeId", "TypeName");
            return View();
        }

        [HttpPost]
        public IActionResult Search([Bind("TypeId")] Type types)
        {
            return RedirectToAction("IndexSingle", new { typeId = types.TypeId });
        }

        public IActionResult IndexSingle(int typeId)
        {
            IEnumerable<Type> typeInfo = new List<Type>
                {
                    _repository.Get(typeId)
                };
            return View(typeInfo);
        }

        // GET: Types/Edit/5
        public IActionResult Edit(int? id)
        {
            var types = _repository.Get(id);
            if (types == null)
            {
                return NotFound();
            }
            return View(types);
        }

        // POST: Types/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("TypeId,TypeName")] DAL.Models.Type @type)
        {
            if (id != @type.TypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _repository.Update(@type);
                return RedirectToAction(nameof(Index));
            }
            return View(@type);
        }

        // GET: Types/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var types = _repository.FindItem(m => m.TypeId == id);
            if (types == null)
            {
                return NotFound();
            }

            return View(types);
        }

        // POST: Types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
