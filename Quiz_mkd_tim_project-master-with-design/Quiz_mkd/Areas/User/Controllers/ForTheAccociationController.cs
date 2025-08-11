using Microsoft.AspNetCore.Mvc;

namespace Quiz.Web.Areas.User.Controllers
{
    [Area("User")]
    public class ForTheAccociationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
