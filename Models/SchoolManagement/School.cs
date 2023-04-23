using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMVC.Models.SchoolManagement
{

    [Table("School")]
    public class School
    {

        [Key]
        public int Id { get; set; }

        //
        [Required(ErrorMessage = "Phải có tên trường")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} dài {1} đến {2}")]
        [Display(Name = "Tên trường")]
        public string? Name { get; set; }

        public DateTime FoundedTime { get; set; }

        [Range(0, 1000)]
        public int Capacity { get; set; }

        // 
        [DataType(DataType.Text)]
        [Display(Name = "Địa chỉ trường")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "{0} dài {1} đến {2}")]
        public string? Address { get; set; }


        public ICollection<Department> Departments { get; set; }
        public ICollection<ClassModel> Classes { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
