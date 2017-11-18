using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Clients;
using System.Configuration;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using System.Threading.Tasks;
using Microsoft.PowerBI.Api.V2;
using CodeurEnSeine2017.PBIEmbeded.WebApp.Models;

namespace CodeurEnSeine2017.PBIEmbeded.WebApp.Controllers
{
    [AllowAnonymous]
    public class ExternalController : BaseController
    {
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId_native"];
        private static string username = ConfigurationManager.AppSettings["ext:username"];
        private static string password = ConfigurationManager.AppSettings["ext:password"];
        private static string groupId = ConfigurationManager.AppSettings["ext:groupid"];

        // GET: External
        public async Task<ActionResult> PBIConsole()
        {
            string _accessToken;

            try
            {
                var getTokenResult = await GetTokenForPBIEmbedded();

                _accessToken = getTokenResult.AccessToken;

            }
            catch (AdalSilentTokenAcquisitionException)
            {
                return RedirectToAction("signin", "account", new { force = true });
            }

            var tokenCredentials = new TokenCredentials(_accessToken, "Bearer");
            var client = new PowerBIClient(new Uri(pbiApiUrl), tokenCredentials);

            var reports = await client.Reports.GetReportsInGroupAsync(groupId);
            var dashboards = await client.Dashboards.GetDashboardsInGroupAsync(groupId);

            PBIItemsListViewModel vm = new PBIItemsListViewModel(reports.Value, dashboards.Value) { AccessToken = _accessToken};

            return View("~/Views/PBIViews/PBIConsole.cshtml",vm);
        }

        private async Task<AuthenticationResult> GetTokenForPBIEmbedded()
        {
            UserPasswordCredential credentials = new UserPasswordCredential(username, password);
            
            var authenticationContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(authority);
            return await authenticationContext.AcquireTokenAsync(pbiApiResourceUrl, clientId, credentials);
        }
    }
}