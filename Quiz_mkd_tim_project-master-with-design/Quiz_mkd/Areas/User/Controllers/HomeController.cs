using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quiz.Domain.ViewModels;
using Quiz.Repository.Data;
using Quiz_mkd.Models;
using System.Diagnostics;

namespace Quiz.Web.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            int eventsToShow = 6;

            // Latest events
            var latestEvents = _db.Events
                .OrderByDescending(e => e.StartDate)
                .Take(eventsToShow)
                .Select(e => new EventVM
                {
                    Event = e,
                    IsUserRegisteredForTheEvent = false
                })
                .ToList();

            // Explicit join to ensure users are fetched correctly
            var topCompetitors = (from rlu in _db.RangList_Users
                                  join user in _db.Users on rlu.UserId equals user.Id
                                  where rlu.Points != null
                                  orderby rlu.Points descending
                                  select new
                                  {
                                      FullName = user.NameUser + " " + user.Surname,
                                      Points = rlu.Points
                                  })
                                  .Take(3)
                                  .ToList();

            _logger.LogInformation("Top competitors count: {Count}", topCompetitors.Count);

            foreach (var competitor in topCompetitors)
            {
                _logger.LogInformation("Competitor: {Name} - Points: {Points}", competitor.FullName, competitor.Points);
            }


            ViewBag.TopCompetitors = topCompetitors;

            return View(latestEvents);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
