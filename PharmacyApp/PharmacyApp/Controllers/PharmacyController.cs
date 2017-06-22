using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PharmacyApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PharmacyApp.Controllers
{
    public class PharmacyController : Controller
    {
        private PharmacyContext _context;

        public PharmacyController(PharmacyContext context)
        {
            _context = context;
        }

        [Route("catalog")]
        public async Task<IActionResult> СatalogPharmacy()
        {
            return View(await _context.Medicines.Include(m => m.Company).ToListAsync());
        }

        [Route("medicine/create")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Companies = new SelectList(_context.Companies.ToList(), "Id", "Name");
            ViewBag.InternationalNames = new SelectList(_context.InternationalNames.ToList(), "Id", "Name");
            return View();
        }

        [Route("medicine/create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Medicine model)
        {
            _context.Medicines.Add(model);
            await _context.SaveChangesAsync();
            ViewBag.Message = "Препорат " + model.Name + " добавлен в каталог";
            ModelState.Clear();
            ViewBag.Companies = new SelectList(_context.Companies.ToList(), "Id", "Name");
            ViewBag.InternationalNames = new SelectList(_context.InternationalNames.ToList(), "Id", "Name");
            return View();
        }
    }
}
