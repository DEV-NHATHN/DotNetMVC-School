﻿using AppMVC.Data;
using AppMVC.Models;
using AppMVC.Models.SchoolManagement;
using AppMVC.Services;
using AppMVC.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AppMVC.Areas.SchoolManagement.Controllers
{
    [Authorize(Roles = RoleName.Administrator)]
    [Area("SchoolManagement")]
    [Route("admin/school-management/school/[action]/{id?}")]
    public class SchoolController : Controller
    {
        private readonly AppDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        private readonly IValidationService _validationService;

        public SchoolController(AppDbContext context, UserManager<AppUser> userManager, IValidationService validationService)
        {
            _context = context;
            _userManager = userManager;
            _validationService = validationService;
        }

        // GET: SchoolManagement/School
        public async Task<IActionResult> Index()
        {
            return _context.Schools != null ?
                        View(await _context.Schools.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.School'  is null.");
        }

        // GET: SchoolManagement/School/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Schools == null)
            {
                return NotFound();
            }

            var school = await _context.Schools
                .FirstOrDefaultAsync(m => m.Id == id);
            if (school == null)
            {
                return NotFound();
            }

            return View(school);
        }

        [HttpGet()]
        // GET: SchoolManagement/School/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SchoolManagement/School/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,FoundedTime,Capacity,Address")] School school)
        {
            ErrorLogger.LogModelStateErrors(ModelState);

            var validate = _validationService.ValidateCreateSchool(school);

            if (validate != 0)
            {
                switch (validate)
                {
                    case 1:
                        ViewBag.error = "School Existed";
                        break;
                }
                ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Name");
                return View(school);
            }

            if (ModelState.IsValid)
            {
                _context.Add(school);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(school);
        }

        // GET: SchoolManagement/School/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Schools == null)
            {
                return NotFound();
            }

            var school = await _context.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }

        // POST: SchoolManagement/School/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FoundedTime,Capacity,Address")] School school)
        {
            if (id != school.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(school);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolExists(school.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(school);
        }

        // GET: SchoolManagement/School/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Schools == null)
            {
                return NotFound();
            }

            var school = await _context.Schools
                .FirstOrDefaultAsync(m => m.Id == id);
            if (school == null)
            {
                return NotFound();
            }

            return View(school);
        }

        // POST: SchoolManagement/School/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Schools == null)
            {
                return Problem("Entity set 'AppDbContext.School'  is null.");
            }
            var school = await _context.Schools.FindAsync(id);
            if (school != null)
            {
                _context.Schools.Remove(school);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolExists(int id)
        {
            return (_context.Schools?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet]
        public async Task<IActionResult> GetNameBySchoolId([FromQuery] int? schoolId)
        {
            if (schoolId == null || _context.Schools == null)
            {
                return NotFound();
            }
            var school = await _context.Schools
                .FirstOrDefaultAsync(m => m.Id == schoolId);
            if (school == null)
            {
                return NotFound();
            }
            return Json(school.Name);   
        }
    }
}
