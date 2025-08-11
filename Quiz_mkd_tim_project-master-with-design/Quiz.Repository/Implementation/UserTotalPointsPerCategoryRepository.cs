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
    public class UserTotalPointsPerCategoryRepository : Repository<UserTotalPointsPerCategory>, IUserTotalPointsPerCategoryRepository
    {
        private readonly ApplicationDbContext _db;
         
        public UserTotalPointsPerCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(UserTotalPointsPerCategory userTotalPointsPerCategory)
        {
            _db.UserTotalPointsPerCategories.Update(userTotalPointsPerCategory);
        }
    }
}
