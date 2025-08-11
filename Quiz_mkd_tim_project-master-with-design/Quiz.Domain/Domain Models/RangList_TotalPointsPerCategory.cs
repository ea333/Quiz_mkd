using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Domain.Domain_Models
{
    public class RangList_TotalPointsPerCategory
    {
        [Key]
        public int Id { get; set; }

        public RangList? RangList { get; set; }

        public int? RangListId { get; set; }

        public UserTotalPointsPerCategory? UserTotalPointsPerCategory { get; set; }
        public int? UserTotalPointsPerCategoryId { get; set; }


    }
}
