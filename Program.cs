using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace AppMVC
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var builder = WebApplication.CreateBuilder(args);

         // Add services to the container.
         var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
         // Add services to the container.
         builder.Services.AddDbContext<AppDbContext>(options =>
         {
            options.UseSqlServer(builder.Configuration.GetConnectionString("AppMvcConnectionString"));
         });
         builder.Services.AddDatabaseDeveloperPageExceptionFilter();

         builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
             .AddEntityFrameworkStores<AppDbContext>();
         builder.Services.AddControllersWithViews();
         builder.Services.AddRazorPages();

         var app = builder.Build();

         // Configure the HTTP request pipeline.
         if (app.Environment.IsDevelopment())
         {
            app.UseMigrationsEndPoint();
         }
         else
         {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
         }

         app.UseHttpsRedirection();
         app.UseStaticFiles();

         app.UseRouting();

         app.UseAuthentication();
         app.UseAuthorization();

         app.MapControllerRoute(
             name: "default",
             pattern: "{controller=Home}/{action=Index}/{id?}");
         app.MapRazorPages();

         app.Run();
      }
   }
}