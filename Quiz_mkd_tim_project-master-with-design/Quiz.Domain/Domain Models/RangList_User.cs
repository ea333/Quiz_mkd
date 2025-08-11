using Quiz.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Domain.Domain_Models
{
    public class RangList_User
    {
        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser? User { get; set; }

        public string? UserId { get; set; }

        public virtual RangList? RangList { get; set; }

        public int? RangListId { get; set; }

        public double? Points { get; set; }
    }
}
