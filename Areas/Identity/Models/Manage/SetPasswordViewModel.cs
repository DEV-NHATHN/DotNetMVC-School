using System.ComponentModel.DataAnnotations;

namespace AppMVC.Areas.Identity.Models.ManageViewModels
{
    public class SetPasswordViewModel
   {
      [Required(ErrorMessage = "Phải nhập {0}")]
      [StringLength(100, ErrorMessage = "{0} dài tối thiểu {2} ký tự.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Mật khẩu mới")]
      public string NewPassword { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = "Xác nhận mật khẩu")]
      [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận phải trùng với mật khẩu mới")]
      public string ConfirmPassword { get; set; }
   }
}
