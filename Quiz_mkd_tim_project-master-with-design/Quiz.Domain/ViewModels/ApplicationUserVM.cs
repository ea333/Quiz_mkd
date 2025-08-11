using Quiz.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Domain.ViewModels
{
    public class ApplicationUserVM
    {
        public ApplicationUser? User { get; set; }

        public string? Role { get; set; }

        public int? EventId { get; set; }

        public List<ApplicationUser>? UsersList { get; set; }
    }
}
