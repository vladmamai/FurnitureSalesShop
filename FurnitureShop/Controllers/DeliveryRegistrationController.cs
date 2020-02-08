using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FurnitureShopApp.DAL.Models;
using FurnitureShopApp.DAL.Interfaces;
using System.IO;
using System.Runtime.Serialization.Json;

namespace FurnitureShopApp.Controllers
{
    public class DeliveryRegistrationController : Controller
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IFurnitureSaleRepository _furnitureSaleRepository;

        public DeliveryRegistrationController(IDeliveryRepository deliveryRepository, IFurnitureSaleRepository furnitureSaleRepository)
        {
            _deliveryRepository = deliveryRepository;
            _furnitureSaleRepository = furnitureSaleRepository;
        }

        // GET: DeliveryRegistration/Details/
        public IActionResult Details(Delivery delivery)
        {
            return View(delivery);
        }

        // GET: DeliveryRegistration/Create
        public IActionResult Create()
        {
            ViewData["CheckId"] = new SelectList(_furnitureSaleRepository.GetAll(), "CheckId", "CheckId");
            return View();
        }

        // POST: DeliveryRegistration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DeliveryId,CheckId,Address,ShippingDate,DeliveryDate,DeliveryPrice")] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                string directory = Directory.GetCurrentDirectory() + "\\tempFiles\\";
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Delivery));

                using (FileStream fs = new FileStream(directory + "Delivery.json", FileMode.OpenOrCreate))
                {
                    jsonFormatter.WriteObject(fs, delivery);
                }

                return RedirectToAction("Details", delivery);
            }
            
            ViewData["CheckId"] = new SelectList(_furnitureSaleRepository.GetAll(), "CheckId", "CheckId", delivery.CheckId);
            return View(delivery);
        }

        public IActionResult DeliveryConfirm()
        {
            string directory = Directory.GetCurrentDirectory() + "\\tempFiles\\";
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Delivery));

            Delivery delivery = null;

            using (FileStream fs = new FileStream(directory + "Delivery.json", FileMode.Open))
            {
                delivery = (Delivery)jsonFormatter.ReadObject(fs);
            }

            System.IO.File.Delete(directory + "Delivery.json");

            _deliveryRepository.Create(delivery);
            return View(delivery);   
        }
    }
}
