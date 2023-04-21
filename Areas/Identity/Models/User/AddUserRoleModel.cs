using AppMVC.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace AppMVC.Areas.Identity.Models.UserViewModels
{
    public class AddUserRoleModel
   {
      public AppUser user { get; set; }

      [DisplayName("Các role gán cho user")]
      public string[] RoleNames { get; set; }

      public List<IdentityRoleClaim<string>> claimsInRole { get; set; }
      public List<IdentityUserClaim<string>> claimsInUserClaim { get; set; }

   }
}