using System.ComponentModel.DataAnnotations;

namespace AppMVC.Areas.Identity.Models.ManageViewModels
{
    public class VerifyPhoneNumberViewModel
   {
      [Required(ErrorMessage = "Phải nhập {0}")]
      [Display(Name = "Mã xác nhận")]
      public string Code { get; set; }

      [Required(ErrorMessage = "Phải nhập {0}")]
      [Phone]
      [Display(Name = "Số điện thoại")]
      public string PhoneNumber { get; set; }
   }
}
