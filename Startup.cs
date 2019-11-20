using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(WebApplication3.Startup))]

namespace WebApplication3
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.Use(async (Context, next) =>
            //{
            //    //logger.Debug("after OIDC, before app handler");
            //    await next.Invoke();
            //    //logger.Debug("after OIDC, after app handler");
            //    Console.WriteLine("Done with first MW");
            //});
            app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ApplicationCookie);

            app.Use(async (Context, next) =>
            {
                //logger.Debug("after OIDC, before app handler");
                await next.Invoke();
                //logger.Debug("after OIDC, after app handler");
                Console.WriteLine("Done with Second MW");
            });
            app.UseCookieAuthentication(new CookieAuthenticationOptions {});
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions()
            {
                ClientId = "dhc.admin",
                AuthenticationType = DefaultAuthenticationTypes.ExternalCookie,
           //     AuthenticationMode=AuthenticationMode.Active,
                //AuthenticationMode = AuthenticationMode.Active,
                Authority = "http://localhost:7000/",
                RedirectUri = "http://localhost:7000/signin-oidc",
                //PostLogoutRedirectUri = "http://localhost:5000/signout-callback-oidc",
                ResponseType = "code id_token",
                Scope="openid profile",
                //AuthenticationMode = AuthenticationMode.Passive,
                //AuthenticationType = OpenIdConnectAuthenticationDefaults.AuthenticationType,
                RequireHttpsMetadata = false,
                ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0",
                UseTokenLifetime = false,
                Notifications = new OpenIdConnectAuthenticationNotifications()
                {
                    MessageReceived = n =>
                    {
                        return Task.FromResult(0);
                    },
                    AuthenticationFailed = n =>
                    {
                        return Task.FromResult(0);
                    },
                    AuthorizationCodeReceived = n =>
                    {
                        return Task.FromResult(0);
                    },
                    SecurityTokenReceived = n =>
                    {
                        return Task.FromResult(0);
                    },
                    SecurityTokenValidated = n =>
                    {
                        return Task.FromResult(0);
                    },
                    RedirectToIdentityProvider = (n) =>
                    {
                        return Task.FromResult(0);
                    }
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.Use(async (Context, next) =>
            {
                //logger.Debug("after OIDC, before app handler");
                await next.Invoke();
                //logger.Debug("after OIDC, after app handler");
            });

        }
    }
}