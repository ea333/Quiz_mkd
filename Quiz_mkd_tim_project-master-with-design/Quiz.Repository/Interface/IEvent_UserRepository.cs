using Quiz.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Interface
{
    public interface IEvent_UserRepository : IRepository<Event_User>
    {
        public void Update(Event_User event_User);
    }
}
