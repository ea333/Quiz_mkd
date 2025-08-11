using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Bcpg;
using Quiz.Domain.Domain_Models;
using Quiz.Domain.ViewModels;
using Quiz.Repository.Implementation;
using Quiz.Repository.Interface;
using Quiz.Utility;

namespace Quiz.Web.Areas.User.Controllers
{

    [Area("User")]
    public class EventController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IApplicationUserRepository _applicationUserRepository;
        public EventController(IUnitOfWork unitOfWork, IApplicationUserRepository applicationUserRepository)
        {
            _unitOfWork = unitOfWork;
            _applicationUserRepository = applicationUserRepository;
        }
        public IActionResult Index()
        {
            var items = _unitOfWork.Event.GetAll();
            items = items.OrderBy(u => u?.StartDate).ToList();
            List<EventVM> eventsVM = new List<EventVM>();
            bool flag = false;
            var userEmail = User.Identity.Name;
            if (userEmail == null) 
            {
                foreach (var item in items)
                {
                    EventVM eventVM = new()
                    {
                        Event = item,
                        IsUserRegisteredForTheEvent = flag
                    };
                    eventsVM.Add(eventVM);
                }
                return View(eventsVM);

            }
            var user = _applicationUserRepository.GetByEmail(userEmail);

            var eventUsers = _unitOfWork.Event_User.GetAll(u => u.UserId == user.Id, includeProperties: "Event,User");
            var eventsList = new List<Event>();

                foreach (var n in eventUsers)
                {
                    eventsList.Add(n.Event);
                }

                foreach (var item in items)
                {
                    flag = false;
                    if (eventsList.Contains(item))
                    {
                        flag = true;
                    }
                    EventVM eventVM = new()
                    {
                        Event = item,
                        IsUserRegisteredForTheEvent = flag
                    };
                    eventsVM.Add(eventVM);
                }
                //TODO chnage the color of the button for prijavi se 
            
            return View(eventsVM);
        }

        public IActionResult Detail(int? eventId) 
        {
            var item = _unitOfWork.Event.Get(u => u.Id == eventId);
            return View(item);
        }

        public IActionResult RegisterEvent(int? eventId)
        {
            var userEmail = User.Identity.Name;
            var user = _applicationUserRepository.GetByEmail(userEmail);

            var event_user = new Event_User
            {
                EventId = eventId,
                UserId = user.Id
            };
            var eventPending_User = new EventPending_User
            {
                EventId = eventId,
                UserId = user.Id

            };
            _unitOfWork.Event_User.Add(event_user);
            _unitOfWork.Save();
            _unitOfWork.EventPending_User.Add(eventPending_User);
            _unitOfWork.Save();

            return RedirectToAction("Index", "Event", new { area = "User"});
        }


        public IActionResult LeaveEvent(int? eventId) 
        {

            var userEmail = User.Identity.Name;
            var user = _applicationUserRepository.GetByEmail(userEmail);

            var eventUser = _unitOfWork.Event_User.Get(u => u.UserId == user.Id && u.EventId == eventId);
            var eventPending_User = _unitOfWork.EventPending_User.Get(u => u.UserId == user.Id && u.EventId == eventId);
            _unitOfWork.Event_User.Remove(eventUser);
            _unitOfWork.Save();
            _unitOfWork.EventPending_User.Remove(eventPending_User);
            _unitOfWork.Save();


            return RedirectToAction("Index", "Event", new { area = "User"});
        }
    }
}
