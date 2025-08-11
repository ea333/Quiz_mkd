using Quiz.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Domain.Domain_Models
{
    public class EventPending_User
    {
        [Key]
        public int Id { get; set; }

        public Event? Event { get; set; }

        public int? EventId { get; set; }

        public ApplicationUser? User { get; set; }

        public string? UserId { get; set; }
    }
}
