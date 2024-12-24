using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage ="Email Alanı boş geçilemez")]
        [EmailAddress(ErrorMessage ="Email Formatı Yanlış")]
        [Display(Name ="Email :")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Şifre alanı boş geçilemez")]
        [Display(Name ="Şifre :")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
