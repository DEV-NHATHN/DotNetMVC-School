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
    [Route("admin/school-management/class/[action]/{id?}")]
    public class ClassController : Controller
    {
        private readonly AppDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        public ClassController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: SchoolManagement/Class
        public async Task<IActionResult> Index()
        {
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: SchoolManagement/Class/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Capacity,CreatedDate,CreatedBy,SchoolId,DepartmentId")] ClassModel classModel)
        {
            ErrorLogger.LogModelStateErrors(ModelState);

            if (ModelState.IsValid)
            {
                classModel.CreatedBy = _userManager.GetUserId(User);
                _context.Add(classModel);
                await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Edit(int id, [Bind("Name,Capacity,CreatedDate,CreatedBy,SchoolId,DepartmentId")] ClassModel classModel)
        {
            if (id != classModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classModel);
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
    }
}
