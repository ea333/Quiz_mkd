using Quiz.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Interface
{
    public interface IEventPending_UserRepository : IRepository<EventPending_User>
    {
        public void Update(EventPending_User eventPending_User);
    }
}
