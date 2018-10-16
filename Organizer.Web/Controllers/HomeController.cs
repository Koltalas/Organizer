using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace Organizer.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : OrganizerControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}