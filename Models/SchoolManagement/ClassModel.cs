using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMVC.Models.SchoolManagement
{

    [Table("Class")]
    public class ClassModel
    {

        [Key]
        public int Id { get; set; }

        //
        [Required(ErrorMessage = "Phải có tên lớp")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} dài {1} đến {2}")]
        [Display(Name = "Tên lớp")]
        public string? Name { get; set; }
        public int Capacity { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(256)]
        public string CreatedBy { get; set; }

        // parent
        [Display(Name = "Trường")]
        public int? SchoolId { get; set; }

        [ForeignKey("Id")]
        [Display(Name = "Trường")]
        public School School { get; set; }

        // parent
        [Display(Name = "Khoa")]
        public int? DepartmentId { get; set; }

        [ForeignKey("Id")]
        [Display(Name = "Khoa")]
        public Department Department { get; set; }

        // child
        public ICollection<Student> Students { get; set; }
    }
}
