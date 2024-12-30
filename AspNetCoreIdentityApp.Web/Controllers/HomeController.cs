using System.Diagnostics;
using AspNetCoreIdentityApp.Web.Extenisons;
using AspNetCoreIdentityApp.Web.Models;
using AspNetCoreIdentityApp.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace AspNetCoreIdentityApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //�dentity y�netimi i�in usermanager dependecy yapt�k
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        //returnUrl kullan�c� giri� yapt�ktan sonra o girmeye �al��t��� sayfaya y�nlendirmek i�in kullanaca��z
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model,string? returnUrl=null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Home");

           var hasUser = await _userManager.FindByEmailAsync(model.Email);
            if(hasUser is null)
            {
                ModelState.AddModelError(string.Empty, "Email veya �ifre yanl��");
                return View();
            }
            var signInresult = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, true);

            if (signInresult.Succeeded)
            {
                return Redirect(returnUrl);
            }

            if(signInresult.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string>() { "3 dakika giri� yapamazs�n�z" });
                return View();
            }


            
            ModelState.AddModelErrorList(new List<string>() { "Email yada �ifre yanl��", $"Ba�ar�s�z Giri� Say�s� = {await _userManager.GetAccessFailedCountAsync(hasUser)}" });


            return View();
           
            
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            
            if (!ModelState.IsValid)
            {
                return View();
            }
            //create async ile request olarak gelen view modeldekileri verileri e�ledik.
            var identyResult = await _userManager.CreateAsync(new() { UserName = request.UserName, PhoneNumber = request.Phone, Email = request.Email }, request.PasswordConfirm);
            //ba�ar�l� ise mesaj verdik 
            if (identyResult.Succeeded)
            {
                TempData["SuccessMessage"] = "�yeli Kay�t i�lemi ba�ar�yla ger�ekle�ti.";
                return RedirectToAction(nameof(SignUp));
            }
            //e�er hata var ise �dentity error ile hatalar� i�inde d�nd�k.
            //hatay� modelstate imze ekledik,�zel bir�eye tan�mlad�k genel oldugu i�in ,a��klamas�nda bast�k

            ModelState.AddModelErrorList(identyResult.Errors.Select(x=>x.Description).ToList());
           
            return View();
           
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel request)
        {
            var hasUser = await _userManager.FindByEmailAsync(request.Email); //bu kullan�c� varm� yokmu
            if(hasUser is null)
            {
                ModelState.AddModelError(String.Empty, "Bu Email adresine sahip kullan�c� bulunamad�");
                return View(); //return view yap�yoruz ��nk� modelstate ler ta��nmaz.Ama e�er ta��mak istersek hatay� Tempdata ile ta��yoruz
            }
            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);
            var passwordResetLink = Url.Action("ForgetPassword","Home",new {userId = hasUser.Id,Token=passwordResetToken}); //url olu�turuyoruz
            //Email service
            TempData["SuccessMessage"] = "�ifre S�f�rlama Maili G�nderildi";
            return RedirectToAction(nameof(ResetPassword));
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
