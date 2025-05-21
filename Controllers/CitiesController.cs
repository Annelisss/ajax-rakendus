using Microsoft.AspNetCore.Mvc;
using AdvancedAjax.Models;
using System.Linq;

namespace AdvancedAjax.Controllers
{
    public class CitiesController : Controller
    {
        private readonly AppDbContext _context;

        public CitiesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetCities(int countryId)
        {
            var cities = _context.Cities
                .Where(c => c.CountryId == countryId)
                .Select(c => new { c.Id, c.Name })
                .ToList();

            return Json(cities);
        }
    }
}