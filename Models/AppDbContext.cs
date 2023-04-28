using AppMVC.Models.SchoolManagement;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppMVC.Models
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

            // school
            modelBuilder.Entity<School>(entity =>
            {
                entity.HasIndex(s => s.Id)
                   .IsUnique();
                entity.HasIndex(s => s.Name)
                   .IsUnique();
            });

            // khoa
            modelBuilder.Entity<Department>().HasIndex(d => d.Id).IsUnique();
            modelBuilder.Entity<Department>()
              .Property(e => e.CreatedDate)
              .HasDefaultValueSql("getdate()");

            // lop
            modelBuilder.Entity<ClassModel>().HasIndex(c => c.Id).IsUnique();

            modelBuilder.Entity<ClassModel>()
                .Property(c => c.CreatedDate)
                .HasDefaultValueSql("getdate()");

            // Fluent API - user
            modelBuilder.Entity<AppUser>()
                 .Property(e => e.CreatedDate)
                 .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<AppUser>()
                .Property(e => e.ModifiedDate)
                .HasDefaultValueSql("getdate()")
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<AppUser>()
               .Property(e => e.CreatedBy)
               .HasMaxLength(256);

            modelBuilder.Entity<AppUser>()
                .Property(e => e.ModifiedBy)
                .HasMaxLength(256);

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<School> Schools { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<ClassModel> Classes { get; set; }

        public DbSet<AppUser> Students { get; set; }
    }
}