using Microsoft.AspNetCore.Mvc;

namespace Pharmify.Controllers
{
    public class DetailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
