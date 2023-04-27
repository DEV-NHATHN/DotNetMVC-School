using AppMVC.Models;
using AppMVC.Models.SchoolManagement;

namespace AppMVC.Services
{
    public interface IValidationService
    {
        int ValidateCreateClass(ClassModel classes);
        int ValidateUpdateClass(int id, ClassModel classes);
        int ValidateCreateDepartment(Department department);
        bool ValidateUpdateDepartment(int id, Department department);
        int ValidateCreateStudent(int? classId, AppUser student);
        int ValidateUpdateUser(string id, AppUser student);
        int ValidateCreateSchool(School school);
    }
}

