using AppMVC.Models.SchoolManagement;

namespace AppMVC.Services
{
    public interface IValidationService
    {
        int ValidateCreateClass(ClassModel classes);
        int ValidateUpdateClass(int id, ClassModel classes);
        int ValidateCreateDepartment(Department department);
        bool ValidateUpdateDepartment(int id, Department department);
        int ValidateUpdateUser(string id, string email);
    }
}

