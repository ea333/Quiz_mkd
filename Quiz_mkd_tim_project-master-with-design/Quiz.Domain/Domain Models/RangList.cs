using Quiz.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Domain.Domain_Models
{
    public class RangList
    {
        [Key]
        public int Id { get; set; }
        
        public Event? Event { get; set; }

        public int? EventId { get; set; }

        public ICollection<RangList_User>? Participants { get; set; }

        public ICollection<Category_User>? Category_Users { get; set; }

        public ICollection<Category_RangList>? Category_RangList { get; set; }

        List<RangList_TotalPointsPerCategory?> RangList_TotalPointsPerCategories { get; set; }

    }
}
