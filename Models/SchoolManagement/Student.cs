using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMVC.Models.SchoolManagement
{
    public class Student: AppUser
    {
        public int? SchoolId { get; set; }

        public int? DepartmentId { get; set; }

        [Display(Name = "Lớp")]
        public int? ClassId { get; set; }

        public string Discriminator { get; set; } = "Student";

        [ForeignKey("Id")]
        [Display(Name = "Lớp")]
        public ClassModel Class { set; get; }
    }
}
