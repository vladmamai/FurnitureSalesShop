using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FurnitureShopApp.DAL.Models;
using FurnitureShopApp.DAL.Interfaces;
using System.Collections.Generic;

namespace FurnitureShopApp.Controllers
{
    public class FurnituresController : Controller
    {
        private readonly ICompanyDeveloperRepository _companyDevRepository;
        private readonly ITypesRepository _typeRepository;
        private readonly ISubtypeRepository _subtypeRepository;
        private readonly IFurnitureRepository _furnitureRepository;

        public FurnituresController(ICompanyDeveloperRepository companyDevRepository, 
            ISubtypeRepository subtypeRepository, 
            IFurnitureRepository furnitureRepository,
            ITypesRepository typeRepository)
        {
            _companyDevRepository = companyDevRepository;
            _subtypeRepository = subtypeRepository;
            _furnitureRepository = furnitureRepository;
            _typeRepository = typeRepository;
        }

        // GET: Furnitures
        public IActionResult Index()
        {
            var furnitureSaleContext = _furnitureRepository.GetFurniturewithShopAndSubtype();
            return View(furnitureSaleContext);
        }

        // GET: Furnitures/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furniture = _furnitureRepository.GetFurniturewithShopAndSubtype()
                .FirstOrDefault(m => m.CatalogId == id);
            if (furniture == null)
            {
                return NotFound();
            }

            return View(furniture);
        }

        // GET: Furnitures/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_companyDevRepository.GetAll(), "CompanyId", "ComName");
            ViewData["SubtypeId"] = new SelectList(_subtypeRepository.GetAll(), "SubtypeId", "SubtypeName");
            return View();
        }

        // POST: Furnitures/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CatalogId,FurnitureName,SubtypeId,CompanyId,Material,Color,Guarantee,Description")] Furniture furniture)
        {
            if (ModelState.IsValid)
            {
                _furnitureRepository.Create(furniture);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_companyDevRepository.GetAll(), "CompanyId", "ComName", furniture.CompanyId);
            ViewData["SubtypeId"] = new SelectList(_subtypeRepository.GetAll(), "SubtypeId", "SubtypeName", furniture.SubtypeId);
            return View(furniture);
        }

        // GET: Furnitures/Search
        public IActionResult Search()
        {
            ViewData["TypeId"] = new SelectList(_typeRepository.GetAll(), "TypeId", "TypeName");
            ViewData["SubtypeId"] = new SelectList(_subtypeRepository.GetAll(), "SubtypeId", "SubtypeName");
            return View();
        }

        [HttpPost]
        public IActionResult Search([Bind("TypeId, SubtypeId")] Subtype furnitureInfo)
        {
            return RedirectToAction("IndexSingle", new { typeId = furnitureInfo.TypeId, subtypeId = furnitureInfo.SubtypeId });
        }

        public IActionResult IndexSingle(int typeId, int subtypeId)
        {
            IEnumerable<Furniture> customerInfo = _furnitureRepository.GetFurnitureByTypeIdAndSubtypeId(typeId, subtypeId);
            return View(customerInfo);
        }

        // GET: Furnitures/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furniture = _furnitureRepository.Get(id);
            if (furniture == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_companyDevRepository.GetAll(), "CompanyId", "ComName", furniture.CompanyId);
            ViewData["SubtypeId"] = new SelectList(_subtypeRepository.GetAll(), "SubtypeId", "SubtypeName", furniture.SubtypeId);
            return View(furniture);
        }

        // POST: Furnitures/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CatalogId,FurnitureName,SubtypeId,CompanyId,Material,Color,Guarantee,Description")] Furniture furniture)
        {
            if (id != furniture.CatalogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _furnitureRepository.Update(furniture);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_companyDevRepository.GetAll(), "CompanyId", "ComName", furniture.CompanyId);
            ViewData["SubtypeId"] = new SelectList(_subtypeRepository.GetAll(), "SubtypeId", "SubtypeName", furniture.SubtypeId);
            return View(furniture);
        }

        // GET: Furnitures/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furniture = _furnitureRepository.GetFurniturewithShopAndSubtype()
                .FirstOrDefault(m => m.CatalogId == id);
            if (furniture == null)
            {
                return NotFound();
            }

            return View(furniture);
        }

        // POST: Furnitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _furnitureRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
