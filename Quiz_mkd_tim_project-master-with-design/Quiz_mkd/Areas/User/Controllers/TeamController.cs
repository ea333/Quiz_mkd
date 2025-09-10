using Microsoft.AspNetCore.Mvc;

namespace Quiz.Web.Areas.User.Controllers
{
    [Area("User")]
    public class TeamController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
