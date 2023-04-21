// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace AppMVC.Areas.Identity.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
   {
      [Required(ErrorMessage = "Phải nhập {0}")]
      [EmailAddress(ErrorMessage = "Phải đúng định dạng email")]
      public string Email { get; set; }
   }
}
