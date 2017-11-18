using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security.Notifications;
using System.Threading.Tasks;
using CodeurEnSeine2017.PBIEmbeded.WebApp.Models.Utils;

namespace CodeurEnSeine2017.PBIEmbeded.WebApp
{
    public partial class Startup
    {
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string tenantId = ConfigurationManager.AppSettings["ida:TenantId"];
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
        private static string appKey = ConfigurationManager.AppSettings["ida:AppKey"];
        private static string pbiApiResourceId = ConfigurationManager.AppSettings["pbi:resourceurl"];

        private static string redirectUri = ConfigurationManager.AppSettings["ida:redirecturi"];
        
        private static string authority = aadInstance + tenantId;

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    Authority = authority,
                    PostLogoutRedirectUri = postLogoutRedirectUri,
                    Notifications = new OpenIdConnectAuthenticationNotifications()
                    {
                        //
                        // If there is a code in the OpenID Connect response, redeem it for an access token and refresh token, and store those away.
                        //
                        AuthorizationCodeReceived = OnAuthorizationCodeReceived
                    }
                    ,RedirectUri = redirectUri

                });
        }

        private async Task OnAuthorizationCodeReceived(AuthorizationCodeReceivedNotification context)
        {
            var code = context.Code;

            ClientCredential credential = new ClientCredential(clientId, appKey);
            string userObjectID = context.AuthenticationTicket.Identity.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            AuthenticationContext authContext = new AuthenticationContext(authority, new NaiveSessionCache(userObjectID, context.OwinContext.Environment["System.Web.HttpContextBase"] as HttpContextBase));

            // If you create the redirectUri this way, it will contain a trailing slash.  
            // Make sure you've registered the same exact Uri in the Azure Portal (including the slash).
            Uri uri = new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path));

            AuthenticationResult result = await authContext.AcquireTokenByAuthorizationCodeAsync(code, uri, credential, pbiApiResourceId);
        }
    }
}
