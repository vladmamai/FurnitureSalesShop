using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FurnitureShopApp.DAL.Models;
using FurnitureShopApp.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FurnitureShopApp.Controllers
{
    public class FurnitureShopsController : Controller
    {
        private readonly IFurnitureShopRepository _furnitureShopRepository;

        public FurnitureShopsController(IFurnitureShopRepository furnitureShopRepository)
        {
            _furnitureShopRepository = furnitureShopRepository;
        }

        // GET: FurnitureShops
        public IActionResult Index()
        {
            return View(_furnitureShopRepository.GetAll());
        }

        // GET: FurnitureShops/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnitureShop = _furnitureShopRepository.FindItem(m => m.ShopId == id);
            if (furnitureShop == null)
            {
                return NotFound();
            }

            return View(furnitureShop);
        }

        // GET: FurnitureShops/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FurnitureShops/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ShopId,ShopAddress,ShopEmail,ShopPhoneNum")] FurnitureShop furnitureShop)
        {
            if (ModelState.IsValid)
            {
                _furnitureShopRepository.Create(furnitureShop);
                return RedirectToAction(nameof(Index));
            }
            return View(furnitureShop);
        }

        // GET: FurnitureShops/Search
        public IActionResult Search()
        {
            ViewData["ShopId"] = new SelectList(_furnitureShopRepository.GetAll(), "ShopId", "ShopAddress");
            return View();
        }

        [HttpPost]
        public IActionResult Search([Bind("ShopId")] FurnitureShop shop)
        {
            return RedirectToAction("IndexSingle", new { shopId = shop.ShopId });
        }

        public IActionResult IndexSingle(int shopId)
        {
            IEnumerable<FurnitureShop> customerInfo = new List<FurnitureShop>
                {
                    _furnitureShopRepository.Get(shopId)
                };
            return View(customerInfo);
        }

        // GET: FurnitureShops/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnitureShop = _furnitureShopRepository.Get(id);
            if (furnitureShop == null)
            {
                return NotFound();
            }
            return View(furnitureShop);
        }

        // POST: FurnitureShops/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ShopId,ShopAddress,ShopEmail,ShopPhoneNum")] FurnitureShop furnitureShop)
        {
            if (id != furnitureShop.ShopId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _furnitureShopRepository.Update(furnitureShop);
                return RedirectToAction(nameof(Index));
            }
            return View(furnitureShop);
        }

        // GET: FurnitureShops/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furnitureShop = _furnitureShopRepository.FindItem(m => m.ShopId == id);
            if (furnitureShop == null)
            {
                return NotFound();
            }

            return View(furnitureShop);
        }

        // POST: FurnitureShops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _furnitureShopRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
