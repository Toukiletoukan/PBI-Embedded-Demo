using CodeurEnSeine2017.PBIEmbeded.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeurEnSeine2017.PBIEmbeded.WebApp.Controllers
{
    [AllowAnonymous]
    public class PBIController : Controller
    {
        public ActionResult ShowReport(string reportid, string reporturl, string reportname, string accesstoken)
        {
            ShowEmbedReportViewModel model = new ShowEmbedReportViewModel { AccessToken = accesstoken, ReportId = reportid, ReportName = reportname, EmbedUrl = reporturl };

            return View("~/Views/PBIViews/ShowEmbedReport.cshtml", model);
        }

        public ActionResult ShowDashboard(string dashboardid, string dashboardurl, string dashboardname, string accesstoken)
        {
            ShowEmbedDashboardVieModel model = new ShowEmbedDashboardVieModel { AccessToken = accesstoken, EmbedUrl = dashboardurl, DashboardId = dashboardid, DashboardName = dashboardname };

            return View("~/Views/PBIViews/ShowEmbedDashboard.cshtml", model);
        }
    }
}