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
      [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} dài {2} đến {1} kí tự")]
      [Display(Name = "Tên trường")]
      public string? Name { get; set; }

      public DateTime FoundedTime { get; set; }

      [Range(0, 1000, ErrorMessage = "Số lượng học sinh phải nằm trong khoảng từ 1 đến 1000")]
      public int Capacity { get; set; }

      // 
      [Display(Name = "Địa chỉ trường")]
      [StringLength(500, MinimumLength = 3, ErrorMessage = "{0} dài {2} đến {1} kí tự")]
      public string? Address { get; set; }


      public ICollection<Department>? Departments { get; set; }
   }
}
