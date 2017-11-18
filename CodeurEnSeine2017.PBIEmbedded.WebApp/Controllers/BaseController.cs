using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeurEnSeine2017.PBIEmbeded.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        protected static string tenantId = ConfigurationManager.AppSettings["ida:TenantId"];
        protected static string authority = aadInstance + tenantId;

        protected static string pbiApiUrl = ConfigurationManager.AppSettings["pbi:apiurl"];
        protected static string pbiApiResourceUrl = ConfigurationManager.AppSettings["pbi:resourceurl"];
        
    }
}