using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace QueensEight.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ValidSolutionsApi",
                routeTemplate: "api/v1/solutions/valid",
                defaults: new { controller = "ValidSolutions"}
                );

            config.Routes.MapHttpRoute(
                name: "PendingSolutionsApi",
                routeTemplate: "api/v1/solutions/pending",
                defaults: new {controller = "PendingSolutions"}
                );
        }
    }
}
