using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdvancedAjax.Models;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedAjax.Controllers
{
    public class CityController : Controller
    {
        private readonly AppDbContext _context;

        public CityController(AppDbContext context)
        {
            _context = context;
        }

        // HTML-vaate jaoks
        public async Task<IActionResult> Index()
        {
            var cities = await _context.Cities.ToListAsync();
            return View(cities);
        }
        // GET: City/Create
        public IActionResult Create()
        {
            return PartialView("CreateModalForm", new City());
        }

        // POST: City/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(City city)
        {
            if (ModelState.IsValid)
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return PartialView("CreateModalForm", city);
        }

        // AJAX jaoks
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
