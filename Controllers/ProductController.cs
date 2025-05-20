using Microsoft.AspNetCore.Mvc;
using AdvancedAjax.Models;

namespace AdvancedAjax.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveProduct([FromBody] Product product)
        {
            // Simuleerime andme salvestamist
            return Json(new { success = true, message = $"Toode '{product.Name}' on salvestatud." });
        }
    }
}

