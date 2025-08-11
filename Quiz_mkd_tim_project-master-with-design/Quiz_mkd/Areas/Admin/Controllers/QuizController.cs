using Microsoft.AspNetCore.Mvc;
using Quiz.Domain.ViewModels;
using Quiz.Repository.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quiz.Domain.Domain_Models;
using ExcelDataReader;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Quiz.Utility;
namespace Quiz.Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class QuizController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public QuizController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

     

        public IActionResult Create()
        {
            QuizVM quizVM = new()
            {
                Quiz = new Domain.Domain_Models.Quiz(),
               
                QuestionList = new List<Question>()
            };
            return View(quizVM);

        }

        [HttpPost]
        public IActionResult Create(QuizVM quizVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string quizPath = Path.Combine(wwwRootPath, @"images\quiz");

                    using (var fileStream = new FileStream(Path.Combine(quizPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    quizVM.Quiz.ImageUrl = @"\images\quiz\" + fileName;
                }
                _unitOfWork.Quiz.Add(quizVM.Quiz);
                _unitOfWork.Save();
             
                quizVM.QuestionList = new List<Question>();
                return View(quizVM);

            }

            QuizVM vm = new()
            {
                Quiz = new Domain.Domain_Models.Quiz(),
                
                QuestionList = new List<Question>()
            };
            return View(vm);

        }

        public IActionResult Delete(int? id)
        {
            var quiz = _unitOfWork.Quiz.Get(u => u.Id == id);

            return View(quiz);
        }

        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteDB(int? id)
        {
            if (id == 0 && id == null)
            {
                return NotFound();
            }
            var quiz = _unitOfWork.Quiz.Get(u => u.Id == id);

            if (quiz == null)
            {
                return NotFound();
            }
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (quiz.ImageUrl != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(quiz.ImageUrl);
                string quizPath = Path.Combine(wwwRootPath, @"images\event");
                if (!string.IsNullOrEmpty(quiz.ImageUrl))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, quiz.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
            }

            _unitOfWork.Quiz.Remove(quiz);
            _unitOfWork.Save();
            return RedirectToAction("Index", "Quiz", new { area="User"});
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var quiz = _unitOfWork.Quiz.Get(u => u.Id == id, includeProperties: "QuestionList");

            if (quiz == null)
            {
                return NotFound();
            }

            QuizVM quizVM = new()
            {
                Quiz = quiz,
              
                QuestionList = new List<Question>()
            };
            return View(quizVM);


        }

        [HttpPost]
        public IActionResult Edit(QuizVM quizVM, IFormFile? file)
        {
            if (quizVM.Quiz == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string quizPath = Path.Combine(wwwRootPath, @"images\quiz");
                    if (!string.IsNullOrEmpty(quizVM.Quiz.ImageUrl))
                    {
                        //delet the old image
                        var oldImagePath = Path.Combine(wwwRootPath, quizVM.Quiz.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }


                    using (var fileStream = new FileStream(Path.Combine(quizPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    quizVM.Quiz.ImageUrl = @"\images\quiz\" + fileName;
                }
                _unitOfWork.Quiz.Update(quizVM.Quiz);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Quiz", new { area="User"});

            }


            return View(quizVM);


        }

        public IActionResult Detail(int? quizId)
        {
            var item = _unitOfWork.Quiz.Get(u => u.Id == quizId, includeProperties: "QuestionList");
            if (item == null)
            {
                return NotFound();
            }

            QuizVM quizVM = new()
            {
                Quiz = item,
               

                QuestionList = _unitOfWork.Question.GetAll(includeProperties: "Quiz,AnswersList")
                .Where(u => u.QuizId == item.Id)


            };
            return View(quizVM);
        }

        public IActionResult ExcelFileReader(int? quizId)
        {
            var item = _unitOfWork.Quiz.Get(u => u.Id == quizId);
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> ExcelFileReader(IFormFile file, int quizId)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            //Upload File
            if (file != null && file.Length > 0)
            {

                var uploadDirectory = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads";

                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                var filePath = Path.Combine(uploadDirectory, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var quiz = _unitOfWork.Quiz.Get(u => u.Id == quizId);
                if (quiz != null)
                {
                    quiz.FileName = file.FileName;
                    _unitOfWork.Quiz.Update(quiz);
                    _unitOfWork.Save();

                }

                //Read File
                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    var excelData = new List<List<object>>();
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {


                        do
                        {
                            bool isHeaderSkipped = false;
                            while (reader.Read())
                            {
                                var rowData = new List<object>();
                                for (int column = 0; column < reader.FieldCount; column++)
                                {
                                    rowData.Add(reader.GetValue(column));
                                }
                                excelData.Add(rowData);

                                if (!isHeaderSkipped)
                                {
                                    isHeaderSkipped = true;
                                    continue;
                                }

                                Question question = new Question();
                                question.Text = reader.GetValue(0).ToString();
                                question.AnswersList = new List<Answer>();
                                question.QuizId = quizId;
                                _unitOfWork.Question.Add(question);
                                _unitOfWork.Save();
                                var q = _unitOfWork.Question.Get(u => u == question);
                                var correctAnswer = reader.GetValue(4).ToString();
                                if (q != null)
                                {
                                    for (int i = 1; i <= 4; i++)
                                    {
                                        Answer answer = new Answer();
                                        answer.QuestionId = q.Id;
                                        answer.Text = reader.GetValue(i).ToString();
                                        if (correctAnswer == answer.Text)
                                        {
                                            answer.isCorrect = true;
                                        }
                                        else
                                        {
                                            answer.isCorrect = false;
                                        }
                                        _unitOfWork.Answer.Add(answer);
                                        _unitOfWork.Save();
                                        q?.AnswersList?.Add(answer);
                                        _unitOfWork.Question.Update(q);
                                        _unitOfWork.Save();
                                    }
                                }

                            }
                        } while (reader.NextResult());

                        ViewBag.ExcelData = excelData;

                    }
                }


            }
            return View();
        }



    }
}
