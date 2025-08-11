using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Domain.Domain_Models
{
    public class TypeQuestion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Тип")]
        public string? Type { get; set; }

        [ValidateNever]
        public ICollection<Question?> QuestionList { get; set; }
    }
}
