using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMVC.Models.SchoolManagement
{
    public class Student: AppUser
    {
        [Display(Name = "Trường")]
        public int? SchoolId { get; set; }

        [ForeignKey("Id")]
        [Display(Name = "Trường")]
        public School School { set; get; }


        [Display(Name = "Khoa")]
        public int? DepartmentId { get; set; }

        [ForeignKey("Id")]
        [Display(Name = "Khoa")]
        public Department Department { set; get; }


        [Display(Name = "Lớp")]
        public int? ClassId { get; set; }

        [ForeignKey("Id")]
        [Display(Name = "Lớp")]
        public ClassModel Class { set; get; }
    }
}
