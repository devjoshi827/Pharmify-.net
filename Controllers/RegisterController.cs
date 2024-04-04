using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Pharmify.Models;

namespace Pharmify.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IMongoCollection<User> _userCollection;

        public RegisterController()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Pharmify");
            _userCollection = database.GetCollection<User>("User");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            _userCollection.InsertOne(user);
            return RedirectToAction("Index", "Login");
        }
    }
}
