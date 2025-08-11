using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quiz.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Domain.ViewModels
{
    public class QuizVM

    {
        public Domain_Models.Quiz Quiz { get; set; }

        public int? QuizId { get; set; }


        [ValidateNever]
        public IEnumerable<Question?> QuestionList { get; set; }

        [ValidateNever]
        public int? count { get; set; }

        [ValidateNever]
        public List<QuestionBelongsVM?> QuestionListBelongs { get; set; }

        [ValidateNever]
        public List<SelectListItem?> TypeQuestions { get; set; }

        public int? SelectedTypeQuestionId { get; set; }


    }
}
