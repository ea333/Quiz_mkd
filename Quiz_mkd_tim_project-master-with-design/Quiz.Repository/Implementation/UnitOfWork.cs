//using Quiz.Domain.Domain_Models;
using Quiz.Repository.Data;
using Quiz.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _db;
        public IAnswerRepository Answer { get; private set; }

        public IEvent_UserRepository Event_User { get; private set; }

        public IEventRepository Event { get; private set; }

        public IQuestionRepository Question { get; private set; }

        public IQuizRepository Quiz { get; private set; }

        public ITypeQuestionRepository TypeQuestion { get; private set; }

        public ICategoryRepository Category  { get; private set; }

        public ICategory_UserRepository Category_User { get; private set; }

        public IRangList_UserRepository RangList_User { get; private set; }

        public IRangListRepository RangList { get; private set; }

        public ICategory_RangListRepository Category_RangList { get; private set; }

        public IEventPending_UserRepository EventPending_User { get; private set; }

        public IUserTotalPointsPerCategoryRepository UserTotalPointsPerCategory { get; private set; }

        public IRangList_TotalPointsPerCategoryRepository RangList_TotalPointsPerCategory { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Answer = new AnswerRepository(_db);
            Event_User = new Event_UserRepository(_db);
            Event = new EventRepository(_db);
            Question = new QuestionRepository(_db);
            Quiz = new QuizRepository(_db);
            TypeQuestion = new TypeQuestionRepository(_db);
            Category = new CategoryRepository(_db);
            Category_User = new Category_UserRepository(_db);
            RangList_User = new RangList_UserRepository(_db);
            RangList = new RangListRepository(_db);
            Category_RangList = new Category_RangListRepository(_db);
            EventPending_User = new EventPending_UserRepository(_db);
            UserTotalPointsPerCategory = new UserTotalPointsPerCategoryRepository(_db);
            RangList_TotalPointsPerCategory = new RangList_TotalPointsPerCategoryRepository(_db);

        }



        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
