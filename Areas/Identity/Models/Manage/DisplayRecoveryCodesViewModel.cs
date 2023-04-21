using System.ComponentModel.DataAnnotations;

namespace AppMVC.Areas.Identity.Models.ManageViewModels
{
    public class DisplayRecoveryCodesViewModel
   {
      [Required]
      public IEnumerable<string> Codes { get; set; }

   }
}
