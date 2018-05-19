//using System;
//using System.Threading.Tasks;
//using Microsoft.Owin;
//using Owin;
//using Trailer_NET_DL.Concrete;
//using Trailer_NET_DL.Indentity;
//using Microsoft.Owin.Security.OAuth;
//using Microsoft.AspNet.Identity;
//using Microsoft.Owin.Cors;

//[assembly: OwinStartup(typeof(Trailer_NET_API.Startup))]

//namespace Trailer_NET_API
//{
//    public class Startup
//    {
//        public void Configuration(IAppBuilder app)
//        {
//            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
//            app.CreatePerOwinContext(AppDbContext.Create);
//            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
//            app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);

//            app.UseCors(CorsOptions.AllowAll);

//            OAuthAuthorizationServerOptions option = new OAuthAuthorizationServerOptions
//            {
//                TokenEndpointPath = new PathString("/token"),
//                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
//                //Provider = 
//                //Provider = new 
//                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
//                AllowInsecureHttp = true
//            };
//            app.UseOAuthAuthorizationServer(option);
//            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
//        }
//    }
//}
