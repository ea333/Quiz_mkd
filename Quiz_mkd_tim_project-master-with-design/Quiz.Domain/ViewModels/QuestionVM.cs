using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quiz.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Domain.ViewModels
{
    public class QuestionVM
    {
        public Question Question { get; set; }

        [ValidateNever]
        public IEnumerable<Answer> Answers { get; set; }

        [ValidateNever]
        public Domain_Models.Quiz Quiz { get; set; }

        [ValidateNever]
        public List<SelectListItem?> TypeQuestionList { get; set; }

        [ValidateNever]
        public int? TypeQuestionId { get; set; }

        [ValidateNever]
        public Answer Answer1 { get; set; }

        [ValidateNever]
        public Answer Answer2 { get; set; }

        [ValidateNever]
        public Answer Answer3 { get; set; }

        [ValidateNever]
        public Answer Answer4 { get; set; }
    }
}
