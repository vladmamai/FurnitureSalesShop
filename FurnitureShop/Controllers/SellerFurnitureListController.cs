using Microsoft.AspNetCore.Mvc;
using FurnitureShopApp.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using FurnitureShopApp.DAL.Models;

namespace FurnitureShopApp.Controllers
{
    public class SellerFurnitureListController : Controller
    {
        private readonly IFurnitureInStorageRepository _furnitureInStorageRepository;
        private readonly IFurnitureRepository _furnitureRepository;

        public SellerFurnitureListController(IFurnitureInStorageRepository furnitureInStorageRepository,
            ITypesRepository typeRepository,
            ISubtypeRepository subtypeRepository,
            IFurnitureRepository furnitureRepository)
        {
            _furnitureInStorageRepository = furnitureInStorageRepository;
            _furnitureRepository = furnitureRepository;
        }

        // GET: SellerFurnitureList
        public IActionResult Index()
        {
            var furnitureSaleContext = _furnitureInStorageRepository.GetFurnitureInStorageForEmployee();
            return View(furnitureSaleContext);
        }

        // GET: FurnitureInStorages/Search
        public IActionResult Search()
        {
            ViewData["FurnitureId"] = new SelectList(_furnitureRepository.GetFurnitureRelatedToShop(), "FurnitureId", "FurnitureNameWithColor");
            return View();
        }

        [HttpPost]
        public IActionResult Search([Bind("FurnitureId")] FurnitureBillingDataModel furnitureStorage)
        {
            return RedirectToAction("IndexSingle", new { furnitureId = furnitureStorage.FurnitureId });
        }

        public IActionResult IndexSingle(int furnitureId)
        {
            IEnumerable<FurnitureInStorage> furnitureStorageInfo =
                _furnitureInStorageRepository.GetFurnitureInStorageById(furnitureId);

            return View(furnitureStorageInfo);
        }
    }
}
