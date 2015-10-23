using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Idbm
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
              name: "UserPage",
              url: "movie/MyPage",
              defaults: new { controller = "Movie", action = "MyPage", id = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "MovieDetails",
              url: "movie/{title}",
              defaults: new { controller = "Movie", action = "DisplayMovieByTitle", id = UrlParameter.Optional }
          );
           
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Movie", action = "MyHomePage", id = UrlParameter.Optional }
            );
           
 
        }
    }
}