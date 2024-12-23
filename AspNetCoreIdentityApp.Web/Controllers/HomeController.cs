using System.Diagnostics;
using AspNetCoreIdentityApp.Web.Models;
using AspNetCoreIdentityApp.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //�dentity y�netimi i�in usermanager dependecy yapt�k
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
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
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            //create async ile request olarak gelen view modeldekileri verileri e�ledik.
            var identyResult = await _userManager.CreateAsync(new() { UserName=request.UserName,PhoneNumber=request.Phone,Email=request.Email},request.PasswordConfirm);
            //ba�ar�l� ise mesaj verdik 
            if (identyResult.Succeeded)
            {
                TempData["SuccessMessage"] = "�yeli Kay�t i�lemi ba�ar�yla ger�ekle�ti.";
                return RedirectToAction(nameof(SignUp));
            }
            //e�er hata var ise �dentity error ile hatalar� i�inde d�nd�k.
            //hatay� modelstate imze ekledik,�zel bir�eye tan�mlad�k genel oldugu i�in ,a��klamas�nda bast�k
            foreach(IdentityError item in identyResult.Errors)
            {
                ModelState.AddModelError(string.Empty,item.Description);
            }
            return View();
           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
