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
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly ApplicationDbContext _db;
        public QuestionRepository(ApplicationDbContext db) : base(db)
        {
            _db =db;
        }

        public void Update(Question question)
        {
            _db.Questions.Update(question);
        }
    }
}
