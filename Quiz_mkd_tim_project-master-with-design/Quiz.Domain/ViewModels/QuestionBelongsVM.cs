using Quiz.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Domain.ViewModels
{
    public class QuestionBelongsVM
    {
        public Question? Question { get; set; }

        public bool  flagBelongs { get; set; }
    }
}
