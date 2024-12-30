using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Email Alanı boş geçilemez")]
        [EmailAddress(ErrorMessage = "Email Formatı Yanlış")]
        [Display(Name = "Email :")]
        public string Email { get; set; }
    }
}
