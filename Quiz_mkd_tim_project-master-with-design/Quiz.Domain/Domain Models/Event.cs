using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;


namespace Quiz.Domain.Domain_Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Име на настан")]
        [Required]
        public string? Name { get; set; }

        [Display(Name = "Дескрипција на настан")]
        [Required]
        public string? Description { get; set; }

        [Display(Name = "Дестинација")]
        [Required]
        public string? Destination { get; set; }

        [Display(Name = "Почетен Датум")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime StartDate { get; set; }

        [Display(Name = "Завршен Датум")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime EndDate { get; set; }

        
        [Display(Name = "Категорија")]
        /* [Required(ErrorMessage = "Категоријата е задолжителна.")] */
        public string? Category { get; set; } 

        [Display(Name = "Корисници")]
        [ValidateNever]
        public ICollection<Event_User>? Event_User { get; set; }

        [ValidateNever]
        public ICollection<EventPending_User>? EventPending_User { get; set; }

        [ValidateNever]
        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }

        [ValidateNever]
        public RangList? RangList { get; set; }


    }
}
