using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Quiz.Domain.Domain_Models;
using Quiz.Domain.Identity;
using Quiz.Domain.ViewModels;
using Quiz.Repository.Interface;
using System.Drawing.Printing;
using System.Net;

namespace Quiz.Web.Areas.User.Controllers
{
    [Area("User")]
    public class PlayQuizController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IApplicationUserRepository _applicationUserRepository;

        public PlayQuizController(IUnitOfWork unitOfWork, 
            IApplicationUserRepository applicationUserRepository
          ) 
        {
            _unitOfWork = unitOfWork;
            _applicationUserRepository = applicationUserRepository;
            
        }

        public IActionResult Index(int? quizId, int? eventId)
        {

            var quiz = _unitOfWork.Quiz.Get(u => u.Id == quizId, includeProperties: "QuestionList");
            var eventItem = _unitOfWork.Event.Get(u => u.Id == eventId);
            TempData["CountCorrectAnswer"] = 0;

            if (quiz == null)
            {
                return NotFound();
            }

            PlayQuizVM playQuizVM = new()
            {
                Quiz = quiz,
                Event = eventItem,
                Question = new Question(),
                answersCorrect = 0,
                timeToAnswerQuestion = 5
            };

            return View(playQuizVM);
        }

        public IActionResult ChooseQuestion(int? quizId, int? answerId, int? countCorrect, bool? firstCall)
        {
            var quiz = _unitOfWork.Quiz.Get(u => u.Id == quizId, includeProperties: "QuestionList");
            var answer = _unitOfWork.Answer.Get(u => u.Id == answerId);
            if (quiz == null)
            {
                return NotFound();
            }

            List<int> questionIds = new List<int>();

            if (TempData["NextQuestions"] != null)
            {
                // Retrieve and parse stored question IDs
                questionIds = TempData["NextQuestions"].ToString()
                    .Split(',')
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(int.Parse)
                    .ToList();
            }

            int countCorrectAnswer = TempData.Peek("CountCorrectAnswer") as int? ?? 0;

            if (answer != null) 
            {
                if (answer.isCorrect)
                {
                    countCorrectAnswer++;
                    TempData["CountCorrectAnswer"] = countCorrectAnswer;
                }
            }

         

            if (questionIds.Count == 0 && firstCall == null)
            {
                TempData["CorrectAnswers"] = countCorrectAnswer;
                TempData["TotalAnswers"] = quiz.QuestionList.Count();
                return RedirectToAction("End", "PlayQuiz", new { quizId });
            }
            List<Question> questionsList = new List<Question>();

            foreach (var q in quiz.QuestionList)
            {
                if (questionIds.Contains(q.Id))
                {
                    questionsList.Add(q);
                }
            }

            var question = new Question();
            if (TempData["NextQuestions"] != null && questionsList.Count != 0)
            {
                var firstQuestion = questionsList[0];
                question = _unitOfWork.Question.Get(u => u.Id == firstQuestion.Id, includeProperties: "AnswersList");
            }
            else
            {
                question = _unitOfWork.Question.Get(u => u.QuizId == quizId, includeProperties: "AnswersList");
            }


            QuestionVM questionVM = new()
            {
                Question = question,
                Answers = question.AnswersList,
                Quiz = quiz

            };
            return View(questionVM);
        }

        public IActionResult NextQuestion(int? answerId, int? quizId, int? questionId)
        {
            var answer = _unitOfWork.Answer.Get(u => u.Id == answerId, includeProperties: "Question");
            if (answer == null)
            {
                return NotFound();
            }

            var quiz = _unitOfWork.Quiz.Get(u => u.Id == quizId, includeProperties: "QuestionList");
            if (quiz == null)
            {
                return NotFound();
            }

            var orderedQuestions = quiz.QuestionList.OrderBy(u => u.Id).ToList();
            var questionsList = new List<int>();
            foreach (var q in orderedQuestions)
            {
                if (q.Id > questionId)
                {
                    questionsList.Add(q.Id);
                }
            }

            TempData["NextQuestions"] = string.Join(",", questionsList);
            return RedirectToAction("ChooseQuestion", "PLayQuiz", new { quizId, answerId = answerId });
        }


        public IActionResult End(int? quizId)
        {
            var quiz = _unitOfWork.Quiz.Get(u => u.Id == quizId);
            if (quiz == null)
            {
                return NotFound();
            }
         
            int correctAnswers = TempData.Peek("CorrectAnswers") as int? ?? 0;
            int totalAnswers = TempData.Peek("TotalAnswers") as int? ?? 0;

            //var singedInUser = User?.Identity?.Name;

            //if (singedInUser != null && quiz.Event != null) 
            //{
            //    var user = _applicationUserRepository.GetByEmail(singedInUser);
            //     _applicationUserRepository.SetPoints(user.Id,correctAnswers);
               
            //}

            PlayQuizVM playQuizVM = new()
            {
                Quiz = quiz,
                Event = null,
                Question = null,
                answersCorrect = correctAnswers,
                totalAnswers = totalAnswers,
            };
            return View(playQuizVM);
        }



    }
}
