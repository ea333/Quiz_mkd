using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quiz.Domain.Identity;
using Quiz.Domain.ViewModels;
using Quiz.Repository.Interface;
using Quiz.Utility;

namespace Quiz.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class UserController : Controller
    {

        private readonly IApplicationUserRepository _applicationUserRepository;

      
        public UserController( IApplicationUserRepository applicationUserRepository)
        {
            _applicationUserRepository = applicationUserRepository;
        }

        public IActionResult Index()
        {
            var userList = _applicationUserRepository.GetAll();
            List<ApplicationUserVM> applicationUserVMs = new List<ApplicationUserVM>();
            var signedInUser = User.Identity.Name;
            foreach (var user in userList) 
            {
                //if (user.Email == signedInUser) 
                //{
                //    continue;
                //}
                
                var role = _applicationUserRepository.GetUserRole(user.Id);
                ApplicationUserVM vm = new () 
                {
                    User = user,
                    Role = role
                };
                applicationUserVMs.Add(vm);
            }
            return View(applicationUserVMs);
        }

        public async Task<IActionResult> ChangeRole(string? userId, string role) 
        {

            if (userId == null)
            {
                return NotFound("User not found.");
            }

            try
            {
                await _applicationUserRepository.ChangeUserRole(userId, role);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using ILogger)
                return StatusCode(500, "An error occurred while changing the user role.");
            }
        }


        public async Task<IActionResult> DeleteUser(string? userId)
        {
            if (userId == null)
            {
                return NotFound("User not found.");
            }

            var result = _applicationUserRepository.DeleteById(userId);

            if (result.Succeeded)
            {
                return RedirectToAction("Index"); 
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("Error"); 
        }


    }
}
