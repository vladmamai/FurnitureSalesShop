using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FurnitureShopApp.DAL.Models;
using FurnitureShopApp.DAL.Interfaces;
using System.Collections.Generic;

namespace FurnitureShopApp.Controllers
{
    public class FurnitureInStoragesController : Controller
    {
        private readonly IFurnitureInStorageRepository _furnitureInStorageRepository;
        private readonly IFurnitureRepository _furnitureRepository;
        private readonly IStorageRepository _storageRepository;

        public FurnitureInStoragesController(IFurnitureInStorageRepository furnitureInStorageRepository,
            IFurnitureRepository furnitureRepository, IStorageRepository storageRepository)
        {
            _furnitureInStorageRepository = furnitureInStorageRepository;
            _furnitureRepository = furnitureRepository;
            _storageRepository = storageRepository;
        }

        // GET: FurnitureInStorages
        public IActionResult Index()
        {
            var furnitureSaleContext = _furnitureInStorageRepository.GetFurnitureInStorageInfo();
            return View(furnitureSaleContext);
        }

        // GET: FurnitureInStorages/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnitureInStorage = _furnitureInStorageRepository.GetFurnitureInStorageInfo().FirstOrDefault(m => m.FurnitureId == id);
            if (furnitureInStorage == null)
            {
                return NotFound();
            }

            return View(furnitureInStorage);
        }

        // GET: FurnitureInStorages/Create
        public IActionResult Create()
        {
            ViewData["CatalogId"] = new SelectList(_furnitureRepository.GetNotAddedFurnitureToStorage(), "CatalogId", "FurnitureName");
            ViewData["StorageId"] = new SelectList(_storageRepository.GetStorageWithShop(), "StorageId", "StorageAddress");
            return View();
        }

        // POST: FurnitureInStorages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FurnitureId,CatalogId,StorageId,WholesalePrice,RetailPrice,QuantityInStorage")] FurnitureInStorage furnitureInStorage)
        {

            if (ModelState.IsValid)
            {
                if (furnitureInStorage.WholesalePrice > furnitureInStorage.RetailPrice)
                {
                    this.ModelState["RetailPrice"].Errors.Clear();
                    this.ModelState["RetailPrice"].Errors.Add("Ціна на продаж не може бути меншою, ніж ціна закупівлі товару!");
                    ViewData["CatalogId"] = new SelectList(_furnitureRepository.GetNotAddedFurnitureToStorage(), "CatalogId", "FurnitureName", furnitureInStorage.CatalogId);
                    ViewData["StorageId"] = new SelectList(_storageRepository.GetStorageWithShop(), "StorageId", "StorageAddress", furnitureInStorage.StorageId);
                    return View(furnitureInStorage);
                }
                _furnitureInStorageRepository.Create(furnitureInStorage);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatalogId"] = new SelectList(_furnitureRepository.GetNotAddedFurnitureToStorage(), "CatalogId", "FurnitureName", furnitureInStorage.CatalogId);
            ViewData["StorageId"] = new SelectList(_storageRepository.GetStorageWithShop(), "StorageId", "StorageAddress", furnitureInStorage.StorageId);
            return View(furnitureInStorage);
        }

        // GET: FurnitureInStorages/Search
        public IActionResult Search()
        {
            ViewData["CatalogId"] = new SelectList(_furnitureRepository.GetFurnitureInStorageByShopForAdmin(), "CatalogId", "FurnitureName");
            ViewData["StorageId"] = new SelectList(_storageRepository.GetStorageWithShop(), "StorageId", "StorageAddress");
            return View();
        }

        [HttpPost]
        public IActionResult Search([Bind("CatalogId, StorageId")] FurnitureInStorage furnitureStorage)
        {
            return RedirectToAction("IndexSingle", new { catalogId = furnitureStorage.CatalogId, storageId = furnitureStorage.StorageId });
        }

        public IActionResult IndexSingle(int catalogId, int storageId)
        {
            IEnumerable<FurnitureInStorage> furnitureStorageInfo = 
                _furnitureInStorageRepository.GetFurnitureByCatalogIdAndStorageId(catalogId,storageId);

            return View(furnitureStorageInfo);
        }

        // GET: FurnitureInStorages/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnitureInStorage = _furnitureInStorageRepository.Get(id);
            if (furnitureInStorage == null)
            {
                return NotFound();
            }
            ViewData["CatalogId"] = new SelectList(_furnitureRepository.GetAll(), "CatalogId", "FurnitureNameWithColor", furnitureInStorage.CatalogId);
            ViewData["StorageId"] = new SelectList(_storageRepository.GetStorageWithShop(), "StorageId", "StorageAddress", furnitureInStorage.StorageId);
            return View(furnitureInStorage);
        }

        // POST: FurnitureInStorages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FurnitureId,CatalogId,StorageId,WholesalePrice,RetailPrice,QuantityInStorage")] FurnitureInStorage furnitureInStorage)
        {
            if (id != furnitureInStorage.FurnitureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (furnitureInStorage.WholesalePrice > furnitureInStorage.RetailPrice)
                {
                    this.ModelState["RetailPrice"].Errors.Clear();
                    this.ModelState["RetailPrice"].Errors.Add("Ціна на продаж не може бути меншою, ніж ціна закупівлі товару!");
                    ViewData["CatalogId"] = new SelectList(_furnitureRepository.GetNotAddedFurnitureToStorage(), "CatalogId", "FurnitureName", furnitureInStorage.CatalogId);
                    ViewData["StorageId"] = new SelectList(_storageRepository.GetStorageWithShop(), "StorageId", "StorageAddress", furnitureInStorage.StorageId);
                    return View(furnitureInStorage);
                }
                _furnitureInStorageRepository.Update(furnitureInStorage);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatalogId"] = new SelectList(_furnitureRepository.GetAll(), "CatalogId", "FurnitureNameWithColor", furnitureInStorage.CatalogId);
            ViewData["StorageId"] = new SelectList(_storageRepository.GetStorageWithShop(), "StorageId", "StorageAddress", furnitureInStorage.StorageId);
            return View(furnitureInStorage);
        }

        // GET: FurnitureInStorages/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnitureInStorage = _furnitureInStorageRepository.GetFurnitureInStorageInfo().FirstOrDefault(m => m.FurnitureId == id);
            if (furnitureInStorage == null)
            {
                return NotFound();
            }

            return View(furnitureInStorage);
        }

        // POST: FurnitureInStorages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _furnitureInStorageRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
