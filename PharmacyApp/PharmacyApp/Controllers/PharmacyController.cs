﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PharmacyApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PharmacyApp.Controllers
{
    public class PharmacyController : Controller
    {
        private PharmacyContext _context;
        private static string Name = null;

        public PharmacyController(PharmacyContext context)
        {
            _context = context;
        }

        [Route("catalog")]
        public IActionResult СatalogPharmacy(string SearchMedicine)
        {
            Name = SearchMedicine;
            return View();
        }

        [HttpGet]
        public JsonResult GetMedicines()
        {
            var Medicines = Name == null ? _context.Medicines.Include(m => m.Company) : 
                _context.Medicines.Include(m => m.Company).Where(m => m.Name.Contains(Name));
            return Json(Medicines.ToList());
        }

        [Authorize(Roles = "admin")]
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
            if (ModelState.IsValid)
            {
                _context.Medicines.Add(model);
                await _context.SaveChangesAsync();
                ViewBag.Message = "Препорат \"" + model.Name + "\" добавлен в каталог аптеки.";
                ModelState.Clear();
                ViewBag.Companies = new SelectList(_context.Companies.ToList(), "Id", "Name");
                ViewBag.InternationalNames = new SelectList(_context.InternationalNames.ToList(), "Id", "Name");
                return View();
            }
            return View(model);
        }

        [Route("medicine/{id}")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id != null)
            {
                var model = await _context.Medicines.FirstOrDefaultAsync(m => m.Id == id);
                if (model != null)
                    return View(model);
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [Route("medicine/delete/{id}")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var model = await _context.Medicines.FirstOrDefaultAsync(m => m.Id == id);
                if (model != null)
                {
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id != null)
            {
                var model = await _context.Medicines.FirstOrDefaultAsync(m => m.Id == id);
                if (model != null)
                {
                    _context.Medicines.Remove(model);
                    await _context.SaveChangesAsync();
                    return View(model);
                }
            }
            return NotFound();
        }

        [Authorize(Roles = "admin")]
        [Route("medicine/edit/{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var model = await _context.Medicines.FirstOrDefaultAsync(m => m.Id == id);
                if (model != null)
                {
                    ViewBag.Companies = new SelectList(_context.Companies.ToList(), "Id", "Name");
                    ViewBag.InternationalNames = new SelectList(_context.InternationalNames.ToList(), "Id", "Name");
                    return View(model);
                }
            }
            return NotFound();
        }

        [Route("medicine/edit/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Medicine model)
        {
            _context.Medicines.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("СatalogPharmacy");
        }
    }
}
