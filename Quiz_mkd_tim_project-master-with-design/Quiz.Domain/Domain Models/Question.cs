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
    public class Question
    {
        [Key]
        public int Id { get; set; }


        [Display(Name ="Прашање")]
        [Required]
        public string? Text { get; set; }

        [ValidateNever]
        public TypeQuestion? TypeQuestion  { get; set; }

        [ValidateNever]
        [Display(Name = "Тип на прашање")]
        public int? TypeQuestionId { get; set; }

        public ICollection<Answer>? AnswersList { get; set; }

        [ForeignKey("QuizId")]
        public Quiz? Quiz { get; set; }

        [Display(Name ="Квиз")]       
        
        public int? QuizId { get; set; }
    }
}
