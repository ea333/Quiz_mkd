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

    public class QuestionController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public QuestionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int quizId,int? selectedTypeQuestionId = null)
        {
            var quiz = _unitOfWork.Quiz.Get(u => u.Id == quizId, includeProperties: "QuestionList");

            if (quiz == null) 
            {
                return NotFound();
            }
            var questions = _unitOfWork.Question.GetAll(includeProperties: "AnswersList,TypeQuestion").ToList();

            if (selectedTypeQuestionId != null) 
            {
                questions = _unitOfWork.Question.GetAll(u=>u.TypeQuestion.Id == selectedTypeQuestionId,includeProperties: "AnswersList,TypeQuestion").ToList();
            }

            List<QuestionBelongsVM?> listQuestionsBelongToQuiz = new List<QuestionBelongsVM?>();
            int count = 0;
            foreach (var q in questions) 
            {
                bool flag = false;   
                if (quiz.QuestionList.Contains(q)) 
                {
                    flag = true;
                    count++;
                }
                var temp = new QuestionBelongsVM
                {
                    Question = q,
                    flagBelongs = flag
                };
                listQuestionsBelongToQuiz.Add(temp);
            }

            var typesOfQuestions = _unitOfWork.TypeQuestion.GetAll()
              .Select(u => new SelectListItem
              {
                  Text = u.Type,
                  Value = u.Id.ToString()
              }).ToList();

           var quizVM = new QuizVM
            {
                Quiz = quiz,
                QuizId = quizId,
                count = count,
               TypeQuestions = typesOfQuestions,
                QuestionListBelongs = listQuestionsBelongToQuiz

            };
            return View(quizVM);
        }

        [HttpPost]
        public IActionResult FilterQuestions(QuizVM quizVM) 
        {

            return RedirectToAction("Index", new { quizId = quizVM.QuizId, selectedTypeQuestionId = quizVM.SelectedTypeQuestionId });
        }

        public IActionResult AddToQuiz(int? quizId, int questionId) 
        {
            var quiz = _unitOfWork.Quiz.Get(u => u.Id == quizId, includeProperties:"QuestionList");
            var question = _unitOfWork.Question.Get(u => u.Id == questionId);
            quiz.QuestionList.Add(question);
            _unitOfWork.Question.Update(question);
            _unitOfWork.Save();
            return RedirectToAction("Index", new { quizId = quizId });
        }

        public IActionResult RemoveFromQuiz(int? quizId, int questionId)
        {
            var quiz = _unitOfWork.Quiz.Get(u => u.Id == quizId, includeProperties: "QuestionList");

            quiz.QuestionList = quiz.QuestionList.Where(q => q.Id != questionId).ToList();

            var question = _unitOfWork.Question.Get(u => u.Id == questionId);

            question.Quiz = null;

            _unitOfWork.Question.Update(question);
            _unitOfWork.Save();
            _unitOfWork.Quiz.Update(quiz);
            _unitOfWork.Save();
           
            return RedirectToAction("Index", new { quizId = quizId });
        }

        public IActionResult Create(int quizId)
        {
            if (quizId == 0)
            {
                return NotFound();
            }
            var quiz = _unitOfWork.Quiz.Get(u => u.Id == quizId, includeProperties: "QuestionList");
            if (quiz == null)
            {
                return NotFound();
            }
            var typeQuestion = _unitOfWork.TypeQuestion.GetAll().Select(u => new SelectListItem
            {
                Text = u.Type,
                Value = u.Id.ToString()
            }).ToList();
            QuestionVM questionVM = new()
            {
                Question = new Question(),
                Quiz = quiz,
                TypeQuestionList = typeQuestion

            };
            return View(questionVM);
        }

        [HttpPost]
        public IActionResult Create(QuestionVM questionVM, int quizId)
        {
            var quiz = _unitOfWork.Quiz.Get(u => u.Id == quizId, includeProperties: "QuestionList");

            if (quiz == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                questionVM.Quiz = quiz;
                var question = questionVM.Question;
                question.TypeQuestionId = questionVM.TypeQuestionId;
                quiz?.QuestionList?.Add(question);
                _unitOfWork.Question.Add(question);
                _unitOfWork.Save();

                questionVM.Answer1.QuestionId = question.Id;
                questionVM.Answer2.QuestionId = question.Id;
                questionVM.Answer3.QuestionId = question.Id;
                questionVM.Answer4.QuestionId = question.Id;

                List<Answer> temp = new List<Answer>();
                temp.Add(questionVM.Answer1);
                temp.Add(questionVM.Answer2);
                temp.Add(questionVM.Answer3);
                temp.Add(questionVM.Answer4);


                if (temp.Count(a => a.isCorrect) == 1) 
                {
                    _unitOfWork.Answer.Add(questionVM.Answer1);
                    _unitOfWork.Answer.Add(questionVM.Answer2);
                    _unitOfWork.Answer.Add(questionVM.Answer3);
                    _unitOfWork.Answer.Add(questionVM.Answer4);
                    _unitOfWork.Save();
                    return RedirectToAction("Detail", "Quiz", new { area = "Admin", quizId = quizId });

                }
                _unitOfWork.Question.Remove(question);
                _unitOfWork.Save();
                return RedirectToAction("Create", "Question", new { area = "Admin", quizId = quizId });

            }

            var typeQuestion = _unitOfWork.TypeQuestion.GetAll().Select(u => new SelectListItem
            {
                Text = u.Type,
                Value = u.Id.ToString()
            }).ToList();

            questionVM.Answers = new List<Answer>();
            questionVM.Quiz = quiz;
            questionVM.Question = new Question();
            questionVM.TypeQuestionList = typeQuestion;
            return View(questionVM);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = _unitOfWork.Question.Get(u => u.Id == id, includeProperties: "AnswersList,Quiz");
            if (item == null)
            {
                return NotFound();
            }
            QuestionVM questionVM = new()
            {
                Question = item,
                Answers = item.AnswersList,
                Quiz = item.Quiz
            };
            return View(questionVM);
        }

        [HttpPost]
        public IActionResult Edit(QuestionVM questionVm)
        {
            if (ModelState.IsValid)
            {

                _unitOfWork.Question.Update(questionVm.Question);
                _unitOfWork.Save();
                return RedirectToAction("Detail", "Quiz", new { quizId = questionVm.Question.QuizId});
            }

            return View(questionVm);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = _unitOfWork.Question.Get(u => u.Id == id, includeProperties: "AnswersList,Quiz");
            if (item == null)
            {
                return NotFound();
            }
            QuestionVM questionVM = new()
            {
                Question = item,
                Answers = item.AnswersList,
                Quiz = item.Quiz
            };
            return View(questionVM);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteDB(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var question = _unitOfWork.Question.Get(u => u.Id == id, includeProperties: "AnswersList,Quiz");
            if (question == null)
            {
                return NotFound();
            }
            _unitOfWork.Question.Remove(question);
            _unitOfWork.Save();
            return RedirectToAction("Detail", "Quiz", new { quizId = question.QuizId });



        }


    }
}
