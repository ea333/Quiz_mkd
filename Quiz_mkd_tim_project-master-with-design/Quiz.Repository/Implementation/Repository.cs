using Microsoft.EntityFrameworkCore;
using Quiz.Domain.Domain_Models;
using Quiz.Repository.Data;
using Quiz.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            _db.Answers.Include(u => u.Question).Include(u => u.QuestionId);
            _db.Questions.Include(u => u.Quiz).Include(u => u.QuizId)
                .Include(u => u.TypeQuestion).Include(u => u.TypeQuestionId)
                .Include(u => u.AnswersList);
            _db.Quizes.Include(u => u.QuestionList);
            _db.TypeQuestions.Include(u => u.QuestionList);
            _db.RangLists.Include(u => u.Event).Include(u => u.EventId)
                .Include(u => u.Participants).Include(u => u.Category_Users)
                .Include(u => u.Category_RangList);
            //_db.Events.Include(u => u.Event_User).ThenInclude(u => u.User);
            //_db.ApplicationUsers.Include(u => u.Event_User).ThenInclude(u => u.Event);
            _db.Events_Users.Include(u => u.Event).Include(u => u.EventId)
                .Include(u => u.User).Include(u => u.UserId);
            _db.Category_Users.Include(u => u.Category).Include(u => u.CategoryId)
                                .Include(u => u.User).Include(u => u.UserId);
            _db.RangList_Users.Include(u => u.RangList).Include(u => u.RangListId)
                .Include(u => u.User).Include(u => u.UserId);
            _db.Category_RangLists.Include(u => u.Category).Include(u => u.CategoryId)
                .Include(u => u.RangList).Include(u => u.RangListId);
            _db.EventPending_Users.Include(u => u.Event).Include(u => u.EventId)
                .Include(u => u.User).Include(u => u.UserId);






        }


        public void Add(T entity)
        {
            dbSet.Add(entity);
        }



        public T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty
                    in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.FirstOrDefault();
        }
        public IEnumerable<T?> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in
                 includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.ToList();
        }


        public IEnumerable<T?> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in
                 includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}