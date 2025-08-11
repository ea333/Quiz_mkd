using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Domain.Domain_Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Име на категорија")]
        public string categoryName { get; set; }

        [ValidateNever]
        public ICollection<Category_User>? Category_User { get; set; }

        [ValidateNever]
        public ICollection<Category_RangList>? Category_RangList { get; set; }

       


    }
}
