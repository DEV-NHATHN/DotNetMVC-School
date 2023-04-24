using AppMVC.Models;
using AppMVC.Models.SchoolManagement;
using AppMVC.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppMVC.Areas.SchoolManagement.Controllers
{
    [Area("SchoolManagement")]
    [Route("admin/school-management/department/[action]/{id?}")]
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        public DepartmentController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: SchoolManagement/Department
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Departments.Include(d => d.School);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SchoolManagement/Department/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .Include(d => d.School)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: SchoolManagement/Department/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Schools, "Id", "Name");
            return View();
        }

        // POST: SchoolManagement/Department/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Capacity,CreatedDate,SchoolId")] Department department)
        {
            ErrorLogger.LogModelStateErrors(ModelState);

            if (ModelState.IsValid)
            {
                department.CreatedBy = _userManager.GetUserId(User);
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Schools, "Id", "Name", department.Id);
            return View(department);
        }

        // GET: SchoolManagement/Department/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Schools, "Id", "Name", department.Id);
            return View(department);
        }

        // POST: SchoolManagement/Department/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Capacity,CreatedDate,CreatedBy,SchoolId")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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
            ViewData["Id"] = new SelectList(_context.Schools, "Id", "Name", department.Id);
            return View(department);
        }

        // GET: SchoolManagement/Department/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .Include(d => d.School)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: SchoolManagement/Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Departments == null)
            {
                return Problem("Entity set 'AppDbContext.Department'  is null.");
            }
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
          return (_context.Departments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
