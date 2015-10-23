using System.Web;
using System.Web.Mvc;

namespace ArtPostings
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Not using this:
            // see - http://benfoster.io/blog/aspnet-mvc-custom-error-pages
            // and - http://dusted.codes/demystifying-aspnet-mvc-5-error-pages-and-error-logging
            //filters.Add(new HandleErrorAttribute());
        }
    }
}
