using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace AspNetCoreIdentityApp.Web.Localizations
{
    public class LocalizationIdentityErrorDescriber : IdentityErrorDescriber
    {
        //hata mesajlarını türkçeye çevirme işlemleri
        //dilediğimiz hata mesajlarını override ederek türkçeye çevirebiliriz
        public override IdentityError DuplicateUserName(string userName)
        {
            //return base.DuplicateUserName(userName);
            return new() { Code = "DuplicateUserName", Description = $"Bu => {userName} daha önce başka bir kullanıcı tarafından alınmıştır" };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            //return base.DuplicateEmail(email);
            return new() { Code = "DuplicateEmail", Description = $"Bu => {email} daha önce başka bir kullanıcı tarafından alınmıştır" };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            //return base.PasswordTooShort(length);
            return new() { Code = "PasswordTooShort", Description = $"Şifre en az 6 karakterli olmalıdır" };
        }
    }
}
