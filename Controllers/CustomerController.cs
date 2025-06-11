using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdvancedAjax.Models;

namespace AdvancedAjax.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public CustomerController(AppDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        public IActionResult Index()
        {
            var customers = _context.Customers
                .Include(c => c.City)
                .ToList();

            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Customer customer = new Customer();
            ViewBag.Countries = GetCountries();
            return View(customer);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            string uniqueFileName = GetProfilePhotoFileName(customer);
            customer.PhotoUrl = uniqueFileName;

            _context.Add(customer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Customer customer = _context.Customers
                .Include(c => c.City)
                .ThenInclude(city => city.Country)
                .FirstOrDefault(c => c.Id == id);

            return View(customer);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Customer customer = _context.Customers
                .Include(c => c.City)
                .FirstOrDefault(c => c.Id == id);

            customer.CountryId = customer.City.CountryId;

            ViewBag.Countries = GetCountries();
            ViewBag.Cities = GetCities(customer.CountryId);

            return View(customer);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (customer.ProfilePhoto != null)
            {
                string uniqueFileName = GetProfilePhotoFileName(customer);
                customer.PhotoUrl = uniqueFileName;
            }

            _context.Attach(customer);
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Customer customer = _context.Customers.FirstOrDefault(c => c.Id == id);
            return View(customer);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            _context.Attach(customer);
            _context.Entry(customer).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult CreateModalForm(Customer customer)
        {
            string uniqueFileName = GetProfilePhotoFileName(customer);
            customer.PhotoUrl = uniqueFileName;

            _context.Add(customer);
            _context.SaveChanges();

            return NoContent(); // AJAX jaoks 204 vastus
        }

        [HttpGet]
        public JsonResult GetCitiesByCountry(int countryId)
        {
            List<SelectListItem> cities = _context.Cities
                .Where(c => c.CountryId == countryId)
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();

            return Json(cities);
        }

        private List<SelectListItem> GetCountries()
        {
            List<SelectListItem> lstCountries = _context.Countries
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();

            lstCountries.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "----Select Country----"
            });

            return lstCountries;
        }

        private List<SelectListItem> GetCities(int countryId)
        {
            return _context.Cities
                .Where(c => c.CountryId == countryId)
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();
        }

        private string GetProfilePhotoFileName(Customer customer)
        {
            if (customer.ProfilePhoto == null)
                return null;

            string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + customer.ProfilePhoto.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                customer.ProfilePhoto.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
    }
}
