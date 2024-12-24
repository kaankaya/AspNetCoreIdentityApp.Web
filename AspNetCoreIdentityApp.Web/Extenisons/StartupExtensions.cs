using AspNetCoreIdentityApp.Web.CustomValidations;
using AspNetCoreIdentityApp.Web.Localizations;
using AspNetCoreIdentityApp.Web.Models;

namespace AspNetCoreIdentityApp.Web.Extenisons
{
    public static class StartupExtensions
    {
        public static void AddIdentityWithExt(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                //PassWord Validation Ayarları
                //Email unique olsun mu
                options.User.RequireUniqueEmail = true;
                //Kullanıcı da kullanabilceği izin verdiğimiz karakterleri yazdık
                options.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvwxyz1234567890_";
                //şifre 6 karakter olsun
                options.Password.RequiredLength = 6;
                //alfanumeric olmasın
                options.Password.RequireNonAlphanumeric = false;
                //küçük karakter zorunlu
                options.Password.RequireLowercase = true;
                //büyük karakter zorunlu değil
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;

            }).AddPasswordValidator<PasswordValidator>().AddUserValidator<UserValidator>().AddErrorDescriber<LocalizationIdentityErrorDescriber>().AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
