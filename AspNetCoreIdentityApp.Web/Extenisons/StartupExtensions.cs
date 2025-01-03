﻿using AspNetCoreIdentityApp.Web.CustomValidations;
using AspNetCoreIdentityApp.Web.Localizations;
using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.Extenisons
{
    public static class StartupExtensions
    {
        public static void AddIdentityWithExt(this IServiceCollection services)
        {
            //reset için token ömrünü belirledik
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(2);
            });
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
                //locklama sistemi kilitlenince 3 dakika block koysun
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                options.Lockout.MaxFailedAccessAttempts = 3;

            }).AddPasswordValidator<PasswordValidator>()
            .AddUserValidator<UserValidator>()
            .AddErrorDescriber<LocalizationIdentityErrorDescriber>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
