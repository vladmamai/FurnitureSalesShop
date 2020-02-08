using Microsoft.AspNetCore.Mvc;
using FurnitureShopApp.DAL.Models;
using FurnitureShopApp.DAL.Interfaces;
using System.IO;
using System.Runtime.Serialization.Json;

namespace FurnitureShopApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeUserRepository _repository;

        public HomeController(IEmployeeUserRepository repository)
        {
            _repository = repository;
        }

        public IActionResult IndexAdmin()
        {
            string directory = Directory.GetCurrentDirectory() + "\\tempFiles\\";
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(int));

            int? employeeId = null;

            using (FileStream fs = new FileStream(directory + "Admin.json", FileMode.Open))
            {
                employeeId = (int)jsonFormatter.ReadObject(fs);
            }


            return View(_repository.LoadUserInfo(employeeId));
        }

        public IActionResult IndexSeller()
        {
            string directory = Directory.GetCurrentDirectory() + "\\tempFiles\\";
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(int));

            int? employeeId = null;

            using (FileStream fs = new FileStream(directory + "Seller.json", FileMode.Open))
            {
                employeeId = (int)jsonFormatter.ReadObject(fs);
            }

            return View(_repository.LoadUserInfo(employeeId));
        }
    }
}
