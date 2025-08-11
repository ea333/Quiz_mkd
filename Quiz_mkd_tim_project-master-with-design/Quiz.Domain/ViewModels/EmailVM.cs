using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Domain.ViewModels
{
    public class EmailVM
    {
        public string? email { get; set; }

        public string? subject { get; set; }

        public string? htmlMessage { get; set; }

        public bool emailsSent { get; set; } = false;
    }
}
