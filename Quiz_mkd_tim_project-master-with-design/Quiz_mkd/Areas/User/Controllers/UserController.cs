using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quiz.Domain.Identity;
using Quiz.Repository.Interface;
using Quiz.Utility;

namespace Quiz.Web.Areas.User.Controllers
{
    [Area("User")]
    public class UserController : Controller
    {


        private readonly IApplicationUserRepository _applicationUserRepository;


        public UserController( IApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
        }

        public IActionResult RangList() 
        {
            var listUsers = _applicationUserRepository.GetAll("User").OrderByDescending(u=> u.Points);
            return View(listUsers);
        }

    }
}
