using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Trailer_NET_DL.Concrete;
using Microsoft.AspNet.Identity.Owin;

namespace Trailer_NET_DL.Indentity
{
    public class AppRoleManager : RoleManager<IdentityRole>
    {
        public AppRoleManager(RoleStore<IdentityRole> store) : base(store) { }
        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context)
        {
            return new AppRoleManager(new RoleStore<IdentityRole>(context.Get<AppDbContext>()));
        }
    }
}
