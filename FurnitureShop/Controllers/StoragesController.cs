using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FurnitureShopApp.DAL.Models;
using FurnitureShopApp.DAL.Interfaces;
using System.Collections.Generic;

namespace FurnitureShopApp.Controllers
{
    public class StoragesController : Controller
    {
        private readonly IStorageRepository _storageRepository;
        private readonly IFurnitureShopRepository _furnitureShopRepository;

        public StoragesController(IStorageRepository storageRepository, IFurnitureShopRepository furnitureShopRepository)
        {
            _storageRepository = storageRepository;
            _furnitureShopRepository = furnitureShopRepository;
        }

        // GET: Storages
        public IActionResult Index()
        {
            var furnitureSaleContext = _storageRepository.GetStorageWithShop();
            return View(furnitureSaleContext);
        }

        // GET: Storages/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = _storageRepository.GetStorageWithShop()
                .FirstOrDefault(m => m.StorageId == id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        // GET: Storages/Create
        public IActionResult Create()
        {
            ViewData["ShopId"] = new SelectList(_furnitureShopRepository.GetAll(), "ShopId", "ShopAddress");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("StorageId,ShopId,StorageAddress")] Storage storage)
        {
            if (ModelState.IsValid)
            {
                _storageRepository.Create(storage);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShopId"] = new SelectList(_furnitureShopRepository.GetAll(), "ShopId", "ShopAddress", storage.ShopId);
            return View(storage);
        }

        // GET: Storages/Search
        public IActionResult Search()
        {
            ViewData["StorageId"] = new SelectList(_storageRepository.GetStorageWithShop(), "StorageId", "StorageAddress");
            return View();
        }

        // POST: Storages/Search
        [HttpPost]
        public IActionResult Search([Bind("StorageId")] Storage storage)
        {
            return RedirectToAction("IndexSingle", new { storageId = storage.StorageId });
        }

        public IActionResult IndexSingle(int storageId)
        {
            IEnumerable<Storage> storageInfo = new List<Storage>
                {
                    _storageRepository.GetStorageInfoById(storageId)
                };
            return View(storageInfo);
        }

        // GET: Storages/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = _storageRepository.Get(id);
            if (storage == null)
            {
                return NotFound();
            }
            ViewData["ShopId"] = new SelectList(_furnitureShopRepository.GetAll(), "ShopId", "ShopAddress", storage.ShopId);
            return View(storage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("StorageId,ShopId,StorageAddress")] Storage storage)
        {
            if (id != storage.StorageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _storageRepository.Update(storage);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShopId"] = new SelectList(_furnitureShopRepository.GetAll(), "ShopId", "ShopAddress", storage.ShopId);
            return View(storage);
        }

        // GET: Storages/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = _storageRepository.GetStorageWithShop()
                .FirstOrDefault(m => m.StorageId == id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        // POST: Storages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _storageRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}