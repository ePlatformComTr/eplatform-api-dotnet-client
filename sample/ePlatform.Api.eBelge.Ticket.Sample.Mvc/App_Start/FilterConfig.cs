using System.Web;
using System.Web.Mvc;

namespace ePlatform.Api.eBelge.Ticket.Sample.Mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
