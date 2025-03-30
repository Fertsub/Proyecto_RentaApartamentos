using System.Web;
using System.Web.Mvc;

namespace ProyectoAPI_FabioDiscua_CristopherFlores
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
