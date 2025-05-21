using Microsoft.AspNetCore.Mvc;

namespace AdvancedAjax.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
