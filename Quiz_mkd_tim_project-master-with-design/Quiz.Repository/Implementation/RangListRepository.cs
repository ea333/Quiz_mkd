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
    public class RangListRepository : Repository<RangList>, IRangListRepository
    {
        private readonly ApplicationDbContext _db;
        public RangListRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(RangList rangList)
        {
            _db.RangLists.Update(rangList);
        }
    }
}
