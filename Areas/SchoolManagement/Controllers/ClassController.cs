using AppMVC.Data;
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
    [Route("admin/school-management/class/[action]/{id?}")]
    public class ClassController : Controller
    {
        private readonly AppDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        private readonly IValidationService _validationService;

        public ClassController(AppDbContext context, UserManager<AppUser> userManager, IValidationService validationService)
        {
            _context = context;
            _userManager = userManager;
            _validationService = validationService;
        }

        // GET: SchoolManagement/Class
        public async Task<IActionResult> Index()
        {
            ViewData["School"] = new SelectList(_context.Schools, "Id", "Name");
            var appDbContext = _context.Classes.Include(c => c.Department);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SchoolManagement/Class/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var classModel = await _context.Classes
                .Include(c => c.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classModel == null)
            {
                return NotFound();
            }

            return View(classModel);
        }

        // GET: SchoolManagement/Class/Create
        public IActionResult Create()
        {
            ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Name");
            return View();
        }

        // POST: SchoolManagement/Class/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Capacity,CreatedDate,CreatedBy,SchoolId,DepartmentId")] ClassModel classModel)
        {
            var departmentCapacity = _context.Departments.FirstOrDefault(m => m.Id == classModel.DepartmentId).Capacity;
            var classCapacity = _context.Classes.Where(m => m.DepartmentId == classModel.DepartmentId).Sum(m => m.Capacity) + classModel.Capacity;
            var availability = departmentCapacity - classCapacity + classModel.Capacity;
            var validate = _validationService.ValidateCreateClass(classModel);

            if (validate != 0)
            {
                switch (validate)
                {
                    case 1:
                        ViewBag.error = "Class Over " + availability.ToString();
                        break;
                    case 2:
                        ViewBag.error = "Class Existed";
                        break;
                }
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", classModel.DepartmentId);
                return View(classModel);
            }

            ErrorLogger.LogModelStateErrors(ModelState);

            if (ModelState.IsValid)
            {
                classModel.CreatedDate = DateTime.Now;
                classModel.CreatedBy = _userManager.GetUserId(User);
                // get school id through department id
                classModel.SchoolId = _context.Departments.Find(classModel.DepartmentId).SchoolId;
                _context.Add(classModel);
                await _context.SaveChangesAsync();
                ViewBag.success = "Create Class";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", classModel.DepartmentId);
            return View(classModel);
        }


        // GET: SchoolManagement/Class/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var classModel = await _context.Classes.FindAsync(id);
            if (classModel == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", classModel.DepartmentId);
            return View(classModel);
        }

        // POST: SchoolManagement/Class/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Capacity,CreatedDate,CreatedBy,SchoolId,DepartmentId")] ClassModel classModel)
        {
            var departmentCapacity = _context.Departments.FirstOrDefault(m => m.Id == classModel.DepartmentId).Capacity;
            var classCapacity = _context.Classes.Where(m => m.DepartmentId == classModel.DepartmentId).Sum(m => m.Capacity);
            var availability = departmentCapacity - classCapacity + _context.Classes.Find(id).Capacity;
            var editClass = _context.Classes.FirstOrDefault(m => m.Id == id);
            var validate = _validationService.ValidateUpdateClass(id, classModel);

            if (validate != 0)
            {
                switch (validate)
                {
                    case 2:
                        ViewBag.error = "Class Existed";
                        break;
                }

                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", classModel.DepartmentId);
                return View(classModel);
            }

            if (ModelState.IsValid)
            {
                if (availability < classModel.Capacity)
                {
                    ViewBag.error = "Class Over: " + availability.ToString();
                    ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", classModel.DepartmentId);
                    return View(classModel);
                }

                try
                {
                    editClass.Name = classModel.Name;
                    editClass.Capacity = classModel.Capacity;
                    editClass.DepartmentId = classModel.DepartmentId;
                    editClass.SchoolId = _context.Departments.Find(classModel.DepartmentId).SchoolId;
                    _context.Classes.Update(editClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassModelExists(classModel.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", classModel.DepartmentId);
            return View(classModel);
        }

        // GET: SchoolManagement/Class/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var classModel = await _context.Classes
                .Include(c => c.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classModel == null)
            {
                return NotFound();
            }

            return View(classModel);
        }

        // POST: SchoolManagement/Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Classes == null)
            {
                return Problem("Entity set 'AppDbContext.ClassModel'  is null.");
            }
            var classModel = await _context.Classes.FindAsync(id);
            if (classModel != null)
            {
                _context.Classes.Remove(classModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassModelExists(int id)
        {
            return (_context.Classes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult GetLopByKhoaId([FromQuery]int? departmentId)
        {
            // string khoaId = HttpContext.Request.RouteValues["khoaId"].ToString();

            var filteredClasses = _context.Classes.Where(c => c.DepartmentId == departmentId).ToList();
            return Json(filteredClasses);
        }
    }
}
