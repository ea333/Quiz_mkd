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
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Име на квиз")]
        [Required]
        public string? Name { get; set; }


        [Display(Name = "Прашања")]
        
        public ICollection<Question>? QuestionList { get; set; }


        [Display(Name="File Name")]
        [ValidateNever]
        public string? FileName { get; set; }

        [ValidateNever]
        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }
    }
}
