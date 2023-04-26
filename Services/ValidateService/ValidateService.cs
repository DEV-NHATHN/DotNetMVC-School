using AppMVC.Models;
using AppMVC.Models.SchoolManagement;

namespace AppMVC.Services.ValidateService
{
    public class ValidateService : IValidationService
    {
        private readonly AppDbContext _context;

        public ValidateService(AppDbContext context)
        {
            _context = context;
        }

        public int ValidateCreateClass(ClassModel classes)
        {
            var departmentCapacity = _context.Departments.FirstOrDefault(e => e.Id == classes.DepartmentId).Capacity;
            var currentClassCapacity = _context.Classes.Where(e => e.DepartmentId == classes.DepartmentId).Sum(e => e.Capacity);
            var newClassCapacity = currentClassCapacity + classes.Capacity;
            if (newClassCapacity > departmentCapacity)
            {
                return 1;
            }

            var existed = _context.Classes.FirstOrDefault(e => e.Name.ToLower().Trim() == classes.Name.ToLower().Trim() && e.DepartmentId == classes.DepartmentId);
            if (existed != null)
            {
                return 2;
            }
            return 0;
        }

        public int ValidateUpdateClass(int id, ClassModel classes)
        {
            var departmentCapacity = _context.Departments.FirstOrDefault(e => e.Id == classes.DepartmentId).Capacity;
            var currentClassCapacity = _context.Classes.Where(e => e.DepartmentId == classes.DepartmentId).Sum(e => e.Capacity);
            var newClassCapacity = currentClassCapacity + classes.Capacity;
            if (newClassCapacity > departmentCapacity)
            {
                // return 1;
            }
            var existed = _context.Classes.FirstOrDefault(e => e.Name.ToLower().Trim() == classes.Name.ToLower().Trim() && e.DepartmentId == classes.DepartmentId && e.Id != id);
            if (existed != null)
            {
                return 2;
            }
            return 0;
        }
        public int ValidateCreateDepartment(Department department)
        {
            var existed = _context.Departments.FirstOrDefault(e => e.Name.ToLower().Trim() == department.Name.ToLower().Trim() && e.SchoolId == department.SchoolId);

            //  Sức chứa của khoa k được vượt quá sức chứa của trường
            var schoolCapacity = _context.Schools.FirstOrDefault(e => e.Id == department.SchoolId).Capacity;
            var currentDepartmentCapacity = _context.Departments.Where(e => e.SchoolId == department.SchoolId).Sum(e => e.Capacity);
            var newDepartmentCapacity = currentDepartmentCapacity + department.Capacity;
            if (newDepartmentCapacity > schoolCapacity)
            {
                return 1;
            }

            if (existed != null) // da co department
            {
                return 2;
            };
            return 0;
        }

        public bool ValidateUpdateDepartment(int id, Department department)
        {
            var existed = _context.Departments.FirstOrDefault(e => e.Name.ToLower().Trim() == department.Name.ToLower().Trim() && e.SchoolId == department.SchoolId && e.Id != id);

            if (existed != null && existed.Id != id) // da co department
            {
                return false;
            };
            return true;
        }

        public int ValidateUpdateUser(string id, string email)
        {
            var existedName = _context.Users.FirstOrDefault(e => e.Id != id && e.UserName.ToLower() == email);
            if (existedName != null)
            {
                return 1;
            };
            if (_context.Users.Any(e => e.Email == email && e.Id != id) == true)
            {
                return 2;
            }
            return 0;
        }

    }
}
