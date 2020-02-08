using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FurnitureShopApp.DAL.Models;
using FurnitureShopApp.DAL.Interfaces;
using System.Collections.Generic;

namespace FurnitureShopApp.Controllers
{
    public class CheckDetailsController : Controller
    {
        private readonly IFurnitureInStorageRepository _furnitureInStorageRepository;
        private readonly IFurnitureSaleRepository _furnituresalerepository;
        private readonly ICheckDetailsRepository _checkdetailsrepository;

        public CheckDetailsController(IFurnitureInStorageRepository furnitureInStorageRepository, IFurnitureSaleRepository furnituresalerepository, ICheckDetailsRepository checkdetailsrepository)
        {
            _furnitureInStorageRepository = furnitureInStorageRepository;
            _furnituresalerepository = furnituresalerepository;
            _checkdetailsrepository = checkdetailsrepository;
        }

        // GET: CheckDetails
        public IActionResult Index()
        {
            var furnitureSaleContext = _checkdetailsrepository.GetCheckDetailsInfo();
            return View(furnitureSaleContext);
        }

        // GET: CheckDetails/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkDetails = _checkdetailsrepository.GetCheckDetailsInfo()
                .FirstOrDefault(m => m.CheckDetailsId == id);
            if (checkDetails == null)
            {
                return NotFound();
            }

            return View(checkDetails);
        }

        // GET: CheckDetails/Create
        public IActionResult Create()
        {
            ViewData["CheckId"] = new SelectList(_furnituresalerepository.GetFurnitureSalesInfo(), "CheckId", "CheckId");
            ViewData["FurnitureId"] = new SelectList(_furnitureInStorageRepository.GetFurnitureInStorageInfo(), "FurnitureId", "FurnitureNameWithColor");
            return View();
        }

        // POST: CheckDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CheckDetailsId,QuantitySelected,CheckId,FurnitureId")] CheckDetails checkDetails)
        {
            if (ModelState.IsValid)
            {
                _checkdetailsrepository.Create(checkDetails);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CheckId"] = new SelectList(_furnituresalerepository.GetFurnitureSalesInfo(), "CheckId", "CheckId", checkDetails.CheckId);
            ViewData["FurnitureId"] = new SelectList(_furnitureInStorageRepository.GetFurnitureInStorageInfo(), "FurnitureId", "FurnitureNameWithColor", checkDetails.FurnitureId);
            return View(checkDetails);
        }

        // GET: CheckDetails/Search
        public IActionResult Search()
        {
            ViewData["CheckId"] = new SelectList(_furnituresalerepository.GetFurnitureSalesInfo(), "CheckId", "CheckId");
            return View();
        }

        [HttpPost]
        public IActionResult Search([Bind("CheckId")] FurnitureSale checkInfo)
        {
            return RedirectToAction("IndexSingle", new { checkId = checkInfo.CheckId });
        }

        public IActionResult IndexSingle(int checkId)
        {
            IEnumerable<CheckDetails> checkDetailsInfo =
                _checkdetailsrepository.GetCheckDetailsInfoByCheckId(checkId);
            return View(checkDetailsInfo);
        }

        // GET: CheckDetails/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkDetails = _checkdetailsrepository.Get(id);
            if (checkDetails == null)
            {
                return NotFound();
            }

            ViewData["CheckId"] = new SelectList(_furnituresalerepository.GetFurnitureSalesInfo(), "CheckId", "CheckId", checkDetails.CheckId);
            ViewData["FurnitureId"] = new SelectList(_furnitureInStorageRepository.GetFurnitureInStorageInfo(), "FurnitureId", "FurnitureNameWithColor", checkDetails.FurnitureId);
            return View(checkDetails);
        }

        // POST: CheckDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CheckDetailsId,QuantitySelected,CheckId,FurnitureId")] CheckDetails checkDetails)
        {
            if (id != checkDetails.CheckDetailsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _checkdetailsrepository.Update(checkDetails);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CheckId"] = new SelectList(_furnituresalerepository.GetFurnitureSalesInfo(), "CheckId", "CheckId", checkDetails.CheckId);
            ViewData["FurnitureId"] = new SelectList(_furnitureInStorageRepository.GetFurnitureInStorageInfo(), "FurnitureId", "FurnitureNameWithColor");
            return View(checkDetails);
        }

        // GET: CheckDetails/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkDetails = _checkdetailsrepository.GetCheckDetailsInfo()
                .FirstOrDefault(m => m.CheckDetailsId == id);
            if (checkDetails == null)
            {
                return NotFound();
            }

            return View(checkDetails);
        }

        // POST: CheckDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _checkdetailsrepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
