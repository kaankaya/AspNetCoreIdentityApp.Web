using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.CustomValidations
{
    public class UserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors = new List<IdentityError>();
            var isNumeric = int.TryParse(user.UserName[0]!.ToString(), out _);
            if(isNumeric)
            {
                errors.Add(new() { Code = "UserNameContainFirstLetterDigit", Description = "Kullanıcı adı rakam ile başlayamaz" });
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
