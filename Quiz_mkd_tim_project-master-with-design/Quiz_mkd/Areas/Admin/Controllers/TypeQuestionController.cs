using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.Domain.Domain_Models;
using Quiz.Domain.ViewModels;
using Quiz.Repository.Interface;
using Quiz.Utility;

namespace Quiz.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class TypeQuestionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TypeQuestionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var items = _unitOfWork.TypeQuestion.GetAll(includeProperties: "QuestionList");
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TypeQuestion TypeQuestion)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.TypeQuestion.Add(TypeQuestion);
                _unitOfWork.Save();
                return RedirectToAction("Index", "TypeQuestion");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var item = _unitOfWork.TypeQuestion.Get(u => u.Id == id, includeProperties: "QuestionList");

            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(TypeQuestion TypeQuestion)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.TypeQuestion.Update(TypeQuestion);
                _unitOfWork.Save();
                return RedirectToAction("Index", "TypeQuestion",new { area="Admin"});
            }

            return View(TypeQuestion);
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var item = _unitOfWork.TypeQuestion.Get(u => u.Id == id, includeProperties: "QuestionList");

            return View(item);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteDB(int? id)
        {
            var item = _unitOfWork.TypeQuestion.Get(u => u.Id == id, includeProperties: "QuestionList");

            if (item == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.TypeQuestion.Remove(item);
                _unitOfWork.Save();
                return RedirectToAction("Index", "TypeQuestion");
            }

            return View(item);
        }
    }
}
