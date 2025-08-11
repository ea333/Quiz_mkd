using Quiz.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Domain.Domain_Models
{
    public class UserTotalPointsPerCategory
    {
        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }

        public int? CategoryId { get; set; }

        public double? Points { get; set; }

        List<RangList_TotalPointsPerCategory?> RangList_TotalPointsPerCategories { get; set; }

    }
}
