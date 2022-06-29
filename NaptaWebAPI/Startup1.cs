using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using NaptaBackend;
using Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(NaptaWebAPI.Startup1))]

namespace NaptaWebAPI
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            app.UseCors(CorsOptions.AllowAll);

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(15),
                AllowInsecureHttp = true,
                Provider = new CreateToken()

            }) ;

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                new { id = RouteParameter.Optional, email = RouteParameter.Optional }
                );
            config.Routes.MapHttpRoute(
                name: "AccountApi",
                routeTemplate: "api/{controller}/{email}",
                new { email = RouteParameter.Optional}
                );

            app.UseWebApi(config);
            
        }
    }

    internal class CreateToken : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add(" Access - Control - Allow - Origin ", new[] { "*" });


            UserStore<IdentityUser> store = new UserStore<IdentityUser>(new Context());
            UserManager<IdentityUser> manager = new UserManager<IdentityUser>(store);
            
            IdentityUser user = await  manager.FindAsync(context.UserName, context.Password);
           
            if (user != null)
            {
                ClaimsIdentity claims = new ClaimsIdentity(context.Options.AuthenticationType);

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                claims.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                context.Validated(claims);
            }
            else
                 context.SetError("grant_error", "Email & password aren't valid");
        }
    }
}
