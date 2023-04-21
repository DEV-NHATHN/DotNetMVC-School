using AppMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Models
{
   // App.Models.AppDbContext
   public class AppDbContext : IdentityDbContext<AppUser>
   {
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
      {
         //..
         // this.Roles
         // IdentityRole<string>
      }

      protected override void OnConfiguring(DbContextOptionsBuilder builder)
      {
         base.OnConfiguring(builder);
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);

         foreach (var entityType in modelBuilder.Model.GetEntityTypes())
         {
            var tableName = entityType.GetTableName();
            if (tableName.StartsWith("AspNet"))
            {
               entityType.SetTableName(tableName.Substring(6));
            }
         }

         // Post
         modelBuilder.Entity<Category>(entity =>
         {
            entity.HasIndex(c => c.Id)
                  .IsUnique();
         });
      }

      public DbSet<Category> Categories { get; set; }
   }
}