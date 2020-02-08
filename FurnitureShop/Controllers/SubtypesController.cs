using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FurnitureShopApp.DAL.Models;
using FurnitureShopApp.DAL.Interfaces;
using System.Collections.Generic;

namespace FurnitureShopApp.Controllers
{
    public class SubtypesController : Controller
    {
        private readonly ISubtypeRepository _subtypeRepository;
        private readonly ITypesRepository _typeRepository;

        public SubtypesController(ISubtypeRepository subtypeRepository, ITypesRepository typeRepository)
        {
            _subtypeRepository = subtypeRepository;
            _typeRepository = typeRepository;
        }

        // GET: Subtypes
        public IActionResult Index()
        {
            var furnitureSaleContext = _subtypeRepository.GetSubtypeWithType();
            return View(furnitureSaleContext);
        }

        // GET: Subtypes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subtype = _subtypeRepository.GetSubtypeWithType()
                .FirstOrDefault(m => m.SubtypeId == id);
            if (subtype == null)
            {
                return NotFound();
            }

            return View(subtype);
        }

        // GET: Subtypes/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_typeRepository.GetAll(), "TypeId", "TypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("SubtypeId,SubtypeName,TypeId")] Subtype subtype)
        {
            if (ModelState.IsValid)
            {
                _subtypeRepository.Create(subtype);
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_typeRepository.GetAll(), "TypeId", "TypeName", subtype.TypeId);
            return View(subtype);
        }

        // GET: Subtypes/Search
        public IActionResult Search()
        {
            ViewData["SubtypeId"] = new SelectList(_subtypeRepository.GetAll(), "SubtypeId", "SubtypeName");
            return View();
        }

        [HttpPost]
        public IActionResult Search([Bind("SubtypeId")] Subtype subtype)
        {
            return RedirectToAction("IndexSingle", new { subtypeId = subtype.SubtypeId });
        }

        public IActionResult IndexSingle(int subtypeId)
        {
            IEnumerable<Subtype> subtypeInfo = new List<Subtype>
                {
                    _subtypeRepository.Get(subtypeId)
                };
            return View(subtypeInfo);
        }

        // GET: Subtypes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subtype = _subtypeRepository.Get(id);
            if (subtype == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_typeRepository.GetAll(), "TypeId", "TypeName", subtype.TypeId);
            return View(subtype);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("SubtypeId,SubtypeName,TypeId")] Subtype subtype)
        {
            if (id != subtype.SubtypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _subtypeRepository.Update(subtype);
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_typeRepository.GetAll(), "TypeId", "TypeName", subtype.TypeId);
            return View(subtype);
        }

        // GET: Subtypes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subtype = _subtypeRepository.GetSubtypeWithType()
                 .FirstOrDefault(m => m.SubtypeId == id);
            if (subtype == null)
            {
                return NotFound();
            }

            return View(subtype);
        }

        // POST: Subtypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _subtypeRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
