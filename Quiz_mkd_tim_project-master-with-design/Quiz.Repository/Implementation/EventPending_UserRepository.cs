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
    public class EventPending_UserRepository : Repository<EventPending_User>, IEventPending_UserRepository
    {

        private readonly ApplicationDbContext _db;
        public EventPending_UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(EventPending_User eventPending_User)
        {
            _db.EventPending_Users.Update(eventPending_User);
        }
    }
}
