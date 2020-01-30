using System;
using System.ComponentModel.DataAnnotations;
using BabyStore.Views.Shared;
namespace BabyStore.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email",ResourceType= typeof(SiteResource))]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string Action { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class ManageUserViewModel
    {


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword",ResourceType=typeof(SiteResource))]
        public string OldPassword { get; set; }



        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword",ResourceType=typeof(SiteResource))]
        public string NewPassword { get; set; }



        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword",ResourceType=typeof(SiteResource))]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email",ResourceType=typeof(SiteResource))]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password",ResourceType=typeof(SiteResource))]
        public string Password { get; set; }

        [Display(Name = "RememberMe",ResourceType=typeof(SiteResource))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email",ResourceType=typeof(SiteResource))]
        public string Email { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password",ResourceType=typeof(SiteResource))]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword",ResourceType=typeof(SiteResource))]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "FirstName",ResourceType=typeof(SiteResource))]
        [StringLength(50)]
        public string FirstName { get; set; }


        [Required]
        [Display(Name = "LastName",ResourceType=typeof(SiteResource))]
        [StringLength(50)]
        public string LastName { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "DateOfBirth",ResourceType=typeof(SiteResource))]

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public bool StatusUser { get; set; }


        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DateCreateUser { get; set; }



        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DateEditUser { get; set; }



        [Display (Name="Address",ResourceType=typeof(SiteResource))]
        public Address Address { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email",ResourceType=typeof(SiteResource))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password",ResourceType=typeof(SiteResource))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword",ResourceType=typeof(SiteResource))]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email",ResourceType=typeof(SiteResource))]
        public string Email { get; set; }
    }
}
