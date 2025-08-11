using Microsoft.AspNetCore.Mvc.Rendering;
using Quiz.Domain.Domain_Models;


namespace Quiz.Domain.ViewModels
{
    public class TypeQuestionVM
    {
        public TypeQuestion TypeQuestion { get; set; }

        public IEnumerable<SelectListItem> QuizList  { get; set; }
    }
}
