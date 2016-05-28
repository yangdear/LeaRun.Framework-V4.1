using System.Web.Mvc;

namespace LeaRun.WebApp.Areas.CommonModule
{
    public class CommonModuleAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CommonModule";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                this.AreaName + "_Default",
                this.AreaName + "/{controller}/{action}/{id}",
                new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
                new string[] { "LeaRun.WebApp.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}
