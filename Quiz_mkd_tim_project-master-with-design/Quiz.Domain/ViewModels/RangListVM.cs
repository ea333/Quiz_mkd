using Quiz.Domain.Domain_Models;
using Quiz.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Domain.ViewModels
{
    public class RangListVM
    {
        public List<RangList_User>? RangListUsers { get; set; }

        public List<Category_RangList>? CategoriesRangList { get; set; }

        public List<Category_User>? CategoryUsers { get; set; }

        public Event? Event { get; set; }

        public ApplicationUser? User { get; set; }
    }
}
