using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Abstractions;
using Quiz.Domain.ViewModels;
using Quiz.Repository.Interface;
using Quiz.Utility;
using Quiz.Utility.Options;
using System.Net;
using System.Net.Mail;

namespace Quiz.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class EmailController : Controller
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IEmailSender _emailSender;
        private readonly SmtpOptions _options;


        public EmailController(IApplicationUserRepository applicationUserRepository, IEmailSender emailSender,IOptions<SmtpOptions> options)
        {
            _applicationUserRepository = applicationUserRepository;
             _emailSender = emailSender;
            _options = options.Value;
        }

        public IActionResult Create()
        {
            EmailVM emailVM = new()
            {
                email = "",
                subject = "",
                htmlMessage="",
                emailsSent = false,
            };
            return View(emailVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmailVM emailVM)//if i want to place images in the email use tinyMice
        {
            var users = _applicationUserRepository.GetAll()
                .Where(u=>u.EmailConfirmed).ToList();

            foreach(var user in users) 
            {
                    try
                    {
                        await _emailSender.SendEmailAsync(user.Email, emailVM.subject, emailVM.htmlMessage);

                    }
                    catch (Exception e) 
                    {
                        Console.WriteLine($"Failed to send mail to user {user.Email} : {e.Message}");
                    }
            }
            emailVM.emailsSent = true;

            return View(emailVM);
        }
    }
}
