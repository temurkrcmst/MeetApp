using MeetApp.MessageHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace MeetApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;
            // we don't need XML
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // we dont need jQueryFormEncoder
            {
                var remove = config.Formatters.FirstOrDefault<MediaTypeFormatter>((i) => typeof(System.Web.Http.ModelBinding.JQueryMvcFormUrlEncodedFormatter) == i.GetType());
                if (remove != null) config.Formatters.Remove(remove);
            }

            // POST Content-type control
            config.MessageHandlers.Add(new ContentTypeHandler());

            // V1 Api Router
            config.Routes.MapHttpRoute("Meet v1", "api/v1/{action}", new { controller = "Default" });
            config.Routes.MapHttpRoute("Meet Login", "api/v1/Session/{action}", new { controller = "Login" });

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            App.Init();

        }
    }
}
