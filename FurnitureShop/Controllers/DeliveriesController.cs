using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FurnitureShopApp.DAL.Models;
using FurnitureShopApp.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace FurnitureShopApp.Controllers
{
    public class DeliveriesController : Controller
    {
        private readonly IFurnitureSaleRepository _furnituresalerepository;
        private readonly IDeliveryRepository _deliveryrepository;

        public DeliveriesController(IFurnitureSaleRepository furnituresalerepository, IDeliveryRepository deliveryrepository)
        {
            _furnituresalerepository = furnituresalerepository;
            _deliveryrepository = deliveryrepository;
        }

        // GET: Deliveries
        public IActionResult Index()
        {
            var furnitureSaleContext = _deliveryrepository.GetDeliveryWithCheck();
            return View(furnitureSaleContext);
        }

        // GET: Deliveries/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = _deliveryrepository.GetDeliveryWithCheck().FirstOrDefault(m => m.DeliveryId == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // GET: Deliveries/Create
        public IActionResult Create()
        {
            ViewData["CheckId"] = new SelectList(_furnituresalerepository.GetFurnitureSalesInfo(), "CheckId", "CheckId");
            return View();
        }

        // POST: Deliveries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DeliveryId,CheckId,Address,ShippingDate,DeliveryDate,DeliveryPrice")] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                if (delivery.ShippingDate > delivery.DeliveryDate)
                {
                    this.ModelState["DeliveryDate"].Errors.Clear();
                    this.ModelState["DeliveryDate"].Errors.Add("Дата доставки не може бути раніше, ніж дата відправлення!");
                    this.ModelState["ShippingDate"].Errors.Clear();
                    this.ModelState["ShippingDate"].Errors.Add("Дата відправлення не може бути пізніше, ніж дата доставки!");
                    ViewData["CheckId"] = new SelectList(_furnituresalerepository.GetAll(), "CheckId", "CheckId", delivery.CheckId);
                    return View(delivery);
                }
                _deliveryrepository.Create(delivery);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CheckId"] = new SelectList(_furnituresalerepository.GetFurnitureSalesInfo(), "CheckId", "CheckId", delivery.CheckId);
            return View(delivery);
        }

        // GET: Deliveries/Search
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search([Bind("ShippingDate")] Delivery deliveryInfo)
        {
            return RedirectToAction("IndexSingle", new { shippingDate = deliveryInfo.ShippingDate });
        }

        public IActionResult IndexSingle(DateTime shippingDate)
        {
            IEnumerable<Delivery> deliveryInfo =
                _deliveryrepository.GetDeliveryInfoByShippingDate(shippingDate);

            if (deliveryInfo == null)
            {
                return View();
            }

            return View(deliveryInfo);
        }

        // GET: Deliveries/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = _deliveryrepository.Get(id);
            if (delivery == null)
            {
                return NotFound();
            }
            ViewData["CheckId"] = new SelectList(_furnituresalerepository.GetFurnitureSalesInfo(), "CheckId", "CheckId", delivery.CheckId);
            return View(delivery);
        }

        // POST: Deliveries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("DeliveryId,CheckId,Address,ShippingDate,DeliveryDate,DeliveryPrice")] Delivery delivery)
        {
            if (id != delivery.DeliveryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _deliveryrepository.Update(delivery);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CheckId"] = new SelectList(_furnituresalerepository.GetAll(), "CheckId", "CheckId", delivery.CheckId);
            return View(delivery);
        }

        // GET: Deliveries/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = _deliveryrepository.GetDeliveryWithCheck()
                .FirstOrDefault(m => m.DeliveryId == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // POST: Deliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _deliveryrepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
