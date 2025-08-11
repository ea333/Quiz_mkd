using Quiz.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Domain.ViewModels
{
    public class PlayQuizVM
    {
        public Domain_Models.Quiz? Quiz { get; set; }

        public Event? Event { get; set; }

        public Question? Question { get; set; }

        public int? answersCorrect { get; set; }
        public int? totalAnswers { get; set; }

        public int? timeToAnswerQuestion { get; set; }
    }
}
