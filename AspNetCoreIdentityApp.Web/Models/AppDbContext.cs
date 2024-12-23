using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityApp.Web.Models
{
    //Identity 3 parametre alır,User için AppUser verdik Role için AppRole,ıd sinide guid tutucağımız için string verdik
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,string>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
       
    }
}
