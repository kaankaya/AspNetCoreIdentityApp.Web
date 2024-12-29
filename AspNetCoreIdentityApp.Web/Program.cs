using AspNetCoreIdentityApp.Web.Extenisons;
using AspNetCoreIdentityApp.Web.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//build edilmeden önce servis olarak db context verdik
//options ayarlarýnda sqlserver kullancaðýmýzý söyledik ve baðlalantý ismimizi verdik
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});
//ýdentity için gerekli jenerik parametreyi verdik ve kullnacagý dbcontext i verdik
//burayý extensions a çevirdik
builder.Services.AddIdentityWithExt();

builder.Services.ConfigureApplicationCookie(options =>
{
    var cookieBuilder = new CookieBuilder();
    cookieBuilder.Name = "AppCookie";
    options.LoginPath = new PathString("/Home/Signin");
    options.LogoutPath = new PathString("/Member/Logout");
    options.Cookie = cookieBuilder;
    //cookie 60 gün kalýcak
    options.ExpireTimeSpan = TimeSpan.FromDays(60);
    //kullanýcý  her giriþ yaptýgýnda 60 gün olarak yenileyecek
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
