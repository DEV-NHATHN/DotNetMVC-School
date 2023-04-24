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
   [Route("admin/school-management/student/[action]/{id?}")]
   public class StudentController : Controller
   {
      private readonly AppDbContext _context;

      private readonly UserManager<AppUser> _userManager;

      public StudentController(AppDbContext context, UserManager<AppUser> userManager)
      {
         _context = context;
         _userManager = userManager;
      }

      // GET: SchoolManagement/Student
      public async Task<IActionResult> Index()
      {
         var appDbContext = _context.Students
                .Include(s => s.Class)
                .Where(s => s.ClassId != null);
         return View(await appDbContext.ToListAsync());
      }

      // GET: SchoolManagement/Student/Details/5
      public async Task<IActionResult> Details(string? id)
      {
         if (id == null || _context.Students == null)
         {
            return NotFound();
         }

         var student = await _context.Students
             .Include(c => c.Class)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (student == null)
         {
            return NotFound();
         }

         return View(student);
      }

      // GET: SchoolManagement/Student/Create
      public IActionResult Create()
      {
         ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Name");
         return View();
      }

      // POST: SchoolManagement/Student/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("HomeAddress,FullName,BirthDate,SchoolId,DepartmentId,ClassId")] AppUser student)
      {
         ErrorLogger.LogModelStateErrors(ModelState);

         if (ModelState.IsValid)
         {
            student.CreatedBy = _userManager.GetUserId(User);
            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         ViewData["ClassId"] = new SelectList(_context.Students, "Id", "Name", student.ClassId);
         return View(student);
      }

      // GET: SchoolManagement/Student/Edit/5
      public async Task<IActionResult> Edit(string? id)
      {
         if (id == null || _context.Students == null)
         {
            return NotFound();
         }

         var classModel = await _context.Students.FindAsync(id);
         if (classModel == null)
         {
            return NotFound();
         }
         ViewData["ClassId"] = new SelectList(_context.Students, "Id", "Name", classModel.ClassId);
         return View(classModel);
      }

      // POST: SchoolManagement/Student/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(string id, [Bind("HomeAddress,FullName,BirthDate,SchoolId,DepartmentId,ClassId")] AppUser student)
      {
         if (id != student.Id)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            try
            {
               _context.Update(student);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!ClassModelExists(student.Id))
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
         ViewData["ClassId"] = new SelectList(_context.Departments, "Id", "Name", student.ClassId);
         return View(student);
      }

      // GET: SchoolManagement/Student/Delete/5
      public async Task<IActionResult> Delete(string? id)
      {
         if (id == null || _context.Classes == null)
         {
            return NotFound();
         }

         var student = await _context.Students
             .Include(c => c.Class)
             .FirstOrDefaultAsync(m => m.Id == id);
         if (student == null)
         {
            return NotFound();
         }

         return View(student);
      }

      // POST: SchoolManagement/Student/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(string id)
      {
         if (_context.Students == null)
         {
            return Problem("Entity set 'AppDbContext.Student'  is null.");
         }
         var student = await _context.Students.FindAsync(id);
         if (student != null)
         {
            _context.Students.Remove(student);
         }

         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool ClassModelExists(string id)
      {
         return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
      }
   }
}
