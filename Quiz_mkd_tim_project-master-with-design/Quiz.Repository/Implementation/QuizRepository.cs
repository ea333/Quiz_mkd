using Quiz.Repository.Data;
using Quiz.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Implementation
{
    public class QuizRepository : Repository<Domain.Domain_Models.Quiz>, IQuizRepository
    {
        private readonly ApplicationDbContext _db;
        public QuizRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Domain.Domain_Models.Quiz quiz)
        {
            _db.Quizes.Update(quiz);
        }
    }
}
