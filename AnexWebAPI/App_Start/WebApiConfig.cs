using System.Web.Http;

namespace AnexWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Çıktıyı XML değilde JSON formatında almak için
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
