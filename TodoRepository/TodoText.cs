using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoRepository
{
    public class TodoText
    {
        [Required]
        [MinLength(1)]
        public string Text { get; set; }
    }
}
