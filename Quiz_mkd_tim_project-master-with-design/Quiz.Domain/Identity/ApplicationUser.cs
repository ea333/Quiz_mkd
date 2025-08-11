using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Quiz.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public string? NameUser { get; set; }
        [Required]
        public string? Surname { get; set; }

        [Required]
        public string? PlaceOfOrigin { get; set; }

      
        public double? Points { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<Event_User>? Event_User { get; set; }

        public ICollection<Category_User>? Category_User { get; set; }

        public ICollection<RangList_User>? RangList_User { get; set; }

        public ICollection<EventPending_User>? EventPending_User { get; set; }

        
    }
}
