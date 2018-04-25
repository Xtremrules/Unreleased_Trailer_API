namespace Trailer_NET_DL.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;
    using Trailer_NET_DL.Indentity;

    internal sealed class Configuration : DbMigrationsConfiguration<Trailer_NET_DL.Concrete.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Trailer_NET_DL.Concrete.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<IdentityRole>(context));
            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(context));

            var AdminUser = userMgr.FindByName("AdminUser");
            if (AdminUser == null)
            {
                userMgr.Create(new AppUser
                {
                    UserName = "AdminUser",
                    Email = "admin@cbt.com",
                }, "AdminSecret");
                AdminUser = userMgr.FindByName("AdminUser");
            }

            if (!roleMgr.RoleExists("AdminRole"))
            {
                roleMgr.Create(new IdentityRole("AdminRole"));
            }
            if (!roleMgr.RoleExists("Uploader"))
            {
                roleMgr.Create(new IdentityRole("Uploader"));
            }
            if (!roleMgr.RoleExists("UserRole"))
            {
                roleMgr.Create(new IdentityRole("UserRole"));
            }

            if (!userMgr.IsInRole(AdminUser.Id, "AdminRole"))
                userMgr.AddToRole(AdminUser.Id, "AdminRole");
        }
    }
}
