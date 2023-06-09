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
    [Area("SchoolManagement")]
    [Route("admin/school-management/student/[action]/{id?}")]
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        private readonly UserManager<AppUser> _userManager;

        private readonly IValidationService _validationService;
        public StudentController(AppDbContext context, UserManager<AppUser> userManager, IValidationService validationService)
        {
            _context = context;
            _userManager = userManager;
            _validationService = validationService;
        }

        [Authorize(Roles = RoleName.Administrator)]
        // GET: SchoolManagement/Student
        public async Task<IActionResult> Index([FromQuery(Name = "p")] int currentPage, int pagesize)
        {
            var students = _context.Students;

            int totalStudents = await students.CountAsync();
            if (pagesize <= 0) pagesize = 10;
            int countPages = (int)Math.Ceiling((double)totalStudents / pagesize);

            if (currentPage > countPages) currentPage = countPages;
            if (currentPage < 1) currentPage = 1;

            var pagingModel = new PagingModel()
            {
                countpages = countPages,
                currentpage = currentPage,
                generateUrl = (pageNumber) => Url.Action("Index", new
                {
                    p = pageNumber,
                    pagesize = pagesize
                })
            };

            ViewBag.pagingModel = pagingModel;
            ViewBag.totalStudents = totalStudents;

            ViewBag.studentIndex = (currentPage - 1) * pagesize;

            var studentInPage = await students
                .Include(c => c.Class)
                .OrderByDescending(c => c.CreatedDate)
                .Skip((currentPage - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();

            return View(studentInPage);
        }

        [Authorize(Roles = RoleName.Member + "," + RoleName.Administrator)]
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

        [Authorize(Roles = RoleName.Administrator)]
        // GET: SchoolManagement/Student/Create
        public IActionResult Create()
        {
            ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Name");
            return View();
        }

        // POST: SchoolManagement/Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Administrator)]
        public async Task<IActionResult> Create([Bind("HomeAddress,FullName,BirthDate,SchoolId,DepartmentId,ClassId")] AppUser student)
        {
            ErrorLogger.LogModelStateErrors(ModelState);

            var validate = _validationService?.ValidateCreateStudent(student.ClassId, student);
            if (validate != 0)
            {
                switch (validate)
                {
                    case 1:
                        ViewBag.error = "UserNameExisted";
                        break;
                    case 2:
                        ViewBag.error = "Class Over Capacity";
                        break;
                }
                ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Name");
                if (student.SchoolId != null)
                {
                    ViewData["DepartmentId"] = new SelectList(_context.Departments.Where(d => d.SchoolId == student.SchoolId), "Id", "Name");
                }
                else
                {
                    ViewData["DepartmentId"] = null;
                }
                if (student.DepartmentId != null)
                {
                    ViewData["ClassId"] = new SelectList(_context.Classes.Where(c => c.DepartmentId == student.DepartmentId), "Id", "Name");
                }
                else
                {
                    ViewData["ClassId"] = null;
                }
                return View(student);
            }

            if (ModelState.IsValid)
            {
                student.CreatedBy = _userManager.GetUserId(User);
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errors = new List<string>();
                foreach (var key in ModelState.Keys)
                {
                    foreach (var error in ModelState[key].Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                ViewBag.error = errors;

                ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Name");
                if (student.SchoolId != null)
                {
                    ViewData["DepartmentId"] = new SelectList(_context.Departments.Where(d => d.SchoolId == student.SchoolId), "Id", "Name");
                }
                else
                {
                    ViewData["DepartmentId"] = null;
                }
                if (student.DepartmentId != null)
                {
                    ViewData["ClassId"] = new SelectList(_context.Classes.Where(c => c.DepartmentId == student.DepartmentId), "Id", "Name");
                }
                else
                {
                    ViewData["ClassId"] = null;
                }
                return View(student);
            }
        }

        [Authorize(Roles = RoleName.Member + "," + RoleName.Administrator)]
        // GET: SchoolManagement/Student/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Name");
            if (student.SchoolId != null)
            {
                ViewData["DepartmentId"] = new SelectList(_context.Departments.Where(d => d.SchoolId == student.SchoolId), "Id", "Name");
            }
            else
            {
                ViewData["DepartmentId"] = null;
            }
            ViewData["ClassId"] = _context.Classes.Where(c => c.DepartmentId == student.DepartmentId).Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();

            return View(student);
        }

        // POST: SchoolManagement/Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Member + "," + RoleName.Administrator)]
        public async Task<IActionResult> Edit(string id, [Bind("Id,HomeAddress,FullName,BirthDate,SchoolId,DepartmentId,ClassId,Email,UserName")] AppUser student)
        {
            ErrorLogger.LogModelStateErrors(ModelState);

            if (!ModelState.IsValid)
            {
                ViewData["id"] = student.Id;
                return View(student);
            }

            var validate = _validationService?.ValidateUpdateUser(id, student);
            if (validate != 0)
            {
                switch (validate)
                {
                    case 1:
                        ViewBag.error = "UserNameExisted";
                        break;
                    case 2:
                        ViewBag.error = "EmailExisted";
                        break;
                    case 3:
                        ViewBag.error = "Class Over Capacity";
                        break;
                }
                ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Name");
                if (student.SchoolId != null)
                {
                    ViewData["DepartmentId"] = new SelectList(_context.Departments.Where(d => d.SchoolId == student.SchoolId), "Id", "Name");
                }
                else
                {
                    ViewData["DepartmentId"] = null;
                }
                ViewData["ClassId"] = _context.Classes.Where(c => c.DepartmentId == student.DepartmentId).Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToList();
                return View(student);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // find old student and update it
                    var oldStudent = await _context.Students.FindAsync(id);
                    if (oldStudent == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        oldStudent.FullName = student.FullName;
                        oldStudent.HomeAddress = student.HomeAddress;
                        oldStudent.BirthDate = student.BirthDate;
                        oldStudent.SchoolId = student.SchoolId;
                        oldStudent.DepartmentId = student.DepartmentId;
                        oldStudent.ClassId = student.ClassId;
                        oldStudent.ModifiedBy = _userManager.GetUserId(User);
                        oldStudent.ModifiedDate = DateTime.Now;
                        oldStudent.Email = student.Email;
                        oldStudent.UserName = student.UserName;
                        _context.Update(oldStudent);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentModelExists(student.Id))
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
            return View(student);
        }

        [Authorize(Roles = RoleName.Administrator)]
        // GET: SchoolManagement/Student/Delete/5
        public async Task<IActionResult> Delete(string? id)
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


        // POST: SchoolManagement/Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Administrator)]
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

        private bool StudentModelExists(string id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet]
        public async Task<IActionResult> GetNameByUserId([FromQuery] string? userId)
        {
            if (userId == null || _context.Students == null)
            {
                return NotFound();
            }
            var user = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == userId);
            if (user == null)
            {
                return NotFound();
            }
            return Json(user.UserName);
        }
    }
}
