using System.Web.Mvc;

namespace TSP_Covid21.Areas.Game
{
    public class GameAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Game";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Game_BauCua",
                "Game/{controller}",
                new { controller = "BauCua", action = "Home", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Game_default",
                "Game/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}