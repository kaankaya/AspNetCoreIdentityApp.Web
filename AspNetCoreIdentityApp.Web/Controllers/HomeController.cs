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
        //ýdentity yönetimi için usermanager dependecy yaptýk
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
            //create async ile request olarak gelen view modeldekileri verileri eþledik.
            var identyResult = await _userManager.CreateAsync(new() { UserName=request.UserName,PhoneNumber=request.Phone,Email=request.Email},request.PasswordConfirm);
            //baþarýlý ise mesaj verdik 
            if (identyResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Üyeli Kayýt iþlemi baþarýyla gerçekleþti.";
                return RedirectToAction(nameof(SignUp));
            }
            //eðer hata var ise ýdentity error ile hatalarý içinde döndük.
            //hatayý modelstate imze ekledik,özel birþeye tanýmladýk genel oldugu için ,açýklamasýnda bastýk
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
