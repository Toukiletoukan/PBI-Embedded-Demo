using System.Web;
using System.Web.Mvc;

namespace CodeurEnSeine2017.PBIEmbeded.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
