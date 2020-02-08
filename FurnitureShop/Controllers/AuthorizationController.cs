using Microsoft.AspNetCore.Mvc;
using FurnitureShopApp.DAL.Models;
using FurnitureShopApp.DAL.Interfaces;
using System.IO;
using System.Runtime.Serialization.Json;

namespace FurnitureShopApp.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IEmployeeUserRepository _repository;

        public AuthorizationController(IEmployeeUserRepository repository)
        {
            _repository = repository;
        }

        // GET: Authorization
        public IActionResult Index()
        {
            return View();
        }

        // POST: Authorization/CheckUser
        [HttpPost]
        public IActionResult CheckUser()
        {
            string login = Request.Form["Login"];
            string password = Request.Form["Password"];

            EmployeeUser user = _repository.UserValidation(login, password);

            if (user == null)
            {
                return RedirectToAction("Index", "Authorization");
            }


            if (user.Employee.Position.PositionName == "Адміністратор")
            {
                string directory = Directory.GetCurrentDirectory() + "\\tempFiles\\";
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(int));

                using (FileStream fs = new FileStream(directory + "Admin.json", FileMode.OpenOrCreate))
                {
                    jsonFormatter.WriteObject(fs, user.UserId);
                }

                return RedirectToAction("IndexAdmin", "Home");

            }
            else
                if (user.Employee.Position.PositionName == "Продавець - консультант")
            {
                string directory = Directory.GetCurrentDirectory() + "\\tempFiles\\";
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(int));

                using (FileStream fs = new FileStream(directory + "Seller.json", FileMode.OpenOrCreate))
                {
                    jsonFormatter.WriteObject(fs, user.UserId);
                }

                return RedirectToAction("IndexSeller", "Home");
            }
            else
                return View();
        }
    }
}
