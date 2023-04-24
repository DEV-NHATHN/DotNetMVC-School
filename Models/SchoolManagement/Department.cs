using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppMVC.Models.SchoolManagement
{

    [Table("Department")]
    public class Department
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Phải có tên khoa")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} dài {1} đến {2}")]
        [Display(Name = "Tên khoa")]
        public string? Name { get; set; }
        public int Capacity { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

        [StringLength(256)]
        public string CreatedBy { get; set; }

        // parent
        [Display(Name = "Trường")]
        public int? SchoolId { get; set; }

        [ForeignKey("Id")]
        [Display(Name = "Trường")]
        public School School { get; set; }

        //children
        public ICollection<ClassModel> Classes { get; set; }
    }
}
