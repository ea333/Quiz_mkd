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
    public class Category_RangListRepository : Repository<Category_RangList>, ICategory_RangListRepository
    {
        private readonly ApplicationDbContext _db;
        public Category_RangListRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category_RangList category_RangList)
        {
            _db.Category_RangLists.Update(category_RangList);
        }
    }
}
