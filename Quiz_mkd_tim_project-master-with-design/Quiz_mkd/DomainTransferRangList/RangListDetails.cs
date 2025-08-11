using Quiz.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Repository.Domain_Transfer
{
    public class RangListDetails
    {
        public List<RangList_User> rangListUsers = new List<RangList_User>();
        public List<Category_RangList> categoryRangList = new List<Category_RangList>();
        public List<Category_User> categoryUsersForView = new List<Category_User>();
    }
}
