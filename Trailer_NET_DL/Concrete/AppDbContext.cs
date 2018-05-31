using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Trailer_NET_Library.Entities;

namespace Trailer_NET_DL.Concrete
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() : base("AppDbContext") { }

        static AppDbContext()
        {
            Database.SetInitializer(new AppDbInitializer());
        }
        
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Movie>().HasMany(x => x.Images).WithMany(x => x.Movies)
            //    .Map(x => x.MapRightKey("ImageID").MapLeftKey("MovieID").ToTable("Movie_Image"));

            modelBuilder.Entity<AppUser>().ToTable("Users").Property(x => x.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserRole>().ToTable("User_Roles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("User_Logins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("User_Claims").Property(x => x.Id).HasColumnName("UserClaimId");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles").Property(x => x.Id).HasColumnName("RoleId");
            modelBuilder.Entity<AppUser>().HasMany(x => x.Liked).WithMany()
               .Map(x => x.MapRightKey("MovieID").MapLeftKey("UserId").ToTable("Liked_Table"));
            modelBuilder.Entity<AppUser>().HasMany(x => x.Watch_Later).WithMany()
                .Map(x => x.MapRightKey("MovieID").MapLeftKey("UserId").ToTable("Watch_Later_Table"));
        }

        public static AppDbContext Create() => new AppDbContext();
    }
}
