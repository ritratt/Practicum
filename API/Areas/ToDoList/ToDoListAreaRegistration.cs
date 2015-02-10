using System.Web.Mvc;

namespace API.Areas.ToDoList
{
    public class ToDoListAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ToDoList";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ToDoList_default",
                "ToDoList/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}