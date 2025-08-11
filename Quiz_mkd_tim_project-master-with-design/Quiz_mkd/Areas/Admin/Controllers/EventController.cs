using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quiz.Domain.Domain_Models;
using Quiz.Domain.ViewModels;
using Quiz.Repository.Interface;
using Quiz.Utility;

namespace Quiz.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class EventController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EventController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // Helper: Example categories - replace with DB source if needed
        private SelectList GetCategories()
        {
            var categories = new[]
            {
                new { Id = 1, Name = "Спорт" },
                new { Id = 2, Name = "Култура" },
                new { Id = 3, Name = "Образование" },
                new { Id = 4, Name = "Технологија" }
            };
            return new SelectList(categories, "Name", "Name");
        }

        public IActionResult Create()
        {
            var eventVM = new EventVM
            {
                Event = new Event()
                {
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(1)
                }
            };
            ViewBag.Categories = GetCategories();
            return View(eventVM);
        }

        [HttpPost]
        public IActionResult Create(EventVM itemVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string eventPath = Path.Combine(wwwRootPath, @"images\event");

                    using (var fileStream = new FileStream(Path.Combine(eventPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    itemVM.Event.ImageUrl = @"\images\event\" + fileName;
                }

                if (itemVM.Event.Id == 0)
                {
                    _unitOfWork.Event.Add(itemVM.Event);
                    _unitOfWork.Save();

                    var rangList = new RangList
                    {
                        EventId = itemVM.Event.Id,
                    };
                    _unitOfWork.RangList.Add(rangList);
                    _unitOfWork.Save();

                    return RedirectToAction("Index", "Event", new { area = "User" });
                }
            }

            ViewBag.Categories = GetCategories();
            return View(itemVM);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var item = _unitOfWork.Event.Get(u => u.Id == id);
            if (item == null) return NotFound();

            var eventVM = new EventVM
            {
                Event = item
            };

            ViewBag.Categories = GetCategories();
            return View(eventVM);
        }

        [HttpPost]
        public IActionResult Edit(EventVM eventVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string eventPath = Path.Combine(wwwRootPath, @"images\event");

                    if (!string.IsNullOrEmpty(eventVM.Event.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, eventVM.Event.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(eventPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    eventVM.Event.ImageUrl = @"\images\event\" + fileName;
                }

                var existingEvent = _unitOfWork.Event.Get(u => u.Id == eventVM.Event.Id);
                existingEvent.Name = eventVM.Event.Name;
                existingEvent.Description = eventVM.Event.Description;
                existingEvent.StartDate = eventVM.Event.StartDate;
                existingEvent.EndDate = eventVM.Event.EndDate;
                existingEvent.ImageUrl = eventVM.Event.ImageUrl;

                // NEW: update category
                existingEvent.Category = eventVM.Event.Category;

                _unitOfWork.Event.Update(existingEvent);
                _unitOfWork.Save();

                return RedirectToAction("Index", "Event", new { area = "User" });
            }

            ViewBag.Categories = GetCategories();
            return View(eventVM);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var item = _unitOfWork.Event.Get(u => u.Id == id, includeProperties: "RangList");
            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteID(int? id)
        {
            if (id == null) return NotFound();

            var item = _unitOfWork.Event.Get(u => u.Id == id, includeProperties: "RangList");
            if (item == null) return NotFound();

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (item.ImageUrl != null)
            {
                var oldImagePath = Path.Combine(wwwRootPath, item.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _unitOfWork.Event.Remove(item);
            _unitOfWork.Save();

            return RedirectToAction("Index", "Event", new { area = "User" });
        }
    }
}
