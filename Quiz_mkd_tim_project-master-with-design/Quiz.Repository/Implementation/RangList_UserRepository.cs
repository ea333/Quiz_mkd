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
    public class RangList_UserRepository : Repository<RangList_User>, IRangList_UserRepository
    {
        private readonly ApplicationDbContext _db;
        public RangList_UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(RangList_User rangList_User)
        {
            _db.RangList_Users.Update(rangList_User);
        }
    }
}
