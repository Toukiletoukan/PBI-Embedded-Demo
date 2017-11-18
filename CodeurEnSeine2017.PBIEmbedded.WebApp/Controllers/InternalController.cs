using CodeurEnSeine2017.PBIEmbeded.WebApp.Models;
using CodeurEnSeine2017.PBIEmbeded.WebApp.Models.Utils;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.PowerBI.Api.V2;
using Microsoft.Rest;
using System;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CodeurEnSeine2017.PBIEmbeded.WebApp.Controllers
{
    [Authorize]
    public class InternalController : BaseController
    {
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string appKey = ConfigurationManager.AppSettings["ida:AppKey"];
        
        public async Task<ActionResult> PBIConsole()
        {
            string _accessToken;

            try
            {
                var getTokenResult = await GetTokenForPBIUser();

                _accessToken = getTokenResult.AccessToken;

            }
            catch (AdalSilentTokenAcquisitionException)
            {
                return RedirectToAction("signin", "account", new { force = true });
            }

            PowerBIClient pbiClient = new PowerBIClient(new Uri(pbiApiUrl), new TokenCredentials(_accessToken));
            var reports = await pbiClient.Reports.GetReportsAsync();
            var dashboards = await pbiClient.Dashboards.GetDashboardsAsync();

            PBIItemsListViewModel vm = new PBIItemsListViewModel(reports.Value, dashboards.Value) { AccessToken = _accessToken };

            return View("~/Views/PBIViews/PBIConsole.cshtml", vm);
        }


        private async Task<AuthenticationResult> GetTokenForPBIUser()
        {
            string userObjectID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

            AuthenticationContext authContext = new AuthenticationContext(authority, new NaiveSessionCache(userObjectID, HttpContext));

            ClientCredential clientCredential = new ClientCredential(clientId, appKey);

            return await authContext.AcquireTokenSilentAsync(pbiApiResourceUrl, clientCredential, new UserIdentifier(userObjectID, UserIdentifierType.UniqueId));
        }
    }
}