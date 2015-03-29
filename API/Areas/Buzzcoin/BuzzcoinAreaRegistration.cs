using System.Web.Mvc;

namespace API.Areas.Buzzcoin
{
    public class BuzzcoinAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Buzzcoin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Buzzcoin_default",
                "{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}