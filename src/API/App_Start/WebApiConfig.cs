using System.Web.Http;

namespace QueensEight.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

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
