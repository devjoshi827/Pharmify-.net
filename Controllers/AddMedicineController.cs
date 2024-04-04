using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using Pharmify.Models;

namespace Pharmify.Controllers
{
    public class AddMedicineController : Controller
    {
        private readonly IMongoCollection<Medicine> _MedicineCollection;

        public AddMedicineController()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Pharmify");
            _MedicineCollection = database.GetCollection<Medicine>("Medicine");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new Medicine());
        }

        [HttpPost]
        public IActionResult Index(Medicine Medicine, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Convert image to byte array
                    byte[] imageData = null;
                    if (photo != null)
                    {
                        using (var stream = photo.OpenReadStream())
                        using (var memoryStream = new MemoryStream())
                        {
                            stream.CopyTo(memoryStream);
                            imageData = memoryStream.ToArray();
                        }
                    }

                    // Create Medicine object
                    var MedicineObject = new Medicine
                    {
                        Id = ObjectId.GenerateNewId().ToString(), 
                        Photo = imageData,
                        Title = Medicine.Title,
                        Description = Medicine.Description,
                        Category = Medicine.Category
                    };

                    // Insert into MongoDB
                    _MedicineCollection.InsertOne(MedicineObject);

                   
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    ModelState.AddModelError(string.Empty, "An error occurred while adding the Medicine.");
                    return View(Medicine);
                }
            }

            // If the model state is not valid, return to the form with validation errors
            return View(Medicine);
        }

    }
}
