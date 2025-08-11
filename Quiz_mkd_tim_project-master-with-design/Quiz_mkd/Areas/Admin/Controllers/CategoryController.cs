using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.Domain.Domain_Models;
using Quiz.Repository.Interface;
using Quiz.Utility;

namespace Quiz.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var items = _unitOfWork.Category.GetAll();
            return View(items);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Category");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var item = _unitOfWork.Category.Get(u => u.Id == id, includeProperties: "Category_User");

            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Category");
            }

            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var item = _unitOfWork.Category.Get(u => u.Id == id, includeProperties: "Category_User");

            return View(item);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteDB(int? id)
        {
            var item = _unitOfWork.Category.Get(u => u.Id == id, includeProperties: "Category_User");

            if (item == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Remove(item);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Category");
            }

            return View(item);
        }



    }
}
