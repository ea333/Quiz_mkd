using Quiz.Domain.Domain_Models;
using Quiz.Repository.Data;
using Quiz.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Implementation
{
    public class AnswerRepository : Repository<Answer>, IAnswerRepository
    {
        private readonly ApplicationDbContext _db;
        public AnswerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool CheckAnswerCorrect(Answer answer)
        {
            return answer.isCorrect;
        }

        public void Update(Answer answer)
        {
            _db.Answers.Update(answer);
        }
    }
}
