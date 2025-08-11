using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Quiz.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Domain.ViewModels
{
    public class AnswerVM
    {
        public Answer Answer { get; set; }

        [ValidateNever]
        public Question Question { get; set; }


    }
}
