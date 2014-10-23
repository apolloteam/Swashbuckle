using System.Web.Http;
using Swashbuckle.Application;
using System.Web.Http.Routing;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;

namespace Swashbuckle
{
    public static class Bootstrapper
    {
        public static void Init(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                "swagger_root",
                SwaggerSpecConfig.StaticInstance.RootPath,
                null,
                null,
                new RedirectHandler(string.Format("{0}/ui/index.html", SwaggerSpecConfig.StaticInstance.RootPath)));

            config.Routes.MapHttpRoute(
                "swagger_ui",
                string.Format("{0}/ui/{{*uiPath}}", SwaggerSpecConfig.StaticInstance.RootPath),
                null,
                new { uiPath = @".+" },
                new SwaggerUiHandler());

            config.Routes.MapHttpRoute(
                "swagger_versioned_api_docs",
                string.Format("{0}/{{apiVersion}}/api-docs/{{resourceName}}", SwaggerSpecConfig.StaticInstance.RootPath),
                new { resourceName = RouteParameter.Optional },
                null,
                new SwaggerSpecHandler());

            config.Routes.MapHttpRoute(
                "swagger_api_docs",
                string.Format("{0}/api-docs/{{resourceName}}", SwaggerSpecConfig.StaticInstance.RootPath),
                new { resourceName = RouteParameter.Optional },
                null,
                new SwaggerSpecHandler());
        }
    }
}