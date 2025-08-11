using Quiz.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Interface
{
    public interface IUnitOfWork
    {
        
        IAnswerRepository Answer { get; }
        IEvent_UserRepository Event_User{ get; }
        IEventRepository Event { get; }
        IQuestionRepository Question { get; }
        IQuizRepository Quiz { get; }
        ITypeQuestionRepository TypeQuestion{ get; }

        ICategoryRepository Category { get; }

        ICategory_UserRepository Category_User{ get; }

        IRangList_UserRepository RangList_User { get; }

        IRangListRepository RangList { get; }

        ICategory_RangListRepository Category_RangList { get; }

        IEventPending_UserRepository EventPending_User { get; }

        IUserTotalPointsPerCategoryRepository UserTotalPointsPerCategory { get; }

        IRangList_TotalPointsPerCategoryRepository RangList_TotalPointsPerCategory { get; }
        void Save();

    }
}
