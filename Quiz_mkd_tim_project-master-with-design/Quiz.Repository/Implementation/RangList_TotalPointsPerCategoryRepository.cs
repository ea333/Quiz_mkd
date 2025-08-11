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
    public class RangList_TotalPointsPerCategoryRepository : Repository<RangList_TotalPointsPerCategory>, IRangList_TotalPointsPerCategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public RangList_TotalPointsPerCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(RangList_TotalPointsPerCategory obj)
        {
            _db.RangList_TotalPointsPerCategories.Update(obj);
        }
    }
}
