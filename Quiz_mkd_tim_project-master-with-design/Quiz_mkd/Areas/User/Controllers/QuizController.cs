using Microsoft.AspNetCore.Mvc;
using Quiz.Domain.ViewModels;
using Quiz.Repository.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quiz.Domain.Domain_Models;
using ExcelDataReader;
using Microsoft.Extensions.Logging;
using Quiz.Utility;
namespace Quiz.Web.Areas.User.Controllers
{
    [Area("User")]
    public class QuizController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public QuizController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var items = _unitOfWork.Quiz.GetAll(includeProperties: "QuestionList");
            items = items.ToList();
            return View(items);
        }

    }
}
