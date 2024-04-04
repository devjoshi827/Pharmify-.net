using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Pharmify.Models;

namespace Pharmify.Controllers
{
    public class LoginController : Controller
    {
        private readonly IMongoCollection<User> _userCollection;

        public LoginController()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Pharmify");
            _userCollection = database.GetCollection<User>("User");
        }

        public IActionResult Index()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            var user = _userCollection.Find(u => u.Email == email && u.Password == password).FirstOrDefault();
            if (user != null)
            {
                TempData["SuccessMessage"] = "Login successful!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = "Incorrect email or password.";
                return RedirectToAction("Index");
            }
        }
    }
}
