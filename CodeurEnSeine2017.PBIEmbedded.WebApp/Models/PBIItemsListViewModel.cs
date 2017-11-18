using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeurEnSeine2017.PBIEmbeded.WebApp.Models
{
    public class PBIItemsListViewModel
    {
        public string AccessToken { get; set; }
        public IEnumerable<Microsoft.PowerBI.Api.V2.Models.Report> Reports { get; }
        public IEnumerable<Microsoft.PowerBI.Api.V2.Models.Dashboard> Dashboards { get; }


        public PBIItemsListViewModel(IEnumerable<Microsoft.PowerBI.Api.V2.Models.Report> reports, IEnumerable<Microsoft.PowerBI.Api.V2.Models.Dashboard> dashboards)
        {
            Reports = reports;
            Dashboards = dashboards;
        }
    }
}