using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Domain.ViewModels
{
    public class ShowRangListOptionsVM
    {

        public IEnumerable<SelectListItem>? Events { get; set; }

        public IEnumerable<SelectListItem>? Categories { get; set; }

        public IEnumerable<SelectListItem>? EventsByYear { get; set; }
        //total points in that year alone for every event if  this is selected get sumn for all of the other Events and Categories

        public RangListVM? ShowRangList { get; set; }

        public int? SelectedEventId { get; set; }

        public int? SelectedCategoryId { get; set; }

        public string? SelectedYear { get; set; }
    }
}
