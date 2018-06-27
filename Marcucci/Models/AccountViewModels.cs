using DataAccess;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Marcucci.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "RememberMe")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]

        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required]

        [Display(Name = "Surname")]
        public string LastName { get; set; }

        [Required]

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "ErrorStrongPassword", ErrorMessageResourceType = typeof(LocalResource.Resource), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessageResourceName = "ErrorThePasswordAndConfirmation", ErrorMessageResourceType = typeof(LocalResource.Resource))]
        public string ConfirmPassword { get; set; }

        public string CompanyName { get; set; }
        public string VAT { get; set; }
        public string Address { get; set; }

        public string City { get; set; }

        [Display(Name = "Agreement")]
        public string Agreement { get; set; }

        public RegisterViewModel() { }

        public RegisterViewModel(ApplicationUser user) : base()
        {
            this.Address = user.Address;
            this.City = user.City;
            this.CompanyName = user.CompanyName;
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.PhoneNumber = user.PhoneNumber;
            this.VAT = user.VAT;
            this.Agreement = user.Agreement;
        }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "ErrorStrongPassword", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Compare("Password", ErrorMessageResourceName = "ErrorThePasswordAndConfirmation")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}