using Quiz.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public void Update(Category category);
    }
}
