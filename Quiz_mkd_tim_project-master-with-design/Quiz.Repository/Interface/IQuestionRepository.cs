using Quiz.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Interface
{
    public interface IQuestionRepository : IRepository<Question>
    {
        public void Update(Question question);
    }
}
