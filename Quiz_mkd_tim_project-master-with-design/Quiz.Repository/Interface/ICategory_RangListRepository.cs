using Quiz.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Interface
{
    public interface ICategory_RangListRepository : IRepository<Category_RangList>
    {
        public void Update(Category_RangList category_RangList);
    }
}
