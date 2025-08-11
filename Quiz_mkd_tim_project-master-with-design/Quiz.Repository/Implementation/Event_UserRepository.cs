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
    public class Event_UserRepository : Repository<Event_User>, IEvent_UserRepository
    {
        private readonly ApplicationDbContext _db;
        public Event_UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Event_User event_User)
        {
            _db.Events_Users.Update(event_User);
        }
    }
}
