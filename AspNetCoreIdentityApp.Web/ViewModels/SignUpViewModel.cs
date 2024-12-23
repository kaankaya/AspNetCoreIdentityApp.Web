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
        [Display(Name = "Kullanıcı Adı :")]
        public string UserName { get; set; }
        [Display(Name = "Email :")]
        public string Email { get; set; }
        [Display(Name = "Telefon :")]
        public string Phone { get; set; }
        [Display(Name = "Şifre :")]
        public string Password { get; set; }
        //Display leri kullanarak view ekranında asp-for(taghelperları) kullanarak daha rahat yapabilirim
        [Display(Name = "Şifre Tekrar :")]
        public string PasswordConfirm { get; set; }
    }
}
