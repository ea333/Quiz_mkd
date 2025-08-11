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
    public class Category_UserRepository : Repository<Category_User>, ICategory_UserRepository
    {
        private readonly ApplicationDbContext _db;
        public Category_UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category_User category_User)
        {
           _db.Category_Users.Update(category_User);
        }
    }
}
