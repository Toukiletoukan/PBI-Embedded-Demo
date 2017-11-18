using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeurEnSeine2017.PBIEmbeded.WebApp.Models
{
    public class ShowEmbedReportViewModel : PBIItemBaseViewModel
    {
        public string ReportId { get; set; }
        public string EmbedUrl { get; set; }
        public string ReportName { get; set; }
    }
}