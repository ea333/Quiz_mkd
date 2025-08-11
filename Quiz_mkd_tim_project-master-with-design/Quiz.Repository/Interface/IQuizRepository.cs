using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Interface
{
    public interface IQuizRepository : IRepository<Domain.Domain_Models.Quiz>
    {
        public void Update(Domain.Domain_Models.Quiz quiz);
    }
}
