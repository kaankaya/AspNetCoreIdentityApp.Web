using System.Diagnostics;
using AspNetCoreIdentityApp.Web.Extenisons;
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
        //returnUrl kullanýcý giriþ yaptýktan sonra o girmeye çalýþtýðý sayfaya yönlendirmek için kullanacaðýz
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model,string? returnUrl=null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Home");

           var hasUser = await _userManager.FindByEmailAsync(model.Email);
            if(hasUser is null)
            {
                ModelState.AddModelError(string.Empty, "Email veya Þifre yanlýþ");
                return View();
            }
            var signInresult = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, false);

            if (signInresult.Succeeded)
            {
                return Redirect(returnUrl);
            }

            ModelState.AddModelErrorList(new List<string>() { "Email yada Þifre yanlýþ" });
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
            //create async ile request olarak gelen view modeldekileri verileri eþledik.
            var identyResult = await _userManager.CreateAsync(new() { UserName = request.UserName, PhoneNumber = request.Phone, Email = request.Email }, request.PasswordConfirm);
            //baþarýlý ise mesaj verdik 
            if (identyResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Üyeli Kayýt iþlemi baþarýyla gerçekleþti.";
                return RedirectToAction(nameof(SignUp));
            }
            //eðer hata var ise ýdentity error ile hatalarý içinde döndük.
            //hatayý modelstate imze ekledik,özel birþeye tanýmladýk genel oldugu için ,açýklamasýnda bastýk

            ModelState.AddModelErrorList(identyResult.Errors.Select(x=>x.Description).ToList());
           
            return View();
           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
