using System.Web;
using System.Web.Mvc;

namespace PhanAnhThang_2011069025_projectB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
