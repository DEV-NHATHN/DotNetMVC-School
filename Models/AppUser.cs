﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AppMVC.Models.SchoolManagement;
using AppMVC.Services.ValidateService;

namespace AppMVC.Models
{
       public class AppUser : IdentityUser
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(400)]
        public string? HomeAddress { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(100)]
        public string? FullName { get; set; }

        // [Required]       
        [DataType(DataType.Date)]
        [CustomValidation(typeof(CustomValidators), nameof(CustomValidators.ValidateAge))]
        public DateTime? BirthDate { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }


        public int? SchoolId { get; set; }

        public int? DepartmentId { get; set; }

        public int? ClassId { get; set; }

        [Display(Name = "Lớp")]
        public ClassModel? Class { set; get; }
    }
}