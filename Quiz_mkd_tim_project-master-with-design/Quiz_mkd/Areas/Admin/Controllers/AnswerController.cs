using Microsoft.AspNetCore.Authorization;
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
    public class AnswerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int? id)
        {
            var items = _unitOfWork.Answer.GetAll(includeProperties: "Question")
                .Where(u => u?.QuestionId == id);

            return View(items);
        }

        public IActionResult Create(int questionId)
        {
            var question = _unitOfWork.Question.Get(u => u.Id == questionId, includeProperties: "Quiz,Answers");
            AnswerVM answerVM = new()
            {
                Answer = new Answer(),
                Question = question,
            };
            return View(answerVM);
        }

        [HttpPost]
        public IActionResult Create(AnswerVM answerVM, int questionId)
        {
            var question = _unitOfWork.Question.Get(u => u.Id == questionId, includeProperties: "Quiz,Answers");

            if (question == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                answerVM.Question = question;
                question?.AnswersList?.Add(answerVM.Answer);
                _unitOfWork.Answer.Add(answerVM.Answer);
                _unitOfWork.Save();
                answerVM.Answer = new Answer();
                if (question?.AnswersList?.Count != 4)
                {
                    return View(answerVM);
                }
                else
                {
                    return RedirectToAction("Index", "Quiz", new { area = "User"});

                }

            }


            answerVM.Answer = new Answer();
            answerVM.Question = question;

            return View(answerVM);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _unitOfWork.Answer.Get(u => u.Id == id, includeProperties: "Question");

            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Answer answer)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Answer.Update(answer);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Answer", new { id = answer.QuestionId });
            }

            return View(answer);
        }

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var item = _unitOfWork.Answer.Get(u => u.Id == id, includeProperties: "Question");

        //    return View(item);
        //}

        //[HttpPost]
        //[ActionName("Delete")]
        //public IActionResult DeleteDB(int? answerId, int questionId)
        //{
        //    if (answerId == null)
        //    {
        //        return NotFound();
        //    }

        //    var item = _unitOfWork.Answer.Get(u => u.Id == answerId, includeProperties: "Question");
        //    _unitOfWork.Answer.Remove(item);
        //    _unitOfWork.Save();

        //    return RedirectToAction("Index", "Answer", new { id = questionId });
        //}
    }
}
