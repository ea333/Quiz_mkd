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
    public class TypeQuestionRepository : Repository<TypeQuestion>, ITypeQuestionRepository
    {
        private readonly ApplicationDbContext _db;
        public TypeQuestionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(TypeQuestion typeQuestion)
        {
            _db.TypeQuestions.Update(typeQuestion);
        }
    }
}
