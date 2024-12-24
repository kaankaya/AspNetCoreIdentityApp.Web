using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.CustomValidations
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            //errorlar için bir liste tanımladık
            var errors = new List<IdentityError>();
            //şifre kullanıcı adını içeriyor ise hata vermesi için if kontrolü
            if (password!.ToLower().Contains(user.UserName!.ToLower()))
            {
                //hatalara ekliyor
                errors.Add(new() { Code = "PasswordContainUserName", Description = "Şifre Alanı kullanıcı adı içeremez" });
            }
            //1234 ile başlıyorsa şifre hata vermesi için if kontrolü
            if (password!.ToLower().StartsWith("1234"))
            {
                errors.Add(new() { Code = "PasswordContain1234", Description = "Şifre alanı ardışık sayı içeremez" });
            }
            //herhangi bir hatalar dan bir var ise failed döndürüyoruz
            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            //herşey tamam ise başarılı olarak döndürüyoruz
            return Task.FromResult(IdentityResult.Success);

        }
    }
}
