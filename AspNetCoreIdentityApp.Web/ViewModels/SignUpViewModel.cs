using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class SignUpViewModel
    {
        //birde genel constructor tanımlası yaptım
        public SignUpViewModel()
        {

        }

        //değerlerin nullable olabileceğini bildiğimi gösteriyorum ve uyarı vermesin diye constructor da gösterdim
        public SignUpViewModel(string userName, string email, string phone, string password, string passwordconfirm)
        {
            UserName = userName;
            Email = email;
            Phone = phone;
            Password = password;
            PasswordConfirm = passwordconfirm;
        }
        [Required(ErrorMessage ="Kullanıcı ad alanı boş bırakılamaz!")]
        [Display(Name = "Kullanıcı Adı :")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Kullanıcı Email alanı boş bırakılamaz!")]
        [EmailAddress(ErrorMessage ="Email formatı yanlış!")]
        [Display(Name = "Email :")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Kullanıcı Telefon alanı boş bırakılamaz!")]
        [Display(Name = "Telefon :")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Kullanıcı Şifre alanı boş bırakılamaz!")]
        [Display(Name = "Şifre :")]
        public string Password { get; set; }

        //Display leri kullanarak view ekranında asp-for(taghelperları) kullanarak daha rahat yapabilirim
        [Compare(nameof(Password),ErrorMessage ="Şifre Eşleşmiyor!")]
        [Required(ErrorMessage = "Kullanıcı şifre tekrar alanı boş bırakılamaz!")]
        [Display(Name = "Şifre Tekrar :")]
        public string PasswordConfirm { get; set; }
    }
}
